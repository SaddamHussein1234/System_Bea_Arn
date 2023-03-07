using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_Default : System.Web.UI.Page
{
    string IDUser = string.Empty, IDUniq = string.Empty, UserN = string.Empty, UserCard = string.Empty, IDAdmin2 = string.Empty;
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
            Response.Redirect("/Cpanel/LogOut.aspx");
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

            pnlMenuArn.Visible = true;
            // القُرى
            A[31] = Convert.ToBoolean(dtViewProfil.Rows[0]["A31"]);
            A[77] = Convert.ToBoolean(dtViewProfil.Rows[0]["A77"]);

            // حالة المستفيد
            A[32] = Convert.ToBoolean(dtViewProfil.Rows[0]["A32"]);
            A[78] = Convert.ToBoolean(dtViewProfil.Rows[0]["A78"]);

            // نوع المسكن
            A[33] = Convert.ToBoolean(dtViewProfil.Rows[0]["A33"]);
            A[79] = Convert.ToBoolean(dtViewProfil.Rows[0]["A79"]);

            // الدخل الشهري
            A[34] = Convert.ToBoolean(dtViewProfil.Rows[0]["A34"]);
            A[80] = Convert.ToBoolean(dtViewProfil.Rows[0]["A80"]);

            // حالة المسكن
            A[35] = Convert.ToBoolean(dtViewProfil.Rows[0]["A35"]);
            A[81] = Convert.ToBoolean(dtViewProfil.Rows[0]["A81"]);

            // نوع الدعم
            A[36] = Convert.ToBoolean(dtViewProfil.Rows[0]["A36"]);
            A[82] = Convert.ToBoolean(dtViewProfil.Rows[0]["A82"]);

            // قرابة عائلة المستفيد
            A[37] = Convert.ToBoolean(dtViewProfil.Rows[0]["A37"]);
            A[83] = Convert.ToBoolean(dtViewProfil.Rows[0]["A83"]);

            // صلة قرابة المستفيد
            A[38] = Convert.ToBoolean(dtViewProfil.Rows[0]["A38"]);
            A[84] = Convert.ToBoolean(dtViewProfil.Rows[0]["A84"]);
            if (A[31] == false && A[32] == false && A[33] == false && A[34] == false && A[35] == false && A[36] == false && A[37] == false && A[38] == false && A[77] == false && A[78] == false && A[79] == false && A[80] == false && A[81] == false && A[82] == false && A[83] == false && A[84] == false)
            {
                pnlMenuArn.Visible = false;
            }

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
                pnlProductMatterOfExchange.Visible = false;

            if (Convert.ToBoolean(dtViewProfil.Rows[0]["_Two_Factor_Enabled_"]) || Convert.ToBoolean(dtViewProfil.Rows[0]["_SMS_Enabled_"]))
                IDMessageWarning.Visible = false;
            else
                IDMessageWarning.Visible = true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCookie();
            FGetAllMostafeed();
        }
        
    }

    private void FGetAllMostafeed()
    {
        try
        {
            DataTable dtToAll = new DataTable();
            dtToAll = ClassDataAccess.GetData("SELECT Count(*) As 'CountAll' FROM [dbo].[RasAlEstemarah] With(NoLock) Inner join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where RasAlEstemarah.IsDelete = @0 And Quaem.AlQriah <> @1", Convert.ToString(false), "مناطق_أخرى");
            if (dtToAll.Rows.Count > 0)
            {
                lblCountAllMostafeed.Text = dtToAll.Rows[0]["CountAll"].ToString();
            }
            FGetMostafeedByDaaem();
        }
        catch (Exception)
        {

        }
    }

    public string GetPercintAllMostafeed()
    {
        string XResult = "";
        try
        {
            double PerSent;
            //PerSent = 100 / 5000;
            PerSent = 0.1;
            XResult = Convert.ToString(Math.Abs(Math.Round(Convert.ToDouble(lblCountAllMostafeed.Text) * PerSent, 1)));
        }
        catch (Exception)
        {

        }
        return XResult;
    }

    private void FGetMostafeedByDaaem()
    {
        DataTable dtToDaaem = new DataTable();
        dtToDaaem = ClassDataAccess.GetData("SELECT Count(*) As 'CountByDaaem' FROM [dbo].[RasAlEstemarah] With(NoLock) Inner join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where TypeMostafeed = @0 And RasAlEstemarah.IsDelete = @1 And Quaem.AlQriah <> @2 ", Convert.ToString("دائم"), Convert.ToString(false), "مناطق_أخرى");
        if (dtToDaaem.Rows.Count > 0)
        {
            lblCountByDaaem.Text = dtToDaaem.Rows[0]["CountByDaaem"].ToString();
        }
        FGetMostafeedByMostabaad();
    }

    public string GetPercintByDaaem()
    {
        string XResult = "";
        try
        {
            double PerSent;
            PerSent = 100 / Convert.ToDouble(lblCountAllMostafeed.Text);
            XResult = Convert.ToString(Math.Abs(Math.Round(Convert.ToDouble(lblCountByDaaem.Text) * PerSent, 1)));
        }
        catch (Exception)
        {

        }
        return XResult;
    }

    private void FGetMostafeedByMostabaad()
    {
        DataTable dtToDaaem = new DataTable();
        dtToDaaem = ClassDataAccess.GetData("SELECT Count(*) As 'CountByMostabaad' FROM [dbo].[RasAlEstemarah] With(NoLock) Inner join Quaem on Quaem.IDItem = RasAlEstemarah.AlQaryah Where TypeMostafeed = @0 And RasAlEstemarah.IsDelete = @1 And Quaem.AlQriah <> @2", Convert.ToString("مستبعد"), Convert.ToString(false), "مناطق_أخرى");
        if (dtToDaaem.Rows.Count > 0)
        {
            lblCountByMostabaad.Text = dtToDaaem.Rows[0]["CountByMostabaad"].ToString();
        }
    }

    public string GetPercintByMostabaad()
    {
        string XResult = "";
        try
        {
            double PerSent;
            PerSent = 100 / Convert.ToDouble(lblCountAllMostafeed.Text);
            XResult = Convert.ToString(Math.Abs(Math.Round(Convert.ToDouble(lblCountByMostabaad.Text) * PerSent, 1)));
        }
        catch (Exception)
        {

        }
        return XResult;
    }

}