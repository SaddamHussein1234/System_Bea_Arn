using Library_CLS_Arn.ClassOutEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageManageProductWarehousebyProductsCloseToCompletion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FArnProductShopWarehouseByProductGet();
        }
    }

    private void FArnProductShopWarehouseByProductGet()
    {
        ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
        CPS.billNumber = Convert.ToInt32(0);
        CPS.IDMosTafeed = Convert.ToInt32(0);
        CPS.IDProduct = Convert.ToInt64(Request.QueryString["XID"]);
        CPS.IDUniq = string.Empty; // txtSearch.Text.Trim();
        CPS.IsDelete = false;
        DataTable dt = new DataTable();
        dt = CPS.BArnProductShopWarehouseByProductGet();
        if (dt.Rows.Count > 0)
        {
            GVProductGet.DataSource = dt;
            GVProductGet.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            txtTitle.Text = " قائمة تفاصيل العمليات التي حصلت لـ " + dt.Rows[0]["CategoryName"].ToString() + " - " + dt.Rows[0]["ProductName"].ToString() + "," + " الكمية المتبقية " + Convert.ToString(Convert.ToInt64(lblSum.Text));
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
        }

    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    
    protected void GVProductGet_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

}