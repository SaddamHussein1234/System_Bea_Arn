<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/WSM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAll.aspx.cs" Inherits="Cpanel_ERP_WSM_PageStoragePlaces_PageAll" %>
<%@ Import Namespace="Library_CLS_Arn.ERP.DataAccess" %>
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
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
                var gv = document.getElementById("<%=GVStoragePlacesAll.ClientID%>");
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
                    return confirm(" هل أنت متأكد من الحذف ؟");
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
                Width: 31%;
                padding-right: 5px;
            }

            .WidthText4 {
                float: right;
                Width: 50%;
            }
            .WidthText5 {
                float: right;
                Width: 100%;
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

            .WidthText5 {
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
            <a href="" data-toggle="tooltip" title="إضافة مسمى جديد" class="btn btn-primary" runat="server" id="IDAdd"><i class="fa fa-plus"></i></a>
            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                title="تحديث" OnClick="btnRefrish_Click">

                    <i class="fa fa-refresh"></i></asp:LinkButton>
            <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger" OnClick="btnDelete1_Click"
                OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
        </div>
        <div class="container-fluid">
            <h1>لوحة التحكم</h1>
            <ul class="breadcrumb">
                <li><a href="Default.aspx">الرئيسية</a></li>
                <li><a href="PageManageStoragePlaces.aspx">قائمة مسميات التخزين في المستودع</a></li>
            </ul>
        </div>
    </div>
    <div class="container-fluid" runat="server" id="Add">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i class="fa fa-pencil"></i>
                    <asp:Label ID="lbmsg" runat="server" Text="إضافة مسمى للمستودع"></asp:Label>
                </h3>
            </div>
            <div class="panel-body">
                <div class="content-box-large">
                    <div class="widget-box">
                        <div class="container-fluid" dir="rtl">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <h5>عنوان المسمى : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtName" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                    <asp:Label ID="lblCheckName" runat="server" ForeColor="Red" Font-Size="10px"></asp:Label>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="IDCheckName" runat="server"
                                                ControlToValidate="txtName" ErrorMessage="* عنوان المسمى" ForeColor="#FF0066" 
                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2"
                                                 Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <h5>حالة التفعيل :
                                    </h5>
                                    <asp:CheckBox ID="CBActive" runat="server" Font-Size="14px" ValidationGroup="g2" Checked="true" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <h5>ملاحظة :
                                    </h5>
                                    <asp:TextBox ID="txtNote" runat="server" class="form-control" ValidationGroup="g2" TextMode="MultiLine" Text="لا يوجد"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <h5>تاريخ الإضافة :
                                    </h5>
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <br />
                                    <br />
                                    <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="margin-right: 4px;" ValidationGroup="g2"
                                        class="btn btn-info btn-fill pull-left" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid" runat="server" id="View">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i class="fa fa-list"></i>قائمة مسميات التخزين في المستودع
                </h3>
                <div style="float: left">
                    <%--<asp:LinkButton ID="btnSearch" runat="server" data-toggle="tooltip" title="بحث" OnClick="btnSearch_Click"
                                class="btn btn-info pull-right"><span class="tip-bottom"><i class="fa fa-search" style="font-size:16px"></i></span></asp:LinkButton>--%>
                    <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;"
                        class="btn btn-info btn-fill pull-right" OnClick="btnSearch_Click" />
                    &nbsp;
                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText2" placeholder=" إبحث هنا ... "></asp:TextBox>
                </div>
            </div>
            <div class="panel-body">
                <asp:Panel ID="pnlData" runat="server">
                    <asp:GridView ID="GVStoragePlacesAll" runat="server" AutoGenerateColumns="False" DataKeyNames="IDItem"
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
                                SortExpression="CategoryID" Visible="false" />
                            <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                <ItemTemplate>
                                    <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="عنوان المسمى" HeaderStyle-ForeColor="#CCCCCC">
                                <ItemTemplate>
                                    <%# Eval("StorageName")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="حالة التفعيل" HeaderStyle-ForeColor="#CCCCCC">
                                <ItemTemplate>
                                    <%# ClassSaddam.FChangeStyleCheckbox((Boolean) (Eval("IsActive")))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:CheckBoxField DataField="IsCheck" HeaderText="حالة الحجز" SortExpression="IsCheck" Visible="false"
                                HeaderStyle-ForeColor="#CCCCCC" />
                            <asp:TemplateField HeaderText="أُضيف من قبل" HeaderStyle-ForeColor="#CCCCCC">
                                <ItemTemplate>
                                    <%# ClassQuaem.FAlBaheth((Int32) (Eval("IDAdmin")))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="تاريخ الإضافة" HeaderStyle-ForeColor="#CCCCCC">
                                <ItemTemplate>
                                    <%# ClassDataAccess.FChangeF((DateTime) (Eval("DateAddStorage")))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="10px">
                                <ItemTemplate>
                                    <a href='PageAll.aspx?ID=<%# Eval("IDItem")%>' title="تعديل" data-toggle="tooltip"><span class="fa fa-edit"></span></a>
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

</asp:Content>

