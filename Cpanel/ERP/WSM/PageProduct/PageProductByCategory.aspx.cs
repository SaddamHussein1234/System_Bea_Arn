using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.WSM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_WSM_PageProduct_PageProductByCategory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A66", "A113", btnDelete1, GVProductByCategory, 0, 12);
            FGetCategoryShop();
            pnlSelect.Visible = true;
        }
    }

    private void FGetCategoryShop()
    {
        DataTable dt = new DataTable();
        dt = WSM_Data_Access_Layer.GetData("SELECT * FROM [dbo].[CategoryShop] With(NoLock) Where IsActive = @0 And IsDelete = @1 Order by IDNumberCategory", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLCategory.Items.Clear();
            DLCategory.Items.Add("");
            DLCategory.AppendDataBoundItems = true;
            DLCategory.DataValueField = "CategoryID";
            DLCategory.DataTextField = "CategoryName";
            DLCategory.DataSource = dt;
            DLCategory.DataBind();
        }
    }

    private void FGetProductShopByCategory()
    {
        try
        {
            GVProductByCategory.UseAccessibleHeader = false;
            WSM_ClassProduct CP = new WSM_ClassProduct();
            CP._IDCategoryShop = Convert.ToInt64(DLCategory.SelectedValue);
            CP._IDUniq = txtSearch.Text.Trim();
            CP._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CP.BArnProductShopByCategory();
            if (dt.Rows.Count > 0)
            {
                GVProductByCategory.DataSource = dt;
                GVProductByCategory.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                GVProductByCategory.Columns[0].Visible = true;
                GVProductByCategory.Columns[12].Visible = true;
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
        Response.Redirect("PageProductByCategory.aspx");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVProductByCategory.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVProductByCategory.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_WSM_);
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
            GVProductByCategory.Columns[0].Visible = false;
            GVProductByCategory.Columns[12].Visible = false;

            GVProductByCategory.UseAccessibleHeader = true;
            GVProductByCategory.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            if (GVProductByCategory.Rows.Count > 25)
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable2.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
            else if (GVProductByCategory.Rows.Count <= 25)
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");

        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (DLCategory.Text != string.Empty)
        {
            lblCategory.Visible = false;
            txtTitle.Text = "قائمة المنتجات حسب " + DLCategory.SelectedItem.ToString();
            FGetProductShopByCategory();
            CLS_Permissions.CheckAccountAdmin("A66", "A113", btnDelete1, GVProductByCategory, 0, 12);
            System.Threading.Thread.Sleep(500);
        }
        else if (DLCategory.Text == string.Empty)
            lblCategory.Visible = true;
    }

}