using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_PageMethodsOfCommunication : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Page.Header.Title = ClassSetting.FGetNameSiteAR() + " | " + " الموقع وطرق الإتصال ";
            FGetSetting();
        }
    }

    private void FGetSetting()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select * from SettingTable Where IDSetting = @0", Convert.ToString(964654));
        if (dt.Rows.Count > 0)
        {
            IDFacebook.HRef = dt.Rows[0]["LinkFacebook"].ToString();
            IDyoutube.HRef = dt.Rows[0]["LinkYouTube"].ToString();
            IDtwitter.HRef = dt.Rows[0]["LinkeTwiter"].ToString();
            IDGoogleplus.HRef = dt.Rows[0]["LinkeGooglePluse"].ToString();
            IDLocation.HRef = dt.Rows[0]["LocationSchool"].ToString();

            lblEmail.Text = dt.Rows[0]["MailSite"].ToString();
            IDEmail.HRef = "mailto:" + lblEmail.Text;
            lblPhone.Text = dt.Rows[0]["PhoneSite"].ToString();
            IDPhone.HRef = "tel:" + lblPhone.Text;
        }
    }

}