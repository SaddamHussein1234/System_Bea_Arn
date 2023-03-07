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

public partial class Cpanel_CHome_PageMyAccount : System.Web.UI.Page
{
    ClassAdmin_Arn CA = new ClassAdmin_Arn();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtCount_Day.Text = ClassSetting.FGetCount_Allow_Day_Cookies().ToString();
            GetData();
        }
    }

    private void GetData()
    {
        try
        {
            GetCookie();
            CA._IDUniq = IDUniq;
            CA._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CA.BArnAdminGetByIDUniq();
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dt.Rows[0]["IsSuperAdmin"]))
                    LBLogOutAll.Visible = true;
                lblGroup.Text = dt.Rows[0]["NameGroup"].ToString();
                txtNameFirst.Text = dt.Rows[0]["FirstName"].ToString();
                txtNameLast.Text = dt.Rows[0]["__ID_Card"].ToString();
                txtUserName.Text = dt.Rows[0]["User_Name_"].ToString();
                Session["YourName"] = dt.Rows[0]["User_Name_"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                Session["YourEmail"] = dt.Rows[0]["Email"].ToString();
                txtPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
                CBView.Checked = Convert.ToBoolean(dt.Rows[0]["IsBlock"]);
                Session["YourImg"] = dt.Rows[0]["ImgAdmin"].ToString();
                ImgAdmin.ImageUrl = "/" + Session["YourImg"].ToString();

                if (Convert.ToBoolean(dt.Rows[0]["_Two_Factor_Enabled_"]))
                {IDActive.Visible = false; IDUnActive.Visible = true;}
                else
                {IDActive.Visible = true; IDUnActive.Visible = false;}

                if (Convert.ToBoolean(dt.Rows[0]["_SMS_Enabled_"]))
                {IDActiveSMS.Visible = false; IDUnActiveSMS.Visible = true;}
                else
                {IDActiveSMS.Visible = true; IDUnActiveSMS.Visible = false;}
            }
            else
                Response.Redirect("Default.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }
    }
    
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FChackUserName();
        }
        catch
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ,,, ";
            return;
        }
    }

    private void FChackUserName()
    {
        if (txtUserName.Text.Trim() != Session["YourName"].ToString())
        {
            DataTable dtUser = new DataTable();
            dtUser = ClassDataAccess.GetData("Select User_Name_,IsDelete from tbl_Admin Where User_Name_ =@0 And IsDelete = @1", txtUserName.Text.Trim(), Convert.ToString(false));
            if (dtUser.Rows.Count > 0)
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblWarning.Text = "إسم المستخدم مستخدم لشخص آخر , قم بتغييرة ,,, ";
                return;
            }
            else
            {
                Session["YourName"] = txtUserName.Text.Trim();
                FChackEmail();
            }
        }
        else
            FChackEmail();

    }

    private void FChackEmail()
    {
        if (txtEmail.Text.Trim() != Session["YourEmail"].ToString())
        {
            DataTable dtEmail = new DataTable();
            dtEmail = ClassDataAccess.GetData("Select Email,IsDelete from tbl_Admin Where Email =@0 And IsDelete = @1", txtEmail.Text.Trim(), Convert.ToString(false));
            if (dtEmail.Rows.Count > 0)
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblWarning.Text = "البريد الالكتروني مستخدم بالفعل ,,, ";
                return;
            }
            else
            {
                Session["YourEmail"] = txtEmail.Text.Trim();
                FChackImgF();
            }
        }
        else
            FChackImgF();
    }

    private void FChackImgF()
    {
        if (FUImgAdmin.HasFile)
        {
            string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
            string ext = Path.GetExtension(FUImgAdmin.PostedFile.FileName);
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
                lblWarning.Text = "المسموح فقط " + string.Join(",", validFileTypes);
                return;
            }
            else
                FUpimg(FUImgAdmin);
        }
        else
            FUpimg(FUImgAdmin);
    }

    protected void FUpimg(FileUpload upl)
    {
        if (upl.HasFile)
        {
            // ReSize Img
            Stream strm = upl.PostedFile.InputStream;
            using (var image = System.Drawing.Image.FromStream(strm))
            {
                int newWidth = 256; // New Width of Image in Pixel
                int newHeight = 192; // New Height of Image in Pixel
                var thumbImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imgRectangle);
                string XRandom = Convert.ToString(ClassDataAccess.RandomGenerator());
                string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/ImgAdmin/"), upl.FileName.Remove(3) + XRandom + ".png");
                thumbImg.Save(theFileName, image.RawFormat);
                Session["YourImg"] = "ImgSystem/ImgAdmin/" + upl.FileName.Remove(3) + XRandom + ".png";
                EditMyData();
            }
        }
        else
            EditMyData();
    }

    private void EditMyData()
    {
        GetCookie();
        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
        conn.Open();
        string sql = "UPDATE [dbo].[tbl_Admin] SET [__ID_Card] = @ID_Card, [User_Name_] = @User_Name_,[Email] = @Email,[PhoneNumber] = @PhoneNumber,[ImgAdmin] = @ImgAdmin WHERE IDUniq = @IDUniq";
        SqlCommand cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@ID_Card", txtNameLast.Text.Trim());
        cmd.Parameters.AddWithValue("@User_Name_", Session["YourName"].ToString());
        cmd.Parameters.AddWithValue("@Email", Session["YourEmail"].ToString());
        cmd.Parameters.AddWithValue("@PhoneNumber", txtPhone.Text.Trim());
        cmd.Parameters.AddWithValue("@ImgAdmin", Session["YourImg"].ToString());
        cmd.Parameters.AddWithValue("@IDUniq", Convert.ToString(IDUniq));
        cmd.ExecuteScalar();
        conn.Close();
        IDMessageSuccess.Visible = true;
        IDMessageWarning.Visible = false;
        lblSuccess.InnerText = "تم تحديث البيانات بنجاح ,,, ";
        GetData();
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        Session.Remove("YourName");
        Session.Remove("YourEmail");
        Session.Remove("YourImg");
        Response.Redirect("Default.aspx");
    }

    protected void LBLogOutAll_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            DataTable DTAdmin = new DataTable();
            DTAdmin = ClassDataAccess.GetData("Select Top(1000) [_ID_Cookie_] from [dbo].[tbl_Admin] With(NoLock) WHERE [IsSuperAdmin] = 0");
            if (DTAdmin.Rows.Count > 0)
            {
                SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                conn.Open();
                for (int i = 0; i <= DTAdmin.Rows.Count - 1; i++)
                {
                    string sql = "UPDATE [dbo].[tbl_Admin] SET [_ID_Cookie_] = @ID_Cookie WHERE [IsSuperAdmin] = @IsSuperAdmin";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID_Cookie", Guid.NewGuid());
                    cmd.Parameters.AddWithValue("@IsSuperAdmin", false);
                    cmd.ExecuteScalar();
                }
                conn.Close();
                IDMessageSuccess.Visible = true;
                IDMessageWarning.Visible = false;
                lblSuccess.InnerText = "تم تسجيل الخروج لجميع المستخدمين بنجاح ,,, ";
            }
        }
        catch
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ,,, ";
            return;
        }
    }

    protected void LBEditCountDay_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            conn.Open();
            string sql = "UPDATE [dbo].[SettingTable] SET [_Count_Allow_Day_Cookies_] = @Count_Allow_Day_Cookies WHERE IDSetting = @IDSetting";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Count_Allow_Day_Cookies", txtCount_Day.Text.Trim());
            cmd.Parameters.AddWithValue("@IDSetting", 964654);
            cmd.ExecuteScalar();
            conn.Close();
            IDMessageSuccess.Visible = true;
            IDMessageWarning.Visible = false;
            lblSuccess.InnerText = "تم تم تحديث البيانات بنجاح ,,, ";
            txtCount_Day.Text = ClassSetting.FGetCount_Allow_Day_Cookies().ToString();
        }
        catch (Exception)
        {
            return;
        }
    }

}