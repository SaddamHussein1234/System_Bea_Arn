<%@ WebHandler Language="C#" Class="Upload" %>
 
using System;
using System.Web;

public class Upload : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        HttpPostedFile uploads = context.Request.Files["upload"];
        string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
        string file = System.IO.Path.GetFileName(uploads.FileName.Remove(3) + ".swf");
        
        uploads.SaveAs(context.Server.MapPath(".") + "\\xup\\swf\\" + file);
        string url = "Saddam/xup/swf/" + file;
        context.Response.Write("<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script><html><body>");
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