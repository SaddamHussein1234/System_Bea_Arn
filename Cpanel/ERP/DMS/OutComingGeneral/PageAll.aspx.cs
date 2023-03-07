using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.DMS;
using Library_CLS_Arn.DMS.Repostry;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_DMS_OutComingGeneral_PageAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IDFilter.Visible = true;
            if (Request.QueryString["Type"] == "Association")
                HFIDStore.Value = Class_Identity_.FGetIdentityAssociation();
            else if (Request.QueryString["Type"] == "Institute")
                HFIDStore.Value = Class_Identity_.FGetIdentityInstitute();
            else
                Response.Redirect("../");
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(CBYears);
            Repostry_DMS_Nature_.FGetDropList(0, "_Ar", Guid.Empty, CBNature);
            Repostry_DMS_Category_.FGetDropList(0, "_Ar", Guid.Empty, "Out_General", CBCategory);
            Repostry_DMS_Importance_.FGetDropList(0, "_Ar", Guid.Empty, CBImportance);
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            FSelectCheck();
        }
    }

    private void FSelectCheck()
    {
        foreach (ListItem lst in CBYears.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBCategory.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBNature.Items) { lst.Selected = true; }
        foreach (ListItem lst in CBImportance.Items) { lst.Selected = true; }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        GVOutComingGeneral.Columns[0].Visible = true;
        GVOutComingGeneral.Columns[8].Visible = true;
        GVOutComingGeneral.Columns[10].Visible = true;
        GVOutComingGeneral.Columns[11].Visible = true;

        GVOutComingGeneral.UseAccessibleHeader = false;
        FGetData();
    }

    private void FGetData()
    {
        try
        {
            string XYear = "Null", XCategory = "Null", XNature = "Null", XImportance = "Null";
            string XValueFilter0 = string.Empty, XValueFilter1 = string.Empty, XValueFilter2 = string.Empty, XValueFilter3 = string.Empty;
            if (Check_Years.Checked == false)
            {
                IDMessageSuccess.Visible = false;
                IDMessageWarning.Visible = true;
                lblMessageWarning.Text = "يجب تحديد إرشيف السنوات ... ";
                return;
            }
            if (Check_Years.Checked)
            {
                XYear = string.Empty;
                foreach (ListItem item in CBYears.Items)
                    XYear += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
                XValueFilter0 = "0";
            }
            if (Check_Category.Checked)
            {
                XCategory = string.Empty;
                foreach (ListItem item in CBCategory.Items)
                    XCategory += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
                XValueFilter1 = "1";
            }
            if (Check_Nature.Checked)
            {
                XNature = string.Empty;
                foreach (ListItem item in CBNature.Items)
                    XNature += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
                XValueFilter2 = "2";
            }
            if (Check_Importance.Checked)
            {
                XImportance = string.Empty;
                foreach (ListItem item in CBImportance.Items)
                    XImportance += item.Selected ? string.Format("{0},", item.Value) : string.Empty;
                XValueFilter3 = "3";
            }
            if (Check_Years.Checked == false && Check_Category.Checked == false && Check_Nature.Checked == false && Check_Importance.Checked == false)
                XValueFilter0 = "GetAllFilter";
            DataTable dt = new DataTable();
            dt = Repostry_DMS_OutComing_General_.FGetDataInDataTable(XValueFilter0 + XValueFilter1 + XValueFilter2 + XValueFilter3, 5000,
                Guid.Empty, new Guid(HFIDStore.Value), Guid.Empty, txtSearch.Text.Trim(),
                txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XCategory.Substring(0, XCategory.Length - 1),
                XNature.Substring(0, XNature.Length - 1), XImportance.Substring(0, XImportance.Length - 1),
                XYear.Substring(0, XYear.Length - 1), string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                txtTitle.Text = " قائمة خطابات الصادر العام , من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
                GVOutComingGeneral.DataSource = dt;
                GVOutComingGeneral.DataBind();
                //GVCustomers.AllowPaging = true;
                lblCount.Text = dt.Rows.Count.ToString();
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                IDFilter.Visible = false;
                btnDelete.Visible = true; btnPrint.Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                pnlSelect.Visible = false;
                IDFilter.Visible = false;
                btnDelete.Visible = false; btnPrint.Visible = false;
            }
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "يرجى تحديد قيمة واحدة في القوائم على الاقل ... ";
            return;
        }
    }

    protected void LBGetFilter_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        IDFilter.Visible = true;
    }

    protected void GVCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVOutComingGeneral.PageIndex = e.NewPageIndex;
        this.FGetData();
    }

    protected void LBEdit_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            foreach (GridViewRow row in GVOutComingGeneral.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVOutComingGeneral.DataKeys[row.RowIndex].Values[0].ToString());
                    Response.Redirect("PageAdd.aspx?ID=" + _XID.ToString());
                }
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void LBDelete_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            string Xresult = string.Empty;
            foreach (GridViewRow row in GVOutComingGeneral.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XID = new Guid(GVOutComingGeneral.DataKeys[row.RowIndex].Values[0].ToString());
                    Xresult = Repostry_DMS_OutComing_General_.FAdd("Delete", _XID, Guid.Empty, Guid.Empty, Guid.Empty, 0, string.Empty, string.Empty, Guid.Empty,
                    Guid.Empty, Guid.Empty, Guid.Empty, string.Empty, string.Empty, string.Empty, false, 0, false, 0, false, 0, false, 0, false, 0, false, 0, 0, 0, 0,
                       Test_Saddam.FGetIDUsiq(), ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"), false);
                }
            }
            if (Xresult == "IsSuccess")
            {
                IDMessageWarning.Visible = false;
                IDMessageSuccess.Visible = true;
                lblMessage.Text = "تم تحديث البيانات بنجاح ... ";
                System.Threading.Thread.Sleep(100);
                FGetData();
            }
        }
        catch
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        IDMessageWarning.Visible = false;
        IDMessageSuccess.Visible = false;
        try
        {
            GVOutComingGeneral.Columns[0].Visible = false;
            GVOutComingGeneral.Columns[8].Visible = false;
            GVOutComingGeneral.Columns[10].Visible = false;
            GVOutComingGeneral.Columns[11].Visible = false;
            GVOutComingGeneral.AllowPaging = false;
            GVOutComingGeneral.UseAccessibleHeader = true;
            GVOutComingGeneral.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["foot"] = pnlDataPrint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/cpanel/Print/PagePrintA4Landscape.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
        }
        catch (Exception)
        {
            IDMessageSuccess.Visible = false;
            IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void LBView_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            foreach (GridViewRow row in GVOutComingGeneral.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid _XIDYear = new Guid(GVOutComingGeneral.DataKeys[row.RowIndex].Values[1].ToString());
                    string _XID = GVOutComingGeneral.DataKeys[row.RowIndex].Values[2].ToString();
                    Response.Redirect("PageView.aspx?ID=" + _XID.ToString() + "&IDYears=" + _XIDYear.ToString());
                }
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void LBViewAdmin_Click(object sender, EventArgs e)
    {
        IDCreatedByStyle.InnerHtml = FGetCreatedBy(Convert.ToInt32(Convert.ToString((((LinkButton)sender).CommandArgument))),
            Convert.ToInt32(Convert.ToString((((LinkButton)sender).CommandName))), Convert.ToString((((LinkButton)sender).ToolTip)));
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowCreatedBy();", true);
    }

    public static string FGetCreatedBy(int XID, int XIDSee, string XDateSee)
    {
        string XResult = string.Empty;
        XResult = "<div id='IDCreatedBy' class='modal fade in modal_New_Style'><div class='modal-dialog' style='max-width:400px;'><div class='modal-content'>";
        XResult += "<div class='modal-header no-border'><button type='button' class='close' data-dismiss='modal'>×</button></div>";
        XResult += "<div class='modal-body' id='modal_ajax_content'><div class='page-container'><div class='page-content'>";
        XResult += "<div class='panel-body' align='right'><label><i class='fa fa-star'></i> قام بالإضافة المستخدم : </label>";
        XResult += "<div align='Center'><br /><b>";
        XResult += ClassQuaem.FAlBaheth(XID);
        XResult += "</b></div></div>";
        XResult += "<div class='panel-body' align='right'><label><i class='fa fa-star'></i> آخر من قام بالإطلاع : </label>";
        XResult += "<div align='Center'><br /><b>";
        if (XIDSee == 0)
            XResult += "لم يُفتح الخطاب بعد";
        else
            XResult += ClassQuaem.FAlBaheth(XIDSee);
        XResult += "</b></div></div>";
        XResult += "<div class='panel-body' align='right'><label><i class='fa fa-star'></i> تاريخ الإطلاع : </label>";
        XResult += "<div align='Center'><br /><b>";
        if (XIDSee == 0)
            XResult += "لم يُفتح الخطاب بعد";
        else
            XResult += XDateSee;
        XResult += "</b></div></div>";
        XResult += "<div class='modal-footer'><button type='button' class='btn btn-default -mb-3' data-dismiss='modal'>اغلاق</button></div>";
        XResult += "</div></div></div></div></div></div>";
        return XResult;
    }

    protected void LBViewAttachments_Click(object sender, EventArgs e)
    {
        lblTitle.Text = Convert.ToString((((LinkButton)sender).CommandName));
        FGetByIDOutComing_General(new Guid(Convert.ToString((((LinkButton)sender).CommandArgument))));
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "ShowAttachments();", true);
    }

    private void FGetByIDOutComing_General(Guid XID)
    {
        DataTable dt = new DataTable();
        dt = Repostry_DMS_OutComing_General_Attachments_.FGetDataInDataTable("GetByIDOutComing_General", 15, XID, new Guid(HFIDStore.Value), string.Empty, true);
        if (dt.Rows.Count > 0)
        {
            RPTFiles.DataSource = dt;
            RPTFiles.DataBind();
            pnlDataAttachments.Visible = true;
            pnlNullAttachments.Visible = false;
        }
        else
        {
            pnlDataAttachments.Visible = false;
            pnlNullAttachments.Visible = true;
        }
    }

    public string FCheck(string XCheck)
    {
        string XResult = "display:none;";
        if (XCheck == "Years")
        {
            if (Check_Years.Checked)
                XResult = "display:block;";
            else if (Check_Years.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "Category")
        {
            if (Check_Category.Checked)
                XResult = "display:block;";
            else if (Check_Nature.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "Nature")
        {
            if (Check_Nature.Checked)
                XResult = "display:block;";
            else if (Check_Nature.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        else if (XCheck == "Importance")
        {
            if (Check_Importance.Checked)
                XResult = "display:block;";
            else if (Check_Importance.Checked == false)
                XResult = "display:none;";
            else
                XResult = "display:none;";
        }
        return XResult;
    }

}