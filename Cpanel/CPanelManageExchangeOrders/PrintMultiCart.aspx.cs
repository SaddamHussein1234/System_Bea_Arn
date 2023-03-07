using Library_CLS_Arn.ClassOutEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelManageExchangeOrders_PrintMultiCart : System.Web.UI.Page
{
    public string XCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
            FArnProductShopMatterOfExchangeByUser(Request.QueryString["IDBill"], Convert.ToInt32(Request.QueryString["XIDCate"]));

        //}
        //catch (Exception)
        //{

        //}
    }

    private void FArnProductShopMatterOfExchangeByUser(string IDBill, int IDProject)
    {

        ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
        CPS.FromDonor = IDBill;
        CPS.IsDelete = false;
        DataTable dt = new DataTable();
        dt = CPS.BArnProductShopMatterOfExchangeByMulti();
        if (dt.Rows.Count > 0)
        {
            //string code = ClassSetting.FGetNameServer() +
            //    "/Cpanel/CPanelManageExchangeOrders/PageManageProductAddThePriceToOrder.aspx?ID=" + txtSearch.Text.Trim() + "&XID="
            //    + IDMostafeed.ToString() + "&XIDCate=" + IDProject + "&IsCart=" + Cart.ToString() + "&IsDevice=" + Device.ToString()
            //    + "&IsTathith=" + Tathith.ToString() + "&IsTalef=" + Talef.ToString();
            //Class_QRScan.FGetQRCode(code, imgBarCode);

            RPTBillCart.DataSource = dt;
            RPTBillCart.DataBind();
            RPTBillCart.Visible = true;
        }
        else
            RPTBillCart.Visible = false;

    }

    protected void RPTBillCart_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

}