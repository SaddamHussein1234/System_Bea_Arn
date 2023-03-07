using Library_CLS_Arn.OM.Models;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shaerd_ERP_FMS_In_Kind_Donation_PageCashier : System.Web.UI.UserControl
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
            Model_In_Kind_Donation_Bill_ MIKDB = new Model_In_Kind_Donation_Bill_();
            MIKDB.IDCheck = "GetByCacher";
            MIKDB.ID_Item = Guid.Empty;
            MIKDB.bill_Number = 0;
            MIKDB.ID_Donor = Guid.Empty;
            MIKDB.Start_Date = string.Empty;
            MIKDB.End_Date = string.Empty;
            MIKDB.DateCheck = "0";
            MIKDB.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_In_Kind_Donation_Bill_ RIKDB = new Repostry_In_Kind_Donation_Bill_();
            dt = RIKDB.BOM_In_Kind_Donation_Bill_Manage(MIKDB);
            if (dt.Rows.Count > 0)
            {
                GVCashier.DataSource = dt;
                GVCashier.DataBind();
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
        Response.Redirect("PageCashier.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVCashier.Columns[0].Visible = false;
            GVCashier.Columns[9].Visible = false;

            GVCashier.UseAccessibleHeader = true;
            GVCashier.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
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
            GVCashier.UseAccessibleHeader = false;
            foreach (GridViewRow row in GVCashier.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label BillID = (Label)row.FindControl("lblID");
                    Model_In_Kind_Donation_Bill_ MIKDB = new Model_In_Kind_Donation_Bill_()
                    {
                        IDCheck = "ByCashier",
                        ID_Item = new Guid(BillID.Text),
                        ID_FinancialYear = Guid.Empty,
                        bill_Number = 0,
                        The_Initiative = 0,
                        ID_Donor = Guid.Empty,
                        ID_Project = 0,
                        Note_Bill = string.Empty,
                        IDRaeesMaglisAlEdarah = 0,
                        IsRaeesMaglisAlEdarah = false,
                        IDAmmenAlSondoq = 0,
                        IsAmmenAlSondoq = true,
                        IDModer = 0,
                        IsModer = false,
                        IDStorekeeper = 0,
                        IsStorekeeper = false,
                        CreatedBy = Test_Saddam.FGetIDUsiq(),
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        DeleteBy = 0,
                        DeleteDate = string.Empty,
                        IsActive = false
                    };
                    Repostry_In_Kind_Donation_Bill_ RIKKB = new Repostry_In_Kind_Donation_Bill_();
                    string Xresult = RIKKB.FOM_In_Kind_Donation_Bill_Add(MIKDB);
                    if (Xresult == "IsSuccessCashier")
                    {
                        System.Threading.Thread.Sleep(100);
                        IDMessageSuccess.Visible = true;
                        FGetData();
                    }
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    int Cou = 0;
    decimal sum = 0;

    protected void GVWarehouseCashier_RowDataBound(object sender, GridViewRowEventArgs e)
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
            }
        }
        catch (Exception)
        {

        }
    }

}