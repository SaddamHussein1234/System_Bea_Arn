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

public partial class Cpanel_CPanelManageSite_PageAboutAr : System.Web.UI.Page
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
            bool A74;
            A74 = Convert.ToBoolean(dtViewProfil.Rows[0]["A74"]);
            if (A74 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetDetails();
        }
    }

    private void FGetDetails()
    {
        ClassSetting CS = new ClassSetting();
        CS.IDSetting = 964654;
        DataTable dt = new DataTable();
        dt = CS.BSettingGetById();
        if (dt.Rows.Count > 0)
        {
            Session["IOldImgVison"] = dt.Rows[0]["ImgVisionAr"].ToString();
            Img.ImageUrl = "/" + Session["IOldImgVison"].ToString();
            txtAbout.Text = dt.Rows[0]["TextAboutAr"].ToString();
            txtVision.Text = dt.Rows[0]["TextVisionAr"].ToString();
            txtMessage.Text = dt.Rows[0]["TextMessageAr"].ToString();
            txtGoals.Text = dt.Rows[0]["TextGoalsAr"].ToString();
            txtValus.Text = dt.Rows[0]["TextValuesAr"].ToString();
            txtLink.Text = dt.Rows[0]["VideoAboutAr"].ToString();
            IDVideo.Src = txtLink.Text;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            FChackImgF();
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FChackImgF()
    {
        if (FUImgVision.HasFile)
        {
            string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
            string ext = Path.GetExtension(FUImgVision.PostedFile.FileName);
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
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblWarning.Text = "Image Allow " + string.Join(",", validFileTypes);
                return;
            }
            else
                FUpimg(FUImgVision);
        }
        else
            FUpimg(FUImgVision);
    }

    protected void FUpimg(FileUpload upl)
    {
        if (upl.HasFile)
        {
            // ReSize Img
            Stream strm = upl.PostedFile.InputStream;
            using (var image = System.Drawing.Image.FromStream(strm))
            {
                int newWidth = 220; // New Width of Image in Pixel
                int newHeight = 147; // New Height of Image in Pixel
                var thumbImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imgRectangle);
                string XRandom = Convert.ToString(ClassDataAccess.RandomGenerator());
                string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/ImgSetting/"), upl.FileName.Remove(3) + XRandom + ".png");
                thumbImg.Save(theFileName, image.RawFormat);
                Session["IOldImgVison"] = "ImgSystem/ImgSetting/" + upl.FileName.Remove(3) + XRandom + ".png";
                EditSetting();
            }
        }
        else
            EditSetting();
    }

    private void EditSetting()
    {
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[SettingTable] SET [ImgVisionAr] = @ImgVisionAr,[TextAboutAr] = @TextAboutAr,[TextVisionAr] = @TextVisionAr,[TextMessageAr] = @TextMessageAr,[TextGoalsAr] = @TextGoalsAr,[TextValuesAr] = @TextValuesAr,[VideoAboutAr] = @VideoAboutAr WHERE IDSetting = @IDSetting";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ImgVisionAr", Session["IOldImgVison"].ToString());
        cmd.Parameters.AddWithValue("@TextAboutAr", txtAbout.Text.Trim());
        cmd.Parameters.AddWithValue("@TextVisionAr", txtVision.Text.Trim());
        cmd.Parameters.AddWithValue("@TextMessageAr", txtMessage.Text.Trim());
        cmd.Parameters.AddWithValue("@TextGoalsAr", txtGoals.Text.Trim());
        cmd.Parameters.AddWithValue("@TextValuesAr", txtValus.Text.Trim());
        cmd.Parameters.AddWithValue("@VideoAboutAr", txtLink.Text.Trim());
        cmd.Parameters.AddWithValue("@IDSetting", 964654);
        cmd.ExecuteScalar();
        conn.Close();
        IDMessageSuccess.Visible = true;
        IDMessageWarning.Visible = false;
        lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
        Img.ImageUrl = "/" + Session["IOldImgVison"].ToString();
        IDVideo.Src = txtLink.Text.Trim();
        FGetDetails();
    }

}