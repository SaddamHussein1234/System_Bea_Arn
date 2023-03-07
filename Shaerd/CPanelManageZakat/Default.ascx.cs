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

public partial class Shaerd_CPanelManageZakat_Default : System.Web.UI.UserControl
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

            lblQariah.Text = " نظام  الزكاة , ";
            lblQariah.Text += Wellcome() + " / " + dtViewProfil.Rows[0]["FirstName"].ToString();

            if (IDCookie != null)
            {
                DataTable dt = new DataTable();
                dt = ClassDataAccess.GetData("SELECT Top(1) tbl_MultiQariah.IDItem,[IDAdminJoin],[IDQariah],AlQriah,[DateAddCall],[IDAdminAdd],[A1],[A2],[A3],[A4],[A5],tbl_MultiQariah.IsDelete FROM [dbo].[tbl_MultiQariah] Inner join Quaem on Quaem.IDItem = tbl_MultiQariah.IDQariah Where IDQariah = @0 And tbl_MultiQariah.IsDelete = @1", IDVillage, Convert.ToString(false));
                if (dt.Rows.Count > 0)
                    lblQariah.Text += " , " + " قرية " + dt.Rows[0]["AlQriah"].ToString();
                else
                    Response.Redirect("LogOut.aspx");
            }

            bool[] A = new bool[150];

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
            
            A[142] = Convert.ToBoolean(dtViewProfil.Rows[0]["A142"]);
            A[117] = Convert.ToBoolean(dtViewProfil.Rows[0]["A117"]);
            A[140] = Convert.ToBoolean(dtViewProfil.Rows[0]["A140"]);
            A[141] = Convert.ToBoolean(dtViewProfil.Rows[0]["A141"]);
            if (A[142] == true) { IDManageProductWarehouseStorekeeperAdd.Visible = true; }
            //if (A[117] == true) { IDManageProductWarehouseApprovalOfTheDirectorAdd.Visible = true; }
            if (A[140] == true) { IDManageProductWarehouseCashierAdd.Visible = true; }
            if (A[141] == true) { IDManageProductWarehouseChairmanOfTheBoardAdd.Visible = true; }

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