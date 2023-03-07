using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CPBeneficiary_PageManageProductAddThePriceToOrder : System.Web.UI.Page
{
    string UserERasAlEstemarah;
    private void GetCookie()
    {
        try
        {
            HttpCookie CookeCheck;  // اسم المستخدم
            CookeCheck = Request.Cookies["__User_True_User"];
            UserERasAlEstemarah = ClassSaddam.UnprotectPassword(CookeCheck["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");
        }
        catch
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    private void CheckAccountAdmin(string XID)
    {
        GetCookie();
        ClassMosTafeed CM = new ClassMosTafeed();
        CM._User_Name_ = UserERasAlEstemarah;
        CM._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CM.BArnRasAlEstemarahLogin();
        if (dtViewProfil.Rows.Count > 0)
        {
            if (dtViewProfil.Rows[0]["NumberMostafeed"].ToString() != XID)
                Response.Redirect("PageNotAccess.aspx");
        }
        else
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //CheckAccountAdmin();
            pnlStar.Visible = true;
            FGetProjec();
            FGetSupportType();
            if (Request.QueryString["ID"] != null)
            {
                if (Request.QueryString["XID"] != null)
                {
                    if (Request.QueryString["ID"] != null)
                    {
                        txtSearch.Text = Request.QueryString["ID"].ToString();
                        DataTable dt = new DataTable();
                        RBCart.Checked = Convert.ToBoolean(Request.QueryString["IsCart"]);
                        RBDevice.Checked = Convert.ToBoolean(Request.QueryString["IsDevice"]);
                        RBTath.Checked = Convert.ToBoolean(Request.QueryString["IsTathith"]);
                        dt = ClassDataAccess.GetData("SELECT Top(5) [_IDItem],[_billNumber],[_IDMosTafeed] FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber = @0 And _IsDelete = @1 And _IsCart = @2 And _IsDevice = @3 And _IsTathith = @4 And _IsTalef = @5", Request.QueryString["ID"].ToString(), Convert.ToString(false), Convert.ToString(RBCart.Checked), Convert.ToString(RBDevice.Checked), Convert.ToString(RBTath.Checked), Convert.ToString(false));
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dt.Rows[0]["_IDMosTafeed"]) != 999999999)
                            {
                                RBTathith.Checked = true;
                                pnlStar.Visible = false;
                                ProductByUser.Visible = true;
                                pnlDataSarf.Visible = false;
                                pnlNullSarf.Visible = true;
                                txtSearch.Focus();
                                Session["XID"] = Convert.ToInt32(dt.Rows[0]["_IDMosTafeed"]);
                                DLSupportType.SelectedValue = Convert.ToString(Request.QueryString["XIDCate"]);
                                RBCart.Checked = Convert.ToBoolean(Request.QueryString["IsCart"]);
                                RBDevice.Checked = Convert.ToBoolean(Request.QueryString["IsDevice"]);
                                RBTath.Checked = Convert.ToBoolean(Request.QueryString["IsTathith"]);
                                FArnProductShopMatterOfExchangeByUser(Convert.ToInt32(Session["XID"]), Convert.ToInt64(Request.QueryString["XIDCate"])
                                    , Convert.ToBoolean(Request.QueryString["IsCart"]), Convert.ToBoolean(Request.QueryString["IsDevice"])
                                    , Convert.ToBoolean(Request.QueryString["IsTathith"]), Convert.ToBoolean(Request.QueryString["IsTalef"]));
                            }
                        }
                    }
                }
            }
            else if (Request.QueryString["IDX"] != null)
            {
                pnlStar.Visible = false;
                ProductByUser.Visible = false;
                ProductByTarmim.Visible = true;
                ProductByPrisms.Visible = false;
                pnlDataTarmim.Visible = false;
                pnlNullTarmim.Visible = true;
                txtSearchTarmim.Focus();
                RPTarmem.Checked = true;
                txtSearchTarmim.Text = Request.QueryString["IDX"].ToString();
                RBBenaCheck.Checked = Convert.ToBoolean(Request.QueryString["IsBena"]);
                RBTarmimCheck.Checked = Convert.ToBoolean(Request.QueryString["IsTarmem"]);
                FArnProductShopMatterOfExchangeByUserHouser(Convert.ToInt32(txtSearchTarmim.Text.Trim()), Convert.ToBoolean(Request.QueryString["IsBena"]), Convert.ToBoolean(Request.QueryString["IsTarmem"]));
            }
            else if (Request.QueryString["IDS"] != null)
            {
                pnlStar.Visible = false;
                ProductByUser.Visible = false;
                ProductByTarmim.Visible = false;
                ProductByPrisms.Visible = true;
                pnlDataPrisms.Visible = false;
                pnlNullPrisms.Visible = true;
                txtSearchPrisms.Focus();
                RPSupportForPrisms.Checked = true;
                txtSearchPrisms.Text = Request.QueryString["IDS"].ToString();
                DLProject.SelectedValue = Request.QueryString["IDCh"].ToString();
                FArnSupportForPrismsByBillPrisms(Convert.ToInt32(txtSearchPrisms.Text.Trim()), Convert.ToInt32(DLProject.SelectedValue));
            }
        }
    }

    private void FGetSupportType()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where TypeAlDam <> @0 And TypeAlDam <> @1 And TypeAlDam <> @2 And TypeAlDam <> @3 And IsDeleteTypeAlDam = @4 Order by IDItem", string.Empty, "بناء منازل", "ترميم منازل", "التالف", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLSupportType.Items.Clear();
            DLSupportType.Items.Add("");
            DLSupportType.AppendDataBoundItems = true;
            DLSupportType.DataValueField = "IDItem";
            DLSupportType.DataTextField = "TypeAlDam";
            DLSupportType.DataSource = dt;
            DLSupportType.DataBind();
        }
    }

    private void FArnProductShopMatterOfExchangeByUser(int IDMostafeed, float IDCategory, bool Cart, bool Device, bool Tathith, bool Talef)
    {
        try
        {
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = Convert.ToInt32(txtSearch.Text.Trim());
            CPS.IDMosTafeed = IDMostafeed;
            CPS.IDCategory = IDCategory;
            CPS.IsDelete = false;
            CPS.IsCart = Cart;
            CPS.IsDevice = Device;
            CPS.IsTathith = Tathith;
            CPS.IsTalef = Talef;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopMatterOfExchangeByUserNew();
            if (dt.Rows.Count > 0)
            {

                CheckAccountAdmin(dt.Rows[0]["_IDMosTafeed"].ToString());
                IDBarcode.ImageUrl = "http://chart.apis.google.com/chart?cht=qr&chl=" + ClassSetting.FGetNameServer() +
                    "/CPBeneficiary/PageManageProductAddThePriceToOrder.aspx?ID=" + txtSearch.Text.Trim() + "&XID=" + IDMostafeed.ToString() + "&XIDCate=" + DLSupportType.SelectedValue
                    + "&IsCart=" + Cart.ToString() + "&IsDevice=" + Device.ToString() + "&IsTathith=" + Tathith.ToString() + "&IsTalef=" + Talef.ToString() + "&chs=95";

                GVMatterOfExchangeByID.DataSource = dt;
                GVMatterOfExchangeByID.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNullSarf.Visible = false;
                pnlDataSarf.Visible = true;
                ProductByUser.Visible = true;
                txtNumberMostafeed2.Text = dt.Rows[0]["_IDMosTafeed"].ToString();
                lblAlQariah2.Text = ClassMosTafeed.FGetMosTafeedQariah(Convert.ToInt32(dt.Rows[0]["_IDMosTafeed"]));
                lblPhone2.Text = ClassMosTafeed.FGetMosTafeedPhone(Convert.ToInt32(dt.Rows[0]["_IDMosTafeed"]));
                lblAmeenAlmosTodaa.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDStorekeeper"]));
                lblNumber.Text = dt.Rows[0]["_billNumber"].ToString();
                lblNameEvint.Text = ClassSaddam.FAlName(Convert.ToInt32(dt.Rows[0]["_IDMosTafeed"]), dt.Rows[0]["_IDType"].ToString());

                lblDateHide.Text = Convert.ToDateTime(dt.Rows[0]["_ProductionDate"]).ToString("yyyy/MM/dd") + "مـ - ";
                lblDateHide.Text += ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["_ProductionDate"])) + "هـ";
                //DLIDStorekeeper2.SelectedValue = dt.Rows[0]["_IDStorekeeper"].ToString();

                lblDataEntry.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDAdmin"]));
                lblDateEntry.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_DateAddProduct"]));
                if (Convert.ToInt32(dt.Rows[0]["_IDUpdate"]) != 0)
                {
                    IDUpdate.Visible = true;
                    lblDataEntryEdit.Text = ClassQuaem.FAlBahethByEdit(Convert.ToInt32(dt.Rows[0]["_IDUpdate"]));
                    lblDateEntryEdit.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_DateUpDate"]));
                }
                else if (Convert.ToInt32(dt.Rows[0]["_IDUpdate"]) == 0)
                {
                    IDUpdate.Visible = false;
                }

                txtTitle.Text = ClassSaddam.FAlTypeEvint(Convert.ToInt32(dt.Rows[0]["_IDType"])) + " لمشروع (" + ClassQuaem.FSupportType(Convert.ToInt32(dt.Rows[0]["_IDCategory"])) + ")";
                //DLRaeesMaglesAlEdarah2.SelectedValue = dt.Rows[0]["_IDRaeesMaglisAlEdarah"].ToString();

                CBDone.Checked = Convert.ToBoolean(dt.Rows[0]["_IsDone"]);
                CBNotDone.Checked = Convert.ToBoolean(dt.Rows[0]["_IsNotDone"]);
                CBReceived.Checked = Convert.ToBoolean(dt.Rows[0]["_IsReceived"]);
                CBNotReceived.Checked = Convert.ToBoolean(dt.Rows[0]["_IsNotReceived"]);
                if (CBNotReceived.Checked)
                {
                    IDNotReceived.Visible = true;
                    lblA2.Text = dt.Rows[0]["_A2"].ToString();
                }
                else
                {
                    IDNotReceived.Visible = false;
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsDone"]) == false && Convert.ToBoolean(dt.Rows[0]["_IsNotDone"]) == false)
                {
                    lblDateGo.Text = "بإنتظار الملاحظة";
                }
                else if (Convert.ToBoolean(dt.Rows[0]["_IsDone"]) == true || Convert.ToBoolean(dt.Rows[0]["_IsNotDone"]) == true)
                {
                    lblDateGo.Text = Convert.ToDateTime(dt.Rows[0]["_ExpiryDate"]).ToString("yyyy/MM/dd");
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsReceived"]) == false && Convert.ToBoolean(dt.Rows[0]["_IsNotReceived"]) == false)
                {
                    lblDateRecived.Text = "بإنتظار التسليم";
                }
                else if (Convert.ToBoolean(dt.Rows[0]["_IsReceived"]) == true || Convert.ToBoolean(dt.Rows[0]["_IsNotReceived"]) == true)
                {
                    lblDateRecived.Text = Convert.ToDateTime(dt.Rows[0]["_DateCaming"]).ToString("yyyy/MM/dd");
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq"]))
                {
                    ImgAmeenAlsondoq.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDAmmenAlSondoq"]), Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq"]));
                    ImgAmeenAlsondoq.Width = 100;
                    ImgAmeenAlsondoq.Visible = true;
                }
                else
                {
                    ImgAmeenAlsondoq.ImageUrl = "loaderMin.gif";
                    ImgAmeenAlsondoq.Width = 30;
                    ImgAmeenAlsondoq.Visible = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah"]))
                {
                    ImgRaees.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDRaeesMaglisAlEdarah"]), Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah"]));
                    ImgRaees.Width = 100;
                    ImgRaees.Visible = true;
                }
                else
                {
                    ImgRaees.ImageUrl = "loaderMin.gif";
                    ImgRaees.Width = 30;
                    ImgRaees.Visible = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsModer"]))
                {
                    ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDModer"]), Convert.ToBoolean(dt.Rows[0]["_IsModer"]));
                    ImgModer.Width = 100;
                    ImgModer.Visible = true;
                }
                else
                {
                    ImgModer.ImageUrl = "loaderMin.gif";
                    ImgModer.Width = 30;
                    ImgModer.Visible = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]))
                {
                    ImgAmeenAlmosTodaa.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDStorekeeper"]), Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]));
                    ImgAmeenAlmosTodaa.Width = 100;
                    ImgAmeenAlmosTodaa.Visible = true;
                }
                else
                {
                    ImgAmeenAlmosTodaa.ImageUrl = "loaderMin.gif";
                    ImgAmeenAlmosTodaa.Width = 30;
                    ImgAmeenAlmosTodaa.Visible = true;
                }

                lblNameEvint2.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDDelivery"]));
                if (Convert.ToBoolean(dt.Rows[0]["_IsReceived"]))
                {
                    ImgAlBaheth.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDDelivery"]), Convert.ToBoolean(dt.Rows[0]["_IsReceived"]));
                    ImgAlBaheth.Width = 100;
                    ImgAlBaheth.Visible = true;
                }
                else
                {
                    ImgAlBaheth.ImageUrl = "loaderMin.gif";
                    ImgAlBaheth.Width = 30;
                    ImgAlBaheth.Visible = true;
                }

                if (Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq"]) && Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah"]) && Convert.ToBoolean(dt.Rows[0]["_IsModer"]) && Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]) && Convert.ToBoolean(dt.Rows[0]["_IsReceived"]))
                {
                    IDKhatm.Visible = true;
                }
                if (dt.Rows[0]["_A1"].ToString() != string.Empty)
                {
                    DivNoteDevice.Visible = true;
                    lblNoteDevice.Text = dt.Rows[0]["_A1"].ToString();
                }
                else
                    lblNoteDevice.Visible = false;
                if (dt.Rows[0]["Name_Initiatives_"].ToString() != "بدون مبادرة")
                    lbl_InitiativesDevice.Text = dt.Rows[0]["Name_Initiatives_"].ToString();
                else
                    lbl_InitiativesDevice.Text = string.Empty;

                lblSarf.Text = "بموجبه يتم الصرف للسيد / ";
                IDUserDetails.Visible = true;
                pnlStar.Visible = false;

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblTotalPrice.Text), currencies[Convert.ToInt32(0)]);
                lblSumSaraf.Text = toWord.ConvertToArabic();
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

    protected void LbRefreshSaraf_Click(object sender, EventArgs e)
    {
        FArnProductShopMatterOfExchangeByUser(Convert.ToInt32(Session["XID"]), Convert.ToInt64(DLSupportType.SelectedValue), RBCart.Checked, RBDevice.Checked, RBTath.Checked, false);
        pnllblPrint.Visible = false;
        pnlDlPrint.Visible = true;
    }

    protected void LBPrintSaraf_Click(object sender, EventArgs e)
    {
        try
        {
            lblDateHide.Visible = true;
            pnllblPrint.Visible = true;
            pnlDlPrint.Visible = false;
            lblIDStorekeeper2.Text = DLIDStorekeeper2.SelectedItem.ToString();
            Session["footable1"] = pnlDataSarf;
            if (GVMatterOfExchangeByID.Rows.Count > 15)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintPage.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
            }
            else if (GVMatterOfExchangeByID.Rows.Count <= 15)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintPage.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnDeleteTaleef_Click(object sender, EventArgs e)
    {

    }

    protected void btnSearchTarmim_Click(object sender, EventArgs e)
    {
        FArnProductShopMatterOfExchangeByUserHouser(Convert.ToInt32(txtSearchTarmim.Text.Trim()), RBBenaCheck.Checked, RBTarmimCheck.Checked);
    }

    private void FArnProductShopMatterOfExchangeByUserHouser(int billNumber, bool IsBena, bool IsTarmem)
    {
        try
        {
            ClassBenaaAndTarmim CBAT = new ClassBenaaAndTarmim();
            CBAT._billNumber_ = billNumber;
            CBAT._IsTarmem = IsTarmem;
            CBAT._IsBena = IsBena;
            CBAT._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CBAT.BArnBenaaAndTarmimByBill();
            if (dt.Rows.Count > 0)
            {
                CheckAccountAdmin(dt.Rows[0]["NumberMostafeed"].ToString());

                lblmsg.ForeColor = System.Drawing.Color.Black;
                IDBarcodeTarmim.ImageUrl = "http://chart.apis.google.com/chart?cht=qr&chl=" + ClassSetting.FGetNameServer() +
                        "/CPBeneficiary/PageManageProductAddThePriceToOrder.aspx?IDX=" + txtSearchTarmim.Text.Trim() + "&XID=" + dt.Rows[0]["NumberMostafeed"].ToString() + "&IsBena=" + Request.QueryString["IsBena"] + "&IsTarmem=" + Request.QueryString["IsTarmem"] + "&chs=95";

                pnlNullTarmim.Visible = false;
                pnlDataTarmim.Visible = true;
                ProductByUser.Visible = false;
                ProductByTarmim.Visible = true;
                txtNumberMostafeed2Tarmim.Text = dt.Rows[0]["NumberMostafeed"].ToString();
                lblAlQariah2Tarmim.Text = ClassMosTafeed.FGetMosTafeedQariah(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]));
                lblPhone2Tarmim.Text = ClassMosTafeed.FGetMosTafeedPhone(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]));
                lblNumberTarmim.Text = dt.Rows[0]["billNumber_"].ToString();
                lblNameEvintTarmim.Text = ClassSaddam.FAlName(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]), "1");

                lblDateHideTarmim.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Get"]).ToString("yyyy/MM/dd") + "مـ - ";
                lblDateHideTarmim.Text += ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["_Date_Get"])) + "هـ";

                lblDataEntryTarmim.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["IDAdmin"]));
                lblDateEntryTarmim.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["Date_Add_Report"]));

                txtTitleTarmim.Text = ClassSaddam.FAlTypeEvint(1) + " لمشروع (" + ClassQuaem.FSupportType(Convert.ToInt32(dt.Rows[0]["ID_Type"])) + ")";

                lblTotalPriceTarmim.Text = dt.Rows[0]["The_Mony"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]))
                {
                    CBCash_Money_.Checked = Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]);
                    lblNumber_Shayk_Bank.Visible = false;
                }
                else
                {
                    CBShayk_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["IsShayk_Bank"]);
                    lblNumber_Shayk_Bank.Text = " / رقم الشيك : " + dt.Rows[0]["Number_Shayk_Bank"].ToString();
                    lblNumber_Shayk_Bank.Visible = true;
                }
                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Get"]).ToString("yyyy/MM/dd");
                lblMore.Text = dt.Rows[0]["More_Details"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsAllowModer"]))
                {
                    ImgModerTarmim.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDModer"]), Convert.ToBoolean(dt.Rows[0]["IsAllowModer"]));
                    ImgModerTarmim.Width = 100;
                    ImgModerTarmim.Visible = true;
                }
                else
                {
                    ImgModerTarmim.ImageUrl = "loaderMin.gif";
                    ImgModerTarmim.Width = 30;
                }
                if (Convert.ToBoolean(dt.Rows[0]["IsAllowRaeesAlMagles"]))
                {
                    ImgRaeesTarmim.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDRaeesAlMagles"]), Convert.ToBoolean(dt.Rows[0]["IsAllowRaeesAlMagles"]));
                    ImgRaeesTarmim.Width = 100;
                    ImgRaeesTarmim.Visible = true;
                    IDKhatmTarmim.Visible = true;
                }
                else
                {
                    ImgRaeesTarmim.ImageUrl = "loaderMin.gif";
                    ImgRaeesTarmim.Width = 30;
                    IDKhatmTarmim.Visible = false;
                }

                CBAllowState.Checked = Convert.ToBoolean(dt.Rows[0]["AllowState"]);
                CBNotAllowState.Checked = Convert.ToBoolean(dt.Rows[0]["NotAllowState"]);
                if (Convert.ToBoolean(dt.Rows[0]["AllowState"]) && Convert.ToBoolean(dt.Rows[0]["NotAllowState"]) == false)
                {
                    ImgAmeenAlsondoqTarmim.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDAmeenAlsondoq"]), Convert.ToBoolean(dt.Rows[0]["AllowState"]));
                    ImgAmeenAlsondoqTarmim.Width = 100;
                    ImgAmeenAlsondoqTarmim.Visible = true;
                    lblDateAllowOrNotAllow.Visible = true;
                    lblWhayNotAllow.Visible = false;
                }
                else if (Convert.ToBoolean(dt.Rows[0]["NotAllowState"]) && Convert.ToBoolean(dt.Rows[0]["AllowState"]) == false)
                {
                    ImgAmeenAlsondoqTarmim.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDAmeenAlsondoq"]), Convert.ToBoolean(dt.Rows[0]["NotAllowState"]));
                    ImgAmeenAlsondoqTarmim.Width = 100;
                    ImgAmeenAlsondoqTarmim.Visible = true;
                    lblDateAllowOrNotAllow.Visible = true;
                    lblWhayNotAllow.Text = " - " + dt.Rows[0]["WhayNotAllow"].ToString();
                    lblWhayNotAllow.Visible = true;
                }
                else
                {
                    ImgAmeenAlsondoqTarmim.ImageUrl = "../loader.gif";
                    ImgAmeenAlsondoqTarmim.Width = 30;
                    lblDateAllowOrNotAllow.Visible = false;
                }

                lblDateAllowOrNotAllow.Text = "بتاريخ / " + Convert.ToDateTime(dt.Rows[0]["Date_AllowOrNotAllow"]).ToString("yyyy/MM/dd");
                if (dt.Rows[0]["A2"].ToString() != string.Empty)
                {
                    DivNoteBenaAndTarmeem.Visible = true;
                    lblNoteBenaAndTarmeem.Text = dt.Rows[0]["A2"].ToString();
                }
                else
                    DivNoteBenaAndTarmeem.Visible = false;

                if (dt.Rows[0]["Name_Initiatives_"].ToString() != "بدون مبادرة")
                    lbl_InitiativesBenaAndTarmeem.Text = dt.Rows[0]["Name_Initiatives_"].ToString();
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

    private string FChangeTitle(string XTitle)
    {
        string XResult = "";
        if (XTitle == "بناء منازل")
        {
            XResult = "بناء منزل";
        }
        else
        {
            XResult = "ترميم منزل";
        }
        return XResult;
    }

    protected void LBRefreshTarmim_Click(object sender, EventArgs e)
    {
        FArnProductShopMatterOfExchangeByUserHouser(Convert.ToInt32(txtSearchTarmim.Text.Trim()), RBBenaCheck.Checked, RBTarmimCheck.Checked);
    }

    protected void LbPrintTarmim_Click(object sender, EventArgs e)
    {
        try
        {
            Session["foot"] = pnlDataTarmim;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearchPrisms_Click(object sender, EventArgs e)
    {
        FArnSupportForPrismsByBillPrisms(Convert.ToInt32(txtSearchPrisms.Text.Trim()), Convert.ToInt32(DLProject.SelectedValue));
    }

    protected void LBRefreshPrisms_Click(object sender, EventArgs e)
    {
        FArnSupportForPrismsByBillPrisms(Convert.ToInt32(txtSearchPrisms.Text.Trim()), Convert.ToInt32(DLProject.SelectedValue));
    }

    protected void LbPrintPrisms_Click(object sender, EventArgs e)
    {
        try
        {
            Session["foot"] = pnlDataPrisms;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    private void F(int IDDevice, int XID)
    {
        DataTable dtDevice = new DataTable();
        dtDevice = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[ReportAlZyaratElectricalAppliances] With(NoLock) Where IDDevice = @0 And IDMustafeed = @1 And IsDelete = @2", Convert.ToString(IDDevice), txtNumberMostafeed2.Text.Trim(), Convert.ToString(false));
        if (dtDevice.Rows.Count > 0)
        {
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            conn.Open();
            string sql = "UPDATE [dbo].[ReportAlZyaratElectricalAppliances] SET [IDNumberCount] = @IDNumberCount Where IDDevice = @IDDevice And IDMustafeed = @IDMustafeed";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IDDevice", IDDevice);
            cmd.Parameters.AddWithValue("@IDMustafeed", Convert.ToInt64(txtNumberMostafeed2.Text.Trim()));
            cmd.Parameters.AddWithValue("@IDNumberCount", Convert.ToInt32(dtDevice.Rows[0]["IDNumberCount"]) + XID);
            cmd.ExecuteScalar();
            conn.Close();
        }

    }

    int Cou = 0;
    decimal sum = 0;
    float Getsum, Setsum = 0;
    protected void GVMatterOfExchangeByID_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Count = (Label)e.Row.FindControl("lblCount");//take lable id
            Cou += int.Parse(Count.Text);
            lblSum.Text = Cou.ToString();

            Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
            sum += decimal.Parse(salary.Text);
            if (sum != 0)
            {
                lblTotalPrice.Text = sum.ToString();
            }
            else
            {
                lblTotalPrice.Text = "بإنتظار التسعير";
            }
        }
    }

    private void FGetSum(int XID)
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Sum([_CountProduct]) As 'Get' FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _IDProduct = @0 And _billNumber <> @1 And _IsDelete = @2", Convert.ToString(XID), Convert.ToString(0), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            try
            {
                Getsum = Convert.ToInt64(dt.Rows[0]["Get"]);
            }
            catch (Exception)
            {
                Getsum = 0;
            }
        }
    }

    private void FSetSum(int XID)
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Sum([_CountProduct]) As 'Set' FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _IDProduct = @0 And _billNumber = @1 And _IsDelete = @2", Convert.ToString(XID), Convert.ToString(0), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            try
            {
                Setsum = Convert.ToInt64(dt.Rows[0]["Set"]);
            }
            catch (Exception)
            {
                Setsum = 0;
            }
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
            ProductByTarmim.Visible = true;
            ProductByPrisms.Visible = false;
            pnlDataTarmim.Visible = false;
            pnlNullTarmim.Visible = true;
            txtSearchTarmim.Focus();
        }
    }

    protected void RPSupportForPrisms_CheckedChanged(object sender, EventArgs e)
    {
        if (RPSupportForPrisms.Checked)
        {
            pnlStar.Visible = false;
            ProductByUser.Visible = false;
            ProductByTarmim.Visible = false;
            ProductByPrisms.Visible = true;
            pnlDataPrisms.Visible = false;
            pnlNullPrisms.Visible = true;
            txtSearchPrisms.Focus();
        }
    }

    private void FGetProjec()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where TypeAlDam <> @0 And TypeAlDam <> @1 And TypeAlDam <> @2 And TypeAlDam <> @3 And TypeAlDam <> @4 And TypeAlDam <> @5 And TypeAlDam <> @6 And TypeAlDam <> @7 And IsDeleteTypeAlDam = @8 Order by IDItem",
            string.Empty, "زكاة التمور", "السلة الغذائية الرمضانية", "بناء منازل", "ترميم منازل", "تاثيث منازل", "الاجهزة الكهربائية", "التالف", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLProject.Items.Clear();
            DLProject.Items.Add("");
            DLProject.AppendDataBoundItems = true;
            DLProject.DataValueField = "IDItem";
            DLProject.DataTextField = "TypeAlDam";
            DLProject.DataSource = dt;
            DLProject.DataBind();
        }
    }

    private void FArnSupportForPrismsByBillPrisms(int billNumber, float IDProject)
    {
        try
        {
            ClassSupportForPrisms CSFP = new ClassSupportForPrisms();
            CSFP._billNumber_ = billNumber;
            CSFP._ID_Type = IDProject;
            CSFP._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CSFP.BArnSupportForPrismsByBill();
            if (dt.Rows.Count > 0)
            {
                CheckAccountAdmin(dt.Rows[0]["NumberMostafeed"].ToString());

                lblmsgPrisms.ForeColor = System.Drawing.Color.Black;
                IDBarcodePrisms.ImageUrl = "http://chart.apis.google.com/chart?cht=qr&chl=" + ClassSetting.FGetNameServer() +
                        "/Cpanel/PScanF.aspx?IDS=" + billNumber.ToString() + "&IDCh=" + IDProject.ToString() + "&chs=75";

                pnlNullPrisms.Visible = false;
                pnlDataPrisms.Visible = true;
                ProductByUser.Visible = false;
                ProductByTarmim.Visible = false;
                ProductByPrisms.Visible = true;
                txtNumberMostafeed2Prisms.Text = dt.Rows[0]["NumberMostafeed"].ToString();
                lblAlQariah2Prisms.Text = ClassMosTafeed.FGetMosTafeedQariah(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]));
                lblPhone2Prisms.Text = ClassMosTafeed.FGetMosTafeedPhone(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]));
                lblNumberPrisms.Text = dt.Rows[0]["billNumber_"].ToString();
                lblNameEvintPrisms.Text = ClassSaddam.FAlName(Convert.ToInt32(dt.Rows[0]["NumberMostafeed"]), "1");

                lblDateHidePrisms.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Get"]).ToString("yyyy/MM/dd") + "مـ - ";
                lblDateHidePrisms.Text += ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["_Date_Get"])) + "هـ";

                lblDataEntryPrisms.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["IDAdmin"]));
                lblDateEntryPrisms.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["Date_Add_Report"]));

                txtTitlePrisms.Text = ClassSaddam.FAlTypeEvint(1) + " لمشروع (" + ClassQuaem.FSupportType(Convert.ToInt32(dt.Rows[0]["ID_Type"])) + ")";

                lblTotalPricePrisms.Text = dt.Rows[0]["The_Mony"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]))
                {
                    CBCash_Money_Prisms.Checked = Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]);
                    lblNumber_Shayk_Bank_Prisms.Visible = false;
                }
                else if (Convert.ToBoolean(dt.Rows[0]["A1"]))
                {
                    CBTrnfire_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["A1"]);
                    lblNumber_Shayk_Bank_Prisms.Visible = false;
                }
                else
                {
                    CBShayk_BankPrisms.Checked = Convert.ToBoolean(dt.Rows[0]["IsShayk_Bank"]);
                    lblNumber_Shayk_Bank_Prisms.Text = " / رقم الشيك : " + dt.Rows[0]["Number_Shayk_Bank"].ToString();
                    lblNumber_Shayk_Bank_Prisms.Visible = true;
                }
                lblDatePrisms.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Get"]).ToString("yyyy/MM/dd");
                lblProjectPrisms.Text = dt.Rows[0]["More_Details"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsAllowModer"]))
                {
                    ImgModerPrisms.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDModer"]), Convert.ToBoolean(dt.Rows[0]["IsAllowModer"]));
                    ImgModerPrisms.Width = 100;
                    ImgModerPrisms.Visible = true;
                }
                else
                {
                    ImgModerPrisms.ImageUrl = "loaderMin.gif";
                    ImgModerPrisms.Width = 30;
                }
                if (Convert.ToBoolean(dt.Rows[0]["IsAllowRaeesAlMagles"]))
                {
                    ImgRaeesPrisms.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDRaeesAlMagles"]), Convert.ToBoolean(dt.Rows[0]["IsAllowRaeesAlMagles"]));
                    ImgRaeesPrisms.Width = 100;
                    ImgRaeesPrisms.Visible = true;
                    IDKhatmPrisms.Visible = true;
                }
                else
                {
                    ImgRaeesPrisms.ImageUrl = "loaderMin.gif";
                    ImgRaeesPrisms.Width = 30;
                    IDKhatmPrisms.Visible = false;
                }

                CBAllowStatePrisms.Checked = Convert.ToBoolean(dt.Rows[0]["AllowState"]);
                CBNotAllowStatePrisms.Checked = Convert.ToBoolean(dt.Rows[0]["NotAllowState"]);
                if (Convert.ToBoolean(dt.Rows[0]["AllowState"]) && Convert.ToBoolean(dt.Rows[0]["NotAllowState"]) == false)
                {
                    ImgAmeenAlsondoqPrisms.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDAmeenAlsondoq"]), Convert.ToBoolean(dt.Rows[0]["AllowState"]));
                    ImgAmeenAlsondoqPrisms.Width = 100;
                    ImgAmeenAlsondoqPrisms.Visible = true;
                    lblDateAllowOrNotAllowPrisms.Visible = true;
                    lblWhayNotAllowPrisms.Visible = false;
                }
                else if (Convert.ToBoolean(dt.Rows[0]["NotAllowState"]) && Convert.ToBoolean(dt.Rows[0]["AllowState"]) == false)
                {
                    ImgAmeenAlsondoqPrisms.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["IDAmeenAlsondoq"]), Convert.ToBoolean(dt.Rows[0]["NotAllowState"]));
                    ImgAmeenAlsondoqPrisms.Width = 100;
                    ImgAmeenAlsondoqPrisms.Visible = true;
                    lblDateAllowOrNotAllowPrisms.Visible = true;
                    lblWhayNotAllowPrisms.Text = " - " + dt.Rows[0]["WhayNotAllow"].ToString();
                    lblWhayNotAllowPrisms.Visible = true;
                }
                else
                {
                    ImgAmeenAlsondoqPrisms.ImageUrl = "loaderMin.gif";
                    ImgAmeenAlsondoqPrisms.Width = 30;
                    lblDateAllowOrNotAllowPrisms.Visible = false;
                }

                lblDateAllowOrNotAllowPrisms.Text = "بتاريخ / " + Convert.ToDateTime(dt.Rows[0]["Date_AllowOrNotAllow"]).ToString("yyyy/MM/dd");
                if (dt.Rows[0]["A3"].ToString() != string.Empty)
                {
                    //DivNote.Visible = true;
                    lblNote.Text = dt.Rows[0]["A3"].ToString();
                }
                else
                    //DivNote.Visible = false;
                if (dt.Rows[0]["Name_Initiatives_"].ToString() != "بدون مبادرة")
                    lbl_Initiatives.Text = dt.Rows[0]["Name_Initiatives_"].ToString();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {

    }

}