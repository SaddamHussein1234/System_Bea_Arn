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

public partial class Cpanel_CPanelSetting_PageSetting : System.Web.UI.Page
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
            bool A71;
            A71 = Convert.ToBoolean(dtViewProfil.Rows[0]["A71"]);
            if (A71 == false)
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
            txtEndeSite.Text = dt.Rows[0]["EndSide"].ToString();
            txtNameAr.Text = dt.Rows[0]["NameSever"].ToString();

            txtNameManager.Text = dt.Rows[0]["NameManager"].ToString();
            CBClose.Checked = Convert.ToBoolean(dt.Rows[0]["IsCloseSite"]);
            txtMessageClse.Text = dt.Rows[0]["MessageClose"].ToString();
            //txtMessageClseTr.Text = dt.Rows[0]["MessageCloseTr"].ToString();
            //txtMessageClseEn.Text = dt.Rows[0]["MessageCloseEn"].ToString();
            Session["ISysAl"] = dt.Rows[0]["ImgSystem"].ToString();
            Img.ImageUrl = "/" + Session["ISysAl"].ToString();
            //DLStart.SelectedValue = dt.Rows[0]["StartRediract"].ToString();
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

        }
    }

    private void FChackImgF()
    {
        if (FUImgTeacher.HasFile)
        {
            string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
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
                lbmsg.Text = "Image Allow " + string.Join(",", validFileTypes);
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
            // ReSize Img
            Stream strm = upl.PostedFile.InputStream;
            using (var image = System.Drawing.Image.FromStream(strm))
            {
                int newWidth = 317; // New Width of Image in Pixel
                int newHeight = 264; // New Height of Image in Pixel
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
                Session["ISysAl"] = "ImgSystem/ImgSetting/" + upl.FileName.Remove(3) + XRandom + ".png";
                EditSetting();
            }
        }
        else
        {
            EditSetting();
        }
    }

    private void EditSetting()
    {
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[SettingTable] SET [EndSide] = @EndSide,[NameSever] = @NameSever,[NameManager] = @NameManager,[IsCloseSite] = @IsCloseSite,[MessageClose] = @MessageClose,[MessageCloseTr] = @MessageCloseTr,[MessageCloseEn] = @MessageCloseEn,[ImgSystem] = @ImgSystem , StartRediract = @StartRediract WHERE IDSetting = @IDSetting";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@EndSide", txtEndeSite.Text.Trim());
        cmd.Parameters.AddWithValue("@NameSever", txtNameAr.Text.Trim());
        cmd.Parameters.AddWithValue("@NameManager", txtNameManager.Text.Trim());
        cmd.Parameters.AddWithValue("@IsCloseSite", Convert.ToBoolean(CBClose.Checked));
        cmd.Parameters.AddWithValue("@MessageClose", txtMessageClse.Text.Trim());
        cmd.Parameters.AddWithValue("@MessageCloseTr", string.Empty);
        cmd.Parameters.AddWithValue("@MessageCloseEn", string.Empty);
        cmd.Parameters.AddWithValue("@ImgSystem", Session["ISysAl"].ToString());
        cmd.Parameters.AddWithValue("@StartRediract", DLStart.SelectedValue);
        cmd.Parameters.AddWithValue("@IDSetting", 964654);
        cmd.ExecuteScalar();
        conn.Close();
        lbmsg.Text = "تم تعديل البيانات بنجاح";
        lbmsg.ForeColor = Color.MediumAquamarine;
        Img.ImageUrl = "/" + Session["ISysAl"].ToString();
        FGetDetails();
    }

}