using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_PageViewDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Label1.Text = ClassDataAccess.GetCurrentTime().ToString("ddd , dd MMM yyyy");

        Label1.Text = DateTime.Now.ToString("ddd , dd MMM yyyy");
    }
}