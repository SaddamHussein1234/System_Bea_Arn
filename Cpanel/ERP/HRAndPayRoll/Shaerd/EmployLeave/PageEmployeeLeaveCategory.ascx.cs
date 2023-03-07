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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployLeave_PageEmployeeLeaveCategory : System.Web.UI.UserControl
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
                CLS_Permissions.CheckAccountAdmin("A159", "A162", btnDelete1, GVEmpLeave, 0, 11); GVEmpLeave.Columns[12].Visible = false;
            }
            else if (XType == "Admin")
            {
                btnDelete1.Visible = false; GVEmpLeave.Columns[0].Visible = false; GVEmpLeave.Columns[11].Visible = false; IDAdd.Visible = false; GVEmpLeave.Columns[12].Visible = true;
            }

            pnlSelect.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom_HRM();
            txtDateTo.Text = ClassSaddam.FGetDateToLast();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
        }
    }

    private void FGetData()
    {
        GetCookie();
        try
        {
            string XTypeEmp = string.Empty;
            GVEmpLeave.UseAccessibleHeader = false;
            Model_EmployeeLeaveCategory_ MELC = new Model_EmployeeLeaveCategory_();
            if (XType == "Manager")
            { MELC.IDCheck = "GetAll"; XTypeEmp = "الموظفين"; }
            else if (XType == "Admin")
            { MELC.IDCheck = "GetAllByAdmin"; XTypeEmp = "الموظف"; }
            MELC.EmployeeLeaveCategoryMapID = new Guid(IDUniq);
            MELC.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MELC.Number_Leave = 0;
            MELC.CreatedDate = txtSearch.Text.Trim();
            MELC.StartDate = txtDateFrom.Text.Trim();
            MELC.EndDate = txtDateTo.Text.Trim();
            MELC.Is_Emp = false;
            MELC.Is_Moder_Allow = false;
            MELC.Is_Raees_Lagnat_Allow = false;
            MELC.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeLeaveCategory_ RELC = new Repostry_EmployeeLeaveCategory_();
            dt = RELC.BErp_EmployeeLeaveCategory_Manage(MELC);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "إرشيف : " + ddlYears.SelectedItem.ToString() + ", قائمة إجازات " + XTypeEmp + " من تاريخ " + txtDateFrom.Text.Trim() +
                    " إلى تاريخ " + txtDateTo.Text.Trim();
                GVEmpLeave.DataSource = dt;
                GVEmpLeave.DataBind();
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
        Response.Redirect("PageEmployeeLeaveCategory.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVEmpLeave.Columns[0].Visible = false;
            GVEmpLeave.Columns[11].Visible = false;
            GVEmpLeave.Columns[12].Visible = false;
            GVEmpLeave.UseAccessibleHeader = true;
            GVEmpLeave.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            foreach (GridViewRow row in GVEmpLeave.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpLeave.DataKeys[row.RowIndex].Value);
                    Model_EmployeeLeaveCategory_ MELC = new Model_EmployeeLeaveCategory_()
                    {
                        IDCheck = "Delete",
                        EmployeeLeaveCategoryMapID = new Guid(Comp_ID),
                        EmployeeId = Guid.NewGuid(),
                        LeaveCategoryId = Guid.NewGuid(),
                        Number_Leave = 0,
                        StartDate = string.Empty,
                        EndDate = string.Empty,
                        TotalDay = 0,
                        IsFirstHalfDay = false,
                        IsLastHalfDay = false,
                        Reason = string.Empty,
                        ID_Emp = 0,
                        Is_Emp = true,
                        Moder_Raees = false,
                        ID_Moder = 0,
                        Is_Moder_Allow = false,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = string.Empty,
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = false,
                        Is_Raees_Lagnat_Not_Allow = false,
                        Apply_Raees_Lagnat_Date = string.Empty,
                        ApprovedBy = string.Empty,
                        ApproveDate = string.Empty,
                        CreatedDate = string.Empty,
                        CreatedBy = 0,
                        ModifiedBy = Test_Saddam.FGetIDUsiq(),
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false,
                        IsApprove = false
                    };
                    Repostry_EmployeeLeaveCategory_ RELC = new Repostry_EmployeeLeaveCategory_();
                    string Xresult = RELC.FErp_EmployeeLeaveCategory_Add(MELC);
                    if (Xresult == "IsSuccessDelete")
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم حذف الملف بنجاح ... ";
                    }
                }
            }
            FGetData();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
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
                       "عرض إجازات الموظفين", XDate);
            }
            else if (XType == "Admin")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "عرض",
                     "عرض الإجازات الخاصة به", XDate);
            }
        }
        else if (txtSearch.Text.Trim() != string.Empty)
        {
            if (XType == "Manager")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "بحث",
                       "بحث " + txtSearch.Text.Trim() + " في إجازات الموظفين", XDate);
            }
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        txtDateFrom.Text = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "01-01");
        txtDateTo.Text = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "12-31");
        FGetData();
    }

}