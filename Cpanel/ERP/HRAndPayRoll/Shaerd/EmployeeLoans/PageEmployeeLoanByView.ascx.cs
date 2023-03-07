using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeLoans_PageEmployeeLoanByView : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (XType == "Manager")
                CLS_Permissions.CheckAccountAdmin("A172");
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            ddlYears.SelectedValue = Request.QueryString["IDYear"];
            txtSearch.Text = Request.QueryString["ID"];
            txtSearch.Focus();
            FEmployeeLoan_Manage();
        }
    }

    private void FEmployeeLoan_Manage()
    {
        try
        {
            Model_EmployeeLoan_ MEL = new Model_EmployeeLoan_();

            if (XType == "Manager")
            {
                MEL.IDCheck = "GetByIDDetails";
                MEL.EmployeeLoanMapID = Guid.Empty;
                MEL.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
                MEL.Number_Loan = Convert.ToInt64(txtSearch.Text.Trim());
            }
            else if (XType == "Admin")
            {
                MEL.IDCheck = "GetByIDUniq";
                MEL.EmployeeLoanMapID = new Guid(Request.QueryString["ID"]);
                MEL.FinancialYear_Id = Guid.Empty;
                MEL.Number_Loan = 0; IDSearch.Visible = false;
            }

            MEL.CreatedDate = string.Empty;
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
                IDEdit.HRef = "/CPanel/ERP/HRAndPayRoll/Transactions/PageEmployeeLoanAdd.aspx?ID=" + dt.Rows[0]["EmployeeLoanMapID"].ToString();
                lblNumberLoan.Text = dt.Rows[0]["Number_Loan_"].ToString();
                lblDateCreate.Text = Convert.ToDateTime(dt.Rows[0]["CreatedDate"]).ToString("ddd") + " / " + Convert.ToDateTime(dt.Rows[0]["CreatedDate"]).ToString("dd/MM/yyyy");
                lblNameEmp.Text = dt.Rows[0]["_Name"].ToString();
                lblNameEmp2.Text = lblNameEmp.Text;
                lblManagment.Text = dt.Rows[0]["Department"].ToString();
                lblEmpNo.Text = dt.Rows[0]["EmployeeNo"].ToString();
                lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
                lblPhone.Text = dt.Rows[0]["MobileNo"].ToString();
                lblMaratialStatus.Text = ClassSaddam.FChangeValue(dt.Rows[0]["MaratialStatus"].ToString());
                Img_Emp.ImageUrl = "/" + dt.Rows[0]["Img_Signature_"].ToString();
                lblAmount.Text = Convert.ToInt32(dt.Rows[0]["Amount"]).ToString() + ClassSaddam.FGetMonySa();
                lblDateLoan.Text = dt.Rows[0]["LoanTitle"].ToString();
                lblCountMounth.Text = dt.Rows[0]["TotalMonths"].ToString() + " / أقساط";
                lblInstallmentMonth.Text = dt.Rows[0]["InstallmentMonth"].ToString() + ClassSaddam.FGetMonySa();
                lbl_Job.Text = ClassAdmin_Arn.FGetNameEmpByID(Convert.ToInt32(dt.Rows[0]["ID_Moder_"].ToString()));
                lblModer.Text = ClassAdmin_Arn.FGetNameByID(Convert.ToInt32(dt.Rows[0]["ID_Moder_"].ToString()));
                if (Convert.ToBoolean(dt.Rows[0]["Is_Moder_Allow_"]) && Convert.ToBoolean(dt.Rows[0]["Is_Moder_Not_Allow_"]) == false)
                { pnlAllow.Visible = true; pnlNotAllow.Visible = false; }
                else if (Convert.ToBoolean(dt.Rows[0]["Is_Moder_Allow_"]) == false && Convert.ToBoolean(dt.Rows[0]["Is_Moder_Not_Allow_"]))
                {
                    lblComments.Text = dt.Rows[0]["Comments"].ToString();
                    pnlAllow.Visible = false; pnlNotAllow.Visible = true;
                }
                else { pnlAllow.Visible = false; pnlNotAllow.Visible = false; }
                if (Convert.ToBoolean(dt.Rows[0]["Is_Moder_Allow_"]) || Convert.ToBoolean(dt.Rows[0]["Is_Moder_Not_Allow_"]))
                {
                    Img_Moder.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["ID_Moder_"]));
                    Img_Moder.Width = 100; Img_Moder.Visible = true; IDKhatm.Visible = true;
                }
                else
                { Img_Moder.ImageUrl = "/Cpanel/loaderMin.gif"; Img_Moder.Width = 30; Img_Moder.Visible = true; IDKhatm.Visible = false; }
                lbl_Raees.Text = ClassAdmin_Arn.FGetNameByID(Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"].ToString()));
                if (Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Allow_"]) && Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Not_Allow_"]) == false)
                { pnlRaeesAllow.Visible = true; pnlRaeesNotAllow.Visible = false; }
                else if (Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Allow_"]) == false && Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Not_Allow_"]))
                { pnlRaeesAllow.Visible = false; pnlRaeesNotAllow.Visible = true; }
                else { pnlRaeesAllow.Visible = false; pnlRaeesNotAllow.Visible = false; }
                //if (Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Allow_"]) || Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Not_Allow_"]))
                //{
                //    Img_Raees.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"]));
                //    Img_Raees.Width = 100; Img_Raees.Visible = true; IDKhatm.Visible = true;
                //}
                //else
                //{ Img_Raees.ImageUrl = "/Cpanel/loaderMin.gif"; Img_Raees.Width = 30; Img_Raees.Visible = true; IDKhatm.Visible = false; }

                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                    "عرض سلفة لـ " + lblNameEmp.Text + " برقم " + lblNumberLoan.Text.Trim(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/ERP/HRAndPayRoll/Transactions/PageEmployeeLoanByView.aspx?IDYear=" + dt.Rows[0]["FinancialYear_Id_"].ToString() + "&ID=" + dt.Rows[0]["Number_Loan_"].ToString();
                Class_QRScan.FGetQRCode(code, ImgQRCode);
                Class_QRScan.FGetQRCode(code, ImgQRCode2);

                pnlPrint.Visible = true;
                pnlSelect.Visible = false;
            }
            else
            {
                pnlPrint.Visible = false;
                pnlSelect.Visible = true;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["foot"] = pnl2;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FEmployeeLoan_Manage();
    }

}