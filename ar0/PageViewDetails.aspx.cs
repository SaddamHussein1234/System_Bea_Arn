using Library_CLS_Arn.ClassOutEntity;
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
    string XIDX = string.Empty, Name = string.Empty, XIDMenu = string.Empty, XPath;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            GetData();
    }

    private void GetData()
    {
        try
        {
            CA.IDUniqArticle = Convert.ToString(Request.QueryString["ID"]);
            CA.DeleteArticle = false;
            DataTable dt = new DataTable();
            dt = CA.BArnArticleByIDUniq();
            this.Page.Header.Title = ClassSetting.FGetNameSiteAR() + " | " + dt.Rows[0]["TitleArticle"].ToString();
            if (dt.Rows.Count > 0)
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
                
                XIDX = dt.Rows[0]["IDPartSection"].ToString();
                Name = dt.Rows[0]["TitleManageAr"].ToString();
                XIDMenu = dt.Rows[0]["IDMenu"].ToString();

                //GetTop5ByNew(Convert.ToInt32(dt.Rows[0]["IDMenu"]));
                //GetTop5ByView(Convert.ToInt32(dt.Rows[0]["IDMenu"]));

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
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }
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