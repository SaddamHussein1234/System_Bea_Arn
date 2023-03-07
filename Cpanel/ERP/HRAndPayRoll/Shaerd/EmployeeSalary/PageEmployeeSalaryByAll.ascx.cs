using Library_CLS_Arn.ERP.Interface.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Permissions;
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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeSalary_PageEmployeeSalaryByAll : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (XType == "Manager")
                CLS_Permissions.CheckAccountAdmin("A193");
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

    public int _Month = ClassSaddam.GetCurrentTime().Month;
    public int _Year = ClassSaddam.GetCurrentTime().Year;

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        pnlSelect.Visible = true;
        ddlMonth.Items.Clear(); ddlMonth.Items.Add(""); ddlMonth.AppendDataBoundItems = true;
        FillMonth();
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FillSalaryProcess();
    }

    private void FillSalaryProcess()
    {
        //if (XType == "Manager")
        //{
        //    MEOT.IDCheck = "GetByIDDetails";
        //    MEOT.EmployeeOverTimeMapID = Guid.Empty;
        //    MEOT.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
        //    MEOT.Number_OverTime = Convert.ToInt64(txtSearch.Text.Trim());
        //}
        //else if (XType == "Admin")
        //{
        //    MEOT.IDCheck = "GetByIDUniq";
        //    MEOT.EmployeeOverTimeMapID = new Guid(Request.QueryString["ID"]);
        //    MEOT.FinancialYear_Id = Guid.Empty;
        //    MEOT.Number_OverTime = 0; IDSearch.Visible = false;
        //}

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
                lblCount.Text = _ResultCompletedSalaryProcess.Data.Count.ToString();
                if (gvEmployeeCompletedSalaryProcess.Rows.Count > 0)
                {
                    txtTitle.Text = "كشف رواتب الموظفين لشهر : " + ddlMonth.SelectedItem.ToString();
                    gvEmployeeCompletedSalaryProcess.UseAccessibleHeader = false;
                    btnDelete.Visible = false;
                    pnlData.Visible = true;
                    pnlNull.Visible = false;
                    pnlSelect.Visible = false;

                    Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                     txtTitle.Text.Trim(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    btnDelete.Visible = false;
                    pnlData.Visible = false;
                    pnlNull.Visible = true;
                    pnlSelect.Visible = false;
                }
            }
        }

        ScriptManager.RegisterStartupScript(this, typeof(Page), "EmployeeSalaryProcessList", "EmployeeSalaryProcessList.InitailGridDataTable();", true);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            gvEmployeeCompletedSalaryProcess.Columns[14].Visible = false;
            gvEmployeeCompletedSalaryProcess.UseAccessibleHeader = true;
            gvEmployeeCompletedSalaryProcess.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable"] = pnlprint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeSalaryByAll.aspx");
    }

}