using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WUCFooterBottom : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FGetFooter();
        }
    }

    public void FGetFooter()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) * from SettingTable With(NoLock) Where IDSetting = @0", Convert.ToString(964654));
        if (dt.Rows.Count > 0)
        {
            lblDatePrint.Text = ClassDataAccess.GetCurrentTime().ToString("dd/MM/yyyy hh:mm:ss ttt");
            /*IDTwitter.HRef = "https://twitter.com/" + dt.Rows[0]["LinkeTwiter"].ToString();
            lblTwitter.Text = dt.Rows[0]["LinkeTwiter"].ToString();
            lblPhone.Text = dt.Rows[0]["PhoneSite"].ToString();
            lblMail.Text = dt.Rows[0]["MailSite"].ToString();*/
        }
    }

}