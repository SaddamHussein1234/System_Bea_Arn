using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CPBeneficiary_PrintFootable1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Control ctrl = (Control)Session["footable1"];
        Library_CLS_Arn.Saddam.ClassPrint_Helper.PrintWebControl2(ctrl);
    }
}