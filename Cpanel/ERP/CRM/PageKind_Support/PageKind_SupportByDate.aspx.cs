using Library_CLS_Arn.CRM.Models;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_CRM_PageKind_Support_PageKind_SupportByDate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
                Repostry_Company_.FCRM_Company_Manage(DLCompany);
                txtStartDate.Text = new DateTime(ClassSaddam.GetCurrentTime().Year, ClassSaddam.GetCurrentTime().Month, 1).ToString("yyyy-MM-dd");
                txtEndDate.Text = new DateTime(ClassSaddam.GetCurrentTime().Year, ClassSaddam.GetCurrentTime().AddMonths(1).Month, 1).ToString("yyyy-MM-dd");
                pnlSelect.Visible = true;
                if(Request.QueryString["ID"] != null)
                {
                    ddlYears.SelectedValue = Request.QueryString["IDYear"];
                    DLCompany.SelectedValue = Request.QueryString["ID"];
                    txtStartDate.Text = Request.QueryString["From_Date"];
                    txtEndDate.Text = Request.QueryString["To_Date"];
                    FCRM_In_Kind_Support_Manage();
                }
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            GVBillAll.Columns[0].Visible = false;
            GVBillAll.Columns[9].Visible = false;
            GVBillAll.UseAccessibleHeader = true;
            GVBillAll.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlPrint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        try
        {
            FCRM_In_Kind_Support_Manage();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FCRM_In_Kind_Support_Manage()
    {
        GVBillAll.Columns[0].Visible = true;
        GVBillAll.Columns[9].Visible = true;
        GVBillAll.UseAccessibleHeader = false;
        Model_In_Kind_Support_ MIKS = new Model_In_Kind_Support_();
        if (DLCompany.SelectedValue == string.Empty)
        {
            MIKS.IDCheck = "GetByDateGet";
            MIKS.ID_Company = Guid.Empty;
            txtTitle.Text = "قائمة جميع فواتير الدعم العيني من تاريخ " + txtStartDate.Text.Trim() + " إلى تاريخ " + txtEndDate.Text.Trim();
        }
        else if (DLCompany.SelectedValue != string.Empty)
        {
            MIKS.IDCheck = "GetByDateGetByID";
            MIKS.ID_Company = new Guid(DLCompany.SelectedValue);
            txtTitle.Text = "قائمة فواتير الدعم العيني لـ " + DLCompany.SelectedItem.ToString() + " من تاريخ " + txtStartDate.Text.Trim() + " إلى تاريخ " + txtEndDate.Text.Trim();
        }
        MIKS.ID_Item = new Guid(ddlYears.SelectedValue);
        MIKS.Start_Date = txtStartDate.Text.Trim();
        MIKS.End_Date = txtEndDate.Text.Trim();
        MIKS.CreatedDate = string.Empty;
        MIKS.Is_Delete = false;
        DataTable dt = new DataTable();
        Repostry_In_Kind_Support_ RIKS = new Repostry_In_Kind_Support_();
        dt = RIKS.BCRM_In_Kind_Support_Manage(MIKS);
        if (dt.Rows.Count > 0)
        {
            GVBillAll.DataSource = dt;
            GVBillAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            btnPrint.Visible = true; btnDelete.Visible = true;
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
            btnPrint.Visible = false; btnDelete.Visible = false;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            foreach (GridViewRow row in GVBillAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid Comp_ID = new Guid(GVBillAll.DataKeys[row.RowIndex].Value.ToString());
                    Model_In_Kind_Support_ MIKS = new Model_In_Kind_Support_()
                    {
                        IDCheck = "Delete",
                        ID_Item = Comp_ID,
                        ID_Company = Guid.Empty,
                        ID_Bill = 0,
                        CreatedDate = string.Empty,
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        Is_Delete = true
                    };

                    Repostry_In_Kind_Support_ RIKS = new Repostry_In_Kind_Support_();
                    string Xresult = RIKS.FCRM_In_Kind_Support_Add(MIKS);
                    if (Xresult == "IsSuccessDelete")
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم حذف البيانات بنجاح ... ";
                    }
                }
            }
            FCRM_In_Kind_Support_Manage();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    decimal sum = 0;
    protected void GVBillAll_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");
            sum += decimal.Parse(salary.Text);
            lblTotalPrice.Text = sum.ToString();

            List<CurrencyInfo> currencies = new List<CurrencyInfo>();
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            ToWord toWord = new ToWord(Convert.ToDecimal(lblTotalPrice.Text), currencies[Convert.ToInt32(0)]);
            lblSumWord.Text = toWord.ConvertToArabic();
        }
    }

}