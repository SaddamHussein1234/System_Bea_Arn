using System;
using System.Collections.Generic;
using System.Data;
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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_JobAssignments_PageEmployeeJobAssignmentAdd : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (XType == "Manager")
            {
                CLS_Permissions.CheckAccountAdmin("A156");
                IDAccess.Visible = true;
            }
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            Repostry_Employee_.FErp_Employee_Master_Manage(CBEmployee);
            txtDate_Job.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            txtDateEnd_Job.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModer);
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
            DataTable dt = new DataTable();
            dt = Repostry_JobAssignment_.FGetDataInDataTable("GetLastRecord", Guid.Empty, new Guid(ddlYears.SelectedValue), 0,
                string.Empty, string.Empty, string.Empty, false, false, true);
            if (dt.Rows.Count > 0)
                txtNumberJob.Text = Convert.ToString(Convert.ToInt64(dt.Rows[0]["Number_Job_"]) + 1);
            else
                txtNumberJob.Text = ClassSaddam.FGetNumberBillStart().ToString();

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
            dt = Repostry_JobAssignment_.FGetDataInDataTable("GetByIDUniq", new Guid(Request.QueryString["ID"]), Guid.Empty, 0,
                string.Empty, string.Empty, string.Empty, false, false, true);

            if (dt.Rows.Count > 0)
            {
                FGetByIDJobAssignment();
                ddlYears.SelectedValue = dt.Rows[0]["FinancialYear_Id_"].ToString();
                txtNumberJob.Text = dt.Rows[0]["Number_Job_"].ToString();
                //ddlDepartment.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                //Repostry_Employee_.FErp_Employee_Master_Manage(new Guid(ddlDepartment.SelectedValue), ddlEmployee);
                //ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeId"].ToString();
                FGetPhoneAndEmail(); DLSend.SelectedValue = "No";
                DLAssignment_Title.SelectedValue = dt.Rows[0]["Assignment_Title_"].ToString();
                txtThe_Assignment.Text = dt.Rows[0]["The_Assignment_"].ToString();
                txtHours_In_Day.Text = dt.Rows[0]["Hours_In_Day_"].ToString();
                CBIs_Mandate.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Mandate_"]);
                txtAmount.Text = dt.Rows[0]["Amount_"].ToString();
                ddlTotalDays.SelectedValue = dt.Rows[0]["TotalDays_"].ToString();
                txtDate_Job.Text = Convert.ToDateTime(dt.Rows[0]["Date_Job_"]).ToString("yyyy-MM-dd");
                txtDateEnd_Job.Text = Convert.ToDateTime(dt.Rows[0]["Date_End_Job_"]).ToString("yyyy-MM-dd");
                DLTime_Assignment.SelectedValue = dt.Rows[0]["Time_Assignment_"].ToString();
                txtThe_Qriah.Text = dt.Rows[0]["The_Qriah_"].ToString().Replace("<br />", Environment.NewLine);
                if (Convert.ToBoolean(dt.Rows[0]["Moder_Raees_"]))
                { RB_Moder.Checked = false; RB_Raees.Checked = true; DLIDRaees.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); }
                else
                { { RB_Moder.Checked = true; RB_Raees.Checked = false; DLModer.SelectedValue = dt.Rows[0]["ID_Moder_"].ToString(); } }
            }
            else
                Response.Redirect("PageEmployeeJobAssignments.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageEmployeeJobAssignments.aspx");
        }
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeJobAssignmentAdd.aspx");
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
            if (Convert.ToDateTime(txtDate_Job.Text.Trim()).ToString("yyyy") == ddlYears.SelectedItem.ToString())
            {
                if (CBEmployee.Items.Cast<ListItem>().Any(item => item.Selected))
                {
                    if (CBIs_Mandate.Checked)
                    {
                        if (txtAmount.Text.Trim() != string.Empty && ddlTotalDays.SelectedValue != string.Empty)
                            FEmployeeAccountable_Add(Convert.ToDecimal(txtAmount.Text.Trim()), Convert.ToInt32(ddlTotalDays.SelectedValue),
                                    Convert.ToDecimal(txtAmount.Text.Trim()) * Convert.ToInt32(ddlTotalDays.SelectedValue));
                        else
                        {
                            lblWarning.Text = "يجب إدخال المبلغ وتحديد الأيام ,,, ";
                            IDMessageSuccess.Visible = false;
                            IDMessageWarning.Visible = true;
                            txtAmount.Focus();
                            return;
                        }
                    }
                    else
                        FEmployeeAccountable_Add(0, 0, 0);
                }
                else
                {
                    IDMessageSuccess.Visible = false;
                    IDMessageWarning.Visible = true;
                    lblWarning.Text = "يُرجى تحديد الموظفين ,,, ";
                    return;
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
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FEmployeeAccountable_Add(decimal XAmount, int XTotalDays, decimal XTotal_Amount)
    {
        string XFrom_Send = string.Empty;
        if (XType == "Manager")
            XFrom_Send = "الإدارة";
        else if (XType == "Admin")
            XFrom_Send = "نظام الموظف";
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        int XIDAdmin = Test_Saddam.FGetIDUsiq();
        int XModer, XIDAdd = 0, XUpdate = 0;
        Guid XID = Guid.Empty;
        string XCheck = string.Empty, Xresult = string.Empty;
        if (Request.QueryString["ID"] == null)
        {
            XCheck = "Add"; XID = Guid.NewGuid(); XIDAdd = XIDAdmin;
        }
        if (Request.QueryString["ID"] != null)
        {
            XCheck = "Edit"; XID = new Guid(Request.QueryString["ID"]); XUpdate = XIDAdmin;
        }
        bool XModer_Raees = false; XModer = Convert.ToInt32(DLModer.SelectedValue);
        if (RB_Moder.Checked == false && RB_Raees.Checked)
        { XModer_Raees = true; XModer = Convert.ToInt32(DLIDRaees.SelectedValue); }
        Xresult = Repostry_JobAssignment_.FAPP(XCheck, XID, Guid.Empty, new Guid(ddlYears.SelectedValue),
            Convert.ToInt64(txtNumberJob.Text.Trim()), txtDate_Job.Text.Trim(), txtDateEnd_Job.Text.Trim(),
            DLAssignment_Title.SelectedValue, txtThe_Assignment.Text.Trim(), DLTime_Assignment.SelectedValue,
            Convert.ToInt32(txtHours_In_Day.Text.Trim()), CBIs_Mandate.Checked, XAmount, XTotalDays, XTotal_Amount,
            txtThe_Qriah.Text.Trim().Replace(Environment.NewLine, "<br />"),
            true, XDate, false, XDate, XModer_Raees, XModer, false, false, string.Empty, XDate,
            0, false, false, false, XDate, string.Empty, false, 0, XDate,
            false, 0, XDate, false, XDate, false, XDate, 0, Guid.Empty, false, XDate, 0, XDate, XDate,
            XFrom_Send, XIDAdd, XUpdate, 0, XDate, true);
        if (Xresult == "IsExistsNumber")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "رقم المهام مستخدم مسبقاً ... ";
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
            FAdd(XID, XCheck, XAmount, XTotalDays, XTotal_Amount, XIDAdd, XUpdate, XDate, XIDAdmin);
    }

    private void FAdd(Guid XID, string XCheck, decimal XAmount,
        int XTotalDays, decimal XTotal_Amount, int XIDAdd, int XUpdate, string XDate, int XIDAdmin)
    {
        string XResult = string.Empty, XresultMandate = string.Empty;
        XResult = Repostry_JobAssignment_Map_.FAPP("Delete", Guid.Empty, Guid.Empty, Guid.Empty, XID, false, string.Empty, 0, 0, string.Empty, false);
        if (XResult == "IsSuccess")
        {
            foreach (ListItem LI in CBEmployee.Items)
            {
                if (LI.Selected)
                {
                    XResult = Repostry_JobAssignment_Map_.FAPP("Add", Guid.NewGuid(), new Guid(ddlYears.SelectedValue),
                        new Guid(LI.Value), XID, false, XDate, XIDAdd, XUpdate, XDate, true);
                }
            }
            if (XResult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                long XNumberMandate = 0;
                if (Request.QueryString["ID"] == null)
                {
                    //if (CBIs_Mandate.Checked)
                    //{
                    //    DataTable dtMandate = new DataTable();
                    //    dtMandate = Repostry_EmployeeMandate_.FGetDataInDataTable("GetLastRecord", Guid.Empty, new Guid(ddlYears.SelectedValue), 0,
                    //        string.Empty, string.Empty, string.Empty, false, false, true);
                    //    if (dtMandate.Rows.Count > 0)
                    //        XNumberMandate = Convert.ToInt64(dtMandate.Rows[0]["Number_Mandate_"]) + 1;
                    //    else
                    //        XNumberMandate = ClassSaddam.FGetNumberBillStart();

                    //    XresultMandate = Repostry_EmployeeMandate_.FAPP(XCheck, XID, new Guid(ddlEmployee.SelectedValue), new Guid(ddlYears.SelectedValue),
                    //     XNumberMandate, XAmount, XTotal_Amount, txtDate_Job.Text.Trim(), txtDateEnd_Job.Text.Trim(), txtThe_Assignment.Text.Trim(),
                    //     txtThe_Qriah.Text.Trim().Replace(Environment.NewLine, "<br />"), XTotalDays,
                    //     Convert.ToInt32(DLModer.SelectedValue), true, false, string.Empty, XDate, Convert.ToInt32(DLModer.SelectedValue),
                    //     false, false, XDate, string.Empty, XIDAdd, XUpdate, 0, XDate, true, false);
                    //}
                    if (DLSend.SelectedValue == "Yes")
                    {
                        foreach (ListItem LI in CBEmployee.Items)
                        {
                            if (LI.Selected)
                            {
                                Attach_Repostry_SMS_Send_.FAddSMSMessage(LI.Text.Split(new char[] { '[', ']' })[1], "لديك مهام عمل جديد" + "\n" + "رقم المهام : " +
                                    txtNumberJob.Text.Trim() + "\n" + "يُرجى المتابعة ,,,", "BerArn", "Add", XIDAdmin);
                            }
                        }
                    }
                    Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "إضافة", "إضافة مهمة عمل برقم " + txtNumberJob.Text.Trim(), XDate);

                    if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "إضافة مهمة عمل" + "\n" + "برقم :" + txtNumberJob.Text.Trim() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Add", XIDAdmin);
                }
                if (Request.QueryString["ID"] != null)
                {
                    //if (CBIs_Mandate.Checked)
                    //{
                    //    DataTable dtMandate = new DataTable();
                    //    dtMandate = Repostry_EmployeeMandate_.FGetDataInDataTable("GetByIDUniq", new Guid(Request.QueryString["ID"]),
                    //    new Guid(ddlYears.SelectedValue), 0, string.Empty, string.Empty, string.Empty, false, false, true);
                    //    if (dtMandate.Rows.Count > 0)
                    //        XNumberMandate = Convert.ToInt64(dtMandate.Rows[0]["Number_Mandate_"].ToString());

                    //    XresultMandate = Repostry_EmployeeMandate_.FAPP(XCheck, XID, new Guid(ddlEmployee.SelectedValue), new Guid(ddlYears.SelectedValue),
                    //     XNumberMandate, XAmount, XTotal_Amount, txtDate_Job.Text.Trim(), txtDateEnd_Job.Text.Trim(), txtThe_Assignment.Text.Trim(),
                    //     txtThe_Qriah.Text.Trim().Replace(Environment.NewLine, "<br />"), XTotalDays,
                    //     Convert.ToInt32(DLModer.SelectedValue), true, false, string.Empty, XDate, Convert.ToInt32(DLModer.SelectedValue),
                    //     false, false, XDate, string.Empty, XIDAdd, XUpdate, 0, XDate, true, false);
                    //}
                    if (DLSend.SelectedValue == "Yes")
                    {
                        foreach (ListItem LI in CBEmployee.Items)
                        {
                            if (LI.Selected)
                            {
                                Attach_Repostry_SMS_Send_.FAddSMSMessage(LI.Text.Split(new char[] { '[', ']' })[1], "تعديل مهام عمل" + "\n" + "رقم المهام : " +
                                    txtNumberJob.Text.Trim() + "\n" + "يُرجى المتابعة ,,,", "BerArn", "Edit", XIDAdmin);
                            }
                        }
                    }
                    Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "تعديل", "تعديل مهمة عمل برقم " + txtNumberJob.Text.Trim(), XDate);
                    if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "تعديل مهمة عمل" + "\n" + "برقم :" + txtNumberJob.Text.Trim() + "\n" + "بإنتظار الإطلاع,,,", "BerArn", "Edit", XIDAdmin);
                }
                Response.Redirect("PageEmployeeJobAssignmentDetails.aspx?ID=" + XID);
            }
        }
    }

    public string FCheck(string XCheck)
    {
        string XResult = "display:none;";
        if (XCheck == "_Mandate")
        {
            if (CBIs_Mandate.Checked)
                XResult = "display:block;";
            else if (CBIs_Mandate.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "_Moder")
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

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeJobAssignments.aspx");
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetLastRecord();
        txtDate_Job.Text = Convert.ToDateTime(txtDate_Job.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
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
    }

}