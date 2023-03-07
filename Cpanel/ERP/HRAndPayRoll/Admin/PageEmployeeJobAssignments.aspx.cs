using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_HRAndPayRoll_Admin_PageEmployeeJobAssignments : System.Web.UI.Page
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
            txtDateFrom.Text = ClassSaddam.FGetDateFrom_HRM();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            //FGetCount("GetByAdminActiveCount", false, false, lblCountActive);
            FGetCount("GetByAdminNewCount", false, lblCountNew);
            FGetCount("GetByAdminLateCount", false, lblCountLate);
            FGetCount("GetByAdminConvertCount", false, lblCountConvert);
            FGetCount("GetByAdminStopedCount", false, lblCountStoped);
            FGetCount("GetByAdminFinshtodayCount", false, lblCountFinshtoday);
            FGetCount("GetByAdminDenyCount", false, lblCountDeny);
            FGetData("GetByAdminActive", false, GVEmpJobAssignmentsActive, lblCountActive, "قائمة مهام العمل السارية حالياً");
            FChangeStyle("btn btn-default", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading");
        }
    }

    private void FGetCount(string XIDCheck, bool XModer, Label lblCount)
    {
        GetCookie();
        try
        {
            lblCount.Text = Repostry_JobAssignment_Map_.FGetCount(XIDCheck, 0, Guid.Empty, new Guid(ddlYears.SelectedValue),
                new Guid(IDUniq), Guid.Empty, txtSearch.Text.Trim(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd"), txtDateTo.Text.Trim(),
                XModer, true, "_Count").ToString();
        }
        catch (Exception)
        {

        }
    }

    private void FGetData(string XIDCheck, bool XModer, GridView GV, Label lblCount, string XTitle)
    {
        GetCookie();
        try
        {
            GVEmpJobAssignmentsActive.Visible = false; GVEmpJobAssignmentsNew.Visible = false;
            GVEmpJobAssignmentsLate.Visible = false; GVEmpJobAssignmentsConverted.Visible = false;
            GVEmpJobAssignmentsStoped.Visible = false;
            GVEmpJobAssignmentsFinshtoday.Visible = false; GVEmpJobAssignmentsDeny.Visible = false;
            GV.UseAccessibleHeader = false;
            DataTable dt = new DataTable();
            dt = Repostry_JobAssignment_Map_.FGetDataInDataTable(XIDCheck, 1000, Guid.Empty, new Guid(ddlYears.SelectedValue),
                new Guid(IDUniq), Guid.Empty, txtSearch.Text.Trim(),
                ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd"), txtDateTo.Text.Trim(), XModer, true);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = XTitle;
                GV.Visible = true;
                GV.DataSource = dt;
                GV.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeJobAssignments.aspx");
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

    protected void btnActive_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData("GetByAdminActive", false, GVEmpJobAssignmentsActive, lblCountActive, "قائمة مهام العمل السارية حالياً");
        FChangeStyle("btn btn-default", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading");
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData("GetByAdminNew", false, GVEmpJobAssignmentsNew, lblCountNew, "قائمة مهام العمل الجديدة");
        FChangeStyle("btn btn-info Colorloading", "btn btn-default", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading");
    }

    protected void btnLate_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData("GetByAdminLate", false, GVEmpJobAssignmentsLate, lblCountLate, "قائمة مهام العمل المتأخرة");
        FChangeStyle("btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-default", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading");
    }

    protected void btnStoped_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData("GetByAdminStoped", false, GVEmpJobAssignmentsStoped, lblCountStoped, "قائمة مهام العمل المتوقفة");
        FChangeStyle("btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-default", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading");
    }

    protected void btnFinshtoday_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData("GetByAdminFinshtoday", false, GVEmpJobAssignmentsFinshtoday, lblCountFinshtoday, "قائمة مهام العمل اللتي أُنجزت اليوم");
        FChangeStyle("btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-default", "btn btn-info Colorloading", "btn btn-info Colorloading");
    }

    private void FChangeStyle(string XActiveStyle, string XNewStyle, string XLateStyle, string XStopedStyle, string XFinshtodayStyle, string XDenyStyle, string XConvertStyle)
    {
        btnActive.CssClass = XActiveStyle;
        btnActive.Text = "سارية ";

        btnNew.CssClass = XNewStyle;
        btnNew.Text = "جديدة ";

        btnLate.CssClass = XLateStyle;
        btnLate.Text = "متأخرة ";

        btnStoped.CssClass = XStopedStyle;
        btnStoped.Text = "متوقفة ";

        btnFinshtoday.CssClass = XFinshtodayStyle;
        btnFinshtoday.Text = "أُنجزت اليوم ";

        btnDeny.CssClass = XDenyStyle;
        btnDeny.Text = "مرفوضة ";

        btnConvert.CssClass = XDenyStyle;
        btnConvert.Text = "محولة ";
    }

    protected void LB_Accept_Click(object sender, EventArgs e)
    {
        FAllowOrDeny(sender, "AllowByAdmin", "_New", true, false, false);
    }

    protected void LB_Deny_Click(object sender, EventArgs e)
    {
        FAllowOrDeny(sender, "AllowByAdmin", "_New", false, true, false);
    }

    private void FAllowOrDeny(object sender, string XScript, string XTypeGet, bool XIsAllow, bool XIsDeny, bool XIsEnd)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty;
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            Xresult = Repostry_JobAssignment_.FAPP(XScript, new Guid(Convert.ToString((((LinkButton)sender).CommandArgument)).ToString()),
                Guid.Empty, Guid.Empty, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, false, 0, 0, 0, string.Empty,
                XIsAllow, XDate, XIsDeny, XDate, false, 0, false, false, string.Empty, string.Empty, 0, false, false, false,
                string.Empty, string.Empty, false, 0, string.Empty, false, 0, string.Empty, XIsEnd, XDate,
                false, string.Empty, 0, Guid.Empty, false, string.Empty, 0, string.Empty, string.Empty, string.Empty, 0, 0, 0, string.Empty, false);
            if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                if (XTypeGet == "_Active")
                {
                    FGetData("GetByAdminActive", false, GVEmpJobAssignmentsActive, lblCountActive, "قائمة مهام العمل السارية حالياً");
                    if (XIsEnd)
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(Convert.ToString((((LinkButton)sender).CommandName)).ToString(),
                            "قمت بالتحديد على أن" + "\n" + "المهام رقم : " + Convert.ToString((((LinkButton)sender).ValidationGroup)).ToString() + "\n" + "قد أُنجزت" + "\n" + "شكراً لك ,,,", "BerArn", "Add",
                            Test_Saddam.FGetIDUsiq());
                }
                else if (XTypeGet == "_New")
                {
                    FGetData("GetByAdminNew", false, GVEmpJobAssignmentsNew, lblCountNew, "قائمة مهام العمل الجديدة");
                    if (XIsAllow && XIsDeny == false)
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(Convert.ToString((((LinkButton)sender).CommandName)).ToString(),
                            "لقد قمت بقبول" + "\n" + "المهام رقم : " + Convert.ToString((((LinkButton)sender).ValidationGroup)).ToString() + "\n" + "شكراً لك ,,,", "BerArn", "Add",
                            Test_Saddam.FGetIDUsiq());
                    else if (XIsAllow == false && XIsDeny)
                        Attach_Repostry_SMS_Send_.FAddSMSMessage(Convert.ToString((((LinkButton)sender).CommandName)).ToString(),
                            "لقد قمت برفض" + "\n" + "المهام رقم : " + Convert.ToString((((LinkButton)sender).ValidationGroup)).ToString() + "\n" + "شكراً لك ,,,", "BerArn", "Add",
                            Test_Saddam.FGetIDUsiq());
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

    protected void LB_End_Click(object sender, EventArgs e)
    {
        FAllowOrDeny(sender, "EndByAdmin", "_Active", false, false, true);
    }

    protected void btnDeny_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData("GetByAdminDeny", false, GVEmpJobAssignmentsDeny, lblCountDeny, "قائمة مهام العمل المرفوضة");
        FChangeStyle("btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-default", "btn btn-info Colorloading");
    }

    protected void btnConvert_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData("GetByAdminConvert", false, GVEmpJobAssignmentsConverted, lblCountConvert, "قائمة مهام العمل المحولة");
        FChangeStyle("btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-info Colorloading", "btn btn-default");
    }

}