using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeJobAssignmentsByRaees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A157");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            GVEmpJobAssignmentsByRaees.Columns[0].Visible = true;
            GVEmpJobAssignmentsByRaees.Columns[8].Visible = true;
            GVEmpJobAssignmentsByRaees.UseAccessibleHeader = false;
            DataTable dt = new DataTable();
            dt = Repostry_JobAssignment_.FGetDataInDataTable("GetByRaees", Guid.Empty, new Guid(ddlYears.SelectedValue), 0,
                txtSearch.Text.Trim(), string.Empty, string.Empty, true, false, true);
            if (dt.Rows.Count > 0)
            {
                GVEmpJobAssignmentsByRaees.DataSource = dt;
                GVEmpJobAssignmentsByRaees.DataBind();
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
        Response.Redirect("PageEmployeeJobAssignmentsByRaees.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVEmpJobAssignmentsByRaees.Columns[0].Visible = false;
            GVEmpJobAssignmentsByRaees.Columns[8].Visible = false;
            GVEmpJobAssignmentsByRaees.UseAccessibleHeader = true;
            GVEmpJobAssignmentsByRaees.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlprint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData();
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            string Xresult = string.Empty;
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            foreach (GridViewRow row in GVEmpJobAssignmentsByRaees.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpJobAssignmentsByRaees.DataKeys[row.RowIndex].Value);
                    Xresult = Repostry_JobAssignment_.FAPP("AllowRaees", new Guid(Comp_ID), Guid.Empty, Guid.Empty,
                    0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, false, 0, 0, 0, string.Empty,
                    false, string.Empty, false, string.Empty, false, 0, false, false, string.Empty, string.Empty,
                    0, true, false, false, XDate, string.Empty, false, 0, string.Empty, false, 0, string.Empty, false, string.Empty,
                    false, string.Empty, 0, Guid.Empty, false, string.Empty, 0, string.Empty, string.Empty, string.Empty, 0, 0, 0, string.Empty, false);
                }
            }
            if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                FGetData();
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

    protected void btnNotAllow_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            string Xresult = string.Empty;
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            foreach (GridViewRow row in GVEmpJobAssignmentsByRaees.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpJobAssignmentsByRaees.DataKeys[row.RowIndex].Value);
                    Xresult = Repostry_JobAssignment_.FAPP("AllowRaees", new Guid(Comp_ID), Guid.Empty, Guid.Empty,
                    0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, false, 0, 0, 0, string.Empty,
                    false, string.Empty, false, string.Empty, false, 0, false, false, string.Empty, string.Empty,
                    0, false, false, true, XDate, txtComments.Text.Trim(), false, 0, string.Empty, false, 0, string.Empty, false, string.Empty,
                    false, string.Empty, 0, Guid.Empty, false, string.Empty, 0, string.Empty, string.Empty, string.Empty, 0, 0, 0, string.Empty, false);
                }
            }
            if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم الموافقة بنجاح ... ";
                FGetData();
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

    protected void btnAllowWithComment_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            string Xresult = string.Empty;
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            foreach (GridViewRow row in GVEmpJobAssignmentsByRaees.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpJobAssignmentsByRaees.DataKeys[row.RowIndex].Value);
                    Xresult = Repostry_JobAssignment_.FAPP("AllowRaees", new Guid(Comp_ID), Guid.Empty, Guid.Empty,
                    0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, false, 0, 0, 0, string.Empty,
                    false, string.Empty, false, string.Empty, false, 0, false, false, string.Empty, string.Empty,
                    0, false, true, false, XDate, txtComments.Text.Trim(), false, 0, string.Empty, false, 0, string.Empty, false, string.Empty,
                    false, string.Empty, 0, Guid.Empty, false, string.Empty, 0, string.Empty, string.Empty, string.Empty, 0, 0, 0, string.Empty, false);
                }
            }
            if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                FGetData();
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

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetData();
    }

}