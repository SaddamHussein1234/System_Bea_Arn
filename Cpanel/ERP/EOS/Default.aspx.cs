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

public partial class Cpanel_ERP_EOS_Default : System.Web.UI.Page
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

                lblQariah.Text = "Exchange Order System - نظام أوامر الصرف , ";
                lblQariah.Text += Wellcome() + " / " + dtViewProfil.Rows[0]["FirstName"].ToString();

                if (IDCookie != null)
                {
                    DataTable dt = new DataTable();
                    dt = ClassDataAccess.GetData("SELECT Top(1) tbl_MultiQariah.IDItem,[IDAdminJoin],[IDQariah],AlQriah,[DateAddCall],[IDAdminAdd],[A1],[A2],[A3],[A4],[A5],tbl_MultiQariah.IsDelete FROM [dbo].[tbl_MultiQariah] Inner join Quaem on Quaem.IDItem = tbl_MultiQariah.IDQariah Where IDQariah = @0 And tbl_MultiQariah.IsDelete = @1", Convert.ToInt32(IDVillage), false);
                    if (dt.Rows.Count > 0)
                        lblQariah.Text += " , " + " قرية " + dt.Rows[0]["AlQriah"].ToString();
                    else
                        Response.Redirect("~/Cpanel/LogOut.aspx");
                }

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

                if (A[106]) { IDManageProductMatterOfExchangeAdd.Visible = true; IDManageProductMatterOfExchangeForDamaged.Visible = false; IDManageProductRestorationAndConstructionAdd.Visible = true; IDSupportForPrismsAdd.Visible = true; }
                if (A[59]) { IDManageProductExchangeOrdersView.Visible = true; }
                if (A[60]) { IDManageProductSupportByBeneficiaryView.Visible = true; IDManageProductSupportByBeneficiaryViewMulti.Visible = true; }
                if (A[107]) { IDManageProductStorekeeperAdd.Visible = true; }
                if (A[108]) { IDManageProductApprovalOfTheDirectorAdd.Visible = true; }
                if (A[109]) { IDManageProductCashierAdd.Visible = true; }
                if (A[110]) { IDManageProductChairmanOfTheBoardAdd.Visible = true; }
                if (A[111]) { IDManageProductFileSearchersAdd.Visible = true; }
                if (A[61]) { IDManageProductAddThePriceToOrder.Visible = true; }
                if (A[129]) { pnlFinancialStatistics.Visible = true; }
                if (A[130]) { IDManageProductViceBoardAdd.Visible = false; }

                if (Convert.ToBoolean(dtViewProfil.Rows[0]["_Two_Factor_Enabled_"]) || Convert.ToBoolean(dtViewProfil.Rows[0]["_SMS_Enabled_"]))
                    IDMessageWarning.Visible = false;
                else
                    IDMessageWarning.Visible = true;
            }
            else
                Response.Redirect("~/Cpanel/LogOut.aspx");
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
            DateTime time = ClassSaddam.GetCurrentTime();
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