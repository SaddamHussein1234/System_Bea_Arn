<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageView.aspx.cs" Inherits="ar_Zakat_PageView" %>

<%@ Register Src="~/Shaerd/CPanelManageZakat/PageView.ascx" TagPrefix="uc1" TagName="PageView" %>

<!DOCTYPE html>

<html  lang="ar" dir="rtl" style="font-family: sans-serif; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; font-size: 10px;-webkit-tap-highlight-color: transparent;" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        @page {
            margin: 11pt 0
        }

        @media print {
            @page {
                size: 297mm 210mm;
                margin: 5mm;
                size: A4;
            }
        }
    </style>
    <title>النظام الإلكتروني</title><script>window.print();</script>
    <link href='/fonts/font-awesome.css' rel='stylesheet' />
    <link href='/view/javascript/font-awesome/css/font-awesome.min.css' rel='stylesheet' />
    <style>.HideThis{display:none;} @font-face {font-family: 'Alwatan';font-size:18px;src: url(/fonts/AlWatanHeadlines-Bold.ttf);} .WidthText2 {float: right;Width: 25%;padding-right: 5px;} .hr {display: block;height: 1px;border: 0;border-top: 1px solid #ccc;margin: 1em 0;padding: 0;}  body , h1 , h2 , h3 ,h4 ,h5 ,h6 ,p  ,ul ,a ,table , input{font-family: 'Droid Arabic Kufi', serif; font-size:11px} .WidthText30 {float: right;Width: 15%;padding-right: 5px; height:100px}.WidthImg {Width: 100%;padding-right: 5px; height:90px} .panel-title {margin-top: 0;margin-bottom: 0;font-size: 14px;color: inherit; } .StyleTD{text-align:center;padding:5px;border:double;border-width:2px;border-color:#a1a0a0;} .WidthText {float: right;Width: 12%;padding-right: 5px;}.WidthText3 {float: right;Width: 16%;padding-right: 5px;}.WidthText2 {float: right;Width: 31.5%;padding-left: 5px;}.WidthText1 {float: right;Width: 21%;padding-right: 5px;}.WidthText4 {float: right;Width: 50%;} .WidthText5 {float: right;Width: 100%;} .footable1 {border-spacing: 0; width: 100%; border: solid #ccc 1px; -moz-border-radius: 6px; -webkit-border-radius: 6px; border-radius: 6px; font-size: 12px; font-weight:bold; color: #000000; text-align:right;} .footable1 > tbody > tr > td, .footable1 > thead > tr > th { border-left: 1px solid #ccc; border-top: 1px solid #ccc; padding: 2px; text-align: right; color: #000000;font-size: 11px;} .footable1 > thead > tr > th, .footable1 > thead > tr > td { background-color: #000000; border-top: 0; } .footable1 > thead > tr > th:first-child, .footable1 > thead > tr > td:first-child {-moz-border-radius: 6px 0 0; -webkit-border-radius: 6px 0 0; border-radius: 6px 0 0; } .sty{color:#FFFFFF} .fo{font-size:12px;} footer {position: absolute;bottom: 0;width: 100%;} .Width20Percint {float: right;Width: 20%;padding-right: 5px;} .Width10Percint {float: right;Width: 10%;padding-right: 5px;} .StyleTD {text-align: center;padding: 2px;border: double;border-width: 2px;border-color: #a1a0a0;} .WidthText74 {float: right;Width: 74%;padding-left: 5px;} .WidthText40 {float: right;Width: 40%;padding-right: 5px;} .WidthText4 {float: right;Width: 49%;} .C31_5 {float: right;Width: 31.5%;padding-left: 5px;} @page {margin: 11pt 0} @media print { @page { size: 297mm 210mm; margin: 5mm;size: A4; } }</style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="content">
            
            <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                <div><img src='/view/image/LogoTitleNew2.jpg' style='width:100%; height:100px;' /></div>
                <uc1:PageView runat="server" ID="PageView" />
                <div><%= Library_CLS_Arn.Saddam.ClassPrint_Helper.FGetFooter() %></div>
            </asp:Panel>
            <style type="text/css">
                .modal-open {
                    overflow: hidden
                }

                .modal {
                    position: fixed;
                    top: 0;
                    right: 0;
                    bottom: 0;
                    left: 0;
                    z-index: 1050;
                    display: none;
                    overflow: hidden;
                    -webkit-overflow-scrolling: touch;
                    outline: 0;
                    background-color: hsla(120, 3%, 82%, 0.30);
                }
            </style>
    </form>
</body>
</html>
