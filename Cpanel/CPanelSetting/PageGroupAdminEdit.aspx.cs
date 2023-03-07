using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelSetting_PageGroupAdminEdit : System.Web.UI.Page
{
    string IDUser, IDUniq;
    private void GetCookie()
    {
        try
        {
            HttpCookie Cooke;  // رقم المستخدم
            Cooke = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
            IDUser = ClassSaddam.UnprotectPassword(Cooke["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
        }
        catch
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        ClassAdmin_Arn CAA = new ClassAdmin_Arn();
        CAA._IDUniq = IDUniq;
        CAA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CAA.BArnAdminGetByIDUniq();
        if (dtViewProfil.Rows.Count > 0)
        {
            bool A6;
            A6 = Convert.ToBoolean(dtViewProfil.Rows[0]["A6"]);
            if (A6 == false)
            {
                Response.Redirect("LogOut.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            txtTitleGroup.Focus();
            FGetData();
        }
    }

    private void FGetData()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) * From tbl_Group_Admin With(NoLock) Where [ID_Uniq_Group] = @0", Convert.ToString(Request.QueryString["ID"]));
        if (dt.Rows.Count > 0)
        {
            txtTitleGroup.Text = dt.Rows[0]["Name_Group"].ToString();
            Session["Old_Name_"] = txtTitleGroup.Text.Trim();
            CBActive.Checked = Convert.ToBoolean(dt.Rows[0]["Is_Active_Group"]);

            // View 
            CBStatusDetailsView.Checked = Convert.ToBoolean(dt.Rows[0]["A1"]);
            CBAcceptanceDecisionView.Checked = Convert.ToBoolean(dt.Rows[0]["A2"]);
            CBFormDataView.Checked = Convert.ToBoolean(dt.Rows[0]["A3"]);
            CBAfieldVisitApprovalView.Checked = Convert.ToBoolean(dt.Rows[0]["A4"]);
            CBVisitReportView.Checked = Convert.ToBoolean(dt.Rows[0]["A5"]);
            CBSupportView.Checked = Convert.ToBoolean(dt.Rows[0]["A6"]);
            CBSupportHomeView.Checked = Convert.ToBoolean(dt.Rows[0]["A7"]);
            CBSupportMonyView.Checked = Convert.ToBoolean(dt.Rows[0]["A8"]);

            //Add
            CBFormDataAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A26"]);
            CBFormDataBoysAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A27"]);
            CBAfieldVisitApprovalAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A28"]);
            CBVisitReportAdd.Checked = Convert.ToBoolean(dt.Rows[0]["A29"]);
        }
        else
        {
            Response.Redirect("PageGroupAdmin.aspx");
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTitleGroup.Text != string.Empty)
            {
                lblTitleGroup.Visible = false;
                FChackName();
                System.Threading.Thread.Sleep(500);
            }
            else
            {
                lblTitleGroup.Visible = true;
                lblTitleGroup.Text = "* عنوان المجموعة";
            }

        }
        catch
        {
            lblMessageWarning.Text = "خطأ غير متوقع حاول لاحقاً";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    private void FChackName()
    {
        if (txtTitleGroup.Text.Trim() != Session["Old_Name_"].ToString())
        {
            DataTable dtUser = new DataTable();
            dtUser = ClassDataAccess.GetData("Select Top(1) [Name_Group],[Is_Delete_Group] from tbl_Group_Admin With(NoLock) Where [Name_Group] =@0 And [Is_Delete_Group] = @1", txtTitleGroup.Text.Trim(), Convert.ToString(false));
            if (dtUser.Rows.Count > 0)
            {
                lblMessageWarning.Text = "تم إضافة المجموعة سابقاً ";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
            }
            else
            {
                Session["Old_Name_"] = txtTitleGroup.Text.Trim();
                FGroupEdit();
            }
        }
        else
        {
            FGroupEdit();
        }
    }

    private void FGroupEdit()
    {
        GetCookie();
        ClassGroup CG = new ClassGroup();
        CG.IDUniqGroup = Convert.ToString(Request.QueryString["ID"]);
        CG.NameGroup = txtTitleGroup.Text.Trim();
        CG.IsActiveGroup = Convert.ToBoolean(CBActive.Checked);

        // View
        CG.A[0] = Convert.ToBoolean(CBStatusDetailsView.Checked);
        CG.A[1] = Convert.ToBoolean(CBAcceptanceDecisionView.Checked);
        CG.A[2] = Convert.ToBoolean(CBFormDataView.Checked);
        CG.A[3] = Convert.ToBoolean(CBAfieldVisitApprovalView.Checked);
        CG.A[4] = Convert.ToBoolean(CBVisitReportView.Checked);
        CG.A[5] = Convert.ToBoolean(CBSupportView.Checked);
        CG.A[6] = Convert.ToBoolean(CBSupportHomeView.Checked);
        CG.A[7] = Convert.ToBoolean(CBSupportMonyView.Checked);
        CG.A[8] = false;
        CG.A[9] = false;
        CG.A[10] = false;
        CG.A[11] = false;
        CG.A[12] = false;
        CG.A[13] = false;
        CG.A[14] = false;
        CG.A[15] = false;
        CG.A[16] = false;
        CG.A[17] = false;
        CG.A[18] = false;
        CG.A[19] = false;
        CG.A[20] = false;
        CG.A[21] = false;
        CG.A[22] = false;
        CG.A[23] = false;
        CG.A[24] = false;

        // Add
        CG.A[25] = Convert.ToBoolean(CBFormDataAdd.Checked);
        CG.A[26] = Convert.ToBoolean(CBFormDataBoysAdd.Checked);
        CG.A[27] = Convert.ToBoolean(CBAfieldVisitApprovalAdd.Checked);
        CG.A[28] = Convert.ToBoolean(CBVisitReportAdd.Checked);
        CG.A[29] = false;
        CG.A[30] = false;
        CG.A[31] = false;
        CG.A[32] = false;
        CG.A[33] = false;
        CG.A[34] = false;
        CG.A[35] = false;
        CG.A[36] = false;
        CG.A[37] = false;
        CG.A[38] = false;
        CG.A[39] = false;
        CG.A[40] = false;
        CG.A[41] = false;
        CG.A[42] = false;
        CG.A[43] = false;
        CG.A[44] = false;
        CG.A[45] = false;
        CG.A[46] = false;
        CG.A[47] = false;
        CG.A[48] = false;
        CG.A[49] = false;
        CG.BGroup_Admin_Edit();
        lblMessage.Text = "تم تعديل البيانات بنجاح";
        IDMessageSuccess.Visible = true;
        IDMessageWarning.Visible = false;
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageGroupAdmin.aspx");
    }

}