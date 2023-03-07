using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.OM.Models;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_OM_PageViewMulti : System.Web.UI.Page
{
    public string XNAmeServer = string.Empty, XMony = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        XNAmeServer = ClassSetting.FGetNameServer(); XMony = ClassSaddam.FGetMonySa();
        if (!IsPostBack)
        {
            if (Request.QueryString["Type"] != null)
            {
                if (Request.QueryString["Type"] == "Cashing")
                    FGetByBillMulti(Request.QueryString["IDUniq"], Request.QueryString["IDBill"]);
            }
        }
    }

    private void FGetByBillMulti(string XIDYear, string XIDBill)
    {
        try
        {
            Model_Cashing_ MC = new Model_Cashing_();
            MC.IDCheck = "GetByBillMulti";
            MC.ID_Item = new Guid(XIDYear);
            MC.bill_Number = 0;
            MC.ID_Donor = Guid.Empty;
            MC.FilterSearch = string.Empty;
            MC.Start_Date = string.Empty;
            MC.End_Date = string.Empty;
            MC.DataCheck = XIDBill;
            MC.DataCheck2 = string.Empty;
            MC.DataCheck3 = string.Empty;
            MC.DataCheck4 = string.Empty;
            MC.DataCheck5 = string.Empty;
            MC.DataCheck6 = string.Empty;
            MC.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Cashing_ RR = new Repostry_Cashing_();
            dt = RR.BOM_Cashing_Manage(MC);
            if (dt.Rows.Count > 0)
            {
                RPTCashing.DataSource = dt;
                RPTCashing.DataBind();
                pnlNullCashing.Visible = false;
                pnlDataCashing.Visible = true;
            }
            else
            {
                pnlNullCashing.Visible = true;
                pnlDataCashing.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    public string FConvertToWord(string XMony)
    {
        List<CurrencyInfo> currencies = new List<CurrencyInfo>();
        currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
        ToWord toWord = new ToWord(Convert.ToDecimal(XMony), currencies[Convert.ToInt32(0)]);
        return toWord.ConvertToArabic();
    }

}