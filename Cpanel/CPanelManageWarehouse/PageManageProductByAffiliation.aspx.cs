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

public partial class Cpanel_CPanelManageWarehouse_PageManageProductByAffiliation : System.Web.UI.Page
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
            bool A65, A112;
            A65 = Convert.ToBoolean(dtViewProfil.Rows[0]["A65"]);
            A112 = Convert.ToBoolean(dtViewProfil.Rows[0]["A112"]);
            if (A65 == false)
                Response.Redirect("PageNotAccess.aspx");
            if (A112 == false)
            {
                IDAdd.Visible = false;
                btnDelete1.Visible = false;
                GVProductByAffiliation.Columns[0].Visible = false;
                GVProductByAffiliation.Columns[11].Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetAffiliationShop();
            pnlSelect.Visible = true;
        }
    }

    private void FGetAffiliationShop()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[AffiliationShop] With(NoLock) Where IsActive = @0 And IsDelete = @1 Order by IDNumberAffiliation", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAffiliation.Items.Clear();
            DLAffiliation.Items.Add("");
            DLAffiliation.AppendDataBoundItems = true;
            DLAffiliation.DataValueField = "AffiliationID";
            DLAffiliation.DataTextField = "AffiliationName";
            DLAffiliation.DataSource = dt;
            DLAffiliation.DataBind();
        }
    }

    private void FGetProductShopByAffiliation()
    {
        try
        {
            GVProductByAffiliation.UseAccessibleHeader = false;
            ClassProduct CP = new ClassProduct();
            CP._IDAffiliationShop = Convert.ToInt64(DLAffiliation.SelectedValue);
            CP._IDUniq = txtSearch.Text.Trim();
            CP._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CP.BArnProductShopByAffiliation();
            if (dt.Rows.Count > 0)
            {
                GVProductByAffiliation.DataSource = dt;
                GVProductByAffiliation.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                GVProductByAffiliation.Columns[0].Visible = true;
                GVProductByAffiliation.Columns[11].Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                pnlSelect.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }
    
    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductByAffiliation.aspx");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVProductByAffiliation.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVProductByAffiliation.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ProductShop] SET [IsDelete] = @IsDelete WHERE ProductID = @ProductID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ProductID", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            Response.Redirect(Request.Url.PathAndQuery);
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
            GVProductByAffiliation.Columns[0].Visible = false;
            GVProductByAffiliation.Columns[11].Visible = false;

            GVProductByAffiliation.UseAccessibleHeader = true;
            GVProductByAffiliation.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");

        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        if (DLAffiliation.Text != string.Empty)
        {
            lblAffiliation.Visible = false;
            System.Threading.Thread.Sleep(500);
            txtTitle.Text = "قائمة المنتجات حسب إنتماء " + DLAffiliation.SelectedItem.ToString();
            FGetProductShopByAffiliation();
            CheckAccountAdmin();
        }
        else if (DLAffiliation.Text == string.Empty)
        {
            lblAffiliation.Visible = true;
        }
    }

}