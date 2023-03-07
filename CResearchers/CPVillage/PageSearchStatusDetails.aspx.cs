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

public partial class CResearchers_CPVillage_PageSearchStatusDetails : System.Web.UI.Page
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
            bool A42;
            A42 = Convert.ToBoolean(dtViewProfil.Rows[0]["A42"]);
            if (A42 == false)
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

            if (Request.QueryString["ID"] != null)
            {
                txtSearch.Text = Request.QueryString["ID"];
                txtSearch.Focus();
                FGetArnBahthHalatMostafeedByDetails(Convert.ToInt32(Request.QueryString["ID"]));
            }
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

    private void FGetArnBahthHalatMostafeedByDetails(int IDNumber)
    {
        GetCookie();
        DataTable dtGetQariah = new DataTable();
        dtGetQariah = ClassDataAccess.GetData("SELECT Top(1) BahthHalatMostafeed.NumberMostafeed,[AlQaryah],BahthHalatMostafeed.IsDelete FROM [dbo].[BahthHalatMostafeed] With(NoLock) Inner Join RasAlEstemarah on RasAlEstemarah.NumberMostafeed = BahthHalatMostafeed.NumberMostafeed Where IDNumberReport = @0", txtSearch.Text.Trim());
        if (dtGetQariah.Rows.Count > 0)
        {
            DataTable dtCheck = new DataTable();
            dtCheck = ClassDataAccess.GetData("SELECT Top(1) [IDItem],[IDAdminJoin],[IDQariah],[IsDelete] FROM [dbo].[tbl_MultiQariah] With(NoLock) Where IDAdminJoin = @0 And IDQariah = @1 And IsDelete = @2"
                , IDUser, dtGetQariah.Rows[0]["AlQaryah"].ToString(), Convert.ToString(false));
            if (dtCheck.Rows.Count > 0)
            {
                try
                {
                    ClassBahthHalatMostafeed CBHM = new ClassBahthHalatMostafeed();
                    CBHM._IDNumberReport = IDNumber;
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
                            ImgModer.ImageUrl = "../loaderMin.gif";
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
                            ImgRaeesLagnatAlBahath.ImageUrl = "../loaderMin.gif";
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

                        IDBarcode.ImageUrl = "http://chart.apis.google.com/chart?cht=qr&chl=" + ClassSetting.FGetNameServer() +
                            "/Cpanel/PScanS.aspx?ID=" + dt.Rows[0]["NumberMostafeed"].ToString() + "&chs=75";
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
        }
        else
        {
            Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
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
        //Session["Title"] = txtTitle.Text.Trim() + " : " + lblNameMosTafeed.Text;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetArnBahthHalatMostafeedByDetails(Convert.ToInt32(txtSearch.Text.Trim()));
        System.Threading.Thread.Sleep(200);
    }

}