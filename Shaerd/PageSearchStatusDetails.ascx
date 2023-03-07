<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageSearchStatusDetails.ascx.cs" Inherits="Shaerd_PageSearchStatusDetails" %>

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
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم الطلب ... "></asp:TextBox>
                    </div>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageSearchStatus.aspx">قائمة بحث الحالات</a></li>
                    <li><a href="">طلب بحث حالة مستفيد</a></li>
                </ul>
            </div>
        </div>
        <div id="bill">
            <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
                <div class="page">
                    <table style="width: 100%;">
                    <tr>
                        <td colspan="2">
                            <div class="WidthText4" style="margin-right: 15px">
                                <asp:TextBox ID="txtTitle" runat="server" Font-Size="14px" class="form-control" placeholder="عنوان البحث" Text="تقرير بحث حالة مستفيد جديد" Style="text-align: center; width: 95%; background-color: #00cc1a; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                            </div>
                            <div class="WidthText">
                                <asp:Image ID="imgBarCode" runat="server" alt='Loding' />
                            </div>
                            <div class="WidthText2">
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
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <table style="width: 95%">
                                <tr>
                                    <td class="StyleTD" colspan="2">
                                        <div class="WidthText4">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم تقرير البحث
                                                    </td>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                        <asp:Label ID="lblNumberOrder" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="WidthText4">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">تاريخ التقرير
                                                    </td>
                                                    <td style="width: 25%;">
                                                        <asp:Label ID="lblDateQarar" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="StyleTD" colspan="2">
                                        <div class="WidthText74">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">إسم المستفيد
                                                    </td>
                                                    <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                        <asp:Label ID="lblNameMosTafeed" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="WidthText1">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">القرية
                                                    </td>
                                                    <td style="width: 50%">
                                                        <asp:Label ID="lblAlqariah" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="StyleTD" colspan="2">
                                        <div class="WidthText4">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم السجل المدني
                                                    </td>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                        <asp:Label ID="lblNumberAlSegelAlMadany" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="WidthText4">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم المستفيد
                                                    </td>
                                                    <td style="width: 25%;">
                                                        <asp:Label ID="lblNumberMostafeed" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="StyleTD" colspan="2">
                                        <div class="WidthText4">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الحالة
                                                    </td>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                        <asp:Label ID="lblHalafAlMosTafeed" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="WidthText4">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم الجوال
                                                    </td>
                                                    <td style="width: 25%;">
                                                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
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
                                                        بناءً على دراسة الحالة للمستفيد الموضح بياناته بعاليه وبعد الزيارة الميدانية فإن ملاحظاتنا للحالة هي : 
                                                    </p>
                                                    <i class="fa fa-minus"></i>
                                                    <asp:Label ID="lblAllowState" runat="server"></asp:Label>

                                                </div>
                                            </div>
                                            <div class="WidthText5" runat="server" id="IDAllow" visible="false">
                                                <div class="form-group">
                                                    <p>
                                                        وعليه فإننا نوصي بـ
                                                    </p>
                                                    <asp:CheckBox ID="CBAllow" runat="server" Enabled="false" />
                                                    <span>قبول الحالة </span>

                                                </div>
                                            </div>
                                            <div class="WidthText5" runat="server" id="IDNotAllow" visible="false">
                                                <div class="form-group">
                                                    <asp:CheckBox ID="CBNotAllow" runat="server" Enabled="false" />
                                                    <span>عدم قبول الحالة </span>
                                                    <br />
                                                    <br />
                                                    <div runat="server" id="IDNotAllowlabel" visible="false">
                                                        <i class="fa fa-star"></i><span>ملاحظة عدم قبول الحالة : </span>
                                                        <br />
                                                        <i class="fa fa-minus"></i>
                                                        <asp:Label ID="lblWhayNotAllow" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="WidthText5">
                                                <div class="form-group">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 50%"></td>
                                                            <td style="width: 25%">
                                                                <p>
                                                                    ولكم أطيب التحايا ... 
                                                                    <br />
                                                                    <br />
                                                                </p>
                                                            </td>
                                                            <td style="width: 25%">
                                                                <p style="padding-top: 40px">
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
                            <div class="container-fluid">
                                <div class="panel-body">
                                    <div class="content-box-large">
                                        <div class="container-fluid" dir="rtl">
                                            <div class="WidthText5">
                                                <hr />
                                                <div align="center">
                                                    <strong>أعضاء لجنة البحث الإجتماعي
                                                    </strong>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <div class="WidthText2" style="text-align: center; padding: 4px; border: double; border-width: 2px; border-color: #a1a0a0;">
                                                                <strong>الباحث الإجتماعي
                                                                </strong>
                                                                <br />
                                                                <asp:Image ID="ImgAlBaheth" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" Enabled="false">
                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblAlbaheth" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                            <div class="WidthText2" style="float: left; text-align: center; padding: 4px; border: double; border-width: 2px; border-color: #a1a0a0;">
                                                                <strong>مدير الجمعية
                                                                </strong>
                                                                <br />
                                                                <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" Enabled="false">
                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <asp:Label ID="lblModerAlGmeiah" runat="server" Visible="false"></asp:Label>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                    </tr>
                                                </table>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 33.3%;">
                                                            <div class="WidthText2">
                                                                <div style="color: #fff">
                                                                    _
                                                                    <br />
                                                                </div>
                                                            </div>
                                                        </td>
                                                        <td valign="top" style="width: 33.3%; text-align: center; padding: 4px; border: double; border-width: 2px; border-color: #a1a0a0;">
                                                            <strong>رئيس لجنة البحث الإجتماعي 
                                                            </strong>
                                                            <br />
                                                            <asp:Image ID="ImgRaeesLagnatAlBahath" runat="server" Width='100px' Height='25' />
                                                            <br />
                                                            <asp:DropDownList ID="DLRaeesLagnatAlBahath" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" Enabled="false">
                                                                <asp:ListItem Value=""></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:Label ID="lblRaeesLagnatAlBahath" runat="server" Visible="false"></asp:Label>
                                                            <br />
                                                        </td>
                                                        <td style="width: 33.3%; text-align: center;">
                                                            <div class="WidthText2">
                                                                <div runat="server" id="IDKhatm" align="left" visible="false">
                                                                    <img src="../ImgSystem/ImgSignature/الختم.png" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                        </td>
                    </tr>
                </table>
                </div>
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
                                            <h3 style="font-size: 20px">
                                                <asp:Label ID="lblMsg" runat="server" Text="لا توجد نتائج"></asp:Label>
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