using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeePermission_PageEmployeePermissionByView : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (XType == "Manager")
                CLS_Permissions.CheckAccountAdmin("A195");
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            ddlYears.SelectedValue = Request.QueryString["IDYear"];
            txtSearch.Text = Request.QueryString["ID"];
            txtSearch.Focus();
            FEmployeeMandate_Manage();
        }
    }

    private void FEmployeeMandate_Manage()
    {
        try
        {
            Model_EmployeePermission_ MEP = new Model_EmployeePermission_();

            if (XType == "Manager")
            {
                MEP.IDCheck = "GetByIDDetails";
                MEP.EmployeePermissionMapID = Guid.Empty;
                MEP.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
                MEP.Number_Permission = Convert.ToInt64(txtSearch.Text.Trim());
            }
            else if (XType == "Admin")
            {
                MEP.IDCheck = "GetByIDUniq";
                MEP.EmployeePermissionMapID = new Guid(Request.QueryString["ID"]);
                MEP.FinancialYear_Id = Guid.Empty;
                MEP.Number_Permission = 0; IDSearch.Visible = false;
            }

            MEP.CreatedDate = txtSearch.Text.Trim();
            MEP.Start_Date = string.Empty;
            MEP.End_Date = string.Empty;
            MEP.Is_Moder_Allow = false;
            MEP.Is_Moder_Not_Allow = false;
            MEP.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeePermission_ REP = new Repostry_EmployeePermission_();
            dt = REP.BErp_EmployeePermission_Manage(MEP);

            if (dt.Rows.Count > 0)
            {
                IDEdit.HRef = "/CPanel/ERP/HRAndPayRoll/Transactions/PageEmployeePermissionAdd.aspx?ID=" + dt.Rows[0]["EmployeePermissionMapID"].ToString();
                txtTitle.Text = dt.Rows[0]["PermissionTitle"].ToString();
                lblNumberPermission.Text = dt.Rows[0]["Number_Permission_"].ToString();
                lblDateCreate.Text = Convert.ToDateTime(dt.Rows[0]["Date_Permission_"]).ToString("ddd") + " / " + Convert.ToDateTime(dt.Rows[0]["Date_Permission_"]).ToString("dd/MM/yyyy");
                lblNameEmp.Text = dt.Rows[0]["_Name"].ToString();
                lbl_Name2.Text = lblNameEmp.Text;
                Img_Emp.ImageUrl = "/" + dt.Rows[0]["Img_Signature_"].ToString();
                lblManagment.Text = dt.Rows[0]["Department"].ToString();
                lblEmpNo.Text = dt.Rows[0]["EmployeeNo"].ToString();
                lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
                lblPhone.Text = dt.Rows[0]["MobileNo"].ToString();
                lblMaratialStatus.Text = ClassSaddam.FChangeValue(dt.Rows[0]["MaratialStatus"].ToString());
                lblDescrption.Text = dt.Rows[0]["Description"].ToString();

                lblEarly_Dismissal.Text = ClassSaddam.FCheckEarly_Dismissal(Convert.ToBoolean(dt.Rows[0]["Is_Early_Dismissal_"]));
                lblLate_In_Attendance.Text = ClassSaddam.FCheckLate_In_Attendance(Convert.ToBoolean(dt.Rows[0]["Is_Late_In_Attendance_"]));
                lblExit_And_Return.Text = ClassSaddam.FCheckExit_And_Return(Convert.ToBoolean(dt.Rows[0]["Is_Exit_And_Return_"]));

                lblFrom.Text = "من الساعة : (" + Convert.ToDateTime(dt.Rows[0]["From_The_Hour_"]).ToString("HH:mm tt") + ")";
                lblTo.Text = "إلى الساعة : (" + Convert.ToDateTime(dt.Rows[0]["To_The_Hour_"]).ToString("HH:mm tt") + ")";

                lblModer.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["ID_Moder_"].ToString()));
                if (Convert.ToBoolean(dt.Rows[0]["Is_Moder_Allow_"]) && Convert.ToBoolean(dt.Rows[0]["Is_Moder_Not_Allow_"]) == false)
                { pnlAllow.Visible = true; pnlNotAllow.Visible = false; }
                else if (Convert.ToBoolean(dt.Rows[0]["Is_Moder_Allow_"]) == false && Convert.ToBoolean(dt.Rows[0]["Is_Moder_Not_Allow_"]))
                {
                    lblComments.Text = dt.Rows[0]["Comment_Moder_"].ToString();
                    pnlAllow.Visible = false; pnlNotAllow.Visible = true;
                }
                else { pnlAllow.Visible = false; pnlNotAllow.Visible = false; }
                if (Convert.ToBoolean(dt.Rows[0]["Is_Moder_Allow_"]) || Convert.ToBoolean(dt.Rows[0]["Is_Moder_Not_Allow_"]))
                {
                    Img_Moder.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["ID_Moder_"]));
                    Img_Moder.Width = 100; Img_Moder.Visible = true; IDKhatm.Visible = true;
                }
                else
                { Img_Moder.ImageUrl = "/Cpanel/loaderMin.gif"; Img_Moder.Width = 30; Img_Moder.Visible = true; }

                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                   "عرض إستئذان لـ " + lblNameEmp.Text + " برقم " + lblNumberPermission.Text.Trim(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));

                lblDateModer.Text = Convert.ToDateTime(dt.Rows[0]["Date_Moder_"]).ToString("MM/dd/yyyy");

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/ERP/HRAndPayRoll/Transactions/PageEmployeePermissionByView.aspx?IDYear=" + dt.Rows[0]["FinancialYear_Id_"].ToString() + "&ID=" + dt.Rows[0]["Number_Permission_"].ToString();
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
        Response.Redirect("PageEmployeePermission.aspx");
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
        FEmployeeMandate_Manage();
    }

}