using Library_CLS_Arn.ClassEntity.Attach.Models;
using Library_CLS_Arn.ClassEntity.Attach.Repostry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Check : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
                FGetData(Request.QueryString["ID"]);
        }
    }

    private void FGetData(string XShort_URL)
    {
        try
        {
            Attach_Model_Short_URL_ MWZB = new Attach_Model_Short_URL_();
            MWZB.IDCheck = "GetByShort_URL";
            MWZB.ID_Item = 0;
            MWZB.Start_Date = string.Empty;
            MWZB.End_Date = string.Empty;
            MWZB.CheckValue = XShort_URL;
            MWZB.Is_Delete = false;
            DataTable dt = new DataTable();
            Attach_Repostry_Short_URL_ RWZB = new Attach_Repostry_Short_URL_();
            dt = RWZB.BAttach_Short_URL_Manage(MWZB);
            if (dt.Rows.Count > 0)
                Response.Redirect(dt.Rows[0]["_Long_URL_"].ToString());
            else
                lblMessageWarning.Text = "Null";
        }
        catch (Exception)
        {
            //IDMessageSuccess.Visible = false;
            //IDMessageWarning.Visible = true;
            lblMessageWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

}