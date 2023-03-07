using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.DataAccess;
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

public partial class Shaerd_ERP_WSM_PageShipping_PageView : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A131");
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            txtSearch.Text = Request.QueryString["ID"];
            ddlYears.SelectedValue = Request.QueryString["IDUniq"];
            txtSearch.Focus();
            if (txtSearch.Text.Trim() != string.Empty)
                FGetByBill();
        }
    }

    private void FGetByBill()
    {
        try
        {
            GVProductShopWarehouseByID.UseAccessibleHeader = false;
            WSM_Model_In_Kind_Donation_Bill_ MIKDB = new WSM_Model_In_Kind_Donation_Bill_();
            MIKDB.IDCheck = "GetByBill";
            MIKDB.ID_Item = new Guid(ddlYears.SelectedValue);
            MIKDB.bill_Number = Convert.ToInt64(txtSearch.Text.Trim());
            MIKDB.ID_Donor = Guid.Empty;
            MIKDB.Start_Date = string.Empty;
            MIKDB.End_Date = string.Empty;
            MIKDB.DateCheck = string.Empty;
            MIKDB.IsActive = true;
            DataTable dt = new DataTable();
            WSM_Repostry_In_Kind_Donation_Bill_ RIKDB = new WSM_Repostry_In_Kind_Donation_Bill_();
            dt = RIKDB.BWSM_In_Kind_Donation_Bill_Manage(MIKDB);
            if (dt.Rows.Count > 0)
            {
                txtCoustmoer.Text = "وقع صورة طبق الأصل";
                string code = ClassSetting.FGetNameServer() +
                "/Cpanel/ERP/WSM/PageShipping/PageView.aspx?ID=" + txtSearch.Text.Trim();
                Class_QRScan.FGetQRCode(code, ImgQRCode);
                Class_QRScan.FGetQRCode(code, ImgQRCode2);
                ID_Edit_.HRef = "/Cpanel/ERP/WSM/PageShipping/PageAdd.aspx?ID=" + dt.Rows[0]["_ID_Item_"].ToString(); ID_Edit_.Visible = true;
                FGetByBillDetails(new Guid(dt.Rows[0]["_ID_Item_"].ToString()));

                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;

                lblAmeenAlmosTodaa2.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDStorekeeper_"]));
                lblNumber.Text = dt.Rows[0]["_bill_Number_"].ToString();

                lblDateHide.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy/MM/dd") + "مـ - ";
                lblDateHide.Text += ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"])) + "هـ";

                lblDataEntry.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_CreatedBy_"]));
                lblDateEntry.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]));
                if (Convert.ToInt32(dt.Rows[0]["_ModifiedBy_"]) != 0)
                {
                    IDUpdate.Visible = true;
                    lblDataEntryEdit.Text = ClassQuaem.FAlBahethByEdit(Convert.ToInt32(dt.Rows[0]["_ModifiedBy_"]));
                    lblDateEntryEdit.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_ModifiedDate_"]));
                }
                else if (Convert.ToInt32(dt.Rows[0]["_ModifiedBy_"]) == 0)
                    IDUpdate.Visible = false;
                lblAmeenAlsondoq.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDAmmenAlSondoq_"]));
                if (Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq_"]))
                {
                    ImgAmeenAlsondoq.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDAmmenAlSondoq_"]), Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq_"]));
                    ImgAmeenAlsondoq.Width = 100;
                    ImgAmeenAlsondoq.Visible = true;
                }
                else
                {
                    ImgAmeenAlsondoq.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgAmeenAlsondoq.Width = 30;
                    ImgAmeenAlsondoq.Visible = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah_"]))
                {
                    ImgRaees.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDRaeesMaglisAlEdarah_"]), Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah_"]));
                    ImgRaees.Width = 100;
                    ImgRaees.Visible = true;
                }
                else
                {
                    ImgRaees.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgRaees.Width = 30;
                    ImgRaees.Visible = true;
                }
                lblModer.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDModer_"]));
                if (Convert.ToBoolean(dt.Rows[0]["_IsModer_"]))
                {
                    ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDModer_"]), Convert.ToBoolean(dt.Rows[0]["_IsModer_"]));
                    ImgModer.Width = 100;
                    ImgModer.Visible = true;
                }
                else
                {
                    ImgModer.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgModer.Width = 30;
                    ImgModer.Visible = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper_"]))
                {
                    ImgAmeenAlmosTodaa.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDStorekeeper_"]), Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper_"]));
                    ImgAmeenAlmosTodaa.Width = 100;
                    ImgAmeenAlmosTodaa.Visible = true;
                    //lblDateAllow.Text = Convert.ToDateTime(dt.Rows[0]["_IDStorekeeper_Date_Allow_"]).ToString("ddd , dd-MM-yyyy");
                }
                else
                {
                    ImgAmeenAlmosTodaa.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgAmeenAlmosTodaa.Width = 30;
                    ImgAmeenAlmosTodaa.Visible = true;
                }

                if (Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq_"]) && Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah_"]))
                    IDKhatm.Visible = true;
                else
                    IDKhatm.Visible = false;

                lblFromDonor.Text = Repostry_Company_.FCRM_Company_Manage(new Guid(dt.Rows[0]["_ID_Donor_"].ToString()));
                lblFromDonorTow.Text = lblFromDonor.Text;
                lblThe_Purpose.Text = dt.Rows[0]["_Note_Bill"].ToString();

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblTotalPrice.Text), currencies[Convert.ToInt32(0)]);
                lblSumWord.Text = toWord.ConvertToArabic();
            }
            else
            {
                ID_Edit_.Visible = false;
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

    private void FGetByBillDetails(Guid IDBill)
    {
        WSM_Model_In_Kind_Donation_Details_ MIKDD = new WSM_Model_In_Kind_Donation_Details_();
        MIKDD.IDCheck = "GetByBillByIDBill";
        MIKDD.ID_Item = IDBill;
        MIKDD.bill_Number = 0;
        MIKDD.Start_Date = string.Empty;
        MIKDD.End_Date = string.Empty;
        MIKDD.DataCheck = string.Empty;
        MIKDD.DataCheck2 = string.Empty;
        MIKDD.DataCheck3 = string.Empty;
        MIKDD.IsActive = true;
        DataTable dt = new DataTable();
        WSM_Repostry_In_Kind_Donation_Details_ RIKDD = new WSM_Repostry_In_Kind_Donation_Details_();
        dt = RIKDD.BWSM_In_Kind_Donation_Details_Manage(MIKDD);
        if (dt.Rows.Count > 0)
        {
            GVProductShopWarehouseByID.DataSource = dt;
            GVProductShopWarehouseByID.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblType.Visible = false;
        DLType.Visible = true;
        FGetByBill();
        System.Threading.Thread.Sleep(200);
    }

    protected void LbRefreshSaraf_Click(object sender, EventArgs e)
    {
        lblType.Visible = false;
        DLType.Visible = true;
        FGetByBill();
    }

    protected void LBPrintSaraf_Click(object sender, EventArgs e)
    {
        try
        {
            //txtCoustmoer.Text = string.Empty;
            lblType.Text = DLType.SelectedItem.ToString();
            lblType.Visible = true;
            DLType.Visible = false;
            GVProductShopWarehouseByID.UseAccessibleHeader = true;
            GVProductShopWarehouseByID.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["foot"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {

        }
    }

    int Cou = 0;
    decimal sum = 0;
    float Getsum, Setsum = 0;
    protected void GVProductShopWarehouseByID_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Count = (Label)e.Row.FindControl("lblCount");//take lable id
                Cou += int.Parse(Count.Text);
                lblSum.Text = Cou.ToString();

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