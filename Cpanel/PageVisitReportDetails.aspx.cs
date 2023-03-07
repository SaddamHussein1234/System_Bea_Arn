using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageVisitReportDetails : System.Web.UI.Page
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
            bool A51;
            A51 = Convert.ToBoolean(dtViewProfil.Rows[0]["A51"]);
            if (A51 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            ClassAdmin_Arn.FGetAlBaheeth(DLAlBaheth);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            ClassAdmin_Arn.FGetRaeesLagnatAlBahath(DLRaeesLagnatAlBahath);
            pnlSelect.Visible = true;
            txtSearch.Text = Request.QueryString["ID"];
            txtSearch.Focus();
            FGetArnReportAlZyaratByDetails();
            FArnReportAlZyaratElectricalAppliancesGetByMostafeed();
        }
    }

    private void FArnReportAlZyaratElectricalAppliancesGetByMostafeed()
    {
        try
        {
            ClassReportAlZyaratElectricalAppliances CRZEA = new ClassReportAlZyaratElectricalAppliances();
            CRZEA._Top = 15;
            CRZEA._IDMustafeed = Convert.ToInt32(lblNumberMostafeed.Text);
            CRZEA._IDReport = Convert.ToInt32(lblNumberReport.Text.Trim());
            CRZEA._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CRZEA.BArnReportAlZyaratElectricalAppliancesGetByMostafeed();
            if (dt.Rows.Count > 0)
            {
                RPTDeviceByMostafeed.DataSource = dt;
                RPTDeviceByMostafeed.DataBind();
                pnlDevice.Visible = true;
                pnlNull.Visible = false;
            }
            FAPI_ReportAlZyaratMedicalEquipments_Manage();
        }
        catch (Exception)
        {
            //return;
        }
    }

    private void FAPI_ReportAlZyaratMedicalEquipments_Manage()
    {
        Model_MedicalEquipments_ MME = new Model_MedicalEquipments_();
        MME.IDCheck = "GetByIDMostafeed";
        MME.Top = 15;
        MME.ID_Item = Guid.Empty;
        MME.ID_Mustafeed = Convert.ToInt32(lblNumberMostafeed.Text.Trim());
        MME.ID_Report = Convert.ToInt32(lblNumberReport.Text.Trim());
        MME.Start_Date = string.Empty;
        MME.End_Date = string.Empty;
        MME.Name = string.Empty;
        MME.IsActive = true;
        DataTable dt = new DataTable();
        Repostry_MedicalEquipments_ RME = new Repostry_MedicalEquipments_();
        dt = RME.BAPI_ReportAlZyaratMedicalEquipments_Manage(MME);
        if (dt.Rows.Count > 0)
        {
            RPTDeviceByMostafeedMedical.DataSource = dt;
            RPTDeviceByMostafeedMedical.DataBind();
            pnlDeviceMedical.Visible = true;
            pnlNull.Visible = false;
        }
    }

    private void FGetArnReportAlZyaratByDetails()
    {
        try
        {
            ClassReportAlZyarat CRA = new ClassReportAlZyarat();
            CRA._NumberReport = Convert.ToInt64(txtSearch.Text.Trim());
            CRA._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CRA.BArnReportAlZyaratByDetails();
            if (dt.Rows.Count > 0)
            {
                lblNumberReport.Text = txtSearch.Text.Trim();
                lblDateReport.Text = Convert.ToDateTime(dt.Rows[0]["DateReport"]).ToString("dd/MM/yyyy");
                lblNameMosTafeed.Text = dt.Rows[0]["NameMostafeed"].ToString();
                lblAlqariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
                lblNumberAlSegelAlMadany.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
                lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
                lblHalafAlMosTafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
                lblNumberMostafeed.Text = dt.Rows[0]["NumberMostafeed"].ToString();

                CBEgathy.Checked = Convert.ToBoolean(dt.Rows[0]["Egathy"]);
                txtEgathy.Text = dt.Rows[0]["NumberEgathy"].ToString();

                CBOther.Checked = Convert.ToBoolean(dt.Rows[0]["_Other"]);
                txtOther.Text = dt.Rows[0]["WathOther"].ToString();

                CBBenaaHome.Checked = Convert.ToBoolean(dt.Rows[0]["BenaManzil"]);
                if (CBBenaaHome.Checked == true)
                    PnlBenaaHome.Visible = true;

                CBTarmemHome.Checked = Convert.ToBoolean(dt.Rows[0]["TarmemManzil"]);
                if (CBTarmemHome.Checked == true)
                    PnlTarmemHome.Visible = true;

                CBTathithHome.Checked = Convert.ToBoolean(dt.Rows[0]["TathithManzil"]);
                if (CBTathithHome.Checked == true)
                    PnlTathithHome.Visible = true;

                DLAlBaheth.SelectedValue = dt.Rows[0]["IDAlBaheth"].ToString();
                ImgBaheth.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDAlBaheth"]));

                DLModerAlGmeiah.SelectedValue = dt.Rows[0]["IDModer"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsModerAllow"]))
                    ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDModer"]), Convert.ToBoolean(dt.Rows[0]["IsModerAllow"]));
                else
                    ImgModer.Visible = false;

                DLRaeesLagnatAlBahath.SelectedValue = dt.Rows[0]["IDRaesLagnatAlBahth"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsRaesLagnatAlBahthAllow"]))
                    ImgRaesLagnatAlBahthAllow.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDRaesLagnatAlBahth"]), Convert.ToBoolean(dt.Rows[0]["IsRaesLagnatAlBahthAllow"]));
                else
                    ImgRaesLagnatAlBahthAllow.Visible = false;

                if (Convert.ToBoolean(dt.Rows[0]["IsModerAllow"]) && Convert.ToBoolean(dt.Rows[0]["IsRaesLagnatAlBahthAllow"]))
                    IDKhatm.Visible = true;

                if (dt.Rows[0]["A2"].ToString() == "0" || dt.Rows[0]["A2"].ToString() == string.Empty)
                    IDNote.Visible = false;
                else
                {
                    lbl_Note.Text = dt.Rows[0]["A2"].ToString();
                    IDNote.Visible = true;
                }
                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/PageVisitReportDetails.aspx?ID=" + txtSearch.Text.Trim();
                Class_QRScan.FGetQRCode(code, imgBarCode);

                pnlPrint.Visible = true;
                pnlSelect.Visible = false;
                FGetImgBenaaHome();
                DLAlBaheth.Visible = true;
                DLModerAlGmeiah.Visible = true;
                DLRaeesLagnatAlBahath.Visible = true;
                lblAlBaheth.Visible = false;
                lblModerAlGmeiah.Visible = false;
                lblRaeesLagnatAlBahath.Visible = false;
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

    private void FGetImgBenaaHome()
    {
        ClassReportAlZyaratImages CRZI = new ClassReportAlZyaratImages();
        CRZI._Top = 6;
        CRZI._IDMustafeed = Convert.ToInt32(lblNumberMostafeed.Text);
        CRZI._IDReport = Convert.ToInt32(lblNumberReport.Text.Trim());
        CRZI._IDType = 1;
        CRZI._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CRZI.BArnReportAlZyaratImagesByID();
        if (dt.Rows.Count > 0)
        {
            RPTBenaaHome.DataSource = dt;
            RPTBenaaHome.DataBind();
        }
        FGetImgTarmemHome();
    }

    private void FGetImgTarmemHome()
    {
        ClassReportAlZyaratImages CRZI = new ClassReportAlZyaratImages();
        CRZI._Top = 15;
        CRZI._IDMustafeed = Convert.ToInt32(lblNumberMostafeed.Text);
        CRZI._IDReport = Convert.ToInt32(lblNumberReport.Text.Trim());
        CRZI._IDType = 2;
        CRZI._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CRZI.BArnReportAlZyaratImagesByID();
        if (dt.Rows.Count > 0)
        {
            RPTTarmemHome.DataSource = dt;
            RPTTarmemHome.DataBind();
        }
        FGetImgTathithHome();
    }

    private void FGetImgTathithHome()
    {
        ClassReportAlZyaratImages CRZI = new ClassReportAlZyaratImages();
        CRZI._Top = 15;
        CRZI._IDMustafeed = Convert.ToInt32(lblNumberMostafeed.Text);
        CRZI._IDReport = Convert.ToInt32(lblNumberReport.Text.Trim());
        CRZI._IDType = 3;
        CRZI._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CRZI.BArnReportAlZyaratImagesByID();
        if (dt.Rows.Count > 0)
        {
            RPTTathithHome.DataSource = dt;
            RPTTathithHome.DataBind();
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            lblAlBaheth.Text = DLAlBaheth.SelectedItem.ToString();
            lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
            lblRaeesLagnatAlBahath.Text = DLRaeesLagnatAlBahath.SelectedItem.ToString();
            DLAlBaheth.Visible = false;
            DLModerAlGmeiah.Visible = false;
            DLRaeesLagnatAlBahath.Visible = false;
            lblAlBaheth.Visible = true;
            lblModerAlGmeiah.Visible = true;
            lblRaeesLagnatAlBahath.Visible = true;
            Session["foot"] = pnlPrint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetArnReportAlZyaratByDetails();
        FArnReportAlZyaratElectricalAppliancesGetByMostafeed();
        System.Threading.Thread.Sleep(100);
    }

}