using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_JobAssignments_PageEmployeeJobAssignmentDetails : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    string IDUniq = string.Empty, IDUser = string.Empty;
    private void GetCookie()
    {
        try
        {
            HttpCookie Cooke;  // رقم المستخدم
            Cooke = HttpContext.Current.Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
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
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            Repostry_Employee_.FErp_Employee_Master_Manage(CBEmployee);
            if (XType == "Manager")
            {
                Repostry_JobAssignment_.FGetDropList(1, "GetByDropByYears", Guid.Empty, new Guid(ddlYears.SelectedValue), 0,
                    string.Empty, string.Empty, string.Empty, false, false, true, DLJobAssignment);
            }
            else if (XType == "Admin")
            {
                GetCookie();
                Repostry_JobAssignment_Map_.FGetDropList(1, "GetByDropByAdminActiveByYears", 1000, Guid.Empty, new Guid(ddlYears.SelectedValue),
                    new Guid(IDUniq), Guid.Empty, IDUser,
                    ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd"), string.Empty, false, true, DLJobAssignment);
            }
            if (Request.QueryString["ID"] != null)
            {
                DLJobAssignment.SelectedValue = Request.QueryString["ID"];
                FGetData();
            }
        }
    }

    private void FGetData()
    {
        try
        {
            lblStatus.Text = string.Empty;
            DataTable dt = new DataTable();
            dt = Repostry_JobAssignment_.FGetDataInDataTable("GetByIDUniq", new Guid(DLJobAssignment.SelectedValue), Guid.Empty, 0,
                string.Empty, string.Empty, string.Empty, false, false, true);
            if (dt.Rows.Count > 0)
            {
                FGetByIDJobAssignment();
                pnlDetails.Visible = true; FGetEmp();
                HFIDNumber.Value = dt.Rows[0]["Number_Job_"].ToString();
                //lblEmp.Text = dt.Rows[0]["_Name"].ToString();
                lblNumberJob.Text = HFIDNumber.Value;
                lblNumberJob2.Text = HFIDNumber.Value;
                //HFName.Value = dt.Rows[0]["_PartName"].ToString();
                lblAssignment_Title.Text = dt.Rows[0]["Assignment_Title_"].ToString();
                lblAssignment_Title2.Text = lblAssignment_Title.Text;
                lblThe_Assignment.Text = dt.Rows[0]["The_Assignment_"].ToString();
                lblThe_Assignment2.Text = lblThe_Assignment.Text;
                lblHours_In_Day.Text = dt.Rows[0]["Hours_In_Day_"].ToString();
                lblHours_In_Day2.Text = lblHours_In_Day.Text;
                HFStartDate.Value = Convert.ToDateTime(dt.Rows[0]["Date_Job_"]).ToString("yyyy-MM-dd");
                lblDate_Job.Text = HFStartDate.Value;
                lblDate_Job2.Text = HFStartDate.Value;
                txtDate_Job.Text = HFStartDate.Value;
                HFEndDate.Value = Convert.ToDateTime(dt.Rows[0]["Date_End_Job_"]).ToString("yyyy-MM-dd");
                lblDateEnd_Job.Text = HFEndDate.Value;
                lblDateEnd_Job2.Text = HFEndDate.Value;
                txtDateEnd_Job.Text = HFEndDate.Value;
                lblThe_Qriah.Text = dt.Rows[0]["The_Qriah_"].ToString();
                lblThe_Qriah2.Text = lblThe_Qriah.Text;
                HFPhoneManager.Value = ClassAdmin_Arn.FGetPhoneByID(Convert.ToInt32(dt.Rows[0]["CreatedBy"]));
                //HFPhoneAdmin.Value = dt.Rows[0]["MobileNo"].ToString();
                HFEmp_Allow.Value = dt.Rows[0]["Is_Emp_Allow_"].ToString();
                HFEnd.Value = dt.Rows[0]["Is_End_"].ToString();
                HFStope.Value = dt.Rows[0]["Is_Stoped_"].ToString();
                HFConvert.Value = dt.Rows[0]["Is_Convert_"].ToString();
                FGetDetails();
                txt_Message.Focus();
                if (Convert.ToBoolean(HFEmp_Allow.Value) && Convert.ToBoolean(HFEnd.Value) == false)
                    IDSuccess.Visible = true;
                else
                    IDSuccess.Visible = false;
                if (Convert.ToBoolean(HFStope.Value))
                { IDActive.Visible = true; IDStop.Visible = false; }
                else
                { IDActive.Visible = false; IDStop.Visible = true; }
                if (Convert.ToBoolean(HFConvert.Value))
                { IDOption.Visible = false; pnlConvert.Visible = false; pnlConverted.Visible = true; }
                else
                {
                    if (XType == "Manager")
                        IDOption.Visible = true;
                    else if (XType == "Admin")
                        IDOption.Visible = false;
                    pnlConvert.Visible = true; pnlConverted.Visible = false;
                }
                lblStatus.Text = ClassSaddam.FCheckStatus((DateTime)(dt.Rows[0]["Date_Job_"]), (DateTime)(dt.Rows[0]["Date_End_Job_"]),
                                                                (bool)(dt.Rows[0]["Is_Emp_Allow_"]), (bool)(dt.Rows[0]["Is_Emp_Deny_"]),
                                                                (bool)(dt.Rows[0]["Is_Moder_Allow_"]), false,
                                                                (bool)(dt.Rows[0]["Is_Moder_Not_Allow_"]), (bool)(dt.Rows[0]["Is_Stoped_"]),
                                                                (bool)(dt.Rows[0]["Is_End_"]), (bool)(dt.Rows[0]["Is_Convert_"]));

                if (Convert.ToBoolean(dt.Rows[0]["Is_Extension_"]))
                {
                    lblExtension.Text = "<br /><br /><span style='border-radius:6px; font-size:12px; background-color:#d80303; color:#FFF; padding: 0 2px 0 2px; margin-left:2px'> تم تمديد التأريخ </span>";
                    lblExtension.Text += "<br /><br /> من تاريخ : ";
                    lblExtension.Text += "<br /><span style='font-size:12px;'>" + Convert.ToDateTime(dt.Rows[0]["Old_Date_Extension_"]).ToString("yyyy/MM/dd") + " - " +
                        Convert.ToDateTime(dt.Rows[0]["Old_Date_End_Extension_"]).ToString("yyyy/MM/dd") + "</span>";

                    lblExtension.Text += "<br /><br /> إلى تاريخ : ";
                    lblExtension.Text += "<br /><span style='font-size:12px;'>" + Convert.ToDateTime(dt.Rows[0]["Date_Job_"]).ToString("yyyy/MM/dd") + " - " +
                        Convert.ToDateTime(dt.Rows[0]["Date_End_Job_"]).ToString("yyyy/MM/dd") + "</span>";
                }
            }
            else
            { pnlDetails.Visible = false; pnlEmp.Visible = false; }
        }
        catch (Exception)
        {
            Response.Redirect("PageEmployeeJobAssignments.aspx");
        }
    }

    private void FGetDetails()
    {
        DataTable dt = new DataTable();
        dt = Repostry_JobAssignment_Attachments_.FGetDataInDataTable("GetByIDJobAssignment", 1000, new Guid(DLJobAssignment.SelectedValue), string.Empty, true);
        if (dt.Rows.Count > 0)
        {
            RPTMessage.DataSource = dt;
            RPTMessage.DataBind();
            pnlNull.Visible = false;
            pnlData.Visible = true;
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
        }
    }

    private void FGetEmp()
    {
        if (XType == "Admin")
        {
            GetCookie();
            DataTable dtView = new DataTable();
            dtView = Repostry_JobAssignment_Map_.FGetDataInDataTable("GetByIDUniq", 1, Guid.Empty, Guid.Empty,
                new Guid(IDUniq), new Guid(DLJobAssignment.SelectedValue), string.Empty, string.Empty, string.Empty, false, true);
            if (dtView.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtView.Rows[0]["Is_View_"]) == false)
                {
                    Repostry_JobAssignment_Map_.FAPP("View", Guid.Empty, Guid.Empty, new Guid(IDUniq),
                        new Guid(DLJobAssignment.SelectedValue), true, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), 0, 0, string.Empty, false);
                }
            }
        }
        DataTable dt = new DataTable();
        dt = Repostry_JobAssignment_Map_.FGetDataInDataTable("GetByIDJobAssignment", 1000, Guid.Empty, Guid.Empty,
            Guid.Empty, new Guid(DLJobAssignment.SelectedValue), string.Empty, string.Empty, string.Empty, false, true);
        if (dt.Rows.Count > 0)
        {
            RPTJobAssignment_Map.DataSource = dt;
            RPTJobAssignment_Map.DataBind();
            pnlEmp.Visible = true;
        }
        else
            pnlEmp.Visible = false;
    }

    private void FGetByIDJobAssignment()
    {
        DataTable dt = new DataTable();
        dt = Repostry_JobAssignment_Map_.FGetDataInDataTable("GetByIDJobAssignment", 1000, Guid.Empty, Guid.Empty,
            Guid.Empty, new Guid(Request.QueryString["ID"]), string.Empty, string.Empty, string.Empty, false, true);
        if (dt.Rows.Count > 0)
        {
            List<string> selectedValues = CBEmployee.Items.Cast<ListItem>()
                                    .Select(li => li.Value)
                                    .ToList();
            foreach (DataRow dr in dt.Rows)
            {
                if (selectedValues.Contains(dr["Employee_Id_"].ToString()))
                    CBEmployee.Items.FindByValue(dr["Employee_Id_"].ToString()).Selected = true;
            }
        }
    }

    protected void DLJobAssignment_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData();
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeJobAssignmentDetails.aspx");
    }

    public string FStyle(string XValue)
    {
        string XResult = string.Empty;
        if (XType == "Manager")
        {
            if (XValue == "Admin")
                XResult = "style='background-color:#f0f0f0; padding:2px 7px 2px 2px; border-top-right-radius:20px; border-bottom-left-radius: 20px; border-bottom-right-radius: 20px; margin-Right:20%'";
            else if (XValue == "Manager")
                XResult = "style='background-color:#bcff9d; padding:2px 7px 2px 2px; border-top-left-radius:20px; border-bottom-left-radius: 20px; border-bottom-right-radius: 20px; margin-left:20%'";
        }
        else if (XType == "Admin")
        {
            if (XValue == "Manager")
                XResult = "style='background-color:#f0f0f0; padding:2px 7px 2px 2px; border-top-right-radius:20px; border-bottom-left-radius: 20px; border-bottom-right-radius: 20px; margin-Right:20%'";
            else if (XValue == "Admin")
                XResult = "style='background-color:#bcff9d; padding:2px 7px 2px 2px; border-top-left-radius:20px; border-bottom-left-radius: 20px; border-bottom-right-radius: 20px; margin-left:20%'";
        }
        
        return XResult;
    }

    protected void LBImage_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FChackFile();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FChackFile()
    {
        if (FUFiles.HasFile)//xlsx
        {
            int fileSize = FUFiles.PostedFile.ContentLength;
            if (fileSize < 10485760)
            {
                //string[] validFileTypes = { "pdf", "PDF" };
                string[] validFileTypes = { "pdf", "PDF", "bmp", "BMP", "gif", "GIF", "png", "PNG", "jpg", "JPG", "jpeg", "JPEG" };
                string ext = Path.GetExtension(FUFiles.PostedFile.FileName);
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
                    lblWarning.Text = "المسموح فقط ملفات من نوع ( " + string.Join(",", validFileTypes) + " ) ";
                    return;
                }
                else
                    FUpimg(FUFiles);
            }
            else
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblWarning.Text = " يجب أن يكون حجم الملف أقل من 10 m";
                return;
            }
        }
        else
            FAPP(string.Empty, string.Empty);
    }

    string _TheFile = "---";
    protected void FUpimg(FileUpload upl)
    {
        if (upl.HasFile)
        {
            DateTime XDateGetCurrentTime = ClassSaddam.GetCurrentTime();
            FAddFolder_ToDay(XDateGetCurrentTime.ToString("yyyy_MM_dd"), XDateGetCurrentTime.ToString("MM"), XDateGetCurrentTime.ToString("yyyy"));

            string XRandom = Convert.ToString(Guid.NewGuid()).Replace("-", "");
            string fileExtension = Path.GetExtension(upl.PostedFile.FileName);
            string XDate = "BerArn" + "/" + XDateGetCurrentTime.ToString("yyyy") + "/" + XDateGetCurrentTime.ToString("MM") + "/" + XDateGetCurrentTime.ToString("yyyy_MM_dd");
            string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/FilesJobAssignment/" + XDate + "/"), XRandom + fileExtension);
            upl.SaveAs(theFileName);
            _TheFile = "ImgSystem/FilesJobAssignment/" + XDate + "/" + XRandom + fileExtension;
            FAPP(fileExtension, FUFiles.PostedFile.ContentLength.ToString());
        }
    }

    private void FAddFolder_ToDay(string Xdate, string XMounth, string XYears)
    {
        string XNameServer = "BerArn";
        string XHost = Server.MapPath("~/ImgSystem/FilesJobAssignment/" + XNameServer);
        if (!Directory.Exists(XHost))
            Directory.CreateDirectory(XHost);
        string folderYears = Server.MapPath("~/ImgSystem/FilesJobAssignment/" + XNameServer + "/" + XYears);
        string folderMounth = Server.MapPath("~/ImgSystem/FilesJobAssignment/" + XNameServer + "/" + XYears + "/" + XMounth);
        string folderDay = Server.MapPath("~/ImgSystem/FilesJobAssignment/" + XNameServer + "/" + XYears + "/" + XMounth + "/" + Xdate);
        if (!Directory.Exists(folderYears))
        {
            Directory.CreateDirectory(folderYears);
            Directory.CreateDirectory(folderMounth);
            Directory.CreateDirectory(folderDay);
        }
        else if (Directory.Exists(folderYears))
        {
            if (!Directory.Exists(folderMounth))
            {
                Directory.CreateDirectory(folderMounth);
                Directory.CreateDirectory(folderDay);
            }
            else if (Directory.Exists(folderMounth))
            {
                if (!Directory.Exists(folderDay))
                    Directory.CreateDirectory(folderDay);
            }
        }
    }

    private void FAPP(string XType_File, string XSize_File)
    {
        string Xresult = Repostry_JobAssignment_Attachments_.FAPP("Add", Guid.NewGuid(), new Guid(DLJobAssignment.SelectedValue),
            XType, txt_Message.Text.Trim().Replace(Environment.NewLine, "<br />"), _TheFile, XType_File, XSize_File, Test_Saddam.FGetIDUsiq(),
            ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), true);
        if (Xresult == "IsExists")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
            return;
        }
        else if (Xresult == "IsSuccess")
        {
            FGetData();
            txt_Message.Text = string.Empty;
            txt_Message.Focus();
            Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "تحديث", 
                "متابعة المهمة برقم " + lblNumberJob.Text.Trim(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));

            if (XType == "Manager")
            {
                foreach (ListItem LI in CBEmployee.Items)
                {
                    if (LI.Selected)
                    {
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(LI.Text.Split(new char[] { '[', ']' })[1], "توجد ملاحظة على" + "\n" + "المهام رقم : " + 
                            HFIDNumber.Value + "\n" + "يُرجى المتابعة ,,,", "BerArn", "Add", Test_Saddam.FGetIDUsiq());
                    }
                }
            }
            else if (XType == "Admin")
                Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhoneManager.Value, "قام " + HFName.Value +" بالرد على" + "\n" + "المهام رقم : " + HFIDNumber.Value + "\n" + "يُرجى المتابعة ,,,", "BerArn", "Add", Test_Saddam.FGetIDUsiq());
        }
    }

    protected void LB_Success_Click(object sender, EventArgs e)
    {
        FAllowOrDeny("EndByAdmin", "_Active", false, false, true, true, false, false, string.Empty);
        Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "تحديث", 
            "المهمة برقم " + lblNumberJob.Text.Trim() + " تحديد أنها أُنجزت", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
    }

    protected void LB_SuccessWithComment_Click(object sender, EventArgs e)
    {
        FAllowOrDeny("EndByAdmin", "_Active", false, false, true, false, true, false, txtComment.Text.Trim().Replace(Environment.NewLine, "<br />"));
        Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "تحديث",
            "المهمة برقم " + lblNumberJob.Text.Trim() + " تحديد أنها أُنجزت مع ملاحظة", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
    }

    protected void LB_NotSuccess_Click(object sender, EventArgs e)
    {
        FAllowOrDeny("EndByAdmin", "_Active", false, false, true, false, false, true, string.Empty);
        Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "تحديث",
            "المهمة برقم " + lblNumberJob.Text.Trim() + " تحديد أنها لم تُنجز", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
    }

    private void FAllowOrDeny(string XScript, string XTypeGet, bool XIsAllow, bool XIsDeny, bool XIsEnd, 
        bool XAllow, bool XAllowWithComment, bool XNotAllow, string XComment)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty;
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            int XIDAdd = Test_Saddam.FGetIDUsiq();
            Xresult = Repostry_JobAssignment_.FAPP(XScript, new Guid(DLJobAssignment.SelectedValue),
                Guid.Empty, Guid.Empty, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, false, 0, 0, 0, string.Empty,
                XIsAllow, XDate, XIsDeny, XDate, false, XIDAdd, XAllow, XNotAllow, XComment, string.Empty, XIDAdd, XAllow, XAllowWithComment, XNotAllow,
                XDate, XComment, false, 0, string.Empty, false, 0, string.Empty, XIsEnd, XDate,
                false, string.Empty, 0, Guid.Empty, false, string.Empty, 0, string.Empty, string.Empty,
                string.Empty, 0, 0, 0, string.Empty, false);
            if (Xresult == "IsSuccess")
            {
                
                if (XTypeGet == "_Active")
                {
                    if (XIsEnd)
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.InnerText = "تم تحديث البيانات بنجاح ... ";
                        FGetData();
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByID(XIDAdd),
                            "قمت بالتحديد على أن" + "\n" + "المهام رقم : " + HFIDNumber.Value + "\n" + "قد أُنجزت" + "\n" + "شكراً لك ,,,", "BerArn", "Add", XIDAdd);
                    }
                    else
                    {
                        IDMessageSuccess.Visible = false;
                        IDMessageWarning.Visible = true;
                        lblWarning.Text = "لم يتم قبول المهمة بعد ... ";
                        return;
                    }
                }
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع حاول لاحقاً ... ";
            return;
        }
    }

    protected void LB_Stop_Click(object sender, EventArgs e)
    {
        FActive("StopedByManager", true, string.Empty, string.Empty, false, string.Empty, string.Empty);
        Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "تحديث",
            "إيقاف المهمة برقم " + lblNumberJob.Text.Trim(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
    }

    protected void LB_Active_Click(object sender, EventArgs e)
    {
        FActive("StopedByManager", false, string.Empty, string.Empty, false, string.Empty, string.Empty);
        Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "تحديث",
            "إلغاء إيقاف المهمة برقم " + lblNumberJob.Text.Trim(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
    }

    private void FActive(string XCheck, bool XValue, string XDate_Job, string XDate_End_Job, bool XIs_Extension, 
                 string XOld_Date_Extension, string XOld_Date_End_Extension)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty;
            int XIDAdd = Test_Saddam.FGetIDUsiq();
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            Xresult = Repostry_JobAssignment_.FAPP(XCheck, new Guid(DLJobAssignment.SelectedValue),
                        Guid.Empty, Guid.Empty, 0, XDate_Job, XDate_End_Job, string.Empty, string.Empty, string.Empty, 0, false, 0, 0, 0, string.Empty, false,
                        string.Empty, false, string.Empty, false, 0, false, false, string.Empty, string.Empty, 0, false, false, false, string.Empty,
                        string.Empty, XValue, XIDAdd, XDate, false, 0, string.Empty, false, string.Empty,
                        false, string.Empty, 0, Guid.Empty, XIs_Extension, XDate, XIDAdd, XOld_Date_Extension, XOld_Date_End_Extension,
                        string.Empty, 0, 0, 0, string.Empty, false);
            if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.InnerText = "تم تحديث البيانات بنجاح ... ";
                FGetData();
                Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByID(Test_Saddam.FGetIDUsiq()),
                    "قام المختص بإيقاف" + "\n" + "المهام رقم : " + HFIDNumber.Value + "\n" + "يُرجى المتابعة ,,,", "BerArn", "Add", Test_Saddam.FGetIDUsiq());
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع حاول لاحقاً ... ";
            return;
        }
    }

    protected void LB_Convert_Click(object sender, EventArgs e)
    {
        IDEmp.Visible = true;
        pnl_Convert.Visible = false;
        pnl_Start.Visible = true;
        pnl_End.Visible = true; LB_Convert_New.Visible = true; LB_Extension_New.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowModelConvert();", true);
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            ddlEmployee.Items.Clear(); ddlEmployee.Items.Add(""); ddlEmployee.AppendDataBoundItems = true;
            Repostry_Employee_.FErp_Employee_Master_Manage(new Guid(ddlDepartment.SelectedValue), ddlEmployee);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowModelConvert();", true);
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowModelConvert();", true);
            return;
        }
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetPhoneAndEmail();
    }

    private void FGetPhoneAndEmail()
    {
        Repostry_Employee_.FGetPhoneAndEmail(ddlEmployee.SelectedValue, HFPhone, HFEmail);
        lblPhone.InnerText = HFPhone.Value;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowModelConvert();", true);
    }

    protected void LB_Convert_New_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty, XresultRecord = string.Empty, XresultNew = string.Empty;
            int XIDAdd = Test_Saddam.FGetIDUsiq();
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            Xresult = Repostry_JobAssignment_.FAPP("Convert", new Guid(DLJobAssignment.SelectedValue),
                        Guid.Empty, Guid.Empty, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, false, 0, 0, 0, string.Empty, false,
                        string.Empty, false, string.Empty, false, 0, false, false, string.Empty, string.Empty, 0, false, false, false, string.Empty,
                        string.Empty, false, 0, XDate, false, 0, string.Empty, false, string.Empty,
                        true, XDate, XIDAdd, Guid.Empty, false, string.Empty, 0, string.Empty, string.Empty, string.Empty, 0, 0, 0, string.Empty, false);
            if (Xresult == "IsSuccess")
            {
                DataTable dt = new DataTable();
                dt = Repostry_JobAssignment_.FGetDataInDataTable("GetByIDUniq", new Guid(DLJobAssignment.SelectedValue), Guid.Empty, 0,
                    string.Empty, string.Empty, string.Empty, false, false, true);
                if (dt.Rows.Count > 0)
                {
                    Guid XID = Guid.NewGuid();
                    long XLastRecord = 0;
                    DataTable dtLast = new DataTable();
                    dtLast = Repostry_JobAssignment_.FGetDataInDataTable("GetLastRecord", Guid.Empty, new Guid(dt.Rows[0]["FinancialYear_Id_"].ToString()), 0,
                        string.Empty, string.Empty, string.Empty, false, false, true);
                    if (dtLast.Rows.Count > 0)
                        XLastRecord = Convert.ToInt64(dtLast.Rows[0]["Number_Job_"]) + 1;
                    else
                        XLastRecord = ClassSaddam.FGetNumberBillStart();

                    XresultNew = Repostry_JobAssignment_.FAPP("Add", XID, Guid.Empty,
                    new Guid(dt.Rows[0]["FinancialYear_Id_"].ToString()),
                    XLastRecord, txtDate_Job.Text.Trim(), txtDateEnd_Job.Text.Trim(),
                    dt.Rows[0]["Assignment_Title_"].ToString(), dt.Rows[0]["The_Assignment_"].ToString(),
                    dt.Rows[0]["Time_Assignment_"].ToString(), Convert.ToInt32(dt.Rows[0]["Hours_In_Day_"]),
                    Convert.ToBoolean(dt.Rows[0]["Is_Mandate_"]), Convert.ToDecimal(dt.Rows[0]["Amount_"]),
                    Convert.ToInt32(dt.Rows[0]["TotalDays_"]), Convert.ToDecimal(dt.Rows[0]["Total_Amount_"]),
                    dt.Rows[0]["The_Qriah_"].ToString(), true, XDate, false, XDate, Convert.ToBoolean(dt.Rows[0]["Moder_Raees_"]), 
                    Convert.ToInt32(dt.Rows[0]["ID_Moder_"]), false, false, string.Empty, XDate, Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"]), 
                    false, false, false, XDate, string.Empty, false, 0, XDate, false, 0, XDate, false, XDate, false, XDate, 0, Guid.Empty, false,
                    XDate, 0, XDate, XDate, "الإدارة", XIDAdd, 0, 0, XDate, true);
                    if (XresultNew == "IsSuccess")
                    {
                        foreach (ListItem LI in CBEmployee.Items)
                        {
                            if (LI.Selected)
                            {
                                Repostry_JobAssignment_Map_.FAPP("Add", Guid.NewGuid(), new Guid(ddlYears.SelectedValue),
                                    new Guid(LI.Value), XID, false, XDate, XIDAdd, 0, XDate, true);
                            }
                        }
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.InnerText = "تم تحويل المهمة بنجاح ... ";
                        if (CBCheckRicord.Checked)
                        {
                            DataTable dtRecord = new DataTable();
                            dtRecord = Repostry_JobAssignment_Attachments_.FGetDataInDataTable("GetByIDJobAssignment", 1000,
                                new Guid(DLJobAssignment.SelectedValue), string.Empty, true);
                            if (dtRecord.Rows.Count > 0)
                            {
                                for (int i = 0; i <= dtRecord.Rows.Count - 1; i++)
                                {
                                    Repostry_JobAssignment_Attachments_ RJAA = new Repostry_JobAssignment_Attachments_();
                                    XresultRecord = Repostry_JobAssignment_Attachments_.FAPP("Add", Guid.NewGuid(), XID,
                                        dtRecord.Rows[i]["_Type_Send_"].ToString(), dtRecord.Rows[i]["_The_Title_"].ToString(),
                                         dtRecord.Rows[i]["_Src_"].ToString(), dtRecord.Rows[i]["_Type_File_"].ToString(),
                                         dtRecord.Rows[i]["_Size_File_"].ToString(), XIDAdd, XDate, true);
                                }
                            }
                            if (XresultRecord == "IsSuccess")
                            {
                                IDMessageWarning.Visible = false;
                                IDMessageSuccess.Visible = true;
                                lblSuccess.InnerText = "تم تحويل المهمة بنجاح ... ";
                                FGetData();
                                Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value,
                                    "قام المختص بتحويل" + "\n" + "المهام برقم : " + HFIDNumber.Value + " إليك\n" + "يُرجى المتابعة ,,,", "BerArn", "Add", XIDAdd);
                                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة مهمة عمل" + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Add", XIDAdd);
                            }
                        }
                        else
                        {
                            IDMessageWarning.Visible = false;
                            IDMessageSuccess.Visible = true;
                            lblSuccess.InnerText = "تم تحويل المهمة بنجاح ... ";
                            FGetData();
                            Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value,
                                "قام المختص بتحويل" + "\n" + "المهام برقم : " + HFIDNumber.Value + " إليك\n" + "يُرجى المتابعة ,,,", "BerArn", "Add", XIDAdd);
                            if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                                Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة مهمة عمل" + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Add", XIDAdd);
                        }

                        Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "تحديث",
                            "تحويل المهمة رقم " + lblNumberJob.Text.Trim() + " إلى المهمة رقم " + XLastRecord.ToString(), XDate);
                    }
                }
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع حاول لاحقاً ... ";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowModelConvert();", true);
            return;
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (XType == "Manager")
        {
            Repostry_JobAssignment_.FGetDropList(1, "GetByDropByYears", Guid.Empty, new Guid(ddlYears.SelectedValue), 0,
                string.Empty, string.Empty, string.Empty, false, false, true, DLJobAssignment);
        }
        else if (XType == "Admin")
        {
            GetCookie();
            Repostry_JobAssignment_.FGetDropList(1, "GetByDropByAdminActiveByYears", new Guid(IDUniq), new Guid(ddlYears.SelectedValue), 0,
                ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd"), string.Empty, string.Empty, false, false, true, DLJobAssignment);
        }
    }

    protected void LB_Extension_Click(object sender, EventArgs e)
    {
        IDEmp.Visible = false;
        pnl_Convert.Visible = false;
        pnl_Start.Visible = true;
        pnl_End.Visible = true; LB_Convert_New.Visible = false; LB_Extension_New.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowModelConvert();", true);
    }

    protected void LB_Extension_New_Click(object sender, EventArgs e)
    {
        FActive("Extension", false, txtDate_Job.Text.Trim(), txtDateEnd_Job.Text.Trim(), true, HFStartDate.Value, HFEndDate.Value);
        Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "تحديث",
            "تمديد تاريخ المهمة برقم " + lblNumberJob.Text.Trim() , ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["foot"] = pnl2;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LB_Delete_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            Guid _XID = new Guid((((LinkButton)sender).CommandArgument.ToString()));
            int[] A = new int[30];
            string XResult = Repostry_JobAssignment_Attachments_.FAPP("DeleteHide", _XID, Guid.Empty, string.Empty, 
                string.Empty, string.Empty, string.Empty, string.Empty,
                   0, string.Empty, false);
            if (XResult == "IsSuccess")
            {
                FGetData();
                txt_Message.Text = string.Empty;
                txt_Message.Focus();
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    public string FHideDelete(int XID)
    {
        string XResult = string.Empty;
        GetCookie();
        if(XID.ToString() == IDUser)
            XResult = "display :block;";
        else
            XResult = "display :none;";
        return XResult;
    }

}