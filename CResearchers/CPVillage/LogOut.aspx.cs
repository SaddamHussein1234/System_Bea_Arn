using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CResearchers_CPVillage_LogOut : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")].Expires = DateTime.Now.AddDays(-30);
            Response.Cookies["__CheckedAdmin_True_"].Expires = DateTime.Now.AddDays(-30);
            Response.Cookies["__User_True_"].Expires = DateTime.Now.AddDays(-30);
            Response.Cookies["__UserUniqAdmin_True_"].Expires = DateTime.Now.AddDays(-30);
            Response.Cookies["AllowByVillage"].Expires = DateTime.Now.AddDays(-30);
            Session.RemoveAll();

            //G/etCookie();
            //ClassTrickerAdmin.TrickerAdd(Convert.ToInt32(IDUser), "إضافة", " خروج آمن ", ClassKhwarism.GetCurrentTime().ToString("yyyy/MM/dd hh:mm:ss"));

            Response.Redirect("../Login.aspx");

        }
        catch
        {
            Response.Redirect("../Login.aspx");
        }
    }

}