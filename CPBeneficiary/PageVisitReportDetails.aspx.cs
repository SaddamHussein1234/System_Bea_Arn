using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CPBeneficiary_PageVisitReportDetails : System.Web.UI.Page
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
            bool A5;
            A5 = Convert.ToBoolean(dt.Rows[0]["A5"]);
            if (A5)
            {
                FGetArnReportAlZyaratByDetails(dt.Rows[0]["NumberMostafeed"].ToString());
                FArnReportAlZyaratElectricalAppliancesGetByMostafeed();
            }
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
            
            ClassAdmin_Arn.FGetAlBaheeth(DLAlBaheth);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            ClassAdmin_Arn.FGetRaeesLagnatAlBahath(DLRaeesLagnatAlBahath);
            pnlSelect.Visible = false;          
            CheckAccountAdmin();
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
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetArnReportAlZyaratByDetails(string XID)
    {
        try
        {
            ClassReportAlZyarat CRA = new ClassReportAlZyarat();
            CRA._IDUniq = Convert.ToString(Request.QueryString["ID"]);
            CRA._NumberMostafeed = Convert.ToInt32(XID);
            CRA._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CRA.BArnReportAlZyaratByIDUniq();
            if (dt.Rows.Count > 0)
            {
                lblNumberReport.Text = dt.Rows[0]["NumberReport"].ToString();
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
                {
                    PnlBenaaHome.Visible = true;
                }

                CBTarmemHome.Checked = Convert.ToBoolean(dt.Rows[0]["TarmemManzil"]);
                if (CBTarmemHome.Checked == true)
                {
                    PnlTarmemHome.Visible = true;
                }

                CBTathithHome.Checked = Convert.ToBoolean(dt.Rows[0]["TathithManzil"]);
                if (CBTathithHome.Checked == true)
                {
                    PnlTathithHome.Visible = true;
                }

                DLAlBaheth.SelectedValue = dt.Rows[0]["IDAlBaheth"].ToString();
                ImgBaheth.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDAlBaheth"]));

                DLModerAlGmeiah.SelectedValue = dt.Rows[0]["IDModer"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsModerAllow"]))
                {
                    ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDModer"]), Convert.ToBoolean(dt.Rows[0]["IsModerAllow"]));
                }
                else
                {
                    ImgModer.Visible = false;
                }

                DLRaeesLagnatAlBahath.SelectedValue = dt.Rows[0]["IDRaesLagnatAlBahth"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsRaesLagnatAlBahthAllow"]))
                {
                    ImgRaesLagnatAlBahthAllow.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDRaesLagnatAlBahth"]), Convert.ToBoolean(dt.Rows[0]["IsRaesLagnatAlBahthAllow"]));
                }
                else
                {
                    ImgRaesLagnatAlBahthAllow.Visible = false;
                }

                if (Convert.ToBoolean(dt.Rows[0]["IsModerAllow"]) && Convert.ToBoolean(dt.Rows[0]["IsRaesLagnatAlBahthAllow"]))
                {
                    IDKhatm.Visible = true;
                }

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/PageVisitReportDetails.aspx?ID=" + lblNumberReport.Text;
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
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../Cpanel/PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

}