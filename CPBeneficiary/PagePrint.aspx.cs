using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CPBeneficiary_PagePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Control ctrl = (Control)Session["foot"];
        Library_CLS_Arn.Saddam.ClassPrint_Helper.PrintWebControl4(ctrl);
    }

}