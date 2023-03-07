using Library_CLS_Arn.CRM.Models;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters;
using Library_CLS_Arn.OM.Models;
using Library_CLS_Arn.OM.Repostry;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_CRM_PageKind_Support_PageKind_SupportAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                pnlSelect.Visible = true;
                Repostry_Company_.FCRM_Company_Manage(DLCompany);
                Repostry_FinancialYear_.FErp_FinancialYear_Manage(ddlYears);
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

    protected void LBRefrsh_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageKind_SupportAdd.aspx");
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageKind_Support.aspx");
    }

    protected void btn_Add_To__Click(object sender, EventArgs e)
    {
        try
        {
            FCheckNumber();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FCheckNumber()
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            Model_In_Kind_Donation_Bill_ MIKDB = new Model_In_Kind_Donation_Bill_();
            MIKDB.IDCheck = "GetByBillCheck";
            MIKDB.ID_Item = new Guid(ddlYears.SelectedValue);
            MIKDB.bill_Number = Convert.ToInt64(txtNo_Bill.Text.Trim());
            MIKDB.ID_Donor = new Guid(DLCompany.SelectedValue);
            MIKDB.Start_Date = string.Empty;
            MIKDB.End_Date = string.Empty;
            MIKDB.DateCheck = string.Empty;
            MIKDB.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_In_Kind_Donation_Bill_ RIKDB = new Repostry_In_Kind_Donation_Bill_();
            dt = RIKDB.BOM_In_Kind_Donation_Bill_Manage(MIKDB);
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                IDMessage.InnerHtml = "تم التحقق من فواتير الدعم العيني , وحصلنا على الفانورة برقم " + txtNo_Bill.Text.Trim();
                IDMessage.InnerHtml += "<br /><br /> مبلغ الفاتورة " + Repostry_In_Kind_Donation_Details_.FOM_In_Kind_Donation_Details_Manage(new Guid(dt.Rows[0]["_ID_Item_"].ToString())) + " " + ClassSaddam.FGetMonySa();
                IDMessage.InnerHtml += "<br /><br /> بإسم " + Repostry_Company_.FCRM_Company_Manage(new Guid(dt.Rows[0]["_ID_Donor_"].ToString()));
            }
            else
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "لاتوجد فاتورة بهذة التفاصيل ... ";
                return;
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

    private void FCRM_In_Kind_Support_Add()
    {
        Model_In_Kind_Support_ MIKS = new Model_In_Kind_Support_()
        {
            IDCheck = "Add",
            ID_Item = Guid.NewGuid(),
            ID_FinancialYear = new Guid(ddlYears.SelectedValue),
            ID_Company = new Guid(DLCompany.SelectedValue),
            ID_Bill = Convert.ToInt64(txtNo_Bill.Text.Trim()),
            CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            ModifiedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            Is_Delete = false
        };

        Repostry_In_Kind_Support_ RIKS = new Repostry_In_Kind_Support_();
        string Xresult = RIKS.FCRM_In_Kind_Support_Add(MIKS);
        if (Xresult == "IsExistsID_BillAdd")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "تم إضافة رقم الفاتورة سابقاً ... ";
            return;
        }
        else if (Xresult == "IsExistsAdd")
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
            FCRM_In_Kind_Support_Manage(new Guid(DLCompany.SelectedValue));
        }
    }

    private void FCRM_In_Kind_Support_Manage(Guid XIDComp)
    {
        GVBillAll.Columns[0].Visible = true;
        GVBillAll.Columns[9].Visible = true;
        GVBillAll.UseAccessibleHeader = false;
        Model_In_Kind_Support_ MIKS = new Model_In_Kind_Support_();
        MIKS.IDCheck = "GetByIDComp";
        MIKS.ID_Item = Guid.Empty;
        MIKS.ID_Company = XIDComp;
        MIKS.Start_Date = string.Empty;
        MIKS.End_Date = string.Empty;
        MIKS.CreatedDate = string.Empty;
        MIKS.Is_Delete = false;
        DataTable dt = new DataTable();
        Repostry_In_Kind_Support_ RIKS = new Repostry_In_Kind_Support_();
        dt = RIKS.BCRM_In_Kind_Support_Manage(MIKS);
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "قائمة فواتير الدعم العيني لـ " + DLCompany.SelectedItem.ToString();
            GVBillAll.DataSource = dt;
            GVBillAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlSelect.Visible = false;
            btnPrint.Visible = true; btnDelete.Visible = true;
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlSelect.Visible = false;
            btnPrint.Visible = false; btnDelete.Visible = false;
        }
    }

    protected void DLCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FCRM_In_Kind_Support_Manage(new Guid(DLCompany.SelectedValue));
            txtNo_Bill.Focus();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            GVBillAll.Columns[0].Visible = false;
            GVBillAll.Columns[9].Visible = false;
            GVBillAll.UseAccessibleHeader = true;
            GVBillAll.HeaderRow.TableSection = TableRowSection.TableHeader;

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

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            foreach (GridViewRow row in GVBillAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    Guid Comp_ID = new Guid(GVBillAll.DataKeys[row.RowIndex].Value.ToString());
                    Model_In_Kind_Support_ MIKS = new Model_In_Kind_Support_()
                    {
                        IDCheck = "Delete",
                        ID_Item = Comp_ID,
                        ID_Company = Guid.Empty,
                        ID_Bill = 0,
                        CreatedDate = string.Empty,
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        Is_Delete = true
                    };

                    Repostry_In_Kind_Support_ RIKS = new Repostry_In_Kind_Support_();
                    string Xresult = RIKS.FCRM_In_Kind_Support_Add(MIKS);
                    if (Xresult == "IsSuccessDelete")
                    {
                        IDMessageWarning.Visible = false;
                        IDMessageSuccess.Visible = true;
                        lblSuccess.Text = "تم حذف البيانات بنجاح ... ";
                    }
                }
            }
            FCRM_In_Kind_Support_Manage(new Guid(DLCompany.SelectedValue));
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    decimal sum = 0;
    protected void GVBillAll_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");
            sum += decimal.Parse(salary.Text);
            lblTotalPrice.Text = sum.ToString();

            List<CurrencyInfo> currencies = new List<CurrencyInfo>();
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            ToWord toWord = new ToWord(Convert.ToDecimal(lblTotalPrice.Text), currencies[Convert.ToInt32(0)]);
            lblSumWord.Text = toWord.ConvertToArabic();
        }
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FCRM_In_Kind_Support_Add();
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