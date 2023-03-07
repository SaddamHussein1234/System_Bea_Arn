<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelSetting/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAdmin.aspx.cs" Inherits="Cpanel_CPanelSetting_PageAdmin" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css?v=2.2" rel="stylesheet" type="text/css" />
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
    </script>

    <style type="text/css">
        .bl {
            color: #fff;
        }

        .fo {
            font-size: 12px;
        }

        @media screen and (min-width: 768px) {
            .WidthText2 {
                Width: 200px;
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
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageAdminAdd.aspx" data-toggle="tooltip" title="إضافة مستخم" class="btn btn-primary" runat="server" id="IDAdd"><i class="fa fa-plus"></i></a>
                    <asp:Button ID="btnActive" runat="server" Text="تفعيل المستخدم المحدد" title="تفعيل المستخدم المحدد" data-toggle="tooltip" CssClass="btn btn-info" OnClientClick="return ConfirmDelete();" OnClick="btnActive_Click" />
                    <asp:Button ID="btnUnActive" runat="server" Text="إيقاف المستخدم المحدد" title="إيقاف المستخدم المحدد" data-toggle="tooltip" CssClass="btn btn-danger" OnClientClick="return ConfirmDelete();" OnClick="btnUnActive_Click" />
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageAdmin.aspx">قائمة مستخدمين النظام </a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة مستخدمين النظام
                        </h3>                       
                        <div style="float: left">
                            <asp:Button ID="btnSearch" runat="server" Text="بحث" title="بحث" data-toggle="tooltip" CssClass="btn btn btn-info pull-right" OnClick="btnSearch_Click" />
                            &nbsp;
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText2" placeholder=" إبحث هنا ... "></asp:TextBox>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <div class="panel-body">
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
                        <asp:Panel ID="pnlData" runat="server" Visible="False">
                            <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                                <div class="row" style="margin: 5px; text-align: center">                                    
                                    <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                                        <br />
                                        <h3 style="font-family: 'Alwatan';">
                                            <asp:Label ID="txtTitle" runat="server" Text="قائمة مستخدمين النظام"></asp:Label>
                                        </h3>
                                        <br />
                                    </div>
                                </div>
                                <hr />
                                <div class="table table-responsive">
                                    <asp:GridView ID="GVAdmin" runat="server" AutoGenerateColumns="False" DataKeyNames="ID_Item"
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
                                            <asp:BoundField DataField="ID_Item" HeaderText="ID_Item" InsertVisible="False" ReadOnly="True" SortExpression="ID_Item" Visible="false" />
                                            <asp:BoundField DataField="NameGroup" HeaderText="المجموعة" SortExpression="NameGroup" HeaderStyle-ForeColor="#CCCCCC" />
                                            <asp:TemplateField HeaderText="الاسم" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# Eval("FirstName") %>
                                                    <%# FCheckManageF((bool) Eval("IsSuperAdmin")) %>
                                                    <%# FCheckOldMaglisF((bool) Eval("_IsOldMaglis")) %>
                                                    <%# FCheckIsBahethF(Convert.ToInt32(Eval("ID_Item")), Convert.ToString(Eval("IDUniq")), Convert.ToBoolean(Eval("IsBaheth"))) %>
                                                    <%# FCheckGeneral_AssmplyF(Convert.ToString(Eval("ID_Item")), Convert.ToBoolean(Eval("A1"))) %>
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
                                            <asp:TemplateField HeaderText="إستخدام النظام" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                                <ItemTemplate>
                                                    <%# ClassSaddam.FCheckActiveAdmin2((Boolean) Eval("IsAdmin")) %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="تاريخ الإضافة" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# ClassDataAccess.FChangeF((DateTime) Eval("DateReg")) %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="15px">
                                                <ItemTemplate>
                                                    <a href='PageAdminEdit.aspx?ID=<%# Eval("IDUniq")%>' title="تعديل" data-toggle="tooltip" class="btn btn-info">
                                                        <span class="fa fa-edit"></span>
                                                    </a>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
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
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />
</asp:Content>

