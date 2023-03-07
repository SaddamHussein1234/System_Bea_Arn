using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CResearchers_LoginAdmin : System.Web.UI.Page
{
    string IDUser, IDUniq, UserE, PassU;
    bool IsBaheth, IsBlockAdmin, IsDeleteAdmin, IsBlockGroup, IsDeleteGroup;
    ClassAdmin_Arn CAA = new ClassAdmin_Arn();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtUserAdmin.Focus();
            HttpCookie cookie = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
            if (cookie != null)
            {
                Response.Redirect("Default.aspx");
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
        else
        {
            //lblResult.Text = "Cookies enabled?: Yes";
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
                CAA._User_Name_ = txtUserAdmin.Text.Trim();
                DataTable dt = new DataTable();
                dt = CAA.BAdminLogin();
                if (dt.Rows.Count > 0)
                {
                    IDUser = dt.Rows[0]["ID_Item"].ToString();
                    IDUniq = dt.Rows[0]["IDUniq"].ToString();
                    UserE = dt.Rows[0]["User_Name_"].ToString();
                    PassU = dt.Rows[0]["__pass_"].ToString();
                    IsBaheth = Convert.ToBoolean(dt.Rows[0]["IsBaheth"]);
                    IsBlockAdmin = Convert.ToBoolean(dt.Rows[0]["IsBlock"]);
                    IsDeleteAdmin = Convert.ToBoolean(dt.Rows[0]["IsDelete"]);
                    IsBlockGroup = Convert.ToBoolean(dt.Rows[0]["IsActiveGroup"]);
                    IsDeleteGroup = Convert.ToBoolean(dt.Rows[0]["IsDeleteGroup"]);
                    if (txtPassAdmin.Text.Trim() == ClassEncryptPassword.Decrypt(PassU, "www.ITFY-Edu.Net_For_Saddam"))
                    {
                        //ClassTrickerAdmin.TrickerAdd(Convert.ToInt32(IDUser), "إضافة", " دخول آمن لحساب " + dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString(), ClassKhwarism.GetCurrentTime().ToString("yyyy/MM/dd hh:mm:ss"));
                        CheckIsBaheth();
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
            System.Threading.Thread.Sleep(50);
            uxdivErrorMessage.Visible = true;
            lbmsg.Text = "يرجى إدخال إسم المستخدم";
        }

    }

    private void CheckIsBaheth()
    {
        if (IsBaheth == false)
        {
            uxdivErrorMessage.Visible = true;
            lbmsg.Text = "أنت لست باحث إجتماعي";
        }
        else
        {
            uxdivErrorMessage.Visible = false;
            CheckBlockAdmin();
        }
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
        if (IsDeleteGroup)
        {
            uxdivErrorMessage.Visible = true;
            lbmsg.Text = "تم حذف المجموعة التي تنتمي اليها";
        }
        else
        {
            uxdivErrorMessage.Visible = false;
            if (this.Page.IsValid && txtCapatshaAdmin.Text.ToString() == Session["randomNumber"].ToString())
            {
                if (CBRememberMe.Checked == true)
                {
                    FSetCookieWithRemembermy();
                }
                else if (CBRememberMe.Checked == false)
                {
                    FSetCookieNotRemembermy();
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

    private void FSetCookieWithRemembermy()
    {
        //HttpCookie UrlID = new HttpCookie(DateTime.Now.ToString("_545_yyyyMMyyyyMM_22_"));
        //UrlID["Url"] = ClassSaddam.ProtectPassword(IDUser, "bahith.ITFY-Edu.Net_BySADDAM");
        //UrlID.Expires = DateTime.Now.AddDays(30);
        //HttpContext.Current.Response.Cookies.Add(UrlID);
        //System.Threading.Thread.Sleep(100);

        //HttpCookie UrlIDUniq = new HttpCookie("__Checkedbahith_True_");
        //UrlIDUniq["Url"] = ClassSaddam.ProtectPassword(IDUniq, "bahith.ITFY-Edu.Net_BySADDAM");
        //UrlIDUniq.Expires = DateTime.Now.AddDays(30);
        //HttpContext.Current.Response.Cookies.Add(UrlIDUniq);
        //System.Threading.Thread.Sleep(100);

        //HttpCookie UrlSite = new HttpCookie("__Userbahith_True_");
        //UrlSite["Url"] = ClassSaddam.ProtectPassword(UserE, "bahith.ITFY-Edu.Net_BySADDAM");
        //UrlSite.Expires = DateTime.Now.AddDays(30);
        //HttpContext.Current.Response.Cookies.Add(UrlSite);
        //System.Threading.Thread.Sleep(100);

        //HttpCookie UrlUser = new HttpCookie("__UserUniqbahith_True_");
        //UrlUser["Url"] = ClassSaddam.ProtectPassword(IDUser, "bahith.ITFY-Edu.Net_BySADDAM");
        //UrlUser.Expires = DateTime.Now.AddDays(30);
        //HttpContext.Current.Response.Cookies.Add(UrlUser);
        //System.Threading.Thread.Sleep(100);

        //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //HttpContext.Current.Response.Cache.SetNoStore();
        //System.Threading.Thread.Sleep(50);

        //------------------------
        HttpCookie UrlID = new HttpCookie(DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_"));
        UrlID["Url"] = ClassSaddam.ProtectPassword(IDUser, "www.ITFY-Edu.Net_ProtectBySADDAM");
        UrlID.Expires = DateTime.Now.AddDays(30);
        HttpContext.Current.Response.Cookies.Add(UrlID);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlIDUniq = new HttpCookie("__CheckedAdmin_True_");
        UrlIDUniq["Url"] = ClassSaddam.ProtectPassword(IDUniq, "www.ITFY-Edu.Net_ProtectBySADDAM");
        UrlIDUniq.Expires = DateTime.Now.AddDays(30);
        HttpContext.Current.Response.Cookies.Add(UrlIDUniq);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlSite = new HttpCookie("__User_True_");
        UrlSite["Url"] = ClassSaddam.ProtectPassword(UserE, "www.ITFY-Edu.Net_ProtectBySADDAM");
        UrlSite.Expires = DateTime.Now.AddDays(30);
        HttpContext.Current.Response.Cookies.Add(UrlSite);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlUser = new HttpCookie("__UserUniqAdmin_True_");
        UrlUser["Url"] = ClassSaddam.ProtectPassword(IDUser, "www.ITFY-Edu.Net_ProtectBySADDAM");
        UrlUser.Expires = DateTime.Now.AddDays(30);
        HttpContext.Current.Response.Cookies.Add(UrlUser);
        System.Threading.Thread.Sleep(100);

        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
        System.Threading.Thread.Sleep(50);

        HttpContext.Current.Response.Redirect("../CResearchers");
    }

    private void FSetCookieNotRemembermy()
    {
        //HttpCookie UrlID = new HttpCookie(DateTime.Now.ToString("_545_yyyyMMyyyyMM_22_"));
        //UrlID["Url"] = ClassSaddam.ProtectPassword(IDUser, "bahith.ITFY-Edu.Net_BySADDAM");
        //HttpContext.Current.Response.Cookies.Add(UrlID);
        //System.Threading.Thread.Sleep(100);

        //HttpCookie UrlIDUniq = new HttpCookie("__Checkedbahith_True_");
        //UrlIDUniq["Url"] = ClassSaddam.ProtectPassword(IDUniq, "bahith.ITFY-Edu.Net_BySADDAM");
        //HttpContext.Current.Response.Cookies.Add(UrlIDUniq);
        //System.Threading.Thread.Sleep(100);

        //HttpCookie UrlSite = new HttpCookie("__Userbahith_True_");
        //UrlSite["Url"] = ClassSaddam.ProtectPassword(UserE, "bahith.ITFY-Edu.Net_BySADDAM");
        //HttpContext.Current.Response.Cookies.Add(UrlSite);
        //System.Threading.Thread.Sleep(100);

        //HttpCookie UrlUser = new HttpCookie("__UserUniqbahith_True_");
        //UrlUser["Url"] = ClassSaddam.ProtectPassword(IDUser, "bahith.ITFY-Edu.Net_BySADDAM");
        //HttpContext.Current.Response.Cookies.Add(UrlUser);
        //System.Threading.Thread.Sleep(100);

        //HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //HttpContext.Current.Response.Cache.SetNoStore();
        //System.Threading.Thread.Sleep(50);

        //------------------------
        HttpCookie UrlID = new HttpCookie(DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_"));
        UrlID["Url"] = ClassSaddam.ProtectPassword(IDUser, "www.ITFY-Edu.Net_ProtectBySADDAM");
        HttpContext.Current.Response.Cookies.Add(UrlID);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlIDUniq = new HttpCookie("__CheckedAdmin_True_");
        UrlIDUniq["Url"] = ClassSaddam.ProtectPassword(IDUniq, "www.ITFY-Edu.Net_ProtectBySADDAM");
        HttpContext.Current.Response.Cookies.Add(UrlIDUniq);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlSite = new HttpCookie("__User_True_");
        UrlSite["Url"] = ClassSaddam.ProtectPassword(UserE, "www.ITFY-Edu.Net_ProtectBySADDAM");
        HttpContext.Current.Response.Cookies.Add(UrlSite);
        System.Threading.Thread.Sleep(100);

        HttpCookie UrlUser = new HttpCookie("__UserUniqAdmin_True_");
        UrlUser["Url"] = ClassSaddam.ProtectPassword(IDUser, "www.ITFY-Edu.Net_ProtectBySADDAM");
        HttpContext.Current.Response.Cookies.Add(UrlUser);
        System.Threading.Thread.Sleep(100);

        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.Cache.SetNoStore();
        System.Threading.Thread.Sleep(50);

        HttpContext.Current.Response.Redirect("../CResearchers");
    }

}