﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PagePrintDocument : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Control ctrl = (Control)Session["foot"];
        Library_CLS_Arn.Saddam.ClassPrint_Helper.PrintWebControlDouument(ctrl);
    }

}