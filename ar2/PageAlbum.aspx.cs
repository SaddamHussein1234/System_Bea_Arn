using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ar_PageAlbum : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Header.Title = ClassSetting.FGetNameSiteAR() + " | " + "البومات الصور";
        GetAlbumArabic();
    }

    private void GetAlbumArabic()
    {
        ClassAlbum CA = new ClassAlbum();
        CA.IsViewAr = true;
        CA.IsDelete = false;
        DataTable dt = new DataTable();
        dt = CA.BGetAlbumByViewByAr();
        if (dt.Rows.Count > 0)
        {
            RPTAlbumArabic.DataSource = dt;
            RPTAlbumArabic.DataBind();
            
        }
        else
        {
            IDNullData.Visible = true;
        }
    }

    public float FCountImg(float X)
    {
        float VaManage = 0;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT COUNT(*) As 'CountImg' FROM [dbo].[tbl_AlbumImg] Where IDAlbum = @0 And IsDelete = @1", Convert.ToString(X), Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            VaManage = Convert.ToInt64(dt.Rows[0]["CountImg"]);
        }
        return VaManage;
    }

}