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
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeAccountables_PageEmployeeAccountableAdd : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["TheFile"] = "---";
            if (XType == "Manager")
            {
                CLS_Permissions.CheckAccountAdmin("A166");
                txtNumberAccountable.Enabled = true;
                pnlDepartment.Visible = true;
                pnlEmployee.Visible = true;
                pnlAccountableType.Visible = true;
                txtDescrption.Enabled = true;
                txtStatement_Request.Enabled = true;
                pnlThe_Statement.Visible = false;
                pnlOther.Visible = true;
                IDAccess.Visible = true;
            }
            else if (XType == "Admin")
            {
                txtNumberAccountable.Enabled = false;
                pnlDepartment.Visible = false;
                pnlEmployee.Visible = false;
                pnlAccountableType.Visible = false;
                txtDescrption.Enabled = false;
                txtStatement_Request.Enabled = false;
                pnlThe_Statement.Visible = true;
                pnlOther.Visible = false;
                IDAccess.Visible = false;
                txtThe_Statement.Focus();
            }
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            txtDate_Accountable.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            //ClassAdmin_Arn.FGetRaeesAlShaoon(DLIDRaees);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModer);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaees);
            Repostry_AccountableType_.FErp_Accountable_Manage(ddlAccountableType);
            FGetLastRecord();
            if (Request.QueryString["ID"] != null)
            {
                FGetData();
            }
        }
    }

    private void FGetLastRecord()
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            DataTable dt = new DataTable();
            dt = Repostry_EmployeeAccountable_.FGetDataInDataTable("GetLastRecord", Guid.Empty, new Guid(ddlYears.SelectedValue),
                0, string.Empty, string.Empty, string.Empty, false, true);
            if (dt.Rows.Count > 0)
                txtNumberAccountable.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["Number_Accountable_"]) + 1);
            else
                txtNumberAccountable.Text = ClassSaddam.FGetNumberBillStart().ToString();

        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Repostry_EmployeeAccountable_.FGetDataInDataTable("GetByIDUniq", new Guid(Request.QueryString["ID"]), new Guid(ddlYears.SelectedValue),
                0, string.Empty, string.Empty, string.Empty, false, true);

            if (dt.Rows.Count > 0)
            {
                ddlYears.SelectedValue = dt.Rows[0]["FinancialYear_Id_"].ToString();
                txtNumberAccountable.Text = dt.Rows[0]["Number_Accountable_"].ToString();
                ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                Repostry_Employee_.FErp_Employee_Master_Manage(new Guid(ddlDepartment.SelectedValue), ddlEmployee);
                ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeId"].ToString();
                FGetPhoneAndEmail(); DLSend.SelectedValue = "No";
                ddlAccountableType.SelectedValue = dt.Rows[0]["ID_AccountableType_"].ToString();
                txtDescrption.Text = dt.Rows[0]["Description_"].ToString().Replace("<br />", Environment.NewLine);
                txtStatement_Request.Text = dt.Rows[0]["Statement_Request_"].ToString().Replace("<br />", Environment.NewLine);
                txtThe_Statement.Text = dt.Rows[0]["The_Statement_"].ToString().Replace("<br />", Environment.NewLine);
                Session["TheFile"] = dt.Rows[0]["The_File_"].ToString();
                txtDate_Accountable.Text = Convert.ToDateTime(dt.Rows[0]["Date_Accountable_"]).ToString("yyyy-MM-dd");
                //DLIDRaees.SelectedValue = dt.Rows[0]["ID_Raees_Lagnat_"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["Moder_Raees_"]))
                { RB_Moder.Checked = false; RB_Raees.Checked = true; DLRaees.SelectedValue = dt.Rows[0]["ID_Raees_Lagnat_"].ToString(); }
                else
                { { RB_Moder.Checked = true; RB_Raees.Checked = false; DLModer.SelectedValue = dt.Rows[0]["ID_Raees_Lagnat_"].ToString(); } }
                if (Request.QueryString["Type"] != null)
                {
                    ID_Raees.Visible = true;
                    if (ddlAccountableType.SelectedItem.ToString() == "مساءلة إنسحاب")
                        IDAllow_Accountable.Visible = false;
                    CB_Allow_Accountable.Checked = Convert.ToBoolean(dt.Rows[0]["Allow_Accountable_"]);
                    CB_Allow_Resolved.Checked = Convert.ToBoolean(dt.Rows[0]["Allow_Resolved_"]);
                    CB_Warning_Oral.Checked = Convert.ToBoolean(dt.Rows[0]["Warning_Oral_"]);
                    CB_Warning_Written.Checked = Convert.ToBoolean(dt.Rows[0]["Warning_Written_"]);
                    CB_Warning_Final.Checked = Convert.ToBoolean(dt.Rows[0]["Warning_Final_"]);
                    txtNumberAccountable.ReadOnly = true; ddlDepartment.Enabled = false; ddlEmployee.Enabled = false;
                    ddlAccountableType.Enabled = false; txtDescrption.ReadOnly = true; txtStatement_Request.ReadOnly = true;
                    txtThe_Statement.ReadOnly = true; txtDate_Accountable.ReadOnly = true; DLModer.Enabled = false; DLRaees.Enabled = false;
                }
                else
                    ID_Raees.Visible = false;
            }
            else
                Response.Redirect("PageEmployeeAccountables.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageEmployeeAccountables.aspx");
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeAccountableAdd.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToDateTime(txtDate_Accountable.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                FChackFile();
            else
            {
                lblWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
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
            FEmployeeAccountable_Add();
    }

    protected void FUpimg(FileUpload upl)
    {
        if (upl.HasFile)
        {
            DateTime XDateGetCurrentTime = ClassSaddam.GetCurrentTime();
            FAddFolder_ToDay(XDateGetCurrentTime.ToString("yyyy_MM_dd"), XDateGetCurrentTime.ToString("MM"), XDateGetCurrentTime.ToString("yyyy"));

            string XRandom = Convert.ToString(Guid.NewGuid()).Replace("-", "");
            string fileExtension = Path.GetExtension(upl.PostedFile.FileName);
            string XDate = "BerArn" + "/" + XDateGetCurrentTime.ToString("yyyy") + "/" + XDateGetCurrentTime.ToString("MM") + "/" + XDateGetCurrentTime.ToString("yyyy_MM_dd");
            string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/FilesAccountable/" + XDate + "/"), XRandom + fileExtension);
            upl.SaveAs(theFileName);
            Session["TheFile"] = "ImgSystem/FilesAccountable/" + XDate + "/" + XRandom + fileExtension;
            FEmployeeAccountable_Add();
        }
    }

    private void FAddFolder_ToDay(string Xdate, string XMounth, string XYears)
    {
        string XNameServer = "BerArn";
        string XHost = Server.MapPath("~/ImgSystem/FilesAccountable/" + XNameServer);
        if (!Directory.Exists(XHost))
            Directory.CreateDirectory(XHost);
        string folderYears = Server.MapPath("~/ImgSystem/FilesAccountable/" + XNameServer + "/" + XYears);
        string folderMounth = Server.MapPath("~/ImgSystem/FilesAccountable/" + XNameServer + "/" + XYears + "/" + XMounth);
        string folderDay = Server.MapPath("~/ImgSystem/FilesAccountable/" + XNameServer + "/" + XYears + "/" + XMounth + "/" + Xdate);
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

    private void FEmployeeAccountable_Add()
    {
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        int XIDAdmin = Test_Saddam.FGetIDUsiq();
        int XModer = 0 , XIDAdd = 0, XUpdate = 0;
        bool XModer_Raees = false, XIs_Reply = false;
        Guid XID = Guid.Empty;
        string XCheck = string.Empty, Xresult = string.Empty;
        XModer = Convert.ToInt32(DLModer.SelectedValue);
        if (Request.QueryString["ID"] == null)
        {
            XCheck = "Add"; XID = Guid.NewGuid(); XIDAdd = XIDAdmin;
        }
        else if (Request.QueryString["ID"] != null)
        {
            XID = new Guid(Request.QueryString["ID"]); XUpdate = XIDAdmin;
            if (XType == "Manager")
            {
                XCheck = "Edit";
                XIs_Reply = false;
            }
            else if (XType == "Admin")
            {
                XCheck = "EditAdmin";
                XIs_Reply = true;
            }
        }

        if (RB_Moder.Checked == false && RB_Raees.Checked)
        { XModer_Raees = true; XModer = Convert.ToInt32(DLRaees.SelectedValue); }
        if (Request.QueryString["Type"] == null)
        {
            Xresult = Repostry_EmployeeAccountable_.FAPP(XCheck, XID, new Guid(ddlEmployee.SelectedValue), new Guid(ddlYears.SelectedValue),
                Convert.ToInt64(txtNumberAccountable.Text.Trim()), txtDate_Accountable.Text.Trim(), new Guid(ddlAccountableType.SelectedValue),
                txtDescrption.Text.Trim().Replace(Environment.NewLine, "<br />"), txtStatement_Request.Text.Trim().Replace(Environment.NewLine, "<br />"),
                XIs_Reply, txtThe_Statement.Text.Trim().Replace(Environment.NewLine, "<br />"), Session["TheFile"].ToString(),
                false, false, false, false, false, XModer_Raees, XModer, false, false, XDate, string.Empty, XIDAdd, XUpdate, 0, XDate, true);
            if (Xresult == "IsExistsNumber")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رقم الإجازة مستخدم مسبقاً ... ";
                return;
            }
            else if (Xresult == "IsExists")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                return;
            }
            else if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";

                if (Request.QueryString["ID"] == null)
                {
                    Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "إضافة",
                    " إضافة إجراء مساءلة لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberAccountable.Text.Trim(), XDate);
                    if (DLSend.SelectedValue == "Yes")
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "لديك" + "\n" + ddlAccountableType.SelectedItem.Text + "\n" + "رقم المساءلة : " + txtNumberAccountable.Text.Trim() + "\n" + "يُرجى المتابعة ,,,", "BerArn", "Add", XIDAdd);

                    if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة مساءلة لموظف" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Add", XIDAdd);
                }
                else if (Request.QueryString["ID"] != null)
                {
                    Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "تعديل",
                    " تعديل إجراء مساءلة لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberAccountable.Text.Trim(), XDate);
                    if (DLSend.SelectedValue == "Yes")
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تعديل" + "\n" + ddlAccountableType.SelectedItem.Text + "\n" + "رقم المساءلة : " + txtNumberAccountable.Text.Trim() + "\n" + "يُرجى المتابعة ,,,", "BerArn", "Edit", XIDAdd);

                    if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل مساءلة لموظف" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Edit", XIDAdd);
                }
            }
        }
        else if (Request.QueryString["ID"] != null && Request.QueryString["Type"] != null)
        {
            if (CB_Allow_Accountable.Checked || CB_Allow_Resolved.Checked || CB_Warning_Oral.Checked || CB_Warning_Written.Checked || CB_Warning_Final.Checked)
            {
                Xresult = Repostry_EmployeeAccountable_.FAPP("AllowRaees", new Guid(Request.QueryString["ID"]), Guid.Empty, Guid.Empty,
                0, string.Empty, Guid.Empty, string.Empty, string.Empty, false, string.Empty, string.Empty, CB_Allow_Accountable.Checked,
                CB_Allow_Resolved.Checked, CB_Warning_Oral.Checked, CB_Warning_Written.Checked, CB_Warning_Final.Checked, false, 0,
                true, false, XDate, string.Empty, XIDAdd, XUpdate, 0, XDate, true);
                if (Xresult == "IsSuccess")
                {
                    IDMessageWarning.Visible = false;
                    IDMessageSuccess.Visible = true;
                    lblSuccess.Text = "تم إرسال الإطلاع بنجاح ... ";
                    Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "وافق",
                   " وافق على إجراء مساءلة لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberAccountable.Text.Trim(), XDate);
                }
            }
            else
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "يجب إختيار الإجراء اللازم ... ";
                return;
            }
        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeAccountables.aspx");
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlEmployee.Items.Clear(); ddlEmployee.Items.Add(""); ddlEmployee.AppendDataBoundItems = true;
            Repostry_Employee_.FErp_Employee_Master_Manage(new Guid(ddlDepartment.SelectedValue), ddlEmployee);
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void ddlAccountableType_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        if (ddlAccountableType.SelectedItem.ToString() == "مساءلة تأخير عن الدوام")
        {
            txtDescrption.Text = "التأخير عن دوام يوم (" + ClassSaddam.GetCurrentTime().ToString("ddd") + ") الموافق " + ClassSaddam.GetCurrentTime().ToString("dd/MM/yyyy") + "م .";
            txtDescrption.Text += "\nمن الساعة : (_____) صباحاً إلى الساعة (_____) صباحاً , فترة التأخير (_____)";

            txtStatement_Request.Text = "من خلال المتابعة لسجل الحضور والانصرف تبين تأخركم عن الدوام خلال الفترة الموضحة بعاليه . \n";
            txtStatement_Request.Text += "آمل الإفادة عن أسباب ذلك , وعليكم تقديم ما يؤيد عذركم . \n";
            txtStatement_Request.Text += "علما بأنة في حالة عدم الالتزام سيتم إتخاذ اللازم حسب التعليمات .";

            txtThe_Statement.Text = "أفيدكم أن تأخري عن الدوام كان للأسباب التالية :. \n";
            txtThe_Statement.Text += Environment.NewLine;
            txtThe_Statement.Text += Environment.NewLine;
            txtThe_Statement.Text += "وسأقوم بتقديم ما يثبت ذلك خلال اسبوع من تاريخه . \n";
        }
        else if (ddlAccountableType.SelectedItem.ToString() == "مساءلة غياب")
        {
            txtDescrption.Text = "الغياب عن الدوام يوم (" + ClassSaddam.GetCurrentTime().ToString("ddd") + ") الموافق " + ClassSaddam.GetCurrentTime().ToString("dd/MM/yyyy") + " م ";

            txtStatement_Request.Text = "من خلال المتابعة لسجل الحضور والانصرف تبين تغيبكم عن الدوام خلال الفترة الموضحة بعاليه . \n";
            txtStatement_Request.Text += "آمل الإفادة عن أسباب ذلك , وعليكم تقديم ما يؤيد عذركم . \n";
            txtStatement_Request.Text += "علما بأنة في حالة عدم الالتزام سيتم إتخاذ اللازم حسب التعليمات .";

            txtThe_Statement.Text = "أفيدكم أن غيابي عن الدوام كان للأسباب التالية : \n";
            txtThe_Statement.Text += Environment.NewLine;
            txtThe_Statement.Text += Environment.NewLine;
            txtThe_Statement.Text += "وسأقوم بتقديم ما يثبت ذلك خلال اسبوع من تاريخه . \n";
        }
        else if (ddlAccountableType.SelectedItem.ToString() == "مساءلة إنسحاب")
        {
            txtDescrption.Text = "إنسحاب عن الدوام يوم (" + ClassSaddam.GetCurrentTime().ToString("ddd") + ") الموافق " + ClassSaddam.GetCurrentTime().ToString("dd/MM/yyyy") + " م ";

            txtStatement_Request.Text = "من خلال المتابعة لسجل الحضور والانصرف تبين إنسحابكم عن الدوام خلال الفترة الموضحة بعاليه . \n";
            txtStatement_Request.Text += "آمل الإفادة عن أسباب ذلك , وعليكم تقديم ما يؤيد عذركم . \n";
            txtStatement_Request.Text += "علما بأنة في حالة عدم الالتزام سيتم إتخاذ اللازم حسب التعليمات .";

            txtThe_Statement.Text = "أفيدكم أن إنسحابي عن الدوام كان للأسباب التالية : \n";
            txtThe_Statement.Text += Environment.NewLine;
            txtThe_Statement.Text += Environment.NewLine;
            txtThe_Statement.Text += "وسأقوم بتقديم ما يثبت ذلك خلال اسبوع من تاريخه . \n";
        }
        else
        {
            txtDescrption.Text = string.Empty;
            txtStatement_Request.Text = string.Empty;
            txtThe_Statement.Text = string.Empty;
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetLastRecord();
        txtDate_Accountable.Text = Convert.ToDateTime(txtDate_Accountable.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetPhoneAndEmail();
    }

    private void FGetPhoneAndEmail()
    {
        Repostry_Employee_.FGetPhoneAndEmail(ddlEmployee.SelectedValue, HFPhone, HFEmail);
        lblPhone.InnerText = HFPhone.Value;
    }

    public string FCheck(string XCheck)
    {
        string XResult = "display:none;";
        if (XCheck == "_Moder")
        {
            if (RB_Moder.Checked)
                XResult = "display:block;";
            else if (RB_Moder.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "_Raees")
        {
            if (RB_Raees.Checked)
                XResult = "display:block;";
            else if (RB_Raees.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        return XResult;
    }

}