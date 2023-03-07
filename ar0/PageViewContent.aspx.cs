using Library_CLS_Arn.ClassOutEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_PageViewContent : System.Web.UI.Page
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
            }
            catch (Exception)
            {
                Response.Redirect("Default.aspx");
            }
        }
        pos = Convert.ToInt32(this.ViewState["vs"]);
        FArnArticleByViewInMenu();
    }

    string XPath;
    private void FArnArticleByViewInMenu()
    {
        try
        {
            ClassArticle CA = new ClassArticle();
            CA.Top = 1000;
            CA.IDMenu = Convert.ToInt32(Request.QueryString["ID"]);
            CA.TypeArticle = 1;
            CA.IsView = true;
            CA.DeleteArticle = false;
            DataTable dt = new DataTable();
            dt = CA.BArnArticleByViewInMenu();
            this.Page.Header.Title = ClassSetting.FGetNameSiteAR() + " | " + dt.Rows[0]["TitleManageAr"].ToString();
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    if (dt.Rows[0]["ImgArt"].ToString() == "0")
                        MainContent_PageContent_img_NewsImage.Visible = false;
                    else
                        MainContent_PageContent_img_NewsImage.Src = "../" + dt.Rows[0]["ImgArt"].ToString();
                    lblTitle.Text = dt.Rows[0]["TitleArticle"].ToString();
                    lblDetails.Text = dt.Rows[0]["DetailsArticle"].ToString();
                    CA.IDItem = Convert.ToInt64(dt.Rows[0]["IDItem"]);
                    CA.CountViews = Convert.ToInt64(dt.Rows[0]["CountViews"]) + 1;
                    CA.BArnUpdateViewsArticle();
                    lblCountViews.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["CountViews"]) + 1).ToString();
                    lblDateAdd.Text = Convert.ToDateTime(dt.Rows[0]["DateAddArticle"]).ToString("dd/MM/yyyy");

                    RPTPath.DataSource = dt;
                    RPTPath.DataBind();

                    XPath = dt.Rows[0]["AttachFile"].ToString();
                    if (XPath != "---")
                    {
                        lblAttach.Text = "المرفقات";
                        RPTPath.Visible = true;
                        if (XPath.Contains(".pdf"))
                            IDViewPDF.InnerHtml = "<br /><iframe frameborder='0' src='https://docs.google.com/viewer?url=https://berarn.org/" + XPath + "&amp;embedded=true' style='width:90%; height:500px;'></iframe>";
                    }
                    else
                    {
                        lblAttach.Text = "لا يوجد مرفقات";
                        RPTPath.Visible = false;
                    }

                    pnlOneArticle.Visible = true;
                    pnlOtherArticle.Visible = false;
                }
                else if (dt.Rows.Count > 1)
                {
                    XIDX = dt.Rows[0]["IDPartSection"].ToString();
                    lblMenu.Text = dt.Rows[0]["TitleManageAr"].ToString();

                    dset = new DataSet();
                    adsource = new PagedDataSource();

                    dset.Tables.Add(dt);

                    adsource.DataSource = dset.Tables[0].DefaultView;
                    adsource.PageSize = 50;
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
                    RPTViewContent.DataSource = adsource;
                    RPTViewContent.DataBind();

                    pnlOneArticle.Visible = false;
                    pnlOtherArticle.Visible = true;
                }
                PNLData.Visible = true;
                PNLNull.Visible = false;
            }
            else
            {
                PNLData.Visible = false;
                PNLNull.Visible = true;
            }
        }
        catch (Exception)
        {
            Response.Redirect("PageSoon.aspx");
        }
    }
    
    protected void btnfirst_Click(object sender, EventArgs e)
    {
        pos = 0;
        FArnArticleByViewInMenu();
    }

    protected void btnprevious_Click(object sender, EventArgs e)
    {
        pos = Convert.ToInt32((this.ViewState["vs"]));
        pos -= 1;
        this.ViewState["vs"] = pos;
        FArnArticleByViewInMenu();
    }

    protected void btnnext_Click(object sender, EventArgs e)
    {
        pos = Convert.ToInt32((this.ViewState["vs"]));
        pos += 1;
        this.ViewState["vs"] = pos;
        FArnArticleByViewInMenu();
    }

    protected void btnlast_Click(object sender, EventArgs e)
    {
        pos = adsource.PageCount - 1;
        FArnArticleByViewInMenu();
    }

    protected void blnAttach_Click(object sender, EventArgs e)
    {
        string filename = "../" + Convert.ToString((((Button)sender).CommandArgument)).ToString();
        if (filename != string.Empty)
        {
            string path = Server.MapPath(filename);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", ("attachment; filename=" + file.Name));
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            else
            {
                //Response.Write("This file does not exist.");
            }
        }
    }

}