using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
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

public partial class Cpanel_ERP_EOS_PageSheard_WUCSortingOutIn_kindSupport : System.Web.UI.UserControl
{
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
            bool IsBaheth_;
            IsBaheth_ = Convert.ToBoolean(dtViewProfil.Rows[0]["IsBaheth"]);
            //if (IsBaheth_ == false)
            //{
                FGetAlQariah();
                ClassMosTafeed.FGetName(DLName);
            //}
            //else if (IsBaheth_)
            //    FGetAlQariahByBaheth();
        }
    }
    public string XMony = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        XMony = ClassSaddam.FGetMonySa();
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();

            GVExchangeOrders.Columns[0].Visible = false;
            //GVExchangeOrders.Columns[8].Visible = false;
        }
    }

    private void FGetAlQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) * FROM [dbo].[Quaem] With(NoLock) Where AlQriah <> @0 And IsDeleteAlQriah = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBAlQariah.DataValueField = "IDItem";
            CBAlQariah.DataTextField = "AlQriah";
            CBAlQariah.DataSource = dt;
            CBAlQariah.DataBind();
        }
        FGetCategoryShop();
    }

    private void FGetAlQariahByBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(100) tbl_MultiQariah.IDItem,[IDAdminJoin],[IDQariah],Quaem.AlQriah,tbl_MultiQariah.IsDelete FROM [dbo].[tbl_MultiQariah] With(NoLock) Inner join Quaem on Quaem.IDItem = tbl_MultiQariah.IDQariah Where IDAdminJoin = @0 And tbl_MultiQariah.IsDelete = @1 Order by IDQariah", IDUser, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            CBAlQariah.DataValueField = "IDQariah";
            CBAlQariah.DataTextField = "AlQriah";
            CBAlQariah.DataSource = dt;
            CBAlQariah.DataBind();
        }
        FGetCategoryShop();
    }

    private void FGetCategoryShop()
    {
        ClassQuaem.FGetSupportType(0, "'1','2','3'", CBCategory);
        FSelectCheck();
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBAlQariah.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBCategory.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBYears.Items) { lst.Selected = true; }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData();
    }

    private void FGetData()
    {
        try
        {
            if (CB2.Checked) { GVExchangeOrders.Columns[2].Visible = true; } else { GVExchangeOrders.Columns[2].Visible = false; }
            if (CB3.Checked) { GVExchangeOrders.Columns[3].Visible = true; } else { GVExchangeOrders.Columns[3].Visible = false; }
            if (CB4.Checked) { GVExchangeOrders.Columns[4].Visible = true; } else { GVExchangeOrders.Columns[4].Visible = false; }
            if (CB6.Checked) { GVExchangeOrders.Columns[6].Visible = true; } else { GVExchangeOrders.Columns[6].Visible = false; }
            if (CB7.Checked) { GVExchangeOrders.Columns[7].Visible = true; } else { GVExchangeOrders.Columns[7].Visible = false; }
            if (CB8.Checked) { GVExchangeOrders.Columns[8].Visible = true; } else { GVExchangeOrders.Columns[8].Visible = false; }
            if (CB9.Checked) { GVExchangeOrders.Columns[9].Visible = true; } else { GVExchangeOrders.Columns[9].Visible = false; }
            if (CB10.Checked) { GVExchangeOrders.Columns[10].Visible = true; } else { GVExchangeOrders.Columns[10].Visible = false; }
            if (CB11.Checked) { GVExchangeOrders.Columns[11].Visible = true; } else { GVExchangeOrders.Columns[11].Visible = false; }
            if (CB12.Checked) { GVExchangeOrders.Columns[12].Visible = true; } else { GVExchangeOrders.Columns[12].Visible = false; }
            if (CB13.Checked) { GVExchangeOrders.Columns[13].Visible = true; } else { GVExchangeOrders.Columns[13].Visible = false; }

            GVExchangeOrders.UseAccessibleHeader = false;
            GVExchangeOrders.Columns[14].Visible = true;

            string XYears = string.Empty, XCategory = string.Empty, XAlQariah = string.Empty;
            foreach (ListItem item in CBYears.Items)
                XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBAlQariah.Items)
                XAlQariah += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_();
            if (DLName.SelectedValue != string.Empty)
            {
                MEOB.IDCheck = "GetSortExchangeOrdersByIDWithShring";
                MEOB.ID_MosTafeed = Convert.ToInt32(DLName.SelectedValue);
            }
            else
            {
                MEOB.IDCheck = "GetSortExchangeOrdersWithShring";
                MEOB.ID_MosTafeed = 0;
            }
            MEOB.ID_Item = Guid.Empty;
            MEOB.ID_FinancialYear = Guid.NewGuid();
            MEOB.ID_Donor = Guid.Empty;
            MEOB.bill_Number = 0;

            MEOB.Start_Date = txtDateFrom.Text.Trim();
            MEOB.End_Date = txtDateTo.Text.Trim();
            MEOB.DataCheck = XYears.Substring(0, XYears.Length - 1);
            MEOB.DataCheck2 = XCategory.Substring(0, XCategory.Length - 1);
            MEOB.DataCheck3 = XAlQariah.Substring(0, XAlQariah.Length - 1);
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
                txtTitle.Text = "قائمة فرز أوامر الصرف من تاريخ " + Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("MM-dd-yyyy") +
                    " إلى تاريخ " + Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("MM-dd-yyyy");
                GVExchangeOrders.DataSource = dt;
                GVExchangeOrders.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                IDFilter.Visible = false;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                pnlSelect.Visible = false;
                IDFilter.Visible = true;
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل ... ";
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVExchangeOrders.Columns[14].Visible = false;

            GVExchangeOrders.UseAccessibleHeader = true;
            GVExchangeOrders.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["foot"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4Landscape.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        pnlNull.Visible = true;
        pnlData.Visible = false;
        pnlSelect.Visible = false;
        IDFilter.Visible = true;
    }

    int Cou = 0;
    decimal sum = 0;
    protected void GVExchangeOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCount = (Label)e.Row.FindControl("lblCountGet");//take lable id
                Cou += int.Parse(lblCount.Text);
                if (Cou != 0)
                    lbl_CountGet.Text = Cou.ToString();

                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sum += decimal.Parse(salary.Text);
                if (sum != 0)
                {
                    lblSum.Text = sum.ToString();
                    lblMony.Text = XMony;
                }
                else
                    lblSum.Text = "0";

            }

            List<CurrencyInfo> currencies = new List<CurrencyInfo>();
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            ToWord toWord = new ToWord(Convert.ToDecimal(lblSum.Text), currencies[Convert.ToInt32(0)]);
            lblSumWord.Text = toWord.ConvertToArabic();
        }
        catch (Exception)
        {

        }
    }

    public Int64 FGetCountCard(Guid XIDYear, int XIDMostafeed, int XIDProject)
    {
        Int64 XResult = 0;
        if (XIDMostafeed == 504)
            XResult = WSM_Repostry_Exchange_Order_Bill_.FCountGetForGuest(XIDYear, XIDMostafeed, XIDProject, "Other", txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), "1");
        else if (XIDMostafeed != 504)
            XResult = WSM_Repostry_Exchange_Order_Bill_.FCountGetForGuest(XIDYear, XIDMostafeed, XIDProject, "Guest", txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), "1");
        else
            XResult = 0;
        return XResult;
    }

}