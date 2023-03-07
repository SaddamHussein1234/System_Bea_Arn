using Library_CLS_Arn.CRM.Models;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_CRM_PageRemamber_PageRemamber : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtStartDate.Text = new DateTime(ClassSaddam.GetCurrentTime().Year, ClassSaddam.GetCurrentTime().Month, 1).ToString("yyyy-MM-dd");
                txtEndDate.Text = new DateTime(ClassSaddam.GetCurrentTime().Year, ClassSaddam.GetCurrentTime().AddMonths(1).Month, 1).ToString("yyyy-MM-dd");
                pnlSelect.Visible = true;
                FCheckFilter();
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

    private void FCheckFilter()
    {
        if (RBAll.Checked && RBIsCaming.Checked == false && RBIsBriv.Checked == false)
            FCRM_Remamber_Manage("GetAll", " قائمة جميع رسائل التذكير من تاريخ " + txtStartDate.Text.Trim() + " - إلى تاريخ " + txtEndDate.Text.Trim());
        else if (RBAll.Checked == false && RBIsCaming.Checked && RBIsBriv.Checked == false)
            FCRM_Remamber_Manage("GetAllCome", " قائمة التنبيهات القادمة من تاريخ " + txtStartDate.Text.Trim() + " - إلى تاريخ " + txtEndDate.Text.Trim());
        else if (RBAll.Checked == false && RBIsCaming.Checked == false && RBIsBriv.Checked)
            FCRM_Remamber_Manage("GetAllGo", " قائمة التنبيهات الفائته من تاريخ " + txtStartDate.Text.Trim() + " - إلى تاريخ " + txtEndDate.Text.Trim());
    }

    private void FCRM_Remamber_Manage(string XIDCheck, string XMessage)
    {
        GVRemamberAll.Columns[0].Visible = true;
        GVRemamberAll.Columns[8].Visible = true;
        GVRemamberAll.UseAccessibleHeader = false;
        Model_Remamber_ MR = new Model_Remamber_();
        MR.IDCheck = XIDCheck;
        MR.ID_Item = Guid.Empty;
        MR.ID_Company = Guid.Empty;
        MR.Start_Date = txtStartDate.Text.Trim();
        MR.End_Date = txtEndDate.Text.Trim();
        MR.Remamber_Date = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd");
        MR.Is_Active = true;
        MR.Is_Delete = false;
        DataTable dt = new DataTable();
        Repostry_Remamber_ RR = new Repostry_Remamber_();
        dt = RR.BCRM_Remamber_Manage(MR);

        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = XMessage;
            GVRemamberAll.DataSource = dt;
            GVRemamberAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVRemamberAll.Columns[0].Visible = false;
            GVRemamberAll.Columns[8].Visible = false;
            GVRemamberAll.UseAccessibleHeader = true;
            GVRemamberAll.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlPrint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
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
        Response.Redirect("PageRemamber.aspx");
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        try
        {
            FCheckFilter();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            foreach (GridViewRow row in GVRemamberAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid Comp_ID = new Guid(GVRemamberAll.DataKeys[row.RowIndex].Value.ToString());
                    Model_Remamber_ MR = new Model_Remamber_()
                    {
                        IDCheck = "Delete",
                        ID_Item = Comp_ID,
                        ID_Company = Guid.Empty,
                        Remamber_Date = string.Empty,
                        Remamber_Desc = string.Empty,
                        Is_Active = false,
                        CreatedDate = string.Empty,
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        Is_Delete = true
                    };

                    Repostry_Remamber_ RR = new Repostry_Remamber_();
                    string Xresult = RR.FCRM_Remamber_Add(MR);
                    if (Xresult == "IsSuccessDelete")
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم حذف البيانات بنجاح ... ";
                    }
                }
            }
            FCheckFilter();
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