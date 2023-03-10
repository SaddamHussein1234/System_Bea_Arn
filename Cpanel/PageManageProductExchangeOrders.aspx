<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductExchangeOrders.aspx.cs" Inherits="Cpanel_PageManageProductExchangeOrders" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSearch.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

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
                Width: 19%;
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
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageManageProductMatterOfExchange.aspx" data-toggle="tooltip" title="إضافة أمر صرف جديد" class="btn btn-primary"><i class="fa fa-plus"></i></a>
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
                        <li><a href="PageManageProductExchangeOrders.aspx">قائمة فرز أوامر الصرف</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة فرز أوامر الصرف
                        </h3>
                        <div style="float: left">
                        </div>
                    </div>
                    <div class="panel-body">
                        <div style="float: right; width: 230px">
                            <asp:Label ID="lbmsg" runat="server" Text="فرز أوامر"></asp:Label>
                            لــ :
                                <asp:DropDownList ID="DLType" runat="server" ValidationGroup="g2" CssClass="form-control2"
                                    Width="150px" Height="30px">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="1">أمر صرف لمستفيد</asp:ListItem>
                                    <%--<asp:ListItem Value="2">أمر صرف لموظف</asp:ListItem>--%>
                                    <asp:ListItem Value="3">تالف</asp:ListItem>
                                </asp:DropDownList>
                            <asp:Label ID="lblType" runat="server" Text="حدد الامر * " ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                        <div style="float: right; width: 250px">
                            <asp:Label ID="Label1" runat="server" Text="مشروع : "></asp:Label>
                            <asp:DropDownList ID="DLCategory" runat="server" ValidationGroup="g2" CssClass="form-control2"
                                Width="170px" Height="30px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblCategory" runat="server" Text="حدد المشروع * " ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                        <div style="float: right; width: 150px">
                            <div class="col-sm-3">
                                <div class="input-group date " style="margin-right: -10px;">
                                    <asp:TextBox ID="txtDateFrom" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; width: 100px"></asp:TextBox>
                                    <asp:Label ID="lblDateFrom" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>

                                </div>
                            </div>
                        </div>
                        <div style="float: right; width: 150px">
                            <div class="col-sm-3">
                                <div class="input-group date " style="margin-right: -10px;">
                                    <asp:TextBox ID="txtDateTo" runat="server" placeholder="إلى تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; width: 100px"></asp:TextBox>
                                    <asp:Label ID="lblDateTo" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>

                                </div>
                            </div>
                        </div>
                        <div style="float: right; width: 150px">
                            <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;" ValidationGroup="g2"
                                class="btn btn-info btn-fill" OnClick="btnSearch_Click" />
                        </div>
                        <hr />
                        <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft" Visible="False">
                            <div class="table table-responsive">
                                <table class='table' style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <div class="HideNow">
                                                    <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                                </div>
                                                <div align="center" class="w">
                                                    <div>
                                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="قائمة فرز أوامر الصرف" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </div>
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
                                                                <%# ClassMosTafeed.FGetMosTafeed((Int32) (Eval("_IDMosTafeed")))%> / رقم الإمر <%# Eval("_billNumber")%> / 
                                                                <%# ClassSaddam.FCheckTaleef((Int32) (Eval("_IDMosTafeed")))%>
                                                                <br />
                                                                <span style="font-size: 11px" class="HideThis">
                                                                    <%# ClassSaddam.FCheckAllowModer((bool) (Eval("_IsModer")))%> 
                                                                     , <%# ClassSaddam.FAmeenAlsondoq((bool) (Eval("_IsAmmenAlSondoq")))%>
                                                                     , <%# ClassSaddam.FRaeesMaglis((bool) (Eval("_IsRaeesMaglisAlEdarah")))%>
                                                                     , <%# ClassSaddam.FAmeenAlmostodaa((bool) (Eval("_IsStorekeeper")))%>
                                                                </span>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="لمشروع" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassQuaem.FSupportType((Int64) (Eval("_IDCategory")))%>
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
                                                        <asp:TemplateField HeaderText="المجموع" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                            <ItemTemplate>
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
                                                                    ,<%# ClassSaddam.FAlbaheth((Convert.ToBoolean(Eval("_IsReceived"))),(Convert.ToBoolean(Eval("_IsNotReceived"))))%>
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
                                                                <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" Text='<%# ClassProductShopWarehouse.FPrice((Int32) (Eval("_billNumber")))%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                                            <ItemTemplate>
                                                                <a href='PageManageProductAddThePriceToOrder.aspx?ID=<%# Eval("_billNumber")%>&XID=<%# Eval("_IDMosTafeed")%>&Name=<%# ClassMosTafeed.FGetMosTafeed((Int32) (Eval("_IDMosTafeed")))%>&Type=AlBaheth' title="عرض التفاصيل" data-toggle="tooltip"
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
                                                                    <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                        مدير الجمعية
                                                                        <br />
                                                                        <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' />
                                                                        <br />
                                                                        <asp:Label ID="lblModerAlGmeiah" runat="server" Font-Size="20px"></asp:Label>
                                                                        <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLModerAlGmeiah_SelectedIndexChanged"
                                                                            CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                            <asp:ListItem Value=""></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                        المشرف المالي
                                                                        <br />
                                                                        <asp:Image ID="ImgAmeenAlSondoq" runat="server" Width='100px' Height='25' />
                                                                        <br />
                                                                        <asp:Label ID="lblAmeenAlSondoq" runat="server" Font-Size="20px"></asp:Label>
                                                                        <asp:DropDownList ID="DLAmeenAlSondoq" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLAmeenAlSondoq_SelectedIndexChanged"
                                                                            CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                            <asp:ListItem Value=""></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="WidthMaglis24" align="center" style="font-family: 'Alwatan'; font-size: 20px;">
                                                                        رئيس مجلس الإدارة
                                                                        <br />
                                                                        <asp:Image ID="ImgRaeesMaglesAlEdarah" runat="server" Width='100px' Height='25' />
                                                                        <br />
                                                                        <asp:Label ID="lblRaeesMaglesAlEdarah" runat="server" Font-Size="20px"></asp:Label>
                                                                        <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLRaeesMaglesAlEdarah_SelectedIndexChanged"
                                                                            CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                                                            <asp:ListItem Value=""></asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                    <div class="WidthMaglis24" align="center">
                                                                        <div runat="server" id="IDKhatm" align="left">
                                                                            <img src="../ImgSystem/ImgSignature/الختم.png" width="120" />
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
                                                <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                - <span style="font-size: 12px; padding-right: 5px">عدد الطلبات : </span>
                                                <asp:Label ID="lblSum" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                - <span style="font-size: 12px; padding-right: 5px">المجموع : </span>
                                                <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                                                <div align="Left" class="HideThis">
                                                    <img src='../Img/IconTrue.png' style='width: 20px' />
                                                    <span style="font-size: 11px">موافق</span>
                                                    <img src='../Img/IconFalse.png' style='width: 20px' />
                                                    <span style="font-size: 11px">غير موافق</span>
                                                </div>
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
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
        <br />
        <br />
        <br />
        <script type="text/javascript"><!--
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
    //--></script>
        <script src="css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

