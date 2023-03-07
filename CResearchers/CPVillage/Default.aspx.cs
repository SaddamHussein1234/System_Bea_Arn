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

public partial class CResearchers_CPVillage_Default : System.Web.UI.Page
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
        if ((IDUser == IDAdmin2) && (IDUser == dtViewProfil.Rows[0]["ID_Item"].ToString()) && (IDUniq == dtViewProfil.Rows[0]["IDUniqUser"].ToString()) && (UserN == dtViewProfil.Rows[0]["User_Name_"].ToString()))
        {
            if (dtViewProfil.Rows.Count > 0)
            {
                //Fetch the Cookie using its Key.  
                HttpCookie IDCookie = Request.Cookies["AllowByVillage"];

                //If Cookie exists fetch its value.  
                string IDVillage = IDCookie != null ? IDCookie.Value.Split('=')[1] : "undefined";

                lblQariah.Text = " نظام البحث الاجتماعي , ";
                lblQariah.Text += Wellcome() + " / " + dtViewProfil.Rows[0]["FirstName"].ToString();

                DataTable dt = new DataTable();
                dt = ClassDataAccess.GetData("SELECT Top(1) tbl_MultiQariah.IDItem,[IDAdminJoin],[IDQariah],AlQriah,[DateAddCall],[IDAdminAdd],[A1],[A2],[A3],[A4],[A5],tbl_MultiQariah.IsDelete FROM [dbo].[tbl_MultiQariah] Inner join Quaem on Quaem.IDItem = tbl_MultiQariah.IDQariah Where IDQariah = @0 And tbl_MultiQariah.IsDelete = @1", IDVillage, Convert.ToString(false));
                if (dt.Rows.Count > 0)
                    lblQariah.Text += " , " + " قرية " + dt.Rows[0]["AlQriah"].ToString();
                else
                    Response.Redirect("LogOut.aspx");

                //
                bool[] A = new bool[150];

                A[39] = Convert.ToBoolean(dtViewProfil.Rows[0]["A39"]); A[40] = Convert.ToBoolean(dtViewProfil.Rows[0]["A40"]);
                A[75] = Convert.ToBoolean(dtViewProfil.Rows[0]["A75"]); A[76] = Convert.ToBoolean(dtViewProfil.Rows[0]["A76"]);
                A[121] = Convert.ToBoolean(dtViewProfil.Rows[0]["A121"]); A[122] = Convert.ToBoolean(dtViewProfil.Rows[0]["A122"]); A[123] = Convert.ToBoolean(dtViewProfil.Rows[0]["A123"]);
                pnlMostafeed.Visible = true; pnlMostafeedStatistic.Visible = true;
                if (A[39] == false && A[40] == false && A[75] == false && A[76] == false && A[121] == false && A[122] == false && A[123] == false)
                {
                    pnlMostafeed.Visible = false; pnlMostafeedStatistic.Visible = false;
                }

                A[85] = Convert.ToBoolean(dtViewProfil.Rows[0]["A85"]); A[41] = Convert.ToBoolean(dtViewProfil.Rows[0]["A41"]);
                A[86] = Convert.ToBoolean(dtViewProfil.Rows[0]["A86"]); A[87] = Convert.ToBoolean(dtViewProfil.Rows[0]["A87"]);
                A[42] = Convert.ToBoolean(dtViewProfil.Rows[0]["A42"]);
                pnlSearchStatus.Visible = true;
                if (A[85] == false && A[41] == false && A[86] == false && A[87] == false && A[42] == false)
                {
                    pnlSearchStatus.Visible = false;
                }

                A[88] = Convert.ToBoolean(dtViewProfil.Rows[0]["A88"]); A[89] = Convert.ToBoolean(dtViewProfil.Rows[0]["A89"]);
                A[43] = Convert.ToBoolean(dtViewProfil.Rows[0]["A43"]); A[44] = Convert.ToBoolean(dtViewProfil.Rows[0]["A44"]);
                pnlAcceptanceDecision.Visible = true;
                if (A[88] == false && A[89] == false && A[43] == false && A[44] == false)
                {
                    pnlAcceptanceDecision.Visible = false;
                }

                A[90] = Convert.ToBoolean(dtViewProfil.Rows[0]["A90"]); A[91] = Convert.ToBoolean(dtViewProfil.Rows[0]["A91"]);
                A[45] = Convert.ToBoolean(dtViewProfil.Rows[0]["A45"]); A[46] = Convert.ToBoolean(dtViewProfil.Rows[0]["A46"]);
                pnlTecisionToExclude.Visible = true;
                if (A[90] == false && A[91] == false && A[45] == false && A[46] == false)
                {
                    pnlTecisionToExclude.Visible = false;
                }

                A[92] = Convert.ToBoolean(dtViewProfil.Rows[0]["A92"]);
                A[93] = Convert.ToBoolean(dtViewProfil.Rows[0]["A93"]);
                A[94] = Convert.ToBoolean(dtViewProfil.Rows[0]["A94"]);
                A[47] = Convert.ToBoolean(dtViewProfil.Rows[0]["A47"]);
                A[48] = Convert.ToBoolean(dtViewProfil.Rows[0]["A48"]);
                A[49] = Convert.ToBoolean(dtViewProfil.Rows[0]["A49"]);

                pnlAfieldVisi.Visible = true;
                if (A[92] == false && A[93] == false && A[94] == false && A[47] == false && A[48] == false && A[49] == false)
                {
                    pnlAfieldVisi.Visible = false;
                }

                A[95] = Convert.ToBoolean(dtViewProfil.Rows[0]["A95"]);
                A[96] = Convert.ToBoolean(dtViewProfil.Rows[0]["A96"]);
                A[97] = Convert.ToBoolean(dtViewProfil.Rows[0]["A97"]);
                A[50] = Convert.ToBoolean(dtViewProfil.Rows[0]["A50"]);
                A[51] = Convert.ToBoolean(dtViewProfil.Rows[0]["A51"]);
                pnlVisitReport.Visible = true;
                if (A[95] == false && A[96] == false && A[97] == false && A[50] == false && A[51] == false)
                {
                    pnlVisitReport.Visible = false;
                }

                A[98] = Convert.ToBoolean(dtViewProfil.Rows[0]["A98"]);
                A[99] = Convert.ToBoolean(dtViewProfil.Rows[0]["A99"]);
                A[100] = Convert.ToBoolean(dtViewProfil.Rows[0]["A100"]);
                A[52] = Convert.ToBoolean(dtViewProfil.Rows[0]["A52"]);
                A[53] = Convert.ToBoolean(dtViewProfil.Rows[0]["A53"]);
                pnlRe_beneficiary.Visible = true;
                if (A[98] == false && A[99] == false && A[100] == false && A[52] == false && A[53] == false)
                {
                    pnlRe_beneficiary.Visible = false;
                }

                A[101] = Convert.ToBoolean(dtViewProfil.Rows[0]["A101"]);
                A[102] = Convert.ToBoolean(dtViewProfil.Rows[0]["A102"]);
                A[103] = Convert.ToBoolean(dtViewProfil.Rows[0]["A103"]);
                A[54] = Convert.ToBoolean(dtViewProfil.Rows[0]["A54"]);
                A[55] = Convert.ToBoolean(dtViewProfil.Rows[0]["A55"]);
                pnlExclusionOfTheBeneficiary.Visible = true;
                if (A[101] == false && A[102] == false && A[103] == false && A[54] == false && A[55] == false)
                {
                    pnlExclusionOfTheBeneficiary.Visible = false;
                }

                A[104] = Convert.ToBoolean(dtViewProfil.Rows[0]["A104"]);
                A[105] = Convert.ToBoolean(dtViewProfil.Rows[0]["A105"]);
                A[56] = Convert.ToBoolean(dtViewProfil.Rows[0]["A56"]);
                A[57] = Convert.ToBoolean(dtViewProfil.Rows[0]["A57"]);
                A[58] = Convert.ToBoolean(dtViewProfil.Rows[0]["A58"]);
                pnlConvertedCases.Visible = true;
                if (A[104] == false && A[105] == false && A[56] == false && A[57] == false && A[58] == false)
                {
                    pnlConvertedCases.Visible = false;
                }

                A[106] = Convert.ToBoolean(dtViewProfil.Rows[0]["A106"]);
                A[59] = Convert.ToBoolean(dtViewProfil.Rows[0]["A59"]);
                A[60] = Convert.ToBoolean(dtViewProfil.Rows[0]["A60"]);
                A[107] = Convert.ToBoolean(dtViewProfil.Rows[0]["A107"]);
                A[108] = Convert.ToBoolean(dtViewProfil.Rows[0]["A108"]);
                A[109] = Convert.ToBoolean(dtViewProfil.Rows[0]["A109"]);
                A[110] = Convert.ToBoolean(dtViewProfil.Rows[0]["A110"]);
                A[111] = Convert.ToBoolean(dtViewProfil.Rows[0]["A111"]);
                pnlProductMatterOfExchange.Visible = true;
                if (A[106] == false && A[59] == false && A[60] == false && A[107] == false && A[108] == false && A[109] == false && A[110] == false && A[111] == false)
                {
                    pnlProductMatterOfExchange.Visible = false;
                }
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

    private string Wellcome()
    {
        string XResult = "0";
        try
        {
            DateTime time = ClassDataAccess.GetCurrentTime();
            if ((time > Convert.ToDateTime("10:00:00 AM")) && (time < Convert.ToDateTime("11:59:50 AM")))
                XResult = "صباح الخير";
            else if ((time > Convert.ToDateTime("12:00:00 PM")) && (time < Convert.ToDateTime("5:00:00 PM")))
                XResult = "نهارك سعيد";
            else if ((time > Convert.ToDateTime("5:01:00 PM")) && (time < Convert.ToDateTime("11:59:50 PM")))
                XResult = "مساء الخير";
            else if ((time > Convert.ToDateTime("12:00:00 AM")) && (time < Convert.ToDateTime("9:59:50 PM")))
                XResult = "صباح الخير";
            else
                XResult = "مرحباً بك";
            System.Threading.Thread.Sleep(50);
        }
        catch (Exception)
        {
            XResult = "مرحباً بك";
        }
        return XResult;
    }

}