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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeLoans_PageEmployeeLoanAdd : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    string IDUniq = string.Empty;
    private void GetCookie()
    {
        try
        {
            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
        }
        catch
        {
            Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (XType == "Manager")
            {
                CLS_Permissions.CheckAccountAdmin("A173");
                IDAccess.Visible = true;
            }
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            txtDateLoan.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModer);
            ClassAdmin_Arn.FGetRaeesAlShaoon(DLIDRaeesShoo);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLIDRaees);
            FGetLastRecord();
            if (XType == "Manager")
            {
                IDDepartment.Visible = true; IDEmployee.Visible = true;
                DLModer.Enabled = true; DLIDRaees.Enabled = true; DLIDRaeesShoo.Enabled = true; DLSend.SelectedValue = "Yes"; DLSend.Enabled = true;
            }
            else if (XType == "Admin")
            {
                IDDepartment.Visible = false; IDEmployee.Visible = false;
                DLModer.Enabled = false; DLIDRaees.Enabled = false; DLIDRaeesShoo.Enabled = false; DLSend.SelectedValue = "Yes"; DLSend.Enabled = false;
                GetCookie();
                FGetPhoneAndEmail(IDUniq);
            }
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetLastRecord()
    {
        try
        {
            Model_EmployeeLoan_ MEL = new Model_EmployeeLoan_();
            MEL.IDCheck = "GetLastRecord";
            MEL.EmployeeLoanMapID = Guid.Empty;
            MEL.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEL.Number_Loan = 0;
            MEL.CreatedDate = string.Empty;
            MEL.StartDate = string.Empty;
            MEL.EndDate = string.Empty;
            MEL.Is_Moder_Allow = false;
            MEL.Is_Raees_Lagnat_Allow = false;
            MEL.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeLoan_ REL = new Repostry_EmployeeLoan_();
            dt = REL.BErp_EmployeeLoan_Manage(MEL);
            if (dt.Rows.Count > 0)
                txtNumberLoan.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["Number_Loan_"]) + 1);
            else
                txtNumberLoan.Text = ClassSaddam.FGetNumberBillStart().ToString();
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
            Model_EmployeeLoan_ MEL = new Model_EmployeeLoan_();
            MEL.IDCheck = "GetByIDUniq";
            MEL.EmployeeLoanMapID = new Guid(Request.QueryString["ID"]);
            MEL.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEL.Number_Loan = 0;
            MEL.CreatedDate = string.Empty;
            MEL.StartDate = string.Empty;
            MEL.EndDate = string.Empty;
            MEL.Is_Moder_Allow = false;
            MEL.Is_Raees_Lagnat_Allow = false;
            MEL.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeLoan_ REL = new Repostry_EmployeeLoan_();
            dt = REL.BErp_EmployeeLoan_Manage(MEL);
            if (dt.Rows.Count > 0)
            {
                ddlYears.SelectedValue = dt.Rows[0]["FinancialYear_Id_"].ToString();
                ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                Repostry_Employee_.FErp_Employee_Master_Manage(new Guid(ddlDepartment.SelectedValue), ddlEmployee);
                ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeId"].ToString();
                FGetPhoneAndEmail(ddlEmployee.SelectedValue); DLSend.SelectedValue = "No";
                txtAmount.Text = dt.Rows[0]["Amount"].ToString();
                txtDateLoan.Text = Convert.ToDateTime(dt.Rows[0]["LoanDate"]).ToString("yyyy-MM-dd");
                txtTitle.Text = dt.Rows[0]["LoanTitle"].ToString();
                txtDescrption.Text = dt.Rows[0]["Description"].ToString();
                ddlTotalMonths.SelectedValue = dt.Rows[0]["TotalMonths"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["Moder_Raees_"]))
                { RB_Moder.Checked = false; RB_Raees.Checked = true; DLIDRaees.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); }
                else
                { { RB_Moder.Checked = true; RB_Raees.Checked = false; DLModer.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); } }
            }
            else
                Response.Redirect("PageEmployeeLoans.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageEmployeeLoans.aspx");
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
            if (Convert.ToDateTime(txtDateLoan.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
            {
                if (XType == "Manager")
                    FEmployeeSalaryAdd(new Guid(ddlEmployee.SelectedValue));
                else if (XType == "Admin")
                {
                    GetCookie();
                    FEmployeeSalaryAdd(new Guid(IDUniq));
                }
            }
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

    private void FEmployeeSalaryAdd(Guid XIDEmp)
    {
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        int XIDAdd = Test_Saddam.FGetIDUsiq(), XModer = 0;
        decimal XAmount = Convert.ToDecimal(txtAmount.Text.Trim());
        bool XModer_Raees = false; XModer = Convert.ToInt32(DLModer.SelectedValue);
        if (RB_Moder.Checked == false && RB_Raees.Checked)
        { XModer_Raees = true; XModer = Convert.ToInt32(DLIDRaees.SelectedValue); }
        if (Request.QueryString["ID"] == null)
        {
            Model_EmployeeLoan_ MEL = new Model_EmployeeLoan_()
            {
                IDCheck = "Add",
                EmployeeLoanMapID = Guid.NewGuid(),
                EmployeeId = XIDEmp,
                FinancialYear_Id = new Guid(ddlYears.SelectedValue),
                Number_Loan = Convert.ToInt64(txtNumberLoan.Text.Trim()),
                Amount = XAmount,
                LoanDate = txtDateLoan.Text.Trim(),
                LoanTitle = txtTitle.Text.Trim(),
                Description = txtDescrption.Text.Trim(),
                ApprovedBy = "hradmin@arityinfoway.com",
                TotalMonths = Convert.ToInt32(ddlTotalMonths.SelectedValue),
                Moder_Raees = XModer_Raees,
                ID_Moder = XModer,
                Is_Moder_Allow = false,
                Is_Moder_Not_Allow = false,
                Comments = string.Empty,
                ApplyDate = XDate,
                ID_Raees_Lagnat = Convert.ToInt32(XModer),
                Is_Raees_Lagnat_Allow = false,
                Is_Raees_Lagnat_Not_Allow = false,
                Apply_Raees_Lagnat_Date = XDate,
                CreatedDate = XDate,
                CreatedBy = XIDAdd,
                ModifiedBy = 0,
                ModifiedDate = XDate,
                IsActive = true,
                IsComplete = false,
                InstallmentMonth = XAmount / Convert.ToDecimal(ddlTotalMonths.SelectedValue),
            };
            Repostry_EmployeeLoan_ REL = new Repostry_EmployeeLoan_();
            string Xresult = REL.FErp_EmployeeLoan_Add(MEL);
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
                   " إضافة سلفة لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberLoan.Text.Trim(), XDate);
                if (DLSend.SelectedValue == "Yes")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تم منحك سلفة بمقدار" + "\n" + txtAmount.Text.Trim() + " " + ClassSaddam.FGetMonySaOutStyle() + "\n" + "تقسط على : " + ddlTotalMonths.SelectedItem.Text + " شهور" + "\n" + "رقم السلفة : " + txtNumberLoan.Text.Trim(), "BerArn", "Add", XIDAdd);

                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة قرض لموظف" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Add", XIDAdd);
            }
        }
        else if (Request.QueryString["ID"] != null)
        {
            Model_EmployeeLoan_ MEL = new Model_EmployeeLoan_()
            {
                IDCheck = "Edit",
                EmployeeLoanMapID = new Guid(Request.QueryString["ID"]),
                EmployeeId = XIDEmp,
                FinancialYear_Id = new Guid(ddlYears.SelectedValue),
                Number_Loan = Convert.ToInt64(txtNumberLoan.Text.Trim()),
                Amount = XAmount,
                LoanDate = txtDateLoan.Text.Trim(),
                LoanTitle = txtTitle.Text.Trim(),
                Description = txtDescrption.Text.Trim(),
                ApprovedBy = "hradmin@arityinfoway.com",
                TotalMonths = Convert.ToInt32(ddlTotalMonths.SelectedValue),
                Moder_Raees = XModer_Raees,
                ID_Moder = XModer,
                Is_Moder_Allow = false,
                Is_Moder_Not_Allow = false,
                Comments = string.Empty,
                ApplyDate = XDate,
                ID_Raees_Lagnat = Convert.ToInt32(XModer),
                Is_Raees_Lagnat_Allow = false,
                Is_Raees_Lagnat_Not_Allow = false,
                Apply_Raees_Lagnat_Date = XDate,
                CreatedDate = XDate,
                CreatedBy = 0,
                ModifiedBy = XIDAdd,
                ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                IsActive = true,
                IsComplete = false,
                InstallmentMonth = XAmount / Convert.ToDecimal(ddlTotalMonths.SelectedValue),
            };
            Repostry_EmployeeLoan_ REL = new Repostry_EmployeeLoan_();
            string Xresult = REL.FErp_EmployeeLoan_Add(MEL);
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
                   " تعديل سلفة لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberLoan.Text.Trim(), XDate);
                if (DLSend.SelectedValue == "Yes")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تعديل سلفة بمقدار" + "\n" + txtAmount.Text.Trim() + " " + ClassSaddam.FGetMonySaOutStyle() + "\n" + "تقسط على : " + ddlTotalMonths.SelectedItem.Text + " شهور" + "\n" + "رقم السلفة : " + txtNumberLoan.Text.Trim(), "BerArn", "Edit", XIDAdd);

                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل إجازة لموظف" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Edit", XIDAdd);
            }
        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeLoans.aspx");
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeLoanAdd.aspx");
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetLastRecord();
        txtDateLoan.Text = Convert.ToDateTime(txtDateLoan.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetPhoneAndEmail(ddlEmployee.SelectedValue);
    }

    private void FGetPhoneAndEmail(string XID)
    {
        Repostry_Employee_.FGetPhoneAndEmail(XID, HFPhone, HFEmail);
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