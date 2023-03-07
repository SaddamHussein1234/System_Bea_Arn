using Library_CLS_Arn.ClassOutEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FGetObjectivesFoundation();
        }
        
    }

    private void FGetObjectivesFoundation()
    {
        ClassObjectivesFoundation COF = new ClassObjectivesFoundation();
        COF.Top = Convert.ToInt32(ClassSetting.FGetCountComp());
        COF.TypeItem = 1;
        COF.IsViewItem = true;
        DataTable dt = new DataTable();
        dt = COF.BArnbjectivesFoundationGetByView();
        if (dt.Rows.Count > 0)
        {
            RPTObjectivesFoundation.DataSource = dt;
            RPTObjectivesFoundation.DataBind();
            IDObjectivesFoundation.Visible = true;
        }
        else
        {
            IDObjectivesFoundation.Visible = false;
        }
    }

}