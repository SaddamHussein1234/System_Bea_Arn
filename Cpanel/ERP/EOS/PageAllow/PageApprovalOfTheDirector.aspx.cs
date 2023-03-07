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
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_EOS_PageAllow_PageApprovalOfTheDirector : System.Web.UI.Page
{
    public string XMony = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        XMony = ClassSaddam.FGetMonySa();
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A106", "A108", Button1, GVApprovalOfTheDirector, 0, 8);
            Button1.Visible = false;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FArnBenaaAndTarmimByModer();
            pnlSelect.Visible = true;
        }
    }

    private void FArnBenaaAndTarmimByModer()
    {
        lblCountCard.Text = WSM_Repostry_Exchange_Order_Bill_.FGetByCount_OtherStaticGeneral("Count_Cart_By_Order_Bill", new Guid(ddlYears.SelectedValue), 0, string.Empty, string.Empty, "0").ToString();
        lblCountHoseHear.Text = "0";
        GVApprovalOfTheDirectorHose.Columns[0].Visible = true;
        GVApprovalOfTheDirectorHose.Columns[11].Visible = true;
        GVApprovalOfTheDirectorHose.UseAccessibleHeader = false;

        Model_BenaaAndTarmim_ MBAT = new Model_BenaaAndTarmim_();
        MBAT.IDCheck = "GetByApprovalOfTheDirector";
        MBAT.IDUniq = Guid.Empty;
        MBAT.ID_FinancialYear = new Guid(ddlYears.SelectedValue);
        MBAT.ID_Donor = Guid.Empty;
        MBAT.NumberMostafeed = 0;
        MBAT.billNumber = 0;
        MBAT.ID_Project = 0;
        MBAT.Start_Date = string.Empty;
        MBAT.End_Date = string.Empty;
        MBAT.DataCheck = string.Empty;
        MBAT.DataCheck2 = string.Empty;
        MBAT.DataCheck3 = string.Empty;
        MBAT.IsTarmem = false;
        MBAT.IsBena = false;
        MBAT.IsDelete = false;
        DataTable dt = new DataTable();
        Repostry_BenaaAndTarmim_ RBAT = new Repostry_BenaaAndTarmim_();
        dt = RBAT.BArn_BenaaAndTarmim_Manage(MBAT);

        if (dt.Rows.Count > 0)
        {
            txtTitleHose.Text = "إرشيف " + ddlYears.SelectedItem.ToString() + " , قائمة أوامر الصرف التي تحتاج إلى موافقة مدير الجمعية ( لدعم بناء المنازل - ترميم المنازل ) ";
            GVApprovalOfTheDirectorHose.DataSource = dt;
            GVApprovalOfTheDirectorHose.DataBind();
            lblCountHose.Text = Convert.ToString(dt.Rows.Count);
            lblCountHoseHear.Text = lblCountHose.Text + " ملف ";
            pnlNullHose.Visible = false;
            pnlDataHose.Visible = true;
        }
        else
        {
            pnlNullHose.Visible = true;
            pnlDataHose.Visible = false;
        }
        FArnSupportForPrismsByModer();
    }

    protected void RBTathith_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        if (RBTathith.Checked)
        {
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            IDHose.Visible = false;
            IDPrisms.Visible = false;
        }
    }

    protected void RBCardCheck_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FCheckSelect();
    }

    protected void RBDeviceCheck_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FCheckSelect();
    }

    protected void RBTathithCheck_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FCheckSelect();
    }

    protected void RBTalefCheck_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FCheckSelect();
    }

    private void FCheckSelect()
    {
        if (RBCardCheck.Checked)
        {
            RPTarmem.Checked = false;
            RPSupportForPrisms.Checked = false;
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            IDHose.Visible = false;
            IDPrisms.Visible = false;
            FArnProductShopByModer(true, false, false, false);
        }
        else if (RBDeviceCheck.Checked)
        {
            RPTarmem.Checked = false;
            RPSupportForPrisms.Checked = false;
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            IDHose.Visible = false;
            IDPrisms.Visible = false;
            FArnProductShopByModer(false, true, false, false);
        }
        else if (RBTathithCheck.Checked)
        {
            RPTarmem.Checked = false;
            RPSupportForPrisms.Checked = false;
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            IDHose.Visible = false;
            IDPrisms.Visible = false;
            FArnProductShopByModer(false, false, true, false);
        }
        else if (RBTalefCheck.Checked)
        {
            RPTarmem.Checked = false;
            RPSupportForPrisms.Checked = false;
            pnlSelect.Visible = false;
            IDCard.Visible = true;
            IDHose.Visible = false;
            IDPrisms.Visible = false;
            FArnProductShopByModer(false, false, false, true);
        }
    }

    private void FArnProductShopByModer(bool Cart, bool Device, bool Tathith, bool Talef)
    {
        try
        {
            GVApprovalOfTheDirector.UseAccessibleHeader = false;
            GVApprovalOfTheDirector.Columns[0].Visible = true;
            GVApprovalOfTheDirector.Columns[10].Visible = true;
            WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_();
            MEOB.IDCheck = "GetByApprovalOfTheDirectorWithShring";
            MEOB.ID_Item = Guid.Empty;
            MEOB.ID_FinancialYear = new Guid(ddlYears.SelectedValue);
            MEOB.ID_Donor = Guid.Empty;
            MEOB.bill_Number = 0;
            MEOB.ID_MosTafeed = 0;
            MEOB.Start_Date = string.Empty;
            MEOB.End_Date = string.Empty;
            MEOB.DataCheck = "0";
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
                txtTitle.Text = "إرشيف " + ddlYears.SelectedItem.ToString() + " , قائمة أوامر الصرف التي تحتاج إلى موافقة مدير الجمعية ( الدعم العيني ) ";
                GVApprovalOfTheDirector.DataSource = dt;
                GVApprovalOfTheDirector.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void RPTarmem_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        if (RPTarmem.Checked)
        {
            pnlSelect.Visible = false;
            IDCard.Visible = false;
            IDHose.Visible = true;
            IDPrisms.Visible = false;

            RBCardCheck.Checked = false;
            RBDeviceCheck.Checked = false;
            RBTathithCheck.Checked = false;
            RBTalefCheck.Checked = false;
        }
    }

    protected void RPSupportForPrisms_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        if (RPSupportForPrisms.Checked)
        {
            pnlSelect.Visible = false;
            IDCard.Visible = false;
            IDHose.Visible = false;
            IDPrisms.Visible = true;

            RBCardCheck.Checked = false;
            RBDeviceCheck.Checked = false;
            RBTathithCheck.Checked = false;
            RBTalefCheck.Checked = false;
        }
    }

    private void FArnSupportForPrismsByModer()
    {
        lblCountPrismsHear.Text = "0";
        GVApprovalOfTheDirectorPrisms.Columns[0].Visible = true;
        GVApprovalOfTheDirectorPrisms.Columns[11].Visible = true;
        GVApprovalOfTheDirectorPrisms.UseAccessibleHeader = false;

        Model_SupportForPrisms_ MSFP = new Model_SupportForPrisms_();
        MSFP.IDCheck = "GetByApprovalOfTheDirector";
        MSFP.IDUniq = Guid.Empty;
        MSFP.ID_FinancialYear = new Guid(ddlYears.SelectedValue);
        MSFP.ID_Donor = Guid.Empty;
        MSFP.NumberMostafeed = 0;
        MSFP.billNumber = 0;
        MSFP.ID_Project = 0;
        MSFP.Start_Date = string.Empty;
        MSFP.End_Date = string.Empty;
        MSFP.DataCheck = string.Empty;
        MSFP.DataCheck2 = string.Empty;
        MSFP.DataCheck3 = string.Empty;
        MSFP.IsDelete = false;
        DataTable dt = new DataTable();
        Repostry_SupportForPrisms_ RSFP = new Repostry_SupportForPrisms_();
        dt = RSFP.BArn_SupportForPrisms_Manage(MSFP);

        if (dt.Rows.Count > 0)
        {
            txtTitlePrisms.Text = "إرشيف " + ddlYears.SelectedItem.ToString() + " , قائمة أوامر الصرف التي تحتاج إلى موافقة مدير الجمعية ( الدعم المالي ) ";
            GVApprovalOfTheDirectorPrisms.DataSource = dt;
            GVApprovalOfTheDirectorPrisms.DataBind();
            lblCountPrisms.Text = Convert.ToString(dt.Rows.Count);
            lblCountPrismsHear.Text = lblCountPrisms.Text + " ملف ";
            pnlNullPrisms.Visible = false;
            pnlDataPrisms.Visible = true;
        }
        else
        {
            pnlNullPrisms.Visible = true;
            pnlDataPrisms.Visible = false;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FArnProductShopByModer(true, false, false, false);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVApprovalOfTheDirector.Columns[0].Visible = false;
            GVApprovalOfTheDirector.Columns[10].Visible = false;

            GVApprovalOfTheDirector.UseAccessibleHeader = true;
            GVApprovalOfTheDirector.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            foreach (GridViewRow row in GVApprovalOfTheDirector.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label BillID = (Label)row.FindControl("lblID");
                    FAllowModer(new Guid(BillID.Text.ToString()));
                }
            }
            System.Threading.Thread.Sleep(100);
            FArnProductShopByModer(RBCardCheck.Checked, RBDeviceCheck.Checked, RBTathithCheck.Checked, RBTalefCheck.Checked);
            FArnBenaaAndTarmimByModer();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FAllowModer(Guid XID)
    {
        WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_()
        {
            IDCheck = "ByDirector",
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
            Date_Moder_Allow = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            ID_Moder_Allow = Test_Saddam.FGetIDUsiq(),
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
            DeleteBy = 0,
            DeleteDate = string.Empty,
            IsActive = true,
            AlQaryah = 0
        };
        WSM_Repostry_Exchange_Order_Bill_ REOB = new WSM_Repostry_Exchange_Order_Bill_();
        string Xresult = REOB.FWSM_Exchange_Order_Bill_Add(MEOB);
        if (Xresult == "IsSuccessDirector")
        {
            IDMessageSuccess.Visible = true;
            IDMessageWarning.Visible = false;
            lblMessage.Text = "تم الموافقة على الملفات بنجاح ... ";
        }
    }

    decimal sum = 0;
    protected void GVApprovalOfTheDirector_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lblTotalPrice.Text = "0";
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

        }
    }

    protected void btnAllowHose_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            int XIDAdd = Test_Saddam.FGetIDUsiq();
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            foreach (GridViewRow row in GVApprovalOfTheDirectorHose.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label BillID = (Label)row.FindControl("lblIDHose");

                    Model_BenaaAndTarmim_ MBAT = new Model_BenaaAndTarmim_()
                    {
                        IDCheck = "ByDirector",
                        IDUniq = Guid.Empty,
                        ID_FinancialYear = Guid.Empty,
                        ID_Donor = Guid.Empty,
                        NumberMostafeed = 0,
                        billNumber = Convert.ToInt32(BillID.Text),
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
                        IsAllowModer = true,
                        IDModer_Allow = XIDAdd,
                        IDModer_Date_Allow = XDate,
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
                        DeleteBy = 0,
                        DeleteDate = string.Empty,
                        IsDelete = false,
                        Al_Qaryah = 0
                    };

                    Repostry_BenaaAndTarmim_ RBAT = new Repostry_BenaaAndTarmim_();
                    string Xresult = RBAT.FArn_BenaaAndTarmim_Add(MBAT);
                    if (Xresult == "IsSuccessDirector")
                    {
                        IDMessageSuccess.Visible = true;
                        IDMessageWarning.Visible = false;
                        lblMessage.Text = "تم الموافقة على الملفات بنجاح ... ";
                    }
                }
            }
            System.Threading.Thread.Sleep(100);
            FArnProductShopByModer(RBCardCheck.Checked, RBDeviceCheck.Checked, RBTathithCheck.Checked, RBTalefCheck.Checked);
            FArnBenaaAndTarmimByModer();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBRefrshHose_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        GVApprovalOfTheDirectorHose.Columns[0].Visible = false;
        GVApprovalOfTheDirectorHose.Columns[11].Visible = false;
        FArnBenaaAndTarmimByModer();
    }

    protected void LBPrintHose_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVApprovalOfTheDirectorHose.Columns[0].Visible = false;
            GVApprovalOfTheDirectorHose.Columns[11].Visible = false;
            GVApprovalOfTheDirectorHose.UseAccessibleHeader = true;
            GVApprovalOfTheDirectorHose.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlDataHose;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    decimal sumHose = 0;
    protected void GVApprovalOfTheDirectorHose_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lblTotalPriceHose.Text = "0";
                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sumHose += decimal.Parse(salary.Text);
                if (sumHose != 0)
                    lblTotalPriceHose.Text = sumHose.ToString();
                else
                    lblTotalPriceHose.Text = "بإنتظار التسعير";

                lblMonyHose.Text = ClassSaddam.FGetMonySa();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnPrisms_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            int XIDAdd = Test_Saddam.FGetIDUsiq();
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            foreach (GridViewRow row in GVApprovalOfTheDirectorPrisms.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label BillID = (Label)row.FindControl("lblIDPrisms");

                    Model_SupportForPrisms_ MSFP = new Model_SupportForPrisms_()
                    {
                        IDCheck = "ByDirector",
                        IDUniq = Guid.Empty,
                        ID_FinancialYear = Guid.Empty,
                        ID_Donor = Guid.Empty,
                        NumberMostafeed = 0,
                        billNumber = Convert.ToInt32(BillID.Text),
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
                        IsAllowModer = true,
                        IDModer_Allow = XIDAdd,
                        IDModer_Date_Allow = XDate,
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
                        DeleteBy = 0,
                        DeleteDate = string.Empty,
                        IsDelete = true,
                        Al_Qaryah = 0
                    };

                    Repostry_SupportForPrisms_ RSFP = new Repostry_SupportForPrisms_();
                    string Xresult = RSFP.FArn_SupportForPrisms_Add(MSFP);
                    if (Xresult == "IsSuccessDirector")
                    {
                        IDMessageSuccess.Visible = true;
                        IDMessageWarning.Visible = false;
                        lblMessage.Text = "تم الموافقة على الملفات بنجاح ... ";
                    }
                }
            }
            System.Threading.Thread.Sleep(100);
            lblCountCard.Text = WSM_Repostry_Exchange_Order_Bill_.FGetByCount_OtherStaticGeneral("Count_Cart_By_Order_Bill", new Guid(ddlYears.SelectedValue), 0, string.Empty, string.Empty, "0").ToString();
            FArnProductShopByModer(RBCardCheck.Checked, RBDeviceCheck.Checked, RBTathithCheck.Checked, RBTalefCheck.Checked);
            FArnBenaaAndTarmimByModer();
            FArnSupportForPrismsByModer();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrshPrisms_Click(object sender, EventArgs e)
    {
        FArnSupportForPrismsByModer();
    }

    protected void btnPrintPrisms_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVApprovalOfTheDirectorPrisms.Columns[0].Visible = false;
            GVApprovalOfTheDirectorPrisms.Columns[11].Visible = false;

            GVApprovalOfTheDirectorPrisms.UseAccessibleHeader = true;
            GVApprovalOfTheDirectorPrisms.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlDataPrisms;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    decimal sumPrisms = 0;
    protected void GVApprovalOfTheDirectorPrisms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                lblTotalPricePrisms.Text = "0";
                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sumPrisms += decimal.Parse(salary.Text);
                if (sumPrisms != 0)
                    lblTotalPricePrisms.Text = sumPrisms.ToString();
                else
                    lblTotalPricePrisms.Text = "بإنتظار التسعير";

                lblMonyPrisms.Text = ClassSaddam.FGetMonySa();
            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FArnBenaaAndTarmimByModer();
    }

    protected void LBRefrsh_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageApprovalOfTheDirector.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            foreach (GridViewRow row in GVApprovalOfTheDirector.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label XID = (Label)row.FindControl("lblID");
                    FDeleteBill(new Guid(XID.Text.ToString()));
                }
            }
        }
        catch (Exception)
        {
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
            FArnProductShopByModer(RBCardCheck.Checked, RBDeviceCheck.Checked, RBTathithCheck.Checked, RBTalefCheck.Checked);
        }
    }

    protected void GVApprovalOfTheDirector_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVApprovalOfTheDirector.PageIndex = e.NewPageIndex;
        this.FCheckSelect();
    }

}