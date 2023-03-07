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

public partial class Cpanel_PageBeneficiary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FGetMostafeedAll();
            txtTitle.Focus();
            FGetAlBaheth();
            lblDate.Text = ClassDataAccess.GetCurrentTime().ToString("dd/MM/yyyy HH:mm:ss ttt");
        }
    }

    private void FGetMostafeedAll()
    {
        try
        {
            ClassMosTafeed CMF = new ClassMosTafeed();
            CMF._NameMostafeed = txtSearch.Text.Trim();
            CMF._IsDelete = false;
            DataTable dt = new DataTable();
            dt = CMF.BArnRasAlEstemarahGetAll();
            if (dt.Rows.Count > 0)
            {
                GVBeneficiaryAll.DataSource = dt;
                GVBeneficiaryAll.DataBind();
                lblCount.Text = Convert.ToString(dt.Rows.Count);
                pnlNull.Visible = false;
                pnlData.Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
            }
            txtTitle.Focus();
        }
        catch (Exception)
        {

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FGetMostafeedAll();
    }

    private void FGetAlBaheth()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where AlBaheth <> @0 And IsDelete = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLAlBaheth.Items.Clear();
            DLAlBaheth.Items.Add("");
            DLAlBaheth.AppendDataBoundItems = true;
            DLAlBaheth.DataValueField = "IDItem";
            DLAlBaheth.DataTextField = "AlBaheth";
            DLAlBaheth.DataSource = dt;
            DLAlBaheth.DataBind();
        }
        FGetModerAlGmeiah();
    }

    private void FGetModerAlGmeiah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where AlModer <> @0 And IsDelete = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLModerAlGmeiah.Items.Clear();
            DLModerAlGmeiah.Items.Add("");
            DLModerAlGmeiah.AppendDataBoundItems = true;
            DLModerAlGmeiah.DataValueField = "IDItem";
            DLModerAlGmeiah.DataTextField = "AlModer";
            DLModerAlGmeiah.DataSource = dt;
            DLModerAlGmeiah.DataBind();
        }
        FGetRaeesMaglesAlEdarah();
    }

    private void FGetRaeesMaglesAlEdarah()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where RaeesMaglesAlEdarah <> @0 And IsDelete = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesMaglesAlEdarah.Items.Clear();
            DLRaeesMaglesAlEdarah.Items.Add("");
            DLRaeesMaglesAlEdarah.AppendDataBoundItems = true;
            DLRaeesMaglesAlEdarah.DataValueField = "IDItem";
            DLRaeesMaglesAlEdarah.DataTextField = "RaeesMaglesAlEdarah";
            DLRaeesMaglesAlEdarah.DataSource = dt;
            DLRaeesMaglesAlEdarah.DataBind();
        }
        FGetRaeesLagnatAlBahath();
    }

    private void FGetRaeesLagnatAlBahath()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT * FROM [dbo].[Quaem] With(NoLock) Where AlAmeenAlAam <> @0 And IsDelete = @1 Order by IDItem", string.Empty, Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLRaeesLagnatAlBahath.Items.Clear();
            DLRaeesLagnatAlBahath.Items.Add("");
            DLRaeesLagnatAlBahath.AppendDataBoundItems = true;
            DLRaeesLagnatAlBahath.DataValueField = "IDItem";
            DLRaeesLagnatAlBahath.DataTextField = "AlAmeenAlAam";
            DLRaeesLagnatAlBahath.DataSource = dt;
            DLRaeesLagnatAlBahath.DataBind();
        }
    }


}