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

public partial class Cpanel_CPanelManageZakat_MPCPanel : System.Web.UI.MasterPage
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
                    if (Convert.ToBoolean(dtViewProfil.Rows[0]["IsSuperAdmin"]) == false)
                        IDHRM.Visible = true;
                    bool[] A = new bool[250];

                    IDCategory.Visible = true;
                    A[134] = Convert.ToBoolean(dtViewProfil.Rows[0]["A134"]);
                    A[135] = Convert.ToBoolean(dtViewProfil.Rows[0]["A135"]);
                    if (A[134] == false && A[135] == false)
                        IDCategory.Visible = false;

                    A[136] = Convert.ToBoolean(dtViewProfil.Rows[0]["A136"]);
                    if (A[136])
                    {
                        IDDeedDonationInKindAll.Visible = true;
                        IDDeedDonationInKindView.Visible = true;
                    }

                    A[137] = Convert.ToBoolean(dtViewProfil.Rows[0]["A137"]);
                    if (A[137])
                        IDDeedDonationInKindAdd.Visible = true;

                    A[138] = Convert.ToBoolean(dtViewProfil.Rows[0]["A138"]);
                    if (A[138])
                        IDDeedDonationInKindInCome.Visible = true;

                    pnlManageProductWarehouse.Visible = true;
                    A[142] = Convert.ToBoolean(dtViewProfil.Rows[0]["A142"]);
                    A[140] = Convert.ToBoolean(dtViewProfil.Rows[0]["A140"]);
                    A[141] = Convert.ToBoolean(dtViewProfil.Rows[0]["A141"]);
                    if (A[142] == true) { IDManageProductWarehouseStorekeeperAdd.Visible = true; }
                    if (A[140] == true) { IDManageProductWarehouseCashierAdd.Visible = true; }
                    if (A[141] == true) { IDManageProductWarehouseApprovalOfTheDirectorAdd.Visible = true; }

                    if (A[140] == false && A[141] == false && A[142] == false)
                        pnlManageProductWarehouse.Visible = false;
                    //}
                    if (A[134] == false && A[135] == false && A[136] == false && A[137] == false && A[138] == false && A[140] == false && A[141] == false && A[142] == false)
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
