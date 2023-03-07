using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PrintFootable_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Control ctrl = (Control)Session["footable"];
        ClassPrint_Helper.PrintWebControl_(ctrl);
    }
}