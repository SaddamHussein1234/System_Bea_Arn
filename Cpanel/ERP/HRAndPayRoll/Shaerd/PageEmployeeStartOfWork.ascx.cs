using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_PageEmployeeStartOfWork : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            ClassAdmin_Arn.FGetModerEmp(DLModerEmp);
            ImgModer.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
            ImgModerEmp.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLModerEmp.SelectedValue));

            lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
            lblModerEmp.Text = DLModerEmp.SelectedItem.ToString();
        }
    }

    public void FGetData(string XID)
    {
        try
        {
            Model_Employee_ ME = new Model_Employee_();
            ME.IDCheck = "GetByIDUniq";
            ME.EmployeeID = new Guid(XID);
            ME.FinancialYear_Id = Guid.Empty;
            ME.FirstName = string.Empty;
            ME.Date_From = string.Empty;
            ME.Date_To = string.Empty;
            ME.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Employee_ RE = new Repostry_Employee_();
            dt = RE.BErp_Employee_Master_Manage(ME);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "إشعار مباشرة العمل";
                lblNameEmp.Text = dt.Rows[0]["_Name"].ToString();
                lblNameEmp2.Text = lblNameEmp.Text;
                lblManagment.Text = dt.Rows[0]["Department"].ToString();
                lblEmpNo.Text = dt.Rows[0]["EmployeeNo"].ToString();
                Img_Emp.ImageUrl = "/" + dt.Rows[0]["Img_Signature_"].ToString();
                lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
                lblPhone.Text = dt.Rows[0]["MobileNo"].ToString();
                lblDateCreate.Text = Convert.ToDateTime(dt.Rows[0]["JoinDate"]).ToString("ddd") + " / " + Convert.ToDateTime(dt.Rows[0]["JoinDate"]).ToString("dd/MM/yyyy");
                lblDateCreate2.Text = lblDateCreate.Text;
                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض", "عرض إشعار مباشرة العمل لـ " + lblNameEmp.Text, ClassSaddam.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss"));

                string code = ClassSetting.FGetNameServer() +
                        "/Cpanel/ERP/HRAndPayRoll/Masters/PageEmployeeStartOfWork.aspx?ID=" + Request.QueryString["ID"];
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
            pnlPrint.Visible = false;
            pnlSelect.Visible = true;
            return;
        }
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageEmployee.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            DLModerAlGmeiah.Visible = false;
            lblModerAlGmeiah.Visible = true;

            DLModerEmp.Visible = false;
            lblModerEmp.Visible = true;

            if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "طباعة إشعار مباشرة العمل" + "\n" + "للموظف :" + lblNameEmp.Text, "BerArn", "Print", Test_Saddam.FGetIDUsiq());

            Session["foot"] = pnl2;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void DLModerAlGmeiah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgModer.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
    }

    protected void DLModerEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgModerEmp.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLModerEmp.SelectedValue));
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

}