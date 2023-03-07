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
        if (!IsPostBack)
        {
            HFLink.Value = Request.Url.Authority;
            FGetSetting();
            this.Page.Header.Title = "معرض الصور" + " - " + HFNameSite.Value;
            GetAlbumArabic();
        }
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
            RPTAlbumArabic.Visible = true;
            pnlNull.Visible = false;
        }
        else
        {
            RPTAlbumArabic.Visible = false;
            pnlNull.Visible = true;
        }
    }

    public float FCountImg(float X)
    {
        float VaManage = 0;
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("SELECT COUNT(1) As 'CountImg' FROM [dbo].[tbl_AlbumImg] With(NoLock) Where IDAlbum = @0 And IsDelete = @1", Convert.ToInt32(X), false);
        if (dt.Rows.Count > 0)
            VaManage = Convert.ToInt64(dt.Rows[0]["CountImg"]);
        return VaManage;
    }

    private void FGetSetting()
    {
        DataTable dt = new DataTable();
        dt = ClassDataAccess.GetData("Select Top(1) * from SettingTable With(NoLock) Where IDSetting = @0", Convert.ToString(964654));
        if (dt.Rows.Count > 0)
        {
            HFNameSite.Value = dt.Rows[0]["NameSiteAR"].ToString();

            HFTitle.Value = "معرض الصور";
            HFDescrption.Value = dt.Rows[0]["DescriptoinSiteAR"].ToString();
            HFKeyWord.Value = dt.Rows[0]["KeyWordAR"].ToString();
            HFImage.Value = dt.Rows[0]["ImgSystem"].ToString();
        }
    }

}