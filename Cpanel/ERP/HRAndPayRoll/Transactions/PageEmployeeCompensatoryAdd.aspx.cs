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

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeCompensatoryAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A161");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            txtStartDate.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            txtEndDate.Text = txtStartDate.Text;
            ClassAdmin_Arn.FGetModerAlGmeiah(DLIDModer);
            ClassAdmin_Arn.FGetRaeesAlShaoon(DLIDRaees);
            FGetLastRecord();
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            Model_EmployeeCompensatory_ MEC = new Model_EmployeeCompensatory_();
            MEC.IDCheck = "GetByIDUniq";
            MEC.EmployeeLeaveCompensatoryMapID = new Guid(Request.QueryString["ID"]);
            MEC.FinancialYear_Id = Guid.Empty;
            MEC.Number_Leave = 0;
            MEC.CreatedDate = string.Empty;
            MEC.StartDate = string.Empty;
            MEC.EndDate = string.Empty;
            MEC.Is_Moder_Allow = false;
            MEC.Is_Raees_Lagnat_Allow = false;
            MEC.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeCompensatory_ REC = new Repostry_EmployeeCompensatory_();
            dt = REC.BErp_EmployeeLeaveCompensatory_Manage(MEC);
            if (dt.Rows.Count > 0)
            {
                ddlYears.SelectedValue = dt.Rows[0]["FinancialYear_Id_"].ToString();
                ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                ddlEmployee.Items.Clear(); ddlEmployee.Items.Add(""); ddlEmployee.AppendDataBoundItems = true;
                Repostry_Employee_.FErp_Employee_Master_Manage(new Guid(ddlDepartment.SelectedValue), ddlEmployee);
                ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeId"].ToString();
                FGetPhoneAndEmail(); DLSend.SelectedValue = "No";
                txtNumberLeave.Text = dt.Rows[0]["Number_Leave_"].ToString();
                txtStartDate.Text = Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToString("yyyy-MM-dd");
                txtEndDate.Text = Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("yyyy-MM-dd");
                txtTotalLeave.Text = Convert.ToInt32(dt.Rows[0]["TotalDay"]).ToString();
                DLIDModer.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString();
                DLIDRaees.SelectedValue = dt.Rows[0]["ID_Raees_Lagnat_"].ToString();
                txtDescrption.Text = dt.Rows[0]["Reason"].ToString().Replace("<br />", Environment.NewLine);
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetLastRecord()
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            Model_EmployeeCompensatory_ MEC = new Model_EmployeeCompensatory_();
            MEC.IDCheck = "GetLastRecord";
            MEC.EmployeeLeaveCompensatoryMapID = Guid.Empty;
            MEC.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEC.Number_Leave = 0;
            MEC.CreatedDate = string.Empty;
            MEC.StartDate = string.Empty;
            MEC.EndDate = string.Empty;
            MEC.Is_Moder_Allow = false;
            MEC.Is_Raees_Lagnat_Allow = false;
            MEC.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeCompensatory_ REC = new Repostry_EmployeeCompensatory_();
            dt = REC.BErp_EmployeeLeaveCompensatory_Manage(MEC);
            if (dt.Rows.Count > 0)
                txtNumberLeave.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["Number_Leave_"]) + 1);
            else
                txtNumberLeave.Text = ClassSaddam.FGetNumberBillStart().ToString();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
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
        try
        {
            if (Convert.ToDateTime(txtStartDate.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
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
        int XIDAdd = Test_Saddam.FGetIDUsiq();
        if (Request.QueryString["ID"] == null)
        {
            Model_EmployeeCompensatory_ MEC = new Model_EmployeeCompensatory_()
            {
                IDCheck = "Add",
                EmployeeLeaveCompensatoryMapID = Guid.NewGuid(),
                EmployeeId = new Guid(ddlEmployee.SelectedValue),
                FinancialYear_Id = new Guid(ddlYears.SelectedValue),
                Number_Leave = Convert.ToInt64(txtNumberLeave.Text.Trim()),
                StartDate = txtStartDate.Text.Trim(),
                EndDate = txtEndDate.Text.Trim(),
                TotalDay = Convert.ToDecimal(txtTotalLeave.Text.Trim()),
                Reason = txtDescrption.Text.Trim().Replace(Environment.NewLine, "<br />"),
                ID_Moder = Convert.ToInt32(DLIDModer.SelectedValue),
                Is_Moder_Allow = true,
                Is_Moder_Not_Allow = false,
                Comments = string.Empty,
                ApplyDate = XDate,
                ID_Raees_Lagnat = Convert.ToInt32(DLIDRaees.SelectedValue),
                Is_Raees_Lagnat_Allow = true,
                Is_Raees_Lagnat_Not_Allow = false,
                Apply_Raees_Lagnat_Date = XDate,
                ApprovedBy = "0",
                CreatedDate = XDate,
                CreatedBy = XIDAdd,
                ModifiedBy = 0,
                ModifiedDate = XDate,
                IsActive = true,
                IsApprove = false
            };
            Repostry_EmployeeCompensatory_ REC = new Repostry_EmployeeCompensatory_();
            string Xresult = REC.FErp_EmployeeLeaveCompensatory_Add(MEC);
            if (Xresult == "IsExistsNumberAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رقم الإجازة مستخدم مسبقاً ... ";
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
                   " إضافة إجازة تعويضية لـ" + ddlEmployee.SelectedItem.Text + " ,برقم " + txtNumberLeave.Text.Trim(), XDate);
                if (DLSend.SelectedValue == "Yes")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تم منحك إجازة تعويضية جديدة" + "\n" + "عدد الأيام : " + txtTotalLeave.Text.Trim() + "\n" + "رقم الإجازة : " + txtNumberLeave.Text.Trim() , "BerArn", "Add", XIDAdd);

                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة إجازة تعويضية" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Add", XIDAdd);
            }
        }
        else if (Request.QueryString["ID"] != null)
        {
            Model_EmployeeCompensatory_ MEC = new Model_EmployeeCompensatory_()
            {
                IDCheck = "Edit",
                EmployeeLeaveCompensatoryMapID = new Guid(Request.QueryString["ID"]),
                EmployeeId = new Guid(ddlEmployee.SelectedValue),
                FinancialYear_Id = new Guid(ddlYears.SelectedValue),
                Number_Leave = Convert.ToInt64(txtNumberLeave.Text.Trim()),
                StartDate = txtStartDate.Text.Trim(),
                EndDate = txtEndDate.Text.Trim(),
                TotalDay = Convert.ToDecimal(txtTotalLeave.Text.Trim()),
                Reason = txtDescrption.Text.Trim().Replace(Environment.NewLine, "<br />"),
                ID_Moder = Convert.ToInt32(DLIDModer.SelectedValue),
                Is_Moder_Allow = true,
                Is_Moder_Not_Allow = false,
                Comments = string.Empty,
                ApplyDate = XDate,
                ID_Raees_Lagnat = Convert.ToInt32(DLIDRaees.SelectedValue),
                Is_Raees_Lagnat_Allow = true,
                Is_Raees_Lagnat_Not_Allow = false,
                Apply_Raees_Lagnat_Date = XDate,
                ApprovedBy = "0",
                CreatedDate = XDate,
                CreatedBy = 0,
                ModifiedBy = XIDAdd,
                ModifiedDate = XDate,
                IsActive = true,
                IsApprove = false
            };
            Repostry_EmployeeCompensatory_ REC = new Repostry_EmployeeCompensatory_();
            string Xresult = REC.FErp_EmployeeLeaveCompensatory_Add(MEC);
            if (Xresult == "IsExistsNumberEdit")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رقم الإجازة مستخدم مسبقاً ... ";
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
                   " تعديل إجازة تعويضية لـ" + ddlEmployee.SelectedItem.Text + " ,برقم " + txtNumberLeave.Text.Trim(), XDate);
                if (DLSend.SelectedValue == "Yes")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تم تعديل الإجازة التعويضية" + "\n" + "عدد الأيام : " + txtTotalLeave.Text.Trim() + "\n" + "رقم الإجازة : " + txtNumberLeave.Text.Trim(), "BerArn", "Edit", XIDAdd);
                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل إجازة تعويضية" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Edit", XIDAdd);
            }
        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeCompensatories.aspx");
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetLastRecord();
        txtStartDate.Text = Convert.ToDateTime(txtStartDate.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
        txtEndDate.Text = Convert.ToDateTime(txtEndDate.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetPhoneAndEmail();
    }

    private void FGetPhoneAndEmail()
    {
        Repostry_Employee_.FGetPhoneAndEmail(ddlEmployee.SelectedValue, HFPhone, HFEmail);
        lblPhone.InnerText = HFPhone.Value;
    }

}