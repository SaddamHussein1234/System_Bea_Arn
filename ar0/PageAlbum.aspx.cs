using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_PageAlbum : System.Web.UI.Page
{
    string XIDX;
    DataSet dset;
    PagedDataSource adsource;
    int pos;
    string x;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                this.ViewState["vs"] = 0;
                this.Page.Header.Title = ClassSetting.FGetNameSiteAR() + " | " + "البوم الصور";
            }
            catch (Exception)
            {
                Response.Redirect("Default.aspx");
            }
        }
        pos = Convert.ToInt32(this.ViewState["vs"]);
        
        GetAlbumArabic();
    }

    private void GetAlbumArabic()
    {
        try
        {
            ClassAlbum CA = new ClassAlbum();
            CA.IsViewAr = true;
            CA.IsDelete = false;
            DataTable dt = new DataTable();
            dt = CA.BGetAlbumByViewByAr();
            if (dt.Rows.Count > 0)
            {
                dset = new DataSet();
                adsource = new PagedDataSource();
                dset.Tables.Add(dt);
                adsource.DataSource = dset.Tables[0].DefaultView;
                adsource.PageSize = 6;
                adsource.AllowPaging = true;
                adsource.CurrentPageIndex = pos;
                btnfirst.Enabled = !adsource.IsFirstPage;
                btnfirst.ForeColor = System.Drawing.Color.White;
                btnprevious.Enabled = !adsource.IsFirstPage;
                btnprevious.ForeColor = System.Drawing.Color.White;
                btnlast.Enabled = !adsource.IsLastPage;
                btnlast.ForeColor = System.Drawing.Color.White;
                btnnext.Enabled = !adsource.IsLastPage;
                btnnext.ForeColor = System.Drawing.Color.White;
                RPTAlbumArabic.DataSource = adsource;
                RPTAlbumArabic.DataBind();
                pnlData.Visible = true;
                pnlNull.Visible = false;
            }
            else
            {
                pnlData.Visible = false;
                pnlNull.Visible = true;
            }
        }
        catch (Exception)
        {
            
        }
    }

    public float FCountImg(float X)
    {
        float VaManage = 0;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT COUNT(*) As 'CountImg' FROM [dbo].[tbl_AlbumImg] Where IDAlbum = @0 And IsDelete = @1", Convert.ToString(X), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            VaManage = Convert.ToInt64(dt.Rows[0]["CountImg"]);
        }
        return VaManage;
    }
    
    protected void btnfirst_Click(object sender, EventArgs e)
    {
        pos = 0;
        GetAlbumArabic();
    }

    protected void btnprevious_Click(object sender, EventArgs e)
    {
        pos = Convert.ToInt32((this.ViewState["vs"]));
        pos -= 1;
        this.ViewState["vs"] = pos;
        GetAlbumArabic();
    }

    protected void btnnext_Click(object sender, EventArgs e)
    {
        pos = Convert.ToInt32((this.ViewState["vs"]));
        pos += 1;
        this.ViewState["vs"] = pos;
        GetAlbumArabic();
    }

    protected void btnlast_Click(object sender, EventArgs e)
    {
        pos = adsource.PageCount - 1;
        GetAlbumArabic();
    }

}