<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/MPCPanel.master" AutoEventWireup="true" CodeFile="PageConvertedCasesByModer.aspx.cs" Inherits="Cpanel_PageConvertedCasesByModer" %>

<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="GridView.css?v=2.2" rel="stylesheet" type="text/css" />
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
            var gv = document.getElementById("<%=GVConvertedCases.ClientID%>");
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
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageConvertedCasesAdd.aspx" runat="server" id="IDAdd" data-toggle="tooltip" title="إضافة طلب جديد" class="btn btn-primary"><i class="fa fa-plus"></i></a>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click"
                        OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
                    <asp:Button ID="btnAllow" runat="server" Text="الموافقة على السجلات المحددة" class="btn btn-info" OnClientClick="return ConfirmDelete();" OnClick="btnAllow_Click" />
                    <asp:Button ID="btnNotAllow" runat="server" Text="عدم الموافقة على السجلات المحددة" class="btn btn-danger" OnClientClick="return ConfirmDelete();" OnClick="btnNotAllow_Click" />
                    السبب  
                        <asp:TextBox ID="txtNotAllow" runat="server" class="form-control2" ValidationGroup="g2" Width="200px"></asp:TextBox>
                    <asp:Label ID="lblNotAllow" runat="server" Text="*" Visible="false" ForeColor="Red"></asp:Label>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageConvertedCasesByModer.aspx">قائمة طلبات التحويل</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة طلبات التحويل التي تحتاج إلى موافقة المدير
                        </h3>
                        <div style="float: left">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText2" placeholder=" إبحث هنا ... "></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="بحث" class="btn btn-info" OnClick="btnSearch_Click" title="بحث" data-toggle="tooltip" />
                        </div>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft" Visible="False">
                            <div class="ColorBackground">
                                <div align="center" class="w">
                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="قائمة طلبات التحويل التي تحتاج إلى موافقة المدير" placeholder="عنوان البحث" Style="text-align: center; width: 100%"></asp:TextBox>
                                    <span class="hr"></span>
                                </div>
                            </div>
                            <div class="table table-responsive">
                                <asp:GridView ID="GVConvertedCases" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
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
                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ر/الطلب" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <%# Eval("NumberOrder")%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <%# Eval("NameMostafeed")%>
                                                <br />
                                                رقم المستفيد <%# Eval("NumberMostafeed")%> / <%# ClassSaddam.FCheckActiveF((bool) (Eval("IsAllowModer"))) %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="القرية" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <%# ClassQuaem.FAlQarabah((Int32) Eval("AlQaryah"))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="الحالة السابقة" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <%# ClassQuaem.FHalatMostafeed((Int32) Eval("HalatAlmostafeedBefor"))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="الحالة الجديدة" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <%# ClassQuaem.FHalatMostafeed((Int32) Eval("HalatAlmostafeedAfter"))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="تاريخ الطلب" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <%# ClassDataAccess.FChangeF((DateTime) (Eval("DateOrder")))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="من قبل" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <%# ClassQuaem.FAlBaheth((Int32) Eval("IDAdmin"))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                            <ItemTemplate>
                                                <a href='PageConvertedCasesDetails.aspx?ID=<%# Eval("NumberOrder")%>&XID=<%# Eval("IDUniq")%>' title="عرض الملف" data-toggle="tooltip"
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
                            </div>
                            <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                            <span style="font-size: 12px; padding-right: 5px">عدد السجلات : </span>
                            <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
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
        </div>
        <br />
        <br />
        <br />
</asp:Content>

