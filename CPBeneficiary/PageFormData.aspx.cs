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

public partial class CPBeneficiary_PageFormData : System.Web.UI.Page
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
            bool A3;
            A3 = Convert.ToBoolean(dt.Rows[0]["A3"]);
            if (A3)
                FGetDataMostafed(dt.Rows[0]["NumberMostafeed"].ToString());
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
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            ClassAdmin_Arn.FGetRaeesLagnatAlBahath(DLRaeesLagnatAlBahath);
            CheckAccountAdmin();                      
        }
    }

    private void FGetDataMostafed(string XID)
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", XID, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            try
            {
                lblFileNumber.Text = XID;
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
                    lblAge.Text = ClassSaddam.FCheckAndGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]));
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
                lblCount.Text = lblCountBoys.Text;

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

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/PageBeneficiaryByView.aspx?ID=" + dt.Rows[0]["NumberMostafeed"].ToString();
                Class_QRScan.FGetQRCode(code, imgBarCode);

                //+ "&XID="+ dt.Rows[0]["IDUniq"].ToString() + "&chs=75";
            }
            catch (Exception)
            {
                lblAge.Text = "لم يُضاف";
                return;
            }
            FGetData(XID);
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

    private void FGetData(string XID)
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[TarafEstemarah] With(NoLock) Where NumberMostafed = @0 And IsDelete = @1", XID, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVMenu.DataSource = dt;
            GVMenu.DataBind();
            pnlNull.Visible = false;
            pnlData.Visible = true;
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            //pnllblPrint.Visible = true;
            pnlDlPrint.Visible = false;
            lblAlBaheth.Text = DLAlBaheth.SelectedItem.ToString();
            lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
            lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
            lblRaeesLagnatAlBahath.Text = DLRaeesLagnatAlBahath.SelectedItem.ToString();
            Session["foot"] = pnlPrint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../Cpanel/PagePrintNew.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }


}