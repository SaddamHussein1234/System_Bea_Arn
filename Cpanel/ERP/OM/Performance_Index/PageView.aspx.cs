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

public partial class Cpanel_ERP_OM_Performance_Index_PageView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlSelect.Visible = true;
            txtSearch.Text = Request.QueryString["ID"];
            txtSearch.Focus();
            FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Repostry_Performance_Index_.FGetDataInDataTable("GetByIDNumber", 1, Guid.Empty, txtSearch.Text.Trim()
                , string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                IDEdit.HRef = "PageAdd.aspx?ID=" + dt.Rows[0]["_ID_Item_"].ToString();
                //txtNumberBill.Text = dt.Rows[0]["_ID_Number_"].ToString();
                lblNameEmp.Text = ClassQuaem.FAlBaheth(int.Parse(dt.Rows[0]["_ID_Admin_"].ToString()));
                lblBSC.Text = dt.Rows[0]["_BSC_"].ToString();
                lblKRA.Text = dt.Rows[0]["_KRA_"].ToString();
                lblStrategic_Goal_Icon.Text = dt.Rows[0]["_Strategic_Goal_Icon_"].ToString();
                lblStrategic_Goal_text.Text = dt.Rows[0]["_Strategic_Goal_Text_"].ToString();
                lblManagement.Text = dt.Rows[0]["_Management_"].ToString();

                lblpointer_Icon.Text = dt.Rows[0]["_pointer_Icon_"].ToString();
                lblpointer.Text = dt.Rows[0]["_pointer_"].ToString();
                lblPointer_Owner.Text = dt.Rows[0]["_pointer_Owner_"].ToString();
                lblMeasruing_Unit.Text = dt.Rows[0]["_Measruing_Unit_"].ToString();
                lblBaseline.Text = dt.Rows[0]["_Baseline_"].ToString();
                lblPolar.Text = dt.Rows[0]["_Polar_"].ToString();
                lblMeasurement_Periodicity.Text = dt.Rows[0]["_Measurement_Periodicity_"].ToString();
                lblCumulative.Text = dt.Rows[0]["_Cumulative_"].ToString();
                lblReference_Value.Text = dt.Rows[0]["_Reference_Value_"].ToString();
                lblPurpose_of_the_Measurement.Text = dt.Rows[0]["_Purpose_of_the_Measurement_"].ToString();
                lblPointer_Formula_Equation_One.Text = dt.Rows[0]["_Pointer_Formula_Equation_One_"].ToString();
                lblPointer_Formula_Equation_Two.Text = dt.Rows[0]["_Pointer_Formula_Equation_Two_"].ToString();
                lblTarget.Text = dt.Rows[0]["_Target_"].ToString();
                lblData_Source.Text = dt.Rows[0]["_Data_Source_"].ToString();
                lblAttached_Evidence.Text = dt.Rows[0]["_Attached_Evidence_"].ToString();
                lblNote.Text = dt.Rows[0]["_Note_"].ToString();
                lblMeasurement_Officer.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDMeasurement_Officer_"].ToString()));
                if (Convert.ToBoolean(dt.Rows[0]["_IsMeasurement_Officer_"]))
                {
                    ImgMeasurement_Officer.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["_IDMeasurement_Officer_"]));
                    ImgMeasurement_Officer.Width = 100; ImgMeasurement_Officer.Visible = true; IDKhatm.Visible = true;
                    lblMeasurement_OfficerAllowDate.Text = "بتاريخ : " + Convert.ToDateTime(dt.Rows[0]["_Measurement_Officer_Date_Allow_"]).ToString("yyyy-MM-dd");
                }
                else
                { ImgMeasurement_Officer.ImageUrl = "/Cpanel/loaderMin.gif"; ImgMeasurement_Officer.Width = 30; 
                    ImgMeasurement_Officer.Visible = true; }

                lblImplementation_Officer.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDImplementation_Officer_"].ToString()));
                if (Convert.ToBoolean(dt.Rows[0]["_IsImplementation_Officer_"]))
                {
                    ImgImplementation_Officer.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["_IDImplementation_Officer_"]));
                    ImgImplementation_Officer.Width = 100; ImgImplementation_Officer.Visible = true; IDKhatm.Visible = true;
                    lblImplementation_OfficerAllowDate.Text = "بتاريخ : " + Convert.ToDateTime(dt.Rows[0]["_Implementation_Officer_Date_Allow_"]).ToString("yyyy-MM-dd");
                }
                else
                {
                    ImgImplementation_Officer.ImageUrl = "/Cpanel/loaderMin.gif"; ImgImplementation_Officer.Width = 30;
                    ImgImplementation_Officer.Visible = true;
                }

                lblGeneral_Director.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDGeneral_Director_"].ToString()));
                if (Convert.ToBoolean(dt.Rows[0]["_IsGeneral_Director_"]))
                {
                    ImgGeneral_Director.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["_IDGeneral_Director_"]));
                    ImgGeneral_Director.Width = 100; ImgGeneral_Director.Visible = true; IDKhatm.Visible = true;
                    lblGeneral_DirectorAllowDate.Text = "بتاريخ : " + Convert.ToDateTime(dt.Rows[0]["_General_Director_Date_Allow_"]).ToString("yyyy-MM-dd");
                }
                else
                {
                    ImgGeneral_Director.ImageUrl = "/Cpanel/loaderMin.gif"; ImgGeneral_Director.Width = 30;
                    ImgGeneral_Director.Visible = true;
                }
                if (Convert.ToBoolean(dt.Rows[0]["_IsMeasurement_Officer_"]) && Convert.ToBoolean(dt.Rows[0]["_IsImplementation_Officer_"]) && Convert.ToBoolean(dt.Rows[0]["_IsGeneral_Director_"]))
                { IDKhatm.Visible = true; IDKhatmLodding.Visible = false; }
                else
                { IDKhatm.Visible = false; IDKhatmLodding.Visible = true; }
                string code = ClassSetting.FGetNameServer() +
                "/Cpanel/ERP/OM/Performance_Index/PageView..aspx?ID=" + dt.Rows[0]["_ID_Number_"].ToString();
                Class_QRScan.FGetQRCode(code, ImgQRCode);
                Class_QRScan.FGetQRCode(code, ImgQRCode2);

                lblDateCreate.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy-MM-dd");
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
            Response.Redirect("PageAll.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
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

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

}