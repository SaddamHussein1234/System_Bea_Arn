<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageSite/MPCPanel.master" AutoEventWireup="true" CodeFile="PageMassageVisit.aspx.cs" Inherits="Cpanel_CPanelManageSite_PageMassageVisit" %>
<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../GridView.css?v=2.2" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnDelete.ClientID %>").disabled = true;
            document.getElementById("<%=btnSearch.ClientID %>").disabled = true;
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
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=RPTMessage.ClientID%>");
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
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <li class="fa fa-refresh"></li></asp:LinkButton>
                    <asp:Button ID="btnDelete" runat="server" Text="حذف السجلات المحددة" title="حذف السجلات المحددة" data-toggle="tooltip" CssClass="btn btn-danger" OnClientClick="return ConfirmDelete();" OnClick="btnDelete_Click" />
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageMassageVisit.aspx">قائمة رسائل الزوار </a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة رسائل الزوار
                        </h3>
                        <div style="float: left">
                            <asp:Button ID="btnSearch" runat="server" Text="بحث" title="بحث" data-toggle="tooltip" CssClass="btn btn btn-info pull-right" OnClick="btnSearch_Click" />
                            &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText2" placeholder=" إبحث هنا ... "></asp:TextBox>
                        </div>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="pnlData" runat="server" Visible="False">
                            <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                                <div align="center" class="w">
                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center"></asp:TextBox>
                                </div>
                                <hr />
                                <div class="table table-responsive">
                                    <asp:GridView ID="RPTMessage" runat="server" AutoGenerateColumns="False" DataKeyNames="IDCont"
                                        Width="100%" CssClass="footable1" AllowPaging="true" OnPageIndexChanging="RPTMessage_PageIndexChanging" PageSize="1000"
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
                                            <asp:BoundField DataField="__ID_Message" HeaderText="__ID_Message" InsertVisible="False"
                                                ReadOnly="True" SortExpression="__ID_Journey" Visible="False" />
                                            <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="الغرض" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <a href='PageMessageVisitView.aspx?ID=<%# Eval("IDCont")%>&Name=<%# Eval("TitleMeassge")%>' data-tooltip="عرض" style="font-size: 12px">
                                                        <span class="fa fa-file"></span>
                                                        <%# Eval("TypeMessage")%><br />
                                                        <%# FCheckNewF((bool) Eval("ViewRead")) %>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="عنوان الرسالة" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <a href='PageMessageVisitView.aspx?ID=<%# Eval("IDCont")%>&Name=<%# Eval("TitleMeassge")%>' title="عرض" class="tip-bottom" style="font-size: 12px">
                                                        <span class="fa fa-file"></span>
                                                        <%# Eval("TitleMeassge")%>
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="مرسلة من" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <a href='PageMessageVisitView.aspx?ID=<%# Eval("IDCont")%>&Name=<%# Eval("TitleMeassge")%>' title="عرض" class="tip-bottom" style="font-size: 12px">
                                                        <span class="fa fa-user"></span>
                                                        <%# Eval("NameUser")%><br />
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="البلد" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <a href='PageMessageVisitView.aspx?ID=<%# Eval("IDCont")%>&Name=<%# Eval("TitleMeassge")%>' title="عرض" class="tip-bottom" style="font-size: 12px">
                                                        <span class="fa fa-location-arrow"></span>
                                                        <%# Eval("CountryUser")%><br />
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="رقم الهاتف" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <a href='PageMessageVisitView.aspx?ID=<%# Eval("IDCont")%>&Name=<%# Eval("TitleMeassge")%>' title="عرض" class="tip-bottom" style="font-size: 12px">
                                                        <span class="fa fa-phone"></span>
                                                        <%# Eval("PhoneUser")%><br />
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="البريد الالكتروني" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <a href='PageMessageVisitView.aspx?ID=<%# Eval("IDCont")%>&Name=<%# Eval("TitleMeassge")%>' title="عرض" class="tip-bottom" style="font-size: 12px">
                                                        <span class="fa fa-envelope"></span>
                                                        <%# Eval("EmailUser")%><br />
                                                    </a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="تاريخ الرسالة" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <span class="fa fa-calendar"></span>
                                                    <%# Eval("DateSend", "{0:dd/MM/yyyy}") + " " + Eval("DateSend", "{0:HH:mm tt}") %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                        <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" -- التالي "
                                            PreviousPageText=" السابق - " PageButtonCount="11" />
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
                                <span style="font-size: 12px; padding-right: 5px">عدد السجلات : </span>
                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                <span class="fa fa-table"></span>
                            </asp:Panel>
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

