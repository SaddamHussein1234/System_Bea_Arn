using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ERP.Interface.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeLoanRepayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A174");
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            //if (Request.QueryString["ID"] != null)
            //    FGetData();
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeLoanRepayment.aspx");
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
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            int XIDAdd = Test_Saddam.FGetIDUsiq();
            foreach (RepeaterItem item in rptLoan.Items)
            {
                TextBox txtPaidLoanAmount = (TextBox)item.FindControl("txtPaidLoanAmount");
                Label lblPendingLoan = (Label)item.FindControl("lblPendingLoan");
                if (txtPaidLoanAmount != null && lblPendingLoan != null)
                {
                    decimal _PaidLoan = String.IsNullOrEmpty(txtPaidLoanAmount.Text.Trim()) ? 0 : Convert.ToDecimal(txtPaidLoanAmount.Text.Trim());
                    HiddenField hfLoanId = (HiddenField)item.FindControl("hfLoanId");
                    if (hfLoanId != null)
                    {
                        FEmployeePaidLoanAdd(new Guid(hfLoanId.Value), _PaidLoan, ClassSaddam.GetCurrentTime().ToString("MM"), Convert.ToInt32(ClassSaddam.GetCurrentTime().ToString("yyyy")));
                        if ((Convert.ToDecimal(lblPendingLoan.Text) - _PaidLoan) <= 0)
                        {
                            Model_EmployeeLoan_ MEL = new Model_EmployeeLoan_()
                            {
                                IDCheck = "IsComplate",
                                EmployeeLoanMapID = new Guid(hfLoanId.Value),
                                EmployeeId = Guid.Empty,
                                Amount = 0,
                                LoanDate = string.Empty,
                                LoanTitle = string.Empty,
                                Description = string.Empty,
                                ApprovedBy = string.Empty,
                                TotalMonths = 0,
                                CreatedDate = XDate,
                                CreatedBy = XIDAdd,
                                ModifiedBy = XIDAdd,
                                ModifiedDate = XDate,
                                IsActive = false,
                                IsComplete = true,
                                InstallmentMonth = 0,
                            };
                            Repostry_EmployeeLoan_ REL = new Repostry_EmployeeLoan_();
                            string XresultComplate = REL.FErp_EmployeeLoan_Add(MEL);
                            if (XresultComplate == "IsSuccessComplate")
                            {
                                IDMessageWarning.Visible = false;
                                IDMessageSuccess.Visible = true;
                                lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
                                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "إضافة",
                                    " تسديد سلفة لـ" + ddlEmployee.SelectedItem.Text + " المبلغ المسدد " + _PaidLoan.ToString(), XDate);
                            }
                        }
                    }
                }
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

    private void FEmployeePaidLoanAdd(Guid XEmployeeLoanMapId, decimal XPaidAmount, string XMounth, int XYears)
    {
        Model_EmployeePaidLoan_ MEPL = new Model_EmployeePaidLoan_()
        {
            IDCheck = "Add",
            EmployeePaidLoanMapID = Guid.NewGuid(),
            EmployeeLoanMapId = XEmployeeLoanMapId,
            PaidAmount = XPaidAmount,
            PaidDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            ModifiedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsActive = true,
            Month = XMounth,
            Year = XYears,
            EmployeeID = new Guid(ddlEmployee.SelectedValue),
        };
        Repostry_EmployeePaidLoan_ REPL = new Repostry_EmployeePaidLoan_();
        string Xresult = REPL.FErp_EmployeePaidLoan_Add(MEPL);
        if (Xresult == "IsExistsAdd")
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
        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeLoans.aspx");
    }

    private void FillEmployeePaidLoanByEmployeeId(Guid p_EmployeeId, string p_Month, int p_Year)
    {
        Interface_EmployeePaidSalary_ _IEmployeePaidSalaryService = new Repostry_EmployeePaidSalary_();
        Result<List<Model_EmployeeLoan_>> _EmployeePaidLoanResult = _IEmployeePaidSalaryService.GetEmployeeLoanByEmployeeId(p_EmployeeId, p_Month, p_Year);

        if (_EmployeePaidLoanResult.IsSuccess)
        {
            if (_EmployeePaidLoanResult.Data != null)
            {
                rptLoan.DataSource = _EmployeePaidLoanResult.Data;
                rptLoan.DataBind();

                lblTotalPaidLoanAmount.Text = String.Format("{0:0.00}", _EmployeePaidLoanResult.Data.Sum(p => p.PaidLoan));
                txtTotalPaidLoanAmount.Text = lblTotalPaidLoanAmount.Text;
                rptLoan.Visible = true;
                pnlNull.Visible = false;
            }
            else
            {
                rptLoan.Visible = false;
                pnlNull.Visible = true;
                lblTotalPaidLoanAmount.Text = "0";
                txtTotalPaidLoanAmount.Text = "0";
            }
        }
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillEmployeePaidLoanByEmployeeId(new Guid(ddlEmployee.SelectedValue), "", 0);
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

}