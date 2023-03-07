using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassOutEntity;

public partial class ar_PageAlbumGallery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
                GetImageAlbumByViewByAr();
        }
        catch (Exception)
        {
            Response.Redirect("PageAlbum.aspx");
        }
    }

    private void GetImageAlbumByViewByAr()
    {
        ClassAlbum CA = new ClassAlbum();
        CA.IDItem = Convert.ToInt32(Request.QueryString["ID"]);
        CA.IsViewAr = true;
        CA.IsDelete = false;
        DataTable dt = new DataTable();
        dt = CA.BArnImageAlbumByViewByAr();
        this.Page.Header.Title = ClassSetting.FGetNameSiteAR() + " | " + " صور " + dt.Rows[0]["TitleAlbumAr"].ToString();
        if (dt.Rows.Count > 0)
        {
            lblNameAlbum.Text = "صور ألبوم " + dt.Rows[0]["TitleAlbumAr"].ToString();
            RPTAlbumImg.DataSource = dt;
            RPTAlbumImg.DataBind();
            tab1.Visible = true;
            PNLNull.Visible = false;
            lblCount.Text = dt.Rows.Count.ToString();
        }
        else
        {
            tab1.Visible = false;
            PNLNull.Visible = true;
        }
    }

}