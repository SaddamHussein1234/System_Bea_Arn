using Library_CLS_Arn.ERP.Permissions;
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

public partial class Shaerd_ERP_WSM_PageShipping_PageStorekeeper : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A116");
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            WSM_Model_In_Kind_Donation_Bill_ MIKDB = new WSM_Model_In_Kind_Donation_Bill_();
            MIKDB.IDCheck = "GetByStorekeeper";
            MIKDB.ID_Item = Guid.Empty;
            MIKDB.bill_Number = 0;
            MIKDB.ID_Donor = Guid.Empty;
            MIKDB.Start_Date = string.Empty;
            MIKDB.End_Date = string.Empty;
            MIKDB.DateCheck = "0";
            MIKDB.IsActive = true;
            DataTable dt = new DataTable();
            WSM_Repostry_In_Kind_Donation_Bill_ RIKDB = new WSM_Repostry_In_Kind_Donation_Bill_();
            dt = RIKDB.BWSM_In_Kind_Donation_Bill_Manage(MIKDB);
            if (dt.Rows.Count > 0)
            {
                GVStorekeeper.DataSource = dt;
                GVStorekeeper.DataBind();
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
        Response.Redirect("PageStorekeeper.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVStorekeeper.Columns[0].Visible = false;
            GVStorekeeper.Columns[9].Visible = false;

            GVStorekeeper.UseAccessibleHeader = true;
            GVStorekeeper.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = IDPrint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        try
        {
            GVStorekeeper.UseAccessibleHeader = false;
            foreach (GridViewRow row in GVStorekeeper.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVStorekeeper.DataKeys[row.RowIndex].Value);
                    WSM_Model_In_Kind_Donation_Bill_ MIKDB = new WSM_Model_In_Kind_Donation_Bill_()
                    {
                        IDCheck = "ByStorekeeper",
                        ID_Item = new Guid(Comp_ID),
                        ID_FinancialYear = Guid.Empty,
                        bill_Number = 0,
                        The_Initiative = 0,
                        ID_Donor = Guid.Empty,
                        ID_Project = 0,
                        Note_Bill = string.Empty,
                        IDRaeesMaglisAlEdarah = 0,
                        IsRaeesMaglisAlEdarah = false,
                        IDAmmenAlSondoq = 0,
                        IsAmmenAlSondoq = false,
                        IDModer = 0,
                        IsModer = false,
                        IDStorekeeper = 0,
                        IsStorekeeper = true,
                        Type_Bill = string.Empty,
                        CreatedBy = Test_Saddam.FGetIDUsiq(),
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd hh:mm:ss"),
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        DeleteBy = 0,
                        DeleteDate = string.Empty,
                        IsActive = false
                    };
                    WSM_Repostry_In_Kind_Donation_Bill_ RIKKB = new WSM_Repostry_In_Kind_Donation_Bill_();
                    string Xresult = RIKKB.FWSM_In_Kind_Donation_Bill_Add(MIKDB);
                    if (Xresult == "IsSuccessStorekeeper")
                    {
                        System.Threading.Thread.Sleep(100);
                        IDMessageSuccess.Visible = true;
                    }
                }
            }
            FGetData();
        }
        catch (Exception)
        {
            return;
        }
    }

    int Cou = 0;
    decimal sum = 0;
    int tempcounter = 0;
    protected void GVStorekeeper_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sum += decimal.Parse(salary.Text);
                if (sum != 0)
                {
                    lblTotalPrice.Text = sum.ToString();
                    lblMonyType.Text = ClassSaddam.FGetMonySa();
                }
                else
                    lblTotalPrice.Text = "بإنتظار التسعير";

                tempcounter = tempcounter + 1;
                if (tempcounter == 5)
                {
                    e.Row.Attributes.Add("style", "page-break-after: always;");
                    tempcounter = 0;
                }
            }
        }
        catch (Exception)
        {

        }
    }

}