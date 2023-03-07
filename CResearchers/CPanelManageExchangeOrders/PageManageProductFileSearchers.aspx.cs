using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CResearchers_CPanelManageExchangeOrders_PageManageProductFileSearchers : System.Web.UI.Page
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
            bool A106, A111;
            A106 = Convert.ToBoolean(dtViewProfil.Rows[0]["A106"]);
            A111 = Convert.ToBoolean(dtViewProfil.Rows[0]["A111"]);
            if (A111 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            if (A106 == false)
            {
                IDAdd.Visible = false;
                GVFileSearchers.Columns[8].Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
            FGetAlBaheth();
            FGetCountCart();
        }
    }

    private void FGetCountCart()
    {
        lblCountCard.Text = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [_billNumber],[_IDMosTafeed],[_DateCaming],[_IDType],[_IsActive],[_IDAdmin],[_IDRaeesMaglisAlEdarah],[_IsRaeesMaglisAlEdarah],[_IDAmmenAlSondoq],[_IsAmmenAlSondoq],[_IDModer],[_IsModer],[_IDStorekeeper],[_IsStorekeeper],[_A1],[_IsDelete],[_IDCategory],[_IDDelivery]FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber <> @0 And _IDMosTafeed <> @0 And _IDDelivery = @1 And _IsRaeesMaglisAlEdarah = @2 And _IsAmmenAlSondoq = @3 And _IsModer = @4 And _IsStorekeeper = @5 And _IsDelete = @6 And (_IsReceived = @7 And _IsNotReceived = @7)", "0", IDUser, "1", "1", "1", "1", "0", "0");
        if (dt.Rows.Count > 0)
        {
            lblCountCard.Text = Convert.ToString(dt.Rows.Count) + " ملف ";
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductFileSearchers.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        GVFileSearchers.Columns[0].Visible = false;
        GVFileSearchers.Columns[9].Visible = false;
        lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
        lblRaeesLagnatAlBahath.Text = DLRaeesLagnatAlBahath.SelectedItem.ToString();
        DLAlBaheth.Visible = false;
        DLModerAlGmeiah.Visible = false;
        DLRaeesMaglesAlEdarah.Visible = false;
        DLRaeesLagnatAlBahath.Visible = false;
        lblAlBaheth.Visible = true;
        lblModerAlGmeiah.Visible = true;
        lblRaeesMaglesAlEdarah.Visible = true;
        lblRaeesLagnatAlBahath.Visible = true;

        GVFileSearchers.UseAccessibleHeader = true;
        GVFileSearchers.HeaderRow.TableSection = TableRowSection.TableHeader;

        Session["footable1"] = pnlData;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");

    }

    protected void RBTathith_CheckedChanged(object sender, EventArgs e)
    {
        if (RBTathith.Checked)
        {
            pnlSelect.Visible = false;
            IDCard.Visible = true;
        }
    }

    protected void RBCardCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (RBCardCheck.Checked)
        {
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            FArnProductShopByFileSearchers(true, false, false, false);
        }
    }

    protected void RBDeviceCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (RBDeviceCheck.Checked)
        {
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            FArnProductShopByFileSearchers(false, true, false, false);
        }
    }

    protected void RBTathithCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (RBTathithCheck.Checked)
        {
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            FArnProductShopByFileSearchers(false, false, true, false);
        }
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        try
        {
            GVFileSearchers.UseAccessibleHeader = false;
            foreach (GridViewRow row in GVFileSearchers.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVFileSearchers.DataKeys[row.RowIndex].Value);
                    Label lbCate = default(Label);
                    lbCate = (Label)row.FindControl("lblCategory");
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ProductShopWarehouse] SET [_IsReceived] = @_IsReceived , _IsNotReceived = @_IsNotReceived , _DateCaming = @_DateCaming WHERE _billNumber = @_billNumber And _IDCategory = @IDCategory And _IsCart = @IsCart And _IsDevice = @IsDevice And _IsTathith = @IsTathith And _IsTalef = @IsTalef";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@_billNumber", Comp_ID);
                    cmd.Parameters.AddWithValue("@IDCategory", Convert.ToInt64(lbCate.Text));
                    cmd.Parameters.AddWithValue("@IsCart", RBCardCheck.Checked);
                    cmd.Parameters.AddWithValue("@IsDevice", RBDeviceCheck.Checked);
                    cmd.Parameters.AddWithValue("@IsTathith", RBTathithCheck.Checked);
                    cmd.Parameters.AddWithValue("@IsTalef", RBTalefCheck.Checked);
                    cmd.Parameters.AddWithValue("@_IsReceived", true);
                    cmd.Parameters.AddWithValue("@_IsNotReceived", false);
                    cmd.Parameters.AddWithValue("@_DateCaming", ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"));
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            System.Threading.Thread.Sleep(500);
            FArnProductShopByFileSearchers(RBCardCheck.Checked, RBDeviceCheck.Checked, RBTathithCheck.Checked, RBTalefCheck.Checked);
            FGetCountCart();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnNotAllow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtNotAllow.Text != string.Empty)
            {
                lblNotAllow.Visible = false;
                foreach (GridViewRow row in GVFileSearchers.Rows)
                {
                    if ((row.FindControl("chkSelect") as CheckBox).Checked)
                    {
                        string Comp_ID = Convert.ToString(GVFileSearchers.DataKeys[row.RowIndex].Value);
                        Label lbCate = default(Label);
                        lbCate = (Label)row.FindControl("lblCategory");
                        SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                        conn.Open();
                        string sql = "UPDATE [dbo].[ProductShopWarehouse] SET [_IsReceived] = @_IsReceived , _IsNotReceived = @_IsNotReceived , _DateCaming = @_DateCaming , _A2 = @_A2 WHERE _billNumber = @_billNumber And _IDCategory = @IDCategory And _IsCart = @IsCart And _IsDevice = @IsDevice And _IsTathith = @IsTathith And _IsTalef = @IsTalef";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@_billNumber", Comp_ID);
                        cmd.Parameters.AddWithValue("@IDCategory", Convert.ToInt64(lbCate.Text));
                        cmd.Parameters.AddWithValue("@IsCart", RBCardCheck.Checked);
                        cmd.Parameters.AddWithValue("@IsDevice", RBDeviceCheck.Checked);
                        cmd.Parameters.AddWithValue("@IsTathith", RBTathithCheck.Checked);
                        cmd.Parameters.AddWithValue("@IsTalef", RBTalefCheck.Checked);
                        cmd.Parameters.AddWithValue("@_IsReceived", false);
                        cmd.Parameters.AddWithValue("@_IsNotReceived", true);
                        cmd.Parameters.AddWithValue("@_DateCaming", ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"));
                        cmd.Parameters.AddWithValue("@_A2", txtNotAllow.Text.Trim());
                        cmd.ExecuteScalar();
                        conn.Close();
                    }
                }
                System.Threading.Thread.Sleep(500);
                FArnProductShopByFileSearchers(RBCardCheck.Checked, RBDeviceCheck.Checked, RBTathithCheck.Checked, RBTalefCheck.Checked);
                FGetCountCart();
            }
            else if (txtNotAllow.Text == string.Empty)
            {
                lblNotAllow.Visible = true;
                txtNotAllow.Focus();
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    int tempcounter = 0;
    protected void GVFileSearchers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tempcounter = tempcounter + 1;
            if (tempcounter == 14)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }
    }

    private void FGetAlBaheth()
    {
        //DataTable dt = new DataTable();
        //dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsBaheth = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        //if (dt.Rows.Count > 0)
        //{
        //    //DLAlBaheth.Items.Clear();
        //    //DLAlBaheth.Items.Add("");
        //    //DLAlBaheth.AppendDataBoundItems = true;
        //    DLAlBaheth.DataValueField = "ID_Item";
        //    DLAlBaheth.DataTextField = "FirstName";
        //    DLAlBaheth.DataSource = dt;
        //    DLAlBaheth.DataBind();
        //}
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsModer = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
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
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
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
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesLgnatAlBath = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesLagnatAlBahath.DataValueField = "ID_Item";
            DLRaeesLagnatAlBahath.DataTextField = "FirstName";
            DLRaeesLagnatAlBahath.DataSource = dt;
            DLRaeesLagnatAlBahath.DataBind();
        }
        //ImgAlBaheth.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLAlBaheth.SelectedValue));
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
        ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

    protected void DLModerAlGmeiah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
    }

    protected void DLRaeesLagnatAlBahath_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
    }

    protected void DLRaeesMaglesAlEdarah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

    private void FArnProductShopByFileSearchers(bool Cart, bool Device, bool Tathith, bool Talef)
    {
        try
        {
            GetCookie();
            GVFileSearchers.UseAccessibleHeader = false;
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.IDCheck = 1;
            CPS.billNumber = Convert.ToInt32(0);
            CPS.IDMosTafeed = Convert.ToInt32(0);
            CPS.IsRaeesMaglisAlEdarah = true;
            CPS.IsAmmenAlSondoq = true;
            CPS.IsModer = true;
            CPS.IsStorekeeper = true;
            CPS.IsDelete = false;
            CPS.IDDelivery = int.Parse(IDUser);
            CPS.IsReceived = false;
            CPS.IsCart = Cart;
            CPS.IsDevice = Device;
            CPS.IsTathith = Tathith;
            CPS.IsTalef = Talef;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopByFileSearchers();
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = " قائمة أوامر الصرف التي تحتاج إلى مراجعة الباحث لمشروع ( " + ClassQuaem.FSupportType(Convert.ToInt64(dt.Rows[0]["_IDCategory"])) + " ) ";
                GVFileSearchers.DataSource = dt;
                GVFileSearchers.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }
    
    protected void RBTalefCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (RBTalefCheck.Checked)
        {
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            FArnProductShopByFileSearchers(false, false, false, true);
        }
    }

}