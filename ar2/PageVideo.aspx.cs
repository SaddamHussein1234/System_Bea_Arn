using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_PageVideo : System.Web.UI.Page
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
            this.Page.Header.Title = ClassSetting.FGetNameSiteAR() + " | " + "مكتبة الفيديو";
            this.ViewState["vs"] = 0;
        }
        pos = Convert.ToInt32(this.ViewState["vs"]);
        GetVideoArabic();
    }

    private void GetVideoArabic()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Video] Where IsViewAr = @0 And IsDelete = @1 Order By OrderVideo", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            //RPTVideoArabic.DataSource = dt;
            //RPTVideoArabic.DataBind();

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
            RPTVideoArabic.DataSource = adsource;
            RPTVideoArabic.DataBind();

            RPTVideoArabic.Visible = true;
            PNLNull.Visible = false;
        }
        else
        {
            RPTVideoArabic.Visible = false;
            PNLNull.Visible = true;
        }
    }


    protected void btnfirst_Click(object sender, EventArgs e)
    {
        pos = 0;
        GetVideoArabic();
    }

    protected void btnprevious_Click(object sender, EventArgs e)
    {
        pos = Convert.ToInt32((this.ViewState["vs"]));
        pos -= 1;
        this.ViewState["vs"] = pos;
        GetVideoArabic();
    }

    protected void btnnext_Click(object sender, EventArgs e)
    {
        pos = Convert.ToInt32((this.ViewState["vs"]));
        pos += 1;
        this.ViewState["vs"] = pos;
        GetVideoArabic();
    }

    protected void btnlast_Click(object sender, EventArgs e)
    {
        pos = adsource.PageCount - 1;
        GetVideoArabic();
    }

}