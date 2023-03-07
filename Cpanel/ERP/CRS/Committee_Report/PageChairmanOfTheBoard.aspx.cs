using Library_CLS_Arn.CRS.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_CRS_Committee_Report_PageChairmanOfTheBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtNote.Focus();
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            GVChairmanOfTheBoard.Columns[0].Visible = true;
            GVChairmanOfTheBoard.Columns[8].Visible = true;
            GVChairmanOfTheBoard.UseAccessibleHeader = false;
            DataTable dt = new DataTable();
            dt = Repostry_CRS_Committee_Report_.FGetDataInDataTable("GetByChairmanOfTheBoard", 1000, Guid.Empty, string.Empty, string.Empty, string.Empty, 
                string.Empty, string.Empty, string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                GVChairmanOfTheBoard.DataSource = dt;
                GVChairmanOfTheBoard.DataBind();
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
        Response.Redirect("PageChairmanOfTheBoard.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        try
        {
            GVChairmanOfTheBoard.Columns[0].Visible = false;
            GVChairmanOfTheBoard.Columns[8].Visible = false;

            GVChairmanOfTheBoard.UseAccessibleHeader = true;
            GVChairmanOfTheBoard.HeaderRow.TableSection = TableRowSection.TableHeader;

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
            GVChairmanOfTheBoard.UseAccessibleHeader = false;
            string Xresult = string.Empty; string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            foreach (GridViewRow row in GVChairmanOfTheBoard.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVChairmanOfTheBoard.DataKeys[row.RowIndex].Value.ToString());
                    Xresult = Repostry_CRS_Committee_Report_.FAPP("ByBoard", _XID, Guid.Empty, Guid.Empty, string.Empty, string.Empty, string.Empty, 
                        string.Empty, string.Empty, txtNote.Text.Trim(), 0, true, Test_Saddam.FGetIDUsiq(), XDate, 0, 0, 0, string.Empty, string.Empty, true);
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