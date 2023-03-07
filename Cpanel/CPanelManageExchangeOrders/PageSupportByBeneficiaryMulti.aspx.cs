using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelManageExchangeOrders_PageSupportByBeneficiaryMulti : System.Web.UI.Page
{
    string IDUser, IDUniq;
    private void GetCookie()
    {
        try
        {
            HttpCookie Cooke;  // رقم المستخدم
            Cooke = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
            IDUser = ClassSaddam.UnprotectPassword(Cooke["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
        }
        catch
        {
            Response.Redirect("PageNotAccess.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        ClassAdmin_Arn CAA = new ClassAdmin_Arn();
        CAA._IDUniq = IDUniq;
        CAA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CAA.BArnAdminGetByIDUniq();
        if (dtViewProfil.Rows.Count > 0)
        {
            bool A60;
            A60 = Convert.ToBoolean(dtViewProfil.Rows[0]["A60"]);
            if (A60 == false)
                Response.Redirect("PageNotAccess.aspx");
            ClassQuaem.FGetSupportType(1, "'1','2','3'", DLCategory);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            if (Request.QueryString["XID"] != null)
            {
                DLCategory.SelectedValue = Request.QueryString["XIDCate"];
                txtDateFrom.Text = Request.QueryString["XIDFrom"];
                txtDateTo.Text = Request.QueryString["XIDTo"];
                IDTathith.Visible = true;
                FArnProductShopBySupportByBeneficiaryMulti();
            }
            else
            {
                return;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        GVExchangeOrders.UseAccessibleHeader = false;
        GVExchangeOrders.Columns[0].Visible = true;
        GVExchangeOrders.Columns[10].Visible = true;
        if (DLType.Text != string.Empty)
        {
            lblType.Visible = false;
            if (DLCategory.Text != string.Empty)
            {
                lblCategory.Visible = false;
                if (txtDateFrom.Text != string.Empty)
                {
                    lblDateFrom.Visible = false;
                    if (txtDateTo.Text != string.Empty)
                    {
                        // Write Code Hear
                        FArnProductShopBySupportByBeneficiaryMulti();
                        System.Threading.Thread.Sleep(500);
                    }
                    else if (txtDateTo.Text == string.Empty)
                        lblDateTo.Visible = true;
                }
                else if (txtDateFrom.Text == string.Empty)
                    lblDateFrom.Visible = true;
            }
            else if (DLCategory.Text == string.Empty)
                lblCategory.Visible = true;
        }
        else if (DLType.Text == string.Empty)
            lblType.Visible = true;
    }

    private void FArnProductShopBySupportByBeneficiaryMulti()
    {
        try
        {
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = Convert.ToInt32(0);
            CPS.IDMosTafeed = Convert.ToInt32(0);
            CPS.IDType = DLType.SelectedValue;
            CPS.IDCategory = Convert.ToInt32(DLCategory.SelectedValue);
            CPS.DateFrom = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
            CPS.DateTo = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
            CPS.IsDelete = false;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopBySupportByBeneficiaryMulti();
            if (dt.Rows.Count > 0)
            {
                GVExchangeOrders.DataSource = dt;
                GVExchangeOrders.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                txtTitle.Text = "قائمة " + " من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim() + " لمشروع " + DLCategory.SelectedItem.ToString();
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

    protected void LBR_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageSupportByBeneficiaryMulti.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVExchangeOrders.Columns[0].Visible = false;
            GVExchangeOrders.Columns[10].Visible = false;

            GVExchangeOrders.UseAccessibleHeader = true;
            GVExchangeOrders.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnPrintMulti_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string XIDBill = string.Empty;
            foreach (GridViewRow row in GVExchangeOrders.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Label BillID = (Label)row.FindControl("lblIDBill");
                    Label ProjectID = (Label)row.FindControl("lblIDProject");
                    XIDBill += BillID.Text + ","; 
                }
            }
            Response.Redirect("PrintMultiCart.aspx?Type=Cart&Name=" + DLCategory.SelectedItem.ToString() + "&XIDCate=" +
                DLCategory.SelectedValue + "&IDBill=" + XIDBill.Substring(0, XIDBill.Length - 1));

        }
        catch (Exception)
        {
            return;
        }
    }

    int Cou = 0;
    decimal sum = 0;
    protected void GVExchangeOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label Count = (Label)e.Row.FindControl("lblCount");//take lable id
                //Cou += int.Parse(Count.Text);
                //lblSum.Text = Cou.ToString();

                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sum += decimal.Parse(salary.Text);
                if (sum != 0)
                    lblTotalPrice.Text = sum.ToString();
                else
                    lblTotalPrice.Text = "بإنتظار التسعير";

                lblMony.Text = ClassSaddam.FGetMonySa();
            }
        }
        catch (Exception)
        {

        }
    }

    public string FGetProject()
    {
        string XResult = DLCategory.SelectedItem.ToString();
        return XResult;
    }

}