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

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeWarning_PageEmployeeWarning : System.Web.UI.UserControl
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
                CLS_Permissions.CheckAccountAdmin("A199", "A200", btnDelete1, GVEmpWarning, 0, 8); GVEmpWarning.Columns[9].Visible = false;
            }
            else if (XType == "Admin")
            {
                btnDelete1.Visible = false; GVEmpWarning.Columns[0].Visible = false; GVEmpWarning.Columns[8].Visible = false; IDAdd.Visible = false; GVEmpWarning.Columns[9].Visible = true;
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
            GVEmpWarning.UseAccessibleHeader = false;

            Model_EmployeeWarning_ MEW = new Model_EmployeeWarning_();
            if (XType == "Manager")
                MEW.IDCheck = "GetAll";
            else if (XType == "Admin")
                MEW.IDCheck = "GetAllByAdmin";
            MEW.EmployeeWarningID = new Guid(IDUniq);
            MEW.FinancialYear_Id = new Guid(ddlYears.SelectedValue);
            MEW.Number_Warning = 0;
            MEW.CreatedDate = txtSearch.Text.Trim();
            MEW.Start_Date = txtDateFrom.Text.Trim();
            MEW.End_Date = txtDateTo.Text.Trim();
            MEW.Is_Moder_Allow = false;
            MEW.Is_Moder_Not_Allow = false;
            MEW.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeWarning_ REW = new Repostry_EmployeeWarning_();
            dt = REW.BErp_EmployeeWarning_Manage(MEW);

            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "إرشيف : " + ddlYears.SelectedItem.ToString() + ", قائمة الإنذارات من تاريخ " + txtDateFrom.Text.Trim() +
                   " إلى تاريخ " + txtDateTo.Text.Trim();
                GVEmpWarning.DataSource = dt;
                GVEmpWarning.DataBind();
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
        Response.Redirect("PageEmployeeWarning.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVEmpWarning.Columns[0].Visible = false;
            GVEmpWarning.Columns[8].Visible = false;
            GVEmpWarning.Columns[9].Visible = false;
            GVEmpWarning.UseAccessibleHeader = true;
            GVEmpWarning.HeaderRow.TableSection = TableRowSection.TableHeader;
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
                       "عرض إنذارات الموظفين", XDate);
            }
            else if (XType == "Admin")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "عرض",
                     "عرض الإنذارات الخاصة به", XDate);
            }
        }
        else if (txtSearch.Text.Trim() != string.Empty)
        {
            if (XType == "Manager")
            {
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), XIDAdd, "HRM", "نظام الموارد البشرية", "بحث",
                       "بحث " + txtSearch.Text.Trim() + " في إنذارات الموظفين", XDate);
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