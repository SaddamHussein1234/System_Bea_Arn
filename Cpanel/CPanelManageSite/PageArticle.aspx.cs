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

public partial class Cpanel_CPanelManageSite_PageArticle : System.Web.UI.Page
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
            Response.Redirect("LogOut.aspx");
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
            bool A11, A12;
            A11 = Convert.ToBoolean(dtViewProfil.Rows[0]["A11"]);
            A12 = Convert.ToBoolean(dtViewProfil.Rows[0]["A12"]);
            if (A11 == false)
            {
                Response.Redirect("PageNotAccess.aspx");
            }
            if (A12 == false)
            {
                IDArticleAdd.Visible = false;
                btnDelete.Visible = false;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckAccountAdmin();
            lblName.Text = ClassSetting.FGetNameSiteAR();
            pnlStar.Visible = true;
            lblDate.Text = ClassDataAccess.GetCurrentTime().ToString("yyyy/MM/dd");
            FGetMenu();
        }
    }

    private void FGetMenu()
    {
        DataTable dt = new DataTable();
        //dt = ClassDataAccess.GetData("SELECT [IDItem],[TitleManageAr]+ ' - ' +[TitleManageTr]+ ' - ' +[TitleManageEn] As 'Title',[IDOrder],[IsDelete] FROM [dbo].[tbl_MenuSite] Where IsDelete = @0 Order By IDOrder", Convert.ToString(false));
        dt = ClassDataAccess.GetData("SELECT [IDItem],[TitleManageAr] As 'Title',[IDOrder],[IsDelete] FROM [dbo].[tbl_MenuSite] Where IsDelete = @0 Order By IDOrder", Convert.ToString(false));
        if (dt.Rows.Count > 0)
        {
            DLMenu.Items.Clear();
            DLMenu.Items.Add("");
            DLMenu.AppendDataBoundItems = true;
            DLMenu.DataValueField = "IDItem";
            DLMenu.DataTextField = "Title";
            DLMenu.DataSource = dt;
            DLMenu.DataBind();
        }
    }

    protected void btnRefrish_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in GVArticle.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string Comp_ID = Convert.ToString(GVArticle.DataKeys[row.RowIndex].Value);
                    SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
                    conn.Open();
                    string sql = "UPDATE [dbo].[tbl_Article] SET [DeleteArticle] = @DeleteArticle WHERE IDItem = @IDItem";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@IDItem", Comp_ID);
                    cmd.Parameters.AddWithValue("@DeleteArticle", true);
                    cmd.ExecuteScalar();
                    conn.Close();
                }
            }
            FArticleByID();
        }
        catch (Exception)
        {

        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FArticleByID();
        }
        catch (Exception)
        {
            
        }
    }

    protected void LinkTitle_Click(object sender, EventArgs e)
    {
        string filename = "/" + Convert.ToString((((LinkButton)sender).CommandArgument)).ToString();
        if (filename != string.Empty)
        {
            string path = Server.MapPath(filename);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", ("attachment; filename=" + file.Name));
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            else
            {
                //Response.Write("This file does not exist.");
            }
        }
    }

    private void FArticleByID()
    {
        ClassArticle CA = new ClassArticle();
        CA.IDMenu = Convert.ToInt32(DLMenu.SelectedValue);
        CA.TypeArticle = Convert.ToInt32(DLType.SelectedValue);
        CA.DeleteArticle = false;
        DataTable dt = new DataTable();
        dt = CA.BArnArticleByID();
        if (dt.Rows.Count > 0)
        {
            lblMenu.Text = DLMenu.SelectedItem.ToString();
            lblType.Text = DLType.SelectedItem.ToString();
            GVArticle.DataSource = dt;
            GVArticle.DataBind();
            lblCount.Text = Convert.ToString(dt.Rows.Count);
            pnlNull.Visible = false;
            pnlData.Visible = true;
            pnlStar.Visible = false;
        }
        else
        {
            pnlNull.Visible = true;
            pnlData.Visible = false;
            pnlStar.Visible = false;
        }
    }

    public string FGetLang(int PathFile)
    {
        string File = "";

        if (PathFile == 1)
            File = "عربي";
        else if (PathFile == 2)
            File = "Türkçe";
        else if (PathFile == 3)
            File = "Einglish";
        return File;
    }

    public string FGetAttach(string PathFile)
    {
        string File = "";
        if (PathFile == "---")
        {
            File = "لايوجدمرفقات";
        }
        else
        {
            File = "يوجدمرفقات";
        }
        return File;
    }

    public string FGetPath(string PathFile)
    {
        string File = "";
        string myFilePath = PathFile;
        string ext = Path.GetExtension(myFilePath);

        if (ext == ".doc" || ext == ".docx" || ext == ".DOC" || ext == ".DOCX")
        {
            File = "<img src='/view/Icon/Word.png' width='20px' />";
        }
        else if (ext == ".XLS" || ext == "xls" || ext == "XLSX" || ext == "xlsx")
        {
            File = "<img src='/view/Icon/excel.png' width='20px' />";
        }
        else if (ext == ".ACCDB" || ext == "ACCDB")
        {
            File = "<img src='/view/Icon/Access.png' width='20px' />";
        }
        else if (ext == ".rar" || ext == "RAR")
        {
            File = "<img src='/view/Icon/rar.png' width='20px' />";
        }
        else if (ext == ".zip" || ext == "ZIP")
        {
            File = "<img src='/view/Icon/rar.png' width='20px' />";
        }
        else if (ext == ".pdf" || ext == "PDF")
        {
            File = "<img src='/view/Icon/pdf.png' width='20px' />";
        }
        else if (ext == ".bmp" || ext == "BMP" || ext == ".gif" || ext == "GIF" || ext == ".png" || ext == "PNG" || ext == ".jpg" || ext == "JPG" || ext == ".jpeg" || ext == "JPEG")
        {
            File = "<img src='/view/Icon/Image.png' width='20px' />";
        }
        return File;
    }

}