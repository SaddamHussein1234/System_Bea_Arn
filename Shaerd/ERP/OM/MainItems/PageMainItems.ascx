<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageMainItems.ascx.cs" Inherits="Shaerd_ERP_OM_MainItems_PageMainItems" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>

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
        var gv = document.getElementById("<%=GVSubItem.ClientID%>");
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
    function ShowIDModelAdd() {
        $("#IDModelAdd").modal('show');
    }

    $(function () {
        $("#btnShow").click(function () {
            showModal();
        });
    });
</script>

<script type="text/javascript">
    function ShowIDModelEdit() {
        $("#IDModelEdit").modal('show');
    }

    $(function () {
        $("#btnShow").click(function () {
            showModal();
        });
    });
</script>

<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <a href='javaScript:void(0)' data-toggle="modal" data-target="#IDModelAdd" title="إضافة بنك جديد" class="btn btn-info" runat="server" id="IDAdd"><i class="fa fa-plus"></i></a>
            <asp:Button ID="btnDelete" runat="server" Text="حذف الملفات المحددة" title="حذف الملفات المحددة" Visible="false"
                data-toggle="tooltip" CssClass="btn btn-danger" OnClientClick="return ConfirmDelete();" OnClick="btnDelete_Click" />

            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                title="تحديث" OnClick="btnRefrish_Click">
                        <i class="fa fa-refresh"></i></asp:LinkButton>
        </div>
        <div class="container-fluid">
            <h1>لوحة التحكم</h1>
            <ul class="breadcrumb">
                <li><a href="../Default.aspx">الرئيسية</a></li>
                <li><a href="PageSubItems.aspx">قائمة البنود الفرعية </a></li>
            </ul>
        </div>
    </div>
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i class="fa fa-list"></i>قائمة البنود الفرعية
                </h3>
                <div style="float: left;">
                    <div class="col-sm-9">
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder=" إبحث هنا ... "></asp:TextBox>
                    </div>
                    <div class="col-sm-3">
                        <asp:Button ID="btnGet" runat="server" Text="بحث" Style="margin-right: 4px;"
                            OnClientClick="return insertConfirmation();" OnClick="btnGet_Click"
                            class="btn btn-info btn-fill " ValidationGroup="gg2" />
                    </div>
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
                        <div class="table table-responsive" id="pnlPrint" runat="server" dir="rtl">
                            <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                <thead>
                                    <tr class="th">
                                        <td>
                                            <div align="center" class="w">
                                                <div>
                                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث"
                                                        Style="text-align: center; width: 100%; font-size: 14px;"
                                                        Text="قائمة البنود الفرعية حتى حينة"></asp:TextBox>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="GVSubItem" runat="server" AutoGenerateColumns="False" DataKeyNames="_ID_Item_"
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
                                                    <asp:TemplateField HeaderText="عدد التفرعات" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("_Count_Part_").ToString().Replace("1","تفرع واحد").Replace("2","تفرعين").Replace("3","ثلاثة تفرعات") %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="البند الرئيسي" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# FGetName(new Guid(Eval("_ID_Part_").ToString())) %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="البند الفرعي الأول" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# FGetName(new Guid(Eval("_ID_Part_Tow_").ToString())) %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="البند الفرعي الثاني" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# FGetName(new Guid(Eval("_ID_Part_Three_").ToString())) %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="البند الفرعي" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("_Name_Ar_")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="الترتيب" HeaderStyle-ForeColor="#CCCCCC">
                                                        <ItemTemplate>
                                                            <%# Eval("_Order_")  %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderText="أُضيف من قبل">
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
                                                            <asp:LinkButton ID="LBEdit" runat="server" OnClick="LBEdit_Click" CommandArgument='<%# Eval("_ID_Item_") %>'
                                                                title="تعديل الحساب" data-toggle="tooltip"><span class="fa fa-edit"></span></asp:LinkButton>
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

<div id="IDModelAdd" class="modal fade in modal_New_Style">
    <div class="modal-dialog modal-lg">
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
                                <asp:Label ID="lblTitle" runat="server" Text="إضافة بند فرعي جديد : "></asp:Label>
                            </label>
                            <div style="float: left;">
                                <asp:Label ID="lblStatusAdd" runat="server" Font-Bold="true" Font-Size="16px"></asp:Label>
                            </div>
                            <div align="center">
                                <div class="" dir="rtl">
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <h5 style="text-align: right">عدد التفرعات : <span style="color: red">*</span></h5>
                                            <asp:DropDownList ID="DLCountAdd" runat="server" ValidationGroup="VGAdd"
                                                CssClass="form-control" OnLoad="DLCountAdd_Load">
                                                <asp:ListItem Value="1" Selected="True"> تفرع واحد </asp:ListItem>
                                                <asp:ListItem Value="2"> تفرعين </asp:ListItem>
                                                <asp:ListItem Value="3"> ثلاثة تفرعات </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="* طبيعة الإستخدام" CssClass="font"
                                                ControlToValidate="DLCountAdd" ValidationGroup="VGAdd" Font-Size="10px" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <h5 style="text-align: right">البند الرئيسي : <i style="color: red">*</i></h5>
                                            <asp:DropDownList ID="DLMain_ItemsAdd" runat="server" class="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="DLMain_ItemsAdd_SelectedIndexChanged" ValidationGroup="VGAdd">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="DLMain_ItemsAdd" ErrorMessage="* حدد البند " ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <h5 style="text-align: right">البند الفرعي الأول : <i style="color: red">*</i></h5>
                                            <asp:DropDownList ID="DLSubItemsAdd" runat="server" class="form-control" AutoPostBack="true" Enabled="false"
                                                OnSelectedIndexChanged="DLSubItemsAdd_SelectedIndexChanged" ValidationGroup="VGAdd">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server" Visible="false"
                                                ControlToValidate="DLSubItemsAdd" ErrorMessage="* حدد البند " ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <h5 style="text-align: right">البند الفرعي الثاني : <i style="color: red">*</i></h5>
                                            <asp:DropDownList ID="DLSubItemsTowAdd" runat="server" class="form-control" AutoPostBack="true" Enabled="false"
                                                OnSelectedIndexChanged="DLSubItemsTowAdd_SelectedIndexChanged" ValidationGroup="VGAdd">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server" Visible="false"
                                                ControlToValidate="DLSubItemsTowAdd" ErrorMessage="* حدد البند " ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <script type="text/javascript">
                                        function ValidateAdd() {
                                            var ddlFruits = document.getElementById("<%=DLCountAdd.ClientID %>");

                                                    if (ddlFruits.value == "2") {
                                                        document.getElementById("<%=DLSubItemsAdd.ClientID %>").disabled = false;
                                                        document.getElementById("<%=DLSubItemsTowAdd.ClientID %>").disabled = true;
                                                    }
                                                    else if (ddlFruits.value == "3") {
                                                        document.getElementById("<%=DLSubItemsAdd.ClientID %>").disabled = false;
                                                        document.getElementById("<%=DLSubItemsTowAdd.ClientID %>").disabled = false;
                                                    }
                                                    else {
                                                        document.getElementById("<%=DLSubItemsAdd.ClientID %>").disabled = true;
                                                        document.getElementById("<%=DLSubItemsTowAdd.ClientID %>").disabled = true;
                                            }
                                            return true;
                                        }
                                    </script>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <h5 style="text-align: right">إسم البند عربي : <i style="color: red">*</i></h5>
                                            <asp:TextBox ID="txtName_Ar_Add" runat="server" class="form-control" ValidationGroup="VGAdd"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                                ControlToValidate="txtName_Ar_Add" ErrorMessage="* الإسم" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <h5 style="text-align: right">إسم البند إنجليزي : <i style="color: red">*</i></h5>
                                            <asp:TextBox ID="txtName_En_Add" runat="server" class="form-control text-left" ValidationGroup="VGAdd"
                                                Style="text-align: left; direction: ltr;" Text="0"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtName_En_Add" ErrorMessage="* الإسم" ForeColor="#FF0066"
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
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="LBNew" runat="server" Style="margin-right: 4px;"
                                OnClientClick="return insertConfirmation();" OnClick="LBNew_Click"
                                class="btn btn-success" ValidationGroup="VGAdd">تحديث البيانات</asp:LinkButton>

                            <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div id="IDModelEdit" class="modal fade in modal_New_Style">
    <div class="modal-dialog modal-lg">
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
                                تعديل بند فرعي :
                                            <asp:HiddenField ID="HF_ID" runat="server" />
                            </label>
                            <div style="float: left;">
                                <asp:Label ID="lblStatusEdit" runat="server" Font-Bold="true" Font-Size="16px"></asp:Label>
                            </div>
                            <div align="center">
                                <div class="" dir="rtl">
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <h5 style="text-align: right">عدد التفرعات : <span style="color: red">*</span></h5>
                                            <asp:DropDownList ID="DLCountEdit" runat="server" ValidationGroup="VGEdit"
                                                CssClass="form-control" OnLoad="DLCountEdit_Load">
                                                <asp:ListItem Value="1" Selected="True"> تفرع واحد </asp:ListItem>
                                                <asp:ListItem Value="2"> تفرعين </asp:ListItem>
                                                <asp:ListItem Value="3"> ثلاثة تفرعات </asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="* طبيعة الإستخدام" CssClass="font"
                                                ControlToValidate="DLCountEdit" ValidationGroup="VGEdit" Font-Size="10px" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <h5 style="text-align: right">البند الرئيسي : <i style="color: red">*</i></h5>
                                            <asp:DropDownList ID="DLMain_Items_Edit" runat="server" class="form-control"
                                                AutoPostBack="true" OnSelectedIndexChanged="DLMain_Items_Edit_SelectedIndexChanged" ValidationGroup="VGEdit">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                                ControlToValidate="DLMain_Items_Edit" ErrorMessage="* حدد البند" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGEdit" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <h5 style="text-align: right">البند الفرعي الأول : <i style="color: red">*</i></h5>
                                            <asp:DropDownList ID="DLSubItemsEdit" runat="server" class="form-control" AutoPostBack="true" Enabled="false"
                                                OnSelectedIndexChanged="DLSubItemsEdit_SelectedIndexChanged" ValidationGroup="VGAdd">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server" Visible="false"
                                                ControlToValidate="DLSubItemsEdit" ErrorMessage="* حدد البند " ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-group">
                                            <h5 style="text-align: right">البند الفرعي الثاني : <i style="color: red">*</i></h5>
                                            <asp:DropDownList ID="DLSubItemsTowEdit" runat="server" class="form-control" AutoPostBack="true" Enabled="false"
                                                OnSelectedIndexChanged="DLSubItemsTowEdit_SelectedIndexChanged" ValidationGroup="VGAdd">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server" Visible="false"
                                                ControlToValidate="DLSubItemsTowEdit" ErrorMessage="* حدد البند " ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <script type="text/javascript">
                                        function ValidateEdit() {
                                            var ddlFruits = document.getElementById("<%=DLCountEdit.ClientID %>");

                                                    if (ddlFruits.value == "2") {
                                                        document.getElementById("<%=DLSubItemsEdit.ClientID %>").disabled = false;
                                                        document.getElementById("<%=DLSubItemsTowEdit.ClientID %>").disabled = true;
                                                    }
                                                    else if (ddlFruits.value == "3") {
                                                        document.getElementById("<%=DLSubItemsEdit.ClientID %>").disabled = false;
                                                        document.getElementById("<%=DLSubItemsTowEdit.ClientID %>").disabled = false;
                                                    }
                                                    else {
                                                        document.getElementById("<%=DLSubItemsEdit.ClientID %>").disabled = true;
                                                        document.getElementById("<%=DLSubItemsTowEdit.ClientID %>").disabled = true;
                                            }
                                            return true;
                                        }
                                    </script>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <h5 style="text-align: right">إسم البند عربي : <i style="color: red">*</i></h5>
                                            <asp:TextBox ID="txtName_Ar_Edit" runat="server" class="form-control" ValidationGroup="VGEdit"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator40" runat="server"
                                                ControlToValidate="txtName_Ar_Edit" ErrorMessage="* الإسم" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGEdit" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <h5 style="text-align: right">إسم البند إنجليزي : <i style="color: red">*</i></h5>
                                            <asp:TextBox ID="txtName_En_Edit" runat="server" class="form-control text-left" ValidationGroup="VGEdit" Style="direction: ltr; text-align: left;" Text="0"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator20" runat="server"
                                                ControlToValidate="txtName_En_Edit" ErrorMessage="* الإسم" ForeColor="#FF0066"
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
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="LBEdit" runat="server" Style="margin-right: 4px;"
                                OnClientClick="return insertConfirmation();" OnClick="LBEdit_Click1"
                                class="btn btn-success" ValidationGroup="VGEdit">تحديث البيانات</asp:LinkButton>

                            <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

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
