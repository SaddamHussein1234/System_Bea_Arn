using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_JobAssignments_PageEmployeeJobAssignmentsList : System.Web.UI.UserControl
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
            txtDateFrom.Text = ClassSaddam.FGetDateTo();
            txtDateTo.Text = ClassSaddam.GetCurrentTime().AddDays(6).ToString("yyyy-MM-dd");
            pnlSelect.Visible = true;
            if (XType == "Manager")
            {
                CLS_Permissions.CheckAccountAdmin("A155");
                IDDepartment.Visible = true;
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                   " عرض ملخص مهام الموظفين", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else if (XType == "Admin")
            {
                IDDepartment.Visible = false;
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                   " عرض ملخص المام الخاصه به", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
            }
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FillMonth();
            
        }
    }

    private void FillMonth()
    {
        int _FinancialYear = Convert.ToInt32(ddlYears.SelectedItem.Text);
        int _no = 0;

        bool _Flag = true;
        for (int no = 1; no < 13; no++)
        {
            _no = _no + 1;
            ddlMonth.Items.Insert(_no, new ListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(no) + " " + no.ToString() + "-" + _FinancialYear, Value = Convert.ToString(no) + "_" + _FinancialYear });

            if (no == ClassSaddam.GetCurrentTime().Month && _FinancialYear == ClassSaddam.GetCurrentTime().Year)
            {
                _Flag = false;
                break;
            }
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            pnlNull.Visible = false;
            pnlData.Visible = false;
            pnlSelect.Visible = true;
            ddlMonth.SelectedValue = null;
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSelect.Visible = true;
        ddlMonth.Items.Clear(); ddlMonth.Items.Add(""); ddlMonth.AppendDataBoundItems = true;
        FillMonth();
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        int _Month = ClassSaddam.GetCurrentTime().Month;
        int _Year = ClassSaddam.GetCurrentTime().Year;

        string[] _SplitDate = ddlMonth.SelectedValue.Split('_');

        if (_SplitDate.Length > 1)
        {
            _Month = Convert.ToInt32(_SplitDate[0]);
            _Year = Convert.ToInt32(_SplitDate[1]);
        }
        lbmsg.Text = _Month.ToString() + "  " + _Year.ToString();
        if (XType == "Manager")
        {
            if (CBDepartment.Checked)
                FGetData("GetByListByID", new Guid(ddlDepartment.SelectedValue), ddlDepartment.SelectedItem.Text
                    + " ملخص مهام الموظفين لشهر (" + ddlMonth.SelectedItem.ToString() + ")", _Month.ToString(), _Year.ToString());
            else if (CBDepartment.Checked == false)
                FGetData("GetByListAll", Guid.Empty, "ملخص مهام الموظفين لشهر (" + ddlMonth.SelectedItem.ToString() + ")", _Month.ToString(), _Year.ToString());
        }
        else if (XType == "Admin")
        {
            GetCookie();
            FGetData("GetByListByIDAdmin", new Guid(IDUniq), ddlDepartment.SelectedItem.Text
                + " ملخص المهام الخاص بك لشهر (" + ddlMonth.SelectedItem.ToString() + ")", _Month.ToString(), _Year.ToString());
        }
    }

    private void FGetData(string XCheck, Guid XID, string XTitle, string XFromDate, string XToDate)
    {
        try
        {
            string XTypeEmp = string.Empty;
            DataTable dt = new DataTable();
            dt = Repostry_JobAssignment_Map_.FGetDataInDataTable(XCheck, 1000, Guid.Empty, Guid.Empty, XID, Guid.Empty,
                string.Empty, XFromDate, XToDate, true, true);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = XTitle;
                RPTJobAssignments.DataSource = dt;
                RPTJobAssignments.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                pnlSelect.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    public string FGetCount(string XIDCheck, string XID)
    {
        string XResult = string.Empty;
        try
        {
            if (RBMonth.Checked && RBDate.Checked == false)
            {
                int _Month = ClassSaddam.GetCurrentTime().Month;
                int _Year = ClassSaddam.GetCurrentTime().Year;

                string[] _SplitDate = ddlMonth.SelectedValue.Split('_');

                if (_SplitDate.Length > 1)
                {
                    _Month = Convert.ToInt32(_SplitDate[0]);
                    _Year = Convert.ToInt32(_SplitDate[1]);
                }
                XResult = Repostry_JobAssignment_Map_.FGetCount(XIDCheck, 0, Guid.Empty, new Guid(ddlYears.SelectedValue), new Guid(XID),
                    Guid.Empty, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd"), _Month.ToString(), _Year.ToString(), false, true, "_Count").ToString();
            }
            else if (RBMonth.Checked == false && RBDate.Checked)
                XResult = Repostry_JobAssignment_Map_.FGetCount(XIDCheck + "D", 0, Guid.Empty, new Guid(ddlYears.SelectedValue), new Guid(XID),
                    Guid.Empty, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd"), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false, true, "_Count").ToString();
        }
        catch (Exception)
        {

        }
        return XResult;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["foot"] = pnlPrint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4Landscape.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeJobAssignmentsList.aspx");
    }

    public string FCheck(string XCheck)
    {
        string XResult = "display:none;";
        if (XCheck == "Department")
        {
            if (CBDepartment.Checked)
                XResult = "display:block;";
            else if (CBDepartment.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "_Month")
        {
            if (RBMonth.Checked)
                XResult = "display:block;";
            else if (RBMonth.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "_Date")
        {
            if (RBDate.Checked)
                XResult = "display:block;";
            else if (RBDate.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        return XResult;
    }

    protected void RPTJobAssignments_PreRender(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem item in RPTJobAssignments.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {

                    Label _lblCountActive = (Label)item.FindControl("lblCountActive");
                    Label _lblCountFinshWithComment = (Label)item.FindControl("lblCountFinshWithComment");
                    Label _lblCountFinshNot = (Label)item.FindControl("lblCountFinshNot");
                    Label _lblCountFinsh = (Label)item.FindControl("lblCountFinsh");
                    Label _lblCountLate = (Label)item.FindControl("lblCountLate");
                    Label _lblCountExtension = (Label)item.FindControl("lblCountExtension");
                    Label _lblCountConvert = (Label)item.FindControl("lblCountConvert");
                    Label _lblCountStoped = (Label)item.FindControl("lblCountStoped");
                    Label _lblCountDeny = (Label)item.FindControl("lblCountDeny");

                    //Label _lblCount = (Label)item.FindControl("lblCount");
                    Label _lblCountSum = (Label)item.FindControl("lblCountSum");

                    int XSum = int.Parse(_lblCountActive.Text) + int.Parse(_lblCountFinshWithComment.Text) + int.Parse(_lblCountFinshNot.Text) + int.Parse(_lblCountFinsh.Text)
                         + int.Parse(_lblCountLate.Text) + int.Parse(_lblCountExtension.Text) + int.Parse(_lblCountConvert.Text) + int.Parse(_lblCountStoped.Text)
                          + int.Parse(_lblCountDeny.Text);
                    _lblCountSum.Text = XSum.ToString();
                    //------------------------
                }
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        if (XType == "Manager")
        {
            if (CBDepartment.Checked)
                FGetData("GetByListByIDByDate", new Guid(ddlDepartment.SelectedValue), ddlDepartment.SelectedItem.Text
                    + " ملخص مهام الموظفين من تاريخ " + txtDateFrom.Text.Trim() + " إلى " + txtDateTo.Text.Trim(),
                    txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());
            else if (CBDepartment.Checked == false)
                FGetData("GetByListAllByDate", Guid.Empty, "ملخص مهام الموظفين من تاريخ " + txtDateFrom.Text.Trim() + " إلى " + 
                    txtDateTo.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());
        }
        else if (XType == "Admin")
        {
            GetCookie();
            FGetData("GetByListByIDAdminByDate", new Guid(IDUniq), ddlDepartment.SelectedItem.Text
                + " ملخص المهام الخاص بك لشهر (" + ddlMonth.SelectedItem.ToString() + ")",
                txtDateFrom.Text.Trim(), txtDateTo.Text.Trim());
        }
    }

}