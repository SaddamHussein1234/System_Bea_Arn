using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Test : System.Web.UI.Page
{
    public string XNAmeServer = string.Empty, XMony = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        XNAmeServer = ClassSetting.FGetNameServer(); XMony = ClassSaddam.FGetMonySa();
        if (!IsPostBack)
        {
            //F();

        }
    }
    
    private void F()
    {
        DataTable DTQarar = new DataTable();
        // قرارات القبول من 1 لى 210
        //DTQarar = ClassDataAccess.GetData("SELECT TOP (1000) *  FROM [dbo].[QararQobolMustafeed] With(NoLock)  Where NumberQarar <= 210 And [IsQobol] = 1 And [IsEstepaad] = 0 Order by NumberQarar")
        
        DTQarar = ClassDataAccess.GetData("SELECT TOP (1000) *  FROM [dbo].[QararQobolMustafeed] With(NoLock)  Where NumberQarar <= 210 Order by NumberQarar");
        for (int x = 0; x <= DTQarar.Rows.Count - 1; x++)
        {
            DataTable DTAdmin = new DataTable();
            DTAdmin = ClassDataAccess.GetData("Select Top(100) [ID_Item],[FirstName],[CommentAdmin],[IsDelete] from tbl_Admin With(noLock) Where [ID_Item] in (110,1132,109,1141,111,1149,121) Order by IsOrderAdminInEdarah");
            if (DTAdmin.Rows.Count > 0)
            {
                for (int i = 0; i <= DTAdmin.Rows.Count - 1; i++)
                {
                    ClassQararQobolAdmin CQQA = new ClassQararQobolAdmin()
                    {
                        _NumberMostafeed = Convert.ToInt64(DTQarar.Rows[x]["NumberMostafeed"]),
                        _NumberReport = Convert.ToInt64(DTQarar.Rows[x]["NumberReport"]),
                        _NumberQarar = Convert.ToInt64(DTQarar.Rows[x]["NumberQarar"]),
                        _IDAdmin = Convert.ToInt32(DTAdmin.Rows[i]["ID_Item"]),
                        _AdminAllow = true,
                        _A1 = DTAdmin.Rows[i]["CommentAdmin"].ToString(),
                        _A2 = "0",
                        _A3 = "0",
                        _A4 = "0",
                        _A5 = "0",
                        _IsDelete = false
                    };
                    CQQA.BArnQararQobolMustafeedAdminAdd();
                }
            }
        }
    }

    protected void btnAddImage_Click(object sender, EventArgs e)
    {
        if (ImageFile.HasFile)  //if file uploaded
        {
            if (checkFileType(ImageFile.FileName))  //Check for file types
            {
                try
                {
                    //save file
                    ImageFile.SaveAs(MapPath("~/ImgSystem/FilesDMS/" + ImageFile.FileName));
                    //Response.Write("<script language =Javascript> alert('File Uploaded Successfully, Click Show Images');</script>");
                    System.Threading.Thread.Sleep(8000);
                    Label1.Text = "Upload successfull!";
                }
                catch (DirectoryNotFoundException)
                {
                    createDir();
                }
            }
        }
        else
        {
            Response.Write("<script language =Javascript> alert('Invalid File Format, choose .gif,.png..jpg.jpeg');</script>");
        }
    }

    private bool checkFileType(string fileName)
    {
        string fileExt = Path.GetExtension(ImageFile.FileName);

        switch (fileExt.ToLower())
        {
            case ".gif":
                return true;
            case ".png":
                return true;
            case ".jpg":
                return true;
            case ".jpeg":
                return true;
            default:
                return false;
        }

    }

    private void createDir()
    {
        DirectoryInfo myDir = new DirectoryInfo(MapPath("~/ImgSystem/FilesDMS/"));
        myDir.Create();

        //Now save file
        ImageFile.SaveAs(MapPath("~/UploadImages/" + ImageFile.FileName));
        Response.Write("<script language =Javascript> alert('File Uploaded Successfully,Click Show Images');</script>");
    }

    protected void btnShowImage_Click(object sender, EventArgs e)
    {
        DirectoryInfo myDir = new DirectoryInfo(MapPath("~/ImgSystem/FilesDMS/"));
        try
        {

            dlImageList.DataSource = myDir.GetFiles();
            dlImageList.DataBind();

        }
        catch (DirectoryNotFoundException)
        {
            Response.Write("<script language =Javascript> alert('Upload File(s) First!');</script>");
        }
    }

    public string FConvertToWord(string XMony)
    {
        List<CurrencyInfo> currencies = new List<CurrencyInfo>();
        currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
        ToWord toWord = new ToWord(Convert.ToDecimal(XMony), currencies[Convert.ToInt32(0)]);
        return toWord.ConvertToArabic();
    }

    protected void LBSubmet_Click(object sender, EventArgs e)
    {
        FGetByBillMulti();
    }

    private void FGetByBillMulti()
    {
        DataTable dt = new DataTable();
        dt = GetData(txtQuery.Text.Trim());
        if (dt.Rows.Count > 0)
        {
            RPTCashing.DataSource = dt;
            RPTCashing.DataBind();
        }
    }

    public static DataTable GetData(string sql)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(string.Format("Data Source=SQL5085.site4now.net;Initial Catalog=db_a513a6_berarnallom;User Id=BerSrnSllShiring;Password=@O!_VS3_4FDG_B@;")))
        {

            SqlDataAdapter adapt = new SqlDataAdapter(sql, con);
            con.Open();
            adapt.Fill(dt);
            con.Close();

        }
        return dt;
    }

}