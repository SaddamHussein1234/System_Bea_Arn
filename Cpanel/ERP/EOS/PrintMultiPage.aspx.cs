using Library_CLS_Arn.ClassOutEntity;
using Library_CLS_Arn.ERP.DataAccess;
using Library_CLS_Arn.Saddam;
using Library_CLS_Arn.WSM;
using Library_CLS_Arn.WSM.Models;
using Library_CLS_Arn.WSM.Repostry;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cpanel_ERP_EOS_PrintMultiPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string phrase = Request.QueryString["IDBill"];
                string[] words = phrase.Split(',');
                foreach (var word in words)
                {
                    IDStyleBill.InnerHtml += FSetBill(new Guid(Request.QueryString["IDUniq"]), word, Convert.ToInt32(Request.QueryString["XIDCate"]));
                }

                //string PName = Session["XName"].ToString();
                //string[] _XName = PName.Split(',');
                //string phrase = Request.QueryString["IDBill"];
                //string[] words = phrase.Split(',');

                //foreach (var _XName_ in _XName)
                //{
                //    foreach (var word in words)
                //    {
                //        IDStyleBill.InnerHtml += FSetBill(new Guid(Request.QueryString["IDUniq"]), word, Convert.ToInt32(Request.QueryString["XIDCate"]), _XName_);
                //    }
                //}

            }
            catch (Exception)
            {

            }
        }
    }

    public string FSetBill(Guid XYear, string IDBill, int IDProject)
    {
        string XResult = string.Empty;

        WSM_Model_Exchange_Order_Bill_ MEOB = new WSM_Model_Exchange_Order_Bill_();
        MEOB.IDCheck = "GetByBillMulti";
        MEOB.ID_Item = Guid.Empty;
        MEOB.ID_FinancialYear = XYear;
        MEOB.ID_Donor = Guid.Empty;
        MEOB.bill_Number = Convert.ToInt32(IDBill);
        MEOB.ID_MosTafeed = IDProject;
        MEOB.Start_Date = string.Empty;
        MEOB.End_Date = string.Empty;
        MEOB.DataCheck = string.Empty;
        MEOB.DataCheck2 = string.Empty;
        MEOB.DataCheck3 = string.Empty;
        MEOB.Is_Cart = false;
        MEOB.Is_Device = false;
        MEOB.Is_Tathith = false;
        MEOB.Is_Talef = false;
        MEOB.IsActive = true;
        DataTable dt = new DataTable();
        WSM_Repostry_Exchange_Order_Bill_ REOB = new WSM_Repostry_Exchange_Order_Bill_();
        dt = REOB.BWSM_Exchange_Order_Bill_Manage(MEOB);
        if (dt.Rows.Count > 0)
        {
            XResult += "<div class='page'><div align='center' class='w'>";
            XResult += "<div><img src='/view/image/LogoTitleNew2.jpg' style='width:100%; height:100px;' /></div>";
            XResult += "<table style='width: 100%; background-color: #ffffff; color: #393939'>";
            XResult += "<tr>";
            XResult += "<td style='border: thin double #808080; border-width: 1px; width: 45%'>";
            XResult += "<span style='text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;'>";
            XResult += ClassSaddam.FAlTypeEvint(Convert.ToInt32(dt.Rows[0]["_ID_Type_Shipment_"])) + " لمشروع (" + Request.QueryString["Name"] + ")";
            XResult += "</span>";
            XResult += "</td>";
            XResult += "<td style='border: thin double #808080; border-width: 1px; width: 20%'>";
            XResult += "<table style='width: 100%; font-size: 12px'><tr><td align='left' style='width: 60%; font-family: 'Alwatan'; font-size: 18px;'>رقم الفاتورة / </td><td style='width: 40%; font-family: 'Alwatan'; font-size: 18px;'>";
            XResult += dt.Rows[0]["_bill_Number_"].ToString();
            XResult += "</td></tr></table>";
            XResult += "</td>";
            XResult += "<td style='border: thin double #808080; border-width: 1px; width: 35%'>";
            XResult += "<table style='width: 100%;'><tr><td align='left' style='width: 20%; font-size: 12px'>التاريخ / </td><td style='width: 80%'>";
            XResult += Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]).ToString("yyyy/MM/dd") + "مـ - ";
            XResult += ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"])) + "هـ";
            XResult += "</td></tr></table>";
            XResult += "</td>";
            XResult += "</tr>";
            XResult += "</table>";
            XResult += "</div>";

            XResult += "<div style='float: right; padding: 10px 10px 0 10px;' class='w'><p style='font-size: 13px'>السيد / أمين المستودع</p><p style='font-size: 13px'>بموجبه يتم الصرف للسيد / </p></div>";
            
            XResult += "<div style='float: left; padding: 10px 0 0 10px' class='w'><table style='font-size: 12px'><tr><td style='border: thin double #C0C0C0; border-width: 1px; padding: 5px'>مدخل البيانات :";
            XResult += ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_CreatedBy_"]));
            XResult += "</td></tr><tr><td style='border: thin double #C0C0C0; border-width: 1px; padding: 5px'>بتاريخ :";
            XResult += ClassDataAccess.FChangeF(Convert.ToDateTime(dt.Rows[0]["_CreatedDate_"]));
            XResult += "</td></tr></table></div>";

            string code = ClassSetting.FGetNameServer() +
                    "/Cpanel/ERP/EOS/In_Kind_Donation/PageView.aspx?IDUniq=" + XYear.ToString() + "&ID=" + IDBill.ToString() + "&XID=" + dt.Rows[0]["_ID_MosTafeed_"].ToString() +
                    "&XIDCate=" + IDProject.ToString() + "&IsCart=" + dt.Rows[0]["_Is_Cart_"].ToString() + "&IsDevice=" + dt.Rows[0]["_Is_Device_"].ToString() +
                    "&IsTathith=" + dt.Rows[0]["_Is_Tathith_"].ToString() + "&IsTalef=" + dt.Rows[0]["_Is_Talef_"].ToString();

            XResult += "<div align='center' class='w'><img src='" + Class_QRScan.FGetQRCodePath(code, Image1) + "' alt='Loding' style='Height:90px; Width:90px;' /></div>";

            XResult += "<table style='width: 100%'><tr runat='server' id='IDUserDetails'><td style='width: 40%; border: thin double #808080; border-width: 1px; padding: 5px' align='center'><p style='font-size: 11px'>";
            if (dt.Rows[0]["_Note_"].ToString() == string.Empty || dt.Rows[0]["_Note_"].ToString() == "0")
                XResult += " الإسم : " + ClassSaddam.FAlName(Convert.ToInt32(dt.Rows[0]["_ID_MosTafeed_"]), dt.Rows[0]["_ID_Type_Shipment_"].ToString());
            else
                XResult += dt.Rows[0]["_Note_"].ToString();
            XResult += "</p></td><td style='width: 20%; border: thin double #808080; border-width: 1px;' align='center'><p style='font-size: 11px'>الجوال : 0";
            XResult += ClassMosTafeed.FGetMosTafeedPhone(Convert.ToInt32(dt.Rows[0]["_ID_MosTafeed_"]));
            XResult += "</p></td><td style='width: 20%; border: thin double #808080; border-width: 1px;' align='center'><p style='font-size: 11px'>القرية : ";
            XResult += ClassMosTafeed.FGetMosTafeedQariah(Convert.ToInt32(dt.Rows[0]["_ID_MosTafeed_"]));
            XResult += "</p></td><td style='width: 20%; border: thin double #808080; border-width: 1px;' align='center'><p style='font-size: 11px'>رقم الملف : ";
            XResult += dt.Rows[0]["_ID_MosTafeed_"].ToString();
            XResult += "</p></td></tr></table>";

            XResult += "<div style='font-family: 'Alwatan'; font-size: 18px; float: right'> الأصناف الموضحة أدناه : </div>";
            XResult += "<div align='left' style='font-family: 'Alwatan'; font-size: 18px'>";
            if (dt.Rows[0]["_The_Initiative_"].ToString() != "1")
                XResult += ClassInitiatives.FGetInitiativesName(Convert.ToInt32(dt.Rows[0]["_The_Initiative_"]));
            else
                XResult += string.Empty;
            XResult += "</div><span class='hr'></span>";

            XResult += "<div class='table table-responsive'><table class='table table-bordered table-condensed' style='width: 100%' aria-multiline='true'>";
            XResult += "<thead>";
            XResult += "<tr>";
            XResult += "<th style='width: 10px; border: thin double #808080; border-width: 1px;' align='center'>م</th>";
            XResult += "<th style='width: 20%; border: thin double #808080; border-width: 1px;' align='center'>الصنف</th>";
            XResult += "<th style='width: 25%; border: thin double #808080; border-width: 1px;' align='center'>المنتج</th>";
            XResult += "<th style='width: 15%; border: thin double #808080; border-width: 1px;' align='center'>العدد</th>";
            XResult += "<th style='width: 20%; border: thin double #808080; border-width: 1px;' align='center'>السعر الفردي</th>";
            XResult += "<th style='width: 20%; border: thin double #808080; border-width: 1px;' align='center'>السعر الإجمالي</th>";
            XResult += "</tr>";
            XResult += "</thead>";

            XResult += "<tbody>";

            XResult += FGetByBill(new Guid(dt.Rows[0]["_ID_Item_"].ToString()), Convert.ToInt32(dt.Rows[0]["_ID_MosTafeed_"]), dt.Rows[0]["_Count_Families_"].ToString(), dt.Rows[0]["_Count_Cart_"].ToString());
            
            XResult += "<div class='table table-responsive'><div class='C31_5' style='border: thin double #808080; border-width: 1px;' align='center'>";
            XResult += "<table style='width: 100%; margin: 5px; font-size: 12px'><tr><td style='width: 45%;'>مدير الجمعية : </td>";
            XResult += "<td style='width: 55%;'>";
            if (Convert.ToBoolean(dt.Rows[0]["_Is_Moder_"]))
                XResult += "<img src='/" + ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Moder_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Moder_"])) + "' alt='Loding,,,' style='Height:30px; Width:100px;' />";
            else
                XResult += "<img src='/Cpanel/loaderMin.gif' alt='Loding,,,' style='Height:30px; Width:30px;' />";
            XResult += "</td></tr></table></div>";

            XResult += "<div class=' C31_5' style='border: thin double #808080; border-width: 1px;' align='center'>";
            XResult += "<table style='width: 100%; margin: 5px; font-size: 12px'><tr><td style='width: 45%;'>المشرف المالي : </td>";
            XResult += "<td style='width: 55%;'>";
            if (Convert.ToBoolean(dt.Rows[0]["_Is_Ammen_AlSondoq_"]))
                XResult += "<img src='/" + ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Ammen_AlSondoq_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Ammen_AlSondoq_"])) + "' alt='Loding,,,' style='Height:30px; Width:100px;' />";
            else
                XResult += "<img src='/Cpanel/loaderMin.gif' alt='Loding,,,' style='Height:30px; Width:30px;' />";
            XResult += "</td></tr></table></div>";

            XResult += "<div class=' C31_5' style='border: thin double #808080; border-width: 1px;' align='center'>";
            XResult += "<table style='width: 100%; margin: 5px; font-size: 12px'><tr><td style='width: 45%;'>رئيس الجمعية : </td>";
            XResult += "<td style='width: 55%;'>";
            if (Convert.ToBoolean(dt.Rows[0]["_Is_Raees_Maglis_AlEdarah_"]))
                XResult += "<img src='/" + ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Raees_Maglis_AlEdarah_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Raees_Maglis_AlEdarah_"])) + "' alt='Loding,,,' style='Height:30px; Width:100px;' />";
            else
                XResult += "<img src='/Cpanel/loaderMin.gif' alt='Loding,,,' style='Height:30px; Width:30px;' />";
            XResult += "</td></tr></table></div>";
            XResult += "</div><hr />";

            XResult += "<table style='width: 100%'><tr><td style='width: 50%; border: thin double #808080; border-width: 1px;'><p style='font-size: 13px'>";
            if (Convert.ToBoolean(dt.Rows[0]["_Is_Done_"]) && Convert.ToBoolean(dt.Rows[0]["_Is_Not_Done_"]) == false)
            {
                XResult += "<span align='center' style='font-size: 13px; margin:5px;'> تم الصرف <img src='/Img/IconTrue.png' style='Height:20px; Width:20px;' /></span>";
                XResult += "<span align='center' style='font-size: 13px; margin:5px;'> / لم يتم الصرف بعد <img src='/Img/IconFalse.png' style='Height:20px; Width:20px;' /></span>";
            }
            else if (Convert.ToBoolean(dt.Rows[0]["_Is_Done_"]) == false && Convert.ToBoolean(dt.Rows[0]["_Is_Not_Done_"]))
            {
                XResult += "<span align='center' style='font-size: 13px; margin:5px;'> تم الصرف <img src='/Img/IconFalse.png' style='Height:20px; Width:20px;' /></span>";
                XResult += "<span align='center' style='font-size: 13px; margin:5px;'> / لم يتم الصرف بعد <img src='/Img/IconTrue.png' style='Height:20px; Width:20px;' /></span>";
            }
            XResult += "</div></p><p style='font-size: 13px; padding-right: 5px'>أمين المستودع / ";
            if (Convert.ToBoolean(dt.Rows[0]["_Is_Storekeeper_"]))
                XResult += "<img src='/" + ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Storekeeper_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Storekeeper_"])) + "' alt='Loding,,,' style='Height:30px; Width:100px;' />";
            else
                XResult += "<img src='/Cpanel/loaderMin.gif' alt='Loding,,,' style='Height:30px; Width:30px;' />";
            XResult += "</p><p style='font-size: 13px; padding-right: 5px'>بتاريخ / ";
            if (Convert.ToBoolean(dt.Rows[0]["_Is_Done_"]) == false && Convert.ToBoolean(dt.Rows[0]["_Is_Not_Done_"]) == false)
                XResult += "بإنتظار الملاحظة";
            else if (Convert.ToBoolean(dt.Rows[0]["_Is_Done_"]) == true || Convert.ToBoolean(dt.Rows[0]["_Is_Not_Done_"]) == true)
                XResult += Convert.ToDateTime(dt.Rows[0]["_Date_Storekeeper_Allow_"]).ToString("yyyy/MM/dd");

            XResult += "</p></td><td style='width: 50%; border: thin double #808080; border-width: 1px;'><p style='font-size: 13px'>";

            if (Convert.ToBoolean(dt.Rows[0]["_Is_Received_"]) && Convert.ToBoolean(dt.Rows[0]["_Is_Not_Received_"]) == false)
            {
                XResult += "<span align='center' style='font-size: 13px; margin:5px;'> تم التسليم <img src='/Img/IconTrue.png' style='Height:20px; Width:20px;' /></span>";
                XResult += "<span align='center' style='font-size: 13px; margin:5px;'> / لم يتم التسليم بعد <img src='/Img/IconFalse.png' style='Height:20px; Width:20px;' /></span>";
            }
            else if (Convert.ToBoolean(dt.Rows[0]["_Is_Received_"]) == false && Convert.ToBoolean(dt.Rows[0]["_Is_Not_Received_"]))
            {
                XResult += "<span align='center' style='font-size: 13px; margin:5px;'> تم التسليم <img src='/Img/IconFalse.png' style='Height:20px; Width:20px;' /></span>";
                XResult += "<span align='center' style='font-size: 13px; margin:5px;'> / لم يتم التسليم بعد <img src='/Img/IconTrue.png' style='Height:20px; Width:20px;' /></span>";
                XResult += "<span>/ السبب : " + dt.Rows[0]["_Note_Not_Received_"].ToString()+ "</span>";
            }

            XResult += "</p><p style='font-size: 13px; padding-right: 5px'>إسم الباحث / ";
            XResult += ClassQuaem.FAlBaheth(Convert.ToInt32(dt.Rows[0]["_ID_Delivery_"]));
            if (Convert.ToBoolean(dt.Rows[0]["_Is_Received_"]))
                XResult += "<img src='/" + ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Delivery_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Received_"])) + "' alt='Loding,,,' style='Height:30px; Width:100px;' />";
            else if (Convert.ToBoolean(dt.Rows[0]["_Is_Not_Received_"]))
                XResult += "<img src='/" + ClassSaddam.FGetSignature(Convert.ToInt32(dt.Rows[0]["_ID_Delivery_"]), Convert.ToBoolean(dt.Rows[0]["_Is_Not_Received_"])) + "' alt='Loding,,,' style='Height:30px; Width:100px;' />";
            else
                XResult += "<img src='/Cpanel/loaderMin.gif' alt='Loding,,,' style='Height:30px; Width:30px;' />";

            XResult += "</p><p style='font-size: 13px; padding-right: 5px'>بتاريخ / ";
            if (Convert.ToBoolean(dt.Rows[0]["_Is_Received_"]) == false && Convert.ToBoolean(dt.Rows[0]["_Is_Not_Received_"]) == false)
                XResult += "بإنتظار التسليم";
            else if (Convert.ToBoolean(dt.Rows[0]["_Is_Received_"]) == true || Convert.ToBoolean(dt.Rows[0]["_Is_Not_Received_"]) == true)
                XResult += Convert.ToDateTime(dt.Rows[0]["_The_Purpose"]).ToString("yyyy/MM/dd");

            XResult += "</p></td></tr></table>";
            if (Convert.ToBoolean(dt.Rows[0]["_Is_Ammen_AlSondoq_"]) && Convert.ToBoolean(dt.Rows[0]["_Is_Raees_Maglis_AlEdarah_"]) && Convert.ToBoolean(dt.Rows[0]["_Is_Moder_"]) && Convert.ToBoolean(dt.Rows[0]["_Is_Storekeeper_"]) && (Convert.ToBoolean(dt.Rows[0]["_Is_Received_"]) || Convert.ToBoolean(dt.Rows[0]["_Is_Not_Received_"])))
                XResult += "<div align='left' style='margin-top: -60px'><img src='/ImgSystem/ImgSignature/الختم.png' /></div>";

            XResult += "<div><hr />";
            if (dt.Rows[0]["_Note_"].ToString() == string.Empty || dt.Rows[0]["_Note_"].ToString() == "0")
            { }
            else
                XResult += "<span><strong>* ملاحظة : </strong></span><br /><span> - " + ClassSaddam.FAlName(Convert.ToInt32(dt.Rows[0]["_ID_MosTafeed_"]), dt.Rows[0]["_ID_Type_Shipment_"].ToString());

            XResult += "</span></div><table style='width: 98%; font-size: 11px' dir='rtl' class='bl HideEdarah' id='footer'><tr><td align='Right' style='width: 34.3%'><i class='fa fa-star'></i>تاريخ الطباعة : ";
            XResult += ClassDataAccess.GetCurrentTime().ToString("dd/MM/yyyy HH:mm:ss ttt");
            XResult += "</td><td align='left' style='width: 35.3%'><i class='fa fa-star'></i>ملاحظة / أي كشط أو تعديل يُعتبر لاغي</td><td align='left' style='width: 30.3%'>";
            XResult += "<strong style='color: #076db1; font-weight: bold'><i class='fa fa-star'></i><u>هذا النموذج الكتروني معتمد</u></strong></td></tr></table>";
            XResult += "<div><img src='/view/image/LogoBottomNew2.jpg' style='width:100%; height:35px;' /></div>";

            XResult += "</div></div>";

        }
        return XResult;
    }

    private string FGetByBill(Guid XIDBill, int IDMostafeed,string XCount_Families, string XCount_Cart)
    {
        string XResult = string.Empty;
        WSM_Model_Exchange_Order_Details_ MEOD = new WSM_Model_Exchange_Order_Details_();
        MEOD.IDCheck = "GetByBill";
        MEOD.IDItem = XIDBill;
        MEOD.ID_FinancialYear = Guid.Empty;
        MEOD.ID_Donor = Guid.Empty;
        MEOD.bill_Number = 0;
        MEOD.ID_MosTafeed = 0;
        MEOD.Start_Date = string.Empty;
        MEOD.End_Date = string.Empty;
        MEOD.DataCheck = string.Empty;
        MEOD.DataCheck2 = string.Empty;
        MEOD.DataCheck3 = string.Empty;
        MEOD.IsActive = true;
        DataTable dt = new DataTable();
        WSM_Repostry_Exchange_Order_Details_ REOD = new WSM_Repostry_Exchange_Order_Details_();
        dt = REOD.BWSM_Exchange_Order_Details_Manage(MEOD);

        if (dt.Rows.Count > 0)
        {
            for (int x = 0; x <= dt.Rows.Count - 1; x++)
            {
                XResult += "<tr>";
                XResult += "<th style='width: 10px; border: thin double #808080; border-width: 1px;' align='center'>" + Convert.ToString(x + 1 ) + "</th>";
                XResult += "<th style='width: 20%; border: thin double #808080; border-width: 1px;' align='center'>" + WSM_ClassProduct.FGetCategoryByProduct(Convert.ToInt32(dt.Rows[x]["_ID_Product_"])) + "</th>";
                XResult += "<th style='width: 25%; border: thin double #808080; border-width: 1px;' align='center'>" + WSM_ClassProduct.FProductName(Convert.ToInt32(dt.Rows[x]["_ID_Product_"])) + "</th>";
                XResult += "<th style='width: 15%; border: thin double #808080; border-width: 1px;' align='center'>" + dt.Rows[x]["_Count_Product_"].ToString() + "</th>";
                XResult += "<th style='width: 20%; border: thin double #808080; border-width: 1px;' align='center'>" + dt.Rows[x]["_One_Price_"].ToString() + " <small>" + ClassSaddam.FGetMonySa() + "</small></th>";
                XResult += "<th style='width: 20%; border: thin double #808080; border-width: 1px;' align='center'>" + dt.Rows[x]["_Total_Price_"].ToString() + " <small>" + ClassSaddam.FGetMonySa()+ "</small></th>";
                XResult += "</tr>";
            }

            if (IDMostafeed == 504)
            {
                XResult += "<tr><td colspan='6'>عدد الأسر المستفيدة : ";
                XResult += XCount_Families;
                XResult += " / العدد الموزع : ";
                XResult += XCount_Cart;
                XResult += "</td></tr>";
            }
            XResult += "</tbody></table></div>";

            object sumObject;
            sumObject = dt.Compute("Sum(_Total_Price_)", string.Empty);

            List<CurrencyInfo> currencies = new List<CurrencyInfo>();
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia));
            ToWord toWord = new ToWord(Convert.ToDecimal(sumObject), currencies[Convert.ToInt32(0)]);

            XResult += "<table style='width: 100%;'><tr><td style='width: 15%; border: thin double #808080; border-width: 1px; padding: 10px' align='center'>المجموع : </td>";
            XResult += "<td style='width: 65%; border: thin double #808080; border-width: 1px;' align='center'>";
            XResult += "<span Style='text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;'>" + toWord.ConvertToArabic() + "</span>";
            XResult += "</td><td style='width: 20%; border: thin double #808080; border-width: 1px;' align='center'>";
            XResult += "<span Style='color: Red; font-size: 12px'>" + sumObject.ToString() + "<small>" + ClassSaddam.FGetMonySa() + "</small></span>";
            XResult += "</td></tr></table><hr />";

            
        }
        return XResult;
    }

}