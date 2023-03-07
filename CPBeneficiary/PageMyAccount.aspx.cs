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

public partial class CPBeneficiary_PageMyAccount : System.Web.UI.Page
{
    string UserERasAlEstemarah;
    private void GetCookie()
    {
        try
        {
            HttpCookie CookeCheck;  // اسم المستخدم
            CookeCheck = Request.Cookies["__User_True_User"];
            UserERasAlEstemarah = ClassSaddam.UnprotectPassword(CookeCheck["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");
        }
        catch
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        ClassMosTafeed CM = new ClassMosTafeed();
        CM._User_Name_ = UserERasAlEstemarah;
        CM._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CM.BArnRasAlEstemarahLogin();
        if (dt.Rows.Count > 0)
        {
            lblNumberMostafeed.Text = dt.Rows[0]["NumberMostafeed"].ToString();
            lblSegelAlMadany.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            Session["User_Name_Old_"] = dt.Rows[0]["User_Name_"].ToString();
            txtUserName.Text = Session["User_Name_Old_"].ToString();
            Session["Email_Name_Old_"] = dt.Rows[0]["Email_User_"].ToString();
            txtEmail.Text = Session["Email_Name_Old_"].ToString();
        }
        else
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            FCheckUserName();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FCheckUserName()
    {
        if (txtUserName.Text.Trim() != Session["User_Name_Old_"].ToString())
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1) [User_Name_] FROM [dbo].[RasAlEstemarah] With(NoLock) Where [User_Name_] = @0 And IsDelete = @1", txtUserName.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "إسم الدخول للنظام مستخدم لشخص آخر قم بتغييره";
            }
            else
            {
                Session["User_Name_Old_"] = txtUserName.Text.Trim();
                FCheckEmail();
            }
        }
        else if (txtUserName.Text.Trim() == Session["User_Name_Old_"].ToString())
        {
            FCheckEmail();
        }
    }

    private void FCheckEmail()
    {
        if (txtEmail.Text.Trim() != Session["Email_Name_Old_"].ToString())
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT [Email_User_] FROM [dbo].[RasAlEstemarah] With(NoLock) Where [Email_User_] = @0 And IsDelete = @1", txtEmail.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "البريد الالكتروني مستخدم بالفعل";
            }
            else
            {
                Session["Email_Name_Old_"] = txtEmail.Text.Trim();
                FMostafeedEdit();
            }
        }
        else if (txtEmail.Text.Trim() == Session["Email_Name_Old_"].ToString())
        {
            FMostafeedEdit();
        }  
    }

    private void FMostafeedEdit()
    {
        GetCookie();
        ClassMosTafeed CM = new ClassMosTafeed();
        CM._User_Name_ = UserERasAlEstemarah;
        CM._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CM.BArnRasAlEstemarahLogin();
        if (dt.Rows.Count > 0)
        {
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            conn.Open();
            string sql = "UPDATE [dbo].[RasAlEstemarah] SET [User_Name_] = @User_Name_,[Email_User_] = @Email_User_ WHERE [NumberMostafeed] = @NumberMostafeed";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@NumberMostafeed", Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]));
            cmd.Parameters.AddWithValue("@User_Name_", Session["User_Name_Old_"].ToString());
            cmd.Parameters.AddWithValue("@Email_User_", Session["Email_Name_Old_"].ToString());
            cmd.ExecuteScalar();
            conn.Close();
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم تعديل البيانات بنجاح";
            CheckAccountAdmin();
        }
        
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

}