﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeLeaveCategoryList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageEmployeeLeaveCategoryList.XType = "Manager";
    }
    
}