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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployLeave_PageEmployeeCompensatories : System.Web.UI.UserControl
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
                CLS_Permissions.CheckAccountAdmin("A158", "A161", btnDelete1, GVEmpLeave, 0, 8);
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                   " عرض الإجازات التعويضية" , ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else if (XType == "Admin")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                  " عرض الإجازات التعويضية الخاصة به", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
                btnDelete1.Visible = false; GVEmpLeave.Columns[0].Visible = false; GVEmpLeave.Columns[8].Visible = false; IDAdd.Visible = false;
            }
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            txtDateFrom.Text = ClassSaddam.FGetDateFrom_HRM();
            txtDateTo.Text = ClassSaddam.FGetDateToLast();
        }
    }

    private void FGetData()
    {
        GetCookie();
        try
        {
            GVEmpLeave.UseAccessibleHeader = false;
            Model_EmployeeCompensatory_ MEC = new Model_EmployeeCompensatory_();
            if (XType == "Manager")
                MEC.IDCheck = "GetAll";
            else if (XType == "Admin")
                MEC.IDCheck = "GetAllByAdmin";
            MEC.EmployeeLeaveCompensatoryMapID = new Guid(IDUniq);
            MEC.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEC.Number_Leave = 0;
            MEC.CreatedDate = txtSearch.Text.Trim();
            MEC.StartDate = txtDateFrom.Text.Trim();
            MEC.EndDate = txtDateTo.Text.Trim();
            MEC.Is_Moder_Allow = false;
            MEC.Is_Raees_Lagnat_Allow = false;
            MEC.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeCompensatory_ REC = new Repostry_EmployeeCompensatory_();
            dt = REC.BErp_EmployeeLeaveCompensatory_Manage(MEC);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "إرشيف : " + ddlYears.SelectedItem.ToString() + ", قائمة الإجازات التعويضية من تاريخ " + txtDateFrom.Text.Trim() +
                   " إلى تاريخ " + txtDateTo.Text.Trim();
                GVEmpLeave.DataSource = dt;
                GVEmpLeave.DataBind();
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
        Response.Redirect("PageEmployeeCompensatories.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVEmpLeave.Columns[0].Visible = false;
            GVEmpLeave.Columns[8].Visible = false;
            GVEmpLeave.UseAccessibleHeader = true;
            GVEmpLeave.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlprint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            foreach (GridViewRow row in GVEmpLeave.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmpLeave.DataKeys[row.RowIndex].Value);

                    Model_EmployeeCompensatory_ MEC = new Model_EmployeeCompensatory_()
                    {
                        IDCheck = "Delete",
                        EmployeeLeaveCompensatoryMapID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        FinancialYear_Id = Guid.Empty,
                        Number_Leave = 0,
                        StartDate = string.Empty,
                        EndDate = string.Empty,
                        TotalDay = 0,
                        Reason = string.Empty,
                        ID_Moder = 0,
                        Is_Moder_Allow = false,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = string.Empty,
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = false,
                        Is_Raees_Lagnat_Not_Allow = false,
                        Apply_Raees_Lagnat_Date = string.Empty,
                        ApprovedBy = string.Empty,
                        CreatedDate = string.Empty,
                        CreatedBy = 0,
                        ModifiedBy = Test_Saddam.FGetIDUsiq(),
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false,
                        IsApprove = false
                    };
                    Repostry_EmployeeCompensatory_ REC = new Repostry_EmployeeCompensatory_();
                    string Xresult = REC.FErp_EmployeeLeaveCompensatory_Add(MEC);

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
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        FGetData();
        Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "بحث",
                   " بحث : " + txtSearch.Text.Trim() + " , في الإجازات التعويضية", ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
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