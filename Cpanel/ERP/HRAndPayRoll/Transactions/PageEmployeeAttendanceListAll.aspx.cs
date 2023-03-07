using System;
using System.Collections.Generic;
using System.Globalization;
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

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeAttendanceListAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A190");
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
        Response.Redirect("PageEmployeeAttendanceListAll.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["XXTitle"] = txtTitle.Text.Trim();
            Session["footable1Title"] = pnlPrint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlNull.Visible = false; pnlData.Visible = false; pnlSelect.Visible = true;
        pnlSelect.Visible = true;
        ddlMonth.Items.Clear(); ddlMonth.Items.Add(""); ddlMonth.AppendDataBoundItems = true;
        FillMonth();
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlNull.Visible = false; pnlData.Visible = false; pnlSelect.Visible = true;
        ddlMonth.Items.Clear(); ddlMonth.Items.Add(""); ddlMonth.AppendDataBoundItems = true;
        FillMonth();
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetData();
    }

    private void FGetData()
    {
        try
        {
            txtTitle.Text = ddlDepartment.SelectedItem.ToString() + " , كشف حضور الموظفين " + " لشهر " + ddlMonth.SelectedItem.ToString();
            string _MonthId = ddlMonth.SelectedValue;

            if (ddlMonth.SelectedIndex > 0 && ddlDepartment.SelectedIndex > 0 && ddlDepartment.SelectedIndex > 0)
            {
                Guid _DepartmentId = new Guid(ddlDepartment.SelectedValue);
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

                Result<List<Model_EmployeeAttendances_>> _Result = _IEmployeeAttendanceService.GetEmployeeAttendanceByDepartmentId(_DepartmentId, _StartDate, _EndDate);

                if (_Result.IsSuccess)
                {
                    if (_Result.Data != null)
                        _FinalResult = _Result.Data;
                }

                HFCountDay.Value = ClassSaddam.getdays(_Year, _Month).ToString();

                foreach (Model_EmployeeAttendances_ SH in _FinalResult)
                {
                    SH.CountLeaveCategory = Repostry_EmployeeLeaveCategory_.BErp_EmployeeLeaveCategory_SumByEmp(SH.EmployeeId, new Guid(ddlYears.SelectedValue), _Month.ToString());
                    SH.CountResolved = Repostry_EmployeeResolved_.BErp_EmployeeResolved_Manage_SumByEmp(SH.EmployeeId, new Guid(ddlYears.SelectedValue), _Month.ToString());
                }

                lblCountDayInMonth.Text = HFCountDay.Value;
                gvEmployeeAttendance.DataSource = _FinalResult;
                gvEmployeeAttendance.DataBind();

                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                  " عرض كشف الحضور للموظفين" + " لشهر " + ddlMonth.SelectedItem.Text, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));

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

    protected void gvEmployeeAttendance_PreRender(object sender, EventArgs e)
    {
        try
        {
            decimal SumLeaveCategory = 0, SumMandate = 0, SumResolved = 0, SumOverTime = 0;
            foreach (RepeaterItem item in gvEmployeeAttendance.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {

                    Label lblCLeaveCategory = (Label)item.FindControl("lblLeaveCategory");
                    SumLeaveCategory += decimal.Parse(lblCLeaveCategory.Text);
                    lblCountLeaveCategory.Text = String.Format("{0:0.#}", SumLeaveCategory);

                    Label lblCMandate = (Label)item.FindControl("lblMandate");
                    SumMandate += decimal.Parse(lblCMandate.Text);
                    lblCountMandate.Text = String.Format("{0:0.#}", SumMandate);

                    Label lblCResolved = (Label)item.FindControl("lblResolved");
                    SumResolved += decimal.Parse(lblCResolved.Text);
                    lblCountResolved.Text = String.Format("{0:0.#}", SumResolved);

                    Label lblCOverTime = (Label)item.FindControl("lblOverTime");
                    SumOverTime += decimal.Parse(lblCOverTime.Text);
                    lblCountOverTime.Text = String.Format("{0:0.#}", SumOverTime);

                }
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

}