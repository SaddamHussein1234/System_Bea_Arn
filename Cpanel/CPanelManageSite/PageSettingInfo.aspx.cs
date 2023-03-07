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

public partial class Cpanel_CPanelManageSite_PageSettingInfo : System.Web.UI.Page
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
            Response.Redirect("LogOut.aspx");
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
            bool A73;
            A73 = Convert.ToBoolean(dtViewProfil.Rows[0]["A73"]);
            if (A73 == false)
                Response.Redirect("LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetDetails();
        }
    }

    private void FGetDetails()
    {
        ClassSetting CS = new ClassSetting();
        CS.IDSetting = 964654;
        DataTable dt = new DataTable();
        dt = CS.BSettingGetById();
        if (dt.Rows.Count > 0)
        {
            txtFacebook.Text = dt.Rows[0]["LinkFacebook"].ToString();
            txtYouTube.Text = dt.Rows[0]["LinkYouTube"].ToString();
            txtTweter.Text = dt.Rows[0]["LinkeTwiter"].ToString();
            txtGooglePlus.Text = dt.Rows[0]["LinkeGooglePluse"].ToString();
            txtLocation.Text = dt.Rows[0]["LocationSchool"].ToString();
            txtSite.Text = dt.Rows[0]["LinkeSite"].ToString();
            txtEmail.Text = dt.Rows[0]["MailSite"].ToString();
            txtPhone.Text = dt.Rows[0]["PhoneSite"].ToString();
            txtSlide.Text = dt.Rows[0]["NumberSlide"].ToString();
            txtNews.Text = dt.Rows[0]["NumberNews"].ToString();
            txtComp.Text = dt.Rows[0]["NumberComp"].ToString();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            EditSetting();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void EditSetting()
    {
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[SettingTable] SET [LinkFacebook] = @LinkFacebook,[LinkYouTube] = @LinkYouTube,[LinkeTwiter] = @LinkeTwiter,[LinkeGooglePluse] = @LinkeGooglePluse,[LocationSchool] = @LocationSchool,[LocationSiteTr] = @LocationSiteTr ,[LocationSiteEn] = @LocationSiteEn , [LinkeSite] = @LinkeSite,[MailSite] = @MailSite,[PhoneSite] = @PhoneSite,[NumberSlide] = @NumberSlide,[NumberNews] = @NumberNews,[NumberComp] = @NumberComp WHERE IDSetting = @IDSetting";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@LinkFacebook", txtFacebook.Text.Trim());
        cmd.Parameters.AddWithValue("@LinkYouTube", txtYouTube.Text.Trim());
        cmd.Parameters.AddWithValue("@LinkeTwiter", txtTweter.Text.Trim());
        cmd.Parameters.AddWithValue("@LinkeGooglePluse", txtGooglePlus.Text.Trim());
        cmd.Parameters.AddWithValue("@LocationSchool", txtLocation.Text.Trim());
        cmd.Parameters.AddWithValue("@LocationSiteTr", string.Empty);
        cmd.Parameters.AddWithValue("@LocationSiteEn", string.Empty);
        cmd.Parameters.AddWithValue("@LinkeSite", txtSite.Text.Trim());
        cmd.Parameters.AddWithValue("@MailSite", txtEmail.Text.Trim());
        cmd.Parameters.AddWithValue("@PhoneSite", txtPhone.Text.Trim());
        cmd.Parameters.AddWithValue("@NumberSlide", Convert.ToInt32(txtSlide.Text.Trim()));
        cmd.Parameters.AddWithValue("@NumberNews", Convert.ToInt32(txtNews.Text.Trim()));
        cmd.Parameters.AddWithValue("@NumberComp", Convert.ToInt32(txtComp.Text.Trim()));
        cmd.Parameters.AddWithValue("@IDSetting", 964654);
        cmd.ExecuteScalar();
        conn.Close();
        IDMessageSuccess.Visible = true;
        IDMessageWarning.Visible = false;
        lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
        FGetDetails();
    }

}