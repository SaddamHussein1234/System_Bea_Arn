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
            this.Page.Header.Title = string.Format(dt.Rows[0]["NameSiteAR"].ToString());
            lblAbout.Text = dt.Rows[0]["TextAboutAr"].ToString();
            lblAbout2.Text = lblAbout.Text;

            HFTitle.Value = dt.Rows[0]["NameSiteAR"].ToString();
            HFDescrption.Value = dt.Rows[0]["DescriptoinSiteAR"].ToString();
            HFKeyWord.Value = dt.Rows[0]["KeyWordAR"].ToString();
            HFImage.Value = dt.Rows[0]["ImgSystem"].ToString();
            HFCount.Value = dt.Rows[0]["CountVisit"].ToString();
            FGetDataSlide();
            FArnArticleByViewIn();
            GetImageAlbumByViewByArLast();
            FGetArticalInSite();
            FGetStatistical();
        }
    }

    private void FArnArticleByViewIn()
    {
        ClassArticle CA = new ClassArticle();
        CA.Top = 30;
        CA.TypeArticle = 1;
        CA.IsView = true;
        CA.IsBarView = true;
        CA.DeleteArticle = false;
        DataTable dt = new DataTable();
        dt = CA.BArnArticleByViewInBarView();
        if (dt.Rows.Count > 0)
        {
            IDPar.Visible = true;
            RPTNews.DataSource = dt;
            RPTNews.DataBind();
        }
        else
            IDPar.Visible = false;
    }

    private void GetImageAlbumByViewByArLast()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP (5) * FROM [dbo].[tbl_AlbumImg] With(NoLock) Where [IsDelete] = 0 Order by [DataAddImg] Desc");
        if (dt.Rows.Count > 0)
        {
            RPTAlbumImages.DataSource = dt;
            RPTAlbumImages.DataBind();

            RPTAlbumImagesmin.DataSource = dt;
            RPTAlbumImagesmin.DataBind();
        }
        GetVideoArabicLast();
    }

    private void GetVideoArabicLast()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(4) * FROM [dbo].[tbl_Video] With(NoLock) Where IsViewAr = @0 And IsDelete = @1 Order By OrderVideo Desc", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPTVideosLast.DataSource = dt;
            RPTVideosLast.DataBind();
        }
    }

    public void FGetDataSlide()
    {
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
            RPTSlide.DataSource = dt;
            RPTSlide.DataBind();
            IDSlide.Visible = true;
        }
        else
            IDSlide.Visible = false;
        FArnArticleByViewInBarView();
    }

    public Int64 FGetCountVisit()
    {
        return Convert.ToInt64(HFCount.Value) + 1;
    }

    public string XImgNewsLast = string.Empty;
    private void FArnArticleByViewInBarView()
    {
        ClassArticle CA = new ClassArticle();
        CA.Top = 5;
        CA.TypeArticle = 1;
        CA.IsView = true;
        CA.IsBarView = true;
        CA.DeleteArticle = false;
        DataTable dt = new DataTable();
        dt = CA.BArnArticleByViewInBarView();
        if (dt.Rows.Count > 0)
            if (dt.Rows.Count > 0)
        {
            IDNewsLast.HRef = "PageViewDetails.aspx?ID=" + dt.Rows[0]["IDUniqArticle"].ToString();
            lblTitleNewsLast.Text = dt.Rows[0]["TitleArticle"].ToString();
            lblDateNewsLast.Text = Convert.ToDateTime(dt.Rows[0]["DateAddArticle"]).ToString("ddd , dd-MMM-yyyy");
            XImgNewsLast = dt.Rows[0]["ImgArt"].ToString();
            RPTPar.DataSource = dt;
            RPTPar.DataBind();
        }
        GetAlbumArabic();
    }

    private void GetAlbumArabic()
    {
        ClassAlbum CA = new ClassAlbum();
        CA.IsViewAr = true;
        CA.IsDelete = false;
        DataTable dt = new DataTable();
        dt = CA.BGetAlbumByViewByAr();
        if (dt.Rows.Count > 0)
        {
            RPTAlbumArabic.DataSource = dt;
            RPTAlbumArabic.DataBind();
            IDAlbum.Visible = true;
        }
        else
            IDAlbum.Visible = false;
        GetVideoArabic();
    }

    public float FCountImg(float X)
    {
        float VaManage = 0;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT COUNT(1) As 'CountImg' FROM [dbo].[tbl_AlbumImg] With(NoLock) Where IDAlbum = @0 And IsDelete = @1", Convert.ToInt32(X), false);
        if (dt.Rows.Count > 0)
            VaManage = Convert.ToInt64(dt.Rows[0]["CountImg"]);
        return VaManage;
    }

    private void GetVideoArabic()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[tbl_Video] With(NoLock) Where IsViewAr = @0 And IsDelete = @1 Order By OrderVideo", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPTAlbumVideo.DataSource = dt;
            RPTAlbumVideo.DataBind();

            IDAlbumVideo.Visible = true;
        }
        else
            IDAlbumVideo.Visible = false;
        FGetPartner();
    }

    public void FGetPartner()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP (100) *  FROM [dbo].[tbl_Partner] With(NoLock) Where [IsDelete] = @0 order by [IDOrder]", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            RPTPartner.DataSource = dt;
            RPTPartner.DataBind();
            IDPartner.Visible = true;
        }
        else
            IDPartner.Visible = false;
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

    public void FGetStatistical()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) [IDItem],[HalatMostafeed] FROM [dbo].[Quaem] With(NoLock) Where ([IDItem] <> 65) And HalatMostafeed <> '' And IsDeleteHalatMostafeed = 0 Order by IDItem");
        if (dt.Rows.Count > 0)
        {
            RPTStatistical.DataSource = dt;
            RPTStatistical.DataBind();
            IDStatistical.Visible = true;
        }
        else
            IDStatistical.Visible = false;
    }

    public int FGetCount(int XID)
    {
        int XResult = 0;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT TOP 1000 Count([IDItem]) As 'CountTypeMostafeed' FROM [dbo].[RasAlEstemarah] With(NoLock) Where HalafAlMosTafeed = @0 And TypeMostafeed = @1 And IsDelete = @2", Convert.ToString(XID), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
            XResult = Convert.ToInt32(dt.Rows[0]["CountTypeMostafeed"]);
        else
            XResult = 0;
        return XResult;
    }

    public int FGetCuntAfradAlOsrah(int XID)
    {
        int XResult = 0;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Count(NumberMostafeed) As 'Count' FROM [dbo].[TarafEstemarah] With(NoLock) Inner Join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where HalafAlMosTafeed = @0 And TypeMostafeed = @1 And TarafEstemarah.IsDelete = @2", Convert.ToString(XID), "دائم", Convert.ToString(false));
        if (dt.Rows.Count > 0)
            XResult = Convert.ToInt32(dt.Rows[0]["Count"]) + FGetCount(XID);
        else
            XResult = 0;
        return XResult;
    }

    public string FGetMostafeedByHalafAlMosTafeed()
    {
        string XResult = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT Top(1000) RasAlEstemarah.NameMostafeed FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And (HalafAlMosTafeed = @1) And (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2) And AlQarabah = @3) or ((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @4) And AlQarabah = @5)) And (TarafEstemarah.IsDelete = @6)",
            "دائم", "27", ClassSetting.FGetAgeBoy(), "2", ClassSetting.FGetAgeGirl(), "1", Convert.ToString(false));
        if (dt.Rows.Count > 0)
            XResult = Convert.ToString(dt.Rows.Count);
        return XResult;
    }

    public string FGetCuntAfradAlOsrahByAitaam(int XID)
    {
        string XResult = "0";
        if (XID == 0)
            XResult = "0";
        else if (XID != 0)
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1000) Count(RasAlEstemarah.NumberMostafeed) As 'CuntAfradAlOsrah' FROM [dbo].[TarafEstemarah] With(NoLock) Inner join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TarafEstemarah.NumberMostafed Where (TypeMostafeed = @0) And (HalafAlMosTafeed = @1) And (((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @2) And AlQarabah = @3) or ((((CONVERT(int,CONVERT(char(8),GETDATE(),112))-CONVERT(char(8),TarafEstemarah.DateBrith,112))/10000) < @4) And AlQarabah = @5)) And (TarafEstemarah.IsDelete = @6)",
                "دائم", Convert.ToString(XID), ClassSetting.FGetAgeBoy(), "2", ClassSetting.FGetAgeGirl(), "1", Convert.ToString(false));
            if (dt.Rows.Count > 0)
                XResult = dt.Rows[0]["CuntAfradAlOsrah"].ToString();
            else
                XResult = "0";
        }
        return XResult;
    }

    protected void RPTStatistical_PreRender(object sender, EventArgs e)
    {
        try
        {
            int XCountAosra = 0; int XCountMostafeed = 0;
            foreach (RepeaterItem item in RPTStatistical.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lbl_CountAosra = (Label)item.FindControl("lblCountAosra");
                    Label lbl_CountMostafeed = (Label)item.FindControl("lblCountMostafeed");

                    if (lbl_CountAosra.Text == string.Empty || lbl_CountAosra.Text == "")
                    { lbl_CountAosra.Text = "0"; }
                    if (lbl_CountMostafeed.Text == string.Empty || lbl_CountMostafeed.Text == "")
                    { lbl_CountMostafeed.Text = "0"; }

                    XCountAosra += int.Parse(lbl_CountAosra.Text);
                    XCountMostafeed += int.Parse(lbl_CountMostafeed.Text);
                    
                }
            }
            HFCountAosra.Value = XCountAosra.ToString();
            HFCountMostafeed.Value += XCountMostafeed.ToString();
        }
        catch (Exception)
        {

        }
    }

}