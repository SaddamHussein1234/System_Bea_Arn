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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployLeave_PageEmployeeLeaveCategoryByView : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (XType == "Manager")
                CLS_Permissions.CheckAccountAdmin("A159");
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            ddlYears.SelectedValue = Request.QueryString["IDYear"];
            txtSearch.Text = Request.QueryString["ID"];
            txtSearch.Focus();
            FEmployeeLeaveCategory_Manage();
        }
    }

    private void FEmployeeLeaveCategory_Manage()
    {
        try
        {
            Model_EmployeeLeaveCategory_ MELC = new Model_EmployeeLeaveCategory_();

            if (XType == "Manager")
            {
                MELC.IDCheck = "GetByIDDetails";
                MELC.EmployeeLeaveCategoryMapID = Guid.Empty;
                MELC.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
                MELC.Number_Leave = Convert.ToInt64(txtSearch.Text.Trim());
            }
            else if (XType == "Admin")
            {
                MELC.IDCheck = "GetByIDUniq";
                MELC.EmployeeLeaveCategoryMapID = new Guid(Request.QueryString["ID"]);
                MELC.FinancialYear_Id = Guid.Empty;
                MELC.Number_Leave = 0; IDSearch.Visible = false;
            }

            MELC.CreatedDate = string.Empty;
            MELC.StartDate = string.Empty;
            MELC.EndDate = string.Empty;
            MELC.Is_Emp = false;
            MELC.Is_Moder_Allow = false;
            MELC.Is_Raees_Lagnat_Allow = false;
            MELC.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeLeaveCategory_ RELC = new Repostry_EmployeeLeaveCategory_();
            dt = RELC.BErp_EmployeeLeaveCategory_Manage(MELC);
            if (dt.Rows.Count > 0)
            {
                IDEdit.HRef = "/CPanel/ERP/HRAndPayRoll/Transactions/PageEmployeeLeaveCategoryAdd.aspx?ID=" + dt.Rows[0]["EmployeeLeaveCategoryMapID"].ToString();
                lblNumberLeave.Text = dt.Rows[0]["Number_Leave_"].ToString();
                lblDateCreate.Text = Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToString("ddd") + " / " + Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToString("dd/MM/yyyy");
                lblNameEmp.Text = dt.Rows[0]["_Name"].ToString();
                lblNameEmp2.Text = lblNameEmp.Text;
                Img_Emp.ImageUrl = "/" + dt.Rows[0]["Img_Signature_"].ToString();
                lblManagment.Text = dt.Rows[0]["Department"].ToString();
                lblEmpNo.Text = dt.Rows[0]["EmployeeNo"].ToString();
                lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
                lblPhone.Text = dt.Rows[0]["MobileNo"].ToString();
                lblMaratialStatus.Text = ClassSaddam.FChangeValue(dt.Rows[0]["MaratialStatus"].ToString());
                lblTotalDay.Text = Convert.ToInt32(dt.Rows[0]["TotalDay"]).ToString() + " أيام ";
                lblDateLeave.Text = Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToString("ddd") + " تاريخ " + Convert.ToDateTime(dt.Rows[0]["StartDate"]).ToString("dd/MM/yyyy") + "م , ";
                lblDateLeave.Text += "حتى يوم " + Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("ddd") + " تاريخ " + Convert.ToDateTime(dt.Rows[0]["EndDate"]).ToString("dd/MM/yyyy");
                lblLeaveCategory.Text = dt.Rows[0]["LeaveCategory"].ToString();
                lblEmp.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["ID_Emp_"].ToString()));
                lblEmp2.Text = lblEmp.Text;
                if (Convert.ToBoolean(dt.Rows[0]["Is_Emp_"]))
                {
                    Img_Emp_Tow.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["ID_Emp_"]));
                    Img_Emp_Tow.Width = 100; Img_Emp_Tow.Visible = true;
                }
                else
                { Img_Emp_Tow.ImageUrl = "/Cpanel/loaderMin.gif"; Img_Emp_Tow.Width = 30; Img_Emp_Tow.Visible = true; }
                lbl_Job.Text = ClassAdmin_Arn.FGetNameEmpByID(Convert.ToInt32(dt.Rows[0]["ID_Moder_"].ToString()));
                lblModer.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["ID_Moder_"].ToString()));
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
                lbl_Raees.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["Is_Basic_"]));
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
                                   "عرض طلب اجازة لـ " + lblNameEmp.Text + " برقم " + lblNumberLeave.Text.Trim(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));

                lblLeaveType.Text = ClassSaddam.FCheckLeave(Convert.ToInt16(dt.Rows[0]["Is_Basic_"]), 2);

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/ERP/HRAndPayRoll/Transactions/PageEmployeeLeaveCategoryByView.aspx?IDYear=" + dt.Rows[0]["FinancialYear_Id_"].ToString() + "&ID=" + dt.Rows[0]["Number_Leave_"].ToString();
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
        FEmployeeLeaveCategory_Manage();
    }

}