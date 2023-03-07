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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeOvertimes_PageEmployeeOvertimes : System.Web.UI.UserControl
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
                CLS_Permissions.CheckAccountAdmin("A181", "A183", btnDelete, GVEmployeeOvertimes, 0, 14); GVEmployeeOvertimes.Columns[15].Visible = false;
            }
            else if (XType == "Admin")
            {
                btnDelete.Visible = false; GVEmployeeOvertimes.Columns[0].Visible = false; GVEmployeeOvertimes.Columns[14].Visible = false;
                IDAdd.Visible = false; GVEmployeeOvertimes.Columns[15].Visible = true;
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
            string XTypeEmp = string.Empty;
            GVEmployeeOvertimes.UseAccessibleHeader = false;
            Model_EmployeeOverTime_ MEOT = new Model_EmployeeOverTime_();
            if (XType == "Manager")
            { MEOT.IDCheck = "GetAll"; XTypeEmp = "الموظفين"; }
            else if (XType == "Admin")
            { MEOT.IDCheck = "GetAllByAdmin"; XTypeEmp = "الموظف"; }
            MEOT.EmployeeOverTimeMapID = new Guid(IDUniq);
            MEOT.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEOT.Number_OverTime = 0;
            MEOT.CreatedDate = txtSearch.Text.Trim();
            MEOT.Start_Date = txtDateFrom.Text.Trim();
            MEOT.End_Date = txtDateTo.Text.Trim();
            MEOT.Is_Moder_Allow = false;
            MEOT.Is_Raees_Lagnat_Allow = false;
            MEOT.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeOverTime_ REOT = new Repostry_EmployeeOverTime_();
            dt = REOT.BErp_EmployeeOverTime_Manage(MEOT);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "إرشيف : " + ddlYears.SelectedItem.ToString() + ", قائمة قرارات العمل الإضافي من تاريخ " + txtDateFrom.Text.Trim() +
                   " إلى تاريخ " + txtDateTo.Text.Trim();

                GVEmployeeOvertimes.DataSource = dt;
                GVEmployeeOvertimes.DataBind();
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
        Response.Redirect("PageEmployeeOvertimes.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData();
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        int XIDAdd = Test_Saddam.FGetIDUsiq();
        if (txtSearch.Text.Trim() == string.Empty)
        {
            if (XType == "Manager")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "عرض",
                       "عرض قرارات العمل الإضافي للموظفين", XDate);
            }
            else if (XType == "Admin")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "عرض",
                     "عرض قرارات العمل الإضافي الخاصة به", XDate);
            }
        }
        else if (txtSearch.Text.Trim() != string.Empty)
        {
            if (XType == "Manager")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "بحث",
                       "بحث " + txtSearch.Text.Trim() + " في قرارات العمل الإضافي للموظفين", XDate);
            }
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVEmployeeOvertimes.Columns[0].Visible = false;
            GVEmployeeOvertimes.Columns[14].Visible = false;
            GVEmployeeOvertimes.Columns[15].Visible = false;
            GVEmployeeOvertimes.UseAccessibleHeader = true;
            GVEmployeeOvertimes.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlprint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            foreach (GridViewRow row in GVEmployeeOvertimes.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmployeeOvertimes.DataKeys[row.RowIndex].Value);
                    Model_EmployeeOverTime_ MEOT = new Model_EmployeeOverTime_()
                    {
                        IDCheck = "Delete",
                        EmployeeOverTimeMapID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        Number_OverTime = 0,
                        Amount = 0,
                        Total_Amount = 0,
                        Start_Time = string.Empty,
                        End_Time = string.Empty,
                        Start_Date = string.Empty,
                        End_Date = string.Empty,
                        OverTimeTitle = string.Empty,
                        Description = string.Empty,
                        TotalDays = 0,
                        Hours_In_Day = 0,
                        ID_Moder = 0,
                        Is_Moder_Allow = false,
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
                        IsActive = false,
                        IsComplete = false
                    };
                    Repostry_EmployeeOverTime_ REOT = new Repostry_EmployeeOverTime_();
                    string Xresult = REOT.FErp_EmployeeOverTime_Add(MEOT);
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