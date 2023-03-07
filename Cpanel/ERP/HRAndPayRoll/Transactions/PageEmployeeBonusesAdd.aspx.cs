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

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeBonusesAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A187");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            txtDate_Bonuses.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModer);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLIDRaees);
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
            Model_EmployeeBonuses_ MEB = new Model_EmployeeBonuses_();
            MEB.IDCheck = "GetLastRecord";
            MEB.EmployeeBonusesMapID = Guid.Empty;
            MEB.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEB.Number_Bonuses = 0;
            MEB.CreatedDate = string.Empty;
            MEB.Date_From = string.Empty;
            MEB.Date_To = string.Empty;
            MEB.Is_Moder_Allow = false;
            MEB.Is_Raees_Lagnat_Allow = false;
            MEB.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeBonuses_ REB = new Repostry_EmployeeBonuses_();
            dt = REB.BErp_EmployeeBonuses_Manage(MEB);
            if (dt.Rows.Count > 0)
                txtNumberBonuses.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["Number_Bonuses_"]) + 1);
            else
                txtNumberBonuses.Text = ClassSaddam.FGetNumberBillStart().ToString();

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
            Model_EmployeeBonuses_ MEB = new Model_EmployeeBonuses_();
            MEB.IDCheck = "GetByIDUniq";
            MEB.EmployeeBonusesMapID = new Guid(Request.QueryString["ID"]);
            MEB.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEB.Number_Bonuses = 0;
            MEB.CreatedDate = string.Empty;
            MEB.Date_From = string.Empty;
            MEB.Date_To = string.Empty;
            MEB.Is_Moder_Allow = false;
            MEB.Is_Raees_Lagnat_Allow = false;
            MEB.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeBonuses_ REB = new Repostry_EmployeeBonuses_();
            dt = REB.BErp_EmployeeBonuses_Manage(MEB);
            if (dt.Rows.Count > 0)
            {
                ddlYears.SelectedValue = dt.Rows[0]["FinancialYear_Id_"].ToString();
                txtNumberBonuses.Text = dt.Rows[0]["Number_Bonuses_"].ToString();
                ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                Repostry_Employee_.FErp_Employee_Master_Manage(new Guid(ddlDepartment.SelectedValue), ddlEmployee);
                ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeId"].ToString();
                FGetPhoneAndEmail(); DLSend.SelectedValue = "No";
                txtAmount.Text = Convert.ToInt32(dt.Rows[0]["Total_Amount"]).ToString();
                txtDate_Bonuses.Text = Convert.ToDateTime(dt.Rows[0]["Date_Bonuses_"]).ToString("yyyy-MM-dd");
                txtTitle.Text = dt.Rows[0]["BonusesTitle"].ToString();
                txtDescrption.Text = dt.Rows[0]["Description"].ToString().Replace("<br />", Environment.NewLine);
                if (Convert.ToBoolean(dt.Rows[0]["Moder_Raees_"]))
                { RB_Moder.Checked = false; RB_Raees.Checked = true; DLIDRaees.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); }
                else
                { { RB_Moder.Checked = true; RB_Raees.Checked = false; DLModer.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); } }

            }
            else
                Response.Redirect("PageEmployeeBonuses.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageEmployeeBonuses.aspx");
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeBonusesAdd.aspx");
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
            if (Convert.ToDateTime(txtDate_Bonuses.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                FEmployeeOverTime_Add();
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

    private void FEmployeeOverTime_Add()
    {
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        int XIDAdd = Test_Saddam.FGetIDUsiq(), XModer = 0;
        bool XModer_Raees = false; XModer = Convert.ToInt32(DLModer.SelectedValue);
        if (RB_Moder.Checked == false && RB_Raees.Checked)
        { XModer_Raees = true; XModer = Convert.ToInt32(DLIDRaees.SelectedValue); }
        if (Request.QueryString["ID"] == null)
        {
            Model_EmployeeBonuses_ MEB = new Model_EmployeeBonuses_()
            {
                IDCheck = "Add",
                EmployeeBonusesMapID = Guid.NewGuid(),
                EmployeeId = new Guid(ddlEmployee.SelectedValue),
                FinancialYear_Id = new Guid(ddlYears.SelectedValue),
                Number_Bonuses = Convert.ToInt64(txtNumberBonuses.Text.Trim()),
                Total_Amount = Convert.ToInt64(txtAmount.Text.Trim()),
                Date_Bonuses = txtDate_Bonuses.Text.Trim(),
                BonusesTitle = txtTitle.Text.Trim(),
                Description = txtDescrption.Text.Trim().Replace(Environment.NewLine, "<br />"),
                Moder_Raees = XModer_Raees,
                ID_Moder = XModer,
                Is_Moder_Allow = true,
                Is_Moder_Not_Allow = false,
                Comments = string.Empty,
                ApplyDate = XDate,
                ID_Raees_Lagnat = XModer,
                Is_Raees_Lagnat_Allow = false,
                Is_Raees_Lagnat_Not_Allow = false,
                Apply_Raees_Lagnat_Date = XDate,
                Note_Raees = string.Empty,
                CreatedDate = XDate,
                CreatedBy = XIDAdd,
                ModifiedBy = 0,
                ModifiedDate = XDate,
                IsActive = true
            };
            Repostry_EmployeeBonuses_ REB = new Repostry_EmployeeBonuses_();
            string Xresult = REB.FErp_EmployeeBonuses_Add(MEB);
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
                  " إضافة قرار مكافأة لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberBonuses.Text.Trim(), XDate);
                if (DLSend.SelectedValue == "Yes")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تهانينا لديك مكافأة" + "\n" + "المبلغ : " + txtAmount.Text.Trim() + " " + ClassSaddam.FGetMonySaOutStyle() + "\n" + "رقم القرار : " + txtNumberBonuses.Text.Trim() + "\n" + "شكراً لك ,,,", "BerArn", "Add", XIDAdd);

                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة قرار عمل إضافي" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Add", XIDAdd);
            }
        }
        else if (Request.QueryString["ID"] != null)
        {
            Model_EmployeeBonuses_ MEB = new Model_EmployeeBonuses_()
            {
                IDCheck = "Edit",
                EmployeeBonusesMapID = new Guid(Request.QueryString["ID"]),
                EmployeeId = new Guid(ddlEmployee.SelectedValue),
                FinancialYear_Id = new Guid(ddlYears.SelectedValue),
                Number_Bonuses = Convert.ToInt64(txtNumberBonuses.Text.Trim()),
                Total_Amount = Convert.ToInt64(txtAmount.Text.Trim()),
                Date_Bonuses = txtDate_Bonuses.Text.Trim(),
                BonusesTitle = txtTitle.Text.Trim(),
                Description = txtDescrption.Text.Trim().Replace(Environment.NewLine, "<br />"),
                Moder_Raees = XModer_Raees,
                ID_Moder = XModer,
                Is_Moder_Allow = true,
                Is_Moder_Not_Allow = false,
                Comments = string.Empty,
                ApplyDate = XDate,
                ID_Raees_Lagnat = XModer,
                Is_Raees_Lagnat_Allow = false,
                Is_Raees_Lagnat_Not_Allow = false,
                Apply_Raees_Lagnat_Date = XDate,
                Note_Raees = string.Empty,
                CreatedDate = XDate,
                CreatedBy = 0,
                ModifiedBy = XIDAdd,
                ModifiedDate = XDate,
                IsActive = true
            };
            Repostry_EmployeeBonuses_ REB = new Repostry_EmployeeBonuses_();
            string Xresult = REB.FErp_EmployeeBonuses_Add(MEB);
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
                lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "تعديل",
                   " تعديل قرار مكافأة لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberBonuses.Text.Trim(), XDate);
                if (DLSend.SelectedValue == "Yes")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تعديل المكافأة" + "\n" + "المبلغ : " + txtAmount.Text.Trim() + " " + ClassSaddam.FGetMonySaOutStyle() + "\n" + "رقم القرار : " + txtNumberBonuses.Text.Trim() + "\n" + "شكراً لك ,,,", "BerArn", "Edit", XIDAdd);
                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل قرار عمل إضافي" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Edit", XIDAdd);
            }
        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeBonuses.aspx");
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetLastRecord();
        txtDate_Bonuses.Text = Convert.ToDateTime(txtDate_Bonuses.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
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