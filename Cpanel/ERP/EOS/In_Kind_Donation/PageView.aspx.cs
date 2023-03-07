using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ClassOutEntity;
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

public partial class Cpanel_ERP_EOS_In_Kind_Donation_PageView : System.Web.UI.Page
{
    public string XMony = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        XMony = ClassSaddam.FGetMonySa();
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A61", "A106", btnDelete1, GVDeedDonationInKind, 0, 0);
            GVDeedDonationInKind.Columns[0].Visible = false;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            pnlStar.Visible = true;
            FGetProjec();
            FGetSupportType();
            FCheckURL();
        }
    }

    private void FCheckURL()
    {
        try
        {
            ddlYears.SelectedValue = Convert.ToString(Request.QueryString["IDUniq"]);
            if (Request.QueryString["ID"] != null)
            {
                if (Request.QueryString["XID"] != null)
                {
                    if (Request.QueryString["ID"] != null)
                    {
                        txtSearch.Text = Request.QueryString["ID"].ToString();
                        txtSearchTalef.Text = Request.QueryString["ID"].ToString();
                        RBCart.Checked = Convert.ToBoolean(Request.QueryString["IsCart"]);
                        RBDevice.Checked = Convert.ToBoolean(Request.QueryString["IsDevice"]);
                        RBTath.Checked = Convert.ToBoolean(Request.QueryString["IsTathith"]);
                        RPTalef.Checked = Convert.ToBoolean(Request.QueryString["IsTalef"]);

                        RBTathith.Checked = true;
                        pnlStar.Visible = false;
                        ProductByUser.Visible = true;
                        pnlDataSarf.Visible = false;
                        pnlNullSarf.Visible = true;
                        ProductByTalef.Visible = false;
                        txtSearch.Focus();
                        DLSupportType.SelectedValue = Convert.ToString(Request.QueryString["XIDCate"]);
                        RBCart.Checked = Convert.ToBoolean(Request.QueryString["IsCart"]);
                        RBDevice.Checked = Convert.ToBoolean(Request.QueryString["IsDevice"]);
                        RBTath.Checked = Convert.ToBoolean(Request.QueryString["IsTathith"]);
                        FWSM_Exchange_Order_Bill_ManageByBill(new Guid(ddlYears.SelectedValue), Convert.ToInt32(txtSearch.Text.Trim()), Convert.ToInt32(DLSupportType.SelectedValue)
                        , Convert.ToBoolean(RBCart.Checked), Convert.ToBoolean(RBDevice.Checked), Convert.ToBoolean(RBTath.Checked), Convert.ToBoolean(RPTalef.Checked));


                        // التالف
                        //RPTalef.Checked = true;
                        //pnlStar.Visible = false;
                        //ProductByUser.Visible = false;
                        //ProductByTalef.Visible = true;
                        //pnlDataTalef.Visible = true;
                        //pnlNullTalef.Visible = false;
                        //txtSearchTalef.Focus();
                        //FArnProductShopMatterOfExchangeByTaleef(false, false, false, true);
                    }
                }
            }
            else if (Request.QueryString["IDX"] != null)
            {
                pnlStar.Visible = false;
                ProductByUser.Visible = false;
                ProductByTalef.Visible = false;
                ProductByTarmim.Visible = true;
                ProductByPrisms.Visible = false;
                pnlDataTarmim.Visible = false;
                pnlNullTarmim.Visible = true;
                txtSearchTarmim.Focus();
                RPTarmem.Checked = true;
                txtSearchTarmim.Text = Request.QueryString["IDX"].ToString();
                RBBenaCheck.Checked = Convert.ToBoolean(Request.QueryString["IsBena"]);
                RBTarmimCheck.Checked = Convert.ToBoolean(Request.QueryString["IsTarmem"]);
                FArn_BenaaAndTarmim_ManageByBill(Convert.ToInt32(txtSearchTarmim.Text.Trim()), Convert.ToBoolean(Request.QueryString["IsBena"]), Convert.ToBoolean(Request.QueryString["IsTarmem"]));
            }
            else if (Request.QueryString["IDS"] != null)
            {
                pnlStar.Visible = false;
                ProductByUser.Visible = false;
                ProductByTalef.Visible = false;
                ProductByTarmim.Visible = false;
                ProductByPrisms.Visible = true;
                pnlDataPrisms.Visible = false;
                pnlNullPrisms.Visible = true;
                txtSearchPrisms.Focus();
                RPSupportForPrisms.Checked = true;
                txtSearchPrisms.Text = Request.QueryString["IDS"].ToString();
                DLProject.SelectedValue = Request.QueryString["IDCh"].ToString();
                FArn_SupportForPrisms_Manage(Convert.ToInt32(txtSearchPrisms.Text.Trim()), Convert.ToInt32(DLProject.SelectedValue));
            }
        }
        catch (Exception)
        {

        }
    }

    private void FGetProjec()
    {
        ClassQuaem.FGetSupportType(1, "'5'", DLProject);
    }

    private void FGetSupportType()
    {
        ClassQuaem.FGetSupportType(1, "'1','2','3'", DLSupportType);
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
                HFID.Value = dt.Rows[0]["_ID_Item_"].ToString();
                HFIDYear.Value = dt.Rows[0]["_ID_FinancialYear_"].ToString();
                HFIDBill.Value = dt.Rows[0]["_bill_Number_"].ToString();
                HFIDProject.Value = dt.Rows[0]["_ID_Project_"].ToString();

                ID_Edit_Sarf.Visible = true; btnDelete1.Visible = true;
                ID_Edit_Sarf.HRef = "PageMatterOfExchange.aspx?ID=" + HFID.Value;

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/ERP/EOS/In_Kind_Donation/PageView.aspx?IDUniq=" + XYear.ToString() + "&ID=" + IDBill.ToString() + "&XID=" + dt.Rows[0]["_ID_MosTafeed_"].ToString() +
                    "&XIDCate=" + IDProject.ToString() + "&IsCart=" + Cart.ToString() + "&IsDevice=" + Device.ToString() + "&IsTathith=" + Tathith.ToString() + "&IsTalef=" + Talef.ToString();
                Class_QRScan.FGetQRCode(code, ImgQRCode);
                Class_QRScan.FGetQRCode(code, ImgQRCode2);

                pnlNullSarf.Visible = false;
                pnlDataSarf.Visible = true;
                ProductByTalef.Visible = false;
                ProductByUser.Visible = true;
                txtNumberMostafeed2.Text = dt.Rows[0]["_ID_MosTafeed_"].ToString();
                lblAlQariah2.Text = ClassMosTafeed.FGetMosTafeedQariah(Convert.ToInt32(dt.Rows[0]["_ID_MosTafeed_"]));
                lblPhone2.Text = ClassMosTafeed.FGetMosTafeedPhone(Convert.ToInt32(dt.Rows[0]["_ID_MosTafeed_"]));
                lblAmeenAlmosTodaa.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_ID_Storekeeper_"]));
                lblNumber.Text = dt.Rows[0]["_bill_Number_"].ToString();

                lblDateHide.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy/MM/dd") + "مـ - ";
                lblDateHide.Text += ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"])) + "هـ";
                //DLIDStorekeeper2.SelectedValue = dt.Rows[0]["_IDStorekeeper"].ToString();

                lblDataEntry.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_CreatedBy_"]));
                lblDateEntry.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]));

                txtTitle.Text = ClassSaddam.FAlTypeEvint(Convert.ToInt32(dt.Rows[0]["_ID_Type_Shipment_"])) + " لمشروع (" + ClassQuaem.FSupportType(Convert.ToInt32(dt.Rows[0]["_ID_Project_"])) + ")";
                //DLRaeesMaglesAlEdarah2.SelectedValue = dt.Rows[0]["_IDRaeesMaglisAlEdarah"].ToString();

                CBDone.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Done_"]);
                CBNotDone.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Not_Done_"]);
                CBReceived.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Received_"]);
                CBNotReceived.Checked = Convert.ToBoolean(dt.Rows[0]["_Is_Not_Received_"]);
                if (CBNotReceived.Checked)
                {
                    IDNotReceived.Visible = true;
                    lblA2.Text = dt.Rows[0]["_Note_Not_Received_"].ToString();
                }
                else
                    IDNotReceived.Visible = false;
                if (Convert.ToBoolean(dt.Rows[0]["_Is_Done_"]) == false && Convert.ToBoolean(dt.Rows[0]["_Is_Not_Done_"]) == false)
                    lblDateGo.Text = "بإنتظار الملاحظة";
                else if (Convert.ToBoolean(dt.Rows[0]["_Is_Done_"]) == true || Convert.ToBoolean(dt.Rows[0]["_Is_Not_Done_"]) == true)
                    lblDateGo.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Storekeeper_Allow_"]).ToString("yyyy/MM/dd");
                if (Convert.ToBoolean(dt.Rows[0]["_Is_Received_"]) == false && Convert.ToBoolean(dt.Rows[0]["_Is_Not_Received_"]) == false)
                    lblDateRecived.Text = "بإنتظار التسليم";
                else if (Convert.ToBoolean(dt.Rows[0]["_Is_Received_"]) == true || Convert.ToBoolean(dt.Rows[0]["_Is_Not_Received_"]) == true)
                    lblDateRecived.Text = Convert.ToDateTime(dt.Rows[0]["_The_Purpose"]).ToString("yyyy/MM/dd");
                if (Convert.ToBoolean(dt.Rows[0]["_Is_Ammen_AlSondoq_"]))
                {
                    ImgAmeenAlsondoq.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Ammen_AlSondoq_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Ammen_AlSondoq_"]));
                    ImgAmeenAlsondoq.Width = 100;
                    ImgAmeenAlsondoq.Visible = true;
                }
                else
                {
                    ImgAmeenAlsondoq.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgAmeenAlsondoq.Width = 30;
                    ImgAmeenAlsondoq.Visible = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["_Is_Raees_Maglis_AlEdarah_"]))
                {
                    ImgRaees.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Raees_Maglis_AlEdarah_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Raees_Maglis_AlEdarah_"]));
                    ImgRaees.Width = 100;
                    ImgRaees.Visible = true;
                }
                else
                {
                    ImgRaees.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgRaees.Width = 30;
                    ImgRaees.Visible = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["_Is_Moder_"]))
                {
                    ImgModer.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Moder_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Moder_"]));
                    ImgModer.Width = 100;
                    ImgModer.Visible = true;
                }
                else
                {
                    ImgModer.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgModer.Width = 30;
                    ImgModer.Visible = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["_Is_Storekeeper_"]))
                {
                    ImgAmeenAlmosTodaa.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Storekeeper_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Storekeeper_"]));
                    ImgAmeenAlmosTodaa.Width = 100;
                    ImgAmeenAlmosTodaa.Visible = true;
                }
                else
                {
                    ImgAmeenAlmosTodaa.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgAmeenAlmosTodaa.Width = 30;
                    ImgAmeenAlmosTodaa.Visible = true;
                }

                lblNameEvint2.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_ID_Delivery_"]));
                if (Convert.ToBoolean(dt.Rows[0]["_Is_Received_"]))
                {
                    ImgAlBaheth.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Delivery_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Received_"]));
                    ImgAlBaheth.Width = 100;
                    ImgAlBaheth.Visible = true;
                }
                else if (Convert.ToBoolean(dt.Rows[0]["_Is_Not_Received_"]))
                {
                    ImgAlBaheth.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Delivery_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Not_Received_"]));
                    ImgAlBaheth.Width = 100;
                    ImgAlBaheth.Visible = true;
                }
                else
                {
                    ImgAlBaheth.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgAlBaheth.Width = 30;
                    ImgAlBaheth.Visible = true;
                }

                if (Convert.ToBoolean(dt.Rows[0]["_Is_Ammen_AlSondoq_"]) && Convert.ToBoolean(dt.Rows[0]["_Is_Raees_Maglis_AlEdarah_"]) && Convert.ToBoolean(dt.Rows[0]["_Is_Moder_"]) && Convert.ToBoolean(dt.Rows[0]["_Is_Storekeeper_"]) && (Convert.ToBoolean(dt.Rows[0]["_Is_Received_"]) || Convert.ToBoolean(dt.Rows[0]["_Is_Not_Received_"])))
                    IDKhatm.Visible = true;

                if (dt.Rows[0]["_Note_"].ToString() == string.Empty || dt.Rows[0]["_Note_"].ToString() == "0")
                {
                    lblNameEvint.Text = " الإسم : " + ClassSaddam.FAlName(Convert.ToInt32(dt.Rows[0]["_ID_MosTafeed_"]), dt.Rows[0]["_ID_Type_Shipment_"].ToString());
                    DivNoteDevice.Visible = false;
                }
                else
                {
                    lblNameEvint.Text = dt.Rows[0]["_Note_"].ToString();
                    DivNoteDevice.Visible = true;
                    lblNoteDevice.Text = ClassSaddam.FAlName(Convert.ToInt32(dt.Rows[0]["_ID_MosTafeed_"]), dt.Rows[0]["_ID_Type_Shipment_"].ToString());
                }

                if (dt.Rows[0]["_The_Initiative_"].ToString() != "1")
                    lbl_InitiativesDevice.Text = ClassInitiatives.FGetInitiativesName(Convert.ToInt32(dt.Rows[0]["_The_Initiative_"]));
                else
                    lbl_InitiativesDevice.Text = string.Empty;

                if (Convert.ToInt32(dt.Rows[0]["_ID_MosTafeed_"]) == 504)
                {
                    PnlOther.Visible = true;
                    lblCount_Cart.Text = dt.Rows[0]["_Count_Cart_"].ToString();
                    lblCount_Familie.Text = dt.Rows[0]["_Count_Families_"].ToString();
                    lblCount_Qariah.Text = dt.Rows[0]["_Count_Qariah_"].ToString();
                }

                lblSarf.Text = "بموجبه يتم الصرف للسيد / ";
                IDUserDetails.Visible = true;
                pnlStar.Visible = false;

                FGetByBill(new Guid(dt.Rows[0]["_ID_Item_"].ToString()));
            }
            else
            {
                pnlNullSarf.Visible = true;
                pnlDataSarf.Visible = false;
                ProductByUser.Visible = true;
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
        GVDeedDonationInKind.UseAccessibleHeader = false;

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
            GVDeedDonationInKind.DataSource = dt;
            GVDeedDonationInKind.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);

            List<CurrencyInfo> currencies = new List<CurrencyInfo>();
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            ToWord toWord = new ToWord(Convert.ToDecimal(lblTotalPrice.Text), currencies[Convert.ToInt32(0)]);
            lblSumSaraf.Text = toWord.ConvertToArabic();
        }
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
                Label Count = (Label)e.Row.FindControl("lblCountTaleef");//take lable id
                CouTaleef += int.Parse(Count.Text);
                lblSumTaleef.Text = CouTaleef.ToString();

                Label salary = (Label)e.Row.FindControl("lblCountTotalPriceTaleef");//take lable id
                sumTaleef += decimal.Parse(salary.Text);
                if (sumTaleef != 0)
                    lblTotalPriceTaleef.Text = sumTaleef.ToString();
                else
                    lblTotalPriceTaleef.Text = "بإنتظار التسعير";
            }
        }
        catch (Exception)
        {

        }
    }

    protected void RBTathith_CheckedChanged(object sender, EventArgs e)
    {
        if (RBTathith.Checked)
        {
            pnlStar.Visible = false;
            ProductByUser.Visible = true;
            pnlDataSarf.Visible = false;
            pnlNullSarf.Visible = true;
            ProductByTalef.Visible = false;
            ProductByTarmim.Visible = false;
            ProductByPrisms.Visible = false;
            txtSearch.Focus();
        }
    }

    protected void RPTarmem_CheckedChanged(object sender, EventArgs e)
    {
        if (RPTarmem.Checked)
        {
            pnlStar.Visible = false;
            ProductByUser.Visible = false;
            ProductByTalef.Visible = false;
            ProductByTarmim.Visible = true;
            ProductByPrisms.Visible = false;
            pnlDataTarmim.Visible = false;
            pnlNullTarmim.Visible = true;
            txtSearchTarmim.Focus();
        }
    }

    protected void RPTalef_CheckedChanged(object sender, EventArgs e)
    {
        if (RPTalef.Checked)
        {
            pnlStar.Visible = false;
            ProductByUser.Visible = false;
            ProductByTalef.Visible = true;
            pnlDataTalef.Visible = false;
            pnlNullTalef.Visible = true;
            ProductByTarmim.Visible = false;
            ProductByPrisms.Visible = false;
            txtSearchTalef.Focus();
        }
    }

    protected void RPSupportForPrisms_CheckedChanged(object sender, EventArgs e)
    {
        if (RPSupportForPrisms.Checked)
        {
            pnlStar.Visible = false;
            ProductByUser.Visible = false;
            ProductByTalef.Visible = false;
            ProductByTarmim.Visible = false;
            ProductByPrisms.Visible = true;
            pnlDataPrisms.Visible = false;
            pnlNullPrisms.Visible = true;
            txtSearchPrisms.Focus();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FSearchData();
    }

    private void FSearchData()
    {
        try
        {
            if (RBTathith.Checked && RPTarmem.Checked == false && RPTalef.Checked == false && RPSupportForPrisms.Checked == false)
            {
                FWSM_Exchange_Order_Bill_ManageByBill(new Guid(ddlYears.SelectedValue), Convert.ToInt32(txtSearch.Text.Trim()), Convert.ToInt32(DLSupportType.SelectedValue),
                    Convert.ToBoolean(RBCart.Checked), Convert.ToBoolean(RBDevice.Checked), Convert.ToBoolean(RBTath.Checked), Convert.ToBoolean(RPTalef.Checked));
            }
            //else if (RBCart.Checked == false && RBDevice.Checked == false && RBTath.Checked == false && RPTalef.Checked)
            //    FArnProductShopMatterOfExchangeByTaleef(false, false, false, true);

        }
        catch (Exception)
        {

        }
    }

    protected void LbRefreshSaraf_Click(object sender, EventArgs e)
    {
        GVDeedDonationInKind.Columns[0].Visible = true;
        FSearchData();
        pnllblPrint.Visible = false;
        pnlDlPrint.Visible = true;
    }

    protected void LBPrintSaraf_Click(object sender, EventArgs e)
    {
        try
        {
            GVDeedDonationInKind.UseAccessibleHeader = true;
            GVDeedDonationInKind.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVDeedDonationInKind.Columns[0].Visible = false;
            lblDateHide.Visible = true;
            pnllblPrint.Visible = true;
            pnlDlPrint.Visible = false;
            lblIDStorekeeper2.Text = DLIDStorekeeper2.SelectedItem.ToString();
            Session["foot"] = pnlDataSarf;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    int Cou = 0;
    decimal sum = 0;
    float Getsum, Setsum = 0;

    protected void GVMatterOfExchangeByID_RowDataBound(object sender, GridViewRowEventArgs e)
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
                lblMony.Text = ClassSaddam.FGetMonySa();
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearchTalef_Click(object sender, EventArgs e)
    {
        //FArnProductShopMatterOfExchangeByTaleef(false, false, false, true);
    }

    protected void btnSearchTarmim_Click(object sender, EventArgs e)
    {
        FArn_BenaaAndTarmim_ManageByBill(Convert.ToInt32(txtSearchTarmim.Text.Trim()), RBBenaCheck.Checked, RBTarmimCheck.Checked);
    }

    protected void LBRefresh_Click(object sender, EventArgs e)
    {
        GVMatterOfExchangeByIDTaleef.Columns[0].Visible = true;
        //FArnProductShopMatterOfExchangeByTaleef(false, false, false, true);
        lblDateHideTaleef.Visible = false;
    }

    protected void LbPrintTaleef_Click(object sender, EventArgs e)
    {
        try
        {
            GVMatterOfExchangeByIDTaleef.Columns[0].Visible = false;
            lblDateHideTaleef.Visible = true;
            Session["footable1"] = pnlDataTalef;
            if (GVMatterOfExchangeByIDTaleef.Rows.Count > 15)
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintPage.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
            else if (GVMatterOfExchangeByIDTaleef.Rows.Count <= 15)
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintPage.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBRefreshTarmim_Click(object sender, EventArgs e)
    {
        FArn_BenaaAndTarmim_ManageByBill(Convert.ToInt32(txtSearchTarmim.Text.Trim()), RBBenaCheck.Checked, RBTarmimCheck.Checked);
    }

    private void FArn_BenaaAndTarmim_ManageByBill(int billNumber, bool IsBena_, bool IsTarmem_)
    {
        try
        {
            Model_BenaaAndTarmim_ MBAT = new Model_BenaaAndTarmim_();
            MBAT.IDCheck = "GetByBill";
            MBAT.IDUniq = Guid.Empty;
            MBAT.ID_FinancialYear = new Guid(ddlYears.SelectedValue);
            MBAT.ID_Donor = Guid.Empty;
            MBAT.NumberMostafeed = 0;
            MBAT.billNumber = billNumber;
            if (IsBena_ && IsTarmem_ == false)
                MBAT.ID_Project = 10;
            else if (IsBena_ == false && IsTarmem_)
                MBAT.ID_Project = 11;
            MBAT.Start_Date = string.Empty;
            MBAT.End_Date = string.Empty;
            MBAT.DataCheck = string.Empty;
            MBAT.DataCheck2 = string.Empty;
            MBAT.DataCheck3 = string.Empty;
            MBAT.IsTarmem = IsTarmem_;
            MBAT.IsBena = IsBena_;
            MBAT.IsDelete = false;
            DataTable dt = new DataTable();
            Repostry_BenaaAndTarmim_ RBAT = new Repostry_BenaaAndTarmim_();
            dt = RBAT.BArn_BenaaAndTarmim_Manage(MBAT);

            if (dt.Rows.Count > 0)
            {
                lblmsg.ForeColor = System.Drawing.Color.Black;
                string code = ClassSetting.FGetNameServer() + "/Cpanel/ERP/EOS/In_Kind_Donation/PageView.aspx?IDUniq=" + ddlYears.SelectedValue
                    + "&IDX=" + txtSearchTarmim.Text.Trim() + "&XID=" + dt.Rows[0]["NumberMostafeed"].ToString()
                    + "&IsBena=" + RBBenaCheck.Checked + "&IsTarmem=" + RBTarmimCheck.Checked;
                Class_QRScan.FGetQRCode(code, imgBarCodeTarmim);
                HFIDTarmim.Value = dt.Rows[0]["IDItem"].ToString(); 
                ID_Edit_Tarmem.Visible = true; btnDeleteTarmem.Visible = true;
                ID_Edit_Tarmem.HRef = "../Cash_Donation/PageRestorationAndConstruction.aspx?ID=" + dt.Rows[0]["IDUniq"].ToString();
                pnlNullTarmim.Visible = false;
                pnlDataTarmim.Visible = true;
                ProductByTalef.Visible = false;
                ProductByUser.Visible = false;
                ProductByTarmim.Visible = true;
                txtNumberMostafeed2Tarmim.Text = dt.Rows[0]["NumberMostafeed"].ToString();
                lblAlQariah2Tarmim.Text = ClassMosTafeed.FGetMosTafeedQariah(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]));
                lblPhone2Tarmim.Text = ClassMosTafeed.FGetMosTafeedPhone(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]));
                lblNumberTarmim.Text = dt.Rows[0]["billNumber_"].ToString();

                lblDateHideTarmim.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy/MM/dd") + "مـ - ";
                lblDateHideTarmim.Text += ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"])) + "هـ";

                lblDataEntryTarmim.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_CreatedBy_"]));
                lblDateEntryTarmim.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]));

                txtTitleTarmim.Text = ClassSaddam.FAlTypeEvint(1) + " لمشروع (" + ClassQuaem.FSupportType(Convert.ToInt32(dt.Rows[0]["ID_Type"])) + ")";

                lblTotalPriceTarmim.Text = dt.Rows[0]["The_Mony"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]))
                {
                    IDCash_Money.Visible = true;
                    IDShayk_Bank.Visible = false;
                    IDTransfer_On_Account.Visible = false;
                    CBCash_Money_.Checked = Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]);
                }
                else if (Convert.ToBoolean(dt.Rows[0]["IsShayk_Bank"]))
                {
                    IDCash_Money.Visible = false;
                    IDShayk_Bank.Visible = true;
                    IDTransfer_On_Account.Visible = false;
                    CBShayk_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["IsShayk_Bank"]);
                    lblNumber_Shayk_Bank.Text = " / رقم الشيك : " + dt.Rows[0]["Number_Shayk_Bank"].ToString();
                    lblDate_Shayk.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Get"]).ToString("yyyy/MM/dd");
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

                lblMore.Text = dt.Rows[0]["More_Details"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsAllowModer"]))
                {
                    ImgModerTarmim.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDModer"]), Convert.ToBoolean(dt.Rows[0]["IsAllowModer"]));
                    ImgModerTarmim.Width = 100;
                    ImgModerTarmim.Visible = true;
                }
                else
                {
                    ImgModerTarmim.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgModerTarmim.Width = 30;
                }
                if (Convert.ToBoolean(dt.Rows[0]["IsAllowRaeesAlMagles"]))
                {
                    ImgRaeesTarmim.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDRaeesAlMagles"]), Convert.ToBoolean(dt.Rows[0]["IsAllowRaeesAlMagles"]));
                    ImgRaeesTarmim.Width = 100;
                    ImgRaeesTarmim.Visible = true;
                    IDKhatmTarmim.Visible = true;
                }
                else
                {
                    ImgRaeesTarmim.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgRaeesTarmim.Width = 30;
                    IDKhatmTarmim.Visible = false;
                }

                CBAllowState.Checked = Convert.ToBoolean(dt.Rows[0]["AllowState"]);
                CBNotAllowState.Checked = Convert.ToBoolean(dt.Rows[0]["NotAllowState"]);
                if (Convert.ToBoolean(dt.Rows[0]["AllowState"]) && Convert.ToBoolean(dt.Rows[0]["NotAllowState"]) == false)
                {
                    ImgAmeenAlsondoqTarmim.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDAmeenAlsondoq"]), Convert.ToBoolean(dt.Rows[0]["AllowState"]));
                    ImgAmeenAlsondoqTarmim.Width = 100;
                    ImgAmeenAlsondoqTarmim.Visible = true;
                    lblDateAllowOrNotAllow.Visible = true;
                    lblWhayNotAllow.Visible = false;
                }
                else if (Convert.ToBoolean(dt.Rows[0]["NotAllowState"]) && Convert.ToBoolean(dt.Rows[0]["AllowState"]) == false)
                {
                    ImgAmeenAlsondoqTarmim.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDAmeenAlsondoq"]), Convert.ToBoolean(dt.Rows[0]["NotAllowState"]));
                    ImgAmeenAlsondoqTarmim.Width = 100;
                    ImgAmeenAlsondoqTarmim.Visible = true;
                    lblDateAllowOrNotAllow.Visible = true;
                    lblWhayNotAllow.Text = " - " + dt.Rows[0]["WhayNotAllow"].ToString();
                    lblWhayNotAllow.Visible = true;
                }
                else
                {
                    ImgAmeenAlsondoqTarmim.ImageUrl = "/loader.gif";
                    ImgAmeenAlsondoqTarmim.Width = 30;
                    lblDateAllowOrNotAllow.Visible = false;
                }

                lblDateAllowOrNotAllow.Text = "بتاريخ / " + Convert.ToDateTime(dt.Rows[0]["Date_AllowOrNotAllow"]).ToString("yyyy/MM/dd");
                if (dt.Rows[0]["_Note_"].ToString() == string.Empty || dt.Rows[0]["_Note_"].ToString() == "0")
                {
                    lblNameEvintTarmim.Text = "الإسم : " + ClassSaddam.FAlName(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]), "1");
                    DivNoteBenaAndTarmeem.Visible = false;
                }
                else
                {
                    DivNoteBenaAndTarmeem.Visible = true;
                    lblNameEvintTarmim.Text = dt.Rows[0]["_Note_"].ToString();
                    lblNoteBenaAndTarmeem.Text = ClassSaddam.FAlName(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]), "1");
                }

                if (dt.Rows[0]["_ID_DLInitiatives_"].ToString() != "بدون مبادرة")
                    lbl_InitiativesBenaAndTarmeem.Text = ClassInitiatives.FGetInitiativesName(Convert.ToInt32(dt.Rows[0]["_ID_DLInitiatives_"]));
                else
                    lbl_InitiativesBenaAndTarmeem.Text = string.Empty;

                IDUserDetailsTarmim.Visible = true;
                pnlStar.Visible = false;

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblTotalPriceTarmim.Text), currencies[Convert.ToInt32(0)]);
                lblSumSarafTarmim.Text = toWord.ConvertToArabic();
            }
            else
            {
                pnlNullTarmim.Visible = true;
                pnlDataTarmim.Visible = false;
                ProductByTarmim.Visible = true;
                pnlStar.Visible = false;
            }
        }
        catch (Exception)
        {
            lblmsg.Text = "يرجى تحديد نوع الدعم ";
            lblmsg.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void LbPrintTarmim_Click(object sender, EventArgs e)
    {
        try
        {
            Session["foot"] = pnlDataTarmim;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearchPrisms_Click(object sender, EventArgs e)
    {
        FArn_SupportForPrisms_Manage(Convert.ToInt32(txtSearchPrisms.Text.Trim()), Convert.ToInt32(DLProject.SelectedValue));
    }

    private void FArn_SupportForPrisms_Manage(int billNumber, int IDProject)
    {
        try
        {
            Model_SupportForPrisms_ MSFP = new Model_SupportForPrisms_();
            MSFP.IDCheck = "GetByBill";
            MSFP.IDUniq = Guid.Empty;
            MSFP.ID_FinancialYear = new Guid(ddlYears.SelectedValue);
            MSFP.ID_Donor = Guid.Empty;
            MSFP.NumberMostafeed = 0;
            MSFP.billNumber = billNumber;
            MSFP.ID_Project = IDProject;
            MSFP.Start_Date = string.Empty;
            MSFP.End_Date = string.Empty;
            MSFP.DataCheck = string.Empty;
            MSFP.DataCheck2 = string.Empty;
            MSFP.DataCheck3 = string.Empty;
            MSFP.IsDelete = false;
            DataTable dt = new DataTable();
            Repostry_SupportForPrisms_ RBAT = new Repostry_SupportForPrisms_();
            dt = RBAT.BArn_SupportForPrisms_Manage(MSFP);

            if (dt.Rows.Count > 0)
            {
                lblmsgPrisms.ForeColor = System.Drawing.Color.Black;
                string code = ClassSetting.FGetNameServer() + "/Cpanel/ERP/EOS/In_Kind_Donation/PageView.aspx?IDUniq=" + MSFP.ID_FinancialYear.ToString() +
                    "IDS=" + MSFP.billNumber.ToString() + "&IDCh=" + MSFP.ID_Project.ToString() + "&IDU=" + MSFP.IDUniq.ToString();
                Class_QRScan.FGetQRCode(code, imgBarCodePrisms);
                HFIDPrisms.Value = dt.Rows[0]["IDItem"].ToString();
                ID_Edit_Prisms.Visible = true; btnDeletePrisms.Visible = true;
                ID_Edit_Prisms.HRef = "../Cash_Donation/PageSupportForPrisms.aspx?ID=" + dt.Rows[0]["IDUniq"].ToString();

                pnlNullPrisms.Visible = false;
                pnlDataPrisms.Visible = true;
                ProductByTalef.Visible = false;
                ProductByUser.Visible = false;
                ProductByTarmim.Visible = false;
                ProductByPrisms.Visible = true;
                txtNumberMostafeed2Prisms.Text = dt.Rows[0]["NumberMostafeed"].ToString();
                lblAlQariah2Prisms.Text = ClassMosTafeed.FGetMosTafeedQariah(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]));
                lblPhone2Prisms.Text = ClassMosTafeed.FGetMosTafeedPhone(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]));
                lblNumberPrisms.Text = dt.Rows[0]["billNumber_"].ToString();

                lblDateHidePrisms.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy/MM/dd") + "مـ - ";
                lblDateHidePrisms.Text += ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"])) + "هـ";

                lblDataEntryPrisms.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_CreatedBy_"]));
                lblDateEntryPrisms.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]));

                txtTitlePrisms.Text = ClassSaddam.FAlTypeEvint(1) + " لمشروع (" + ClassQuaem.FSupportType(Convert.ToInt32(dt.Rows[0]["ID_Project_"])) + ")";

                lblTotalPricePrisms.Text = dt.Rows[0]["The_Mony"].ToString();

                if (Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]))
                {
                    CBCash_Money_Prisms.Checked = Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]);
                    lblNumber_Shayk_Bank_Prisms.Visible = false;
                }

                lblTotalPriceTarmim.Text = dt.Rows[0]["The_Mony"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]))
                {
                    IDCash_Money_Prisms.Visible = true;
                    IDShayk_Bank_Prisms.Visible = false;
                    IDTransfer_On_Account_Prisms.Visible = false;
                    CBCash_Money_Prisms.Checked = Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]);
                }
                else if (Convert.ToBoolean(dt.Rows[0]["IsShayk_Bank"]))
                {
                    IDCash_Money_Prisms.Visible = false;
                    IDShayk_Bank_Prisms.Visible = true;
                    IDTransfer_On_Account_Prisms.Visible = false;
                    CBShayk_Bank_Prisms.Checked = Convert.ToBoolean(dt.Rows[0]["IsShayk_Bank"]);
                    lblNumber_Shayk_Bank_Prisms.Text = " / رقم الشيك : " + dt.Rows[0]["Number_Shayk_Bank"].ToString();
                    lblDate_Shayk_Prisms.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Get"]).ToString("yyyy/MM/dd");
                    lblFor_Bank_Prisms.Text = dt.Rows[0]["_For_Bank_"].ToString();
                }
                else if (Convert.ToBoolean(dt.Rows[0]["_Transfer_On_Account_"]))
                {
                    IDCash_Money_Prisms.Visible = false;
                    IDShayk_Bank_Prisms.Visible = false;
                    IDTransfer_On_Account_Prisms.Visible = true;
                    CBTransfer_On_Account_Prisms.Checked = Convert.ToBoolean(dt.Rows[0]["_Transfer_On_Account_"]);
                    lblNumber_Account_Prisms.Text = " / حساب رقم : " + dt.Rows[0]["_Number_Account_"].ToString();
                    lblDate_Bank_Transfer_Prisms.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Bank_Transfer_"]).ToString("yyyy/MM/dd");
                    lblFor_Bank_Transfer_Prisms.Text = dt.Rows[0]["_For_Bank_Transfer_"].ToString();
                }

                lblProjectPrisms.Text = dt.Rows[0]["More_Details"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsAllowModer"]))
                {
                    ImgModerPrisms.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDModer"]), Convert.ToBoolean(dt.Rows[0]["IsAllowModer"]));
                    ImgModerPrisms.Width = 100;
                    ImgModerPrisms.Visible = true;
                }
                else
                {
                    ImgModerPrisms.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgModerPrisms.Width = 30;
                }
                if (Convert.ToBoolean(dt.Rows[0]["IsAllowRaeesAlMagles"]))
                {
                    ImgRaeesPrisms.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDRaeesAlMagles"]), Convert.ToBoolean(dt.Rows[0]["IsAllowRaeesAlMagles"]));
                    ImgRaeesPrisms.Width = 100;
                    ImgRaeesPrisms.Visible = true;
                    IDKhatmPrisms.Visible = true;
                }
                else
                {
                    ImgRaeesPrisms.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgRaeesPrisms.Width = 30;
                    IDKhatmPrisms.Visible = false;
                }

                CBAllowStatePrisms.Checked = Convert.ToBoolean(dt.Rows[0]["AllowState"]);
                CBNotAllowStatePrisms.Checked = Convert.ToBoolean(dt.Rows[0]["NotAllowState"]);
                if (Convert.ToBoolean(dt.Rows[0]["AllowState"]) && Convert.ToBoolean(dt.Rows[0]["NotAllowState"]) == false)
                {
                    ImgAmeenAlsondoqPrisms.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDAmeenAlsondoq"]), Convert.ToBoolean(dt.Rows[0]["AllowState"]));
                    ImgAmeenAlsondoqPrisms.Width = 100;
                    ImgAmeenAlsondoqPrisms.Visible = true;
                    lblDateAllowOrNotAllowPrisms.Visible = true;
                    lblWhayNotAllowPrisms.Visible = false;
                }
                else if (Convert.ToBoolean(dt.Rows[0]["NotAllowState"]) && Convert.ToBoolean(dt.Rows[0]["AllowState"]) == false)
                {
                    ImgAmeenAlsondoqPrisms.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDAmeenAlsondoq"]), Convert.ToBoolean(dt.Rows[0]["NotAllowState"]));
                    ImgAmeenAlsondoqPrisms.Width = 100;
                    ImgAmeenAlsondoqPrisms.Visible = true;
                    lblDateAllowOrNotAllowPrisms.Visible = true;
                    lblWhayNotAllowPrisms.Text = " - " + dt.Rows[0]["WhayNotAllow"].ToString();
                    lblWhayNotAllowPrisms.Visible = true;
                }
                else
                {
                    ImgAmeenAlsondoqPrisms.ImageUrl = "/Cpanel/loaderMin.gif";
                    ImgAmeenAlsondoqPrisms.Width = 30;
                    lblDateAllowOrNotAllowPrisms.Visible = false;
                }

                lblDateAllowOrNotAllowPrisms.Text = "بتاريخ / " + Convert.ToDateTime(dt.Rows[0]["Date_AllowOrNotAllow"]).ToString("yyyy/MM/dd");

                if (dt.Rows[0]["_Note_"].ToString() == string.Empty || dt.Rows[0]["_Note_"].ToString() == "0")
                {
                    lblNameEvintPrisms.Text = "الإسم : " + ClassSaddam.FAlName(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]), "1");
                    DivNote.Visible = false;
                }
                else
                {
                    DivNote.Visible = true;
                    lblNameEvintPrisms.Text = dt.Rows[0]["_Note_"].ToString();
                    lblNote.Text = ClassSaddam.FAlName(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]), "1");
                }

                if (dt.Rows[0]["_ID_DLInitiatives_"].ToString() != "1")
                    lbl_Initiatives.Text = ClassInitiatives.FGetInitiativesName(Convert.ToInt32(dt.Rows[0]["_ID_DLInitiatives_"]));
                else
                    lbl_Initiatives.Text = string.Empty;

                IDUserDetailsPrisms.Visible = true;
                pnlStar.Visible = false;

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblTotalPricePrisms.Text), currencies[Convert.ToInt32(0)]);
                lblSumSarafPrisms.Text = toWord.ConvertToArabic();
            }
            else
            {
                pnlNullPrisms.Visible = true;
                pnlDataPrisms.Visible = false;
                ProductByPrisms.Visible = true;
                pnlStar.Visible = false;
            }
        }
        catch (Exception)
        {
            lblmsgPrisms.Text = "يرجى تحديد نوع الدعم ";
            lblmsgPrisms.ForeColor = System.Drawing.Color.Red;
        }
    }

    protected void LBRefreshPrisms_Click(object sender, EventArgs e)
    {
        FArn_SupportForPrisms_Manage(Convert.ToInt32(txtSearchPrisms.Text.Trim()), Convert.ToInt32(DLProject.SelectedValue));
    }

    protected void LbPrintPrisms_Click(object sender, EventArgs e)
    {
        try
        {
            Session["foot"] = pnlDataPrisms;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FDeleteBill(new Guid(HFID.Value));
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "خطأ غير متوقع حاول لاحقاً ... ";
            return;
        }
    }

    private void FDeleteBill(Guid XID)
    {
        WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_()
        {
            IDCheck = "Delete",
            ID_Item = XID,
            ID_FinancialYear = Guid.Empty,
            ID_Donor = Guid.Empty,
            bill_Number = 0,
            ID_MosTafeed = 0,
            The_Initiative = 0,
            ID_Project = 0,
            ID_Type_Shipment = string.Empty,
            Img_Product = string.Empty,
            ID_Raees_Maglis_AlEdarah = 0,
            Is_Raees_Maglis_AlEdarah = false,
            Date_Raees_Allow = string.Empty,
            ID_Raees_Allow = 0,
            ID_Naeb_Raees = 0,
            Is_Naeb_Raees = false,
            Date_Naeb_Raees_Allow = string.Empty,
            ID_Naeb_Raees_Allow = 0,
            ID_Ammen_AlSondoq = 0,
            Is_Ammen_AlSondoq = false,
            Date_Ammen_AlSondoq_Allow = string.Empty,
            ID_Ammen_AlSondoq_Allow = 0,
            ID_Moder = 0,
            Is_Moder = true,
            Date_Moder_Allow = string.Empty,
            ID_Moder_Allow = 0,
            ID_Storekeeper = 0,
            Is_Storekeeper = false,
            Date_Storekeeper_Allow = string.Empty,
            ID_Storekeeper_Allow = 0,
            Note = string.Empty,
            Is_Done = false,
            Is_Not_Done = false,
            Is_Received = false,
            Is_Not_Received = false,
            Note_Not_Received = string.Empty,
            ID_Delivery = 0,
            ID_Delivery_Allow = 0,
            The_Purpose = string.Empty,
            Is_Cart = true,
            Is_Device = false,
            Is_Tathith = false,
            Is_Talef = false,
            Count_Cart = 0,
            Count_Families = 0,
            Count_Qariah = 0,
            CreatedBy = 0,
            CreatedDate = string.Empty,
            ModifiedBy = 0,
            ModifiedDate = string.Empty,
            DeleteBy = Test_Saddam.FGetIDUsiq(),
            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsActive = false,
            AlQaryah = 0
        };
        WSM_Repostry_Exchange_Order_Bill_ REOB = new WSM_Repostry_Exchange_Order_Bill_();
        string Xresult = REOB.FWSM_Exchange_Order_Bill_Add(MEOB);
        if (Xresult == "IsSuccessDelete")
            FDeleteCategory(XID);
    }

    private void FDeleteCategory(Guid XIDBill)
    {
        string Xresult = string.Empty;
        WSM_Model_Exchange_Order_Details_ MEOD = new WSM_Model_Exchange_Order_Details_()
        {
            IDCheck = "DeleteBill",
            IDItem = Guid.Empty,
            ID_FinancialYear = Guid.Empty,
            ID_Bill = XIDBill,
            ID_Donor = Guid.Empty,
            bill_Number = 0,
            ID_MosTafeed = 0,
            ID_Product = 0,
            Count_Product = 0,
            One_Price = 0,
            Total_Price = 0,
            ID_Project = 0,
            Is_There_Partition = false,
            Count_Partition = 0,
            Sum_Partition = 0,
            CreatedBy = 0,
            CreatedDate = string.Empty,
            ModifiedBy = 0,
            ModifiedDate = string.Empty,
            DeleteBy = Test_Saddam.FGetIDUsiq(),
            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsActive = false
        };
        WSM_Repostry_Exchange_Order_Details_ REOD = new WSM_Repostry_Exchange_Order_Details_();
        Xresult = REOD.FWSM_Exchange_Order_Details_Add(MEOD);
        if (Xresult == "IsSuccessDeleteBill")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblMessage.Text = "تم حذف الفاتورة بنجاح ... ";
            FSearchData();
        }
    }

    protected void btnDeleteTarmem_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            Model_BenaaAndTarmim_ MBAT = new Model_BenaaAndTarmim_()
            {
                IDCheck = "Delete",
                IDUniq = Guid.Empty,
                ID_FinancialYear = Guid.Empty,
                ID_Donor = Guid.Empty,
                NumberMostafeed = 0,
                billNumber = Convert.ToInt32(HFIDTarmim.Value),
                The_Mony = 0,
                IsCash_Money = false,
                IsShayk_Bank = false,
                Number_Shayk_Bank = string.Empty,
                Date_Get = string.Empty,
                For_Bank = string.Empty,
                Transfer_On_Account = false,
                Number_Account = string.Empty,
                For_Bank_Transfer = string.Empty,
                Date_Bank_Transfer = string.Empty,
                IDModer = 0,
                IsAllowModer = false,
                IDModer_Allow = 0,
                IDModer_Date_Allow = string.Empty,
                IDAmeenAlsondoq = 0,
                AllowState = false,
                AllowStateDetalis = string.Empty,
                NotAllowState = false,
                WhayNotAllow = string.Empty,
                ID_Allow_Ameen = 0,
                Date_AllowOrNotAllow = string.Empty,
                IDRaeesAlMagles = 0,
                IsAllowRaeesAlMagles = false,
                IDRaees_Allow = 0,
                IDRaees_Date_Allow = string.Empty,
                IDAlBaheth = 0,
                ID_Project = 0,
                More_Details = string.Empty,
                IsTarmem = false,
                IsBena = false,
                ID_DLInitiatives = 0,
                Note_ = string.Empty,
                Finance_Account = string.Empty,
                Is_Bank = false,
                ID_Bank = Guid.Empty,
                ID_Account = Guid.Empty,
                CreatedBy = 0,
                CreatedDate = string.Empty,
                ModifiedBy = 0,
                ModifiedDate = string.Empty,
                DeleteBy = Test_Saddam.FGetIDUsiq(),
                DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                IsDelete = true,
                Al_Qaryah = 0
            };

            Repostry_BenaaAndTarmim_ RBAT = new Repostry_BenaaAndTarmim_();
            string Xresult = RBAT.FArn_BenaaAndTarmim_Add(MBAT);
            if (Xresult == "IsSuccessDelete")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblMessage.Text = "تم حذف الفاتورة بنجاح ... ";
                FArn_BenaaAndTarmim_ManageByBill(Convert.ToInt32(txtSearchTarmim.Text.Trim()), RBBenaCheck.Checked, RBTarmimCheck.Checked);
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "خطأ غير متوقع حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnDeletePrisms_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            Model_SupportForPrisms_ MSFP = new Model_SupportForPrisms_()
            {
                IDCheck = "Delete",
                IDUniq = Guid.Empty,
                ID_FinancialYear = Guid.Empty,
                ID_Donor = Guid.Empty,
                NumberMostafeed = 0,
                billNumber = Convert.ToInt32(HFIDPrisms.Value),
                The_Mony = 0,
                IsCash_Money = false,
                IsShayk_Bank = false,
                Number_Shayk_Bank = string.Empty,
                Date_Get = string.Empty,
                For_Bank = string.Empty,
                Transfer_On_Account = false,
                Number_Account = string.Empty,
                For_Bank_Transfer = string.Empty,
                Date_Bank_Transfer = string.Empty,
                IDModer = 0,
                IsAllowModer = false,
                IDModer_Allow = 0,
                IDModer_Date_Allow = string.Empty,
                IDAmeenAlsondoq = 0,
                AllowState = false,
                AllowStateDetalis = string.Empty,
                NotAllowState = false,
                WhayNotAllow = string.Empty,
                ID_Allow_Ameen = 0,
                Date_AllowOrNotAllow = string.Empty,
                IDRaeesAlMagles = 0,
                IsAllowRaeesAlMagles = false,
                IDRaees_Allow = 0,
                IDRaees_Date_Allow = string.Empty,
                IDAlBaheth = 0,
                ID_Project = 0,
                More_Details = string.Empty,
                ID_DLInitiatives = 0,
                Note_ = string.Empty,
                Finance_Account = string.Empty,
                Is_Bank = false,
                ID_Bank = Guid.Empty,
                ID_Account = Guid.Empty,
                CreatedBy = 0,
                CreatedDate = string.Empty,
                ModifiedBy = 0,
                ModifiedDate = string.Empty,
                DeleteBy = Test_Saddam.FGetIDUsiq(),
                DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                IsDelete = true,
                Al_Qaryah = 0
            };

            Repostry_SupportForPrisms_ RSFP = new Repostry_SupportForPrisms_();
            string Xresult = RSFP.FArn_SupportForPrisms_Add(MSFP);
            if (Xresult == "IsSuccessDelete")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblMessage.Text = "تم حذف الفاتورة بنجاح ... ";
                FArn_SupportForPrisms_Manage(Convert.ToInt32(txtSearchPrisms.Text.Trim()), Convert.ToInt32(DLProject.SelectedValue));
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "خطأ غير متوقع حاول لاحقاً ... ";
            return;
        }
    }

}