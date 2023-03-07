<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/CRM/CRM_Main.master" AutoEventWireup="true" CodeFile="PageCompanyType.aspx.cs" Inherits="Cpanel_ERP_CRM_PageSetting_PageCompanyType" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
            var gv = document.getElementById("<%=GVCompanyType.ClientID%>");
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
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageCompanyTypeAdd.aspx" data-toggle="tooltip" title="إضافة إدارة جديدة" class="btn btn-info" runat="server" id="IDAdd"><i class="fa fa-plus"></i></a>
                    <asp:Button ID="btnDelete" runat="server" Text="حذف الملفات المحددة" title="حذف الملفات المحددة" 
                        data-toggle="tooltip" CssClass="btn btn-danger" OnClientClick="return ConfirmDelete();" OnClick="btnDelete_Click" />
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="/Default.aspx">الرئيسية</a></li>
                        <li><a href="PageCompanyType.aspx">قائمة أنواع شركات الدعم </a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة أنواع شركات الدعم
                        </h3>
                        <div style="float: left">
                            <asp:Button ID="btnSearch" runat="server" Text="بحث" title="بحث" data-toggle="tooltip" CssClass="btn btn btn-info pull-right" OnClick="btnSearch_Click" />
                            &nbsp;
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText2" placeholder=" إبحث هنا ... "></asp:TextBox>
                        </div>
                    </div>
                    <div class="panel-body">
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
                        <asp:Panel ID="pnlData" runat="server" Visible="False">
                            <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                                <div class="row" style="margin: 5px; text-align: center">
                                    <div class="col-md-12" style="border: solid; border-width: 3px; border-color: #006011; border-radius: 5px">
                                        <br />
                                        <h3 style="font-family: 'Alwatan';">
                                            قائمة أنواع شركات الدعم
                                        </h3>
                                        <br />
                                    </div>
                                </div>
                                <div class="table table-responsive">
                                    <asp:GridView ID="GVCompanyType" runat="server" AutoGenerateColumns="False" DataKeyNames="_ID_Item_"
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
                                            <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="_Company_Type_" HeaderText="عنوان الإدارة" SortExpression="_Company_Type_" HeaderStyle-ForeColor="#CCCCCC" />
                                            <asp:BoundField DataField="CreatedDate" HeaderText="تاريخ الإضافة" SortExpression="CreatedDate" HeaderStyle-ForeColor="#CCCCCC" />
                                            <asp:TemplateField HeaderText="حالة التفعيل" HeaderStyle-ForeColor="#CCCCCC">
                                                <ItemTemplate>
                                                    الحالة <%# ClassSaddam.FChangeStyleCheckbox((Boolean) Eval("Is_Active_")) %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderText="أُضيف من قبل">
                                                <ItemTemplate>
                                                    <%# ClassQuaem.FAlBaheth((Int32) (Eval("CreatedBy")))%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16px">
                                                <ItemTemplate>
                                                    <a href="PageCompanyTypeAdd.aspx?ID=<%# Eval("_ID_Item_") %>" data-toggle="tooltip" title="تعديل البيانات">
                                                        <i class="fa fa-edit"></i>
                                                    </a>
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
                                <span style="font-size: 12px; padding-right: 5px">العدد : </span>
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
        </div>
        <br />
        <br />
        <br />
        <br />
        <br />


</asp:Content>

