using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CResearchers_CPVillage_PageTecisionToExcludeDetails : System.Web.UI.Page
{
    string XID;
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
            Response.Redirect("PageNotAccess.aspx");
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
            bool A46;
            A46 = Convert.ToBoolean(dtViewProfil.Rows[0]["A46"]);
            if (A46 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
            txtSearch.Text = Request.QueryString["ID"];
            txtSearch.Focus();
            if (Request.QueryString["ID"] != null)
            {
                txtSearch.Text = Request.QueryString["ID"];
                txtSearch.Focus();
                FGetArnAcceptanceDecisionByDetails(Convert.ToInt32(Request.QueryString["ID"]));
            }
        }
    }

    private void FGetArnAcceptanceDecisionByDetails(int IDNumber)
    {
        GetCookie();
        DataTable dtGetQariah = new DataTable();
        dtGetQariah = ClassDataAccess.GetData("SELECT Top(1) QararQobolMustafeed.NumberMostafeed,[AlQaryah],QararQobolMustafeed.IsDelete FROM [dbo].[QararQobolMustafeed] With(NoLock) Inner Join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = QararQobolMustafeed.NumberMostafeed Where NumberQarar = @0 And IsQobol = @1 And IsEstepaad = @2 And QararQobolMustafeed.IsDelete = @3 And RasAlEstemarah.IsDelete = @3", txtSearch.Text.Trim(), Convert.ToString(false), Convert.ToString(true), Convert.ToString(false));
        if (dtGetQariah.Rows.Count > 0)
        {
            DataTable dtCheck = new DataTable();
            dtCheck = ClassDataAccess.GetData("SELECT Top(1) [IDItem],[IDAdminJoin],[IDQariah],[IsDelete] FROM [dbo].[tbl_MultiQariah] With(NoLock) Where IDAdminJoin = @0 And IDQariah = @1 And IsDelete = @2"
                , IDUser, dtGetQariah.Rows[0]["AlQaryah"].ToString(), Convert.ToString(false));
            if (dtCheck.Rows.Count > 0)
            {
                try
                {
                    ClassQararQobol CQQ = new ClassQararQobol();
                    CQQ._NumberQarar = IDNumber;
                    CQQ._IsQobol = false;
                    CQQ._IsEstepaad = true;
                    CQQ._IsDelete = false;
                    DataTable dt = new DataTable();
                    dt = CQQ.BArnAcceptanceDecisionByDetails();
                    if (dt.Rows.Count > 0)
                    {
                        lblDateQarar.Text = Convert.ToDateTime(dt.Rows[0]["DateQarar"]).ToString("dd/MM/yyyy");
                        lblNameMosTafeed.Text = dt.Rows[0]["NameMostafeed"].ToString();
                        lblAlqariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
                        lblNumberAlSegelAlMadany.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
                        lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
                        lblHalafAlMosTafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
                        lblNumberMostafeed.Text = dt.Rows[0]["NumberMostafeed"].ToString();
                        lblNumberReport.Text = dt.Rows[0]["NumberReport"].ToString();
                        lblDateReport.Text = Convert.ToDateTime(dt.Rows[0]["DateReport"]).ToString("dd/MM/yyyy");
                        lblDataEntery.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["IDAdmin"].ToString()));
                        lblDateEntery.Text = Convert.ToDateTime(dt.Rows[0]["DateAddQara"]).ToString("dd/MM/yyyy");

                        IDBarcode.ImageUrl = "http://chart.apis.google.com/chart?cht=qr&chl=" + ClassSetting.FGetNameServer() +
                            "/Cpanel/PScanQE.aspx?ID=" + txtSearch.Text.Trim() + "&chs=75";

                        pnlPrint.Visible = true;
                        pnlSelect.Visible = false;
                        FGetArnAcceptanceDecisionAdminByDetails();
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
        }
        else
        {
            Response.Redirect("PageNotAccess.aspx");
        }
    }

    private void FGetArnAcceptanceDecisionAdminByDetails()
    {
        ClassQararQobolAdmin CQQA = new ClassQararQobolAdmin();
        CQQA._NumberMostafeed = Convert.ToInt32(lblNumberMostafeed.Text.Trim());
        CQQA._NumberQarar = Convert.ToInt32(txtSearch.Text.Trim());
        CQQA._NumberReport = Convert.ToInt32(lblNumberReport.Text.Trim());
        CQQA._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CQQA.BArnAcceptanceDecisionAdminByDetails();
        if (dt.Rows.Count > 0)
        {
            RPTGetAdminInManagment.DataSource = dt;
            RPTGetAdminInManagment.DataBind();
            pnlAdmin.Visible = true;
            pnlAdminNull.Visible = false;
        }
        else
        {
            pnlAdmin.Visible = false;
            pnlAdminNull.Visible = true;
        }

        if (Convert.ToBoolean(dt.Rows[0]["AdminAllow"]))
        {
            IDKhatm.Visible = true;
        }

        System.Threading.Thread.Sleep(500);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["foot"] = pnlPrint;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetArnAcceptanceDecisionByDetails(Convert.ToInt32(txtSearch.Text.Trim()));
        System.Threading.Thread.Sleep(200);
    }

}