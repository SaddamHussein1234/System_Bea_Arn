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

public partial class Cpanel_CPanelManageSite_PageAlbumAdd : System.Web.UI.Page
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
            bool A16;
            A16 = Convert.ToBoolean(dtViewProfil.Rows[0]["A16"]);
            if (A16 == false)
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
            txtAR.Focus();
            FGetLastRecord();
        }
    }

    private void FGetLastRecord()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Album] With(NoLock) Where IsDelete = @0 Order by IsOrder Desc", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            txtOrder.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["IsOrder"]) + 1);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //try
        //{
            FChackName();
        //}
        //catch
        //{
        //    lbmsg.Text = "خطأ غير متوقع حاول لاحقاً";
        //    lbmsg.ForeColor = Color.Red;
        //}
    }

    private void FChackName()
    {
        DataTable dtUser = new DataTable();
        dtUser = ClassDataAccess.GetData("Select * from tbl_Album Where TitleAlbumAr =@0 And IsDelete = @1", txtAR.Text.Trim(), Convert.ToString(false));
        if (dtUser.Rows.Count > 0)
        {
            lbmsg.Text = "قمت بإضافة الالبوم سابقاً";
            lbmsg.ForeColor = Color.Red;
        }
        else
        {
            FChackImgArticleF();
        }
    }

    private void FChackImgArticleF()
    {
        if (FUImgAlbum.HasFile)
        {
            string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
            string ext = Path.GetExtension(FUImgAlbum.PostedFile.FileName);
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
                FUpimgArticle(FUImgAlbum);
            }
        }
        else
        {
            FUpimgArticle(FUImgAlbum);
        }
    }

    string ImgArt;
    protected void FUpimgArticle(FileUpload upl)
    {
        if (upl.HasFile)
        {
            // ReSize Img
            Stream strm = upl.PostedFile.InputStream;
            using (var image = System.Drawing.Image.FromStream(strm))
            {
                int newWidth = 300; // New Width of Image in Pixel
                int newHeight = 300; // New Height of Image in Pixel
                var thumbImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imgRectangle);
                string XRandom = Convert.ToString(ClassDataAccess.RandomGenerator());
                string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/ImgAlbum/"), upl.FileName.Remove(3) + XRandom + ".png");
                thumbImg.Save(theFileName, image.RawFormat);
                ImgArt = "ImgSystem/ImgAlbum/" + upl.FileName.Remove(3) + XRandom + ".png";
                FArnAlbumAdd();
            }
        }
        else
        {
            ImgArt = "Img/Logo2.png";
            FArnAlbumAdd();
        }
    }

    private void FArnAlbumAdd()
    {
        GetCookie();
        string IDUniz;
        IDUniz = Convert.ToString(Guid.NewGuid());
        ClassAlbum CA = new ClassAlbum();
        CA.RandomUniq = Convert.ToString(IDUniz);
        CA.TitleAlbumAr = txtAR.Text.Trim();
        CA.TitleAlbumTr = txtTR.Text.Trim();
        CA.TitleAlbumEn = txtEN.Text.Trim();
        CA.IsOrder = Convert.ToInt32(txtOrder.Text.Trim());
        CA.IsViewAr = Convert.ToBoolean(CBViewAR.Checked);
        CA.IsViewTr = Convert.ToBoolean(CBViewTR.Checked);
        CA.IsViewEn = Convert.ToBoolean(CBViewEN.Checked);
        CA.imgAlbum = ImgArt;
        CA.DetailsAR = txtDetailsAR.Text.Trim();
        CA.DetailsTR = txtDetailsTR.Text.Trim();
        CA.DetailsEN = txtDetailsEN.Text.Trim();
        CA.IsDelete = false;
        CA.DateAddAlbum = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd");
        CA.IDAdmin = Convert.ToInt32(IDUser);
        CA.BArnAlbumAdd();
        lbmsg.Text = "تم الإضافة بنجاح ";
        lbmsg.ForeColor = Color.MediumAquamarine;

        Response.Redirect("PageAlbumImgAdd.aspx?ID=" + IDUniz + "&Name" + txtEN.Text.Trim());
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAlbum.aspx");
    }

}