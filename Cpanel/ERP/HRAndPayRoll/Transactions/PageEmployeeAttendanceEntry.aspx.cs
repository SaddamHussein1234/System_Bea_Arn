using MKB.TimePicker;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Interface.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll;
using Library_CLS_Arn.Saddam;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.OM.Repostry;

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeAttendanceEntry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A191");
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FillMonth();
        }
    }

    private void FillMonth()
    {
        ddlMonth.Items.Clear();
        ddlMonth.Items.Add("");
        ddlMonth.AppendDataBoundItems = true;

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

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetPhoneAndEmail();
        FillGrid();
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FillGrid();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            int XIDAdd = Test_Saddam.FGetIDUsiq();
            string Xresult = string.Empty;
            foreach (GridViewRow row in gvEmployeeAttendance.Rows)
            {
                Model_EmployeeAttendances_ MEA = new Model_EmployeeAttendances_();
                HiddenField hfEmployeeAttendanceId = (HiddenField)row.FindControl("hfEmployeeAttendanceId");
                if (hfEmployeeAttendanceId.Value == "00000000-0000-0000-0000-000000000000")
                {
                    MEA.IDCheck = "Add"; MEA.EmployeeAttendanceID = Guid.NewGuid();                   
                }
                else
                {
                    MEA.IDCheck = "Edit"; MEA.EmployeeAttendanceID = new Guid(hfEmployeeAttendanceId.Value);
                }

                MEA.FinancialYearId = new Guid(ddlYears.SelectedValue);
                MEA.EmployeeId = new Guid(ddlEmployee.SelectedValue);
                
                Label lblAttendanceDate = (Label)row.FindControl("lblAttendanceDate");
                if (lblAttendanceDate != null)
                    MEA.AttendanceDateString = Convert.ToDateTime(lblAttendanceDate.Text.Trim()).ToString("yyyy-MM-dd");

                TimeSelector tsTimeIn = (TimeSelector)row.FindControl("tsTimeIn");
                if (tsTimeIn != null)
                    MEA.TimeIn = tsTimeIn.Hour.ToString("00") + ":" + tsTimeIn.Minute.ToString("00") + " " + tsTimeIn.AmPm;

                TimeSelector tsTimeOut = (TimeSelector)row.FindControl("tsTimeOut");
                if (tsTimeOut != null)
                    MEA.TimeOut = tsTimeOut.Hour.ToString("00") + ":" + tsTimeOut.Minute.ToString("00") + " " + tsTimeOut.AmPm;

                TimeSelector tsTimeIn_Tow = (TimeSelector)row.FindControl("tsTimeIn_Tow");
                if (tsTimeIn_Tow != null)
                    MEA.TimeIn_Tow = tsTimeIn_Tow.Hour.ToString("00") + ":" + tsTimeIn_Tow.Minute.ToString("00") + " " + tsTimeIn_Tow.AmPm;

                TimeSelector tsTimeOut_Tow = (TimeSelector)row.FindControl("tsTimeOut_Tow");
                if (tsTimeOut_Tow != null)
                    MEA.TimeOut_Tow = tsTimeOut_Tow.Hour.ToString("00") + ":" + tsTimeOut_Tow.Minute.ToString("00") + " " + tsTimeOut_Tow.AmPm;

                TextBox txtWorkingHours = (TextBox)row.FindControl("txtWorkingHours");

                if (txtWorkingHours.Text != string.Empty)
                    MEA.WorkingHours = Convert.ToDouble(txtWorkingHours.Text);
                else
                    MEA.WorkingHours = 0;

                TextBox txtOverTimeHours = (TextBox)row.FindControl("txtOverTimeHours");

                if (txtOverTimeHours.Text != string.Empty)
                    MEA.OverTimeHours = Convert.ToDecimal(txtOverTimeHours.Text);
                else
                    MEA.OverTimeHours = 0;

                DropDownList ddlAttendanceType = (DropDownList)row.FindControl("ddlAttendanceType");
                MEA.AttendanceType = Convert.ToInt32(Common.AttendanceType.Present);
                if (ddlAttendanceType.SelectedValue != null)
                    MEA.AttendanceType = Convert.ToInt32(ddlAttendanceType.SelectedValue);

                DropDownList ddlAttendance = (DropDownList)row.FindControl("ddlAttendance");
                if (ddlAttendance.SelectedValue != null)
                    MEA.Attendance = Convert.ToDecimal(ddlAttendance.SelectedValue);

                TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
                MEA.Description = txtDescription.Text;

                MEA.CreatedDate = XDate;
                MEA.CreatedBy = XIDAdd;
                MEA.ModifiedBy = XIDAdd;
                MEA.ModifiedDate = XDate;
                MEA.IsActive = true;

                Repostry_EmployeeAttendance_ REA = new Repostry_EmployeeAttendance_();
                Xresult = REA.FErp_EmployeeAttendance_Add(MEA);   
            }
            if (Xresult == "IsSuccessAdd")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "إضافة",
                  " إضافة كشف الحضور لـ" + ddlEmployee.SelectedItem.Text + " لشهر " + ddlMonth.SelectedItem.Text, XDate);

                Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تم إضافة كشف الحضور" + "\n" + "لـ :" + ddlMonth.SelectedItem.Text + "\n" + "يمكنك الإطلاع من حسابك" + "\n" + "شكراً لك", "BerArn", "Add", XIDAdd);
                FillGrid();
            }
            else if (Xresult == "IsSuccessEdit")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "تعديل",
                 " تعديل كشف الحضور لـ" + ddlEmployee.SelectedItem.Text + " لشهر " + ddlMonth.SelectedItem.Text, XDate);

                Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تم تعديل كشف الحضور" + "\n" + "لـ :" + ddlMonth.SelectedItem.Text + "\n" + "يمكنك الإطلاع من حسابك" + "\n" + "شكراً لك", "BerArn", "Edit", XIDAdd);
                FillGrid();
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

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeAttendanceList.aspx");
    }

    protected void ddlAttendanceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            IDMessageSuccess.Visible = false; IDMessageWarning.Visible = false;
            DropDownList ddlAttendanceType = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddlAttendanceType.NamingContainer;
            DropDownList ddlAttendance = (DropDownList)row.FindControl("ddlAttendance");
            TextBox txtWorkingHours = (TextBox)row.FindControl("txtWorkingHours");
            Label lblAttendance = (Label)row.FindControl("lblAttendance");

            ddlAttendance.Visible = false;
            txtWorkingHours.Text = "0";
            txtWorkingHours.Enabled = false;
            lblAttendance.Visible = false;

            if (ddlAttendanceType.SelectedValue == (Convert.ToInt32(Common.AttendanceType.Leave)).ToString())
            {
                ddlAttendance.Visible = true;
                txtWorkingHours.Enabled = true;
            }
            else if (ddlAttendanceType.SelectedValue == (Convert.ToInt32(Common.AttendanceType.Present)).ToString())
            {
                ddlAttendance.SelectedValue = "1.00";
                txtWorkingHours.Enabled = true;
                lblAttendance.Text = "حاضر";
                lblAttendance.Visible = true;
            }
            else if (ddlAttendanceType.SelectedValue == (Convert.ToInt32(Common.AttendanceType.Mandate)).ToString())
            {
                ddlAttendance.SelectedValue = "1.00";
                txtWorkingHours.Enabled = true;
                lblAttendance.Text = "حاضر";
                lblAttendance.Visible = true;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FillGrid()
    {
        try
        {
            // التأكد من أنه لم يتم إضافة الرسوم
            btnAdd.Visible = true;
            string _MonthId = ddlMonth.SelectedValue;
            if (!string.IsNullOrEmpty(_MonthId) && !string.IsNullOrEmpty(ddlEmployee.SelectedValue))
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

                Result<Boolean> _Result = _IEmployeePaidSalaryService.CheckSalaryPaidByEmployee(new Guid(ddlEmployee.SelectedValue), _Month.ToString(), _Year);

                if (_Result.IsSuccess)
                {
                    if (_Result.Data)
                        btnAdd.Visible = false;
                }
            }

            if (ddlMonth.SelectedIndex > 0 && ddlEmployee.SelectedIndex > 0 && ddlDepartment.SelectedIndex > 0)
            {
                FGetPhoneAndEmail();
                Guid _EmployeeId = new Guid(ddlEmployee.SelectedValue);
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

                for (DateTime _Date = _StartDate; _Date <= _EndDate; _Date = _Date.AddDays(1))
                {
                    Model_EmployeeAttendances_ MEA = new Model_EmployeeAttendances_();

                    MEA.EmployeeAttendanceID = Guid.Empty;
                    MEA.AttendanceDate = _Date;

                    MEA.TimeIn = "00:00 AM";
                    MEA.TimeOut = "00:00 AM";
                    MEA.TimeIn_Tow = "00:00 AM";
                    MEA.TimeOut_Tow = "00:00 AM";
                    MEA.WorkingHours = 0;
                    MEA.OverTimeHours = 0;
                    MEA.AttendanceType = Convert.ToInt32(Common.AttendanceType.Present);
                    MEA.Attendance = Convert.ToDecimal("1.00");

                    bool _Flag = true;

                    if (_EmployeeResult.IsSuccess)
                    {
                        if (_EmployeeResult.Data.CountShift == 1)
                        {
                            MEA.TimeIn = _EmployeeResult.Data.From_Time;
                            MEA.TimeOut = _EmployeeResult.Data.To_Time;
                            MEA.TimeIn_Tow = "12:55 PM";
                            MEA.TimeOut_Tow = "12:55 PM";

                            DateTime start = DateTime.Parse(_EmployeeResult.Data.From_Time);
                            DateTime end = DateTime.Parse(_EmployeeResult.Data.To_Time);
                            if (start > end)
                                end = end.AddDays(1);
                            TimeSpan duration2 = end.Subtract(start);
                            MEA.WorkingHours = Convert.ToDouble(duration2.TotalHours);

                            gvEmployeeAttendance.Columns[5].Visible = false;
                            gvEmployeeAttendance.Columns[6].Visible = false;

                            if (MEA.WorkingHours > 8 || MEA.WorkingHours >= 6)
                                MEA.WorkingHours = 8;
                        }
                        else if (_EmployeeResult.Data.CountShift == 2)
                        {
                            MEA.TimeIn = _EmployeeResult.Data.From_Time;
                            MEA.TimeOut = _EmployeeResult.Data.To_Time;
                            MEA.TimeIn_Tow = _EmployeeResult.Data.From_Time_Tow;
                            MEA.TimeOut_Tow = _EmployeeResult.Data.To_Time_Tow;

                            DateTime StartOne = DateTime.Parse(_EmployeeResult.Data.From_Time);
                            DateTime EndOne = DateTime.Parse(_EmployeeResult.Data.To_Time);
                            if (StartOne > EndOne)
                                EndOne = EndOne.AddDays(1);
                            TimeSpan durationOne = EndOne.Subtract(StartOne);

                            DateTime StartTow = Convert.ToDateTime(_EmployeeResult.Data.From_Time_Tow);
                            DateTime EndTow = Convert.ToDateTime(_EmployeeResult.Data.To_Time_Tow);
                            if (StartTow > EndTow)
                                EndTow = EndTow.AddDays(1);
                            TimeSpan durationTow = EndTow.Subtract(StartTow);

                            MEA.WorkingHours = Convert.ToDouble(durationOne.TotalHours) + Convert.ToDouble(durationTow.TotalHours);

                            gvEmployeeAttendance.Columns[5].Visible = true;
                            gvEmployeeAttendance.Columns[6].Visible = true;

                            if (MEA.WorkingHours > 8 || MEA.WorkingHours >= 7.50)
                                MEA.WorkingHours = 8;
                        }

                        

                        Model_EmployeeWorkingDay_ MEWD = new Model_EmployeeWorkingDay_();
                        MEWD.IDCheck = "GetByIDEmpAndDay";
                        MEWD.EmployeeId = new Guid(ddlEmployee.SelectedValue);
                        MEWD.DayName = _Date.ToString("ddd");
                        MEWD.IsActive = true;
                        DataTable dtDay = new DataTable();
                        Repostry_EmployeeWorkingDay_ REWD = new Repostry_EmployeeWorkingDay_();
                        dtDay = REWD.BErp_EmployeeWorkingDay_Manage(MEWD);
                        if (dtDay.Rows.Count == 0)
                        {
                            MEA.WorkingHours = 0;
                            MEA.AttendanceType = Convert.ToInt32(Common.AttendanceType.WeeklyOff);
                            //_Flag = false;
                        }
                    }

                    if (_Flag)
                    {
                        Model_EmployeeLeaveCategory_ MELC = new Model_EmployeeLeaveCategory_();
                        MELC.IDCheck = "GetByAttchment";
                        MELC.EmployeeLeaveCategoryMapID = _EmployeeId;
                        MELC.FinancialYear_Id = Guid.Empty;
                        MELC.Number_Leave = 0;
                        MELC.CreatedDate = string.Empty;
                        MELC.StartDate = MEA.AttendanceDate.ToString("yyyy-MM-dd");
                        MELC.EndDate = string.Empty;
                        MELC.Is_Emp = true;
                        MELC.Is_Moder_Allow = true;
                        MELC.Is_Raees_Lagnat_Allow = true;
                        MELC.IsActive = true;
                        DataTable dt = new DataTable();
                        Repostry_EmployeeLeaveCategory_ RELC = new Repostry_EmployeeLeaveCategory_();
                        dt = RELC.BErp_EmployeeLeaveCategory_Manage(MELC);
                        if (dt.Rows.Count > 0)
                        {
                            _Flag = false;
                            MEA.AttendanceType = Convert.ToInt32(Common.AttendanceType.Leave);

                            //if (Convert.ToBoolean(dt.Rows[0]["IsFirstHalfDay"]) && dt.Rows[0]["_Start"].ToString() == MEA.AttendanceDate.ToString("yyyy-MM-dd"))
                            //    MEA.Attendance = Convert.ToDecimal("0.50");
                            //else if (Convert.ToBoolean(dt.Rows[0]["IsLastHalfDay"]) && dt.Rows[0]["_End"].ToString() == MEA.AttendanceDate.ToString("yyyy-MM-dd"))
                            //    MEA.Attendance = Convert.ToDecimal("0.50");
                            //else
                               MEA.Attendance = Convert.ToDecimal("0.00");

                            MEA.Description = dt.Rows[0]["LeaveCategory"].ToString();
                        }
                    }

                    if (_Flag)
                    {
                        Model_EmployeePermission_ MEP = new Model_EmployeePermission_();
                        MEP.IDCheck = "GetByAttendance";
                        MEP.EmployeePermissionMapID = _EmployeeId;
                        MEP.FinancialYear_Id = Guid.Empty;
                        MEP.Number_Permission = 0;
                        MEP.CreatedDate = string.Empty;
                        MEP.Start_Date = MEA.AttendanceDate.ToString("yyyy-MM-dd");
                        MEP.End_Date = string.Empty;
                        MEP.Is_Moder_Allow = false;
                        MEP.Is_Moder_Not_Allow = false;
                        MEP.IsActive = true;
                        DataTable dt_Permission = new DataTable();
                        Repostry_EmployeePermission_ REP = new Repostry_EmployeePermission_();
                        dt_Permission = REP.BErp_EmployeePermission_Manage(MEP);

                        if (dt_Permission.Rows.Count > 0)
                            MEA.Description = "لدية إستئذان";
                    }
                    if (_Flag)
                    {
                        Model_Holiday_ MH = new Model_Holiday_();
                        MH.IDCheck = "GetByHoliday";
                        MH.HolidayID = Guid.NewGuid();
                        MH.Title = string.Empty;
                        MH.StartDate = MEA.AttendanceDate.ToString("yyyy-MM-dd");
                        MH.EndDate = string.Empty;
                        MH.IsActive = true;
                        DataTable dt_Holiday = new DataTable();
                        Repostry_Holiday_ RH = new Repostry_Holiday_();
                        dt_Holiday = RH.BErp_Holiday_Manage(MH);
                        if (dt_Holiday.Rows.Count > 0)
                        {
                            MEA.AttendanceType = Convert.ToInt32(Common.AttendanceType.Holiday);
                            MEA.Description = dt_Holiday.Rows[0]["Title"].ToString();
                        }
                    }

                    if (_Flag)
                    {
                        DataTable dt_Mandate = new DataTable();
                        dt_Mandate = Repostry_EmployeeMandate_.FGetDataInDataTable("GetByAttendance", _EmployeeId,
                            new Guid(ddlYears.SelectedValue), 0, string.Empty, MEA.AttendanceDate.ToString("yyyy-MM-dd"),
                            string.Empty, true, true, true);
                        if (dt_Mandate.Rows.Count > 0)
                        {
                            MEA.AttendanceType = Convert.ToInt32(Common.AttendanceType.Mandate);
                            MEA.Description = dt_Mandate.Rows[0]["MandateTitle"].ToString();
                        }
                    }

                    if (_Flag)
                    {
                        Model_EmployeeOverTime_ MEOT = new Model_EmployeeOverTime_();
                        MEOT.IDCheck = "GetByAttendance";
                        MEOT.EmployeeOverTimeMapID = _EmployeeId;
                        MEOT.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
                        MEOT.Number_OverTime = 0;
                        MEOT.CreatedDate = string.Empty;
                        MEOT.Start_Date = MEA.AttendanceDate.ToString("yyyy-MM-dd");
                        MEOT.End_Date = string.Empty;
                        MEOT.Is_Moder_Allow = true;
                        MEOT.Is_Raees_Lagnat_Allow = true;
                        MEOT.IsActive = true;
                        DataTable dt_OverTime = new DataTable();
                        Repostry_EmployeeOverTime_ REOT = new Repostry_EmployeeOverTime_();
                        dt_OverTime = REOT.BErp_EmployeeOverTime_Manage(MEOT);
                        if (dt_OverTime.Rows.Count > 0)
                        {
                            MEA.OverTimeHours = Convert.ToDecimal(dt_OverTime.Rows[0]["Hours_In_Day_"]);
                            MEA.Description = "عمل إضافي";
                        }
                    }

                    _FinalResult.Add(MEA);
                }

                gvEmployeeAttendance.DataSource = _FinalResult;
                gvEmployeeAttendance.DataBind();
                if (gvEmployeeAttendance.Rows.Count > 0)
                    IDAdd.Visible = true;
            }
            else
            {
                gvEmployeeAttendance.DataSource = null;
                gvEmployeeAttendance.DataBind();
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

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FillMonth();
        gvEmployeeAttendance.DataSource = null;
        gvEmployeeAttendance.DataBind();
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeAttendanceEntry.aspx");
    }

    private void FGetPhoneAndEmail()
    {
        Repostry_Employee_.FGetPhoneAndEmail(ddlEmployee.SelectedValue, HFPhone, HFEmail);
        lblPhone.InnerText = HFPhone.Value;
    }

}