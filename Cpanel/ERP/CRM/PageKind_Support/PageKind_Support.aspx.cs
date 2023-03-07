using Library_CLS_Arn.CRM.Models;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_CRM_PageKind_Support_PageKind_Support : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
                txtStartDate.Text = new DateTime(ClassSaddam.GetCurrentTime().Year, ClassSaddam.GetCurrentTime().Month, 1).ToString("yyyy-MM-dd");
                txtEndDate.Text = new DateTime(ClassSaddam.GetCurrentTime().Year, ClassSaddam.GetCurrentTime().AddMonths(1).Month, 1).ToString("yyyy-MM-dd");
                pnlSelect.Visible = true;
                FCheckFilter();
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

    private void FCheckFilter()
    {
        FCheck();
        if (RBIsGet.Checked && RBIsSet.Checked == false)
            FCRM_In_Kind_Support_Manage("GetByDateEXISTS", " قائمة جميع فواتير الدعم من تاريخ " + txtStartDate.Text.Trim() + " - إلى تاريخ " + txtEndDate.Text.Trim());
        else if (RBIsGet.Checked == false && RBIsSet.Checked)
            FCRM_In_Kind_Support_Manage("GetByDateNotEXISTS", " قائمة الشركات التي لم تدعم من تاريخ " + txtStartDate.Text.Trim() + " - إلى تاريخ " + txtEndDate.Text.Trim());
    }

    private void FCRM_In_Kind_Support_Manage(string XIDCheck, string XMessage)
    {
        GVBillAll.UseAccessibleHeader = false;
        GVBillAll.Columns[8].Visible = true;
        Model_In_Kind_Support_ MIKS = new Model_In_Kind_Support_();
        MIKS.IDCheck = XIDCheck;
        MIKS.ID_Item = new Guid(ddlYears.SelectedValue);
        MIKS.ID_Company = Guid.Empty;
        MIKS.Start_Date = txtStartDate.Text.Trim();
        MIKS.End_Date = txtEndDate.Text.Trim();
        MIKS.CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
        MIKS.Is_Delete = false;
        DataTable dt = new DataTable();
        Repostry_In_Kind_Support_ RIKS = new Repostry_In_Kind_Support_();
        dt = RIKS.BCRM_In_Kind_Support_Manage(MIKS);
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = XMessage;
            GVBillAll.DataSource = dt;
            GVBillAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            btnPrint.Visible = true;
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
            btnPrint.Visible = false;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            GVBillAll.UseAccessibleHeader = true;
            GVBillAll.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVBillAll.Columns[8].Visible = false;
            Session["footable1"] = pnlPrint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageKind_Support.aspx");
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        try
        {
            FCheckFilter();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FCheck()
    {
        if (RBIsGet.Checked == false && RBIsSet.Checked)
            GVBillAll.Columns[8].Visible = false;
        else if (RBIsGet.Checked && RBIsSet.Checked == false)
            GVBillAll.Columns[8].Visible = true;
    }

}