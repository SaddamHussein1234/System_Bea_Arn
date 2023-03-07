using Library_CLS_Arn.CRS.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_CRS_Committee_Report_PageCommitteeMembers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            GVCommitteeMembers.Columns[0].Visible = true;
            GVCommitteeMembers.Columns[9].Visible = true;
            GVCommitteeMembers.UseAccessibleHeader = false;
            DataTable dt = new DataTable();
            dt = Repostry_CRS_Committee_Members_.FGetDataInDataTable("GetAllByCommittee_Members", 1000, Guid.Empty, Test_Saddam.FGetIDUsiq().ToString(), string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                GVCommitteeMembers.DataSource = dt;
                GVCommitteeMembers.DataBind();
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
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageCommitteeMembers.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        try
        {
            GVCommitteeMembers.Columns[0].Visible = false;
            GVCommitteeMembers.Columns[9].Visible = false;

            GVCommitteeMembers.UseAccessibleHeader = true;
            GVCommitteeMembers.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["foot"] = pnlData;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4Landscape.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        try
        {
            GVCommitteeMembers.UseAccessibleHeader = false;
            string Xresult = string.Empty; string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            foreach (GridViewRow row in GVCommitteeMembers.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVCommitteeMembers.DataKeys[row.RowIndex].Value.ToString());
                    Xresult = Repostry_CRS_Committee_Members_.FAPP("ByCommitte", _XID, Guid.Empty, 0, true,
                       Test_Saddam.FGetIDUsiq(), XDate, string.Empty, 0, 0, 0, 0, string.Empty, false);
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
    
}