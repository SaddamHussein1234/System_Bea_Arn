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

public partial class Cpanel_CPanelManageZakat_PageManageProductWarehouseChairmanOfTheBoard : System.Web.UI.Page
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
            bool A141;
            A141 = Convert.ToBoolean(dtViewProfil.Rows[0]["A141"]);
            if (A141 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FArnWarehouse_Zakat_Bill_ByCashier();
        }
    }

    private void FArnWarehouse_Zakat_Bill_ByCashier()
    {
        try
        {
            GVWarehouseCashier.Columns[0].Visible = true;
            GVWarehouseCashier.Columns[9].Visible = true;

            ClassWarehouse_Zakat_Bill CWZB = new ClassWarehouse_Zakat_Bill();
            CWZB.IDCheck = 0;
            CWZB.Name_Donor = string.Empty;
            CWZB.DateFrom = string.Empty;
            CWZB.DateTo = string.Empty;
            CWZB.IsModer = true;
            CWZB.IsRaeesMaglisAlEdarah = false;
            CWZB.IsAmmenAlSondoq = true;
            CWZB.IsStorekeeper = false;
            CWZB.IsDelete = false;
            DataTable dt = new DataTable();
            dt = CWZB.BArnWarehouse_Zakat_Bill_ByCashier();
            if (dt.Rows.Count > 0)
            {
                GVWarehouseCashier.DataSource = dt;
                GVWarehouseCashier.DataBind();
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductWarehouseChairmanOfTheBoard.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {

            GVWarehouseCashier.Columns[0].Visible = false;
            GVWarehouseCashier.Columns[9].Visible = false;

            GVWarehouseCashier.UseAccessibleHeader = true;
            GVWarehouseCashier.HeaderRow.TableSection = TableRowSection.TableHeader;

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
            GVWarehouseCashier.UseAccessibleHeader = false;
            foreach (GridViewRow row in GVWarehouseCashier.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVWarehouseCashier.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_Warehouse_Zakat_Bill] SET [_IsRaeesMaglisAlEdarah_] = @IsRaeesMaglisAlEdarah WHERE _ID_Item = @ID_Item";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID_Item", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsRaeesMaglisAlEdarah", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            System.Threading.Thread.Sleep(100);
            IDMessageSuccess.Visible = true;
            FArnWarehouse_Zakat_Bill_ByCashier();
        }
        catch (Exception)
        {
            return;
        }
    }

    int Cou = 0;
    decimal sum = 0;
    protected void GVWarehouseCashier_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
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
        catch (Exception)
        {

        }
    }

}