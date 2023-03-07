using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class ar_MPAr : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //DateTimeFormatInfo fi = new CultureInfo("ar-SA", false).DateTimeFormat;
            //fi.Calendar = new HijriCalendar();
            //fi.ShortDatePattern = "dd/MM/yyyy";
            lblYear.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy");
            FArnArticleByViewInBarView();
        }
    }

    private void FArnArticleByViewInBarView()
    {
        ClassArticle CA = new ClassArticle();
        CA.Top = 15;
        CA.TypeArticle = 1;
        CA.IsView = true;
        CA.IsBarView = true;
        CA.DeleteArticle = false;
        DataTable dt = new DataTable();
        dt = CA.BArnArticleByViewInBarView();
        if (dt.Rows.Count > 0)
        {
            IDPar.Visible = true;
            RPTPar.DataSource = dt;
            RPTPar.DataBind();
        }
        else
        {
            IDPar.Visible = false;
        }
        FGetSetting();
    }

    public string FManagementsGetViewF()
    {
        string VaManage = "";
        int IDManagE;
        ClassMenuSite CM = new ClassMenuSite();
        CM.TypeSection = 1;
        CM.IDPartSection = 0;
        CM.ViewMenu = true;
        CM.IsDelete = false;
        DataTable dt = new DataTable();
        dt = CM.MenuSiteGetView();
        if (dt.Rows.Count > 0)
        {
            for (int x = 0; x <= dt.Rows.Count - 1; x++)
            {
                IDManagE = Convert.ToInt32(dt.Rows[x]["IDItem"]);
                if (Convert.ToBoolean(dt.Rows[x]["IsFork"]))
                {
                    VaManage += "<li>";
                    VaManage += "<a href='PageViewcontent.aspx?ID=" + dt.Rows[x]["IDItem"].ToString() + "&Name=" + dt.Rows[x]["TitleManageAr"].ToString() + "'>";
                    VaManage += dt.Rows[x]["TitleManageAr"].ToString();
                    VaManage += "</a>";
                    VaManage += CompaniesGetViewWindows(IDManagE);
                    VaManage += "</li>";
                }
                else if (Convert.ToBoolean(dt.Rows[x]["IsFork"]) == false)
                {
                    VaManage += "<li>";
                    VaManage += "<a href='PageViewcontent.aspx?ID=" + dt.Rows[x]["IDItem"].ToString() + "&Name=" + dt.Rows[x]["TitleManageAr"].ToString() + "'>";
                    VaManage += dt.Rows[x]["TitleManageAr"].ToString();
                    VaManage += "</a></li>";
                }
            }
        }
        return VaManage;
    }

    public string CompaniesGetViewWindows(int IDManage)
    {
        string VaCompanies = "";
        ClassMenuSite CM = new ClassMenuSite();
        CM.TypeSection = 2;
        CM.IDPartSection = IDManage;
        CM.ViewMenu = true;
        CM.IsDelete = false;
        DataTable dt = new DataTable();
        dt = CM.MenuSiteGetView();
        if (dt.Rows.Count > 0)
        {
            VaCompanies += "<ul class='sub1 dropdown-menu'>";
            for (int x = 0; x <= dt.Rows.Count - 1; x++)
            {
                VaCompanies += "<li> <a href='PageViewcontent.aspx?ID=" + dt.Rows[x]["IDItem"].ToString() + "&Name=" + dt.Rows[x]["TitleManageAr"].ToString() + "' title=''> * " + dt.Rows[x]["TitleManageAr"].ToString() + "</a></li>";
            }
            VaCompanies += "</ul>";
        }
        return VaCompanies;
    }

    private void FGetSetting()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select * from SettingTable Where IDSetting = @0", Convert.ToString(964654));
        if (dt.Rows.Count > 0)
        {
            lblNameOrg.Text = dt.Rows[0]["NameSiteAR"].ToString();
            IDFacebook.HRef = dt.Rows[0]["LinkFacebook"].ToString();
            IDFacebook2.HRef = IDFacebook.HRef;
            IDyoutube.HRef = dt.Rows[0]["LinkYouTube"].ToString();
            IDyoutube2.HRef = IDyoutube.HRef;
            IDtwitter.HRef = "https://twitter.com/" + dt.Rows[0]["LinkeTwiter"].ToString();
            IDtwitter2.HRef = IDtwitter.HRef;
            IDGoogleplus.HRef = dt.Rows[0]["LinkeGooglePluse"].ToString();
            IDGoogleplus2.HRef = IDGoogleplus.HRef;
            IDLocation.HRef = dt.Rows[0]["LocationSchool"].ToString();

            //Creating Meta Image
            HtmlMeta metaimage = new HtmlMeta();
            metaimage.Attributes.Add("property", "og:image");
            string xNewImage = "http://" + Request.Url.Host + "/" + dt.Rows[0]["ImgSystem"].ToString();
            metaimage.Content = xNewImage.ToString();
            Page.Header.Controls.Add(metaimage);
            //Creating Meta Description
            HtmlMeta metaDesc = new HtmlMeta();
            metaDesc.Name = "DESCRIPTION";
            metaDesc.Content = dt.Rows[0]["DescriptoinSiteAR"].ToString();
            Page.Header.Controls.Add(metaDesc);
            //Creating Meta Keywords
            HtmlMeta metaKeywords = new HtmlMeta();
            metaKeywords.Name = "KEYWORDS";
            metaKeywords.Content = dt.Rows[0]["KeyWordAR"].ToString();
            this.Page.Header.Controls.Add(metaKeywords);

            lblEmail.Text = dt.Rows[0]["MailSite"].ToString();
            IDEmail.HRef = "mailto:" + lblEmail.Text;
            lblPhone.Text = dt.Rows[0]["PhoneSite"].ToString();
            IDPhone.HRef = "tel:" + lblPhone.Text;
            if (Convert.ToBoolean(dt.Rows[0]["IsCloseSite"]))
            {
                Response.Redirect("PageSiteClosed.aspx");
            }
            if (dt.Rows[0]["IDUniq"].ToString() != "0ad8d66c-e87c-45c8-8999-b27d04f92072")
            {
                Response.Redirect("PageSiteClosed.aspx");
            }

            ClassSetting CS = new ClassSetting();
            CS.IDSetting = 964654;
            CS.CountVisit = Convert.ToInt64(dt.Rows[0]["CountVisit"]) + 1;
            CS.BArnSettingTableUpdateVisit();

            lblCountVisit.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["CountVisit"]) + 1);
        }
    }

}
