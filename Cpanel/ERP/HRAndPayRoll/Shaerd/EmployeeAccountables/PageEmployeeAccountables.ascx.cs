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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeAccountables_PageEmployeeAccountables : System.Web.UI.UserControl
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
            pnlSelect.Visible = true;
            if (XType == "Manager")
            {
                CLS_Permissions.CheckAccountAdmin("A165", "A166", btnDelete1, GVEmpAccountables, 0, 10); GVEmpAccountables.Columns[11].Visible = false;
            }
            else if (XType == "Admin")
            {
                btnDelete1.Visible = false; GVEmpAccountables.Columns[0].Visible = false; GVEmpAccountables.Columns[10].Visible = false;
                IDAdd.Visible = false; GVEmpAccountables.Columns[11].Visible = true;
            }
            
            txtDateFrom.Text = ClassSaddam.FGetDateFrom_HRM();
            txtDateTo.Text = ClassSaddam.FGetDateToLast();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
        }
    }

    private void FGetData()
    {
        GetCookie();
        //try
        //{
            string XIDCheck = string.Empty, XTypeEmp = string.Empty;
            GVEmpAccountables.UseAccessibleHeader = false;
            if (XType == "Manager")
            { XIDCheck = "GetAll"; XTypeEmp = "الموظفين"; }
            else if (XType == "Admin")
            { XIDCheck = "GetAllByAdmin"; XTypeEmp = "الموظف"; }
            DataTable dt = new DataTable();
            dt = Repostry_EmployeeAccountable_.FGetDataInDataTable(XIDCheck, new Guid(IDUniq), new Guid(ddlYears.SelectedValue),
                0, txtSearch.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), false, true);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "إرشيف : " + ddlYears.SelectedItem.ToString() + ", قائمة مساءلات " + XTypeEmp + " من تاريخ " + txtDateFrom.Text.Trim() +
                   " إلى تاريخ " + txtDateTo.Text.Trim();
                GVEmpAccountables.DataSource = dt;
                GVEmpAccountables.DataBind();
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
        //}
        //catch (Exception)
        //{
        //    return;
        //}
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeAccountables.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVEmpAccountables.Columns[0].Visible = false;
            GVEmpAccountables.Columns[10].Visible = false;
            GVEmpAccountables.Columns[11].Visible = false;
            GVEmpAccountables.UseAccessibleHeader = true;
            GVEmpAccountables.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlprint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
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
            foreach (GridViewRow row in GVEmpAccountables.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpAccountables.DataKeys[row.RowIndex].Value);
                    Xresult = Repostry_EmployeeAccountable_.FAPP("Delete", new Guid(Comp_ID), Guid.Empty, Guid.Empty, 0, string.Empty, Guid.Empty,
                        string.Empty, string.Empty, false, string.Empty, string.Empty, false, false, false, false, false, false, 0, false, false,
                        string.Empty, string.Empty, 0, XDelete, 0, XDate, false);
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData();
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        int XIDAdd = Test_Saddam.FGetIDUsiq();
        if (txtSearch.Text.Trim() == string.Empty)
        {
            if (XType == "Manager")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "عرض",
                       "عرض مساءلات الموظفين", XDate);
            }
            else if (XType == "Admin")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "عرض",
                     "عرض مساءلات الخاصة به", XDate);
            }
        }
        else if (txtSearch.Text.Trim() != string.Empty)
        {
            if (XType == "Manager")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "بحث",
                       "بحث " + txtSearch.Text.Trim() + " في مساءلات الموظفين", XDate);
            }
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        txtDateFrom.Text = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "MM-dd");
        txtDateTo.Text = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "12-31");
        FGetData();
    }

}