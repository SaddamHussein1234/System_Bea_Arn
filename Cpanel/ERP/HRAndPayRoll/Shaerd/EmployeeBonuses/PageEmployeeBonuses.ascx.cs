using System;
using System.Collections.Generic;
using System.Data;
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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeBonuses_PageEmployeeBonuses : System.Web.UI.UserControl
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
            pnlSelect.Visible = true;
            if (XType == "Manager")
            {
                CLS_Permissions.CheckAccountAdmin("A185", "A187", btnDelete, GVEmployeeBonuses, 0, 9); GVEmployeeBonuses.Columns[10].Visible = false;
            }
            else if (XType == "Admin")
            {
                btnDelete.Visible = false; GVEmployeeBonuses.Columns[0].Visible = false; GVEmployeeBonuses.Columns[9].Visible = false;
                IDAdd.Visible = false; GVEmployeeBonuses.Columns[10].Visible = true;
            }
            txtDateFrom.Text = ClassSaddam.FGetDateFrom_HRM();
            txtDateTo.Text = ClassSaddam.FGetDateToLast();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
        }
    }

    private void FGetData()
    {
        GetCookie();
        try
        {
            GVEmployeeBonuses.UseAccessibleHeader = false;
            Model_EmployeeBonuses_ MEB = new Model_EmployeeBonuses_();
            if (XType == "Manager")
            { MEB.IDCheck = "GetAll"; }
            else if (XType == "Admin")
            { MEB.IDCheck = "GetAllByAdmin"; }
            MEB.EmployeeBonusesMapID = new Guid(IDUniq);
            MEB.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEB.Number_Bonuses = 0;
            MEB.CreatedDate = txtSearch.Text.Trim();
            MEB.Date_From = txtDateFrom.Text.Trim();
            MEB.Date_To = txtDateTo.Text.Trim();
            MEB.Is_Moder_Allow = false;
            MEB.Is_Raees_Lagnat_Allow = false;
            MEB.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeBonuses_ REB = new Repostry_EmployeeBonuses_();
            dt = REB.BErp_EmployeeBonuses_Manage(MEB);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "إرشيف : " + ddlYears.SelectedItem.ToString() + ", قائمة قرارات المكافآت من تاريخ " + txtDateFrom.Text.Trim() +
                   " إلى تاريخ " + txtDateTo.Text.Trim();
                GVEmployeeBonuses.DataSource = dt;
                GVEmployeeBonuses.DataBind();
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

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVEmployeeBonuses.Columns[0].Visible = false;
            GVEmployeeBonuses.Columns[9].Visible = false;
            GVEmployeeBonuses.Columns[10].Visible = false;
            GVEmployeeBonuses.UseAccessibleHeader = true;
            GVEmployeeBonuses.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlprint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetData();
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        int XIDAdd = Test_Saddam.FGetIDUsiq();
        if (txtSearch.Text.Trim() == string.Empty)
        {
            if (XType == "Manager")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "عرض",
                       "عرض قرارات المكآفأت للموظفين", XDate);
            }
            else if (XType == "Admin")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "عرض",
                     "عرض قرارات المكآفأت الخاصة به", XDate);
            }
        }
        else if (txtSearch.Text.Trim() != string.Empty)
        {
            if (XType == "Manager")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "بحث",
                       "بحث " + txtSearch.Text.Trim() + " في قرارات المكآفأت للموظفين", XDate);
            }
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeBonuses.aspx");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            foreach (GridViewRow row in GVEmployeeBonuses.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmployeeBonuses.DataKeys[row.RowIndex].Value);
                    Model_EmployeeBonuses_ MEB = new Model_EmployeeBonuses_()
                    {
                        IDCheck = "Delete",
                        EmployeeBonusesMapID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        Number_Bonuses = 0,
                        Total_Amount = 0,
                        Date_Bonuses = string.Empty,
                        BonusesTitle = string.Empty,
                        Description = string.Empty,
                        ID_Moder = 0,
                        Is_Moder_Allow = true,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = string.Empty,
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = true,
                        Is_Raees_Lagnat_Not_Allow = false,
                        Note_Raees = string.Empty,
                        Apply_Raees_Lagnat_Date = string.Empty,
                        CreatedDate = string.Empty,
                        CreatedBy = 0,
                        ModifiedBy = Test_Saddam.FGetIDUsiq(),
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false
                    };
                    Repostry_EmployeeBonuses_ REB = new Repostry_EmployeeBonuses_();
                    string Xresult = REB.FErp_EmployeeBonuses_Add(MEB);
                    if (Xresult == "IsSuccessDelete")
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم حذف البيانات بنجاح ... ";
                    }
                }
            }
            FGetData();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        txtDateFrom.Text = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "01-01");
        txtDateTo.Text = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString(ddlYears.SelectedItem.ToString() + "-" + "12-31");
        FGetData();
    }

}