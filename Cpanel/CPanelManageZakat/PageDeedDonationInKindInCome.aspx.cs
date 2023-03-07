using Library_CLS_Arn.ClassEntity.Warehouse.Models;
using Library_CLS_Arn.ClassEntity.Warehouse.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelManageZakat_PageDeedDonationInKindInCome : System.Web.UI.Page
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
            bool A138;
            A138 = Convert.ToBoolean(dtViewProfil.Rows[0]["A138"]);
            if (A138 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            pnlSelect.Visible = true;
            ClassQuaem.FGetProject(DLCategory);
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
        }
    }

    private void FGetByCategory()
    {

        Model_Warehouse_Zakat_ MWZ = new Model_Warehouse_Zakat_();
        MWZ.IDCheck = "GetByCategory";
        MWZ.ID_Item = 0;
        MWZ.ID_FinancialYear = new Guid(ddlYears.SelectedValue);
        MWZ.bill_Number = 0;
        MWZ.ID_Project = Convert.ToInt32(DLCategory.SelectedValue);
        MWZ.Start_Date = txtDateFrom.Text.Trim();
        MWZ.End_Date = txtDateTo.Text.Trim();
        MWZ.DateCheck = "1";
        MWZ.IsDelete = false;
        Repostry_Warehouse_Zakat_ RWZ = new Repostry_Warehouse_Zakat_();
        DataTable dt = new DataTable();
        dt = RWZ.BArn_Warehouse_Zakat_Manage(MWZ);

        if (dt.Rows.Count > 0)
        {
            GVFinancialStatistics.DataSource = dt;
            GVFinancialStatistics.DataBind();
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageDeedDonationInKindInCome.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVFinancialStatistics.UseAccessibleHeader = true;
            GVFinancialStatistics.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlData;
            if (GVFinancialStatistics.Rows.Count > 14)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintFootable2.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
            else if (GVFinancialStatistics.Rows.Count <= 14)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        GVFinancialStatistics.UseAccessibleHeader = false;
        txtTitle.Text = " الإحصاء المالي (الوارد) لمشروع ( " + DLCategory.SelectedItem.ToString() + " ) لعام " + Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy") + "م - " + Convert.ToDateTime(ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(txtDateFrom.Text.Trim()))).ToString("yyyy") + "هـ";
        FGetByCategory();
    }

    int CoutAosar = 0;
    decimal sumTotal = 0;
    protected void GVFinancialStatistics_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Count = (Label)e.Row.FindControl("lblCountQuantity");//take lable id
                CoutAosar += int.Parse(Count.Text);
                lblCount.Text = CoutAosar.ToString();

                Label Sum = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sumTotal += decimal.Parse(Sum.Text);
                lblSum.Text = sumTotal.ToString();

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblSum.Text), currencies[Convert.ToInt32(0)]);
                lblSumWord.Text = toWord.ConvertToArabic();

            }
        }
        catch (Exception)
        {

        }
    }

    protected void DLAlBaheth_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public string FGetSumation(int X1 , int X2)
    {
        string XResult = "";
        XResult = Convert.ToString(X1 * X2);
        return XResult;
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlNull.Visible = false;
        pnlData.Visible = false;
        pnlSelect.Visible = true;
        txtDateFrom.Text = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-MM-dd");
        txtDateTo.Text = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-12-31");
    }

}