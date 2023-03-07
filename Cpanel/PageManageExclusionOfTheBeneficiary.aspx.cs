using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageManageExclusionOfTheBeneficiary : System.Web.UI.Page
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

            A[101] = Convert.ToBoolean(dtViewProfil.Rows[0]["A101"]);
            A[102] = Convert.ToBoolean(dtViewProfil.Rows[0]["A102"]);
            A[103] = Convert.ToBoolean(dtViewProfil.Rows[0]["A103"]);
            A[54] = Convert.ToBoolean(dtViewProfil.Rows[0]["A54"]);
            A[55] = Convert.ToBoolean(dtViewProfil.Rows[0]["A55"]);

            if (A[101]) { IDExclusionOfTheBeneficiaryAdd.Visible = true; }
            if (A[102]) { IDExclusionOfTheBeneficiaryByModerViewAdd.Visible = true; }
            if (A[103]) { IDExclusionOfTheBeneficiaryByRaeesAdd.Visible = true; }
            if (A[54]) { IDExclusionOfTheBeneficiaryView.Visible = true; }
            if (A[55]) { IDExclusionOfTheBeneficiaryDetailsView.Visible = true; }
            if (A[101] == false && A[102] == false && A[103] == false && A[54] == false && A[55] == false)
            {
                Response.Redirect("LogOut.aspx");
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