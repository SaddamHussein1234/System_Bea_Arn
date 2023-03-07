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

public partial class CResearchers_CPVillage_PageBeneficiaryByView : System.Web.UI.Page
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
            bool A40;
            A40 = Convert.ToBoolean(dtViewProfil.Rows[0]["A40"]);
            if (A40 == false)
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
            txtSearch.Text = Request.QueryString["ID"];
            FGetAlBaheth();
            FGetDataMostafed();
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
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesMaglesAlEdarah.Items.Clear();
            DLRaeesMaglesAlEdarah.Items.Add("");
            DLRaeesMaglesAlEdarah.AppendDataBoundItems = true;
            DLRaeesMaglesAlEdarah.DataValueField = "ID_Item";
            DLRaeesMaglesAlEdarah.DataTextField = "FirstName";
            DLRaeesMaglesAlEdarah.DataSource = dt;
            DLRaeesMaglesAlEdarah.DataBind();
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

    private void FGetDataMostafed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", txtSearch.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            try
            {
                GetCookie();
                DataTable dtGetQariah = new DataTable();
                dtGetQariah = ClassDataAccess.GetData("SELECT Top(1) [NumberMostafeed],[AlQaryah],[IsDelete] FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0", txtSearch.Text.Trim());
                if (dtGetQariah.Rows.Count > 0)
                {
                    DataTable dtCheck = new DataTable();
                    dtCheck = ClassDataAccess.GetData("SELECT Top(1) [IDItem],[IDAdminJoin],[IDQariah],[IsDelete] FROM [dbo].[tbl_MultiQariah] With(NoLock) Where IDAdminJoin = @0 And IDQariah = @1 And IsDelete = @2"
                        , IDUser, dtGetQariah.Rows[0]["AlQaryah"].ToString(), Convert.ToString(false));
                    if (dtCheck.Rows.Count > 0)
                    {
                        lblFileNumber.Text = txtSearch.Text.Trim();
                        lblName.Text = dt.Rows[0]["NameMostafeed"].ToString();
                        lblTypeMostafeed.Text = dt.Rows[0]["TypeMostafeed"].ToString();
                        lblAlQariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
                        lblGender.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
                        lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
                        lblNumberSigal.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
                        lblDateRigstry.Text = dt.Rows[0]["DateRegistry"].ToString();
                        lblHalatAlmostafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));

                        lblDateBrithDay.Text = ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["dateBrith"])) + "هـ" + " - " + Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") + "مـ";
                        if (dt.Rows[0]["dateBrith"].ToString() != string.Empty)
                        {
                            lblAge.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
                        }
                        else
                        {
                            lblAge.Text = dt.Rows[0]["Age"].ToString();
                        }

                        if (Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") == "01/01/1900")
                        {
                            lblDateBrithDay.Text = "لم يُضاف";
                            lblAge.Text = "لم يُضاف";
                        }

                        lblAlMehnahAlHaliyahllmostafeed.Text = dt.Rows[0]["AlMehnahAlHaliyahllmostafeed"].ToString();
                        lblAlHalahAlTaelimiahllmostafeed.Text = dt.Rows[0]["AlHalahAlTaelimiahllmostafeed"].ToString();
                        if (lblHalatAlmostafeed.Text == "(ارمله)" || lblHalatAlmostafeed.Text == "(ايتام)")
                        {
                            pnlCheckDead.Visible = true;
                            lblMehnahAlAAbKablAlWafah.Text = dt.Rows[0]["MehnahAlAAbKablAlWafah"].ToString();
                        }
                        else
                        {
                            pnlCheckDead.Visible = false;
                        }

                        CBSaleem.Checked = Convert.ToBoolean(dt.Rows[0]["Saleem"]);
                        CBMaak.Checked = Convert.ToBoolean(dt.Rows[0]["Moalek"]);
                        lblTypaAleaakah.Text = dt.Rows[0]["TypeAleakah"].ToString();
                        CBMareed.Checked = Convert.ToBoolean(dt.Rows[0]["Mareedh"]);
                        lblMareed.Text = dt.Rows[0]["TypeAlmaradh"].ToString();

                        lblDataEntery.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["EsmAlMostakhdem"]));
                        lblDateEntery.Text = dt.Rows[0]["dateEntry"].ToString();
                        lblCountBoys.Text = dt.Rows[0]["AfradAlOsrah"].ToString();

                        lblAlDakhlAlShahryllMostafeed.Text = dt.Rows[0]["AlDakhlAlShahryllMostafeed"].ToString();
                        lblMasderAlDakhl.Text = ClassQuaem.FAlDakhlAlShahryWaMasdarah(Convert.ToInt32(dt.Rows[0]["MasderAlDakhl"])).ToString();

                        lblTypeAlMaskan.Text = ClassQuaem.FTypeAlMaskan(Convert.ToInt32(dt.Rows[0]["TypeAlMasken"]));
                        lblHalatAlMaskan.Text = ClassQuaem.FHalatAlMaskan(Convert.ToInt32(dt.Rows[0]["HaletAlMasken"]));

                        if (dt.Rows[0]["AlBaheth"].ToString() != "0")
                        {
                            DLAlBaheth.SelectedValue = dt.Rows[0]["AlBaheth"].ToString();
                            ImgAlBaheth.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["AlBaheth"]));
                            ImgAlBaheth2.ImageUrl = ImgAlBaheth.ImageUrl;
                        }
                        DLRaeesLagnatAlBahath.SelectedValue = dt.Rows[0]["IDRaeesLagnatAlBahth_"].ToString();
                        if (Convert.ToBoolean(dt.Rows[0]["IsAllowRaeesLagnatAlBahth_"]))
                        {
                            ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDRaeesLagnatAlBahth_"]), Convert.ToBoolean(dt.Rows[0]["IsAllowRaeesLagnatAlBahth_"]));
                            ImgRaeesLagnatAlBahath2.ImageUrl = ImgRaeesLagnatAlBahath.ImageUrl;
                        }
                        else
                        {
                            ImgRaeesLagnatAlBahath.Visible = false;
                            ImgRaeesLagnatAlBahath2.Visible = false;
                        }
                        DLModerAlGmeiah.SelectedValue = dt.Rows[0]["IDModer_"].ToString();
                        if (Convert.ToBoolean(dt.Rows[0]["IsAllowModer_"]))
                        {
                            ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDModer_"]), Convert.ToBoolean(dt.Rows[0]["IsAllowModer_"]));
                            ImgModer2.ImageUrl = ImgModer.ImageUrl;
                        }
                        else
                        {
                            ImgModer.Visible = false;
                            ImgModer2.Visible = false;
                        }

                        DLRaeesMaglesAlEdarah.SelectedValue = dt.Rows[0]["IDRaeesMaglisAlEdarah_"].ToString();
                        if (Convert.ToBoolean(dt.Rows[0]["IsRaeesMaglisAlEdarah_"]))
                        {
                            ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDRaeesMaglisAlEdarah_"]), Convert.ToBoolean(dt.Rows[0]["IsRaeesMaglisAlEdarah_"]));
                            ImgRaeesMaglesAlEdarah2.ImageUrl = ImgRaeesMaglesAlEdarah.ImageUrl;
                        }
                        else
                        {
                            ImgRaeesMaglesAlEdarah.Visible = false;
                            ImgRaeesMaglesAlEdarah2.Visible = false;
                        }
                        if (Convert.ToBoolean(dt.Rows[0]["IsRaeesMaglisAlEdarah_"]))
                        {
                            IDKhatm.Visible = true;
                        }
                        else
                        {
                            IDKhatm.Visible = false;
                        }
                        pnlPrint.Visible = true;
                        pnlSelect.Visible = false;
                        FGetImgMosTafeed(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]), Convert.ToInt32(dt.Rows[0]["_IDImg"]));

                        lblAlBaheth.Text = DLAlBaheth.SelectedItem.ToString();
                        lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
                        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
                        lblRaeesLagnatAlBahath.Text = DLRaeesLagnatAlBahath.SelectedItem.ToString();

                        IDBarcode.ImageUrl = "http://chart.apis.google.com/chart?cht=qr&chl=" + ClassSetting.FGetNameServer() +
                            "/Cpanel/PScan.aspx?ID=" + dt.Rows[0]["NumberMostafeed"].ToString() + "&chs=75";
                    }
                    else
                    {
                        Response.Redirect("PageNotAccess.aspx");
                    }
                }
            }
            catch (Exception)
            {
                lblAge.Text = "لم يُضاف";
            }
            FGetData();
        }
        else
        {
            pnlPrint.Visible = false;
            pnlSelect.Visible = true;
        }
    }

    private void FGetImgMosTafeed(int IDMustafeed, int IDImg)
    {
        ClassRasAlEstemarahImages CREI = new ClassRasAlEstemarahImages();
        CREI._Top = 15;
        CREI.IDMustafeed = IDMustafeed;
        CREI._IDUniqInt = IDImg;
        CREI._IDType = 1;
        CREI._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CREI.BArnRasAlEstemarahImagesImagesByID();
        if (dt.Rows.Count > 0)
        {
            RPTAttach.DataSource = dt;
            RPTAttach.DataBind();
            pnlImgAttach.Visible = true;
            pnlImgAttachNull.Visible = false;
        }
        else
        {
            pnlImgAttach.Visible = false;
            pnlImgAttachNull.Visible = true;
        }
    }

    private void FGetData()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[TarafEstemarah] Where NumberMostafed = @0 And IsDelete = @1", txtSearch.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVMenu.DataSource = dt;
            GVMenu.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
        }
        txtSearch.Focus();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //pnllblPrint.Visible = true;
        pnlDlPrint.Visible = false;
        lblAlBaheth.Text = DLAlBaheth.SelectedItem.ToString();
        lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
        lblRaeesLagnatAlBahath.Text = DLRaeesLagnatAlBahath.SelectedItem.ToString();
        Session["foot"] = pnlPrint;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PagePrintNew.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetDataMostafed();
        //pnllblPrint.Visible = false;
        //pnlDlPrint.Visible = true;
    }

}