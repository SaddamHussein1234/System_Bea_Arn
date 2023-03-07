<%@ Page Title="" Language="C#" MasterPageFile="~/CPBeneficiary/MPBeneficiary.master" AutoEventWireup="true" CodeFile="PageSupport.aspx.cs" Inherits="CPBeneficiary_PageSupport" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="GridView.css" rel="stylesheet" type="text/css" />
    <link href="css/chosen.css" rel="stylesheet" />
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
            .WidthText2 {
                Width: 250px;
                height: 36px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthText2 {
                Width: 150px;
                height: 36px;
            }
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
                Width: 17%;
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
            .WidthTex {
                Width: 95%;
            }

            .WidthText {
                Width: 95%;
            }

            .WidthText1 {
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
            .WidthMaglis24 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthMaglis24 {
                Width: 95%;
            }
        }
    </style>

    <script type="text/javascript">
        function insertConfirmation() {
            var answer = confirm("هل تريد الإستمرار ؟")
            if (answer) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
    <div class="page-header" runat="server" id="IDStar">
        <div class="container-fluid">
            <div class="pull-right">
                <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                    title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click" Visible="true"
                    title="طباعة" OnClientClick="return insertConfirmation();">
                    <i class="fa fa-print"></i></asp:LinkButton>
            </div>
            <div class="container-fluid">
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="">قائمة فرز دعم المستفيد</a></li>
                </ul>
            </div>
        </div>
    </div>

        <div class="page-header">
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-list"></i>قائمة فرز الدعم العيني
                    </h3>
                    <div style="float: left">
                    </div>
                </div>
                <div class="panel-body">
                    <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft" Visible="false">
                        <div class="table table-responsive">
                            <table class='table' style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>
                                            <div align="center" class="w">
                                                <div>
                                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="قائمة فرز أوامر الصرف" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                </div>
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <div class="container-fluid" style="text-align: right; font-size: 12px">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td class="StyleTD">الاسم :
                                                            </td>
                                                            <td class="StyleTD" colspan="2">
                                                                <asp:Label ID="lblName" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">رقم الملف :
                                                            </td>
                                                            <td class="StyleTD">
                                                                <asp:Label ID="lblNumberFile" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">القرية :
                                                            </td>
                                                            <td class="StyleTD">
                                                                <asp:Label ID="lblAlQariah" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">الجنس :
                                                            </td>
                                                            <td class="StyleTD">
                                                                <asp:Label ID="lblGender" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="StyleTD">رقم الهاتف :
                                                            </td>
                                                            <td class="StyleTD" colspan="2">0<asp:Label ID="lblPhone" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">حالة المستفيد :
                                                            </td>
                                                            <td class="StyleTD" colspan="2">
                                                                <asp:Label ID="lblHalatAlmostafeed" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">السجل المدني :
                                                            </td>
                                                            <td class="StyleTD" colspan="2">
                                                                <asp:Label ID="lblNumberSigal" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="StyleTD">تاريخ الميلاد :
                                                            </td>
                                                            <td class="StyleTD" colspan="3">
                                                                <asp:Label ID="lblDateBrithDay" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">العمر :
                                                            </td>
                                                            <td class="StyleTD" colspan="4">
                                                                <asp:Label ID="lblAge" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                            </div>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GVExchangeOrders" runat="server" AutoGenerateColumns="False" DataKeyNames="_billNumber"
                                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVExchangeOrders_RowDataBound"
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
                                                    <asp:BoundField DataField="_billNumber" HeaderText="_billNumber" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="_billNumber" Visible="false" />
                                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# ClassMosTafeed.FGetMosTafeed((Int32) (Eval("_IDMosTafeed")))%> / رقم الإمر <%# Eval("_billNumber")%>
                                                            <br />
                                                            <span style="font-size: 11px" class="HideThis">
                                                                <%# ClassSaddam.FCheckAllowModer2((bool) (Eval("_IsModer")))%> 
                                                                    , <%# ClassSaddam.FAmeenAlsondoq2((bool) (Eval("_IsAmmenAlSondoq")))%>
                                                                    , <%# ClassSaddam.FRaeesMaglis2((bool) (Eval("_IsRaeesMaglisAlEdarah")))%>
                                                                    , <%# ClassSaddam.FAmeenAlmostodaa2((bool) (Eval("_IsStorekeeper")))%>
                                                            </span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="المشروع" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# ClassQuaem.FSupportType(Convert.ToInt64(Eval("_IDCategory"))) %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="التاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# ClassDataAccess.FChangeF((DateTime) (Eval("_ProductionDate")))%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="العدد" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCount" runat="server" Font-Size="12px" Text='<%# ClassProductShopWarehouse.FCount((Int32) (Eval("_billNumber")))%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="مدخل البيانات" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px">
                                                                <%# ClassQuaem.FAlBaheth((Int32) Eval("_IDAdmin"))%> 
                                                            </span>
                                                            <div style="font-size: 11px" class="HideThis">
                                                                <span style="font-size: 11px">الباحث/</span><%# ClassQuaem.FAlBaheth((Int32) Eval("_IDDelivery"))%>
                                                                    , <%# ClassSaddam.FAlbaheth2((Convert.ToBoolean(Eval("_IsReceived"))),(Convert.ToBoolean(Eval("_IsNotReceived"))))%>
                                                            </div>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تاريخ الإدخال" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                        <ItemTemplate>
                                                            <%--<%# ClassDataAccess.FChangeF((DateTime) (Eval("_DateAddProduct")))%>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px"
                                                                Text='<%# ClassProductShopWarehouse.FPriceByTalef(Convert.ToInt32(Eval("_billNumber")) , Convert.ToInt64(Eval("_IDCategory")))%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                                        <ItemTemplate>
                                                            <a href='PageManageProductAddThePriceToOrder.aspx?ID=<%# Eval("_billNumber")%>&XID=<%# Eval("_IDMosTafeed")%>&XIDCate=<%# Eval("_IDCategory")%>&IsCart=<%# Eval("_IsCart") %>&IsDevice=<%# Eval("_IsDevice") %>&IsTathith=<%# Eval("_IsTathith") %>&IsTalef=<%# Eval("_IsTalef") %>' title="عرض التفاصيل" data-toggle="tooltip"
                                                                class="btn btn-info"><span class="fa fa-eye"></span></a>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                                    PreviousPageText=" السابق - " PageButtonCount="30" />
                                                <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                                <RowStyle CssClass="rows"></RowStyle>
                                                <RowStyle CssClass="rows"></RowStyle>
                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                            <div>
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <div class="container-fluid" dir="rtl" runat="server">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td>
                                                                <div class="WidthMaglis24" align="center" runat="server" visible="false">
                                                                    الباحث الإجتماعي
                                                                        <br />
                                                                    <asp:Image ID="ImgAlBaheth" runat="server" Width='100px' Height='25' />
                                                                    <br />
                                                                    <asp:Label ID="lblAlBaheth" runat="server" Font-Size="11px"></asp:Label>
                                                                    <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="g2" Width="100%"
                                                                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                        <asp:ListItem Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px">
                                                                    مدير الجمعية
                                                                        <br />
                                                                    <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' />
                                                                    <br />
                                                                    <asp:Label ID="lblModerAlGmeiah" runat="server" Font-Size="20px"></asp:Label>
                                                                    <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" Width="100%"
                                                                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                        <asp:ListItem Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px">
                                                                    أمين الصندوق
                                                                        <br />
                                                                    <asp:Image ID="ImgAmeenAlSondoq" runat="server" Width='100px' Height='25' />
                                                                    <br />
                                                                    <asp:Label ID="lblAmeenAlSondoq" runat="server" Font-Size="20px"></asp:Label>
                                                                    <asp:DropDownList ID="DLAmeenAlSondoq" runat="server" ValidationGroup="g2" Width="100%"
                                                                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                        <asp:ListItem Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px">
                                                                    رئيس مجلس الإدارة
                                                                        <br />
                                                                    <asp:Image ID="ImgRaeesMaglesAlEdarah" runat="server" Width='100px' Height='25' />
                                                                    <br />
                                                                    <asp:Label ID="lblRaeesMaglesAlEdarah" runat="server" Font-Size="20px"></asp:Label>
                                                                    <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" Width="100%"
                                                                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                        <asp:ListItem Value=""></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="WidthMaglis24" align="center">
                                                                    <div runat="server" id="IDKhatm" align="left">
                                                                        <img src="../../ImgSystem/ImgSignature/الختم.png" alt="" />
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th>
                                            <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                            <hr style='border: solid; border-width: 1px; width: 100%' />
                                            <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                            <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            - <span style="font-size: 12px; padding-right: 5px">عدد الطلبات : </span>
                                            <asp:Label ID="lblSum" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            - <span style="font-size: 12px; padding-right: 5px">المجموع : </span>
                                            <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                                            <div align="Left" class="HideThis">
                                                <img src='../../Img/IconTrue.png' style='width: 20px' alt="" />
                                                <span style="font-size: 11px">موافق</span>
                                                <img src='../../Img/IconFalse.png' style='width: 20px' alt="" />
                                                <span style="font-size: 11px">غير موافق</span>
                                            </div>
                                        </th>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlNull" runat="server" Visible="False">
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
                    </asp:Panel>
                    <asp:Panel ID="pnlSelect" runat="server" Visible="False">
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
                            <h3 style="font-size: 20px">يرجى تحديد البيانات
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
                    </asp:Panel>
                </div>
            </div>
        </div>
    </div>
    <script src="css/chosen.jquery.js" type="text/javascript"></script>
    <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

