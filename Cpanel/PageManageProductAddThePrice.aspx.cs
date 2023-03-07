using Library_CLS_Arn.ClassOutEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageManageProductAddThePrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FArnProductShopAddThePrice();
        }
    }

    private void FArnProductShopAddThePrice()
    {
        try
        {
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = Convert.ToInt32(0);
            CPS.IDMosTafeed = Convert.ToInt32(0);
            CPS.TotalPrice = Convert.ToInt64(0);
            CPS.IsDelete = false;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopAddThePrice();
            if (dt.Rows.Count > 0)
            {
                GVAddThePrice.DataSource = dt;
                GVAddThePrice.DataBind();
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
        Response.Redirect("PageManageProductAddThePrice.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        GVAddThePrice.Columns[8].Visible = false;
        Session["footable1"] = pnlData;
        if (GVAddThePrice.Rows.Count > 15)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable2.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        else if (GVAddThePrice.Rows.Count <= 15)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
    }

}