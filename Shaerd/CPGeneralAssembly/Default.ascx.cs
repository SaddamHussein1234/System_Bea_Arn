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

public partial class Shaerd_CPGeneralAssembly_Default : System.Web.UI.UserControl
{
    string IDUser, IDUniq;
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

            CheckAccountAdmin();
        }
        catch
        {
            Response.Redirect("LogOut.aspx");
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
            //Fetch the Cookie using its Key.  
            HttpCookie IDCookie = Request.Cookies["AllowByVillage"];
            //If Cookie exists fetch its value.  
            string IDVillage = IDCookie != null ? IDCookie.Value.Split('=')[1] : "undefined";

            lblQariah.Text = "General Assembly System - نظام الجمعية العمومية , ";
            lblQariah.Text += Wellcome() + " / " + dtViewProfil.Rows[0]["FirstName"].ToString();

            if (IDCookie != null)
            {
                DataTable dt = new DataTable();
                dt = ClassDataAccess.GetData("SELECT Top(1) tbl_MultiQariah.IDItem,[IDAdminJoin],[IDQariah],AlQriah,[DateAddCall],[IDAdminAdd],[A1],[A2],[A3],[A4],[A5],tbl_MultiQariah.IsDelete FROM [dbo].[tbl_MultiQariah] With(NoLock) Inner join Quaem on Quaem.IDItem = tbl_MultiQariah.IDQariah Where IDQariah = @0 And tbl_MultiQariah.IsDelete = @1", IDVillage, Convert.ToString(false));
                if (dt.Rows.Count > 0)
                    lblQariah.Text += " , " + " قرية " + dt.Rows[0]["AlQriah"].ToString();
                else
                    Response.Redirect("LogOut.aspx");
            }

            bool[] A = new bool[150];

            A[143] = Convert.ToBoolean(dtViewProfil.Rows[0]["A143"]);
            A[144] = Convert.ToBoolean(dtViewProfil.Rows[0]["A144"]);

            if (A[143]) { IDAdmin.Visible = true; IDGeneralAssembly.Visible = true; IDGeneralAssemblyView.Visible = true; }
            if (A[144]) { IDAdminAdd.Visible = true; IDGeneralAssemblyAdd.Visible = true; }

            A[145] = Convert.ToBoolean(dtViewProfil.Rows[0]["A145"]);
            if (A[145]) IDGeneralAssemblyAllow.Visible = true;

            A[146] = Convert.ToBoolean(dtViewProfil.Rows[0]["A146"]);
            A[147] = Convert.ToBoolean(dtViewProfil.Rows[0]["A147"]);
            if (A[146]) { IDGeneralAssemblyBill.Visible = true; IDGeneralAssemblyBillView.Visible = true; }
            if (A[147]) IDGeneralAssemblyBillAdd.Visible = false;

            A[148] = Convert.ToBoolean(dtViewProfil.Rows[0]["A148"]);
            A[149] = Convert.ToBoolean(dtViewProfil.Rows[0]["A149"]);
            if (A[148]) IDGeneralAssemblyBillAmeen.Visible = true;
            if (A[149]) IDGeneralAssemblyBillRaees.Visible = true;

            if (Convert.ToBoolean(dtViewProfil.Rows[0]["_Two_Factor_Enabled_"]) || Convert.ToBoolean(dtViewProfil.Rows[0]["_SMS_Enabled_"]))
                IDMessageWarning.Visible = false;
            else
                IDMessageWarning.Visible = true;
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