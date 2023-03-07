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

public partial class CPBeneficiary_PageSupportHome : System.Web.UI.Page
{
    string UserERasAlEstemarah;
    private void GetCookie()
    {
        try
        {
            HttpCookie CookeCheck;  // اسم المستخدم
            CookeCheck = Request.Cookies["__User_True_User"];
            UserERasAlEstemarah = ClassSaddam.UnprotectPassword(CookeCheck["Url"], "www.ITFY-Edu.Net_ProtectBySADDAM_ForUser");
        }
        catch
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    private void CheckAccountAdmin()
    {
        GetCookie();
        ClassMosTafeed CM = new ClassMosTafeed();
        CM._User_Name_ = UserERasAlEstemarah;
        CM._IsDelete = false;
        DataTable dt = new DataTable();
        dt = CM.BArnRasAlEstemarahLogin();
        if (dt.Rows.Count > 0)
        {
            bool A7;
            A7 = Convert.ToBoolean(dt.Rows[0]["A7"]);
            if (A7)
                FArnProductShopBySupportByBeneficiaryTarmem(dt.Rows[0]["NumberMostafeed"].ToString());
            else
                Response.Redirect("PageNotAccess.aspx");           
        }
        else
        {
            Response.Redirect("LogOut.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelectTarmem.Visible = false;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageSupportHome.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVExchangeOrdersTarmem.Columns[0].Visible = false;
            GVExchangeOrdersTarmem.Columns[10].Visible = false;
            lblModerAlGmeiahTarmem.Text = DLModerAlGmeiahTarmem.SelectedItem.ToString();
            lblRaeesMaglesAlEdarahTarmem.Text = DLRaeesMaglesAlEdarahTarmem.SelectedItem.ToString();
            lblAmeenAlSondoqTarmem.Text = DLAmeenAlSondoqTarmem.SelectedItem.ToString();
            DLModerAlGmeiahTarmem.Visible = false;
            DLRaeesMaglesAlEdarahTarmem.Visible = false;
            DLAmeenAlSondoqTarmem.Visible = false;
            lblModerAlGmeiahTarmem.Visible = true;
            lblRaeesMaglesAlEdarahTarmem.Visible = true;
            lblAmeenAlSondoqTarmem.Visible = true;

            GVExchangeOrdersTarmem.UseAccessibleHeader = true;
            GVExchangeOrdersTarmem.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlDataTarmem;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    int Cou = 0;
    decimal sum = 0;
    int tempcounter = 0;
    protected void GVExchangeOrdersTarmem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
            sum += decimal.Parse(salary.Text);
            if (sum != 0)
            {
                lblTotalPriceTarmem.Text = sum.ToString();
            }
            else
            {
                lblTotalPriceTarmem.Text = "بإنتظار التسعير";
            }

            tempcounter = tempcounter + 1;
            if (tempcounter == 14)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }
    }

    private void FArnProductShopBySupportByBeneficiaryTarmem(string XID)
    {
        try
        {
            ClassBenaaAndTarmim CBAT = new ClassBenaaAndTarmim();
            CBAT._NumberMostafeed = Convert.ToInt32(XID);
            CBAT._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CBAT.BArnProductShopBySupportByBeneficiaryHouseByMostafeed();
            if (dt.Rows.Count > 0)
            {
                GVExchangeOrdersTarmem.DataSource = dt;
                GVExchangeOrdersTarmem.DataBind();
                lblCountTarmem.Text = Convert.ToString(dt.Rows.Count);
                pnlNullTarmem.Visible = false;
                pnlDataTarmem.Visible = true;
                pnlSelectTarmem.Visible = false;
                txtTitleTarmem.Text = "قائمة الدعم العيني ( بناء وترميم المنزل ) ";
                FGetDataMostafedTarmem(XID);
            }
            else
            {
                pnlNullTarmem.Visible = true;
                pnlDataTarmem.Visible = false;
                pnlSelectTarmem.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetDataMostafedTarmem(string XID)
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0 And IsDelete = @1", XID, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberFileTarmem.Text = XID;
            lblNameTarmem.Text = dt.Rows[0]["NameMostafeed"].ToString();
            lblAlQariahTarmem.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
            lblGenderTarmem.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
            lblPhoneTarmem.Text = dt.Rows[0]["PhoneNumber"].ToString();
            lblNumberSigalTarmem.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            lblHalatAlmostafeedTarmem.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
            Session["XID"] = Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]);
            lblDateBrithDayTarmem.Text = ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["dateBrith"])) + "هـ" + " - " + Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") + "مـ";
            if (dt.Rows[0]["dateBrith"].ToString() != string.Empty)
            {
                lblAgeTarmem.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            }
            else
            {
                lblAgeTarmem.Text = dt.Rows[0]["Age"].ToString();
            }

            if (Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") == "01/01/1900")
            {
                lblDateBrithDayTarmem.Text = "لم يُضاف";
                lblAgeTarmem.Text = "لم يُضاف";
            }
        }
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiahTarmem);
        ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarahTarmem);
        ClassAdmin_Arn.FGetRaeesLagnatAlBahath(DLAmeenAlSondoqTarmem);

        ImgModerTarmem.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiahTarmem.SelectedValue));
        ImgAmeenAlSondoqTarmem.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLAmeenAlSondoqTarmem.SelectedValue));
        ImgRaeesMaglesAlEdarahTarmem.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarahTarmem.SelectedValue));

        lblModerAlGmeiahTarmem.Text = DLModerAlGmeiahTarmem.SelectedItem.ToString();
        lblRaeesMaglesAlEdarahTarmem.Text = DLRaeesMaglesAlEdarahTarmem.SelectedItem.ToString();
        lblAmeenAlSondoqTarmem.Text = DLAmeenAlSondoqTarmem.SelectedItem.ToString();
        DLModerAlGmeiahTarmem.Visible = false;
        DLRaeesMaglesAlEdarahTarmem.Visible = false;
        DLAmeenAlSondoqTarmem.Visible = false;
        lblModerAlGmeiahTarmem.Visible = true;
        lblRaeesMaglesAlEdarahTarmem.Visible = true;
        lblAmeenAlSondoqTarmem.Visible = true;
    }

}