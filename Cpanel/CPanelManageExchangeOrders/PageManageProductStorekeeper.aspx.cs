﻿using Library_CLS_Arn.ClassOutEntity;
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

public partial class Cpanel_CPanelManageExchangeOrders_PageManageProductStorekeeper : System.Web.UI.Page
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
            bool A106, A107;
            A106 = Convert.ToBoolean(dtViewProfil.Rows[0]["A106"]);
            A107 = Convert.ToBoolean(dtViewProfil.Rows[0]["A107"]);
            if (A107 == false)
                Response.Redirect("PageNotAccess.aspx");
            if (A106 == false)
            {
                IDAdd.Visible = false;
                GVStorekeeper.Columns[8].Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
            FGetCountCart();
        }
    }

    private void FGetCountCart()
    {
        lblCountCard.Text = "0";
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT DISTINCT TOP 1000 [_billNumber],[_IDMosTafeed],[_DateCaming],[_IDType],[_IsActive],[_IDAdmin],[_IDRaeesMaglisAlEdarah],[_IsRaeesMaglisAlEdarah],[_IDAmmenAlSondoq],[_IsAmmenAlSondoq],[_IDModer],[_IsModer],[_IDStorekeeper],[_IsStorekeeper],[_A1],[_IsDelete],[_IDCategory],[_IDDelivery]FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber <> @0 And _IDMosTafeed <> @0 And _IsRaeesMaglisAlEdarah = @1 And _IsAmmenAlSondoq = @2 And _IsModer = @3 And _IsStorekeeper = @4 And _IsDelete = @5", "0", "1", "1", "1", "0", "0");
        if (dt.Rows.Count > 0)
            lblCountCard.Text = Convert.ToString(dt.Rows.Count) + " ملف ";
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductStorekeeper.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVStorekeeper.Columns[0].Visible = false;
            GVStorekeeper.Columns[9].Visible = false;
            GVStorekeeper.UseAccessibleHeader = true;
            GVStorekeeper.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        try
        {
            GVStorekeeper.UseAccessibleHeader = false;
            foreach (GridViewRow row in GVStorekeeper.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVStorekeeper.DataKeys[row.RowIndex].Value);
                    Label lbCate = default(Label);
                    lbCate = (Label)row.FindControl("lblCategory");
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ProductShopWarehouse] SET [_IsStorekeeper] = @_IsStorekeeper , _IsDone = @_IsDone , _IsNotDone = @_IsNotDone , _ExpiryDate = @_ExpiryDate WHERE _billNumber = @_billNumber And _IDCategory = @IDCategory And _IsCart = @IsCart And _IsDevice = @IsDevice And _IsTathith = @IsTathith And _IsTalef = @IsTalef";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@_billNumber", Comp_ID);
                    cmd.Parameters.AddWithValue("@IDCategory", Convert.ToInt64(lbCate.Text));
                    cmd.Parameters.AddWithValue("@IsCart", RBCardCheck.Checked);
                    cmd.Parameters.AddWithValue("@IsDevice", RBDeviceCheck.Checked);
                    cmd.Parameters.AddWithValue("@IsTathith", RBTathithCheck.Checked);
                    cmd.Parameters.AddWithValue("@IsTalef", RBTalefCheck.Checked);
                    cmd.Parameters.AddWithValue("@_IsStorekeeper", true);
                    cmd.Parameters.AddWithValue("@_IsDone", true);
                    cmd.Parameters.AddWithValue("@_IsNotDone", false);
                    cmd.Parameters.AddWithValue("@_ExpiryDate", ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"));
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            System.Threading.Thread.Sleep(500);
            FArnProductShopByModer(RBCardCheck.Checked, RBDeviceCheck.Checked, RBTathithCheck.Checked, RBTalefCheck.Checked);
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
            foreach (GridViewRow row in GVStorekeeper.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVStorekeeper.DataKeys[row.RowIndex].Value);
                    Label lbCate = default(Label);
                    lbCate = (Label)row.FindControl("lblCategory");
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ProductShopWarehouse] SET [_IsStorekeeper] = @_IsStorekeeper , _IsDone = @_IsDone , _IsNotDone = @_IsNotDone , _ExpiryDate = @_ExpiryDate WHERE _billNumber = @_billNumber And _IDCategory = @IDCategory And _IsCart = @IsCart And _IsDevice = @IsDevice And _IsTathith = @IsTathith And _IsTalef = @IsTalef";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@_billNumber", Comp_ID);
                    cmd.Parameters.AddWithValue("@IDCategory", Convert.ToInt64(lbCate.Text));
                    cmd.Parameters.AddWithValue("@IsCart", RBCardCheck.Checked);
                    cmd.Parameters.AddWithValue("@IsDevice", RBDeviceCheck.Checked);
                    cmd.Parameters.AddWithValue("@IsTathith", RBTathithCheck.Checked);
                    cmd.Parameters.AddWithValue("@IsTalef", RBTalefCheck.Checked);
                    cmd.Parameters.AddWithValue("@_IsStorekeeper", true);
                    cmd.Parameters.AddWithValue("@_IsDone", false);
                    cmd.Parameters.AddWithValue("@_IsNotDone", true);
                    cmd.Parameters.AddWithValue("@_ExpiryDate", ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"));
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            System.Threading.Thread.Sleep(500);
            FArnProductShopByModer(RBCardCheck.Checked, RBDeviceCheck.Checked, RBTathithCheck.Checked, RBTalefCheck.Checked);
        }
        catch (Exception)
        {
            return;
        }
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
            FArnProductShopByModer(true, false, false, false);
        }
    }

    protected void RBDeviceCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (RBDeviceCheck.Checked)
        {
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            FArnProductShopByModer(false, true, false, false);
        }
    }

    protected void RBTathithCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (RBTathithCheck.Checked)
        {
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            FArnProductShopByModer(false, false, true, false);
        }
    }

    protected void RBTalefCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (RBTalefCheck.Checked)
        {
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            FArnProductShopByModer(false, false, false, true);
        }
    }

    private void FArnProductShopByModer(bool Cart, bool Device, bool Tathith, bool Talef)
    {
        try
        {
            GVStorekeeper.UseAccessibleHeader = false;
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = Convert.ToInt32(0);
            CPS.IDMosTafeed = Convert.ToInt32(0);
            CPS.IsRaeesMaglisAlEdarah = true;
            CPS.IsAmmenAlSondoq = true;
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
                
                GVStorekeeper.DataSource = dt;
                GVStorekeeper.DataBind();
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

}