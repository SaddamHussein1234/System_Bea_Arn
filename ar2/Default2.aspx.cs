using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            FGetSetting();
    }

    private void FGetSetting()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) * from SettingTable With(NoLock) Where IDSetting = @0", Convert.ToString(964654));
        if (dt.Rows.Count > 0)
        {
            //IDImgVison.Src = "../" + dt.Rows[0]["ImgVisionAr"].ToString();
            lblAbout.Text = dt.Rows[0]["TextAboutAr"].ToString();
            lblVision.Text = dt.Rows[0]["TextVisionAr"].ToString();
            lblMessage.Text = dt.Rows[0]["TextMessageAr"].ToString();
            lblGoals.Text = dt.Rows[0]["TextGoalsAr"].ToString();
            lblValus.Text = dt.Rows[0]["TextValuesAr"].ToString();
            //IDVideo.Src = dt.Rows[0]["VideoAboutAr"].ToString();
            //lblNameSite.Text = dt.Rows[0]["NameSiteAR"].ToString();
            //ImgSystem.Src = "../" + dt.Rows[0]["ImgSystem"].ToString();

            //IDFacebook.HRef = dt.Rows[0]["LinkFacebook"].ToString();

            //IDYoutube.HRef = dt.Rows[0]["LinkYouTube"].ToString();

            //IDTwitter.HRef = dt.Rows[0]["LinkeTwiter"].ToString();

            //IDGoogleplus.HRef = dt.Rows[0]["LinkeGooglePluse"].ToString();

            //lblLocation.Text = dt.Rows[0]["LocationSchool"].ToString();
            //IDSite.HRef = "http://" + dt.Rows[0]["NameSever"].ToString();
            //IDSite2.Text = "http://" + dt.Rows[0]["NameSever"].ToString().Replace("http://", "");
            //lblEmail.Text = dt.Rows[0]["MailSite"].ToString();
            //IDEmail.HRef = "mailto:" + lblEmail.Text;
            //lblPhone.Text = dt.Rows[0]["PhoneSite"].ToString();
            if (Convert.ToBoolean(dt.Rows[0]["IsCloseSite"]))
            {
                Response.Redirect("PageSiteClosed.aspx");
            }
            if (dt.Rows[0]["IDUniq"].ToString() != "0ad8d66c-e87c-45c8-8999-b27d04f92072")
            {
                Response.Redirect("PageSiteClosed.aspx");
            }

            //ClassSetting CS = new ClassSetting();
            //CS.IDSetting = 1;
            //CS.CountVisit = Convert.ToInt64(dt.Rows[0]["CountVisit"]) + 1;
            //CS.BUpdateVisit();

            //lblCountVisit.Text = dt.Rows[0]["CountVisit"].ToString();

        }
    }

}