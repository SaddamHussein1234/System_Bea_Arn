using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ITSupport_CRM_PageDetailsVideo : System.Web.UI.Page
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
            DataTable dt = new DataTable();
            dt = Attach_Data_Access_Layer.GetData("SELECT Top(1) * FROM [dbo].[TBL_IT_Support] With(NoLock) Where IDUniq = @0 And IsActive_ = @1 And IsDelete_ = @2", Convert.ToString(Request.QueryString["ID"]), Convert.ToString(true), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                lblTitle.Text = dt.Rows[0]["TitleLesson_"].ToString();
                IDVideo.Src = "../" + dt.Rows[0]["LinkVideo_"].ToString();
            }
            else
                Response.Redirect("Default.aspx");
        }
        catch (Exception)
        {
            return;
        }
    }

}