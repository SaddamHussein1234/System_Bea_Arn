using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_OM_Performance_Index_PageAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ClassAdmin_Arn.FGetAdminAllByItem("ByID", DLAdmin);
            txtDateAdd.Text = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
            ClassAdmin_Arn.FGetAdminAllByItem("ByID", DLMeasurement_Officer);
            ClassAdmin_Arn.FGetAdminAllByItem("ByID", DLImplementation_Officer);
            ClassAdmin_Arn.FGetModerAlGmeiah(DLGeneral_Director);
            FGetLastBill();

            if (Request.QueryString["ID"] != null)
                FGetData();
            System.Threading.Thread.Sleep(100);
        }
    }

    private void FGetData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Repostry_Performance_Index_.FGetDataInDataTable("GetByIDUniq", 1, new Guid(Request.QueryString["ID"]), string.Empty
                , string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                txtNumberBill.Text = dt.Rows[0]["_ID_Number_"].ToString();
                DLAdmin.SelectedValue = dt.Rows[0]["_ID_Admin_"].ToString();
                txtBSC.Text = dt.Rows[0]["_BSC_"].ToString();
                txtKRA.Text = dt.Rows[0]["_KRA_"].ToString();
                txtStrategic_Goal_Icon.Text = dt.Rows[0]["_Strategic_Goal_Icon_"].ToString();
                txtStrategic_Goal_text.Text = dt.Rows[0]["_Strategic_Goal_Text_"].ToString();
                txtManagement.Text = dt.Rows[0]["_Management_"].ToString();

                txtpointer_Icon.Text = dt.Rows[0]["_pointer_Icon_"].ToString();
                txtpointer.Text = dt.Rows[0]["_pointer_"].ToString();
                txtPointer_Owner.Text = dt.Rows[0]["_pointer_Owner_"].ToString();
                txtMeasruing_Unit.Text = dt.Rows[0]["_Measruing_Unit_"].ToString();
                txtBaseline.Text = dt.Rows[0]["_Baseline_"].ToString();
                txtPolar.Text = dt.Rows[0]["_Polar_"].ToString();
                txtMeasurement_Periodicity.Text = dt.Rows[0]["_Measurement_Periodicity_"].ToString();
                txtCumulative.Text = dt.Rows[0]["_Cumulative_"].ToString();
                txtReference_Value.Text = dt.Rows[0]["_Reference_Value_"].ToString();
                txtPurpose_of_the_Measurement.Text = dt.Rows[0]["_Purpose_of_the_Measurement_"].ToString();
                txtPointer_Formula_Equation_One.Text = dt.Rows[0]["_Pointer_Formula_Equation_One_"].ToString();
                txtPointer_Formula_Equation_Two.Text = dt.Rows[0]["_Pointer_Formula_Equation_Two_"].ToString();
                DLTarget.SelectedValue = dt.Rows[0]["_Target_"].ToString();
                txtData_Source.Text = dt.Rows[0]["_Data_Source_"].ToString();
                txtAttached_Evidence.Text = dt.Rows[0]["_Attached_Evidence_"].ToString();
                txtNote.Text = dt.Rows[0]["_Note_"].ToString();
                DLMeasurement_Officer.SelectedValue = dt.Rows[0]["_IDMeasurement_Officer_"].ToString();
                DLImplementation_Officer.SelectedValue = dt.Rows[0]["_IDImplementation_Officer_"].ToString();
                DLGeneral_Director.SelectedValue = dt.Rows[0]["_IDGeneral_Director_"].ToString();
                txtDateAdd.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy-MM-dd");
            }
            else
                Response.Redirect("PageAll.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageAll.aspx");
        }
    }

    private void FGetLastBill()
    {
        txtNumberBill.Text = Convert.ToString(Repostry_Performance_Index_.FGetCount("GetLast", 1, Guid.Empty, string.Empty, string.Empty, string.Empty, true, "IDOrder") + 1);
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAdd.aspx");
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

    protected void LBNew_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            if (txtNumberBill.Text.Trim() != string.Empty)
                FAdd();
            else
            {
                lblMessageWarning.Text = "يُرجى إدخال رقم بطاقة المؤشر ... ";
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                return;
            }
        }
        catch (Exception)
        {
            lblMessageWarning.Text = "حدث خطأ غير متوقع لم يتم إضافة الفاتورة";
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            return;
        }
    }

    private void FAdd()
    {
        string Xresult = string.Empty, XCheck = string.Empty;
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        Guid XID = Guid.Empty;
        int XIDAdd = 0, XUpdate = 0;
        if (Request.QueryString["ID"] == null)
        {
            XCheck = "Add"; XID = Guid.NewGuid(); XIDAdd = Test_Saddam.FGetIDUsiq();
        }
        if (Request.QueryString["ID"] != null)
        {
            XCheck = "Edit"; XID = new Guid(Request.QueryString["ID"]); XUpdate = Test_Saddam.FGetIDUsiq();
        }

        Xresult = Repostry_Performance_Index_.FAPP(XCheck, XID, Convert.ToInt64(txtNumberBill.Text.Trim()), Convert.ToInt32(DLAdmin.SelectedValue),
            string.Empty, txtBSC.Text.Trim(), txtKRA.Text.Trim(), txtStrategic_Goal_Icon.Text.Trim(), txtStrategic_Goal_text.Text.Trim(), 
            txtManagement.Text.Trim(), txtpointer_Icon.Text.Trim(), txtpointer.Text.Trim(), txtPointer_Owner.Text.Trim(),
            txtMeasruing_Unit.Text.Trim(), txtBaseline.Text.Trim(), txtPolar.Text.Trim(), txtMeasurement_Periodicity.Text.Trim(),
            txtCumulative.Text.Trim(), txtReference_Value.Text.Trim(), txtPurpose_of_the_Measurement.Text.Trim(),
            txtPointer_Formula_Equation_One.Text.Trim(), txtPointer_Formula_Equation_Two.Text.Trim(), DLTarget.SelectedValue,
            txtData_Source.Text.Trim(), txtAttached_Evidence.Text.Trim(), txtNote.Text.Trim(),Convert.ToInt32(DLMeasurement_Officer.SelectedValue),
            false,0, XDate, Convert.ToInt32(DLImplementation_Officer.SelectedValue),false,0,XDate, Convert.ToInt32(DLGeneral_Director.SelectedValue),
            false,0,XDate, XIDAdd, XUpdate, 0,
            Convert.ToDateTime(txtDateAdd.Text.Trim() + ClassSaddam.GetCurrentTime().ToString(" HH:mm:ss")).ToString("yyyy-MM-dd HH:mm:ss"), true);
        if (Xresult == "IsExistsNumber")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "رقم بطاقة المؤشر مستخدم بالفعل !!! ";
            txtNumberBill.Focus();
            return;
        }
        else if (Xresult == "IsExists")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "هذا الملف تم إضافتة مسبقاً ... ";
            txtNumberBill.Focus();
            return;
        }
        else if (Xresult == "IsSuccess")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم تحديث البيانات بنجاح ... ";
            FGetLastBill();
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

}