<%@ Page Title="" Language="C#" MasterPageFile="~/CResearchers/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageProductFileSearchers.aspx.cs" Inherits="CResearchers_CPanelManageExchangeOrders_PageManageProductFileSearchers" %>

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
            var gv = document.getElementById("<%=GVFileSearchers.ClientID%>");
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
    <script src="../../view/javascript/jquery.min.js"></script>
    <script src="../../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageManageProductMatterOfExchange.aspx" runat="server" id="IDAdd" data-toggle="tooltip" title="إضافة أمر صرف جديد" class="btn btn-primary"><i class="fa fa-plus"></i></a>
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
                        <li><a href="PageManageProductFileSearchers.aspx">قائمة أوامر الصرف التي تحتاج إلى مراجعة الباحث</a></li>
                    </ul>
                </div>
            </div>
            <div align="rigth" style="padding: 0 15px 0 15px">
                <hr style="border: double; border-width: 1px; width: 100%" />
                <h5><i class="fa fa-star"></i>حدد نوع الصرف : </h5>
                <asp:RadioButton ID="RBTathith" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RBTathith_CheckedChanged" Visible="false" />
                <i class="fa fa-star"></i>
                <span>فرز أوامر السلل الغذائية - الأجهزة الكهربائية - تأثيث المنازل - التالف </span>
                <span style="color: #9c1800">( الموجود حالياً
                    <asp:Label ID="lblCountCard" runat="server" Text="0"></asp:Label>
                    )</span>
                <nav class="navbar-dark bg-dark">
                    <a class="btn navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarToggleExternalContent" aria-controls="navbarToggleExternalContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="fa fa-list"></span> عرض الفلترة
                    </a>
                </nav>
                <div class="collapse" id="navbarToggleExternalContent" style="background-color: #ecedec">
                    <div class="bg-dark p-4" style="padding: 5px 5px 10px 0">
                        <h5 class="text-white h4"><i class="fa fa-star"></i>حدد الملفات المراد عرضها : </h5>
                        <span class="text-muted">
                            <asp:RadioButton ID="RBCardCheck" runat="server" GroupName="RBCard" AutoPostBack="true" OnCheckedChanged="RBCardCheck_CheckedChanged" />
                            <span>عرض ملفات السلل الغذائية </span>
                            <i class="fa fa-minus"></i>
                            <asp:RadioButton ID="RBDeviceCheck" runat="server" GroupName="RBCard" AutoPostBack="true" OnCheckedChanged="RBDeviceCheck_CheckedChanged" />
                            <span>عرض ملفات الاجهزة الكهربائية </span>
                            <i class="fa fa-minus"></i>
                            <asp:RadioButton ID="RBTathithCheck" runat="server" GroupName="RBCard" AutoPostBack="true" OnCheckedChanged="RBTathithCheck_CheckedChanged" />
                            <span>عرض ملفات تأثيث منازل </span>
                            <asp:Panel ID="pnlTaleef" runat="server" Visible="false">
                                <i class="fa fa-minus"></i>
                                <asp:RadioButton ID="RBTalefCheck" runat="server" GroupName="RBCard" AutoPostBack="true" OnCheckedChanged="RBTalefCheck_CheckedChanged" />
                                <span>عرض ملفات التالف </span>
                            </asp:Panel>
                        </span>
                    </div>
                </div>
                <hr style="border: double; border-width: 1px; width: 100%" />
            </div>
            <div class="container-fluid" id="IDCard" runat="server" visible="false">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة مراجعة الباحثين
                        </h3>
                        <div style="float: left">
                            <asp:Button ID="btnAllow" class="btn btn-info" runat="server" Text="تحديد على انه تم التسليم" OnClick="btnAllow_Click"
                                title="تحديد على انه تم التسليم" data-toggle="tooltip" OnClientClick="return ConfirmDelete();" />
                            <asp:Button ID="btnNotAllow" class="btn btn-danger" runat="server" Text="تحديد على انه لم يتم التسليم" OnClick="btnNotAllow_Click"
                                title="تحديد على انه لم يتم التسليم" data-toggle="tooltip" OnClientClick="return ConfirmDelete();" />
                            سبب عدم التسليم  
                            <asp:TextBox ID="txtNotAllow" runat="server" class="form-control2" ValidationGroup="g2" Width="200px"></asp:TextBox>
                            <asp:Label ID="lblNotAllow" runat="server" Text="*" Visible="false" ForeColor="Red"></asp:Label>
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
                                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="قائمة أوامر الصرف التي تحتاج إلى مراجعة الباحث" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVFileSearchers" runat="server" AutoGenerateColumns="False" DataKeyNames="_billNumber"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVFileSearchers_RowDataBound"
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
                                                                    <%# ClassSaddam.FCheckAllowModer2((bool) (Eval("_IsModer")))%> 
                                                                     , <%# ClassSaddam.FAmeenAlsondoq2((bool) (Eval("_IsAmmenAlSondoq")))%>
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
                                                                <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("_IDCategory")%>' Visible="false"></asp:Label>
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
                                                                <a href='PageManageProductAddThePriceToOrder.aspx?ID=<%# Eval("_billNumber")%>&XID=<%# Eval("_IDMosTafeed")%>&XIDCate=<%# Eval("_IDCategory")%>&IsCart=<%# RBCardCheck.Checked %>&IsDevice=<%# RBDeviceCheck.Checked %>&IsTathith=<%# RBTathithCheck.Checked %>&IsTalef=<%# RBTalefCheck.Checked %>' title="عرض التفاصيل" data-toggle="tooltip"
                                                                    class="btn btn-info"><span class="fa fa-edit"></span></a>
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
                                                    <div class="container-fluid" dir="rtl" runat="server">
                                                        <hr style='border: solid; border-width: 1px; width: 100%' />
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
                                                                        رئيس لجنة البحث الإجتماعية
                                                                        <br />
                                                                        <asp:Image ID="ImgRaeesLagnatAlBahath" runat="server" Width='100px' Height='25' />
                                                                        <br />
                                                                        <asp:Label ID="lblRaeesLagnatAlBahath" runat="server" Font-Size="20px"></asp:Label>
                                                                        <asp:DropDownList ID="DLRaeesLagnatAlBahath" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLRaeesLagnatAlBahath_SelectedIndexChanged"
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
                                                                        <div runat="server" id="IDKhatm" align="left" style="margin-top: 0px">
                                                                            <img src="../../ImgSystem/ImgSignature/الختم.png" />
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
                                                <div align="Left" class="HideThis">
                                                    <img src='../../Img/IconTrue.png' style='width: 20px' />
                                                    <span style="font-size: 11px">موافق</span>
                                                    <img src='../../Img/IconFalse.png' style='width: 20px' />
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

