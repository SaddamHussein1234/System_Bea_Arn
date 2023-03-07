using Library_CLS_Arn.CRM.Models;
using Library_CLS_Arn.CRM.Repostry;
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

public partial class Cpanel_ERP_CRM_PageCash_Support_PageCash_SupportAdd : System.Web.UI.Page
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
                    Model_In_Cash_Support_ MICS = new Model_In_Cash_Support_()
                    {
                        IDCheck = "Delete",
                        ID_Item = Comp_ID,
                        ID_Company = Guid.Empty,
                        ID_Bill = 0,
                        The_Mony = 0,
                        CreatedDate = string.Empty,
                        CreatedBy = 0,
                        ModifiedBy = 0,
                        ModifiedDate = string.Empty,
                        Is_Delete = true
                    };

                    Repostry_In_Cash_Support_ RICS = new Repostry_In_Cash_Support_();
                    string Xresult = RICS.FCRM_In_Cash_Support_Add(MICS);
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

    protected void LBRefrsh_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageCash_SupportAdd.aspx");
    }

    protected void LBExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageCash_Support.aspx");
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
            Model_Cash_Donation_ MCD = new Model_Cash_Donation_();
            MCD.IDCheck = "GetByBillCheck";
            MCD.ID_Item = new Guid(ddlYears.SelectedValue);
            MCD.bill_Number = Convert.ToInt64(txtNo_Bill.Text.Trim());
            MCD.ID_Donor = new Guid(DLCompany.SelectedValue);
            MCD.Start_Date = string.Empty;
            MCD.End_Date = string.Empty;
            MCD.DataCheck = string.Empty;
            MCD.DataCheck2 = string.Empty;
            MCD.DataCheck3 = string.Empty;
            MCD.IsActive = true;
            DataTable dt = new DataTable();
            Repostry_Cash_Donation_ RCD = new Repostry_Cash_Donation_();
            dt = RCD.BOM_Cash_Donation_Manage(MCD);
            if (dt.Rows.Count > 0)
            {
                txtMoney_Bill.Text = Convert.ToInt64(dt.Rows[0]["_The_Mony_"]).ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModal();", true);
                IDMessage.InnerHtml = "تم التحقق من فواتير الدعم العيني , وحصلنا على الفانورة برقم " + txtNo_Bill.Text.Trim();
                IDMessage.InnerHtml += "<br /><br /> مبلغ الفاتورة " + dt.Rows[0]["_The_Mony_"].ToString() + " " + ClassSaddam.FGetMonySa();
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

    private void FCRM_In_Cash_Support_Add()
    {
        Model_In_Cash_Support_ MICS = new Model_In_Cash_Support_()
        {
            IDCheck = "Add",
            ID_Item = Guid.NewGuid(),
            ID_FinancialYear = new Guid(ddlYears.SelectedValue),
            ID_Company = new Guid(DLCompany.SelectedValue),
            ID_Bill = Convert.ToInt64(txtNo_Bill.Text.Trim()),
            The_Mony = Convert.ToInt64(txtMoney_Bill.Text.Trim()),
            CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            ModifiedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            Is_Delete = false
        };

        Repostry_In_Cash_Support_ RICS = new Repostry_In_Cash_Support_();
        string Xresult = RICS.FCRM_In_Cash_Support_Add(MICS);
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
        Model_In_Cash_Support_ MICS = new Model_In_Cash_Support_();
        MICS.IDCheck = "GetByIDComp";
        MICS.ID_Item = Guid.Empty;
        MICS.ID_Company = XIDComp;
        MICS.Start_Date = string.Empty;
        MICS.End_Date = string.Empty;
        MICS.CreatedDate = string.Empty;
        MICS.Is_Delete = false;
        DataTable dt = new DataTable();
        Repostry_In_Cash_Support_ RICS = new Repostry_In_Cash_Support_();
        dt = RICS.BCRM_In_Cash_Support_Manage(MICS);
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "قائمة فواتير الدعم النقدي لـ " + DLCompany.SelectedItem.ToString();
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
            lblSa.Text = ClassSaddam.FGetMonySa();
        }
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {
        IDMessageSuccess.Visible = false;
        IDMessageWarning.Visible = false;
        try
        {
            FCRM_In_Cash_Support_Add();
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