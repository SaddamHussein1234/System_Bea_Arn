<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageManageProductShippingWarehouse.ascx.cs" Inherits="Shaerd_CPanelManageWarehouse_PageManageProductShippingWarehouse" %>
<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

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
            var gv = document.getElementById("<%=GVProductShopWarehouseByID.ClientID%>");
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

<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <a href="" class="btn btn-primary">
                <i class="fa fa-plus"></i>
            </a>
            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                title="تحديث" OnClick="btnRefrish_Click">
            <i class="fa fa-refresh"></i></asp:LinkButton>
            <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" class="btn btn-default">
                <i class="fa fa-reply"></i></asp:LinkButton>
        </div>
        <h1>لوحة التحكم</h1>
        <ul class="breadcrumb">
            <li><a href="Default.aspx">الرئيسية</a></li>
            <li><a href="">المنتجات</a></li>
            <li><a href="">إضافة شحنة للمستودع</a></li>
        </ul>
    </div>
</div>
<div class="container-fluid">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="lbmsg" runat="server" Text="إضافة شحنة للمستودع"></asp:Label>
                /
                <asp:Label ID="lblLastBill" runat="server" Text="إضافة شحنة للمستودع"></asp:Label>
            </h3>
            <div style="float: left">
                رقم الفاتورة :
                    <asp:TextBox ID="txtNumberBill" runat="server" ValidationGroup="g2" Width="100" Style="padding-right: 5px"></asp:TextBox>
                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                    ControlToValidate="txtNumberBill" ErrorMessage="* رقم الفاتورة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumberBill"
                    ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                    Display="Dynamic">
                </asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="panel-body">
            <div class="content-box-large">
                <div class="widget-box">
                    <div style="float: left; padding: 5px;">
                        <asp:Image ID="Img" runat="server" ImageUrl="/ImgSystem/ImgProductStorage/no-img.jpg" Style="border-radius: 6px" Width="320px" Height="180" />
                        <h5>صورة المنتج :
                        </h5>
                        <asp:FileUpload ID="FUArticle" runat="server" />
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-lg-7">
                            <div class="form-group">
                                <h5>إستلمنا من : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLCompany" runat="server" ValidationGroup="g2"
                                    Height="25px" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator18" runat="server"
                                    ControlToValidate="DLCompany" ErrorMessage="* حدد الداعم" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <h5>حدد الصنف : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLCategory" runat="server" ValidationGroup="g2" AutoPostBack="true" CssClass="form-control2 chzn-select dropdown"
                                    Width="100%" OnSelectedIndexChanged="DLCategory_SelectedIndexChanged">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="DLCategory" ErrorMessage="* حدد الصنف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                <br />
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <h5>حدد المنتج : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLProduct" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown" AutoPostBack="true"
                                    Width="100%" OnSelectedIndexChanged="DLProduct_SelectedIndexChanged">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="DLProduct" ErrorMessage="* حدد المنتج" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <h5>رقم الشحنة : <span style="color: red">*</span>
                                </h5>
                                <asp:TextBox ID="txtIDNumberProduct" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                    ControlToValidate="txtIDNumberProduct" ErrorMessage="* رقم الشحنة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtIDNumberProduct"
                                    ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                    Display="Dynamic">
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <h5>الكمية (العدد) :
                                    <asp:Label ID="lblCheckCountProduct" runat="server" ForeColor="Red"></asp:Label> <span style="color: red">*</span>
                                </h5>
                                <asp:TextBox ID="txtCountProduct" runat="server" class="form-control" ValidationGroup="g2" TextMode="Number"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="txtCountProduct" ErrorMessage="* الكمية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <h5>سعر الحبة : <span style="color: red">*</span>
                                </h5>
                                <asp:TextBox ID="txtPriceOfTheGrain" runat="server" class="form-control" ValidationGroup="g2" AutoPostBack="true" OnTextChanged="txtPriceOfTheGrain_TextChanged"
                                    Style="direction: ltr"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="txtPriceOfTheGrain" ErrorMessage="* سعر الحبة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPriceOfTheGrain"
                                    ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]{1,2}([,.]{1}[0-9]{1,2})?$" ValidationGroup="g2"
                                    Display="Dynamic">
                                </asp:RegularExpressionValidator>--%>
                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPriceOfTheGrain"
                                    ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                    Display="Dynamic">
                                </asp:RegularExpressionValidator>--%>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <h5>السعر الكلي : <span style="color: red">*</span>
                                </h5>
                                <asp:TextBox ID="txtTotalPrice" runat="server" class="form-control" ValidationGroup="g2" Enabled="false"
                                    Style="direction: ltr"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                    ControlToValidate="txtTotalPrice" ErrorMessage="* السعر الكلي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-lg-3" runat="server" visible="false">
                            <div class="form-group">
                                <h5>تاريخ الإنتاج : 
                                </h5>
                                <div class="col-sm-3">
                                    <div class="input-group date " style="margin-right: -10px">
                                        <asp:TextBox ID="txtProductionDate" runat="server" class="form-control" Width="170" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                        <%--<asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator32" runat="server"
                                            ControlToValidate="txtProductionDate" ErrorMessage="* تاريخ الإنتاج" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <h5>تاريخ الإنتهاء : 
                                </h5>
                                <div class="col-sm-3">
                                    <div class="input-group date " style="margin-right: -10px">
                                        <asp:TextBox ID="txtExpiryDate" runat="server" class="form-control" Width="170" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                        <%--<asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="txtExpiryDate" ErrorMessage="* تاريخ الإنتهاء" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <h5>تاريخ وصول الشحنة : <span style="color: red">*</span>
                                </h5>
                                <div class="col-sm-3">
                                    <div class="input-group date " style="margin-right: -10px">
                                        <asp:TextBox ID="txtDateCaming" runat="server" class="form-control" Width="170" data-date-format="DD-MM-YYYY" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                            ControlToValidate="txtDateCaming" ErrorMessage="* تاريخ التسجيل" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <h5>تاريخ الإضافة : <span style="color: red">*</span>
                                </h5>
                                <div class="col-sm-3">
                                    <div class="input-group date " style="margin-right: -10px">
                                        <asp:TextBox ID="txt_Add" runat="server" class="form-control" Width="170" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator32" runat="server"
                                            ControlToValidate="txt_Add" ErrorMessage="* تاريخ الإضافة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="WidthText3">
                            <div class="form-group">
                                <h5>نوع الشحنة :  <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLType" runat="server" ValidationGroup="g2" CssClass="form-control2"
                                    Width="100%">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="وارد إلى المستودع" Selected="True">وارد إلى المستودع</asp:ListItem>
                                    <asp:ListItem Value="مردودات المستفيدين">مردودات المستفيدين</asp:ListItem>
                                    <asp:ListItem Value="وارد إلى الجمعية">وارد إلى الجمعية</asp:ListItem>
                                    <%--<asp:ListItem Value="مردودات الموظفين">مردودات الموظفين</asp:ListItem>--%>
                                    <asp:ListItem Value="غير ذلك">غير ذلك</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                    ControlToValidate="DLType" ErrorMessage="* نوع الشحنة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="WidthText3">
                            <div class="form-group">
                                <h5>مكان التخزين :  <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLStoragePlaces" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                    Width="100%">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                    ControlToValidate="DLStoragePlaces" ErrorMessage="* مكان التخزين" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="WidthText2">
                            <div class="form-group">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%">
                                            <h5>رئيس مجلس الإدارة : <span style="color: red">*</span>
                                            </h5>
                                            <asp:DropDownList ID="DLRaeesMaglesAlEdarah2" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                                Width="100%">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                                ControlToValidate="DLRaeesMaglesAlEdarah2" ErrorMessage="* رئيس مجلس الإدارة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 50%">
                                            <h5>المشرف المالي : <span style="color: red">*</span>
                                            </h5>
                                            <asp:DropDownList ID="DLAmeenAlSondoq" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                                Width="100%">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server"
                                                ControlToValidate="DLAmeenAlSondoq" ErrorMessage="* المشرف المالي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%">
                                            <h5>مدير الجمعية : <span style="color: red">*</span>
                                            </h5>
                                            <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                                Width="100%">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator13" runat="server"
                                                ControlToValidate="DLModerAlGmeiah" ErrorMessage="* مدير الجمعية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 50%">
                                            <h5>أمين المستودع : <span style="color: red">*</span>
                                            </h5>
                                            <asp:DropDownList ID="DLIDStorekeeper" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                                Width="100%">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                                ControlToValidate="DLIDStorekeeper" ErrorMessage="* أمين المخزن" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr runat="server" visible="false">
                                        <td style="width: 100%" colspan="2">
                                            <h5>الباحث الذي سيقوم بالتسليم : <span style="color: red">*</span>
                                            </h5>
                                            <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                                Width="100%">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator15" runat="server"
                                                ControlToValidate="DLAlBaheth" ErrorMessage="* حدد الباحث" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <h5>وذلك لغرض :
                                </h5>
                                <asp:TextBox ID="txtThePurpose" runat="server" class="form-control" ValidationGroup="g2" Text="-"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                    ControlToValidate="txtThePurpose" ErrorMessage="* إدخل الغرض" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <h5>مزيد من التفاصيل :
                                </h5>
                                <asp:TextBox ID="txtdescription" runat="server" class="form-control" ValidationGroup="g2" Text="لا يوجد"
                                    TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <br />
                                <br />
                                <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="margin-right: 4px;"
                                    class="btn btn-info btn-fill pull-left" ValidationGroup="g2" OnClick="btnAdd_Click" />
                                <asp:LinkButton ID="LBNew" runat="server" Style="margin-right: 4px;" OnClick="LBNew_Click"
                                    class="btn btn-danger btn-fill pull-left">الذهاب لتفاصيل الفاتورة</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="container-fluid" runat="server" visible="false" id="ProductByID">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-list"></i>قائمة شحنات المنتج
            </h3>
            <div style="float: left">
                <asp:LinkButton ID="LinkButton2" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="LinkButton2_Click"
                    title="تحديث"><li class="fa fa-refresh"></li></asp:LinkButton>
                <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                    title="طباعة">
            <li class="fa fa-print"></li></asp:LinkButton>
                <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click"
                    OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
            <li class="fa fa-trash-o"></li></span></asp:LinkButton>
            </div>
        </div>
        <div class="panel-body">
            <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft">
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
                                            <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                        </div>
                                    </div>
                                </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:GridView ID="GVProductShopWarehouseByID" runat="server" AutoGenerateColumns="False" DataKeyNames="_IDItem"
                                        Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVProductShopWarehouseByID_RowDataBound"
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
                                            <asp:TemplateField HeaderText="رقم الشحنة" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# Eval("_IDNumberProduct")%>
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
                                            <asp:TemplateField HeaderText="سعر الحبة" HeaderStyle-ForeColor="#CCCCCC">
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
                                            <asp:TemplateField HeaderText="تاريخ الإنتاج" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# ClassSaddam.FCheckNullDate((String) (ClassDataAccess.FChangeF((DateTime) (Eval("_ProductionDate")))))%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="تاريخ الإنتهاء" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# ClassSaddam.FCheckNullDate((String) (ClassDataAccess.FChangeF((DateTime) (Eval("_ExpiryDate")))))%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="تاريخ الشحن" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# ClassDataAccess.FChangeF((DateTime) (Eval("_DateCaming")))%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="مكان التخزين" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# Eval("StorageName")%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="من قبل" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# ClassQuaem.FAlBaheth((Int32) (Eval("_IDAdmin")))%>
                                                    <br />
                                                    <%# ClassQuaem.FAlBahethByEdit((Int32) (Eval("_IDUpdate")))%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="10px">
                                                <ItemTemplate>
                                                    <a href='PageManageProductShippingWarehouse.aspx?ID=<%# Eval("_IDUniq")%>' title="تعديل" data-toggle="tooltip"
                                                        class="btn btn-primary"><span class="fa fa-edit"></span></a>
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
                    </table>
                </div>
                <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                - <span style="font-size: 12px; padding-right: 5px">مجموع الشحنات : </span>
                <asp:Label ID="lblSum" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                - <span style="font-size: 12px; padding-right: 5px">السعر الكلي : </span>
                <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                <br />
                <div class="container-fluid" dir="rtl" runat="server">
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <div class="WidthMaglis" align="center" runat="server">
                                    أمين المستودع
                                            <br />
                                    <asp:Image ID="ImgIDStorekeeper" runat="server" Width='100px' Height='25' />
                                    <br />
                                    <asp:Label ID="lblIDStorekeeper" runat="server" Font-Size="11px"></asp:Label>
                                    <asp:DropDownList ID="DLIDStorekeeper2" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLIDStorekeeper2_SelectedIndexChanged"
                                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="WidthMaglis" align="center">
                                    مدير الجمعية
                                            <br />
                                    <asp:Image ID="ImgModer" runat="server" Width='100px' Height='25' />
                                    <br />
                                    <asp:Label ID="lblModerAlGmeiah" runat="server" Font-Size="11px"></asp:Label>
                                    <asp:DropDownList ID="DLModerAlGmeiah2" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLModerAlGmeiah2_SelectedIndexChanged"
                                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="WidthMaglis" align="center">
                                    المشرف المالي
                                            <br />
                                    <asp:Image ID="ImgAmeenAlSondoq" runat="server" Width='100px' Height='25' />
                                    <br />
                                    <asp:Label ID="lblAmeenAlSondoq" runat="server" Font-Size="11px"></asp:Label>
                                    <asp:DropDownList ID="DLAmeenAlSondoq2" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLAmeenAlSondoq2_SelectedIndexChanged"
                                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="WidthMaglis" align="center">
                                    رئيس مجلس الإدارة
                                            <br />
                                    <asp:Image ID="ImgRaeesMaglesAlEdarah" runat="server" Width='100px' Height='25' />
                                    <br />
                                    <asp:Label ID="lblRaeesMaglesAlEdarah" runat="server" Font-Size="11px"></asp:Label>
                                    <asp:DropDownList ID="DLRaeesMaglesAlEdarah3" runat="server" ValidationGroup="g2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="DLRaeesMaglesAlEdarah3_SelectedIndexChanged"
                                        CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="WidthMaglis" align="center">
                                    <div runat="server" id="IDKhatm" align="left" style="margin-top: 0px">
                                        <img src="/ImgSystem/ImgSignature/الختم.png" width="120" />
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlNull" runat="server" Visible="False">
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