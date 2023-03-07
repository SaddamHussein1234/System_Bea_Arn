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

public partial class Shaerd_ERP_FMS_Receipt_PageAll : System.Web.UI.UserControl
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
            GVAll.Columns[10].Visible = true;

            GVAll.UseAccessibleHeader = false;
            Model_Receipt_ MR = new Model_Receipt_();
            MR.IDCheck = "GetAllByYears";
            MR.ID_Item = new Guid(ddlYears.SelectedValue);
            MR.bill_Number = 0;
            MR.ID_Donor = Guid.Empty;
            MR.FilterSearch = txtSearch.Text.Trim();
            MR.Start_Date = txtDateFrom.Text.Trim();
            MR.End_Date = txtDateTo.Text.Trim();
            MR.DataCheck = "0";
            MR.DataCheck2 = string.Empty;
            MR.DataCheck3 = string.Empty;
            MR.DataCheck4 = string.Empty;
            MR.DataCheck5 = string.Empty;
            MR.DataCheck6 = string.Empty;
            MR.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Receipt_ RR = new Repostry_Receipt_();
            dt = RR.BOM_Receipt_Manage(MR);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "قائمة سندات القبض من تاريخ " + Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("dd-MM-yyyy") + " إلى تاريخ " + Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("dd-MM-yyyy");
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
        Response.Redirect("PageAll.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageSuccess.Visible = false;
            GVAll.Columns[0].Visible = false;
            GVAll.Columns[10].Visible = false;

            GVAll.UseAccessibleHeader = true;
            GVAll.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["foot"] = pnlData2;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4Landscape.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        try
        {
            IDMessageSuccess.Visible = false;
            foreach (GridViewRow row in GVAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label BillID = (Label)row.FindControl("lblID");
                    Model_Receipt_ MR = new Model_Receipt_()
                    {
                        IDCheck = "Delete",
                        ID_Item = new Guid(BillID.Text),
                        ID_FinancialYear = Guid.Empty,
                        Count_Part = 0,
                        ID_Main_Item = Guid.Empty,
                        ID_Sub_Item = Guid.Empty,
                        ID_Sub_Item_Tow = Guid.Empty,
                        ID_Sub_Item_Three = Guid.Empty,
                        bill_Number = 0,
                        The_Initiative = 0,
                        ID_Donor = Guid.Empty,
                        ID_Project = 0,
                        Note_Bill = string.Empty,
                        The_Mony = 0,
                        IsCash_Money = false,
                        IsShayk_Bank = false,
                        Number_Shayk_Bank = string.Empty,
                        Date_Shayk = string.Empty,
                        For_Bank = string.Empty,
                        Transfer_On_Account = false,
                        Number_Account = string.Empty,
                        For_Bank_Transfer = string.Empty,
                        Date_Bank_Transfer = string.Empty,
                        IDRaeesMaglisAlEdarah = 0,
                        IsRaeesMaglisAlEdarah = false,
                        IDRaees_Allow = 0,
                        IDRaees_Date_Allow = string.Empty,
                        IDAmmenAlSondoq = 0,
                        IsAmmenAlSondoq = true,
                        IDAmmen_Allow = 0,
                        IDAmmen_Date_Allow = string.Empty,
                        ID_Moder = 0,
                        Is_Moder_Allow = false,
                        Is_Moder_Not_Allow = false,
                        Moder_Comment = string.Empty,
                        ID_Moder_Allow = 0,
                        ID_Moder_Date_Allow = string.Empty,
                        Finance_Account = string.Empty,
                        Is_Bank = false,
                        ID_Bank = Guid.Empty,
                        ID_Account = Guid.Empty,
                        Type_Date = string.Empty,
                        CreatedBy = 0,
                        CreatedDate = string.Empty,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        DeleteBy = Test_Saddam.FGetIDUsiq(),
                        DeleteDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false
                    };
                    Repostry_Receipt_ RR = new Repostry_Receipt_();
                    string Xresult = RR.FOM_Receipt_Add(MR);
                    if (Xresult == "IsSuccessDelete")
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

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        pnlNull.Visible = false;
        pnlData.Visible = false;
        pnlSelect.Visible = true;
        txtDateFrom.Text = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-01-01");
        txtDateTo.Text = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-12-31");
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        FGetData();
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
                    lblTotalPrice.Text = sum.ToString();
                else
                    lblTotalPrice.Text = "بإنتظار التسعير";
            }
        }
        catch (Exception)
        {

        }
    }

}