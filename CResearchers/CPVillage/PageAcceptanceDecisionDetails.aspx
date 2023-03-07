<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPVillage/MPVillage.master" AutoEventWireup="true" CodeFile="PageAcceptanceDecisionDetails.aspx.cs" Inherits="CResearchers_CPVillage_PageAcceptanceDecisionDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    <link href="../css/chosen.css" rel="stylesheet" />
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
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
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم القرار ... "></asp:TextBox>
                    </div>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageAcceptanceDecision.aspx">قرارات القبول</a></li>
                    <li><a href="">قرار قبول تسجيل مستفيد</a></li>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
            <table style="width: 100%;">
                <tr>
                    <td colspan="2">
                        <div class="WidthText4" style="margin-right: 15px">
                            <asp:TextBox ID="txtTitle" runat="server" Font-Size="14px" class="form-control" placeholder="عنوان البحث" Text="قرار قبول مستفيد" Style="text-align: center; width: 95%; background-color: #00cc1a; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                        </div>
                        <div class="WidthText">
                            <asp:Image ID="IDBarcode" runat="server" alt='Loding' />
                        </div>
                        <div class="WidthText2">
                            <table style="width: 100%">
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
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <table style="width: 95%">
                            <tr>
                                <td class="StyleTD" colspan="2">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم المستفيد
                                            </td>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                <asp:Label ID="lblNumberMostafeed" runat="server"></asp:Label>
                                            </td>
                                            <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">تاريخ القرار
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
                                            <td style="width: 75%;">
                                                <asp:Label ID="lblNumberAlSegelAlMadany" runat="server"></asp:Label>
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
                                            <td style="width: 25%;">0<asp:Label ID="lblPhone" runat="server"></asp:Label>
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
                                                <strong>سعادة مدير الجمعية ... تحية طيبة وبعد :
                                                </strong>
                                            </div>
                                        </div>
                                        <div class="WidthText2">
                                            <div class="form-group">
                                            </div>
                                        </div>
                                        <div class="WidthText5">
                                            <div class="form-group">
                                                <p>
                                                    بناءً على تقرير لجنة البحث الإجتماعي رقم 
                                                    (
                                                    <asp:Label ID="lblNumberReport" runat="server"></asp:Label>
                                                    )

                                                    وتاريخ : 
                                                    <asp:Label ID="lblDateReport" runat="server"></asp:Label>
                                                </p>
                                                <p>
                                                    فإنه تم قبول تسجيل (المستفيد) الموضح بياناته بعاليه اعتباراً من تاريخة
                                                </p>
                                                <div align="center">
                                                    <p>

                                                        <strong>والله ولي التوفيق
                                                        </strong>
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="container-fluid">
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="container-fluid" dir="rtl">
                            <div class="WidthText5">
                                <hr />
                                <div align="center">
                                    <strong>أعضاء مجلس الإدارة
                                    </strong>
                                    <div runat="server" id="IDKhatm" align="left" style="margin-top: -100px" visible="false">
                                        <img src="../../ImgSystem/ImgSignature/الختم.png" />
                                    </div>
                                    <br />
                                    <br />
                                    <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
                                        <div class="table-responsive">
                                            <table style="width: 100%;" class="table table-hover table-bordered">
                                                <tr>
                                                    <th class="StyleTD" style="width: 7%;">
                                                        <strong>م
                                                        </strong>
                                                    </th>
                                                    <th class="StyleTD" style="width: 31%;">
                                                        <strong>الإسم
                                                        </strong>
                                                    </th>
                                                    <th class="StyleTD" style="width: 31%;">
                                                        <strong>الصفة
                                                        </strong>
                                                    </th>
                                                    <th class="StyleTD" style="width: 31%;">
                                                        <strong>التوقيع
                                                        </strong>
                                                    </th>
                                                </tr>
                                                <asp:Repeater ID="RPTGetAdminInManagment" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="StyleTD" style="width: 7%;">
                                                                <%# Eval("IsOrderAdminInEdarah") %>
                                                            </td>
                                                            <td class="StyleTD" style="width: 31%;">
                                                                <%# Eval("FirstName") %>
                                                            </td>
                                                            <td class="StyleTD" style="width: 31%;">
                                                                <%# Eval("CommentAdmin") %>
                                                            </td>
                                                            <td class="StyleTD" style="width: 31%;">
                                                                <%# Library_CLS_Arn.Saddam.ClassSaddam.FGetSignature2(Convert.ToInt32(Eval("IDIteam")),Convert.ToBoolean(Eval("AdminAllow")),Convert.ToString(Eval("AddImgSignature"))) %>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </table>
                                        </div>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlAdminNull" runat="server" Visible="false">
                                        <br />
                                        <br />
                                        <br />
                                        <div align="center">
                                            <h3 style="font-size: 20px">لم يتم إضافة أعضاء مجلس الإدارة
                                            </h3>
                                        </div>
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
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

