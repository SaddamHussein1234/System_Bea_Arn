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

public partial class Cpanel_ERP_EOS_PageStatistics_PageStatisticsGeneral : System.Web.UI.Page
{
    public string XYears = string.Empty, XCategory = string.Empty, XYearsName = string.Empty, XCategoryName = string.Empty, XMony = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        XMony = ClassSaddam.FGetMonySa();
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A129");
            ClassQuaem.FGetSupportType(0, "'1','2','3'", CBCategory);
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            pnlSelect.Visible = true;
            txtType_Cart.Text = ClassSetting.FGetType_Cart();
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            FSelectCheck();
        }
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBYears.Items) { lst.Selected = false; }
        foreach (ListItem lst in CBCategory.Items) { lst.Selected = true; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageStatisticsGeneral.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            lbl_Title_Bottom.Text = txt_Title_Bottom.Text.Trim();

            txt_Title_Bottom.Visible = false;

            lbl_Title_Bottom.Visible = true;
            GVFinancialStatistics.UseAccessibleHeader = true;
            GVFinancialStatistics.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlDataPrint;
            if (GVFinancialStatistics.Rows.Count > 13)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable2.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
            else if (GVFinancialStatistics.Rows.Count <= 13)
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
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
        FGetData();
    }

    private void FGetData()
    {
        GVFinancialStatistics.UseAccessibleHeader = false;
        txt_Title_Bottom.Visible = true;
        lbl_Title_Bottom.Visible = false;
        FGetSupportType();
    }

    int CoutAosar = 0, CoutCard = 0;
    decimal sumTotal = 0;
    protected void GVFinancialStatistics_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label Count = (Label)e.Row.FindControl("lblCountAsrah");//take lable id
                //CoutAosar += int.Parse(Count.Text);
                //lbl_Count.Text = CoutAosar.ToString();

                Label _Count_Cart = (Label)e.Row.FindControl("lbl_Count_Cart");//take lable id
                CoutCard += int.Parse(_Count_Cart.Text);
                txt_Title_Bottom.Text = ", إجمالي العدد الموزع : " + CoutCard.ToString() + " " + txtType_Cart.Text.Trim();

                Label lbl_Sum = (Label)e.Row.FindControl("lblSumTotal");//take lable id
                Label lbl_SumOperating = (Label)e.Row.FindControl("lblSumOperating_Expenses");//take lable id

                Label Sum = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                Sum.Text = Convert.ToString(decimal.Parse(lbl_Sum.Text) + decimal.Parse(lbl_SumOperating.Text));

                sumTotal += decimal.Parse(Sum.Text);
                lblSum.Text = sumTotal.ToString();

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblSum.Text), currencies[Convert.ToInt32(0)]);
                lblSumWord.Text = toWord.ConvertToArabic();

                lblMony.Text = XMony;
            }
        }
        catch (Exception)
        {

        }
    }

    private void FGetSupportType()
    {
        try
        {
            foreach (ListItem item in CBYears.Items)
                XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBYears.Items)
                XYearsName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategoryName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_();
            MEOB.IDCheck = "GetByPojectStaticGeneral";
            MEOB.ID_Item = Guid.Empty;
            MEOB.ID_FinancialYear = Guid.Empty;
            MEOB.ID_Donor = Guid.Empty;
            MEOB.bill_Number = 0;
            MEOB.ID_MosTafeed = 0;
            MEOB.Start_Date = txtDateFrom.Text.Trim();
            MEOB.End_Date = txtDateTo.Text.Trim();
            MEOB.DataCheck = DLType.SelectedValue;
            MEOB.DataCheck2 = XYears.Substring(0, XYears.Length - 1);
            MEOB.DataCheck3 = XCategory.Substring(0, XCategory.Length - 1);
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
                txtTitle.Text = " الإحصاء المالي العام للدعم العيني حسب المشاريع الموضحة , من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();

                lbl_Years.Text = XYearsName;
                GVFinancialStatistics.DataSource = dt;
                GVFinancialStatistics.DataBind();
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                IDFilter.Visible = false;
                FGetPojectStaticGeneral(XYears.Substring(0, XYears.Length - 1), XCategory.Substring(0, XCategory.Length - 1));
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

    private void FGetPojectStaticGeneral(string XYears, string XIDProject)
    {
        WSM_Model_Exchange_Order_Details_ MEOD = new WSM_Model_Exchange_Order_Details_();
        MEOD.IDCheck = "GetPojectStaticGeneral";
        MEOD.IDItem = Guid.Empty;
        MEOD.ID_FinancialYear = Guid.Empty;
        MEOD.ID_Donor = Guid.Empty;
        MEOD.bill_Number = 0;
        MEOD.ID_MosTafeed = 0;
        MEOD.Start_Date = txtDateFrom.Text.Trim();
        MEOD.End_Date = txtDateTo.Text.Trim();
        MEOD.DataCheck = DLType.SelectedValue;
        MEOD.DataCheck2 = XYears;
        MEOD.DataCheck3 = XIDProject;
        MEOD.IsActive = true;
        DataTable dt = new DataTable();
        WSM_Repostry_Exchange_Order_Details_ REOD = new WSM_Repostry_Exchange_Order_Details_();
        dt = REOD.BWSM_Exchange_Order_Details_Manage(MEOD);

        if (dt.Rows.Count > 0)
        {
            RPTProductAll.DataSource = dt;
            RPTProductAll.DataBind();
            pnlProduct.Visible = true;
        }
        else
            pnlProduct.Visible = false;
    }

    protected void LBEditAge_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            conn.Open();
            string sql = "UPDATE [dbo].[SettingTable] SET [_Type_Cart] = @Type_Cart WHERE IDSetting = @IDSetting";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Type_Cart", txtType_Cart.Text.Trim());
            cmd.Parameters.AddWithValue("@IDSetting", 964654);
            cmd.ExecuteScalar();
            conn.Close();
            FGetData();
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
        IDFilter.Visible = true;
    }

}