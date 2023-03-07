<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/CRS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageView.aspx.cs" Inherits="Cpanel_ERP_CRS_Committee_Report_PageView" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

            .WidthText33 {
                float: right;
                Width: 33%;
                height: 100px;
                padding-left: 3px;
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

            .WidthText33 {
                Width: 95%;
            }
        }

        .MarginBottom {
            margin-top: 15px;
        }
    </style>
    <link href="/Cpanel/css/chosen.css" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a runat="server" id="ID_Edit_" class="btn btn-info" visible="false" data-toggle="tooltip" title="تعديل التقرير">الذهاب إلى وضع التعديل <span class="fa fa-edit"></span></a>
                    &nbsp;
                    السنة :
                    <asp:DropDownList ID="ddlYears" runat="server" ValidationGroup="VGDetails"
                        Height="25px" CssClass="form-control2" Style="font-size: 12px; height:36px;">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم التقرير ... "></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;" data-toggle="tooltip" title="بحث"
                        class="btn btn-info" OnClick="btnSearch_Click" />
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة">
                    <i class="fa fa-print"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" OnClick="LBExit_Click"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>

                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../">الرئيسية</a></li>
                    <li><a href="">تقرير اللجنة</a></li>
                </ul>
            </div>
        </div>
        <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft" Visible="false">
            <div id="IDPrint" runat="server" class="" dir="rtl">
                <div class="table table-responsive">
                    <style>
                        #headerCRS {top: 0; height:110px;} #footerCRS {bottom: 0; height:60px;}
                    </style>
                    <div id="IDBody" runat="server">
                    <table style="width: 95%; background-color: #ffffff; color: #393939" align="center">
                        <thead>
                            <tr>
                                <th colspan="2">
                                    <div class="hide" id="headerCRS">
                                        <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                    </div>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td align="right">
                                    <table style="width: 70%; text-align: right;">
                                        <tr>
                                            <td>
                                                <div>
                                                    <asp:Label ID="txtTitle" runat="server" class="form-control" Style="text-align: center; width: 95%; font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 100px;">
                                                <div style="text-align: center">
                                                    <a href='javaScript:void(0)' data-toggle='modal' data-target='#IDQRCode' title='تكبير'>
                                                        <asp:Image ID="ImgQRCode" runat="server" alt='QR Code' />
                                                    </a>
                                                    <div id="IDQRCode" class="modal fade in modal_New_Style HideThis">
                                                        <div class="modal-dialog " style="max-width: 450px">
                                                            <div class="modal-content">
                                                                <div class="modal-header no-border">
                                                                    <button type="button" class="close" data-dismiss="modal">×</button>
                                                                </div>
                                                                <div class="modal-body" id="modal_ajax_content">
                                                                    <div class="page-container">
                                                                        <div class="page-content">
                                                                            <div class=" panel-body">
                                                                                <label>
                                                                                    <i class="fa fa-star"></i>QR Code : 
                                                                                </label>
                                                                                <br />
                                                                                <div align="center">
                                                                                    <asp:Image ID="ImgQRCode2" runat="server" alt='صورة QRCode' Style="width: 300px; height: 300px;" />
                                                                                </div>
                                                                                <div class='clearfix'></div>
                                                                            </div>
                                                                            <div class="modal-footer">
                                                                                <button type="button" class="btn btn-default" data-dismiss="modal">اغلاق</button>
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
                                </td>
                                <%--<td align="center">
                                    <table style="width: 90%; display: none;">
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
                                </td>--%>
                            </tr>
                            <tr>
                                <td align="center">
                                    <table style="width: 100%">
                                        <tr>
                                            <td class="StyleTD" colspan="2">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 15%; border-left: double; border-width: 2px; border-color: #a1a0a0; text-align: center;">رقم التقرير
                                                        </td>
                                                        <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                            <asp:Label ID="lblNmber" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 15%; border-left: double; border-width: 2px; border-color: #a1a0a0; text-align: center;">تاريخ التقرير
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="StyleTD" colspan="2">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 15%; border-left: double; border-width: 2px; border-color: #a1a0a0; text-align: center;">
                                                             موضوع التقرير
                                                        </td>
                                                        <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                            <asp:Label ID="lblType" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 15%; border-left: double; border-width: 2px; border-color: #a1a0a0; text-align: center;">
                                                            مقر الاجتماع
                                                        </td>
                                                        <td style="width: 20%;">
                                                            <asp:Label ID="lblMeeting_Venue" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" Style="width: 95%; font-family: 'Alwatan'; font-size: 18px;" Text="الهدف من التقرير : "></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <span id="IDObjective_Of_the_Report" runat="server"></span>
                            <tr>
                                <td>
                                    <div class="form-group">
                                        <asp:Label ID="Label9" runat="server" Style="width: 95%; font-family: 'Alwatan'; font-size: 18px;" Text="توصيات التقرير : "></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <span id="IDReport_Recommendations" runat="server"></span>
                            <tr>
                                <td>
                                    <div id="pnlDataImages" runat="server" visible="false">
                                        <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" Style="width: 95%; font-family: 'Alwatan'; font-size: 18px;" Text="صور الاجتماع : "></asp:Label><br />
                                            <asp:Repeater ID="RPTImages" runat="server" Visible="false">
                                                <ItemTemplate>
                                                    <div style="float: right; width: 10%; height: 50px; padding-left: 3px;">
                                                        <a href='<%# "/" + Eval("_Src_") %>' target="_blank" title="تكبير الصورة" data-toggle="tooltip">
                                                            <img src='<%# "/" + Eval("_Src_") %>' style="border-radius: 5px; max-height: 100px; max-width: 100px;" />
                                                        </a>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Panel ID="pnlNullImages" runat="server" Visible="true">
                                                <div align="center" style="margin-top: -40px;">
                                                    <h4>لم يتم رفع صور بعد
                                                    </h4>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div align="center">
                                        <asp:Label ID="Label4" runat="server" Style="width: 95%; font-family: 'Alwatan'; font-size: 18px;" Text="أعضاء اللجنة"></asp:Label>
                                    </div>
                                    <div align="center">
                                        <asp:Panel ID="pnlDataCommittee_Members" runat="server" Visible="false">
                                            <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                                <thead>
                                                    <tr class="th">
                                                        <th class="StyleTD">م</th>
                                                        <th class="StyleTD">الإسم</th>
                                                        <th class="StyleTD">الصفة في المجلس</th>
                                                        <th class="StyleTD">الصفة في اللجنة</th>
                                                        <th class="StyleTD">التوقيع</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RPTCommittee_Members" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="width: 10px;" class="StyleTD">
                                                                    <span style="margin-right: 5px; font-size: 11px"><%# Container.ItemIndex + 1 %></span>
                                                                </td>
                                                                <td class="StyleTD">
                                                                    <span style="font-size: 12px"><%# ClassQuaem.FAlBaheth(Convert.ToInt32(Eval("_ID_Admin_")))%></span>
                                                                </td>
                                                                <td class="StyleTD">
                                                                    <span style="font-size: 12px"><%# ClassQuaem.FGetCommentAdmin(Convert.ToInt32(Eval("_ID_Admin_")))%></span>
                                                                </td>
                                                                <td class="StyleTD">
                                                                    <span style="font-size: 12px"><%# Eval("_Adjective_")%></span>
                                                                </td>
                                                                <td class="StyleTD">
                                                                    <img src='<%# ClassSaddam.FGetSignature(Convert.ToInt32(Eval("_ID_Admin_")), Convert.ToBoolean(Eval("_Is_Admin_"))) %>' alt="Img" width="50" height="25" />
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlNullCommittee_Members" runat="server" Visible="true">
                                            <div align="center">
                                                <br />
                                                <br />
                                                <h4>لم يتم إضافة الأعضاء بعد
                                                </h4>
                                                <br />
                                                <br />
                                                <br />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                    <div align="center">
                                        <asp:Label ID="Label7" runat="server" Style="width: 95%; font-family: 'Alwatan'; font-size: 18px;" Text="اعتماد رئيس مجلس الإدارة"></asp:Label><br />
                                        <asp:Label ID="txtNote" runat="server" Style="width: 95%; font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                                    </div>
                                    <div class="WidthText1" align="center" style="position: absolute; left: 10px; margin-top: -35px">
                                        <span style="font-family: 'Alwatan'; font-size: 18px;">رئيس مجلس الإدارة</span>
                                        <br />
                                        <asp:Image ID="Img_Chairman_Of_Board_Of_Directors" runat="server" Width='100px' Height='25' />
                                        <br />
                                        <asp:Label ID="lbl_Chairman_Of_Board_Of_Directors" runat="server" Style="width: 95%; font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                                    </div>
                                    <div align="right" style="margin-top: -50px" runat="server" id="IDKhatm" visible="false">
                                        <img src="/ImgSystem/ImgSignature/الختم.png" />
                                    </div>
                                    <div align="right" style="margin-top: -50px" runat="server" id="IDKhatmLodding" visible="false">
                                        <img src="/Cpanel/loader.gif" width="113" />
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                        <tfoot>
                            <tr class="th">
                                <td >
                                    <div class="hide" id="footerCRS">
                                        <footer>
                                            <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                        </footer>
                                    </div>
                                </td>
                            </tr>
                        </tfoot>
                    </table>
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
                            <asp:Label ID="Label6" runat="server" Text="يرجى إدخال رقم تقرير صحيح"></asp:Label>
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
        <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

