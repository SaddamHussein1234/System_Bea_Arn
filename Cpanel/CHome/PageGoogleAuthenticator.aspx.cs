using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CHome_PageGoogleAuthenticator : System.Web.UI.Page
{
    String AuthenticationCode
    {
        get
        {
            if (ViewState["AuthenticationCode"] != null)
                return ViewState["AuthenticationCode"].ToString().Trim();
            return String.Empty;
        }
        set
        {
            ViewState["AuthenticationCode"] = value.Trim();
        }
    }

    String AuthenticationTitle
    {
        get
        {
            return HFUser_Name.Value;
        }
    }

    String AuthenticationBarCodeImage
    {
        get;
        set;
    }

    String AuthenticationManualCode
    {
        get;
        set;
    }

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
            GetData();
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
                lblCheck.Text = Wellcome();
                lblCheck2.Text = lblCheck.Text;
                HFIDUniq.Value = dt.Rows[0]["IDUniqUser"].ToString();
                HFUser_Name.Value = dt.Rows[0]["FirstName"].ToString();
                lblName.Text = HFUser_Name.Value;
                lblName2.Text = HFUser_Name.Value;
                lblUser.Text = dt.Rows[0]["User_Name_"].ToString();
                lblUser2.Text = lblUser.Text;
                HFPhone.Value = dt.Rows[0]["PhoneNumber"].ToString();
                lblPhone.Text = HFPhone.Value;
                GenerateTwoFactorAuthentication();
                ImgAdmin.ImageUrl = AuthenticationBarCodeImage;
                lblManualSetupCode.Text = AuthenticationManualCode;

                if (Convert.ToBoolean(dt.Rows[0]["_Two_Factor_Enabled_"]))
                {
                    lbmsg.Text = "تم تفعيل المصادقة الثنائية , هل تريد الإلغاء (لا يُنصح بذلك)";
                    pnlActive.Visible = false;
                    pnlActived.Visible = true;
                    btnAdd.Visible = false;
                }
                else
                {
                    lbmsg.Text = "تفعيل المصادقة الثنائية";
                    pnlActive.Visible = true;
                    pnlActived.Visible = false;
                    btnAdd.Visible = true;
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("PageMyAccount.aspx");
        }
    }

    private string Wellcome()
    {
        try
        {
            DateTime time = ClassDataAccess.GetCurrentTime();
            if ((time > Convert.ToDateTime("10:00:00 AM")) && (time < Convert.ToDateTime("11:59:50 AM")))
                return "صباح الخير";
            else if ((time > Convert.ToDateTime("12:00:00 PM")) && (time < Convert.ToDateTime("5:00:00 PM")))
                return "نهارك سعيد";
            else if ((time > Convert.ToDateTime("5:01:00 PM")) && (time < Convert.ToDateTime("11:59:50 PM")))
                return "مساء الخير";
            else if ((time > Convert.ToDateTime("12:00:00 AM")) && (time < Convert.ToDateTime("9:59:50 PM")))
                return "صباح الخير";
            else
                return "مرحباً بك";
        }
        catch (Exception)
        {

        }
        return "مرحباً بك";
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;

        String pin = txt_Code.Text.Trim();
        Boolean status = ValidateTwoFactorPIN(pin);
        if (status)
        {
            FEnableAuth(true);
            lblSuccess.Text = "تم تفعيل المصادقة بنجاح , شكراً لك ... ";
        }
        else
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "رمز التحقق المدخل غير صحيح ... ";
            return;
        }
    }

    private void FEnableAuth(bool XValue)
    {
        try
        {
            GetCookie();
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            conn.Open();
            string sql = "UPDATE [dbo].[tbl_Admin] SET [_Two_Factor_Enabled_] = @Two_Factor WHERE IDUniq = @IDUniq";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IDUniq", Convert.ToString(IDUniq));
            cmd.Parameters.AddWithValue("@Two_Factor", XValue);
            cmd.ExecuteScalar();
            conn.Close();
            IDMessageSuccess.Visible = true;
            IDMessageWarning.Visible = false;
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
        GetData();
    }

    public Boolean ValidateTwoFactorPIN(String pin)
    {
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        return tfa.ValidateTwoFactorPIN(AuthenticationCode, pin);
    }

    public Boolean GenerateTwoFactorAuthentication()
    {
        txt_Code.Focus();
        AuthenticationCode = FGetSecrtKey();

        Dictionary<String, String> result = new Dictionary<String, String>();
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        var setupInfo = tfa.GenerateSetupCode("BerArn", AuthenticationTitle, AuthenticationCode, false, 3);
        if (setupInfo != null)
        {
            AuthenticationBarCodeImage = setupInfo.QrCodeSetupImageUrl;
            AuthenticationManualCode = setupInfo.ManualEntryKey;
            return true;
        }
        return false;
    }

    private string FGetSecrtKey()
    {
        Guid guid = new Guid(HFIDUniq.Value);
        String uniqueUserKey = Convert.ToString(guid).Replace("-", "").Substring(0, 10);
        return uniqueUserKey;
    }

    protected void LBUnActive_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;

        FEnableAuth(false);
        lblSuccess.Text = "تم إلغاء تفعيل المصادقة بنجاح , شكراً لك ... ";

    }

}