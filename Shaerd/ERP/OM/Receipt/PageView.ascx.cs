﻿using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.DataAccess;
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

public partial class Shaerd_ERP_OM_Receipt_PageView : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SetData(string XIDYear, string XIDBill)
    {
        FGetByBill(XIDYear, XIDBill);
    }

    private void FGetByBill(string XIDYear, string XIDBill)
    {
        try
        {
            Model_Receipt_ MR = new Model_Receipt_();
            MR.IDCheck = "GetByBill";
            MR.ID_Item = new Guid(XIDYear);
            MR.bill_Number = Convert.ToInt64(XIDBill);
            MR.ID_Donor = Guid.Empty;
            MR.FilterSearch = string.Empty;
            MR.Start_Date = string.Empty;
            MR.End_Date = string.Empty;
            MR.DataCheck = string.Empty;
            MR.DataCheck2 = string.Empty;
            MR.DataCheck3 = string.Empty;
            MR.DataCheck4 = string.Empty;
            MR.DataCheck5 = string.Empty;
            MR.DataCheck6 = string.Empty;
            MR.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Receipt_ RR = new Repostry_Receipt_();
            dt = RR.BOM_Receipt_Manage(MR);
            if (dt.Rows.Count > 0)
            {
                txtCoustmoer.Text = "وقع صورة طبق الأصل"; txtCoustmoer.Visible = true;
                string code = ClassSetting.FGetNameServer() +
                "/ar/Receipt/PageView.aspx?ID=" + XIDBill + "&IDUniq=" + XIDYear;
                Class_QRScan.FGetQRCode(code, ImgQRCode);
                Class_QRScan.FGetQRCode(code, ImgQRCode2);
                HFID.Value = dt.Rows[0]["_ID_Item_"].ToString();
                //ID_Edit.HRef = "PageAdd.aspx?ID=" + dt.Rows[0]["_ID_Item_"].ToString() + "";
                //ID_Edit.Visible = true;
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;

                lblNumber.Text = dt.Rows[0]["_bill_Number_"].ToString();
                if (dt.Rows[0]["_ID_Project_"].ToString() != "0")
                    lblNameProject.Text += " / لمشروع ( " + ClassQuaem.FSupportType(Convert.ToInt32(dt.Rows[0]["_ID_Project_"])) + " )";
                lblFromDonor.Text = dt.Rows[0]["_Name_"].ToString();
                lblFromDonorTow.Text = lblFromDonor.Text;
                string XPart = Repostry_Main_Items_.FGetNameByID(new Guid(dt.Rows[0]["_ID_Sub_Item_"].ToString()));
                if (dt.Rows[0]["_ID_Sub_Item_Tow_"].ToString() != Guid.Empty.ToString())
                    XPart += " / " + Repostry_Main_Items_.FGetNameByID(new Guid(dt.Rows[0]["_ID_Sub_Item_Tow_"].ToString()));
                if (dt.Rows[0]["_ID_Sub_Item_Three_"].ToString() != Guid.Empty.ToString())
                    XPart += " / " + Repostry_Main_Items_.FGetNameByID(new Guid(dt.Rows[0]["_ID_Sub_Item_Three_"].ToString()));
                lblProject.Text = "وذلك لغرض / " + dt.Rows[0]["_Name_Ar_"].ToString() + " / " + XPart;               
                lblThe_Purpose.Text = dt.Rows[0]["_Note_Bill_"].ToString();

                lblTotalPrice.Text = dt.Rows[0]["_The_Mony_"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["_IsCash_Money_"]))
                {
                    IDCash_Money.Visible = true;
                    IDShayk_Bank.Visible = false;
                    IDTransfer_On_Account.Visible = false;
                    CBCash_Money_.Checked = Convert.ToBoolean(dt.Rows[0]["_IsCash_Money_"]);
                }
                else if (Convert.ToBoolean(dt.Rows[0]["_IsShayk_Bank_"]))
                {
                    IDCash_Money.Visible = false;
                    IDShayk_Bank.Visible = true;
                    IDTransfer_On_Account.Visible = false;
                    CBShayk_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["_IsShayk_Bank_"]);
                    lblNumber_Shayk_Bank.Text = " / رقم الشيك : " + dt.Rows[0]["_Number_Shayk_Bank_"].ToString();
                    lblDate_Shayk.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Shayk_"]).ToString("yyyy/MM/dd");
                    lblFor_Bank.Text = dt.Rows[0]["_For_Bank_"].ToString();
                }
                else if (Convert.ToBoolean(dt.Rows[0]["_Transfer_On_Account_"]))
                {
                    IDCash_Money.Visible = false;
                    IDShayk_Bank.Visible = false;
                    IDTransfer_On_Account.Visible = true;
                    CBTransfer_On_Account.Checked = Convert.ToBoolean(dt.Rows[0]["_Transfer_On_Account_"]);
                    lblNumber_Account.Text = " / حساب رقم : " + dt.Rows[0]["_Number_Account_"].ToString();
                    lblDate_Bank_Transfer.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Bank_Transfer_"]).ToString("yyyy/MM/dd");
                    lblFor_Bank_Transfer.Text = dt.Rows[0]["_For_Bank_Transfer_"].ToString();
                }
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

                lblRaeesMaglis.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDRaeesMaglisAlEdarah_"]));
                if (Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah_"]))
                {
                    lblRaeesMaglisAllowDate.Text = Convert.ToDateTime(dt.Rows[0]["_IDRaees_Date_Allow_"]).ToString("yyyy/MM/dd");
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

                lblAmeenAlsondoq.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDAmmenAlSondoq_"]));
                if (Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq_"]))
                {
                    lblAmeenAlsondoqAllowDate.Text = Convert.ToDateTime(dt.Rows[0]["_IDAmmen_Date_Allow_"]).ToString("yyyy/MM/dd");
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

                lblModer.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_ID_Moder_"]));
                if (Convert.ToBoolean(dt.Rows[0]["_Is_Moder_Allow_"]))
                {
                    lblModerAllowDate.Text = Convert.ToDateTime(dt.Rows[0]["_ID_Moder_Date_Allow_"]).ToString("yyyy/MM/dd");
                    ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Moder_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Moder_Allow_"]));
                    ImgModer.Width = 100;
                    ImgModer.Visible = true;
                }
                else
                {
                    ImgModer.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgModer.Width = 30;
                    ImgModer.Visible = true;
                }

                if (Convert.ToBoolean(dt.Rows[0]["_Is_Moder_Allow_"]) && Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq_"]))
                { IDKhatm.Visible = true; IDKhatmLodding.Visible = false; }
                else
                { IDKhatm.Visible = false; IDKhatmLodding.Visible = true; }
                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblTotalPrice.Text), currencies[Convert.ToInt32(0)]);
                lblSumWord.Text = toWord.ConvertToArabic();
                
            }
            else
            {
                //ID_Edit.Visible = false;
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

    public void FPrint()
    {
        try
        {
            txtCoustmoer.Text = string.Empty;
            txtCoustmoer.Visible = false;
            lblType.Text = DLType.SelectedItem.ToString();
            lblType.Visible = true;
            DLType.Visible = false;
            Session["foot"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA5.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

}