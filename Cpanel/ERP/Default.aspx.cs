using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_Default : System.Web.UI.Page
{
    string IDUniq;
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
            Response.Redirect("/Cpanel/LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        try
        {
            ClassAdmin_Arn CA = new ClassAdmin_Arn();
            CA._IDUniq = IDUniq;
            CA._IsDelete = false;
            DataTable dtViewProfil = new DataTable();
            dtViewProfil = CA.BArnAdminGetByIDUniq();
            if (dtViewProfil.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtViewProfil.Rows[0]["_Two_Factor_Enabled_"]) || Convert.ToBoolean(dtViewProfil.Rows[0]["_SMS_Enabled_"]))
                    IDMessageWarning.Visible = false;
                else
                    IDMessageWarning.Visible = true;
            }
        }
        catch (Exception)
        {
            Response.Redirect("/Cpanel/LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetBirthday();
        }
    }

    private void FGetBirthday()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = HRM_Data_Access_Layer.GetData("SELECT TOP (100) [FirstName] + ' ' + [MiddleName] + ' ' + [LastName] + ' ' + [FatherName] As '_Name',[BirthDate] FROM [dbo].[EmployeeMaster] With(NoLock) Where (MONTH([BirthDate]) = @0) And [IsLeave] = @1 And [IsActive] = @2"
            , ClassDataAccess.GetCurrentTime().ToString("MM"), "0", "1");
        if (dt.Rows.Count > 0)
            {
                gvUpcomingBirthday.DataSource = dt;
                gvUpcomingBirthday.DataBind();
            }
            FGetContDepartment();
            FGetHoliday();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetContDepartment()
    {
        DataTable dt = new DataTable();
        dt = HRM_Data_Access_Layer.GetData("SELECT Count([DepartmentID]) As 'ContDepartment' FROM [dbo].[DepartmentMaster] With(NoLock) Where [IsActive] = @0", "1");
        if (dt.Rows.Count > 0)
            lblCountDepartment.Text = dt.Rows[0]["ContDepartment"].ToString();
        FGetContEmployee();
    }

    private void FGetContEmployee()
    {
        DataTable dt = new DataTable();
        dt = HRM_Data_Access_Layer.GetData("SELECT Count([EmployeeID]) As 'ContEmployee' FROM [dbo].[EmployeeMaster] With(NoLock) Where [IsActive] = @0 And [IsLeave] = @1"
        ,"1", "0");
        if (dt.Rows.Count > 0)
            lblCountEmp.Text = dt.Rows[0]["ContEmployee"].ToString();
    }

    private void FGetHoliday()
    {
        DataTable dt = new DataTable();
        dt = HRM_Data_Access_Layer.GetData("SELECT TOP (10) * FROM [dbo].[HolidayMaster] With(NoLock) Where ([StartDate] Between @0 And @1) And [IsActive] = @2"
        , ClassDataAccess.GetCurrentTime().AddDays(-10).ToString("yyyy-MM-dd"), ClassDataAccess.GetCurrentTime().AddDays(10).ToString("yyyy-MM-dd"), "1");
        if (dt.Rows.Count > 0)
        {
            gvUpcomingHoliday.DataSource = dt;
            gvUpcomingHoliday.DataBind();
        }
    }

}