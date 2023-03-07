using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageEmployeeAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A152");
            ClassAdmin_Arn.FGetAdminAllByItem("ByUniq", DLAdmin);
            txtBirthDate.Text = ClassSaddam.GetCurrentTime().AddYears(-20).ToString("yyyy-MM-dd");
            txtDateAdd.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            txtJoinDate.Text = txtDateAdd.Text;
            Repostry_Employee_Type_.FErp_EmployeeType_Manage(ddlEmployeeType);
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            Repostry_Designation_.FErp_Designation_Manage(ddlDesignation);
            Repostry_Employee_Grade_.FErp_Employee_Grade_Manage(ddlEmployeeGrade);
            Repostry_Shift_.FErp_Shift_Manage(ddlShift);
            Repostry_Country_.FErp_Country_Manage(ddlCountry);
            FGetLastID();
            pnlAdd.Visible = true; pnlEdit.Visible = false; pnlNull.Visible = false;

            if (Request.QueryString["ID"] != null)
                FGetData();
            if (Request.QueryString["Active"] != null)
                FHide(false);
        }
    }

    private void FHide(bool XValue)
    {
        pnl_colums.Visible = XValue;
        pnl_colums2.Visible = XValue;
        btnAdd.Visible = XValue;
    }

    private void FGetLastID()
    {
        Model_Employee_ ME = new Model_Employee_();
        ME.IDCheck = "GetByIDEmp";
        ME.EmployeeID = Guid.Empty;
        ME.FinancialYear_Id = Guid.Empty;
        ME.FirstName = string.Empty;
        ME.Date_From = string.Empty;
        ME.Date_To = string.Empty;
        ME.IsActive = true;
        DataTable dt = new DataTable();
        Repostry_Employee_ RE = new Repostry_Employee_();
        dt = RE.BErp_Employee_Master_Manage(ME);
        if (dt.Rows.Count > 0)
            txtPFNumber.Text = Convert.ToString(Convert.ToInt32(dt.Rows[0]["IDNo"].ToString()) + 1);
    }

    private void FGetData()
    {
        try
        {
            Model_Employee_ ME = new Model_Employee_();
            ME.IDCheck = "GetByIDUniq";
            ME.EmployeeID = new Guid(Request.QueryString["ID"]);
            ME.FinancialYear_Id = Guid.Empty;
            ME.FirstName = string.Empty;
            ME.Date_From = string.Empty;
            ME.Date_To = string.Empty;
            ME.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Employee_ RE = new Repostry_Employee_();
            dt = RE.BErp_Employee_Master_Manage(ME);
            if (dt.Rows.Count > 0)
            {
                DLAdmin.SelectedValue = dt.Rows[0]["EmployeeID"].ToString();
                DLAdmin.Enabled = false;
                ddlEmployeeType.SelectedValue = dt.Rows[0]["EmployeeTypeId"].ToString();
                ddlEmployeeGrade.SelectedValue = dt.Rows[0]["EmployeeGradeId"].ToString();
                ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                ddlDesignation.SelectedValue = dt.Rows[0]["DesignationId"].ToString();
                ddlShift.SelectedValue = dt.Rows[0]["ShiftId"].ToString();
                txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();
                txtMiddleName.Text = dt.Rows[0]["MiddleName"].ToString();
                txtLastName.Text = dt.Rows[0]["LastName"].ToString();
                txtBirthDate.Text = Convert.ToDateTime(dt.Rows[0]["BirthDate"]).ToString("yyyy-MM-dd");
                txtFatherName.Text = dt.Rows[0]["FatherName"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["Gender"]))
                {
                    rbtnMale.Checked = true; rbtnFeMale.Checked = false;
                }
                else
                {
                    rbtnMale.Checked = false; rbtnFeMale.Checked = true;
                }
                ddlMaratialStatus.SelectedValue = dt.Rows[0]["MaratialStatus"].ToString();
                txtCast.Text = dt.Rows[0]["Cast"].ToString();
                ddlCountry.SelectedValue = dt.Rows[0]["CountryId"].ToString();
                Repostry_State_.FErp_State_Manage(new Guid(ddlCountry.SelectedValue), ddlState);
                ddlState.SelectedValue = dt.Rows[0]["StateId"].ToString();
                txtCity.Text = dt.Rows[0]["City"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtPinCode.Text = dt.Rows[0]["PinCode"].ToString();
                txtMobile.Text = dt.Rows[0]["MobileNo"].ToString();
                txtPhone.Text = dt.Rows[0]["PhoneNo"].ToString();
                txtJoinDate.Text = Convert.ToDateTime(dt.Rows[0]["JoinDate"]).ToString("yyyy-MM-dd");
                txtPFNumber.Text = dt.Rows[0]["EmployeeNo"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtBankName.Text = dt.Rows[0]["BankName"].ToString();
                txtBranchName.Text = dt.Rows[0]["BranchName"].ToString();
                txtAccountName.Text = dt.Rows[0]["AccountName"].ToString();
                txtAccountNumber.Text = dt.Rows[0]["AccountNo"].ToString();
                Session["Img_Icon_"] = dt.Rows[0]["PhotoName"].ToString();
                img_Photo.Src = "/" + Session["Img_Icon_"].ToString();
                Session["Img_Signature"] = dt.Rows[0]["Img_Signature_"].ToString();
                img_Signature.Src = "/" + Session["Img_Signature"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsLeave"]))
                    FHide(false);
                else
                    FHide(true);

                txtDateAdd.Text = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]).ToString("yyyy-MM-dd");
                chkListWorkingDays.ClearSelection();

                Model_EmployeeWorkingDay_ MEWD = new Model_EmployeeWorkingDay_();
                MEWD.IDCheck = "GetByIDEmp";
                MEWD.EmployeeId = new Guid(Request.QueryString["ID"]);
                MEWD.DayName = string.Empty;
                MEWD.IsActive = true;
                DataTable dtDay = new DataTable();
                Repostry_EmployeeWorkingDay_ REWD = new Repostry_EmployeeWorkingDay_();
                dtDay = REWD.BErp_EmployeeWorkingDay_Manage(MEWD);
                if (dtDay.Rows.Count > 0)
                {
                    foreach (DataRow row in dtDay.Rows)
                    {
                        foreach (ListItem item in chkListWorkingDays.Items)
                        {
                            string currencyId = row.Field<string>("DayName");
                            if (item.Value == currencyId.ToString())
                            {
                                item.Selected = true;
                                break;
                            }
                        }
                    }
                }
                if (dt.Rows[0]["PhotoName"].ToString() != string.Empty)
                {
                    string _FilePath = "~/" + dt.Rows[0]["PhotoName"].ToString();
                    imgPhoto.Src = _FilePath;
                    divViewPhoto.Style.Add("display", "block");
                    hfPhoto.Value = _FilePath;
                }
                FGetEmployeeAttachment(new Guid(Request.QueryString["ID"]));
            }
            else
                Response.Redirect("PageEmployee.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageEmployee.aspx");
        }
    }

    private void FGetEmployeeAttachment(Guid XID)
    {
        try
        {
            Model_EmployeeAttachment_ MEA = new Model_EmployeeAttachment_();
            MEA.IDCheck = "GetByIDEmp";
            MEA.EmployeeId = XID;
            MEA.Name = string.Empty;
            MEA.IsActive = true;

            DataTable dt = new DataTable();
            Repostry_EmployeeAttachment_ REA = new Repostry_EmployeeAttachment_();
            dt = REA.BErp_EmployeeAttachment_Manage(MEA);
            if (dt.Rows.Count > 0)
            {
                GVEmployeeAttachment.DataSource = dt;
                GVEmployeeAttachment.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                btnDelete.Visible = true;
                pnlNull.Visible = false;
                pnlEdit.Visible = true;
                pnlAdd.Visible = false;
            }
            else
            {
                btnDelete.Visible = false;
                pnlNull.Visible = true;
                pnlEdit.Visible = true;
                pnlAdd.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
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
                string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/ImgAdmin/"), XRandom + ".png");
                thumbImg.Save(theFileName, image.RawFormat);
                Session["Img_Icon_"] = "ImgSystem/ImgAdmin/" + XRandom + ".png";
                FChackImgSignatureF();
            }
        }
        else
        {
            if (Request.QueryString["id"] == null)
                Session["Img_Icon_"] = "ImgSystem/ImgAdmin/no-img.jpg";
            FChackImgSignatureF();
        }
    }

    private void FChackImgSignatureF()
    {
        if (FAddImgSignature.HasFile)
        {
            string[] validFileTypes = { "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
            string ext = Path.GetExtension(FAddImgSignature.PostedFile.FileName);
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
                FUpimgSignature(FAddImgSignature);
        }
        else
            FUpimgSignature(FAddImgSignature);
    }

    protected void FUpimgSignature(FileUpload upl)
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
                string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/ImgSignature/"), XRandom + ".png");
                thumbImg.Save(theFileName, image.RawFormat);
                Session["Img_Signature"] = "ImgSystem/ImgSignature/" + XRandom + ".png";
                FAddEmp();
            }
        }
        else
            FAddEmp();
    }

    private void FAddEmp()
    {
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        int XIDAdd = Test_Saddam.FGetIDUsiq();
        bool XCheckGender = false;
        if (rbtnMale.Checked && rbtnFeMale.Checked == false)
            XCheckGender = true;
        else if (rbtnMale.Checked == false && rbtnFeMale.Checked)
            XCheckGender = false;
        string XName = string.Empty;
        if (Request.QueryString["ID"] == null)
        {
            Session["Img_Signature"] = "Cpanel/loader.gif";
            Model_Employee_ ME = new Model_Employee_()
            {
                IDCheck = "Add",
                EmployeeID = new Guid(DLAdmin.SelectedValue),
                EmployeeTypeId = new Guid(ddlEmployeeType.SelectedValue),
                EmployeeGradeId = new Guid(ddlEmployeeGrade.SelectedValue),
                DepartmentId = new Guid(ddlDepartment.SelectedValue),
                DesignationId = new Guid(ddlDesignation.SelectedValue),
                ShiftId = new Guid(ddlShift.SelectedValue),
                FirstName = txtFirstName.Text.Trim(),
                MiddleName = txtMiddleName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                BirthDate = txtBirthDate.Text.Trim(),
                FatherName = txtFatherName.Text.Trim(),
                IsGender = XCheckGender,
                MaratialStatus = ddlMaratialStatus.SelectedValue,
                Cast = txtCast.Text.Trim(),
                PhotoName = Session["Img_Icon_"].ToString(),
                CountryId = new Guid(ddlCountry.SelectedValue),
                StateId = new Guid(ddlState.SelectedValue),
                City = txtCity.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                PinCode = txtPinCode.Text.Trim(),
                MobileNo = txtMobile.Text.Trim(),
                PhoneNo = txtPhone.Text.Trim(),
                JoinDate = txtJoinDate.Text.Trim(),
                EmployeeNo = Convert.ToInt32(txtPFNumber.Text.Trim()),
                PFNo = "0",
                Email = txtEmail.Text.Trim(),
                BankName = txtBankName.Text.Trim(),
                BranchName = txtBranchName.Text.Trim(),
                AccountName = txtAccountName.Text.Trim(),
                AccountNo = txtAccountNumber.Text.Trim(),
                CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedBy = XIDAdd,
                ModifiedBy = 0,
                ModifiedDate = XDate,
                IsActive = true,
                IsLeave = false,
                LeaveDate = XDate,
                LeaveDescription = string.Empty,
                Img_Signature = Session["Img_Signature"].ToString()
            };
            //Signature
            Repostry_Employee_ RE = new Repostry_Employee_();
            string Xresult = RE.FErp_Employee_Add(ME);
            if (Xresult == "IsExistsAdded")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                return;
            }
            else if (Xresult == "IsExistsEmployeeNoAdded")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رقم الموظف مستخدم لشخص آخر ... ";
                return;
            }
            else if (Xresult == "IsExistsEmailAdded")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "البريد الإلكتروني مستخدم لشخص آخر ... ";
                return;
            }
            else if (Xresult == "IsSuccessAdded")
            {
                foreach (ListItem _ListItem in chkListWorkingDays.Items)
                {
                    if (_ListItem.Selected == true)
                    {
                        Model_EmployeeWorkingDay_ MEWD = new Model_EmployeeWorkingDay_()
                        {
                            IDCheck = "Add",
                            EmployeeWorkingDayMapID = Guid.NewGuid(),
                            EmployeeId = ME.EmployeeID,
                            DayName = _ListItem.Value,
                            CreatedDate = XDate,
                            CreatedBy = XIDAdd,
                            ModifiedBy = 0,
                            ModifiedDate = XDate,
                            IsActive = true,
                        };
                        Repostry_EmployeeWorkingDay_ REWD = new Repostry_EmployeeWorkingDay_();
                        REWD.FErp_EmployeeWorkingDay_Add(MEWD);
                    }
                }
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
                FChackFileAttach(fuResume, ME.EmployeeID, lblResume.Text, lblResume.Text);
                FChackFileAttach(fuOfferLetter, ME.EmployeeID, lblOfferLetter.Text, lblOfferLetter.Text);
                FChackFileAttach(fuJoiningLetter, ME.EmployeeID, lblJoiningLetter.Text, lblJoiningLetter.Text);
                FChackFileAttach(fuContractPaper, ME.EmployeeID, lblContractPaper.Text, lblContractPaper.Text);
                FChackFileAttach(fuIDProff, ME.EmployeeID, lblIDProff.Text, lblIDProff.Text);
                FChackFileAttach(fuOtherDocument, ME.EmployeeID, lblOtherDocument.Text, lblOtherDocument.Text);
                XName = ME.FirstName + ' ' + ME.MiddleName + ' ' + ME.LastName + ' ' + ME.FatherName;
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "إضافة", "إضافة الموظف " + XName, XDate);
                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة موظف" + "\n" + "بإسم :" + XName + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Add", XIDAdd);
            }
        }
        else if (Request.QueryString["ID"] != null)
        {
            Model_Employee_ ME = new Model_Employee_()
            {
                IDCheck = "Edit",
                EmployeeID = new Guid(DLAdmin.SelectedValue),
                EmployeeTypeId = new Guid(ddlEmployeeType.SelectedValue),
                EmployeeGradeId = new Guid(ddlEmployeeGrade.SelectedValue),
                DepartmentId = new Guid(ddlDepartment.SelectedValue),
                DesignationId = new Guid(ddlDesignation.SelectedValue),
                ShiftId = new Guid(ddlShift.SelectedValue),
                FirstName = txtFirstName.Text.Trim(),
                MiddleName = txtMiddleName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                BirthDate = txtBirthDate.Text.Trim(),
                FatherName = txtFatherName.Text.Trim(),
                IsGender = XCheckGender,
                MaratialStatus = ddlMaratialStatus.SelectedValue,
                Cast = txtCast.Text.Trim(),
                PhotoName = Session["Img_Icon_"].ToString(),
                CountryId = new Guid(ddlCountry.SelectedValue),
                StateId = new Guid(ddlState.SelectedValue),
                City = txtCity.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                PinCode = txtPinCode.Text.Trim(),
                MobileNo = txtMobile.Text.Trim(),
                PhoneNo = txtPhone.Text.Trim(),
                JoinDate = txtJoinDate.Text.Trim(),
                EmployeeNo = Convert.ToInt32(txtPFNumber.Text.Trim()),
                PFNo = "0",
                Email = txtEmail.Text.Trim(),
                BankName = txtBankName.Text.Trim(),
                BranchName = txtBranchName.Text.Trim(),
                AccountName = txtAccountName.Text.Trim(),
                AccountNo = txtAccountNumber.Text.Trim(),
                CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedBy = 0,
                ModifiedBy = XIDAdd,
                ModifiedDate = XDate,
                IsActive = true,
                IsLeave = false,
                LeaveDate = XDate,
                LeaveDescription = string.Empty,
                Img_Signature = Session["Img_Signature"].ToString()
            };
            Repostry_Employee_ RE = new Repostry_Employee_();
            string Xresult = RE.FErp_Employee_Add(ME);
            if (Xresult == "IsExistsEdited")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                return;
            }
            else if (Xresult == "IsExistsEmployeeNoEdited")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رقم الموظف مستخدم لشخص آخر ... ";
                return;
            }
            else if (Xresult == "IsExistsEmailEdited")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "البريد الإلكتروني مستخدم لشخص آخر ... ";
                return;
            }
            else if (Xresult == "IsSuccessEdit")
            {
                Model_EmployeeWorkingDay_ MEWDelete = new Model_EmployeeWorkingDay_()
                {
                    IDCheck = "Delete",
                    EmployeeWorkingDayMapID = Guid.NewGuid(),
                    EmployeeId = ME.EmployeeID,
                    DayName = string.Empty,
                    CreatedDate = XDate,
                    CreatedBy = XIDAdd,
                    ModifiedBy = XIDAdd,
                    ModifiedDate = XDate,
                    IsActive = true,
                };
                Repostry_EmployeeWorkingDay_ REWDelete = new Repostry_EmployeeWorkingDay_();
                REWDelete.FErp_EmployeeWorkingDay_Add(MEWDelete);

                foreach (ListItem _ListItem in chkListWorkingDays.Items)
                {
                    if (_ListItem.Selected == true)
                    {
                        Model_EmployeeWorkingDay_ MEWD = new Model_EmployeeWorkingDay_()
                        {
                            IDCheck = "Add",
                            EmployeeWorkingDayMapID = Guid.NewGuid(),
                            EmployeeId = ME.EmployeeID,
                            DayName = _ListItem.Value,
                            CreatedDate = XDate,
                            CreatedBy = XIDAdd,
                            ModifiedBy = XIDAdd,
                            ModifiedDate = XDate,
                            IsActive = true,
                        };
                        Repostry_EmployeeWorkingDay_ REWD = new Repostry_EmployeeWorkingDay_();
                        REWD.FErp_EmployeeWorkingDay_Add(MEWD);
                    }
                }
                FGetData();
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
                XName = ME.FirstName + ' ' + ME.MiddleName + ' ' + ME.LastName + ' ' + ME.FatherName;
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "تعديل", "تعديل الموظف " + XName, XDate);

                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل موظف" + "\n" + "بإسم :" + XName + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Edit", XIDAdd);
            }
        }
    }

    private void FChackFileAttach(FileUpload upload, Guid XIDEmp, string XTitle, string XDescription)
    {
        if (upload.HasFile)
        {
            string[] validFileTypes = { "pdf", "PDF" };
            string ext = Path.GetExtension(upload.PostedFile.FileName);
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
                lblWarning.Text = "المسموح فقط " + string.Join(",", validFileTypes);
                return;
            }
            else
                FUpFileAttach(upload, XIDEmp, XTitle, XDescription);
        }
    }

    protected void FUpFileAttach(FileUpload upl, Guid XIDEmp, string XTitle, string XDescription)
    {
        if (upl.HasFile)
        {
            string XAttachmentName = string.Empty;
            string XRandom = Convert.ToString(Guid.NewGuid()).Replace("-", "");
            string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/FileAdmin/"), XRandom + "." + upl.PostedFile.FileName);
            upl.SaveAs(theFileName);
            XAttachmentName = "ImgSystem/FileAdmin/" + XRandom + "." + upl.PostedFile.FileName;
            FAddEmployeeAttachment(XIDEmp, XTitle, XDescription, XAttachmentName);
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم رفع الملف بنجاح ... ";
        }
    }

    private void FAddEmployeeAttachment(Guid XIDEmp, string XTitle, string XDescription, string XAttachmentName)
    {
        Model_EmployeeAttachment_ MEA = new Model_EmployeeAttachment_()
        {
            IDCheck = "Add",
            EmployeeAttachmentMapID = Guid.NewGuid(),
            EmployeeId = XIDEmp,
            Name = XTitle,
            Description = XDescription,
            AttachmentName = XAttachmentName,
            CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            ModifiedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsActive = true,
        };
        Repostry_EmployeeAttachment_ REA = new Repostry_EmployeeAttachment_();
        REA.FErp_EmployeeAttachment_Add(MEA);
        if (Request.QueryString["ID"] != null)
            FGetEmployeeAttachment(new Guid(Request.QueryString["ID"]));
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployee.aspx");
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeAdd.aspx");
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtBankName.Focus();
        Repostry_State_.FErp_State_Manage(new Guid(ddlCountry.SelectedValue), ddlState);
    }

    protected void LinkDownload_Click(object sender, EventArgs e)
    {
        string filename = "~/" + Convert.ToString((((LinkButton)sender).CommandArgument)).ToString();
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
                Response.Write("This file does not exist.");
        }
    }

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        FChackFileAttach(FUEdit, new Guid(Request.QueryString["ID"]), txt_Name_Edit.Text, txt_Name_Edit.Text);
        Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "إضافة", "إرفاق " + txt_Name_Edit.Text.Trim() + " لـ " +
            txtFirstName.Text.Trim() + ' ' + txtMiddleName.Text.Trim() + ' ' + txtLastName.Text.Trim() + ' ' + txtFatherName.Text.Trim(),
            ClassSaddam.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss"));
        btnAdd.Focus();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVEmployeeAttachment.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmployeeAttachment.DataKeys[row.RowIndex].Value);
                    Model_EmployeeAttachment_ MEA = new Model_EmployeeAttachment_()
                    {
                        IDCheck = "Delete",
                        EmployeeAttachmentMapID = new Guid(Comp_ID),
                        EmployeeId = new Guid(Comp_ID),
                        Name = string.Empty,
                        Description = string.Empty,
                        AttachmentName = string.Empty,
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false,
                    };
                    Repostry_EmployeeAttachment_ REA = new Repostry_EmployeeAttachment_();
                    REA.FErp_EmployeeAttachment_Add(MEA);
                }
            }
            if (Request.QueryString["ID"] != null)
                FGetEmployeeAttachment(new Guid(Request.QueryString["ID"]));
            btnAdd.Focus();
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم حذف الملفات بنجاح ... ";
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblSuccess.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

}