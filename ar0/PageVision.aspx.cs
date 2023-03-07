using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_PageVision : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FGetSetting();
        }
    }

    private void FGetSetting()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) * from SettingTable With(NoLock) Where IDSetting = @0", Convert.ToString(964654));
        if (dt.Rows.Count > 0)
        {
            lblAbout.Text = dt.Rows[0]["TextAboutAr"].ToString();
            lblVision.Text = dt.Rows[0]["TextVisionAr"].ToString();
            lblMessage.Text = dt.Rows[0]["TextMessageAr"].ToString();
            lblValus.Text = dt.Rows[0]["TextValuesAr"].ToString();
            lblGoals.Text = dt.Rows[0]["TextGoalsAr"].ToString();
        }
    }

}