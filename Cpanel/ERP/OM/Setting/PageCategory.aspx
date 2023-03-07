<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/OM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageCategory.aspx.cs" Inherits="Cpanel_ERP_OM_Setting_PageCategory" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    //-->
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
        var gv = document.getElementById("<%=GVCategory.ClientID%>");
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
        function showModal() {
            $("#IDModel2").modal('show');
        }

        $(function () {
            $("#btnShow").click(function () {
                showModal();
            });
        });
    </script>

    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href='javaScript:void(0)' data-toggle="modal" data-target="#IDModel3" data-tooltip="إضافة جديد" class="btn btn-info" runat="server" id="IDAdd"><i class="fa fa-plus"></i></a>
                    <asp:Button ID="btnDelete" runat="server" Text="حذف الملفات المحددة" data-tooltip="حذف الملفات المحددة" Visible="false"
                        CssClass="btn btn-danger" OnClientClick="return ConfirmDelete();" OnClick="btnDelete_Click" />

                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default"
                        data-tooltip="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="../Default.aspx">الرئيسية</a></li>
                        <li>قائمة التصنيف</li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>
                            <asp:Label ID="lblCustomer_Case2" runat="server" Text="قائمة التصنيف"></asp:Label>
                        </h3>
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
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder=" إبحث هنا ... "></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                                <asp:Button ID="btnGet" runat="server" Text="بحث" Style="margin-right: 4px;" OnClick="btnGet_Click"
                                    class="btn btn-info btn-fill " ValidationGroup="gg2" />
                            </div>
                            <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                                <div class="table table-responsive" id="pnlPrint" runat="server" dir="rtl">
                                    <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                        <thead>
                                            <tr class="th">
                                                <td>
                                                    <div align="center" class="w">
                                                        <div>
                                                            <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث"
                                                                Style="text-align: center; width: 100%; font-size: 14px;"
                                                                Text="قائمة التصنيف"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GVCategory" runat="server" AutoGenerateColumns="False" DataKeyNames="_ID_Item_"
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
                                                            <asp:TemplateField HeaderText="العنوان عربي" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <%# Eval("_Name_Ar_")  %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="العنوان إنجليزي" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <div style="text-align: left;"><%# Eval("_Name_En_")  %></div>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="الترتيب" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <%# Eval("_ID_Order_")  %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="حالة العرض" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <%# ClassSaddam.FChangeStyleCheckbox((Boolean) Eval("_Is_Allow_Ar_"))%> <span>عربي</span>
                                                                    <%# ClassSaddam.FChangeStyleCheckbox((Boolean) Eval("_Is_Allow_En_"))%> <span>إنجليزي</span>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="أُضيف من قبل" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <%# ClassQuaem.FAlBaheth((Int32) (Eval("_CreatedBy_")))%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="بتاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <%# Eval("_CreatedDate_", "{0:dd/MM/yyyy}") + " " + Eval("_CreatedDate_", "{0:HH:mm tt}")  %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="15px">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LBEdit" runat="server"
                                                                        OnClick="LBEdit_Click" CommandArgument='<%# Eval("_ID_Item_") %>'
                                                                        data-tooltip="تعديل الملف"><span class="fa fa-edit"></span></asp:LinkButton>
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
                                                </td>
                                            </tr>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <th colspan="9">
                                                    <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                                    <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                                                    <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                    <span class="fa fa-table"></span>
                                                </th>
                                            </tr>
                                        </tfoot>
                                    </table>
                                    <div class="hide">
                                        <hr style='border: solid; border-width: 1px; width: 100%' />
                                    </div>
                                </div>
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

        <div id="IDModel3" class="modal fade in modal_New_Style">
            <div class="modal-dialog " style="max-width: 450px">
                <div class="modal-content">
                    <div class="modal-header no-border">
                        <button type="button" class="close" data-dismiss="modal">×</button>
                    </div>
                    <div class="modal-body" id="modal_ajax_content">
                        <div class="page-container">
                            <div class="page-content">
                                <div class=" panel-body">
                                    <label>
                                        <i class="fa fa-star"></i>
                                        <asp:Label ID="lblTitle" runat="server" Text="إضافة جديد : "></asp:Label>
                                    </label>
                                    <div style="float: left;">
                                        <asp:Label ID="lblStatusAdd" runat="server" Font-Bold="true" Font-Size="16px"></asp:Label>
                                    </div>
                                    <div align="center">
                                        <div class="" dir="rtl">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <h5 style="text-align: right">العنوان عربي : <i style="color: red">*</i></h5>
                                                    <asp:TextBox ID="txtName_Ar_Add" runat="server" class="form-control" ValidationGroup="VGAdd"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                                        ControlToValidate="txtName_Ar_Add" ErrorMessage="* العنوان" ForeColor="#FF0066"
                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <h5 style="text-align: right">العنوان إنجليزي : <i style="color: red">*</i></h5>
                                                    <asp:TextBox ID="txtName_En_Add" runat="server" class="form-control text-left" ValidationGroup="VGAdd" Style="text-align: left; direction: ltr;" Text="-"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                                        ControlToValidate="txtName_En_Add" ErrorMessage="* العنوان" ForeColor="#FF0066"
                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <h5 style="text-align: right">ترتيب رقم : <i style="color: red">*</i></h5>
                                                    <asp:TextBox ID="txt_Order_Add" runat="server" class="form-control text-left" ValidationGroup="VGAdd"
                                                        TextMode="Number" Style="text-align: center"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txt_Order_Add" ErrorMessage="* ترتيب رقم" ForeColor="#FF0066"
                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <h5>حالة العرض عربي : 
                                                    </h5>
                                                    <div class="keepmeLogged">
                                                        <label class="switch">
                                                            <input name="RememberMe" type="checkbox" id="CBAllow_Ar_Add" runat="server" checked />
                                                            <span class="slider round"></span>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <h5>حالة العرض إنجليزي : 
                                                    </h5>
                                                    <div class="keepmeLogged">
                                                        <label class="switch">
                                                            <input name="RememberMe" type="checkbox" id="CBAllow_En_Add" runat="server" checked />
                                                            <span class="slider round"></span>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:LinkButton ID="LBNew" runat="server" Style="margin-right: 4px;" OnClick="LBNew_Click"
                                        class="btn btn-success" ValidationGroup="VGAdd">تحديث البيانات</asp:LinkButton>

                                    <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="IDModel2" class="modal fade in modal_New_Style">
            <div class="modal-dialog " style="max-width: 450px">
                <div class="modal-content">
                    <div class="modal-header no-border">
                        <button type="button" class="close" data-dismiss="modal">×</button>
                    </div>
                    <div class="modal-body" id="modal_ajax_content">
                        <div class="page-container">
                            <div class="page-content">
                                <div class=" panel-body">
                                    <label>
                                        <i class="fa fa-star"></i>
                                        تعديل البيانات :
                                        <asp:HiddenField ID="HF_ID" runat="server" />
                                    </label>
                                    <div style="float: left;">
                                        <asp:Label ID="lblStatusEdit" runat="server" Font-Bold="true" Font-Size="16px"></asp:Label>
                                    </div>
                                    <div align="center">
                                        <div class="" dir="rtl">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <h5 style="text-align: right">العنوان عربي : <i style="color: red">*</i></h5>
                                                    <asp:TextBox ID="txtName_Ar_Edit" runat="server" class="form-control" ValidationGroup="VGEdit"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator40" runat="server"
                                                        ControlToValidate="txtName_Ar_Edit" ErrorMessage="* العنوان" ForeColor="#FF0066"
                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGEdit" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <h5 style="text-align: right">العنوان إنجليزي : <i style="color: red">*</i></h5>
                                                    <asp:TextBox ID="txtName_En_Edit" runat="server" class="form-control text-left" ValidationGroup="VGEdit" Style="text-align: left; direction: ltr;"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator20" runat="server"
                                                        ControlToValidate="txtName_En_Edit" ErrorMessage="* العنوان" ForeColor="#FF0066"
                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGEdit" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <h5 style="text-align: right">ترتيب رقم : <i style="color: red">*</i></h5>
                                                    <asp:TextBox ID="txt_Order_Edit" runat="server" class="form-control text-left" ValidationGroup="VGEdit"
                                                        TextMode="Number" Style="text-align: center"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                                        ControlToValidate="txt_Order_Edit" ErrorMessage="* ترتيب رقم" ForeColor="#FF0066"
                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGEdit" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <h5>حالة العرض عربي : 
                                                    </h5>
                                                    <div class="keepmeLogged">
                                                        <label class="switch">
                                                            <input name="RememberMe" type="checkbox" id="CBAllow_Ar_Edit" runat="server" />
                                                            <span class="slider round"></span>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <h5>حالة العرض إنجليزي : 
                                                    </h5>
                                                    <div class="keepmeLogged">
                                                        <label class="switch">
                                                            <input name="RememberMe" type="checkbox" id="CBAllow_En_Edit" runat="server" />
                                                            <span class="slider round"></span>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:LinkButton ID="LBEditData" runat="server" Style="margin-right: 4px;" OnClick="LBEditData_Click"
                                        class="btn btn-success" ValidationGroup="VGEdit">تحديث البيانات</asp:LinkButton>

                                    <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <br />
        <br />
        <script type="text/javascript">
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
        </script>
        <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>

        <style type="text/css">
        .modal-open {
            overflow: hidden
        }

        .modal {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            z-index: 1050;
            display: none;
            overflow: hidden;
            -webkit-overflow-scrolling: touch;
            outline: 0;
            background-color: hsla(120, 3%, 82%, 0.30);
        }
    </style>
</asp:Content>

