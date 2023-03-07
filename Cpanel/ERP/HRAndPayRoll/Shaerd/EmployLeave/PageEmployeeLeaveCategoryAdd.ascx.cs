using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployLeave_PageEmployeeLeaveCategoryAdd : System.Web.UI.UserControl
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
                CLS_Permissions.CheckAccountAdmin("A162");
                IDAccess.Visible = true;
            }
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            txtStartDate.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            txtEndDate.Text = txtStartDate.Text;
            Repostry_LeaveCategory_.FErp_LeaveCategory_Manage(ddlLeaveCategory);
            ClassAdmin_Arn.FGetAdmin(DLIDEmp);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModer);
            ClassAdmin_Arn.FGetRaeesAlShaoon(DLIDRaeesShoon);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLIDRaees);
            CB_Basic.Checked = true;
            FGetLastRecord();
            if (XType == "Manager")
            {
                IDDepartment.Visible = true; IDEmployee.Visible = true; IDView.Visible = true;
                DLModer.Enabled = true; DLIDRaees.Enabled = true; DLSend.SelectedValue = "Yes"; DLSend.Enabled = true;
            }
            else if (XType == "Admin")
            {
                IDDepartment.Visible = false; IDEmployee.Visible = false; IDView.Visible = false;
                DLModer.Enabled = false; DLIDRaees.Enabled = false; DLSend.SelectedValue = "Yes"; DLSend.Enabled = false;
                GetCookie();
                FGetPhoneAndEmail(1, IDUniq);
                FCheckR(new Guid(IDUniq));
            }
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            Model_EmployeeLeaveCategory_ MELC = new Model_EmployeeLeaveCategory_();
            MELC.IDCheck = "GetByIDUniq";
            MELC.EmployeeLeaveCategoryMapID = new Guid(Request.QueryString["ID"]);
            MELC.FinancialYear_Id = Guid.Empty;
            MELC.Number_Leave = 0;
            MELC.CreatedDate = string.Empty;
            MELC.StartDate = string.Empty;
            MELC.EndDate = string.Empty;
            MELC.Is_Emp = false;
            MELC.Is_Moder_Allow = false;
            MELC.Is_Raees_Lagnat_Allow = false;
            MELC.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeLeaveCategory_ RELC = new Repostry_EmployeeLeaveCategory_();
            dt = RELC.BErp_EmployeeLeaveCategory_Manage(MELC);
            if (dt.Rows.Count > 0)
            {
                ddlYears.SelectedValue = dt.Rows[0]["FinancialYear_Id_"].ToString();
                ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                ddlEmployee.Items.Clear(); ddlEmployee.Items.Add(""); ddlEmployee.AppendDataBoundItems = true;
                Repostry_Employee_.FErp_Employee_Master_Manage(new Guid(ddlDepartment.SelectedValue), ddlEmployee);
                ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeId"].ToString();
                FGetPhoneAndEmail(1, ddlEmployee.SelectedValue); DLSend.SelectedValue = "No"; 
                ddlLeaveCategory.SelectedValue = dt.Rows[0]["LeaveCategoryId"].ToString();
                txtNumberLeave.Text = dt.Rows[0]["Number_Leave_"].ToString();
                txtStartDate.Text = Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToString("yyyy-MM-dd");
                txtEndDate.Text = Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("yyyy-MM-dd");
                txtTotalLeave.Text = Convert.ToInt32(dt.Rows[0]["TotalDay"]).ToString();
                chkStartDateHalfLeave.Checked = Convert.ToBoolean(dt.Rows[0]["IsFirstHalfDay"]);
                chkEndDateHalfLeave.Checked = Convert.ToBoolean(dt.Rows[0]["IsLastHalfDay"]);
                DLIDEmp.SelectedValue = dt.Rows[0]["ID_Emp_"].ToString();
                FGetPhoneAndEmail(2, ddlEmployee.SelectedValue);
                //DLModer.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString();
                //DLIDRaees.SelectedValue = dt.Rows[0]["ID_Raees_Lagnat_"].ToString();
                txtDescrption.Text = dt.Rows[0]["Reason"].ToString().Replace("<br />", Environment.NewLine);
                if (Convert.ToInt16(dt.Rows[0]["Is_Basic_"]) == 1)
                    CB_Basic.Checked = true;
                else if (Convert.ToInt16(dt.Rows[0]["Is_Basic_"]) == 2)
                    CB_Compensatory.Checked = true;
                else if (Convert.ToInt16(dt.Rows[0]["Is_Basic_"]) == 3)
                    CB_Sick.Checked = true;
                if (Convert.ToBoolean(dt.Rows[0]["Moder_Raees_"]))
                { RB_Moder.Checked = false; RB_Raees.Checked = true; DLIDRaees.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); }
                else
                { { RB_Moder.Checked = true; RB_Raees.Checked = false; DLModer.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); } }
                FCheckR(new Guid(ddlEmployee.SelectedValue));
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetLastRecord()
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            Model_EmployeeLeaveCategory_ MELC = new Model_EmployeeLeaveCategory_();
            MELC.IDCheck = "GetLastRecord";
            MELC.EmployeeLeaveCategoryMapID = Guid.Empty;
            MELC.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MELC.Number_Leave = 0;
            MELC.CreatedDate = string.Empty;
            MELC.StartDate = string.Empty;
            MELC.EndDate = string.Empty;
            MELC.Is_Emp = false;
            MELC.Is_Moder_Allow = false;
            MELC.Is_Raees_Lagnat_Allow = false;
            MELC.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeLeaveCategory_ RELC = new Repostry_EmployeeLeaveCategory_();
            dt = RELC.BErp_EmployeeLeaveCategory_Manage(MELC);
            if (dt.Rows.Count > 0)
                txtNumberLeave.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["Number_Leave_"]) + 1);
            else
                txtNumberLeave.Text = ClassSaddam.FGetNumberBillStart().ToString();

        }
        catch (Exception)
        {
            return;
        }
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            if (CB_Basic.Checked && CB_Emergency.Checked == false && CB_Compensatory.Checked == false && CB_Sick.Checked == false)
            {
                if (Convert.ToDecimal(txtTotalLeave.Text.Trim()) <= Convert.ToDecimal(HFCountDayAllow.Value))
                {
                    if (Convert.ToDateTime(txtStartDate.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                    {
                        if (XType == "Manager")
                            FEmployeeLeaveCategoryAdd(new Guid(ddlEmployee.SelectedValue));
                        else if (XType == "Admin")
                        {
                            GetCookie();
                            FEmployeeLeaveCategoryAdd(new Guid(IDUniq));
                        }
                    }
                    else
                    {
                        lblWarning.Text = "أنت في إرشيف سنة " + ddlYears.SelectedItem.ToString() + ", لا يمكن الإنتقال لسنة أخرى";
                        IDMessageSuccess.Visible = false;
                        IDMessageWarning.Visible = true;
                        return;
                    }
                }
                else
                {
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    lblWarning.Text = "رصيدك المتبقي من الاعتيادي لا يكفي لعمل هذه الإجازة ... ";
                    return;
                }
            }
            else if (CB_Basic.Checked == false && CB_Emergency.Checked && CB_Compensatory.Checked == false && CB_Sick.Checked == false)
            {
                if (Convert.ToDecimal(txtTotalLeave.Text.Trim()) <= Convert.ToDecimal(HFCountEmergencyAllow.Value))
                {
                    if (XType == "Manager")
                        FEmployeeLeaveCategoryAdd(new Guid(ddlEmployee.SelectedValue));
                    else if (XType == "Admin")
                    {
                        GetCookie();
                        FEmployeeLeaveCategoryAdd(new Guid(IDUniq));
                    }
                }
                else
                {
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    lblWarning.Text = "رصيدك المتبقي من الاضطراري لا يكفي لعمل هذه الإجازة ... ";
                    return;
                }
            }
            else if (CB_Basic.Checked == false && CB_Emergency.Checked == false && CB_Compensatory.Checked && CB_Sick.Checked == false)
            {
                if (Convert.ToDecimal(txtTotalLeave.Text.Trim()) <= Convert.ToDecimal(HFCountCompensatoryAllow.Value))
                {
                    if (XType == "Manager")
                        FEmployeeLeaveCategoryAdd(new Guid(ddlEmployee.SelectedValue));
                    else if (XType == "Admin")
                    {
                        GetCookie();
                        FEmployeeLeaveCategoryAdd(new Guid(IDUniq));
                    }
                }
                else
                {
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    lblWarning.Text = "رصيدك المتبقي من التعويضي لا يكفي لعمل هذه الإجازة ... ";
                    return;
                }
            }
            else if (CB_Basic.Checked == false && CB_Emergency.Checked == false && CB_Compensatory.Checked == false && CB_Sick.Checked)
            {
                if (XType == "Manager")
                    FEmployeeLeaveCategoryAdd(new Guid(ddlEmployee.SelectedValue));
                else if (XType == "Admin")
                {
                    GetCookie();
                    FEmployeeLeaveCategoryAdd(new Guid(IDUniq));
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

    private void FEmployeeLeaveCategoryAdd(Guid XIDEmp)
    {
        Int16 _Is_Basic = 0;
        if (CB_Basic.Checked && CB_Emergency.Checked == false && CB_Compensatory.Checked == false && CB_Sick.Checked == false)
            _Is_Basic = 1;
        else if (CB_Basic.Checked == false && CB_Emergency.Checked == false && CB_Compensatory.Checked && CB_Sick.Checked == false)
            _Is_Basic = 2;
        else if (CB_Basic.Checked == false && CB_Emergency.Checked == false && CB_Compensatory.Checked == false && CB_Sick.Checked)
            _Is_Basic = 3;
        else if (CB_Basic.Checked == false && CB_Emergency.Checked && CB_Compensatory.Checked == false && CB_Sick.Checked == false)
            _Is_Basic = 4;
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        int XIDAdd = Test_Saddam.FGetIDUsiq();
        int XModer = 0;
        bool XModer_Raees = false; XModer = Convert.ToInt32(DLModer.SelectedValue);
        if (RB_Moder.Checked == false && RB_Raees.Checked)
        { XModer_Raees = true; XModer = Convert.ToInt32(DLIDRaees.SelectedValue); }
        if (Request.QueryString["ID"] == null)
        {
            Model_EmployeeLeaveCategory_ MELC = new Model_EmployeeLeaveCategory_()
            {
                IDCheck = "Add",
                EmployeeLeaveCategoryMapID = Guid.NewGuid(),
                EmployeeId = XIDEmp,
                LeaveCategoryId = new Guid(ddlLeaveCategory.SelectedValue),
                FinancialYear_Id = new Guid(ddlYears.SelectedValue),
                Number_Leave = Convert.ToInt64(txtNumberLeave.Text.Trim()),
                StartDate = txtStartDate.Text.Trim(),
                EndDate = txtEndDate.Text.Trim(),
                TotalDay = Convert.ToDecimal(txtTotalLeave.Text.Trim()),
                IsFirstHalfDay = Convert.ToBoolean(chkStartDateHalfLeave.Checked),
                IsLastHalfDay = Convert.ToBoolean(chkEndDateHalfLeave.Checked),
                Reason = txtDescrption.Text.Trim().Replace(Environment.NewLine, "<br />"),
                ID_Emp = Convert.ToInt32(DLIDEmp.SelectedValue),
                Is_Emp = false,
                Moder_Raees = XModer_Raees,
                ID_Moder = XModer,
                Is_Moder_Allow = false,
                Is_Moder_Not_Allow = false,
                Comments = string.Empty,
                ApplyDate = XDate,
                ID_Raees_Lagnat = Convert.ToInt32(DLIDRaeesShoon.SelectedValue),
                Is_Raees_Lagnat_Allow = false,
                Is_Raees_Lagnat_Not_Allow = false,
                Apply_Raees_Lagnat_Date = XDate,
                ApprovedBy = "ITFYEduu@gmail.com",
                ApproveDate = XDate,
                CreatedDate = XDate,
                CreatedBy = XIDAdd,
                ModifiedBy = 0,
                ModifiedDate = XDate,
                IsActive = true,
                IsApprove = false,
                Is_Basic = _Is_Basic
            };
            Repostry_EmployeeLeaveCategory_ RELC = new Repostry_EmployeeLeaveCategory_();
            string Xresult = RELC.FErp_EmployeeLeaveCategory_Add(MELC);
            if (Xresult == "IsExistsNumberAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رقم الإجازة مستخدم مسبقاً ... ";
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
                   " إضافة إجاوة لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberLeave.Text.Trim(), XDate);
                else if (XType == "Admin")
                    Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "إضافة",
                   "طلب إجازة له " + " برقم " + txtNumberLeave.Text.Trim(), XDate);

                if (DLSend.SelectedValue == "Yes")
                {
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تم منحك " + ddlLeaveCategory.SelectedItem.Text + "\n" + "عدد الأيام : " + txtTotalLeave.Text.Trim() + "\n" + "رقم الإجازة : " + txtNumberLeave.Text.Trim(), "BerArn", "Add", XIDAdd);
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone2.Value, "تم إختيارك الموظف البديل لـ " + "\n" + ddlEmployee.SelectedItem.Text + "\n" + "عدد الأيام : " + txtTotalLeave.Text.Trim(), "BerArn", "Add", XIDAdd);
                }

                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة إجازة لموظف" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Add", XIDAdd);
            }
        }
        else if (Request.QueryString["ID"] != null)
        {
            Model_EmployeeLeaveCategory_ MELC = new Model_EmployeeLeaveCategory_()
            {
                IDCheck = "Edit",
                EmployeeLeaveCategoryMapID = new Guid(Request.QueryString["ID"]),
                EmployeeId = XIDEmp,
                LeaveCategoryId = new Guid(ddlLeaveCategory.SelectedValue),
                FinancialYear_Id = new Guid(ddlYears.SelectedValue),
                Number_Leave = Convert.ToInt64(txtNumberLeave.Text.Trim()),
                StartDate = txtStartDate.Text.Trim(),
                EndDate = txtEndDate.Text.Trim(),
                TotalDay = Convert.ToDecimal(txtTotalLeave.Text.Trim()),
                IsFirstHalfDay = Convert.ToBoolean(chkStartDateHalfLeave.Checked),
                IsLastHalfDay = Convert.ToBoolean(chkEndDateHalfLeave.Checked),
                Reason = txtDescrption.Text.Trim().Replace(Environment.NewLine, "<br />"),
                ID_Emp = Convert.ToInt32(DLIDEmp.SelectedValue),
                Is_Emp = false,
                Moder_Raees = XModer_Raees,
                ID_Moder = XModer,
                Is_Moder_Allow = false,
                Is_Moder_Not_Allow = false,
                Comments = string.Empty,
                ApplyDate = XDate,
                ID_Raees_Lagnat = Convert.ToInt32(DLIDRaeesShoon.SelectedValue),
                Is_Raees_Lagnat_Allow = false,
                Is_Raees_Lagnat_Not_Allow = false,
                Apply_Raees_Lagnat_Date = XDate,
                ApprovedBy = "ITFYEduu@gmail.com",
                ApproveDate = XDate,
                CreatedDate = XDate,
                CreatedBy = 0,
                ModifiedBy = XIDAdd,
                ModifiedDate = XDate,
                IsActive = true,
                IsApprove = false,
                Is_Basic = _Is_Basic
            };
            Repostry_EmployeeLeaveCategory_ RELC = new Repostry_EmployeeLeaveCategory_();
            string Xresult = RELC.FErp_EmployeeLeaveCategory_Add(MELC);
            if (Xresult == "IsExistsNumberEdit")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رقم الإجازة مستخدم مسبقاً ... ";
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
                   " إضافة إجاوة لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberLeave.Text.Trim(), XDate);
                else if (XType == "Admin")
                    Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "تعديل",
                   "تعديل طلب إجازة له " + " برقم " + txtNumberLeave.Text.Trim(), XDate);
                if (DLSend.SelectedValue == "Yes")
                {
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تعديل " + ddlLeaveCategory.SelectedItem.Text + "\n" + "عدد الأيام : " + txtTotalLeave.Text.Trim() + "\n" + "رقم الإجازة : " + txtNumberLeave.Text.Trim(), "BerArn", "Edit", XIDAdd);
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone2.Value, "تم إختيارك الموظف البديل لـ " + "\n" + ddlEmployee.SelectedItem.Text + "\n" + "عدد الأيام : " + txtTotalLeave.Text.Trim(), "BerArn", "Edit", XIDAdd);
                }
                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل إجازة لموظف" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Edit", XIDAdd);
            }
        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeLeaveCategory.aspx");
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        IDLeaveDetails.Visible = false;
        ddlEmployee.SelectedValue = null;
        FGetLastRecord();
        txtStartDate.Text = Convert.ToDateTime(txtStartDate.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
        txtEndDate.Text = Convert.ToDateTime(txtEndDate.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetPhoneAndEmail(1, ddlEmployee.SelectedValue);
        FCheckR(new Guid(ddlEmployee.SelectedValue));
    }

    private void FCheckR(Guid XID)
    {
        IDLeaveDetails.Visible = true;

        // الاعتيادي
        HFCountDay.Value = Repostry_FinancialYear_.FErp_FinancialYear_ByID(new Guid(ddlYears.SelectedValue), "Org");
        lblCountDay.Text = HFCountDay.Value;

        HFCountDayUse.Value = Repostry_EmployeeLeaveCategory_.BErp_EmployeeLeaveCategory_SumByEmp(XID, new Guid(ddlYears.SelectedValue), 1);
        lblCountDayUse.Text = HFCountDayUse.Value;

        HFCountDayAllow.Value = Convert.ToString(Convert.ToDecimal(HFCountDay.Value) - Convert.ToDecimal(HFCountDayUse.Value));
        lblCountDayAllow.Text = HFCountDayAllow.Value;

        // الاضطراري
        HFCountEmergency.Value = Repostry_FinancialYear_.FErp_FinancialYear_ByID(new Guid(ddlYears.SelectedValue), "Emg");
        lblCountEmergency.Text = HFCountEmergency.Value;

        HFCountEmergencyUse.Value = Repostry_EmployeeLeaveCategory_.BErp_EmployeeLeaveCategory_SumByEmp(XID, new Guid(ddlYears.SelectedValue), 4);
        lblCountEmergencyUse.Text = HFCountEmergencyUse.Value;

        HFCountEmergencyAllow.Value = Convert.ToString(Convert.ToDecimal(HFCountEmergency.Value) - Convert.ToDecimal(HFCountEmergencyUse.Value));
        lblCountEmergencyAllow.Text = HFCountEmergencyAllow.Value;

        // التعويضي
        HFCountCompensatory.Value = Repostry_EmployeeCompensatory_.BErp_EmployeeLeaveCompensatory_SumByEmp(XID, new Guid(ddlYears.SelectedValue));
        lblCountCompensatory.Text = HFCountCompensatory.Value;

        HFCountCompensatoryUse.Value = Repostry_EmployeeLeaveCategory_.BErp_EmployeeLeaveCategory_SumByEmp(XID, new Guid(ddlYears.SelectedValue), 2);
        lblCountCompensatoryUse.Text = HFCountCompensatoryUse.Value;

        HFCountCompensatoryAllow.Value = Convert.ToString(Convert.ToDecimal(HFCountCompensatory.Value) - Convert.ToDecimal(HFCountCompensatoryUse.Value));
        lblCountCompensatoryAllow.Text = HFCountCompensatoryAllow.Value;
    }

    private void FGetPhoneAndEmail(Int16 XProssess, string XID)
    {
        if (XProssess == 1)
        {
            Repostry_Employee_.FGetPhoneAndEmail(XID, HFPhone, HFEmail);
            lblPhone.InnerText = HFPhone.Value;
        }
        else if (XProssess == 2)
        {
            HFPhone2.Value = ClassAdmin_Arn.FGetPhoneByID(Convert.ToInt32(DLIDEmp.SelectedValue));
            lblPhone2.InnerText = HFPhone2.Value;
        }
    }

    protected void DLIDEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetPhoneAndEmail(2, ddlEmployee.SelectedValue);
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