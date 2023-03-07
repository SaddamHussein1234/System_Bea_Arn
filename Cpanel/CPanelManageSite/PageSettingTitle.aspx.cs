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

public partial class Cpanel_CPanelManageSite_PageSettingTitle : System.Web.UI.Page
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
            bool A72;
            A72 = Convert.ToBoolean(dtViewProfil.Rows[0]["A72"]);
            if (A72 == false)
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
            //txtNameSiteEN.Text = dt.Rows[0]["NameSiteEN"].ToString();
            //txtDescriptoinSiteEn.Text = dt.Rows[0]["DescriptoinSiteEn"].ToString();
            //txtKeyWordEN.Text = dt.Rows[0]["KeyWordEN"].ToString();
            txtNameSiteAR.Text = dt.Rows[0]["NameSiteAR"].ToString();
            txtDescriptoinSiteAR.Text = dt.Rows[0]["DescriptoinSiteAR"].ToString();
            txtKeyWordAR.Text = dt.Rows[0]["KeyWordAR"].ToString();
            //txtNameSiteTR.Text = dt.Rows[0]["NameSiteTR"].ToString();
            //txtDescriptoinSiteTR.Text = dt.Rows[0]["DescriptoinSiteTR"].ToString();
            //txtKeyWordTR.Text = dt.Rows[0]["KeyWordTR"].ToString();
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
        string sql = "UPDATE [dbo].[SettingTable] SET [NameSiteEN] = @NameSiteEN,[DescriptoinSiteEn] = @DescriptoinSiteEn,[KeyWordEN] = @KeyWordEN,[NameSiteAR] = @NameSiteAR,[DescriptoinSiteAR] = @DescriptoinSiteAR,[KeyWordAR] = @KeyWordAR,[NameSiteTR] = @NameSiteTR,[DescriptoinSiteTR] = @DescriptoinSiteTR,[KeyWordTR] = @KeyWordTR WHERE IDSetting = @IDSetting";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@NameSiteEN", string.Empty);
        cmd.Parameters.AddWithValue("@DescriptoinSiteEn", string.Empty);
        cmd.Parameters.AddWithValue("@KeyWordEN", string.Empty);

        cmd.Parameters.AddWithValue("@NameSiteAR", txtNameSiteAR.Text.Trim());
        cmd.Parameters.AddWithValue("@DescriptoinSiteAR", txtDescriptoinSiteAR.Text.Trim());
        cmd.Parameters.AddWithValue("@KeyWordAR", txtKeyWordAR.Text.Trim());

        cmd.Parameters.AddWithValue("@NameSiteTR", string.Empty);
        cmd.Parameters.AddWithValue("@DescriptoinSiteTR", string.Empty);
        cmd.Parameters.AddWithValue("@KeyWordTR", string.Empty);

        cmd.Parameters.AddWithValue("@IDSetting", 964654);
        cmd.ExecuteScalar();
        conn.Close();
        IDMessageSuccess.Visible = true;
        IDMessageWarning.Visible = false;
        lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
        FGetDetails();
    }

}