using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageVisitReportEdit : System.Web.UI.Page
{
    string XID;
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
            Response.Redirect("PageNotAccess.aspx");
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
            bool A95;
            A95 = Convert.ToBoolean(dtViewProfil.Rows[0]["A95"]);
            if (A95 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlNullData.Visible = true;
            FGetAlBaheth();
            FGetArnReportAlZyaratByID();
            FGetProductShopBtCategoryShop();
            FArnReportAlZyaratElectricalAppliancesGetByMostafeed();
            FAPI_ReportAlZyaratMedicalEquipments_Manage();
            txtDate.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy-MM-dd");
            txtDateMedical.Text = txtDate.Text;
        }
    }

    private void FGetProductShopBtCategoryShop()
    {
        DataTable dt = new DataTable();
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "106", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLDivice.Items.Clear();
            DLDivice.Items.Add("");
            DLDivice.AppendDataBoundItems = true;
            DLDivice.DataValueField = "ProductID";
            DLDivice.DataTextField = "ProductName";
            DLDivice.DataSource = dt;
            DLDivice.DataBind();
        }

        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[ProductShop] With(NoLock) Where IDCategoryShop = @0 And IsActive = @1 And IsDelete = @2 Order by IDNumberProduct", "107", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLDiviceMedical.Items.Clear();
            DLDiviceMedical.Items.Add("");
            DLDiviceMedical.AppendDataBoundItems = true;
            DLDiviceMedical.DataValueField = "ProductID";
            DLDiviceMedical.DataTextField = "ProductName";
            DLDiviceMedical.DataSource = dt;
            DLDiviceMedical.DataBind();
        }
    }

    private void FGetArnReportAlZyaratByID()
    {
        ClassReportAlZyarat CRZ = new ClassReportAlZyarat();
        CRZ._NumberReport = Convert.ToInt32(Request.QueryString["ID"]);
        CRZ._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CRZ.BArnReportAlZyaratByID();
        if (dt.Rows.Count > 0)
        {
            txtNumberMostafeed.Text = dt.Rows[0]["NumberMostafeed"].ToString();
            txtNumberReport.Text = dt.Rows[0]["NumberReport"].ToString();
            txtDateReport.Text = Convert.ToDateTime(dt.Rows[0]["DateReport"]).ToString("yyyy-MM-dd");

            
            lblName.Text = dt.Rows[0]["NameMostafeed"].ToString();
            lblAlQariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
            lblGender.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
            lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
            lblNumberSigal.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            lblHalatAlmostafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
            lblDateBrithDay.Text = ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["dateBrith"])) + "هـ" + " - " + Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") + "مـ";
            if (dt.Rows[0]["dateBrith"].ToString() != string.Empty)
                lblAge.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            else
                lblAge.Text = dt.Rows[0]["Age"].ToString();

            CBEgathy.Checked = Convert.ToBoolean(dt.Rows[0]["Egathy"]);
            txtEgathy.Text = dt.Rows[0]["NumberEgathy"].ToString();

            CBOther.Checked = Convert.ToBoolean(dt.Rows[0]["_Other"]);
            txtOther.Text = dt.Rows[0]["WathOther"].ToString();

            CBBenaaHome.Checked = Convert.ToBoolean(dt.Rows[0]["BenaManzil"]);
            CBTarmemHome.Checked = Convert.ToBoolean(dt.Rows[0]["TarmemManzil"]);
            CBTathithHome.Checked = Convert.ToBoolean(dt.Rows[0]["TathithManzil"]);

            DLAlBaheth.SelectedValue = dt.Rows[0]["IDAlBaheth"].ToString();
            DLModerAlGmeiah.SelectedValue = dt.Rows[0]["IDModer"].ToString();
            DLRaeesLagnatAlBahath.SelectedValue = dt.Rows[0]["IDRaesLagnatAlBahth"].ToString();
            DLPercint.SelectedValue = dt.Rows[0]["A1"].ToString();
            txt_Note_Baheth.Text = dt.Rows[0]["A2"].ToString().Replace("<br />", Environment.NewLine);
            FGetImgBenaaHome();
            FGetImgTarmemHome();
            FGetImgTathithHome();

            pnlData.Visible = true;
            pnlNullData.Visible = false;
            pnlCheckData.Visible = true;

        }
        else
        {
            pnlData.Visible = false;
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "لا توجد نتائج لهذا التقرير ... ";
            pnlNullData.Visible = true;
            pnlCheckData.Visible = false;
        }
    }

    private void FGetImgBenaaHome()
    {
        ClassReportAlZyaratImages CRZI = new ClassReportAlZyaratImages();
        CRZI._Top = 15;
        CRZI._IDMustafeed = Convert.ToInt32(txtNumberMostafeed.Text);
        CRZI._IDReport = Convert.ToInt32(txtNumberReport.Text);
        CRZI._IDType = 1;
        CRZI._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CRZI.BArnReportAlZyaratImagesByID();
        if (dt.Rows.Count > 0)
        {
            RPTBenaaHome.DataSource = dt;
            RPTBenaaHome.DataBind();
        }
    }

    private void FGetImgTarmemHome()
    {
        ClassReportAlZyaratImages CRZI = new ClassReportAlZyaratImages();
        CRZI._Top = 15;
        CRZI._IDMustafeed = Convert.ToInt32(txtNumberMostafeed.Text);
        CRZI._IDReport = Convert.ToInt32(txtNumberReport.Text);
        CRZI._IDType = 2;
        CRZI._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CRZI.BArnReportAlZyaratImagesByID();
        if (dt.Rows.Count > 0)
        {
            RPTTarmemHome.DataSource = dt;
            RPTTarmemHome.DataBind();
        }
    }

    private void FGetImgTathithHome()
    {
        ClassReportAlZyaratImages CRZI = new ClassReportAlZyaratImages();
        CRZI._Top = 15;
        CRZI._IDMustafeed = Convert.ToInt32(txtNumberMostafeed.Text);
        CRZI._IDReport = Convert.ToInt32(txtNumberReport.Text);
        CRZI._IDType = 3;
        CRZI._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CRZI.BArnReportAlZyaratImagesByID();
        if (dt.Rows.Count > 0)
        {
            RPTTathithHome.DataSource = dt;
            RPTTathithHome.DataBind();
        }
    }

    private void FGetAlBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsBaheth = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAlBaheth.Items.Clear();
            DLAlBaheth.Items.Add("");
            DLAlBaheth.AppendDataBoundItems = true;
            DLAlBaheth.DataValueField = "ID_Item";
            DLAlBaheth.DataTextField = "FirstName";
            DLAlBaheth.DataSource = dt;
            DLAlBaheth.DataBind();
        }
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
        ClassAdmin_Arn.FGetRaeesLagnatAlBahath(DLRaeesLagnatAlBahath);
    }

    protected void LBUpdate_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            FArnReportAlZyaratEdit();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FArnReportAlZyaratEdit()
    {
        ClassReportAlZyarat CRZ = new ClassReportAlZyarat();
        CRZ._NumberReport = Convert.ToInt32(Request.QueryString["ID"]);
        CRZ._DateReport = Convert.ToDateTime(txtDateReport.Text.Trim()).ToString("yyyy/MM/dd");
        CRZ._TathithManzil = Convert.ToBoolean(CBTathithHome.Checked);
        CRZ._PhotoTathithManzil = "0";
        CRZ._BenaManzil = Convert.ToBoolean(CBBenaaHome.Checked);
        CRZ._PhotoBenaManzil = "0";
        CRZ._TarmemManzil = Convert.ToBoolean(CBTarmemHome.Checked);
        CRZ._PhotoTarmemManzil = "0";
        CRZ._IDAlBaheth = Convert.ToInt32(DLAlBaheth.SelectedValue);
        CRZ._Egathy = Convert.ToBoolean(CBEgathy.Checked);
        CRZ._NumberEgathy = Convert.ToInt32(txtEgathy.Text.Trim());        
        CRZ.__Other = Convert.ToBoolean(CBOther.Checked);
        CRZ._WathOther = txtOther.Text.Trim();
        CRZ._IDModer = Convert.ToInt32(DLModerAlGmeiah.SelectedValue);
        CRZ._IsModerAllow = false;
        CRZ._IDRaesLagnatAlBahth = Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue);
        CRZ._IsRaesLagnatAlBahthAllow = false;
        CRZ._A1 = DLPercint.SelectedValue;
        CRZ._A2 = txt_Note_Baheth.Text.Trim().Replace(Environment.NewLine, "<br />");
        CRZ._A3 = "0";
        CRZ._A4 = "0";
        CRZ._A5 = "0";
        CRZ.BArnReportAlZyaratEdit2();
        FGetArnReportAlZyaratByID();
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = true;
        lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
        if (Attach_Repostry_SMS_Send_.AllSendSystemSocialSearch())
            Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل تقرير زيارة ميدانية" + "\n" + "رقم الملف :" + txtNumberMostafeed.Text.Trim() + "\n" + "بإنتظار الموافقة ,,,", "BerArn", "Edit", Test_Saddam.FGetIDUsiq());
    }

    string ImgArt;
    protected void LBBenaaHome_Click(object sender, EventArgs e)
    {
        if (FBenaaHome.HasFiles)
        {
            foreach (HttpPostedFile uploadedFile in FBenaaHome.PostedFiles)
            {
                
                string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
                string ext = Path.GetExtension(FBenaaHome.PostedFile.FileName);
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
                    IDMessageWarning.Visible = true;
                    IDMessageSuccess.Visible = false;
                    lblWarning.Text = "Image Allow " + string.Join(",", validFileTypes);
                    return;
                }
                else
                {
                    // ReSize Img
                    Stream strm = uploadedFile.InputStream;
                    System.Drawing.Image im = System.Drawing.Image.FromStream(uploadedFile.InputStream);
                    double h = im.PhysicalDimension.Height;
                    double w = im.PhysicalDimension.Width;
                    using (var image = System.Drawing.Image.FromStream(strm))
                    {
                        int newWidth = Convert.ToInt32(w); // 855; // New Width of Image in Pixel
                        int newHeight = Convert.ToInt32(h); // 495; // New Height of Image in Pixel
                        var thumbImg = new Bitmap(newWidth, newHeight);
                        var thumbGraph = Graphics.FromImage(thumbImg);
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imgRectangle);
                        string XRandom = Convert.ToString(ClassDataAccess.RandomGenerator());
                        string theFileName = Path.Combine(Server.MapPath("~/ImgHpme/"), uploadedFile.FileName.Remove(3) + XRandom + ".png");
                        thumbImg.Save(theFileName, image.RawFormat);
                        ImgArt = "ImgHpme/" + uploadedFile.FileName.Remove(3) + XRandom + ".png";
                        FArticleAdd(1);
                    }
                }
            }
        }
        else
        {
            ImgArt = "ImgArticle/logo.png";
            FArticleAdd(1);
        }
        LBTarmemHome.Focus();
    }

    protected void LBTarmemHome_Click(object sender, EventArgs e)
    {
        if (FTarmemHome.HasFiles)
        {
            foreach (HttpPostedFile uploadedFile in FTarmemHome.PostedFiles)
            {

                //uploadedFile.SaveAs(System.IO.Path.Combine(Server.MapPath("~/ImgHpme/"),
                //uploadedFile.FileName));
                string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
                string ext = Path.GetExtension(FTarmemHome.PostedFile.FileName);
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
                    IDMessageWarning.Visible = true;
                    IDMessageSuccess.Visible = false;
                    lblWarning.Text = "Image Allow " + string.Join(",", validFileTypes);
                    return;
                }
                else
                {
                    // ReSize Img
                    Stream strm = uploadedFile.InputStream;
                    System.Drawing.Image im = System.Drawing.Image.FromStream(uploadedFile.InputStream);
                    double h = im.PhysicalDimension.Height;
                    double w = im.PhysicalDimension.Width;
                    using (var image = System.Drawing.Image.FromStream(strm))
                    {
                        int newWidth = Convert.ToInt32(w); // 855; // New Width of Image in Pixel
                        int newHeight = Convert.ToInt32(h); // 495; // New Height of Image in Pixel
                        var thumbImg = new Bitmap(newWidth, newHeight);
                        var thumbGraph = Graphics.FromImage(thumbImg);
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imgRectangle);
                        string XRandom = Convert.ToString(ClassDataAccess.RandomGenerator());
                        string theFileName = Path.Combine(Server.MapPath("~/ImgHpme/"), uploadedFile.FileName.Remove(3) + XRandom + ".png");
                        thumbImg.Save(theFileName, image.RawFormat);
                        ImgArt = "ImgHpme/" + uploadedFile.FileName.Remove(3) + XRandom + ".png";
                        FArticleAdd(2);
                    }
                }
            }
        }
        else
        {
            ImgArt = "ImgArticle/logo.png";
            FArticleAdd(2);
        }
        LBTathithHome.Focus();
    }

    protected void LBTathithHome_Click(object sender, EventArgs e)
    {
        if (FTathithHome.HasFiles)
        {
            foreach (HttpPostedFile uploadedFile in FTathithHome.PostedFiles)
            {

                //uploadedFile.SaveAs(System.IO.Path.Combine(Server.MapPath("~/ImgHpme/"),
                //uploadedFile.FileName));
                string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
                string ext = Path.GetExtension(FTathithHome.PostedFile.FileName);
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
                    IDMessageWarning.Visible = true;
                    IDMessageSuccess.Visible = false;
                    lblWarning.Text = "Image Allow " + string.Join(",", validFileTypes);
                    return;
                }
                else
                {
                    // ReSize Img
                    Stream strm = uploadedFile.InputStream;
                    System.Drawing.Image im = System.Drawing.Image.FromStream(uploadedFile.InputStream);
                    double h = im.PhysicalDimension.Height;
                    double w = im.PhysicalDimension.Width;
                    using (var image = System.Drawing.Image.FromStream(strm))
                    {
                        int newWidth = Convert.ToInt32(w); // 855; // New Width of Image in Pixel
                        int newHeight = Convert.ToInt32(h); // 495; // New Height of Image in Pixel
                        var thumbImg = new Bitmap(newWidth, newHeight);
                        var thumbGraph = Graphics.FromImage(thumbImg);
                        thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                        thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                        thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph.DrawImage(image, imgRectangle);
                        string XRandom = Convert.ToString(ClassDataAccess.RandomGenerator());
                        string theFileName = Path.Combine(Server.MapPath("~/ImgHpme/"), uploadedFile.FileName.Remove(3) + XRandom + ".png");
                        thumbImg.Save(theFileName, image.RawFormat);
                        ImgArt = "ImgHpme/" + uploadedFile.FileName.Remove(3) + XRandom + ".png";
                        FArticleAdd(3);
                    }
                }
            }
        }
        else
        {
            ImgArt = "ImgArticle/logo.png";
            FArticleAdd(3);
        }
        txt_Note_Baheth.Focus();
    }

    private void FArticleAdd(int IDType)
    {
        ClassReportAlZyaratImages CRAI = new ClassReportAlZyaratImages()
        {
            _IDMustafeed = Convert.ToInt32(txtNumberMostafeed.Text),
            _IDReport = Convert.ToInt32(txtNumberReport.Text.Trim()),
            _IDType = IDType,
            _ImgReport = ImgArt,
            _DateAddimg = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss")
        };
        CRAI.BArnReportAlZyaratImagesAdd();
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = true;
        lblSuccess.Text = "تم رفع الصور بنجاح ... ";
        FGetImgBenaaHome();
        FGetImgTarmemHome();
        FGetImgTathithHome();
    }

    protected void LBDeleteBenaaHome_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Comp_ID = Convert.ToString((((LinkButton)sender).CommandArgument)).ToString();
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            conn.Open();
            string sql = "UPDATE [dbo].[ReportAlZyaratImages] SET [IsDelete] = @IsDelete WHERE IDItam = @IDItam";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IDItam", Convert.ToInt32(Comp_ID));
            cmd.Parameters.AddWithValue("@IsDelete", true);
            cmd.ExecuteScalar();
            conn.Close();
            FGetArnReportAlZyaratByID();
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم حذف الصور بنجاح ... ";
            LBTarmemHome.Focus();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void LBDeleteTarmemHome_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Comp_ID = Convert.ToString((((LinkButton)sender).CommandArgument)).ToString();
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            conn.Open();
            string sql = "UPDATE [dbo].[ReportAlZyaratImages] SET [IsDelete] = @IsDelete WHERE IDItam = @IDItam";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IDItam", Convert.ToInt32(Comp_ID));
            cmd.Parameters.AddWithValue("@IsDelete", true);
            cmd.ExecuteScalar();
            conn.Close();
            FGetArnReportAlZyaratByID();
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم حذف الصور بنجاح ... ";
            LBTathithHome.Focus();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void LBDeleteTathithHome_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Comp_ID = Convert.ToString((((LinkButton)sender).CommandArgument)).ToString();
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            conn.Open();
            string sql = "UPDATE [dbo].[ReportAlZyaratImages] SET [IsDelete] = @IsDelete WHERE IDItam = @IDItam";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IDItam", Convert.ToInt32(Comp_ID));
            cmd.Parameters.AddWithValue("@IsDelete", true);
            cmd.ExecuteScalar();
            conn.Close();
            FGetArnReportAlZyaratByID();
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم حذف الصور بنجاح ... ";
            txt_Note_Baheth.Focus();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnAddDevice_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            FCheckAddDevice();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FCheckAddDevice()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[ReportAlZyaratElectricalAppliances] With(NoLock) Where IDDevice = @0 And IDMustafeed = @1 And IDReport = @2 And IsDelete = @3", DLDivice.SelectedValue, txtNumberMostafeed.Text.Trim(), txtNumberReport.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "لقد قمت بإضافة الجهاز مسبقاً ... ";
            return;
        }
        else
            FArnReportAlZyaratElectricalAppliancesAdd();
    }

    private void FArnReportAlZyaratElectricalAppliancesAdd()
    {
        GetCookie();
        ClassReportAlZyaratElectricalAppliances CRZEA = new ClassReportAlZyaratElectricalAppliances()
        {
            _IDDevice = Convert.ToInt32(DLDivice.SelectedValue),
            _IDNumberCount = Convert.ToInt32(txtNumber.Text.Trim()),
            _IDMustafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
            _IDReport = Convert.ToInt32(txtNumberReport.Text.Trim()),
            _DateAddDevice = txtDate.Text.Trim(),
            _A1 = "0",
            _A2 = "0",
            _A3 = "0",
            _A4 = "0",
            _A5 = "0",
            _IsDelete = false
        };
        CRZEA.BArnReportAlZyaratElectricalAppliancesAdd();
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = true;
        lblSuccess.Text = "تم الإضافة بنجاح ... ";
        txtNumber.Text = string.Empty;
        FArnReportAlZyaratElectricalAppliancesGetByMostafeed();
    }

    private void FArnReportAlZyaratElectricalAppliancesGetByMostafeed()
    {
        ClassReportAlZyaratElectricalAppliances CRZEA = new ClassReportAlZyaratElectricalAppliances();
        CRZEA._Top = 15;
        CRZEA._IDMustafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim());
        CRZEA._IDReport = Convert.ToInt32(txtNumberReport.Text.Trim());
        CRZEA._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CRZEA.BArnReportAlZyaratElectricalAppliancesGetByMostafeed();
        if (dt.Rows.Count > 0)
        {
            GVDevice.DataSource = dt;
            GVDevice.DataBind();
            lblCount.Text = dt.Rows.Count.ToString();
            pnlDevice.Visible = true;
            pnlNull.Visible = false;
        }
        else
        {
            pnlNull.Visible = true;
            pnlDevice.Visible = false;
        }
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            foreach (GridViewRow row in GVDevice.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVDevice.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ReportAlZyaratElectricalAppliances] SET [IsDelete] = @IsDelete WHERE IDItam = @IDItam";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItam", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم حذف البيانات بنجاح ... ";
            txtNumberMedical.Focus();
            FArnReportAlZyaratElectricalAppliancesGetByMostafeed();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnDeleteDevice_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            foreach (GridViewRow row in GVDeviceMedical.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVDeviceMedical.DataKeys[row.RowIndex].Value);

                    Model_MedicalEquipments_ MME = new Model_MedicalEquipments_()
                    {
                        IDCheck = "Delete",
                        ID_Item = new Guid(Comp_ID),
                        ID_Device = 0,
                        ID_Number_Count = 0,
                        ID_Mustafeed = 0,
                        ID_Report = 0,
                        CreatedBy = 0,
                        CreatedDate = string.Empty,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        DeleteBy = Test_Saddam.FGetIDUsiq(),
                        DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false
                    };
                    Repostry_MedicalEquipments_ RME = new Repostry_MedicalEquipments_();
                    string Xresult = RME.FAPI_ReportAlZyaratMedicalEquipments_Add(MME);
                    if (Xresult == "IsSuccessDelete")
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم حذف البيانات بنجاح ... ";
                        txtEgathy.Focus();
                        FAPI_ReportAlZyaratMedicalEquipments_Manage();
                    }
                }
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnAddDeviceMedical_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            FAPI_ReportAlZyaratMedicalEquipments_Add();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FAPI_ReportAlZyaratMedicalEquipments_Add()
    {
        Model_MedicalEquipments_ MME = new Model_MedicalEquipments_()
        {
            IDCheck = "Add",
            ID_Item = Guid.NewGuid(),
            ID_Device = Convert.ToInt32(DLDiviceMedical.SelectedValue),
            ID_Number_Count = Convert.ToInt32(txtNumberMedical.Text.Trim()),
            ID_Mustafeed = Convert.ToInt32(txtNumberMostafeed.Text.Trim()),
            ID_Report = Convert.ToInt32(txtNumberReport.Text.Trim()),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            CreatedDate = Convert.ToDateTime(txtDateMedical.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
            ModifiedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            DeleteBy = 0,
            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsActive = true
        };
        Repostry_MedicalEquipments_ RME = new Repostry_MedicalEquipments_();
        string Xresult = RME.FAPI_ReportAlZyaratMedicalEquipments_Add(MME);
        if (Xresult == "IsExistsAdd")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "لقد قمت بإضافة الجهاز مسبقاً ... ";
            return;
        }
        else if (Xresult == "IsSuccessAdd")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
            txtNumberMedical.Text = string.Empty;
            txtEgathy.Focus();
            FAPI_ReportAlZyaratMedicalEquipments_Manage();
        }
    }

    private void FAPI_ReportAlZyaratMedicalEquipments_Manage()
    {
        Repostry_MedicalEquipments_.FAPI_ReportAlZyaratMedicalEquipments_Manage(Convert.ToInt32(txtNumberMostafeed.Text.Trim()), Convert.ToInt32(txtNumberReport.Text.Trim()),
            GVDeviceMedical, lblCountMedical, pnlDeviceMedical, pnlNullMedical);
    }

}