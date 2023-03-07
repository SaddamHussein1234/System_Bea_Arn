<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductViceBoard.aspx.cs" Inherits="Cpanel_CPanelManageExchangeOrders_PageManageProductViceBoard" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterWSM.ascx" TagPrefix="uc1" TagName="WUCFooterWSM" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <link href="../css/chosen.css" rel="stylesheet" />
    <style type="text/css">
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
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVChairmanOfTheBoard.ClientID%>");
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
                return confirm(" هل أنت متأكد من الإستمرار ؟");
            }
        }
    </script>
    <script type="text/javascript">
        function ConfirmDeleteHose() {
            var count = document.getElementById("<%=hfCountHose.ClientID %>").value;
            var gv = document.getElementById("<%=GVApprovalOfTheDirectorHose.ClientID%>");
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
                return confirm(" هل أنت متأكد من الإستمرار ؟");
            }
        }
    </script>
    <script type="text/javascript">
        function ConfirmDeletePrisms() {
            var count = document.getElementById("<%=hfCountPrisms.ClientID %>").value;
            var gv = document.getElementById("<%=GVApprovalOfTheDirectorPrisms.ClientID%>");
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
                return confirm(" هل أنت متأكد من الإستمرار ؟");
            }
        }
    </script>
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageManageProductChairmanOfTheBoard.aspx" runat="server" id="IDAdd" data-toggle="tooltip" title="إضافة أمر صرف جديد" class="btn btn-primary" visible="false"><i class="fa fa-plus"></i></a>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageManageProductViceBoard.aspx">قائمة أوامر الصرف التي تحتاج إلى موافقة نائب رئيس المجلس</a></li>
                    </ul>
                </div>
            </div>
            <div align="rigth" style="padding: 0 15px 0 15px">
                <hr style="border: double; border-width: 1px; width: 100%" />
                <h5><i class="fa fa-star"></i>حدد نوع الصرف : </h5>
                <asp:RadioButton ID="RBTathith" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RBTathith_CheckedChanged" Visible="false" />
                <i class="fa fa-star"></i>
                <span>فرز أوامر الدعم العيني - الأدوية والأجهزة - تأثيث المنازل - التالف </span>
                <span style="color: #9c1800">( الموجود حالياً
                    <asp:Label ID="lblCountCard" runat="server" Text="0"></asp:Label>
                    )</span>
                <nav class="navbar-dark bg-dark">
                    <a class="btn navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarToggleExternalContent" aria-controls="navbarToggleExternalContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="fa fa-list"></span> عرض الفلترة
                    </a>
                </nav>
                <div class="collapse" id="navbarToggleExternalContent" style="background-color:#ecedec">
                    <div class="bg-dark p-4" style="padding:5px 5px 10px 0">
                        <h5 class="text-white h4"><i class="fa fa-star"></i>حدد الملفات المراد عرضها : </h5>
                        <span class="text-muted">
                            <asp:RadioButton ID="RBCardCheck" runat="server" GroupName="RBCard" AutoPostBack="true" OnCheckedChanged="RBCardCheck_CheckedChanged" Enabled="false" />
                            <span>عرض ملفات الدعم العيني </span>
                            <i class="fa fa-minus"></i>
                            <asp:RadioButton ID="RBDeviceCheck" runat="server" GroupName="RBCard" AutoPostBack="true" OnCheckedChanged="RBDeviceCheck_CheckedChanged" Enabled="false" />
                            <span>عرض ملفات الأدوية والأجهزة </span>
                            <i class="fa fa-minus"></i>
                            <asp:RadioButton ID="RBTathithCheck" runat="server" GroupName="RBCard" AutoPostBack="true" OnCheckedChanged="RBTathithCheck_CheckedChanged" Enabled="false" />
                            <span>عرض ملفات تأثيث منازل </span>
                            <i class="fa fa-minus"></i>
                            <asp:RadioButton ID="RBTalefCheck" runat="server" GroupName="RBCard" AutoPostBack="true" OnCheckedChanged="RBTalefCheck_CheckedChanged" Checked="true" />
                            <span>عرض ملفات التالف </span>
                        </span>
                    </div>
                </div>
                <br />
                <div runat="server" visible="false">
                <asp:RadioButton ID="RPTarmem" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RPTarmem_CheckedChanged" Visible="" />
                <span>فرز أوامر بناء المنازل - ترميم المنازل </span>
                <span style="color: #9c1800">( الموجود حالياً
                    <asp:Label ID="lblCountHoseHear" runat="server" Text="0"></asp:Label>
                    )</span>
                <br />
                </div>
                <div runat="server" visible="false">
                <asp:RadioButton ID="RPSupportForPrisms" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RPSupportForPrisms_CheckedChanged"  />
                <span>فرز أوامر صرف الدعم المالي </span>
                <span style="color: #9c1800">( الموجود حالياً
                    <asp:Label ID="lblCountPrismsHear" runat="server" Text="0"></asp:Label>
                    )</span>
                </div>
                <hr style="border: double; border-width: 1px; width: 100%" />
            </div>
            <div class="container-fluid" id="IDCard" runat="server" visible="false">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة أوامر الصرف التي تحتاج إلى موافقة نائب رئيس المجلس
                        </h3>
                        <div style="float: left">
                            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                                title="تحديث" OnClick="btnRefrish_Click">
                            <i class="fa fa-refresh"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                                title="طباعة" OnClientClick="return insertConfirmation();">
                            <i class="fa fa-print"></i></asp:LinkButton>
                            <asp:Button ID="btnAllow" class="btn btn-info" runat="server" Text="الموافقة على السجلات المحددة" OnClick="btnAllow_Click"
                                title="الموافقة على السجلات المحددة" data-toggle="tooltip" OnClientClick="return ConfirmDelete();" />
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
                                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="قائمة أوامر الصرف التي تحتاج إلى موافقة نائب رئيس المجلس" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVChairmanOfTheBoard" runat="server" AutoGenerateColumns="False" DataKeyNames="_billNumber"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
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
                                                        <asp:BoundField DataField="_billNumber" HeaderText="_billNumber" InsertVisible="False" ReadOnly="True"
                                                            SortExpression="_billNumber" Visible="false" />
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ر/الأمر" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("_billNumber")%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassMosTafeed.FGetMosTafeed((Int32) (Eval("_IDMosTafeed")))%> / <%# ClassSaddam.FCheckTaleef((Int32) (Eval("_IDMosTafeed")))%>
                                                                <br />
                                                                <span style="font-size: 11px" class="HideThis">
                                                                    <%# ClassSaddam.FCheckAllowNaeb2((bool) (Eval("_IsNaebRaees")))%> 
                                                                  , <%# ClassSaddam.FRaeesMaglis2((bool) (Eval("_IsRaeesMaglisAlEdarah")))%>
                                                                  , <%# ClassSaddam.FAmeenAlmostodaa2((bool) (Eval("_IsStorekeeper")))%>
                                                                </span>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="القرية" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassQuaem.FAlQarabah(ClassQuaem.FGetIDQriah((Int32) Eval("_IDMosTafeed")))%>
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
                                                                <%# ClassDataAccess.FChangeF((DateTime) (Eval("_DateCaming")))%>
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
                                                                <a href='PageManageProductAddThePriceToOrder.aspx?ID=<%# Eval("_billNumber")%>&XID=<%# Eval("_IDMosTafeed")%>&IsCart=<%# RBCardCheck.Checked %>&IsDevice=<%# RBDeviceCheck.Checked %>&IsTathith=<%# RBTathithCheck.Checked %>&IsTalef=<%# RBTalefCheck.Checked %>' title="عرض التفاصيل" data-toggle="tooltip"
                                                                    class="btn btn-info"><span class="fa fa-edit"></span></a>
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
                                                <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                <div align="Left" class="HideThis">
                                                    <img src='/Img/IconTrue.png' style='width: 20px' />
                                                    <span style="font-size: 11px">موافق</span>
                                                    <img src='/Img/IconFalse.png' style='width: 20px' />
                                                    <span style="font-size: 11px">غير موافق</span>
                                                </div>
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>
                                <div>
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
                    </div>
                </div>
            </div>
            <div class="container-fluid" id="IDHose" runat="server" visible="false">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة أوامر الصرف التي تحتاج إلى موافقة نائب رئيس المجلس
                        </h3>
                        <div style="float: left">
                            <asp:LinkButton ID="LBRefrshHose" runat="server" class="btn btn-default" data-toggle="tooltip"
                                title="تحديث" OnClick="LBRefrshHose_Click">
                            <i class="fa fa-refresh"></i></asp:LinkButton>
                            <asp:LinkButton ID="LBPrintHose" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="LBPrintHose_Click"
                                title="طباعة" OnClientClick="return insertConfirmation();">
                            <i class="fa fa-print"></i></asp:LinkButton>
                            <asp:Button ID="btnAllowHose" class="btn btn-info" runat="server" Text="الموافقة على السجلات المحددة" OnClick="btnAllowHose_Click"
                                title="الموافقة على السجلات المحددة" data-toggle="tooltip" OnClientClick="return ConfirmDeleteHose();" />
                        </div>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="pnlDataHose" runat="server" Direction="RightToLeft" Visible="False">
                            <div class="table table-responsive">
                                <table class='table' style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <div class="HideNow">
                                                    <uc1:WUCHeader runat="server" ID="WUCHeader1" />
                                                </div>
                                                <div align="center" class="w">
                                                    <div>
                                                        <asp:TextBox ID="txtTitleHose" runat="server" class="form-control" Text="قائمة أوامر الصرف التي تحتاج إلى موافقة نائب رئيس المجلس" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVApprovalOfTheDirectorHose" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
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
                                                        <asp:BoundField DataField="billNumber_" HeaderText="_billNumber" InsertVisible="False" ReadOnly="True"
                                                            SortExpression="billNumber_" Visible="false" />
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ر/الأمر" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("billNumber_")%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassMosTafeed.FGetMosTafeed((Int32) (Eval("NumberMostafeed")))%> /
                                                                <br />
                                                                <span style="font-size: 11px" class="HideThis">
                                                                    <%# ClassSaddam.FCheckAllowModer2((bool) (Eval("IsAllowModer")))%> 
                                                                     , <%# ClassSaddam.FAmeenAlsondoq2(Convert.ToBoolean(Eval("AllowState")), Convert.ToBoolean(Eval("NotAllowState")))%>
                                                                     , <%# ClassSaddam.FRaeesMaglis2((bool) (Eval("IsAllowRaeesAlMagles")))%>
                                                                </span>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="القرية" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassQuaem.FAlQarabah(ClassQuaem.FGetIDQriah((Int32) Eval("NumberMostafeed")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="لمشروع" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassQuaem.FSupportType((Int64) (Eval("ID_Type")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="التاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassDataAccess.FChangeF((DateTime) (Eval("Date_Add_Report")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="مدخل البيانات" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="font-size: 11px">
                                                                    <%# ClassQuaem.FAlBaheth((Int32) Eval("IDAdmin"))%> 
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
                                                        <asp:TemplateField HeaderText="المبلغ" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" Text='<%# Eval("The_Mony")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                                            <ItemTemplate>
                                                                <a href='PageManageProductAddThePriceToOrder.aspx?IDX=<%# Eval("billNumber_")%>&XID=<%# Eval("NumberMostafeed")%>&IsBena=<%# Eval("IsBena")%>&IsTarmem=<%# Eval("IsTarmem")%>' title="عرض التفاصيل" data-toggle="tooltip"
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
                                                <asp:HiddenField ID="hfCountHose" runat="server" Value="0" />
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                                <asp:Label ID="lblCountHose" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                <div align="Left" class="HideThis">
                                                    <img src='/Img/IconTrue.png' style='width: 20px' />
                                                    <span style="font-size: 11px">موافق</span>
                                                    <img src='/Img/IconFalse.png' style='width: 20px' />
                                                    <span style="font-size: 11px">غير موافق</span>
                                                </div>
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>
                                <div>
                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                    <div class="container-fluid" dir="rtl" runat="server">
                                        <uc1:WUCFooterWSM runat="server" ID="WUCFooterWSM1" />
                                    </div>
                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                    <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom1" />
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlNullHose" runat="server" Visible="False">
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
                    </div>
                </div>
            </div>
            <div class="container-fluid" id="IDPrisms" runat="server" visible="false">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة أوامر الصرف التي تحتاج إلى موافقة نائب رئيس المجلس
                        </h3>
                        <div style="float: left">
                            <asp:Button ID="btnPrisms" class="btn btn-info" runat="server" Text="الموافقة على السجلات المحددة" OnClick="btnPrisms_Click"
                                title="الموافقة على السجلات المحددة" data-toggle="tooltip" OnClientClick="return ConfirmDeletePrisms();" />
                            <asp:LinkButton ID="btnRefrshPrisms" runat="server" class="btn btn-default" data-toggle="tooltip"
                                title="تحديث" OnClick="btnRefrshPrisms_Click">
                            <i class="fa fa-refresh"></i></asp:LinkButton>
                            <asp:LinkButton ID="btnPrintPrisms" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrintPrisms_Click"
                                title="طباعة" OnClientClick="return insertConfirmation();">
                            <i class="fa fa-print"></i></asp:LinkButton>
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
                                                    <uc1:WUCHeader runat="server" ID="WUCHeader2" />
                                                </div>
                                                <div align="center" class="w">
                                                    <div>
                                                        <asp:TextBox ID="txtTitlePrisms" runat="server" class="form-control" Text="قائمة أوامر الصرف التي تحتاج إلى موافقة نائب رئيس المجلس" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVApprovalOfTheDirectorPrisms" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
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
                                                        <asp:BoundField DataField="billNumber_" HeaderText="_billNumber" InsertVisible="False" ReadOnly="True"
                                                            SortExpression="billNumber_" Visible="false" />
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ر/الأمر" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("billNumber_")%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassMosTafeed.FGetMosTafeed((Int32) (Eval("NumberMostafeed")))%> /
                                                                <br />
                                                                <span style="font-size: 11px" class="HideThis">
                                                                    <%# ClassSaddam.FCheckAllowModer2((bool) (Eval("IsAllowModer")))%> 
                                                                     , <%# ClassSaddam.FAmeenAlsondoq2((bool) (Eval("AllowState")))%>
                                                                     , <%# ClassSaddam.FRaeesMaglis2((bool) (Eval("IsAllowRaeesAlMagles")))%>
                                                                </span>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="القرية" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassQuaem.FAlQarabah(ClassQuaem.FGetIDQriah((Int32) Eval("NumberMostafeed")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="لمشروع" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassQuaem.FSupportType((Int64) (Eval("ID_Type")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="التاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassDataAccess.FChangeF((DateTime) (Eval("Date_Add_Report")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="مدخل البيانات" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="font-size: 11px">
                                                                    <%# ClassQuaem.FAlBaheth((Int32) Eval("IDAdmin"))%> 
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
                                                        <asp:TemplateField HeaderText="المبلغ" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" Text='<%# Eval("The_Mony")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                                            <ItemTemplate>
                                                                <a href='PageManageProductAddThePriceToOrder.aspx?IDS=<%# Eval("billNumber_")%>&IDCh=<%# Eval("ID_Type")%>&IDU=<%# Convert.ToString(Guid.NewGuid()) %>' title="عرض التفاصيل" data-toggle="tooltip"
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
                                                <asp:HiddenField ID="hfCountPrisms" runat="server" Value="0" />
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                                <asp:Label ID="lblCountPrisms" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                <div align="Left" class="HideThis">
                                                    <img src='/Img/IconTrue.png' style='width: 20px' />
                                                    <span style="font-size: 11px">موافق</span>
                                                    <img src='/Img/IconFalse.png' style='width: 20px' />
                                                    <span style="font-size: 11px">غير موافق</span>
                                                </div>
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>
                                <div>
                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                    <div class="container-fluid" dir="rtl" runat="server">
                                        <uc1:WUCFooterWSM runat="server" ID="WUCFooterWSM2" />
                                    </div>
                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                    <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom2" />
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
                    </div>
                </div>
            </div>
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
                    <h3 style="font-size: 20px">يرجى تحديد نوع الفرز
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
        <br />
        <br />
        <br />
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

