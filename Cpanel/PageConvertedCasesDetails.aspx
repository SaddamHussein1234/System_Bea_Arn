<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageConvertedCasesDetails.aspx.cs" Inherits="Cpanel_PageConvertedCasesDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .StyleTD {
            text-align: center;
            padding: 5px;
            border: double;
            border-width: 2px;
            border-color: #a1a0a0;
        }

        .bl {
            color: #fff;
        }

        .fo {
            font-size: 12px;
        }

        @media screen and (min-width: 768px) {
            .WidthTex {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }

            .WidthText {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }

            .WidthText3 {
                float: right;
                Width: 19%;
                padding-right: 5px;
            }

            .WidthText30 {
                float: right;
                Width: 16%;
                padding-right: 5px;
            }

            .WidthText2 {
                float: right;
                Width: 32%;
                padding-left: 5px;
            }

            .WidthText1 {
                float: right;
                Width: 24%;
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

            .WidthText20 {
                Width: 150px;
                height: 36px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthTex {
                Width: 95%;
            }

            .WidthText {
                Width: 95%;
            }

            .WidthText1 {
                Width: 95%;
            }

            .WidthText2 {
                Width: 95%;
            }

            .WidthText3 {
                Width: 95%;
            }

            .WidthText30 {
                Width: 95%;
            }

            .WidthText4 {
                Width: 95%;
            }

            .WidthText5 {
                Width: 95%;
            }

            .WidthText20 {
                Width: 100px;
                height: 36px;
            }
        }

        .MarginBottom {
            margin-top: 15px;
        }
    </style>

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('PagePrint.aspx', 'PrintMe', 'height=900px,width=1000px,scrollbars=1');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            prtContent.innerHTML = strOldOne;
        }
    </script>

    <script type="text/javascript">
        function PrintTextbox() {
            var text_box = document.getElementById("<%=pnlPrint.ClientID %>");
        var printWindow = window.open('', '', 'height=900,width=1000');
        printWindow.document.write('<html><head><title>النظام الإلكتروني</title>');
        printWindow.document.write('<link href="../fonts/font-awesome.css" rel="stylesheet" /></head><body >');
        printWindow.document.write(text_box.innerHTML);
        printWindow.document.write('</body></html>');
        printWindow.document.close();
        setTimeout(function () {
            printWindow.print();
        }, 500);
        return false;
    }
    </script>
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
    <link href="css/chosen.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة" OnClientClick="return ConfirmDelete();" Style="margin-left: 5px">
                    <i class="fa fa-print"></i></asp:LinkButton>
                    <div style="float: left">
                        <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;"
                            class="btn btn-info btn-fill pull-right" OnClick="btnSearch_Click" />
                        &nbsp;
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم القرار ... "></asp:TextBox>
                    </div>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageConvertedCases.aspx">قائمة الطلبات</a></li>
                    <li><a href="">طلب تحويل حالة مستفيد</a></li>
                </ul>
            </div>
        </div>
        <div id="bill">
            <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
                <table style="width: 100%;">
                    <tr>
                        <td align="center" style="width: 50%">
                            <div class="WidthText4">
                                <asp:TextBox ID="txtTitle" runat="server" Font-Size="14px" class="form-control" placeholder="عنوان البحث" Text="طلب تحويل حالة مستفيد" Style="text-align: center; width: 95%; background-color: #00cc1a; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                            </div>
                            <div class="WidthText4" style="text-align: center">
                                <asp:Image ID="imgBarCode" runat="server" alt='Loding' />
                            </div>
                        </td>
                        <td align="center" style="width: 50%">
                            <table style="width: 90%">
                                <tr>
                                    <td class="StyleTD">مدخل البيانات
                                    </td>
                                    <td class="StyleTD">
                                        <asp:Label ID="lblDataEntery" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="StyleTD">تاريخ الإدخال
                                    </td>
                                    <td class="StyleTD">
                                        <asp:Label ID="lblDateEntery" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <table style="width: 95%">
                                <tr>
                                    <td class="StyleTD" colspan="2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم الطلب
                                                </td>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:Label ID="lblNumberOrder" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">تاريخ الطلب
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblDateQarar" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="StyleTD" colspan="2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">إسم المستفيد
                                                </td>
                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:Label ID="lblNameMosTafeed" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">القرية
                                                            </td>
                                                            <td style="width: 50%">
                                                                <asp:Label ID="lblAlqariah" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="StyleTD" colspan="2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم السجل المدني
                                                </td>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:Label ID="lblNumberAlSegelAlMadany" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم المستفيد
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblNumberMostafeed" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="StyleTD" colspan="2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الحالة
                                                </td>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:Label ID="lblHalafAlMosTafeed" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم الجوال
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <br />
                            <div class="container-fluid">
                                <div class="panel-body">
                                    <div class="content-box-large">
                                        <div class="container-fluid" dir="rtl">
                                            <div class="WidthText4">
                                                <div class="form-group">
                                                    <h4>سعادة مدير الجمعية
                                                    </h4>
                                                </div>
                                            </div>
                                            <div class="WidthText2">
                                                <div class="form-group">
                                                    <h4>وفقه الله
                                                    </h4>
                                                </div>
                                            </div>
                                            <div class="WidthText4">
                                                <div class="form-group">
                                                    تحية طيبة وبعد :-
                                                </div>
                                            </div>
                                            <div class="WidthText5">
                                                <div class="form-group">
                                                    <p>
                                                        نأمل من سعادتكم الموافقة على طلب (تحويل حالة) المستفيد الموضحة بياناته بعاليه
                                                    </p>
                                                    <p>
                                                        من حالة :-
                                                    <strong><u>
                                                        <asp:Label ID="lblOld" runat="server"></asp:Label></u></strong>
                                                        الى حالة 
                                                     <strong><u>
                                                         <asp:Label ID="lblNew" runat="server"></asp:Label></u></strong>
                                                        وذلك للأسباب التالية : 
                                                    </p>
                                                    <p>
                                                        <li class="fa fa-star"></li>
                                                        <strong>
                                                            <asp:Label ID="lblWhayTahweel" runat="server"></asp:Label></strong>
                                                    </p>
                                                </div>
                                            </div>
                                            <div class="WidthText5">
                                                <div class="form-group">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 100%" colspan="2" align="center">
                                                                <p>
                                                                    ولكم الشكر والتقدير ... 
                                                                   <br />
                                                                    <br />
                                                                    <br />
                                                                </p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 50%">الباحث الاجتماعي / 
                                                                <asp:Label ID="lblAlbaheth" runat="server"></asp:Label>
                                                            </td>
                                                            <td style="width: 50%">التوقيع /
                                                                <asp:Image ID="ImgBaheth" runat="server" Width='100px' Height='25' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid">
                                <div class="panel-body">
                                    <div class="content-box-large">
                                        <div class="container-fluid" dir="rtl">
                                            <div class="WidthText5">
                                                <hr />
                                                <div align="Right">
                                                    <strong>مرئيات مدير الجمعية 
                                                    </strong>
                                                </div>
                                            </div>
                                            <div class="WidthText5" runat="server" id="IDAllow" visible="false">
                                                <div class="form-group">
                                                    <br />
                                                    <asp:CheckBox ID="CBAllow" runat="server" Enabled="false" />
                                                    <span>يتم إعتماد الحالة الجديدة </span>
                                                    <br />
                                                    <asp:CheckBox ID="CBNotAllow" runat="server" Enabled="false" />
                                                    <span>لا يتم إعتماد الحالة الجديدة  </span>
                                                </div>
                                            </div>
                                            <div class="WidthText5" runat="server" id="IDNotAllow" visible="false">
                                                <div class="form-group">

                                                    <br />
                                                    <asp:Label ID="lblAlAsBab" runat="server"></asp:Label>
                                                    <hr />
                                                </div>
                                            </div>
                                            <div class="WidthText5">
                                                <div class="form-group">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 25%"></td>
                                                            <td style="width: 25%"></td>
                                                            <td style="width: 25%"></td>
                                                            <td style="width: 25%">
                                                                <p>
                                                                    <strong>مدير الجمعية</strong>
                                                                    <br />
                                                                    <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' /><br />
                                                                    <asp:Label ID="lblModer" runat="server"></asp:Label>
                                                                </p>
                                                                <div runat="server" id="IDKhatm" align="left" visible="false" style="margin-top:-15px">
                                                                    <img src="../ImgSystem/ImgSignature/الختم.png" width="100" />
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="WidthText5" id="LastUpdate" runat="server" visible="false">
                                                <div class="form-group">
                                                    <hr />
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 25%">
                                                                <i class="fa fa-star"></i>
                                                                عدل من قبل : 
                                                            <asp:Label ID="lblUpdate" runat="server"></asp:Label>
                                                                <br />
                                                                <i class="fa fa-star"></i>
                                                                بتاريخ : 
                                                            <asp:Label ID="lblDateLastUpdate" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>

            </asp:Panel>
        </div>
        <asp:Panel ID="pnlSelect" runat="server" Direction="RightToLeft" Visible="false">
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label6" runat="server" Text="يرجى إدخال رقم سجل صحيح"></asp:Label>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
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
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
        <script src="css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

