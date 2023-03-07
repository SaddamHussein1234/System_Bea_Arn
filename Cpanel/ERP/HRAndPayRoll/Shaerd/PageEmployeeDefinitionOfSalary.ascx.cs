using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.ERP.Models.HRAndPayRoll.Masters;
using Library_CLS_Arn.ERP.Permissions;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_HRAndPayRoll_Shaerd_PageEmployeeDefinitionOfSalary : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiah);
            ImgModer.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));

            lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
        }
    }

    public void FGetData(string XCheck, string XID)
    {
        try
        {
            Model_EmployeeSalary_ MES = new Model_EmployeeSalary_();
            MES.IDCheck = XCheck;
            MES.EmployeeSalaryID = new Guid(XID);
            MES.CreatedDate = string.Empty;
            MES.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_EmployeeSalary_ RES = new Repostry_EmployeeSalary_();
            dt = RES.BErp_EmployeeSalary_Manage(MES);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = "إشعار تعريف بالراتب";
                lblNameEmp.Text = dt.Rows[0]["_Name"].ToString();
                lblNameEmp2.Text = lblNameEmp.Text;
                lblManagment.Text = dt.Rows[0]["Department"].ToString();
                lblEmpNo.Text = dt.Rows[0]["EmployeeNo"].ToString();
                Img_Emp.ImageUrl = "/" + dt.Rows[0]["Img_Signature_"].ToString();
                lblDesignation.Text = dt.Rows[0]["Designation"].ToString();
                lblPhone.Text = dt.Rows[0]["MobileNo"].ToString();
                lblDateCreate.Text = Convert.ToDateTime(dt.Rows[0]["JoinDate"]).ToString("ddd") + " / " + Convert.ToDateTime(dt.Rows[0]["JoinDate"]).ToString("dd/MM/yyyy");
                lblDateCreate2.Text = lblDateCreate.Text;
                if (Convert.ToBoolean(dt.Rows[0]["IsLeave"]) == false)
                    lblCheck.Text = "ولا يزال على رأس العمل حتى تأريخة ,";
                else if (Convert.ToBoolean(dt.Rows[0]["IsLeave"]))
                    lblCheck.Text = "وقد قدم إستقالتة في تاريخ " + Convert.ToDateTime(dt.Rows[0]["LeaveDate"]).ToString("ddd") + " / " + Convert.ToDateTime(dt.Rows[0]["LeaveDate"]).ToString("dd/MM/yyyy");
                lblMony.Text = dt.Rows[0]["Basic"].ToString() + " <smal>" + ClassSaddam.FGetMonySa() + "</smal>";

                Repostry_Tricker_.FAPP_Add("Add", Guid.NewGuid(), Test_Saddam.FGetIDUsiq(), "HRM", "نظام الموارد البشرية", "عرض", "عرض إشعار تعريف بالراتب لـ " + lblNameEmp.Text, ClassSaddam.GetCurrentTime().ToString("yyyy/MM/dd HH:mm:ss"));

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
                ToWord toWord = new ToWord(Convert.ToDecimal(dt.Rows[0]["Basic"]), currencies[Convert.ToInt32(0)]);
                lblMonyWord.Text = toWord.ConvertToArabic();

                string code = ClassSetting.FGetNameServer() +
                        "/Cpanel/ERP/HRAndPayRoll/Masters/PageEmployeeDefinitionOfSalary.aspx?ID=" + Request.QueryString["ID"];
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
        Response.Redirect("PageEmployeeSalary.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            DLModerAlGmeiah.Visible = false;
            lblModerAlGmeiah.Visible = true;

            if (Attach_Repostry_SMS_Send_.AllSendSystemHR())
                Attach_Repostry_SMS_Send_.FAddSMSMessage(ClassAdmin_Arn.FGetPhoneByIDRaees(), "طباعة إشعار تعريف بالراتب" + "\n" + "للموظف :" + lblNameEmp.Text, "BerArn", "Print", Test_Saddam.FGetIDUsiq());

            Session["foot"] = pnl2;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PagePrint.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void DLModerAlGmeiah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgModer.ImageUrl = ClassSaddam.FGetSignature_ERP(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
    }

}