using Library_CLS_Arn.ERP.Permissions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageEmployeeDefinitionOfSalary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A153");
            PageEmployeeDefinitionOfSalary.FGetData("GetByIDUniq", Request.QueryString["ID"]);
        }
    }

}