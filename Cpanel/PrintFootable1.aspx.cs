using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FusionCharts.Charts;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_PrintFootable1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Control ctrl = (Control)Session["footable1"];
        ClassPrint_Helper.PrintWebControl2(ctrl);
    }

}