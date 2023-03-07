using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_Cash_Donation_PageView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ID"] != string.Empty)
            PageView.SetData(Request.QueryString["IDUniq"], Request.QueryString["ID"]);
    }

}