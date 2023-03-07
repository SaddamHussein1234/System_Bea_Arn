using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_PageExclusionOfTheBeneficiary : System.Web.UI.Page
{
    string XID;
    string IDUser, IDUniq;
    private void GetCookie()
    {
        try
        {
            HttpCookie Cooke;  // رقم المستخدم
            Cooke = Request.Cookies[DateTime.Now.ToString("_545_yyyyMMyyyyMM_125_")];
            IDUser = ClassSaddam.UnprotectPassword(Cooke["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");

            HttpCookie CookeUniq;  // رقم يونيك
            CookeUniq = Request.Cookies["__CheckedAdmin_True_"];
            IDUniq = ClassSaddam.UnprotectPassword(CookeUniq["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM");
        }
        catch
        {
            Response.Redirect("PageNotAccess.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        ClassAdmin_Arn CAA = new ClassAdmin_Arn();
        CAA._IDUniq = IDUniq;
        CAA._IsDelete = false;
        DataTable dtViewProfil = new DataTable();
        dtViewProfil = CAA.BArnAdminGetByIDUniq();
        if (dtViewProfil.Rows.Count > 0)
        {
            bool A54;
            A54 = Convert.ToBoolean(dtViewProfil.Rows[0]["A54"]);
            if (A54 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
        }
    }

    private void FArnEadatMostafeedByAll()
    {
        try
        {
            GVRe_beneficiary.UseAccessibleHeader = false;
            ClassEadatMostafeed CEM = new ClassEadatMostafeed();
            CEM._IDCheck = 0;
            CEM._AlQaryah = 0;
            CEM._IDUniq = txtSearch.Text.Trim();
            CEM._IsEaadat = false;
            CEM._IsEstbaad = true;
            CEM._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CEM.BArnEadatMostafeedByAll();
            if (dt.Rows.Count > 0)
            {
                GVRe_beneficiary.DataSource = dt;
                GVRe_beneficiary.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                txtTitle.Focus();
                GVRe_beneficiary.Columns[0].Visible = true;
                GVRe_beneficiary.Columns[11].Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                pnlSelect.Visible = false;
            }
            CheckAccountAdmin();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageExclusionOfTheBeneficiary.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVRe_beneficiary.Columns[0].Visible = false;
            GVRe_beneficiary.Columns[11].Visible = false;
            GVRe_beneficiary.UseAccessibleHeader = true;
            GVRe_beneficiary.HeaderRow.TableSection = TableRowSection.TableHeader;
            Session["footable1"] = pnlData;
            //if (GVRe_beneficiary.Rows.Count > 14)
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable2.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
            //}
            //else if (GVRe_beneficiary.Rows.Count <= 14)
            //{
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
            //}
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FArnEadatMostafeedByAll();
        System.Threading.Thread.Sleep(500);
    }

}