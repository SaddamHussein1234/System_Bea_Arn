using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CPBeneficiary_PageAcceptanceDecision : System.Web.UI.Page
{
    string UserERasAlEstemarah;
    private void GetCookie()
    {
        try
        {
            HttpCookie CookeCheck;  // اسم المستخدم
            CookeCheck = Request.Cookies["__User_True_User"];
            UserERasAlEstemarah = ClassSaddam.UnprotectPassword(CookeCheck["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");
        }
        catch
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        ClassMosTafeed CM = new ClassMosTafeed();
        CM._User_Name_ = UserERasAlEstemarah;
        CM._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CM.BArnRasAlEstemarahLogin();
        if (dt.Rows.Count > 0)
        {
            bool A2;
            A2 = Convert.ToBoolean(dt.Rows[0]["A2"]);
            if (A2)
                FGetArnAcceptanceDecisionByDetails(dt.Rows[0]["NumberMostafeed"].ToString());
            else
                Response.Redirect("PageNotAccess.aspx");            
        }
        else
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
        }
    }

    private void FGetArnAcceptanceDecisionByDetails(string XID)
    {
        try
        {
            ClassQararQobol CQQ = new ClassQararQobol();
            CQQ._NumberQarar = Convert.ToInt32(XID);
            CQQ._IsQobol = true;
            CQQ._IsEstepaad = false;
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

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/PageAcceptanceDecisionDetails.aspx?ID=" + dt.Rows[0]["NumberMostafeed"].ToString();
                Class_QRScan.FGetQRCode(code, imgBarCode);

                pnlPrint.Visible = true;
                FGetArnAcceptanceDecisionAdminByDetails(XID);
            }
            else
                pnlPrint.Visible = false;
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetArnAcceptanceDecisionAdminByDetails(string XID)
    {
        ClassQararQobolAdmin CQQA = new ClassQararQobolAdmin();
        CQQA._NumberMostafeed = Convert.ToInt32(lblNumberMostafeed.Text.Trim());
        CQQA._NumberQarar = Convert.ToInt32(XID);
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
        //for (int x = 0; x <= dt.Rows.Count - 1; x++)
        //{
        if (Convert.ToBoolean(dt.Rows[0]["AdminAllow"]))
        {
            IDKhatm.Visible = true;
        }
        //}
        System.Threading.Thread.Sleep(200);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["foot"] = pnlPrint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../Cpanel/PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

}