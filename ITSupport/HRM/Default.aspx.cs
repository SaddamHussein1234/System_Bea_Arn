using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ITSupport_HRM_Default : System.Web.UI.Page
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
            dt = Attach_Data_Access_Layer.GetData("SELECT Top(100) * FROM [dbo].[TBL_IT_Support] With(NoLock) Where [Group_Lesson_] = @0 And [IsActive_] = @1 And [IsDelete_] = @2 Order by [Is_Order_]", "HRM", Convert.ToString(true), Convert.ToString(false));
            if (dt.Rows.Count > 0)
            {
                RPTITSupport.DataSource = dt;
                RPTITSupport.DataBind();
            }
        }
        catch (Exception)
        {
            return;
        }
    }

}