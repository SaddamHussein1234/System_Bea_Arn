﻿using Library_CLS_Arn.ClassOutEntity;
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

public partial class CResearchers_CPVillage_PageBeneficiaryByModer : System.Web.UI.Page
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
            bool A122, A75;
            A122 = Convert.ToBoolean(dtViewProfil.Rows[0]["A122"]);
            A75 = Convert.ToBoolean(dtViewProfil.Rows[0]["A75"]);
            if (A122 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            if (A75 == false)
            {

                btnDelete1.Visible = false;
                GVBeneficiaryAll.Columns[0].Visible = false;
                //GVBeneficiaryAll.Columns[12].Visible = false;
            }
            if (A122 == false && A75 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            FGetMostafeedByModer();
            FGetAlBaheth();
        }
    }

    private void FGetMostafeedByModer()
    {
        DLAlBaheth.Visible = true;
        DLModerAlGmeiah.Visible = true;
        DLRaeesMaglesAlEdarah.Visible = true;
        DLRaeesLagnatAlBahath.Visible = true;
        lblAlBaheth.Visible = false;
        lblModerAlGmeiah.Visible = false;
        lblRaeesMaglesAlEdarah.Visible = false;
        lblRaeesLagnatAlBahath.Visible = false;
        GVBeneficiaryAll.Columns[0].Visible = true;
        GVBeneficiaryAll.Columns[11].Visible = true;
        //GVBeneficiaryAll.Columns[12].Visible = true;
        GVBeneficiaryAll.UseAccessibleHeader = false;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[RasAlEstemarah] Where CONVERT(VARCHAR(30), NumberMostafeed, 111) + NameMostafeed Like '%' + @0 + '%' And IsDelete = @1 And IsAllowModer_ = @1", txtSearch.Text.Trim(), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            GVBeneficiaryAll.DataSource = dt;
            GVBeneficiaryAll.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            txtTitle.Text = " مستفيدين يحتاجون إلى موافقة المدير ";
            txtTitle.Focus();
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
        }
        txtTitle.Focus();
        CheckAccountAdmin();
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageBeneficiaryByModer.aspx");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        lblAlBaheth.Text = DLAlBaheth.SelectedItem.ToString();
        lblModerAlGmeiah.Text = DLModerAlGmeiah.SelectedItem.ToString();
        lblRaeesMaglesAlEdarah.Text = DLRaeesMaglesAlEdarah.SelectedItem.ToString();
        lblRaeesLagnatAlBahath.Text = DLRaeesLagnatAlBahath.SelectedItem.ToString();
        DLAlBaheth.Visible = false;
        DLModerAlGmeiah.Visible = false;
        DLRaeesMaglesAlEdarah.Visible = false;
        DLRaeesLagnatAlBahath.Visible = false;
        lblAlBaheth.Visible = true;
        lblModerAlGmeiah.Visible = true;
        lblRaeesMaglesAlEdarah.Visible = true;
        lblRaeesLagnatAlBahath.Visible = true;

        GVBeneficiaryAll.UseAccessibleHeader = true;
        GVBeneficiaryAll.HeaderRow.TableSection = TableRowSection.TableHeader;

        GVBeneficiaryAll.Columns[0].Visible = false;
        GVBeneficiaryAll.Columns[11].Visible = false;
        //GVBeneficiaryAll.Columns[12].Visible = false;

        Session["footable1"] = pnlData;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('../../Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
    }

    protected void btnAllow_Click(object sender, EventArgs e)
    {
        FAllowModer();
    }

    private void FAllowModer()
    {
        try
        {
            foreach (GridViewRow row in GVBeneficiaryAll.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVBeneficiaryAll.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[RasAlEstemarah] SET [IsAllowModer_] = @IsAllowModer_ WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@IsAllowModer_", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            System.Threading.Thread.Sleep(500);
            Response.Redirect(Request.Url.PathAndQuery);
        }
        catch (Exception)
        {
            return;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetMostafeedByModer();
    }

    int tempcounter = 0;
    protected void GVBeneficiaryAll_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            tempcounter = tempcounter + 1;
            if (tempcounter == 14)
            {
                e.Row.Attributes.Add("style", "page-break-after: always;");
                tempcounter = 0;
            }
        }
    }

    private void FGetAlBaheth()
    {
        //DataTable dt = new DataTable();
        //dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsBaheth = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        //if (dt.Rows.Count > 0)
        //{
        //    //DLAlBaheth.Items.Clear();
        //    //DLAlBaheth.Items.Add("");
        //    //DLAlBaheth.AppendDataBoundItems = true;
        //    DLAlBaheth.DataValueField = "ID_Item";
        //    DLAlBaheth.DataTextField = "FirstName";
        //    DLAlBaheth.DataSource = dt;
        //    DLAlBaheth.DataBind();
        //}
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsModer = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah ", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLModerAlGmeiah.DataValueField = "ID_Item";
            DLModerAlGmeiah.DataTextField = "FirstName";
            DLModerAlGmeiah.DataSource = dt;
            DLModerAlGmeiah.DataBind();
        }
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesMaglisAlEdarah = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesMaglesAlEdarah.DataValueField = "ID_Item";
            DLRaeesMaglesAlEdarah.DataTextField = "FirstName";
            DLRaeesMaglesAlEdarah.DataSource = dt;
            DLRaeesMaglesAlEdarah.DataBind();
        }
        FGetRaeesLagnatAlBahath();
    }

    private void FGetRaeesLagnatAlBahath()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT Top(1000) * FROM [dbo].[tbl_Admin] With(NoLock) Where IsRaeesLgnatAlBath = @0 And IsDelete = @1 Order by IsOrderAdminInEdarah", Convert.ToString(true), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesLagnatAlBahath.DataValueField = "ID_Item";
            DLRaeesLagnatAlBahath.DataTextField = "FirstName";
            DLRaeesLagnatAlBahath.DataSource = dt;
            DLRaeesLagnatAlBahath.DataBind();
        }
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
        ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

    protected void DLModerAlGmeiah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgModer.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLModerAlGmeiah.SelectedValue));
    }

    protected void DLRaeesLagnatAlBahath_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesLagnatAlBahath.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesLagnatAlBahath.SelectedValue));
    }

    protected void DLRaeesMaglesAlEdarah_SelectedIndexChanged(object sender, EventArgs e)
    {
        ImgRaeesMaglesAlEdarah.ImageUrl = ClassSaddam.FGetSignature(Convert.ToInt32(DLRaeesMaglesAlEdarah.SelectedValue));
    }

}