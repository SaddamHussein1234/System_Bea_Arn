using Library_CLS_Arn.ClassEntity.Attach.Models;
using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_CPanelAttach_PageMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlSelect.Visible = true;
            txtDateFrom.Text = new DateTime(ClassDataAccess.GetCurrentTime().Year, ClassDataAccess.GetCurrentTime().Month, 1).ToString("yyyy-MM-dd");
            txtDateTo.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy-MM-dd");
        }
    }

    private void FAttach_SMS_Send_Manage(string XIDCheck,string XMessage)
    {
        GVMessage.UseAccessibleHeader = false;
        Attach_Model_SMS_Send_ AMSS = new Attach_Model_SMS_Send_();
        AMSS.IDCheck = XIDCheck;
        AMSS.ID_Item = Guid.Empty;
        AMSS.Start_Date = txtDateFrom.Text.Trim();
        AMSS.End_Date = txtDateTo.Text.Trim();
        AMSS.Type_Message = RBLFilter.SelectedValue;
        AMSS.Is_Delete = false;
        DataTable dt = new DataTable();
        Attach_Repostry_SMS_Send_ ARSS = new Attach_Repostry_SMS_Send_();
        dt = ARSS.BAttach_SMS_Send_Manage(AMSS);

        if (dt.Rows.Count > 0)
        {
            txtSearch.Text = XMessage;
            GVMessage.DataSource = dt;
            GVMessage.DataBind();
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

    protected void LBPrintAll_Click(object sender, EventArgs e)
    {
        try
        {
            GVMessage.UseAccessibleHeader = true;
            GVMessage.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
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
        Response.Redirect("PageMessage.aspx");
    }

    protected void btnGet_Click(object sender, EventArgs e)
    {
        try
        {
            if (RBLFilter.SelectedValue == "All")
                FAttach_SMS_Send_Manage("GetAll", " قائمة الرسائل المرسلة من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim());
            else
                FAttach_SMS_Send_Manage("GetAllByFilter", " قائمة الرسائل المرسلة من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim() + ", حسب " + RBLFilter.SelectedItem.ToString());
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