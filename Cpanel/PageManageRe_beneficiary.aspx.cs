using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageManageRe_beneficiary : System.Web.UI.Page
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

            A[98] = Convert.ToBoolean(dtViewProfil.Rows[0]["A98"]);
            A[99] = Convert.ToBoolean(dtViewProfil.Rows[0]["A99"]);
            A[100] = Convert.ToBoolean(dtViewProfil.Rows[0]["A100"]);
            A[52] = Convert.ToBoolean(dtViewProfil.Rows[0]["A52"]);
            A[53] = Convert.ToBoolean(dtViewProfil.Rows[0]["A53"]);

            if (A[98]) { IDRe_beneficiaryAdd.Visible = true; }
            if (A[99]) { IDRe_beneficiaryByModerAdd.Visible = true; }
            if (A[100]) { IDRe_beneficiaryByRaeesAdd.Visible = true; }
            if (A[52]) { IDRe_beneficiaryView.Visible = true; }
            if (A[53]) { IDRe_beneficiaryDetailsView.Visible = true; }
            if (A[98] == false && A[99] == false && A[100] == false && A[52] == false && A[53] == false)
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