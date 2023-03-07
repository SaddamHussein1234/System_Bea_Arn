using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageManageAfieldVisit : System.Web.UI.Page
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
            A[92] = Convert.ToBoolean(dtViewProfil.Rows[0]["A92"]);
            A[93] = Convert.ToBoolean(dtViewProfil.Rows[0]["A93"]);
            A[94] = Convert.ToBoolean(dtViewProfil.Rows[0]["A94"]);
            A[47] = Convert.ToBoolean(dtViewProfil.Rows[0]["A47"]);
            A[48] = Convert.ToBoolean(dtViewProfil.Rows[0]["A48"]);
            A[49] = Convert.ToBoolean(dtViewProfil.Rows[0]["A49"]);
            if (A[92]) { IDAfieldVisitAdd.Visible = true; }
            if (A[93]) { IDAfieldVisitPendingApprovalAdd.Visible = true; }
            //if (A[94]) { IDAfieldVisitPendingApprovalByRaeesAdd.Visible = true; }
            if (A[47]) { IDAfieldVisitApprovalView.Visible = true; }
            if (A[48]) { IDAfieldVisitNotApprovedView.Visible = true; }
            if (A[49]) { IDAfieldVisitDetailsView.Visible = true; }
            if (A[92] == false && A[93] == false && A[94] == false && A[47] == false && A[48] == false && A[49] == false)
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