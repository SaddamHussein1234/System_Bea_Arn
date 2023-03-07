using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            FGetSetting();
    }

    private void FGetSetting()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) StartRediract from SettingTable With(NoLock) Where IDSetting = @0", Convert.ToString(964654));
        if (dt.Rows.Count > 0)
            Response.Redirect(dt.Rows[0]["StartRediract"].ToString() + "/");
    }

}