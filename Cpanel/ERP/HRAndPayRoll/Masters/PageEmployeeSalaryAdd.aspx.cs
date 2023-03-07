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

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageEmployeeSalaryAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A154");
            Repostry_Department_.FErp_Department_Manage(ddlDepartment);
            Repostry_Allowance_.FErp_Allowance_Manage(rptAllowance);
            Repostry_Allowance_.FErp_Allowance_Manage(ddlAllowance);
            Repostry_Deduction_.FErp_Deduction_Manage(rptDeduction);
            Repostry_Deduction_.FErp_Deduction_Manage(ddlDeduction);
            if (Request.QueryString["ID"] != null)
            {
                pnlAllowanceAdd.Visible = false; pnlAllowanceEdit.Visible = true;
                pnlDeductionAdd.Visible = false; pnlDeductionEdit.Visible = true;
                FGetData();
            }
            else
            {
                pnlAllowanceAdd.Visible = true; pnlAllowanceEdit.Visible = false;
                pnlDeductionAdd.Visible = true; pnlDeductionEdit.Visible = false;
            }       
        }
    }

    private void FGetData()
    {
        try
        {
            Model_EmployeeSalary_ MES = new Model_EmployeeSalary_();
            MES.IDCheck = "GetByIDUniq";
            MES.EmployeeSalaryID = new Guid(Request.QueryString["ID"]);
            MES.CreatedDate = string.Empty;
            MES.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeSalary_ RES = new Repostry_EmployeeSalary_();
            dt = RES.BErp_EmployeeSalary_Manage(MES);
            if (dt.Rows.Count > 0)
            {
                HFIDEmployee.Value = dt.Rows[0]["EmployeeId"].ToString();
                if (Convert.ToBoolean(dt.Rows[0]["IsMonthlySalary"]))
                {
                    rbtnMonthSalary.Checked = true; rbtnHourSalary.Checked = false;
                }
                else
                {
                    rbtnMonthSalary.Checked = false; rbtnHourSalary.Checked = true;
                }
                FCheck();
                lblDepartment.Visible = true;
                lblDepartment.Text = dt.Rows[0]["Department"].ToString();
                ddlDepartment.Visible = false; rfvDepartment.Visible = false;
                lblTitleDepartment.Text = "ينتمي إلى إدارة : ";

                lblEmployee.Visible = true;
                lblEmployee.Text = dt.Rows[0]["_Name"].ToString();
                ddlEmployee.Visible = false; rfvEmployee.Visible = false;
                lblTitleEmployee.Text = "إسم الموظف : ";
                txtBasic.Text = dt.Rows[0]["Basic"].ToString();
                FGetEmployeeAllowanceMap(new Guid(HFIDEmployee.Value));
            }
            else
                Response.Redirect("PageEmployeeSalary.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageEmployeeSalary.aspx");
        }
    }

    private void FGetEmployeeAllowanceMap(Guid XID)
    {
        Model_EmployeeAllowanceMap_ MEAM = new Model_EmployeeAllowanceMap_();
        MEAM.IDCheck = "GetByIDEmp";
        MEAM.EmployeeId = XID;
        MEAM.IsActive = true;
        DataTable dt = new DataTable();
        Repostry_EmployeeAllowanceMap_ REAM = new Repostry_EmployeeAllowanceMap_();
        dt = REAM.BErp_EmployeeAllowanceMap_Manage(MEAM);
        if (dt.Rows.Count > 0)
        {
            hfTotalAllowance.Value = dt.Compute("SUM(Amount)", string.Empty).ToString();
            lblTotalAllowance.Text = hfTotalAllowance.Value;
            GVAllowance.DataSource = dt;
            GVAllowance.DataBind();
            lblCountAllowance.Text = Convert.ToString(dt.Rows.Count);
            btnAllowanceDelete.Visible = true;
            pnlAllowanceNull.Visible = false;
            pnlAllowanceEdit.Visible = true;
            pnlAllowanceAdd.Visible = false;
        }
        else
        {
            btnAllowanceDelete.Visible = false;
            pnlAllowanceNull.Visible = true;
            pnlAllowanceEdit.Visible = false;
            pnlAllowanceAdd.Visible = false;
        }
        FGetEmployeeDeductionMap(XID);
    }

    private void FGetEmployeeDeductionMap(Guid XID)
    {
        Model_EmployeeDeductionMap_ MEDM = new Model_EmployeeDeductionMap_();
        MEDM.IDCheck = "GetByIDEmp";
        MEDM.EmployeeId = XID;
        MEDM.IsActive = true;
        DataTable dt = new DataTable();
        Repostry_EmployeeDeductionMap_ REDM = new Repostry_EmployeeDeductionMap_();
        dt = REDM.BErp_EmployeeDeductionMap_Manage(MEDM);
        if (dt.Rows.Count > 0)
        {
            hfTotalDeduction.Value = dt.Compute("SUM(Amount)", string.Empty).ToString();
            lblTotalDeduction.Text = hfTotalDeduction.Value;
            GVDeduction.DataSource = dt;
            GVDeduction.DataBind();
            lblCountDeduction.Text = Convert.ToString(dt.Rows.Count);
            btnDeductionDelete.Visible = true;
            pnlDeductionNull.Visible = false;
            pnlDeductionEdit.Visible = true;
            pnlDeductionAdd.Visible = false;
        }
        else
        {
            btnDeductionDelete.Visible = false;
            pnlDeductionNull.Visible = true;
            pnlDeductionEdit.Visible = false;
            pnlDeductionAdd.Visible = false;
        }
        hfTotalSalary.Value = Convert.ToString(Convert.ToDecimal(hfTotalAllowance.Value) - Convert.ToDecimal(hfTotalDeduction.Value));
        lblTotalSalary.Text = hfTotalSalary.Value + ClassSaddam.FGetMonySa();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            FEmployeeSalaryAdd();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FEmployeeSalaryAdd()
    {
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        bool XCheckAmount = false;
        if (rbtnMonthSalary.Checked && rbtnHourSalary.Checked == false)
            XCheckAmount = true;
        else if (rbtnMonthSalary.Checked == false && rbtnHourSalary.Checked)
            XCheckAmount = false;

        if (Request.QueryString["ID"] == null)
        {
            decimal XBasic = Convert.ToDecimal(txtBasic.Text.Trim());
            Model_EmployeeSalary_ MES = new Model_EmployeeSalary_()
            {
                IDCheck = "Add",
                EmployeeSalaryID = Guid.NewGuid(),
                EmployeeId = new Guid(ddlEmployee.SelectedValue),
                Basic = XBasic,
                TotalEarning = Convert.ToDecimal(hfTotalAllowance.Value.Trim()),
                TotalDeduction = Convert.ToDecimal(hfTotalDeduction.Value.Trim()),
                TotalSalary = Convert.ToDecimal(hfTotalSalary.Value.Trim()) + XBasic,
                IsMonthlySalary = XCheckAmount,
                CreatedDate = XDate,
                CreatedBy = Test_Saddam.FGetIDUsiq(),
                ModifiedBy = 0,
                ModifiedDate = XDate,
                IsActive = true
            };

            Repostry_EmployeeSalary_ RES = new Repostry_EmployeeSalary_();
            string Xresult = RES.FErp_EmployeeSalary_Add(MES);
            if (Xresult == "IsExistsAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                return;
            }
            else if (Xresult == "IsSuccessAdd")
            {
                foreach (RepeaterItem item in rptAllowance.Items)
                {
                    TextBox txtAllowance = (TextBox)item.FindControl("txtAllowance");
                    if (txtAllowance != null)
                    {
                        if (!String.IsNullOrEmpty(txtAllowance.Text.Trim()))
                        {
                            HiddenField hfAllowanceId = (HiddenField)item.FindControl("hfAllowanceId");
                            if (hfAllowanceId != null)
                            {
                                FAllowanceAdd(new Guid(ddlEmployee.SelectedValue), new Guid(hfAllowanceId.Value), Convert.ToDecimal(txtAllowance.Text.Trim()));
                            }
                        }
                    }
                }

                foreach (RepeaterItem item in rptDeduction.Items)
                {
                    TextBox txtDeduction = (TextBox)item.FindControl("txtDeduction");
                    if (txtDeduction != null)
                    {
                        if (!String.IsNullOrEmpty(txtDeduction.Text.Trim()))
                        {
                            HiddenField hfDeductionId = (HiddenField)item.FindControl("hfDeductionId");
                            if (hfDeductionId != null)
                            {
                                FDeductionAdd(new Guid(ddlEmployee.SelectedValue), new Guid(hfDeductionId.Value), Convert.ToDecimal(txtDeduction.Text.Trim()));
                            }
                        }
                    }
                }

                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "إضافة", "إضافة الراتب لـ " + ddlEmployee.SelectedItem.Text, XDate);

                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
            }
        }
        else if (Request.QueryString["ID"] != null)
        {
            decimal XBasic = Convert.ToDecimal(txtBasic.Text.Trim());
            Model_EmployeeSalary_ MES = new Model_EmployeeSalary_()
            {
                IDCheck = "Edit",
                EmployeeSalaryID = new Guid(Request.QueryString["ID"]),
                EmployeeId = Guid.NewGuid(),
                Basic = XBasic,
                TotalEarning = Convert.ToDecimal(hfTotalAllowance.Value.Trim()),
                TotalDeduction = Convert.ToDecimal(hfTotalDeduction.Value.Trim()),
                TotalSalary = Convert.ToDecimal(hfTotalSalary.Value.Trim()) + XBasic,
                IsMonthlySalary = XCheckAmount,
                CreatedDate = XDate,
                CreatedBy = 0,
                ModifiedBy = Test_Saddam.FGetIDUsiq(),
                ModifiedDate = XDate,
                IsActive = true
            };

            Repostry_EmployeeSalary_ RES = new Repostry_EmployeeSalary_();
            string Xresult = RES.FErp_EmployeeSalary_Add(MES);
            if (Xresult == "IsExistsEdit")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                return;
            }
            else if (Xresult == "IsSuccessEdit")
            {
                FGetData();
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "تعديل", "تعديل الراتب لـ " + ddlEmployee.SelectedItem.Text, XDate);
            }
        }
    }

    private void FAllowanceAdd(Guid XIDEmp, Guid XIDAllowance, decimal XAllowance)
    {
        Model_EmployeeAllowanceMap_ MEAM = new Model_EmployeeAllowanceMap_()
        {
            IDCheck = "Add",
            EmployeeAllowanceMapID = Guid.NewGuid(),
            EmployeeId = XIDEmp,
            AllowanceId = XIDAllowance,
            Amount = XAllowance,
            CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            ModifiedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsActive = true,
        };
        Repostry_EmployeeAllowanceMap_ REAM = new Repostry_EmployeeAllowanceMap_();
        string Xresult = REAM.FErp_EmployeeAllowanceMap_Add(MEAM);
        if (Request.QueryString["ID"] != null)
        {
            if (Xresult == "IsExistsAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                return;
            }
            else if (Xresult == "IsSuccessAdd")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
            }
        }   
    }

    private void FDeductionAdd(Guid XIDEmp, Guid XIDDeduction, decimal XDeduction)
    {
        Model_EmployeeDeductionMap_ MEDM = new Model_EmployeeDeductionMap_()
        {
            IDCheck = "Add",
            EmployeeDeductionMapID = Guid.NewGuid(),
            EmployeeId = XIDEmp,
            DeductionId = XIDDeduction,
            Amount = XDeduction,
            CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            ModifiedBy = Test_Saddam.FGetIDUsiq(),
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            IsActive = true,
        };
        Repostry_EmployeeDeductionMap_ REDM = new Repostry_EmployeeDeductionMap_();
        string Xresult = REDM.FErp_EmployeeDeductionMap_Add(MEDM);
        if (Request.QueryString["ID"] != null)
        {
            if (Xresult == "IsExistsAdd")
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                return;
            }
            else if (Xresult == "IsSuccessAdd")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
            }
        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeSalary.aspx");
    }

    protected void rbtnMonthSalary_CheckedChanged(object sender, EventArgs e)
    {
        FCheck();
    }

    private void FCheck()
    {
        if (rbtnHourSalary.Checked == true)
        {
            divAllowance.Visible = false;
            lblBasic.InnerText = "الراتب الأساسي لكل ساعة";
        }
        else
        {
            divAllowance.Visible = true;
            lblBasic.InnerText = "الأساسي (36%)";
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlEmployee.Items.Clear(); ddlEmployee.Items.Add(""); ddlEmployee.AppendDataBoundItems = true;
            Repostry_Employee_.FErp_Employee_Master_Manage_By_Depam(new Guid(ddlDepartment.SelectedValue), ddlEmployee);
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnAllowanceEdit_Click(object sender, EventArgs e)
    {
        try
        {
            FAllowanceAdd(new Guid(HFIDEmployee.Value), new Guid(ddlAllowance.SelectedValue), Convert.ToDecimal(txtAllowance.Text.Trim()));
            ddlAllowance.SelectedValue = null;
            txtAllowance.Text = string.Empty;
            FGetEmployeeAllowanceMap(new Guid(HFIDEmployee.Value));
            lblTotalDeduction.Focus();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnAllowanceDelete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVAllowance.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVAllowance.DataKeys[row.RowIndex].Value);
                    Model_EmployeeAllowanceMap_ MEAM = new Model_EmployeeAllowanceMap_()
                    {
                        IDCheck = "Delete",
                        EmployeeAllowanceMapID = new Guid(Comp_ID),
                        EmployeeId = Guid.NewGuid(),
                        AllowanceId = Guid.NewGuid(),
                        Amount = 0,
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false,
                    };
                    Repostry_EmployeeAllowanceMap_ REAM = new Repostry_EmployeeAllowanceMap_();
                    string Xresult = REAM.FErp_EmployeeAllowanceMap_Add(MEAM);
                }
            }
            if (Request.QueryString["ID"] != null)
                FGetEmployeeAllowanceMap(new Guid(HFIDEmployee.Value));
            btnAdd.Focus();
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم حذف الملفات بنجاح ... ";
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnDeductionEdit_Click(object sender, EventArgs e)
    {
        try
        {
            FDeductionAdd(new Guid(HFIDEmployee.Value), new Guid(ddlDeduction.SelectedValue), Convert.ToDecimal(txtDeduction.Text.Trim()));
            ddlDeduction.SelectedValue = null;
            txtDeduction.Text = string.Empty;
            FGetEmployeeDeductionMap(new Guid(HFIDEmployee.Value));
            btnAdd.Focus();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnDeductionDelete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVDeduction.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVDeduction.DataKeys[row.RowIndex].Value);
                    Model_EmployeeDeductionMap_ MEDM = new Model_EmployeeDeductionMap_()
                    {
                        IDCheck = "Delete",
                        EmployeeDeductionMapID = new Guid(Comp_ID),
                        EmployeeId = Guid.NewGuid(),
                        DeductionId = Guid.NewGuid(),
                        Amount = 0,
                        CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                        IsActive = false,
                    };
                    Repostry_EmployeeDeductionMap_ REDM = new Repostry_EmployeeDeductionMap_();
                    string Xresult = REDM.FErp_EmployeeDeductionMap_Add(MEDM);
                }
            }
            if (Request.QueryString["ID"] != null)
                FGetEmployeeDeductionMap(new Guid(HFIDEmployee.Value));
            btnAdd.Focus();
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم حذف الملفات بنجاح ... ";
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