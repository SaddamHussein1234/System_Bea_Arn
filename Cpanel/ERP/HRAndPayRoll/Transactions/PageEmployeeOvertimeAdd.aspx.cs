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

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeOvertimeAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A183");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            txtDateAdd.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            txtStartDate.Text = txtDateAdd.Text; txtEndDate.Text = txtDateAdd.Text;
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModer);
            //ClassAdmin_Arn.FGetRaeesAlShaoon(DLIDRaees);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLIDRaees);
            FGetLastRecord();
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
            Model_EmployeeOverTime_ MEOT = new Model_EmployeeOverTime_();
            MEOT.IDCheck = "GetLastRecord";
            MEOT.EmployeeOverTimeMapID = Guid.Empty;
            MEOT.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEOT.Number_OverTime = 0;
            MEOT.CreatedDate = string.Empty;
            MEOT.Start_Date = string.Empty;
            MEOT.End_Date = string.Empty;
            MEOT.Is_Moder_Allow = false;
            MEOT.Is_Raees_Lagnat_Allow = false;
            MEOT.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeOverTime_ REOT = new Repostry_EmployeeOverTime_();
            dt = REOT.BErp_EmployeeOverTime_Manage(MEOT);
            if (dt.Rows.Count > 0)
                txtNumberOverTime.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["Number_OverTime_"]) + 1);
            else
                txtNumberOverTime.Text = ClassSaddam.FGetNumberBillStart().ToString();

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
            Model_EmployeeOverTime_ MEOT = new Model_EmployeeOverTime_();
            MEOT.IDCheck = "GetByIDUniq";
            MEOT.EmployeeOverTimeMapID = new Guid(Request.QueryString["ID"]);
            MEOT.FinancialYear_Id = Guid.Empty;
            MEOT.Number_OverTime = 0;
            MEOT.CreatedDate = string.Empty;
            MEOT.Start_Date = string.Empty;
            MEOT.End_Date = string.Empty;
            MEOT.Is_Moder_Allow = false;
            MEOT.Is_Raees_Lagnat_Allow = false;
            MEOT.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeOverTime_ REOT = new Repostry_EmployeeOverTime_();
            dt = REOT.BErp_EmployeeOverTime_Manage(MEOT);
            if (dt.Rows.Count > 0)
            {
                ddlYears.SelectedValue = dt.Rows[0]["FinancialYear_Id_"].ToString();
                txtNumberOverTime.Text = dt.Rows[0]["Number_OverTime_"].ToString();
                ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                Repostry_Employee_.FErp_Employee_Master_Manage(new Guid(ddlDepartment.SelectedValue), ddlEmployee);
                ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeId"].ToString();
                FGetPhoneAndEmail(); DLSend.SelectedValue = "No";
                HFAmount.Value = dt.Rows[0]["Amount"].ToString();
                lblAmount.Text = HFAmount.Value;
                string XFromTime = dt.Rows[0]["Start_Time_"].ToString(); string XToTime = dt.Rows[0]["End_Time_"].ToString();
                ddlFromHour.SelectedValue = XFromTime.Substring(0, 2);
                ddlFromMinute.SelectedValue = XFromTime.Substring(3, 2);
                ddlFromMeridiem.SelectedValue = XFromTime.Substring(6, 2);
                ddlToHour.SelectedValue = XToTime.Substring(0, 2);
                ddlToMinute.SelectedValue = XToTime.Substring(3, 2);
                ddlToMeridiem.SelectedValue = XToTime.Substring(6, 2);

                txtStartDate.Text = Convert.ToDateTime(dt.Rows[0]["Start_Date_"]).ToString("yyyy-MM-dd");
                txtEndDate.Text = Convert.ToDateTime(dt.Rows[0]["End_Date_"]).ToString("yyyy-MM-dd");
                txtTitle.Text = dt.Rows[0]["OverTimeTitle"].ToString();
                txtDescrption.Text = dt.Rows[0]["Description"].ToString().Replace("<br />", Environment.NewLine);
                ddlTotalDays.SelectedValue = dt.Rows[0]["TotalDays"].ToString();
                ddlTotalHours.SelectedValue = dt.Rows[0]["Hours_In_Day_"].ToString();

                txtDateAdd.Text = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]).ToString("yyyy-MM-dd");

                decimal XBaisc = Repostry_EmployeeSalary_.FErp_EmployeeSalary_Manage(new Guid(ddlEmployee.SelectedValue));
                HFBaiscHours.Value = String.Format("{0:0.00}", XBaisc);
                lblBaiscHours.Text = HFBaiscHours.Value;
                if (Convert.ToBoolean(dt.Rows[0]["Moder_Raees_"]))
                { RB_Moder.Checked = false; RB_Raees.Checked = true; DLIDRaees.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); }
                else
                { { RB_Moder.Checked = true; RB_Raees.Checked = false; DLModer.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); } }
            }
            else
                Response.Redirect("PageEmployeeOverTimes.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageEmployeeOverTimes.aspx");
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeOvertimeAdd.aspx");
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
            if (Convert.ToDateTime(txtStartDate.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
                FEmployeeOverTime_Add();
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

    private void FEmployeeOverTime_Add()
    {
        int XIDAdd = Test_Saddam.FGetIDUsiq(), XModer = 0;
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        bool XModer_Raees = false; XModer = Convert.ToInt32(DLModer.SelectedValue);
        if (RB_Moder.Checked == false && RB_Raees.Checked)
        { XModer_Raees = true; XModer = Convert.ToInt32(DLIDRaees.SelectedValue); }
        decimal XAmount = Convert.ToDecimal(HFAmount.Value);
        decimal XTotalAmount = Convert.ToDecimal(HFAmount.Value) * Convert.ToDecimal(ddlTotalDays.SelectedValue);
        if (Request.QueryString["ID"] == null)
        {
            Model_EmployeeOverTime_ MEOT = new Model_EmployeeOverTime_()
            {
                IDCheck = "Add",
                EmployeeOverTimeMapID = Guid.NewGuid(),
                EmployeeId = new Guid(ddlEmployee.SelectedValue),
                FinancialYear_Id = new Guid(ddlYears.SelectedValue),
                Number_OverTime = Convert.ToInt64(txtNumberOverTime.Text.Trim()),
                Amount = XAmount,
                Total_Amount = XTotalAmount,
                Start_Time = ddlFromHour.SelectedValue + ":" + ddlFromMinute.SelectedValue + " " + ddlFromMeridiem.SelectedValue,
                End_Time = ddlToHour.SelectedValue + ":" + ddlToMinute.SelectedValue + " " + ddlToMeridiem.SelectedValue,
                Start_Date = txtStartDate.Text.Trim(),
                End_Date = txtEndDate.Text.Trim(),
                OverTimeTitle = txtTitle.Text.Trim(),
                Description = txtDescrption.Text.Trim().Replace(Environment.NewLine, "<br />"),
                TotalDays = Convert.ToDecimal(ddlTotalDays.SelectedValue),
                Hours_In_Day = Convert.ToDecimal(ddlTotalHours.SelectedValue),
                Moder_Raees = XModer_Raees,
                ID_Moder = XModer,
                Is_Moder_Allow = true,
                Is_Moder_Not_Allow = false,
                Comments = string.Empty,
                ApplyDate = XDate,
                ID_Raees_Lagnat = XModer,
                Is_Raees_Lagnat_Allow = false,
                Is_Raees_Lagnat_Not_Allow = false,
                Apply_Raees_Lagnat_Date = XDate,
                Note_Raees = string.Empty,
                CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedBy = XIDAdd,
                ModifiedBy = 0,
                ModifiedDate = XDate,
                IsActive = true,
                IsComplete = false
            };
            Repostry_EmployeeOverTime_ REOT = new Repostry_EmployeeOverTime_();
            string Xresult = REOT.FErp_EmployeeOverTime_Add(MEOT);
            if (Xresult == "IsExistsNumberAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رقم القرار مستخدم مسبقاً ... ";
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
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "إضافة",
                   " إضافة عمل إضافي لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberOverTime.Text.Trim(), XDate);
                if (DLSend.SelectedValue == "Yes")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "عمل إضافي" + "\n" + "لمدة :" + ddlTotalDays.SelectedItem.Text.Trim() + " أيام" + "\n" + "الإجمالي :" + String.Format("{0:0.#}", XTotalAmount) + " " + ClassSaddam.FGetMonySaOutStyle() + "\n" + "برقم :" + txtNumberOverTime.Text.Trim() + "\n" + "يُرجى المتابعة,,,", "BerArn", "Add", XIDAdd);

                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة عمل إضافة" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Add", XIDAdd);
            }
        }
        else if (Request.QueryString["ID"] != null)
        {
            Model_EmployeeOverTime_ MEOT = new Model_EmployeeOverTime_()
            {
                IDCheck = "Edit",
                EmployeeOverTimeMapID = new Guid(Request.QueryString["ID"]),
                EmployeeId = new Guid(ddlEmployee.SelectedValue),
                FinancialYear_Id = new Guid(ddlYears.SelectedValue),
                Number_OverTime = Convert.ToInt64(txtNumberOverTime.Text.Trim()),
                Amount = XAmount,
                Total_Amount = XTotalAmount,
                Start_Time = ddlFromHour.SelectedValue + ":" + ddlFromMinute.SelectedValue + " " + ddlFromMeridiem.SelectedValue,
                End_Time = ddlToHour.SelectedValue + ":" + ddlToMinute.SelectedValue + " " + ddlToMeridiem.SelectedValue,
                Start_Date = txtStartDate.Text.Trim(),
                End_Date = txtEndDate.Text.Trim(),
                OverTimeTitle = txtTitle.Text.Trim(),
                Description = txtDescrption.Text.Trim().Replace(Environment.NewLine, "<br />"),
                TotalDays = Convert.ToDecimal(ddlTotalDays.SelectedValue),
                Hours_In_Day = Convert.ToDecimal(ddlTotalHours.SelectedValue),
                Moder_Raees = XModer_Raees,
                ID_Moder = XModer,
                Is_Moder_Allow = true,
                Is_Moder_Not_Allow = false,
                Comments = string.Empty,
                ApplyDate = XDate,
                ID_Raees_Lagnat = XModer,
                Is_Raees_Lagnat_Allow = false,
                Is_Raees_Lagnat_Not_Allow = false,
                Apply_Raees_Lagnat_Date = XDate,
                Note_Raees = string.Empty,
                CreatedDate = Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"),
                CreatedBy = 0,
                ModifiedBy = XIDAdd,
                ModifiedDate = XDate,
                IsActive = true,
                IsComplete = false
            };
            Repostry_EmployeeOverTime_ REOT = new Repostry_EmployeeOverTime_();
            string Xresult = REOT.FErp_EmployeeOverTime_Add(MEOT);
            if (Xresult == "IsExistsNumberEdit")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "رقم القرار مستخدم مسبقاً ... ";
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
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "تعديل",
                   " تعديل عمل إضافي لـ" + ddlEmployee.SelectedItem.Text + " برقم " + txtNumberOverTime.Text.Trim(), XDate);
                if (DLSend.SelectedValue == "Yes")
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(HFPhone.Value, "تعديل عمل إضافي" + "\n" + "لمدة :" + ddlTotalDays.SelectedItem.Text.Trim() + " أيام" + "\n" + "الإجمالي :" + String.Format("{0:0.#}", XTotalAmount) + " " + ClassSaddam.FGetMonySaOutStyle() + "\n" + "برقم :" + txtNumberOverTime.Text.Trim() + "\n" + "يُرجى المتابعة,,,", "BerArn", "Edit", XIDAdd);
                if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                    Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل عمل إضافي" + "\n" + "بإسم :" + ddlEmployee.SelectedItem.ToString() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Edit", XIDAdd);
            }
        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeOverTimes.aspx");
    }

    protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetPhoneAndEmail();
        HFBaiscHours.Value = "0"; HFAmount.Value = "0"; lblAmount.Text = "0";
        ddlTotalHours.SelectedValue = null;
        decimal XBaisc = Repostry_EmployeeSalary_.FErp_EmployeeSalary_Manage(new Guid(ddlEmployee.SelectedValue));
        HFBaiscHours.Value = String.Format("{0:0.00}", XBaisc);
        lblBaiscHours.Text = HFBaiscHours.Value;
    }

    protected void ddlTotalHours_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        decimal XTotal = Convert.ToDecimal(HFBaiscHours.Value) * Convert.ToDecimal(ddlTotalHours.SelectedValue);
        HFAmount.Value = String.Format("{0:0.00}", XTotal);
        lblAmount.Text = HFAmount.Value;
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetLastRecord();
        txtDateAdd.Text = Convert.ToDateTime(txtDateAdd.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
        txtStartDate.Text = Convert.ToDateTime(txtStartDate.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
        txtEndDate.Text = Convert.ToDateTime(txtEndDate.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
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