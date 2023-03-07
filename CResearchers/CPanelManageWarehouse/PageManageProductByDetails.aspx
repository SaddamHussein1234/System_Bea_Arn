<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPanelManageWarehouse/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductByDetails.aspx.cs" Inherits="CResearchers_CPanelManageWarehouse_PageManageProductByDetails" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css" rel="stylesheet" type="text/css" />
    <link href="../css/chosen.css" rel="stylesheet" />
    <style>
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
                Width: 19%;
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
            .WidthMaglis {
                float: right;
                Width: 19%;
                padding-right: 5px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthMaglis {
                Width: 95%;
            }
        }
    </style>
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageManageProductShippingWarehouse.aspx" data-toggle="tooltip" title="إضافة أمر شحن للمستودع" class="btn btn-primary" runat="server" id="IDAdd"><i class="fa fa-plus"></i></a>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة" OnClientClick="return insertConfirmation();">
                    <i class="fa fa-print"></i></asp:LinkButton>

                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="">قائمة تفاصيل العمليات التي حصلت لهذا المنتج</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة تفاصيل العمليات التي حصلت لهذا المنتج
                        </h3>
                        <div style="float: left" runat="server" visible="false">
                            <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;"
                                class="btn btn-info btn-fill pull-right" />
                            &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText2" placeholder=" إبحث هنا ... "></asp:TextBox>
                        </div>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft" Visible="False">
                            <table class='table' style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>
                                            <div class="HideNow">
                                                <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                            </div>
                                            <div align="center" class="w">
                                                <div>
                                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <hr style='border: solid; border-width: 1px; width: 100%' />
                                            <span>الكميات الواردة للمستودع
                                            </span>
                                            <asp:GridView ID="GVProductGet" runat="server" AutoGenerateColumns="False" DataKeyNames="_IDItem"
                                                Width="100%" CssClass="footable" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVProductGet_RowDataBound"
                                                UseAccessibleHeader="False">
                                                <Columns>
                                                    <asp:BoundField DataField="_IDItem" HeaderText="_IDItem" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="_IDItem" Visible="false" />
                                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ر/الشحنة" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                        <ItemTemplate>
                                                            <%# Eval("_IDNumberProduct")%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="بيانات الشحنة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px">رقم الشحنة <%# Eval("_IDNumberProduct")%> / الإنتماء <%# Eval("AffiliationName")%> / الصنف <%# Eval("CategoryName")%> / المنتج <%# Eval("ProductName")%> 
                                                            </span>
                                                            <br />
                                                            <span style="font-size: 11px" class="HideThis">
                                                                <%# ClassSaddam.FAmeenAlmostodaa2((bool) (Eval("_IsStorekeeper")))%>
                                                              , <%# ClassSaddam.FCheckAllowModer2((bool) (Eval("_IsModer")))%> 
                                                              , <%# ClassSaddam.FAmeenAlsondoq2((bool) (Eval("_IsAmmenAlSondoq")))%>
                                                              , <%# ClassSaddam.FRaeesMaglis2((bool) (Eval("_IsRaeesMaglisAlEdarah")))%>
                                                            </span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="الكمية والسعر" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px">العدد
                                                <asp:Label ID="lblCount" runat="server" Font-Size="12px" Text='<%# Eval("_CountProduct")%>'></asp:Label>
                                                                / السعر الفردي <%# Eval("_PriceOfTheGrain")%>
                                             / الإجمالي
                                                <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="11px" Text='<%# Eval("_TotalPrice")%>'></asp:Label>
                                                                <%--<br />
                                                النوع ( <%# Eval("_IDType")%> )--%>
                                                            </span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="التاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px">
                                                                <%--الإنتاج <%# ClassDataAccess.FChangeF((DateTime) (Eval("_ProductionDate")))%> / 
                                               الإنتهاء <%# ClassDataAccess.FChangeF((DateTime) (Eval("_ExpiryDate")))%>
                                                <br />--%>
                                                تاريخ وصول الشحنة <%# ClassDataAccess.FChangeF((DateTime) (Eval("_DateCaming")))%>
                                                            </span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="مدخل البيانات" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px">
                                                                <%# ClassQuaem.FAlBaheth((Int32) Eval("_IDAdmin"))%> - <%# ClassDataAccess.FChangeF((DateTime) (Eval("_DateAddProduct")))%>
                                                                <br />
                                                                <%# ClassQuaem.FAlBahethByEdit((Int32) (Eval("_IDUpdate")))%>
                                                            </span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تاريخ الإدخال" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                        <ItemTemplate>
                                                            <%# ClassDataAccess.FChangeF((DateTime) (Eval("_DateAddProduct")))%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16" Visible="false">
                                                        <ItemTemplate>
                                                            <a href='#.aspx?ID=<%# Eval("_billNumber")%>&XID=<%# Eval("_IDMosTafeed")%>&Name=<%# ClassMosTafeed.FGetMosTafeed((Int32) (Eval("_IDMosTafeed")))%>&Type=Moder' title="عرض التفاصيل" data-toggle="tooltip"
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
                                            <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                            <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            - <span style="font-size: 12px; padding-right: 5px">مجموع الشحنات : </span>
                                            <asp:Label ID="lblSum" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            - <span style="font-size: 12px; padding-right: 5px">السعر الكلي : </span>
                                            <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                                            <hr style='border: solid; border-width: 1px; width: 100%' />
                                            <span>الكميات الصادرة من المستودع
                                            </span>
                                            <asp:GridView ID="GVProductSet" runat="server" AutoGenerateColumns="False" DataKeyNames="_IDItem"
                                                Width="100%" CssClass="footable" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVProductSet_RowDataBound"
                                                UseAccessibleHeader="False">
                                                <Columns>
                                                    <asp:BoundField DataField="_IDItem" HeaderText="_IDItem" InsertVisible="False" ReadOnly="True"
                                                        SortExpression="_IDItem" Visible="false" />
                                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ر/الشحنة" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                        <ItemTemplate>
                                                            <%# Eval("_IDNumberProduct")%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="بيانات المستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# ClassMosTafeed.FGetMosTafeed((Int32) (Eval("_IDMosTafeed")))%> / رقم الإمر <%# Eval("_billNumber")%> /
                                                          <%# ClassSaddam.FCheckTaleef((Int32) (Eval("_IDMosTafeed")))%>
                                                        </span>
                                                        <br />
                                                            <span style="font-size: 11px" class="HideThis">
                                                                <%# ClassSaddam.FAmeenAlmostodaa2((bool) (Eval("_IsStorekeeper")))%>
                                                              , <%# ClassSaddam.FCheckAllowModer2((bool) (Eval("_IsModer")))%> 
                                                              , <%# ClassSaddam.FAmeenAlsondoq2((bool) (Eval("_IsAmmenAlSondoq")))%>
                                                              , <%# ClassSaddam.FRaeesMaglis2((bool) (Eval("_IsRaeesMaglisAlEdarah")))%>
                                                            </span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="الكمية والسعر" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px">العدد
                                                <asp:Label ID="lblCount" runat="server" Font-Size="12px" Text='<%# Eval("_CountProduct")%>'></asp:Label>
                                                                / السعر الفردي <%# Eval("_PriceOfTheGrain")%>
                                             / الإجمالي
                                                <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="11px" Text='<%# Eval("_TotalPrice")%>'></asp:Label>
                                                                <%--<br />
                                                النوع ( <%# ClassSaddam.FAlTypeEvint(Convert.ToInt32((string) (Eval("_IDType"))))%> )--%>
                                                            </span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="التاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span>تاريخ الطلب <%# ClassDataAccess.FChangeF((DateTime) (Eval("_DateCaming")))%>
                                                            </span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="مدخل البيانات" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px">
                                                                <%# ClassQuaem.FAlBaheth((Int32) Eval("_IDAdmin"))%> - <%# ClassDataAccess.FChangeF((DateTime) (Eval("_DateAddProduct")))%>
                                                                <br />
                                                                <%# ClassQuaem.FAlBahethByEdit((Int32) (Eval("_IDUpdate")))%>
                                                            </span>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="تاريخ الإدخال" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                        <ItemTemplate>
                                                            <%# ClassDataAccess.FChangeF((DateTime) (Eval("_DateAddProduct")))%>
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
                                            <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                            <asp:Label ID="lblCount2" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            - <span style="font-size: 12px; padding-right: 5px">مجموع الطلبات : </span>
                                            <asp:Label ID="lblSum2" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            - <span style="font-size: 12px; padding-right: 5px">السعر الكلي : </span>
                                            <asp:Label ID="lblTotalPrice2" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            <asp:Label ID="Label5" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                                            <div align="Left" class="HideThis">
                                                <img src='../../Img/IconTrue.png' style='width: 20px' />
                                                <span style="font-size: 11px">موافق</span>
                                                <img src='../../Img/IconFalse.png' style='width: 20px' />
                                                <span style="font-size: 11px">غير موافق</span>
                                            </div>
                                            <hr style='border: solid; border-width: 1px; width: 100%' />
                                            <div class="container-fluid" dir="rtl" runat="server">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <div class="WidthMaglis" align="center" runat="server" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                أمين المستودع
                                                                <br />
                                                                <asp:Image ID="ImgIDStorekeeper" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:Label ID="lblIDStorekeeper" runat="server" Font-Size="20px"></asp:Label>
                                                                <asp:DropDownList ID="DLIDStorekeeper2" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLIDStorekeeper2_SelectedIndexChanged"
                                                                    CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="WidthMaglis" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                مدير الجمعية
                                                                <br />
                                                                <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:Label ID="lblModerAlGmeiah" runat="server" Font-Size="20px"></asp:Label>
                                                                <asp:DropDownList ID="DLModerAlGmeiah2" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLModerAlGmeiah2_SelectedIndexChanged"
                                                                    CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="WidthMaglis" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                أمين الصندوق
                                                                <br />
                                                                <asp:Image ID="ImgAmeenAlSondoq" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:Label ID="lblAmeenAlSondoq" runat="server" Font-Size="20px"></asp:Label>
                                                                <asp:DropDownList ID="DLAmeenAlSondoq2" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLAmeenAlSondoq2_SelectedIndexChanged"
                                                                    CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="WidthMaglis" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                رئيس مجلس الإدارة
                                                                <br />
                                                                <asp:Image ID="ImgRaeesMaglesAlEdarah" runat="server" Width='100px' Height='25' />
                                                                <br />
                                                                <asp:Label ID="lblRaeesMaglesAlEdarah" runat="server" Font-Size="20px"></asp:Label>
                                                                <asp:DropDownList ID="DLRaeesMaglesAlEdarah3" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLRaeesMaglesAlEdarah3_SelectedIndexChanged"
                                                                    CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                    <asp:ListItem Value=""></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <div class="WidthMaglis" align="center">
                                                                <div runat="server" id="IDKhatm" align="left" style="margin-top: 0px">
                                                                    <img src="../../ImgSystem/ImgSignature/الختم.png" width="120" />
                                                                </div>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th>
                                            <hr style='border: solid; border-width: 1px; width: 100%' />
                                            <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                        </th>
                                    </tr>
                                </tfoot>
                            </table>
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
                                <h3 style="font-size: 20px">يرجى إدخال جملة البحث
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
        <br />
        <br />
        <br />
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

