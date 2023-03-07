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
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeResolvedsByRaees : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A171");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            GVEmpResolvedByRaees.Columns[0].Visible = true;
            GVEmpResolvedByRaees.Columns[9].Visible = true;
            GVEmpResolvedByRaees.UseAccessibleHeader = false;

            Model_EmployeeResolved_ MER = new Model_EmployeeResolved_();
            MER.IDCheck = "GetByRaees";
            MER.EmployeeResolvedID = Guid.Empty;
            MER.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MER.Number_Resolved = 0;
            MER.CreatedDate = txtSearch.Text.Trim();
            MER.Date_From = string.Empty;
            MER.Date_To = string.Empty;
            MER.Is_Moder_Allow = true;
            MER.Is_Raees_Lagnat_Allow = false;
            MER.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeResolved_ RER = new Repostry_EmployeeResolved_();
            dt = RER.BErp_EmployeeResolved_Manage(MER);
            if (dt.Rows.Count > 0)
            {
                GVEmpResolvedByRaees.DataSource = dt;
                GVEmpResolvedByRaees.DataBind();
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
        Response.Redirect("PageEmployeeResolvedsByRaees.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVEmpResolvedByRaees.Columns[0].Visible = false;
            GVEmpResolvedByRaees.Columns[9].Visible = false;

            GVEmpResolvedByRaees.UseAccessibleHeader = true;
            GVEmpResolvedByRaees.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            foreach (GridViewRow row in GVEmpResolvedByRaees.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpResolvedByRaees.DataKeys[row.RowIndex].Value);
                    Model_EmployeeResolved_ MER = new Model_EmployeeResolved_()
                    {
                        IDCheck = "AllowRaees",
                        EmployeeResolvedID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        FinancialYear_Id = Guid.Empty,
                        Number_Resolved = 0,
                        Date_Resolved = string.Empty,
                        ID_ResolvedPeriod = Guid.Empty,
                        Reason = string.Empty,
                        Moder_Raees = false,
                        ID_Moder = 0,
                        Is_Moder_Allow = false,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = XDate,
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = true,
                        Is_Raees_Lagnat_Not_Allow = false,
                        Apply_Raees_Lagnat_Date = XDate,
                        ApprovedBy = "0",
                        CreatedDate = XDate,
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = XDate,
                        IsActive = true
                    };
                    Repostry_EmployeeResolved_ REP = new Repostry_EmployeeResolved_();
                    string Xresult = REP.FErp_EmployeeResolved_Add(MER);
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
            foreach (GridViewRow row in GVEmpResolvedByRaees.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpResolvedByRaees.DataKeys[row.RowIndex].Value);
                    Model_EmployeeResolved_ MER = new Model_EmployeeResolved_()
                    {
                        IDCheck = "AllowRaees",
                        EmployeeResolvedID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        FinancialYear_Id = Guid.Empty,
                        Number_Resolved = 0,
                        Date_Resolved = string.Empty,
                        ID_ResolvedPeriod = Guid.Empty,
                        Reason = string.Empty,
                        Moder_Raees = false,
                        ID_Moder = 0,
                        Is_Moder_Allow = false,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = false,
                        Is_Raees_Lagnat_Not_Allow = true,
                        Apply_Raees_Lagnat_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        ApprovedBy = txtComments.Text.Trim(),
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = true
                    };
                    Repostry_EmployeeResolved_ REP = new Repostry_EmployeeResolved_();
                    string Xresult = REP.FErp_EmployeeResolved_Add(MER);
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