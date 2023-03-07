<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelAttach/MPCPanel.master" AutoEventWireup="true" CodeFile="PageMessageGroupAdd.aspx.cs" Inherits="Cpanel_CPanelAttach_PageMessageGroupAdd" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
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
        function ConfirmAdmin() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVAdmin.ClientID%>");
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

        function ConfirmMostafeed() {
            var count = document.getElementById("<%=hfCountMostafees.ClientID %>").value;
            var gv = document.getElementById("<%=GVMostafeedByDakhl.ClientID%>");
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

    <link href="../GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <link href="../test/LoginAr.css" rel="stylesheet" />
    <link href="../css/chosen.css" rel="stylesheet" />
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" class="btn btn-default" OnClick="LBBack_Click"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageMessageGroupAdd.aspx">إضافة رسالة جماعية</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title"><i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة رسالة SMS جماعية"></asp:Label>
                    </h3>
                    <div style="float: left">
                        <asp:RadioButtonList ID="RBLFilter" runat="server" RepeatDirection="Horizontal"
                            CssClass="left" AutoPostBack="true" OnSelectedIndexChanged="RBLFilter_SelectedIndexChanged">
                            <%--<asp:ListItem Value="Other" Selected="True">رقم آخر</asp:ListItem>--%>
                            <asp:ListItem Value="Admin">مستخدمين النظام</asp:ListItem>
                            <asp:ListItem Value="Mostafeed">المستفيدين</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="col-sm-12">
                    <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                        <span class="badge badge-pill badge-warning">تحذير</span>
                        <asp:Label ID="lblWarning" runat="server"></asp:Label>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                </div>
                <div class="col-sm-12">
                    <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible" role="alert">
                        <span class="badge badge-pill badge-success">عملية ناجحة</span>
                        <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <h5>مزود الخدمة : </h5>
                                        <asp:DropDownList ID="DLURL" runat="server" Width="100%" ValidationGroup="g2"
                                            CssClass="form-control chzn-select dropdown">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="DLSenderName" ErrorMessage="* مزود الخدمة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <h5>إسم المرسل : </h5>
                                        <asp:DropDownList ID="DLSenderName" runat="server" Width="100%" ValidationGroup="g2" Style="direction: ltr;"
                                            CssClass="form-control chzn-select dropdown">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="DLSenderName" ErrorMessage="* إسم المرسل" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid">
                                <div class="col-lg-10">
                                    <div class="form-group">
                                        <h5>نص الرسالة : </h5>
                                        <asp:TextBox ID="txt_Message" runat="server" class="form-control" ValidationGroup="g2" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txt_Message" ErrorMessage="* نص الرسالة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>نوع الرسالة : </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                <input runat="server" name="RememberMe" id="RArabic" type="radio" value="عربي" />
                                                <span class="slider round"></span>
                                                <span class="keepme">عربي </span>
                                            </label>
                                            <br />
                                            <label class="switch">
                                                <input runat="server" name="RememberMe" type="radio" id="REnglish" value="إنجليزي" />
                                                <span class="slider round"></span>
                                                <span class="keepme">إنجليزي </span>
                                            </label>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" runat="server" visible="false" id="IDAdmin">
                                <div class="col-md-12" align="center">
                                    <asp:RadioButtonList ID="rblCheck" runat="server" CssClass="checkbox-inline"
                                        RepeatDirection="Horizontal" AutoPostBack="True"
                                        OnSelectedIndexChanged="rblCheck_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text=" جميع المستخدمين " Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text=" أعضاء مجلس الإدارة "></asp:ListItem>
                                        <asp:ListItem Value="4" Text=" أعضاء الجمعية العمومية "></asp:ListItem>
                                        <asp:ListItem Value="2" Text=" الباحثين "></asp:ListItem>
                                        <asp:ListItem Value="3" Text=" المستخدمين "></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-lg-12">
                                    <asp:Panel ID="pnlData" runat="server" Visible="False">
                                        <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                                            <div class="row" style="margin: 5px; text-align: center">
                                                <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                                                    <br />
                                                    <h3 style="font-family: 'Alwatan';">
                                                        <asp:Label ID="txtTitle" runat="server" Text="قائمة مستخدمين النظام"></asp:Label>
                                                    </h3>
                                                </div>
                                            </div>
                                            <div class="table table-responsive">
                                                <asp:GridView ID="GVAdmin" runat="server" AutoGenerateColumns="False" DataKeyNames="PhoneNumber"
                                                    Width="100%" CssClass="footable1"
                                                    EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="10px">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الاسم" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("FirstName") %>
                                                                <%# FCheckManageF((bool) Eval("IsSuperAdmin")) %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="المسمى الوظيفي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("CommentAdmin") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="رقم الهاتف" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("PhoneNumber") %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="حالة التفعيل" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# ClassSaddam.FCheckActiveAdmin2((Boolean) Eval("IsBlock")) %>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                    <PagerSettings Mode="NextPrevious" Position="TopAndBottom" NextPageText=" -- التالي " PreviousPageText=" السابق - " />
                                                    <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" />
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                    <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                    <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                    <SortedDescendingHeaderStyle BackColor="#242121" />
                                                </asp:GridView>
                                            </div>
                                            <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                            <span style="font-size: 12px; padding-right: 5px">عدد المستخدمين : </span>
                                            <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                            <span class="fa fa-table"></span>
                                        </asp:Panel>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlNull" runat="server" Visible="False">
                                        <br />
                                        <br />
                                        <br />
                                        <div align="center">
                                            <h3 style="font-size: 20px">لا توجد نتائج
                                            </h3>
                                        </div>
                                        <br />
                                        <br />
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="container-fluid" runat="server" visible="false" id="IDMostafeed">
                                <asp:Panel ID="pnlFilter" runat="server">
                                    <div class="panel-body">
                                        <asp:Panel ID="pnlHideFilter" runat="server">
                                            <div class="col-lg-2">
                                                <div class="form-group">
                                                    نوع المستفيد : 
                                                        <asp:DropDownList ID="DLTypeMostafeed" runat="server" ValidationGroup="g2" CssClass="dropdown" Enabled="false"
                                                            Height="34" Width="100%">
                                                            <asp:ListItem></asp:ListItem>
                                                            <asp:ListItem Value="دائم" Selected="True">دائم</asp:ListItem>
                                                            <asp:ListItem Value="مستبعد">مستبعد</asp:ListItem>
                                                        </asp:DropDownList>
                                                    <hr />
                                                    الراتب : 
                                                    <asp:TextBox ID="txtMasderAlDkhalMinimum" runat="server" class="form-control2" Text="0" ValidationGroup="g2" Width="100%" Height="30" placeholder="الحد الادنى .." TextMode="Number" Style="text-align: center; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                                        ControlToValidate="txtMasderAlDkhalMinimum" ErrorMessage="* إدخل قيمة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <br />
                                                    <i class="fa fa-minus"></i>
                                                    <br />
                                                    <asp:TextBox ID="txtMasderAlDkhalMaxiMam" runat="server" class="form-control2" Text="20000" ValidationGroup="g2" Width="100%" Height="30" placeholder="الحد الاعلى .." TextMode="Number" Style="text-align: center; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                                        ControlToValidate="txtMasderAlDkhalMaxiMam" ErrorMessage="* إدخل قيمة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <hr />
                                                    <asp:Button ID="btnGetByAlMasder" runat="server" Text="عرض المستفيدين" Style="margin-right: 4px;"
                                                        CssClass="btn btn-info btn-fill" OnClick="btnGetByAlMasder_Click" />
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <h5><i class="fa fa-star"></i>حسب القرية : </h5>
                                                    <div class="checkbox checkbox-primary">
                                                        <asp:CheckBoxList ID="CBAlQariah" runat="server"
                                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                            <asp:ListItem></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <h5><i class="fa fa-star"></i>حسب مصدر الدخل : </h5>
                                                    <div class="checkbox checkbox-primary">
                                                        <asp:CheckBoxList ID="CBLMasderAlDakhl" runat="server"
                                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                            <asp:ListItem></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <h5><i class="fa fa-star"></i>حسب حالات الاسر : </h5>
                                                    <div class="checkbox checkbox-primary">
                                                        <asp:CheckBoxList ID="CBFamliyCases" runat="server"
                                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                            <asp:ListItem></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <h5><i class="fa fa-star"></i>حسب نوع المسكن : </h5>
                                                    <div class="checkbox checkbox-primary">
                                                        <asp:CheckBoxList ID="CBAccommodationType" runat="server"
                                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                            <asp:ListItem></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <h5><i class="fa fa-star"></i>حسب حالة المسكن : </h5>
                                                    <div class="checkbox checkbox-primary">
                                                        <asp:CheckBoxList ID="CBHousingStatus" runat="server"
                                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                            <asp:ListItem></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr />
                                            <div class="col-md-12" runat="server" visible="false">
                                                <div class="form-group">
                                                    <h5><i class="fa fa-star"></i>عرض المستفيدين : </h5>
                                                    <div class="checkbox checkbox-primary">
                                                        <asp:CheckBoxList ID="CMMostafeed" runat="server"
                                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                            <asp:ListItem></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr />
                                        </asp:Panel>
                                        <div class="col-md-12" runat="server" visible="false">
                                            <div class="form-group">
                                                <h5><i class="fa fa-star"></i>فلترة العرض : </h5>
                                                <asp:CheckBox ID="CB3" runat="server" Text="رقم الملف" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB4" runat="server" Text="الإسم" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB6" runat="server" Text="حالات الاسر" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB7" runat="server" Text="القرية" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB8" runat="server" Text="رقم السجل" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB9" runat="server" Text="الجوال" Font-Size="12px" Checked="true" />
                                                <asp:CheckBox ID="CB10" runat="server" Text="تاريخ الميلاد" Font-Size="12px" Checked="false" />
                                                <asp:CheckBox ID="CB11" runat="server" Text="العمر" Font-Size="12px" Checked="false" />
                                                <asp:CheckBox ID="CB12" runat="server" Text="حالة المسكن" Font-Size="12px" Checked="false" />
                                                <asp:CheckBox ID="CB13" runat="server" Text="الحالة الصحية" Font-Size="12px" Checked="false" />
                                                <asp:CheckBox ID="CB14" runat="server" Text="أفراد الاسرة" Font-Size="12px" Checked="false" />
                                                <asp:CheckBox ID="CB15" runat="server" Text="الراتب" Font-Size="12px" Checked="false" />
                                            </div>
                                        </div>
                                        <hr />

                                        <div class="form-group">
                                            <asp:TextBox Visible="false" ID="txtSearchByFilter" runat="server" Style="direction: ltr" placeholder="Filter The Search ... "
                                                CssClass="form-control" TextMode="MultiLine" Rows="6" Width="100%" type="password"></asp:TextBox>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlPrintAllData" runat="server" Direction="RightToLeft" Visible="False">
                                    <div class="table table-responsive">
                                        <table class='table' style="width: 100%">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        <div align="center" class="col-lg-11">
                                                            <div>
                                                                <asp:TextBox ID="txtSearchMostafeed" runat="server" class="form-control" placeholder="عنوان البحث" Text="قائمة بيانات المستفيدين" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-1 HideThis">
                                                            <asp:LinkButton ID="LBGetFilter" runat="server" OnClick="LBGetFilter_Click" data-toggle="tooltip" title="جلب قائمة الفلترة"> <i class="fa fa-refresh"></i> </asp:LinkButton>
                                                        </div>
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:GridView ID="GVMostafeedByDakhl" runat="server" AutoGenerateColumns="False" DataKeyNames="PhoneNumber"
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
                                                                <asp:BoundField DataField="IDItem" HeaderText="IDItem" InsertVisible="False" ReadOnly="True"
                                                                    SortExpression="IDItem" Visible="false" />
                                                                <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                                    <ItemTemplate>
                                                                        <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="NumberMostafeed" HeaderText="الملف" SortExpression="NumberMostafeed" Visible="false"
                                                                    HeaderStyle-ForeColor="#CCCCCC" />
                                                                <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <span style="font-size: 11px"><%# Eval("NameMostafeed")%></span>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="المستفيد" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# ClassMosTafeed.FChangColor((string) Eval("TypeMostafeed"))%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="الحالة" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# ClassQuaem.FHalatMostafeed((Int32) Eval("HalafAlMosTafeed"))%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="القرية" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# ClassQuaem.FAlQarabah((Int32) Eval("AlQaryah"))%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="رقم السجل" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# Eval("NumberAlSegelAlMadany")%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="الجوال" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        0<%# Eval("PhoneNumber")%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="تاريخ الميلاد" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# ClassSaddam.FCheckNullDateMostafeed((String) (ClassDataAccess.FChangeF((DateTime) Eval("dateBrith"))))%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="العمر" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# ClassSaddam.FCheckNullDateMostafeedAge((DateTime) (Eval("dateBrith"))) %>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="حالة المسكن" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# ClassQuaem.FHalatAlMaskan((Int32) Eval("HaletAlMasken"))%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="الحالة الصحية" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# ClassSaddam.FCheckHealthStatus(Convert.ToBoolean(Eval("Saleem")), Convert.ToBoolean(Eval("Moalek")), Convert.ToString(Eval("TypeAleakah")), Convert.ToBoolean(Eval("Mareedh")), Convert.ToString(Eval("TypeAlmaradh"))) %>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="الافراد" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# Eval("AfradAlOsrah")%>  فرد
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="الراتب" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <%# Eval("AlDakhlAlShahryllMostafeed")%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                                    <ItemTemplate>
                                                                        <a href='PageBeneficiaryByView.aspx?ID=<%# Eval("NumberMostafeed")%>&XID=<%# Eval("IDUniq")%>' title="عرض الملف" data-toggle="tooltip"><span class="fa fa-eye"></span></a>
                                                                        <a href='PageBeneficiaryEdit.aspx?XID=<%# Eval("IDUniq")%>' title="تعديل" data-toggle="tooltip"><span class="fa fa-edit"></span></a>
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                            <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                            <PagerSettings Mode="NextPrevious" Position="TopAndBottom" NextPageText=" -- التالي "
                                                                PreviousPageText=" السابق - " />
                                                            <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" />
                                                            <RowStyle CssClass="rows"></RowStyle>
                                                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                            <%--<SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                                <SortedDescendingHeaderStyle BackColor="#242121" />--%>
                                                        </asp:GridView>
                                                        <asp:HiddenField ID="hfCountMostafees" runat="server" Value="0" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                            <tfoot>
                                                <tr>
                                                    <th>
                                                        <hr style='border: solid; border-width: 1px; width: 100%' />
                                                        <div style="float: right">
                                                            <span style="font-size: 12px; padding-right: 5px">عدد الأسر : </span>
                                                            <asp:Label ID="lblCountAll" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                        </div>
                                                        <div style="float: left; display: none">
                                                            <span style="font-size: 12px; padding-right: 5px">عدد القرى : </span>
                                                            <asp:Label ID="lblCountQriah" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                        </div>
                                                        <hr style='border: solid; border-width: 1px; width: 100%' />
                                                    </th>
                                                </tr>
                                            </tfoot>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="Panel1" runat="server" Visible="False">
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
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </asp:Panel>
                                <asp:Panel ID="pnlWaiting" runat="server" Visible="False">
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
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </asp:Panel>
                            </div>
                            <div class="container-fluid">
                                <br />
                                <div style="float: left">
                                    <asp:Button ID="btnAdd" runat="server" Text="إرسال الرسائل للمستخدمين" OnClick="btnAdd_Click" Style="margin-left: 5px"
                                        Visible="false"
                                        OnClientClick="return ConfirmAdmin();" CssClass="btn btn-info btn-fill pull-left" ValidationGroup="g2" />
                                    <asp:Button ID="btnAddMostafeed" runat="server" Text="إرسال الرسائل للمستفيدين" OnClick="btnAddMostafeed_Click" Style="margin-left: 5px"
                                        Visible="false"
                                        OnClientClick="return ConfirmMostafeed();" CssClass="btn btn-info btn-fill pull-left" ValidationGroup="g2" />
                                    <asp:LinkButton ID="LBBack" runat="server" OnClick="LBBack_Click"
                                        CssClass="btn btn-danger btn-fill pull-left">رجوع</asp:LinkButton>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
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
        <script type="text/javascript"><!--
    $('#language a:first').tab('show');
    $('#option a:first').tab('show');
    //--></script>
</asp:Content>

