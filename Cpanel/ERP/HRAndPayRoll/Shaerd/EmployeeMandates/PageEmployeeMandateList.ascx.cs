using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeMandates_PageEmployeeMandateList : System.Web.UI.UserControl
{
    public string XType = string.Empty;
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
        if (!IsPostBack)
        {
            if (XType == "Manager")
                CLS_Permissions.CheckAccountAdmin("A178");
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
            string XTypeEmp = string.Empty;
            int _Month = ClassSaddam.GetCurrentTime().Month;
            int _Year = ClassSaddam.GetCurrentTime().Year;

            string[] _SplitDate = ddlMonth.SelectedValue.Split('_');

            if (_SplitDate.Length > 1)
            {
                _Month = Convert.ToInt32(_SplitDate[0]);
                _Year = Convert.ToInt32(_SplitDate[1]);
            }
            string XIDCheck = string.Empty;
            if (XType == "Manager")
            { XIDCheck = "GetByList"; XTypeEmp = "الموظفين"; }
            else if (XType == "Admin")
            { XIDCheck = "GetByListByAdmin"; XTypeEmp = "الموظف"; }
            DataTable dt = new DataTable();
            dt = Repostry_EmployeeMandate_.FGetDataInDataTable(XIDCheck, new Guid(IDUniq), Guid.Empty, 0, string.Empty,
                _Month.ToString(), _Year.ToString(), false, false, true);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "مسير إنتداب "+ XTypeEmp + " لشهر (" + ddlMonth.SelectedItem.ToString() + ")";
                GVEmployeeMandates.DataSource = dt;
                GVEmployeeMandates.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;

                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                   "عرض مسير الإنتدابات للموظفين لشهر " + ddlMonth.SelectedItem.Text, ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
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

    protected void GVEmployeeMandates_PreRender(object sender, EventArgs e)
    {
        try
        {
            decimal SumlblTotalAmount = 0;
            foreach (RepeaterItem item in GVEmployeeMandates.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    Label lblTotalAmount = (Label)item.FindControl("lblTotal_Amount");

                    SumlblTotalAmount += decimal.Parse(lblTotalAmount.Text);

                    List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                    currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                    ToWord toWord = new ToWord(SumlblTotalAmount, currencies[Convert.ToInt32(0)]);
                    lblSumWord.Text = toWord.ConvertToArabic();

                    lbl_Sum.Text = String.Format("{0:0.#}", SumlblTotalAmount) + ClassSaddam.FGetMonySa();
                }
            }
        }
        catch (Exception)
        {
            return;
        }
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeMandateList.aspx");
    }

}