using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeMandateAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A179");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            txtStartDate.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            txtEndDate.Text = txtStartDate.Text;
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModer);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLIDRaees);
            FGetLastRecord();
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetLastRecord()
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            DataTable dt = new DataTable();
            dt = Repostry_EmployeeMandate_.FGetDataInDataTable("GetLastRecord", Guid.Empty, new Guid(ddlYears.SelectedValue), 0,
                string.Empty, string.Empty, string.Empty, false, false, true);
            if (dt.Rows.Count > 0)
                txtNumberMandate.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["Number_Mandate_"]) + 1);
            else
                txtNumberMandate.Text = ClassSaddam.FGetNumberBillStart().ToString();

        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Repostry_EmployeeMandate_.FGetDataInDataTable("GetByIDUniq", new Guid(Request.QueryString["ID"]),
                new Guid(ddlYears.SelectedValue), 0, string.Empty, string.Empty, string.Empty, false, false, true);
            if (dt.Rows.Count > 0)
            {
                ddlYears.SelectedValue = dt.Rows[0]["FinancialYear_Id_"].ToString();
                txtNumberMandate.Text = dt.Rows[0]["Number_Mandate_"].ToString();
                ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                Repostry_Employee_.FErp_Employee_Master_Manage(new Guid(ddlDepartment.SelectedValue), ddlEmployee);
                ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeId"].ToString();
                FGetPhoneAndEmail(); DLSend.SelectedValue = "No";
                txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                txtStartDate.Text = Convert.ToDateTime(dt.Rows[0]["Start_Date_"]).ToString("yyyy-MM-dd");
                txtEndDate.Text = Convert.ToDateTime(dt.Rows[0]["End_Date_"]).ToString("yyyy-MM-dd");
                txtTitle.Text = dt.Rows[0]["MandateTitle"].ToString();
                txtDescrption.Text = dt.Rows[0]["Description"].ToString().Replace("<br />", Environment.NewLine);
                ddlTotalDays.SelectedValue = dt.Rows[0]["TotalDays"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["Moder_Raees_"]))
                { RB_Moder.Checked = false; RB_Raees.Checked = true; DLIDRaees.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); }
                else
                { { RB_Moder.Checked = true; RB_Raees.Checked = false; DLModer.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); } }
            }
            else
                Response.Redirect("PageEmployeeMandates.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageEmployeeMandates.aspx");
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            ddlEmployee.Items.Clear(); ddlEmployee.Items.Add(""); ddlEmployee.AppendDataBoundItems = true;
            Repostry_Employee_.FErp_Employee_Master_Manage(new Guid(ddlDepartment.SelectedValue), ddlEmployee);
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeMandateAdd.aspx");        
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            if (Convert.ToDateTime(txtStartDate.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                FEmployeeMandate_Add();
            else
            {
                lblWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FEmployeeMandate_Add()
    {
        int XIDAdd = 0, XUpdate = 0, XModer = 0;
        Guid XID = Guid.Empty;
        string XCheck = string.Empty, Xresult = string.Empty;
        decimal XAmount = Convert.ToDecimal(txtAmount.Text.Trim());
        decimal XTotalAmount = Convert.ToDecimal(txtAmount.Text.Trim()) * Convert.ToDecimal(ddlTotalDays.SelectedValue);
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        bool XModer_Raees = false; XModer = Convert.ToInt32(DLModer.SelectedValue);
        if (RB_Moder.Checked == false && RB_Raees.Checked)
        { XModer_Raees = true; XModer = Convert.ToInt32(DLIDRaees.SelectedValue); }
        if (Request.QueryString["ID"] == null)
        {
            XCheck = "Add"; XID = Guid.NewGuid(); XIDAdd = Test_Saddam.FGetIDUsiq();
        }
        if (Request.QueryString["ID"] != null)
        {
            XCheck = "Edit"; XID = new Guid(Request.QueryString["ID"]); XUpdate = Test_Saddam.FGetIDUsiq();
        }
        Xresult = Repostry_EmployeeMandate_.FAPP(XCheck, XID, new Guid(ddlEmployee.SelectedValue), new Guid(ddlYears.SelectedValue),
             Convert.ToInt64(txtNumberMandate.Text.Trim()),
                 XAmount, XTotalAmount, txtStartDate.Text.Trim(), txtEndDate.Text.Trim(), txtTitle.Text.Trim(),
                 txtDescrption.Text.Trim().Replace(Environment.NewLine, "<br />"), Convert.ToInt32(ddlTotalDays.SelectedValue),
                 XModer_Raees, XModer, true, false, string.Empty, XDate, XModer,
                 false, false, XDate, string.Empty, XIDAdd, XUpdate, 0, XDate, true, false);

        if (Xresult == "IsExistsNumber")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "رقم الإجازة مستخدم مسبقاً ... ";
            return;
        }
        else if (Xresult == "IsExists")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
            return;
        }
        else if (Xresult == "IsSuccess")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";

            if (Request.QueryString["ID"] == null)
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "إضافة",
                   " إضافة إنتداب لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberMandate.Text.Trim(), XDate);

                if (DLSend.SelectedValue == "Yes")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "لديك إنتداب" + "\n" + "لمدة :" + ddlTotalDays.Text.Trim() + " أيام" + "\n" + "الإجمالي :" + String.Format("{0:0.#}", XTotalAmount) + " " + ClassSaddam.FGetMonySaOutStyle() + "\n" + "برقم :" + txtNumberMandate.Text.Trim() + "\n" + "يُرجى المتابعة", "BerArn", "Add", Test_Saddam.FGetIDUsiq());

                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة إنتداب لموظف" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Add", Test_Saddam.FGetIDUsiq());
            }
            if (Request.QueryString["ID"] != null)
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "تعديل",
                      " تعديل إنتداب لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberMandate.Text.Trim(), XDate);

                if (DLSend.SelectedValue == "Yes")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تعديل إنتداب" + "\n" + "لمدة :" + ddlTotalDays.Text.Trim() + " أيام" + "\n" + "الإجمالي :" + String.Format("{0:0.#}", XTotalAmount) + " " + ClassSaddam.FGetMonySaOutStyle() + "\n" + "برقم :" + txtNumberMandate.Text.Trim() + "\n" + "يُرجى المتابعة", "BerArn", "Edit", Test_Saddam.FGetIDUsiq());

                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل إنتداب لموظف" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Edit", Test_Saddam.FGetIDUsiq());
            }

        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeMandates.aspx");
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetLastRecord();
        txtStartDate.Text = Convert.ToDateTime(txtStartDate.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
        txtEndDate.Text = Convert.ToDateTime(txtEndDate.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetPhoneAndEmail();
    }

    private void FGetPhoneAndEmail()
    {
        Repostry_Employee_.FGetPhoneAndEmail(ddlEmployee.SelectedValue, HFPhone, HFEmail);
        lblPhone.InnerText = HFPhone.Value;
    }

    public string FCheck(string XCheck)
    {
        string XResult = "display:none;";
        if (XCheck == "_Moder")
        {
            if (RB_Moder.Checked)
                XResult = "display:block;";
            else if (RB_Moder.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "_Raees")
        {
            if (RB_Raees.Checked)
                XResult = "display:block;";
            else if (RB_Raees.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        return XResult;
    }

}