using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeResolvedAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A169");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            Repostry_ResolvedPeriod_.FErp_ResolvedPeriod_Manage(DLResolvedPeriod);
            txtDateAdd.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            txtDate.Text = txtDateAdd.Text;
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModer);
            ClassAdmin_Arn.FGetRaeesAlShaoon(DLIDRaees);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaees);
            FGetLastRecord();
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetLastRecord()
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            Model_EmployeeResolved_ MER = new Model_EmployeeResolved_();
            MER.IDCheck = "GetLastRecord";
            MER.EmployeeResolvedID = Guid.Empty;
            MER.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MER.Number_Resolved = 0;
            MER.CreatedDate = string.Empty;
            MER.Date_From = string.Empty;
            MER.Date_To = string.Empty;
            MER.Is_Moder_Allow = false;
            MER.Is_Raees_Lagnat_Allow = false;
            MER.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeResolved_ RER = new Repostry_EmployeeResolved_();
            dt = RER.BErp_EmployeeResolved_Manage(MER);
            if (dt.Rows.Count > 0)
                txtNumberLeave.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["Number_Resolved_"]) + 1);
            else
                txtNumberLeave.Text = ClassSaddam.FGetNumberBillStart().ToString();
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
            Model_EmployeeResolved_ MER = new Model_EmployeeResolved_();
            MER.IDCheck = "GetByIDUniq";
            MER.EmployeeResolvedID = new Guid(Request.QueryString["ID"]);
            MER.FinancialYear_Id = Guid.Empty;
            MER.Number_Resolved = 0;
            MER.CreatedDate = string.Empty;
            MER.Date_From = string.Empty;
            MER.Date_To = string.Empty;
            MER.Is_Moder_Allow = false;
            MER.Is_Raees_Lagnat_Allow = false;
            MER.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeResolved_ RER = new Repostry_EmployeeResolved_();
            dt = RER.BErp_EmployeeResolved_Manage(MER);
            if (dt.Rows.Count > 0)
            {
                ddlYears.SelectedValue = dt.Rows[0]["FinancialYear_Id_"].ToString();
                ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                ddlEmployee.Items.Clear(); ddlEmployee.Items.Add(""); ddlEmployee.AppendDataBoundItems = true;
                Repostry_Employee_.FErp_Employee_Master_Manage(new Guid(ddlDepartment.SelectedValue), ddlEmployee);
                ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeId"].ToString();
                FGetPhoneAndEmail(); DLSend.SelectedValue = "No";
                txtNumberLeave.Text = dt.Rows[0]["Number_Resolved_"].ToString();
                txtDate.Text = Convert.ToDateTime(dt.Rows[0]["Date_Resolved_"]).ToString("yyyy-MM-dd");
                DLResolvedPeriod.SelectedValue = dt.Rows[0]["ID_ResolvedPeriod_"].ToString();
                DLModer.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString();
                //DLIDRaees.SelectedValue = dt.Rows[0]["ID_Raees_Lagnat_"].ToString();
                txtDescrption.Text = dt.Rows[0]["Reason"].ToString().Replace("<br />", Environment.NewLine);
                txtDateAdd.Text = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]).ToString("yyyy-MM-dd");
                if (Convert.ToBoolean(dt.Rows[0]["Moder_Raees_"]))
                { RB_Moder.Checked = false; RB_Raees.Checked = true; DLRaees.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); }
                else
                { { RB_Moder.Checked = true; RB_Raees.Checked = false; DLModer.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); } }
            }
        }
        catch (Exception)
        {
            return;
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            if (Convert.ToDateTime(txtDate.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                FEmployeeLeaveCategoryAdd();
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

    private void FEmployeeLeaveCategoryAdd()
    {
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        int XModer = 0, XIDAdd = Test_Saddam.FGetIDUsiq();
        bool XModer_Raees = false; XModer = Convert.ToInt32(DLModer.SelectedValue);
        if (RB_Moder.Checked == false && RB_Raees.Checked)
        { XModer_Raees = true; XModer = Convert.ToInt32(DLRaees.SelectedValue); }
        if (Request.QueryString["ID"] == null)
        {
            Model_EmployeeResolved_ MER = new Model_EmployeeResolved_()
            {
                IDCheck = "Add",
                EmployeeResolvedID = Guid.NewGuid(),
                EmployeeId = new Guid(ddlEmployee.SelectedValue),
                FinancialYear_Id = new Guid(ddlYears.SelectedValue),
                Number_Resolved = Convert.ToInt64(txtNumberLeave.Text.Trim()),
                Date_Resolved = txtDate.Text.Trim(),
                ID_ResolvedPeriod = new Guid(DLResolvedPeriod.SelectedValue),
                Reason = txtDescrption.Text.Trim().Replace(Environment.NewLine, "<br />"),
                Moder_Raees = XModer_Raees,
                ID_Moder = XModer,
                Is_Moder_Allow = false,
                Is_Moder_Not_Allow = false,
                Comments = "0",
                ApplyDate = XDate,
                ID_Raees_Lagnat = XModer,
                Is_Raees_Lagnat_Allow = false,
                Is_Raees_Lagnat_Not_Allow = false,
                Apply_Raees_Lagnat_Date = XDate,
                ApprovedBy = "0",
                CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedBy = XIDAdd,
                ModifiedBy = 0,
                ModifiedDate = XDate,
                IsActive = true
            };
            Repostry_EmployeeResolved_ RER = new Repostry_EmployeeResolved_();
            string Xresult = RER.FErp_EmployeeResolved_Add(MER);
            if (Xresult == "IsExistsNumberAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رقم القرار مستخدم مسبقاً ... ";
                return;
            }
            else if (Xresult == "IsExistsAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                return;
            }
            else if (Xresult == "IsSuccessAdd")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "إضافة",
                   " إضافة حسم لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberLeave.Text.Trim(), XDate);
                if (DLSend.SelectedValue == "Yes")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "لديك قرار حسم لمدة" + "\n" + DLResolvedPeriod.SelectedItem.Text.Replace(".0","") + "\n" + "رقم القرار : " + txtNumberLeave.Text.Trim() + "\n" + "يُرجى المتابعة ,,,", "BerArn", "Add", XIDAdd);

                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة حسم لموظف" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Add", XIDAdd);
            }
        }
        else if (Request.QueryString["ID"] != null)
        {
            Model_EmployeeResolved_ MER = new Model_EmployeeResolved_()
            {
                IDCheck = "Edit",
                EmployeeResolvedID = new Guid(Request.QueryString["ID"]),
                EmployeeId = new Guid(ddlEmployee.SelectedValue),
                FinancialYear_Id = new Guid(ddlYears.SelectedValue),
                Number_Resolved = Convert.ToInt64(txtNumberLeave.Text.Trim()),
                Date_Resolved = txtDate.Text.Trim(),
                ID_ResolvedPeriod = new Guid(DLResolvedPeriod.SelectedValue),
                Reason = txtDescrption.Text.Trim().Replace(Environment.NewLine, "<br />"),
                Moder_Raees = XModer_Raees,
                ID_Moder = XModer,
                Is_Moder_Allow = false,
                Is_Moder_Not_Allow = false,
                Comments = string.Empty,
                ApplyDate = XDate,
                ID_Raees_Lagnat = XModer,
                Is_Raees_Lagnat_Allow = false,
                Is_Raees_Lagnat_Not_Allow = false,
                Apply_Raees_Lagnat_Date = XDate,
                ApprovedBy = "0",
                CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedBy = 0,
                ModifiedBy = XIDAdd,
                ModifiedDate = XDate,
                IsActive = true
            };
            Repostry_EmployeeResolved_ RER = new Repostry_EmployeeResolved_();
            string Xresult = RER.FErp_EmployeeResolved_Add(MER);
            if (Xresult == "IsExistsNumberEdit")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رقم القرار مستخدم مسبقاً ... ";
                return;
            }
            else if (Xresult == "IsExistsEdit")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                return;
            }
            else if (Xresult == "IsSuccessEdit")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "تعديل",
                   " تعديل حسم لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberLeave.Text.Trim(), XDate);
                if (DLSend.SelectedValue == "Yes")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تعديل قرار حسم" + "\n" + DLResolvedPeriod.SelectedItem.Text.Replace(".0", "") + "\n" + "رقم القرار : " + txtNumberLeave.Text.Trim() + "\n" + "يُرجى المتابعة ,,,", "BerArn", "Edit", XIDAdd);

                lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل إستئذان لموظف" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Edit", XIDAdd);
            }
        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeResolveds.aspx");
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetLastRecord();
        txtDate.Text = Convert.ToDateTime(txtDate.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
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