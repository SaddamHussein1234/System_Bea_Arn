using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_OM_Customers_PageAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            txtDateFrom.Text = new DateTime(ClassSaddam.GetCurrentTime().Year, 1, 1).ToString("yyyy-MM-dd");
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            FGeAllByDate();
        }
    }

    private void FGeAllByDate()
    {
        try
        {
            GVCustomers.Columns[0].Visible = true;
            GVCustomers.Columns[10].Visible = true;
            GVCustomers.UseAccessibleHeader = false;

            DataTable dt = new DataTable();
            dt = Repostry_Customers_.FGetDataInDataTable("GeAllByDate", 1000, Guid.Empty, new Guid(ddlYears.SelectedValue),
               txtSearch.Text.Trim(), txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), true);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "قائمة بنك المعلومات من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim() + " , إرشيف " + ddlYears.SelectedItem.ToString();
                GVCustomers.DataSource = dt;
                GVCustomers.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                btnDelete.Visible = true;
                pnlNull.Visible = false;
                pnlData.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
                pnlNull.Visible = true;
                pnlData.Visible = false;
            }
        }
        catch
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        FGeAllByDate();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            string Xresult = string.Empty;
            foreach (GridViewRow row in GVCustomers.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVCustomers.DataKeys[row.RowIndex].Value.ToString());
                    Xresult = Repostry_Customers_.FAPP_Add("Delete", _XID, Guid.Empty, 0, string.Empty,
                        string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                        string.Empty, string.Empty, string.Empty, false, 0, 0, Test_Saddam.FGetIDUsiq(),
                        ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), false);
                }
            }
            if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
                System.Threading.Thread.Sleep(100);
                FGeAllByDate();
            }
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
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtDateFrom.Text = ddlYears.SelectedItem.ToString() + "-01-01";
        txtDateTo.Text = new DateTime(Convert.ToInt32(ddlYears.SelectedItem.ToString()), 12, 31).ToString("yyyy-MM-dd");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = false;
            GVCustomers.Columns[0].Visible = false;
            GVCustomers.Columns[10].Visible = false;

            GVCustomers.UseAccessibleHeader = true;
            GVCustomers.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData2;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

}