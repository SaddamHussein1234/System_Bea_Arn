using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ClassOutEntity;
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

public partial class Cpanel_ERP_EOS_In_Kind_Donation_PageSupportByBeneficiaryMulti : System.Web.UI.Page
{
    public string XMony = string.Empty;
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
            ClassQuaem.FGetSupportType(1, "'1','2','3'", DLCategory);
            ClassQuaem.FGetSupportType(1, "'4'", DLCategoryTarmem);
            ClassQuaem.FGetSupportType(1, "'5'", DLCategoryPrisms);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        XMony = ClassSaddam.FGetMonySa();
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A60");
            CheckAccountAdmin();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            ddlYears.SelectedValue = Request.QueryString["IDYear"];
            pnlSelect.Visible = true;
            pnlSelectTarmem.Visible = true;
            pnlSelectPrisms.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            txtDateFromTarmem.Text = txtDateFrom.Text;
            txtDateToTarmem.Text = txtDateTo.Text;
            txtDateFromPrisms.Text = txtDateFrom.Text;
            txtDateToPrisms.Text = txtDateTo.Text;

            if (Request.QueryString["XID"] != null)
            {
                DLCategory.SelectedValue = Request.QueryString["XIDCate"];
                txtDateFrom.Text = Request.QueryString["XIDFrom"];
                txtDateTo.Text = Request.QueryString["XIDTo"];
                RBTathith.Checked = true;

                Panel1.Visible = false;
                IDTathith.Visible = true;
                IDTarmem.Visible = false;
                IDPrisms.Visible = false;
                FGetSupportByBeneficiaryMulti();
            }
            else if (Request.QueryString["TIDX"] != null)
            {
                DLCategoryTarmem.SelectedValue = Request.QueryString["XIDCate"];
                txtDateFromTarmem.Text = Request.QueryString["XIDFrom"];
                txtDateToTarmem.Text = Request.QueryString["XIDTo"];
                RPTarmem.Checked = true;
                Panel1.Visible = false;
                IDTathith.Visible = false;
                IDTarmem.Visible = true;
                IDPrisms.Visible = false;
                FGetByMostafeedTarmem();
            }
            else if (Request.QueryString["SIDX"] != null)
            {
                DLCategoryPrisms.SelectedValue = Request.QueryString["XIDCate"];
                txtDateFromPrisms.Text = Request.QueryString["XIDFrom"];
                txtDateToPrisms.Text = Request.QueryString["XIDTo"];
                RPSupportForPrisms.Checked = true;
                Panel1.Visible = false;
                IDTathith.Visible = false;
                IDTarmem.Visible = false;
                IDPrisms.Visible = true;
                FGetByMostafeedPrisms();
            }
            else
            {
                return;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        GVExchangeOrders.UseAccessibleHeader = false;
        GVExchangeOrders.Columns[0].Visible = true;
        GVExchangeOrders.Columns[10].Visible = true;
        if (DLType.Text != string.Empty)
        {
            lblType.Visible = false;
            if (DLCategory.Text != string.Empty)
            {
                lblCategory.Visible = false;
                if (txtDateFrom.Text != string.Empty)
                {
                    lblDateFrom.Visible = false;
                    if (txtDateTo.Text != string.Empty)
                    {
                        // Write Code Hear
                        FGetSupportByBeneficiaryMulti();
                        System.Threading.Thread.Sleep(500);
                    }
                    else if (txtDateTo.Text == string.Empty)
                        lblDateTo.Visible = true;
                }
                else if (txtDateFrom.Text == string.Empty)
                    lblDateFrom.Visible = true;
            }
            else if (DLCategory.Text == string.Empty)
                lblCategory.Visible = true;
        }
        else if (DLType.Text == string.Empty)
            lblType.Visible = true;
    }

    private void FGetSupportByBeneficiaryMulti()
    {
        try
        {
            WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_();
            MEOB.IDCheck = "GetSupportByBeneficiaryMultiWithShring";
            MEOB.ID_Item = Guid.Empty;
            MEOB.ID_FinancialYear = new Guid(ddlYears.SelectedValue);
            MEOB.ID_Donor = Guid.Empty;
            MEOB.bill_Number = Convert.ToInt32(DLCategory.SelectedValue);
            MEOB.ID_MosTafeed = 0;
            MEOB.Start_Date = txtDateFrom.Text.Trim();
            MEOB.End_Date = txtDateTo.Text.Trim();
            MEOB.DataCheck = DLType.SelectedValue;
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
                GVExchangeOrders.DataSource = dt;
                GVExchangeOrders.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                txtTitle.Text = "قائمة إرشيف " + ddlYears.SelectedItem.ToString() + " من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim() + " لمشروع " + DLCategory.SelectedItem.ToString();

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

    protected void LBR_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageSupportByBeneficiaryMulti.aspx");
    }

    protected void RBTathith_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        if (RBTathith.Checked)
        {
            Panel1.Visible = false;
            IDTathith.Visible = true;
            IDTarmem.Visible = false;
            IDPrisms.Visible = false;
        }
    }

    protected void RPTarmem_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        if (RPTarmem.Checked)
        {
            Panel1.Visible = false;
            IDTathith.Visible = false;
            IDTarmem.Visible = true;
            IDPrisms.Visible = false;
        }
    }

    protected void RPSupportForPrisms_CheckedChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        if (RPSupportForPrisms.Checked)
        {
            Panel1.Visible = false;
            IDTathith.Visible = false;
            IDTarmem.Visible = false;
            IDPrisms.Visible = true;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageSupportByBeneficiaryMulti.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVExchangeOrders.Columns[0].Visible = false;
            GVExchangeOrders.Columns[10].Visible = false;

            GVExchangeOrders.UseAccessibleHeader = true;
            GVExchangeOrders.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnPrintMulti_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string XIDBill = string.Empty, XIDName = string.Empty;
            foreach (GridViewRow row in GVExchangeOrders.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label XID = (Label)row.FindControl("lblID");
                    Label BillID = (Label)row.FindControl("lblIDBill");
                    Label YearID = (Label)row.FindControl("lblIDYear");
                    Label ProjectID = (Label)row.FindControl("lblIDProject");
                    Label NameID = (Label)row.FindControl("lblName");
                    XIDBill += BillID.Text + ","; XIDName += NameID.Text + ",";
                }
            }
            Session["XName"] = XIDName.Substring(0, XIDName.Length - 1);
            Response.Redirect("../PrintMultiCart.aspx?Type=Cart&IDUniq=" + ddlYears.SelectedValue + "&Name=" + DLCategory.SelectedItem.ToString() + "&XIDCate=" +
                DLCategory.SelectedValue + "&IDBill=" + XIDBill.Substring(0, XIDBill.Length - 1));

        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetByMostafeedTarmem()
    {
        try
        {

            Model_BenaaAndTarmim_ MBAT = new Model_BenaaAndTarmim_();
            MBAT.IDCheck = "GetByMostafeedMulti";
            MBAT.IDUniq = Guid.Empty;
            MBAT.ID_FinancialYear = new Guid(ddlYears.SelectedValue);
            MBAT.ID_Donor = Guid.Empty;
            MBAT.NumberMostafeed = 0;
            MBAT.billNumber = 0;
            MBAT.ID_Project = Convert.ToInt32(DLCategoryTarmem.SelectedValue);
            MBAT.Start_Date = txtDateFromTarmem.Text.Trim();
            MBAT.End_Date = txtDateToTarmem.Text.Trim();
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
                GVExchangeOrdersTarmem.DataSource = dt;
                GVExchangeOrdersTarmem.DataBind();
                lblCountTarmem.Text = Convert.ToString(dt.Rows.Count);
                pnlNullTarmem.Visible = false;
                pnlDataTarmem.Visible = true;
                pnlSelectTarmem.Visible = false;
                txtTitleTarmem.Text = "قائمة إرشيف " + ddlYears.SelectedItem.ToString() + " من تاريخ " + txtDateFromTarmem.Text.Trim() + " إلى تاريخ " + txtDateToTarmem.Text.Trim() + " لمشروع " + DLCategoryTarmem.SelectedItem.ToString();
            }
            else
            {
                pnlNullTarmem.Visible = true;
                pnlDataTarmem.Visible = false;
                pnlSelectTarmem.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    int Cou = 0;
    decimal sum = 0;
    protected void GVExchangeOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label Count = (Label)e.Row.FindControl("lblCount");//take lable id
                //Cou += int.Parse(Count.Text);
                //lblSum.Text = Cou.ToString();

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

    public string FGetProject()
    {
        string XResult = DLCategory.SelectedItem.ToString();
        return XResult;
    }

    public string FGetProjectTarmem()
    {
        string XResult = DLCategoryTarmem.SelectedItem.ToString();
        return XResult;
    }

    public string FGetProjectPrisms()
    {
        string XResult = DLCategoryPrisms.SelectedItem.ToString();
        return XResult;
    }

    protected void LBRefreshTarmem_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetByTarmem();
    }

    protected void LBPrintTarmem_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVExchangeOrdersTarmem.Columns[0].Visible = false;
            GVExchangeOrdersTarmem.Columns[11].Visible = false;

            GVExchangeOrdersTarmem.UseAccessibleHeader = true;
            GVExchangeOrdersTarmem.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlDataTarmem;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnDeleteTarmem_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty;
            int XIDAdd = Test_Saddam.FGetIDUsiq();
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            foreach (GridViewRow row in GVExchangeOrdersTarmem.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label XID = (Label)row.FindControl("lblIDTarmem");
                    Model_BenaaAndTarmim_ MBAT = new Model_BenaaAndTarmim_()
                    {
                        IDCheck = "Delete",
                        IDUniq = Guid.Empty,
                        ID_FinancialYear = Guid.Empty,
                        ID_Donor = Guid.Empty,
                        NumberMostafeed = 0,
                        billNumber = Convert.ToInt32(XID.Text),
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
                        DeleteBy = XIDAdd,
                        DeleteDate = XDate,
                        IsDelete = true,
                        Al_Qaryah = 0
                    };

                    Repostry_BenaaAndTarmim_ RBAT = new Repostry_BenaaAndTarmim_();
                    Xresult = RBAT.FArn_BenaaAndTarmim_Add(MBAT);
                }
            }
            if (Xresult == "IsSuccessDelete")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblMessage.Text = "تم حذف الفاتورة بنجاح ... ";
                FGetByTarmem();
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetByTarmem()
    {
        GVExchangeOrdersTarmem.UseAccessibleHeader = false;
        GVExchangeOrdersTarmem.Columns[0].Visible = true;
        GVExchangeOrdersTarmem.Columns[11].Visible = true;
        if (DLTypeTarmem.Text != string.Empty)
        {
            lblTypeTarmem.Visible = false;
            if (DLCategoryTarmem.Text != string.Empty)
            {
                lblCategoryTarmem.Visible = false;
                if (txtDateFromTarmem.Text != string.Empty)
                {
                    lblDateFromTarmem.Visible = false;
                    if (txtDateToTarmem.Text != string.Empty)
                    {
                        // Write Code Hear
                        FGetByMostafeedTarmem();
                        System.Threading.Thread.Sleep(500);
                    }
                    else if (txtDateToTarmem.Text == string.Empty)
                        lblDateToTarmem.Visible = true;
                }
                else if (txtDateFromTarmem.Text == string.Empty)
                    lblDateFromTarmem.Visible = true;
            }
            else if (DLCategoryTarmem.Text == string.Empty)
                lblCategoryTarmem.Visible = true;
        }
        else if (DLTypeTarmem.Text == string.Empty)
            lblTypeTarmem.Visible = true;
    }

    protected void btnSearchTarmem_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetByTarmem();
    }

    protected void GVExchangeOrdersTarmem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sum += decimal.Parse(salary.Text);
                if (sum != 0)
                    lblTotalPriceTarmem.Text = sum.ToString();
                else
                    lblTotalPriceTarmem.Text = "بإنتظار التسعير";

            }
            lblMonyTarmim.Text = ClassSaddam.FGetMonySa();
        }
        catch (Exception)
        {

        }
    }

    protected void LBRefreshPrisms_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetDatabyPrisms();
    }

    protected void LBPrintPrisms_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVExchangeOrdersPrisms.Columns[0].Visible = false;
            GVExchangeOrdersPrisms.Columns[11].Visible = false;

            GVExchangeOrdersPrisms.UseAccessibleHeader = true;
            GVExchangeOrdersPrisms.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlDataPrisms;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnDeletePrisms_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty;
            int XIDAdd = Test_Saddam.FGetIDUsiq();
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            foreach (GridViewRow row in GVExchangeOrdersPrisms.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label XID = (Label)row.FindControl("lblIDPrisms");
                    Model_SupportForPrisms_ MSFP = new Model_SupportForPrisms_()
                    {
                        IDCheck = "Delete",
                        IDUniq = Guid.Empty,
                        ID_FinancialYear = Guid.Empty,
                        ID_Donor = Guid.Empty,
                        NumberMostafeed = 0,
                        billNumber = Convert.ToInt32(XID.Text),
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
                        DeleteBy = XIDAdd,
                        DeleteDate = XDate,
                        IsDelete = true,
                        Al_Qaryah = 0
                    };

                    Repostry_SupportForPrisms_ RSFP = new Repostry_SupportForPrisms_();
                    Xresult = RSFP.FArn_SupportForPrisms_Add(MSFP);
                }
            }
            if (Xresult == "IsSuccessDelete")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblMessage.Text = "تم حذف الفاتورة بنجاح ... ";
                FGetDatabyPrisms();
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearchPrisms_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetDatabyPrisms();
    }

    private void FGetDatabyPrisms()
    {
        GVExchangeOrdersPrisms.UseAccessibleHeader = false;
        GVExchangeOrdersPrisms.Columns[0].Visible = true;
        GVExchangeOrdersPrisms.Columns[11].Visible = true;
        if (DLTypePrisms.Text != string.Empty)
        {
            lblTypePrisms.Visible = false;
            if (DLCategoryPrisms.Text != string.Empty)
            {
                lblCategoryPrisms.Visible = false;
                if (txtDateFromPrisms.Text != string.Empty)
                {
                    lblDateFromPrisms.Visible = false;
                    if (txtDateToPrisms.Text != string.Empty)
                    {
                        // Write Code Hear
                        FGetByMostafeedPrisms();
                        System.Threading.Thread.Sleep(500);
                    }
                    else if (txtDateToPrisms.Text == string.Empty)
                        lblDateToPrisms.Visible = true;
                }
                else if (txtDateFromPrisms.Text == string.Empty)
                    lblDateFromPrisms.Visible = true;
            }
            else if (DLCategoryPrisms.Text == string.Empty)
                lblCategoryPrisms.Visible = true;
        }
        else if (DLTypePrisms.Text == string.Empty)
            lblTypePrisms.Visible = true;
    }

    private void FGetByMostafeedPrisms()
    {
        try
        {
            Model_SupportForPrisms_ MSFP = new Model_SupportForPrisms_();
            MSFP.IDCheck = "GetByMostafeedMulti";
            MSFP.IDUniq = Guid.Empty;
            MSFP.ID_FinancialYear = new Guid(ddlYears.SelectedValue);
            MSFP.ID_Donor = Guid.Empty;
            MSFP.NumberMostafeed = 0;
            MSFP.billNumber = 0;
            MSFP.ID_Project = Convert.ToInt32(DLCategoryPrisms.SelectedValue);
            MSFP.Start_Date = txtDateFromPrisms.Text.Trim();
            MSFP.End_Date = txtDateToPrisms.Text.Trim();
            MSFP.DataCheck = string.Empty;
            MSFP.DataCheck2 = string.Empty;
            MSFP.DataCheck3 = string.Empty;
            MSFP.IsDelete = false;
            DataTable dt = new DataTable();
            Repostry_SupportForPrisms_ RSFP = new Repostry_SupportForPrisms_();
            dt = RSFP.BArn_SupportForPrisms_Manage(MSFP);

            if (dt.Rows.Count > 0)
            {
                GVExchangeOrdersPrisms.DataSource = dt;
                GVExchangeOrdersPrisms.DataBind();
                lblCountPrisms.Text = Convert.ToString(dt.Rows.Count);
                pnlNullPrisms.Visible = false;
                pnlDataPrisms.Visible = true;
                pnlSelectPrisms.Visible = false;
                txtTitlePrisms.Text = "قائمة إرشيف " + ddlYears.SelectedItem.ToString() + " من تاريخ " + txtDateFromPrisms.Text.Trim() + " إلى تاريخ " + txtDateToPrisms.Text.Trim() + " لمشروع " + DLCategoryPrisms.SelectedItem.ToString();
            }
            else
            {
                pnlNullPrisms.Visible = true;
                pnlDataPrisms.Visible = false;
                pnlSelectPrisms.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void GVExchangeOrdersPrisms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sum += decimal.Parse(salary.Text);
                if (sum != 0)
                    lblTotalPricePrisms.Text = sum.ToString();
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
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        txtDateFrom.Text = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-01-01");
        txtDateTo.Text = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-12-31");

        txtDateFromTarmem.Text = txtDateFrom.Text;
        txtDateToTarmem.Text = txtDateTo.Text;

        txtDateFromPrisms.Text = txtDateFrom.Text;
        txtDateToPrisms.Text = txtDateTo.Text;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            foreach (GridViewRow row in GVExchangeOrders.Rows)
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
            FGetSupportByBeneficiaryMulti();
        }
    }

}