using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageAfieldVisitDetails : System.Web.UI.Page
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
            bool A49;
            A49 = Convert.ToBoolean(dtViewProfil.Rows[0]["A49"]);
            if (A49 == false)
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
            //FGetAlBaheth();
            FGetDataMostafed();
        }
    }

    private void FGetDataMostafed()
    {
        try
        {
            ClassZeyarahMaydanyah CZM = new ClassZeyarahMaydanyah();
            CZM._NumberAlZyarah = Convert.ToInt64(txtSearch.Text.Trim());
            CZM._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CZM.BArnZeyarahMaydanyahByID();
            if (dt.Rows.Count > 0)
            {
                lblNumberZyara.Text = txtSearch.Text.Trim();
                lblDateZyara.Text = Convert.ToDateTime(dt.Rows[0]["DataAddAlZeyarah"]).ToString("dd/MM/yyyy");
                lblNameMosTafeed.Text = dt.Rows[0]["NameMostafeed"].ToString();
                lblAlqariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
                lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();

                CBBahthHalatMosTafeed.Checked = Convert.ToBoolean(dt.Rows[0]["BahthHalatMosTafeed"]);
                if (CBBahthHalatMosTafeed.Checked)
                {
                    IDBahthHalatMosTafeed.Visible = true;
                }
                CBEadatAlBahthLestafeedHaly.Checked = Convert.ToBoolean(dt.Rows[0]["EadatAlBahthLestafeedHaly"]);
                if (CBEadatAlBahthLestafeedHaly.Checked)
                {
                    IDEadatAlBahthLestafeedHaly.Visible = true;
                }
                CBEadatAlBahthLeMostafeedSabeq.Checked = Convert.ToBoolean(dt.Rows[0]["EadatAlBahthLeMostafeedSabeq"]);
                if (CBEadatAlBahthLeMostafeedSabeq.Checked)
                {
                    IDEadatAlBahthLeMostafeedSabeq.Visible = true;
                }

                lblNameBaheth.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["NameAlBaheth"]));
                ImgBaheth.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["NameAlBaheth"]));

                lblDay.Text = dt.Rows[0]["ToDay"].ToString();
                lblAllsoDay.Text = Convert.ToDateTime(dt.Rows[0]["DayAlZeyarah"]).ToString("dd/MM/yyyy");
                lblModer.Text = ClassQuaem.FAlModer(Convert.ToInt32(dt.Rows[0]["IDModer"]));
                if (Convert.ToBoolean(dt.Rows[0]["IsModer"]))
                {
                    ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDModer"]), Convert.ToBoolean(dt.Rows[0]["IsModer"]));
                }
                else
                {
                    ImgModer.Visible = false;
                }

                CBAllowAlZeyarah.Checked = Convert.ToBoolean(dt.Rows[0]["AllowAlZeyarah"]);
                //if (CBAllowAlZeyarah.Checked)
                //{
                    IDAllowAlZeyarah.Visible = true;
                //}
                CBNotAllowAlZeyarah.Checked = Convert.ToBoolean(dt.Rows[0]["NotAllowAlZeyarah"]);
                if (CBNotAllowAlZeyarah.Checked)
                {
                    IDNotAllowAlZeyarahlabel.Visible = true;
                }
                IDNotAllowAlZeyarah.Visible = true;
                lblAlAsBab.Text = dt.Rows[0]["AlAsBab"].ToString();
                //lblRaeesMaglesAEdarah.Text = ClassQuaem.FRaeesMaglesAlEdarah(Convert.ToInt32(dt.Rows[0]["IDRaeesMaglesAEdarah"]));
                //if (Convert.ToBoolean(dt.Rows[0]["AllowAlZeyarah"]))
                //{
                //    ImgRaeesMaglesAEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDRaeesMaglesAEdarah"]), Convert.ToBoolean(dt.Rows[0]["AllowAlZeyarah"]));
                //}
                //else
                //{
                //    ImgRaeesMaglesAEdarah.Visible = false;
                //}
                

                lblDataEntery.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["IDAdmin"].ToString()));
                lblDateEntery.Text = Convert.ToDateTime(dt.Rows[0]["DataAddAlZeyarah"]).ToString("dd/MM/yyyy");

                if (Convert.ToBoolean(dt.Rows[0]["AllowAlZeyarah"]))
                {
                    IDKhatm.Visible = true;
                }
                else
                {
                    IDKhatm.Visible = false;
                }

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/PageAfieldVisitDetails.aspx?ID=" + txtSearch.Text.Trim();
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
            
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
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
        FGetDataMostafed();
    }

}