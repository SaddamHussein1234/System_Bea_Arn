using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeAccountables_PageEmployeeAccountableByView : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (XType == "Manager")
                CLS_Permissions.CheckAccountAdmin("A165");
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            ddlYears.SelectedValue = Request.QueryString["IDYear"];
            txtSearch.Text = Request.QueryString["ID"];
            txtSearch.Focus();
            FEmployeeAccountable_Manage();
        }
    }

    private void FEmployeeAccountable_Manage()
    {
        try
        {
            string XIDCheck = string.Empty;
            Guid XEmployeeAccountableID = Guid.Empty, XFinancialYear_Id = Guid.Empty;
            long XNumber_Accountable = 0;
            if (XType == "Manager")
            {
                XIDCheck = "GetByIDDetails";
                XEmployeeAccountableID = Guid.Empty;
                XFinancialYear_Id = new Guid(ddlYears.SelectedValue);
                XNumber_Accountable = Convert.ToInt64(txtSearch.Text.Trim());
            }
            else if (XType == "Admin")
            {
                XIDCheck = "GetByIDUniq";
                XEmployeeAccountableID = new Guid(Request.QueryString["ID"]);
                XFinancialYear_Id = Guid.Empty;
                XNumber_Accountable = 0; IDSearch.Visible = false;
            }

            DataTable dt = new DataTable();
            dt = Repostry_EmployeeAccountable_.FGetDataInDataTable(XIDCheck, XEmployeeAccountableID, XFinancialYear_Id,
                XNumber_Accountable, string.Empty, string.Empty, string.Empty, false, true);

            if (dt.Rows.Count > 0)
            {
                IDEdit.HRef = "/CPanel/ERP/HRAndPayRoll/Transactions/PageEmployeeAccountableAdd.aspx?ID=" + dt.Rows[0]["EmployeeAccountableID"].ToString();
                lblNumberAccountable.Text = dt.Rows[0]["Number_Accountable_"].ToString();
                txtTitle.Text = dt.Rows[0]["Accountable"].ToString();
                lblDate_Accountable_Top.Text = Convert.ToDateTime(dt.Rows[0]["Date_Accountable_"]).ToString("dd/MM/yyyy");
                lblDate_Accountable_Mediam.Text = lblDate_Accountable_Top.Text;
                lblDate_Accountable_Bottom.Text = lblDate_Accountable_Top.Text;
                lblDateCreate.Text = Convert.ToDateTime(dt.Rows[0]["Date_Accountable_"]).ToString("ddd") + " / " + Convert.ToDateTime(dt.Rows[0]["Date_Accountable_"]).ToString("dd/MM/yyyy");
                lblNameEmp.Text = dt.Rows[0]["_Name"].ToString();
                lblName_Down.Text = lblNameEmp.Text;
                Img_Emp.ImageUrl = "/" + dt.Rows[0]["Img_Signature_"].ToString();
                lblManagment.Text = dt.Rows[0]["Department"].ToString();
                lblEmpNo.Text = dt.Rows[0]["EmployeeNo"].ToString();
                lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
                lblPhone.Text = dt.Rows[0]["MobileNo"].ToString();
                lblMaratialStatus.Text = ClassSaddam.FChangeValue(dt.Rows[0]["MaratialStatus"].ToString());

                lbl_Description.Text = dt.Rows[0]["Description_"].ToString();
                lblStatement_Request.Text = dt.Rows[0]["Statement_Request_"].ToString();
                lblTheStatement.Text = dt.Rows[0]["The_Statement_"].ToString();

                bool XAllow_Accountable, XAllow_Resolved, XWarning_Oral, XWarning_Written, XWarning_Final;
                XAllow_Accountable = Convert.ToBoolean(dt.Rows[0]["Allow_Accountable_"]);
                XAllow_Resolved = Convert.ToBoolean(dt.Rows[0]["Allow_Resolved_"]);
                XWarning_Oral = Convert.ToBoolean(dt.Rows[0]["Warning_Oral_"]);
                XWarning_Written = Convert.ToBoolean(dt.Rows[0]["Warning_Written_"]);
                XWarning_Final = Convert.ToBoolean(dt.Rows[0]["Warning_Final_"]);
                if (XAllow_Accountable) pnl_Allow_Accountable.Visible = true;
                else if (XAllow_Resolved) pnl_Allow_Resolved.Visible = true;
                else if (XWarning_Oral) pnl_Warning_Oral.Visible = true;
                else if (XWarning_Written) pnl_Warning_Written.Visible = true;
                else if (XWarning_Final) pnl_Warning_Final.Visible = true;

                lbl_Job.Text = ClassAdmin_Arn.FGetNameEmpByID(Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"].ToString()));
                lbl_Raees.Text = ClassAdmin_Arn.FGetNameByID(Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"].ToString()));
                lbl_Raees_Top.Text = lbl_Raees.Text;
                lbl_Raees_Mediam.Text = lbl_Raees_Top.Text;
                if (Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Allow_"]) || Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Not_Allow_"]))
                {
                    Img_Raees.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"]));
                    Img_Raees_Top.ImageUrl = Img_Raees.ImageUrl;
                    Img_Raees.Width = 100; Img_Raees_Top.Width = 100; Img_Raees.Visible = true; IDKhatm.Visible = true;
                }
                else
                {
                    Img_Raees.ImageUrl = "/Cpanel/loaderMin.gif"; Img_Raees_Top.ImageUrl = Img_Raees.ImageUrl;
                    Img_Raees.Width = 30; Img_Raees_Top.Width = 30; Img_Raees.Visible = true; IDKhatm.Visible = false;
                }

                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                    "عرض مساءلة لـ " + lblNameEmp.Text + " برقم " + lblNumberAccountable.Text.Trim(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/ERP/HRAndPayRoll/Transactions/PageEmployeeAccountableByView.aspx?IDYear=" + dt.Rows[0]["FinancialYear_Id_"].ToString() + "&ID=" + dt.Rows[0]["Number_Accountable_"].ToString();
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

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeAccountables.aspx");
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
        FEmployeeAccountable_Manage();
    }

}