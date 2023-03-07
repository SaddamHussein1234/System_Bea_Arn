using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_JobAssignments_PageEmployeeJobAssignmentByView : System.Web.UI.UserControl
{
    public string XType = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            ddlYears.SelectedValue = Request.QueryString["IDYear"];
            txtSearch.Text = Request.QueryString["ID"];
            txtSearch.Focus();
            FEmployeeJobAssignment_Manage();
        }
    }

    private void FEmployeeJobAssignment_Manage()
    {
        try
        {
            string XIDCheck = string.Empty;
            Guid XEmployeeJobAssignmentID = Guid.Empty, XFinancialYear_Id = Guid.Empty;
            long XNumber_Job = 0;
            if (XType == "Manager")
            {
                XIDCheck = "GetByIDDetails";
                XEmployeeJobAssignmentID = Guid.Empty;
                XFinancialYear_Id = new Guid(ddlYears.SelectedValue);
                XNumber_Job = Convert.ToInt64(txtSearch.Text.Trim());
            }
            else if (XType == "Admin")
            {
                XIDCheck = "GetByIDUniq";
                XEmployeeJobAssignmentID = new Guid(Request.QueryString["ID"]);
                XFinancialYear_Id = Guid.Empty;
                XNumber_Job = 0; IDSearch.Visible = false;
            }
            DataTable dt = new DataTable();
            dt = Repostry_JobAssignment_.FGetDataInDataTable(XIDCheck, XEmployeeJobAssignmentID, XFinancialYear_Id, XNumber_Job, string.Empty,
                string.Empty, string.Empty, false, false, true);

            if (dt.Rows.Count > 0)
            {
                FGetEmp(new Guid(dt.Rows[0]["EmployeeJobAssignmentID"].ToString()));
                IDEdit.HRef = "/CPanel/ERP/HRAndPayRoll/Transactions/PageEmployeeJobAssignmentAdd.aspx?ID=" + dt.Rows[0]["EmployeeJobAssignmentID"].ToString();
                lblNumberAccountable.Text = dt.Rows[0]["Number_Job_"].ToString();
                txtTitle.Text = dt.Rows[0]["Assignment_Title_"].ToString();
                lblDate_Job.Text = "تاريخ البدء : " + dt.Rows[0]["Date_Job"].ToString() + " تاريخ الانتهاء : " + dt.Rows[0]["Date_End_Job"].ToString();
                lblDateCreate.Text = Convert.ToDateTime(dt.Rows[0]["Date_Job_"]).ToString("ddd") + " / " + Convert.ToDateTime(dt.Rows[0]["Date_Job_"]).ToString("dd/MM/yyyy");
                //lblNameEmp.Text = dt.Rows[0]["_Name"].ToString();
                //lblEmp.Text = lblNameEmp.Text;

                //if (Convert.ToBoolean(dt.Rows[0]["Is_Emp_Allow_"]) || Convert.ToBoolean(dt.Rows[0]["Is_Emp_Deny_"]))
                //{
                //    Img_Emp.ImageUrl = "/" + dt.Rows[0]["Img_Signature_"].ToString();
                //    Img_Emp.Width = 100; Img_Emp.Visible = true;
                //}
                //else
                //{ Img_Emp.ImageUrl = "/Cpanel/loaderMin.gif"; Img_Emp.Width = 30; Img_Emp.Visible = true; }

                //lblManagment.Text = dt.Rows[0]["Department"].ToString();
                //lblEmpNo.Text = dt.Rows[0]["EmployeeNo"].ToString();
                //lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
                //lblPhone.Text = dt.Rows[0]["MobileNo"].ToString();
                //lblMaratialStatus.Text = ClassSaddam.FChangeValue(dt.Rows[0]["MaratialStatus"].ToString());

                lblThe_Assignment.Text = dt.Rows[0]["The_Assignment_"].ToString();
                lblTime_Assignment.Text = dt.Rows[0]["Time_Assignment_"].ToString();
                lblThe_Qriah.Text = dt.Rows[0]["The_Qriah_"].ToString();

                lblModer.Text = ClassAdmin_Arn.FGetNameByID(Convert.ToInt32(dt.Rows[0]["ID_Moder_"].ToString()));
                lbl_Job.Text = ClassAdmin_Arn.FGetNameEmpByID(Convert.ToInt32(dt.Rows[0]["ID_Moder_"].ToString()));
                if (Convert.ToBoolean(dt.Rows[0]["Is_Moder_Allow_"]) || Convert.ToBoolean(dt.Rows[0]["Is_Moder_Not_Allow_"]))
                {
                    Img_Moder.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["ID_Moder_"]));
                    Img_Moder.Width = 100; Img_Moder.Visible = true; IDKhatm.Visible = true;
                }
                else
                { Img_Moder.ImageUrl = "/Cpanel/loaderMin.gif"; Img_Moder.Width = 30; Img_Moder.Visible = true; IDKhatm.Visible = false; }

                lbl_Raees.Text = ClassAdmin_Arn.FGetNameByID(Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"].ToString()));
                bool Allow_Final, Allow_With_Commant, Not_Allow;
                Allow_Final = Convert.ToBoolean(dt.Rows[0]["Is_Raees_Allow_Final_"]);
                Allow_With_Commant = Convert.ToBoolean(dt.Rows[0]["Is_Raees_Allow_With_Commant_"]);
                Not_Allow = Convert.ToBoolean(dt.Rows[0]["Is_Raees_Lagnat_Not_Allow_"]);
                if (Allow_Final && Allow_With_Commant == false && Not_Allow == false)
                {
                    pnlRaeesAllow.Visible = true;
                    pnlRaeesWithComment.Visible = false;
                    pnlRaeesNotAllow.Visible = false;
                }
                else if (Allow_Final == false && Allow_With_Commant && Not_Allow == false)
                {
                    pnlRaeesAllow.Visible = false;
                    lblRaees.Text = dt.Rows[0]["Comment_Raees_"].ToString();
                    pnlRaeesWithComment.Visible = true;
                    pnlRaeesNotAllow.Visible = false;
                }
                else if (Allow_Final == false && Allow_With_Commant == false && Not_Allow)
                {
                    pnlRaeesAllow.Visible = false;
                    pnlRaeesWithComment.Visible = false;
                    pnlRaeesNotAllow.Visible = true;
                }

                //if (Allow_Final || Allow_With_Commant || Not_Allow)
                //{
                //    Img_Raees.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["ID_Raees_Lagnat_"]));
                //    Img_Raees.Width = 100; IDKhatm.Visible = true;
                //}
                //else
                //{
                //    Img_Raees.ImageUrl = "/Cpanel/loaderMin.gif";
                //    Img_Raees.Width = 30; IDKhatm.Visible = false;
                //}

                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض",
                    "عرض إستمارة المهمة برقم " + lblNumberAccountable.Text.Trim(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"));
                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/ERP/HRAndPayRoll/Transactions/PageEmployeeJobAssignmentByView.aspx?IDYear=" + dt.Rows[0]["FinancialYear_Id_"].ToString() + "&ID=" + dt.Rows[0]["Number_Job_"].ToString();
                Class_QRScan.FGetQRCode(code, ImgQRCode);
                Class_QRScan.FGetQRCode(code, ImgQRCode2);

                pnlPrint.Visible = true;
                pnlSelect.Visible = false;
            }
            else
            {
                pnlPrint.Visible = false;
                pnlSelect.Visible = true;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetEmp(Guid XJobAssignmentID)
    {
        DataTable dt = new DataTable();
        dt = Repostry_JobAssignment_Map_.FGetDataInDataTable("GetByIDJobAssignment", 1000, Guid.Empty, Guid.Empty,
            Guid.Empty, XJobAssignmentID, string.Empty, string.Empty, string.Empty, false, true);
        if (dt.Rows.Count > 0)
        {
            RPTJobAssignment_Map.DataSource = dt;
            RPTJobAssignment_Map.DataBind();
        }
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployeeJobAssignments.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["foot"] = pnl2;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FEmployeeJobAssignment_Manage();
    }

}