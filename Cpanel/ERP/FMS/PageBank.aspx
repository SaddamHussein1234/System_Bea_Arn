<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/FMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageBank.aspx.cs" Inherits="Cpanel_ERP_FMS_PageBank" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
        var gv = document.getElementById("<%=GVBanksName.ClientID%>");
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
    </style>

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href='javaScript:void(0)' data-toggle="modal" data-target="#IDModel3" data-tooltip="إضافة بنك جديد" class="btn btn-info" runat="server" id="IDAdd"><i class="fa fa-plus"></i></a>
                    <asp:Button ID="btnDelete" runat="server" Text="حذف الملفات المحددة" data-tooltip="حذف الملفات المحددة" Visible="false"
                        CssClass="btn btn-danger" OnClientClick="return ConfirmDelete();" OnClick="btnDelete_Click" />
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default"
                        data-tooltip="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="../Default">الرئيسية</a></li>
                        <li><a href="PageBank.aspx">قائمة البنوك </a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة البنوك
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
                        <div class="col-sm-2">
                            <div class="input-group date " style="margin-right: -10px;">
                                <asp:TextBox ID="txtDateFrom" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="gg2" Style="direction: ltr;"></asp:TextBox>
                                <asp:Label ID="lblDateFrom" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="input-group date " style="margin-right: -10px;">
                                <asp:TextBox ID="txtDateTo" runat="server" placeholder="إلى تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="gg2" Style="direction: ltr;"></asp:TextBox>
                                <asp:Label ID="lblDateTo" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </span>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder=" إبحث هنا ... "></asp:TextBox>
                        </div>
                        <div class="col-sm-3">
                            <asp:Button ID="btnGet" runat="server" Text="بحث" Style="margin-right: 4px;" OnClick="btnGet_Click"
                                class="btn btn-info btn-fill " ValidationGroup="gg2" />
                        </div>
                        <div class="clearfix"></div>
                        <asp:Panel ID="pnlData" runat="server" Visible="False">
                            <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                                <div class="table table-responsive" id="pnlPrint" runat="server" dir="rtl">
                                    <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                        <thead>
                                            <tr class="th">
                                                <td>
                                                    <div align="center" class="w">
                                                        <div class="col-md-1">
                                                            <asp:DropDownList ID="DLCountRows" runat="server" ValidationGroup="VGCustomer"
                                                                CssClass="form-control" Width="75" AutoPostBack="true"
                                                                OnSelectedIndexChanged="DLCountRows_SelectedIndexChanged">
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                <asp:ListItem Value="25" Selected="True">25</asp:ListItem>
                                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                                <asp:ListItem Value="250">250</asp:ListItem>
                                                                <asp:ListItem Value="500">500</asp:ListItem>
                                                                <asp:ListItem Value="1000">1000</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-11">
                                                            <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث"
                                                                Style="text-align: center; width: 100%; font-size: 14px;"
                                                                Text="قائمة البنوك"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GVBanksName" runat="server" AutoGenerateColumns="False" DataKeyNames="_ID_Item_"
                                                        Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False"
                                                        OnPageIndexChanging="GVBanksName_PageIndexChanging" AllowPaging="true" PageSize="25">
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
                                                            <asp:TemplateField HeaderText="إسم البنك عربي" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <%# Eval("_Name_Bank_Ar_")  %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="إسم البنك إنجليزي" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <%# Eval("_Name_Bank_En_")  %>
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
                                                            <asp:TemplateField HeaderText="الحسابات المرتبطة به" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    0
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="تاريخ الإضافة" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <%# Eval("_CreatedDate_", "{0:dd/MM/yyyy HH:mm tt}") %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="أُضيف من قبل" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <%# ClassAdmin_Arn.FGetNameByID((Int32) (Eval("_CreatedBy_")))%>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="15px">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LBEdit" runat="server" OnClick="LBEdit_Click" CommandArgument='<%# Eval("_ID_Item_") %>'
                                                                        data-tooltip="تعديل الحساب"><span class="fa fa-edit"></span></asp:LinkButton>
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
                            <hr />
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
                        <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                            <hr />
                            <br />
                            <br />
                            <div align="center">
                                <h3 style="font-size: 20px">حدد البيانات
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
                                        <asp:Label ID="lblTitle" runat="server" Text="إضافة بنك جديد : "></asp:Label>
                                    </label>
                                    <div style="float: left;">
                                        <asp:Label ID="lblStatusAdd" runat="server" Font-Bold="true" Font-Size="16px"></asp:Label>
                                    </div>
                                    <div align="center">
                                        <div class="" dir="rtl">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <h5 style="text-align: right">إسم البنك عربي : <i style="color: red">*</i></h5>
                                                    <asp:TextBox ID="txtName_Bank_Ar" runat="server" class="form-control" ValidationGroup="VGAdd"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                                        ControlToValidate="txtName_Bank_Ar" ErrorMessage="* الإسم" ForeColor="#FF0066"
                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <h5 style="text-align: right">إسم البنك إنجليزي : <i style="color: red">*</i></h5>
                                                    <asp:TextBox ID="txtName_Bank_En" runat="server" class="form-control text-left" ValidationGroup="VGAdd"
                                                        Style="text-align: left; direction: ltr;" Text="-"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                                        ControlToValidate="txtName_Bank_En" ErrorMessage="* الإسم" ForeColor="#FF0066"
                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <h5 style="text-align: right">ترتيب رقم : <i style="color: red">*</i></h5>
                                                    <asp:TextBox ID="txt_Order" runat="server" class="form-control text-left" ValidationGroup="VGAdd"
                                                        TextMode="Number" Style="text-align: center"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                                        ControlToValidate="txt_Order" ErrorMessage="* ترتيب رقم" ForeColor="#FF0066"
                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                        تعديل بيانات البنك :
                                        <asp:HiddenField ID="HF_ID" runat="server" />
                                    </label>
                                    <div style="float: left;">
                                        <asp:Label ID="lblStatusEdit" runat="server" Font-Bold="true" Font-Size="16px"></asp:Label>
                                    </div>
                                    <div align="center">
                                        <div class="" dir="rtl">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <h5 style="text-align: right">إسم البنك عربي : <i style="color: red">*</i></h5>
                                                    <asp:TextBox ID="txtName_Bank_Ar_Edit" runat="server" class="form-control" ValidationGroup="VGEdit"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator40" runat="server"
                                                        ControlToValidate="txtName_Bank_Ar_Edit" ErrorMessage="* الإسم" ForeColor="#FF0066"
                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGEdit" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <h5 style="text-align: right">إسم البنك إنجليزي : <i style="color: red">*</i></h5>
                                                    <asp:TextBox ID="txtName_Bank_En_Edit" runat="server" class="form-control text-left" ValidationGroup="VGEdit" Style="text-align: left"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator20" runat="server"
                                                        ControlToValidate="txtName_Bank_En_Edit" ErrorMessage="* الإسم" ForeColor="#FF0066"
                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGEdit" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <h5 style="text-align: right">ترتيب رقم : <i style="color: red">*</i></h5>
                                                    <asp:TextBox ID="txt_Order_Edi" runat="server" class="form-control text-left" ValidationGroup="VGEdit"
                                                        TextMode="Number" Style="text-align: center"></asp:TextBox>
                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                                        ControlToValidate="txt_Order_Edi" ErrorMessage="* ترتيب رقم" ForeColor="#FF0066"
                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGEdit" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <asp:LinkButton ID="LBEdit" runat="server" Style="margin-right: 4px;" OnClick="LBEdit_Click1"
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
        <script src="<%=ResolveUrl("~/files/cpanel/view/Chosen/chosen.jquery.js")%>" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
        <span runat="server" id="IDCreatedByStyle"></span>
</asp:Content>

