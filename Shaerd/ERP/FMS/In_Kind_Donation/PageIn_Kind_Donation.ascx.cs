using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
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

public partial class Shaerd_ERP_FMS_In_Kind_Donation_PageIn_Kind_Donation : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
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
            Model_In_Kind_Donation_Bill_ MIKDB = new Model_In_Kind_Donation_Bill_();
            MIKDB.IDCheck = "GetAllByYears";
            MIKDB.ID_Item = new Guid(ddlYears.SelectedValue);
            MIKDB.bill_Number = 0;
            MIKDB.ID_Donor = Guid.Empty;
            MIKDB.Start_Date = txtDateFrom.Text.Trim();
            MIKDB.End_Date = txtDateTo.Text.Trim();
            MIKDB.DateCheck = string.Empty;
            MIKDB.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_In_Kind_Donation_Bill_ RIKDB = new Repostry_In_Kind_Donation_Bill_();
            dt = RIKDB.BOM_In_Kind_Donation_Bill_Manage(MIKDB);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "قائمة فواتير الدعم العيني من تاريخ " + Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("dd-MM-yyyy") + " إلى تاريخ " + Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("dd-MM-yyyy");
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
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageIn_Kind_Donation.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            GVAll.Columns[0].Visible = false;
            GVAll.Columns[9].Visible = false;

            GVAll.UseAccessibleHeader = true;
            GVAll.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData2;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    int Cou = 0;
    decimal sum = 0;

    protected void GVAll_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlNull.Visible = false;
        pnlData.Visible = false;
        pnlSelect.Visible = true;
        txtDateFrom.Text = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-MM-dd");
        txtDateTo.Text = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-12-31");
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            foreach (GridViewRow row in GVAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label lblID = (Label)row.FindControl("lblID");
                    Label lblIDBill = (Label)row.FindControl("lblIDBill");
                    Model_In_Kind_Donation_Bill_ MIKDB = new Model_In_Kind_Donation_Bill_()
                    {
                        IDCheck = "Delete",
                        ID_Item = new Guid(lblID.Text),
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
                        CreatedBy = 0,
                        CreatedDate = string.Empty,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        DeleteBy = Test_Saddam.FGetIDUsiq(),
                        DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false
                    };
                    Repostry_In_Kind_Donation_Bill_ RIKKB = new Repostry_In_Kind_Donation_Bill_();
                    string Xresult = RIKKB.FOM_In_Kind_Donation_Bill_Add(MIKDB);
                    if (Xresult == "IsSuccessDelete")
                    {
                        Model_In_Kind_Donation_Details_ MIKDD = new Model_In_Kind_Donation_Details_()
                        {
                            IDCheck = "DeleteBill",
                            ID_Item = Guid.Empty,
                            ID_FinancialYear = new Guid(ddlYears.SelectedValue),
                            ID_Bill = Guid.Empty,
                            bill_Number = Convert.ToInt32(lblIDBill.Text),
                            ID_Category = 0,
                            ID_Project = 0,
                            CountProduct = 0,
                            One_Price = 0,
                            Total_Price = 0,
                            Is_There_Partition = false,
                            Count_Partition = 0,
                            CreatedBy = 0,
                            CreatedDate = string.Empty,
                            ModifiedBy = 0,
                            ModifiedDate = string.Empty,
                            DeleteBy = Test_Saddam.FGetIDUsiq(),
                            DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                            IsActive = false
                        };
                        Repostry_In_Kind_Donation_Details_ RICS = new Repostry_In_Kind_Donation_Details_();
                        string XresultDetails = RICS.FOM_In_Kind_Donation_Details_Add(MIKDD);
                        if (XresultDetails == "IsSuccessDeleteBill")
                        {
                            System.Threading.Thread.Sleep(100);
                            IDMessageSuccess.Visible = true;
                            FGetData();
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        FGetData();
    }

}