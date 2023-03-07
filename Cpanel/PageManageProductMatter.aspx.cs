using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageManageProductMatter : System.Web.UI.Page
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

            A[106] = Convert.ToBoolean(dtViewProfil.Rows[0]["A106"]);
            A[59] = Convert.ToBoolean(dtViewProfil.Rows[0]["A59"]);
            A[60] = Convert.ToBoolean(dtViewProfil.Rows[0]["A60"]);
            A[61] = Convert.ToBoolean(dtViewProfil.Rows[0]["A61"]);
            A[107] = Convert.ToBoolean(dtViewProfil.Rows[0]["A107"]);
            A[108] = Convert.ToBoolean(dtViewProfil.Rows[0]["A108"]);
            A[109] = Convert.ToBoolean(dtViewProfil.Rows[0]["A109"]);
            A[110] = Convert.ToBoolean(dtViewProfil.Rows[0]["A110"]);
            A[111] = Convert.ToBoolean(dtViewProfil.Rows[0]["A111"]);

            if (A[106]) { IDManageProductMatterOfExchangeAdd.Visible = true; }
            if (A[59]) { IDManageProductExchangeOrdersView.Visible = true; }
            if (A[60]) { IDManageProductSupportByBeneficiaryView.Visible = true; }
            if (A[107]) { IDManageProductStorekeeperAdd.Visible = true; }
            if (A[108]) { IDManageProductApprovalOfTheDirectorAdd.Visible = true; }
            if (A[109]) { IDManageProductCashierAdd.Visible = true; }
            if (A[110]) { IDManageProductChairmanOfTheBoardAdd.Visible = true; }
            if (A[111]) { IDManageProductFileSearchersAdd.Visible = true; }
            if (A[61]) { IDManageProductAddThePriceToOrder.Visible = true; }
            if (A[106] == false && A[59] == false && A[60] == false && A[61] == false && A[107] == false && A[108] == false && A[109] == false && A[110] == false && A[111] == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
        else
        {
            Response.Redirect("LogOut.aspx");
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