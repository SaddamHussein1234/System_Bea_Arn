<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCFooterBill.ascx.cs" Inherits="Cpanel_CAttach_WUCFooterBill" %>
<table style='width: 98%;' dir='rtl' class='bl'>
    <tr>
        <td align='Right' style='width: 34.3%'><i class='fa fa-star'></i>تاريخ الطباعة : 
             <%= Library_CLS_Arn.Saddam.ClassSaddam.GetCurrentTime().ToString("dd/MM/yyyy HH:mm:ss ttt") %>   
        </td>
        <td align='left' style='width: 35.3%'><i class='fa fa-star'></i>ملاحظة / أي كشط أو تعديل يُعتبر لاغي
        </td>
        <td align='left' style='width: 30.3%'>
            <strong style='color: #076db1'><i class='fa fa-star'></i><u>هذا النموذج الكتروني معتمد</u></strong>
        </td>
    </tr>
</table>
<div>
<img src='/view/image/LogoBottomNew2.jpg' style='width: 100%; height: 35px;' /></div>
