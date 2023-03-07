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
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeSalaryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A194");
            pnlSelect.Visible = true;
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

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FillSalaryProcess();
    }

    public int _Month = ClassSaddam.GetCurrentTime().Month;
    public int _Year = ClassSaddam.GetCurrentTime().Year;

    private void FillSalaryProcess()
    {
        pnlSelect.Visible = false;
        divSalaryProcess.Visible = false;
        string _MonthId = ddlMonth.SelectedValue;
        if (!string.IsNullOrEmpty(_MonthId))
        {
            string[] _SplitDate = ddlMonth.SelectedValue.Split('_');

            if (_SplitDate.Length > 1)
            {
                _Month = Convert.ToInt32(_SplitDate[0]);
                _Year = Convert.ToInt32(_SplitDate[1]);
            }

            Interface_EmployeePaidSalary_ _IEmployeePaidSalaryService = new Repostry_EmployeePaidSalary_();

            Result<List<Model_EmployeePaidSalary_>> _ResultCompletedSalaryProcess = _IEmployeePaidSalaryService.GetEmployeeCompletedPaidSalaryByMonth(_Month.ToString(), _Year);

            if (_ResultCompletedSalaryProcess.IsSuccess)
            {
                gvEmployeeCompletedSalaryProcess.DataSource = _ResultCompletedSalaryProcess.Data;
                gvEmployeeCompletedSalaryProcess.DataBind();

                if (gvEmployeeCompletedSalaryProcess.Rows.Count > 0)
                {
                    gvEmployeeCompletedSalaryProcess.UseAccessibleHeader = false;
                    btnDelete.Visible = true;
                }
                else
                    btnDelete.Visible = false;
            }

            Result<List<Model_EmployeePaidSalary_>> _ResultPendingSalaryProcess = _IEmployeePaidSalaryService.GetEmployeePendingSalaryByMonth(_Month, _Year);

            if (_ResultPendingSalaryProcess.IsSuccess)
            {
                gvEmployeePendingSalaryProcess.DataSource = _ResultPendingSalaryProcess.Data;
                gvEmployeePendingSalaryProcess.DataBind();

                if (gvEmployeePendingSalaryProcess.Rows.Count > 0)
                    gvEmployeePendingSalaryProcess.UseAccessibleHeader = false;
            }

            divSalaryProcess.Visible = true;
        }

        ScriptManager.RegisterStartupScript(this, typeof(Page), "EmployeeSalaryProcessList", "EmployeeSalaryProcessList.InitailGridDataTable();", true);
    }

    public string FGetIDMounth()
    {
        return _Month.ToString() + "_" + _Year.ToString();
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        pnlSelect.Visible = true;
        divSalaryProcess.Visible = false;
        ddlMonth.Items.Clear(); ddlMonth.Items.Add(""); ddlMonth.AppendDataBoundItems = true;
        FillMonth();
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeSalaryList.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            string[] _SplitDate = ddlMonth.SelectedValue.Split('_');
            if (_SplitDate.Length > 1)
            {
                _Month = Convert.ToInt32(_SplitDate[0]);
                _Year = Convert.ToInt32(_SplitDate[1]);
            }
            foreach (GridViewRow row in gvEmployeeCompletedSalaryProcess.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(gvEmployeeCompletedSalaryProcess.DataKeys[row.RowIndex].Value);
                    Label lbl_PaidDate = (Label)row.FindControl("lblPaidDate") as Label;

                    Model_EmployeePaidSalary_ MEPS = new Model_EmployeePaidSalary_()
                    {
                        IDCheck = "Delete",
                        EmployeeId = new Guid(Comp_ID),
                        PaidDate = lbl_PaidDate.Text,
                        Month = _Month.ToString(),
                        Year = _Year,
                        IsActive = false,
                    };
                    Repostry_EmployeePaidSalary_ REPS = new Repostry_EmployeePaidSalary_();
                    string Xresult = REPS.FErp_EmployeePaidSalary_Delete_All(MEPS);
                    if (Xresult == "IsSuccessSalaryDelete" || Xresult == "IsSuccessSalaryAllowanceDelete" || Xresult == "IsSuccessSalaryDeductionDelete")
                    {
                        IDMessageWarning.Visible = true;
                        IDMessageSuccess.Visible = false;
                        lblWarning.Text = "لم يتم حذف البيانات كاملاً ... ";
                        return;
                    }
                    else if (Xresult == "IsSuccessSalaryPaidLoanDelete")
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم حذف البيانات بنجاح ... ";
                    }
                }
            }
            FillSalaryProcess();
        }
        catch (Exception)
        {
            return;
        }
    }

}