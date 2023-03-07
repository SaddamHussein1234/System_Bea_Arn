<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageManageProductAddThePriceToOrder.ascx.cs" Inherits="Shaerd_CPanelManageExchangeOrders_PageManageProductAddThePriceToOrder" %>
<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>

<style type="text/css">
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

<link href="../css/chosen.css" rel="stylesheet" />
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
            var gv = document.getElementById("<%=GVMatterOfExchangeByID.ClientID%>");
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
    function ConfirmDeleteTaleef() {
        var count = document.getElementById("<%=hfCountTaleef.ClientID %>").value;
            var gv = document.getElementById("<%=GVMatterOfExchangeByIDTaleef.ClientID%>");
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

<link href="../GridView.css" rel="stylesheet" type="text/css" />

<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
        </div>
        <h1>لوحة التحكم</h1>
        <ul class="breadcrumb">
            <li><a href="Default.aspx">الرئيسية</a></li>
            <li><a href="">المنتجات</a></li>
            <li><a href="">تفاصيل أمر الصرف</a></li>
        </ul>
    </div>
</div>

<div align="rigth" style="padding: 0 15px 0 15px">
    <hr style='border: double; border-width: 1px; width: 100%' />
    <h5><i class="fa fa-star"></i>حدد نوع الصرف : </h5>
    <asp:RadioButton ID="RBTathith" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RBTathith_CheckedChanged" />
    <span>فرز أوامر السلل لـــ 
                : 
            <asp:RadioButton ID="RBCart" runat="server" Text="السلل" GroupName="GCheck" />
        - 
            <asp:RadioButton ID="RBDevice" runat="server" Text="أجهزة كهربائية" GroupName="GCheck" />
        - 
            <asp:RadioButton ID="RBTath" runat="server" Text="تأثيث منزل" GroupName="GCheck" />
    </span>
    <br />
    <asp:RadioButton ID="RPTarmem" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RPTarmem_CheckedChanged" />
    <span>فرز أوامر بناء المنازل - ترميم المنازل </span>
    <br />
    <asp:RadioButton ID="RPTalef" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RPTalef_CheckedChanged" />
    <span>فرز أوامر صرف التالف </span>
    <br />
    <asp:RadioButton ID="RPSupportForPrisms" runat="server" GroupName="RB1" AutoPostBack="true" OnCheckedChanged="RPSupportForPrisms_CheckedChanged" />
    <span>فرز أوامر صرف الدعم المالي </span>
    <hr style="border: double; border-width: 1px; width: 100%" />
</div>
<div class="container-fluid" runat="server" id="ProductByUser" visible="false">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-list"></i>
                <asp:Label ID="lbmsg" runat="server" Text="قائمة فاتورة أمر صرف سلة"></asp:Label>
            </h3>
            <div style="float: left">
                <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم الفاتورة ... " Width="100"></asp:TextBox>
                <asp:LinkButton ID="btnSearch" runat="server" data-toggle="tooltip" title="جلب" OnClick="btnSearch_Click"
                    class="btn btn-info"><span class="tip-bottom"><i class="fa fa-search" style="font-size:16px"></i></span></asp:LinkButton>
                <asp:LinkButton ID="LbRefreshSaraf" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LbRefreshSaraf_Click"
                    title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                <asp:LinkButton ID="LBPrintSaraf" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="LBPrintSaraf_Click"
                    title="طباعة">
                    <i class="fa fa-print"></i></asp:LinkButton>
                <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click"
                    OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
            </div>
            <div style="float: left">
                <span>حدد المشروع : <span style="color: red">*</span>
                </span>
                <asp:DropDownList ID="DLSupportType" runat="server" ValidationGroup="g2" CssClass="form-control2" Width="150" Height="36" Style="margin-left: 3px">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator13" runat="server"
                    ControlToValidate="DLSupportType" ErrorMessage="* حدد المشروع" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div class="panel-body">
            <asp:Panel ID="pnlDataSarf" runat="server" Direction="RightToLeft">
                <div class="">
                    <div align="center" class="w">
                        <div class="table table-responsive">
                            <table style="width: 100%; background-color: #ffffff; color: #393939">
                                <tr>
                                    <td style="border: thin double #808080; border-width: 1px; width: 45%">
                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                    </td>
                                    <td style="border: thin double #808080; border-width: 1px; width: 20%">
                                        <table style="width: 100%; font-size: 12px">
                                            <tr>
                                                <td align="left" style="width: 60%; font-family: 'Alwatan'; font-size: 18px;">رقم الأمر / 
                                                </td>
                                                <td style="width: 40%; font-family: 'Alwatan'; font-size: 18px;">
                                                    <asp:Label ID="lblNumber" runat="server"></asp:Label>
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
                                                    <asp:Label ID="lblDateHide" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div style="float: right; padding: 10px 10px 0 10px;" class="w">
                    <p style="font-size: 13px">
                        السيد / أمين المستودع<asp:Label ID="lblAmeenAlmosTodaa" runat="server" Visible="false"></asp:Label>
                    </p>
                    <p style="font-size: 13px">
                        <asp:Label ID="lblSarf" runat="server" Text="بموجبه يتم الصرف للسيد / "></asp:Label>
                    </p>
                </div>
                <div style="float: left; padding: 10px 0 0 10px" class="w">
                    <table style="font-size: 12px">
                        <tr>
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">مدخل البيانات :
                                        <asp:Label ID="lblDataEntry" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                        <asp:Label ID="lblDateEntry" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr runat="server" id="IDUpdate" visible="false">
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">
                                <asp:Label ID="lblDataEntryEdit" runat="server"></asp:Label>
                            </td>
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                        <asp:Label ID="lblDateEntryEdit" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div align='center' class="w">
                    <asp:Image ID="IDBarcode" runat="server" alt='Loding' />
                </div>
                <table style="width: 100%">
                    <tr runat="server" id="IDUserDetails">
                        <td style="width: 40%; border: thin double #808080; border-width: 1px; padding: 5px" align="center">
                            <p style="font-size: 11px">
                                الإسم :
                                        <asp:Label ID="lblNameEvint" runat="server" Font-Size="11px"></asp:Label>
                                <asp:Label ID="lblCategory" runat="server" Visible="false"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                            <p style="font-size: 11px">
                                الجوال :
                                            0<asp:Label ID="lblPhone2" runat="server" Font-Size="11px"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                            <p style="font-size: 11px">
                                القرية :
                                            <asp:Label ID="lblAlQariah2" runat="server" Font-Size="11px"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                            <p style="font-size: 11px">
                                رقم الملف :
                                            <asp:Label ID="txtNumberMostafeed2" runat="server" Font-Size="11px"></asp:Label>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" align="Right">
                            <p style="font-size: 13px">
                                الأصناف الموضحة أدناه : 
                            </p>
                        </td>
                    </tr>
                </table>
                <span class="hr"></span>
                <div class="table table-responsive">
                    <asp:GridView ID="GVMatterOfExchangeByID" runat="server" AutoGenerateColumns="False" DataKeyNames="_IDItem"
                        Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVMatterOfExchangeByID_RowDataBound"
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
                            <asp:TemplateField HeaderText="رقم الطلب" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                <ItemTemplate>
                                    <%# Eval("_IDNumberProduct")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="الصنف" HeaderStyle-ForeColor="#CCCCCC">
                                <ItemTemplate>
                                    <asp:Label ID="lblCategory" runat="server" Font-Size="12px" Text='<%# Eval("CategoryName")%>'></asp:Label>
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
                            <asp:TemplateField HeaderText="العدد" HeaderStyle-ForeColor="#CCCCCC">
                                <ItemTemplate>
                                    <asp:Label ID="lblCount" runat="server" Font-Size="12px" Text='<%# Eval("_CountProduct")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="السعر الفردي" HeaderStyle-ForeColor="#CCCCCC">
                                <ItemTemplate>
                                    <%# Eval("_PriceOfTheGrain")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="المجموع" HeaderStyle-ForeColor="#CCCCCC">
                                <ItemTemplate>
                                    <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" Text='<%# Eval("_TotalPrice")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="تاريخ الطلب" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                <ItemTemplate>
                                    <%# ClassDataAccess.FChangeF((DateTime) (Eval("_ProductionDate")))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="مدخل البيانات" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                <ItemTemplate>
                                    <%# ClassQuaem.FAlBaheth((Int32) (Eval("_IDAdmin")))%>
                                    <br />
                                    <%# ClassQuaem.FAlBahethByEdit((Int32) (Eval("_IDUpdate")))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="بتاريخ" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                <ItemTemplate>
                                    <%# ClassDataAccess.FChangeF((DateTime) (Eval("_DateAddProduct")))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="10px" Visible="false">
                                <ItemTemplate>
                                    <a href='PageManageProductMatterOfExchange.aspx?ID=<%# Eval("_IDUniq")%>' title="تعديل" data-toggle="tooltip"
                                        class="btn btn-primary"><span class="fa fa-eye"></span></a>
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
                </div>
                <div style="display: none">
                    <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                    <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                    <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                    - <span style="font-size: 12px; padding-right: 5px">عدد الطلبات : </span>
                    <asp:Label ID="lblSum" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                </div>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 15%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">المجموع : 
                        </td>
                        <td style="width: 65%; border: thin double #808080; border-width: 1px;" align="center">
                            <asp:TextBox ID="lblSumSaraf" runat="server" class="form-control" placeholder="المبلغ" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                        </td>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                            <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                        </td>
                    </tr>
                </table>
                <hr />
                <div class="table table-responsive">
                    <div class="WidthText2" style="border: thin double #808080; border-width: 1px;" align="center">
                        <table style="width: 100%; margin: 5px; font-size: 12px">
                            <tr>
                                <td style="width: 45%;">مدير الجمعية : 
                                </td>
                                <td style="width: 55%;">
                                    <asp:Image ID="ImgModer" runat="server" Width='100px' Height='30px' />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="WidthText2" style="border: thin double #808080; border-width: 1px;" align="center">
                        <table style="width: 100%; margin: 5px; font-size: 12px">
                            <tr>
                                <td style="width: 45%;">المشرف المالي : 
                                </td>
                                <td style="width: 55%;">
                                    <asp:Image ID="ImgAmeenAlsondoq" runat="server" Width='100px' Height='30px' />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="WidthText2" style="border: thin double #808080; border-width: 1px;" align="center">
                        <table style="width: 100%; margin: 5px; font-size: 12px">
                            <tr>
                                <td style="width: 45%;">رئيس الجمعية : 
                                </td>
                                <td style="width: 55%;">
                                    <asp:Image ID="ImgRaees" runat="server" Width='100px' Height='30px' />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <asp:Panel ID="IDTableManager" runat="server" Visible="false">
                    <table style="width: 100%">
                        <tr>
                            <td colspan="4">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="center" style="width: 50%">أمين المستودع 
                                        <br />
                                <br />
                            </td>
                            <td align="center" style="width: 50%">رئيس مجلس الإدارة
                                        <br />
                                <br />
                            </td>
                        </tr>
                        <asp:Panel ID="pnllblPrint" runat="server" Visible="false">
                            <tr>
                                <td align="center" style="width: 50%">
                                    <asp:Label ID="lblIDStorekeeper2" runat="server" Font-Size="12px"></asp:Label>
                                </td>
                                <td align="center" style="width: 50%">
                                    <asp:Label ID="lblRaeesMaglesAlEdarah" runat="server" Font-Size="12px"></asp:Label>
                                </td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel ID="pnlDlPrint" runat="server">
                            <tr>
                                <td align="center" style="width: 50%">
                                    <asp:DropDownList ID="DLIDStorekeeper2" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td align="center" style="width: 50%">
                                    <asp:DropDownList ID="DLRaeesMaglesAlEdarah2" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </asp:Panel>
                    </table>
                </asp:Panel>
                <hr />
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%; border: thin double #808080; border-width: 1px;">
                            <p style="font-size: 13px">
                                <div align="center" style="font-size: 13px">
                                    تم الصرف
                                            <asp:CheckBox ID="CBDone" runat="server" Enabled="false" />
                                    / لم يتم الصرف بعد
                                            <asp:CheckBox ID="CBNotDone" runat="server" Enabled="false" />
                                </div>
                            </p>
                            <p style="font-size: 13px; padding-right: 5px">
                                أمين المستودع / 
                                            <asp:Label ID="lblAmeenAlmosTodaa2" runat="server"></asp:Label>
                                <asp:Image ID="ImgAmeenAlmosTodaa" runat="server" Width='100px' Height='30px' />
                            </p>
                            <p style="font-size: 13px; padding-right: 5px">
                                بتاريخ / 
                                            <asp:Label ID="lblDateGo" runat="server"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 50%; border: thin double #808080; border-width: 1px;">
                            <p style="font-size: 13px">
                                <div align="center" style="font-size: 13px">
                                    تم التسليم
                                            <asp:CheckBox ID="CBReceived" runat="server" Enabled="false" />
                                    / لم يتم التسليم بعد
                                            <asp:CheckBox ID="CBNotReceived" runat="server" Enabled="false" />
                                    <span runat="server" id="IDNotReceived" visible="false">/ السبب :
                                                <asp:Label ID="lblA2" runat="server"></asp:Label>
                                    </span>
                                </div>
                            </p>
                            <p style="font-size: 13px; padding-right: 5px">
                                إسم الباحث / 
                                            <asp:Label ID="lblNameEvint2" runat="server"></asp:Label>
                                <asp:Image ID="ImgAlBaheth" runat="server" Width='100px' Height='30px' />
                            </p>
                            <p style="font-size: 13px; padding-right: 5px">
                                تاريخ التسليم / 
                                            <asp:Label ID="lblDateRecived" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                </table>
                <div align="left" style="margin-top: -60px" runat="server" id="IDKhatm" visible="false">
                    <img src="/ImgSystem/ImgSignature/الختم.png" alt="" />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlNullSarf" runat="server" Visible="False">
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
<div class="container-fluid" runat="server" id="ProductByTalef" visible="false">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-list"></i>قائمة فاتورة أمر صرف تالف
            </h3>
            <div style="float: left">
                <asp:TextBox ID="txtSearchTalef" runat="server" CssClass="WidthText20" placeholder=" رقم الفاتورة ... "></asp:TextBox>
                <asp:LinkButton ID="btnSearchTalef" runat="server" data-toggle="tooltip" title="جلب" OnClick="btnSearchTalef_Click"
                    class="btn btn-info"><span class="tip-bottom"><i class="fa fa-search" style="font-size:16px"></i></span></asp:LinkButton>
                <asp:LinkButton ID="LBRefresh" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LBRefresh_Click"
                    title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                <asp:LinkButton ID="LbPrintTaleef" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="LbPrintTaleef_Click"
                    title="طباعة">
                    <i class="fa fa-print"></i></asp:LinkButton>
                <asp:LinkButton ID="btnDeleteTaleef" runat="server" class="btn btn-danger" OnClick="btnDeleteTaleef_Click"
                    OnClientClick="return ConfirmDeleteTaleef();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
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
                                            <td align="left" style="width: 60%; font-family: 'Alwatan'; font-size: 18px;">رقم الأمر / 
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
                    <asp:Image ID="IDBarcodeTalef" runat="server" alt='Loding' />
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
                        <asp:TemplateField HeaderText="رقم الطلب" HeaderStyle-ForeColor="#CCCCCC">
                            <ItemTemplate>
                                <%# Eval("_IDNumberProduct")%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
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
                        <asp:TemplateField HeaderText="العدد" HeaderStyle-ForeColor="#CCCCCC">
                            <ItemTemplate>
                                <asp:Label ID="lblCountTaleef" runat="server" Font-Size="12px" Text='<%# Eval("_CountProduct")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="السعر الفردي" HeaderStyle-ForeColor="#CCCCCC">
                            <ItemTemplate>
                                <%# Eval("_PriceOfTheGrain")%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="المجموع" HeaderStyle-ForeColor="#CCCCCC">
                            <ItemTemplate>
                                <asp:Label ID="lblCountTotalPriceTaleef" runat="server" Font-Size="12px" Text='<%# Eval("_TotalPrice")%>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="تاريخ الطلب" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                            <ItemTemplate>
                                <%# ClassDataAccess.FChangeF((DateTime) (Eval("_ProductionDate")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="مدخل البيانات" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                            <ItemTemplate>
                                <%# ClassQuaem.FAlBaheth((Int32) (Eval("_IDAdmin")))%>
                                <br />
                                <%# ClassQuaem.FAlBahethByEdit((Int32) (Eval("_IDUpdate")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="بتاريخ" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                            <ItemTemplate>
                                <%# ClassDataAccess.FChangeF((DateTime) (Eval("_DateAddProduct")))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="10px" Visible="false">
                            <ItemTemplate>
                                <a href='PageManageProductMatterOfExchange.aspx?ID=<%# Eval("_IDUniq")%>' title="تعديل" data-toggle="tooltip"
                                    class="btn btn-primary"><span class="fa fa-eye"></span></a>
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
                            <asp:Label ID="Label4" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
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
                    <img src="/ImgSystem/ImgSignature/الختم.png" width="120" alt="" />
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
<div class="container-fluid" runat="server" id="ProductByTarmim" visible="false">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-list"></i>
                <asp:Label ID="lblmsg" runat="server" Text="قائمة فاتورة"></asp:Label>
                   <asp:RadioButton ID="RBBenaCheck" runat="server" GroupName="GCheck" Checked="true" /> بناء منزل
                - 
                   <asp:RadioButton ID="RBTarmimCheck" runat="server" GroupName="GCheck" /> ترميم منزل 
            </h3>
            <div style="float: left">
                <asp:TextBox ID="txtSearchTarmim" runat="server" CssClass="WidthText20" placeholder=" رقم الامر ... " ValidationGroup="gTarmim"></asp:TextBox>
                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                    ControlToValidate="txtSearchTarmim" ErrorMessage="* رقم الامر" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                    ValidationGroup="gTarmim" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSearchTarmim"
                    ErrorMessage="أرقام فقط" Font-Size="10px" ValidationExpression="^[0-9]+$" ValidationGroup="gTarmim"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
                <asp:LinkButton ID="btnSearchTarmim" runat="server" data-toggle="tooltip" title="جلب" OnClick="btnSearchTarmim_Click" ValidationGroup="gTarmim"
                    class="btn btn-info"><span class="tip-bottom"><i class="fa fa-search" style="font-size:16px"></i></span></asp:LinkButton>
                <asp:LinkButton ID="LBRefreshTarmim" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LBRefreshTarmim_Click"
                    title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                <asp:LinkButton ID="LbPrintTarmim" runat="server" class="btn btn-success" data-toggle="tooltip"
                    title="طباعة" OnClick="LbPrintTarmim_Click">
                        <i class="fa fa-print"></i></asp:LinkButton>
                <asp:LinkButton ID="btnDeleteTarmim" runat="server" class="btn btn-danger"
                    OnClientClick="return ConfirmDeleteTaleef();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                        <i class="fa fa-trash-o"></i></span></asp:LinkButton>
            </div>
        </div>
        <div class="panel-body">
            <asp:Panel ID="pnlDataTarmim" runat="server" Direction="RightToLeft" Font-Size="14px">
                <div class="">
                    <div align="center" class="w">
                        <table style="width: 100%; background-color: #ffffff; color: #393939">
                            <tr>
                                <td style="border: thin double #808080; border-width: 1px; width: 45%">
                                    <asp:TextBox ID="txtTitleTarmim" runat="server" class="form-control" Text="أمر صرف لمستفيد لمشروع " placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                </td>
                                <td style="border: thin double #808080; border-width: 1px; width: 20%; font-family: 'Alwatan'; font-size: 18px;">
                                    <span style="padding-right: 10px; font-size: 18px;">رقم الأمر /  </span>
                                    <asp:Label ID="lblNumberTarmim" runat="server"></asp:Label>
                                </td>
                                <td style="border: thin double #808080; border-width: 1px; width: 35%">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td align="left" style="width: 20%; font-size: 12px">التاريخ / 
                                            </td>
                                            <td style="width: 80%">
                                                <asp:Label ID="lblDateHideTarmim" runat="server" Font-Size="12px"></asp:Label>
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
                        السيد / المشرف المالي<asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                    </p>
                    <p style="font-size: 13px">
                        <asp:Label ID="Label5" runat="server" Text="بموجبه يتم الصرف للسيد / "></asp:Label>
                    </p>
                </div>
                <div style="float: left; padding: 10px 0 0 10px" class="w">
                    <table style="font-size: 12px">
                        <tr>
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">مدخل البيانات :
                                        <asp:Label ID="lblDataEntryTarmim" runat="server" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                        <asp:Label ID="lblDateEntryTarmim" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr runat="server" id="Tr1" visible="false">
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">
                                <asp:Label ID="Label10" runat="server" Font-Size="12px"></asp:Label>
                            </td>
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                        <asp:Label ID="Label11" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div align='center' class="w">
                    <asp:Image ID="IDBarcodeTarmim" runat="server" alt='Loding' />
                </div>
                <table style="width: 100%">
                    <tr runat="server" id="IDUserDetailsTarmim">
                        <td style="width: 40%; border: thin double #808080; border-width: 1px; padding: 5px" align="center">
                            <p style="font-size: 13px">
                                الإسم :
                                        <asp:Label ID="lblNameEvintTarmim" runat="server" Font-Size="13px"></asp:Label>
                                <asp:Label ID="lblCategoryTarmim" runat="server" Visible="false"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                            <p style="font-size: 13px">
                                الجوال :
                                            0<asp:Label ID="lblPhone2Tarmim" runat="server" Font-Size="13px"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                            <p style="font-size: 13px">
                                القرية :
                                            <asp:Label ID="lblAlQariah2Tarmim" runat="server" Font-Size="13px"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                            <p style="font-size: 13px">
                                رقم الملف :
                                            <asp:Label ID="txtNumberMostafeed2Tarmim" runat="server" Font-Size="13px"></asp:Label>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" align="Right" colspan="4">
                            <span style="font-size: 14px">
                                <hr style="border: double; border-width: 1px; width: 100%" />
                                مبلغ وقدرة : - 
                            </span>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">
                            <asp:Label ID="lblTotalPriceTarmim" runat="server" Text="0" Style='color: Red; font-size: 13px'></asp:Label>
                            <asp:Label ID="Label15" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                        </td>
                        <td style="width: 80%; border: thin double #808080; border-width: 1px;" align="center">
                            <asp:TextBox ID="lblSumSarafTarmim" runat="server" class="form-control" placeholder="المبلغ" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">طريقة الدفع 
                        </td>
                        <td style="width: 80%; border: thin double #808080; border-width: 1px;" align="center">
                            <asp:CheckBox ID="CBCash_Money_" runat="server" Width="20px" Enabled="false" />
                            <span>نقداً </span>
                            <asp:CheckBox ID="CBShayk_Bank" runat="server" Width="20px" Enabled="false" />
                            <span>شيك </span>
                            <asp:Label ID="lblNumber_Shayk_Bank" runat="server"></asp:Label>
                            - التاريخ : 
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div style="margin: 10px">
                    وذلك عن مشروع /   
                            <asp:Label ID="lblProject" runat="server" Text="---"></asp:Label>
                    المستفيد الموضح بياناته بعالية , بناء على الفواتير المرفقة . 
                </div>
                <hr style="border: double; border-width: 1px; width: 100%" />
                <div class="table ">
                    <div class="WidthText4" style="border: thin double #808080; border-width: 1px;" align="center">
                        <table style="width: 100%; margin: 5px; font-size: 13px">
                            <tr>
                                <td style="width: 45%;">مدير الجمعية : 
                                </td>
                                <td style="width: 55%;">
                                    <asp:Image ID="ImgModerTarmim" runat="server" Width='100px' Height='30px' />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="WidthText4" style="border: thin double #808080; border-width: 1px;" align="center">
                        <table style="width: 100%; margin: 5px; font-size: 13px">
                            <tr>
                                <td style="width: 45%;">رئيس مجلس الإدارة : 
                                </td>
                                <td style="width: 55%;">
                                    <asp:Image ID="ImgRaeesTarmim" runat="server" Width='100px' Height='30px' />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <hr />
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%; border: thin double #808080; border-width: 1px;">
                            <p style="font-size: 13px">
                                <div align="center" style="font-size: 13px">
                                    تم الصرف
                                            <asp:CheckBox ID="CBAllowState" runat="server" Enabled="false" />
                                    / لم يتم الصرف
                                            <asp:CheckBox ID="CBNotAllowState" runat="server" Enabled="false" />
                                    <asp:Label ID="lblWhayNotAllow" runat="server"></asp:Label>
                                </div>
                            </p>
                            <p style="font-size: 13px; padding-right: 5px">
                                المشرف المالي / 
                                            <asp:Label ID="Label7" runat="server"></asp:Label>
                                <asp:Image ID="ImgAmeenAlsondoqTarmim" runat="server" Width='100px' Height='30px' />
                                <asp:Label ID="lblDateAllowOrNotAllow" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                </table>
                <div align="left" style="margin-top: -60px" runat="server" id="IDKhatmTarmim" visible="false">
                    <img src="/ImgSystem/ImgSignature/الختم.png" alt="" />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlNullTarmim" runat="server" Visible="False">
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
<div class="container-fluid" runat="server" id="ProductByPrisms" visible="false">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-list"></i>
                <asp:Label ID="lblmsgPrisms" runat="server" Text="قائمة فاتورة الدعم المالي"></asp:Label> <asp:Label ID="lbl" runat="server" Text="---"></asp:Label>
            </h3>
            <div style="float: left">
                <span>ححد المشروع : </span>
                <asp:DropDownList ID="DLProject" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2" Style="font-size: 12px; width: 150px" Height="36">
                    <asp:ListItem Value=""></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                    ControlToValidate="DLProject" ErrorMessage="* المشروع" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                    ValidationGroup="gTarmim" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtSearchPrisms" runat="server" CssClass="WidthText20" placeholder=" رقم الامر ... " ValidationGroup="gTarmim"></asp:TextBox>
                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtSearchPrisms" ErrorMessage="* رقم الامر" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                    ValidationGroup="gTarmim" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSearchPrisms"
                    ErrorMessage="أرقام فقط" Font-Size="10px" ValidationExpression="^[0-9]+$" ValidationGroup="gTarmim"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
                <asp:LinkButton ID="btnSearchPrisms" runat="server" data-toggle="tooltip" title="جلب" OnClick="btnSearchPrisms_Click" ValidationGroup="gTarmim"
                    class="btn btn-info"><span class="tip-bottom"><i class="fa fa-search" style="font-size:16px"></i></span></asp:LinkButton>
                <asp:LinkButton ID="LBRefreshPrisms" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LBRefreshPrisms_Click"
                    title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                <asp:LinkButton ID="LbPrintPrisms" runat="server" class="btn btn-success" data-toggle="tooltip"
                    title="طباعة" OnClick="LbPrintPrisms_Click">
                        <i class="fa fa-print"></i></asp:LinkButton>
                <asp:LinkButton ID="LinkButton4" runat="server" class="btn btn-danger"
                    OnClientClick="return ConfirmDeleteTaleef();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                        <i class="fa fa-trash-o"></i></span></asp:LinkButton>
            </div>
        </div>
        <div class="panel-body">
            <asp:Panel ID="pnlDataPrisms" runat="server" Direction="RightToLeft" Font-Size="14px">
                <div class="">
                    <div align="center" class="w">
                        <table style="width: 100%; background-color: #ffffff; color: #393939">
                            <tr>
                                <td style="border: thin double #808080; border-width: 1px; width: 45%">
                                    <asp:TextBox ID="txtTitlePrisms" runat="server" class="form-control" Text="أمر صرف لمستفيد لمشروع " placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                </td>
                                <td style="border: thin double #808080; border-width: 1px; width: 20%; font-family: 'Alwatan'; font-size: 18px;">
                                    <span style="padding-right: 10px; font-size: 18px;">رقم الأمر /  </span>
                                    <asp:Label ID="lblNumberPrisms" runat="server"></asp:Label>
                                </td>
                                <td style="border: thin double #808080; border-width: 1px; width: 35%">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td align="left" style="width: 20%; font-size: 12px">التاريخ / 
                                            </td>
                                            <td style="width: 80%">
                                                <asp:Label ID="lblDateHidePrisms" runat="server" Font-Size="12px"></asp:Label>
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
                        السيد / المشرف المالي<asp:Label ID="Label12" runat="server" Visible="false"></asp:Label>
                    </p>
                    <p style="font-size: 13px">
                        <asp:Label ID="Label13" runat="server" Text="بموجبه يتم الصرف للسيد / "></asp:Label>
                    </p>
                </div>
                <div style="float: left; padding: 10px 0 0 10px" class="w">
                    <table style="font-size: 12px">
                        <tr>
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">مدخل البيانات :
                                        <asp:Label ID="lblDataEntryPrisms" runat="server" Font-Size="12px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                        <asp:Label ID="lblDateEntryPrisms" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr runat="server" id="Tr2" visible="false">
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">
                                <asp:Label ID="Label17" runat="server" Font-Size="12px"></asp:Label>
                            </td>
                            <td style="border: thin double #C0C0C0; border-width: 1px; padding: 5px">بتاريخ :
                                        <asp:Label ID="Label18" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div align='center' class="w">
                    <asp:Image ID="IDBarcodePrisms" runat="server" alt='Loding' />
                </div>
                <table style="width: 100%">
                    <tr runat="server" id="IDUserDetailsPrisms">
                        <td style="width: 40%; border: thin double #808080; border-width: 1px; padding: 5px" align="center">
                            <p style="font-size: 13px">
                                الإسم :
                                        <asp:Label ID="lblNameEvintPrisms" runat="server" Font-Size="13px"></asp:Label>
                                <asp:Label ID="lblCategoryPrisms" runat="server" Visible="false"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                            <p style="font-size: 13px">
                                الجوال :
                                            0<asp:Label ID="lblPhone2Prisms" runat="server" Font-Size="13px"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                            <p style="font-size: 13px">
                                القرية :
                                            <asp:Label ID="lblAlQariah2Prisms" runat="server" Font-Size="13px"></asp:Label>
                            </p>
                        </td>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                            <p style="font-size: 13px">
                                رقم الملف :
                                            <asp:Label ID="txtNumberMostafeed2Prisms" runat="server" Font-Size="13px"></asp:Label>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%" align="Right" colspan="4">
                            <span style="font-size: 14px">
                                <hr style="border: double; border-width: 1px; width: 100%" />
                                مبلغ وقدرة : - 
                            </span>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">
                            <asp:Label ID="lblTotalPricePrisms" runat="server" Text="0" Style='color: Red; font-size: 13px'></asp:Label>
                            <asp:Label ID="Label150" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                        </td>
                        <td style="width: 80%; border: thin double #808080; border-width: 1px;" align="center">
                            <asp:TextBox ID="lblSumSarafPrisms" runat="server" class="form-control" placeholder="المبلغ" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">طريقة الدفع 
                        </td>
                        <td style="width: 80%; border: thin double #808080; border-width: 1px;" align="center">
                            <asp:CheckBox ID="CBCash_Money_Prisms" runat="server" Width="20px" Enabled="false" />
                            <span>نقداً </span>
                            <asp:CheckBox ID="CBShayk_BankPrisms" runat="server" Width="20px" Enabled="false" />
                            <span>شيك </span>

                            <asp:Label ID="lblNumber_Shayk_Bank_Prisms" runat="server"></asp:Label>

                            - التاريخ : 
                                    <asp:Label ID="lblDatePrisms" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div style="margin: 10px">
                    <asp:Label ID="lblProjectPrisms" runat="server" Text="---"></asp:Label>
                </div>
                <hr style="border: double; border-width: 1px; width: 100%" />
                <div class="table ">
                    <div class="WidthText4" style="border: thin double #808080; border-width: 1px;" align="center">
                        <table style="width: 100%; margin: 5px; font-size: 13px">
                            <tr>
                                <td style="width: 45%;">مدير الجمعية : 
                                </td>
                                <td style="width: 55%;">
                                    <asp:Image ID="ImgModerPrisms" runat="server" Width='100px' Height='30px' />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="WidthText4" style="border: thin double #808080; border-width: 1px;" align="center">
                        <table style="width: 100%; margin: 5px; font-size: 13px">
                            <tr>
                                <td style="width: 45%;">رئيس مجلس الإدارة : 
                                </td>
                                <td style="width: 55%;">
                                    <asp:Image ID="ImgRaeesPrisms" runat="server" Width='100px' Height='30px' />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <hr />
                <table style="width: 100%">
                    <tr>
                        <td style="width: 50%; border: thin double #808080; border-width: 1px;">
                            <p style="font-size: 13px">
                                <div align="center" style="font-size: 13px">
                                    تم الصرف
                                            <asp:CheckBox ID="CBAllowStatePrisms" runat="server" Enabled="false" />
                                    / لم يتم الصرف
                                            <asp:CheckBox ID="CBNotAllowStatePrisms" runat="server" Enabled="false" />
                                    <asp:Label ID="lblWhayNotAllowPrisms" runat="server"></asp:Label>
                                </div>
                            </p>
                            <p style="font-size: 13px; padding-right: 5px">
                                المشرف المالي / 
                                            <asp:Label ID="Label70" runat="server"></asp:Label>
                                <asp:Image ID="ImgAmeenAlsondoqPrisms" runat="server" Width='100px' Height='30px' />
                                <asp:Label ID="lblDateAllowOrNotAllowPrisms" runat="server"></asp:Label>
                            </p>
                        </td>
                    </tr>
                </table>
                <div align="left" style="margin-top: -60px" runat="server" id="IDKhatmPrisms" visible="false">
                    <img src="/ImgSystem/ImgSignature/الختم.png" alt="" />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlNullPrisms" runat="server" Visible="False">
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

