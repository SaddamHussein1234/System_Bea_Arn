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

public partial class Cpanel_CPanelManageWarehouse_PageManageProductWarehouseStorekeeper : System.Web.UI.Page
{
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
            Response.Redirect("LogOut.aspx");
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
            bool A116;
            A116 = Convert.ToBoolean(dtViewProfil.Rows[0]["A116"]);
            if (A116 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FArnProductShopWarehouseByStorekeeper();
        }
    }

    private void FArnProductShopWarehouseByStorekeeper()
    {
        try
        {
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = Convert.ToInt32(0);
            CPS.IDMosTafeed = Convert.ToInt32(0);
            CPS.IsRaeesMaglisAlEdarah = true;
            CPS.IsAmmenAlSondoq = true;
            CPS.IsModer = true;
            CPS.IsStorekeeper = false;
            CPS.IsDelete = false;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopWarehouseByCashier();
            if (dt.Rows.Count > 0)
            {
                GVWarehouseStorekeeper.DataSource = dt;
                GVWarehouseStorekeeper.DataBind();
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

        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductWarehouseStorekeeper.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVWarehouseStorekeeper.UseAccessibleHeader = true;
            GVWarehouseStorekeeper.HeaderRow.TableSection = TableRowSection.TableHeader;

            GVWarehouseStorekeeper.Columns[0].Visible = false;
            GVWarehouseStorekeeper.Columns[6].Visible = false;

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
            GVWarehouseStorekeeper.UseAccessibleHeader = false;
            foreach (GridViewRow row in GVWarehouseStorekeeper.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVWarehouseStorekeeper.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ProductShopWarehouse] SET [_IsStorekeeper] = @IsStorekeeper, _IsDone = @IsDone , _IsNotDone = @IsNotDone , _ExpiryDate = @ExpiryDate WHERE _billNumber = @billNumber And _IDMosTafeed = @IDMosTafeed And _IDNaebRaees = @IDNaebRaees";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@billNumber", 0);
                    cmd.Parameters.AddWithValue("@IDMosTafeed", 0);
                    cmd.Parameters.AddWithValue("@IDNaebRaees", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsStorekeeper", true);
                    cmd.Parameters.AddWithValue("@IsDone", true);
                    cmd.Parameters.AddWithValue("@IsNotDone", false);
                    cmd.Parameters.AddWithValue("@ExpiryDate", ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"));
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            System.Threading.Thread.Sleep(500);
            Response.Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception)
        {

        }
    }

    int Cou = 0;
    decimal sum = 0;
    protected void GVWarehouseStorekeeper_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
            sum += decimal.Parse(salary.Text);
            if (sum != 0)
                lblTotalPrice.Text = sum.ToString();
            else
                lblTotalPrice.Text = "بإنتظار التسعير";
        }
    }
    
    protected void btnNotAllow_Click(object sender, EventArgs e)
    {
        try
        {
            GVWarehouseStorekeeper.UseAccessibleHeader = false;
            foreach (GridViewRow row in GVWarehouseStorekeeper.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVWarehouseStorekeeper.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ProductShopWarehouse] SET [_IsStorekeeper] = @IsStorekeeper, _IsDone = @IsDone , _IsNotDone = @IsNotDone , _ExpiryDate = @ExpiryDate WHERE _billNumber = @billNumber And _IDMosTafeed = @IDMosTafeed And _IDNaebRaees = @IDNaebRaees";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@billNumber", 0);
                    cmd.Parameters.AddWithValue("@IDMosTafeed", 0);
                    cmd.Parameters.AddWithValue("@IDNaebRaees", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsStorekeeper", true);
                    cmd.Parameters.AddWithValue("@IsDone", false);
                    cmd.Parameters.AddWithValue("@IsNotDone", true);
                    cmd.Parameters.AddWithValue("@ExpiryDate", ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd"));
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            System.Threading.Thread.Sleep(500);
            Response.Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception)
        {

        }
    }

}