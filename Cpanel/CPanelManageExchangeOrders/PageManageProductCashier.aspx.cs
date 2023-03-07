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

public partial class Cpanel_CPanelManageExchangeOrders_PageManageProductCashier : System.Web.UI.Page
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
            bool A106, A109;
            A106 = Convert.ToBoolean(dtViewProfil.Rows[0]["A106"]);
            A109 = Convert.ToBoolean(dtViewProfil.Rows[0]["A109"]);
            if (A109 == false)
                Response.Redirect("PageNotAccess.aspx");
            if (A106 == false)
            {
                IDAdd.Visible = false;
                GVCashier.Columns[8].Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FArnBenaaAndTarmimByCashier();
            pnlSelect.Visible = true;
        }
    }

    private void FArnBenaaAndTarmimByCashier()
    {
        lblCountHoseHear.Text = "0" ;
        GVApprovalOfTheDirectorHose.Columns[0].Visible = true;
        GVApprovalOfTheDirectorHose.Columns[11].Visible = true;
        GVApprovalOfTheDirectorHose.UseAccessibleHeader = false;
        ClassBenaaAndTarmim CBAT = new ClassBenaaAndTarmim();
        CBAT._IsAllowModer = true;
        CBAT._AllowState = false;
        CBAT._NotAllowState = false;
        CBAT._IsAllowRaeesAlMagles = false;
        CBAT._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CBAT.BArnBenaaAndTarmimByModer();
        if (dt.Rows.Count > 0)
        {
            GVApprovalOfTheDirectorHose.DataSource = dt;
            GVApprovalOfTheDirectorHose.DataBind();
            lblCountHose.Text = Convert.ToString(dt.Rows.Count);
            lblCountHoseHear.Text = lblCountHose.Text + " ملف ";
            pnlNullHose.Visible = false;
            pnlDataHose.Visible = true;
        }
        else
        {
            pnlNullHose.Visible = true;
            pnlDataHose.Visible = false;
        }
        FArnSupportForPrismsByModer();
    }

    private void FGetCountCart()
    {
        lblCountCard.Text = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [_billNumber],[_IDMosTafeed],[_DateCaming],[_IDType],[_IsActive],[_IDAdmin],[_IDRaeesMaglisAlEdarah],[_IsRaeesMaglisAlEdarah],[_IDAmmenAlSondoq],[_IsAmmenAlSondoq],[_IDModer],[_IsModer],[_IDStorekeeper],[_IsStorekeeper],[_A1],[_IsDelete],[_IDCategory],[_IDDelivery]FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber <> @0 And _IDMosTafeed <> @0 And _IsRaeesMaglisAlEdarah = @1 And _IsAmmenAlSondoq = @2 And _IsModer = @3 And _IsStorekeeper = @4 And _IsDelete = @5", "0", "0", "0", "1", "0", "0");
        if (dt.Rows.Count > 0)
            lblCountCard.Text = Convert.ToString(dt.Rows.Count) + " ملف ";
    }

    protected void RBTathith_CheckedChanged(object sender, EventArgs e)
    {
        if (RBTathith.Checked)
        {
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            IDHose.Visible = false;
            IDPrisms.Visible = false;
        }
    }

    protected void RBCardCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (RBCardCheck.Checked)
        {
            RPTarmem.Checked = false;
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            IDHose.Visible = false;
            IDPrisms.Visible = false;
            FArnProductShopByCashier(true, false, false, false);
        }
    }

    protected void RBDeviceCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (RBDeviceCheck.Checked)
        {
            RPTarmem.Checked = false;
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            IDHose.Visible = false;
            IDPrisms.Visible = false;
            FArnProductShopByCashier(false, true, false, false);
        }
    }

    protected void RBTathithCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (RBTathithCheck.Checked)
        {
            RPTarmem.Checked = false;
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            IDHose.Visible = false;
            IDPrisms.Visible = false;
            FArnProductShopByCashier(false, false, true, false);
        }
    }

    protected void RBTalefCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (RBTalefCheck.Checked)
        {
            RPTarmem.Checked = false;
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            IDHose.Visible = false;
            IDPrisms.Visible = false;
            FArnProductShopByCashier(false, false, false, true);
        }
    }

    private void FArnProductShopByCashier(bool Cart, bool Device, bool Tathith, bool Talef)
    {
        try
        {
            GVCashier.UseAccessibleHeader = false;
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = Convert.ToInt32(0);
            CPS.IDMosTafeed = Convert.ToInt32(0);
            CPS.IsRaeesMaglisAlEdarah = false;
            CPS.IsAmmenAlSondoq = false;
            CPS.IsModer = true;
            CPS.IsStorekeeper = false;
            CPS.IsDelete = false;
            CPS.IsCart = Cart;
            CPS.IsDevice = Device;
            CPS.IsTathith = Tathith;
            CPS.IsTalef = Talef;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopByModer();
            if (dt.Rows.Count > 0)
            {
                GVCashier.DataSource = dt;
                GVCashier.DataBind();
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

    protected void RPTarmem_CheckedChanged(object sender, EventArgs e)
    {
        if (RPTarmem.Checked)
        {
            pnlSelect.Visible = false;
            IDCard.Visible = false;
            IDHose.Visible = true;
            IDPrisms.Visible = false;

            RBCardCheck.Checked = false;
            RBDeviceCheck.Checked = false;
            RBTathithCheck.Checked = false;
            RBTalefCheck.Checked = false;
        }
    }

    protected void btnRefrish_Click1(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductCashier.aspx");
    }

    protected void btnPrint_Click1(object sender, EventArgs e)
    {
        try
        {
            GVCashier.Columns[0].Visible = false;
            GVCashier.Columns[11].Visible = false;

            GVCashier.UseAccessibleHeader = true;
            GVCashier.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnAllow_Click1(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVCashier.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVCashier.DataKeys[row.RowIndex].Value);
                    Label lbCate = default(Label);
                    lbCate = (Label)row.FindControl("lblCategory");
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ProductShopWarehouse] SET [_IsAmmenAlSondoq] = @_IsAmmenAlSondoq WHERE _billNumber = @_billNumber And _IDCategory = @IDCategory And _IsCart = @IsCart And _IsDevice = @IsDevice And _IsTathith = @IsTathith And _IsTalef = @IsTalef";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@_billNumber", Comp_ID);
                    cmd.Parameters.AddWithValue("@IDCategory", Convert.ToInt64(lbCate.Text));
                    cmd.Parameters.AddWithValue("@IsCart", RBCardCheck.Checked);
                    cmd.Parameters.AddWithValue("@IsDevice", RBDeviceCheck.Checked);
                    cmd.Parameters.AddWithValue("@IsTathith", RBTathithCheck.Checked);
                    cmd.Parameters.AddWithValue("@IsTalef", RBTalefCheck.Checked);
                    cmd.Parameters.AddWithValue("@_IsAmmenAlSondoq", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            System.Threading.Thread.Sleep(500);
            Response.Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBRefrshHose_Click(object sender, EventArgs e)
    {
        pnlSelect.Visible = false;
        IDCard.Visible = false;
        IDHose.Visible = true;

        RBCardCheck.Checked = false;
        RBDeviceCheck.Checked = false;
        RBTathithCheck.Checked = false;
        RBTalefCheck.Checked = false;
        FArnBenaaAndTarmimByCashier();
    }

    protected void LBPrintHose_Click(object sender, EventArgs e)
    {
        try
        {
            GVApprovalOfTheDirectorHose.Columns[0].Visible = false;
            GVApprovalOfTheDirectorHose.Columns[11].Visible = false;

            GVApprovalOfTheDirectorHose.UseAccessibleHeader = true;
            GVApprovalOfTheDirectorHose.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlDataHose;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }
    
    protected void btnAllowHose_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVApprovalOfTheDirectorHose.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVApprovalOfTheDirectorHose.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[BenaaAndTarmim] SET [AllowState] = @AllowState , AllowStateDetalis = @AllowStateDetalis , NotAllowState = @NotAllowState , WhayNotAllow = @WhayNotAllow , Date_AllowOrNotAllow = @Date_AllowOrNotAllow WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@AllowState", true);
                    cmd.Parameters.AddWithValue("@AllowStateDetalis", txtAllowStateDetalis.Text.Trim());
                    cmd.Parameters.AddWithValue("@NotAllowState", false);
                    cmd.Parameters.AddWithValue("@WhayNotAllow", txtNotAllowStateDetalis.Text.Trim());
                    cmd.Parameters.AddWithValue("@Date_AllowOrNotAllow", ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"));
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            System.Threading.Thread.Sleep(500);
            FArnBenaaAndTarmimByCashier();
        }
        catch (Exception)
        {

        }
    }

    protected void btnNotAllowHose_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVApprovalOfTheDirectorHose.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVApprovalOfTheDirectorHose.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[BenaaAndTarmim] SET [AllowState] = @AllowState , AllowStateDetalis = @AllowStateDetalis , NotAllowState = @NotAllowState , WhayNotAllow = @WhayNotAllow , Date_AllowOrNotAllow = @Date_AllowOrNotAllow WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@AllowState", false);
                    cmd.Parameters.AddWithValue("@AllowStateDetalis", txtAllowStateDetalis.Text.Trim());
                    cmd.Parameters.AddWithValue("@NotAllowState", true);
                    cmd.Parameters.AddWithValue("@WhayNotAllow", txtNotAllowStateDetalis.Text.Trim());
                    cmd.Parameters.AddWithValue("@Date_AllowOrNotAllow", ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"));
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            System.Threading.Thread.Sleep(500);
            FArnBenaaAndTarmimByCashier();
        }
        catch (Exception)
        {

        }
    }
    
    protected void RPSupportForPrisms_CheckedChanged(object sender, EventArgs e)
    {
        if (RPSupportForPrisms.Checked)
        {
            pnlSelect.Visible = false;
            IDCard.Visible = false;
            IDHose.Visible = false;
            IDPrisms.Visible = true;

            RBCardCheck.Checked = false;
            RBDeviceCheck.Checked = false;
            RBTathithCheck.Checked = false;
            RBTalefCheck.Checked = false;
        }
    }

    private void FArnSupportForPrismsByModer()
    {
        lblCountPrismsHear.Text = "0";
        GVApprovalOfTheDirectorPrisms.Columns[0].Visible = true;
        GVApprovalOfTheDirectorPrisms.Columns[11].Visible = true;
        GVApprovalOfTheDirectorPrisms.UseAccessibleHeader = false;
        ClassSupportForPrisms CSFP = new ClassSupportForPrisms();
        CSFP._IsAllowModer = true;
        CSFP._AllowState = false;
        CSFP._NotAllowState = false;
        CSFP._IsAllowRaeesAlMagles = false;
        CSFP._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CSFP.BArnSupportForPrismsByModer();
        if (dt.Rows.Count > 0)
        {
            GVApprovalOfTheDirectorPrisms.DataSource = dt;
            GVApprovalOfTheDirectorPrisms.DataBind();
            lblCountPrisms.Text = Convert.ToString(dt.Rows.Count);
            lblCountPrismsHear.Text = lblCountPrisms.Text + " ملف ";
            pnlNullPrisms.Visible = false;
            pnlDataPrisms.Visible = true;
        }
        else
        {
            pnlNullPrisms.Visible = true;
            pnlDataPrisms.Visible = false;
        }
        FGetCountCart();
    }

    protected void btnPrisms_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVApprovalOfTheDirectorPrisms.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVApprovalOfTheDirectorPrisms.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_SupportForPrisms] SET [AllowState] = @AllowState , AllowStateDetalis = @AllowStateDetalis , NotAllowState = @NotAllowState , WhayNotAllow = @WhayNotAllow , Date_AllowOrNotAllow = @Date_AllowOrNotAllow WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@AllowState", true);
                    cmd.Parameters.AddWithValue("@AllowStateDetalis", txtAllowStateDetalisPrisms.Text.Trim());
                    cmd.Parameters.AddWithValue("@NotAllowState", false);
                    cmd.Parameters.AddWithValue("@WhayNotAllow", txtNotAllowStateDetalisPrisms.Text.Trim());
                    cmd.Parameters.AddWithValue("@Date_AllowOrNotAllow", ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"));
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            System.Threading.Thread.Sleep(500);
            FArnSupportForPrismsByModer();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrshPrisms_Click(object sender, EventArgs e)
    {
        FArnSupportForPrismsByModer();
    }

    protected void btnPrintPrisms_Click(object sender, EventArgs e)
    {
        try
        {
            GVApprovalOfTheDirectorPrisms.Columns[0].Visible = false;
            GVApprovalOfTheDirectorPrisms.Columns[11].Visible = false;

            GVApprovalOfTheDirectorPrisms.UseAccessibleHeader = true;
            GVApprovalOfTheDirectorPrisms.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlDataPrisms;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnNotAllowPrisms_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVApprovalOfTheDirectorPrisms.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVApprovalOfTheDirectorPrisms.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_SupportForPrisms] SET [AllowState] = @AllowState , AllowStateDetalis = @AllowStateDetalis , NotAllowState = @NotAllowState , WhayNotAllow = @WhayNotAllow , Date_AllowOrNotAllow = @Date_AllowOrNotAllow WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@AllowState", false);
                    cmd.Parameters.AddWithValue("@AllowStateDetalis", txtAllowStateDetalisPrisms.Text.Trim());
                    cmd.Parameters.AddWithValue("@NotAllowState", true);
                    cmd.Parameters.AddWithValue("@WhayNotAllow", txtNotAllowStateDetalisPrisms.Text.Trim());
                    cmd.Parameters.AddWithValue("@Date_AllowOrNotAllow", ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"));
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            System.Threading.Thread.Sleep(500);
            FArnSupportForPrismsByModer();
        }
        catch (Exception)
        {
            return;
        }
    }
    
    protected void btnDeleteHose_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVApprovalOfTheDirectorHose.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVApprovalOfTheDirectorHose.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[BenaaAndTarmim] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FArnBenaaAndTarmimByCashier();
        }
        catch (Exception)
        {
            return;
        }
    }
    
    protected void btnDeletePrisms_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVApprovalOfTheDirectorPrisms.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVApprovalOfTheDirectorPrisms.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_SupportForPrisms] SET [IsDelete] = @IsDelete WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FArnSupportForPrismsByModer();
        }
        catch (Exception)
        {
            return;
        }
    }

}