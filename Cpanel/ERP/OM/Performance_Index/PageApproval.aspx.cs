using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_OM_Performance_Index_PageApproval : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            FCheck();
    }

    private void FCheck()
    {
        if (Request.QueryString["Type"] != null)
        {
            if (Request.QueryString["Type"] == "Measurement_Officer")
                FGetData("GeAllByMeasurement", "قائمة تحتاج لموافقة مسؤول القياس");
            else if (Request.QueryString["Type"] == "Implementation_Officer")
                FGetData("GeAllByImplementation", "قائمة تحتاج لموافقة مسؤول التنفيذ");
            else if (Request.QueryString["Type"] == "General_Director")
                FGetData("GeAllByDirector", "قائمة تحتاج لموافقة مدير الجمعية");
        }
    }

    private void FGetData(string XIDCheck, string XTitle)
    {
        try
        {
            GVApproval.Columns[0].Visible = true;
            GVApproval.Columns[9].Visible = true;
            GVApproval.UseAccessibleHeader = false;
            DataTable dt = new DataTable();
            dt = Repostry_Performance_Index_.FGetDataInDataTable(XIDCheck, 1000, Guid.Empty, string.Empty, string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = XTitle;
                GVApproval.DataSource = dt;
                GVApproval.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
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
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVApproval.Columns[0].Visible = false;
            GVApproval.Columns[9].Visible = false;

            GVApproval.UseAccessibleHeader = true;
            GVApproval.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["foot"] = pnlData;
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

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVApproval.UseAccessibleHeader = false;
            string Xresult = string.Empty; string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            string XIDCheck = string.Empty; int XID = Test_Saddam.FGetIDUsiq();
            foreach (GridViewRow row in GVApproval.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVApproval.DataKeys[row.RowIndex].Value.ToString());

                    if (Request.QueryString["Type"] == "Measurement_Officer")
                        XIDCheck = "ByMeasurement";
                    else if (Request.QueryString["Type"] == "Implementation_Officer")
                        XIDCheck = "ByImplementation";
                    else if (Request.QueryString["Type"] == "General_Director")
                        XIDCheck = "ByDirector";
                    Xresult = Repostry_Performance_Index_.FAPP(XIDCheck, _XID, 0, 0, string.Empty,
                       string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                       string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                       string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                       0, true, XID, XDate, 0, true, XID, XDate, 0, true, XID, XDate,
                       0, 0, 0, string.Empty, false);
                }
            }
            if (Xresult == "IsSuccess")
            {
                System.Threading.Thread.Sleep(100);
                IDMessageSuccess.Visible = true;
                FCheck();
            }
        }
        catch (Exception)
        {
            return;
        }
    }

}