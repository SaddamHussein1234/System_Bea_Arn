using Library_CLS_Arn.CRM.Models;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_CRM_PageCompany_PageCompanyTricker : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            FGetData();
    }

    private void FGetData()
    {
        try
        {
            GVCompany.Columns[7].Visible = true;
            GVCompany.UseAccessibleHeader = false;

            Model_Tricker_ MC = new Model_Tricker_();
            MC.IDCheck = "GetLastWeek";
            MC.ID_Item = Guid.Empty;
            MC.ID_Company = Guid.Empty;
            MC.Start_Date = ClassSaddam.GetCurrentTime().AddDays(-6).ToString("yyyy-MM-dd");
            MC.End_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            MC.CreatedDate = txtSearch.Text.Trim();
            MC.Is_Delete = false;
            DataTable dt = new DataTable();
            Repostry_Tricker_ RC = new Repostry_Tricker_();
            dt = RC.BCRM_Tricker_Manage(MC);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "قائمة الداعمين اللذين تم متابعتهم من تاريخ " + MC.Start_Date + " إلى تاريخ " + MC.End_Date;
                GVCompany.DataSource = dt;
                GVCompany.DataBind();
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
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageCompanyTricker.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVCompany.Columns[7].Visible = false;
            GVCompany.UseAccessibleHeader = true;
            GVCompany.HeaderRow.TableSection = TableRowSection.TableHeader;

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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
    }

}