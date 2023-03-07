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

public partial class Cpanel_CPanelManageWarehouse_MPCPanel : System.Web.UI.MasterPage
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
                    //if (Convert.ToBoolean(dtViewProfil.Rows[0]["IsBaheth"]) && Convert.ToBoolean(dtViewProfil.Rows[0]["IsModer"]) == false)
                    //    Response.Redirect("LogOut.aspx");
                    //else
                    //{
                    lblFirstName.Text = dtViewProfil.Rows[0]["FirstName"].ToString();

                    bool[] A = new bool[250];

                    pnlManageProductWarehouse.Visible = true;
                    // الإنتماء
                    A[62] = Convert.ToBoolean(dtViewProfil.Rows[0]["A62"]);
                    A[112] = Convert.ToBoolean(dtViewProfil.Rows[0]["A112"]);
                    if (A[62] == false && A[112] == false)
                        IDManageAffiliation.Visible = false;
                    else if (A[62] == true || A[112] == true)
                        IDManageAffiliation.Visible = true;

                    // الاصناف
                    A[63] = Convert.ToBoolean(dtViewProfil.Rows[0]["A63"]);
                    A[113] = Convert.ToBoolean(dtViewProfil.Rows[0]["A113"]);
                    if (A[63] == false && A[113] == false)
                        IDManageCategory.Visible = false;
                    else if (A[63] == true || A[113] == true)
                        IDManageCategory.Visible = true;

                    // المنتجات
                    A[64] = Convert.ToBoolean(dtViewProfil.Rows[0]["A64"]);
                    A[114] = Convert.ToBoolean(dtViewProfil.Rows[0]["A114"]);
                    if (A[64] == false && A[114] == false)
                        IDManageProduct.Visible = false;
                    else if (A[64] == true || A[114] == true)
                        IDManageProduct.Visible = true;

                    A[65] = Convert.ToBoolean(dtViewProfil.Rows[0]["A65"]);
                    if (A[65] == true) { IDManageProductByAffiliationView.Visible = true; }
                    pnlAffiliation.Visible = true;
                    if (A[62] == false && A[65] == false && A[112] == false)
                        pnlAffiliation.Visible = false;

                    A[66] = Convert.ToBoolean(dtViewProfil.Rows[0]["A66"]);
                    if (A[66] == true) { IDManageProductByCategoryView.Visible = true; }
                    pnlCategory.Visible = true;
                    if (A[63] == false && A[66] == false && A[113] == false)
                        pnlCategory.Visible = false;

                    // أماكن التخزين
                    A[67] = Convert.ToBoolean(dtViewProfil.Rows[0]["A67"]);
                    A[115] = Convert.ToBoolean(dtViewProfil.Rows[0]["A115"]);
                    if (A[67] == false && A[115] == false)
                        IDManageStoragePlaces.Visible = false;
                    else if (A[67] == true || A[115] == true)
                        IDManageStoragePlaces.Visible = true;


                    A[68] = Convert.ToBoolean(dtViewProfil.Rows[0]["A68"]);
                    A[69] = Convert.ToBoolean(dtViewProfil.Rows[0]["A69"]);
                    A[70] = Convert.ToBoolean(dtViewProfil.Rows[0]["A70"]);
                    if (A[68] == true) { IDManageProductWarehousebyContainedView.Visible = true; }
                    if (A[69] == true) { IDManageProductWarehousebyIssuedView.Visible = true; }
                    if (A[70] == true) { IDManageProductWarehousebyContainedAndIssuedView.Visible = true; }

                    A[116] = Convert.ToBoolean(dtViewProfil.Rows[0]["A116"]);
                    A[117] = Convert.ToBoolean(dtViewProfil.Rows[0]["A117"]);
                    A[118] = Convert.ToBoolean(dtViewProfil.Rows[0]["A118"]);
                    A[119] = Convert.ToBoolean(dtViewProfil.Rows[0]["A119"]);
                    if (A[116] == true) { IDManageProductWarehouseStorekeeperAdd.Visible = true; }
                    if (A[117] == true) { IDManageProductWarehouseApprovalOfTheDirectorAdd.Visible = true; }
                    if (A[118] == true) { IDManageProductWarehouseCashierAdd.Visible = true; }
                    if (A[119] == true) { IDManageProductWarehouseChairmanOfTheBoardAdd.Visible = true; }

                    A[120] = Convert.ToBoolean(dtViewProfil.Rows[0]["A120"]);
                    if (A[120] == true) { IDManageProductShippingWarehouseAdd.Visible = true; }

                    if (A[113] == false && A[117] == false && A[118] == false && A[119] == false)
                        pnlManageProductWarehouse.Visible = false;

                    A[131] = Convert.ToBoolean(dtViewProfil.Rows[0]["A131"]);
                    if (A[131] == true) { IDBill.Visible = true; }
                    //}

                    if (A[62] == false && A[63] == false && A[64] == false && A[65] == false && A[66] == false && A[67] == false && A[68] == false &&
                        A[69] == false && A[70] == false && A[112] == false && A[113] == false && A[114] == false && A[115] == false && A[116] == false &&
                        A[117] == false && A[118] == false && A[119] == false && A[120] == false)
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
