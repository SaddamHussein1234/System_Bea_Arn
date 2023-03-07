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

public partial class Shaerd_ERP_FMS_GeneralAssembly_PageView : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    string IDUniq = string.Empty;
    private void GetCookie()
    {
        try
        {
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
            bool A146;
            A146 = Convert.ToBoolean(dtViewProfil.Rows[0]["A146"]);
            if (A146 == false)
                Response.Redirect("/Cpanel/CHome/PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (XType == "GA")
                CheckAccountAdmin();
            pnlSelect.Visible = true;
            txtSearch.Text = Request.QueryString["ID"];
            txtSearch.Focus();
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT TOP (1) TGAB.*,TA.[ID_Item],TA.[FirstName],TA.[AddImgSignature] FROM [dbo].[tbl_General_Assmply_Bill] TGAB With(NoLock) Inner Join tbl_General_Assmply TGA on TGA.[ID_Item_] = TGAB.[Number_Admin_] Inner Join tbl_Admin TA on TA.ID_Item = TGA.ID_Admin_Account_ Where [bill_Number_] = @0 And TGAB.[Is_Delete_] = @1",
                txtSearch.Text.Trim(), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                IDEdit.HRef = "/Cpanel/ERP/FMS/GA/PageEdit.aspx?XID=" + dt.Rows[0]["ID_Uniq_"].ToString();
                lblNumber.Text = dt.Rows[0]["bill_Number_"].ToString();
                lbl_Name.Text = dt.Rows[0]["FirstName"].ToString();
                Img_Admin.ImageUrl = "/" + dt.Rows[0]["AddImgSignature"].ToString();
                lbl_Name_2.Text = lbl_Name.Text;
                lblTotalPrice.Text = dt.Rows[0]["The_Mony_"].ToString();
                lblDateHide.Text = ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["_Date_Get_"])) + "هـ" + " - " + Convert.ToDateTime(dt.Rows[0]["_Date_Get_"]).ToString("dd/MM/yyyy") + "مـ";

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblTotalPrice.Text), currencies[Convert.ToInt32(0)]);
                lblSumSaraf.Text = toWord.ConvertToArabic();

                CBCash_Money_.Checked = Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]);
                CBShayk_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["IsShayk_Bank_"]);
                CBTrnfire_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["IsEdaa_Bank_"]);
                if (CBCash_Money_.Checked)
                {
                    PnlCash.Visible = true;
                    PnlShayk.Visible = false;
                    PnlTrnfire.Visible = false;
                }
                else if (CBShayk_Bank.Checked)
                {
                    PnlCash.Visible = false;
                    PnlShayk.Visible = true;
                    PnlTrnfire.Visible = false;
                    lblNumber_Shayk_Bank_.Text = dt.Rows[0]["Number_Shayk_"].ToString();
                    lblDate_Shayk_Bank_.Text = Convert.ToDateTime(dt.Rows[0]["Date_Shayk_Bank_"]).ToString("dd/MM/yyyy");
                    lblFor_Bank_.Text = dt.Rows[0]["For_Bank_"].ToString();

                }
                else if (CBTrnfire_Bank.Checked)
                {
                    PnlCash.Visible = false;
                    PnlShayk.Visible = false;
                    PnlTrnfire.Visible = true;
                    lblFor_Edaa_Bank.Text = dt.Rows[0]["For_Edaa_Bank_"].ToString();
                    lblNumber_Edaa.Text = dt.Rows[0]["Number_Edaa_"].ToString();
                    lblDate_Edaa_Bank.Text = Convert.ToDateTime(dt.Rows[0]["Date_Edaa_Bank_"]).ToString("dd/MM/yyyy");
                }
                lblMore_Details.Text = dt.Rows[0]["More_Details_"].ToString();
                lbl_Years.Text = dt.Rows[0]["_Years_"].ToString();
                lblAmeenAlsondoq.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["ID_Ameen_Alsondoq_"].ToString()));
                if (Convert.ToBoolean(dt.Rows[0]["IsAllow_Ameen_Alsondoq_"]))
                {
                    ImgAmeenAlsondoq.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Ameen_Alsondoq_"]), Convert.ToBoolean(dt.Rows[0]["IsAllow_Ameen_Alsondoq_"]));
                    ImgAmeenAlsondoq.Width = 100;
                    ImgAmeenAlsondoq.Height = 30;
                    ImgAmeenAlsondoq.Visible = true;
                }
                else
                {
                    ImgAmeenAlsondoq.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgAmeenAlsondoq.Width = 30;
                    ImgAmeenAlsondoq.Visible = true;
                }

                lblReesAlmaglis.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["ID_Raees_AlMagles_"].ToString()));
                if (Convert.ToBoolean(dt.Rows[0]["IsAllow_Raees_AlMagles_"]))
                {
                    ImgRaees_AlMagles.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["ID_Raees_AlMagles_"]), Convert.ToBoolean(dt.Rows[0]["IsAllow_Raees_AlMagles_"]));
                    ImgRaees_AlMagles.Visible = true;
                    ImgRaees_AlMagles.Width = 100;
                    ImgRaees_AlMagles.Height = 30;
                    IDKhatm.Visible = true;
                }
                else
                {
                    ImgRaees_AlMagles.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgRaees_AlMagles.Width = 30;
                    ImgRaees_AlMagles.Visible = true;
                    IDKhatm.Visible = false;
                }
                lblDataEntry.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["ID_Admin_"].ToString()));
                lblDateEntry.Text = Convert.ToDateTime(dt.Rows[0]["Date_Add_"]).ToString("dd/MM/yyyy");
                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/ERP/FMS/GA/PageView.aspx?ID=" + txtSearch.Text.Trim();
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

        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["foot"] = pnlPrint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA5.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
    }

}