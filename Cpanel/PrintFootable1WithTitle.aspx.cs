using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PrintFootable1WithTitle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string XXTitle = Session["XXTitle"].ToString();
        Control ctrl = (Control)Session["footable1Title"];
        ClassPrint_Helper.PrintWebControlWithTitle(ctrl, XXTitle);
    }

}