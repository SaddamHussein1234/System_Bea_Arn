using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_OM_Performance_Index_PageAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            pnlSelect.Visible = true;
        }
    }

    private void FGetData()
    {
        try
        {
            GVAll.Columns[0].Visible = true;
            GVAll.Columns[9].Visible = true;
            GVAll.UseAccessibleHeader = false;
            DataTable dt = new DataTable();
            dt = Repostry_Performance_Index_.FGetDataInDataTable("GeAllByDate", 1000, Guid.Empty, txtSearch.Text.Trim()
                , txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "قائمة بطاقة مؤشر الأداء من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
                GVAll.DataSource = dt;
                GVAll.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
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
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVAll.Columns[0].Visible = false;
            GVAll.Columns[9].Visible = false;

            GVAll.UseAccessibleHeader = true;
            GVAll.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["foot"] = pnlData2;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4Landscape.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVAll.UseAccessibleHeader = false;
            string Xresult = string.Empty; string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            foreach (GridViewRow row in GVAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVAll.DataKeys[row.RowIndex].Value.ToString());
                    Xresult = Repostry_Performance_Index_.FAPP("Delete", _XID, 0, 0, string.Empty,
                       string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                       string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                       string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                       0, false, 0, string.Empty, 0, false, 0, string.Empty, 0, false, 0, string.Empty,
                       0, 0, Test_Saddam.FGetIDUsiq(), XDate, false);
                }
            }
            if (Xresult == "IsSuccess")
            {
                System.Threading.Thread.Sleep(100);
                IDMessageSuccess.Visible = true;
                FGetData();
            }
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

}