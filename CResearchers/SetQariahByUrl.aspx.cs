using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CResearchers_SetQariahByUrl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Create a Cookie with a suitable Key.  
        HttpCookie IDCookie = new HttpCookie("AllowByVillage");

        //Set the Cookie value.  
        IDCookie.Values["AllowByVillage"] = Request.QueryString["IDQariah"];

        //Set the Expiry date.  
        IDCookie.Expires = DateTime.Now.AddDays(30);

        //Add the Cookie to Browser.  
        Response.Cookies.Add(IDCookie);

        //Rediract To Cpanel
        Response.Redirect("PageElectronicGate.aspx");
    }

}