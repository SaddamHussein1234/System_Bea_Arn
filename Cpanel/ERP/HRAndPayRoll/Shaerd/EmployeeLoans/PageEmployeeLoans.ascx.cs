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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeLoans_PageEmployeeLoans : System.Web.UI.UserControl
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
                CLS_Permissions.CheckAccountAdmin("A172", "A173", btnDelete, GVEmployeeLoans, 0, 12); GVEmployeeLoans.Columns[13].Visible = false;
            }
            else if (XType == "Admin")
            {
                btnDelete.Visible = false; GVEmployeeLoans.Columns[0].Visible = false; GVEmployeeLoans.Columns[12].Visible = false;
                IDAdd.Visible = false; GVEmployeeLoans.Columns[13].Visible = true;
            }
            
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
            GVEmployeeLoans.UseAccessibleHeader = false;
            Model_EmployeeLoan_ MEL = new Model_EmployeeLoan_();
            if (XType == "Manager")
            { MEL.IDCheck = "GetAll"; XTypeEmp = "الموظفين"; }
            else if (XType == "Admin")
            { MEL.IDCheck = "GetAllByAdmin"; XTypeEmp = "الموظف"; }
            MEL.EmployeeLoanMapID = new Guid(IDUniq);
            MEL.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEL.Number_Loan = 0;
            MEL.CreatedDate = txtSearch.Text.Trim();
            MEL.StartDate = txtDateFrom.Text.Trim();
            MEL.EndDate = txtDateTo.Text.Trim();
            MEL.Is_Moder_Allow = true;
            MEL.Is_Raees_Lagnat_Allow = false;
            MEL.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeLoan_ REL = new Repostry_EmployeeLoan_();
            dt = REL.BErp_EmployeeLoan_Manage(MEL);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "إرشيف : " + ddlYears.SelectedItem.ToString() + ", قائمة قروض " + XTypeEmp + " من تاريخ " + txtDateFrom.Text.Trim() +
                    " إلى تاريخ " + txtDateTo.Text.Trim();
                GVEmployeeLoans.DataSource = dt;
                GVEmployeeLoans.DataBind();
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
        Response.Redirect("PageEmployeeLoans.aspx");
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
                       "عرض قروض الموظفين", XDate);
            }
            else if (XType == "Admin")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "عرض",
                     "عرض قروض الخاصة به", XDate);
            }
        }
        else if (txtSearch.Text.Trim() != string.Empty)
        {
            if (XType == "Manager")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "بحث",
                       "بحث " + txtSearch.Text.Trim() + " في قروض الموظفين", XDate);
            }
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVEmployeeLoans.Columns[0].Visible = false;
            GVEmployeeLoans.Columns[12].Visible = false;
            GVEmployeeLoans.Columns[13].Visible = false;
            GVEmployeeLoans.UseAccessibleHeader = true;
            GVEmployeeLoans.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlprint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            foreach (GridViewRow row in GVEmployeeLoans.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmployeeLoans.DataKeys[row.RowIndex].Value);
                    Model_EmployeeLoan_ MEL = new Model_EmployeeLoan_()
                    {
                        IDCheck = "Delete",
                        EmployeeLoanMapID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        Number_Loan = 0,
                        Amount = 0,
                        LoanDate = string.Empty,
                        LoanTitle = string.Empty,
                        Description = string.Empty,
                        ApprovedBy = string.Empty,
                        TotalMonths = 0,
                        ID_Moder = 0,
                        Is_Moder_Allow = false,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = string.Empty,
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = true,
                        Is_Raees_Lagnat_Not_Allow = false,
                        Apply_Raees_Lagnat_Date = string.Empty,
                        CreatedDate = string.Empty,
                        CreatedBy = 0,
                        ModifiedBy = Test_Saddam.FGetIDUsiq(),
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false,
                        IsComplete = false,
                        InstallmentMonth = 0
                    };
                    Repostry_EmployeeLoan_ REL = new Repostry_EmployeeLoan_();
                    string Xresult = REL.FErp_EmployeeLoan_Add(MEL);
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

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        txtDateFrom.Text = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "01-01");
        txtDateTo.Text = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "12-31");
        FGetData();
    }

}