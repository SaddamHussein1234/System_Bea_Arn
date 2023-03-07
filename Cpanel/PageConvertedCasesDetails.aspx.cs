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

public partial class Cpanel_PageConvertedCasesDetails : System.Web.UI.Page
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
            bool A58;
            A58 = Convert.ToBoolean(dtViewProfil.Rows[0]["A58"]);
            if (A58 == false)
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
            FGetArnEadatMostafeedByDetails();
        }
    }

    private void FGetArnEadatMostafeedByDetails()
    {
        try
        {
            ClassTahweelAlHalah CTA = new ClassTahweelAlHalah();
            CTA._NumberOrder = Convert.ToInt32(txtSearch.Text.Trim());
            CTA._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CTA.BArnTahweelAlHalahByDetails();
            if (dt.Rows.Count > 0)
            {
                lblDateQarar.Text = Convert.ToDateTime(dt.Rows[0]["DateOrder"]).ToString("dd/MM/yyyy");
                lblNameMosTafeed.Text = dt.Rows[0]["NameMostafeed"].ToString();
                lblAlqariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
                lblNumberAlSegelAlMadany.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
                lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
                lblHalafAlMosTafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
                lblNumberMostafeed.Text = dt.Rows[0]["NumberMostafeed"].ToString();
                lblNumberOrder.Text = dt.Rows[0]["NumberOrder"].ToString();
                lblDateEntery.Text = Convert.ToDateTime(dt.Rows[0]["DateAddOrder"]).ToString("dd/MM/yyyy");
                lblDataEntery.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["IDAdmin"].ToString()));

                if (Convert.ToInt32(dt.Rows[0]["IDUpdate"]) != 0)
                {
                    LastUpdate.Visible = true;
                    lblUpdate.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["IDUpdate"].ToString()));
                    lblDateLastUpdate.Text = dt.Rows[0]["DateLastUpdate"].ToString();
                }
                else
                {
                    LastUpdate.Visible = false;
                }
                
                lblModer.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["IDModer"]));
                if (Convert.ToBoolean(dt.Rows[0]["AllowAlhalah"]))
                {
                    ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDModer"]), Convert.ToBoolean(dt.Rows[0]["AllowAlhalah"]));
                    ImgModer.Width = 100;
                    ImgModer.Visible = true;
                    IDKhatm.Visible = true;
                }
                else
                {
                    ImgModer.ImageUrl = "loaderMin.gif";
                    ImgModer.Width = 30;
                    ImgModer.Visible = true;
                    IDKhatm.Visible = false;
                }
                lblOld.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalatAlmostafeedBefor"]));
                lblNew.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalatAlmostafeedAfter"]));
                lblWhayTahweel.Text = dt.Rows[0]["SabbAlTahweel"].ToString();
                lblAlbaheth.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["IDAlbaheth"]));
                ImgBaheth.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDAlbaheth"]));
                CBAllow.Checked = Convert.ToBoolean(dt.Rows[0]["AllowAlhalah"]);
                if (CBAllow.Checked == true)
                {
                    IDAllow.Visible = true;
                }

                CBNotAllow.Checked = Convert.ToBoolean(dt.Rows[0]["BlockAlhalah"]);
                if (CBNotAllow.Checked == true)
                {
                    IDNotAllow.Visible = true;
                }

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/PageConvertedCasesDetails.aspx?ID=" + txtSearch.Text.Trim() + "&XID=" + dt.Rows[0]["IDUniq"].ToString();
                Class_QRScan.FGetQRCode(code, imgBarCode);

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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["foot"] = pnlPrint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetArnEadatMostafeedByDetails();
        System.Threading.Thread.Sleep(200);
    }

}