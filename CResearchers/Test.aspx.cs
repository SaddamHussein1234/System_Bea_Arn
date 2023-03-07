using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CResearchers_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnsendSet_Click(object sender, EventArgs e)
    {
        //Create a Cookie with a suitable Key.  
        HttpCookie nameCookie = new HttpCookie("Name");

        //Set the Cookie value.  
        nameCookie.Values["Name"] = txtcookie.Text;

        //Set the Expiry date.  
        nameCookie.Expires = DateTime.Now.AddDays(30);

        //Add the Cookie to Browser.  
        Response.Cookies.Add(nameCookie);
    }

    protected void btnsendGet_Click(object sender, EventArgs e)
    {
        //Fetch the Cookie using its Key.  
        HttpCookie nameCookie = Request.Cookies["Name"];

        //If Cookie exists fetch its value.  
        string name = nameCookie != null ? nameCookie.Value.Split('=')[1] : "undefined";
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + name + "');", true);
    }

}