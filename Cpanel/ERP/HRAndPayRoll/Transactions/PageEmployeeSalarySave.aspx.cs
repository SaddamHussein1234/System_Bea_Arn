using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.ERP.Interface.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeSalarySave : System.Web.UI.Page
{
    Interface_EmployeePaidSalary_ _IEmployeePaidSalaryService = new Repostry_EmployeePaidSalary_();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A194");
            hfId.Value = Guid.Empty.ToString();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            if (Request.QueryString["employeeid"] != null && Request.QueryString["monthyear"] != null)
            {
                FGetPhoneAndEmail();
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

                    lblTotalPaidLoanAmount.Text = Convert.ToString(_Result.Data.PaidLoanAmount);

                    lblNetSalary.Text = Convert.ToString(_Result.Data.TotalSalary);

                    decimal _CalculateSalary = Convert.ToDecimal(_Result.Data.PaidBasic) + Convert.ToDecimal(lblPaidTotalAllowance.Text);
                    decimal _ProfessinalTax = _Result.Data.ProfessionalTax;

                    _CalculateSalary = _CalculateSalary - _ProfessinalTax;
                    lblProfessionalTax.Text = String.Format("{0:0.00}", _ProfessinalTax);
                    hfCalculateSalary.Value = String.Format("{0:0.00}", _CalculateSalary);

                    lblOnHandSalary.Text = Convert.ToString(_Result.Data.PaidTotalSalary);
                    txtOnHandSalary.Text = lblOnHandSalary.Text;

                    txtPaidDate.Text = Convert.ToDateTime(_Result.Data.PaidDate).ToString("yyyy/MM/dd");
                    chkbxIsPaid.Checked = _Result.Data.IsPaid;
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

                    if (_Result.Data.IsPaid)
                        btnSave.Visible = false;
                }
                else
                    FillDefaultValues(XMounth, XYear);
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

    private void FillDefaultValues(string _Month , string _Year)
    {
        Guid _EmployeeId = new Guid(hfEmployeeId.Value);

        DateTime _JoinDate = ClassSaddam.GetCurrentTime();
        Model_EmployeeSalary_ MES = new Model_EmployeeSalary_();
        MES.IDCheck = "GetByIDEmp";
        MES.EmployeeSalaryID = _EmployeeId;
        MES.CreatedDate = string.Empty;
        MES.IsActive = true;
        DataTable dt_Emp_S = new DataTable();
        Repostry_EmployeeSalary_ RES = new Repostry_EmployeeSalary_();
        dt_Emp_S = RES.BErp_EmployeeSalary_Manage(MES);
        if (dt_Emp_S.Rows.Count > 0)
        {
            lblDepartment.Text = dt_Emp_S.Rows[0]["Department"].ToString();
            lblEmployeeName.Text = dt_Emp_S.Rows[0]["_Name"].ToString();
            lblEmployeeNo.Text = dt_Emp_S.Rows[0]["EmployeeNo"].ToString();
            lblBasic.Text = dt_Emp_S.Rows[0]["Basic"].ToString();
            lblTotalAllowance.Text = dt_Emp_S.Rows[0]["TotalEarning"].ToString();
            lblTotalDeduction.Text = dt_Emp_S.Rows[0]["TotalDeduction"].ToString();
            lblNetSalary.Text = dt_Emp_S.Rows[0]["TotalSalary"].ToString();
            _JoinDate = Convert.ToDateTime(dt_Emp_S.Rows[0]["JoinDate"]);
        }

        Result<List<Model_EmployeePaidSalary_>> _ListOfPaidSalary = _IEmployeePaidSalaryService.GetLeaveDetailsByEmployeeId(_EmployeeId, new Guid(Request.QueryString["IDYear"].ToString()));

        decimal _UsedLeave = 0;
        decimal _AlreadyPendingLeave = 0;

        if (_ListOfPaidSalary.IsSuccess)
            _UsedLeave = _ListOfPaidSalary.Data.Sum(p => p.TotalUseLeave);

        //Interface_EmployeeLeaveCategory_ _IEmployeeLeaveCategoryService = new Repostry_EmployeeLeaveCategory_();

        //Result<Dashboard> _EmployeeLeaveResult = _IEmployeeLeaveCategoryService.GetTotalEmployeeLeavesByEmployeeId(_EmployeeId, SessionHelper.SessionDetail.FinancialYearId);

        //if (_EmployeeLeaveResult.IsSuccess)
        //{
        //    if (_EmployeeLeaveResult.Data != null)
        //    {
        //        hfAllowLeave.Value = String.Format("{0:0.#}", _EmployeeLeaveResult.Data.NoOfLeavesPerMonth);
        //        _AlreadyPendingLeave = _EmployeeLeaveResult.Data.EmployeeLeave - _UsedLeave;

        //        if (_AlreadyPendingLeave < 0)
        //        {
        //            _AlreadyPendingLeave = 0;
        //        }
        //    }
        //}

        int _TotalDaysOfMonth = DateTime.DaysInMonth(Convert.ToInt32(lblYear.Text), Convert.ToInt32(hfMonth.Value));
        DateTime _StartDate = new DateTime(Convert.ToInt32(lblYear.Text), Convert.ToInt32(hfMonth.Value), 1);
        DateTime _EndDate = _StartDate.AddMonths(1).AddDays(-1);

        int _JoinDay = 0;
        if (_StartDate < _JoinDate)
            _JoinDay = (_StartDate - _JoinDate).Days;

        Interface_EmployeeAttendance_ _IEmployeeAttendanceService = new Repostry_EmployeeAttendance_();
        Result<List<Model_EmployeeAttendances_>> _MA = _IEmployeeAttendanceService.GetEmployeeAttendanceByEmployeeId(_EmployeeId, _StartDate, _EndDate);

        decimal WorkingHours = 0, TotalOverTimeDays = 0, TotalPresentDays = 0, TotalLeaveGo = 0, TotlaMandate = 0, AmountTotlaMandate = 0, TotlaResolved = 0, AmountTotlaResolved = 0;
        int TotalHolidays = 0, _WeeklyOff = 0;
        bool _Flag = true;
        if (_MA.IsSuccess)
        {
            if (_MA.Data != null)
            {
                if (_MA.Data.Count() > 0)
                    _Flag = false;

                _WeeklyOff = _MA.Data.Where(h => h.AttendanceType == Convert.ToInt32(Common.AttendanceType.WeeklyOff)).Count();
                TotalHolidays = _MA.Data.Where(h => h.AttendanceType == Convert.ToInt32(Common.AttendanceType.Holiday)).Count();
                TotalLeaveGo = _MA.Data.Where(h => h.AttendanceType == Convert.ToInt32(Common.AttendanceType.Leave)).Count();
                TotlaMandate = _MA.Data.Where(h => h.AttendanceType == Convert.ToInt32(Common.AttendanceType.Mandate)).Count();
                TotlaResolved = Repostry_EmployeeResolved_.BErp_EmployeeResolved_Manage_SumByEmp(_EmployeeId, new Guid(Request.QueryString["IDYear"]), _Month);
                HFMandate.Value = String.Format("{0:0.#}", TotlaMandate);
                lblMandate.Text = HFMandate.Value;
                HFResolved.Value = String.Format("{0:0.#}", TotlaResolved);
                lblResolved.Text = HFResolved.Value;

                DataTable dt_Mandate = new DataTable();
                dt_Mandate = Repostry_EmployeeMandate_.FGetDataInDataTable("GetByMony", _EmployeeId, new Guid(ddlYears.SelectedValue),
                    0, string.Empty, _Month, _Year, false, false, true);
                if (dt_Mandate.Rows.Count > 0)
                    AmountTotlaMandate = Convert.ToDecimal(dt_Mandate.Rows[0]["Sum_Amount"]);

                HFTotlMandate.Value = String.Format("{0:0.00}", AmountTotlaMandate);
                lblTotlMandate.Text = HFTotlMandate.Value;

                HFTotalHolidays.Value = String.Format("{0:0.#}", TotalHolidays);
                lblTotalHolidays.Text = HFTotalHolidays.Value;
                hfTotalUsedLeave.Value = String.Format("{0:0.#}", _MA.Data.Where(el => el.AttendanceType == Convert.ToInt32(Common.AttendanceType.Leave)).Sum(l => l.Attendance));
                TotalOverTimeDays =  Convert.ToDecimal(_MA.Data.Sum(ot => ot.OverTimeHours));
                lblTotalOverTimeDays.Text = String.Format("{0:0.#}", TotalOverTimeDays);
                lblWeeklyOff.Text = String.Format("{0:0.#}", _WeeklyOff);
                TotalPresentDays = _MA.Data.Count() - (Convert.ToDecimal(hfTotalUsedLeave.Value) + TotalHolidays + Convert.ToDecimal(_WeeklyOff) + TotalLeaveGo);

                HFLeave2.Value = String.Format("{0:0.#}", TotalLeaveGo);
                lblLeave2.Text = HFLeave2.Value;
                lblTotalPresentDays.Text = String.Format("{0:0.#}", TotalPresentDays);
                //WorkingHours = Convert.ToDecimal(_MA.Data.Sum(ot => ot.WorkingHours));
                WorkingHours = (_MA.Data.Where(h => h.AttendanceType == Convert.ToInt32(Common.AttendanceType.Present)).Count() + _MA.Data.Where(h => h.AttendanceType == Convert.ToInt32(Common.AttendanceType.Mandate)).Count()) * 8;
                lblWorkingHours.Text = String.Format("{0:0.#}", WorkingHours);
                lblOrgWorkingHours.Text = String.Format("{0:0.#}", (TotalPresentDays) * 8);
                lblGo.Text = String.Format("{0:0.#}", Convert.ToDecimal(lblOrgWorkingHours.Text) - WorkingHours);
            }
        }

        if (_Flag)
        {
            btnSave.Visible = false;
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "لم يتم إضافة ملف الحضور والغياب للموظف ... ";
        }

        decimal _CalculateLeave = _AlreadyPendingLeave - Convert.ToDecimal(hfTotalUsedLeave.Value);
        hfCalculateLeave.Value = Convert.ToString(_CalculateLeave);

        lblTotalDays.Text = String.Format("{0:0.#}", _TotalDaysOfMonth + _JoinDay);
        lblLeave.Text = String.Format("{0:0.#}", hfTotalUsedLeave.Value);
        
        decimal _BasicPerDay = (Convert.ToDecimal(dt_Emp_S.Rows[0]["Basic"]) / _TotalDaysOfMonth) / 8;
        decimal _PresentDay = _BasicPerDay * WorkingHours;

        AmountTotlaResolved = (TotlaResolved * 8) * _BasicPerDay;
        HFTotalResolved.Value = String.Format("{0:0.00}", AmountTotlaResolved);
        lblTotalResolved.Text = HFTotalResolved.Value;
        decimal _OverTimeSalary = 0;
        _OverTimeSalary = _BasicPerDay * TotalOverTimeDays;
        decimal TotalLeave = (Convert.ToDecimal(hfTotalUsedLeave.Value) * 8) * _BasicPerDay;
        lblTotalPaidLeaveSalary.Text = String.Format("{0:0.#}", TotalLeave);
        decimal TotalPaidBasic = ((TotalHolidays + Convert.ToDecimal(_WeeklyOff)) * 8) * _BasicPerDay;

        decimal XPaidBasic = _PresentDay + TotalPaidBasic + ((TotalLeaveGo * 8) * _BasicPerDay);
        if (XPaidBasic > Convert.ToDecimal(dt_Emp_S.Rows[0]["Basic"]))
            XPaidBasic = Convert.ToDecimal(dt_Emp_S.Rows[0]["Basic"]);

        lblPaidBasic.Text = String.Format("{0:0.#}", XPaidBasic);
        HFTotalGo.Value = String.Format("{0:0.#}", (_BasicPerDay) * Convert.ToDecimal(lblGo.Text));
        lblTotalGo.Text = HFTotalGo.Value;
        decimal TotalAllowance = 0;
        Result<List<Model_Allowance_>> _ResultAllowance = _IEmployeePaidSalaryService.GetEmployeeAllowanceByEmployeePaidSalaryId(Guid.Empty, _EmployeeId);
        if (_ResultAllowance.IsSuccess)
        {
            if (_JoinDay < 0 || _CalculateLeave < 0 || Convert.ToInt32(hfMonth.Value) == 3 || Convert.ToDecimal(lblTotalOverTimeDays.Text) > 0)
            {
                decimal _PaidAllowancePerDay;
                foreach (Model_Allowance_ _PaidAllowance in _ResultAllowance.Data)
                {
                    if (_PaidAllowance.IsConsider)
                    {
                        _PaidAllowancePerDay = 0;
                        _PaidAllowancePerDay = Convert.ToDecimal(_PaidAllowance.Amount) / 30;
                        _PaidAllowance.PaidAmount = (TotalPresentDays + TotalHolidays + _WeeklyOff) * _PaidAllowancePerDay; 
                    }
                }
            }
            
            rptAllowance.DataSource = _ResultAllowance.Data;
            rptAllowance.DataBind();
            TotalAllowance = Convert.ToDecimal(_ResultAllowance.Data.Sum(pa => pa.PaidAmount));
            lblPaidTotalAllowance.Text = String.Format("{0:0.00}", TotalAllowance);
        }

        Result<List<Model_Deduction_>> _ResultDeduction = _IEmployeePaidSalaryService.GetEmployeeDeductionByEmployeePaidSalaryId(Guid.Empty, _EmployeeId);

        if (_ResultDeduction.IsSuccess)
        {
            if (_JoinDay < 0 || _CalculateLeave < 0 || Convert.ToInt32(hfMonth.Value) == 3 || Convert.ToDecimal(lblTotalOverTimeDays.Text) > 0)
            {
                decimal _PaidDeductionPerDay;

                foreach (Model_Deduction_ _PaidDeduction in _ResultDeduction.Data)
                {
                    _PaidDeduction.PaidAmount = _PaidDeduction.Amount;
                    if (_PaidDeduction.IsConsider)
                    {
                        _PaidDeductionPerDay = 0;
                        _PaidDeductionPerDay = Convert.ToDecimal(_PaidDeduction.Amount / 30);
                        _PaidDeduction.PaidAmount = (TotalPresentDays + TotalHolidays + _WeeklyOff) * _PaidDeductionPerDay;
                    }
                }
            }
            rptDeduction.DataSource = _ResultDeduction.Data;
            rptDeduction.DataBind();

            lblPaidTotalDeduction.Text = String.Format("{0:0.00}", _ResultDeduction.Data.Sum(pa => pa.PaidAmount));
        }

        lblTotalOverTimeSalary.Text = String.Format("{0:0.00}", _OverTimeSalary);

        if (_OverTimeSalary > 0)
            lblTotalOverTimeSalary.CssClass = "greenFont";

        FillEmployeePaidLoanByEmployeeId(_EmployeeId, lblMonth.Text, Convert.ToInt32(lblYear.Text));

        decimal _CalculateSalary = (XPaidBasic + TotalAllowance + AmountTotlaMandate + _OverTimeSalary) - (TotalLeave + Convert.ToDecimal(HFTotalGo.Value) + AmountTotlaResolved);
        decimal _ProfessinalTax = 0;
        if (_CalculateSalary >= 6000 && _CalculateSalary < 9000)
            _ProfessinalTax = 80;
        else if (_CalculateSalary >= 9000 && _CalculateSalary < 12000)
            _ProfessinalTax = 150;
        else if (_CalculateSalary >= 12000)
            _ProfessinalTax = 200;

        _CalculateSalary = _CalculateSalary - _ProfessinalTax;
        lblProfessionalTax.Text = String.Format("{0:0.00}", _ProfessinalTax);
        hfCalculateSalary.Value = String.Format("{0:0.00}", (_CalculateSalary - Convert.ToDecimal(lblTotalPaidLoanAmount.Text) - Convert.ToDecimal(lblPaidTotalDeduction.Text)));
        lblOnHandSalary.Text = hfCalculateSalary.Value; txtOnHandSalary.Text = lblOnHandSalary.Text;
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

                lblTotalPaidLoanAmount.Text = String.Format("{0:0.00}", _EmployeePaidLoanResult.Data.Sum(p => p.PaidLoan));
                txtTotalPaidLoanAmount.Text = lblTotalPaidLoanAmount.Text;
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            int XIDAdd = Test_Saddam.FGetIDUsiq();
            Model_EmployeePaidSalary_ MEPS = new Model_EmployeePaidSalary_()
            {
                IDCheck = "Add",
                EmployeePaidSalaryID = Guid.NewGuid(),
                EmployeeId = new Guid(hfEmployeeId.Value),
                Basic = Convert.ToDecimal(lblBasic.Text),
                TotalEarning = Convert.ToDecimal(lblTotalAllowance.Text),
                TotalDeduction = Convert.ToDecimal(lblTotalDeduction.Text),
                TotalSalary = Convert.ToDecimal(lblNetSalary.Text),
                PaidBasic = Convert.ToDecimal(lblPaidBasic.Text),
                PaidTotalEarning = Convert.ToDecimal(lblPaidTotalAllowance.Text),
                PaidTotalDeduction = Convert.ToDecimal(lblPaidTotalDeduction.Text),
                PaidTotalSalary = Convert.ToDecimal(hfCalculateSalary.Value),
                Month = HMonth.Value,
                Year = Convert.ToInt32(lblYear.Text),
                TotalOverTimeDays = Convert.ToDecimal(lblTotalOverTimeDays.Text),
                TotalDays = Convert.ToInt32(lblTotalDays.Text.Trim()),
                AllowLeave = Convert.ToDecimal(hfAllowLeave.Value),
                TotalUseLeave = Convert.ToDecimal(hfTotalUsedLeave.Value),
                TotalHoliday = Convert.ToDecimal(HFTotalHolidays.Value),
                TotalPaidLeave = Convert.ToDecimal(hfCalculateLeave.Value),
                TotalPaidLeaveAmount = Convert.ToDecimal(lblTotalPaidLeaveSalary.Text),
                TotalOverTimeAmount = Convert.ToDecimal(lblTotalOverTimeSalary.Text),
                PaidLoanAmount = Convert.ToDecimal(lblTotalPaidLoanAmount.Text),
                PaidDate = Convert.ToDateTime(txtPaidDate.Text.Trim()).ToString("yyyy-MM-dd"),
                PaidBy = string.Empty,
                CreatedDate = XDate,
                CreatedBy = XIDAdd,
                ModifiedBy = XIDAdd,
                ModifiedDate = XDate,
                IsActive = true,
                IsPaid = chkbxIsPaid.Checked,
                FinancialYearId = new Guid(ddlYears.SelectedValue),
                TotalPresentDays = Convert.ToDecimal(lblTotalPresentDays.Text),
                ProfessionalTax = Convert.ToDecimal(lblProfessionalTax.Text),
                Total_WeeklyOff = Convert.ToDecimal(lblWeeklyOff.Text),
                Work_Hours = Convert.ToDecimal(lblWorkingHours.Text),
                Total_Hours = Convert.ToDecimal(lblOrgWorkingHours.Text),
                Go_Hours = Convert.ToDecimal(HFTotalGo.Value),
                Amount_Go = Convert.ToDecimal(HFTotalGo.Value),
                Count_Leave = Convert.ToDecimal(HFLeave2.Value),
                Count_Mandate = Convert.ToDecimal(HFMandate.Value),
                Amount_Mandate = Convert.ToDecimal(HFTotlMandate.Value),
                Count_Resolved = Convert.ToDecimal(HFResolved.Value),
                Amount_Resolved = Convert.ToDecimal(HFTotalResolved.Value)
            };
            Repostry_EmployeePaidSalary_ REPS = new Repostry_EmployeePaidSalary_();
            string Xresult = REPS.FErp_EmployeePaidSalary_Add(MEPS);
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

                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "إضافة",
                  " تسليم الراتب لـ" + lblEmployeeName.Text + " لشهر " + HMonth.Value + " من سنة " + lblYear.Text, XDate);

                Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تسليم راتب" + "\n" + "شهر :" + HMonth.Value + " لسنة :" + lblYear.Text + "\n" + "يمكنك الإطلاع من حسابك" + "\n" + "شكراً لك ,,,", "BerArn", "Add", XIDAdd);
                
                foreach (RepeaterItem item in rptAllowance.Items)
                {
                    Label lblPaidAllowance = (Label)item.FindControl("lblPaidAllowance");
                    Label lblAllowance = (Label)item.FindControl("lblAllowance");
                    if (lblPaidAllowance != null && lblAllowance != null)
                    {
                        if (!String.IsNullOrEmpty(lblPaidAllowance.Text))
                        {
                            HiddenField hfAllowanceId = (HiddenField)item.FindControl("hfAllowanceId");
                            if (hfAllowanceId != null)
                            {
                                FAllowanceAdd(MEPS.EmployeePaidSalaryID, new Guid(Request.QueryString["Employeeid"])
                                    , new Guid(hfAllowanceId.Value), Convert.ToDecimal(lblAllowance.Text), Convert.ToDecimal(lblPaidAllowance.Text)
                                    , Convert.ToInt32(HMonth.Value), Convert.ToInt32(lblYear.Text), XIDAdd, XDate);
                            }
                        }
                    }
                }

                foreach (RepeaterItem item in rptDeduction.Items)
                {
                    Label lblPaidDeduction = (Label)item.FindControl("lblPaidDeduction");
                    Label lblDeduction = (Label)item.FindControl("lblDeduction");

                    if (lblPaidDeduction != null && lblDeduction != null)
                    {
                        if (!String.IsNullOrEmpty(lblPaidDeduction.Text))
                        {
                            HiddenField hfDeductionId = (HiddenField)item.FindControl("hfDeductionId");
                            if (hfDeductionId != null)
                            {
                                FDeductionAdd(MEPS.EmployeePaidSalaryID, new Guid(Request.QueryString["Employeeid"])
                                    , new Guid(hfDeductionId.Value), Convert.ToDecimal(lblDeduction.Text), Convert.ToDecimal(lblPaidDeduction.Text)
                                    , Convert.ToInt32(HMonth.Value), Convert.ToInt32(lblYear.Text), XIDAdd, XDate);
                            }
                        }
                    }
                }

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
                            FEmployeePaidLoanAdd(new Guid(hfLoanId.Value), _PaidLoan, HMonth.Value, Convert.ToInt32(lblYear.Text), XIDAdd, XDate);
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
                                }
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

    private void FAllowanceAdd(Guid XIDEmployeePaidSalaryId, Guid XIDEmp, Guid XIDAllowance, decimal XAmount, decimal XPaidAmount, 
        int XID_Mounth , int XID_Years, int XIDAdd, string XDate)
    {
        Model_EmployeePaidAllowanceMap_ MEPAM = new Model_EmployeePaidAllowanceMap_()
        {
            IDCheck = "Add",
            EmployeePaidAllowanceMapID = Guid.NewGuid(),
            EmployeePaidSalaryId = XIDEmployeePaidSalaryId,
            EmployeeId = XIDEmp,
            AllowanceId = XIDAllowance,
            Amount = XAmount,
            PaidAmount = XPaidAmount,
            CreatedDate = XDate,
            CreatedBy = XIDAdd,
            ModifiedBy = 0,
            ModifiedDate = XDate,
            IsActive = true,
            ID_Mounth = XID_Mounth,
            ID_Years = XID_Years
        };
        Repostry_EmployeePaidAllowanceMap_ REPAM = new Repostry_EmployeePaidAllowanceMap_();
        string Xresult = REPAM.FErp_EmployeePaidAllowanceMap_Add(MEPAM);
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

    private void FDeductionAdd(Guid XIDEmployeePaidSalaryId, Guid XIDEmp, Guid XIDAllowance, decimal XAmount, decimal XPaidAmount, 
        int XID_Mounth, int XID_Years, int XIDAdd, string XDate)
    {
        Model_EmployeePaidDeductionMap_ MEPDM = new Model_EmployeePaidDeductionMap_()
        {
            IDCheck = "Add",
            EmployeePaidDeductionMapID = Guid.NewGuid(),
            EmployeePaidSalaryId = XIDEmployeePaidSalaryId,
            EmployeeId = XIDEmp,
            DeductionId = XIDAllowance,
            Amount = XAmount,
            PaidAmount = XPaidAmount,
            CreatedDate = XDate,
            CreatedBy = XIDAdd,
            ModifiedBy = 0,
            ModifiedDate = XDate,
            IsActive = true,
            ID_Mounth = XID_Mounth,
            ID_Years = XID_Years
        };
        Repostry_EmployeePaidDeductionMap_ REPDM = new Repostry_EmployeePaidDeductionMap_();
        string Xresult = REPDM.FErp_EmployeePaidDeductionMap_Add(MEPDM);
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

    private void FEmployeePaidLoanAdd(Guid XEmployeeLoanMapId, decimal XPaidAmount, string XMounth, int XYears, int XIDAdd, string XDate)
    {
        Model_EmployeePaidLoan_ MEPL = new Model_EmployeePaidLoan_()
        {
            IDCheck = "Add",
            EmployeePaidLoanMapID = Guid.NewGuid(),
            EmployeeLoanMapId = XEmployeeLoanMapId,
            PaidAmount = XPaidAmount,
            PaidDate = XDate,
            CreatedDate = XDate,
            CreatedBy = XIDAdd,
            ModifiedBy = 0,
            ModifiedDate = XDate,
            IsActive = true,
            Month = XMounth,
            Year = XYears,
            EmployeeID = new Guid(Request.QueryString["Employeeid"]),
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
            btnSave.Visible = false;
        }
    }

    private void FGetPhoneAndEmail()
    {
        Repostry_Employee_.FGetPhoneAndEmail(Request.QueryString["Employeeid"], HFPhone, HFEmail);
        lblPhone.InnerText = " - " + HFPhone.Value;
    }

}