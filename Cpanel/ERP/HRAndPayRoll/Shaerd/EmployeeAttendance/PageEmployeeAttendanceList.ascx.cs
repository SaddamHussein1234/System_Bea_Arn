using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ERP.Interface.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeAttendance_PageEmployeeAttendanceList : System.Web.UI.UserControl
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
                    CLS_Permissions.CheckAccountAdmin("A189");
                    IDDepartment.Visible = true; IDEmployee.Visible = true;
            }
            else if (XType == "Admin")
            {
                IDDepartment.Visible = false; IDEmployee.Visible = false;
            }

            pnlSelect.Visible = true;
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FillMonth();
        }
    }

    private void FillMonth()
    {
        int _FinancialYear = Convert.ToInt32(ddlYears.SelectedItem.ToString());
        int _no = 0;

        bool _Flag = true;
        for (int no = 1; no < 13; no++)
        {
            _no = _no + 1;
            ddlMonth.Items.Insert(_no, new ListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(no) + " " + no.ToString() + "-" + _FinancialYear, Value = Convert.ToString(no) + "_" + _FinancialYear });

            if (no == ClassSaddam.GetCurrentTime().Month && _FinancialYear == ClassSaddam.GetCurrentTime().Year)
            {
                _Flag = false;
                break;
            }
        }

    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeAttendanceList.aspx");
    }

    private void FGetData()
    {
        GetCookie();
        try
        {
            string XIDEmp = string.Empty;
            if (XType == "Manager")
            {
                XIDEmp = ddlEmployee.SelectedValue;
                txtTitle.Text = "كشف حضور الموظف " + ddlEmployee.SelectedItem.ToString() + " لشهر " + ddlMonth.SelectedItem.ToString();
            }
            else if (XType == "Admin")
            {
                XIDEmp = IDUniq;
                txtTitle.Text = "كشف حضور لشهر " + ddlMonth.SelectedItem.ToString();
            }
            
            string _MonthId = ddlMonth.SelectedValue;
            if (!string.IsNullOrEmpty(_MonthId) && !string.IsNullOrEmpty(XIDEmp))
            {
                int _Month = ClassSaddam.GetCurrentTime().Month;
                int _Year = ClassSaddam.GetCurrentTime().Year;

                string[] _SplitDate = ddlMonth.SelectedValue.Split('_');

                if (_SplitDate.Length > 1)
                {
                    _Month = Convert.ToInt32(_SplitDate[0]);
                    _Year = Convert.ToInt32(_SplitDate[1]);
                }

                Interface_EmployeePaidSalary_ _IEmployeePaidSalaryService = new Repostry_EmployeePaidSalary_();

                Result<Boolean> _Result = _IEmployeePaidSalaryService.CheckSalaryPaidByEmployee(new Guid(XIDEmp), _Month.ToString(), _Year);
            }

            //if (ddlMonth.SelectedIndex > 0 && ddlEmployee.SelectedIndex > 0 && ddlDepartment.SelectedIndex > 0)
            if (ddlMonth.SelectedIndex > 0)
            {
                Guid _EmployeeId = new Guid(XIDEmp);
                int _Month = ClassSaddam.GetCurrentTime().Month;
                int _Year = ClassSaddam.GetCurrentTime().Year;

                string[] _SplitDate = ddlMonth.SelectedValue.Split('_');

                if (_SplitDate.Length > 1)
                {
                    _Month = Convert.ToInt32(_SplitDate[0]);
                    _Year = Convert.ToInt32(_SplitDate[1]);
                }


                DateTime _StartDate = new DateTime(_Year, _Month, 1);
                DateTime _EndDate = _StartDate.AddMonths(1).AddDays(-1);

                if (_EndDate.Date > ClassSaddam.GetCurrentTime().Date)
                    _EndDate = ClassSaddam.GetCurrentTime().Date;

                Interface_EmployeeAttendance_ _IEmployeeAttendanceService = new Repostry_EmployeeAttendance_();
                Interface_Employee_ _IEmployeeService = new Repostry_Employee_();

                List<Model_EmployeeAttendances_> _FinalResult = new List<Model_EmployeeAttendances_>();

                Result<List<Model_EmployeeAttendances_>> _Result = _IEmployeeAttendanceService.GetEmployeeAttendanceByEmployeeId(_EmployeeId, _StartDate, _EndDate);

                Result<Model_Employee_> _EmployeeResult = _IEmployeeService.GetEmployeeById(_EmployeeId);

                if (_Result.IsSuccess)
                {
                    if (_Result.Data != null)
                    {
                        _FinalResult = _Result.Data;

                        if (_FinalResult.Count() > 0)
                            _StartDate = _FinalResult.Max(a => a.AttendanceDate).AddDays(1);
                        else if (_StartDate < _EmployeeResult.Data.JoinDate_Date)
                            _StartDate = _EmployeeResult.Data.JoinDate_Date;
                    }
                }

                if (_EmployeeResult.Data.CountShift == 1)
                {
                    IDTowPart1.Visible = false; IDTowPart2.Visible = false;
                    colspanBottom.ColSpan = 9; colspanBottomTow.ColSpan = 9;
                    IDColspanTop.ColSpan = 1; IDColspanDown.ColSpan = 1;
                }
                else if (_EmployeeResult.Data.CountShift == 2)
                {
                    IDTowPart1.Visible = true; IDTowPart2.Visible = true;
                    colspanBottom.ColSpan = 11; colspanBottomTow.ColSpan = 11;
                    IDColspanTop.ColSpan = 3; IDColspanDown.ColSpan = 3;
                }
                HiddenField hfTotalUsedLeave = new HiddenField();
                hfTotalUsedLeave.Value = String.Format("{0:0.#}", _Result.Data.Where(el => el.AttendanceType == Convert.ToInt32(Common.AttendanceType.Leave)).Sum(l => l.Attendance));
                decimal TotalLeaveGo = 0, TotlaMandate = 0, TotlaResolved = 0, TotalOverTimeDays = 0;
                int TotalHolidays = 0, _WeeklyOff = 0;
                _WeeklyOff = _Result.Data.Where(h => h.AttendanceType == Convert.ToInt32(Common.AttendanceType.WeeklyOff)).Count();
                TotalHolidays = _Result.Data.Where(h => h.AttendanceType == Convert.ToInt32(Common.AttendanceType.Holiday)).Count();
                TotalLeaveGo = _Result.Data.Where(h => h.AttendanceType == Convert.ToInt32(Common.AttendanceType.Leave)).Count();
                TotlaMandate = _Result.Data.Where(h => h.AttendanceType == Convert.ToInt32(Common.AttendanceType.Mandate)).Count();
                TotlaResolved = Repostry_EmployeeResolved_.BErp_EmployeeResolved_Manage_SumByEmp(_EmployeeId, new Guid(ddlYears.SelectedValue), _Month.ToString());
                TotalOverTimeDays = Convert.ToDecimal(_Result.Data.Sum(ot => ot.OverTimeHours));

                lblCountDayInMonth.Text = ClassSaddam.getdays(_Year, _Month).ToString();
                lblCountDayLeveal.Text = (_Result.Data.Count() - (Convert.ToDecimal(hfTotalUsedLeave.Value) + TotalHolidays + Convert.ToDecimal(_WeeklyOff) + TotalLeaveGo)).ToString();
                lblCountDayWeeklyOff.Text = (_WeeklyOff).ToString();
                lblCountDayTotalHolidays.Text = (TotalHolidays).ToString();
                lblCountDayLeave.Text = (TotalLeaveGo).ToString();
                lblCountDayMandate.Text = (TotlaMandate).ToString();
                lblCountDayResolved.Text = (TotlaResolved).ToString();
                lblCountDayOverTimeDays.Text = (TotalOverTimeDays).ToString();

                gvEmployeeAttendance.DataSource = _FinalResult;
                gvEmployeeAttendance.DataBind();

                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                  " عرض كشف الحضور لـ" + ddlEmployee.SelectedItem.Text + " لشهر " + ddlMonth.SelectedItem.Text, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));

            }
            else
            {
                gvEmployeeAttendance.DataSource = null;
                gvEmployeeAttendance.DataBind();
            }
            if (gvEmployeeAttendance.Controls.Count > 0)
            {
                lblCount.Text = gvEmployeeAttendance.Controls.Count.ToString();
                pnlNull.Visible = false; pnlData.Visible = true; pnlSelect.Visible = false;
            }
            else
            {
                pnlNull.Visible = true; pnlData.Visible = false; pnlSelect.Visible = false;
            }

        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً .... ";
            return;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["footable1"] = pnlPrint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSelect.Visible = true;
        ddlMonth.Items.Clear(); ddlMonth.Items.Add(""); ddlMonth.AppendDataBoundItems = true;
        FillMonth();
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetData();
    }

    protected void GVEmployeeAttendance_PreRender(object sender, EventArgs e)
    {

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

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetData();
    }

}