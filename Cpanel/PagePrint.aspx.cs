using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_PagePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Control ctrl = (Control)Session["foot"];
        ClassPrint_Helper.PrintWebControl4(ctrl);
    }

}