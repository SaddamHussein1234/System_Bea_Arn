using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FGetSetting();

            ClassArticle CA = new ClassArticle();
            CA.Top = 15;
            CA.TypeArticle = 1;
            CA.IsView = true;
            CA.IsSlide = true;
            CA.DeleteArticle = false;
            DataTable dt = new DataTable();
            dt = CA.BArnArticleByViewInSlide();
            if (dt.Rows.Count == 0)
            {
                IDSlide.Visible = false;
            }
        }
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
            lblValus.Text = dt.Rows[0]["TextValuesAr"].ToString();

            this.Page.Header.Title = string.Format(dt.Rows[0]["NameSiteAR"].ToString() + " | " + "الرئيسية");
            FGetLastNews();
        }
    }

    public string FArnArticleByViewInSlideByFanction()
    {
        string XResult = "";
        ClassArticle CA = new ClassArticle();
        CA.Top = 15;
        CA.TypeArticle = 1;
        CA.IsView = true;
        CA.IsSlide = true;
        CA.DeleteArticle = false;
        DataTable dt = new DataTable();
        dt = CA.BArnArticleByViewInSlide();
        if (dt.Rows.Count > 0)
        {
            XResult += "<div class='carousel-inner carousel-zoom'>";
            for (int x = 0; x <= dt.Rows.Count - 1; x++)
            {
                if (x < 1)
                {
                    XResult += "<div class='item active'><a href='PageViewDetails.aspx?ID=" + dt.Rows[x]["IDUniqArticle"].ToString() + "'><img src='../" + dt.Rows[x]["ImgArt"].ToString() + "' alt='Loading ... ' /><div class='carousel-caption'><div class='content'><h3>" + dt.Rows[x]["TitleArticle"].ToString() + "</h3><p>" + dt.Rows[x]["TitleArticle"].ToString() + "</p></div></div></a></div>";
                }
                else
                {
                    XResult += "<div class='item'><a href='PageViewDetails.aspx?ID=" + dt.Rows[x]["IDUniqArticle"].ToString() + "'><img src='../" + dt.Rows[x]["ImgArt"].ToString() + "' alt='Loading ... ' /><div class='carousel-caption'><div class='content'><h3>" + dt.Rows[x]["TitleArticle"].ToString() + "</h3><p>" + dt.Rows[x]["TitleArticle"].ToString() + "</p></div></div></a></div>";
                }

            }
            XResult += "</div>";
            XResult += "<div class='galleryControls'>";
            XResult += "<a class='carousel-control left' href='#home_gallery' role='button' data-slide='prev'><i class='fa fa-angle-left'></i></a>";
            XResult += "<a class='carousel-control right' href='#home_gallery' role='button' data-slide='next'><i class='fa fa-angle-right'></i></a>";
            XResult += "</div>";
        }
        return XResult;
    }

    public void FGetLastNews()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) *  FROM [dbo].[tbl_Article] Where TypeArticle = @0 And IsView = @1 And DeleteArticle = @2 Order By DateAddArticle", "1", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPTLastNews.DataSource = dt;
            RPTLastNews.DataBind();
        }
        else
            RPTLastNews.Visible = false;
        FGetArticalInSite();
    }

    public void FGetArticalInSite()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) *  FROM [dbo].[tbl_Article] Where TypeArticle = @0 And IsView = @1 And DeleteArticle = @2 And [IsSite] = @3 Order By DateAddArticle", "1", Convert.ToString(true), Convert.ToString(false), Convert.ToString(true));
        if (dt.Rows.Count > 0)
        {
            RPTSite.DataSource = dt;
            RPTSite.DataBind();
            IDSite.Visible = true;
        }
        else
            IDSite.Visible = false;
    }

    public string FText(string XHTMLText)
    {
        string XResult = string.Empty;
        var result = Regex.Replace(XHTMLText, @"<(.|\n)*?>", string.Empty);
        XResult = result.ToString();
        if (XResult.Length > 20)
            XResult.Remove(20);
        return XResult;
    }

}