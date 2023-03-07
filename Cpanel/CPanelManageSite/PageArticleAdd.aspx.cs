using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelManageSite_PageArticleAdd : System.Web.UI.Page
{
    string IDUser, IDUniq;
    private void GetCookie()
    {
        try
        {
            HttpCookie Cooke;  // رقم المستخدم
            Cooke = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
            IDUser = ClassSaddam.UnprotectPassword(Cooke["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
        }
        catch
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        ClassAdmin_Arn CAA = new ClassAdmin_Arn();
        CAA._IDUniq = IDUniq;
        CAA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CAA.BArnAdminGetByIDUniq();
        if (dtViewProfil.Rows.Count > 0)
        {
            bool A12;
            A12 = Convert.ToBoolean(dtViewProfil.Rows[0]["A12"]);
            if (A12 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetMenu();
            txtDateArticle.Text = ClassDataAccess.GetCurrentTime().ToString("dd-MM-yyyy");
        }
    }

    private void FGetMenu()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) [IDItem],[TitleManageAr] As 'Title',[IDOrder],[IsDelete] FROM [dbo].[tbl_MenuSite] Where IsDelete = @0 Order By IDOrder", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLMenu.Items.Clear();
            DLMenu.Items.Add("");
            DLMenu.AppendDataBoundItems = true;
            DLMenu.DataValueField = "IDItem";
            DLMenu.DataTextField = "Title";
            DLMenu.DataSource = dt;
            DLMenu.DataBind();
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageArticleAdd.aspx");
    }
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            FChackName();
        }
        catch
        {
            lbmsg.Text = "خطأ غير متوقع حاول لاحقاً";
            lbmsg.ForeColor = Color.Red;
        }
    }

    private void FChackName()
    {
        DataTable dtUser = new DataTable();
        dtUser = ClassDataAccess.GetData("Select Top(1) * from tbl_Article Where IDMenu =@0 And TypeArticle = @1 And TitleArticle = @2 And DeleteArticle = @3", DLMenu.SelectedValue, DLType.SelectedValue, txtTitle.Text.Trim(), Convert.ToString(false));
        if (dtUser.Rows.Count > 0)
        {
            lbmsg.Text = "قمت بإضافة المقالة سابقاً";
            lbmsg.ForeColor = Color.Red;
        }
        else
        {
            FChackImgArticleF();
        }
    }

    private void FChackImgArticleF()
    {
        if (FUArticle.HasFile)
        {
            string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
            string ext = Path.GetExtension(FUArticle.PostedFile.FileName);
            bool isValidFile = false;
            for (int i = 0; i <= validFileTypes.Length - 1; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            if (!isValidFile)
            {
                lbmsg.ForeColor = Color.Red;
                lbmsg.Text = "Image Allow " + string.Join(",", validFileTypes);
                return;
            }
            else
                FUpimgArticle(FUArticle);
        }
        else
            FUpimgArticle(FUArticle);
    }

    string ImgArt, FileArt;
    protected void FUpimgArticle(FileUpload upl)
    {
        if (upl.HasFile)
        {
            // ReSize Img
            Stream strm = upl.PostedFile.InputStream;
            using (var image = System.Drawing.Image.FromStream(strm))
            {
                int newWidth = 1772; // New Width of Image in Pixel
                int newHeight = 400; // New Height of Image in Pixel
                var thumbImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imgRectangle);
                string XRandom = Convert.ToString(ClassDataAccess.RandomGenerator());
                string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/ImgArticle/"), upl.FileName.Remove(3) + XRandom + ".png");
                thumbImg.Save(theFileName, image.RawFormat);
                ImgArt = "ImgSystem/ImgArticle/" + upl.FileName.Remove(3) + XRandom + ".png";
                FChackFile();
            }
        }
        else
        {
            ImgArt = "ImgSystem/ImgArticle/Log913700880.png";
            FChackFile();
        }
    }

    private void FChackFile()
    {
        if (FUImgTeacher.HasFile)//xlsx
        {
            string[] validFileTypes = { "rar", "RAR", "zip", "ZIP", "XLS", "xls", "XLSX", "xlsx",
                "ACCDB", "accdb", "pdf", "PDF", "DOC", "DOCX", "doc", "docx",
                "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
            string ext = Path.GetExtension(FUImgTeacher.PostedFile.FileName);
            bool isValidFile = false;
            for (int i = 0; i <= validFileTypes.Length - 1; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            if (!isValidFile)
            {
                lbmsg.ForeColor = Color.Red;
                lbmsg.Text = "Allow " + string.Join(",", validFileTypes);
                return;
            }
            else
                FUpimg(FUImgTeacher);
        }
        else
            FUpimg(FUImgTeacher);
    }

    protected void FUpimg(FileUpload upl)
    {
        if (upl.HasFile)
        {
            string XRandom = Convert.ToString(ClassDataAccess.RandomGenerator());
            string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/FileAttach/"), upl.FileName.Remove(3) + XRandom + upl.FileName);
            upl.SaveAs(theFileName);
            FileArt = "ImgSystem/FileAttach/" + upl.FileName.Remove(3) + XRandom + upl.FileName;
            FArticleAdd();
        }
        else
        {
            FileArt = "---";
            FArticleAdd();
        }
    }

    string IDUniz;
    private void FArticleAdd()
    {
        GetCookie();
        IDUniz = Convert.ToString(Guid.NewGuid());
        ClassArticle CA = new ClassArticle();
        CA.IDUniqArticle = Convert.ToString(IDUniz);
        CA.IDMenu = Convert.ToInt32(DLMenu.SelectedValue);
        CA.TypeArticle = Convert.ToInt32(DLType.SelectedValue);
        CA.TopWord = "0"; // txttopword.Text.Trim();
        CA.TitleArticle = txtTitle.Text.Trim();
        CA.ImgArt = ImgArt;
        CA.IsView = Convert.ToBoolean(CBView.Checked);
        CA.IsSlide = Convert.ToBoolean(CBSlide.Checked);
        CA.IsLastNwes = Convert.ToBoolean(CBLastNews.Checked);
        CA.AttachFile = FileArt;
        CA.DetailsArticle = txtDetails.Text.Trim();
        CA.DateAddArticle = Convert.ToDateTime(txtDateArticle.Text.Trim()).ToString("yyyy/MM/dd");
        CA.CountViews = 0;
        CA.DeleteArticle = false;
        CA.IDAdmin = Convert.ToInt32(IDUser);
        CA.IsBarView = Convert.ToBoolean(CBBarView.Checked);
        CA.IsSite = Convert.ToBoolean(CBSite.Checked);
        CA.BArnArticlAdd();
        lbmsg.Text = "تم الإضافة بنجاح ";
        lbmsg.ForeColor = Color.MediumAquamarine;

        if (CBTwitter.Checked == true)
        {
            //FChackImgF();
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageArticle.aspx");
    }

}