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

public partial class Shaerd_ERP_FMS_Cashing_PageCashier : System.Web.UI.UserControl
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
            GVCashier.Columns[0].Visible = true;
            GVCashier.Columns[9].Visible = true;

            GVCashier.UseAccessibleHeader = false;

            Model_Cashing_ MC = new Model_Cashing_();
            MC.IDCheck = "GetByCacher";
            MC.ID_Item = Guid.Empty;
            MC.bill_Number = 0;
            MC.ID_Donor = Guid.Empty;
            MC.FilterSearch = string.Empty;
            MC.Start_Date = string.Empty;
            MC.End_Date = string.Empty;
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
        IDMessageSuccess.Visible = false;
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
        IDMessageSuccess.Visible = false;
        try
        {
            int XIDAdd = Test_Saddam.FGetIDUsiq();
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            GVCashier.UseAccessibleHeader = false;
            string Xresult = string.Empty;
            foreach (GridViewRow row in GVCashier.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label BillID = (Label)row.FindControl("lblID");
                    Model_Cashing_ MC = new Model_Cashing_()
                    {
                        IDCheck = "ByCashier",
                        ID_Item = new Guid(BillID.Text),
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
                        IDAmmen_Allow = XIDAdd,
                        IDAmmen_Date_Allow = XDate,
                        Finance_Account = string.Empty,
                        Is_Bank = false,
                        ID_Bank = Guid.Empty,
                        ID_Account = Guid.Empty,
                        Type_Date = string.Empty,
                        CreatedBy = 0,
                        CreatedDate = string.Empty,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        DeleteBy = 0,
                        DeleteDate = string.Empty,
                        IsActive = false
                    };
                    Repostry_Cashing_ RC = new Repostry_Cashing_();
                    Xresult = RC.FOM_Cashing_Add(MC);
                }
            }
            if (Xresult == "IsSuccessCashier")
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

    int Cou = 0;
    decimal sum = 0;

    protected void GVCashier_RowDataBound(object sender, GridViewRowEventArgs e)
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