using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployLeave_PageEmployeeLeaveCategoryByAdmin : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    string IDUser = string.Empty;
    private void GetCookie()
    {
        try
        {
            HttpCookie Cooke;  // رقم المستخدم
            Cooke = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
            IDUser = ClassSaddam.UnprotectPassword(Cooke["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
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
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FData();
        }
    }

    private void FData()
    {
        if (XType == "Manager")
        {
            FGetData("GetByAdmin", 0);
            Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                      "عرض ملفات الموافقة عن الموظف البديل للمناوبة ", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
        }
        else if (XType == "Admin")
        {
            GetCookie();
            FGetData("GetByAdminAllow", Convert.ToInt64(IDUser));
            Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                      "عرض ملفات الموافقة عن الموظف البديل للمناوبة الخاصة به ", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    private void FGetData(string XScript, long IDUser)
    {
        try
        {
            GVEmpLeaveByAdmin.Columns[0].Visible = true;
            GVEmpLeaveByAdmin.Columns[11].Visible = true;
            GVEmpLeaveByAdmin.UseAccessibleHeader = false;
            Model_EmployeeLeaveCategory_ MELC = new Model_EmployeeLeaveCategory_();
            MELC.IDCheck = XScript;
            MELC.EmployeeLeaveCategoryMapID = Guid.Empty;
            MELC.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MELC.Number_Leave = IDUser;
            MELC.CreatedDate = txtSearch.Text.Trim();
            MELC.StartDate = string.Empty;
            MELC.EndDate = string.Empty;
            MELC.Is_Emp = false;
            MELC.Is_Moder_Allow = false;
            MELC.Is_Raees_Lagnat_Allow = false;
            MELC.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeLeaveCategory_ RELC = new Repostry_EmployeeLeaveCategory_();
            dt = RELC.BErp_EmployeeLeaveCategory_Manage(MELC);
            if (dt.Rows.Count > 0)
            {
                GVEmpLeaveByAdmin.DataSource = dt;
                GVEmpLeaveByAdmin.DataBind();
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
        Response.Redirect("PageEmployeeLeaveCategoryByAdmin.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVEmpLeaveByAdmin.Columns[0].Visible = false;
            GVEmpLeaveByAdmin.Columns[11].Visible = false;
            GVEmpLeaveByAdmin.UseAccessibleHeader = true;
            GVEmpLeaveByAdmin.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlprint;
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
        IDMessageWarning.Visible = false;
        try
        {
            string Xresult = string.Empty;
            foreach (GridViewRow row in GVEmpLeaveByAdmin.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpLeaveByAdmin.DataKeys[row.RowIndex].Value);
                    Model_EmployeeLeaveCategory_ MELC = new Model_EmployeeLeaveCategory_()
                    {
                        IDCheck = "AllowAdmin",
                        EmployeeLeaveCategoryMapID = new Guid(Comp_ID),
                        EmployeeId = Guid.NewGuid(),
                        LeaveCategoryId = Guid.NewGuid(),
                        Number_Leave = 0,
                        StartDate = string.Empty,
                        EndDate = string.Empty,
                        TotalDay = 0,
                        IsFirstHalfDay = false,
                        IsLastHalfDay = false,
                        Reason = string.Empty,
                        ID_Emp = 0,
                        Is_Emp = true,
                        Moder_Raees = false,
                        ID_Moder = 0,
                        Is_Moder_Allow = false,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = false,
                        Is_Raees_Lagnat_Not_Allow = false,
                        Apply_Raees_Lagnat_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ApprovedBy = "hradmin@arityinfoway.com",
                        ApproveDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false,
                        IsApprove = false
                    };
                    Repostry_EmployeeLeaveCategory_ RELC = new Repostry_EmployeeLeaveCategory_();
                    Xresult = RELC.FErp_EmployeeLeaveCategory_Add(MELC);
                }
            }
            if (Xresult == "IsSuccessAllow")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم الموافقة بنجاح ... ";
                FData();
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FData();
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FData();
    }

}