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

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeOvertimesByRaees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A184");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FGetData();
        } 
    }

    private void FGetData()
    {
        try
        {
            GVEmpOvertimeByManager.Columns[0].Visible = true;
            GVEmpOvertimeByManager.Columns[14].Visible = true;
            GVEmpOvertimeByManager.UseAccessibleHeader = false;

            Model_EmployeeOverTime_ MEOT = new Model_EmployeeOverTime_();
            MEOT.IDCheck = "GetByRaees";
            MEOT.EmployeeOverTimeMapID = Guid.Empty;
            MEOT.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEOT.Number_OverTime = 0;
            MEOT.CreatedDate = txtSearch.Text.Trim();
            MEOT.Start_Date = string.Empty;
            MEOT.End_Date = string.Empty;
            MEOT.Is_Moder_Allow = true;
            MEOT.Is_Raees_Lagnat_Allow = false;
            MEOT.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeOverTime_ REOT = new Repostry_EmployeeOverTime_();
            dt = REOT.BErp_EmployeeOverTime_Manage(MEOT);
            if (dt.Rows.Count > 0)
            {
                GVEmpOvertimeByManager.DataSource = dt;
                GVEmpOvertimeByManager.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                     "عرض ملفات المشرف المختص لـ قائمة العمل الإضافي", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
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
        Response.Redirect("PageEmployeeOvertimesByRaees.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVEmpOvertimeByManager.Columns[0].Visible = false;
            GVEmpOvertimeByManager.Columns[14].Visible = false;
            GVEmpOvertimeByManager.UseAccessibleHeader = true;
            GVEmpOvertimeByManager.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlprint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
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
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            foreach (GridViewRow row in GVEmpOvertimeByManager.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpOvertimeByManager.DataKeys[row.RowIndex].Value);
                    Model_EmployeeOverTime_ MEOT = new Model_EmployeeOverTime_()
                    {
                        IDCheck = "AllowRaees",
                        EmployeeOverTimeMapID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        Number_OverTime = 0,
                        Amount = 0,
                        Total_Amount = 0,
                        Start_Time = string.Empty,
                        End_Time = string.Empty,
                        Start_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        End_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        OverTimeTitle = string.Empty,
                        Description = string.Empty,
                        TotalDays = 0,
                        Hours_In_Day = 0,
                        ID_Moder = 0,
                        Is_Moder_Allow = false,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = true,
                        Is_Raees_Lagnat_Not_Allow = false,
                        Note_Raees = string.Empty,
                        Apply_Raees_Lagnat_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = true,
                        IsComplete = false
                    };
                    Repostry_EmployeeOverTime_ REOT = new Repostry_EmployeeOverTime_();
                    string Xresult = REOT.FErp_EmployeeOverTime_Add(MEOT);
                    if (Xresult == "IsSuccessAllow")
                    {
                        
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم الموافقة بنجاح ... ";
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

    protected void btnNotAllow_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            foreach (GridViewRow row in GVEmpOvertimeByManager.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpOvertimeByManager.DataKeys[row.RowIndex].Value);
                    Model_EmployeeOverTime_ MEOT = new Model_EmployeeOverTime_()
                    {
                        IDCheck = "AllowRaees",
                        EmployeeOverTimeMapID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        Number_OverTime = 0,
                        Amount = 0,
                        Total_Amount = 0,
                        Start_Time = string.Empty,
                        End_Time = string.Empty,
                        Start_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        End_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        OverTimeTitle = string.Empty,
                        Description = string.Empty,
                        TotalDays = 0,
                        Hours_In_Day = 0,
                        ID_Moder = 0,
                        Is_Moder_Allow = false,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = false,
                        Is_Raees_Lagnat_Not_Allow = true,
                        Note_Raees = txtComments.Text.Trim(),
                        Apply_Raees_Lagnat_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = true,
                        IsComplete = false
                    };
                    Repostry_EmployeeOverTime_ REOT = new Repostry_EmployeeOverTime_();
                    string Xresult = REOT.FErp_EmployeeOverTime_Add(MEOT);
                    if (Xresult == "IsSuccessAllow")
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم الموافقة بنجاح ... ";
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
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetData();
    }

}