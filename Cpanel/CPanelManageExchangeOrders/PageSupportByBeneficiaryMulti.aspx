<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageSupportByBeneficiaryMulti.aspx.cs" Inherits="Cpanel_CPanelManageExchangeOrders_PageSupportByBeneficiaryMulti" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterWSM.ascx" TagPrefix="uc1" TagName="WUCFooterWSM" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<%@ Import Namespace="Library_CLS_Arn.WSM.Repostry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

    <script type="text/javascript">
<!--
    function Check_Click(objRef) {
        var row = objRef.parentNode.parentNode;
        var GridView = row.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var headerCheckBox = inputList[0];
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }
        headerCheckBox.checked = checked;
    }
    function checkAll(objRef) {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                if (objRef.checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        }
    }
    //-->
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVExchangeOrders.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].checked && chk[i].id.indexOf("chkAll") == -1) {
                    count++;
                }
            }
            if (count == 0) {
                alert("لم تقم بالتحديد على أي سجل");
                return false;
            }
            else {
                return confirm(" هل تريد الإستمرار ؟");
            }
        }
    </script>

    <link href="../GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
    <div class="page-header" runat="server" id="IDStar">
        <div class="container-fluid">
            <div class="pull-right">
                <asp:LinkButton ID="LBR" runat="server" class="btn btn-default" data-toggle="tooltip"
                    title="تحديث" OnClick="LBR_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
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
    <div class="col-sm-12">
        <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
            <span class="badge badge-pill badge-warning">تحذير</span>
            <asp:Label ID="lblMessageWarning" runat="server"></asp:Label>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
    </div>
    <div class="col-sm-12">
        <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible" role="alert">
            <span class="badge badge-pill badge-success">عملية ناجحة</span>
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                <span aria-hidden="true">&times;</span>
            </button>
        </div>
    </div>
    <div class="page-header" runat="server" id="IDTathith">
        <div class="container-fluid">
            <div class="pull-right">
                <asp:LinkButton ID="btnPrintMulti" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrintMulti_Click"
                    title="طباعة السجلات المحددة" OnClientClick="return ConfirmDelete();" >
                    <i class="fa fa-print"></i> طباعة السجلات المحددة</asp:LinkButton>
                <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                    title="طباعة" OnClientClick="return insertConfirmation();">
                    <i class="fa fa-print"></i></asp:LinkButton>
            </div>
            <div class="container-fluid">
                <h5>
                    <i class="fa fa-pencil"></i> فرز أوامر الدعم العيني - الأدوية والأجهزة - تأثيث المنازل
                </h5>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i> بيانات المستفيد - الدعم العيني - الأدوية والأجهزة - تأثيث المنازل 
                    </h3>
                    <div style="float: left">
                        <asp:DropDownList ID="DLType" runat="server" ValidationGroup="g2" CssClass="form-control2" AutoPostBack="true" Enabled="false"
                            Width="150px" Height="30px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">أمر صرف لمستفيد</asp:ListItem>
                            <%--<asp:ListItem Value="2">أمر صرف لموظف</asp:ListItem>--%>
                            <asp:ListItem Value="3">تالف</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblType" runat="server" Text="حدد الامر * " ForeColor="Red" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-md-3">
                        <div class="form-group">
                            <h5>حدد المشروع : </h5>
                            <asp:DropDownList ID="DLCategory" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                Width="100%" Height="30px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblCategory" runat="server" Text="حدد المشروع * " ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <h5>من تاريخ : 
                            </h5>
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
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <h5>إلى تاريخ : 
                            </h5>
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
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;" ValidationGroup="g2"
                                class="btn btn-info btn-fill" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-list"></i>قائمة فرز دعم المستفيد
                    </h3>
                    <div style="float: left">
                    </div>
                </div>
                <div class="panel-body">
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
                                                            <%# ClassMosTafeed.FGetMosTafeed((Int32) (Eval("_IDMosTafeed")))%> / رقم الفاتورة 
                                                            <asp:Label ID="lblIDBill" runat="server" Text='<%# Eval("_billNumber")%>'></asp:Label>
                                                            <asp:Label ID="lblIDProject" runat="server" Text='<%# DLCategory.SelectedValue %>'></asp:Label>
                                                            
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
                                                    <asp:TemplateField HeaderText="لمشروع" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# FGetProject() %>
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
                                                <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                                    PreviousPageText=" السابق - " PageButtonCount="30" />
                                                <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                                <RowStyle CssClass="rows"></RowStyle>
                                                <RowStyle CssClass="rows"></RowStyle>
                                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th>
                                            <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                            <hr style='border: solid; border-width: 1px; width: 100%' />
                                            <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                                            <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label> 
                                            / <span style="font-size: 12px; padding-right: 5px">المبلغ الاجمالي : </span>
                                            <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            <asp:Label ID="lblMony" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            <div align="Left" class="HideThis">
                                                <img src='/Img/IconTrue.png' style='width: 20px' alt="" />
                                                <span style="font-size: 11px">موافق</span>
                                                <img src='/Img/IconFalse.png' style='width: 20px' alt="" />
                                                <span style="font-size: 11px">غير موافق</span>
                                            </div>
                                        </th>
                                    </tr>
                                </tfoot>
                            </table>
                            <div class="hide">
                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                <div class="container-fluid" dir="rtl" runat="server">
                                    <uc1:WUCFooterWSM runat="server" ID="WUCFooterWSM" />
                                </div>
                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                            </div>
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

