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
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeMandatesByRaees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A180");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            GVEmpMandateByManager.Columns[0].Visible = true;
            GVEmpMandateByManager.Columns[11].Visible = true;
            GVEmpMandateByManager.UseAccessibleHeader = false;

            DataTable dt = new DataTable();
            dt = Repostry_EmployeeMandate_.FGetDataInDataTable("GetByRaees", Guid.Empty, new Guid(ddlYears.SelectedValue),
                0, txtSearch.Text.Trim(), string.Empty, string.Empty, true, false, true);
            if (dt.Rows.Count > 0)
            {
                GVEmpMandateByManager.DataSource = dt;
                GVEmpMandateByManager.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                     "عرض ملفات المشرف المختص لـ قائمة الإنتدابات", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
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
        Response.Redirect("PageEmployeeMandatesByRaees.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVEmpMandateByManager.Columns[0].Visible = false;
            GVEmpMandateByManager.Columns[11].Visible = false;
            GVEmpMandateByManager.UseAccessibleHeader = true;
            GVEmpMandateByManager.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetData();
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty, XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            int XDelete = Test_Saddam.FGetIDUsiq();
            foreach (GridViewRow row in GVEmpMandateByManager.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpMandateByManager.DataKeys[row.RowIndex].Value);
                    Xresult = Repostry_EmployeeMandate_.FAPP("AllowRaees", new Guid(Comp_ID), Guid.Empty, Guid.Empty,
                        0, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty, 0,
                        false, 0, false, false, txtComments.Text.Trim(), XDate, 0, true, false, txtComments.Text.Trim(), XDate, 0, XDelete, 0, XDate, true, false);
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

    protected void btnNotAllow_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty, XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            int XDelete = Test_Saddam.FGetIDUsiq();
            foreach (GridViewRow row in GVEmpMandateByManager.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpMandateByManager.DataKeys[row.RowIndex].Value);
                    Xresult = Repostry_EmployeeMandate_.FAPP("AllowRaees", new Guid(Comp_ID), Guid.Empty, Guid.Empty,
                        0, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty, 0,
                        false, 0, false, false, txtComments.Text.Trim(), XDate, 0, false, true, txtComments.Text.Trim(), XDate, 0, XDelete, 0, XDate, true, false);
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

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetData();
    }

}