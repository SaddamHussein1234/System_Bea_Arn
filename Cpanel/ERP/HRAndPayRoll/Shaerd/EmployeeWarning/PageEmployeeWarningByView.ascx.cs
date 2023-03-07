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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeWarning_PageEmployeeWarningByView : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (XType == "Manager")
                CLS_Permissions.CheckAccountAdmin("A199");
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
            Model_EmployeeWarning_ MEW = new Model_EmployeeWarning_();

            if (XType == "Manager")
            {
                MEW.IDCheck = "GetByIDDetails";
                MEW.EmployeeWarningID = Guid.Empty;
                MEW.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
                MEW.Number_Warning = Convert.ToInt64(txtSearch.Text.Trim());
            }
            else if (XType == "Admin")
            {
                MEW.IDCheck = "GetByIDUniq";
                MEW.EmployeeWarningID = new Guid(Request.QueryString["ID"]);
                MEW.FinancialYear_Id = Guid.Empty;
                MEW.Number_Warning = 0; IDSearch.Visible = false;
            }

            MEW.CreatedDate = txtSearch.Text.Trim();
            MEW.Start_Date = string.Empty;
            MEW.End_Date = string.Empty;
            MEW.Is_Moder_Allow = false;
            MEW.Is_Moder_Not_Allow = false;
            MEW.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeWarning_ REW = new Repostry_EmployeeWarning_();
            dt = REW.BErp_EmployeeWarning_Manage(MEW);

            if (dt.Rows.Count > 0)
            {
                IDEdit.HRef = "/CPanel/ERP/HRAndPayRoll/Transactions/PageEmployeeWarningAdd.aspx?ID=" + dt.Rows[0]["EmployeeWarningID"].ToString();
                txtTitle.Text = dt.Rows[0]["Title_"].ToString();
                lblNumberPermission.Text = dt.Rows[0]["Number_Warning_"].ToString();
                lblDateCreate.Text = Convert.ToDateTime(dt.Rows[0]["Date_Warning_"]).ToString("ddd") + " / " + Convert.ToDateTime(dt.Rows[0]["Date_Warning_"]).ToString("dd/MM/yyyy");
                lblNameEmp.Text = dt.Rows[0]["_Name"].ToString();
                lbl_Name2.Text = lblNameEmp.Text;
                Img_Emp.ImageUrl = "/" + dt.Rows[0]["Img_Signature_"].ToString();
                lblManagment.Text = dt.Rows[0]["Department"].ToString();
                lblEmpNo.Text = dt.Rows[0]["EmployeeNo"].ToString();
                lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
                lblPhone.Text = dt.Rows[0]["MobileNo"].ToString();
                lblMaratialStatus.Text = ClassSaddam.FChangeValue(dt.Rows[0]["MaratialStatus"].ToString());
                lblDescrption.Text = dt.Rows[0]["Details_"].ToString();

                lbl_Job.Text = ClassAdmin_Arn.FGetNameEmpByID(Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"].ToString()));
                lblModer.Text = ClassAdmin_Arn.FGetNameByID(Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"].ToString()));
                if (Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Allow_"]) || Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Not_Allow_"]))
                {
                    Img_Moder.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"]));
                    Img_Moder.Width = 100; Img_Moder.Visible = true; IDKhatm.Visible = true;
                }
                else
                { Img_Moder.ImageUrl = "/Cpanel/loaderMin.gif"; Img_Moder.Width = 30; Img_Moder.Visible = true; IDKhatm.Visible = false; }

                lbl_Raees.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"].ToString()));
                bool Allow_Final, Not_Allow;
                Allow_Final = Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Allow_"]);
                Not_Allow = Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Not_Allow_"]);
                if (Allow_Final && Not_Allow == false)
                {
                    pnlRaeesAllow.Visible = true;
                    pnlRaeesWithComment.Visible = false;
                    pnlRaeesNotAllow.Visible = false;
                }
                else if (Allow_Final == false && Not_Allow == false)
                {
                    pnlRaeesAllow.Visible = false;
                    //lblRaees.Text = dt.Rows[0]["Comment_Raees_"].ToString();
                    pnlRaeesWithComment.Visible = true;
                    pnlRaeesNotAllow.Visible = false;
                }
                else if (Allow_Final == false && Not_Allow)
                {
                    pnlRaeesAllow.Visible = false;
                    pnlRaeesWithComment.Visible = false;
                    pnlRaeesNotAllow.Visible = true;
                }

                //if (Allow_Final || Not_Allow)
                //{
                //    Img_Raees.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"]));
                //    Img_Raees.Width = 100; IDKhatm.Visible = true;
                //}
                //else
                //{
                //    Img_Raees.ImageUrl = "/Cpanel/loaderMin.gif";
                //    Img_Raees.Width = 30; IDKhatm.Visible = false;
                //}
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                    "عرض إنذار لـ " + lblNameEmp.Text + " برقم " + lblNumberPermission.Text.Trim(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/ERP/HRAndPayRoll/Transactions/PageEmployeeWarningByView.aspx?IDYear=" + dt.Rows[0]["FinancialYear_Id_"].ToString() + "&ID=" + dt.Rows[0]["Number_Warning_"].ToString();
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
        Response.Redirect("PageEmployeeWarning.aspx");
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