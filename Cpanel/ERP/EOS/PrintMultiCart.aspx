<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintMultiCart.aspx.cs" Inherits="Cpanel_ERP_EOS_PrintMultiCart" %>

<%@ Import Namespace="Library_CLS_Arn.WSM" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>

<!DOCTYPE html>

<html lang="ar" dir="rtl" style="font-family: sans-serif; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; font-size: 10px; -webkit-tap-highlight-color: transparent;" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>النظام الإلكتروني</title>
    <script>window.print();</script>

    <link href='/fonts/font-awesome.css' rel='stylesheet' />
    <link href='/view/javascript/font-awesome/css/font-awesome.min.css' rel='stylesheet' />
    <script src='/view/Chart/fusioncharts.js'></script>
    <script src='/view/Chart/fusioncharts.charts.js'></script>

    <style type="text/css">
        .MarginTop_ {
            margin-top: -10px;
        }

        .hr {
            display: block;
            height: 1px;
            border: 0;
            border-top: 1px solid #ccc;
            margin: 1em 0;
            padding: 0;
        }

        body, h1, h2, h3, h4, h5, h6, p, ul, a, table, input {
            font-family: 'Droid Arabic Kufi', serif;
            font-size: 11px
        }

        span {
            font-size: 14px
        }

        .footable1 {
            border-spacing: 0;
            width: 100%;
            border: solid #ccc 1px;
            -moz-border-radius: 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px;
            font-size: 12px;
            font-weight: bold;
            color: #000000;
            text-align: right;
        }

            .footable1 > tbody > tr > td, .footable1 > thead > tr > th {
                border-left: 1px solid #ccc;
                border-top: 1px solid #ccc;
                padding: 2px;
                text-align: right;
                color: #000000;
                font-size: 11px;
            }

            .footable1 > thead > tr > th, .footable1 > thead > tr > td {
                background-color: #000000;
                border-top: 0;
            }

                .footable1 > thead > tr > th:first-child, .footable1 > thead > tr > td:first-child {
                    -moz-border-radius: 6px 0 0;
                    -webkit-border-radius: 6px 0 0;
                    border-radius: 6px 0 0;
                }

        .sty {
            color: #FFFFFF
        }

        .fo {
            font-size: 12px;
        }

        footer {
            position: absolute;
            bottom: 0;
            width: 100%;
        }

        .WidthMaglis {
            float: right;
            Width: 19%;
            padding-right: 5px;
        }

        .WidthMaglis24 {
            float: right;
            Width: 24%;
            padding-right: 5px;
        }

        .HideThis {
            display: none;
        }

        .WidthText3 {
            float: right;
            Width: 15%;
            padding-right: 5px;
        }

        .StyleTD {
            text-align: center;
            padding: 5px;
            border: double;
            border-width: 2px;
            border-color: #a1a0a0;
        }

        @font-face {
            font-family: 'Alwatan';
            font-size: 18px;
            src: url(../fonts/AlWatanHeadlines-Bold.ttf);
        }

        .Width20Percint {
            float: right;
            Width: 20%;
            padding-right: 5px;
        }

        .Width10Percint {
            float: right;
            Width: 10%;
            padding-right: 5px;
        }

        .WidthText1 {
            float: right;
            Width: 24%;
            padding-right: 5px;
        }

        @media print {
            .th {
                color: black;
                background-color: lightgrey;
            }

            thead {
                display: table-header-group;
            }

            tbody {
                display: table-row-group;
            }
        }

        .WidthText2 {
            float: right;
            Width: 25%;
            padding-right: 5px;
        }

        .hr {
            display: block;
            height: 1px;
            border: 0;
            border-top: 1px solid #ccc;
            margin: 1em 0;
            padding: 0;
        }

        body, h1, h2, h3, h4, h5, h6, p, ul, a, table, input {
            font-family: 'Droid Arabic Kufi', serif;
            font-size: 11px
        }

        .WidthText30 {
            float: right;
            Width: 15%;
            padding-right: 5px;
            height: 100px
        }

        .WidthImg {
            Width: 100%;
            padding-right: 5px;
            height: 90px
        }

        .panel-title {
            margin-top: 0;
            margin-bottom: 0;
            font-size: 14px;
            color: inherit;
        }

        .StyleTD {
            text-align: center;
            padding: 5px;
            border: double;
            border-width: 2px;
            border-color: #a1a0a0;
        }

        .WidthText {
            float: right;
            Width: 12%;
            padding-right: 5px;
        }

        .WidthText3 {
            float: right;
            Width: 16%;
            padding-right: 5px;
        }

        .C31_5 {
            float: right;
            Width: 31.5%;
            padding-left: 5px;
        }

        .WidthText2 {
            float: right;
            Width: 32.5%;
            padding-left: 5px;
        }

        .WidthText1 {
            float: right;
            Width: 21%;
            padding-right: 5px;
        }

        .WidthText4 {
            float: right;
            Width: 50%;
        }

        .WidthText5 {
            float: right;
            Width: 100%;
        }

        .footable1 {
            border-spacing: 0;
            width: 100%;
            border: solid #ccc 1px;
            -moz-border-radius: 6px;
            -webkit-border-radius: 6px;
            border-radius: 6px;
            font-size: 12px;
            font-weight: bold;
            color: #000000;
            text-align: right;
        }

            .footable1 > tbody > tr > td, .footable1 > thead > tr > th {
                border-left: 1px solid #ccc;
                border-top: 1px solid #ccc;
                padding: 2px;
                text-align: right;
                color: #000000;
                font-size: 11px;
            }

            .footable1 > thead > tr > th, .footable1 > thead > tr > td {
                background-color: #000000;
                border-top: 0;
            }

                .footable1 > thead > tr > th:first-child, .footable1 > thead > tr > td:first-child {
                    -moz-border-radius: 6px 0 0;
                    -webkit-border-radius: 6px 0 0;
                    border-radius: 6px 0 0;
                }

        .sty {
            color: #FFFFFF
        }

        .fo {
            font-size: 12px;
        }

        footer {
            position: absolute;
            bottom: 0;
            width: 100%;
        }

        .Width20Percint {
            float: right;
            Width: 20%;
            padding-right: 5px;
        }

        .Width10Percint {
            float: right;
            Width: 10%;
            padding-right: 5px;
        }

        .StyleTD {
            text-align: center;
            padding: 2px;
            border: double;
            border-width: 2px;
            border-color: #a1a0a0;
        }

        .WidthText74 {
            float: right;
            Width: 74%;
            padding-left: 5px;
        }

        .WidthText40 {
            float: right;
            Width: 40%;
            padding-right: 5px;
        }

        .WidthText4 {
            float: right;
            Width: 49%;
        }

        .page {
            page-break-after: always;
        }
    </style>

    <style type="text/css">
        @page {
            margin: 11pt 0
        }

        @media print {
            @page {
                size: 297mm 210mm;
                margin: 5mm;
                size: a4;
            }

            p {
                margin: 0px;
            }

            html .page-container, body .page-container {
                padding: 10pt 20pt
            }

            .text-tiny {
                font-size: 10pt
            }

            .title.large {
                font-size: 14pt
            }

            .title.medium {
                font-size: 13pt
            }

            .table-content thead tr th {
                background-color: #f8f8f8 !important;
                -webkit-print-color-adjust: exact
            }

            .client-area {
                background-color: #f5f5f7;
                -webkit-print-color-adjust: exact
            }

            img, tr {
                page-break-inside: avoid
            }

            .price-before {
                text-decoration: line-through;
                color: #bbb;
                padding: 0 0 0 8px
            }

            .inv-store-logo {
                max-width: 120px;
                max-height: 50px
            }
        }
        footer {position: absolute;bottom: 0;width: 100%;} 
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Image ID="Image1" runat="server" Visible="false" />
        <asp:Repeater ID="RPTBillCart" runat="server" OnItemDataBound="RPTBillCart_ItemDataBound" Visible="false">
            <ItemTemplate>
                <div class='page'>
                    <div class="HideNow">
                        <uc1:wucheader runat="server" id="WUCHeader" />
                    </div>
                    <div class="">
                        <div align="center" class="w">
                            <div class="table table-responsive">
                                <table style="width: 100%; background-color: #ffffff; color: #393939">
                                    <tr>
                                        <td style="border: thin double #808080; border-width: 1px; width: 45%">
                                            <asp:HiddenField ID="HFXID" runat="server" Value='<%# Eval("_ID_Item_") %>' />
                                            <asp:HiddenField ID="HFIDMostafeed" runat="server" Value='<%# Eval("_ID_MosTafeed_") %>' />
                                            <asp:HiddenField ID="HFIDBill" runat="server" Value='<%# Eval("_bill_Number_") %>' />
                                            <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" 
                                                Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"
                                                Text='<%# /*ClassSaddam.FAlTypeEvint(Convert.ToInt32(Eval("_ID_Type_Shipment_"))) +*/ " أمر صرف لمستفيد لمشروع (" + Request.QueryString["Name"] + ")" %>'></asp:TextBox>
                                        </td>
                                        <td style="border: thin double #808080; border-width: 1px; width: 20%">
                                            <table style="width: 100%; font-size: 12px">
                                                <tr>
                                                    <td align="left" style="width: 60%; font-family: 'Alwatan'; font-size: 18px;">رقم الفاتورة / 
                                                    </td>
                                                    <td style="width: 40%; font-family: 'Alwatan'; font-size: 18px;">
                                                        <%# Eval("_bill_Number_") %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="border: thin double #808080; border-width: 1px; width: 35%">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="left" style="width: 20%; font-size: 12px">التاريخ / 
                                                    </td>
                                                    <td style="width: 80%">
                                                        <%# Eval("_CreatedDate_", "{0:yyyy/MM/dd}") + "مـ - " /*+ ClassSaddam.FConvertDateToHijri(Convert.ToDateTime(Eval("_CreatedDate_"))) + "هـ" */%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div style="float: right; padding: 10px 10px 0 10px;" class="w">
                        <p style="font-size: 13px">
                            السيد / أمين المستودع<asp:Label ID="lblAmeenAlmosTodaa" runat="server" Visible="false"></asp:Label>
                        </p>
                        <p style="font-size: 13px">
                            <asp:Label ID="lblSarf" runat="server" Text="بموجبه يتم الصرف للسيد / "></asp:Label>
                        </p>
                    </div>
                    <div style="float: left; padding: 10px 0 0 10px" class="w">
                        <table style="font-size: 12px">
                            <tr>
                                <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">مدخل البيانات :
                                    <%# Eval("FirstName") %>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                    <%# Eval("_CreatedDate_", "{0:yyyy/MM/dd}") %>
                                </td>
                            </tr>
                            <tr runat="server" id="IDUpdate" visible="false">
                                <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">
                                    <asp:Label ID="lblDataEntryEdit" runat="server"></asp:Label>
                                </td>
                                <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                        <asp:Label ID="lblDateEntryEdit" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div align='center' class="w">
                        <img src='<%# Class_QRScan.FGetQRCodePath(XNAmeServer +
                    "/Cpanel/ERP/EOS/In_Kind_Donation/PageView.aspx?IDUniq=" + Request.QueryString["IDUniq"] + "&ID=" + Eval("_bill_Number_") + "&XID=" + Eval("_ID_MosTafeed_") +
                    "&XIDCate=" + Eval("_ID_Project_") + "&IsCart=" + Eval("_Is_Cart_") + "&IsDevice=" + Eval("_Is_Device_") +
                    "&IsTathith=" + Eval("_Is_Tathith_") + "&IsTalef=" + Eval("_Is_Talef_"), Image1) %>' alt='Loding' style='Height:90px; Width:90px;' />
                    </div>
                    <table style="width: 100%">
                        <tr runat="server" id="IDUserDetails">
                            <td style="width: 40%; border: thin double #808080; border-width: 1px; padding: 5px" align="center">
                                <p style="font-size: 11px">
                                    <%# Eval("_Note_").ToString() == string.Empty || Eval("_Note_").ToString() == "0" ?
                                        Eval("NameMostafeed") : Eval("_Note_")
                                    %>
                                </p>
                            </td>
                            <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                <p style="font-size: 11px">
                                    الجوال :
                                           0<%# Eval("PhoneNumber") %>
                                </p>
                            </td>
                            <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                <p style="font-size: 11px">
                                    القرية :
                                        <%# Eval("AlQriah") %>
                                </p>
                            </td>
                            <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                <p style="font-size: 11px">
                                    رقم الملف :
                                            <%# Eval("_ID_MosTafeed_") %>
                                </p>
                            </td>
                        </tr>
                    </table>
                    <div style="font-family: 'Alwatan'; font-size: 18px; float: right">
                        الأصناف الموضحة أدناه : 
                    </div>
                    <%--<div align="left" style="font-family: 'Alwatan'; font-size: 18px">
                        <%# Eval("_The_Initiative_").ToString() != "1" ?
                            ClassInitiatives.FGetInitiativesName(Convert.ToInt32(Eval("_The_Initiative_")))
                            :
                            ""
                        %>
                    </div>--%>
                    <span class="hr"></span>
                    <div class="table table-responsive">
                        <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:GridView ID="GVDeedDonationInKind" runat="server" AutoGenerateColumns="False" DataKeyNames="_IDItem"
                                            Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
                                            UseAccessibleHeader="False">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="10px" Visible="false">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="_IDItem" HeaderText="_IDItem" InsertVisible="False" ReadOnly="True"
                                                    SortExpression="_IDItem" Visible="false" />
                                                <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="الصنف" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# Eval("CategoryName")%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="المنتج" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# Eval("ProductName")%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="التجزئة" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# Convert.ToBoolean(Eval("_Is_There__Partition_")) ?
                                                            Eval("_Count__Partition_") + " / <small>" + Eval("UnitName") + "</small>"
                                                            :
                                                            " 1 " + " / <small>" + Eval("UnitName") + "</small>"
                                                        %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="العدد" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCount" runat="server" Font-Size="12px" Text='<%# Eval("_Count_Product_")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="السعر الفردي" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <%# Eval("_One_Price_")%> <small><%# XMony %></small>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="السعر الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" Text='<%# Eval("_Total_Price_")%>'></asp:Label>
                                                        <small><%# XMony %></small>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderText="أُضيف من قبل" Visible="false">
                                                    <ItemTemplate>
                                                        <%# ClassQuaem.FAlBaheth((Int32) (Eval("_CreatedBy_")))%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="بتاريخ" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate_Add" runat="server"
                                                            Text='<%# Eval("_CreatedDate_", "{0:dd/MM/yyyy}") + " " + Eval("_CreatedDate_", "{0:HH:mm tt}")  %>' Font-Size="11px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                                PreviousPageText=" السابق - " PageButtonCount="30" />
                                            <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                            <RowStyle CssClass="rows"></RowStyle>
                                            <RowStyle CssClass="rows"></RowStyle>
                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <%# Convert.ToInt32(Eval("_ID_MosTafeed_")) == 504 ?
                                            "<small>عدد الأسر المستفيدة : " + Eval("_Count_Families_") + " / العدد الموزع : " + Eval("_Count_Cart_") + "</small>"
                                            :
                                            ""
                                        %>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 15%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">المجموع : 
                            </td>
                            <td style="width: 65%; border: thin double #808080; border-width: 1px;" align="center">
                                <asp:TextBox ID="txtSumWord" runat="server" class="form-control" placeholder="المبلغ"
                                    Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                            </td>
                            <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                <asp:Label ID="lblTotalPrice" runat="server" Text="0"></asp:Label>
                                 <small><%# XMony %></small> 
                            </td>
                        </tr>
                    </table>
                    <hr />
                    <div class="table table-responsive">
                        <div class="WidthText5" style="border: thin double #808080; border-width: 1px;" align="center">
                            <table style="width: 100%; margin: 5px; font-size: 12px">
                                <tr>
                                    <td style="width: 45%;">مدير الجمعية : 
                                    </td>
                                    <td style="width: 55%;">
                                        <%# Convert.ToBoolean(Eval("_Is_Moder_")) ?
                                            "<img src='/" + Eval("SignatureModer") + "' alt='' Width='100' Height='30px' />"
                                            :
                                            "<img src='/Cpanel/loaderMin.gif' alt='' Width='30' Height='30px' />"
                                        %>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class=" C31_5" style="border: thin double #808080; border-width: 1px; display:none;" align="center">
                            <%--<table style="width: 100%; margin: 5px; font-size: 12px">
                                <tr>
                                    <td style="width: 45%;">المشرف المالي : 
                                    </td>
                                    <td style="width: 55%;">
                                        <%# Convert.ToBoolean(Eval("_Is_Ammen_AlSondoq_")) ?
                                            "<img src='"+ClassSaddam.FGetSignature(Convert.ToInt32(Eval("_ID_Ammen_AlSondoq_")), Convert.ToBoolean(Eval("_Is_Ammen_AlSondoq_")))+"' alt='' Width='100' Height='30px' />"
                                            :
                                            "<img src='/Cpanel/loaderMin.gif' alt='' Width='30' Height='30px' />"
                                        %>
                                    </td>
                                </tr>
                            </table>--%>
                        </div>
                        <div class=" C31_5" style="border: thin double #808080; border-width: 1px; display:none;" align="center">
                            <%--<table style="width: 100%; margin: 5px; font-size: 12px">
                                <tr>
                                    <td style="width: 45%;">رئيس الجمعية : 
                                    </td>
                                    <td style="width: 55%;">
                                        <%# Convert.ToBoolean(Eval("_Is_Raees_Maglis_AlEdarah_")) ?
                                            "<img src='"+ClassSaddam.FGetSignature(Convert.ToInt32(Eval("_ID_Raees_Maglis_AlEdarah_")), Convert.ToBoolean(Eval("_Is_Raees_Maglis_AlEdarah_")))+"' alt='' Width='100' Height='30px' />"
                                            :
                                            "<img src='/Cpanel/loaderMin.gif' alt='' Width='30' Height='30px' />"
                                        %>
                                    </td>
                                </tr>
                            </table>--%>
                        </div>
                    </div>
                    <hr />
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 50%; border: thin double #808080; border-width: 1px;">
                                <p style="font-size: 13px">
                                    <div align="center" style="font-size: 13px">
                                        تم الصرف
                                            <asp:CheckBox ID="CBDone" runat="server" Enabled="false" Checked=<%# Eval("_Is_Done_") %> />
                                        / لم يتم الصرف بعد
                                            <asp:CheckBox ID="CBNotDone" runat="server" Enabled="false" Checked=<%# Eval("_Is_Not_Done_") %> />
                                    </div>
                                </p>
                                <p style="font-size: 13px; padding-right: 5px">
                                    أمين المستودع / 
                                    <%--<%# Convert.ToBoolean(Eval("_Is_Storekeeper_")) ?
                                        "<img src='"+ClassSaddam.FGetSignature(Convert.ToInt32(Eval("_ID_Storekeeper_")), Convert.ToBoolean(Eval("_Is_Storekeeper_")))+"' alt='' Width='100' Height='30px' />"
                                        :
                                        "<img src='/Cpanel/loaderMin.gif' alt='' Width='30' Height='30px' />"
                                    %>--%>
                                    <%# Convert.ToBoolean(Eval("_Is_Storekeeper_")) ?
                                        "<img src='/" + Eval("SignatureStorekeeper") + "' alt='' Width='100' Height='30px' />"
                                        :
                                        "<img src='/Cpanel/loaderMin.gif' alt='' Width='30' Height='30px' />"
                                    %>
                                </p>
                                <p style="font-size: 13px; padding-right: 5px">
                                    بتاريخ / 
                                    <%# Convert.ToBoolean(Eval("_Is_Done_")) == false && Convert.ToBoolean(Eval("_Is_Not_Done_")) == false ?
                                        "بإنتظار الملاحظة"
                                        :
                                        Eval("_Date_Storekeeper_Allow_", "{0:yyyy/MM/dd}")
                                    %>
                                </p>
                            </td>
                            <td style="width: 50%; border: thin double #808080; border-width: 1px;">
                                <p style="font-size: 13px">
                                    <div align="center" style="font-size: 13px">
                                        تم التسليم
                                            <asp:CheckBox ID="CBReceived" runat="server" Enabled="false" Checked=<%# Eval("_Is_Received_") %> />
                                        / لم يتم التسليم بعد
                                            <asp:CheckBox ID="CBNotReceived" runat="server" Enabled="false" Checked=<%# Eval("_Is_Not_Received_") %> />
                                        <%# Convert.ToBoolean(Eval("_Is_Not_Received_")) ?
                                            Eval("_Note_Not_Received_").ToString()
                                            :
                                            ""
                                        %>
                                    </div>
                                </p>
                                <p style="font-size: 13px; padding-right: 5px">
                                    إسم الباحث / 
                                    <%# Eval("NameBaheth") %>
                                    <%# Convert.ToBoolean(Eval("_Is_Received_")) && Convert.ToBoolean(Eval("_Is_Not_Received_")) == false ?
                                        "<img src='/" + Eval("SignatureBaheth") + "' alt='' Width='100' Height='30px' />"
                                        :
                                        ""
                                    %>
                                    <%# Convert.ToBoolean(Eval("_Is_Received_")) == false && Convert.ToBoolean(Eval("_Is_Not_Received_")) ?
                                        "<img src='/" + Eval("SignatureBaheth") + "' alt='' Width='100' Height='30px' />"
                                        :
                                        ""
                                    %>
                                    <%# Convert.ToBoolean(Eval("_Is_Received_")) == false && Convert.ToBoolean(Eval("_Is_Not_Received_")) == false ?
                                        "<img src='/Cpanel/loaderMin.gif' alt='' Width='30' Height='30px' /> <p style='font-size: 13px; padding-right: 5px'> بإنتظار التسليم </p>"
                                        :
                                        ""
                                    %>
                                </p>
                                <%# Convert.ToBoolean(Eval("_Is_Received_")) || Convert.ToBoolean(Eval("_Is_Not_Received_")) ?
                                        "<p style='font-size: 13px; padding-right: 5px'> بتاريخ / "+ Eval("_The_Purpose", "{0:yyyy/MM/dd}") + " </p>" 
                                        :
                                        ""
                                %>
                            </td>
                        </tr>
                    </table>
                    <%# Convert.ToBoolean(Eval("_Is_Ammen_AlSondoq_")) && Convert.ToBoolean(Eval("_Is_Raees_Maglis_AlEdarah_")) && Convert.ToBoolean(Eval("_Is_Moder_")) && Convert.ToBoolean(Eval("_Is_Storekeeper_")) && (Convert.ToBoolean(Eval("_Is_Received_")) || Convert.ToBoolean(Eval("_Is_Not_Received_"))) ?
                        "<div align='left' style='margin-top: -60px'><img src='/ImgSystem/ImgSignature/الختم.png' /></div>" 
                        :
                        ""
                    %>
                    <%# Eval("_Note_").ToString() == string.Empty || Eval("_Note_").ToString() == "0" ?
                        "" 
                        :
                        "<div><hr /><span><strong>* ملاحظة : </strong></span><br /><span>-" + ClassSaddam.FAlName(Convert.ToInt32(Eval("_ID_MosTafeed_")), Eval("_ID_Type_Shipment_").ToString()) + "</span></div>"
                    %>
                    <div class="HideNow">
                        <uc1:wucfooterbottom runat="server" id="WUCFooterBottom" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
