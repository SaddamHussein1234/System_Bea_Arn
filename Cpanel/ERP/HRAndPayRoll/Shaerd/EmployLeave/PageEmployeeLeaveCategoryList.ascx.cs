using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployLeave_PageEmployeeLeaveCategoryList : System.Web.UI.UserControl
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
                CLS_Permissions.CheckAccountAdmin("A160");
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FGetData();
            if (XType == "Manager")
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                   " عرض رصيد إجازات الموظفين", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
            else if (XType == "Admin")
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                   " عرض رصيد الإجازات الخاصه به", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    private void FGetData()
    {
        GetCookie();
        try
        {
            string XTypeEmp = string.Empty;
            Model_Employee_ ME = new Model_Employee_();
            if (XType == "Manager")
            { ME.IDCheck = "GetByLeaveList"; XTypeEmp = "الموظفين"; }
            else if (XType == "Admin")
            { ME.IDCheck = "GetByLeaveListByAdmin"; XTypeEmp = "الموظف"; }
            ME.EmployeeID = new Guid(IDUniq);
            ME.FinancialYear_Id = Guid.Empty;
            ME.FirstName = ddlYears.SelectedItem.ToString();
            ME.Date_From = string.Empty;
            ME.Date_To = string.Empty;
            ME.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Employee_ RE = new Repostry_Employee_();
            dt = RE.BErp_Employee_Master_Manage(ME);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "بيان تفصيلي لرصيد " + XTypeEmp + " من الاجازات لسنة " + ddlYears.SelectedItem.ToString() + " م";
                RPEmpLeave.DataSource = dt;
                RPEmpLeave.DataBind();
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
        Response.Redirect("PageEmployeeLeaveCategoryList.aspx");
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
        FGetData();
    }

    protected void RPEmpLeave_PreRender(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem item in RPEmpLeave.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {

                    Label lblLeave = (Label)item.FindControl("lblCountLeave");
                    Label lblLeaveUse = (Label)item.FindControl("lblCountLeaveUse");
                    Label lblTotal = (Label)item.FindControl("lblCountLeaveUseAllow");

                    if (lblLeave.Text == string.Empty || lblLeave.Text == "")
                    { lblLeave.Text = "0"; }
                    if (lblLeaveUse.Text == string.Empty || lblLeaveUse.Text == "")
                    { lblLeaveUse.Text = "0"; }

                    decimal sum = decimal.Parse(lblLeave.Text) - decimal.Parse(lblLeaveUse.Text);
                    lblTotal.Text = String.Format("{0:0.#}", sum);
                    //------------------------

                    Label lblEmergency = (Label)item.FindControl("lblCountEmergency");
                    Label lblEmergencyUse = (Label)item.FindControl("lblCountEmergencyUse");
                    Label lblEmergencyTotal = (Label)item.FindControl("lblCountEmergencyAllow");

                    if (lblEmergency.Text == string.Empty || lblEmergency.Text == "")
                    { lblEmergency.Text = "0"; }
                    if (lblEmergencyUse.Text == string.Empty || lblEmergencyUse.Text == "")
                    { lblEmergencyUse.Text = "0"; }

                    decimal SumEmergency = decimal.Parse(lblEmergency.Text) - decimal.Parse(lblEmergencyUse.Text);
                    lblEmergencyTotal.Text = String.Format("{0:0.#}", SumEmergency);
                    //-------------------------

                    Label lblCompensatory = (Label)item.FindControl("lblCountCompensatory");
                    Label lblCompensatoryUse = (Label)item.FindControl("lblCountCompensatoryUse");
                    Label lblCompensatoryUseAllow = (Label)item.FindControl("lblCountCompensatoryUseAllow");

                    if (lblCompensatory.Text == string.Empty || lblCompensatory.Text == "")
                    { lblCompensatory.Text = "0"; }
                    if (lblCompensatoryUse.Text == string.Empty || lblCompensatoryUse.Text == "")
                    { lblCompensatoryUse.Text = "0"; }

                    decimal SumCompensatory = decimal.Parse(lblCompensatory.Text) - decimal.Parse(lblCompensatoryUse.Text);
                    lblCompensatoryUseAllow.Text = String.Format("{0:0.#}", SumCompensatory);

                    Label lblTotalAllow = (Label)item.FindControl("lblCountTotalAllow");
                    decimal SumTotal = decimal.Parse(lblTotal.Text) + decimal.Parse(lblEmergencyTotal.Text) + decimal.Parse(lblCompensatoryUseAllow.Text);
                    lblTotalAllow.Text = String.Format("{0:0.#}", SumTotal);
                }
            }
        }
        catch (Exception)
        {

        }
    }

}