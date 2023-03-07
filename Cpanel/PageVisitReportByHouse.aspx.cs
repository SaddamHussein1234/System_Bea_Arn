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

public partial class Cpanel_PageVisitReportByHouse : System.Web.UI.Page
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
            bool A50;
            A50 = Convert.ToBoolean(dtViewProfil.Rows[0]["A50"]);
            if (A50 == false)
                Response.Redirect("PageNotAccess.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            pnlSelect.Visible = true;
            FGetAlQariah();
            txtDateFrom.Text = "24-12-2014";
            txtDateTo.Text = ClassDataAccess.GetCurrentTime().ToString("dd-MM-yyyy");
        }
    }

    private void FGetAlQariah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where AlQriah <> @0 And IsDelete = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAlQriah.Items.Clear();
            DLAlQriah.Items.Add("");
            DLAlQriah.AppendDataBoundItems = true;
            DLAlQriah.DataValueField = "IDItem";
            DLAlQriah.DataTextField = "AlQriah";
            DLAlQriah.DataSource = dt;
            DLAlQriah.DataBind();
        }
        FGetHalafAlMosTafeed();
    }

    private void FGetHalafAlMosTafeed()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[Quaem] With(NoLock) Where HalatMostafeed <> @0 And IsDelete = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLHalafAlMosTafeed.Items.Clear();
            DLHalafAlMosTafeed.Items.Add("");
            DLHalafAlMosTafeed.AppendDataBoundItems = true;
            DLHalafAlMosTafeed.DataValueField = "IDItem";
            DLHalafAlMosTafeed.DataTextField = "HalatMostafeed";
            DLHalafAlMosTafeed.DataSource = dt;
            DLHalafAlMosTafeed.DataBind();
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageVisitReportByHouse.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            GVVisitReport.Columns[0].Visible = false;
            GVVisitReport.Columns[14].Visible = false;

            GVVisitReport.UseAccessibleHeader = true;
            GVVisitReport.HeaderRow.TableSection = TableRowSection.TableHeader;

            Session["footable1"] = pnlData;
            //if (GVVisitReport.Rows.Count > 14)
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('PrintFootable2.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
            //}
            //else if (GVVisitReport.Rows.Count <= 14)
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
        GVVisitReport.Columns[0].Visible = true;
        GVVisitReport.Columns[14].Visible = true;
        GVVisitReport.UseAccessibleHeader = false;
        FCheckSelect();
    }

    private void FCheckSelect()
    {
        try
        {
            if ((DLAlQriah.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty && CBBena.Checked == false && CBTarmem.Checked == false && CBTathith.Checked == false) || (DLAlQriah.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty && CBBena.Checked == true && CBTarmem.Checked == true && CBTathith.Checked == true))
            {
                txtTitle.Text = "قائمة الإحتياجات لبناء , ترميم , تأثيث منازل " + " حتى تاريخ " + txtDateTo.Text.Trim();
                lblTitle.Text = "بناء و ترميم و تأثيث منازل";
                IDBenaa.Visible = true; IDTarmem.Visible = true; IDTathith.Visible = true;
                GVVisitReport.Columns[7].Visible = true;
                GVVisitReport.Columns[8].Visible = true;
                GVVisitReport.Columns[9].Visible = true;
                FArnReportAlZyaratByAll(0);
            }
            else if (DLAlQriah.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty && CBTathith.Checked == true && CBBena.Checked == false && CBTarmem.Checked == false)
            {
                txtTitle.Text = "قائمة الإحتياجات لتأثيث منازل " + " حتى تاريخ " + txtDateTo.Text.Trim();
                lblTitle.Text = "تأثيث منازل";
                IDBenaa.Visible = false; IDTarmem.Visible = false; IDTathith.Visible = true;
                GVVisitReport.Columns[7].Visible = false;
                GVVisitReport.Columns[8].Visible = false;
                GVVisitReport.Columns[9].Visible = true;
                FArnReportAlZyaratByAll(1);
            }
            else if (DLAlQriah.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty && CBTathith.Checked == false && CBBena.Checked == true && CBTarmem.Checked == false)
            {
                txtTitle.Text = "قائمة الإحتياجات لبناء منازل " + " حتى تاريخ " + txtDateTo.Text.Trim();
                lblTitle.Text = "بناء منازل";
                IDBenaa.Visible = true; IDTarmem.Visible = false; IDTathith.Visible = false;
                GVVisitReport.Columns[7].Visible = true;
                GVVisitReport.Columns[8].Visible = false;
                GVVisitReport.Columns[9].Visible = false;
                FArnReportAlZyaratByAll(2);
            }
            else if (DLAlQriah.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty && CBTathith.Checked == false && CBBena.Checked == false && CBTarmem.Checked == true)
            {
                txtTitle.Text = "قائمة الإحتياجات لترميم منازل " + " حتى تاريخ " + txtDateTo.Text.Trim();
                lblTitle.Text = "ترميم منازل";
                IDBenaa.Visible = false; IDTarmem.Visible = true; IDTathith.Visible = false;
                GVVisitReport.Columns[7].Visible = false;
                GVVisitReport.Columns[8].Visible = true;
                GVVisitReport.Columns[9].Visible = false;
                FArnReportAlZyaratByAll(3);
            }
            else if (DLAlQriah.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty && CBTathith.Checked == true && CBBena.Checked == true && CBTarmem.Checked == false)
            {
                txtTitle.Text = "قائمة الإحتياجات لتأثيث وبناء منازل " + " حتى تاريخ " + txtDateTo.Text.Trim();
                lblTitle.Text = "تأثيث وبناء منازل";
                IDBenaa.Visible = true; IDTarmem.Visible = false; IDTathith.Visible = true;
                GVVisitReport.Columns[7].Visible = true;
                GVVisitReport.Columns[8].Visible = false;
                GVVisitReport.Columns[9].Visible = true;
                FArnReportAlZyaratByAll(4);
            }
            else if (DLAlQriah.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty && CBTathith.Checked == true && CBBena.Checked == false && CBTarmem.Checked == true)
            {
                txtTitle.Text = "قائمة الإحتياجات لتأثيث وترميم منازل " + " حتى تاريخ " + txtDateTo.Text.Trim();
                lblTitle.Text = "تأثيث وترميم منازل";
                IDBenaa.Visible = false; IDTarmem.Visible = true; IDTathith.Visible = true;
                GVVisitReport.Columns[7].Visible = false;
                GVVisitReport.Columns[8].Visible = true;
                GVVisitReport.Columns[9].Visible = true;
                FArnReportAlZyaratByAll(5);
            }
            else if (DLAlQriah.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty && CBTathith.Checked == false && CBBena.Checked == true && CBTarmem.Checked == true)
            {
                txtTitle.Text = "قائمة الإحتياجات لبناء وترميم منازل " + " حتى تاريخ " + txtDateTo.Text.Trim();
                lblTitle.Text = "بناء وترميم منازل";
                IDBenaa.Visible = true; IDTarmem.Visible = true; IDTathith.Visible = false;
                GVVisitReport.Columns[7].Visible = true;
                GVVisitReport.Columns[8].Visible = true;
                GVVisitReport.Columns[9].Visible = false;
                FArnReportAlZyaratByAll(6);
            }
            else if (DLAlQriah.SelectedValue != string.Empty && DLHalafAlMosTafeed.SelectedValue == string.Empty)
            {
                FArnReportAlZyaratByQariah();
            }
            else if (DLAlQriah.SelectedValue == string.Empty && DLHalafAlMosTafeed.SelectedValue != string.Empty)
            {
                FArnReportAlZyaratByHalafAlMosTafeed();
            }
            else if (DLAlQriah.SelectedValue != string.Empty && DLHalafAlMosTafeed.SelectedValue != string.Empty)
            {
                FArnReportAlZyaratByQariahAndHalafAlMosTafeed();
            }
            System.Threading.Thread.Sleep(500);
        }
        catch (Exception)
        {

        }
    }

    private void FArnReportAlZyaratByAll(int IDCheck)
    {
        ClassReportAlZyarat CRA = new ClassReportAlZyarat();
        CRA.IDCheck = IDCheck.ToString();
        CRA._IDUniq = txtNameMostafeed.Text.Trim();
        CRA._IsDelete = false;
        CRA._A1 = DLPercint.SelectedValue;
        CRA._FromDate = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
        CRA._ToDate = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
        CRA._Null = 0;
        CRA._IsAllow = true;
        DataTable dt = new DataTable();
        dt = CRA.BArnReportAlZyaratByDate();
        if (dt.Rows.Count > 0)
        {
            GVVisitReport.DataSource = dt;
            GVVisitReport.DataBind();
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

    private void FArnReportAlZyaratByQariah()
    {
        ClassReportAlZyarat CRA = new ClassReportAlZyarat();
        CRA._AlQaryah = Convert.ToInt32(DLAlQriah.SelectedValue);
        CRA._IDUniq = txtNameMostafeed.Text.Trim();
        CRA._IsDelete = false;
        CRA._A1 = DLPercint.SelectedValue;
        CRA._FromDate = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
        CRA._ToDate = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
        CRA._Null = 0;
        CRA._IsAllow = true;
        DataTable dt = new DataTable();
        dt = CRA.BArnReportAlZyaratByQariah();
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "قائمة إحتياجات قرية " + DLAlQriah.SelectedItem.ToString() + " من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
            GVVisitReport.DataSource = dt;
            GVVisitReport.DataBind();
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

    private void FArnReportAlZyaratByHalafAlMosTafeed()
    {
        ClassReportAlZyarat CRA = new ClassReportAlZyarat();
        CRA._HalafAlMosTafeed = Convert.ToInt32(DLHalafAlMosTafeed.SelectedValue);
        CRA._IDUniq = txtNameMostafeed.Text.Trim();
        CRA._IsDelete = false;
        CRA._A1 = DLPercint.SelectedValue;
        CRA._FromDate = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
        CRA._ToDate = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
        CRA._Null = 0;
        CRA._IsAllow = true;
        DataTable dt = new DataTable();
        dt = CRA.BArnReportAlZyaratByHalafAlMosTafeed();
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "قائمة إحتياجات نوع حالة " + DLHalafAlMosTafeed.SelectedItem.ToString() + " من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
            GVVisitReport.DataSource = dt;
            GVVisitReport.DataBind();
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

    private void FArnReportAlZyaratByQariahAndHalafAlMosTafeed()
    {
        ClassReportAlZyarat CRA = new ClassReportAlZyarat();
        CRA._AlQaryah = Convert.ToInt32(DLAlQriah.SelectedValue);
        CRA._HalafAlMosTafeed = Convert.ToInt32(DLHalafAlMosTafeed.SelectedValue);
        CRA._IDUniq = txtNameMostafeed.Text.Trim();
        CRA._IsDelete = false;
        CRA._A1 = DLPercint.SelectedValue;
        CRA._FromDate = Convert.ToDateTime(txtDateFrom.Text.Trim()).ToString("yyyy/MM/dd");
        CRA._ToDate = Convert.ToDateTime(txtDateTo.Text.Trim()).ToString("yyyy/MM/dd");
        CRA._Null = 0;
        CRA._IsAllow = true;
        DataTable dt = new DataTable();
        dt = CRA.BArnReportAlZyaratByQariahAndHalafAlMosTafeed();
        if (dt.Rows.Count > 0)
        {
            txtTitle.Text = "قائمة إحتياجات قرية " + DLAlQriah.SelectedItem.ToString() + " حسب نوع حالة " + DLHalafAlMosTafeed.SelectedItem.ToString() + " من تاريخ " + txtDateFrom.Text.Trim() + " إلى تاريخ " + txtDateTo.Text.Trim();
            GVVisitReport.DataSource = dt;
            GVVisitReport.DataBind();
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

    int CouCountBenaa, CouTarmemManzil, CouTathithManzil = 0;
    protected void GVVisitReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label CountBenaa = (Label)e.Row.FindControl("lblBenaManzil");//take lable id
            CouCountBenaa += int.Parse(CountBenaa.Text);
            lblCountBenaa.Text = CouCountBenaa.ToString();

            Label CountTarmem = (Label)e.Row.FindControl("lblTarmemManzil");//take lable id
            CouTarmemManzil += int.Parse(CountTarmem.Text);
            lblCountTarmem.Text = CouTarmemManzil.ToString();

            Label CountTathitrh = (Label)e.Row.FindControl("lblTathithManzil");//take lable id
            CouTathithManzil += int.Parse(CountTathitrh.Text);
            lblCountTathith.Text = CouTathithManzil.ToString();
        }
    }
    
    protected void btnDelete1_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVVisitReport.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVVisitReport.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[ReportAlZyarat] SET [IsDelete] = @IsDelete WHERE NumberMostafeed = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsDelete", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FCheckSelect();
        }
        catch (Exception)
        {
            return;
        }
    }

}