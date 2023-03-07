using Library_CLS_Arn.ClassOutEntity;
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

public partial class Shaerd_ERP_FMS_Operating_Expenses_PageAll : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //CLS_Permissions.CheckAccountAdmin("A106");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            pnlSelect.Visible = true;
            FGetCategoryShop();
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
        }
    }

    private void FGetCategoryShop()
    {
        ClassQuaem.FGetSupportType(0, CBCategory);
        FSelectCheck(true);
    }

    private void FSelectCheck(bool XValue)
    {
        foreach (ListItem lst in CBCategory.Items) { lst.Selected = XValue; }
        foreach (ListItem lst in CBYears.Items) { lst.Selected = XValue; }
    }

    public string XCategory = string.Empty;
    private void FGetByStatic()
    {
        try
        {
            GVOperating_Expenses.UseAccessibleHeader = false;
            GVOperating_Expenses.Columns[0].Visible = true;
            GVOperating_Expenses.Columns[9].Visible = true;
            string XYears = string.Empty, XYearsName = string.Empty, XCategoryName = string.Empty;
            foreach (ListItem item in CBYears.Items)
                XYears += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBYears.Items)
                XYearsName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBCategory.Items)
                XCategoryName += item.Selected ? string.Format("{0},", item.Text) : string.Empty;

            WSM_Model_Operating_Expenses_ MEOE = new WSM_Model_Operating_Expenses_();
            MEOE.IDCheck = "GetByStatic";
            MEOE.Top = 5000;
            MEOE.ID_Item = Guid.Empty;
            MEOE.ID_FinancialYear = Guid.Empty;
            MEOE.ID_Donor = Guid.Empty;
            MEOE.ID_Project = 0;
            MEOE.Start_Date = txtDateFrom.Text.Trim();
            MEOE.End_Date = txtDateTo.Text.Trim();
            MEOE.DataCheck = XYears.Substring(0, XYears.Length - 1);
            MEOE.DataCheck2 = XCategory.Substring(0, XCategory.Length - 1);
            MEOE.DataCheck3 = string.Empty;
            MEOE.IsActive = true;
            DataTable dt = new DataTable();
            WSM_Repostry_Operating_Expenses_ WROE = new WSM_Repostry_Operating_Expenses_();
            dt = WROE.BWSM_Operating_Expenses_Manage(MEOE);

            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = " المصروفات التشغيلية لمشروع ( " + XCategoryName + " ) من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " +
                    txtDateTo.Text.Trim();
                lbl_Years.Text = XYearsName;
                GVOperating_Expenses.DataSource = dt;
                GVOperating_Expenses.DataBind();
                lblCount.Text = dt.Rows.Count.ToString();
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                IDFilter.Visible = false;
                btnDelete.Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                pnlSelect.Visible = false;
                btnDelete.Visible = false;
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل ... ";
            return;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty;
            foreach (GridViewRow row in GVOperating_Expenses.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVOperating_Expenses.DataKeys[row.RowIndex].Value.ToString());
                    Xresult = WSM_Repostry_Operating_Expenses_.FWSM_Operating_Expenses_Add("Delete", _XID, 0, Test_Saddam.FGetIDUsiq(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), false);
                }
            }
            if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                FGetByStatic();
            }
            txtTitle.Text = Xresult;
        }
        catch
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetByStatic();
    }

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        IDFilter.Visible = true;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVOperating_Expenses.UseAccessibleHeader = true;
            GVOperating_Expenses.HeaderRow.TableSection = TableRowSection.TableHeader;
            GVOperating_Expenses.Columns[0].Visible = false;
            GVOperating_Expenses.Columns[9].Visible = false;
            Session["footable1"] = pnlPrint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable2.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    decimal sumTotal = 0;
    protected void GVOperating_Expenses_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label Count = (Label)e.Row.FindControl("lblCountAsrah");//take lable id
                //CoutAosar += int.Parse(Count.Text);
                //lbl_Count.Text = CoutAosar.ToString();

                Label Sum = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sumTotal += decimal.Parse(Sum.Text);
                lblSum.Text = sumTotal.ToString();

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(lblSum.Text), currencies[Convert.ToInt32(0)]);
                lblSumWord.Text = toWord.ConvertToArabic();

                lblMony.Text = ClassSaddam.FGetMonySa();
            }
        }
        catch (Exception)
        {

        }
    }

}