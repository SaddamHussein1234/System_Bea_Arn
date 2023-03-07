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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeResolveds_PageEmployeeResolveds : System.Web.UI.UserControl
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
                CLS_Permissions.CheckAccountAdmin("A168", "A169", btnDelete1, GVEmpResolvedByRaees, 0, 9); GVEmpResolvedByRaees.Columns[10].Visible = false;
            }
            else if (XType == "Admin")
            {
                btnDelete1.Visible = false; GVEmpResolvedByRaees.Columns[0].Visible = false; GVEmpResolvedByRaees.Columns[9].Visible = false; IDAdd.Visible = false; GVEmpResolvedByRaees.Columns[10].Visible = true;
            }
            
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
            GVEmpResolvedByRaees.UseAccessibleHeader = false;

            Model_EmployeeResolved_ MER = new Model_EmployeeResolved_();
            if (XType == "Manager")
                MER.IDCheck = "GetAll";
            else if (XType == "Admin")
                MER.IDCheck = "GetAllByAdmin";
            MER.EmployeeResolvedID = new Guid(IDUniq);
            MER.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MER.Number_Resolved = 0;
            MER.CreatedDate = txtSearch.Text.Trim();
            MER.Date_From = txtDateFrom.Text.Trim();
            MER.Date_To = txtDateTo.Text.Trim();
            MER.Is_Moder_Allow = true;
            MER.Is_Raees_Lagnat_Allow = true;
            MER.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeResolved_ RER = new Repostry_EmployeeResolved_();
            dt = RER.BErp_EmployeeResolved_Manage(MER);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "إرشيف : " + ddlYears.SelectedItem.ToString() + ", قائمة الحسومات من تاريخ " + txtDateFrom.Text.Trim() +
                   " إلى تاريخ " + txtDateTo.Text.Trim();
                GVEmpResolvedByRaees.DataSource = dt;
                GVEmpResolvedByRaees.DataBind();
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
        Response.Redirect("PageEmployeeResolveds.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            GVEmpResolvedByRaees.Columns[0].Visible = false;
            GVEmpResolvedByRaees.Columns[9].Visible = false;
            GVEmpResolvedByRaees.Columns[10].Visible = false;

            GVEmpResolvedByRaees.UseAccessibleHeader = true;
            GVEmpResolvedByRaees.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                       "عرض حسومات الموظفين", XDate);
            }
            else if (XType == "Admin")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "عرض",
                     "عرض حسومات الخاصة به", XDate);
            }
        }
        else if (txtSearch.Text.Trim() != string.Empty)
        {
            if (XType == "Manager")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "بحث",
                       "بحث " + txtSearch.Text.Trim() + " في حسومات الموظفين", XDate);
            }
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

    protected void btnDelete1_Click(object sender, EventArgs e)
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
                        IDCheck = "Delete",
                        EmployeeResolvedID = new Guid(Comp_ID),
                        EmployeeId = Guid.Empty,
                        FinancialYear_Id = Guid.Empty,
                        Number_Resolved = 0,
                        Date_Resolved = string.Empty,
                        ID_ResolvedPeriod = Guid.Empty,
                        Reason = string.Empty,
                        Moder_Raees = false,
                        ID_Moder = 0,
                        Is_Moder_Allow = true,
                        Is_Moder_Not_Allow = false,
                        Comments = string.Empty,
                        ApplyDate = string.Empty,
                        ID_Raees_Lagnat = 0,
                        Is_Raees_Lagnat_Allow = false,
                        Is_Raees_Lagnat_Not_Allow = false,
                        Apply_Raees_Lagnat_Date = string.Empty,
                        ApprovedBy = "0",
                        CreatedDate = string.Empty,
                        CreatedBy = 0,
                        ModifiedBy = Test_Saddam.FGetIDUsiq(),
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false
                    };
                    Repostry_EmployeeResolved_ REP = new Repostry_EmployeeResolved_();
                    string Xresult = REP.FErp_EmployeeResolved_Add(MER);
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

}