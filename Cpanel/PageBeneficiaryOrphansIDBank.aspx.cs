using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageBeneficiaryOrphansIDBank : System.Web.UI.Page
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
        }
        catch
        {
            Response.Redirect("PageNotAccess.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        ClassAdmin_Arn CAA = new ClassAdmin_Arn();
        CAA._IDUniq = IDUniq;
        CAA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CAA.BArnAdminGetByIDUniq();
        if (dtViewProfil.Rows.Count > 0)
        {
            bool A76;
            A76 = Convert.ToBoolean(dtViewProfil.Rows[0]["A76"]);
            if (A76 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetDataMostafed();
            txtIDBank.Focus();
        }
    }

    private void FGetDataMostafed()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0 And IsDelete = @1", Request.QueryString["ID"], Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["NameMostafeed"].ToString();
                lblTypeMostafeed.Text = dt.Rows[0]["TypeMostafeed"].ToString();
                lblAlQariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
                lblGender.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
                lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
                lblNumberSigal.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
                FGetSun();
            }
            else
            {
                Response.Redirect("PageBeneficiaryOrphans.aspx");
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetSun()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[TarafEstemarah] Where IDUniq = @0 And IsDelete = @1", Request.QueryString["XID"], Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNameSun.Text = dt.Rows[0]["Name"].ToString();
            txtIDBank.Text = dt.Rows[0]["A4"].ToString();
        }
        else
        {
            Response.Redirect("PageBeneficiaryOrphans.aspx");
        }
    }
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnAdd.Text == "حفظ البيانات")
            {
                FUpdateIDBank();
            }
        }
        catch (Exception)
        {
            IDMessage.Visible = false;
            return;
        }
    }

    private void FUpdateIDBank()
    {
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[TarafEstemarah] SET [A4] = @A4 WHERE IDUniq = @IDUniq";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@IDUniq", Convert.ToString(Request.QueryString["XID"]));
        cmd.Parameters.AddWithValue("@A4", txtIDBank.Text.Trim());
        cmd.ExecuteScalar();
        conn.Close();
        IDMessage.Visible = true;
        FGetDataMostafed();
    }
    
    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryOrphans.aspx");
    }

}