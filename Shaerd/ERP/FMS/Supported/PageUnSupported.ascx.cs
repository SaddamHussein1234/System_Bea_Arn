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

public partial class Shaerd_ERP_FMS_Supported_PageUnSupported : System.Web.UI.UserControl
{
    public string XYears = string.Empty, XAccount = string.Empty, XSupport = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            ClassQuaem.FGetSupportType(0, CBSupport);
            pnlSelect.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            FSelectCheck();
        }
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBYears.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBSupport.Items) { lst.Selected = true; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageUnSupported.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            Session["foot"] = pnl2;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4OutHeader.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
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
        pnlDataPrint.Visible = true;
        pnlSelect.Visible = false;
        IDFilter.Visible = false;
        pnlDataPrint.Visible = true;
        FGetData();

    }

    private void FGetData()
    {
        try
        {
            string XMony = ClassSaddam.FGetMonySa();
            foreach (ListItem item in CBSupport.Items)
                XSupport += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            DataTable dt = new DataTable();
            dt = ClassDataAccess.GetData("SELECT Top(1000) [IDItem],[TypeAlDam] FROM [dbo].[Quaem] TQ With(NoLock) WHERE ([IDItem] In (SELECT * FROM [dbo].[FStringToInt](@0))) And (Not EXISTS (select 1 from [db_a513a6_berarnallom].[dbo].[tbl_Receipt_] TR Where TR.[_ID_Project_] = TQ.[IDItem] And (CONVERT(Date,TR.[_CreatedDate_]) Between @1 And @2) And TR.[_IsActive_] = @3) And Not EXISTS (select 1 from [db_a513a6_berarnallom].[dbo].[tbl_Cash_Donation_] TCD Where TCD.[_ID_Project_] = TQ.[IDItem] And (CONVERT(Date,TCD.[_CreatedDate_]) Between @1 And @2) And TCD.[_IsActive_] = @3) And Not EXISTS (select 1 from [dbo].[tbl_General_Assmply_Bill] TGSA Where TGSA.[_The_Project_] = TQ.[IDItem] And (CONVERT(Date,TGSA.[Date_Add_]) Between @1 And @2) And TGSA.[Is_Delete_] = @6)) And [TypeAlDam] <> @4 And [TypeAlDam] <> @5 And [IsDeleteTypeAlDam] = @6 Order by [TypeAlDam]",
               XSupport.Substring(0, XSupport.Length - 1), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true.ToString(), string.Empty, "التالف", false.ToString());
            if (dt.Rows.Count > 0)
            {
                txtTitleReceipt.Text = " قائمة المشاريع الغير مدعومة , من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
                RPTSupported.DataSource = dt;
                RPTSupported.DataBind();
                lblCount.Text = dt.Rows.Count.ToString();
                pnlSelect.Visible = false;
                IDFilter.Visible = false;
                PnlNull.Visible = false;
                pnlDataPrint.Visible = true;
            }
            else
            {
                pnlSelect.Visible = false;
                IDFilter.Visible = true;
                PnlNull.Visible = true;
                pnlDataPrint.Visible = false;
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

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        IDFilter.Visible = true;
        pnlDataPrint.Visible = false;
    }

    public string FGetDate()
    {
        return txtDateFrom.Text.Trim() + " - " + txtDateTo.Text.Trim();
    }

}