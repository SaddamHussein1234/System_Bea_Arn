using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_JobAssignments_PageEmployeeJobAssignments : System.Web.UI.UserControl
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
                GVEmpJobAssignments.Columns[0].Visible = true; GVEmpJobAssignments.Columns[11].Visible = true;
                GVEmpJobAssignments.Columns[12].Visible = false;
                CLS_Permissions.CheckAccountAdmin("A155", "A156", btnDelete1, GVEmpJobAssignments, 0, 11);
            }
            else if (XType == "Admin")
            {
                btnDelete1.Visible = false; IDAdd.Visible = false; GVEmpJobAssignments.Columns[0].Visible = false;
                GVEmpJobAssignments.Columns[11].Visible = false; GVEmpJobAssignments.Columns[12].Visible = true;
            }
            pnlSelect.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom_HRM();
            txtDateTo.Text = ClassSaddam.FGetDateToLast();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                   "عرض مهام الموظفين ", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    public void FGetData()
    {
        GetCookie();
        try
        {
            GVEmpJobAssignments.UseAccessibleHeader = false;
            string XIDCheck = string.Empty;
            if (XType == "Manager")
                XIDCheck = "GetAll";
            else if (XType == "Admin")
                XIDCheck = "GetAllByAdmin";
            DataTable dt = new DataTable();
            dt = Repostry_JobAssignment_.FGetDataInDataTable(XIDCheck, new Guid(IDUniq), new Guid(ddlYears.SelectedValue), 0, txtSearch.Text.Trim(),
                txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true, false, true);

            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "إرشيف : " + ddlYears.SelectedItem.ToString() + ", قائمة مهام العمل من تاريخ " + txtDateFrom.Text.Trim() +
                   " إلى تاريخ " + txtDateTo.Text.Trim();
                GVEmpJobAssignments.DataSource = dt;
                GVEmpJobAssignments.DataBind();
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeJobAssignments.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVEmpJobAssignments.Columns[0].Visible = false;
            GVEmpJobAssignments.Columns[11].Visible = false;
            GVEmpJobAssignments.Columns[12].Visible = false;
            GVEmpJobAssignments.UseAccessibleHeader = true;
            GVEmpJobAssignments.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["foot"] = pnlprint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4Landscape.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
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
        if (XType == "Manager")
        {
            GVEmpJobAssignments.Columns[0].Visible = true; GVEmpJobAssignments.Columns[11].Visible = true;
            GVEmpJobAssignments.Columns[12].Visible = false;
        }
        else if (XType == "Admin")
        {
            btnDelete1.Visible = false; IDAdd.Visible = false; GVEmpJobAssignments.Columns[0].Visible = false;
            GVEmpJobAssignments.Columns[11].Visible = false; GVEmpJobAssignments.Columns[12].Visible = true;
        }
        FGetData();
        Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "بحث",
                   "بحث : " + txtSearch.Text.Trim() + " في مهام الموظفين", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        txtDateFrom.Text = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "01-01");
        txtDateTo.Text = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "12-31");
        FGetData();
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty;
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            int XDelete = Test_Saddam.FGetIDUsiq();
            foreach (GridViewRow row in GVEmpJobAssignments.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid X_ID = new Guid(GVEmpJobAssignments.DataKeys[row.RowIndex].Values[0].ToString());
                    bool X_Mandate = Convert.ToBoolean(GVEmpJobAssignments.DataKeys[row.RowIndex].Values[1]);

                    Xresult = Repostry_JobAssignment_.FAPP("Delete", X_ID, Guid.Empty, Guid.Empty, 0, string.Empty, string.Empty,
                        string.Empty, string.Empty, string.Empty, 0, false, 0, 0, 0, string.Empty, false, string.Empty, false, string.Empty, false,
                        0, false, false, string.Empty, string.Empty, 0, false, false, false, string.Empty, string.Empty, false, 0, string.Empty, false, 0,
                        string.Empty, false, string.Empty,
                        false, string.Empty, 0, Guid.Empty, false, string.Empty, 0, string.Empty, string.Empty, string.Empty, 0, 0, XDelete, XDate, false);

                    if(X_Mandate)
                        Repostry_EmployeeMandate_.FAPP("Delete", X_ID, Guid.Empty, Guid.Empty,
                        0, 0, 0, string.Empty, string.Empty, string.Empty, string.Empty, 0,
                        false, 0, false, false, string.Empty, XDate, 0, false, false, XDate, string.Empty, 0, XDelete, 0, XDate, false, false);
                }
            }
            if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم حذف البيانات بنجاح ... ";
                FGetData();
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

    protected void LB_Stoped_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty;
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            Xresult = Repostry_JobAssignment_.FAPP("StopedByManager", new Guid(Convert.ToString((((LinkButton)sender).CommandArgument)).ToString()),
                        Guid.Empty, Guid.Empty, 0, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, false, 0, 0, 0, string.Empty, false,
                        string.Empty, false, string.Empty, false, 0, false, false, string.Empty, string.Empty, 0, false, false, false, string.Empty,
                        string.Empty, true, Test_Saddam.FGetIDUsiq(), XDate, false, 0, string.Empty, false, string.Empty,
                        false, string.Empty, 0, Guid.Empty, false, string.Empty, 0, string.Empty, string.Empty, string.Empty, 0, 0, 0, string.Empty, false);
            if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                FGetData();
                Attach_Repostry_SMS_Send_.FAddSMSMessage(Convert.ToString((((LinkButton)sender).CommandName)).ToString(),
                    "قام المختص بإيقاف" + "\n" + "المهام رقم : " + Convert.ToString((((LinkButton)sender).ValidationGroup)).ToString() + "\n" + "يُرجى المتابعة ,,,", "BerArn", "Add", Test_Saddam.FGetIDUsiq());
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

}