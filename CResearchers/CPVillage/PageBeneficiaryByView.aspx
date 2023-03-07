﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPVillage/MPVillage.master" AutoEventWireup="true" CodeFile="PageBeneficiaryByView.aspx.cs" Inherits="CResearchers_CPVillage_PageBeneficiaryByView" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>--%>

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

            .WidthText30 {
                float: right;
                Width: 16%;
                padding-right: 5px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthText30 {
                Width: 95%;
            }

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
                    <li class="fa fa-print"></li></asp:LinkButton>
                    <div style="float: left">
                        <asp:LinkButton ID="btnSearch" runat="server" data-toggle="tooltip" title="جلب" OnClick="btnSearch_Click"
                            class="btn btn-info pull-right"><span class="tip-bottom"><i class="fa fa-search" style="font-size:16px"></i></span></asp:LinkButton>
                        &nbsp;
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم المستفيد ... "></asp:TextBox>
                    </div>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageBeneficiaryBySearch.aspx">إدارة المستفيدين</a></li>
                    <li><a href="PageBeneficiaryByView.aspx">إستمارة المستفيد</a></li>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
            <table style="width: 100%;">
                <tr>
                    <td colspan="2">
                        <hr />
                        <div class="container-fluid">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <i class="fa fa-pencil"></i>
                                        <asp:Label ID="lbmsg" runat="server" Text="بيانات المستفيد"></asp:Label>
                                    </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="content-box-large">
                                        <div class="widget-box">
                                            <div class="container-fluid" dir="rtl">
                                                <div style='float: right; padding: 0 5px 0 0;' class='w'>
                                                    <table style="width: 100%;">
                                                        <tr style="margin: 5px">
                                                            <td class="StyleTD">رقم المستفيد
                                                            </td>
                                                            <td class="StyleTD">
                                                                <asp:Label ID="lblFileNumber" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="StyleTD">تاريخ التسجيل
                                                            </td>
                                                            <td class="StyleTD">
                                                                <asp:Label ID="lblDateRigstry" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="StyleTD">نوع المستفيد
                                                            </td>
                                                            <td class="StyleTD">
                                                                <asp:Label ID="lblTypeMostafeed" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div style='float: left; padding: 0 0 0 5px' class='w'>
                                                    <table style="width: 100%;">
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
                                                        <tr>
                                                            <td class="StyleTD">أفراد الاسرة
                                                            </td>
                                                            <td class="StyleTD">
                                                                <asp:Label ID="lblCountBoys" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div align="center">
                                                    <asp:TextBox ID="txtTitle" runat="server" Font-Size="12px" class="form-control" placeholder="عنوان البحث" Text="إستمارة بيانات المستفيد" Style="text-align: center; width: 50%"></asp:TextBox>
                                                    <asp:Image ID="IDBarcode" runat="server" alt='Loding' />
                                                </div>
                                                <hr />
                                                <div class="WidthText4">
                                                    <div class="form-group">
                                                        الاسم :
                                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="WidthText1">
                                                    <div class="form-group">
                                                        القرية :
                                                        <asp:Label ID="lblAlQariah" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="WidthText1">
                                                    <div class="form-group">
                                                        الجنس :
                                    
                                                        <asp:Label ID="lblGender" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="WidthText3">
                                                    <div class="form-group">
                                                        <h5>رقم الجوال :
                                                        </h5>
                                                        0<asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="WidthText3">
                                                    <div class="form-group">
                                                        <h5>حالة المستفيد :
                                                        </h5>
                                                        <asp:Label ID="lblHalatAlmostafeed" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="WidthText3">
                                                    <div class="form-group">
                                                        <h5>السجل المدني :
                                                        </h5>
                                                        <asp:Label ID="lblNumberSigal" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="WidthText3">
                                                    <div class="form-group">
                                                        <h5>تاريخ الميلاد :
                                                        </h5>
                                                        <asp:Label ID="lblDateBrithDay" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="WidthText3">
                                                    <div class="form-group">
                                                        <h5>العمر :
                                                        </h5>
                                                        <asp:Label ID="lblAge" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                        <div class="container-fluid">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <i class="fa fa-pencil"></i>
                                        <asp:Label ID="Label1" runat="server" Text="الحالة العلمية والتعليمية للمستفيد"></asp:Label>
                                    </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="content-box-large">
                                        <div class="container-fluid" dir="rtl">
                                            <div class="WidthText2">
                                                <div class="form-group">
                                                    المهنة الحالية :
                                                    <asp:Label ID="lblAlMehnahAlHaliyahllmostafeed" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="WidthText2">
                                                <div class="form-group">
                                                    الحالة التعليمية :
                                                    <asp:Label ID="lblAlHalahAlTaelimiahllmostafeed" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnlCheckDead" runat="server" Visible="false">
                                                <div class="WidthText2">
                                                    <div class="form-group">
                                                        مهنة الاب قبل الوفاة :
                                                    <asp:Label ID="lblMehnahAlAAbKablAlWafah" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                        <div class="container-fluid">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <i class="fa fa-pencil"></i>
                                        <asp:Label ID="Label2" runat="server" Text="الحالة الصحية للمستفيد"></asp:Label>
                                    </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="content-box-large">
                                        <div class="widget-box">
                                            <div class="container-fluid" dir="rtl">
                                                <div class="WidthText1">
                                                    <div class="form-group">
                                                        <h5>سليم :
                                                        </h5>
                                                        <asp:CheckBox ID="CBSaleem" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                                <div class="WidthText">
                                                    <div class="form-group">
                                                        <h5>معاق :
                                                        </h5>
                                                        <asp:CheckBox ID="CBMaak" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                                <div class="WidthText1">
                                                    <div class="form-group">
                                                        <h5>نوع الإعاقة :
                                                        </h5>
                                                        <asp:Label ID="lblTypaAleaakah" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="WidthText">
                                                    <div class="form-group">
                                                        <h5>مريض :
                                                        </h5>
                                                        <asp:CheckBox ID="CBMareed" runat="server" Enabled="false" />
                                                    </div>
                                                </div>
                                                <div class="WidthText1">
                                                    <div class="form-group">
                                                        <h5>نوع المرض :
                                                        </h5>
                                                        <asp:Label ID="lblMareed" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr />
                        <div class="container-fluid">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <i class="fa fa-pencil"></i>
                                        <asp:Label ID="Label4" runat="server" Text="الحالة المادية والسكنية للمستفيد"></asp:Label>
                                    </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="content-box-large">
                                        <div class="widget-box">
                                            <div class="container-fluid" dir="rtl">
                                                <div class="WidthText2">
                                                    <div class="form-group">
                                                        الدخل الشهري :
                                                        <asp:Label ID="lblAlDakhlAlShahryllMostafeed" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="WidthText2">
                                                    <div class="form-group">
                                                        مصدر الدخل :
                                                        <asp:Label ID="lblMasderAlDakhl" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="WidthText2">
                                                    <div class="form-group">
                                                        نوع المسكن :
                                                        <asp:Label ID="lblTypeAlMaskan" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="WidthText5">
                                                    <div class="form-group" align="center">
                                                        حالة المسكن :
                                                        <asp:Label ID="lblHalatAlMaskan" runat="server"></asp:Label>
                                                    </div>
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
            <hr />

            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>
                            <asp:Label ID="Label5" runat="server" Text="معلومات أفراد الإسرة"></asp:Label>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <asp:Panel ID="pnlData" runat="server" Visible="False">
                                        <div class="table table-responsive">
                                            <asp:GridView ID="GVMenu" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
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
                                                    <asp:BoundField DataField="IDItem" HeaderText="IDItem" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="IDItem" Visible="false" />
                                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="margin-right: 5px"><%# Container.DataItemIndex + 1 %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="الإسم" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("Name")%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="القرابة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# ClassQuaem.FQarabah((Int32) Eval("AlQarabah"))%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تاريخ الميلاد" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# ClassDataAccess.FCheckAndChangeF((DateTime) (Eval("DateBrith")))%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="العمر" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# ClassSaddam.FCheckAndGetAge((DateTime) (Eval("DateBrith"))) %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="AlMehnahAlHaliah" HeaderText="المهنة الحالية" SortExpression="AlMehnahAlHaliah"
                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:BoundField DataField="AlmostawaAlDerasy" HeaderText="المستوى الدراسي" SortExpression="AlmostawaAlDerasy"
                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:BoundField DataField="AlHalahAlseHeyah" HeaderText="الحالة الصحية" SortExpression="AlHalahAlseHeyah"
                                                        HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:TemplateField HeaderText="من قبل" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# ClassQuaem.FAlBaheth((Int32) Eval("IDAdmin"))%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings Mode="NextPrevious" Position="TopAndBottom" NextPageText=" -- التالي "
                                                    PreviousPageText=" السابق - " />
                                                <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" />
                                                <RowStyle CssClass="rows"></RowStyle>
                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                <%--<SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#242121" />--%>
                                            </asp:GridView>
                                        </div>
                                        <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                        <span style="font-size: 12px; padding-right: 5px">عدد أفراد الاسره : </span>
                                        <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
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
                                    <hr />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <div class="container-fluid">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title">
                                        <i class="fa fa-pencil"></i>
                                        <asp:Label ID="Label3" runat="server" Text="المرفقات"></asp:Label>
                                    </h3>
                                </div>
                                <div class="panel-body">
                                    <div class="content-box-large">
                                        <div class="widget-box">
                                            <div class="container-fluid" dir="rtl">
                                                <asp:Panel ID="pnlImgAttach" runat="server" Visible="False">
                                                    <asp:Repeater ID="RPTAttach" runat="server">
                                                        <ItemTemplate>
                                                            <div class="WidthText30">
                                                                <span><%# Eval("TitleImg") %></span>
                                                                <a href='<%# "../../" + Eval("ImgMosTafeed") %>' target="_blank" title="تكبير الصورة" data-toggle="tooltip">
                                                                    <img src='<%# "../../" + Eval("ImgMosTafeed") %>' style="margin: 4px; border-radius: 5px;" width="100%" height="120" class="WidthImg" />
                                                                </a>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <div class="clearfix visible-sm-block"></div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnlImgAttachNull" runat="server" Visible="False">
                                                    <br />
                                                    <div align="center">
                                                        <h3 style="font-size: 20px">لا توجد مرفقات
                                                        </h3>
                                                    </div>
                                                    <br />
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <hr />
                        <div class="container-fluid" dir="rtl">
                            <div class="WidthText3" align="center" style="font-family: 'Alwatan'; font-size: 14px;">
                                الباحث الإجتماعي
                                <div id="IDBaheethPrint">
                                    <asp:Image ID="ImgAlBaheth" runat="server" Width='100px' Height='25' />
                                    <br />
                                    <asp:Label ID="lblAlBaheth" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="WidthText3" align="center" style="font-family: 'Alwatan'; font-size: 14px;">
                                مدير الجمعية
                                <div id="IDModer5Print">
                                    <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' />
                                    <br />
                                    <asp:Label ID="lblModerAlGmeiah" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="WidthText3" align="center" style="font-family: 'Alwatan'; font-size: 14px;">
                                رئيس لجنة البحث الإجتماعية
                                <div id="IDRaeesLagnatPrint">
                                    <asp:Image ID="ImgRaeesLagnatAlBahath" runat="server" Width='100px' Height='25' />
                                    <br />
                                    <asp:Label ID="lblRaeesLagnatAlBahath" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="WidthText3" align="center" style="font-family: 'Alwatan'; font-size: 14px;">
                                رئيس مجلس الإدارة
                                <div id="IDRaeesPrint">
                                    <asp:Image ID="ImgRaeesMaglesAlEdarah" runat="server" Width='100px' Height='25' />
                                    <br />
                                    <asp:Label ID="lblRaeesMaglesAlEdarah" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="WidthText3" align="center">
                                <div runat="server" id="IDKhatm" visible="false" align="left">
                                    <img src="../../ImgSystem/ImgSignature/الختم.png" width="100" />
                                </div>
                            </div>
                        </div>
                        <table style="width: 96%" align="center">
                            <asp:Panel ID="pnlDlPrint" runat="server" Visible="false">
                                <tr>
                                    <td align="center" style="width: 25%">
                                        <asp:Image ID="ImgAlBaheth2" runat="server" Width='100px' Height='25' />
                                        <br />
                                        <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" Enabled="false">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" style="width: 25%">
                                        <asp:Image ID="ImgModer2" runat="server" Width='100px' Height='25' />
                                        <br />
                                        <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" Enabled="false">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" style="width: 25%">
                                        <asp:Image ID="ImgRaeesLagnatAlBahath2" runat="server" Width='100px' Height='25' />
                                        <br />
                                        <asp:DropDownList ID="DLRaeesLagnatAlBahath" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" Enabled="false">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td align="center" style="width: 25%">
                                        <asp:Image ID="ImgRaeesMaglesAlEdarah2" runat="server" Width='100px' Height='25' />
                                        <br />
                                        <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" Enabled="false">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </asp:Panel>
                        </table>

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
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>
