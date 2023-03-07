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

public partial class Cpanel_ERP_EOS_MPCPanel : System.Web.UI.MasterPage
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
                //Response.Redirect("LogOut.aspx");
                //else
                //{
                lblFirstName.Text = dtViewProfil.Rows[0]["FirstName"].ToString();
                //if (Convert.ToBoolean(dtViewProfil.Rows[0]["IsSuperAdmin"]) == false)
                    IDHRM.Visible = true;
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
                A[129] = Convert.ToBoolean(dtViewProfil.Rows[0]["A129"]);
                A[130] = Convert.ToBoolean(dtViewProfil.Rows[0]["A130"]);
                pnlProductMatterOfExchange.Visible = true;
                if (A[106])
                {
                    pnlEyesupport.Visible = true;
                    IDManageProductMatterOfExchangeAdd.Visible = true;
                    IDManageProductMatterOfExchangeGroupAdd.Visible = true;
                    IDManageProductMatterOfExchangeForDeviceAdd.Visible = true;
                    IDAnOrderToTxchangeHomeFurnishingAdd.Visible = true;
                    IDManageProductRestorationAndConstructionAdd.Visible = true;
                    IDManageProductMatterOfExchangeForDamagedAdd.Visible = false;

                    pnlSupportForPrisms.Visible = true;
                    IDSupportForPrisms.Visible = true;
                }
                else
                    pnlEyesupport.Visible = false;
                if (A[59]) { IDManageProductExchangeOrdersView.Visible = true; }
                if (A[60]) { IDManageProductSupportByBeneficiaryView.Visible = true; IDManageProductSupportByBeneficiaryViewMulti.Visible = true; }
                if (A[61]) { IDManageProductAddThePriceToOrder.Visible = true; }
                if (A[107]) { IDManageProductStorekeeperAdd.Visible = true; }
                if (A[108]) { IDManageProductApprovalOfTheDirectorAdd.Visible = true; }
                if (A[109]) { IDManageProductCashierAdd.Visible = true; }
                if (A[110]) { IDManageProductChairmanOfTheBoardAdd.Visible = true; }
                if (A[111]) { IDManageProductFileSearchersAdd.Visible = true; }
                if (A[129]) { pnlFinancialStatistics.Visible = true; }
                if (A[130]) { IDManageProductViceBoardAdd.Visible = true; }
                IDManageProductViceBoardAdd.Visible = false;
                if (A[106] == false && A[59] == false && A[60] == false && A[61] == false && A[107] == false && A[108] == false && A[109] == false && A[110] == false && A[111] == false && A[129] == false && A[130] == false)
                {
                    pnlProductMatterOfExchange.Visible = false;
                    Response.Redirect("~/Cpanel/CHome/PageNotAccess.aspx");
                }
                //}
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
