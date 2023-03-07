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
            lblYear.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy");
            FGetSetting();
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
                    VaManage += "<li class='nav-item nav-dropdown-item'>";
                    VaManage += "<a href='PageViewcontent.aspx?ID=" + dt.Rows[x]["IDItem"].ToString() + "&Uniq=" + Guid.NewGuid().ToString() + "'><i class='" + dt.Rows[x]["_Icon_"].ToString() + "'></i>&nbsp;&nbsp;" + dt.Rows[x]["TitleManageAr"].ToString() + " <span class='fas fa-angle-down'></span></a>";
                    VaManage += CompaniesGetViewWindows(IDManagE);
                    VaManage += "</li>";
                }
                else if (Convert.ToBoolean(dt.Rows[x]["IsFork"]) == false)
                    VaManage += "<li class='nav-item'><a href='PageViewcontent.aspx?ID=" + dt.Rows[x]["IDItem"].ToString() + "&Uniq=" + Guid.NewGuid().ToString() + "'><i class='" + dt.Rows[x]["_Icon_"].ToString() + "'></i>&nbsp;&nbsp;" + dt.Rows[x]["TitleManageAr"].ToString() + "</a></li>";
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
            VaCompanies += "<ul class=nav-dropdown><div class=menu-links>";
            for (int x = 0; x <= dt.Rows.Count - 1; x++)
                VaCompanies += "<div class='menu_block single'><b><a href='PageViewcontent.aspx?ID=" + dt.Rows[x]["IDItem"].ToString() + "&Uniq=" + Guid.NewGuid().ToString() + "'><i class='" + dt.Rows[x]["_Icon_"].ToString() + "'></i>&nbsp;&nbsp;" + dt.Rows[x]["TitleManageAr"].ToString() + "</a></b></div>";
            VaCompanies += "</ul>";
        }
        return VaCompanies;
    }

    private void FGetSetting()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) * from [dbo].[SettingTable] With(NoLock) Where IDSetting = @0", Convert.ToString(964654));
        if (dt.Rows.Count > 0)
        {
            lblAbout.Text = dt.Rows[0]["TextAboutAr"].ToString();

            lblNameOrg.Text = dt.Rows[0]["NameSiteAR"].ToString();
            lblName.Text = lblNameOrg.Text;
            IDFacebook.HRef = dt.Rows[0]["LinkFacebook"].ToString();
            IDyoutube.HRef = dt.Rows[0]["LinkYouTube"].ToString();
            IDtwitter.HRef = dt.Rows[0]["LinkeTwiter"].ToString();
            IDLocation.HRef = dt.Rows[0]["LocationSchool"].ToString();
            IDShortIcon.Href = "/ImgSystem/ImgSetting/StartLogo.png";

            lblEmail.Text = dt.Rows[0]["MailSite"].ToString();
            IDEmail.HRef = "mailto:" + lblEmail.Text;
            lblPhone.Text = dt.Rows[0]["PhoneSite"].ToString();
            lblPhone2.Text = lblPhone.Text;
            IDPhone.HRef = "tel:" + lblPhone.Text;
            IDPhone2.HRef = IDPhone.HRef;
            if (Convert.ToBoolean(dt.Rows[0]["IsCloseSite"]))
                Response.Redirect("PageSiteClosed.aspx");
            if (dt.Rows[0]["IDUniq"].ToString() != "0ad8d66c-e87c-45c8-8999-b27d04f92072")
                Response.Redirect("PageSiteClosed.aspx");

            ClassSetting CS = new ClassSetting();
            CS.IDSetting = 964654;
            CS.CountVisit = Convert.ToInt64(dt.Rows[0]["CountVisit"]) + 1;
            CS.BArnSettingTableUpdateVisit();

            lblCountVisit.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["CountVisit"]) + 1);
        }
    }

}
