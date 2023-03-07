using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeePermissionByRaees : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A198");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FGetData();
            Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                      "عرض ملفات المشرف المختص لـ قائمة الإستئذانات", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    private void FGetData()
    {
        try
        {
            GVEmpPermissionByManager.Columns[0].Visible = true;
            GVEmpPermissionByManager.Columns[10].Visible = true;
            GVEmpPermissionByManager.UseAccessibleHeader = false;

            Model_EmployeePermission_ MEP = new Model_EmployeePermission_();
            MEP.IDCheck = "GetByModer";
            MEP.EmployeePermissionMapID = Guid.Empty;
            MEP.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEP.Number_Permission = 0;
            MEP.CreatedDate = txtSearch.Text.Trim();
            MEP.Start_Date = string.Empty;
            MEP.End_Date = string.Empty;
            MEP.Is_Moder_Allow = false;
            MEP.Is_Moder_Not_Allow = false;
            MEP.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeePermission_ REP = new Repostry_EmployeePermission_();
            dt = REP.BErp_EmployeePermission_Manage(MEP);

            if (dt.Rows.Count > 0)
            {
                GVEmpPermissionByManager.DataSource = dt;
                GVEmpPermissionByManager.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                      "عرض ملفات المشرف المختص لـ قائمة الإستئذانات", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
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

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeePermissionByRaees.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVEmpPermissionByManager.Columns[0].Visible = false;
            GVEmpPermissionByManager.Columns[10].Visible = false;
            GVEmpPermissionByManager.UseAccessibleHeader = true;
            GVEmpPermissionByManager.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlprint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVEmpPermissionByManager.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string ID = Convert.ToString(GVEmpPermissionByManager.DataKeys[row.RowIndex].Value);
                    FAllow(new Guid(ID), true, false);
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

    private void FAllow(Guid XID,bool XValueAllow, bool XValueNotAllow)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        Model_EmployeePermission_ MEP = new Model_EmployeePermission_()
        {
            IDCheck = "AllowManager",
            EmployeePermissionMapID = XID,
            FinancialYear_Id = Guid.Empty,
            EmployeeId = Guid.Empty,
            Number_Permission = 0,
            PermissionTitle = string.Empty,
            Date_Permission = string.Empty,
            Is_Early_Dismissal = false,
            Is_Late_In_Attendance = false,
            Is_Exit_And_Return = false,
            From_The_Hour = string.Empty,
            To_The_Hour = string.Empty,
            Description = string.Empty,
            Moder_Raees = false,
            ID_Moder = 0,
            Is_Moder_Allow = XValueAllow,
            Is_Moder_Not_Allow = XValueNotAllow,
            Comment_Moder = txtComments.Text.Trim(),
            Date_Moder = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            CreatedDate = string.Empty,
            CreatedBy = 0,
            ModifiedDate = string.Empty,
            ModifiedBy = 0,
            IsActive = true,
            DeleteDate = string.Empty,
            DeleteBy = 0
        };
        Repostry_EmployeePermission_ REP = new Repostry_EmployeePermission_();
        string Xresult = REP.FErp_EmployeePermission_Add(MEP);
        if (Xresult == "IsSuccessAllow")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم الإطلاع بنجاح ... ";
        }
    }

    protected void btnNotAllow_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVEmpPermissionByManager.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string ID = Convert.ToString(GVEmpPermissionByManager.DataKeys[row.RowIndex].Value);
                    FAllow(new Guid(ID), false, true);
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetData();
    }

    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        FGetData();
    }

}