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

public partial class Cpanel_CPanelSetting_MPCPanel : System.Web.UI.MasterPage
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

                // الإعدادات الرئيسية
                A[71] = Convert.ToBoolean(dtViewProfil.Rows[0]["A71"]);
                pnlSystem.Visible = true;
                if (A[71]) { IDSetting.Visible = true; }
                if (A[71] == false)
                    pnlSystem.Visible = false;

                // المجموعات
                A[5] = Convert.ToBoolean(dtViewProfil.Rows[0]["A5"]);
                A[6] = Convert.ToBoolean(dtViewProfil.Rows[0]["A6"]);
                pnlGroup.Visible = true;
                pnlGroupAdmin.Visible = true;
                if (A[5]) { IDGroup.Visible = true; IDGroupAdmin.Visible = true; }
                if (A[6]) { IDGroupAdd.Visible = true; IDGroupAdminAdd.Visible = true; }
                if (A[5] == false && A[6] == false)
                {
                    pnlGroup.Visible = false; pnlGroupAdmin.Visible = false;
                }
                // المستخدمين
                A[7] = Convert.ToBoolean(dtViewProfil.Rows[0]["A7"]);
                A[8] = Convert.ToBoolean(dtViewProfil.Rows[0]["A8"]);
                pnlAdmin.Visible = true;
                if (A[7]) { IDAdmin.Visible = true; }
                if (A[8]) { IDAdminAdd.Visible = true; IDMultiLinkingAdd.Visible = true; }
                if (A[7] == false && A[8] == false)
                    pnlAdmin.Visible = false;
                //}
                if (A[71] == false && A[5] == false && A[6] == false && A[7] == false && A[8] == false)
                    Response.Redirect("~/Cpanel/CHome/PageNotAccess.aspx");
            }
        }
        else
            Response.Redirect("/Cpanel/LogOut.aspx");
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
