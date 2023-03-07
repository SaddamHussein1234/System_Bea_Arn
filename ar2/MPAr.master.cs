using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_MPAr : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FGetSetting();
            Wellcome();
        }
    }

    public string SetInner()
    {
        string XPath = "";
        if (Request.Url.PathAndQuery.Contains("Default") || Request.Url.PathAndQuery.ToLower() == "/ar/default.aspx")
        {
            XPath = "site-header";
        }
        if (Request.Url.PathAndQuery.Contains("Default") == false)
        {
            XPath = "site-header inner";
        }
        return XPath;
    }

    private void Wellcome()
    {
        try
        {
            DateTime time = ClassDataAccess.GetCurrentTime();
            if ((time > Convert.ToDateTime("10:00:00 AM")) && (time < Convert.ToDateTime("11:59:50 AM")))
                lblLestName.Text = "صباح الخير";
            else if ((time > Convert.ToDateTime("12:00:00 PM")) && (time < Convert.ToDateTime("5:00:00 PM")))
                lblLestName.Text = "نهارك سعيد";
            else if ((time > Convert.ToDateTime("5:01:00 PM")) && (time < Convert.ToDateTime("11:59:50 PM")))
                lblLestName.Text = "مساء الخير";
            else if ((time > Convert.ToDateTime("12:00:00 AM")) && (time < Convert.ToDateTime("9:59:50 PM")))
                lblLestName.Text = "صباح الخير";
            else
                lblLestName.Text = "مرحباً بك";
            System.Threading.Thread.Sleep(50);
        }
        catch (Exception)
        {
            lblLestName.Text = "مرحباً بك";
        }
    }

    private void FGetSetting()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select * from SettingTable Where IDSetting = @0", Convert.ToString(964654));
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

            //lblLocation.Text = dt.Rows[0]["LocationSchool"].ToString();
            //IDSite.HRef = "http://" + dt.Rows[0]["NameSever"].ToString();
            //IDSite2.Text = "http://" + dt.Rows[0]["NameSever"].ToString().Replace("http://", "");
            //lblEmail.Text = dt.Rows[0]["MailSite"].ToString();
            //IDEmail.HRef = "mailto:" + lblEmail.Text;
            //lblPhone.Text = dt.Rows[0]["PhoneSite"].ToString();

            lblNameOrg.Text = dt.Rows[0]["NameSiteAR"].ToString();
            IDFacebook.HRef = dt.Rows[0]["LinkFacebook"].ToString();
            IDFacebook2.HRef = IDFacebook.HRef;
            IDyoutube.HRef = dt.Rows[0]["LinkYouTube"].ToString();
            IDyoutube2.HRef = IDyoutube.HRef;
            IDtwitter.HRef = dt.Rows[0]["LinkeTwiter"].ToString();
            IDtwitter2.HRef = IDtwitter.HRef;
            IDGoogleplus.HRef = dt.Rows[0]["LinkeGooglePluse"].ToString();
            IDGoogleplus2.HRef = IDGoogleplus.HRef;
            IDLocation.HRef = dt.Rows[0]["LocationSchool"].ToString();

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

            //ClassSetting CS = new ClassSetting();
            //CS.IDSetting = 1;
            //CS.CountVisit = Convert.ToInt64(dt.Rows[0]["CountVisit"]) + 1;
            //CS.BUpdateVisit();

            //lblCountVisit.Text = dt.Rows[0]["CountVisit"].ToString();
        }
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
                    VaManage += "<div class='m-service-header'>";
                    VaManage += "<a id='" + dt.Rows[x]["TitleManageAr"].ToString() + "' href='PageViewcontent.aspx?ID=" + dt.Rows[x]["IDItem"].ToString() + "&Name=" + dt.Rows[x]["TitleManageAr"].ToString() + "'>";
                    //VaManage += "<span class='icon'><i class='icon_v1 fab fa-pagelines' style='color: #0bbcba''></i></span>";
                    VaManage += "<span class='icon'><img src='../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png' style='width:80px; height:80px' /></span>";
                    VaManage += dt.Rows[x]["TitleManageAr"].ToString();
                    VaManage += "</a>";
                    VaManage += CompaniesGetViewWindows(IDManagE);
                    VaManage += "</div>";
                }
                else if (Convert.ToBoolean(dt.Rows[x]["IsFork"]) == false)
                {
                    VaManage += "<div class='m-service-header'>";
                    VaManage += "<a id='" + dt.Rows[x]["TitleManageAr"].ToString() + "' href='PageViewcontent.aspx?ID=" + dt.Rows[x]["IDItem"].ToString() + "&Name=" + dt.Rows[x]["TitleManageAr"].ToString() + "'>";
                    //VaManage += "<span class='icon'><i class='icon_v1 fab fa-pagelines' style='color: #0bbcba''></i></span>";
                    VaManage += "<span class='icon'><img src='../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png' style='width:80px; height:80px' /></span>";
                    VaManage += dt.Rows[x]["TitleManageAr"].ToString();

                    VaManage += "</a></div>";
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
            VaCompanies += "<ul>";
            for (int x = 0; x <= dt.Rows.Count - 1; x++)
            {
                VaCompanies += "<li> <a class='hover' id='" + dt.Rows[x]["TitleManageAr"].ToString() + "' href='PageViewcontent.aspx?ID=" + dt.Rows[x]["IDItem"].ToString() + "&Name=" + dt.Rows[x]["TitleManageAr"].ToString() + "' title=''> * " + dt.Rows[x]["TitleManageAr"].ToString() + "</a></li>";
            }
            VaCompanies += "</ul>";
        }
        return VaCompanies;
    }


}
