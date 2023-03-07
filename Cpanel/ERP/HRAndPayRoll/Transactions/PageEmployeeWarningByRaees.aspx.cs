using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Transactions;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeWarningByRaees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A202");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            GVEmpWarningByRaees.Columns[0].Visible = true;
            GVEmpWarningByRaees.Columns[8].Visible = true;
            GVEmpWarningByRaees.UseAccessibleHeader = false;

            Model_EmployeeWarning_ MEW = new Model_EmployeeWarning_();
            MEW.IDCheck = "GetByRaees";
            MEW.EmployeeWarningID = Guid.Empty;
            MEW.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEW.Number_Warning = 0;
            MEW.CreatedDate = txtSearch.Text.Trim();
            MEW.Start_Date = string.Empty;
            MEW.End_Date = string.Empty;
            MEW.Is_Moder_Allow = true;
            MEW.Is_Raees_Lagnat_Allow = false;
            MEW.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeWarning_ REW = new Repostry_EmployeeWarning_();
            dt = REW.BErp_EmployeeWarning_Manage(MEW);
            if (dt.Rows.Count > 0)
            {
                GVEmpWarningByRaees.DataSource = dt;
                GVEmpWarningByRaees.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                      "عرض ملفات المشرف المختص لـ قائمة الإنذارات", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
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
        Response.Redirect("PageEmployeeWarningByRaees.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVEmpWarningByRaees.Columns[0].Visible = false;
            GVEmpWarningByRaees.Columns[8].Visible = false;

            GVEmpWarningByRaees.UseAccessibleHeader = true;
            GVEmpWarningByRaees.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            foreach (GridViewRow row in GVEmpWarningByRaees.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpWarningByRaees.DataKeys[row.RowIndex].Value);
                    Model_EmployeeWarning_ MEW = new Model_EmployeeWarning_()
                    {
                        IDCheck = "AllowModerInRaees",
                        EmployeeWarningID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        FinancialYear_Id = Guid.Empty,
                        Number_Warning = 0,
                        Date_Warning = string.Empty,
                        Title = string.Empty,
                        Details = string.Empty,
                        Moder_Raees = false,
                        ID_Moder = 0,
                        Is_Moder_Allow = true,
                        Is_Moder_Not_Allow = false,
                        Comments = txtComments.Text.Trim(),
                        ApplyDate = XDate,
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = true,
                        Is_Raees_Lagnat_Not_Allow = false,
                        Apply_Raees_Lagnat_Date = XDate,
                        ApprovedBy = txtComments.Text.Trim(),
                        CreatedDate = XDate,
                        CreatedBy = 0,
                        ModifiedBy = Test_Saddam.FGetIDUsiq(),
                        ModifiedDate = XDate,
                        IsActive = true,
                        DeleteBy = 0,
                        DeleteDate = XDate,
                    };
                    Repostry_EmployeeWarning_ REP = new Repostry_EmployeeWarning_();
                    string Xresult = REP.FErp_EmployeeWarning_Add(MEW);
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
            string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
            foreach (GridViewRow row in GVEmpWarningByRaees.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpWarningByRaees.DataKeys[row.RowIndex].Value);
                    Model_EmployeeWarning_ MEW = new Model_EmployeeWarning_()
                    {
                        IDCheck = "AllowModerInRaees",
                        EmployeeWarningID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        FinancialYear_Id = Guid.Empty,
                        Number_Warning = 0,
                        Date_Warning = string.Empty,
                        Title = string.Empty,
                        Details = string.Empty,
                        Moder_Raees = false,
                        ID_Moder = 0,
                        Is_Moder_Allow = false,
                        Is_Moder_Not_Allow = false,
                        Comments = txtComments.Text.Trim(),
                        ApplyDate = XDate,
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = false,
                        Is_Raees_Lagnat_Not_Allow = true,
                        Apply_Raees_Lagnat_Date = XDate,
                        ApprovedBy = txtComments.Text.Trim(),
                        CreatedDate = XDate,
                        CreatedBy = 0,
                        ModifiedBy = Test_Saddam.FGetIDUsiq(),
                        ModifiedDate = XDate,
                        IsActive = true,
                        DeleteBy = 0,
                        DeleteDate = XDate
                    };
                    Repostry_EmployeeWarning_ REP = new Repostry_EmployeeWarning_();
                    string Xresult = REP.FErp_EmployeeWarning_Add(MEW);
                    if (Xresult == "IsSuccessAllow")
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم إلغاء الطلب بنجاح ... ";
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