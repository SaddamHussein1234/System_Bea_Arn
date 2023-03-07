<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/EOS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageSupportByBeneficiary.aspx.cs" Inherits="Cpanel_ERP_EOS_In_Kind_Donation_PageSupportByBeneficiary" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterWSM.ascx" TagPrefix="uc1" TagName="WUCFooterWSM" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<%@ Import Namespace="Library_CLS_Arn.WSM.Repostry" %>

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

    <script type="text/javascript">
        function ConfirmDeleteTarmem() {
            var count = document.getElementById("<%=hfCountTarmem.ClientID %>").value;
        var gv = document.getElementById("<%=GVExchangeOrdersTarmem.ClientID%>");
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

    <script type="text/javascript">
        function ConfirmDeletePrisms() {
            var count = document.getElementById("<%=hfCountPrisms.ClientID %>").value;
        var gv = document.getElementById("<%=GVExchangeOrdersPrisms.ClientID%>");
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

    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
    <div class="page-header" runat="server" id="IDStar">
        <div class="container-fluid">
            <div class="pull-right">
                <label class="control-label">
                    الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                </label>
                <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2" AutoPostBack="true" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged"
                    Width="100" ValidationGroup="GDetails">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
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
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-list"></i>قائمة فرز دعم المستفيد
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="panel-body">
                        <span><i class="fa fa-star"></i> حدد نوع أوامر الصرف </span>
                        <br />
                        <asp:RadioButton ID="RBTathith" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RBTathith_CheckedChanged" />
                        <span>فرز أوامر الدعم العيني - الأدوية والأجهزة - تأثيث المنازل </span>
                        <br />
                        <asp:RadioButton ID="RPTarmem" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RPTarmem_CheckedChanged" />
                        <span>فرز أوامر بناء المنازل - ترميم المنازل </span>
                        <br />
                        <asp:RadioButton ID="RPSupportForPrisms" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RPSupportForPrisms_CheckedChanged" />
                        <span>فرز أوامر صرف الدعم المالي </span>
                        <hr style='border: solid; border-width: 1px; width: 100%' />
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
                            <div align="center">
                                <h3 style="font-size: 20px">يرجى تحديد أمر الصرف
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
                            <hr style='border: solid; border-width: 1px; width: 100%' />
                        </asp:Panel>
                    </div>
                </div>
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
    <div class="page-header" runat="server" id="IDTathith" visible="false">
        <div class="container-fluid">
            <div class="pull-right">
                <a href="PageManageProductMatterOfExchange.aspx" data-toggle="tooltip" title="إضافة أمر صرف جديد" class="btn btn-primary"><i class="fa fa-plus"></i></a>
                <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                    title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                    title="طباعة" OnClientClick="return insertConfirmation();">
                    <i class="fa fa-print"></i></asp:LinkButton>
                <asp:LinkButton ID="btnDelete" runat="server" class="btn btn-danger" OnClick="btnDelete_Click"
                        OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                        <i class="fa fa-trash-o"></i></span></asp:LinkButton>
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
                    <div class="WidthText1">
                        <div class="form-group">
                            <h5>المستفيد : </h5>
                            <asp:DropDownList ID="DLMostafeed" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                Width="100%" Height="30px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblMostafeed" runat="server" Text="حدد المستفيد * " ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="WidthText1">
                        <div class="form-group">
                            <h5>لمشروع : </h5>
                            <asp:DropDownList ID="DLCategory" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                Width="100%" Height="30px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblCategory" runat="server" Text="حدد المشروع * " ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="WidthText3">
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
                    <div class="WidthText3">
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
                    <div class="WidthText">
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
                                            <asp:GridView ID="GVExchangeOrders" runat="server" AutoGenerateColumns="False" DataKeyNames="_ID_Item_"
                                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVExchangeOrders_RowDataBound"
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
                                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Font-Size="12px" Text='<%# Eval("_ID_Item_") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblIDYear" runat="server" Font-Size="12px" Text='<%# Eval("_ID_FinancialYear_") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblIDBill" runat="server" Font-Size="12px" Text='<%# Eval("_bill_Number_") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblIDProject" runat="server" Font-Size="12px" Text='<%# Eval("_ID_Project_") %>' Visible="false"></asp:Label>
                                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="_ID_MosTafeed_" HeaderText="رقم الملف" InsertVisible="False" ReadOnly="True"
                                                         SortExpression="_ID_MosTafeed_" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:BoundField DataField="_bill_Number_" HeaderText="رقم الفاتورة" InsertVisible="False" ReadOnly="True"
                                                         SortExpression="_bill_Number_" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:TemplateField HeaderText="حالة المراجعة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <div style="font-size: 11px" >
                                                                <%# ClassSaddam.FCheckAllowModer4((bool) (Eval("_Is_Moder_")))%> 
                                                                    , <%# ClassSaddam.FAmeenAlmostodaa4((bool) (Eval("_Is_Storekeeper_")))%>
                                                            </div>
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
                                                    <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderText="أُضيف من قبل">
                                                        <ItemTemplate>
                                                            <%# ClassQuaem.FAlBaheth((Int32) (Eval("_CreatedBy_")))%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="بتاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate_Add" runat="server" 
                                                                Text='<%# Eval("_CreatedDate_", "{0:dd/MM/yyyy}") + " " + Eval("_CreatedDate_", "{0:HH:mm tt}")  %>' Font-Size="11px"></asp:Label>
                                                            <div style="font-size: 11px" class="HideThis">
                                                                <span style="font-size: 11px">الباحث/</span><%# ClassQuaem.FAlBaheth((Int32) Eval("_ID_Delivery_"))%>
                                                                    , <%# ClassSaddam.FAlbaheth4((Convert.ToBoolean(Eval("_Is_Received_"))),(Convert.ToBoolean(Eval("_Is_Not_Received_"))))%>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="العدد" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblCount" runat="server" Font-Size="12px" Text='<%# ClassProductShopWarehouse.FCount((Int32) (Eval("_bill_Number_")))%>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px"
                                                                Text='<%# WSM_Repostry_Exchange_Order_Details_.FGetBySumBill(new Guid(Eval("_ID_Item_").ToString()))%>'></asp:Label>
                                                            <%# ClassSaddam.FGetMonySa() %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                                        <ItemTemplate>
                                                            <a href='PageMatterOfExchange.aspx?ID=<%# Eval("_ID_Item_") %>' title='تعديل البيانات' data-toggle='tooltip'><span class='fa fa-edit'></span></a>
                                                            <br />
                                                            <a href='PageView.aspx?IDUniq=<%# ddlYears.SelectedValue %>&ID=<%# Eval("_bill_Number_")%>&XID=<%# Eval("_ID_MosTafeed_")%>&XIDCate=<%# Eval("_ID_Project_")%>&IsCart=<%# Eval("_Is_Cart_") %>&IsDevice=<%# Eval("_Is_Device_") %>&IsTathith=<%# Eval("_Is_Tathith_") %>&IsTalef=<%# Eval("_Is_Talef_") %>' 
                                                                title="عرض التفاصيل" data-toggle="tooltip"><span class="fa fa-eye"></span></a>
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
    <div class="page-header" runat="server" id="IDTarmem" visible="false">
        <div class="container-fluid">
            <div class="pull-right">
                <a href="../Cash_Donation/PageRestorationAndConstruction.aspx" data-toggle="tooltip" title="إضافة أمر صرف جديد" class="btn btn-primary"><i class="fa fa-plus"></i></a>
                <asp:LinkButton ID="LBRefreshTarmem" runat="server" class="btn btn-default" data-toggle="tooltip"
                    title="تحديث" OnClick="LBRefreshTarmem_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                <asp:LinkButton ID="LBPrintTarmem" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="LBPrintTarmem_Click"
                    title="طباعة" OnClientClick="return insertConfirmation();">
                    <i class="fa fa-print"></i></asp:LinkButton>
                <asp:LinkButton ID="btnDeleteTarmem" runat="server" class="btn btn-danger" OnClick="btnDeleteTarmem_Click"
                    OnClientClick="return ConfirmDeleteTarmem();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
            </div>
            <div class="container-fluid">
                <h5>
                    <i class="fa fa-pencil"></i> فرز أوامر بناء المنازل - ترميم المنازل
                </h5>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>بيانات المستفيد - فرز أوامر بناء المنازل - ترميم المنازل
                    </h3>
                    <div style="float: left">
                        <asp:DropDownList ID="DLTypeTarmem" runat="server" ValidationGroup="g2" CssClass="form-control2" AutoPostBack="true" Enabled="false"
                            Width="150px" Height="30px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">أمر صرف لمستفيد</asp:ListItem>
                            <%--<asp:ListItem Value="2">أمر صرف لموظف</asp:ListItem>--%>
                            <asp:ListItem Value="3">تالف</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblTypeTarmem" runat="server" Text="حدد الامر * " ForeColor="Red" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="WidthText1">
                        <div class="form-group">
                            <h5>المستفيد : </h5>
                            <asp:DropDownList ID="DLMostafeedTarmem" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                Width="100%" Height="30px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblMostafeedTarmem" runat="server" Text="حدد المستفيد * " ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="WidthText1">
                        <div class="form-group">
                            <h5>لمشروع : </h5>
                            <asp:DropDownList ID="DLCategoryTarmem" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                Width="100%" Height="30px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblCategoryTarmem" runat="server" Text="حدد المشروع * " ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="WidthText3">
                        <div class="form-group">
                            <h5>من تاريخ : 
                            </h5>
                            <div class="col-sm-3">
                                <div class="input-group date " style="margin-right: -10px;">
                                    <asp:TextBox ID="txtDateFromTarmem" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; width: 100px"></asp:TextBox>
                                    <asp:Label ID="lblDateFromTarmem" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="WidthText3">
                        <div class="form-group">
                            <h5>إلى تاريخ : 
                            </h5>
                            <div class="col-sm-3">
                                <div class="input-group date " style="margin-right: -10px;">
                                    <asp:TextBox ID="txtDateToTarmem" runat="server" placeholder="إلى تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; width: 100px"></asp:TextBox>
                                    <asp:Label ID="lblDateToTarmem" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="WidthText">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="btnSearchTarmem" runat="server" Text="بحث" Style="margin-right: 4px;" ValidationGroup="g2"
                                class="btn btn-info btn-fill" OnClick="btnSearchTarmem_Click" />
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
                    <asp:Panel ID="pnlDataTarmem" runat="server" Direction="RightToLeft" Visible="False">
                        <div class="table table-responsive">
                            <table class='table' style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>
                                            <div class="HideNow">
                                                <uc1:wucheader runat="server" id="WUCHeader1" />
                                            </div>
                                            <div align="center" class="w">
                                                <div>
                                                    <asp:TextBox ID="txtTitleTarmem" runat="server" class="form-control" Text="قائمة فرز أوامر الصرف" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                </div>
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <div class="container-fluid" style="text-align: right; font-size: 12px">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td class="StyleTD">الاسم :
                                                            </td>
                                                            <td class="StyleTD" colspan="2">
                                                                <asp:Label ID="lblNameTarmem" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">رقم الملف :
                                                            </td>
                                                            <td class="StyleTD">
                                                                <asp:Label ID="lblNumberFileTarmem" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">القرية :
                                                            </td>
                                                            <td class="StyleTD">
                                                                <asp:Label ID="lblAlQariahTarmem" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">الجنس :
                                                            </td>
                                                            <td class="StyleTD">
                                                                <asp:Label ID="lblGenderTarmem" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="StyleTD">رقم الهاتف :
                                                            </td>
                                                            <td class="StyleTD" colspan="2">0<asp:Label ID="lblPhoneTarmem" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">حالة المستفيد :
                                                            </td>
                                                            <td class="StyleTD" colspan="2">
                                                                <asp:Label ID="lblHalatAlmostafeedTarmem" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">السجل المدني :
                                                            </td>
                                                            <td class="StyleTD" colspan="2">
                                                                <asp:Label ID="lblNumberSigalTarmem" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="StyleTD">تاريخ الميلاد :
                                                            </td>
                                                            <td class="StyleTD" colspan="3">
                                                                <asp:Label ID="lblDateBrithDayTarmem" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">العمر :
                                                            </td>
                                                            <td class="StyleTD" colspan="4">
                                                                <asp:Label ID="lblAgeTarmem" runat="server" Font-Size="12px"></asp:Label>
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
                                            <asp:GridView ID="GVExchangeOrdersTarmem" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
                                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVExchangeOrdersTarmem_RowDataBound"
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
                                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDTarmem" runat="server" Font-Size="12px" Text='<%# Eval("IDItem") %>' Visible="false"></asp:Label>
                                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NumberMostafeed" HeaderText="رقم الملف" InsertVisible="False" ReadOnly="True"
                                                         SortExpression="NumberMostafeed" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:BoundField DataField="billNumber_" HeaderText="رقم الفاتورة" InsertVisible="False" ReadOnly="True"
                                                         SortExpression="billNumber_" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:TemplateField HeaderText="حالة المراجعة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <div style="font-size: 11px">
                                                                <%# ClassSaddam.FCheckAllowModer4((bool) (Eval("IsAllowModer")))%> 
                                                                    , <%# ClassSaddam.FAmeenAlsondoq4((bool) (Eval("AllowState")))%>
                                                                    , <%# ClassSaddam.FRaeesMaglis4((bool) (Eval("IsAllowRaeesAlMagles")))%>
                                                            </div>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="لمشروع" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# FGetProjectTarmem() %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="التاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# ClassDataAccess.FChangeF((DateTime) (Eval("_CreatedDate_")))%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="العدد" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblCount" runat="server" Font-Size="12px" Text='<%# ClassProductShopWarehouse.FCount((Int32) (Eval("_billNumber")))%>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="مدخل البيانات" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px">
                                                                <%# ClassQuaem.FAlBaheth((Int32) Eval("_CreatedBy_"))%> 
                                                            </span>
                                                            <div style="font-size: 11px" class="HideThis">
                                                                <span style="font-size: 11px">الباحث/</span><%# ClassQuaem.FAlBaheth((Int32) Eval("IDAlBaheth"))%>
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
                                                            <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" Text='<%# Eval("The_Mony")%>'></asp:Label>
                                                            <small><%# ClassSaddam.FGetMonySa() %></small>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                                        <ItemTemplate>
                                                            <a href='PageView.aspx?IDUniq=<%# ddlYears.SelectedValue %>&IDX=<%# Eval("billNumber_")%>&XID=<%# Eval("NumberMostafeed")%>&IsBena=<%# Eval("IsBena")%>&IsTarmem=<%# Eval("IsTarmem")%>' 
                                                                title="عرض التفاصيل" data-toggle="tooltip"><span class="fa fa-eye"></span></a>
                                                            <br />
                                                            <a href='../Cash_Donation/PageRestorationAndConstruction.aspx?ID=<%# Eval("IDUniq") %>' title='تعديل البيانات' data-toggle='tooltip'><span class='fa fa-edit'></span></a>
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
                                            <asp:HiddenField ID="hfCountTarmem" runat="server" Value="0" />
                                            <hr style='border: solid; border-width: 1px; width: 100%' />
                                            <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                                            <asp:Label ID="lblCountTarmem" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            / <span style="font-size: 12px; padding-right: 5px">المبلغ الاجمالي : </span>
                                            <asp:Label ID="lblTotalPriceTarmem" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            <asp:Label ID="lblMonyTarmim" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
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
                                    <uc1:WUCFooterWSM runat="server" ID="WUCFooterWSM1" />
                                </div>
                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                <uc1:wucfooterbottom runat="server" id="WUCFooterBottom1" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlNullTarmem" runat="server" Visible="False">
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
                    <asp:Panel ID="pnlSelectTarmem" runat="server" Visible="False">
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
    <div class="page-header" runat="server" id="IDPrisms" visible="false">
        <div class="container-fluid">
            <div class="pull-right">
                <a href="../Cash_Donation/PageSupportForPrisms.aspx" data-toggle="tooltip" title="إضافة أمر صرف جديد" class="btn btn-primary"><i class="fa fa-plus"></i></a>
                <asp:LinkButton ID="LBRefreshPrisms" runat="server" class="btn btn-default" data-toggle="tooltip"
                    title="تحديث" OnClick="LBRefreshPrisms_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                <asp:LinkButton ID="LBPrintPrisms" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="LBPrintPrisms_Click"
                    title="طباعة" OnClientClick="return insertConfirmation();">
                    <i class="fa fa-print"></i></asp:LinkButton>
                <asp:LinkButton ID="btnDeletePrisms" runat="server" class="btn btn-danger" OnClick="btnDeletePrisms_Click"
                    OnClientClick="return ConfirmDeletePrisms();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
            </div>
            <div class="container-fluid">
                <h5>
                    <i class="fa fa-pencil"></i> فرز أوامر صرف الدعم المالي
                </h5>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>بيانات المستفيد - فرز أوامر صرف الدعم المالي
                    </h3>
                    <div style="float: left">
                        <asp:DropDownList ID="DLTypePrisms" runat="server" ValidationGroup="g2" CssClass="form-control2" AutoPostBack="true" Enabled="false"
                            Width="150px" Height="30px">
                            <asp:ListItem></asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">أمر صرف لمستفيد</asp:ListItem>
                            <%--<asp:ListItem Value="2">أمر صرف لموظف</asp:ListItem>--%>
                            <asp:ListItem Value="3">تالف</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lblTypePrisms" runat="server" Text="حدد الامر * " ForeColor="Red" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="WidthText1">
                        <div class="form-group">
                            <h5>المستفيد : </h5>
                            <asp:DropDownList ID="DLMostafeedPrisms" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                Width="100%" Height="30px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblMostafeedPrisms" runat="server" Text="حدد المستفيد * " ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="WidthText1">
                        <div class="form-group">
                            <h5>لمشروع : </h5>
                            <asp:DropDownList ID="DLCategoryPrisms" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                Width="100%" Height="30px">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="lblCategoryPrisms" runat="server" Text="حدد المشروع * " ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="WidthText3">
                        <div class="form-group">
                            <h5>من تاريخ : 
                            </h5>
                            <div class="col-sm-3">
                                <div class="input-group date " style="margin-right: -10px;">
                                    <asp:TextBox ID="txtDateFromPrisms" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; width: 100px"></asp:TextBox>
                                    <asp:Label ID="lblDateFromPrisms" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="WidthText3">
                        <div class="form-group">
                            <h5>إلى تاريخ : 
                            </h5>
                            <div class="col-sm-3">
                                <div class="input-group date " style="margin-right: -10px;">
                                    <asp:TextBox ID="txtDateToPrisms" runat="server" placeholder="إلى تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; width: 100px"></asp:TextBox>
                                    <asp:Label ID="lblDateToPrisms" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="WidthText">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="btnSearchPrisms" runat="server" Text="بحث" Style="margin-right: 4px;" ValidationGroup="g2"
                                class="btn btn-info btn-fill" OnClick="btnSearchPrisms_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-list"></i>قائمة فرز أوامر صرف الدعم المالي
                    </h3>
                    <div style="float: left">
                    </div>
                </div>
                <div class="panel-body">
                    <asp:Panel ID="pnlDataPrisms" runat="server" Direction="RightToLeft" Visible="False">
                        <div class="table table-responsive">
                            <table class='table' style="width: 100%">
                                <thead>
                                    <tr>
                                        <th>
                                            <div class="HideNow">
                                                <uc1:wucheader runat="server" id="WUCHeader2" />
                                            </div>
                                            <div align="center" class="w">
                                                <div>
                                                    <asp:TextBox ID="txtTitlePrisms" runat="server" class="form-control" Text="قائمة فرز أوامر الصرف" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                </div>
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <div class="container-fluid" style="text-align: right; font-size: 12px">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td class="StyleTD">الاسم :
                                                            </td>
                                                            <td class="StyleTD" colspan="2">
                                                                <asp:Label ID="lblNamePrisms" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">رقم الملف :
                                                            </td>
                                                            <td class="StyleTD">
                                                                <asp:Label ID="lblNumberFilePrisms" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">القرية :
                                                            </td>
                                                            <td class="StyleTD">
                                                                <asp:Label ID="lblAlQariahPrisms" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">الجنس :
                                                            </td>
                                                            <td class="StyleTD">
                                                                <asp:Label ID="lblGenderPrisms" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="StyleTD">رقم الهاتف :
                                                            </td>
                                                            <td class="StyleTD" colspan="2">0<asp:Label ID="lblPhonePrisms" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">حالة المستفيد :
                                                            </td>
                                                            <td class="StyleTD" colspan="2">
                                                                <asp:Label ID="lblHalatAlmostafeedPrisms" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">السجل المدني :
                                                            </td>
                                                            <td class="StyleTD" colspan="2">
                                                                <asp:Label ID="lblNumberSigalPrisms" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="StyleTD">تاريخ الميلاد :
                                                            </td>
                                                            <td class="StyleTD" colspan="3">
                                                                <asp:Label ID="lblDateBrithDayPrisms" runat="server" Font-Size="12px"></asp:Label>
                                                            </td>
                                                            <td class="StyleTD">العمر :
                                                            </td>
                                                            <td class="StyleTD" colspan="4">
                                                                <asp:Label ID="lblAgePrisms" runat="server" Font-Size="12px"></asp:Label>
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
                                            <asp:GridView ID="GVExchangeOrdersPrisms" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
                                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVExchangeOrdersPrisms_RowDataBound"
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
                                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDPrisms" runat="server" Font-Size="12px" Text='<%# Eval("IDItem") %>' Visible="false"></asp:Label>
                                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="NumberMostafeed" HeaderText="رقم الملف" InsertVisible="False" ReadOnly="True"
                                                         SortExpression="NumberMostafeed" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:BoundField DataField="billNumber_" HeaderText="رقم الفاتورة" InsertVisible="False" ReadOnly="True"
                                                         SortExpression="billNumber_" HeaderStyle-ForeColor="#CCCCCC" />
                                                    <asp:TemplateField HeaderText="حالة المراجعة" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <div style="font-size: 11px">
                                                                <%# ClassSaddam.FCheckAllowModer4((bool) (Eval("IsAllowModer")))%> 
                                                                    , <%# ClassSaddam.FAmeenAlsondoq4((bool) (Eval("AllowState")))%>
                                                                    , <%# ClassSaddam.FRaeesMaglis4((bool) (Eval("IsAllowRaeesAlMagles")))%>
                                                            </div>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="لمشروع" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# FGetProjectPrisms() %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="التاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# ClassDataAccess.FChangeF((DateTime) (Eval("_CreatedDate_")))%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="العدد" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblCount" runat="server" Font-Size="12px" Text='<%# ClassProductShopWarehouse.FCount((Int32) (Eval("_billNumber")))%>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="مدخل البيانات" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <span style="font-size: 11px">
                                                                <%# ClassQuaem.FAlBaheth((Int32) Eval("_CreatedBy_"))%> 
                                                            </span>
                                                            <div style="font-size: 11px" class="HideThis">
                                                                <span style="font-size: 11px">الباحث/</span><%# ClassQuaem.FAlBaheth((Int32) Eval("IDAlBaheth"))%>
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
                                                            <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" Text='<%# Eval("The_Mony")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                                        <ItemTemplate>
                                                            <a href='PageView.aspx?IDUniq=<%# ddlYears.SelectedValue %>&IDS=<%# Eval("billNumber_")%>&IDCh=<%# Eval("ID_Project_")%>&IDU=<%# Eval("IDUniq")%>' title="عرض التفاصيل" data-toggle="tooltip"><span class="fa fa-eye"></span></a>
                                                            <br />
                                                            <a href='../Cash_Donation/PageSupportForPrisms.aspx?ID=<%# Eval("IDUniq")%>' title='تعديل البيانات' data-toggle='tooltip'><span class='fa fa-edit'></span></a>
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
                                            <asp:HiddenField ID="hfCountPrisms" runat="server" Value="0" />
                                            <hr style='border: solid; border-width: 1px; width: 100%' />
                                            <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                                            <asp:Label ID="lblCountPrisms" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            / <span style="font-size: 12px; padding-right: 5px">المبلغ الاجمالي : </span>
                                            <asp:Label ID="lblTotalPricePrisms" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            <asp:Label ID="lblMonyPrisms" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
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
                                    <uc1:WUCFooterWSM runat="server" ID="WUCFooterWSM2" />
                                </div>
                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                <uc1:wucfooterbottom runat="server" id="WUCFooterBottom2" />
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="pnlNullPrisms" runat="server" Visible="False">
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
                    <asp:Panel ID="pnlSelectPrisms" runat="server" Visible="False">
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

