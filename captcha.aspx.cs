using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class captcha : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Bitmap objBMP = new Bitmap(60, 20);
        Graphics obgGraphics = Graphics.FromImage(objBMP);
        obgGraphics.Clear(Color.White);
        obgGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        Font objFont = new Font("Arial", 12, FontStyle.Bold);
        string randomstr = "";
        int[] myIntArray = new int[5];
        int x = 0;
        Random auotRand = new Random();
        for (x = 0; x <= 4; x++)
        {
            myIntArray[x] = System.Convert.ToInt32(auotRand.Next(0, 9));
            randomstr += (myIntArray[x].ToString());
        }

        Session.Add("randomNumber", randomstr);
        obgGraphics.RotateTransform(-7f);
        obgGraphics.DrawString(randomstr, objFont, Brushes.Black, 3, 3);
        Response.ContentType = "image/Gif";
        objBMP.Save(Response.OutputStream, ImageFormat.Gif);
        objFont.Dispose();
    }

}