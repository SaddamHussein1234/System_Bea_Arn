using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class_QRScan
/// </summary>
public class Class_QRScan
{
    public Class_QRScan()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string GetImage(object img)
    {
        return "data:image/jpg;base64," + Convert.ToBase64String((byte[])img);
    }

    public static string FGetQRCodePath(string XCode, System.Web.UI.WebControls.Image XImg)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(XCode, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);
        using (Bitmap bitMap = qrCode.GetGraphic(20, Color.Black, Color.White, true))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();

                XImg.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                XImg.Height = 90;
                XImg.Width = 90;
            }
        }
        return XImg.ImageUrl;
    }

    public System.Web.UI.WebControls.Image HexStringToWebControlImage(string hexString)
    {
        var imageAsString = HexString2Bytes(hexString);

        MemoryStream ms = new MemoryStream();
        ms.Write(imageAsString, 0, imageAsString.Length);

        if (imageAsString.Length > 0)
        {
            var base64Data = Convert.ToBase64String(ms.ToArray());
            return new System.Web.UI.WebControls.Image
            {
                ImageUrl = "data:image/jpg;base64," + base64Data
            };
        }
        else
        {
            return null;
        }
    }

    public byte[] HexString2Bytes(string hexString)
    {
        int bytesCount = (hexString.Length) / 2;
        byte[] bytes = new byte[bytesCount];
        for (int x = 0; x < bytesCount; ++x)
        {
            bytes[x] = Convert.ToByte(hexString.Substring(x * 2, 2), 16);
        }

        return bytes;
    }

    public static void FGetQRCode(string XCode, System.Web.UI.WebControls.Image XImg)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(XCode, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);
        using (Bitmap bitMap = qrCode.GetGraphic(20, Color.Black, Color.White, true))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();

                XImg.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                XImg.Height = 90;
                XImg.Width = 90;
            }
        }
    }

}