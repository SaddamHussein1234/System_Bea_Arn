using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_PageViewDetails : System.Web.UI.Page
{
    ClassArticle CA = new ClassArticle();
    string XIDX, Name, XIDMenu, XPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HFLink.Value = Request.Url.Authority; HFQuery.Value = Request.Url.PathAndQuery;
            FGetSetting();
            if (Request.QueryString["ID"] != null)
                GetData();
            else
                Response.Redirect("Default.aspx");
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

    private void GetData()
    {
        try
        {
            CA.IDUniqArticle = Convert.ToString(Request.QueryString["ID"]);
            CA.DeleteArticle = false;
            DataTable dt = new DataTable();
            dt = CA.BArnArticleByIDUniq();
            if (dt.Rows.Count > 0)
            {
                HFTitle.Value = dt.Rows[0]["TitleArticle"].ToString();
                this.Page.Header.Title = dt.Rows[0]["TitleArticle"].ToString() + " - " + HFNameSite.Value;
                RPTTitle.DataSource = dt;
                RPTTitle.DataBind();

                lblName.Text = dt.Rows[0]["TitleManageAr"].ToString();
                if (dt.Rows[0]["ImgArt"].ToString() == "0")
                    ImgMain.Visible = false;
                else
                {
                    ImgMain.Src = "/" + dt.Rows[0]["ImgArt"].ToString();
                    ImgMain2.Src = ImgMain.Src;
                    IDMain.HRef = "/" + dt.Rows[0]["ImgArt"].ToString();
                }

                lblTitle.Text = dt.Rows[0]["TitleArticle"].ToString();
                lblDetails.Text = dt.Rows[0]["DetailsArticle"].ToString();

                CA.IDItem = Convert.ToInt64(dt.Rows[0]["IDItem"]);
                CA.CountViews = Convert.ToInt64(dt.Rows[0]["CountViews"]) + 1;
                CA.BArnUpdateViewsArticle();

                lblCountViews.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["CountViews"]) + 1).ToString();
                lblDateAdd.Text = Convert.ToDateTime(dt.Rows[0]["DateAddArticle"]).ToString("ddd , dd-MMM-yyyy");

                XIDX = dt.Rows[0]["IDPartSection"].ToString();
                Name = dt.Rows[0]["TitleManageAr"].ToString();
                XIDMenu = dt.Rows[0]["IDMenu"].ToString();

                RPTPath.DataSource = dt;
                RPTPath.DataBind();

                XPath = dt.Rows[0]["AttachFile"].ToString();
                if (XPath != "---")
                {
                    lblAttach.Text = "المرفقات";
                    RPTPath.Visible = true;
                }
                else
                {
                    lblAttach.Text = "لا يوجد مرفقات";
                    RPTPath.Visible = false;
                }
                FArnArticleByViewInBarView();
            }
            else
                Response.Redirect("Default.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void FArnArticleByViewInBarView()
    {
        ClassArticle CA = new ClassArticle();
        CA.Top = 5;
        CA.TypeArticle = 1;
        CA.IsView = true;
        CA.IsLastNwes = true;
        CA.DeleteArticle = false;
        DataTable dt = new DataTable();
        dt = CA.BArnArticleByViewInBarView();
        if (dt.Rows.Count > 0)
        {
            RPTPar.DataSource = dt;
            RPTPar.DataBind();
        }
    }

    protected void blnAttach_Click(object sender, EventArgs e)
    {
        string filename = "/" + Convert.ToString((((Button)sender).CommandArgument)).ToString();
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