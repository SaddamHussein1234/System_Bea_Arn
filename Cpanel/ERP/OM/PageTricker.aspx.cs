using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_OM_PageTricker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlSelect.Visible = true;
            ClassAdmin_Arn.FGetAdmin(CBUsers);
            foreach (ListItem lst in CBUsers.Items) { lst.Selected = true; }
            foreach (ListItem lst in CBSystems.Items) { lst.Selected = true; }
            foreach (ListItem lst in CBProcess.Items) { lst.Selected = true; }
            txtDateFrom.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            txtDateTo.Text = txtDateFrom.Text;
        }
    }

    protected void LB_Print_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            Session["foot"] = pnlprint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4Landscape.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void Lb_Refresh_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageTricker.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            //if (DL_Process.SelectedValue == "All")
                FGetData("GetAllBySys");
            //else
            //    FGetData("GetAllBySysByProcess");
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل ... ";
            return;
        }
    }

    private void FGetData(string XCheck)
    {
        try
        {
            string XUserValue = string.Empty, XSystemValue = string.Empty, XProcessValue = string.Empty;
            foreach (ListItem item in CBUsers.Items)
                XUserValue += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            foreach (ListItem item in CBSystems.Items)
                XSystemValue += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
            
            foreach (ListItem item in CBProcess.Items)
                XProcessValue += item.Selected ? string.Format("{0},", item.Value) : string.Empty;

            DataTable dt = new DataTable();
            dt = Repostry_Tricker_.FGetDataInDataTable(XCheck, 5000, Guid.Empty, string.Empty, string.Empty,
                txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XUserValue.Substring(0, XUserValue.Length - 1), 
                XSystemValue.Substring(0, XSystemValue.Length - 1), XProcessValue.Substring(0, XProcessValue.Length - 1));
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = " قائمة تتبع مركز العمليات , من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
                RPTProccess.DataSource = dt;
                RPTProccess.DataBind();
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
            lblMessageWarning.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل ... ";
            return;
        }
    }

}