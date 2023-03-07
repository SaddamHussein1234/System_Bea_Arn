using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_PageContact_Us : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Page.Header.Title = ClassSetting.FGetNameSiteAR() + " | " + " إتصل بنا ";
            FGetSetting();
        }
    }

    private void FGetSetting()
    {
        ClassSetting CS = new ClassSetting();
        CS.IDSetting = 964654;
        DataTable dt = new DataTable();
        dt = CS.BSettingGetById();
        if (dt.Rows.Count > 0)
        {
            lblNameOrg.Text = dt.Rows[0]["NameSiteAR"].ToString();
            IDFacebook.HRef = dt.Rows[0]["LinkFacebook"].ToString();
            IDyoutube.HRef = dt.Rows[0]["LinkYouTube"].ToString();
            IDtwitter.HRef = dt.Rows[0]["LinkeTwiter"].ToString();
            IDGoogleplus.HRef = dt.Rows[0]["LinkeGooglePluse"].ToString();
            IDLocation.HRef = dt.Rows[0]["LocationSchool"].ToString();

            lblEmail.Text = dt.Rows[0]["MailSite"].ToString();
            IDEmail.HRef = "mailto:" + lblEmail.Text;
            //lblPhone.Text = dt.Rows[0]["PhoneSite"].ToString();
        }
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            FContentAdd();
        }
        catch (Exception)
        {
            
        }
    }

    private void FContentAdd()
    {
        ClassContent Ccont = new ClassContent();
        Ccont.TitleMeassge = txtTitle.Text.Trim();
        Ccont.NameUser = txtName.Text.Trim();
        Ccont.CountryUser = "Saudi Arabia";
        Ccont.EmailUser = txtEmail.Text.Trim();
        Ccont.PhoneUser = txtPhone.Text.Trim();
        Ccont.DetailsMessega = txtMessage.Text.Trim();
        Ccont.DateSend = Convert.ToString(ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd hh:mm:ss"));
        Ccont.BContentAdd();
        pnlMessage.Visible = false;
        pnlTitle.Visible = false;
        pnlOK.Visible = true;
        this.Page.Header.Title = ClassSetting.FGetNameSiteAR() + " | " + " إتصل بنا ";
    }

}