<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;

public class Upload : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        HttpPostedFile uploads = context.Request.Files["upload"];
        string CKEditorFuncNum = context.Request["CKEditorFuncNum"];

        string file = System.IO.Path.GetFileName(uploads.FileName.Remove(3) + Library_CLS_Arn.ERP.DataAccess.ClassDataAccess.RandomGenerator().ToString().Replace("-", "") + ".jpg");
        

        uploads.SaveAs(context.Server.MapPath(".") + "\\xup\\img\\" + file);
        string url = "../../Saddam/xup/img/" + file ;
        context.Response.Write("<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script><html><body>" );
        context.Response.End();
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }



}