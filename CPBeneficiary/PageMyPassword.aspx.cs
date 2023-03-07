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

public partial class CPBeneficiary_PageMyPassword : System.Web.UI.Page
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
            txtOldPass.Focus();
            Session["User_Old_Pass_"] = dt.Rows[0]["Password_User_"].ToString();
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
            CheckPassOld();
        }
        catch
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع حاول لاحقاً";
            return;
        }
    }

    private void CheckPassOld()
    {
        if ((txtPass.Text == string.Empty) && (txtRePass.Text == string.Empty))
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "لم تقم بتغيير كلمة المرور";
        }
        else
        {
            if (txtOldPass.Text.Trim() == ClassEncryptPassword.Decrypt(Session["User_Old_Pass_"].ToString(), "23ABC6587685DE4654A325BC456"))
            {
                if (txtPass.Text == txtRePass.Text)
                {
                    Session["User_Old_Pass_"] = ClassEncryptPassword.Encrypt(txtPass.Text.Trim(), "23ABC6587685DE4654A325BC456");
                    FMostafeedEdit();
                }
                else
                {
                    IDMessageWarning.Visible = true;
                    IDMessageSuccess.Visible = false;
                    lblWarning.Text = "كلمات المرور التي ادخلتها غير متطابقة !";
                }
            }
            else
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "كلمة المرور القديمة غير صحيحة !";
            }
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
            string sql = "UPDATE [dbo].[RasAlEstemarah] SET [Password_User_] = @Password_User WHERE [NumberMostafeed] = @NumberMostafeed";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@NumberMostafeed", Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]));
            cmd.Parameters.AddWithValue("@Password_User", Session["User_Old_Pass_"].ToString());
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
        Session.Remove("User_Old_Pass_");
        Response.Redirect("Default.aspx");
    }

}