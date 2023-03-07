using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CPBeneficiary_PageStatusDetails : System.Web.UI.Page
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
            bool A1;
            A1 = Convert.ToBoolean(dt.Rows[0]["A1"]);
            if (A1)
                FGetArnBahthHalatMostafeedByDetails(dt.Rows[0]["NumberMostafeed"].ToString());
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
            FGetRaeesLagnatAlBahath();
            ClassAdmin_Arn.FGetAlBaheeth(DLAlBaheth);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            pnlSelect.Visible = true;
            CheckAccountAdmin();
        }
    }

    private void FGetRaeesLagnatAlBahath()
    {
        ClassAdmin_Arn.FGetRaeesLagnatAlBahath(DLRaeesLagnatAlBahath);
    }

    private void FGetArnBahthHalatMostafeedByDetails(string XID)
    {
        try
        {
            ClassBahthHalatMostafeed CBHM = new ClassBahthHalatMostafeed();
            CBHM._IDNumberReport = Convert.ToInt32(XID);
            CBHM._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CBHM.BArnBahthHalatMostafeedByDetails();
            if (dt.Rows.Count > 0)
            {
                lblDateQarar.Text = Convert.ToDateTime(dt.Rows[0]["DateOfReport"]).ToString("dd/MM/yyyy");
                lblNameMosTafeed.Text = dt.Rows[0]["NameMostafeed"].ToString();
                lblAlqariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
                lblNumberAlSegelAlMadany.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
                lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
                lblHalafAlMosTafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
                lblNumberMostafeed.Text = dt.Rows[0]["NumberMostafeed"].ToString();
                lblNumberOrder.Text = dt.Rows[0]["IDNumberReport"].ToString();
                lblDateEntery.Text = Convert.ToDateTime(dt.Rows[0]["DateAddReport"]).ToString("dd/MM/yyyy");
                lblDataEntery.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["IDAdmin"].ToString()));

                DLModerAlGmeiah.SelectedValue = dt.Rows[0]["IDModer"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsAllowModer"]))
                {
                    ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDModer"]), Convert.ToBoolean(dt.Rows[0]["IsAllowModer"]));
                    ImgModer.Width = 100;
                    ImgModer.Visible = true;
                }
                else
                {
                    ImgModer.ImageUrl = "loaderMin.gif";
                    ImgModer.Width = 30;
                    ImgModer.Visible = true;
                }
                lblAllowState.Text = dt.Rows[0]["AllowStateDetalis"].ToString();
                lblWhayNotAllow.Text = dt.Rows[0]["WhayNotAllow"].ToString();
                DLAlBaheth.SelectedValue = dt.Rows[0]["IDAlbaheth"].ToString();
                ImgAlBaheth.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDAlbaheth"]));
                DLRaeesLagnatAlBahath.SelectedValue = dt.Rows[0]["IDRaeesLagnatAlBahth"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsAllowRaeesLagnatAlBahth"]))
                {
                    ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDRaeesLagnatAlBahth"]), Convert.ToBoolean(dt.Rows[0]["IsAllowRaeesLagnatAlBahth"]));
                    ImgRaeesLagnatAlBahath.Width = 100;
                    ImgRaeesLagnatAlBahath.Visible = true;
                }
                else
                {
                    ImgRaeesLagnatAlBahath.ImageUrl = "loaderMin.gif";
                    ImgRaeesLagnatAlBahath.Width = 30;
                    ImgRaeesLagnatAlBahath.Visible = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["IsAllowModer"]) && Convert.ToBoolean(dt.Rows[0]["IsAllowRaeesLagnatAlBahth"]))
                {
                    IDKhatm.Visible = true;
                }
                else
                {
                    IDKhatm.Visible = false;
                }
                CBAllow.Checked = Convert.ToBoolean(dt.Rows[0]["AllowState"]);

                //if (CBAllow.Checked == true)
                //{
                IDAllow.Visible = true;
                //}
                CBNotAllow.Checked = Convert.ToBoolean(dt.Rows[0]["NotAllowState"]);
                if (CBNotAllow.Checked == true)
                {
                    IDNotAllowlabel.Visible = true;

                }
                IDNotAllow.Visible = true;
                pnlPrint.Visible = true;
                pnlSelect.Visible = false;
                //if (Convert.ToBoolean(dt.Rows[0]["AllowState"]) == false && Convert.ToBoolean(dt.Rows[0]["NotAllowState"]) == false)
                //{
                //    pnlPrint.Visible = false;
                //    pnlSelect.Visible = true;
                //    lblMsg.Text = "يبدو أن المدير لم يطلع على هذه الحالة";
                //    lblMsg.ForeColor = System.Drawing.Color.Red;
                //}
                DLAlBaheth.Visible = true;
                DLModerAlGmeiah.Visible = true;
                DLRaeesLagnatAlBahath.Visible = true;
                lblAlbaheth.Visible = false;
                lblModerAlGmeiah.Visible = false;
                lblRaeesLagnatAlBahath.Visible = false;

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/PageSearchStatusDetails.aspx?ID=" + dt.Rows[0]["IDNumberReport"].ToString();
                Class_QRScan.FGetQRCode(code, imgBarCode);
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
            lblAlbaheth.Text = DLAlBaheth.SelectedItem.ToString();
            lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
            lblRaeesLagnatAlBahath.Text = DLRaeesLagnatAlBahath.SelectedItem.ToString();
            DLAlBaheth.Visible = false;
            DLModerAlGmeiah.Visible = false;
            DLRaeesLagnatAlBahath.Visible = false;
            lblAlbaheth.Visible = true;
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