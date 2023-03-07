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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeePermission_PageEmployeePermission : System.Web.UI.UserControl
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
                CLS_Permissions.CheckAccountAdmin("A195", "A196", btnDelete1, GVEmpPermission, 0, 10); GVEmpPermission.Columns[11].Visible = false;
            }
            else if (XType == "Admin")
            {
                btnDelete1.Visible = false; GVEmpPermission.Columns[0].Visible = false; GVEmpPermission.Columns[10].Visible = false; 
                IDAdd.Visible = false; GVEmpPermission.Columns[11].Visible = true;
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
            GVEmpPermission.UseAccessibleHeader = false;

            Model_EmployeePermission_ MEP = new Model_EmployeePermission_();
            if (XType == "Manager")
                MEP.IDCheck = "GetAll";
            else if (XType == "Admin")
                MEP.IDCheck = "GetAllByAdmin";
            MEP.EmployeePermissionMapID = new Guid(IDUniq);
            MEP.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEP.Number_Permission = 0;
            MEP.CreatedDate = txtSearch.Text.Trim();
            MEP.Start_Date = txtDateFrom.Text.Trim();
            MEP.End_Date = txtDateTo.Text.Trim();
            MEP.Is_Moder_Allow = false;
            MEP.Is_Moder_Not_Allow = false;
            MEP.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeePermission_ REP = new Repostry_EmployeePermission_();
            dt = REP.BErp_EmployeePermission_Manage(MEP);

            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "إرشيف : " + ddlYears.SelectedItem.ToString() + ", قائمة الإستئذانات من تاريخ " + txtDateFrom.Text.Trim() +
                   " إلى تاريخ " + txtDateTo.Text.Trim();
                GVEmpPermission.DataSource = dt;
                GVEmpPermission.DataBind();
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
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeePermission.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVEmpPermission.Columns[0].Visible = false;
            GVEmpPermission.Columns[10].Visible = false;
            GVEmpPermission.Columns[11].Visible = false;
            GVEmpPermission.UseAccessibleHeader = true;
            GVEmpPermission.HeaderRow.TableSection = TableRowSection.TableHeader;
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
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetData();
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        int XIDAdd = Test_Saddam.FGetIDUsiq();
        if (txtSearch.Text.Trim() == string.Empty)
        {
            if (XType == "Manager")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "عرض",
                       "عرض إستئذانات الموظفين", XDate);
            }
            else if (XType == "Admin")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "عرض",
                     "عرض الإستئذانات الخاصة به", XDate);
            }
        }
        else if (txtSearch.Text.Trim() != string.Empty)
        {
            if (XType == "Manager")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "بحث",
                       "بحث " + txtSearch.Text.Trim() + " في إستئذانات الموظفين", XDate);
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

}