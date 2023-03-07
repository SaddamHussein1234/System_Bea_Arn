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

public partial class Shaerd_CPanelManageExchangeOrders_PageManageProductExchangeOrdersDetailsForDamaged : System.Web.UI.UserControl
{
    string XID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlSelect.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        GVExchangeOrders.UseAccessibleHeader = false;
        GVExchangeOrders.Columns[9].Visible = true;
        FArnProductShopForDamaged();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVExchangeOrders.UseAccessibleHeader = true;
            GVExchangeOrders.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVExchangeOrders.Columns[9].Visible = false;
            Session["footable1"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVExchangeOrders.UseAccessibleHeader = false;
        GVExchangeOrders.Columns[9].Visible = true;
        FArnProductShopForDamaged();
    }

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDFilter.Visible = true;
    }

    int Cou = 0;
    decimal sum = 0;
    int tempcounter = 0;
    protected void GVExchangeOrders_RowDataBound(object sender, GridViewRowEventArgs e)
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

                tempcounter = tempcounter + 1;
                if (tempcounter == 14)
                {
                    e.Row.Attributes.Add("style", "page-break-after: always;");
                    tempcounter = 0;
                }
            }
        }
        catch (Exception)
        {

        }
    }

    private void FArnProductShopForDamaged()
    {
        try
        {
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = Convert.ToInt32(0);
            CPS.IDMosTafeed = Convert.ToInt32(999999999);
            CPS.DateFrom = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
            CPS.DateTo = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
            CPS.IsDelete = false; CPS.IsCart = false; CPS.IsDevice = false; CPS.IsTathith = false; CPS.IsTalef = true;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopForDamaged();
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "قائمة فرز أوامر صرف تالف من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
                GVExchangeOrders.DataSource = dt;
                GVExchangeOrders.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                IDFilter.Visible = false;
                pnlSelect.Visible = false;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                IDFilter.Visible = true;
                pnlSelect.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

}