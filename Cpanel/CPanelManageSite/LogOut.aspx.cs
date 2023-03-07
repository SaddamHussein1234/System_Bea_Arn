using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelManageSite_LogOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")].Expires = DateTime.Now.AddDays(-30);
            Response.Cookies["__CheckedAdmin_True_"].Expires = DateTime.Now.AddDays(-30);
            Response.Cookies["__User_True_"].Expires = DateTime.Now.AddDays(-30);
            Response.Cookies["__UserUniqAdmin_True_"].Expires = DateTime.Now.AddDays(-30);
            Response.Cookies["__User_Screen_"].Expires = DateTime.Now.AddDays(-30);
            Session.RemoveAll();
            Repostry_Tricker_.FAPP_Add("AddLogin", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "SSP", "الإعدادات والصلاحيات", "تسجيل خروج", "خروج آمن",
                ClassSaddam.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss"));
            Response.Redirect("../Login.aspx");

        }
        catch
        {
            Response.Redirect("../Login.aspx");
        }
    }

}