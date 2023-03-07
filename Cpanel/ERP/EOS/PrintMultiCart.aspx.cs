using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using Library_CLS_Arn.WSM.Models;
using Library_CLS_Arn.WSM.Repostry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_EOS_PrintMultiCart : System.Web.UI.Page
{
    public string XNAmeServer = string.Empty, XMony = string.Empty, XCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            XNAmeServer = ClassSetting.FGetNameServer(); XMony = ClassSaddam.FGetMonySa();
            if (Request.QueryString["Type"] == "Cart")
                FSetBill(new Guid(Request.QueryString["IDUniq"]), Request.QueryString["IDBill"], Convert.ToInt32(Request.QueryString["XIDCate"]));
            
        }
        catch (Exception)
        {

        }
    }

    private void FSetBill(Guid XYear, string IDBill, int IDProject)
    {
        WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_();
        MEOB.IDCheck = "GetByBillMultiCartWithShring";
        MEOB.ID_Item = Guid.Empty;
        MEOB.ID_FinancialYear = XYear;
        MEOB.ID_Donor = Guid.Empty;
        MEOB.bill_Number = 0;
        MEOB.ID_MosTafeed = IDProject;
        MEOB.Start_Date = string.Empty;
        MEOB.End_Date = string.Empty;
        MEOB.DataCheck = IDBill;
        MEOB.DataCheck2 = string.Empty;
        MEOB.DataCheck3 = string.Empty;
        MEOB.Is_Cart = false;
        MEOB.Is_Device = false;
        MEOB.Is_Tathith = false;
        MEOB.Is_Talef = false;
        MEOB.IsActive = true;
        DataTable dt = new DataTable();
        WSM_Repostry_Exchange_Order_Bill_ REOB = new WSM_Repostry_Exchange_Order_Bill_();
        dt = REOB.BWSM_Exchange_Order_Bill_Manage(MEOB);
        if (dt.Rows.Count > 0)
        {
            RPTBillCart.DataSource = dt;
            RPTBillCart.DataBind();
            RPTBillCart.Visible = true;
        }
        else
            RPTBillCart.Visible = false;
    }

    protected void RPTBillCart_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string _XID = (e.Item.FindControl("HFXID") as HiddenField).Value;

                GridView GVDeedDonation = e.Item.FindControl("GVDeedDonationInKind") as GridView;

                WSM_Model_Exchange_Order_Details_ MEOD = new WSM_Model_Exchange_Order_Details_();
                MEOD.IDCheck = "GetByBill";
                MEOD.IDItem = new Guid(_XID);
                MEOD.ID_FinancialYear = Guid.Empty;
                MEOD.ID_Donor = Guid.Empty;
                MEOD.bill_Number = 0;
                MEOD.ID_MosTafeed = 0;
                MEOD.Start_Date = string.Empty;
                MEOD.End_Date = string.Empty;
                MEOD.DataCheck = string.Empty;
                MEOD.DataCheck2 = string.Empty;
                MEOD.DataCheck3 = string.Empty;
                MEOD.IsActive = true;
                DataTable dt = new DataTable();
                WSM_Repostry_Exchange_Order_Details_ REOD = new WSM_Repostry_Exchange_Order_Details_();
                dt = REOD.BWSM_Exchange_Order_Details_Manage(MEOD);
                if (dt.Rows.Count > 0)
                {
                    GVDeedDonation.DataSource = dt;
                    GVDeedDonation.DataBind();
                    Label XlblTotalPriceX = (Label)e.Item.FindControl("lblTotalPrice");
                    XlblTotalPriceX.Text = dt.Compute("Sum(_Total_Price_)", string.Empty).ToString();

                    TextBox XtxtSumWordX = (TextBox)e.Item.FindControl("txtSumWord");
                    List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                    currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                    ToWord toWord = new ToWord(Convert.ToDecimal(XlblTotalPriceX.Text), currencies[Convert.ToInt32(0)]);
                    XtxtSumWordX.Text = toWord.ConvertToArabic();
                }

            }
        }
        catch (Exception)
        {

        }
    }

}