<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageViewMulti.aspx.cs" Inherits="Cpanel_ERP_OM_PageViewMulti" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterBill.ascx" TagPrefix="uc1" TagName="WUCFooterBill" %>

<!DOCTYPE html>

<html lang="ar" dir="rtl" style="font-family: sans-serif; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; font-size: 10px; -webkit-tap-highlight-color: transparent;" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>النظام الإلكتروني</title>
    <script>window.print();</script>
    <link href='/fonts/font-awesome.css' rel='stylesheet' />
    <link href='/view/javascript/font-awesome/css/font-awesome.min.css' rel='stylesheet' />

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
            margin: 11pt -11pt
        }

        @media print {
            @page {
                
                size: a4; size: 210mm 297mm;
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
        <asp:Image ID="ImgQRCode" runat="server" alt='QR Code' Visible="false" />
        <asp:Panel ID="pnlDataCashing" runat="server" Direction="RightToLeft" Visible="false">
            <asp:Repeater ID="RPTCashing" runat="server">
                <ItemTemplate>
                    <div class='page'>
                        <header class="hide">
                            <img src='/view/image/LogoTitleNew2.jpg' style='width: 100%; height: 100px;' />
                        </header>
                        <div class="">
                            <div align="center" class="w">
                                <table style="width: 100%; background-color: #ffffff; color: #393939">
                                    <tr>
                                        <td style="border: thin double #808080; border-width: 1px; width: 45%">
                                            <div align="center">
                                                <asp:Label ID="txtTitle" runat="server" Text="سند صرف" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                                            </div>
                                        </td>
                                        <td style="border: thin double #808080; border-width: 1px; width: 20%; font-family: 'Alwatan'; font-size: 18px;">
                                            <span style="padding-right: 10px; font-size: 18px;">رقم الفاتورة /  </span>
                                            <%# Eval("_bill_Number_") %>
                                        </td>
                                        <td rowspan="2" style="border: thin double #808080; border-width: 1px; width: 35%">
                                            <div align='center' class="w">

                                                <img src='<%# Class_QRScan.FGetQRCodePath(XNAmeServer +
                                                    "/ar/Cashing/PageView.aspx?ID=" + Eval("_bill_Number_") + "&IDUniq=" + Request.QueryString["IDUniq"] 
                                                        , ImgQRCode) %>' alt='Loding' style='Height:90px; Width:90px;' />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="border: thin double #808080; border-width: 1px; width: 35%">
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
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 30%;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 46%;">
                                                <span style="font-size: 13px">بموجبة يتم الصرف / 
                                                </span>
                                                <asp:DropDownList ID="DLType" runat="server" ValidationGroup="GPrint">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="للسيد">للسيد</asp:ListItem>
                                                    <asp:ListItem Value="للسيدة">للسيدة</asp:ListItem>
                                                    <asp:ListItem Value="للسادة">للسادة</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="DLType" ErrorMessage="* إجباري" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="REVPrint" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:Label ID="lblType" runat="server" Visible="false"></asp:Label>
                                                <div style="font-family: 'Alwatan'; font-size: 17px" align="center">
                                                    <asp:Label ID="lblFromDonor" runat="server"></asp:Label>
                                                    <%# Eval("_Name_") %>
                                                </div>
                                            </td>
                                            <td style="width: 33%; display: none;">
                                                <table style="font-size: 12px; margin: 10px; width: 90%; display: none;">
                                                    <tr>
                                                        <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">مدخل البيانات :
                                                                    <%# Eval("NameAdmin") %>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                                                    <%# Eval("_ModifiedDate_", "{0:yyyy/MM/dd}") %>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="IDUpdate" visible="false" style="display: none">
                                                        <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">
                                                            <asp:Label ID="lblDataEntryEdit" runat="server" Font-Size="12px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px; display: none">بتاريخ :
                                                                    <asp:Label ID="lblDateEntryEdit" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 70%;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 20%; border: thin double #808080; border-width: 1px; padding: 5px" align="center">
                                                <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Eval("_The_Mony_") %>' Style='color: Red; font-size: 13px'></asp:Label>
                                                <asp:Label ID="Label150" runat="server" Text='<%# XMony %>' Style='color: Red; font-size: 12px'></asp:Label>
                                            </td>
                                            <td style="width: 80%; border: thin double #808080; border-width: 1px;" align="center">
                                                <asp:Label ID="lblSumWord" runat="server" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"
                                                    Text='<%# FConvertToWord(Eval("_The_Mony_").ToString()) %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 20%; border: thin double #808080; border-width: 1px; padding: 5px" align="center">طريقة الدفع 
                                            </td>
                                            <td style="width: 80%; border: thin double #808080; border-width: 1px;" align="center">
                                                <%# Convert.ToBoolean(Eval("_IsCash_Money_")) ?
                                                    "<input id='CBCash_Money_' type='checkbox' Checked='" + Eval("_IsCash_Money_") + "' disabled /> <span>نقداً </span>" : ""
                                                %>
                                                <%# Convert.ToBoolean(Eval("_IsShayk_Bank_")) ?
                                                    "<input id='CBShayk_Bank' type='checkbox' Checked='" + Eval("_IsShayk_Bank_") + "' disabled /> <span style='font-size: 11px;'>شيك رقم : </span>" +
                                                    " / رقم الشيك : " + Eval("_Number_Shayk_Bank_").ToString() + "- بتاريخ : " + Eval("_Date_Shayk_", "{0:yyyy/MM/dd}") + 
                                                    "- على : " + Eval("_For_Bank_") : ""
                                                %>
                                                <%# Convert.ToBoolean(Eval("_Transfer_On_Account_")) ?
                                                    "<input id='CBShayk_Bank' type='checkbox' Checked='" + Eval("_Transfer_On_Account_") + "' disabled /> <span style='font-size: 11px;'>إيداع بنكي على :</span>" +
                                                    Eval("_For_Bank_Transfer_").ToString() + " / حساب رقم : " + Eval("_Number_Account_") + 
                                                    "- تاريخ :" + Eval("_Date_Bank_Transfer_", "{0:yyyy/MM/dd}") : ""
                                                %>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <div style="margin: 10px">
                            <span style="font-size:12px;">وذلك لغرض / <%# Eval("_Name_Ar_") + " / " + Library_CLS_Arn.OM.Repostry.Repostry_Main_Items_.FGetNameByID(new Guid(Eval("_ID_Sub_Item_").ToString())) %> </span>
                            / 
                            <span style="font-size:12px;"><%# Eval("_Note_Bill_") %> </span>
                        </div>
                        <%# Convert.ToBoolean(Eval("_IsAmmenAlSondoq_")) && Convert.ToBoolean(Eval("_IsRaeesMaglisAlEdarah_")) ? 
                        "<div align='left' style='margin-top: -60px'><img src='/ImgSystem/ImgSignature/الختم.png' /></div>" 
                        : 
                        "<div align='left' style='margin-top: -60px'><img src='/Cpanel/loader.gif' width='113' /></div>" %>
                        <table style="width: 100%; margin-top: -60px; font-size: 12px">
                            <tr>
                                <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                                    <div style="margin: 0 0 5px 0;">
                                        <span style="font-family: 'Alwatan'; font-size: 17px">المشرف المالي</span>
                                        <div align="right" style="margin-right: 5px;">
                                            الإسم :
                                            <%# Eval("NameAmmenAlSondoq") %><br />
                                            التوقيع :
                                            <%# Convert.ToBoolean(Eval("_IsAmmenAlSondoq_")) ?
                                                "<img src='/" + Eval("ImgAmmenAlSondoq") + "' alt='' Width='100' Height='30px' />"
                                                :
                                                "<img src='/Cpanel/loaderMin.gif' alt='' Width='30' Height='30px' />"
                                            %>
                                            <%# Eval("_IDAmmen_Date_Allow_", "{0:yyyy/MM/dd}") %>
                                        </div>
                                    </div>
                                </td>
                                <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                                    <div style="margin: 0 0 5px 0;">
                                        <span style="font-family: 'Alwatan'; font-size: 17px">رئيس مجلس الإدارة</span>
                                        <div align="right" style="margin-right: 5px;">
                                            الإسم :
                                            <%# Eval("NameRaeesMaglis") %><br />
                                            التوقيع :
                                            <%# Convert.ToBoolean(Eval("_IsRaeesMaglisAlEdarah_")) ?
                                                "<img src='/" + Eval("ImgRaeesMaglis") + "' alt='' Width='100' Height='30px' />"
                                                :
                                                "<img src='/Cpanel/loaderMin.gif' alt='' Width='30' Height='30px' />"
                                            %>
                                            <%# Eval("_IDRaees_Date_Allow_", "{0:yyyy/MM/dd}") %>
                                        </div>
                                    </div>
                                </td>
                                <td style="width: 33%; border: thin double #808080; border-width: 1px;" align="center">
                                    <div style="margin: 0 0 5px 0;">
                                        <span style="font-family: 'Alwatan'; font-size: 17px">المستلم</span>
                                        <div align="right" style="margin-right: 5px;">
                                            الإسم :
                                            <%# Eval("_Name_") %>
                                        </div>
                                        <asp:Label ID="txtCoustmoer" runat="server" Text="وقع صورة طبق الأصل"
                                             Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div class="hide">
                            <uc1:wucfooterbill runat="server" id="WUCFooterBill" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Panel>
        <asp:Panel ID="pnlNullCashing" runat="server" Visible="False">
            <br />
            <br />
            <br />
            <br />
            <br />
            <div align="center">
                <h3 style="font-size: 20px">لا توجد نتائج
                </h3>
            </div>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </asp:Panel>
    </form>
</body>
</html>
