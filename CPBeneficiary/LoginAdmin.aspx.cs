using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;

public partial class CPBeneficiary_LoginAdmin : System.Web.UI.Page
{
    string IDUserRasAlEstemarah, IDUniqRasAlEstemarah, UserERasAlEstemarah, PassURasAlEstemarah;
    bool IsActiveAdminRasAlEstemarah, IsDeleteAdminRasAlEstemarah;
    ClassMosTafeed CM = new ClassMosTafeed();

    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(100);

        if (!IsPostBack)
        {
            txtUserAdmin.Focus();
            HttpCookie cookie = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_335_")];
            if (cookie != null)
            {
                Response.Redirect("../CPBeneficiary");
            }
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
                //lbmsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void btnLoginAdmin_Click(object sender, EventArgs e)
    {
        try
        {
            System.Threading.Thread.Sleep(500);
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
        if (txtUserAdmin.Text != string.Empty)
        {
            uxdivErrorMessage.Visible = false;
            if (txtPassAdmin.Text != string.Empty)
            {
                uxdivErrorMessage.Visible = false;
                CM.IDCheck = "Web";
                CM._User_Name_ = txtUserAdmin.Text.Trim();
                DataTable dt = new DataTable();
                dt = CM.BArnRasAlEstemarahLogin();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["TypeMostafeed"].ToString() == "دائم")
                    {
                        IDUserRasAlEstemarah = dt.Rows[0]["IDItem"].ToString();
                        IDUniqRasAlEstemarah = dt.Rows[0]["IDUniq"].ToString();
                        UserERasAlEstemarah = dt.Rows[0]["User_Name_"].ToString();
                        PassURasAlEstemarah = dt.Rows[0]["Password_User_"].ToString();
                        IsActiveAdminRasAlEstemarah = Convert.ToBoolean(dt.Rows[0]["IsActive"]);
                        IsDeleteAdminRasAlEstemarah = Convert.ToBoolean(dt.Rows[0]["IsDelete"]);
                        if (txtPassAdmin.Text.Trim() == ClassEncryptPassword.Decrypt(PassURasAlEstemarah, "23ABC6587685DE4654A325BC456"))
                        {
                            //ClassTrickerAdmin.TrickerAdd(Convert.ToInt32(IDUser), "إضافة", " دخول آمن لحساب " + dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString(), ClassKhwarism.GetCurrentTime().ToString("yyyy/MM/dd hh:mm:ss"));
                            if (Convert.ToBoolean(dt.Rows[0]["Is_Active_Group"]) == false)
                            {
                                uxdivErrorMessage.Visible = true;
                                lbmsg.Text = "تم إيقاف المجموعة التي تنتمي إليها";
                            }
                            else
                            {
                                if (Convert.ToBoolean(dt.Rows[0]["Is_Delete_Group"]))
                                {
                                    uxdivErrorMessage.Visible = true;
                                    lbmsg.Text = "تم حذف المجموعة التي تنتمي إليها";
                                }
                                else
                                    CheckBlockAdmin();
                            }

                            
                        }
                        else
                        {
                            //ClassTrickerAdmin.TrickerAdd(Convert.ToInt32(IDUser), "إضافة", " محاولة تسجيل دخول الى حساب " + dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString(), ClassKhwarism.GetCurrentTime().ToString("yyyy/MM/dd hh:mm:ss"));
                            uxdivErrorMessage.Visible = true;
                            lbmsg.Text = "البيانات المرسلة غير صحيحة";
                        }
                    }
                    else
                    {
                        uxdivErrorMessage.Visible = true;
                        lbmsg.Text = "لقد تم إستبعاد حسابك , يرجى مراجعة الجمعية";
                    }
                }
                else
                {
                    uxdivErrorMessage.Visible = true;
                    lbmsg.Text = "لا توجد بيانات بالطلب المرسل";
                }
            }
            else if (txtUserAdmin.Text == string.Empty)
            {
                uxdivErrorMessage.Visible = true;
                lbmsg.Text = "يرجى إدخال كلمة المرور";
            }
        }
        else if (txtUserAdmin.Text == string.Empty)
        {
            uxdivErrorMessage.Visible = true;
            lbmsg.Text = "يرجى إدخال إسم المستخدم";
        }
    }

    private void CheckBlockAdmin()
    {
        if (IsActiveAdminRasAlEstemarah == false)
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
        if (IsDeleteAdminRasAlEstemarah)
        {
            uxdivErrorMessage.Visible = true;
            lbmsg.Text = "تم حذف حسابك مسبقاً";
        }
        else
        {
            uxdivErrorMessage.Visible = false;
            if (this.Page.IsValid && txtCapatshaAdmin.Text.ToString() == Session["randomNumber"].ToString())
            {
                if (CBRememberMe.Checked == true)
                {
                    FSetCookieWithRemembermyAdmin();
                }
                else if (CBRememberMe.Checked == false)
                {
                    FSetCookieNotRemembermyAdmin();
                }
            }
            else
            {
                uxdivErrorMessage.Visible = true;
                lbmsg.Text = "رمز التحقق خطأ";
                txtCapatshaAdmin.Text = string.Empty;
            }
        }
    }

    private void FSetCookieWithRemembermyAdmin()
    {
        HttpCookie UrlID = new HttpCookie(DateTime.Now.ToString("_545_yyyyMMyyyyMM_335_"));
        UrlID["Url"] = ClassSaddam.ProtectPassword(IDUserRasAlEstemarah, "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");
        UrlID.Expires = DateTime.Now.AddDays(30);
        HttpContext.Current.Response.Cookies.Add(UrlID);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlIDUniq = new HttpCookie("__CheckedAdmin_True_User");
        UrlIDUniq["Url"] = ClassSaddam.ProtectPassword(IDUniqRasAlEstemarah, "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");
        UrlIDUniq.Expires = DateTime.Now.AddDays(30);
        HttpContext.Current.Response.Cookies.Add(UrlIDUniq);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlSite = new HttpCookie("__User_True_User");
        UrlSite["Url"] = ClassSaddam.ProtectPassword(UserERasAlEstemarah, "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");
        UrlSite.Expires = DateTime.Now.AddDays(30);
        HttpContext.Current.Response.Cookies.Add(UrlSite);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlUser = new HttpCookie("__UserUniqAdmin_True_User");
        UrlUser["Url"] = ClassSaddam.ProtectPassword(IDUserRasAlEstemarah, "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");
        UrlUser.Expires = DateTime.Now.AddDays(30);
        HttpContext.Current.Response.Cookies.Add(UrlUser);
        System.Threading.Thread.Sleep(100);

        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
        System.Threading.Thread.Sleep(50);

        HttpContext.Current.Response.Redirect("../CPBeneficiary");
    }

    private void FSetCookieNotRemembermyAdmin()
    {
        HttpCookie UrlID = new HttpCookie(DateTime.Now.ToString("_545_yyyyMMyyyyMM_335_"));
        UrlID["Url"] = ClassSaddam.ProtectPassword(IDUserRasAlEstemarah, "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");
        HttpContext.Current.Response.Cookies.Add(UrlID);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlIDUniq = new HttpCookie("__CheckedAdmin_True_User");
        UrlIDUniq["Url"] = ClassSaddam.ProtectPassword(IDUniqRasAlEstemarah, "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");
        HttpContext.Current.Response.Cookies.Add(UrlIDUniq);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlSite = new HttpCookie("__User_True_User");
        UrlSite["Url"] = ClassSaddam.ProtectPassword(UserERasAlEstemarah, "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");
        HttpContext.Current.Response.Cookies.Add(UrlSite);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlUser = new HttpCookie("__UserUniqAdmin_True_User");
        UrlUser["Url"] = ClassSaddam.ProtectPassword(IDUserRasAlEstemarah, "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");
        HttpContext.Current.Response.Cookies.Add(UrlUser);
        System.Threading.Thread.Sleep(100);

        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
        System.Threading.Thread.Sleep(50);

        HttpContext.Current.Response.Redirect("../CPBeneficiary");
    }

}