using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRM.Models;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
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

public partial class Cpanel_ERP_CRM_PageCompany_PageCompanyAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repostry_Company_Type_.FCRM_Company_Type_Manage(ddlCompanyType);
            txtRegistration_No.Text = (Repostry_Company_.FCRM_Company_Manage() + 1).ToString();
            Repostry_Country_.FErp_Country_Manage(ddlCountry);
            ddlCountry.SelectedValue = "dbe3bd9e-af08-41f5-b579-2543b6b0e132";
            txtEmail.Text = ClassSaddam.RandomGenerator().ToString().Replace("-", "") + "@gmail.com";
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        Model_Company_ MC = new Model_Company_();
        MC.IDCheck = "GetByIDUniq";
        MC.ID_Item = new Guid(Request.QueryString["ID"]);
        MC.Company_Name = string.Empty;
        MC.Is_Active = false;
        MC.Is_Delete = false;
        DataTable dt = new DataTable();
        Repostry_Company_ RC = new Repostry_Company_();
        dt = RC.BCRM_Company_Manage(MC);
        if (dt.Rows.Count > 0)
        {
            DLType_Customer.SelectedValue = dt.Rows[0]["_Type_Customer_"].ToString();
            txtCompanyName.Text = dt.Rows[0]["_Company_Name_"].ToString();
            ddlCompanyType.SelectedValue = dt.Rows[0]["_Company_Type_"].ToString();
            txtRegistration_No.Text = dt.Rows[0]["_Registration_No_"].ToString();
            txtAddress.Text = dt.Rows[0]["_Address_"].ToString();
            ddlCountry.SelectedValue = dt.Rows[0]["_Country_"].ToString();
            txtCity.Text = dt.Rows[0]["_City_"].ToString();
            txtSite.Text = dt.Rows[0]["_Website_"].ToString();
            txtEmail.Text = dt.Rows[0]["_Email_Address_"].ToString();
            txtEstablished_Year.Text = dt.Rows[0]["_Established_Year_"].ToString();
            txtFax.Text = dt.Rows[0]["_Fax_"].ToString();
            txtPhone_Number1.Text = dt.Rows[0]["_Phone_Number1_"].ToString();
            txtMobile_Number1.Text = dt.Rows[0]["_Mobile_Number1_"].ToString();
            txtPhone_Number2.Text = dt.Rows[0]["_Phone_Number2_"].ToString();
            Session["Img_Icon_"] = dt.Rows[0]["_Icon_Img_"].ToString();
            CBIsActive.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Active_"]);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;
            FChackImgF();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FChackImgF()
    {
        if (fuPhoto.HasFile)
        {
            string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
            string ext = Path.GetExtension(fuPhoto.PostedFile.FileName);
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
                FUpimg(fuPhoto);
        }
        else
            FUpimg(fuPhoto);
    }

    protected void FUpimg(FileUpload upl)
    {
        if (upl.HasFile)
        {
            // ReSize Img
            Stream strm = upl.PostedFile.InputStream;
            System.Drawing.Image im = System.Drawing.Image.FromStream(strm);
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
                string XRandom = Convert.ToString(Guid.NewGuid()).Replace("-", "");
                string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/ImgCompany/"), XRandom + ".png");
                thumbImg.Save(theFileName, image.RawFormat);
                Session["Img_Icon_"] = "ImgSystem/ImgCompany/" + XRandom + ".png";
                FAddAndEdit();
            }
        }
        else
        {
            if (Request.QueryString["id"] == null)
                Session["Img_Icon_"] = "ImgSystem/ImgCompany/no-img.jpg";
            FAddAndEdit();
        }
    }

    private void FAddAndEdit()
    {
        if (Request.QueryString["id"] == null)
        {
            Model_Company_ MC = new Model_Company_()
            {
                IDCheck = "Add",
                ID_Item = Guid.NewGuid(),
                Type_Customer = DLType_Customer.SelectedValue,
                Company_Name = txtCompanyName.Text.Trim(),
                Company_Type = new Guid(ddlCompanyType.SelectedValue),
                Registration_No = Convert.ToInt32(txtRegistration_No.Text.Trim()),
                Address = txtAddress.Text.Trim(),
                Country = new Guid(ddlCountry.SelectedValue),
                City = txtCity.Text.Trim(),
                Website = txtSite.Text.Trim(),
                Email_Address = txtEmail.Text.Trim(),
                Established_Year = Convert.ToInt32(txtEstablished_Year.Text.Trim()),
                Fax = txtFax.Text.Trim(),
                Phone_Number1 = txtPhone_Number1.Text.Trim(),
                Mobile_Number1 = txtMobile_Number1.Text.Trim(),
                Phone_Number2 = txtPhone_Number2.Text.Trim(),
                Icon_Img = Session["Img_Icon_"].ToString(),
                Is_Active = Convert.ToBoolean(CBIsActive.Checked),
                CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedBy = Test_Saddam.FGetIDUsiq(),
                ModifiedBy = 0,
                ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                Is_Delete = false
            };

            Repostry_Company_ RC = new Repostry_Company_();
            string Xresult = RC.FCRM_Company_Add(MC);
            if (Xresult == "IsExistsNumberAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة رقم التسجيل مسبقاً ... ";
                return;
            }
            else if (Xresult == "IsExistsNameAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة الداعم مسبقاً ... ";
                return;
            }
            else if (Xresult == "IsExistsEmailAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البريد الإلكتروني مسبقاً ... ";
                return;
            }
            else if (Xresult == "IsSuccessAdd")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
                txtRegistration_No.Text = (Convert.ToInt32(txtRegistration_No.Text) + 1).ToString();
                if (Attach_Repostry_SMS_Send_.AllSendSystemCRM())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة داعم" + "\n" + "بإسم :" + txtCompanyName.Text.Trim() , "BerArn", "Add", Test_Saddam.FGetIDUsiq());
            }
        }
        else if (Request.QueryString["id"] != null)
        {
            Model_Company_ MC = new Model_Company_()
            {
                IDCheck = "Edit",
                ID_Item = new Guid(Request.QueryString["id"]),
                Type_Customer = DLType_Customer.SelectedValue,
                Company_Name = txtCompanyName.Text.Trim(),
                Company_Type = new Guid(ddlCompanyType.SelectedValue),
                Registration_No = Convert.ToInt32(txtRegistration_No.Text.Trim()),
                Address = txtAddress.Text.Trim(),
                Country = new Guid(ddlCountry.SelectedValue),
                City = txtCity.Text.Trim(),
                Website = txtSite.Text.Trim(),
                Email_Address = txtEmail.Text.Trim(),
                Established_Year = Convert.ToInt32(txtEstablished_Year.Text.Trim()),
                Fax = txtFax.Text.Trim(),
                Phone_Number1 = txtPhone_Number1.Text.Trim(),
                Mobile_Number1 = txtMobile_Number1.Text.Trim(),
                Phone_Number2 = txtPhone_Number2.Text.Trim(),
                Icon_Img = Session["Img_Icon_"].ToString(),
                Is_Active = Convert.ToBoolean(CBIsActive.Checked),
                CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedBy = 0,
                ModifiedBy = Test_Saddam.FGetIDUsiq(),
                ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                Is_Delete = false
            };
            Repostry_Company_ RC = new Repostry_Company_();
            string Xresult = RC.FCRM_Company_Add(MC);
            if (Xresult == "IsExistsNumberEdit")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة رقم التسجيل مسبقاً ... ";
                return;
            }
            else if (Xresult == "IsExistsNameEdit")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة الداعم مسبقاً ... ";
                return;
            }
            else if (Xresult == "IsExistsEmailEdit")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البريد الإلكتروني مسبقاً ... ";
                return;
            }
            else if (Xresult == "IsSuccessEdit")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
                FGetData();
                if (Attach_Repostry_SMS_Send_.AllSendSystemCRM())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل داعم" + "\n" + "بإسم :" + txtCompanyName.Text.Trim() , "BerArn", "Edit", Test_Saddam.FGetIDUsiq());
            }
        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageCompany.aspx");
    }

}