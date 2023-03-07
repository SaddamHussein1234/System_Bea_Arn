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

public partial class Cpanel_ERP_DMS_OutComingGeneral_PageView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            pnlSelect.Visible = true;
            if (Request.QueryString["Type"] == "Association")
                HFIDStore.Value = Class_Identity_.FGetIdentityAssociation();
            else if (Request.QueryString["Type"] == "Institute")
                HFIDStore.Value = Class_Identity_.FGetIdentityInstitute();
            else
                HFIDStore.Value = Class_Identity_.FGetIdentityAssociation();
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            ddlYears.SelectedValue = Request.QueryString["IDYear"];
            txtSearch.Text = Request.QueryString["ID"];
            txtSearch.Focus();
            if (Request.QueryString["ID"] != null)
                FUpdateSee(Convert.ToInt64(txtSearch.Text.Trim()));
        }
    }

    private void FGetByIDNumber()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Repostry_DMS_OutComing_General_.FGetDataInDataTable("GetByIDNumber", 1, Guid.Empty, new Guid(HFIDStore.Value), new Guid(ddlYears.SelectedValue),
                txtSearch.Text.Trim(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                ID_Edit_.HRef = "PageAdd.aspx?ID=" + dt.Rows[0]["_ID_Item_"].ToString() + "&Type=" + Request.QueryString["Type"]; ID_Edit_.Visible = true;
                FGetByIDOutComing_General(new Guid(dt.Rows[0]["_ID_Item_"].ToString()));
                lblSender.Text = dt.Rows[0]["_Name_Ar_"].ToString();
                lblNumber_File.Text = dt.Rows[0]["_Number_File_"].ToString();
                lblDate_Transaction.Text = Convert.ToDateTime(dt.Rows[0]["_Date_Transaction_"]).ToString("yyyy-MM-dd") + " م";
                lblTitle.Text = dt.Rows[0]["_The_Title_"].ToString();
                lblTitle_Attachments.Text = dt.Rows[0]["_The_Title_Attachments_"].ToString();
                IDDetails.InnerHtml += "<tr><td colspan='2'><b>" + dt.Rows[0]["_The_Details_"].ToString().Replace("<br /><br />", "<br /><br>").Replace("<br /><br /><br />", "<br /><br><br>").Replace("<br />", "</b></td></tr><tr><td colspan='2'><b>") + "</b></td></tr>";
                
                if (Convert.ToBoolean(dt.Rows[0]["_Is_General_Director_"]))
                {
                    ID_General_Director.Visible = true;
                    lbl_General_Director.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_ID_General_Director_"].ToString()));
                    Img_General_Director.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["_ID_General_Director_"]));
                    Img_General_Director.Width = 100;
                }
                else
                    ID_General_Director.Visible = false;

                if (Convert.ToBoolean(dt.Rows[0]["_Is_Director_Of_Personnel_"]))
                {
                    ID_Director_Of_Personnel.Visible = true;
                    lbl_Director_Of_Personnel.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_ID_Director_Of_Personnel_"].ToString()));
                    Img_Director_Of_Personnel.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["_ID_Director_Of_Personnel_"]));
                    Img_Director_Of_Personnel.Width = 100;
                }
                else
                    ID_Director_Of_Personnel.Visible = false;

                if (Convert.ToBoolean(dt.Rows[0]["_Is_Cashier_"]))
                {
                    ID_Cashier.Visible = true;
                    lbl_Cashier.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_ID_Cashier_"].ToString()));
                    Img_Cashier.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["_ID_Cashier_"]));
                    Img_Cashier.Width = 100;
                }
                else
                    ID_Cashier.Visible = false;

                if (Convert.ToBoolean(dt.Rows[0]["_Is_Secretary_General_"]))
                {
                    ID_Secretary_General.Visible = true;
                    lbl_Secretary_General.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_ID_Secretary_General_"].ToString()));
                    Img_Secretary_General.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["_ID_Secretary_General_"]));
                    Img_Secretary_General.Width = 100;
                }
                else
                    ID_Secretary_General.Visible = false;


                if (Convert.ToBoolean(dt.Rows[0]["_Is_Deputy_Chairman_Of_The_Board_"]))
                {
                    ID_Deputy_Chairman_Of_The_Board.Visible = true;
                    lbl_Deputy_Chairman_Of_The_Board.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_ID_Deputy_Chairman_Of_The_Board_"].ToString()));
                    Img_Deputy_Chairman_Of_The_Board.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["_ID_Deputy_Chairman_Of_The_Board_"]));
                    Img_Deputy_Chairman_Of_The_Board.Width = 100;
                }
                else
                    ID_Deputy_Chairman_Of_The_Board.Visible = false;

                if (Convert.ToBoolean(dt.Rows[0]["_Is_Chairman_Of_Board_Of_Directors_"]))
                {
                    ID_Chairman_Of_Board_Of_Directors.Visible = true;
                    lbl_Chairman_Of_Board_Of_Directors.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_ID_Chairman_Of_Board_Of_Directors_"].ToString()));
                    Img_Chairman_Of_Board_Of_Directors.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["_ID_Chairman_Of_Board_Of_Directors_"]));
                    Img_Chairman_Of_Board_Of_Directors.Width = 100;
                }
                else
                    ID_Chairman_Of_Board_Of_Directors.Visible = false;

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/ERP/DMS/OutComingGeneral/PageView.aspx?ID=" + txtSearch.Text.Trim() + "&IDYears=" + ddlYears.SelectedValue + "&Type=" + Request.QueryString["Type"];
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

    private void FUpdateSee(Int64 XID_File)
    {
        string XCheck = string.Empty;
        Guid XID = Guid.Empty; Guid XID_Marketed = Guid.Empty;
        string XDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss");
        string Xresult = Repostry_DMS_OutComing_General_.FAdd("UpdateSee", Guid.Empty, new Guid(HFIDStore.Value), new Guid(ddlYears.SelectedValue),
            Guid.Empty, XID_File, string.Empty, string.Empty, Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, string.Empty,
            string.Empty, string.Empty, false, 0, false, 0, false, 0, false, 0, false, 0, false, 0, Test_Saddam.FGetIDUsiq(), 0, 0, 0, XDate, true);
        if (Xresult == "IsSuccess")
            FGetByIDNumber();
    }

    private void FGetByIDOutComing_General(Guid XID)
    {
        DataTable dt = new DataTable();
        dt = Repostry_DMS_OutComing_General_Attachments_.FGetDataInDataTable("GetByIDOutComing_General", 15, XID, new Guid(HFIDStore.Value), string.Empty, true);
        if (dt.Rows.Count > 0)
        {
            RPTFiles.DataSource = dt;
            RPTFiles.DataBind();
            IDTable.Visible = true;
        }
        else
        {
            IDTable.Visible = false;
        }
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["Type"] == "Association")
            {
                if (DLPrint.SelectedValue == "OutCome")
                { IDKhatm.Visible = false; FCheckKhatm(false); Kelesha.Visible = true; wucHeaderAssociation.Visible = false; wucHeaderInstitute.Visible = false; wucFooterNewAssociation.Visible = false; wucFooterInstitute.Visible = false; IDBody.Attributes.Add("class", "Margin_Body"); }
                else if (DLPrint.SelectedValue == "OutCome_Khatm")
                { IDKhatm.Visible = true; FCheckKhatm(true); Kelesha.Visible = true; wucHeaderAssociation.Visible = false; wucHeaderInstitute.Visible = false; wucFooterNewAssociation.Visible = false; wucFooterInstitute.Visible = false; IDBody.Attributes.Add("class", "Margin_Body"); }
                else if (DLPrint.SelectedValue == "OutCome_Kalesh")
                { IDKhatm.Visible = false; FCheckKhatm(false); Kelesha.Visible = true; wucHeaderAssociation.Visible = true; wucHeaderInstitute.Visible = false; wucFooterNewAssociation.Visible = true; wucFooterInstitute.Visible = false; IDBody.Attributes.Add("class", ""); }
                else if (DLPrint.SelectedValue == "OutCome_Khatm_Kalesh")
                { IDKhatm.Visible = true; FCheckKhatm(true); Kelesha.Visible = true; wucHeaderAssociation.Visible = true; wucHeaderInstitute.Visible = false; wucFooterNewAssociation.Visible = true; wucFooterInstitute.Visible = false; IDBody.Attributes.Add("class", ""); }
            }
            else if (Request.QueryString["Type"] == "Institute")
            {
                if (DLPrint.SelectedValue == "OutCome")
                { IDKhatm.Visible = false; FCheckKhatm(false); Kelesha.Visible = true; wucHeaderAssociation.Visible = false; wucHeaderInstitute.Visible = false; wucFooterNewAssociation.Visible = false; wucFooterInstitute.Visible = false; IDBody.Attributes.Add("class", "Margin_Body"); }
                else if (DLPrint.SelectedValue == "OutCome_Khatm")
                { IDKhatm.Visible = true; FCheckKhatm(true); Kelesha.Visible = true; wucHeaderAssociation.Visible = false; wucHeaderInstitute.Visible = false; wucFooterNewAssociation.Visible = false; wucFooterInstitute.Visible = false; IDBody.Attributes.Add("class", "Margin_Body"); }
                else if (DLPrint.SelectedValue == "OutCome_Kalesh")
                { IDKhatm.Visible = false; FCheckKhatm(false); Kelesha.Visible = true; wucHeaderAssociation.Visible = false; wucHeaderInstitute.Visible = true; wucFooterNewAssociation.Visible = false; wucFooterInstitute.Visible = true; IDBody.Attributes.Add("class", ""); }
                else if (DLPrint.SelectedValue == "OutCome_Khatm_Kalesh")
                { IDKhatm.Visible = true; FCheckKhatm(true); Kelesha.Visible = true; wucHeaderAssociation.Visible = false; wucHeaderInstitute.Visible = true; wucFooterNewAssociation.Visible = false; wucFooterInstitute.Visible = true; IDBody.Attributes.Add("class", ""); }
            }
            Session["foot"] = pnl2;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4OutHeader.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');printWindow.document.write('<html><head><title>Name of File</title>');printWindow.document.write('</head><body>');printWindow.document.write('</body></html>');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FCheckKhatm(bool XValue)
    {
        Img_General_Director.Visible = XValue;
        Img_Director_Of_Personnel.Visible = XValue;
        Img_Cashier.Visible = XValue;
        Img_Secretary_General.Visible = XValue;
        Img_Deputy_Chairman_Of_The_Board.Visible = XValue;
        Img_Chairman_Of_Board_Of_Directors.Visible = XValue;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FUpdateSee(Convert.ToInt64(txtSearch.Text.Trim()));
    }

}