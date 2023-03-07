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

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeLoansByManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A175");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            GVEmpLoanByManager.Columns[0].Visible = true;
            GVEmpLoanByManager.Columns[11].Visible = true;
            GVEmpLoanByManager.UseAccessibleHeader = false;
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            Model_EmployeeLoan_ MEL = new Model_EmployeeLoan_();
            MEL.IDCheck = "GetByAdmin";
            MEL.EmployeeLoanMapID = Guid.Empty;
            MEL.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEL.Number_Loan = 0;
            MEL.CreatedDate = txtSearch.Text.Trim();
            MEL.StartDate = string.Empty;
            MEL.EndDate = string.Empty;
            MEL.Is_Moder_Allow = false;
            MEL.Is_Raees_Lagnat_Allow = false;
            MEL.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeLoan_ REL = new Repostry_EmployeeLoan_();
            dt = REL.BErp_EmployeeLoan_Manage(MEL);
            if (dt.Rows.Count > 0)
            {
                GVEmpLoanByManager.DataSource = dt;
                GVEmpLoanByManager.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                      "عرض ملفات المشرف المختص لـ قائمة القروض/السلفة", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
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
        Response.Redirect("PageEmployeeLoansByManager.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVEmpLoanByManager.Columns[0].Visible = false;
            GVEmpLoanByManager.Columns[11].Visible = false;
            GVEmpLoanByManager.UseAccessibleHeader = true;
            GVEmpLoanByManager.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        try
        {
            foreach (GridViewRow row in GVEmpLoanByManager.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpLoanByManager.DataKeys[row.RowIndex].Value);
                    Model_EmployeeLoan_ MEL = new Model_EmployeeLoan_()
                    {
                        IDCheck = "AllowManager",
                        EmployeeLoanMapID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        Number_Loan = 0,
                        Amount = 0,
                        LoanDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        LoanTitle = string.Empty,
                        Description = string.Empty,
                        ApprovedBy = string.Empty,
                        TotalMonths = 0,
                        ID_Moder = 0,
                        Is_Moder_Allow = true,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = false,
                        Is_Raees_Lagnat_Not_Allow = false,
                        Apply_Raees_Lagnat_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = true,
                        IsComplete = false,
                        InstallmentMonth = 0
                    };
                    Repostry_EmployeeLoan_ REL = new Repostry_EmployeeLoan_();
                    string Xresult = REL.FErp_EmployeeLoan_Add(MEL);
                    if (Xresult == "IsSuccessAllow")
                    {
                        FGetData();
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم الموافقة بنجاح ... ";
                    }
                }
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
            foreach (GridViewRow row in GVEmpLoanByManager.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpLoanByManager.DataKeys[row.RowIndex].Value);
                    Model_EmployeeLoan_ MEL = new Model_EmployeeLoan_()
                    {
                        IDCheck = "AllowManager",
                        EmployeeLoanMapID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        Number_Loan = 0,
                        Amount = 0,
                        LoanDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        LoanTitle = string.Empty,
                        Description = string.Empty,
                        ApprovedBy = string.Empty,
                        TotalMonths = 0,
                        ID_Moder = 0,
                        Is_Moder_Allow = false,
                        Is_Moder_Not_Allow = true,
                        Comments = txtComments.Text.Trim(),
                        ApplyDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = false,
                        Is_Raees_Lagnat_Not_Allow = false,
                        Apply_Raees_Lagnat_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = true,
                        IsComplete = false,
                        InstallmentMonth = 0
                    };
                    Repostry_EmployeeLoan_ REL = new Repostry_EmployeeLoan_();
                    string Xresult = REL.FErp_EmployeeLoan_Add(MEL);
                    if (Xresult == "IsSuccessAllow")
                    {
                        FGetData();
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم الموافقة بنجاح ... ";
                    }
                }
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