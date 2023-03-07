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

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeBonusesByRaees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A188");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            GVEmpBonusesByManager.Columns[0].Visible = true;
            GVEmpBonusesByManager.Columns[9].Visible = true;
            GVEmpBonusesByManager.UseAccessibleHeader = false;

            Model_EmployeeBonuses_ MEB = new Model_EmployeeBonuses_();
            MEB.IDCheck = "GetByRaees";
            MEB.EmployeeBonusesMapID = Guid.Empty;
            MEB.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEB.Number_Bonuses = 0;
            MEB.CreatedDate = txtSearch.Text.Trim();
            MEB.Date_From = string.Empty;
            MEB.Date_To = string.Empty;
            MEB.Is_Moder_Allow = true;
            MEB.Is_Raees_Lagnat_Allow = false;
            MEB.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeBonuses_ REOT = new Repostry_EmployeeBonuses_();
            dt = REOT.BErp_EmployeeBonuses_Manage(MEB);
            if (dt.Rows.Count > 0)
            {
                GVEmpBonusesByManager.DataSource = dt;
                GVEmpBonusesByManager.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                      "عرض ملفات المشرف المختص لـ قائمة قرارات المكافآت", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
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
        Response.Redirect("PageEmployeeBonusesByRaees.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVEmpBonusesByManager.Columns[0].Visible = false;
            GVEmpBonusesByManager.Columns[9].Visible = false;
            GVEmpBonusesByManager.UseAccessibleHeader = true;
            GVEmpBonusesByManager.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData();
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            foreach (GridViewRow row in GVEmpBonusesByManager.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpBonusesByManager.DataKeys[row.RowIndex].Value);
                    Model_EmployeeBonuses_ MEB = new Model_EmployeeBonuses_()
                    {
                        IDCheck = "AllowRaees",
                        EmployeeBonusesMapID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        Number_Bonuses = 0,
                        Total_Amount = 0,
                        Date_Bonuses = string.Empty,
                        BonusesTitle = txtTitle.Text.Trim(),
                        Description = string.Empty,
                        ID_Moder = 0,
                        Is_Moder_Allow = true,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = true,
                        Is_Raees_Lagnat_Not_Allow = false,
                        Note_Raees = string.Empty,
                        Apply_Raees_Lagnat_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedBy = Test_Saddam.FGetIDUsiq(),
                        ModifiedBy = 0,
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = true
                    };
                    Repostry_EmployeeBonuses_ REB = new Repostry_EmployeeBonuses_();
                    string Xresult = REB.FErp_EmployeeBonuses_Add(MEB);
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
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            foreach (GridViewRow row in GVEmpBonusesByManager.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpBonusesByManager.DataKeys[row.RowIndex].Value);
                    Model_EmployeeBonuses_ MEB = new Model_EmployeeBonuses_()
                    {
                        IDCheck = "AllowRaees",
                        EmployeeBonusesMapID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        Number_Bonuses = 0,
                        Total_Amount = 0,
                        Date_Bonuses = string.Empty,
                        BonusesTitle = txtTitle.Text.Trim(),
                        Description = string.Empty,
                        ID_Moder = 0,
                        Is_Moder_Allow = true,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = false,
                        Is_Raees_Lagnat_Not_Allow = true,
                        Note_Raees = txtComments.Text.Trim(),
                        Apply_Raees_Lagnat_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedBy = Test_Saddam.FGetIDUsiq(),
                        ModifiedBy = 0,
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = true
                    };
                    Repostry_EmployeeBonuses_ REB = new Repostry_EmployeeBonuses_();
                    string Xresult = REB.FErp_EmployeeBonuses_Add(MEB);
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
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData();
    }

}