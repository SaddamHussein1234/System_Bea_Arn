using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.CRS.Repostry;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_CRS_Committee_Report_PageView : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtSearch.Focus();
            pnlSelect.Visible = true;
            Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
            ddlYears.SelectedValue = Request.QueryString["IDUniq"];
            txtSearch.Text = Request.QueryString["ID"];
            FGetDataMostafed();
        }
    }

    private void FGetDataMostafed()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Repostry_CRS_Committee_Report_.FGetDataInDataTable("GetByBill", 1, new Guid(ddlYears.SelectedValue), txtSearch.Text.Trim(), string.Empty, 
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, true);
            if (dt.Rows.Count > 0)
            {
                ID_Edit_.HRef = "PageAdd.aspx?ID=" + dt.Rows[0]["_ID_Item_"].ToString(); ID_Edit_.Visible = true;
                txtTitle.Text = "تقرير (" + dt.Rows[0]["_Name_Ar_"].ToString() + ")";
                lblType.Text = dt.Rows[0]["_Title_"].ToString();
                lblNmber.Text = dt.Rows[0]["_Nmber_"].ToString() + " \\ " + ddlYears.SelectedItem.Text;
                lblMeeting_Venue.Text = dt.Rows[0]["_Meeting_Venue_"].ToString();

                lblDate.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("dd/MM/yyyy");
                txtNote.Text = dt.Rows[0]["_Note_"].ToString();
                lbl_Chairman_Of_Board_Of_Directors.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_IDRaees_"].ToString()));
                if (Convert.ToBoolean(dt.Rows[0]["_IsRaees_"]))
                {
                    Img_Chairman_Of_Board_Of_Directors.ImageUrl = ClassSaddam.FGetSignatureERP(Convert.ToInt32(dt.Rows[0]["_IDRaees_"]));
                    Img_Chairman_Of_Board_Of_Directors.Width = 100; IDKhatm.Visible = true; IDKhatmLodding.Visible = false;
                }
                else
                {
                    Img_Chairman_Of_Board_Of_Directors.ImageUrl = "/Cpanel/loaderMin.gif";
                    Img_Chairman_Of_Board_Of_Directors.Width = 30; IDKhatm.Visible = false; IDKhatmLodding.Visible = true;
                }

                //lblDataEntery.Text = ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_CreatedBy_"].ToString()));
                //lblDateEntery.Text = Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("dd/MM/yyyy");

                IDObjective_Of_the_Report.InnerHtml += "<tr><td>" + dt.Rows[0]["_Objective_Of_the_Report_"].ToString().Replace("<br /><br />", "<br /><br>").Replace("<br /><br /><br />", "<br /><br><br>").Replace("<br />", "</td></tr><tr><td>") + "</td></tr>";
                IDReport_Recommendations.InnerHtml += "<tr><td>" + dt.Rows[0]["_Report_Recommendations_"].ToString().Replace("<br /><br />", "<br /><br>").Replace("<br /><br /><br />", "<br /><br><br>").Replace("<br />", "</td></tr><tr><td>") + "</td></tr>";

                string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/ERP/CRS/Committee_Report/PageView.aspx?ID=" + txtSearch.Text.Trim() + "&IDUniq=" + ddlYears.SelectedValue;
                Class_QRScan.FGetQRCode(code, ImgQRCode);
                Class_QRScan.FGetQRCode(code, ImgQRCode2);

                pnlData.Visible = true;
                pnlSelect.Visible = false;
                FGetImages(new Guid(dt.Rows[0]["_ID_Item_"].ToString()));
                FGetCommittee_Members(new Guid(dt.Rows[0]["_ID_Item_"].ToString()));
            }
            else
            {
                pnlData.Visible = false;
                pnlSelect.Visible = true;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetImages(Guid XID)
    {
        DataTable dt = new DataTable();
        dt = Repostry_CRS_Images_.FGetDataInDataTable("GetAllByReport", 50, XID, string.Empty, string.Empty, string.Empty, true);
        if (dt.Rows.Count > 0)
        {
            RPTImages.DataSource = dt;
            RPTImages.DataBind();
            pnlDataImages.Visible = true;
            RPTImages.Visible = true;
            pnlNullImages.Visible = false;
        }
        else
        {
            pnlDataImages.Visible = false;
            RPTImages.Visible = false;
            pnlNullImages.Visible = true;
        }
    }

    private void FGetCommittee_Members(Guid XID)
    {
        RPTCommittee_Members.DataBind();
        DataTable dt = new DataTable();
        dt = Repostry_CRS_Committee_Members_.FGetDataInDataTable("GetAllByReport", 50, XID, string.Empty, string.Empty, string.Empty, true);
        if (dt.Rows.Count > 0)
        {
            RPTCommittee_Members.DataSource = dt;
            RPTCommittee_Members.DataBind();
            pnlDataCommittee_Members.Visible = true;
            pnlNullCommittee_Members.Visible = false;
        }
        else
        {
            pnlDataCommittee_Members.Visible = false;
            pnlNullCommittee_Members.Visible = true;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["foot"] = IDPrint;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/Print/PagePrintA4OutHeader.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageView.aspx?ID=" + txtSearch.Text + "&IDUniq=" + ddlYears.SelectedValue);
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageAll.aspx");
    }

}