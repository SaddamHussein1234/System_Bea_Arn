using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Interface.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeSalary_PageEmployeeSalaryByView : System.Web.UI.UserControl
{
    Interface_EmployeePaidSalary_ _IEmployeePaidSalaryService = new Repostry_EmployeePaidSalary_();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlSelect.Visible = true;

            hfId.Value = Guid.Empty.ToString();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            if (Request.QueryString["employeeid"] != null && Request.QueryString["monthyear"] != null)
            {
                txtPaidDate.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
                Guid _EmployeeId;
                bool _Result;
                _Result = Guid.TryParse(Convert.ToString(Request.QueryString["employeeid"]), out _EmployeeId);
                if (_Result)
                {
                    hfEmployeeId.Value = Convert.ToString(_EmployeeId);
                    string _MonthId = Request.QueryString["monthyear"].ToString();
                    if (!string.IsNullOrEmpty(_MonthId))
                    {
                        int _Month = DateTime.Now.Month;
                        int _Year = DateTime.Now.Year;

                        string[] _SplitDate = _MonthId.Split('_');
                        if (_SplitDate.Length > 1)
                        {
                            _Month = Convert.ToInt32(_SplitDate[0]);
                            _Year = Convert.ToInt32(_SplitDate[1]);
                        }

                        hfMonth.Value = Convert.ToString(_Month);
                        lblMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_Month);
                        HMonth.Value = _Month.ToString();
                        lblYear.Text = _Year.ToString();

                        CheckForEmployeePaidSalaryId(_Month.ToString(), _Year.ToString());
                    }
                }
                else
                    Response.Redirect("PageEmployeeSalaryList.aspx", false);
            }
            else
                Response.Redirect("PageEmployeeSalaryList.aspx", false);
        }
    }

    private void CheckForEmployeePaidSalaryId(string XMounth, string XYear)
    {
        try
        {
            Result<Model_EmployeePaidSalary_> _Result = _IEmployeePaidSalaryService.GetEmployeePaidSalaryByEmployeeIds(new Guid(hfEmployeeId.Value), XMounth, Convert.ToInt32(lblYear.Text), new Guid(Request.QueryString["IDYear"].ToString()));

            if (_Result.IsSuccess)
            {
                if (_Result.Data != null)
                {
                    hfId.Value = _Result.Data.EmployeePaidSalaryID.ToString();
                    lblDepartment.Text = _Result.Data.Department;
                    lblEmployeeName.Text = _Result.Data.FullName;
                    lblEmployeeNo.Text = _Result.Data.EmployeeNo.ToString();
                    lblTotalDays.Text = Convert.ToString(_Result.Data.TotalDays);
                    HFTotalHolidays.Value = Convert.ToString(_Result.Data.TotalHolidays);
                    lblTotalHolidays.Text = HFTotalHolidays.Value;
                    hfAllowLeave.Value = Convert.ToString(_Result.Data.AllowLeave);
                    hfTotalUsedLeave.Value = Convert.ToString(_Result.Data.TotalUseLeave);
                    hfCalculateLeave.Value = Convert.ToString(_Result.Data.TotalPaidLeave);

                    lblLeave.Text = hfTotalUsedLeave.Value;

                    lblTotalPresentDays.Text = Convert.ToString(_Result.Data.TotalPresentDays);
                    lblTotalOverTimeDays.Text = Convert.ToString(_Result.Data.TotalOverTimeDays);
                    lblTotalOverTimeSalary.Text = Convert.ToString(_Result.Data.TotalOverTimeAmount);
                    lblTotalPaidLeaveSalary.Text = Convert.ToString(_Result.Data.TotalPaidLeaveAmount);

                    if (_Result.Data.TotalOverTimeAmount > 0)
                        lblTotalOverTimeSalary.CssClass = "greenFont";

                    if (_Result.Data.PaidBasic > _Result.Data.Basic)
                        _Result.Data.PaidBasic = _Result.Data.Basic;

                    lblBasic.Text = Convert.ToString(_Result.Data.Basic);
                    lblPaidBasic.Text = Convert.ToString(_Result.Data.PaidBasic);
                    lblTotalAllowance.Text = Convert.ToString(_Result.Data.TotalEarning);
                    lblPaidTotalAllowance.Text = Convert.ToString(_Result.Data.PaidTotalEarning);
                    lblTotalDeduction.Text = Convert.ToString(_Result.Data.TotalDeduction);
                    lblPaidTotalDeduction.Text = Convert.ToString(_Result.Data.PaidTotalDeduction);

                    int _TotalDaysOfMonth = DateTime.DaysInMonth(Convert.ToInt32(lblYear.Text), Convert.ToInt32(hfMonth.Value));
                    FGetEmployeeAllowanceMap(_Result.Data.EmployeeId, _Result.Data.TotalPresentDays, _Result.Data.TotalHolidays, _Result.Data.Total_WeeklyOff, _TotalDaysOfMonth);
                    FillEmployeePaidLoanByEmployeeId(_Result.Data.EmployeeId, _Result.Data.Month, _Result.Data.Year);

                    lblNetSalary.Text = Convert.ToString(_Result.Data.TotalSalary);

                    decimal _CalculateSalary = Convert.ToDecimal(_Result.Data.PaidBasic) + Convert.ToDecimal(lblPaidTotalAllowance.Text);
                    decimal _ProfessinalTax = _Result.Data.ProfessionalTax;

                    _CalculateSalary = _CalculateSalary - _ProfessinalTax;
                    lblProfessionalTax.Text = String.Format("{0:0.00}", _ProfessinalTax);
                    hfCalculateSalary.Value = String.Format("{0:0.00}", _CalculateSalary);

                    lblOnHandSalary.Text = Convert.ToString(_Result.Data.PaidTotalSalary);
                    txtOnHandSalary.Text = lblOnHandSalary.Text;

                    txtPaidDate.Text = Convert.ToDateTime(_Result.Data.PaidDate).ToString("yyyy/MM/dd");
                    //chkbxIsPaid.Checked = _Result.Data.IsPaid;
                    lblWeeklyOff.Text = Convert.ToString(_Result.Data.Total_WeeklyOff);
                    lblWorkingHours.Text = Convert.ToString(_Result.Data.Work_Hours);
                    lblOrgWorkingHours.Text = Convert.ToString(_Result.Data.Total_Hours);
                    lblGo.Text = Convert.ToString(_Result.Data.Go_Hours);
                    HFTotalGo.Value = Convert.ToString(_Result.Data.Amount_Go);
                    lblTotalGo.Text = HFTotalGo.Value;
                    HFLeave2.Value = Convert.ToString(_Result.Data.Count_Leave);
                    lblLeave2.Text = HFLeave2.Value;
                    HFMandate.Value = Convert.ToString(_Result.Data.Count_Mandate);
                    lblMandate.Text = HFMandate.Value;
                    HFTotlMandate.Value = Convert.ToString(_Result.Data.Amount_Mandate);
                    lblTotlMandate.Text = HFTotlMandate.Value;
                    HFResolved.Value = Convert.ToString(_Result.Data.Count_Resolved);
                    lblResolved.Text = HFResolved.Value;
                    HFTotalResolved.Value = Convert.ToString(_Result.Data.Amount_Resolved);
                    lblTotalResolved.Text = HFTotalResolved.Value;

                    Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                      "كشف راتب لـ " + lblEmployeeName.Text + " لشهر " + lblMonth.Text + " " + HMonth.Value + "/" + lblYear.Text,
                      ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));

                    string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/ERP/HRAndPayRoll/Transactions/PageEmployeeResolvedByView.aspx?Monthyear=" + Request.QueryString["Monthyear"].ToString() +
                    "&Employeeid=" + Request.QueryString["Employeeid"].ToString() + "&IDYear=" + Request.QueryString["IDYear"].ToString();
                    Class_QRScan.FGetQRCode(code, ImgQRCode);
                    Class_QRScan.FGetQRCode(code, ImgQRCode2);

                    pnlPrint.Visible = true;
                    pnlSelect.Visible = false;
                }
                else
                {
                    pnlPrint.Visible = false;
                    pnlSelect.Visible = true;
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetEmployeeAllowanceMap(Guid XID, decimal TotalPresentDays, decimal TotalHolidays, decimal _WeeklyOff, int CountDay)
    {
        Result<List<Model_Allowance_>> _ResultAllowance = _IEmployeePaidSalaryService.GetEmployeeAllowanceByEmployeePaidSalaryId(Guid.Empty, XID);
        if (_ResultAllowance.IsSuccess)
        {
            decimal _PaidAllowancePerDay;
            foreach (Model_Allowance_ _PaidAllowance in _ResultAllowance.Data)
            {
                if (_PaidAllowance.IsConsider)
                {
                    _PaidAllowancePerDay = 0;
                    _PaidAllowancePerDay = Convert.ToDecimal(_PaidAllowance.Amount) / CountDay;
                    _PaidAllowance.PaidAmount = (TotalPresentDays + TotalHolidays + _WeeklyOff) * _PaidAllowancePerDay;
                }
            }

            rptAllowance.DataSource = _ResultAllowance.Data;
            rptAllowance.DataBind();
        }
        FGetEmployeeDeductionMap(XID, TotalPresentDays, TotalHolidays, _WeeklyOff, CountDay);
    }

    private void FGetEmployeeDeductionMap(Guid XID, decimal TotalPresentDays, decimal TotalHolidays, decimal _WeeklyOff, int CountDay)
    {
        Result<List<Model_Deduction_>> _ResultDeduction = _IEmployeePaidSalaryService.GetEmployeeDeductionByEmployeePaidSalaryId(Guid.Empty, XID);
        decimal _PaidDeductionPerDay;

        foreach (Model_Deduction_ _PaidDeduction in _ResultDeduction.Data)
        {
            _PaidDeduction.PaidAmount = _PaidDeduction.Amount;
            if (_PaidDeduction.IsConsider)
            {
                _PaidDeductionPerDay = 0;
                _PaidDeductionPerDay = Convert.ToDecimal(_PaidDeduction.Amount / CountDay);
                _PaidDeduction.PaidAmount = (TotalPresentDays + TotalHolidays + _WeeklyOff) * _PaidDeductionPerDay;
            }
        }
        rptDeduction.DataSource = _ResultDeduction.Data;
        rptDeduction.DataBind();
    }

    private void FillEmployeePaidLoanByEmployeeId(Guid p_EmployeeId, string p_Month, int p_Year)
    {
        Result<List<Model_EmployeeLoan_>> _EmployeePaidLoanResult = _IEmployeePaidSalaryService.GetEmployeeLoanByEmployeeId(p_EmployeeId, p_Month, p_Year);

        if (_EmployeePaidLoanResult.IsSuccess)
        {
            if (_EmployeePaidLoanResult.Data != null)
            {
                rptLoan.DataSource = _EmployeePaidLoanResult.Data;
                rptLoan.DataBind();
            }
        }
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeSalaryList.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["foot"] = pnl2;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

}