using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_PageAbout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HFLink.Value = Request.Url.Authority;
            FGetSetting();
        }
    }

    private void FGetSetting()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) * from SettingTable With(NoLock) Where IDSetting = @0", Convert.ToString(964654));
        if (dt.Rows.Count > 0)
        {
            this.Page.Header.Title = string.Format("نبذة عنا" + " - " + dt.Rows[0]["NameSiteAR"].ToString());
            HFTitle.Value = "نبذة عنا";
            HFDescrption.Value = dt.Rows[0]["DescriptoinSiteAR"].ToString();
            HFKeyWord.Value = dt.Rows[0]["KeyWordAR"].ToString();
            HFImage.Value = dt.Rows[0]["ImgSystem"].ToString();

            lblAbout.Text = dt.Rows[0]["TextAboutAr"].ToString();
            lblVision.Text = dt.Rows[0]["TextVisionAr"].ToString();
            lblMessage.Text = dt.Rows[0]["TextMessageAr"].ToString();
            lblGoals.Text = dt.Rows[0]["TextGoalsAr"].ToString();
            lblValus.Text = dt.Rows[0]["TextValuesAr"].ToString();

            IDFacebook.HRef = dt.Rows[0]["LinkFacebook"].ToString();
            IDyoutube.HRef = dt.Rows[0]["LinkYouTube"].ToString();
            IDtwitter.HRef = dt.Rows[0]["LinkeTwiter"].ToString();
            IDLocation.HRef = dt.Rows[0]["LocationSchool"].ToString();
            lblEmail.Text = dt.Rows[0]["MailSite"].ToString();
            IDEmail.HRef = "mailto:" + lblEmail.Text;
            lblPhone.Text = dt.Rows[0]["PhoneSite"].ToString();
            IDPhone.HRef = "tel:" + lblPhone.Text;
        }
    }

}