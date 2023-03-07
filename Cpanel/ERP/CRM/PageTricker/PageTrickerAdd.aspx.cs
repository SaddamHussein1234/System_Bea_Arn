using Library_CLS_Arn.CRM.Models;
using Library_CLS_Arn.CRM.Repostry;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_CRM_PageTricker_PageTrickerAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                txtTitle.Text = "سجل متابعة الداعم : " + Request.QueryString["Name"];
                FGetData(new Guid(Request.QueryString["ID"]));
                txt_Message.Focus();
            }
            else
                Response.Redirect("../PageCompany/PageCompany.aspx");
        } 
    }

    private void FGetData(Guid XID)
    {
        try
        {
            Model_Tricker_ MC = new Model_Tricker_();
            MC.IDCheck = "GetByIDComp";
            MC.ID_Item = Guid.Empty;
            MC.ID_Company = XID;
            MC.Start_Date = string.Empty;
            MC.End_Date = string.Empty;
            MC.CreatedDate = string.Empty;
            MC.Is_Delete = false;
            DataTable dt = new DataTable();
            Repostry_Tricker_ RC = new Repostry_Tricker_();
            dt = RC.BCRM_Tricker_Manage(MC);
            if (dt.Rows.Count > 0)
            {
                RPTMessage.DataSource = dt;
                RPTMessage.DataBind();
                pnlNull.Visible = false;
                pnlData.Visible = true;
            }
            else
            {
                pnlNull.Visible = true;
                pnlData.Visible = false;
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    protected void LBBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../PageCompany/PageCompany.aspx");
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            FChackImgF();
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    private void FChackImgF()
    {
        if (fuPhoto.HasFile)
        {
            string[] validFileTypes = { "pdf", "PDF" };
            string ext = Path.GetExtension(fuPhoto.PostedFile.FileName);
            bool isValidFile = false;
            for (int i = 0; i <= validFileTypes.Length - 1; i++)
            {
                if (ext == "." + validFileTypes[i])
                {
                    isValidFile = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            if (!isValidFile)
            {
                IDMessageWarning.Visible = true;
                IDMessageSuccess.Visible = false;
                lblWarning.Text = "Image Allow " + string.Join(",", validFileTypes);
                return;
            }
            else
                FUpimg(fuPhoto);
        }
        else
            FUpimg(fuPhoto);
    }

    string FileArt = string.Empty;
    protected void FUpimg(FileUpload upl)
    {
        if (upl.HasFile)
        {
            string XRandom = Convert.ToString(ClassDataAccess.RandomGenerator());
            string theFileName = Path.Combine(Server.MapPath("~/ImgSystem/FileAttachComp/"), upl.FileName.Remove(3) + XRandom + upl.FileName);
            upl.SaveAs(theFileName);
            FileArt = "ImgSystem/FileAttachComp/" + upl.FileName.Remove(3) + XRandom + upl.FileName;
            FAdd();
        }
        else
        {
            FileArt = "---";
            FAdd();
        }
    }

    private void FAdd()
    {
        Model_Tricker_ MT = new Model_Tricker_()
        {
            IDCheck = "Add",
            ID_Item = Guid.NewGuid(),
            ID_Company = new Guid(Request.QueryString["ID"]),
            Descryption = txt_Message.Text.Trim().Replace(Environment.NewLine, "<br />"),
            File_Attach = FileArt,
            CreatedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            CreatedBy = Test_Saddam.FGetIDUsiq(),
            ModifiedBy = 0,
            ModifiedDate = ClassSaddam.GetCurrentTime().ToString("yyyy-MM-dd HH:mm:ss"),
            Is_Delete = false
        };

        Repostry_Tricker_ RT = new Repostry_Tricker_();
        string Xresult = RT.FCRM_Tricker_Add(MT);
        if (Xresult == "IsExistsAdd")
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "تم إضافة البيانات سابقاً ... ";
            return;
        }
        else if (Xresult == "IsSuccessAdd")
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = true;
            lblSuccess.Text = "تم إضافة البيانات بنجاح ... ";
            FGetData(new Guid(Request.QueryString["ID"]));
            txt_Message.Focus();
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            Session["footable1"] = pnlPrint;
            ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('/Cpanel/PrintFootable1.aspx','PrintMe','height=900px,width=1000px,scrollbars=1');</script>");
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

    public string FGetAttach(string PathFile)
    {
        string File = "";
        if (PathFile == "---")
            File = "لايوجدمرفقات";
        else
            File = "يوجدمرفقات";
        return File;
    }

    public string FGetPath(string PathFile)
    {
        string File = "";
        string myFilePath = PathFile;
        string ext = Path.GetExtension(myFilePath);

        if (ext == ".pdf" || ext == "PDF")
            File = "<img src='/view/Icon/pdf.png' width='20px' />";
        return File;
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

    protected void LBDelete_Click(object sender, EventArgs e)
    {
        try
        {
            IDMessageWarning.Visible = false;
            IDMessageSuccess.Visible = false;
            string XID = Convert.ToString((((LinkButton)sender).CommandArgument)).ToString();
            if (XID != string.Empty)
            {
                Model_Tricker_ MT = new Model_Tricker_()
                {
                    IDCheck = "Delete",
                    ID_Item = new Guid(XID),
                    ID_Company = Guid.Empty,
                    Descryption = string.Empty,
                    File_Attach = FileArt,
                    CreatedDate = string.Empty,
                    CreatedBy = 0,
                    ModifiedBy = 0,
                    ModifiedDate = string.Empty,
                    Is_Delete = true
                };

                Repostry_Tricker_ RT = new Repostry_Tricker_();
                string Xresult = RT.FCRM_Tricker_Add(MT);
                if (Xresult == "IsSuccessDelete")
                {
                    IDMessageWarning.Visible = false;
                    IDMessageSuccess.Visible = true;
                    lblSuccess.Text = "تم إلغاء الملف بنجاح ... ";
                    FGetData(new Guid(Request.QueryString["ID"]));
                    txt_Message.Focus();
                }
            }
        }
        catch (Exception)
        {
            IDMessageWarning.Visible = true;
            IDMessageSuccess.Visible = false;
            lblWarning.Text = "خطأ غير متوقع , حاول لاحقاً ... ";
            return;
        }
    }

}