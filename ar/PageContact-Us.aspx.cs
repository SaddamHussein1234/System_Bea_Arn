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
            HFLink.Value = Request.Url.Authority; HFQuery.Value = Request.Url.PathAndQuery;
            FGetSetting();
            this.Page.Header.Title = " إترك لنا رسالة " + " - " + HFNameSite.Value;
            if (Request.QueryString["Type"] != null)
                DLType.SelectedValue = Request.QueryString["Type"];
            txtName.Focus();
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        Response.Redirect("/ar/");
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            if (this.Page.IsValid && txtCapatsha.Text.ToString() == Session["randomNumber"].ToString())
                FContentAdd();
            else
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رمز التحقق غير صحيح ,,, ";
                txtCapatsha.Text = string.Empty;
                txtCapatsha.Focus();
            }
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
        Ccont.CountryUser = DLCountry.SelectedValue;
        Ccont.EmailUser = txtEmail.Text.Trim();
        Ccont.PhoneUser = txtPhone.Text.Trim();
        Ccont.DetailsMessega = txtMessage.Text.Trim().Replace(Environment.NewLine, "<br />");
        Ccont.DateSend = Convert.ToString(ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss"));
        Ccont.TypeMessage = DLType.SelectedItem.Text;
        Ccont.BContentAdd();
        pnlMessage.Visible = false;
        pnlOK.Visible = true;
        this.Page.Header.Title = " إترك لنا رسالة " + " - " + HFNameSite.Value;
    }

    private void FGetSetting()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) * from SettingTable With(NoLock) Where IDSetting = @0", Convert.ToString(964654));
        if (dt.Rows.Count > 0)
        {
            HFNameSite.Value = dt.Rows[0]["NameSiteAR"].ToString();

            HFTitle.Value = "إترك لنا رسالة";
            HFDescrption.Value = dt.Rows[0]["DescriptoinSiteAR"].ToString();
            HFKeyWord.Value = dt.Rows[0]["KeyWordAR"].ToString();
            HFImage.Value = dt.Rows[0]["ImgSystem"].ToString();

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