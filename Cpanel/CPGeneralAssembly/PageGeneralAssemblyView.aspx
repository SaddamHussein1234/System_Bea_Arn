<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPGeneralAssembly/MPCPanel.master" AutoEventWireup="true" CodeFile="PageGeneralAssemblyView.aspx.cs" Inherits="Cpanel_CPGeneralAssembly_PageGeneralAssemblyView" %>

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
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
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
                    <div style="float: left">
                        <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;" data-toggle="tooltip" title="بحث"
                            class="btn btn-info btn-fill pull-right" OnClick="btnSearch_Click" />
                        &nbsp;
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم العضوية ... "></asp:TextBox>
                    </div>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageGeneralAssembly.aspx">أعضاء الجمعية العمومية</a></li>
                    <li><a href="PageGeneralAssemblyView.aspx">إستمارة طلب عضوية</a></li>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
            <table style="width: 100%;">
                <tr>
                    <td align="center" style="width:50%">
                        <asp:TextBox ID="txtTitle" runat="server" Font-Size="14px" class="form-control" placeholder="عنوان البحث" Text="إستمارة طلب عضوية" Style="text-align: center; width: 95%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                    </td>
                    <td align="center" style="width:20%">
                            <asp:Image ID="imgBarCode" runat="server" alt='Loding' />
                    </td>
                    <td align="center" style="width:30%">
                        <table style="width: 95%">
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
                    <td align="center" colspan="3">
                        <table style="width: 95%">
                            <tr>
                                <td class="StyleTD" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الإسم
                                            </td>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                <asp:Label ID="lblFull_Name" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم العضوية
                                            </td>
                                            <td style="width: 25%; border-width: 2px; border-color: #a1a0a0;">
                                                <asp:Label ID="lblNumber_Rigstry" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="StyleTD" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">تاريخ الميلاد
                                            </td>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                <asp:Label ID="lblDate_Bridth" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">المهنة
                                            </td>
                                            <td style="width: 25%;">
                                                <asp:Label ID="lblThe_Job" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="StyleTD" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                عنوان العمل
                                            </td>
                                            <td style="width: 75%;">
                                                <asp:Label ID="lblAddress_Job" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="StyleTD" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width:20%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم السجل الدني
                                            </td>
                                            <td style="width:20%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                <asp:Label ID="lblID_Card" runat="server"></asp:Label>
                                            </td>
                                            <td style="width:15%; border-left: double; border-width: 2px; border-color: #a1a0a0;">تاريخها
                                            </td>
                                            <td style="width:15%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                <asp:Label ID="lblDate_Card" runat="server"></asp:Label>
                                            </td>
                                            <td style="width:15%; border-left: double; border-width: 2px; border-color: #a1a0a0;">مصدرها
                                            </td>
                                            <td style="width:15%;">
                                                <asp:Label ID="lblCard_Source" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="StyleTD" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width:20%; border-left: double; border-width: 2px; border-color: #a1a0a0;">هاتف المنزل
                                            </td>
                                            <td style="width:20%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                <asp:Label ID="lblPhone_Home" runat="server"></asp:Label>
                                            </td>
                                            <td style="width:15%; border-left: double; border-width: 2px; border-color: #a1a0a0;">هاتف العمل
                                            </td>
                                            <td style="width:15%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                <asp:Label ID="lblPhone_Work" runat="server"></asp:Label>
                                            </td>
                                            <td style="width:15%; border-left: double; border-width: 2px; border-color: #a1a0a0;">هاتف آخر
                                            </td>
                                            <td style="width:15%;">
                                                <asp:Label ID="lblPhone_Other" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="StyleTD" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">جوال
                                            </td>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                <asp:Label ID="lblPhone_Personal" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">بريد إلكتروني
                                            </td>
                                            <td style="width: 25%;">
                                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="StyleTD" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">صندوق البريد
                                            </td>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                <asp:Label ID="lblBox_Email" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الرمز البريدي
                                            </td>
                                            <td style="width: 25%;">
                                                <asp:Label ID="lblSerial_Email" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="StyleTD" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">العنوان
                                            </td>
                                            <td style="width: 75%;">
                                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <br />
                        <div class="container-fluid">
                            <div class="panel-body">
                                <div class="content-box-large">
                                    <div class="container-fluid" dir="rtl">
                                        <div class="WidthText4">
                                            <div class="form-group" style="margin-top: 4px">
                                                أرغب الأنظمام لعضوية الجمعية إعتباراً من <asp:Label ID="lbl_Date_Rigstry" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="WidthText5">
                                            <div class="form-group">
                                                <p>
                                                    نوع العضوية <asp:CheckBox ID="CBIs_Aamel" runat="server" Text="عضو عامل" Enabled="false" /> 
                                                    <asp:CheckBox ID="CBIs_Montaseeb" runat="server" Text="عضو منتسب" Enabled="false" />
                                                </p>
                                                <p>
                                                    وقد اطلعت على لوائح وأنظمة الجمعية وما تضمنته من شروط للعضوية وإستمرارها .
                                                </p>
                                                <p>
                                                    <div class="WidthText2">
                                                        الإسم : 
                                                        <asp:Label ID="lblFull_Name2" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="WidthText2 hide" style="display:none;">
                                                        التوقيع : 
                                                        <asp:Image ID="Img_Signature" runat="server" Width='100px' Height='25' />
                                                    </div>
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
                                                <strong>قرار مجلس الإدارة
                                                </strong>
                                            </div>
                                        </div>
                                        <p>
                                            قرر مجلس ادرة الجمعية في جلستة رقم : <asp:Label ID="lblNumber_Qarar" runat="server"></asp:Label>
                                            , وبتاريخ <asp:Label ID="lblDate_Qarar" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            قبول العضوية كما هو موضح بعالية إعتباراً من : <asp:Label ID="lblDate_Qobol" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            وسجل في سجل العضوية برقم : <asp:Label ID="lblNumber_Rigstry2" runat="server"></asp:Label>
                                             كعضو : <asp:Label ID="lblCheck" runat="server"></asp:Label>
                                        </p>
                                        <hr />
                                        <div class="WidthText4" align="center" style="font-family: 'Alwatan'; font-size: 20px">
                                            رئيس مجلس الإدارة <br />
                                            <asp:Image ID="ImgRaees_AlMagles" runat="server" Width='100px' Height='25' /><br />
                                            <asp:Label ID="lblReesAlmaglis" runat="server"></asp:Label>
                                        </div>
                                        <div class="WidthText4" align="center">
                                            <div runat="server" id="IDKhatm" visible="false">
                                                <img src="/ImgSystem/ImgSignature/الختم.png" />
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
</asp:Content>

