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

public partial class Cpanel_PScanF : System.Web.UI.Page
{
    string XID;
    string IDUser, IDUniq;
    private void GetCookie()
    {
        try
        {
            HttpCookie Cooke;  // رقم المستخدم
            Cooke = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
            IDUser = ClassSaddam.UnprotectPassword(Cooke["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
        }
        catch
        {
            Response.Redirect("PageNotAccess.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        ClassAdmin_Arn CAA = new ClassAdmin_Arn();
        CAA._IDUniq = IDUniq;
        CAA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CAA.BArnAdminGetByIDUniq();
        if (dtViewProfil.Rows.Count > 0)
        {
            bool A61, A106;
            A61 = Convert.ToBoolean(dtViewProfil.Rows[0]["A61"]);
            A106 = Convert.ToBoolean(dtViewProfil.Rows[0]["A106"]);
            if (A61 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            if (A106 == false)
            {
                btnDelete1.Visible = false;
                GVMatterOfExchangeByID.Columns[0].Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlStar.Visible = true;
            FGetProjec();
            FGetSupportType();
            if (Request.QueryString["ID"] != null)
            {
                if (Request.QueryString["XID"] == "999999999")
                {
                    RPTalef.Checked = true;
                    pnlStar.Visible = false;
                    ProductByUser.Visible = false;
                    ProductByTalef.Visible = true;
                    pnlDataTalef.Visible = true;
                    pnlNullTalef.Visible = false;
                    txtSearchTalef.Text = Request.QueryString["ID"].ToString();
                    txtSearchTalef.Focus();
                    FArnProductShopMatterOfExchangeByTaleef(false, false, false, true);
                }
                else if (Request.QueryString["XID"] != null)
                {
                    if (Request.QueryString["ID"] != null)
                    {
                        txtSearch.Text = Request.QueryString["ID"].ToString();
                        txtSearchTalef.Text = Request.QueryString["ID"].ToString();
                        DLSupportType.SelectedValue = Convert.ToString(Request.QueryString["CT"]);
                        RBCart.Checked = Convert.ToBoolean(Request.QueryString["C"]);
                        RBDevice.Checked = Convert.ToBoolean(Request.QueryString["D"]);
                        RBTath.Checked = Convert.ToBoolean(Request.QueryString["T"]);
                        RPTalef.Checked = Convert.ToBoolean(Request.QueryString["Ta"]);
                        DataTable dt = new DataTable();
                        dt = ClassDataAccess.GetData("SELECT Top(5) [_IDItem],[_billNumber],[_IDMosTafeed] FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber = @0 And _IsDelete = @1 And _IsCart = @2 And _IsDevice = @3 And _IsTathith = @4 And _IsTalef = @5", Request.QueryString["ID"].ToString(), Convert.ToString(false), Convert.ToString(RBCart.Checked), Convert.ToString(RBDevice.Checked), Convert.ToString(RBTath.Checked), Convert.ToString(RPTalef.Checked));
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dt.Rows[0]["_IDMosTafeed"]) != 999999999)
                            {
                                RBTathith.Checked = true;
                                pnlStar.Visible = false;
                                ProductByUser.Visible = true;
                                pnlDataSarf.Visible = false;
                                pnlNullSarf.Visible = true;
                                ProductByTalef.Visible = false;
                                txtSearch.Focus();
                                Session["XID"] = Convert.ToInt32(dt.Rows[0]["_IDMosTafeed"]);
                                //RBCart.Checked = Convert.ToBoolean(Request.QueryString["C"]);
                                //RBDevice.Checked = Convert.ToBoolean(Request.QueryString["D"]);
                                //RBTath.Checked = Convert.ToBoolean(Request.QueryString["T"]);
                                FArnProductShopMatterOfExchangeByUser(Convert.ToInt32(Session["XID"]), Convert.ToInt64(DLSupportType.SelectedValue)
                                    , Convert.ToBoolean(Request.QueryString["C"]), Convert.ToBoolean(Request.QueryString["D"])
                                    , Convert.ToBoolean(Request.QueryString["T"]), Convert.ToBoolean(Request.QueryString["Ta"]));
                            }
                            else
                            {
                                if (Convert.ToInt32(dt.Rows[0]["_IDMosTafeed"]) == 999999999)
                                {
                                    RPTalef.Checked = true;
                                    pnlStar.Visible = false;
                                    ProductByUser.Visible = false;
                                    ProductByTalef.Visible = true;
                                    pnlDataTalef.Visible = true;
                                    pnlNullTalef.Visible = false;
                                    txtSearchTalef.Focus();
                                    FArnProductShopMatterOfExchangeByTaleef(false, false, false, true);
                                }
                            }
                        }
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
                FArnProductShopMatterOfExchangeByUserHouser(Convert.ToInt32(txtSearchTarmim.Text.Trim()), Convert.ToBoolean(Request.QueryString["IsBena"]), Convert.ToBoolean(Request.QueryString["IsTarmem"]));
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
                FArnSupportForPrismsByBillPrisms(Convert.ToInt32(txtSearchPrisms.Text.Trim()), Convert.ToInt32(DLProject.SelectedValue));
            }
        }
    }

    private void FGetSupportType()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where TypeAlDam <> @0 And TypeAlDam <> @1 And TypeAlDam <> @2 And TypeAlDam <> @3 And TypeAlDam <> @4 And IsDeleteTypeAlDam = @5 Order by IDItem", string.Empty, "بناء منازل", "ترميم منازل", "تاثيث منازل", "التالف", Convert.ToString(false));
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
                IDBarcode.ImageUrl = "http://chart.apis.google.com/chart?cht=qr&chl=" + ClassSetting.FGetNameServer() +
                    "/Cpanel/PScanF.aspx?ID=" + txtSearch.Text.Trim() + "&XID=" + IDMostafeed.ToString() + "&chs=75";
                GVMatterOfExchangeByID.DataSource = dt;
                GVMatterOfExchangeByID.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNullSarf.Visible = false;
                pnlDataSarf.Visible = true;
                ProductByTalef.Visible = false;
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
                    ImgModer.ImageUrl =  ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDModer"]), Convert.ToBoolean(dt.Rows[0]["_IsModer"]));
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT [_IDItem],[_billNumber],[_IDMosTafeed] FROM [dbo].[ProductShopWarehouse] With(NoLock) Where _billNumber = @0 And _IsDelete = @1 And _IsCart = @2 And _IsDevice = @3 And _IsTathith = @4 And _IsTalef = @5", txtSearch.Text.Trim(), Convert.ToString(false), Convert.ToString(RBCart.Checked), Convert.ToString(RBDevice.Checked), Convert.ToString(RBTath.Checked), Convert.ToString(RPTalef.Checked));
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToInt32(dt.Rows[0]["_IDMosTafeed"]) != 999999999)
            {
                if (RBCart.Checked == false && RBDevice.Checked == false && RBTath.Checked == false)
                {
                    lbmsg.Text = "يجب تحديد نوع الفرز ";
                    lbmsg.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    lbmsg.Text = "قائمة فاتورة أمر صرف سلة ";
                    lbmsg.ForeColor = System.Drawing.Color.Black;
                    Session["XID"] = Convert.ToInt32(dt.Rows[0]["_IDMosTafeed"]);
                    FArnProductShopMatterOfExchangeByUser(Convert.ToInt32(Session["XID"]), Convert.ToInt64(DLSupportType.SelectedValue), RBCart.Checked, RBDevice.Checked, RBTath.Checked, false);
                }
            }
            else
            {
                if (Convert.ToInt32(dt.Rows[0]["_IDMosTafeed"]) == 999999999)
                {
                    FArnProductShopMatterOfExchangeByTaleef(false, false, false, true);
                }
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

    private void FGetProjec()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where TypeAlDam <> @0 And (TypeAlDam = @1 Or TypeAlDam = @2 Or TypeAlDam = @3 Or TypeAlDam = @4 Or TypeAlDam = @5) And IsDeleteTypeAlDam = @6 Order by IDItem", string.Empty, "دعم مالي ( زكاة المال )", "دعم مالي عام", "دعم مالي ( رعاية الايتام )", "دعم مالي ( تفريج كربة )", "دعم مالي ( مشروع الحج )", Convert.ToString(false));
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

    protected void LbRefreshSaraf_Click(object sender, EventArgs e)
    {
        GVMatterOfExchangeByID.Columns[0].Visible = true;
        FArnProductShopMatterOfExchangeByUser(Convert.ToInt32(Session["XID"]), Convert.ToInt64(DLSupportType.SelectedValue), RBCart.Checked, RBDevice.Checked, RBTath.Checked, false);
        pnllblPrint.Visible = false;
        pnlDlPrint.Visible = true;
    }

    protected void LBPrintSaraf_Click(object sender, EventArgs e)
    {
        GVMatterOfExchangeByID.Columns[0].Visible = false;
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

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVMatterOfExchangeByID.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVMatterOfExchangeByID.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ProductShopWarehouse] SET [_IsDelete] = @_IsDelete WHERE _IDItem = @_IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@_IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@_IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                    DataTable dtResult = new DataTable();
                    dtResult = ClassDataAccess.GetData("Select _IDItem,_IDProduct From ProductShopWarehouse Where _IDItem = @0", Comp_ID);
                    if (dtResult.Rows.Count > 0)
                    {
                        float XSumation = 0;
                        FSetSum(Convert.ToInt32(dtResult.Rows[0]["_IDProduct"]));
                        FGetSum(Convert.ToInt32(dtResult.Rows[0]["_IDProduct"]));
                        XSumation = Setsum - Getsum;
                        SqlConnection conn2 = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                        conn2.Open();
                        string sql2 = "UPDATE [dbo].[ProductShop] SET [CountProduct] = @CountProduct WHERE ProductID = @ProductID";
                        SqlCommand cmd2 = new SqlCommand(sql2, conn2);
                        cmd2.Parameters.AddWithValue("@ProductID", Convert.ToInt64(Convert.ToInt32(dtResult.Rows[0]["_IDProduct"])));
                        cmd2.Parameters.AddWithValue("@CountProduct", Convert.ToInt64(XSumation));
                        cmd2.ExecuteScalar();
                        conn2.Close();

                        string XCategory = Convert.ToString((row.FindControl("lblCategory") as Label).Text);
                        if (XCategory == "الأجهزة الكهربائية")
                        {
                            string XID = Convert.ToString((row.FindControl("lblCount") as Label).Text);
                            F(Convert.ToInt32(dtResult.Rows[0]["_IDProduct"]), Convert.ToInt32(XID));
                        }
                    }
                }
            }
            FArnProductShopMatterOfExchangeByUser(Convert.ToInt32(Session["XID"]), Convert.ToInt64(DLSupportType.SelectedValue), RBCart.Checked, RBDevice.Checked, RBTath.Checked, false);
        }
        catch (Exception)
        {
            return;
        }
    }

    private void F(int IDDevice, int XID)
    {
        DataTable dtDevice = new DataTable();
        dtDevice = ClassDataAccess.GetData("SELECT * FROM [dbo].[ReportAlZyaratElectricalAppliances] Where IDDevice = @0 And IDMustafeed = @1 And IsDelete = @2", Convert.ToString(IDDevice), txtNumberMostafeed2.Text.Trim(), Convert.ToString(false));
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

    private void FArnProductShopMatterOfExchangeByTaleef(bool Cart, bool Device, bool Tathith, bool Talef)
    {
        try
        {
            IDBarcodeTalef.ImageUrl = "http://chart.apis.google.com/chart?cht=qr&chl=" + ClassSetting.FGetNameServer() +
                    "/Cpanel/PScanF.aspx?ID=" + txtSearchTalef.Text.Trim() + "&XID=" + 999999999 + "&chs=75";
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = Convert.ToInt32(txtSearchTalef.Text.Trim());
            CPS.IsDelete = false;
            CPS.IsCart = Cart;
            CPS.IsDevice = Device;
            CPS.IsTathith = Tathith;
            CPS.IsTalef = Talef;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopMatterOfExchangeByTaleef();
            if (dt.Rows.Count > 0)
            {
                lblToday.Text = ClassDataAccess.GetCurrentTime().ToString("ddd");
                lblDateToDay.Text = ClassDataAccess.GetCurrentTime().ToString("dd/MM/yyyy");
                GVMatterOfExchangeByIDTaleef.DataSource = dt;
                GVMatterOfExchangeByIDTaleef.DataBind();
                lblCountTaleef.Text = Convert.ToString(dt.Rows.Count);
                pnlNullTalef.Visible = false;
                pnlDataTalef.Visible = true;
                ProductByTalef.Visible = true;
                ProductByUser.Visible = false;
                lblNumberTaleef.Text = " " + dt.Rows[0]["_billNumber"].ToString();

                lblDataEntryTaleef.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDAdmin"]));
                lblDateEntryTaleef.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_DateAddProduct"]));
                if (Convert.ToInt32(dt.Rows[0]["_IDUpdate"]) != 0)
                {
                    IDUpdateTaleef.Visible = true;
                    lblDataEntryEditTaleef.Text = ClassQuaem.FAlBahethByEdit(Convert.ToInt32(dt.Rows[0]["_IDUpdate"]));
                    lblDateEntryEditTaleef.Text = ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_DateUpDate"]));
                }
                else if (Convert.ToInt32(dt.Rows[0]["_IDUpdate"]) == 0)
                {
                    IDUpdateTaleef.Visible = false;
                }
                lblDateHideTaleef.Text = Convert.ToDateTime(dt.Rows[0]["_ProductionDate"]).ToString("yyyy/MM/dd") + "مـ - ";
                lblDateHideTaleef.Text += ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["_ProductionDate"])) + "هـ";
                //txtTitleTalef.Text = ClassSaddam.FAlTypeEvint(Convert.ToInt32(dt.Rows[0]["_IDType"])) + "(المنتج " + ClassQuaem.FSupportType(Convert.ToInt32(dt.Rows[0]["_IDCategory"])) + ")";
                txtTitleTalef.Text = "عقد حصر وإتلاف";
                if (Convert.ToBoolean(dt.Rows[0]["_IsAmmenAlSondoq"]) && Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah"]) && Convert.ToBoolean(dt.Rows[0]["_IsModer"]) && Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]) && Convert.ToBoolean(dt.Rows[0]["_IsReceived"]))
                {
                    IDKhatmTaleef.Visible = true;
                }

                lblRaees.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDRaeesMaglisAlEdarah"]));
                lblNaeeb.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDNaebRaees"]));
                lblAmeen.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDStorekeeper"]));
                if (Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah"]))
                {
                    IDRaees.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDRaeesMaglisAlEdarah"]), Convert.ToBoolean(dt.Rows[0]["_IsRaeesMaglisAlEdarah"]));
                    IDRaees.Width = 100;
                    IDRaees.Visible = true;
                    IDKhatmTaleef.Visible = true;
                }
                else
                {
                    IDRaees.ImageUrl = "loaderMin.gif";
                    IDRaees.Width = 30;
                    IDRaees.Visible = true;
                }

                if (Convert.ToBoolean(dt.Rows[0]["_IsNaebRaees"]))
                {
                    IDNeeb.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDNaebRaees"]), Convert.ToBoolean(dt.Rows[0]["_IsNaebRaees"]));
                    IDNeeb.Width = 100;
                    IDNeeb.Visible = true;
                }
                else
                {
                    IDNeeb.ImageUrl = "loaderMin.gif";
                    IDNeeb.Width = 30;
                    IDNeeb.Visible = true;
                }

                if (Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]))
                {
                    IDAmeen.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_IDStorekeeper"]), Convert.ToBoolean(dt.Rows[0]["_IsStorekeeper"]));
                    IDAmeen.Width = 100;
                    IDAmeen.Visible = true;
                }
                else
                {
                    IDAmeen.ImageUrl = "loaderMin.gif";
                    IDAmeen.Width = 30;
                    IDAmeen.Visible = true;
                }
                pnlStar.Visible = false;

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblTotalPriceTaleef.Text), currencies[Convert.ToInt32(0)]);
                lblSumTalef.Text = toWord.ConvertToArabic();
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
        dt = ClassDataAccess.GetData("SELECT Sum([_CountProduct]) As 'Get' FROM [dbo].[ProductShopWarehouse] Where _IDProduct = @0 And _billNumber <> @1 And _IsDelete = @2", Convert.ToString(XID), Convert.ToString(0), Convert.ToString(false));
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
        dt = ClassDataAccess.GetData("SELECT Sum([_CountProduct]) As 'Set' FROM [dbo].[ProductShopWarehouse] Where _IDProduct = @0 And _billNumber = @1 And _IsDelete = @2", Convert.ToString(XID), Convert.ToString(0), Convert.ToString(false));
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

    protected void btnSearchTalef_Click(object sender, EventArgs e)
    {
        FArnProductShopMatterOfExchangeByTaleef(false, false, false, true);
    }

    protected void LBRefresh_Click(object sender, EventArgs e)
    {
        GVMatterOfExchangeByIDTaleef.Columns[0].Visible = true;
        FArnProductShopMatterOfExchangeByTaleef(false, false, false, true);
        lblDateHideTaleef.Visible = false;
    }

    protected void LbPrintTaleef_Click(object sender, EventArgs e)
    {
        GVMatterOfExchangeByIDTaleef.Columns[0].Visible = false;
        lblDateHideTaleef.Visible = true;
        Session["footable1"] = pnlDataTalef;
        if (GVMatterOfExchangeByIDTaleef.Rows.Count > 15)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintPage.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        else if (GVMatterOfExchangeByIDTaleef.Rows.Count <= 15)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintPage.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
    }

    protected void btnDeleteTaleef_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVMatterOfExchangeByIDTaleef.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVMatterOfExchangeByIDTaleef.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ProductShopWarehouse] SET [_IsDelete] = @_IsDelete WHERE _IDItem = @_IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@_IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@_IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();

                    DataTable dtResult = new DataTable();
                    dtResult = ClassDataAccess.GetData("Select _IDItem,_IDProduct From ProductShopWarehouse Where _IDItem = @0", Comp_ID);
                    if (dtResult.Rows.Count > 0)
                    {
                        float XSumation = 0;
                        FSetSum(Convert.ToInt32(dtResult.Rows[0]["_IDProduct"]));
                        FGetSum(Convert.ToInt32(dtResult.Rows[0]["_IDProduct"]));
                        XSumation = Setsum - Getsum;
                        SqlConnection conn2 = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                        conn2.Open();
                        string sql2 = "UPDATE [dbo].[ProductShop] SET [CountProduct] = @CountProduct WHERE ProductID = @ProductID";
                        SqlCommand cmd2 = new SqlCommand(sql2, conn2);
                        cmd2.Parameters.AddWithValue("@ProductID", Convert.ToInt64(Convert.ToInt32(dtResult.Rows[0]["_IDProduct"])));
                        cmd2.Parameters.AddWithValue("@CountProduct", Convert.ToInt64(XSumation));
                        cmd2.ExecuteScalar();
                        conn2.Close();
                    }
                }
            }
            FArnProductShopMatterOfExchangeByTaleef(false, false, false, true);
        }
        catch (Exception)
        {
            return;
        }
    }

    int CouTaleef = 0;
    decimal sumTaleef = 0;
    float GetsumTaleef, SetsumTaleef = 0;

    protected void GVMatterOfExchangeByIDTaleef_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label Count = (Label)e.Row.FindControl("lblCountTaleef");//take lable id
            CouTaleef += int.Parse(Count.Text);
            lblSumTaleef.Text = CouTaleef.ToString();

            Label salary = (Label)e.Row.FindControl("lblCountTotalPriceTaleef");//take lable id
            sumTaleef += decimal.Parse(salary.Text);
            if (sumTaleef != 0)
            {
                lblTotalPriceTaleef.Text = sumTaleef.ToString();
            }
            else
            {
                lblTotalPriceTaleef.Text = "بإنتظار التسعير";
            }
        }
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
                lblmsg.ForeColor = System.Drawing.Color.Black;
                IDBarcodeTarmim.ImageUrl = "http://chart.apis.google.com/chart?cht=qr&chl=" + ClassSetting.FGetNameServer() +
                        "/Cpanel/PScanF.aspx?IDX=" + txtSearchTarmim.Text.Trim() + "&XID=" + dt.Rows[0]["NumberMostafeed"].ToString() + "&chs=75";

                pnlNullTarmim.Visible = false;
                pnlDataTarmim.Visible = true;
                ProductByTalef.Visible = false;
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
                    CBShayk_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["IsShayk_Bank"]);
                    lblNumber_Shayk_Bank.Visible = false;
                }
                else
                {
                    CBCash_Money_.Checked = Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]);
                    CBShayk_Bank.Checked = Convert.ToBoolean(dt.Rows[0]["IsShayk_Bank"]);
                    lblNumber_Shayk_Bank.Text = " / رقم الشيك : " + dt.Rows[0]["Number_Shayk_Bank"].ToString();
                    lblNumber_Shayk_Bank.Visible = true;
                }
                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Get"]).ToString("yyyy/MM/dd");
                lblProject.Text = FChangeTitle(ClassQuaem.FSupportType(Convert.ToInt32(dt.Rows[0]["ID_Type"])));
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
                    ImgAmeenAlsondoqTarmim.ImageUrl = "loader.gif";
                    ImgAmeenAlsondoqTarmim.Width = 30;
                    lblDateAllowOrNotAllow.Visible = false;
                }

                lblDateAllowOrNotAllow.Text = "بتاريخ / " + Convert.ToDateTime(dt.Rows[0]["Date_AllowOrNotAllow"]).ToString("yyyy/MM/dd");
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
        Session["foot"] = pnlDataTarmim;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void btnSearchPrisms_Click(object sender, EventArgs e)
    {
        FArnSupportForPrismsByBillPrisms(Convert.ToInt32(txtSearchPrisms.Text.Trim()), Convert.ToInt32(DLProject.SelectedValue));
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
                lblmsgPrisms.ForeColor = System.Drawing.Color.Black;
                IDBarcodePrisms.ImageUrl = "http://chart.apis.google.com/chart?cht=qr&chl=" + ClassSetting.FGetNameServer() +
                        "/Cpanel/PScanF.aspx?IDS=" + billNumber.ToString() + "&IDCh=" + IDProject.ToString() + "&chs=75";

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
                    CBShayk_BankPrisms.Checked = Convert.ToBoolean(dt.Rows[0]["IsShayk_Bank"]);
                    lblNumber_Shayk_Bank_Prisms.Visible = false;
                }
                else
                {
                    CBCash_Money_Prisms.Checked = Convert.ToBoolean(dt.Rows[0]["IsCash_Money_"]);
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
        FArnSupportForPrismsByBillPrisms(Convert.ToInt32(txtSearchPrisms.Text.Trim()), Convert.ToInt32(DLProject.SelectedValue));
    }

    protected void LbPrintPrisms_Click(object sender, EventArgs e)
    {
        Session["foot"] = pnlDataPrisms;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

}