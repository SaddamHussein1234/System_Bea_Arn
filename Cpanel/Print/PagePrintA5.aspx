<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PagePrintA5.aspx.cs" Inherits="Cpanel_Print_PagePrintA5" %>

<!DOCTYPE html>

<html lang="ar" dir="rtl" style="font-family: sans-serif; -ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; font-size: 10px; -webkit-tap-highlight-color: transparent;" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<style type="text/css">
        @page {
            margin: 11pt 0
        }

        @media print {
            @page {
                size: 148mm 210mm;
                margin: 5mm;
                size: A5;
                size: landscape;
            }
        }

        @media print { @page { size: 8.5in 11in; } }

        @media print { @page { size: A5 landscape; margin:0;}}
    </style>--%>
    <style>
        @page { margin: 11pt 0} @media print { @page { size: 148mm 210mm; margin: 5mm; size: A5; size: portrait; } }
        @page { size: 8.5in 11in; margin: 2%; @top-left { content: 'Hamlet'; } @top-right { content: 'Page ' counter(page); } }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
