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

public partial class CResearchers_CPVillage_PageVisitReportDetails : System.Web.UI.Page
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
            FGetAlBaheth();
            pnlSelect.Visible = true;
            FArnReportAlZyaratElectricalAppliancesGetByMostafeed();
            txtSearch.Focus();
            if (Request.QueryString["ID"] != null)
            {
                txtSearch.Text = Request.QueryString["ID"];
                FGetArnReportAlZyaratByDetails(Convert.ToInt32(Request.QueryString["ID"]));
            }
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

    private void FGetArnReportAlZyaratByDetails(int IDNumber)
    {
        GetCookie();
        DataTable dtGetQariah = new DataTable();
        dtGetQariah = ClassDataAccess.GetData("SELECT Top(1) ReportAlZyarat.NumberMostafeed,[AlQaryah],ReportAlZyarat.IsDelete FROM [dbo].[ReportAlZyarat] With(NoLock) Inner Join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = ReportAlZyarat.NumberMostafeed Where NumberReport = @0 And ReportAlZyarat.IsDelete = @1 And RasAlEstemarah.IsDelete = @1", txtSearch.Text.Trim(), Convert.ToString(false));
        if (dtGetQariah.Rows.Count > 0)
        {
            DataTable dtCheck = new DataTable();
            dtCheck = ClassDataAccess.GetData("SELECT Top(1) [IDItem],[IDAdminJoin],[IDQariah],[IsDelete] FROM [dbo].[tbl_MultiQariah] With(NoLock) Where IDAdminJoin = @0 And IDQariah = @1 And IsDelete = @2"
                , IDUser, dtGetQariah.Rows[0]["AlQaryah"].ToString(), Convert.ToString(false));
            if (dtCheck.Rows.Count > 0)
            {
                try
                {
                    ClassReportAlZyarat CRA = new ClassReportAlZyarat();
                    CRA._NumberReport = IDNumber;
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
                            ImgModer.Width = 100;
                            ImgModer.Visible = true;
                        }
                        else
                        {
                            ImgModer.ImageUrl = "../loaderMin.gif";
                            ImgModer.Width = 30;
                            ImgModer.Visible = true;
                        }

                        DLRaeesLagnatAlBahath.SelectedValue = dt.Rows[0]["IDRaesLagnatAlBahth"].ToString();
                        if (Convert.ToBoolean(dt.Rows[0]["IsRaesLagnatAlBahthAllow"]))
                        {
                            ImgRaesLagnatAlBahthAllow.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDRaesLagnatAlBahth"]), Convert.ToBoolean(dt.Rows[0]["IsRaesLagnatAlBahthAllow"]));
                            ImgRaesLagnatAlBahthAllow.Width = 100;
                            ImgRaesLagnatAlBahthAllow.Visible = true;
                        }
                        else
                        {
                            ImgRaesLagnatAlBahthAllow.ImageUrl = "../loaderMin.gif";
                            ImgRaesLagnatAlBahthAllow.Width = 30;
                            ImgRaesLagnatAlBahthAllow.Visible = true;
                        }

                        if (Convert.ToBoolean(dt.Rows[0]["IsModerAllow"]) && Convert.ToBoolean(dt.Rows[0]["IsRaesLagnatAlBahthAllow"]))
                        {
                            IDKhatm.Visible = true;
                        }

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
            else
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
        else
        {
            Response.Redirect("PageNotAccess.aspx");
        }
    }

    private void FGetAlBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsBaheth = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAlBaheth.Items.Clear();
            DLAlBaheth.Items.Add("");
            DLAlBaheth.AppendDataBoundItems = true;
            DLAlBaheth.DataValueField = "ID_Item";
            DLAlBaheth.DataTextField = "FirstName";
            DLAlBaheth.DataSource = dt;
            DLAlBaheth.DataBind();
        }
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsModer = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLModerAlGmeiah.Items.Clear();
            DLModerAlGmeiah.Items.Add("");
            DLModerAlGmeiah.AppendDataBoundItems = true;
            DLModerAlGmeiah.DataValueField = "ID_Item";
            DLModerAlGmeiah.DataTextField = "FirstName";
            DLModerAlGmeiah.DataSource = dt;
            DLModerAlGmeiah.DataBind();
        }
        FGetRaeesLagnatAlBahath();
    }

    private void FGetRaeesLagnatAlBahath()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesLgnatAlBath = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesLagnatAlBahath.Items.Clear();
            DLRaeesLagnatAlBahath.Items.Add("");
            DLRaeesLagnatAlBahath.AppendDataBoundItems = true;
            DLRaeesLagnatAlBahath.DataValueField = "ID_Item";
            DLRaeesLagnatAlBahath.DataTextField = "FirstName";
            DLRaeesLagnatAlBahath.DataSource = dt;
            DLRaeesLagnatAlBahath.DataBind();
        }
    }

    private void FGetImgBenaaHome()
    {
        ClassReportAlZyaratImages CRZI = new ClassReportAlZyaratImages();
        CRZI._Top = 6;
        CRZI._IDMustafeed = Convert.ToInt32(lblNumberMostafeed.Text);
        CRZI._IDReport = Convert.ToInt32(txtSearch.Text.Trim());
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
        CRZI._IDMustafeed = Convert.ToInt32(lblNumberMostafeed.Text);
        CRZI._IDReport = Convert.ToInt32(txtSearch.Text.Trim());
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
        CRZI._IDMustafeed = Convert.ToInt32(lblNumberMostafeed.Text);
        CRZI._IDReport = Convert.ToInt32(txtSearch.Text.Trim());
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
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetArnReportAlZyaratByDetails(Convert.ToInt32(txtSearch.Text.Trim()));
        System.Threading.Thread.Sleep(200);
    }

}