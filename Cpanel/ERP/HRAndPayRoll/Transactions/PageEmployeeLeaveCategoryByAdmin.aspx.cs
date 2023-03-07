using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeLeaveCategoryByAdmin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageEmployeeLeaveCategoryByAdmin.XType = "Manager";
    }

}