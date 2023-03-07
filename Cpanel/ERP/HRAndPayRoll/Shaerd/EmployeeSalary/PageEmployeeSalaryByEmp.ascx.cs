using Library_CLS_Arn.ERP.Interface.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeSalary_PageEmployeeSalaryByEmp : System.Web.UI.UserControl
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
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            if (XType == "Manager")
            {
                CLS_Permissions.CheckAccountAdmin("A192");
                IDDepartment.Visible = true; IDEmployee.Visible = true;
            }
            else if (XType == "Admin")
            {
                GetCookie();
                FillSalaryProcess(IDUniq);
                IDDepartment.Visible = false; IDEmployee.Visible = false;
            }
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlSelect.Visible = true;
            pnlData.Visible = false;
            pnlNull.Visible = false;
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            ddlDepartment.Items.Clear(); ddlDepartment.Items.Add(""); ddlDepartment.AppendDataBoundItems = true;
            ddlEmployee.Items.Clear(); ddlEmployee.Items.Add(""); ddlEmployee.AppendDataBoundItems = true;
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            if (XType == "Admin")
            {
                GetCookie();
                FillSalaryProcess(IDUniq);
                IDDepartment.Visible = false; IDEmployee.Visible = false;
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

    public int _Year = ClassSaddam.GetCurrentTime().Year;

    private void FillSalaryProcess(string XIDEmp)
    {
        _Year = Convert.ToInt32(ddlYears.SelectedItem.ToString());
        Interface_EmployeePaidSalary_ _IEmployeePaidSalaryService = new Repostry_EmployeePaidSalary_();

        Result<List<Model_EmployeePaidSalary_>> _ResultCompletedSalaryProcess = _IEmployeePaidSalaryService.GetEmployeeCompletedPaidSalaryByEmp(new Guid(XIDEmp), _Year);

        if (_ResultCompletedSalaryProcess.IsSuccess)
        {
            gvEmployeeCompletedSalaryProcess.DataSource = _ResultCompletedSalaryProcess.Data;
            gvEmployeeCompletedSalaryProcess.DataBind();
            lblCount.Text = _ResultCompletedSalaryProcess.Data.Count.ToString();
            if (gvEmployeeCompletedSalaryProcess.Rows.Count > 0)
            {
                FGetDataEmp(XIDEmp);
                gvEmployeeCompletedSalaryProcess.UseAccessibleHeader = false;
                btnDelete.Visible = false;
                pnlData.Visible = true;
                pnlNull.Visible = false;
                pnlSelect.Visible = false;
            }
            else
            {
                btnDelete.Visible = false;
                pnlData.Visible = false;
                pnlNull.Visible = true;
                pnlSelect.Visible = false;
            }
        }

        ScriptManager.RegisterStartupScript(this, typeof(Page), "EmployeeSalaryProcessList", "EmployeeSalaryProcessList.InitailGridDataTable();", true);
    }

    private void FGetDataEmp(string XIDEmp)
    {
        Model_Employee_ ME = new Model_Employee_();
        ME.IDCheck = "GetByIDUniq";
        ME.EmployeeID = new Guid(XIDEmp);
        ME.FinancialYear_Id = Guid.Empty;
        ME.FirstName = string.Empty;
        ME.Date_From = string.Empty;
        ME.Date_To = string.Empty;
        ME.IsActive = true;
        DataTable dt = new DataTable();
        Repostry_Employee_ RE = new Repostry_Employee_();
        dt = RE.BErp_Employee_Master_Manage(ME);
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "كشف رواتب لـ : " + dt.Rows[0]["_Name"].ToString() + " , لسنة " + ddlYears.SelectedItem.ToString();
            lblName.Text = dt.Rows[0]["_Name"].ToString();
            lblManagment.Text = dt.Rows[0]["Department"].ToString();
            lblBirthDate.Text = Convert.ToDateTime(dt.Rows[0]["BirthDate"]).ToString("yyyy-MM-dd");
            if (Convert.ToBoolean(dt.Rows[0]["Gender"]))
                lblGender.Text = "ذكر";
            else
                lblGender.Text = "أنثى";
            lblJoinDate.Text = Convert.ToDateTime(dt.Rows[0]["JoinDate"]).ToString("yyyy-MM-dd");
            lblPFNumber.Text = dt.Rows[0]["EmployeeNo"].ToString();

            Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                       txtTitle.Text.Trim(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeSalaryByEmp.aspx");
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            pnlSelect.Visible = true;
            pnlData.Visible = false;
            pnlNull.Visible = false;
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
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            pnlSelect.Visible = true;
            pnlData.Visible = false;
            pnlNull.Visible = false;
            gvEmployeeCompletedSalaryProcess.DataBind();
            FillSalaryProcess(ddlEmployee.SelectedValue);
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            gvEmployeeCompletedSalaryProcess.Columns[10].Visible = false;
            gvEmployeeCompletedSalaryProcess.UseAccessibleHeader = true;
            gvEmployeeCompletedSalaryProcess.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlprint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

}