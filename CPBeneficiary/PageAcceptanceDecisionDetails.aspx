<%@ Page Title="" Language="C#" MasterPageFile="~/CPBeneficiary/MPBeneficiary.master" AutoEventWireup="true" CodeFile="PageAcceptanceDecisionDetails.aspx.cs" Inherits="CPBeneficiary_PageAcceptanceDecisionDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
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
                    <li><a href="">عرض كشف زيارة</a></li>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
            <table style="width: 100%;">
                <tr>
                    <td align="center">
                        <div class="WidthText4">
                            <asp:TextBox ID="txtTitle" runat="server" Font-Size="14px" class="form-control" placeholder="عنوان البحث" Text="طلب موافقة (زيارة ميدانية)" Style="text-align: center; width: 95%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                        </div>
                        <div class="WidthText4" style="text-align: center">
                            <asp:Image ID="imgBarCode" runat="server" alt='Loding' />
                        </div>
                    </td>
                    <td align="center">
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
                    <td align="center">
                        <table style="width: 95%">
                            <tr>
                                <td class="StyleTD" style="width: 25%">إسم المستفيد
                                </td>
                                <td class="StyleTD" style="width: 50%; text-align: right">
                                    <asp:Label ID="lblNameMosTafeed" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="StyleTD" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">القرية
                                            </td>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                <asp:Label ID="lblAlqariah" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم الجوال
                                            </td>
                                            <td style="width: 25%;">
                                                0<asp:Label ID="lblPhone" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="center">
                        <table style="width: 90%">
                            <tr>
                                <td class="StyleTD" style="width: 50%">رقم الزيارة
                                </td>
                                <td class="StyleTD" style="width: 50%">
                                    <asp:Label ID="lblNumberZyara" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="StyleTD" style="width: 50%">تاريخ الزيارة
                                </td>
                                <td class="StyleTD" style="width: 50%">
                                    <asp:Label ID="lblDateZyara" runat="server"></asp:Label>
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
                                                <h4>سعادة رئيس مجلس الإدارة
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
                                                    نأمل التكرم بالموافقة لعمل زيارة ميدانية للمستفيد الموضحة بياناته اعلاه لعمل ما يلي :
                                                </p>
                                            </div>
                                        </div>
                                        <div class="WidthText5" runat="server" id="IDBahthHalatMosTafeed" visible="false">
                                            <div class="form-group">
                                                <asp:CheckBox ID="CBBahthHalatMosTafeed" runat="server" Enabled="false" />
                                                <span>بحث حالة مستفيد جديد </span>
                                            </div>
                                        </div>
                                        <div class="WidthText5" runat="server" id="IDEadatAlBahthLestafeedHaly" visible="false">
                                            <div class="form-group">

                                                <asp:CheckBox ID="CBEadatAlBahthLestafeedHaly" runat="server" Enabled="false" />
                                                <span>إعادة البحث لمستفيد حالي </span>
                                            </div>
                                        </div>
                                        <div class="WidthText5" runat="server" id="IDEadatAlBahthLeMostafeedSabeq" visible="false">
                                            <div class="form-group">
                                                <asp:CheckBox ID="CBEadatAlBahthLeMostafeedSabeq" runat="server" Enabled="false" />
                                                <span>إعادة البحث لمستفيد سابق </span>
                                            </div>
                                        </div>
                                        <div class="WidthText5">
                                            <div class="form-group">
                                                <p>
                                                    والتي سيقوم بها الباحث التالية بياناته : -
                                                </p>
                                            </div>
                                        </div>
                                        <div class="WidthText4">
                                            <div class="form-group">
                                                <p>
                                                    إسم الباحث / 
                                                        <asp:Label ID="lblNameBaheth" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="WidthText4">
                                            <div class="form-group">
                                                <p>
                                                    التوقيع /
                                                    <asp:Image ID="ImgBaheth" runat="server" Width='100px' Height='20' />
                                                </p>
                                            </div>
                                        </div>
                                        <div class="WidthText2">
                                            <div class="form-group">
                                                <p>
                                                    وذلك يوم /
                                                       <asp:Label ID="lblDay" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="WidthText4">
                                            <div class="form-group">
                                                <p>
                                                    بتاريخ /
                                                       <asp:Label ID="lblAllsoDay" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="WidthText5">
                                            <div class="form-group">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 25%"></td>
                                                        <td style="width: 25%"></td>
                                                        <td style="width: 25%">
                                                            <p>
                                                                ولكم أطيب التحايا ... 
                                                                <br />
                                                                <br />
                                                                <br />
                                                                <br />
                                                            </p>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <%--<p style="padding-top: 40px">
                                                                مدير الجمعية
                                                                    <br />
                                                                <br />
                                                                <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' /><br />
                                                                <asp:Label ID="lblModer" runat="server"></asp:Label>
                                                            </p>--%>
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
                                            <div align="center">
                                                <h4>
                                                    توصيات مدير الجمعية
                                                </h4>
                                            </div>
                                        </div>
                                        <div class="WidthText5" runat="server" id="IDAllowAlZeyarah" visible="false">
                                            <div class="form-group">
                                                <asp:CheckBox ID="CBAllowAlZeyarah" runat="server" Enabled="false" />
                                                <span>يسمح بعمل الزيارة الميدانية </span>
                                            </div>
                                        </div>
                                        <div class="WidthText5" runat="server" id="IDNotAllowAlZeyarah" visible="false">
                                            <div class="form-group">
                                                <asp:CheckBox ID="CBNotAllowAlZeyarah" runat="server" Enabled="false" />
                                                <span>لا يسمح بعمل الزيارة الميدانية للأسباب التالية </span>
                                                <div runat="server" id="IDNotAllowAlZeyarahlabel" visible="false">
                                                <br />
                                                <br />
                                                <i class="fa fa-star"></i> <asp:Label ID="lblAlAsBab" runat="server"></asp:Label>
                                                <hr />
                                            </div>
                                        </div>
                                        <div class="WidthText4" runat="server"  visible="false">
                                            <div class="form-group">
                                                
                                            </div>
                                        </div>
                                        <div class="WidthText5">
                                            <div class="form-group">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 25%">
                                                            
                                                        </td>

                                                        <td style="width: 25%"></td>
                                                        
                                                        <td style="width: 25%">
                                                            <div  runat="server" id="IDKhatm" visible="false">
                                                                <img src="../ImgSystem/ImgSignature/الختم.png" />
                                                            </div>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <p>
                                                                مدير الجمعية
                                                                    <br />
                                                                <br />
                                                                <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' /><br />
                                                                <asp:Label ID="lblModer" runat="server"></asp:Label>
                                                            </p>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<div class="container-fluid">
                            <div class="panel-body">
                                <div class="content-box-large">
                                    <div class="container-fluid" dir="rtl">
                                        <div class="WidthText5">
                                            <hr />
                                            <div align="center">
                                                <h4>توصيات رئيس مجلس الإدارة
                                                </h4>
                                            </div>
                                        </div>
                                        <div class="WidthText5" runat="server" id="IDAllowAlZeyarah" visible="false">
                                            <div class="form-group">
                                                <asp:CheckBox ID="CBAllowAlZeyarah" runat="server" Enabled="false" />
                                                <span>يسمح بعمل الزيارة الميدانية </span>
                                            </div>
                                        </div>
                                        <div class="WidthText5" runat="server" id="IDNotAllowAlZeyarah" visible="false">
                                            <div class="form-group">
                                                <asp:CheckBox ID="CBNotAllowAlZeyarah" runat="server" Enabled="false" />
                                                <span>لا يسمح بعمل الزيارة الميدانية للأسباب التالية </span>
                                                <br />
                                                <br />
                                                <i class="fa fa-star"></i> <asp:Label ID="lblAlAsBab" runat="server"></asp:Label>
                                                <hr />
                                            </div>
                                        </div>
                                        <div class="WidthText4" runat="server"  visible="false">
                                            <div class="form-group">
                                                
                                            </div>
                                        </div>
                                        <div class="WidthText5">
                                            <div class="form-group">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 25%">
                                                            
                                                        </td>

                                                        <td style="width: 25%"></td>
                                                        
                                                        <td style="width: 25%">
                                                            <div  runat="server" id="IDKhatm" visible="false">
                                                                <img src="../ImgSystem/ImgSignature/الختم.png" />
                                                            </div>
                                                        </td>
                                                        <td style="width: 25%">
                                                            <p>
                                                                رئيس مجلس الإدارة
                                                                    <br />
                                                                <br />
                                                                <asp:Image ID="ImgRaeesMaglesAEdarah" runat="server" Width='100px' Height='25' /><br />
                                                                <asp:Label ID="lblRaeesMaglesAEdarah" runat="server"></asp:Label>
                                                            </p>

                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
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
</asp:Content>

