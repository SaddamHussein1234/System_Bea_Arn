<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductWarehousebyProductsCloseToCompletion.aspx.cs" Inherits="Cpanel_PageManageProductWarehousebyProductsCloseToCompletion" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <link href="css/chosen.css" rel="stylesheet" />
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
    </style>
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <li class="fa fa-refresh"></li></asp:LinkButton>
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة" OnClientClick="return insertConfirmation();">
                    <li class="fa fa-print"></li></asp:LinkButton>

                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="">قائمة تفاصيل المنتجات التي قاربت على الإنتهاء</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة تفاصيل المنتجات التي قاربت على الإنتهاء
                        </h3>
                        <div style="float: left" runat="server" visible="false">
                        </div>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft" Visible="False">
                            <div style="background-color: #8bfe5b;">
                                <div align='center' class='w'>
                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="قائمة تفاصيل المنتجات التي قاربت على الإنتهاء" placeholder="عنوان البحث" Style="text-align: center; width: 100%"></asp:TextBox>
                                </div>
                            </div>
                            <hr />
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
                                            <span style="font-size: 11px">
                                                <%# ClassSaddam.FAmeenAlmostodaa((bool) (Eval("_IsStorekeeper")))%>
                                              , <%# ClassSaddam.FCheckAllowModer((bool) (Eval("_IsModer")))%> 
                                              , <%# ClassSaddam.FAmeenAlsondoq((bool) (Eval("_IsAmmenAlSondoq")))%>
                                              , <%# ClassSaddam.FRaeesMaglis((bool) (Eval("_IsRaeesMaglisAlEdarah")))%>
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
                                                <br />
                                                النوع ( <%# Eval("_IDType")%> )
                                            </span>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="التاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <span style="font-size: 11px">الإنتاج <%# ClassDataAccess.FChangeF((DateTime) (Eval("_ProductionDate")))%> / 
                                        الإنتهاء <%# ClassDataAccess.FChangeF((DateTime) (Eval("_ExpiryDate")))%>
                                                <br />
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
                                <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                    PreviousPageText=" السابق - " PageButtonCount="30" />
                                <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                <RowStyle CssClass="rows"></RowStyle>
                                <RowStyle CssClass="rows"></RowStyle>
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
                            <span style="font-size: 12px; padding-right: 5px">عدد السجلات : </span>
                            <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                            - <span style="font-size: 12px; padding-right: 5px">مجموع الشحنات : </span>
                            <asp:Label ID="lblSum" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                            - <span style="font-size: 12px; padding-right: 5px">السعر الكلي : </span>
                            <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                            <div align="Left">
                                <img src='../Img/IconTrue.png' style='width: 20px' />
                                <span style="font-size: 11px">موافق</span>
                                <img src='../Img/IconFalse.png' style='width: 20px' />
                                <span style="font-size: 11px">غير موافق</span>
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
</asp:Content>

