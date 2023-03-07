using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeePermission_PageEmployeePermissionAdd : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    string IDUniq = string.Empty;
    private void GetCookie()
    {
        try
        {
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
            if (XType == "Manager")
            {
                CLS_Permissions.CheckAccountAdmin("A196");
                IDAccess.Visible = true;
            }
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            txtTitle.Text = "إستئذان موظف";
            txtDateAdd.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            txtDate_Permission.Text = txtDateAdd.Text;
            txtFrom_The_Hour_.Text = ClassSaddam.GetCurrentTime().ToString("HH:mm");
            txtDescrption.Text = "-";
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModer);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLIDRaees);
            FGetLastRecord();
            if (XType == "Manager")
            {
                IDDepartment.Visible = true; IDEmployee.Visible = true;
                DLModer.Enabled = true; DLSend.SelectedValue = "Yes"; DLSend.Enabled = true;
            }
            else if (XType == "Admin")
            {
                IDDepartment.Visible = false; IDEmployee.Visible = false;
                DLModer.Enabled = false; DLSend.SelectedValue = "Yes"; DLSend.Enabled = false;
                GetCookie();
                FGetPhoneAndEmail(IDUniq);
                if (FRefresh(new Guid(IDUniq)))
                {
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    lblWarning.Text = "لقد إستخدمت كامل الرصيد ,,, ";
                    btnAdd.Visible = false;
                    return; ;
                }
                else
                    btnAdd.Visible = true;
            }
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetLastRecord()
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            Model_EmployeePermission_ MEP = new Model_EmployeePermission_();
            MEP.IDCheck = "GetLastRecord";
            MEP.EmployeePermissionMapID = Guid.Empty;
            MEP.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEP.Number_Permission = 0;
            MEP.CreatedDate = string.Empty;
            MEP.Start_Date = string.Empty;
            MEP.End_Date = string.Empty;
            MEP.Is_Moder_Allow = false;
            MEP.Is_Moder_Not_Allow = false;
            MEP.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeePermission_ REP = new Repostry_EmployeePermission_();
            dt = REP.BErp_EmployeePermission_Manage(MEP);
            if (dt.Rows.Count > 0)
                txtNumberPermission.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["Number_Permission_"]) + 1);
            else
                txtNumberPermission.Text = ClassSaddam.FGetNumberBillStart().ToString();
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
            Model_EmployeePermission_ MEP = new Model_EmployeePermission_();
            MEP.IDCheck = "GetByIDUniq";
            MEP.EmployeePermissionMapID = new Guid(Request.QueryString["ID"]);
            MEP.FinancialYear_Id = Guid.Empty;
            MEP.Number_Permission = 0;
            MEP.CreatedDate = string.Empty;
            MEP.Start_Date = string.Empty;
            MEP.End_Date = string.Empty;
            MEP.Is_Moder_Allow = false;
            MEP.Is_Moder_Not_Allow = false;
            MEP.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeePermission_ REP = new Repostry_EmployeePermission_();
            dt = REP.BErp_EmployeePermission_Manage(MEP);
            if (dt.Rows.Count > 0)
            {
                ddlYears.SelectedValue = dt.Rows[0]["FinancialYear_Id_"].ToString();
                ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                ddlEmployee.Items.Clear(); ddlEmployee.Items.Add(""); ddlEmployee.AppendDataBoundItems = true;
                Repostry_Employee_.FErp_Employee_Master_Manage(new Guid(ddlDepartment.SelectedValue), ddlEmployee);
                ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeId"].ToString();
                FGetPhoneAndEmail(ddlEmployee.SelectedValue); DLSend.SelectedValue = "No";
                txtNumberPermission.Text = dt.Rows[0]["Number_Permission_"].ToString();
                txtTitle.Text = dt.Rows[0]["PermissionTitle"].ToString();
                txtDate_Permission.Text = Convert.ToDateTime(dt.Rows[0]["Date_Permission_"]).ToString("yyyy-MM-dd");
                CB_Early_Dismissal_.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Early_Dismissal_"]);
                CB_Late_In_Attendance_.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Late_In_Attendance_"]);
                CB_Exit_And_Return_.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Exit_And_Return_"]);
                txtFrom_The_Hour_.Text = dt.Rows[0]["From_The_Hour_"].ToString();
                txtTo_The_Hour_.Text = dt.Rows[0]["To_The_Hour_"].ToString();
                txtDescrption.Text = dt.Rows[0]["Description"].ToString().Replace("<br />", Environment.NewLine);
                if (Convert.ToBoolean(dt.Rows[0]["Moder_Raees_"]))
                { RB_Moder.Checked = false; RB_Raees.Checked = true; DLIDRaees.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); }
                else
                { { RB_Moder.Checked = true; RB_Raees.Checked = false; DLModer.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); } }
                txtDateAdd.Text = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]).ToString("yyyy-MM-dd");
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاُ ... ";
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeePermissionAdd.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            if (CB_Early_Dismissal_.Checked == false && CB_Late_In_Attendance_.Checked == false && CB_Exit_And_Return_.Checked == false)
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblWarning.Text = "يجب تحديد طبيعة الإستئذان ... ";
                return;
            }
            else if (CB_Early_Dismissal_.Checked || CB_Late_In_Attendance_.Checked || CB_Exit_And_Return_.Checked)
            {
                Guid XID = Guid.Empty;
                if (XType == "Manager")
                    XID = new Guid(ddlEmployee.SelectedValue);
                else if (XType == "Admin")
                {
                    GetCookie();
                    XID = new Guid(IDUniq);
                }
                if (Convert.ToDateTime(txtDate_Permission.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                {
                    if (CB_Early_Dismissal_.Checked || CB_Late_In_Attendance_.Checked == false || CB_Exit_And_Return_.Checked == false)
                        FEmployeeOverTime_Add(IDEarly_Dismissal.InnerText, XID);
                    else if (CB_Early_Dismissal_.Checked == false || CB_Late_In_Attendance_.Checked || CB_Exit_And_Return_.Checked == false)
                        FEmployeeOverTime_Add(IDLate_In_Attendance.InnerText, XID);
                    else if (CB_Early_Dismissal_.Checked == false || CB_Late_In_Attendance_.Checked == false || CB_Exit_And_Return_.Checked)
                        FEmployeeOverTime_Add(IDExit_And_Return.InnerText, XID);
                }
                else
                {
                    lblWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    return;
                }
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

    private void FEmployeeOverTime_Add(string Xtype, Guid XIDEmp)
    {
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        int XIDAdd = Test_Saddam.FGetIDUsiq();
        int XModer = 0;
        bool XModer_Raees = false; XModer = Convert.ToInt32(DLModer.SelectedValue);
        if (RB_Moder.Checked == false && RB_Raees.Checked)
        { XModer_Raees = true; XModer = Convert.ToInt32(DLIDRaees.SelectedValue); }
        if (Request.QueryString["ID"] == null)
        {
            if (FRefresh(XIDEmp))
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblWarning.Text = "لقد إستخدمت كامل الرصيد ,,, ";
                btnAdd.Visible = false;
                return; ;
            }
            else
                btnAdd.Visible = true;
            Model_EmployeePermission_ MEP = new Model_EmployeePermission_()
            {
                IDCheck = "Add",
                EmployeePermissionMapID = Guid.NewGuid(),
                FinancialYear_Id = new Guid(ddlYears.SelectedValue),
                EmployeeId = XIDEmp,
                Number_Permission = Convert.ToInt64(txtNumberPermission.Text.Trim()),
                PermissionTitle = txtTitle.Text.Trim(),
                Date_Permission = txtDate_Permission.Text.Trim(),
                Is_Early_Dismissal = Convert.ToBoolean(CB_Early_Dismissal_.Checked),
                Is_Late_In_Attendance = Convert.ToBoolean(CB_Late_In_Attendance_.Checked),
                Is_Exit_And_Return = Convert.ToBoolean(CB_Exit_And_Return_.Checked),
                From_The_Hour = txtFrom_The_Hour_.Text.Trim(),
                To_The_Hour = txtTo_The_Hour_.Text.Trim(),
                Description = txtDescrption.Text.Trim().Replace(Environment.NewLine, "<br />"),
                Moder_Raees = XModer_Raees,
                ID_Moder = XModer,
                Is_Moder_Allow = false,
                Is_Moder_Not_Allow = false,
                Comment_Moder = string.Empty,
                Date_Moder = XDate,
                CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedBy = XIDAdd,
                ModifiedDate = XDate,
                ModifiedBy = 0,
                IsActive = true,
                DeleteDate = XDate,
                DeleteBy = 0
            };
            Repostry_EmployeePermission_ REP = new Repostry_EmployeePermission_();
            string Xresult = REP.FErp_EmployeePermission_Add(MEP);
            if (Xresult == "IsExistsNumberAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رقم الإستئذان مستخدم مسبقاً ... ";
                return;
            }
            else if (Xresult == "IsExistsAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                return;
            }
            else if (Xresult == "IsSuccessAdd")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
                if (XType == "Manager")
                    Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "إضافة",
                   " إضافة إستئذان لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberPermission.Text.Trim(), XDate);
                else if (XType == "Admin")
                    Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "إضافة",
                   "طلب إستئذان له " + " برقم " + txtNumberPermission.Text.Trim(), XDate);
                if (DLSend.SelectedValue == "Yes")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تم منحك إستئذان" + "\n" + Xtype + "\n" + "رقم الإستئذان : " + txtNumberPermission.Text.Trim() + "\n" + "شكراً لك ,,,", "BerArn", "Add", XIDAdd);

                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة إستئذان لموظف" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Add", XIDAdd);
            }
        }
        else if (Request.QueryString["ID"] != null)
        {
            Model_EmployeePermission_ MEP = new Model_EmployeePermission_()
            {
                IDCheck = "Edit",
                EmployeePermissionMapID = new Guid(Request.QueryString["ID"]),
                FinancialYear_Id = new Guid(ddlYears.SelectedValue),
                EmployeeId = XIDEmp,
                Number_Permission = Convert.ToInt64(txtNumberPermission.Text.Trim()),
                PermissionTitle = txtTitle.Text.Trim(),
                Date_Permission = txtDate_Permission.Text.Trim(),
                Is_Early_Dismissal = Convert.ToBoolean(CB_Early_Dismissal_.Checked),
                Is_Late_In_Attendance = Convert.ToBoolean(CB_Late_In_Attendance_.Checked),
                Is_Exit_And_Return = Convert.ToBoolean(CB_Exit_And_Return_.Checked),
                From_The_Hour = txtFrom_The_Hour_.Text.Trim(),
                To_The_Hour = txtTo_The_Hour_.Text.Trim(),
                Description = txtDescrption.Text.Trim().Replace(Environment.NewLine, "<br />"),
                Moder_Raees = XModer_Raees,
                ID_Moder = XModer,
                Is_Moder_Allow = false,
                Is_Moder_Not_Allow = false,
                Comment_Moder = string.Empty,
                Date_Moder = XDate,
                CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedBy = XIDAdd,
                ModifiedDate = XDate,
                ModifiedBy = 0,
                IsActive = true,
                DeleteDate = XDate,
                DeleteBy = 0
            };
            Repostry_EmployeePermission_ REP = new Repostry_EmployeePermission_();
            string Xresult = REP.FErp_EmployeePermission_Add(MEP);
            if (Xresult == "IsExistsNumberEdit")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رقم الإستئذان مستخدم مسبقاً ... ";
                return;
            }
            else if (Xresult == "IsExistsEdit")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                return;
            }
            else if (Xresult == "IsSuccessEdit")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
                if (XType == "Manager")
                    Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "تعديل",
                   " تعديل إستئذان لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberPermission.Text.Trim(), XDate);
                else if (XType == "Admin")
                    Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "تعديل",
                   "تعديل طلب إستئذان له " + " برقم " + txtNumberPermission.Text.Trim(), XDate);
                if (DLSend.SelectedValue == "Yes")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تعديل إستئذان" + "\n" + Xtype + "\n" + "رقم الإستئذان : " + txtNumberPermission.Text.Trim() + "\n" + "شكراً لك ,,,", "BerArn", "Edit", XIDAdd);

                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل إستئذان لموظف" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Edit", XIDAdd);
            }
        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeePermission.aspx");
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
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

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetPhoneAndEmail(ddlEmployee.SelectedValue);
        if (FRefresh(new Guid(ddlEmployee.SelectedValue)))
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "لقد إستخدمت كامل الرصيد ,,, ";
            btnAdd.Visible = false;
            return; ;
        }
        else
            btnAdd.Visible = true;
    }

    protected void LBRefresh_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        Guid XID = Guid.Empty;
        if (XType == "Manager")
        {
            XID = new Guid(ddlEmployee.SelectedValue);
        }
        else if (XType == "Admin")
        {
            GetCookie();
            XID = new Guid(IDUniq);
        }
        if (FRefresh(XID))
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "لقد إستخدمت كامل الرصيد ,,, ";
            btnAdd.Visible = false;
            return; ;
        }
        else
            btnAdd.Visible = true;
    }

    private bool FRefresh(Guid XID)
    {
        bool XCheck = false;
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;
            if (Request.QueryString["ID"] == null)
            {
                Model_EmployeePermission_ MEP = new Model_EmployeePermission_();
                MEP.IDCheck = "CheckThree";
                MEP.EmployeePermissionMapID = XID;
                MEP.FinancialYear_Id = Guid.Empty;
                MEP.Number_Permission = 0;
                MEP.CreatedDate = string.Empty;
                MEP.Start_Date = txtDate_Permission.Text.Trim();
                MEP.End_Date = string.Empty;
                MEP.Is_Moder_Allow = false;
                MEP.Is_Moder_Not_Allow = false;
                MEP.IsActive = true;
                DataTable dt = new DataTable();
                Repostry_EmployeePermission_ REP = new Repostry_EmployeePermission_();
                dt = REP.BErp_EmployeePermission_Manage(MEP);
                if (dt.Rows.Count > 0)
                {
                    IDDetails.Visible = true;
                    HFCountAll.Value = "3"; lblCountAll.Text = HFCountAll.Value;
                    HFCountUse.Value = dt.Rows[0]["_Count"].ToString();
                    lblCountUse.Text = HFCountUse.Value;

                    HFCountAllow.Value = Convert.ToString(Convert.ToInt16(HFCountAll.Value) - Convert.ToInt16(HFCountUse.Value));
                    lblCountAllow.Text = HFCountAllow.Value;
                    if (HFCountAllow.Value == "0")
                        XCheck = true;
                    else
                        XCheck = false;
                }
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
        }
        return XCheck;
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetLastRecord();
        txtDate_Permission.Text = Convert.ToDateTime(txtDate_Permission.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
    }

    private void FGetPhoneAndEmail(string XID)
    {
        Repostry_Employee_.FGetPhoneAndEmail(XID, HFPhone, HFEmail);
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