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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeBonuses_PageEmployeeBonusesByView : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (XType == "Manager")
                CLS_Permissions.CheckAccountAdmin("A185");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            ddlYears.SelectedValue = Request.QueryString["IDYear"];
            txtSearch.Text = Request.QueryString["ID"];
            txtSearch.Focus();
            pnlSelect.Visible = true;
            FEmployeeBonuses_Manage();
        }
    }

    private void FEmployeeBonuses_Manage()
    {
        try
        {
            Model_EmployeeBonuses_ MEB = new Model_EmployeeBonuses_();

            if (XType == "Manager")
            {
                MEB.IDCheck = "GetByIDDetails";
                MEB.EmployeeBonusesMapID = Guid.Empty;
                MEB.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
                MEB.Number_Bonuses = Convert.ToInt64(txtSearch.Text.Trim());
            }
            else if (XType == "Admin")
            {
                MEB.IDCheck = "GetByIDUniq";
                MEB.EmployeeBonusesMapID = new Guid(Request.QueryString["ID"]);
                MEB.FinancialYear_Id = Guid.Empty;
                MEB.Number_Bonuses = 0; IDSearch.Visible = false;
            }

            MEB.CreatedDate = string.Empty;
            MEB.Date_From = string.Empty;
            MEB.Date_To = string.Empty;
            MEB.Is_Moder_Allow = false;
            MEB.Is_Raees_Lagnat_Allow = false;
            MEB.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeBonuses_ REB = new Repostry_EmployeeBonuses_();
            dt = REB.BErp_EmployeeBonuses_Manage(MEB);
            if (dt.Rows.Count > 0)
            {
                IDEdit.HRef = "/CPanel/ERP/HRAndPayRoll/Transactions/PageEmployeeBonusesAdd.aspx?ID=" + dt.Rows[0]["EmployeeBonusesMapID"].ToString();
                lblNumberBonuses.Text = dt.Rows[0]["Number_Bonuses_"].ToString();
                lblAmount.Text = dt.Rows[0]["Total_Amount"].ToString() + ClassSaddam.FGetMonySa();
                lblDateCreate.Text = Convert.ToDateTime(dt.Rows[0]["Date_Bonuses_"]).ToString("ddd") + " / " + Convert.ToDateTime(dt.Rows[0]["Date_Bonuses_"]).ToString("dd/MM/yyyy");
                lblNameEmp.Text = dt.Rows[0]["_Name"].ToString();
                lbl_Name2.Text = lblNameEmp.Text;
                Img_Emp.ImageUrl = "/" + dt.Rows[0]["Img_Signature_"].ToString();
                lblManagment.Text = dt.Rows[0]["Department"].ToString();
                lblEmpNo.Text = dt.Rows[0]["EmployeeNo"].ToString();
                lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
                lblPhone.Text = dt.Rows[0]["MobileNo"].ToString();
                lblMaratialStatus.Text = ClassSaddam.FChangeValue(dt.Rows[0]["MaratialStatus"].ToString());

                lblDateLeave.Text = " , وذلك يوم ";
                lblDateLeave.Text += Convert.ToDateTime(dt.Rows[0]["Date_Bonuses_"]).ToString("ddd") + " بتاريخ " + Convert.ToDateTime(dt.Rows[0]["Date_Bonuses_"]).ToString("dd/MM/yyyy") + "م";

                lblDescrption.Text = dt.Rows[0]["Description"].ToString();
                lbl_Job.Text = ClassAdmin_Arn.FGetNameEmpByID(Convert.ToInt32(dt.Rows[0]["ID_Moder_"].ToString()));
                lbl_Job2.Text = lbl_Job.Text;
                lblModer.Text = ClassAdmin_Arn.FGetNameByID(Convert.ToInt32(dt.Rows[0]["ID_Moder_"].ToString()));
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
                    Img_Moder.Width = 100; Img_Moder.Visible = true;
                }
                else
                { Img_Moder.ImageUrl = "/Cpanel/loaderMin.gif"; Img_Moder.Width = 30; Img_Moder.Visible = true; }
                lbl_Raees.Text = ClassAdmin_Arn.FGetNameByID(Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"].ToString()));
                if (Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Allow_"]) && Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Not_Allow_"]) == false)
                { pnlRaeesAllow.Visible = true; pnlRaeesNotAllow.Visible = false; }
                else if (Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Allow_"]) == false && Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Not_Allow_"]))
                { pnlRaeesAllow.Visible = false; pnlRaeesNotAllow.Visible = true; }
                else { pnlRaeesAllow.Visible = false; pnlRaeesNotAllow.Visible = false; }
                if (Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Allow_"]) || Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Not_Allow_"]))
                {
                    lblRaees.Text = dt.Rows[0]["Note_Raees_"].ToString();
                    Img_Raees.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"]));
                    Img_Raees.Width = 100; Img_Raees.Visible = true; IDKhatm.Visible = true;
                }
                else
                { Img_Raees.ImageUrl = "/Cpanel/loaderMin.gif"; Img_Raees.Width = 30; Img_Raees.Visible = true; IDKhatm.Visible = false; }
                
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                    "عرض قرار مكافأة لـ " + lblNameEmp.Text + " برقم " + lblNumberBonuses.Text.Trim(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/ERP/HRAndPayRoll/Transactions/PageEmployeeBonusesByView.aspx?IDYear=" + dt.Rows[0]["FinancialYear_Id_"].ToString() + "&ID=" + dt.Rows[0]["Is_Moder_Not_Allow_"].ToString();
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
        Response.Redirect("PageEmployeeBonuses.aspx");
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
        FEmployeeBonuses_Manage();
    }

}