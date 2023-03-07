<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageSite/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAlbum.aspx.cs" Inherits="Cpanel_CPanelManageSite_PageAlbum" %>
<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            var gv = document.getElementById("<%=GVAlbum.ClientID%>");
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
    </style>
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageAlbumAdd.aspx" data-toggle="tooltip" title="إضافة البوم جديد" class="btn btn-primary" id="IDAdd" runat="server"><i class="fa fa-plus"></i></a>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <li class="fa fa-refresh"></li></asp:LinkButton>
                    <asp:Button ID="btnDelete" runat="server" Text="حذف السجلات المحددة" title="حذف السجلات المحددة" data-toggle="tooltip" CssClass="btn btn-danger" OnClientClick="return ConfirmDelete();" OnClick="btnDelete_Click" />
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageAlbum.aspx">البوم الصور </a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>البوم الصور
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
                                    <asp:GridView ID="GVAlbum" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
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
                                            <asp:BoundField DataField="IDItem" HeaderText="IDItem" InsertVisible="False" ReadOnly="True" SortExpression="IDItem" Visible="false" />
                                            <asp:BoundField DataField="TitleAlbumAr" HeaderText="عنوان عربي" SortExpression="TitleAlbumAr" HeaderStyle-ForeColor="#CCCCCC" />
                                            <asp:BoundField DataField="TitleAlbumTr" HeaderText="عنوان تركي" SortExpression="TitleAlbumTr" HeaderStyle-ForeColor="#CCCCCC" Visible="false" />
                                            <asp:BoundField DataField="TitleAlbumEn" HeaderText="عنوان إنجليزي" SortExpression="TitleAlbumEn" HeaderStyle-ForeColor="#CCCCCC" Visible="false" />
                                            <asp:BoundField DataField="IsOrder" HeaderText="ترتيب" SortExpression="IsOrder" HeaderStyle-ForeColor="#CCCCCC" />
                                            <asp:TemplateField HeaderText="حالة العرض" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                            <%# ClassSaddam.FChangeStyleCheckbox2((Boolean) (Eval("IsViewAr")))%>
                                                    <%--تركي
                                                            <asp:CheckBox ID="CBCenter" runat="server"
                                                                Checked='<%# Eval("IsViewTr")%>' Enabled="false" />
                                                    إنجليزي
                                                            <asp:CheckBox ID="CBRigth" runat="server"
                                                                Checked='<%# Eval("IsViewEn")%>' Enabled="false" />--%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="تاريخ الإضافة" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# ClassDataAccess.FChangeF((DateTime) Eval("DateAddAlbum")) %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="عدد الصور" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <a href='PageAlbumImgAdd.aspx?ID=<%# Eval("RandomUniq")%>&IDX<%# Eval("IDItem")%>' title="إضافة صور للإلبوم" class="tip-bottom" style="font-size: 12px">
                                                        <span class="fa fa-picture-o"></span> <%# FCountImg((Int32) Eval("IDItem")) %>
                                                    </a>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="نشر من قبل" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <%# ClassSetting.FGetNameAdmin((Int32) Eval("IDAdmin")) %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="15px">
                                                <ItemTemplate>
                                                    <a href='PageAlbumEdit.aspx?ID=<%# Eval("RandomUniq")%>' title="تعديل" class="tip-bottom">
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
                                    <span style="font-size: 12px; padding-right: 5px">عدد السجلات : </span>
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

