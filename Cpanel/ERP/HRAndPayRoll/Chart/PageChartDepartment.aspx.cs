using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Chart_PageChartDepartment : System.Web.UI.Page
{
    public string XNameCompany;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            XNameCompany = " إحصائية الموظفين حسب الإدارات لسنة " + ClassSaddam.GetCurrentTime().ToString("yyyy") + " م";
    }

    public string FGeDepartment_Manage()
    {
        string XResult = string.Empty;
        try
        {
            Model_Department_ MD = new Model_Department_();
            MD.IDCheck = "GetAll";
            MD.DepartmentID = Guid.NewGuid();
            MD.DepartmentName = string.Empty;
            MD.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Department_ RP = new Repostry_Department_();
            dt = RP.BErp_Department_Manage(MD);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    XResult += "{name: '" + dt.Rows[i]["Department"].ToString() + "', y: "+ FGeCountEmp(dt.Rows[i]["DepartmentID"].ToString()) + ".00, sliced: true, selected: true },";
            }
        }
        catch (Exception)
        {

        }
        return XResult;
    }

    public string FGeCountEmp(string XID)
    {
        string XResult = string.Empty;
        DataTable dt = new DataTable();
        dt = HRM_Data_Access_Layer.GetData("SELECT TOP (100) [IsActive] FROM [dbo].[EmployeeMaster] With(NoLock) Where [DepartmentId] = @0 And [IsActive] = @1 And [IsLeave] = @2",
            XID, "1", "0");
        if (dt.Rows.Count > 0)
        {
            XResult = dt.Rows.Count.ToString();
        }
        return XResult;
    }

}