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

public partial class CPBeneficiary_PageSupportMony : System.Web.UI.Page
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
                FArnProductShopBySupportByBeneficiaryPrisms(dt.Rows[0]["NumberMostafeed"].ToString());
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
            pnlSelectPrisms.Visible = false;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageSupportMony.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVExchangeOrdersPrisms.Columns[0].Visible = false;
            GVExchangeOrdersPrisms.Columns[10].Visible = false;
            lblModerAlGmeiahPrisms.Text = DLModerAlGmeiahPrisms.SelectedItem.ToString();
            lblRaeesMaglesAlEdarahPrisms.Text = DLRaeesMaglesAlEdarahPrisms.SelectedItem.ToString();
            lblAmeenAlSondoqPrisms.Text = DLAmeenAlSondoqPrisms.SelectedItem.ToString();
            DLModerAlGmeiahPrisms.Visible = false;
            DLRaeesMaglesAlEdarahPrisms.Visible = false;
            DLAmeenAlSondoqPrisms.Visible = false;
            lblModerAlGmeiahPrisms.Visible = true;
            lblRaeesMaglesAlEdarahPrisms.Visible = true;
            lblAmeenAlSondoqPrisms.Visible = true;

            GVExchangeOrdersPrisms.UseAccessibleHeader = true;
            GVExchangeOrdersPrisms.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlDataPrisms;
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
    protected void GVExchangeOrdersPrisms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
            sum += decimal.Parse(salary.Text);
            if (sum != 0)
            {
                lblTotalPricePrisms.Text = sum.ToString();
            }
            else
            {
                lblTotalPricePrisms.Text = "بإنتظار التسعير";
            }

            tempcounter = tempcounter + 1;
            if (tempcounter == 14)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }
    }

    private void FArnProductShopBySupportByBeneficiaryPrisms(string XID)
    {
        try
        {
            ClassSupportForPrisms CSFP = new ClassSupportForPrisms();
            CSFP._NumberMostafeed = Convert.ToInt32(XID);
            CSFP._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CSFP.BArnSupportForPrismsByBeneficiaryByMostafeed();
            if (dt.Rows.Count > 0)
            {
                GVExchangeOrdersPrisms.DataSource = dt;
                GVExchangeOrdersPrisms.DataBind();
                lblCountPrisms.Text = Convert.ToString(dt.Rows.Count);
                pnlNullPrisms.Visible = false;
                pnlDataPrisms.Visible = true;
                pnlSelectPrisms.Visible = false;
                txtTitlePrisms.Text = "قائمة الدعم النقدي ( للمستفيد ) ";
                FGetDataMostafedPrisms(XID);
            }
            else
            {
                pnlNullPrisms.Visible = true;
                pnlDataPrisms.Visible = false;
                pnlSelectPrisms.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetDataMostafedPrisms(string XID)
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", XID, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberFilePrisms.Text = XID;
            lblNamePrisms.Text = dt.Rows[0]["NameMostafeed"].ToString();
            lblAlQariahPrisms.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
            lblGenderPrisms.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
            lblPhonePrisms.Text = dt.Rows[0]["PhoneNumber"].ToString();
            lblNumberSigalPrisms.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            lblHalatAlmostafeedPrisms.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
            //Session["XID"] = Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]);
            lblDateBrithDayPrisms.Text = ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["dateBrith"])) + "هـ" + " - " + Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") + "مـ";
            if (dt.Rows[0]["dateBrith"].ToString() != string.Empty)
            {
                lblAgePrisms.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            }
            else
            {
                lblAgePrisms.Text = dt.Rows[0]["Age"].ToString();
            }

            if (Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") == "01/01/1900")
            {
                lblDateBrithDayPrisms.Text = "لم يُضاف";
                lblAgePrisms.Text = "لم يُضاف";
            }
            FGetModerAlGmeiah();
        }
    }

    private void FGetModerAlGmeiah()
    {
        ClassAdmin_Arn.FGetModerAlGmeiah(DLModerAlGmeiahPrisms);
        ClassAdmin_Arn.FGetRaeesMaglesAlEdarah(DLRaeesMaglesAlEdarahPrisms);
        ClassAdmin_Arn.FGetRaeesLagnatAlBahath(DLAmeenAlSondoqPrisms);

        ImgModerPrisms.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiahPrisms.SelectedValue));
        ImgAmeenAlSondoqPrisms.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLAmeenAlSondoqPrisms.SelectedValue));
        ImgRaeesMaglesAlEdarahPrisms.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarahPrisms.SelectedValue));

        lblModerAlGmeiahPrisms.Text = DLModerAlGmeiahPrisms.SelectedItem.ToString();
        lblRaeesMaglesAlEdarahPrisms.Text = DLRaeesMaglesAlEdarahPrisms.SelectedItem.ToString();
        lblAmeenAlSondoqPrisms.Text = DLAmeenAlSondoqPrisms.SelectedItem.ToString();
        DLModerAlGmeiahPrisms.Visible = false;
        DLRaeesMaglesAlEdarahPrisms.Visible = false;
        DLAmeenAlSondoqPrisms.Visible = false;
        lblModerAlGmeiahPrisms.Visible = true;
        lblRaeesMaglesAlEdarahPrisms.Visible = true;
        lblAmeenAlSondoqPrisms.Visible = true;
    }

}