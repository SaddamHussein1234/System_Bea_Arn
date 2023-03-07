<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/WSM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageView.aspx.cs" Inherits="Cpanel_ERP_WSM_DamagedExchange_PageView" %>

<%@ Import Namespace="Library_CLS_Arn.WSM" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .C31_5 {float: right;Width: 31.5%;padding-left: 5px;}

        @media screen and (min-width: 768px) {
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
                padding-right: 5px;
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
        }

        @media screen and (max-width: 767px) {
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
        }

        @media screen and (min-width: 768px) {
            .WidthText20 {
                Width: 150px;
                height: 36px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthText20 {
                Width: 100px;
                height: 36px;
            }
        }

        @font-face {
            font-family: 'Alwatan';
            font-size: 18px;
            src: url(../fonts/AlWatanHeadlines-Bold.ttf);
        }

        .checkbox label input[type="checkbox"] {
            display: none;
        }

            .checkbox label input[type="checkbox"] + .cr > .cr-icon {
                opacity: 0;
            }

            .checkbox label input[type="checkbox"]:checked + .cr > .cr-icon {
                opacity: 1;
            }

            .checkbox label input[type="checkbox"]:disabled + .cr {
                opacity: .5;
            }
    </style>

    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                     <a runat="server" id="IDEdit" href="#" data-toggle="tooltip" title="تعديل الملف" class="btn btn-info">وضع التعديل</a>
                    <label class="control-label">
                        الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                    </label>
                    <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2"
                        Width="100" ValidationGroup="GDetails">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtSearchTalef" runat="server" CssClass="WidthText20" placeholder=" رقم الفاتورة ... "></asp:TextBox>
                        <asp:LinkButton ID="btnSearchTalef" runat="server" data-toggle="tooltip" title="جلب" OnClick="btnSearchTalef_Click"
                            class="btn btn-info"><span class="tip-bottom"><i class="fa fa-search" style="font-size:16px"></i></span></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="#">تفاصيل أمر صرف التالف</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid" runat="server" id="ProductByTalef" visible="false">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-list"></i>قائمة فاتورة أمر صرف تالف
                    </h3>
                    <div style="float: left">

                        <asp:LinkButton ID="LBPrintSaraf" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="LBPrintSaraf_Click"
                            title="طباعة">
                    <i class="fa fa-print"></i></asp:LinkButton>

                        <asp:LinkButton ID="btnDeleteTaleef" runat="server" class="btn btn-danger" Visible="false"
                            OnClientClick="return ConfirmDeleteTaleef();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
                        <asp:LinkButton ID="LBRefresh" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LBRefresh_Click"
                            title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                    </div>
                    <div style="float: left">
                        <span>حدد المشروع : <span style="color: red">*</span>
                        </span>
                        <asp:DropDownList ID="DLSupportType" runat="server" ValidationGroup="g2" CssClass="form-control2" Width="150" Height="36" style="margin-left:3px">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator13" runat="server"
                            ControlToValidate="DLSupportType" ErrorMessage="* حدد المشروع" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="panel-body">
                    <asp:Panel ID="pnlDataTalef" runat="server" Direction="RightToLeft">
                        <div class="">
                            <div align="center" class="w">
                                <table style="width: 100%; background-color: #ffffff; color: #393939">
                                    <tr>
                                        <td style="border: thin double #808080; border-width: 1px; width: 45%">
                                            <asp:TextBox ID="txtTitleTalef" runat="server" class="form-control" Text="عقد حصر وإتلاف" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                        </td>
                                        <td style="border: thin double #808080; border-width: 1px; width: 20%">
                                            <table style="width: 100%; font-size: 12px">
                                                <tr>
                                                    <td align="left" style="width: 60%; font-family: 'Alwatan'; font-size: 18px;">رقم الفاتورة / 
                                                    </td>
                                                    <td style="width: 40%; font-family: 'Alwatan'; font-size: 18px;">
                                                        <asp:Label ID="lblNumberTaleef" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="border: thin double #808080; border-width: 1px; width: 35%">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td align="left" style="width: 20%; font-size: 12px">التاريخ / 
                                                    </td>
                                                    <td style="width: 80%">
                                                        <asp:Label ID="lblDateHideTaleef" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div style="float: right; padding: 10px 10px 0 10px;" class="w">
                            <p style="font-size: 13px">
                                أنه في يوم
                                <asp:Label ID="lblToday" runat="server"></asp:Label>
                                <i class="fa fa-minus" style="color: #fff"></i>
                                بتاريخ
                                <asp:Label ID="lblDateToDay" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div style="float: left; padding: 10px 0 0 10px" class="w">
                            <table style="font-size: 12px">
                                <tr>
                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">مدخل البيانات :
                                        <asp:Label ID="lblDataEntryTaleef" runat="server" Font-Size="12px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                        <asp:Label ID="lblDateEntryTaleef" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr runat="server" id="IDUpdateTaleef" visible="false">
                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">
                                        <asp:Label ID="lblDataEntryEditTaleef" runat="server" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                        <asp:Label ID="lblDateEntryEditTaleef" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div align='center' class="w">
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
                                                            <i class="fa fa-star"></i> QR Code : 
                                                        </label><br />
                                                        <div align="center">
                                                            <asp:Image ID="ImgQRCode2" runat="server" alt='صورة QRCode' style="width:300px; height:300px;" />
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
                        <div align="right">
                            <p style="font-size: 13px">
                                تم إجتماع اللجنة لعقد الحصر والإتلاف في مقر الجمعية 
                                <br />
                                وتم حصر المواد التي تحتاج إلى الإتلاف حسب اللائحه على إتلاف المواد التاليه
                            </p>
                        </div>
                        <span class="hr"></span>
                        <asp:GridView ID="GVMatterOfExchangeByIDTaleef" runat="server" AutoGenerateColumns="False" DataKeyNames="_IDItem"
                            Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVMatterOfExchangeByIDTaleef_RowDataBound"
                            UseAccessibleHeader="False">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Width="10px">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="_IDItem" HeaderText="_IDItem" InsertVisible="False" ReadOnly="True"
                                    SortExpression="_IDItem" Visible="false" />
                                <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                    <ItemTemplate>
                                        <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="الصنف" HeaderStyle-ForeColor="#CCCCCC">
                                    <ItemTemplate>
                                        <%# Eval("CategoryName")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="المنتج" HeaderStyle-ForeColor="#CCCCCC">
                                    <ItemTemplate>
                                        <%# Eval("ProductName")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="التجزئة" HeaderStyle-ForeColor="#CCCCCC">
                                    <ItemTemplate>
                                        <%# Eval("_Count__Partition_") + " / <small>" + Eval("UnitName") + "</small>"%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="إجمالي التجزئة" HeaderStyle-ForeColor="#CCCCCC">
                                    <ItemTemplate>
                                        <%# Eval("_Sum__Partition_") + " / <small>" + Eval("UnitName") + "</small>"%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="العدد" HeaderStyle-ForeColor="#CCCCCC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCount" runat="server" Font-Size="12px" Text='<%# Eval("_Count_Product_")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="السعر الفردي" HeaderStyle-ForeColor="#CCCCCC">
                                    <ItemTemplate>
                                        <%# Eval("_One_Price_")%> <%# XMony %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="السعر الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" Text='<%# Eval("_Total_Price_")%>'></asp:Label>
                                        <%# XMony %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderText="أُضيف من قبل" Visible="false">
                                    <ItemTemplate>
                                        <%# ClassQuaem.FAlBaheth((Int32) (Eval("_CreatedBy_")))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="بتاريخ" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate_Add" runat="server" 
                                            Text='<%# Eval("_CreatedDate_", "{0:dd/MM/yyyy}") + " " + Eval("_CreatedDate_", "{0:HH:mm tt}")  %>' Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                PreviousPageText=" السابق - " PageButtonCount="30" />
                            <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                            <RowStyle CssClass="rows"></RowStyle>
                            <RowStyle CssClass="rows"></RowStyle>
                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                        <asp:HiddenField ID="hfCountTaleef" runat="server" Value="0" />
                        <div style="display: none">
                            <span style="font-size: 12px; padding-right: 5px">عدد السجلات : </span>
                            <asp:Label ID="lblCountTaleef" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                            - <span style="font-size: 12px; padding-right: 5px">عدد التالف : </span>
                            <asp:Label ID="lblSumTaleef" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                        </div>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 15%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">المجموع : 
                                </td>
                                <td style="width: 65%; border: thin double #808080; border-width: 1px;" align="center">
                                    <asp:TextBox ID="lblSumTalef" runat="server" class="form-control" placeholder="المبلغ" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                </td>
                                <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                    <asp:Label ID="lblTotalPriceTaleef" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                    <asp:Label ID="lblMony" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <hr />
                        <div align="center">
                            <table style="width: 80%">
                                <tr>
                                    <td class="StyleTD" style="width: 10%;">
                                        <strong>م
                                        </strong>
                                    </td>
                                    <td class="StyleTD" style="width: 30%;">
                                        <strong>الإسم
                                        </strong>
                                    </td>
                                    <td class="StyleTD" style="width: 30%;">
                                        <strong>الصفة
                                        </strong>
                                    </td>
                                    <td class="StyleTD" style="width: 30%;">
                                        <strong>التوقيع
                                        </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="StyleTD">
                                        <strong>1
                                        </strong>
                                    </td>
                                    <td class="StyleTD">
                                        <asp:Label ID="lblRaees" runat="server" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="StyleTD">رئيس مجلس الإدارة
                                    </td>
                                    <td class="StyleTD">
                                        <asp:Image Width='100' Height='30' ID="IDRaees" runat="server" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="StyleTD">
                                        <strong>2
                                        </strong>
                                    </td>
                                    <td class="StyleTD" style="width: 30%;">
                                        <asp:Label ID="lblNaeeb" runat="server" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="StyleTD" style="width: 30%;">نائب رئيس مجلس الإدارة
                                    </td>
                                    <td class="StyleTD" style="width: 30%;">
                                        <asp:Image Width='100' Height='30' ID="IDNeeb" runat="server" Visible="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="StyleTD" style="width: 10%;">
                                        <strong>3
                                        </strong>
                                    </td>
                                    <td class="StyleTD" style="width: 30%;">
                                        <asp:Label ID="lblAmeen" runat="server" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="StyleTD" style="width: 30%;">أمين المستودع
                                    </td>
                                    <td class="StyleTD" style="width: 30%;">
                                        <asp:Image Width='100' Height='30' ID="IDAmeen" runat="server" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <hr />
                        <div align="left" style="margin-top: -60px" runat="server" id="IDKhatmTaleef" visible="false">
                            <img src="/ImgSystem/ImgSignature/الختم.png" width="120" />
                        </div>

                    </asp:Panel>
                    <asp:Panel ID="pnlNullTalef" runat="server" Visible="False">
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
                    </asp:Panel>
                </div>
            </div>
        </div>
        <div class="container-fluid" id="pnlStar" runat="server" visible="false">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label3" runat="server" Text="يرجى تحديد نوع الصرف"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="container-fluid" dir="rtl">
                            <asp:Panel ID="pnlSelect" runat="server">
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <div align="center">
                                    <h3 style="font-size: 20px">حدد نوع الصرف
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
                                <br />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            $('.date').datetimepicker({
                pickTime: false
            });

            $('.time').datetimepicker({
                pickDate: false
            });

            $('.datetime').datetimepicker({
                pickDate: true,
                pickTime: true
            });
        </script>
        <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

