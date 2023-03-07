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

public partial class Cpanel_CHome_PageMyPassword : System.Web.UI.Page
{
    ClassAdmin_Arn CA = new ClassAdmin_Arn();
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
            Response.Redirect("LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetData();
        }
    }

    private void GetData()
    {
        try
        {
            GetCookie();
            CA._IDUniq = IDUniq;
            CA._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CA.BArnAdminGetByIDUniq();
            if (dt.Rows.Count > 0)
            {
                Session["YourPassword"] = dt.Rows[0]["__pass_"].ToString();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
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
            lbmsg.Text = "خطأ غير متوقع حاول لاحقاً";
            lbmsg.ForeColor = System.Drawing.Color.Red;
        }
    }

    private void CheckPassOld()
    {
        if ((txtPass.Text == string.Empty) && (txtRePass.Text == string.Empty))
        {
            lbmsg.Text = "لم تقم بتغيير كلمة المرور";
            lbmsg.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            if (txtOldPass.Text.Trim() == ClassEncryptPassword.Decrypt(Session["YourPassword"].ToString(), "www.ITFY-Edu.Net_For_Saddam"))
            {
                if (txtPass.Text == txtRePass.Text)
                {
                    Session["YourPassword"] = ClassEncryptPassword.Encrypt(txtPass.Text.Trim(), "www.ITFY-Edu.Net_For_Saddam");
                    EditMyData();
                }
                else
                {
                    lbmsg.Text = "كلمات المرور التي ادخلتها غير متطابقة !";
                    lbmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lbmsg.Text = "كلمة المرور القديمة غير صحيحة !";
                lbmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    private void EditMyData()
    {
        GetCookie();
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[tbl_Admin] SET [__pass_] = @pass_ WHERE IDUniq = @IDUniq";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@pass_", Session["YourPassword"].ToString());
        cmd.Parameters.AddWithValue("@IDUniq", Convert.ToString(IDUniq));
        cmd.ExecuteScalar();
        conn.Close();
        lbmsg.Text = "تم تغيير كلمة المرور الخاصة بك بنجاح";
        lbmsg.ForeColor = System.Drawing.Color.MediumAquamarine;
        GetData();
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Session.Remove("YourPassword");
        Response.Redirect("Default.aspx");
    }

}