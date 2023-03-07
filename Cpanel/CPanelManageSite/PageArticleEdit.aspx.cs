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

public partial class Cpanel_CPanelManageSite_PageArticleEdit : System.Web.UI.Page
{
    ClassArticle CA = new ClassArticle();
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
                lbmsg.Text = "لا تمتلك صلاحية التعديل ";
                lbmsg.ForeColor = System.Drawing.Color.Red;
                DLType.Enabled = false;
                DLMenu.Enabled = false;
                txtTitle.Enabled = false;
                //txttopword.Enabled = false;
                txtDetails.Enabled = false;
                FUArticle.Enabled = false;
                FUImgTeacher.Enabled = false;
                CBLastNews.Disabled = true;
                CBSlide.Disabled = true;
                CBView.Disabled = true;
                CBBarView.Disabled = true;
                btnEdit.Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetDetails();
            FGetMenu();
        }
    }

    private void FGetMenu()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT [IDItem],[TitleManageAr] As 'Title',[IDOrder],[IsDelete] FROM [dbo].[tbl_MenuSite] Where IsDelete = @0 Order By IDOrder", Convert.ToString(false));
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

    string ImgArt, FileArt;
    private void FGetDetails()
    {
        try
        {
            CA.IDUniqArticle = Convert.ToString(Request.QueryString["ID"]);
            DataTable dt = new DataTable();
            dt = CA.BArnArticleByIDEdit();
            if (dt.Rows.Count > 0)
            {
                DLMenu.SelectedValue = dt.Rows[0]["ManageID"].ToString();
                DLType.SelectedValue = dt.Rows[0]["TypeArticle"].ToString();
                //txttopword.Text = dt.Rows[0]["TopWord"].ToString();
                txtTitle.Text = dt.Rows[0]["TitleArticle"].ToString();
                ImgArt = dt.Rows[0]["ImgArt"].ToString();
                Session["Img"] = ImgArt;
                ImgArticle.ImageUrl = "/" + ImgArt;
                Session["OldNameArticle"] = txtTitle.Text.Trim();
                CBView.Checked = Convert.ToBoolean(dt.Rows[0]["IsView"]);
                CBSlide.Checked = Convert.ToBoolean(dt.Rows[0]["IsSlide"]);
                CBLastNews.Checked = Convert.ToBoolean(dt.Rows[0]["IsLastNwes"]);
                FileArt = dt.Rows[0]["AttachFile"].ToString();
                Session["File"] = FileArt;
                txtDetails.Text = dt.Rows[0]["DetailsArticle"].ToString();
                CBBarView.Checked = Convert.ToBoolean(dt.Rows[0]["IsBarView"]);
                CBSite.Checked = Convert.ToBoolean(dt.Rows[0]["IsSite"]);
            }
            else
                Response.Redirect("PageArticle.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageArticle.aspx");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
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
        if (txtTitle.Text.Trim() != Session["OldNameArticle"].ToString())
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("Select Top(1) * from tbl_Article Where IDMenu =@0 And TypeArticle = @1 And TitleArticle = @2 And DeleteArticle = @3", DLMenu.SelectedValue, DLType.SelectedValue, txtTitle.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                lbmsg.Text = "تم إضافة المقالة سابقاً";
                lbmsg.ForeColor = Color.Red;
            }
            else
            {
                Session["OldNameArticle"] = txtTitle.Text.Trim();
                FChackImgArticleF();
            }
        }
        else
        {
            FChackImgArticleF();
        }

        FChackImgArticleF();
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
            {
                FUpimgArticle(FUArticle);
            }
        }
        else
        {
            FUpimgArticle(FUArticle);
        }
    }

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
            ImgArt = Session["Img"].ToString();
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
            {
                FUpimg(FUImgTeacher);
            }
        }
        else
        {
            FUpimg(FUImgTeacher);
        }
    }

    protected void FUpimg(FileUpload upl)
    {
        if (upl.HasFile)
        {
            string XRandom = Convert.ToString(ClassDataAccess.RandomGenerator());
            string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/FileAttach/"), upl.FileName.Remove(3) + XRandom + upl.FileName);
            upl.SaveAs(theFileName);
            FileArt = "ImgSystem/FileAttach/" + upl.FileName.Remove(3) + XRandom + upl.FileName;
            FArticleEdit();
        }
        else
        {
            FileArt = Session["File"].ToString();
            FArticleEdit();
        }
    }

    private void FArticleEdit()
    {
        ClassArticle CA = new ClassArticle()
        {
            IDUniqArticle = Convert.ToString(Request.QueryString["ID"]),
            IDMenu = Convert.ToInt32(DLMenu.SelectedValue),
            TypeArticle = Convert.ToInt32(DLType.SelectedValue),
            TopWord = "0",
            TitleArticle = Session["OldNameArticle"].ToString(),
            ImgArt = ImgArt,
            IsView = Convert.ToBoolean(CBView.Checked),
            IsSlide = Convert.ToBoolean(CBSlide.Checked),
            IsLastNwes = Convert.ToBoolean(CBLastNews.Checked),
            AttachFile = FileArt,
            DetailsArticle = txtDetails.Text.Trim(),
            IsBarView = Convert.ToBoolean(CBBarView.Checked),
            IsSite = Convert.ToBoolean(CBSite.Checked)
        };
        CA.BArnArticleEdit();
        lbmsg.Text = "تم التعديل بنجاح ";
        lbmsg.ForeColor = Color.MediumAquamarine;
        FGetDetails();
        ImgArticle.ImageUrl = "/" + ImgArt;

    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session.Remove("OldNameArticle");
        Session.Remove("Img");
        Session.Remove("File");
        Response.Redirect("PageArticle.aspx");
    }

}