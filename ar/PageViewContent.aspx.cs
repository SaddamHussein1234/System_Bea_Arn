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

public partial class ar_PageViewContent : System.Web.UI.Page
{
    string XIDX, Name, XIDMenu, XPath;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HFLink.Value = Request.Url.Authority; HFQuery.Value = Request.Url.PathAndQuery;
            FGetSetting();
            if (Request.QueryString["ID"] != null)
                FArnArticleByViewInMenu();
            else
                Response.Redirect("/ar/");
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

    private void FArnArticleByViewInMenu()
    {
        try
        {
            ClassArticle CA = new ClassArticle();
            CA.Top = 100;
            CA.IDMenu = Convert.ToInt32(Request.QueryString["ID"]);
            CA.TypeArticle = 1;
            CA.IsView = true;
            CA.DeleteArticle = false;
            DataTable dt = new DataTable();
            dt = CA.BArnArticleByViewInMenu();
            this.Page.Header.Title = dt.Rows[0]["TitleManageAr"].ToString() + " - " + HFNameSite.Value;
            HFTitle.Value = dt.Rows[0]["TitleManageAr"].ToString();
            if (dt.Rows.Count > 0)
            {
                lblName.Text = dt.Rows[0]["TitleManageAr"].ToString();
                if (dt.Rows.Count == 1)
                {
                    RPTTitle.DataSource = dt;
                    RPTTitle.DataBind();
                    RPTTitle.Visible = true; IDMulti.Visible = false;
                    lblName.Text = dt.Rows[0]["TitleManageAr"].ToString();
                    if (dt.Rows[0]["ImgArt"].ToString() == "0")
                        ImgMain.Visible = false;
                    else
                    {
                        ImgMain.Src = "/" + dt.Rows[0]["ImgArt"].ToString().Replace("FileSystem/ImgArticle/", "FileSystem/ImgArticle/400_267/");
                        ImgMain2.Src = ImgMain.Src;
                        IDMain.HRef = ImgMain.Src;
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

                    pnlOneArticle.Visible = true;
                    RPTViewContentNetwork.Visible = false;
                    PNLNullNetwork.Visible = false;
                    RPTViewContentList.Visible = false;
                    PNLNullList.Visible = false;
                }
                else if (dt.Rows.Count > 1)
                {
                    RPTTitle.Visible = false; IDMulti.Visible = true;
                    pnlOneArticle.Visible = false;
                    if (dt.Rows[0]["_Type_Style_"].ToString() == "شبكي")
                    {
                        RPTViewContentNetwork.DataSource = dt;
                        RPTViewContentNetwork.DataBind();
                        RPTViewContentNetwork.Visible = true;
                        PNLNullNetwork.Visible = false;

                        RPTViewContentList.Visible = false;
                        PNLNullList.Visible = false;
                    }
                    else if (dt.Rows[0]["_Type_Style_"].ToString() == "قائمة")
                    {
                        RPTViewContentList.DataSource = dt;
                        RPTViewContentList.DataBind();
                        RPTViewContentList.Visible = true;
                        PNLNullList.Visible = false;

                        RPTViewContentNetwork.Visible = false;
                        PNLNullNetwork.Visible = false;
                    }
                }
            }
            else
                Response.Redirect("PageSoon.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageSoon.aspx");
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

    public string Test(string _Path)
    {
        long length = 0;
        DirectoryInfo objDir = new DirectoryInfo(Server.MapPath("../" + _Path));
        FileInfo[] objFI = objDir.GetFiles();   // Get all the files in the folder.

        if (objFI.Length > 0)
        {      // Just a counter.
            foreach (FileInfo file in objFI)
            {
                System.Diagnostics.Debug.WriteLine(objFI[length].Length / (double)1024 + " KB");
                length += 1;
            }
        }



        return length.ToString();
    }

    public string FCheckNullFile(string _Path)
    {
        string Xresult = string.Empty;
        if (_Path == "---")
            Xresult = "display:none;";
        return Xresult;
    }

    public string FCheckNotNullFile(string _Path)
    {
        string Xresult = string.Empty;
        if (_Path != "---")
            Xresult = "display:none;";
        return Xresult;
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