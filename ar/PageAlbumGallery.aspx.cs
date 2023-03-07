using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;

public partial class ar_PageAlbumGallery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                HFLink.Value = Request.Url.Authority; HFQuery.Value = Request.Url.PathAndQuery;
                FGetSetting();
                GetImageAlbumByViewByAr();
                GetImageAlbumByViewByArLast();
            }
        }
        catch (Exception)
        {
            Response.Redirect("PageAlbum.aspx");
        }
    }

    private void FGetSetting()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) * from SettingTable With(NoLock) Where IDSetting = @0", Convert.ToString(964654));
        if (dt.Rows.Count > 0)
        {
            HFNameSite.Value = dt.Rows[0]["NameSiteAR"].ToString();
            HFDescrption.Value = dt.Rows[0]["DescriptoinSiteAR"].ToString();
            HFKeyWord.Value = dt.Rows[0]["KeyWordAR"].ToString();
            HFImage.Value = dt.Rows[0]["ImgSystem"].ToString();
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
        if (dt.Rows.Count > 0)
        {
            lblNameAlbum.Text = dt.Rows[0]["TitleAlbumAr"].ToString();
            HFTitle.Value = lblNameAlbum.Text;
            this.Page.Header.Title = lblNameAlbum.Text + " - " + HFNameSite.Value;
            ImgAlbum.Src = "/" + dt.Rows[0]["imgAlbum"].ToString();
            ImgAlbum.Alt = lblNameAlbum.Text;
            IDAlbum.HRef = ImgAlbum.Src;
            RPTAlbumImg.DataSource = dt;
            RPTAlbumImg.DataBind();

            RPTAlbumImg.Visible = true;
            PNLNull.Visible = false;
            lblCount.Text = dt.Rows.Count.ToString();
        }
        else
        {
            RPTAlbumImg.Visible = false;
            PNLNull.Visible = true;
        }
    }

    private void GetImageAlbumByViewByArLast()
    {
        ClassAlbum CA = new ClassAlbum();
        CA.IsViewAr = true;
        CA.IsDelete = false;
        DataTable dt = new DataTable();
        dt = CA.BGetAlbumByViewByAr();
        if (dt.Rows.Count > 0)
        {
            IDOtherAlbum.Visible = true;
            RPTOtherAlbum.DataSource = dt;
            RPTOtherAlbum.DataBind();
        }
        else
            IDOtherAlbum.Visible = false;
    }

}