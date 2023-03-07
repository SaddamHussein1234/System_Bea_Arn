using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Test_Saddam
/// </summary>
public class Test_Saddam
{
    public Test_Saddam()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static string GetCookie()
    {
        string IDUser = string.Empty;
        try
        {
            HttpCookie Cooke;  // رقم المستخدم
            Cooke = HttpContext.Current.Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
            IDUser = ClassSaddam.UnprotectPassword(Cooke["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
        }
        catch
        {
            HttpContext.Current.Response.Redirect("/Cpanel/LogOut.aspx");
        }
        return IDUser;
    }

    public static int FGetIDUsiq()
    {
        return Convert.ToInt32(GetCookie());
    }

}