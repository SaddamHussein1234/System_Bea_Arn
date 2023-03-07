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

public partial class Shaerd_ERP_FMS_Cashing_PageAll : System.Web.UI.UserControl
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
            GVAll.Columns[11].Visible = true;

            GVAll.UseAccessibleHeader = false;
            Model_Cashing_ MC = new Model_Cashing_();
            MC.IDCheck = "GetAllByYears";
            MC.ID_Item = new Guid(ddlYears.SelectedValue);
            MC.bill_Number = 0;
            MC.ID_Donor = Guid.Empty;
            MC.FilterSearch = txtSearch.Text.Trim();
            MC.Start_Date = txtDateFrom.Text.Trim();
            MC.End_Date = txtDateTo.Text.Trim();
            MC.DataCheck = "0";
            MC.DataCheck2 = string.Empty;
            MC.DataCheck3 = string.Empty;
            MC.DataCheck4 = string.Empty;
            MC.DataCheck5 = string.Empty;
            MC.DataCheck6 = string.Empty;
            MC.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Cashing_ RC = new Repostry_Cashing_();
            dt = RC.BOM_Cashing_Manage(MC);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "قائمة سندات الصرف من تاريخ " + Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("dd-MM-yyyy") + " إلى تاريخ " + Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("dd-MM-yyyy");
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
        IDMessageSuccess.Visible = false;
        Response.Redirect("PageAll.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        try
        {
            GVAll.Columns[0].Visible = false;
            GVAll.Columns[11].Visible = false;

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
            int XIDAdd = Test_Saddam.FGetIDUsiq();
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            string Xresult = string.Empty;
            foreach (GridViewRow row in GVAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVAll.DataKeys[row.RowIndex].Values[0].ToString());
                    Model_Cashing_ MC = new Model_Cashing_()
                    {
                        IDCheck = "Delete",
                        ID_Item = _XID,
                        ID_FinancialYear = Guid.Empty,
                        Count_Part = 0,
                        ID_Main_Item = Guid.Empty,
                        ID_Sub_Item = Guid.Empty,
                        ID_Sub_Item_Tow = Guid.Empty,
                        ID_Sub_Item_Three = Guid.Empty,
                        ID_Donor = Guid.Empty,
                        bill_Number = 0,
                        The_Initiative = 0,
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
                        Finance_Account = string.Empty,
                        Is_Bank = false,
                        ID_Bank = Guid.Empty,
                        ID_Account = Guid.Empty,
                        Type_Date = string.Empty,
                        CreatedBy = 0,
                        CreatedDate = string.Empty,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        DeleteBy = XIDAdd,
                        DeleteDate = XDate,
                        IsActive = false
                    };
                    Repostry_Cashing_ RC = new Repostry_Cashing_();
                    Xresult = RC.FOM_Cashing_Add(MC);
                }
            }
            if (Xresult == "IsSuccessDelete")
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

    protected void btnPrintMulti_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        try
        {
            string XIDBill = string.Empty;
            foreach (GridViewRow row in GVAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVAll.DataKeys[row.RowIndex].Values[0].ToString());
                    string _XIDBill = GVAll.DataKeys[row.RowIndex].Values[1].ToString();
                    XIDBill += _XIDBill + ",";
                }
            }
            Response.Redirect("../PageViewMulti.aspx?Type=Cashing&IDUniq=" + ddlYears.SelectedValue +
                 "&IDBill=" + XIDBill.Substring(0, XIDBill.Length - 1));
        }
        catch (Exception)
        {
            return;
        }
    }

}