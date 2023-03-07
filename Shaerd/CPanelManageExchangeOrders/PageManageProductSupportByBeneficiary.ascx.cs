using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Shaerd_CPanelManageExchangeOrders_PageManageProductSupportByBeneficiary : System.Web.UI.UserControl
{
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
            bool IsBaheth_, A60;
            IsBaheth_ = Convert.ToBoolean(dtViewProfil.Rows[0]["IsBaheth"]);
            A60 = Convert.ToBoolean(dtViewProfil.Rows[0]["A60"]);
            if (A60 == false)
                Response.Redirect("PageNotAccess.aspx");
            //if (IsBaheth_ == false)
                FGetName();
            //else if (IsBaheth_)
            //    FGetNameByBaheth();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
            pnlSelectTarmem.Visible = true;
            pnlSelectPrisms.Visible = true;
            txtDateFrom.Text = ClassSaddam.FGetDateFrom();
            txtDateTo.Text = ClassSaddam.FGetDateTo();
            txtDateFromTarmem.Text = txtDateFrom.Text;
            txtDateToTarmem.Text = txtDateTo.Text;
            txtDateFromPrisms.Text = txtDateFrom.Text;
            txtDateToPrisms.Text = txtDateTo.Text;

            if (Request.QueryString["XID"] != null)
            {
                DLMostafeed.SelectedValue = Request.QueryString["XID"];
                DLCategory.SelectedValue = Request.QueryString["XIDCate"];
                txtDateFrom.Text = Request.QueryString["XIDFrom"];
                txtDateTo.Text = Request.QueryString["XIDTo"];
                RBTathith.Checked = true;

                Panel1.Visible = false;
                IDTathith.Visible = true;
                IDTarmem.Visible = false;
                IDPrisms.Visible = false;
                FArnProductShopBySupportByBeneficiary();
            }
            else if (Request.QueryString["TIDX"] != null)
            {
                DLMostafeedTarmem.SelectedValue = Request.QueryString["TIDX"];
                DLCategoryTarmem.SelectedValue = Request.QueryString["XIDCate"];
                txtDateFromTarmem.Text = Request.QueryString["XIDFrom"];
                txtDateToTarmem.Text = Request.QueryString["XIDTo"];
                RPTarmem.Checked = true;
                Panel1.Visible = false;
                IDTathith.Visible = false;
                IDTarmem.Visible = true;
                IDPrisms.Visible = false;
                FArnProductShopBySupportByBeneficiaryTarmem();
            }
            else if (Request.QueryString["SIDX"] != null)
            {
                DLMostafeedPrisms.SelectedValue = Request.QueryString["SIDX"];
                DLCategoryPrisms.SelectedValue = Request.QueryString["XIDCate"];
                txtDateFromPrisms.Text = Request.QueryString["XIDFrom"];
                txtDateToPrisms.Text = Request.QueryString["XIDTo"];
                RPSupportForPrisms.Checked = true;
                Panel1.Visible = false;
                IDTathith.Visible = false;
                IDTarmem.Visible = false;
                IDPrisms.Visible = true;
                FArnProductShopBySupportByBeneficiaryPrisms();
            }
            else
            {
                return;
            }
        }
    }

    private void FGetName()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT [IDItem],[NumberMostafeed],[NameMostafeed] FROM [dbo].[RasAlEstemarah] Where IsDelete = @0 Order By NameMostafeed", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLMostafeed.Items.Clear();
            DLMostafeed.Items.Add("");
            DLMostafeed.AppendDataBoundItems = true;
            DLMostafeed.DataValueField = "NumberMostafeed";
            DLMostafeed.DataTextField = "NameMostafeed";
            DLMostafeed.DataSource = dt;
            DLMostafeed.DataBind();

            DLMostafeedTarmem.Items.Clear();
            DLMostafeedTarmem.Items.Add("");
            DLMostafeedTarmem.AppendDataBoundItems = true;
            DLMostafeedTarmem.DataValueField = "NumberMostafeed";
            DLMostafeedTarmem.DataTextField = "NameMostafeed";
            DLMostafeedTarmem.DataSource = dt;
            DLMostafeedTarmem.DataBind();

            DLMostafeedPrisms.Items.Clear();
            DLMostafeedPrisms.Items.Add("");
            DLMostafeedPrisms.AppendDataBoundItems = true;
            DLMostafeedPrisms.DataValueField = "NumberMostafeed";
            DLMostafeedPrisms.DataTextField = "NameMostafeed";
            DLMostafeedPrisms.DataSource = dt;
            DLMostafeedPrisms.DataBind();
        }
        FGetCategoryShop();
    }

    private void FGetNameByBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM RasAlEstemarah , tbl_MultiQariah With(NoLock) WHERE IDAdminJoin = @0 And RasAlEstemarah.IsDelete = @1 And tbl_MultiQariah.IsDelete = @1 And (RasAlEstemarah.AlQaryah = tbl_MultiQariah.IDQariah)  Order by RasAlEstemarah.NameMostafeed ", IDUser, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLMostafeed.Items.Clear();
            DLMostafeed.Items.Add("");
            DLMostafeed.AppendDataBoundItems = true;
            DLMostafeed.DataValueField = "NumberMostafeed";
            DLMostafeed.DataTextField = "NameMostafeed";
            DLMostafeed.DataSource = dt;
            DLMostafeed.DataBind();

            DLMostafeedTarmem.Items.Clear();
            DLMostafeedTarmem.Items.Add("");
            DLMostafeedTarmem.AppendDataBoundItems = true;
            DLMostafeedTarmem.DataValueField = "NumberMostafeed";
            DLMostafeedTarmem.DataTextField = "NameMostafeed";
            DLMostafeedTarmem.DataSource = dt;
            DLMostafeedTarmem.DataBind();

            DLMostafeedPrisms.Items.Clear();
            DLMostafeedPrisms.Items.Add("");
            DLMostafeedPrisms.AppendDataBoundItems = true;
            DLMostafeedPrisms.DataValueField = "NumberMostafeed";
            DLMostafeedPrisms.DataTextField = "NameMostafeed";
            DLMostafeedPrisms.DataSource = dt;
            DLMostafeedPrisms.DataBind();
        }
        FGetCategoryShop();
    }

    private void FGetCategoryShop()
    {
        ClassQuaem.FGetSupportType(1, "'1','2','3','6'", DLCategory);
        FGetCategoryShopTarmem();
    }

    private void FGetCategoryShopTarmem()
    {
        ClassQuaem.FGetSupportType(1, "'4'", DLCategoryTarmem);
        FGetCategoryShopPrisms();
    }

    private void FGetCategoryShopPrisms()
    {
        ClassQuaem.FGetSupportType(1, "'5'", DLCategoryPrisms);
    }

    protected void LBR_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductSupportByBeneficiary.aspx");
    }

    protected void RBTathith_CheckedChanged(object sender, EventArgs e)
    {
        if (RBTathith.Checked)
        {
            Panel1.Visible = false;
            IDTathith.Visible = true;
            IDTarmem.Visible = false;
            IDPrisms.Visible = false;
        }
    }

    protected void RPTarmem_CheckedChanged(object sender, EventArgs e)
    {
        if (RPTarmem.Checked)
        {
            Panel1.Visible = false;
            IDTathith.Visible = false;
            IDTarmem.Visible = true;
            IDPrisms.Visible = false;
        }
    }

    protected void RPSupportForPrisms_CheckedChanged(object sender, EventArgs e)
    {
        if (RPSupportForPrisms.Checked)
        {
            Panel1.Visible = false;
            IDTathith.Visible = false;
            IDTarmem.Visible = false;
            IDPrisms.Visible = true;
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageManageProductSupportByBeneficiary.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVExchangeOrders.Columns[0].Visible = false;
            GVExchangeOrders.Columns[10].Visible = false;

            GVExchangeOrders.UseAccessibleHeader = true;
            GVExchangeOrders.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GVExchangeOrders.UseAccessibleHeader = false;
        GVExchangeOrders.Columns[10].Visible = true;
        if (DLType.Text != string.Empty)
        {
            lblType.Visible = false;
            if (DLMostafeed.Text != string.Empty)
            {
                lblMostafeed.Visible = false;
                if (DLCategory.Text != string.Empty)
                {
                    lblCategory.Visible = false;
                    if (txtDateFrom.Text != string.Empty)
                    {
                        lblDateFrom.Visible = false;
                        if (txtDateTo.Text != string.Empty)
                        {
                            // Write Code Hear
                            FArnProductShopBySupportByBeneficiary();
                            System.Threading.Thread.Sleep(500);
                        }
                        else if (txtDateTo.Text == string.Empty)
                            lblDateTo.Visible = true;
                    }
                    else if (txtDateFrom.Text == string.Empty)
                        lblDateFrom.Visible = true;
                }
                else if (DLCategory.Text == string.Empty)
                    lblCategory.Visible = true;
            }
            else if (DLMostafeed.Text == string.Empty)
                lblMostafeed.Visible = true;
        }
        else if (DLType.Text == string.Empty)
            lblType.Visible = true;
    }

    private void FArnProductShopBySupportByBeneficiary()
    {
        try
        {
            ClassProductShopWarehouse CPS = new ClassProductShopWarehouse();
            CPS.billNumber = Convert.ToInt32(0);
            CPS.IDMosTafeed = Convert.ToInt32(0);
            CPS.IDMosTafeed2 = Convert.ToInt32(DLMostafeed.SelectedValue);
            CPS.IDType = DLType.SelectedValue;
            CPS.IDCategory = Convert.ToInt32(DLCategory.SelectedValue);
            CPS.DateFrom = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
            CPS.DateTo = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
            CPS.IsDelete = false;
            DataTable dt = new DataTable();
            dt = CPS.BArnProductShopBySupportByBeneficiary();
            if (dt.Rows.Count > 0)
            {
                GVExchangeOrders.DataSource = dt;
                GVExchangeOrders.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
                pnlSelect.Visible = false;
                txtTitle.Text = "قائمة دعم المستفيد " + DLMostafeed.SelectedItem.ToString() + " من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim() + " لمشروع " + DLCategory.SelectedItem.ToString();
                FGetDataMostafed();
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
                pnlSelect.Visible = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FArnProductShopBySupportByBeneficiaryTarmem()
    {
        try
        {
            ClassBenaaAndTarmim CBAT = new ClassBenaaAndTarmim();
            CBAT._NumberMostafeed = Convert.ToInt32(DLMostafeedTarmem.SelectedValue);
            CBAT._ID_Type = Convert.ToInt32(DLCategoryTarmem.SelectedValue);
            CBAT._Date_From = Convert.ToDateTime(txtDateFromTarmem.Text.Trim()).ToString("yyyy/MM/dd");
            CBAT._Date_To = Convert.ToDateTime(txtDateToTarmem.Text.Trim()).ToString("yyyy/MM/dd");
            CBAT._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CBAT.BArnProductShopBySupportByBeneficiaryHouse();
            if (dt.Rows.Count > 0)
            {
                GVExchangeOrdersTarmem.DataSource = dt;
                GVExchangeOrdersTarmem.DataBind();
                lblCountTarmem.Text = Convert.ToString(dt.Rows.Count);
                pnlNullTarmem.Visible = false;
                pnlDataTarmem.Visible = true;
                pnlSelectTarmem.Visible = false;
                txtTitleTarmem.Text = "قائمة دعم المستفيد " + DLMostafeedTarmem.SelectedItem.ToString() + " من تاريخ " + txtDateFromTarmem.Text.Trim() + " إلى تاريخ " + txtDateToTarmem.Text.Trim() + " لمشروع " + DLCategoryTarmem.SelectedItem.ToString();
                FGetDataMostafedTarmem();
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

    private void FGetDataMostafed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", DLMostafeed.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberFile.Text = DLMostafeed.SelectedValue;
            lblName.Text = dt.Rows[0]["NameMostafeed"].ToString();
            lblAlQariah.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
            lblGender.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
            lblPhone.Text = dt.Rows[0]["PhoneNumber"].ToString();
            lblNumberSigal.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            lblHalatAlmostafeed.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
            Session["XID"] = Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]);
            lblDateBrithDay.Text = ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["dateBrith"])) + "هـ" + " - " + Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") + "مـ";
            if (dt.Rows[0]["dateBrith"].ToString() != string.Empty)
                lblAge.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            else
                lblAge.Text = dt.Rows[0]["Age"].ToString();

            if (Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") == "01/01/1900")
            {
                lblDateBrithDay.Text = "لم يُضاف";
                lblAge.Text = "لم يُضاف";
            }
        }
    }

    private void FGetDataMostafedTarmem()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1) * FROM [dbo].[RasAlEstemarah] With(NoLock) Where NumberMostafeed = @0 And IsDelete = @1", DLMostafeedTarmem.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberFileTarmem.Text = DLMostafeedTarmem.SelectedValue;
            lblNameTarmem.Text = dt.Rows[0]["NameMostafeed"].ToString();
            lblAlQariahTarmem.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
            lblGenderTarmem.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
            lblPhoneTarmem.Text = dt.Rows[0]["PhoneNumber"].ToString();
            lblNumberSigalTarmem.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            lblHalatAlmostafeedTarmem.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
            Session["XID"] = Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]);
            lblDateBrithDayTarmem.Text = ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["dateBrith"])) + "هـ" + " - " + Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") + "مـ";
            if (dt.Rows[0]["dateBrith"].ToString() != string.Empty)
                lblAgeTarmem.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            else
                lblAgeTarmem.Text = dt.Rows[0]["Age"].ToString();

            if (Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") == "01/01/1900")
            {
                lblDateBrithDayTarmem.Text = "لم يُضاف";
                lblAgeTarmem.Text = "لم يُضاف";
            }
        }
    }

    int Cou = 0;
    decimal sum = 0;
    int tempcounter = 0;
    protected void GVExchangeOrders_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Count = (Label)e.Row.FindControl("lblCount");//take lable id
                Cou += int.Parse(Count.Text);
                lblSum.Text = Cou.ToString();

                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sum += decimal.Parse(salary.Text);
                if (sum != 0)
                    lblTotalPrice.Text = sum.ToString();
                else
                    lblTotalPrice.Text = "بإنتظار التسعير";

                tempcounter = tempcounter + 1;
                if (tempcounter == 14)
                {
                    e.Row.Attributes.Add("style", "page-break-after: always;");
                    tempcounter = 0;
                }
            }
        }
        catch (Exception)
        {

        }
    }

    public string FGetProject()
    {
        string XResult = DLCategory.SelectedItem.ToString();
        return XResult;
    }

    public string FGetProjectTarmem()
    {
        string XResult = DLCategoryTarmem.SelectedItem.ToString();
        return XResult;
    }

    public string FGetProjectPrisms()
    {
        string XResult = DLCategoryPrisms.SelectedItem.ToString();
        return XResult;
    }

    protected void LBRefreshTarmem_Click(object sender, EventArgs e)
    {
        FGetByTarmem();
    }

    protected void LBPrintTarmem_Click(object sender, EventArgs e)
    {
        try
        {
            GVExchangeOrdersTarmem.Columns[0].Visible = false;
            GVExchangeOrdersTarmem.Columns[10].Visible = false;

            GVExchangeOrdersTarmem.UseAccessibleHeader = true;
            GVExchangeOrdersTarmem.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlDataTarmem;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnDeleteTarmem_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVExchangeOrdersTarmem.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVExchangeOrdersTarmem.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[BenaaAndTarmim] SET [IsDelete] = @IsDelete WHERE NumberMostafeed = @NumberMostafeed And billNumber_ = @billNumber And ID_Type = @ID_Type";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@NumberMostafeed", Convert.ToInt32(DLMostafeedTarmem.SelectedValue));
                    cmd.Parameters.AddWithValue("@billNumber", Comp_ID);
                    cmd.Parameters.AddWithValue("@ID_Type", Convert.ToInt64(DLCategoryTarmem.SelectedValue));
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FGetByTarmem();
        }
        catch (Exception)
        {
            return;
        }
    }

    private void FGetByTarmem()
    {
        GVExchangeOrdersTarmem.UseAccessibleHeader = false;
        GVExchangeOrdersTarmem.Columns[0].Visible = true;
        GVExchangeOrdersTarmem.Columns[10].Visible = true;
        if (DLTypeTarmem.Text != string.Empty)
        {
            lblTypeTarmem.Visible = false;
            if (DLMostafeedTarmem.Text != string.Empty)
            {
                lblMostafeedTarmem.Visible = false;
                if (DLCategoryTarmem.Text != string.Empty)
                {
                    lblCategoryTarmem.Visible = false;
                    if (txtDateFromTarmem.Text != string.Empty)
                    {
                        lblDateFromTarmem.Visible = false;
                        if (txtDateToTarmem.Text != string.Empty)
                        {
                            // Write Code Hear
                            FArnProductShopBySupportByBeneficiaryTarmem();
                            System.Threading.Thread.Sleep(500);
                        }
                        else if (txtDateToTarmem.Text == string.Empty)
                            lblDateToTarmem.Visible = true;
                    }
                    else if (txtDateFromTarmem.Text == string.Empty)
                        lblDateFromTarmem.Visible = true;
                }
                else if (DLCategoryTarmem.Text == string.Empty)
                    lblCategoryTarmem.Visible = true;
            }
            else if (DLMostafeedTarmem.Text == string.Empty)
                lblMostafeedTarmem.Visible = true;
        }
        else if (DLTypeTarmem.Text == string.Empty)
            lblTypeTarmem.Visible = true;
    }

    protected void btnSearchTarmem_Click(object sender, EventArgs e)
    {
        FGetByTarmem();
    }

    protected void GVExchangeOrdersTarmem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sum += decimal.Parse(salary.Text);
                if (sum != 0)
                    lblTotalPriceTarmem.Text = sum.ToString();
                else
                    lblTotalPriceTarmem.Text = "بإنتظار التسعير";

                tempcounter = tempcounter + 1;
                if (tempcounter == 14)
                {
                    e.Row.Attributes.Add("style", "page-break-after: always;");
                    tempcounter = 0;
                }
            }
        }
        catch (Exception)
        {

        }
    }

    protected void LBRefreshPrisms_Click(object sender, EventArgs e)
    {
        FGetDatabyPrisms();
    }

    protected void LBPrintPrisms_Click(object sender, EventArgs e)
    {
        try
        {
            GVExchangeOrdersPrisms.Columns[0].Visible = false;
            GVExchangeOrdersPrisms.Columns[10].Visible = false;

            GVExchangeOrdersPrisms.UseAccessibleHeader = true;
            GVExchangeOrdersPrisms.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlDataPrisms;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/CPanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnDeletePrisms_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVExchangeOrdersPrisms.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVExchangeOrdersPrisms.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_SupportForPrisms] SET [IsDelete] = @IsDelete WHERE NumberMostafeed = @NumberMostafeed And billNumber_ = @billNumber And ID_Type = @ID_Type";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@NumberMostafeed", Convert.ToInt32(DLMostafeedPrisms.SelectedValue));
                    cmd.Parameters.AddWithValue("@billNumber", Comp_ID);
                    cmd.Parameters.AddWithValue("@ID_Type", Convert.ToInt64(DLCategoryPrisms.SelectedValue));
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FGetDatabyPrisms();
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearchPrisms_Click(object sender, EventArgs e)
    {
        FGetDatabyPrisms();
    }

    private void FGetDatabyPrisms()
    {
        GVExchangeOrdersPrisms.UseAccessibleHeader = false;
        GVExchangeOrdersPrisms.Columns[0].Visible = true;
        GVExchangeOrdersPrisms.Columns[10].Visible = true;
        if (DLTypePrisms.Text != string.Empty)
        {
            lblTypePrisms.Visible = false;
            if (DLMostafeedPrisms.Text != string.Empty)
            {
                lblMostafeedPrisms.Visible = false;
                if (DLCategoryPrisms.Text != string.Empty)
                {
                    lblCategoryPrisms.Visible = false;
                    if (txtDateFromPrisms.Text != string.Empty)
                    {
                        lblDateFromPrisms.Visible = false;
                        if (txtDateToPrisms.Text != string.Empty)
                        {
                            // Write Code Hear
                            FArnProductShopBySupportByBeneficiaryPrisms();
                            System.Threading.Thread.Sleep(500);
                        }
                        else if (txtDateToPrisms.Text == string.Empty)
                            lblDateToPrisms.Visible = true;
                    }
                    else if (txtDateFromPrisms.Text == string.Empty)
                        lblDateFromPrisms.Visible = true;
                }
                else if (DLCategoryPrisms.Text == string.Empty)
                    lblCategoryPrisms.Visible = true;
            }
            else if (DLMostafeedPrisms.Text == string.Empty)
                lblMostafeedPrisms.Visible = true;
        }
        else if (DLTypePrisms.Text == string.Empty)
            lblTypePrisms.Visible = true;
    }

    private void FArnProductShopBySupportByBeneficiaryPrisms()
    {
        try
        {
            ClassSupportForPrisms CSFP = new ClassSupportForPrisms();
            CSFP._NumberMostafeed = Convert.ToInt32(DLMostafeedPrisms.SelectedValue);
            CSFP._ID_Type = Convert.ToInt32(DLCategoryPrisms.SelectedValue);
            CSFP._Date_From = Convert.ToDateTime(txtDateFromPrisms.Text.Trim()).ToString("yyyy/MM/dd");
            CSFP._Date_To = Convert.ToDateTime(txtDateToPrisms.Text.Trim()).ToString("yyyy/MM/dd");
            CSFP._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CSFP.BArnSupportForPrismsByBeneficiary();
            if (dt.Rows.Count > 0)
            {
                GVExchangeOrdersPrisms.DataSource = dt;
                GVExchangeOrdersPrisms.DataBind();
                lblCountPrisms.Text = Convert.ToString(dt.Rows.Count);
                pnlNullPrisms.Visible = false;
                pnlDataPrisms.Visible = true;
                pnlSelectPrisms.Visible = false;
                txtTitlePrisms.Text = "قائمة دعم المستفيد " + DLMostafeedPrisms.SelectedItem.ToString() + " من تاريخ " + txtDateFromPrisms.Text.Trim() + " إلى تاريخ " + txtDateToPrisms.Text.Trim() + " لمشروع " + DLCategoryPrisms.SelectedItem.ToString();
                FGetDataMostafedPrisms();
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

    private void FGetDataMostafedPrisms()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[RasAlEstemarah] Where NumberMostafeed = @0 And IsDelete = @1", DLMostafeedPrisms.SelectedValue, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            lblNumberFilePrisms.Text = DLMostafeedTarmem.SelectedValue;
            lblNamePrisms.Text = dt.Rows[0]["NameMostafeed"].ToString();
            lblAlQariahPrisms.Text = ClassQuaem.FAlQarabah(Convert.ToInt32(dt.Rows[0]["AlQaryah"]));
            lblGenderPrisms.Text = ClassQuaem.FGender(Convert.ToInt32(dt.Rows[0]["Gender"]));
            lblPhonePrisms.Text = dt.Rows[0]["PhoneNumber"].ToString();
            lblNumberSigalPrisms.Text = dt.Rows[0]["NumberAlSegelAlMadany"].ToString();
            lblHalatAlmostafeedPrisms.Text = ClassQuaem.FHalatMostafeed(Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]));
            Session["XID"] = Convert.ToInt32(dt.Rows[0]["HalafAlMosTafeed"]);
            lblDateBrithDayPrisms.Text = ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["dateBrith"])) + "هـ" + " - " + Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") + "مـ";
            if (dt.Rows[0]["dateBrith"].ToString() != string.Empty)
                lblAgePrisms.Text = ClassSaddam.FGetAge(Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("yyyy"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("MM"), Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd"));
            else
                lblAgePrisms.Text = dt.Rows[0]["Age"].ToString();

            if (Convert.ToDateTime(dt.Rows[0]["dateBrith"]).ToString("dd/MM/yyyy") == "01/01/1900")
            {
                lblDateBrithDayPrisms.Text = "لم يُضاف";
                lblAgePrisms.Text = "لم يُضاف";
            }
        }
    }

    protected void GVExchangeOrdersPrisms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label salary = (Label)e.Row.FindControl("lblCountTotalPrice");//take lable id
                sum += decimal.Parse(salary.Text);
                if (sum != 0)
                    lblTotalPricePrisms.Text = sum.ToString();
                else
                    lblTotalPricePrisms.Text = "بإنتظار التسعير";

                tempcounter = tempcounter + 1;
                if (tempcounter == 14)
                {
                    e.Row.Attributes.Add("style", "page-break-after: always;");
                    tempcounter = 0;
                }
            }
        }
        catch (Exception)
        {

        }
    }
    
}