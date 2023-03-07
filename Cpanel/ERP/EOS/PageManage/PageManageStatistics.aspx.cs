using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_EOS_PageManage_PageManageStatistics : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CLS_Permissions.CheckAccountAdmin("A129");
    }

}