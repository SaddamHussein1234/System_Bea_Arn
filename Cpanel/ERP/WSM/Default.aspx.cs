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

public partial class Cpanel_ERP_WSM_Default : System.Web.UI.Page
{
    string IDUniq;
    private void GetCookie()
    {
        try
        {
            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

            CheckAccountAdmin();
        }
        catch
        {
            Response.Redirect("~/Cpanel/LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        try
        {
            ClassAdmin_Arn CA = new ClassAdmin_Arn();
            CA._IDUniq = IDUniq;
            CA._IsDelete = false;
            DataTable dtViewProfil = new DataTable();
            dtViewProfil = CA.BArnAdminGetByIDUniq();
            if (dtViewProfil.Rows.Count > 0)
            {
                //Fetch the Cookie using its Key.  
                HttpCookie IDCookie = Request.Cookies["AllowByVillage"];
                //If Cookie exists fetch its value.  
                string IDVillage = IDCookie != null ? IDCookie.Value.Split('=')[1] : "undefined";

                lblQariah.Text = "Warehouse System - نظام إدارة المستودع , ";
                lblQariah.Text += Wellcome() + " / " + dtViewProfil.Rows[0]["FirstName"].ToString();

                if (IDCookie != null)
                {
                    DataTable dt = new DataTable();
                    dt = ClassDataAccess.GetData("SELECT Top(1) tbl_MultiQariah.IDItem,[IDAdminJoin],[IDQariah],AlQriah,[DateAddCall],[IDAdminAdd],[A1],[A2],[A3],[A4],[A5],tbl_MultiQariah.IsDelete FROM [dbo].[tbl_MultiQariah] Inner join Quaem on Quaem.IDItem = tbl_MultiQariah.IDQariah Where IDQariah = @0 And tbl_MultiQariah.IsDelete = @1", IDVillage, Convert.ToString(false));
                    if (dt.Rows.Count > 0)
                        lblQariah.Text += " , " + " قرية " + dt.Rows[0]["AlQriah"].ToString();
                    else
                        Response.Redirect("~/Cpanel/LogOut.aspx");
                }

                bool[] A = new bool[150];

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

                A[66] = Convert.ToBoolean(dtViewProfil.Rows[0]["A66"]);
                if (A[66] == true) { IDManageProductByCategoryView.Visible = true; }

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
                if (A[68] == true) { IDManageProductWarehousebyContainedView.Visible = false; }
                if (A[69] == true) { IDManageProductWarehousebyIssuedView.Visible = false; }
                if (A[70] == true) { IDManageProductWarehousebyContainedAndIssuedView.Visible = false; }

                A[116] = Convert.ToBoolean(dtViewProfil.Rows[0]["A116"]);
                A[117] = Convert.ToBoolean(dtViewProfil.Rows[0]["A117"]);
                A[118] = Convert.ToBoolean(dtViewProfil.Rows[0]["A118"]);
                A[119] = Convert.ToBoolean(dtViewProfil.Rows[0]["A119"]);
                if (A[116] == true) { IDManageProductWarehouseStorekeeperAdd.Visible = true; }
                if (A[117] == true) { IDManageProductWarehouseApprovalOfTheDirectorAdd.Visible = true; }
                if (A[118] == true) { IDManageProductWarehouseCashierAdd.Visible = false; }
                if (A[119] == true) { IDManageProductWarehouseChairmanOfTheBoardAdd.Visible = false; }

                A[120] = Convert.ToBoolean(dtViewProfil.Rows[0]["A120"]);
                if (A[120] == true) { IDManageProductShippingWarehouseAdd.Visible = true; }

                A[131] = Convert.ToBoolean(dtViewProfil.Rows[0]["A131"]);
                if (A[131] == true) { IDBill.Visible = true; }

                if (Convert.ToBoolean(dtViewProfil.Rows[0]["_Two_Factor_Enabled_"]) || Convert.ToBoolean(dtViewProfil.Rows[0]["_SMS_Enabled_"]))
                    IDMessageWarning.Visible = false;
                else
                    IDMessageWarning.Visible = true;
            }
        }
        catch (Exception)
        {
            Response.Redirect("~/Cpanel/LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            GetCookie();
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