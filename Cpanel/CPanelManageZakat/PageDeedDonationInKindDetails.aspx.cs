using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelManageZakat_PageDeedDonationInKindDetails : System.Web.UI.Page
{
    string IDUser, IDUniq;
    private void GetCookie()
    {
        try
        {
            HttpCookie Cooke;  // رقم المستخدم
            Cooke = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
            IDUser = ClassSaddam.UnprotectPassword(Cooke["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
        }
        catch
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        ClassAdmin_Arn CAA = new ClassAdmin_Arn();
        CAA._IDUniq = IDUniq;
        CAA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CAA.BArnAdminGetByIDUniq();
        if (dtViewProfil.Rows.Count > 0)
        {
            bool A136, A137;
            A136 = Convert.ToBoolean(dtViewProfil.Rows[0]["A136"]);
            A137 = Convert.ToBoolean(dtViewProfil.Rows[0]["A137"]);
            if (A136 == false)
                Response.Redirect("PageNotAccess.aspx");
            if (A137 == false)
                btnDelete1.Visible = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            ClassQuaem.FGetProject(DL_ProjectNew);
            ddlYears.SelectedValue = Request.QueryString["IDYear"];
            DL_ProjectNew.SelectedValue = Request.QueryString["IDP"];
            txtSearch.Text = Request.QueryString["ID"];
            txtSearch.Focus();
            if (txtSearch.Text.Trim() != string.Empty)
            {
                PageView.SetData(ddlYears.SelectedValue, txtSearch.Text.Trim(), DL_ProjectNew.SelectedValue);
                ID_Edit.Visible = true;
                ID_Edit.HRef = "PageDeedDonationInKind.aspx?ID=" + PageView.XID();
                FCheck();
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        //lblType.Visible = false;
        //DLType.Visible = true;
        PageView.SetData(ddlYears.SelectedValue, txtSearch.Text.Trim(), DL_ProjectNew.SelectedValue);
        ID_Edit.HRef = "PageDeedDonationInKind.aspx?ID=" + PageView.XID();
        FCheck();
        System.Threading.Thread.Sleep(100);
    }

    protected void LbRefreshSaraf_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        //lblType.Visible = false;
        //DLType.Visible = true;
        PageView.SetData(ddlYears.SelectedValue, txtSearch.Text.Trim(), DL_ProjectNew.SelectedValue);
        ID_Edit.HRef = "PageDeedDonationInKind.aspx?ID=" + PageView.XID();
        FCheck();
    }

    protected void LBPrintSaraf_Click(object sender, EventArgs e)
    {
        PageView.FPrint();
    }

    protected void CBViewKHatm_CheckedChanged(object sender, EventArgs e)
    {
        FCheck();
    }

    private void FCheck()
    {
        if (CBViewKHatm.Checked)
            PageView.XView();
        else
            PageView.XHide();
    }

}