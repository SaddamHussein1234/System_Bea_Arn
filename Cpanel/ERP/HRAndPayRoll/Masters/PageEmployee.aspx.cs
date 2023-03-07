using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageEmployee : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A151", "A152", btnDelete, GVEmp, 0, 13);
            btnDelete.Visible = false;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            FGetData();
            Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                    "عرض بيانات الموظفين " , ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }

    private void FGetData()
    {
        try
        {
            Model_Employee_ ME = new Model_Employee_();
            ME.IDCheck = "GetAll";
            ME.EmployeeID = Guid.Empty;
            ME.FinancialYear_Id = Guid.Empty;
            ME.FirstName = txtSearch.Text.Trim();
            ME.Date_From = txtDateFrom.Text.Trim();
            ME.Date_To = txtDateTo.Text.Trim();
            ME.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Employee_ RE = new Repostry_Employee_();
            dt = RE.BErp_Employee_Master_Manage(ME);
            if (dt.Rows.Count > 0)
            {
                lblTitle.Text = " قائمة بيانات الموظفين من تاريخ " + txtDateFrom.Text.Trim() +
                    " إلى تاريخ " + txtDateTo.Text.Trim();
                GVEmp.DataSource = dt;
                GVEmp.DataBind();
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
        Response.Redirect("PageEmployee.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        FGetData();
        Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "بحث", "بحث : " + txtSearch.Text.Trim() + " في بيانات الموظفين", ClassSaddam.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss"));
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty;
            foreach (GridViewRow row in GVEmp.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVEmp.DataKeys[row.RowIndex].Value);

                    Model_Employee_ ME = new Model_Employee_()
                    {
                        IDCheck = "Delete",
                        EmployeeID = new Guid(Comp_ID),
                        EmployeeTypeId = Guid.Empty,
                        EmployeeGradeId = Guid.Empty,
                        DepartmentId = Guid.Empty,
                        DesignationId = Guid.Empty,
                        ShiftId = Guid.Empty,
                        FirstName = string.Empty,
                        MiddleName = string.Empty,
                        LastName = string.Empty,
                        BirthDate = string.Empty,
                        FatherName = string.Empty,
                        IsGender = false,
                        MaratialStatus = string.Empty,
                        Cast = string.Empty,
                        PhotoName = string.Empty,
                        CountryId = Guid.Empty,
                        StateId = Guid.Empty,
                        City = string.Empty,
                        Address = string.Empty,
                        PinCode = string.Empty,
                        MobileNo = string.Empty,
                        PhoneNo = string.Empty,
                        JoinDate = string.Empty,
                        EmployeeNo = 0,
                        PFNo = string.Empty,
                        Email = string.Empty,
                        BankName = string.Empty,
                        BranchName = string.Empty,
                        AccountName = string.Empty,
                        AccountNo = string.Empty,
                        CreatedDate = string.Empty,
                        CreatedBy = 0,
                        ModifiedBy = Test_Saddam.FGetIDUsiq(),
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false,
                        IsLeave = false,
                        LeaveDate = string.Empty,
                        LeaveDescription = string.Empty,
                        Img_Signature = string.Empty
                    };
                    //Signature
                    Repostry_Employee_ RE = new Repostry_Employee_();
                    Xresult = RE.FErp_Employee_Add(ME);
                }
            }
            if (Xresult == "IsSuccessDelete")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم حذف البيانات بنجاح ... ";
                FGetData();
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblWarning.Text = "خطأ غير متوقع حاول لاحقاً ... ";
            return;
        }
    }

}