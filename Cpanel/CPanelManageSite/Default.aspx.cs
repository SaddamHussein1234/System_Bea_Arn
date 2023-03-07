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

public partial class Cpanel_CPanelManageSite_Default : System.Web.UI.Page
{
    string IDUniq;
    private void GetCookie()
    {
        try
        {
            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
        }
        catch
        {
            Response.Redirect("/Cpanel/LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        try
        {
            ClassAdmin_Arn CA = new ClassAdmin_Arn();
            CA._IDUniq = IDUniq;
            CA._IsDelete = false;
            DataTable dtViewProfil = new DataTable();
            dtViewProfil = CA.BArnAdminGetByIDUniq();
            if (dtViewProfil.Rows.Count > 0)
            {
                lblQariah.Text = "CMS - نظام إدارة الموقع , ";
                lblQariah.Text += Wellcome() + " / " + dtViewProfil.Rows[0]["FirstName"].ToString();

                bool[] A = new bool[250];

                // قائمة الموقع
                A[9] = Convert.ToBoolean(dtViewProfil.Rows[0]["A9"]);
                A[10] = Convert.ToBoolean(dtViewProfil.Rows[0]["A10"]);
                pnlMenuSite.Visible = true;
                if (A[9] == false && A[10] == false)
                {
                    pnlMenuSite.Visible = false;
                }

                // قائمة المقالات
                A[11] = Convert.ToBoolean(dtViewProfil.Rows[0]["A11"]);
                A[12] = Convert.ToBoolean(dtViewProfil.Rows[0]["A12"]);
                pnlArticle.Visible = true;
                if (A[11] == false && A[12] == false)
                {
                    pnlArticle.Visible = false;
                }

                // البوم الصور
                A[15] = Convert.ToBoolean(dtViewProfil.Rows[0]["A15"]);
                A[16] = Convert.ToBoolean(dtViewProfil.Rows[0]["A16"]);
                pnlMyAlbum.Visible = true;
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

                if (Convert.ToBoolean(dtViewProfil.Rows[0]["_Two_Factor_Enabled_"]) || Convert.ToBoolean(dtViewProfil.Rows[0]["_SMS_Enabled_"]))
                    IDMessageWarning.Visible = false;
                else
                    IDMessageWarning.Visible = true;
            }
        }
        catch (Exception)
        {
            Response.Redirect("/Cpanel/LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            System.Threading.Thread.Sleep(100);
            CheckAccountAdmin();
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