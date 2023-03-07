<%@ Page Title="" Language="C#" MasterPageFile="~/CPBeneficiary/MPBeneficiary.master" AutoEventWireup="true" CodeFile="PageVisitReportDetails.aspx.cs" Inherits="CPBeneficiary_PageVisitReportDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .StyleTD {
            text-align: center;
            padding: 5px;
            border: double;
            border-width: 2px;
            border-color: #a1a0a0;
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

            .Width10Percint {
                float: right;
                Width: 10%;
                padding-right: 5px;
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

            .Width10Percint {
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
    <link href="css/chosen.css" rel="stylesheet" />
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة" OnClientClick="return ConfirmDelete();" Style="margin-left: 5px">
                    <i class="fa fa-print"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a>تقرير نتيجة زيارة ميدانية</a></li>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
            <table style="width: 100%;">
                <tr>
                    <td align="center">
                        <asp:TextBox ID="txtTitle" runat="server" Font-Size="14px" class="form-control" placeholder="عنوان البحث" Text="تقرير نتيجة زياره ميدانية" Style="text-align: center; width: 95%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                    </td>
                    <td align="center">
                            <asp:Image ID="imgBarCode" runat="server" alt='Loding' />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <table style="width: 95%">
                            <tr>
                                <td class="StyleTD" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم التقرير
                                            </td>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                <asp:Label ID="lblNumberReport" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">تاريخ التقرير
                                            </td>
                                            <td style="width: 25%;">
                                                <asp:Label ID="lblDateReport" runat="server"></asp:Label>
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
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم الجوال
                                            </td>
                                            <td style="width: 25%;">0<asp:Label ID="lblPhone" runat="server"></asp:Label>
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
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم ملف المستفيد
                                            </td>
                                            <td style="width: 25%;">
                                                <asp:Label ID="lblNumberMostafeed" runat="server"></asp:Label>
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
                                                <strong>سعادة رئيس مجلس الإدارة
                                                </strong>
                                            </div>
                                        </div>
                                        <div class="WidthText2">
                                            <div class="form-group">
                                                <strong>وفقه الله
                                                </strong>
                                            </div>
                                        </div>
                                        <div class="WidthText4">
                                            <div class="form-group" style="margin-top: 4px">
                                                تحية طيبة وبعد :-
                                            </div>
                                        </div>
                                        <div class="WidthText5">
                                            <div class="form-group">
                                                <p>
                                                    بناءً على دراسة الحالة للمستفيد الموضح بياناته بعاليه وبعد الزيارة الميدانية كانت ملاحظاتنا للحالة على النحو التالي : 
                                                </p>
                                                <p>
                                                    المستفيد يحتاج إلى : 
                                                </p>
                                                <div class="container-fluid" dir="rtl">
                                                    <asp:Panel ID="pnlDevice" runat="server" Visible="False">
                                                        <asp:Repeater ID="RPTDeviceByMostafeed" runat="server">
                                                            <ItemTemplate>
                                                                <div class="Width10Percint StyleTD">
                                                                    <%# Eval("ProductName") %> : 
                                                                    <%# Eval("IDNumberCount") %>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </asp:Panel>
                                                    <asp:Panel ID="pnlNull" runat="server" Visible="False">
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <div align="center">
                                                            <h3 style="font-size: 20px">لا توجد نتائج
                                                            </h3>
                                                        </div>
                                                        <br />
                                                        <br />
                                                    </asp:Panel>
                                                </div>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td colspan="2">
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 25%">
                                                            <p>
                                                                <asp:CheckBox ID="CBEgathy" runat="server" Enabled="false" />
                                                                <span>سلة غذائية </span>| <span>عدد </span>
                                                                <asp:Label ID="txtEgathy" runat="server" Text="0"></asp:Label>
                                                            </p>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <p>
                                                                <asp:CheckBox ID="CBOther" runat="server" Enabled="false" />
                                                                <span>اُخرى </span>| 
                                                                <asp:Label ID="txtOther" runat="server"></asp:Label>
                                                            </p>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="WidthText5" runat="server" id="PnlTathithHome" visible="false">
                                            <div class="form-group">
                                                <hr />
                                                <p>
                                                    <asp:CheckBox ID="CBTathithHome" runat="server" Enabled="false" />
                                                    <span>تأثيث منزل </span>
                                                    <br />
                                                    <asp:Repeater ID="RPTTathithHome" runat="server">
                                                        <ItemTemplate>
                                                            <div class="WidthText30">
                                                                <a href='<%# "../" + Eval("ImgReport") %>' target="_blank" title="تكبير الصورة" data-toggle="tooltip">
                                                                    <img src='<%# "../" + Eval("ImgReport") %>' style="margin: 4px; border-radius: 5px" width="70%" height="82" class="WidthImg" />
                                                                </a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="WidthText5" runat="server" id="PnlTarmemHome" visible="false">
                                            <div class="form-group">
                                                <hr />
                                                <p>
                                                    <asp:CheckBox ID="CBTarmemHome" runat="server" Enabled="false" />
                                                    <span>ترميم منزل </span>
                                                    <br />
                                                    <asp:Repeater ID="RPTTarmemHome" runat="server">
                                                        <ItemTemplate>
                                                            <div class="WidthText30">
                                                                <a href='<%# "../" + Eval("ImgReport") %>' target="_blank" title="تكبير الصورة" data-toggle="tooltip">
                                                                    <img src='<%# "../" + Eval("ImgReport") %>' style="margin: 4px; border-radius: 5px" width="70%" height="82" class="WidthImg" />
                                                                </a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="WidthText5" runat="server" id="PnlBenaaHome" visible="false">
                                            <div class="form-group">
                                                <hr />
                                                <p>
                                                    <asp:CheckBox ID="CBBenaaHome" runat="server" Enabled="false" />
                                                    <span>بناء منزل </span>
                                                    <br />
                                                    <asp:Repeater ID="RPTBenaaHome" runat="server">
                                                        <ItemTemplate>
                                                            <div class="WidthText30">
                                                                <a href='<%# "../" + Eval("ImgReport") %>' target="_blank" title="تكبير الصورة" data-toggle="tooltip">
                                                                    <img src='<%# "../" + Eval("ImgReport") %>' style="margin: 4px; border-radius: 5px" width="70%" height="82" class="WidthImg" />
                                                                </a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </p>
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
                                            <div align="center">
                                                <strong>أعضاء لجنة البحث الاجتماعي
                                                </strong>
                                            </div>
                                        </div>
                                        <div class="WidthText5">
                                            <div class="form-group">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 33.3%; text-align: center; padding: 4px; border: double; border-width: 2px; border-color: #a1a0a0;">
                                                            <strong>الباحث الإجتماعي
                                                            </strong>
                                                        </td>
                                                        <td style="width: 33.3%"></td>
                                                        <td style="width: 33.3%; text-align: right; padding: 4px; border: double; border-width: 2px; border-color: #a1a0a0;">
                                                            <strong>مدير الجمعية
                                                            </strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 33.3%; text-align: center">
                                                            <br />
                                                            <asp:Image ID="ImgBaheth" runat="server" Width='100px' Height='25' />
                                                            <br />
                                                            <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" Enabled="false">
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblAlBaheth" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                        <td style="width: 33.3%; text-align: center; padding: 4px; border-left: double; border-right: double; border-top: double; border-width: 2px; border-color: #a1a0a0;">
                                                            <strong>مصادقة رئيس لجنة البحث الإجتماعي 
                                                            </strong>
                                                        </td>
                                                        <td style="width: 33.3%; text-align: right;">
                                                            <br />
                                                            <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' />
                                                            <br />
                                                            <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" Enabled="false">
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblModerAlGmeiah" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 33.3%;"></td>
                                                        <td style="width: 33.3%; text-align: center; padding: 4px; border-left: double; border-right: double; border-bottom: double; border-width: 2px; border-color: #a1a0a0;">
                                                            <div style="margin-top:-25px">
                                                                <asp:Image ID="ImgRaesLagnatAlBahthAllow" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:DropDownList ID="DLRaeesLagnatAlBahath" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" Enabled="false">
                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblRaeesLagnatAlBahath" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                        </td>
                                                        <td style="width: 33.3%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td>
                                                            <div runat="server" id="IDKhatm" align="left" style="margin-top: -140px" visible="false">
                                                                <img src="../ImgSystem/ImgSignature/الختم.png" />
                                                            </div>
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

