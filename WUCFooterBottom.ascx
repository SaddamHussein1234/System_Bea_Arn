<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCFooterBottom.ascx.cs" Inherits="WUCFooterBottom" %>
<table style='width: 98%; font-size: 11px' dir='rtl' class='bl HideEdarah' id="footer">
    <tr>
        <td align='Right' style='width: 34.3%'><i class='fa fa-star'></i>تاريخ الطباعة : 
    <asp:Label ID="lblDatePrint" runat="server" Font-Size="12px"></asp:Label>
        </td>
        <td align='left' style='width: 35.3%'><i class='fa fa-star'></i>ملاحظة / أي كشط أو تعديل يلغي البيان
        </td>
        <td align='left' style='width: 30.3%'>
            <strong style='color: #076db1; font-weight: bold'><i class='fa fa-star'></i><u>هذا النموذج الكتروني معتمد</u></strong>
        </td>
    </tr>
</table>
<div><img src='<%=ResolveUrl("~/view/image/LogoBottomNew2.jpg?ID=12346")%>' style='width:100%; height:35px;' /></div>