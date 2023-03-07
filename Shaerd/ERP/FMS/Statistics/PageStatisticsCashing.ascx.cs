using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.OM.Models;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using Library_CLS_Arn.WSM.Repostry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shaerd_ERP_FMS_Statistics_PageStatisticsCashing : System.Web.UI.UserControl
{
    private string XYears = string.Empty, XYearsName = string.Empty, XMainItems = string.Empty, XSubItems = string.Empty,
        XSubItemsTow = string.Empty, XSubItemsThree = string.Empty, XCompany = string.Empty, XCompanyResult = string.Empty, XAccount = string.Empty;
    public string XMony = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        XMony = ClassSaddam.FGetMonySa();
        if (!IsPostBack)
        {
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            Repostry_Organstions_.FGetDropList(1, "_Cashing", "_ID", "_Ar", DLCompany);
            Repostry_Organstions_.FGetDropList(1, "_Cashing", "_ID", "_Ar", CBCompany);

            FGetByDropList("MainItems", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            FSelectCheck();
        }
    }

    private void FGetByDropList(string XValue, string XDataCheck1, string XDataCheck2, string XDataCheck3, string XDataCheck4, string XDataCheck5)
    {
        try
        {
            Model_Main_Items_ MMI = new Model_Main_Items_();
            MMI.Top = 1000;
            MMI.ID_Item = Guid.Empty;
            MMI.Type = "_Cashing";
            if (XValue == "MainItems")
                MMI.IDCheck = "GetByDropList";
            else if (XValue == "SubItems")
                MMI.IDCheck = "GetByDropListOneMulti";
            else if (XValue == "SubItemsTow")
                MMI.IDCheck = "GetByDropListTowMulti";
            else if (XValue == "SubItemsThree")
                MMI.IDCheck = "GetByDropListThreeMulti";
            MMI.ID_Part = Guid.Empty;
            MMI.FilterSearch = Guid.Empty.ToString();
            MMI.Start_Date = string.Empty;
            MMI.End_Date = string.Empty;
            MMI.DataCheck1 = XDataCheck1;
            MMI.DataCheck2 = XDataCheck2;
            MMI.DataCheck3 = XDataCheck3;
            MMI.DataCheck4 = XDataCheck4;
            MMI.DataCheck5 = XDataCheck5;
            MMI.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Main_Items_ RMI = new Repostry_Main_Items_();
            dt = RMI.BOM_Main_Items_Manage(MMI);
            if (dt.Rows.Count > 0)
            {
                if (XValue == "MainItems")
                {
                    CBMainItems.DataValueField = "_ID";
                    CBMainItems.DataTextField = "_Ar"; CBMainItems.DataSource = dt;
                    CBMainItems.DataBind();
                }
                else if (XValue == "SubItems")
                {
                    CBSubItems.DataValueField = "_ID"; CBSubItems.DataTextField = "_Ar";
                    CBSubItems.DataSource = dt; CBSubItems.DataBind();
                }
                else if (XValue == "SubItemsTow")
                {
                    CBSubItemsTow.DataValueField = "_ID"; CBSubItemsTow.DataTextField = "_Ar";
                    CBSubItemsTow.DataSource = dt; CBSubItemsTow.DataBind();
                }
                else if (XValue == "SubItemsThree")
                {
                    CBSubItemsThree.DataValueField = "_ID"; CBSubItemsThree.DataTextField = "_Ar";
                    CBSubItemsThree.DataSource = dt; CBSubItemsThree.DataBind();
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBYears.Items) { lst.Selected = false; }
        foreach (ListItem lst in CBCompany.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBAccount.Items) { lst.Selected = true; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageStatisticsCashing.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            if (DLCount.SelectedValue == "ByYears")
            {
                GVStatisticsByCashingByOrg.UseAccessibleHeader = true;
                GVStatisticsByCashingByOrg.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else if (DLCount.SelectedValue == "ByMain_")
            {
                GVStatisticsByCashingByMain.UseAccessibleHeader = true;
                GVStatisticsByCashingByMain.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else if (DLCount.SelectedValue == "ByOne")
            {
                GVStatisticsByCashingByOne.UseAccessibleHeader = true;
                GVStatisticsByCashingByOne.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else if (DLCount.SelectedValue == "ByTwo")
            {
                GVStatisticsByCashingByTwo.UseAccessibleHeader = true;
                GVStatisticsByCashingByTwo.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else if (DLCount.SelectedValue == "ByThree")
            {
                GVStatisticsByCashingByThree.UseAccessibleHeader = true;
                GVStatisticsByCashingByThree.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            Session["foot"] = pnlDataPrint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4Landscape.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string XSingle = string.Empty;
            foreach (ListItem item in CBYears.Items)
                XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
            if (RBMulti.Checked && RBSingle.Checked == false)
            {
                foreach (ListItem item in CBCompany.Items)
                    XCompany += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
                XCompanyResult = XCompany.Substring(0, XCompany.Length - 1);
                XSingle = string.Empty;
            }
            else if (RBMulti.Checked == false && RBSingle.Checked)
            {
                XCompany = DLCompany.SelectedValue;
                XCompanyResult = XCompany;
                XSingle = "Single";
            }

            foreach (ListItem item in CBAccount.Items)
                XAccount += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            if (DLCount.SelectedValue == "ByYears")
            {
                FGetStaticByCashing("GetStaticByOrg" + XSingle, XYears.Substring(0, XYears.Length - 1), string.Empty,
                    string.Empty, string.Empty, string.Empty, XCompanyResult,
                    XAccount.Substring(0, XAccount.Length - 1), GVStatisticsByCashingByOrg);
                FHideTable(true, false, false, false, false);
            }
            else if (DLCount.SelectedValue == "ByMain_")
            {
                foreach (ListItem item in CBMainItems.Items)
                    XMainItems += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

                FGetStaticByCashing("GetStaticByMain" + XSingle, XYears.Substring(0, XYears.Length - 1), XMainItems.Substring(0, XMainItems.Length - 1),
                    string.Empty, string.Empty, string.Empty, XCompanyResult,
                    XAccount.Substring(0, XAccount.Length - 1), GVStatisticsByCashingByMain);
                FHideTable(false, true, false, false, false);
            }
            else if (DLCount.SelectedValue == "ByOne")
            {
                foreach (ListItem item in CBMainItems.Items)
                    XMainItems += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
                foreach (ListItem item in CBSubItems.Items)
                    XSubItems += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
                FGetStaticByCashing("GetStaticByItemsOne" + XSingle, XYears.Substring(0, XYears.Length - 1), XMainItems.Substring(0, XMainItems.Length - 1),
                     XSubItems.Substring(0, XSubItems.Length - 1), string.Empty, string.Empty, XCompanyResult,
                     XAccount.Substring(0, XAccount.Length - 1), GVStatisticsByCashingByOne);
                FHideTable(false, false, true, false, false);
            }
            else if (DLCount.SelectedValue == "ByTwo")
            {
                foreach (ListItem item in CBMainItems.Items)
                    XMainItems += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
                foreach (ListItem item in CBSubItems.Items)
                    XSubItems += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
                foreach (ListItem item in CBSubItemsTow.Items)
                    XSubItemsTow += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
                FGetStaticByCashing("GetStaticByItemsTwo" + XSingle, XYears.Substring(0, XYears.Length - 1), XMainItems.Substring(0, XMainItems.Length - 1),
                     XSubItems.Substring(0, XSubItems.Length - 1), XSubItemsTow.Substring(0, XSubItemsTow.Length - 1),
                     string.Empty, XCompanyResult, XAccount.Substring(0, XAccount.Length - 1), GVStatisticsByCashingByTwo);
                FHideTable(false, false, false, true, false);
            }
            else if (DLCount.SelectedValue == "ByThree")
            {
                foreach (ListItem item in CBMainItems.Items)
                    XMainItems += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
                foreach (ListItem item in CBSubItems.Items)
                    XSubItems += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
                foreach (ListItem item in CBSubItemsTow.Items)
                    XSubItemsTow += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
                foreach (ListItem item in CBSubItemsThree.Items)
                    XSubItemsThree += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
                FGetStaticByCashing("GetStaticByItemsThree" + XSingle, XYears.Substring(0, XYears.Length - 1), XMainItems.Substring(0, XMainItems.Length - 1),
                     XSubItems.Substring(0, XSubItems.Length - 1), XSubItemsTow.Substring(0, XSubItemsTow.Length - 1),
                     XSubItemsThree.Substring(0, XSubItemsThree.Length - 1), XCompanyResult,
                     XAccount.Substring(0, XAccount.Length - 1), GVStatisticsByCashingByThree);
                FHideTable(false, false, false, false, true);
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

    private void FGetStaticByCashing(string XCheck, string XDataCheck1, string XDataCheck2, string XDataCheck3,
                 string XDataCheck4, string XDataCheck5, string XDataCheck6, string XFilterSearch, GridView GV)
    {
        try
        {
            foreach (ListItem item in CBYears.Items)
                XYearsName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            GV.UseAccessibleHeader = false;
            Model_Cashing_ MC = new Model_Cashing_();
            MC.IDCheck = XCheck;
            MC.ID_Item = Guid.Empty;
            MC.bill_Number = 0;
            MC.ID_Donor = Guid.Empty;
            MC.FilterSearch = XFilterSearch;
            MC.Start_Date = txtDateFrom.Text.Trim();
            MC.End_Date = txtDateTo.Text.Trim();
            MC.DataCheck = XDataCheck1;
            MC.DataCheck2 = XDataCheck2;
            MC.DataCheck3 = XDataCheck3;
            MC.DataCheck4 = XDataCheck4;
            MC.DataCheck5 = XDataCheck5;
            MC.DataCheck6 = XDataCheck6;
            MC.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Cashing_ RC = new Repostry_Cashing_();
            dt = RC.BOM_Cashing_Manage(MC);

            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = " الإحصاء المالي لسندات الصرف , من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim() + " , حسب" + DLCount.SelectedItem.Text;
                lbl_AccountName.Text = "(" + XFilterSearch + ")";
                lbl_Years.Text = "(" + XYearsName.Substring(0, XYearsName.Length - 1) + ")";
                GV.DataSource = dt;
                GV.DataBind();
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
                IDFilter.Visible = false;
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

    private void FHideTable(bool _One, bool _Two, bool _Three, bool _fuor, bool _Five)
    {
        GVStatisticsByCashingByOrg.Visible = _One;
        GVStatisticsByCashingByMain.Visible = _Two;
        GVStatisticsByCashingByOne.Visible = _Three;
        GVStatisticsByCashingByTwo.Visible = _fuor;
        GVStatisticsByCashingByThree.Visible = _Five;
    }

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        IDFilter.Visible = true;
        pnlData.Visible = false;
        pnlSelect.Visible = true;
    }

    protected void CBMainItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        FCheckMainItems(string.Empty);
    }

    protected void CBSelectAllMainItems_CheckedChanged(object sender, EventArgs e)
    {
        FCheckMainItems("All");
    }

    private void FCheckMainItems(string XCheck)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            CBSubItems.ClearSelection(); CBSubItems.Items.Clear();
            CBSubItemsTow.ClearSelection(); CBSubItemsTow.Items.Clear();
            CBSubItemsThree.ClearSelection(); CBSubItemsThree.Items.Clear();
            if (XCheck == "All")
            {
                if (CBSelectAllMainItems.Checked)
                    foreach (ListItem lst in CBMainItems.Items) { lst.Selected = true; }
                else
                    foreach (ListItem lst in CBMainItems.Items) { lst.Selected = false; }
            }
            string XMain = string.Empty;
            foreach (ListItem item in CBMainItems.Items)
                XMain += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
            FGetByDropList("SubItems", XMain.Substring(0, XMain.Length - 1), string.Empty, string.Empty, string.Empty, string.Empty);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void CBSubItems_SelectedIndexChanged(object sender, EventArgs e)
    {
        FCheckSubItems(string.Empty);
    }

    protected void CBSelectAllSubItems_CheckedChanged(object sender, EventArgs e)
    {
        FCheckSubItems("All");
    }

    private void FCheckSubItems(string XCheck)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            CBSubItemsTow.ClearSelection(); CBSubItemsTow.Items.Clear();
            CBSubItemsThree.ClearSelection(); CBSubItemsThree.Items.Clear();
            if (XCheck == "All")
            {
                if (CBSelectAllSubItems.Checked)
                    foreach (ListItem lst in CBSubItems.Items) { lst.Selected = true; }
                else
                    foreach (ListItem lst in CBSubItems.Items) { lst.Selected = false; }
            }
            string XMain = string.Empty;
            foreach (ListItem item in CBSubItems.Items)
                XMain += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
            FGetByDropList("SubItemsTow", string.Empty, XMain.Substring(0, XMain.Length - 1), string.Empty, string.Empty, string.Empty);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void CBSubItemsTow_SelectedIndexChanged(object sender, EventArgs e)
    {
        FCheckSubItemsTow(string.Empty);
    }

    protected void CBSelectAllSubItemsTow_CheckedChanged(object sender, EventArgs e)
    {
        FCheckSubItemsTow("All");
    }

    private void FCheckSubItemsTow(string XCheck)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            CBSubItemsThree.ClearSelection(); CBSubItemsThree.Items.Clear();
            if (XCheck == "All")
            {
                if (CBSelectAllSubItemsTow.Checked)
                    foreach (ListItem lst in CBSubItemsTow.Items) { lst.Selected = true; }
                else
                    foreach (ListItem lst in CBSubItemsTow.Items) { lst.Selected = false; }
            }
            string XMain = string.Empty;
            foreach (ListItem item in CBSubItemsTow.Items)
                XMain += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
            FGetByDropList("SubItemsThree", string.Empty, string.Empty, XMain.Substring(0, XMain.Length - 1), string.Empty, string.Empty);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void DLCount_Load(object sender, EventArgs e)
    {
        DLCount.Attributes["onchange"] = "ValidateAdd();";
    }

    public string FCheck(string XCheck)
    {
        string XResult = "display:none;";
        if (XCheck == "_Main")
        {
            if (DLCount.SelectedValue == "ByMain_" || DLCount.SelectedValue == "ByOne" || DLCount.SelectedValue == "ByTwo" || DLCount.SelectedValue == "ByThree")
                XResult = "display:block;";
        }
        else if (XCheck == "_One")
        {
            if (DLCount.SelectedValue == "ByOne" || DLCount.SelectedValue == "ByTwo" || DLCount.SelectedValue == "ByThree")
                XResult = "display:block;";
        }
        else if (XCheck == "_Two")
        {
            if (DLCount.SelectedValue == "ByTwo" || DLCount.SelectedValue == "ByThree")
                XResult = "display:block;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "_Three")
        {
            if (DLCount.SelectedValue == "ByThree")
                XResult = "display:block;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "Single")
        {
            if (RBSingle.Checked)
                XResult = "display:block;";
            else if (RBSingle.Checked == false)
                XResult = "display:none;";
        }
        else if (XCheck == "Multi")
        {
            if (RBMulti.Checked)
                XResult = "display:block;";
            else if (RBMulti.Checked == false)
                XResult = "display:none;";
        }
        else
            XResult = "display:none;";
        return XResult;
    }

    decimal sumTotal = 0;
    private void FGetTotal(GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Sum = (Label)e.Row.FindControl("lblTotal");//take lable id
                sumTotal += decimal.Parse(Sum.Text);
                lblSum.Text = sumTotal.ToString();

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(sumTotal, currencies[Convert.ToInt32(0)]);
                lblSumWord.Text = toWord.ConvertToArabic();

                lblMony.Text = XMony;
            }
        }
        catch (Exception)
        {

        }
    }

    protected void GVStatisticsByCashingByOrg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        FGetTotal(e);
    }

    protected void GVStatisticsByCashingByMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        FGetTotal(e);
    }

    protected void GVStatisticsByCashingByOne_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        FGetTotal(e);
    }

    protected void GVStatisticsByCashingByTwo_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        FGetTotal(e);
    }

    protected void GVStatisticsByCashingByThree_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        FGetTotal(e);
    }

}