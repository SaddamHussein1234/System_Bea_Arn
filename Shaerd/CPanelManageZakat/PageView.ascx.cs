using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
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

public partial class Shaerd_CPanelManageZakat_PageView : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SetData(string XIDYear, string XIDBill, string XIDProject)
    {
        FGetByBill_Bill(XIDYear, XIDBill, XIDProject);
    }

    private void FGetByBill_Bill(string XIDYear, string XIDBill, string XIDProject)
    {
        try
        {
            GVDeedDonationInKind.UseAccessibleHeader = false;

            Model_Warehouse_Zakat_Bill_ MWZB = new Model_Warehouse_Zakat_Bill_();
            MWZB.IDCheck = "GetByBill";
            MWZB.IDUniq = Guid.Empty;
            MWZB.ID_FinancialYear = new Guid(XIDYear);
            MWZB.bill_Number = Convert.ToInt64(XIDBill);
            MWZB.ID_Project = Convert.ToInt32(XIDProject);
            MWZB.Start_Date = string.Empty;
            MWZB.End_Date = string.Empty;
            MWZB.DateCheck = string.Empty;
            MWZB.DateCheck2 = string.Empty;
            MWZB.IsDelete = false;
            DataTable dt = new DataTable();
            Repostry_Warehouse_Zakat_Bill_ RWZB = new Repostry_Warehouse_Zakat_Bill_();
            dt = RWZB.BArn_Warehouse_Zakat_Bill_Manage(MWZB);
            if (dt.Rows.Count > 0)
            {
                string code = "http://chart.apis.google.com/chart?cht=qr&chl=" + ClassSetting.FGetNameServer() +
                "/ar/Zakat/PageView.aspx?IDYear=" + XIDYear + "&ID=" + XIDBill + "&IDP=" + XIDProject;
                Class_QRScan.FGetQRCode(code, ImgQRCode);
                Class_QRScan.FGetQRCode(code, ImgQRCode2);
                HFID.Value = dt.Rows[0]["_IDUniq"].ToString();                
                lblNumber.Text = dt.Rows[0]["_bill_Number_"].ToString();
                lblFromDonor.Text = dt.Rows[0]["_Name_Donor_"].ToString();
                lblFromDonorTow.Text = lblFromDonor.Text;
                if (dt.Rows[0]["_Phone_Donor_"].ToString() == string.Empty || dt.Rows[0]["_Phone_Donor_"].ToString() == "0")
                    lblPhoneDonor.Text = "رقم الجوال : " + "لم يحدد";
                else if (dt.Rows[0]["_Phone_Donor_"].ToString() != string.Empty && dt.Rows[0]["_Phone_Donor_"].ToString() != "0")
                    lblPhoneDonor.Text = "رقم الجوال : " + dt.Rows[0]["_Phone_Donor_"].ToString();
                FGetByBill(XIDYear, XIDBill, XIDProject);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;

                lblAmeenAlmosTodaa2.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDStorekeeper_"]));

                lblDateHide.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy/MM/dd") + "مـ - ";
                lblDateHide.Text += ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"])) + "هـ";

                lblDataEntry.Text = ClassAdmin_Arn.FGetNameByID(Convert.ToInt32(dt.Rows[0]["_CreatedBy_"]));
                lblDateEntry.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]));
                if (Convert.ToInt32(dt.Rows[0]["_ModifiedBy_"]) != 0)
                {
                    IDUpdate.Visible = true;
                    lblDataEntryEdit.Text = ClassQuaem.FAlBahethByEdit(Convert.ToInt32(dt.Rows[0]["_ModifiedBy_"]));
                    lblDateEntryEdit.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_ModifiedDate_"]));
                }
                else if (Convert.ToInt32(dt.Rows[0]["_ModifiedBy_"]) == 0)
                    IDUpdate.Visible = false;
                lblThe_Purpose.Text = dt.Rows[0]["_Note_Bill"].ToString();
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
                }
                else
                {
                    ImgAmeenAlmosTodaa.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgAmeenAlmosTodaa.Width = 30;
                    ImgAmeenAlmosTodaa.Visible = true;
                }

                //if (Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq_"]) && Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah_"]))
                //{
                //    IDKhatm.Visible = true;
                //

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblTotalPrice.Text), currencies[Convert.ToInt32(0)]);
                lblSumWord.Text = toWord.ConvertToArabic();
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

    public string XID()
    {
        return HFID.Value;
    }

    private void FGetByBill(string XIDYear, string XIDBill, string XIDProject)
    {
        GVDeedDonationInKind.UseAccessibleHeader = false;
        Model_Warehouse_Zakat_ MWZ = new Model_Warehouse_Zakat_();
        MWZ.IDCheck = "GetByBill";
        MWZ.ID_Item = 0;
        MWZ.ID_FinancialYear = new Guid(XIDYear);
        MWZ.bill_Number = Convert.ToInt32(XIDBill);
        MWZ.ID_Project = Convert.ToInt32(XIDProject);
        MWZ.Start_Date = string.Empty;
        MWZ.End_Date = string.Empty;
        MWZ.DateCheck = string.Empty;
        MWZ.IsDelete = false;
        Repostry_Warehouse_Zakat_ RWZ = new Repostry_Warehouse_Zakat_();
        DataTable dt = new DataTable();
        dt = RWZ.BArn_Warehouse_Zakat_Manage(MWZ);
        if (dt.Rows.Count > 0)
        {
            GVDeedDonationInKind.DataSource = dt;
            GVDeedDonationInKind.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
        }
    }

    int Cou = 0;
    decimal sum = 0;
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
    
    public void XView()
    {
        IDKhatm.Visible = true;
    }
    
    public void XHide()
    {
        IDKhatm.Visible = false;
    }

    public void FPrint()
    {
        try
        {
            lblType.Text = DLType.SelectedItem.ToString();
            lblType.Visible = true;
            DLType.Visible = false;
            GVDeedDonationInKind.UseAccessibleHeader = true;
            GVDeedDonationInKind.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["foot"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

}