using Library_CLS_Arn.ERP.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Sitemap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string XLink = Request.Url.Host;
        DataTable dtSetting = new DataTable();
        dtSetting = ClassDataAccess.GetData("Select Top(1) * from SettingTable With(NoLock) Where IDSetting = @0", Convert.ToString(964654));
        if (dtSetting.Rows.Count > 0)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(_Link_Connection_.ConnectionString_System_);
            using (conn)
            {
                SqlDataAdapter ad = new SqlDataAdapter("SELECT Top(100) * from [dbo].[tbl_Article] Where [IsView] = 1 And [DeleteArticle] = 0 Order By [IDItem] Desc", conn);
                ad.Fill(dt);
            }

            Response.Clear();
            Response.ContentType = "text/xml";
            XmlTextWriter TextWriter = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);
            TextWriter.WriteStartDocument();
            // Below tags are mandatory rss
            TextWriter.WriteStartElement("rss");
            TextWriter.WriteAttributeString("version", "2.0");
            // Channel tag will contain RSS feed details
            TextWriter.WriteStartElement("channel");
            TextWriter.WriteElementString("title", "" + dtSetting.Rows[0]["NameSiteAR"].ToString() + " - RSS");
            TextWriter.WriteElementString("link", "https://"+ XLink + "/ar/");
            TextWriter.WriteElementString("description", dtSetting.Rows[0]["DescriptoinSiteAR"].ToString());
            TextWriter.WriteElementString("copyright", "Copyright " + DateTime.Now.Year + " "+ XLink + ". All rights reserved.");
            foreach (DataRow oFeedItem in dt.Rows)
            {
                TextWriter.WriteStartElement("item");
                TextWriter.WriteElementString("title", oFeedItem["TitleArticle"].ToString());
                TextWriter.WriteElementString("link", "https://" + XLink + "/ar/PageViewDetails.aspx?ID=" + oFeedItem["IDUniqArticle"].ToString());
                TextWriter.WriteElementString("pubDate", oFeedItem["DateAddArticle"].ToString());

                TextWriter.WriteElementString("description", FText(oFeedItem["DetailsArticle"].ToString()));

                TextWriter.WriteStartElement("image");
                TextWriter.WriteElementString("url", "https://" + XLink + "/" + oFeedItem["ImgArt"].ToString().Replace("~", ""));
                TextWriter.WriteEndElement();

                TextWriter.WriteEndElement();
            }
            TextWriter.WriteEndElement();
            TextWriter.WriteEndElement();
            TextWriter.WriteEndDocument();
            TextWriter.Flush();
            TextWriter.Close();
            Response.End();
        }

        
    }

    public string FText(string XHTMLText)
    {
        string XResult = string.Empty;
        var result = Regex.Replace(XHTMLText, @"<(.|\n)*?>", string.Empty);
        XResult = result.ToString();
        if (XResult.Length > 20)
            XResult.Remove(20);
        return XResult;
    }

}