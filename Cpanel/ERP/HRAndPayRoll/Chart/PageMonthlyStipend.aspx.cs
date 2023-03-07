using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_HRAndPayRoll_Chart_PageMonthlyStipend : System.Web.UI.Page
{
    public string XNameCompany;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            XNameCompany = " إحصائية المصروفات الشهرية لسنة " + ClassSaddam.GetCurrentTime().ToString("yyyy") + " م";
    }

    public string FCountMonthlyStipend(string IDMonth)
    {
        string Xresult = "0";
        try
        {
            DataTable dt = new DataTable();
            dt = HRM_Data_Access_Layer.GetData("SELECT ISNULL(Sum([PaidTotalSalary]),0) As 'CountMonthlyStipend' FROM [dbo].[EmployeePaidSalary] With(NoLock) Where YEAR([PaidDate]) = @0 And MONTH([PaidDate]) = @1 And [IsActive] = @2"
                , ClassSaddam.GetCurrentTime().ToString("yyyy"), IDMonth, Convert.ToString(true));
            if (dt.Rows.Count > 0)
                Xresult = String.Format("{0:0.00}", dt.Rows[0]["CountMonthlyStipend"]);
        }
        catch (Exception)
        {

        }
        return Xresult;
    }

    public string FCountMandate(string IDMonth)
    {
        string Xresult = "0";
        try
        {
            DataTable dt = new DataTable();
            dt = HRM_Data_Access_Layer.GetData("SELECT ISNULL(Sum([Total_Amount]),0) As 'CountMandate' FROM [dbo].[EmployeeMandate] With(NoLock) Where YEAR([CreatedDate]) = @0 And MONTH([CreatedDate]) = @1 And [Is_Moder_Allow_] = @2 And ([Is_Raees_Lagnat_Allow_] = @2 or [Is_Raees_Lagnat_Not_Allow_] = @2) And [IsActive] = @2"
                , ClassSaddam.GetCurrentTime().ToString("yyyy"), IDMonth, Convert.ToString(true));
            if (dt.Rows.Count > 0)
            {
                Xresult = dt.Rows[0]["CountMandate"].ToString();
            }
        }
        catch (Exception)
        {

        }
        return Xresult;
    }

    public string FCountOverTime(string IDMonth)
    {
        string Xresult = "0";
        try
        {
            DataTable dt = new DataTable();
            dt = HRM_Data_Access_Layer.GetData("SELECT ISNULL(Sum([Total_Amount]),0) As 'CountOverTime' FROM [dbo].[EmployeeOverTime] With(NoLock) Where YEAR([CreatedDate]) = @0 And MONTH([CreatedDate]) = @1 And [Is_Moder_Allow_] = @2 And ([Is_Raees_Lagnat_Allow_] = @2 or [Is_Raees_Lagnat_Not_Allow_] = @2) And [IsActive] = @2"
                , ClassSaddam.GetCurrentTime().ToString("yyyy"), IDMonth, Convert.ToString(true));
            if (dt.Rows.Count > 0)
            {
                Xresult = dt.Rows[0]["CountOverTime"].ToString();
            }
        }
        catch (Exception)
        {

        }
        return Xresult;
    }

}