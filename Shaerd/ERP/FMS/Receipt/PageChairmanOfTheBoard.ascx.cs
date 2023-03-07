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

public partial class Shaerd_ERP_FMS_Receipt_PageChairmanOfTheBoard : System.Web.UI.UserControl
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
            GVChairmanOfTheBoard.Columns[0].Visible = true;
            GVChairmanOfTheBoard.Columns[10].Visible = true;

            GVChairmanOfTheBoard.UseAccessibleHeader = false;

            Model_Receipt_ MR = new Model_Receipt_();
            MR.IDCheck = "GetByChairmanOfTheBoard";
            MR.ID_Item = Guid.Empty;
            MR.bill_Number = 0;
            MR.ID_Donor = Guid.Empty;
            MR.FilterSearch = string.Empty;
            MR.Start_Date = string.Empty;
            MR.End_Date = string.Empty;
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
            GVChairmanOfTheBoard.Columns[10].Visible = false;

            GVChairmanOfTheBoard.UseAccessibleHeader = true;
            GVChairmanOfTheBoard.HeaderRow.TableSection = TableRowSection.TableHeader;

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
            GVChairmanOfTheBoard.UseAccessibleHeader = false;
            foreach (GridViewRow row in GVChairmanOfTheBoard.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label BillID = (Label)row.FindControl("lblID");
                    Model_Receipt_ MR = new Model_Receipt_()
                    {
                        IDCheck = "ByBoard",
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
                        IsRaeesMaglisAlEdarah = true,
                        IDRaees_Allow = Test_Saddam.FGetIDUsiq(),
                        IDRaees_Date_Allow = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IDAmmenAlSondoq = 0,
                        IsAmmenAlSondoq = false,
                        IDAmmen_Allow = Test_Saddam.FGetIDUsiq(),
                        IDAmmen_Date_Allow = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
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
                        DeleteBy = 0,
                        DeleteDate = string.Empty,
                        IsActive = false
                    };
                    Repostry_Receipt_ RR = new Repostry_Receipt_();
                    string Xresult = RR.FOM_Receipt_Add(MR);
                    if (Xresult == "IsSuccessBoard")
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

    protected void GVChairmanOfTheBoard_RowDataBound(object sender, GridViewRowEventArgs e)
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