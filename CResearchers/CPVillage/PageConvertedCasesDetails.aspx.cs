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

public partial class CResearchers_CPVillage_PageConvertedCasesDetails : System.Web.UI.Page
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
            txtSearch.Focus();
            if (Request.QueryString["ID"] != null)
            {
                txtSearch.Text = Request.QueryString["ID"];
                FGetArnEadatMostafeedByDetails(Convert.ToInt32(Request.QueryString["ID"]));
            }
        }
    }

    private void FGetArnEadatMostafeedByDetails(int IDNumber)
    {
        GetCookie();
        DataTable dtGetQariah = new DataTable();
        dtGetQariah = ClassDataAccess.GetData("SELECT Top(1) TahweelAlHalah.NumberMostafeed,[AlQaryah],TahweelAlHalah.IsDelete FROM [dbo].[TahweelAlHalah] With(NoLock) Inner Join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = TahweelAlHalah.NumberMostafeed Where NumberOrder = @0 And TahweelAlHalah.IsDelete = @1 And RasAlEstemarah.IsDelete = @1", txtSearch.Text.Trim(), Convert.ToString(false));
        if (dtGetQariah.Rows.Count > 0)
        {
            DataTable dtCheck = new DataTable();
            dtCheck = ClassDataAccess.GetData("SELECT Top(1) [IDItem],[IDAdminJoin],[IDQariah],[IsDelete] FROM [dbo].[tbl_MultiQariah] With(NoLock) Where IDAdminJoin = @0 And IDQariah = @1 And IsDelete = @2"
                , IDUser, dtGetQariah.Rows[0]["AlQaryah"].ToString(), Convert.ToString(false));
            if (dtCheck.Rows.Count > 0)
            {
                try
                {
                    ClassTahweelAlHalah CTA = new ClassTahweelAlHalah();
                    CTA._NumberOrder = IDNumber;
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
                            ImgModer.ImageUrl = "../loaderMin.gif";
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Session["foot"] = pnlPrint;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetArnEadatMostafeedByDetails(Convert.ToInt32(txtSearch.Text.Trim()));
        System.Threading.Thread.Sleep(200);
    }

}