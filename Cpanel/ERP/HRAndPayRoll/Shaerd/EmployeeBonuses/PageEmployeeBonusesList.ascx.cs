using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeBonuses_PageEmployeeBonusesList : System.Web.UI.UserControl
{
    public string XType = string.Empty, XMony = string.Empty;
    string IDUniq = string.Empty;
    private void GetCookie()
    {
        try
        {
            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
        }
        catch
        {
            Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        XMony = ClassSaddam.FGetMonySa();
        if (!IsPostBack)
        {
            if (XType == "Manager")
                CLS_Permissions.CheckAccountAdmin("A186");
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FillMonth();
        }
    }

    private void FillMonth()
    {
        int _FinancialYear = Convert.ToInt32(ddlYears.SelectedItem.ToString());
        int _no = 0;

        bool _Flag = true;
        for (int no = 1; no < 13; no++)
        {
            _no = _no + 1;
            ddlMonth.Items.Insert(_no, new ListItem() { Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(no) + " " + no.ToString() + "-" + _FinancialYear, Value = Convert.ToString(no) + "_" + _FinancialYear });

            if (no == ClassSaddam.GetCurrentTime().Month && _FinancialYear == ClassSaddam.GetCurrentTime().Year)
            {
                _Flag = false;
                break;
            }
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeBonusesList.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["footable1"] = pnlPrint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnlSelect.Visible = true;
        ddlMonth.Items.Clear(); ddlMonth.Items.Add(""); ddlMonth.AppendDataBoundItems = true;
        FillMonth();
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetData();
    }

    private void FGetData()
    {
        GetCookie();
        try
        {
            int _Month = ClassSaddam.GetCurrentTime().Month;
            int _Year = ClassSaddam.GetCurrentTime().Year;

            string[] _SplitDate = ddlMonth.SelectedValue.Split('_');

            if (_SplitDate.Length > 1)
            {
                _Month = Convert.ToInt32(_SplitDate[0]);
                _Year = Convert.ToInt32(_SplitDate[1]);
            }

            Model_EmployeeBonuses_ MEB = new Model_EmployeeBonuses_();
            if (XType == "Manager")
                MEB.IDCheck = "GetByList";
            else if (XType == "Admin")
                MEB.IDCheck = "GetByListByAdmin";
            MEB.EmployeeBonusesMapID = new Guid(IDUniq);
            MEB.FinancialYear_Id = Guid.Empty;
            MEB.Number_Bonuses = 0;
            MEB.CreatedDate = string.Empty;
            MEB.Date_From = _Month.ToString();
            MEB.Date_To = _Year.ToString();
            MEB.Is_Moder_Allow = false;
            MEB.Is_Raees_Lagnat_Allow = false;
            MEB.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeBonuses_ REB = new Repostry_EmployeeBonuses_();
            dt = REB.BErp_EmployeeBonuses_Manage(MEB);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "مسير المكافآت لشهر (" + ddlMonth.SelectedItem.ToString() + ")";
                GVEmployeeBonuses.DataSource = dt;
                GVEmployeeBonuses.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;

                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                   "عرض مسير المكآفئات للموظفين لشهر " + ddlMonth.SelectedItem.Text, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
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

    protected void GVEmployeeOvertime_PreRender(object sender, EventArgs e)
    {
        try
        {
            decimal SumlblTotalAmount = 0;
            foreach (RepeaterItem item in GVEmployeeBonuses.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblTotalAmount = (Label)item.FindControl("lblTotal_Amount");

                    SumlblTotalAmount += decimal.Parse(lblTotalAmount.Text);

                    List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                    currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                    ToWord toWord = new ToWord(SumlblTotalAmount, currencies[Convert.ToInt32(0)]);
                    lblSumWord.Text = toWord.ConvertToArabic();

                    lbl_Sum.Text = String.Format("{0:0.#}", SumlblTotalAmount) + XMony;
                }
            }
        }
        catch (Exception)
        {

        }
    }

}