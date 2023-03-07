using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageEmployeeResignSave : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A152");
            if (Request.QueryString["ID"] != null)
            {
                FGetData();
                FChangeTitles();
            }
        }
    }

    private void FChangeTitles()
    {
        if (Request.QueryString["Type"] != null)
        {
            if (Request.QueryString["Type"] == "Leave")
            {
                lbmsg.Text = "ملف إستقالة الموظف";
                lblDate.Text = "تاريخ الإستقالة";
                lblfile.Text = "ملف الإستقالة";
                txtLeaveDescription.Text = " ملف إستقالة الموظف : " + lbl_Name.Text;
            }
            else if (Request.QueryString["Type"] == "Come")
            {
                lbmsg.Text = "ملف إلغاء إستقالة الموظف";
                lblDate.Text = "تاريخ إلغاء الإستقالة";
                lblfile.Text = "ملف إلغاء الإستقالة";
                txtLeaveDescription.Text = " ملف إلغاء إستقالة الموظف : " + lbl_Name.Text;
            }
        }
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
                lbl_Name.Text = dt.Rows[0]["_Name"].ToString();
                txtDateResign.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            }
            else
                Response.Redirect("PageEmployee.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageEmployee.aspx");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["Type"] == "Leave")
                FAddEmp(true, "ملف الإستقالة", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
            else if (Request.QueryString["Type"] == "Come")
                FAddEmp(false, "ملف إلغاء الإستقالة", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
            
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = " خطأ غير متوقع , حاول لاحقاً ";
            return;
        }
    }

    private void FAddEmp(bool XValue, string XText, string XDate)
    {
        string XIDU = Guid.NewGuid().ToString();
        if (Request.QueryString["ID"] != null)
        {
            Model_Employee_ ME = new Model_Employee_()
            {
                IDCheck = "EditByResign",
                EmployeeID = new Guid(Request.QueryString["ID"]),
                EmployeeTypeId = new Guid(XIDU),
                EmployeeGradeId = new Guid(XIDU),
                DepartmentId = new Guid(XIDU),
                DesignationId = new Guid(XIDU),
                ShiftId = new Guid(XIDU),
                FirstName = string.Empty,
                MiddleName = string.Empty,
                LastName = string.Empty,
                BirthDate = string.Empty,
                FatherName = string.Empty,
                IsGender = false,
                MaratialStatus = string.Empty,
                Cast = string.Empty,
                PhotoName = string.Empty,
                CountryId = new Guid(XIDU),
                StateId = new Guid(XIDU),
                City = string.Empty,
                Address = string.Empty,
                PinCode = string.Empty,
                MobileNo = string.Empty,
                PhoneNo = string.Empty,
                JoinDate = string.Empty,
                EmployeeNo = Convert.ToInt32(0),
                PFNo = string.Empty,
                Email = string.Empty,
                BankName = string.Empty,
                BranchName = string.Empty,
                AccountName = string.Empty,
                AccountNo = string.Empty,
                CreatedDate = XDate,
                CreatedBy = Test_Saddam.FGetIDUsiq(),
                ModifiedBy = 0,
                ModifiedDate = XDate,
                IsActive = true,
                IsLeave = XValue,
                LeaveDate = Convert.ToDateTime(txtDateResign.Text).ToString("yyyy-MM-dd"),
                LeaveDescription = txtLeaveDescription.Text.Trim(),
                Img_Signature = string.Empty
            };
            Repostry_Employee_ RE = new Repostry_Employee_();
            string Xresult = RE.FErp_Employee_Add(ME);
            if (Xresult == "IsSuccessEditByResign")
            {
                Model_EmployeeAttachment_ MEA = new Model_EmployeeAttachment_()
                {
                    IDCheck = "DeleteGo",
                    EmployeeAttachmentMapID = Guid.NewGuid(),
                    EmployeeId = new Guid(Request.QueryString["ID"]),
                    Name = XText,
                    Description = string.Empty,
                    AttachmentName = string.Empty,
                    CreatedDate = XDate,
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedBy = 0,
                    ModifiedDate = XDate,
                    IsActive = false,
                };
                Repostry_EmployeeAttachment_ REA = new Repostry_EmployeeAttachment_();
                REA.FErp_EmployeeAttachment_Add(MEA);
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تقديم الإستقالة بنجاح ... ";
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "إضافة", txtLeaveDescription.Text.Trim(), ClassSaddam.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss"));

                FChackFileAttach(fuJoiningLetter, XText, XText);
                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إستقالة الموظف" + "\n" + "بإسم :" + lbl_Name.Text + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Add", Test_Saddam.FGetIDUsiq());
            }
        }
    }

    private void FChackFileAttach(FileUpload upload,  string XTitle, string XDescription)
    {
        if (upload.HasFile)
        {
            string[] validFileTypes = { "pdf", "PDF"};
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
                FUpFileAttach(upload, XTitle, XDescription);
        }
    }

    protected void FUpFileAttach(FileUpload upl, string XTitle, string XDescription)
    {
        if (upl.HasFile)
        {
            string XAttachmentName = string.Empty;
            string XRandom = Convert.ToString(Guid.NewGuid()).Replace("-", "");
            string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/FileAdmin/"), XRandom + "." + upl.PostedFile.FileName);
            upl.SaveAs(theFileName);
            XAttachmentName = "ImgSystem/FileAdmin/" + XRandom + "." + upl.PostedFile.FileName;
            FAddEmployeeAttachment(XTitle, XDescription, XAttachmentName);
        }
    }

    private void FAddEmployeeAttachment(string XTitle, string XDescription, string XAttachmentName)
    {
        Model_EmployeeAttachment_ MEA = new Model_EmployeeAttachment_()
        {
            IDCheck = "Add",
            EmployeeAttachmentMapID = Guid.NewGuid(),
            EmployeeId = new Guid(Request.QueryString["ID"]),
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
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployee.aspx");
    }

}