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

public partial class Cpanel_CPGeneralAssembly_MPCPanel : System.Web.UI.MasterPage
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
        try
        {
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
                    //    Response.Redirect("LogOut.aspx");
                    //else
                    //{
                    lblFirstName.Text = dtViewProfil.Rows[0]["FirstName"].ToString();
                    //if (Convert.ToBoolean(dtViewProfil.Rows[0]["IsSuperAdmin"]) == false)
                        IDHRM.Visible = true;
                    bool[] A = new bool[250];
                    PnlAdmin.Visible = true;
                    PnlGeneralAssembly.Visible = true;
                    A[143] = Convert.ToBoolean(dtViewProfil.Rows[0]["A143"]);
                    A[144] = Convert.ToBoolean(dtViewProfil.Rows[0]["A144"]);

                    if (A[143]) { IDAdmin.Visible = true; IDGeneralAssembly.Visible = true; IDGeneralAssemblyView.Visible = true; }
                    if (A[144]) { IDAdminAdd.Visible = true; IDGeneralAssemblyAdd.Visible = true; }
                    if (A[143] == false && A[144] == false)
                        PnlGeneralAssembly.Visible = false;

                    A[145] = Convert.ToBoolean(dtViewProfil.Rows[0]["A145"]);
                    if (A[145]) PnlGeneralAssemblyAllow.Visible = true;

                    PnlGeneralAssemblyBill.Visible = true;
                    A[146] = Convert.ToBoolean(dtViewProfil.Rows[0]["A146"]);
                    A[147] = Convert.ToBoolean(dtViewProfil.Rows[0]["A147"]);
                    if (A[146]) { IDGeneralAssemblyBill.Visible = true; IDGeneralAssemblyBillView.Visible = true; }
                    if (A[147]) IDGeneralAssemblyBillAdd.Visible = false;
                    if (A[143] == false && A[144] == false)
                        PnlGeneralAssemblyBill.Visible = false;

                    PnlGeneralAssemblyBillAllow.Visible = true;
                    A[148] = Convert.ToBoolean(dtViewProfil.Rows[0]["A148"]);
                    A[149] = Convert.ToBoolean(dtViewProfil.Rows[0]["A149"]);
                    if (A[148]) IDGeneralAssemblyBillAmeen.Visible = true;
                    if (A[149]) IDGeneralAssemblyBillRaees.Visible = true;
                    if (A[148] == false && A[149] == false)
                        PnlGeneralAssemblyBillAllow.Visible = false;
                    //}
                    if (A[143] == false && A[144] == false && A[145] == false && A[146] == false && A[147] == false && A[148] == false && A[149] == false)
                        Response.Redirect("~/Cpanel/CHome/PageNotAccess.aspx");
                }
            }
            else
                Response.Redirect("/Cpanel/LogOut.aspx");
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
