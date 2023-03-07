using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
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

public partial class Cpanel_ERP_WSM_DamagedExchange_PageView : System.Web.UI.Page
{
    public string XMony = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        XMony = ClassSaddam.FGetMonySa();
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A61", "A106", btnDeleteTaleef, GVMatterOfExchangeByIDTaleef, 0, 0);
            GVMatterOfExchangeByIDTaleef.Columns[0].Visible = false;
            btnDeleteTaleef.Visible = false;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            pnlStar.Visible = true;
            FGetSupportType();
            FCheckURL();
        }
    }

    private void FGetSupportType()
    {
        ClassQuaem.FGetSupportType(0, "'6'", DLSupportType);
    }

    private void FCheckURL()
    {
        try
        {
            if (Request.QueryString["ID"] != null)
            {
                ddlYears.SelectedValue = Convert.ToString(Request.QueryString["IDUniq"]);
                DLSupportType.SelectedValue = Convert.ToString(Request.QueryString["XIDCate"]);

                txtSearchTalef.Text = Request.QueryString["ID"].ToString();

                pnlStar.Visible = false;
                ProductByTalef.Visible = true;
                pnlDataTalef.Visible = false;
                pnlNullTalef.Visible = true;
                txtSearchTalef.Focus();
                FWSM_Exchange_Order_Bill_ManageByBill(new Guid(ddlYears.SelectedValue), Convert.ToInt32(txtSearchTalef.Text.Trim()), Convert.ToInt32(DLSupportType.SelectedValue), false, false, false, true);
            }
        }
        catch (Exception)
        {

        }
    }

    private void FWSM_Exchange_Order_Bill_ManageByBill(Guid XYear, int IDBill, int IDProject, bool Cart, bool Device, bool Tathith, bool Talef)
    {
        try
        {
            WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_();
            MEOB.IDCheck = "GetByBill";
            MEOB.ID_Item = Guid.Empty;
            MEOB.ID_FinancialYear = XYear;
            MEOB.ID_Donor = Guid.Empty;
            MEOB.bill_Number = IDBill;
            MEOB.ID_MosTafeed = IDProject;
            MEOB.Start_Date = string.Empty;
            MEOB.End_Date = string.Empty;
            MEOB.DataCheck = string.Empty;
            MEOB.DataCheck2 = string.Empty;
            MEOB.DataCheck3 = string.Empty;
            MEOB.Is_Cart = Cart;
            MEOB.Is_Device = Device;
            MEOB.Is_Tathith = Tathith;
            MEOB.Is_Talef = Talef;
            MEOB.IsActive = true;
            DataTable dt = new DataTable();
            WSM_Repostry_Exchange_Order_Bill_ REOB = new WSM_Repostry_Exchange_Order_Bill_();
            dt = REOB.BWSM_Exchange_Order_Bill_Manage(MEOB);

            if (dt.Rows.Count > 0)
            {
                IDEdit.HRef = "PageMatterOfExchangeForDamaged.aspx?ID=" + dt.Rows[0]["_ID_Item_"].ToString();
                string code = ClassSetting.FGetNameServer() +
                   "/Cpanel/ERP/WSM/DamagedExchange/PageView.aspx?IDUniq=" + XYear.ToString() + "&ID=" + IDBill.ToString() + "&XID=" + dt.Rows[0]["_ID_MosTafeed_"].ToString() +
                   "&XIDCate=" + IDProject.ToString() + "&IsCart=" + Cart.ToString() + "&IsDevice=" + Device.ToString() + "&IsTathith=" + Tathith.ToString() + "&IsTalef=" + Talef.ToString();
                Class_QRScan.FGetQRCode(code, ImgQRCode);
                Class_QRScan.FGetQRCode(code, ImgQRCode2);

                lblToday.Text = ClassSaddam.GetCurrentTime().ToString("ddd");
                lblDateToDay.Text = ClassSaddam.GetCurrentTime().ToString("dd/MM/yyyy");

                pnlNullTalef.Visible = false;
                pnlDataTalef.Visible = true;
                ProductByTalef.Visible = true;
                lblNumberTaleef.Text = " " + dt.Rows[0]["_bill_Number_"].ToString();

                lblDataEntryTaleef.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_CreatedBy_"]));
                lblDateEntryTaleef.Text = ClassSaddam.FChangeDate(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]));

                lblDateHideTaleef.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy/MM/dd") + "مـ - ";
                lblDateHideTaleef.Text += ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"])) + "هـ";
                //txtTitleTalef.Text = ClassSaddam.FAlTypeEvint(Convert.ToInt32(dt.Rows[0]["_IDType"])) + "(المنتج " + ClassQuaem.FSupportType(Convert.ToInt32(dt.Rows[0]["_IDCategory"])) + ")";
                txtTitleTalef.Text = "عقد حصر وإتلاف";
                if (Convert.ToBoolean(dt.Rows[0]["_Is_Raees_Maglis_AlEdarah_"]) && Convert.ToBoolean(dt.Rows[0]["_Is_Naeb_Raees_"]) && Convert.ToBoolean(dt.Rows[0]["_Is_Storekeeper_"]))
                {
                    IDKhatmTaleef.Visible = true;
                }

                lblRaees.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_ID_Raees_Maglis_AlEdarah_"]));
                lblNaeeb.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_ID_Naeb_Raees_"]));
                lblAmeen.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_ID_Storekeeper_"]));
                if (Convert.ToBoolean(dt.Rows[0]["_Is_Raees_Maglis_AlEdarah_"]))
                {
                    IDRaees.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Raees_Maglis_AlEdarah_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Raees_Maglis_AlEdarah_"]));
                    IDRaees.Width = 100;
                    IDRaees.Visible = true;
                }
                else
                {
                    IDRaees.ImageUrl = "/Cpanel/loaderMin.gif";
                    IDRaees.Width = 30;
                    IDRaees.Visible = true;
                }

                if (Convert.ToBoolean(dt.Rows[0]["_Is_Naeb_Raees_"]))
                {
                    IDNeeb.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Naeb_Raees_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Naeb_Raees_"]));
                    IDNeeb.Width = 100;
                    IDNeeb.Visible = true;
                }
                else
                {
                    IDNeeb.ImageUrl = "/Cpanel/loaderMin.gif";
                    IDNeeb.Width = 30;
                    IDNeeb.Visible = true;
                }

                if (Convert.ToBoolean(dt.Rows[0]["_Is_Storekeeper_"]))
                {
                    IDAmeen.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Storekeeper_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Storekeeper_"]));
                    IDAmeen.Width = 100;
                    IDAmeen.Visible = true;
                }
                else
                {
                    IDAmeen.ImageUrl = "/Cpanel/loaderMin.gif";
                    IDAmeen.Width = 30;
                    IDAmeen.Visible = true;
                }
                pnlStar.Visible = false;
                pnlDataTalef.Visible = true;
                FGetByBill(new Guid(dt.Rows[0]["_ID_Item_"].ToString()));
            }
            else
            {
                pnlNullTalef.Visible = true;
                pnlDataTalef.Visible = false;
                ProductByTalef.Visible = true;
                pnlStar.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetByBill(Guid XIDBill)
    {
        GVMatterOfExchangeByIDTaleef.UseAccessibleHeader = false;

        WSM_Model_Exchange_Order_Details_ MEOD = new WSM_Model_Exchange_Order_Details_();
        MEOD.IDCheck = "GetByBill";
        MEOD.IDItem = XIDBill;
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
            GVMatterOfExchangeByIDTaleef.DataSource = dt;
            GVMatterOfExchangeByIDTaleef.DataBind();
            lblCountTaleef.Text = Convert.ToString(dt.Rows.Count);

            List<CurrencyInfo> currencies = new List<CurrencyInfo>();
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            ToWord toWord = new ToWord(Convert.ToDecimal(lblTotalPriceTaleef.Text), currencies[Convert.ToInt32(0)]);
            lblSumTalef.Text = toWord.ConvertToArabic();
        }
    }

    protected void btnSearchTalef_Click(object sender, EventArgs e)
    {
        FWSM_Exchange_Order_Bill_ManageByBill(new Guid(ddlYears.SelectedValue), Convert.ToInt32(txtSearchTalef.Text.Trim()), Convert.ToInt32(DLSupportType.SelectedValue), false, false, false, true);
    }

    protected void LBRefresh_Click(object sender, EventArgs e)
    {
        FWSM_Exchange_Order_Bill_ManageByBill(new Guid(ddlYears.SelectedValue), Convert.ToInt32(txtSearchTalef.Text.Trim()), Convert.ToInt32(DLSupportType.SelectedValue), false, false, false, true);
        lblDateHideTaleef.Visible = false;
    }

    int CouTaleef = 0;
    decimal sumTaleef = 0;
    float GetsumTaleef, SetsumTaleef = 0;
    protected void GVMatterOfExchangeByIDTaleef_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Count = (Label)e.Row.FindControl("lblCount");//take lable id
                CouTaleef += int.Parse(Count.Text);
                lblSumTaleef.Text = CouTaleef.ToString();

                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sumTaleef += decimal.Parse(salary.Text);
                if (sumTaleef != 0)
                    lblTotalPriceTaleef.Text = sumTaleef.ToString();
                else
                    lblTotalPriceTaleef.Text = "بإنتظار التسعير";
                lblMony.Text = ClassSaddam.FGetMonySa();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void LBPrintSaraf_Click(object sender, EventArgs e)
    {
        try
        {
            GVMatterOfExchangeByIDTaleef.UseAccessibleHeader = true;
            GVMatterOfExchangeByIDTaleef.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["foot"] = pnlDataTalef;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

}