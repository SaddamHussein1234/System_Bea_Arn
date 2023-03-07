using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.OM.Repostry;

public partial class Cpanel_Log : System.Web.UI.Page
{
    string PassU;
    bool IsBaheth, IsBlockAdmin, IsDeleteAdmin, IsBlockGroup, IsDeleteGroup, Two_Factor, Two_Factor_SMS;
    ClassAdmin_Arn CAA = new ClassAdmin_Arn();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtUser.Focus();
            HttpCookie cookie = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
            if (cookie != null)
                Response.Redirect("CHome");
        }
        if (Request.Cookies["CheckBrowser"] == null)
        {
            if (string.IsNullOrWhiteSpace(Request.QueryString["cookie"]))
            {
                Response.Cookies["CheckBrowser"].Value = "Yes";
                Response.Redirect(Request.Url.ToString() + "?cookie=created", true);
            }
            else if (Request.QueryString["cookie"].Equals("created"))
            {
                uxdivErrorMessage.Visible = true;
                lbmsg.Text = "<p class='btn btn-primary' data-toggle='modal' data-target='#BYI' style='Width:100%; height:30px; font-size:12px'> إن الكوكيز موقفه في جهازك , قم بتشغيلها " + "</p>";
            }
        }
        else
        {
            //lblResult.Text = "Cookies enabled?: Yes";
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            uxdivErrorMessage.Visible = false;
            System.Threading.Thread.Sleep(100);
            FLoginUser();
        }
        catch
        {
            uxdivErrorMessage.Visible = true;
            lbmsg.Text = "خطأ غير متوقع حاول لاحقاً";
        }
    }

    // تسجيل دخول
    private void FLoginUser()
    {
        if (txtUser.Text != string.Empty)
        {
            uxdivErrorMessage.Visible = false;
            if (txtPass.Text != string.Empty)
            {
                uxdivErrorMessage.Visible = false;
                CAA._User_Name_ = txtUser.Text.Trim();
                DataTable dt = new DataTable();
                dt = CAA.BAdminLogin();
                if (dt.Rows.Count > 0)
                {
                    HFID.Value = dt.Rows[0]["ID_Item"].ToString();
                    HFUniq.Value = dt.Rows[0]["IDUniq"].ToString();
                    HFName.Value = dt.Rows[0]["FirstName"].ToString();
                    HFUser.Value = dt.Rows[0]["User_Name_"].ToString();
                    PassU = dt.Rows[0]["__pass_"].ToString();
                    HFPhone.Value = dt.Rows[0]["PhoneNumber"].ToString();
                    IsBaheth = Convert.ToBoolean(dt.Rows[0]["IsBaheth"]);
                    IsBlockAdmin = Convert.ToBoolean(dt.Rows[0]["IsBlock"]);
                    IsDeleteAdmin = Convert.ToBoolean(dt.Rows[0]["IsDelete"]);
                    IsBlockGroup = Convert.ToBoolean(dt.Rows[0]["IsActiveGroup"]);
                    IsDeleteGroup = Convert.ToBoolean(dt.Rows[0]["IsDeleteGroup"]);
                    Two_Factor = Convert.ToBoolean(dt.Rows[0]["_Two_Factor_Enabled_"]);
                    Two_Factor_SMS = Convert.ToBoolean(dt.Rows[0]["_SMS_Enabled_"]);
                    HFIDCookie.Value = dt.Rows[0]["_ID_Cookie_"].ToString();
                    if (txtPass.Text.Trim() == ClassEncryptPassword.Decrypt(PassU, CLS_Key.FGetKeyAdmin()))
                        CheckIsBaheth();
                    else
                    {
                        uxdivErrorMessage.Visible = true;
                        lbmsg.Text = "البيانات المرسلة غير صحيحة";
                    }
                }
                else
                {
                    uxdivErrorMessage.Visible = true;
                    lbmsg.Text = "لا توجد بيانات بالطلب المرسل";
                }
            }
            else if (txtUser.Text == string.Empty)
            {
                uxdivErrorMessage.Visible = true;
                lbmsg.Text = "يرجى إدخال كلمة المرور";
            }
        }
        else if (txtUser.Text == string.Empty)
        {
            System.Threading.Thread.Sleep(50);
            uxdivErrorMessage.Visible = true;
            lbmsg.Text = "يرجى إدخال إسم المستخدم";
        }
    }

    private void CheckIsBaheth()
    {
        //if (IsBaheth)
        //{
        //    uxdivErrorMessage.Visible = true;
        //    lbmsg.Text = "أنت باحث , لا يمكنك دخل هذه الإدارة";
        //}
        //else
        //{
        //    uxdivErrorMessage.Visible = false;
            CheckBlockAdmin();
        //}
    }

    private void CheckBlockAdmin()
    {
        if (IsBlockAdmin == false)
        {
            uxdivErrorMessage.Visible = true;
            lbmsg.Text = "حسابك غير مفعل";
        }
        else
        {
            uxdivErrorMessage.Visible = false;
            CheckDeleteAdmin();
        }
    }

    private void CheckDeleteAdmin()
    {
        if (IsDeleteAdmin)
        {
            uxdivErrorMessage.Visible = true;
            lbmsg.Text = "تم حذف حسابك مسبقاً";
        }
        else
        {
            uxdivErrorMessage.Visible = false;
            CheckBlockGroup();
        }
    }

    private void CheckBlockGroup()
    {
        if (IsBlockGroup == false)
        {
            uxdivErrorMessage.Visible = true;
            lbmsg.Text = "المجموعة التي تنتمي اليها مغلقه";
        }
        else
        {
            uxdivErrorMessage.Visible = false;
            CheckDeleteGroup();
        }
    }

    private void CheckDeleteGroup()
    {
        uxdivErrorMessage.Visible = false;
        if (IsDeleteGroup)
        {
            uxdivErrorMessage.Visible = true;
            lbmsg.Text = "تم حذف المجموعة التي تنتمي اليها";
        }
        else
        {
            uxdivErrorMessage.Visible = false;
            if (this.Page.IsValid && txtCapatsha.Text.ToString() == Session["randomNumber"].ToString())
            {
                if (Two_Factor_SMS)
                {
                    Session["_OTP_Code_"] = ClassSaddam.Generate_OTP_();
                    string XMessage = " كود تحقق الحساب هو : " + Session["_OTP_Code_"].ToString();
                    string XR = Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, XMessage, "BerArn", "Code_SMS", 101);
                    if (XR == "IsSuccessAdd")
                    {
                        HFCheck.Value = "Log_SMS";
                        uxdivErrorMessage.Visible = true;
                        txt_Code.Focus();
                        uxSystemLoginForm.Visible = false;
                        uxSystemCode.Visible = true;
                    }
                }
                else if (Two_Factor)
                {
                    HFCheck.Value = "Log_App";
                    uxdivErrorMessage.Visible = true;
                    txt_Code.Focus();
                    uxSystemLoginForm.Visible = false;
                    uxSystemCode.Visible = true;
                    GenerateTwoFactorAuthentication();
                }
                else
                {
                    if (CBRememberMe.Checked == true)
                        FSetCookieWithRemembermy(ClassSetting.FGetCount_Allow_Day_Cookies());
                    else if (CBRememberMe.Checked == false)
                        FSetCookieWithRemembermy(0);
                }
            }
            else
            {
                uxdivErrorMessage.Visible = true;
                lbmsg.Text = "رمز التحقق خطأ";
                txtCapatsha.Text = string.Empty;
            }
        }
    }

    private void FSetCookieWithRemembermy(int XCountDay)
    {
        HttpCookie UrlID = new HttpCookie(DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_"));
        UrlID["Url"] = ClassSaddam.ProtectPassword(HFID.Value, "www.ITFY-Edu.Net_ProtectBySADDAM");
        HttpContext.Current.Response.Cookies.Add(UrlID);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlIDUniq = new HttpCookie("__CheckedAdmin_True_");
        UrlIDUniq["Url"] = ClassSaddam.ProtectPassword(HFUniq.Value, "www.ITFY-Edu.Net_ProtectBySADDAM");
        HttpContext.Current.Response.Cookies.Add(UrlIDUniq);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlSite = new HttpCookie("__User_True_");
        UrlSite["Url"] = ClassSaddam.ProtectPassword(HFUser.Value, "www.ITFY-Edu.Net_ProtectBySADDAM");
        HttpContext.Current.Response.Cookies.Add(UrlSite);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlUser = new HttpCookie("__UserUniqAdmin_True_");
        UrlUser["Url"] = ClassSaddam.ProtectPassword(HFID.Value, "www.ITFY-Edu.Net_ProtectBySADDAM");
        HttpContext.Current.Response.Cookies.Add(UrlUser);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlIDCookie = new HttpCookie("__User_Screen_");
        UrlIDCookie["Url"] = ClassSaddam.ProtectPassword(HFIDCookie.Value, "www.ITFY-Edu.Net_ProtectByITFY");
        HttpContext.Current.Response.Cookies.Add(UrlIDCookie);
        System.Threading.Thread.Sleep(100);
        if(XCountDay > 0)
        {
            UrlID.Expires = DateTime.Now.AddDays(XCountDay);
            UrlIDUniq.Expires = DateTime.Now.AddDays(XCountDay);
            UrlSite.Expires = DateTime.Now.AddDays(XCountDay);
            UrlUser.Expires = DateTime.Now.AddDays(XCountDay);
            UrlIDCookie.Expires = DateTime.Now.AddDays(XCountDay);
        }

        Repostry_Tricker_.FAPP_Add("AddLogin", Guid.NewGuid(), Convert.ToInt32(HFID.Value), "SSP", "الإعدادات والصلاحيات", "تسجيل دخول", "دخول آمن", ClassSaddam.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss"));
        
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
        System.Threading.Thread.Sleep(50);

        HttpContext.Current.Response.Redirect("CHome");
    }

    protected void btn_Code_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_Code.Text != string.Empty)
            {
                if (HFCheck.Value == "Log_SMS")
                {
                    if (txt_Code.Text.Trim() == Session["_OTP_Code_"].ToString())
                    {
                        if (CBRememberMe.Checked == true)
                            FSetCookieWithRemembermy(ClassSetting.FGetCount_Allow_Day_Cookies());
                        else if (CBRememberMe.Checked == false)
                            FSetCookieWithRemembermy(0);
                    }
                    else
                    {
                        uxdivErrorMessage.Visible = true;
                        lbmsg.Text = "الكود المدخل غير صحيح ... ";
                    }
                }
                else if (HFCheck.Value == "Log_App")
                {
                    String pin = txt_Code.Text.Trim();
                    Boolean status = ValidateTwoFactorPIN(pin);
                    if (status)
                    {
                        if (CBRememberMe.Checked == true)
                            FSetCookieWithRemembermy(ClassSetting.FGetCount_Allow_Day_Cookies());
                        else if (CBRememberMe.Checked == false)
                            FSetCookieWithRemembermy(0);
                    }
                    else
                    {
                        uxdivErrorMessage.Visible = true;
                        lbmsg.Text = "الكود المدخل غير صحيح ... ";
                    }
                }
            }
            else
            {
                uxdivErrorMessage.Visible = true;
                lbmsg.Text = "يرجى إدخال رمز التحقق بخطوتين";
            }
        }
        catch (Exception)
        {
            uxdivErrorMessage.Visible = true;
            lbmsg.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

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
            return HFName.Value;
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

    public Boolean ValidateTwoFactorPIN(String pin)
    {
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        return tfa.ValidateTwoFactorPIN(AuthenticationCode, pin);
    }

    public Boolean GenerateTwoFactorAuthentication()
    {
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
        Guid guid = new Guid(HFUniq.Value);
        String uniqueUserKey = Convert.ToString(guid).Replace("-", "").Substring(0, 10);
        return uniqueUserKey;
    }

}