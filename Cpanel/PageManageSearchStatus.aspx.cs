using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageManageSearchStatus : System.Web.UI.Page
{
    string IDUser, IDUniq;
    private void GetCookie()
    {
        try
        {
            HttpCookie Cooke;  // رقم المستخدم
            Cooke = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
            IDUser = ClassSaddam.UnprotectPassword(Cooke["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

            CheckAccountAdmin();
        }
        catch
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        ClassAdmin_Arn CA = new ClassAdmin_Arn();
        CA._IDUniq = IDUniq;
        CA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CA.BArnAdminGetByIDUniq();
        if (dtViewProfil.Rows.Count > 0)
        {
            bool[] A = new bool[150];

            A[85] = Convert.ToBoolean(dtViewProfil.Rows[0]["A85"]); A[41] = Convert.ToBoolean(dtViewProfil.Rows[0]["A41"]);
            A[86] = Convert.ToBoolean(dtViewProfil.Rows[0]["A86"]); A[87] = Convert.ToBoolean(dtViewProfil.Rows[0]["A87"]);
            A[42] = Convert.ToBoolean(dtViewProfil.Rows[0]["A42"]);
            if (A[85]) { IDSearchStatusAdd.Visible = true; }
            if (A[41]) { IDSearchStatus.Visible = true; }
            if (A[86]) { IDSearchStatusManager.Visible = true; }
            if (A[87]) { IDSearchStatusLagnat.Visible = true; }
            if (A[42]) { IDSearchStatusDetails.Visible = true; }
            if (A[85] == false && A[41] == false && A[86] == false && A[87] == false && A[42] == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCookie();
        }
    }

}