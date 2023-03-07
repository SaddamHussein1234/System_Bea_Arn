using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelManageSite_MPCPanel : System.Web.UI.MasterPage
{
    string IDUser, IDUniq, UserN, IDAdmin2, IDCookie;
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

            HttpCookie CookeID;  // رقم المستخدم
            CookeID = Request.Cookies["__User_Screen_"];
            IDCookie = ClassSaddam.UnprotectPassword(CookeID["Url"], "www.ITFY-Edu.Net_ProtectByITFY");

        }
        catch
        {
            Response.Redirect("/Cpanel/LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();

        ClassAdmin_Arn CA = new ClassAdmin_Arn();
        CA._IDUniq = IDUniq;
        CA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CA.BArnAdminGetByIDUniq();
        if ((IDUser == IDAdmin2) && (IDUser == dtViewProfil.Rows[0]["ID_Item"].ToString()) && (IDUniq == dtViewProfil.Rows[0]["IDUniqUser"].ToString()) && (UserN == dtViewProfil.Rows[0]["User_Name_"].ToString()) && (IDCookie == dtViewProfil.Rows[0]["_ID_Cookie_"].ToString()))
        {
            if (dtViewProfil.Rows.Count > 0)
            {
                lblNotifications.Text = Repostry_JobAssignment_Map_.FGetCount("GetByAdminActiveCountMain", 0, Guid.Empty, Guid.Empty,
                        new Guid(IDUniq), Guid.Empty, string.Empty, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd"), string.Empty,
                        false, true, "_Count").ToString();
                //if (Convert.ToBoolean(dtViewProfil.Rows[0]["IsBaheth"]) && Convert.ToBoolean(dtViewProfil.Rows[0]["IsModer"]) == false)
                //{
                //    Response.Redirect("LogOut.aspx");
                //}
                //else
                //{
                lblFirstName.Text = dtViewProfil.Rows[0]["FirstName"].ToString();
                    //if (Convert.ToBoolean(dtViewProfil.Rows[0]["IsSuperAdmin"]) == false)
                        IDHRM.Visible = true;
                    bool[] A = new bool[250];

                    // قائمة الموقع
                    A[9] = Convert.ToBoolean(dtViewProfil.Rows[0]["A9"]);
                    A[10] = Convert.ToBoolean(dtViewProfil.Rows[0]["A10"]);
                    pnlMenuSite.Visible = true;
                    if (A[9]) { IDMenuView.Visible = true; }
                    if (A[10]) { IDMenuAdd.Visible = true; }
                    if (A[9] == false && A[10] == false)
                    {
                        pnlMenuSite.Visible = false;
                    }

                    // قائمة المقالات
                    A[11] = Convert.ToBoolean(dtViewProfil.Rows[0]["A11"]);
                    A[12] = Convert.ToBoolean(dtViewProfil.Rows[0]["A12"]);
                    pnlArticle.Visible = true;
                    if (A[11]) { IDArticle.Visible = true; }
                    if (A[12]) { IDArticleAdd.Visible = true; }
                    if (A[11] == false && A[12] == false)
                    {
                        pnlArticle.Visible = false;
                    }

                    // البوم الصور
                    A[15] = Convert.ToBoolean(dtViewProfil.Rows[0]["A15"]);
                    A[16] = Convert.ToBoolean(dtViewProfil.Rows[0]["A16"]);
                    pnlMyAlbum.Visible = true;
                    if (A[15]) { LAlbumView.Visible = true; }
                    if (A[16]) { LAlbumAdd.Visible = true; }
                    if (A[15] == false && A[16] == false)
                    {
                        pnlMyAlbum.Visible = false;
                    }

                    // قائمة الرسائل
                    A[21] = Convert.ToBoolean(dtViewProfil.Rows[0]["A21"]);
                    if (A[21]) { IDMessageView.Visible = true; }

                    // الإعدادات الرئيسية
                    A[72] = Convert.ToBoolean(dtViewProfil.Rows[0]["A72"]);
                    A[73] = Convert.ToBoolean(dtViewProfil.Rows[0]["A73"]); A[74] = Convert.ToBoolean(dtViewProfil.Rows[0]["A74"]);
                    pnlSystem.Visible = true;
                    if (A[72]) { IDSettingTitle.Visible = true; }
                    if (A[73]) { IDSettingInfo.Visible = true; }
                    if (A[74]) { IDAboutAr.Visible = true; }
                    if (A[72] == false && A[73] == false && A[74] == false)
                        pnlSystem.Visible = false;

                    if (A[9] == false && A[10] == false && A[11] == false && A[12] == false && A[15] == false && A[16] == false && A[21] == false && A[72] == false && A[73] == false && A[74] == false)
                        Response.Redirect("~/Cpanel/CHome/PageNotAccess.aspx");
                //}
            }
        }
        else
        {
            Response.Redirect("/Cpanel/LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            Wellcome();
            lblYears.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy");
        }
    }

    private void Wellcome()
    {
        try
        {
            DateTime time = ClassDataAccess.GetCurrentTime();
            if ((time > Convert.ToDateTime("10:00:00 AM")) && (time < Convert.ToDateTime("11:59:50 AM")))
                lblLestName.Text = "صباح الخير";
            else if ((time > Convert.ToDateTime("12:00:00 PM")) && (time < Convert.ToDateTime("5:00:00 PM")))
                lblLestName.Text = "نهارك سعيد";
            else if ((time > Convert.ToDateTime("5:01:00 PM")) && (time < Convert.ToDateTime("11:59:50 PM")))
                lblLestName.Text = "مساء الخير";
            else if ((time > Convert.ToDateTime("12:00:00 AM")) && (time < Convert.ToDateTime("9:59:50 PM")))
                lblLestName.Text = "صباح الخير";
            else
                lblLestName.Text = "مرحباً بك";
            System.Threading.Thread.Sleep(50);
        }
        catch (Exception)
        {
            lblLestName.Text = "مرحباً بك";
        }
    }

}
