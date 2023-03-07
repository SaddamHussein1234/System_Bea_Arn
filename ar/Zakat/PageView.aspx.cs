using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_Zakat_PageView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["IDYear"] != null)
            {
                PageView.SetData(Request.QueryString["IDYear"], Request.QueryString["ID"], Request.QueryString["IDP"]);
                PageView.XView();
            }
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        PageView.FPrint();
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("/ar/");
    }

}