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

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeLeaveCategoryByRaees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A164");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FGetData();
            Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                      "عرض ملفات المشرف المختص لـ قائمة الإجازات", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    private void FGetData()
    {
        try
        {
            GVEmpLeaveByManager.Columns[0].Visible = true;
            GVEmpLeaveByManager.Columns[11].Visible = true;
            GVEmpLeaveByManager.UseAccessibleHeader = false;

            Model_EmployeeLeaveCategory_ MELC = new Model_EmployeeLeaveCategory_();
            MELC.IDCheck = "GetByRaees";
            MELC.EmployeeLeaveCategoryMapID = Guid.Empty;
            MELC.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MELC.Number_Leave = 0;
            MELC.CreatedDate = txtSearch.Text.Trim();
            MELC.StartDate = string.Empty;
            MELC.EndDate = string.Empty;
            MELC.Is_Emp = true;
            MELC.Is_Moder_Allow = true;
            MELC.Is_Raees_Lagnat_Allow = false;
            MELC.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeLeaveCategory_ RELC = new Repostry_EmployeeLeaveCategory_();
            dt = RELC.BErp_EmployeeLeaveCategory_Manage(MELC);
            if (dt.Rows.Count > 0)
            {
                GVEmpLeaveByManager.DataSource = dt;
                GVEmpLeaveByManager.DataBind();
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
        Response.Redirect("PageEmployeeLeaveCategoryByRaees.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVEmpLeaveByManager.Columns[0].Visible = false;
            GVEmpLeaveByManager.Columns[11].Visible = false;
            GVEmpLeaveByManager.UseAccessibleHeader = true;
            GVEmpLeaveByManager.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            foreach (GridViewRow row in GVEmpLeaveByManager.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpLeaveByManager.DataKeys[row.RowIndex].Value);
                    Model_EmployeeLeaveCategory_ MELC = new Model_EmployeeLeaveCategory_()
                    {
                        IDCheck = "AllowRaees",
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
                        Is_Emp = false,
                        Moder_Raees = false,
                        ID_Moder = 0,
                        Is_Moder_Allow = false,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = true,
                        Is_Raees_Lagnat_Not_Allow = false,
                        Apply_Raees_Lagnat_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ApprovedBy = "0",
                        ApproveDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false,
                        IsApprove = true
                    };
                    Repostry_EmployeeLeaveCategory_ RELC = new Repostry_EmployeeLeaveCategory_();
                    string Xresult = RELC.FErp_EmployeeLeaveCategory_Add(MELC);
                    if (Xresult == "IsSuccessAllow")
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم الموافقة بنجاح ... ";
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

    protected void btnNotAllow_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            foreach (GridViewRow row in GVEmpLeaveByManager.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpLeaveByManager.DataKeys[row.RowIndex].Value);
                    Model_EmployeeLeaveCategory_ MELC = new Model_EmployeeLeaveCategory_()
                    {
                        IDCheck = "AllowRaees",
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
                        Is_Emp = false,
                        Moder_Raees = false,
                        ID_Moder = 0,
                        Is_Moder_Allow = false,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = false,
                        Is_Raees_Lagnat_Not_Allow = true,
                        Apply_Raees_Lagnat_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ApprovedBy = "hradmin@arityinfoway.com",
                        ApproveDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false,
                        IsApprove = false
                    };
                    Repostry_EmployeeLeaveCategory_ RELC = new Repostry_EmployeeLeaveCategory_();
                    string Xresult = RELC.FErp_EmployeeLeaveCategory_Add(MELC);
                    if (Xresult == "IsSuccessAllow")
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم الموافقة بنجاح ... ";
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

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData();
    }

}