using Library_CLS_Arn.CRM.Models;
using Library_CLS_Arn.CRM.Repostry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_CRM_PageCompany_PageCompany : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            FGetData();
    }

    private void FGetData()
    {
        try
        {
            GVCompany.Columns[0].Visible = true;
            GVCompany.Columns[9].Visible = true;
            GVCompany.UseAccessibleHeader = false;

            Model_Company_ MC = new Model_Company_();
            MC.IDCheck = "GetAll";
            MC.ID_Item = Guid.NewGuid();
            MC.Company_Name = txtSearch.Text.Trim();
            MC.Is_Active = true;
            MC.Is_Delete = false;
            DataTable dt = new DataTable();
            Repostry_Company_ RC = new Repostry_Company_();
            dt = RC.BCRM_Company_Manage(MC);
            if (dt.Rows.Count > 0)
            {
                GVCompany.DataSource = dt;
                GVCompany.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            foreach (GridViewRow row in GVCompany.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVCompany.DataKeys[row.RowIndex].Value);
                    Model_Company_ MC = new Model_Company_()
                    {
                        IDCheck = "Delete",
                        ID_Item = new Guid(Comp_ID),
                        Type_Customer = string.Empty,
                        Company_Name = string.Empty,
                        Company_Type = Guid.Empty,
                        Registration_No = 0,
                        Address = string.Empty,
                        Country = Guid.Empty,
                        City = string.Empty,
                        Website = string.Empty,
                        Email_Address = string.Empty,
                        Established_Year = 0,
                        Fax = string.Empty,
                        Phone_Number1 = string.Empty,
                        Mobile_Number1 = string.Empty,
                        Phone_Number2 = string.Empty,
                        Icon_Img = string.Empty,
                        Is_Active = false,
                        CreatedDate = string.Empty,
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        Is_Delete = true
                    };
                    Repostry_Company_ RC = new Repostry_Company_();
                    string Xresult = RC.FCRM_Company_Add(MC);
                    if (Xresult == "IsSuccessDelete")
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم حذف البيانات بنجاح ... ";
                    }
                }
            }
            FGetData();
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
        Response.Redirect("PageCompany.aspx");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetData();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVCompany.Columns[0].Visible = false;
            GVCompany.Columns[9].Visible = false;
            GVCompany.UseAccessibleHeader = true;
            GVCompany.HeaderRow.TableSection = TableRowSection.TableHeader;

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

}