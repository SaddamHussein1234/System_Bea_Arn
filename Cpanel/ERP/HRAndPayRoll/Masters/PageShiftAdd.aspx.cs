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
using Library_CLS_Arn.Saddam;

public partial class Cpanel_ERP_HRAndPayRoll_Masters_PageShiftAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CLS_Permissions.CheckAccountAdmin("A150");
            txtShift.Focus();
            if (Request.QueryString["ID"] != null)
                FGetData();
        }
    }

    private void FGetData()
    {
        try
        {
            Model_Shift_ MS = new Model_Shift_();
            MS.IDCheck = "GetByIDUniq";
            MS.ShiftID = new Guid(Request.QueryString["ID"]);
            MS.Shift = string.Empty;
            MS.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Shift_ RS = new Repostry_Shift_();
            dt = RS.BErp_Shift_Manage(MS);
            if (dt.Rows.Count > 0)
            {
                txtShift.Text = dt.Rows[0]["Shift"].ToString();
                ddlCheckCountShift.SelectedValue = dt.Rows[0]["Count_Shift_"].ToString();
                if (ddlCheckCountShift.SelectedValue == "1")
                    PnlTow.Visible = false;
                else
                    PnlTow.Visible = true;
                string XFromTime = dt.Rows[0]["FromTime"].ToString(); string XToTime = dt.Rows[0]["ToTime"].ToString();
                ddlFromHour.SelectedValue = XFromTime.Substring(0, 2);
                ddlFromMinute.SelectedValue = XFromTime.Substring(3, 2);
                ddlFromMeridiem.SelectedValue = XFromTime.Substring(6, 2);
                ddlToHour.SelectedValue = XToTime.Substring(0, 2);
                ddlToMinute.SelectedValue = XToTime.Substring(3, 2);
                ddlToMeridiem.SelectedValue = XToTime.Substring(6, 2);

                string XFromTime_Tow = dt.Rows[0]["FromTime_Tow"].ToString(); string XToTime_Tow = dt.Rows[0]["ToTime_To"].ToString();
                ddlFromHour_Tow.SelectedValue = XFromTime_Tow.Substring(0, 2);
                ddlFromMinute_Tow.SelectedValue = XFromTime_Tow.Substring(3, 2);
                ddlFromMeridiem_Tow.SelectedValue = XFromTime_Tow.Substring(6, 2);
                ddlToHour_Tow.SelectedValue = XToTime_Tow.Substring(0, 2);
                ddlToMinute_Tow.SelectedValue = XToTime_Tow.Substring(3, 2);
                ddlToMeridiem_Tow.SelectedValue = XToTime_Tow.Substring(6, 2);
            }
                
            else
                Response.Redirect("PageShift.aspx");
        }
        catch (Exception)
        {
            Response.Redirect("PageShift.aspx");
            return;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] == null)
            {
                Model_Shift_ MD = new Model_Shift_()
                {
                    IDCheck = "Add",
                    ShiftID = Guid.NewGuid(),
                    Shift = txtShift.Text.Trim(),
                    CountShift = Convert.ToInt32(ddlCheckCountShift.SelectedValue),
                    FromTime = ddlFromHour.SelectedValue + ":" + ddlFromMinute.SelectedValue + " " + ddlFromMeridiem.SelectedValue,
                    ToTime = ddlToHour.SelectedValue + ":" + ddlToMinute.SelectedValue + " " + ddlToMeridiem.SelectedValue,
                    FromTime_Tow = ddlFromHour_Tow.SelectedValue + ":" + ddlFromMinute_Tow.SelectedValue + " " + ddlFromMeridiem_Tow.SelectedValue,
                    ToTime_Tow = ddlToHour_Tow.SelectedValue + ":" + ddlToMinute_Tow.SelectedValue + " " + ddlToMeridiem_Tow.SelectedValue,
                    CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    CreatedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedBy = 0,
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_Shift_ RD = new Repostry_Shift_();
                string Xresult = RD.FErp_Shift_Add(MD);
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
            else if (Request.QueryString["id"] != null)
            {
                Model_Shift_ MD = new Model_Shift_()
                {
                    IDCheck = "Edit",
                    ShiftID = new Guid(Request.QueryString["id"]),
                    Shift = txtShift.Text.Trim(),
                    CountShift = Convert.ToInt32(ddlCheckCountShift.SelectedValue),
                    FromTime = ddlFromHour.SelectedValue + ":" + ddlFromMinute.SelectedValue + " " + ddlFromMeridiem.SelectedValue,
                    ToTime = ddlToHour.SelectedValue + ":" + ddlToMinute.SelectedValue + " " + ddlToMeridiem.SelectedValue,
                    FromTime_Tow = ddlFromHour_Tow.SelectedValue + ":" + ddlFromMinute_Tow.SelectedValue + " " + ddlFromMeridiem_Tow.SelectedValue,
                    ToTime_Tow = ddlToHour_Tow.SelectedValue + ":" + ddlToMinute_Tow.SelectedValue + " " + ddlToMeridiem_Tow.SelectedValue,
                    CreatedDate = string.Empty,
                    CreatedBy = 0,
                    ModifiedBy = Test_Saddam.FGetIDUsiq(),
                    ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    IsActive = true
                };

                Repostry_Shift_ RD = new Repostry_Shift_();
                string Xresult = RD.FErp_Shift_Add(MD);
                if (Xresult == "IsExistsEdit")
                {
                    IDMessageWarning.Visible = true;
                    IDMessageSuccess.Visible = false;
                    lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
                    return;
                }
                else if (Xresult == "IsSuccessEdit")
                {
                    IDMessageWarning.Visible = false;
                    IDMessageSuccess.Visible = true;
                    lblSuccess.Text = "تم تعديل البيانات بنجاح ... ";
                    FGetData();
                }
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

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageShift.aspx");
    }

    protected void ddlMaratialStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        if (ddlCheckCountShift.SelectedValue == "1")
        {
            ddlFromHour_Tow.SelectedValue = "00";
            ddlFromMinute_Tow.SelectedValue = "00";
            ddlFromMeridiem_Tow.SelectedValue = "PM";

            ddlToHour_Tow.SelectedValue = "00";
            ddlToMinute_Tow.SelectedValue = "00";
            ddlToMeridiem_Tow.SelectedValue = "PM";
            PnlTow.Visible = false;
        }
        else
        {
            ddlFromHour_Tow.SelectedValue = "05";
            ddlFromMinute_Tow.SelectedValue = "00";
            ddlFromMeridiem_Tow.SelectedValue = "PM";

            ddlToHour_Tow.SelectedValue = "08";
            ddlToMinute_Tow.SelectedValue = "00";
            ddlToMeridiem_Tow.SelectedValue = "PM";
            PnlTow.Visible = true;
        }
    }

}