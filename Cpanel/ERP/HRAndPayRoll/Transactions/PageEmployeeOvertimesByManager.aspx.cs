using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeOvertimesByManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FGetData();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarah);
            ClassAdmin_Arn.FGetRaeesLagnatAlBahath(DLRaeesLagnatAlBahath);
            ImgModer.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
            ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
            ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
        }
    }

    private void FGetData()
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            Model_EmployeeOverTime_ MEOT = new Model_EmployeeOverTime_();
            MEOT.IDCheck = "GetByAdmin";
            MEOT.EmployeeOverTimeMapID = Guid.Empty;
            MEOT.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEOT.Number_OverTime = 0;
            MEOT.CreatedDate = txtSearch.Text.Trim();
            MEOT.Start_Date = string.Empty;
            MEOT.End_Date = string.Empty;
            MEOT.Is_Moder_Allow = false;
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
        Response.Redirect("PageEmployeeOvertimesByManager.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
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
                        IDCheck = "AllowManager",
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
                        Is_Moder_Allow = true,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = false,
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
                        FGetData();
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم الموافقة بنجاح ... ";
                    }
                }
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
                        IDCheck = "AllowManager",
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
                        Is_Moder_Not_Allow = true,
                        Comments = txtComments.Text.Trim(),
                        ApplyDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = false,
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
                        FGetData();
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم الموافقة بنجاح ... ";
                    }
                }
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

    protected void DLModerAlGmeiah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgModer.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
    }

    protected void DLRaeesLagnatAlBahath_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
    }

    protected void DLRaeesMaglesAlEdarah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetData();
    }

}