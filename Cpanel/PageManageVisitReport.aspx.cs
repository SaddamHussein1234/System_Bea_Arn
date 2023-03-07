using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageManageVisitReport : System.Web.UI.Page
{
    string IDUser, IDUniq, UserN, UserCard, IDAdmin2;
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

            HttpCookie CookeCheck;  // اسم المستخدم
            CookeCheck = Request.Cookies["__User_True_"];
            UserN = ClassSaddam.UnprotectPassword(CookeCheck["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

            HttpCookie CookeUser;  // رقم المستخدم
            CookeUser = Request.Cookies["__UserUniqAdmin_True_"];
            IDAdmin2 = ClassSaddam.UnprotectPassword(CookeUser["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

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

            A[95] = Convert.ToBoolean(dtViewProfil.Rows[0]["A95"]);
            A[96] = Convert.ToBoolean(dtViewProfil.Rows[0]["A96"]);
            A[97] = Convert.ToBoolean(dtViewProfil.Rows[0]["A97"]);
            A[50] = Convert.ToBoolean(dtViewProfil.Rows[0]["A50"]);
            A[51] = Convert.ToBoolean(dtViewProfil.Rows[0]["A51"]);

            if (A[95]) { IDVisitReportAdd.Visible = true; }
            if (A[96]) { IDVisitReportByModerAdd.Visible = true; }
            if (A[97]) { IDVisitReportByRaeesAllagnahAdd.Visible = true; }
            if (A[50]) { IDVisitReportView.Visible = true; IDVisitReportByDevice.Visible = true; IDVisitReportByHouse.Visible = true; }
            if (A[51]) { IDVisitReportDetailsView.Visible = true; }
            if (A[95] == false && A[96] == false && A[97] == false && A[50] == false && A[51] == false)
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