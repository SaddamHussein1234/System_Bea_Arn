<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/CRM/CRM_Main.master" AutoEventWireup="true" CodeFile="PageKind_SupportAdd.aspx.cs" Inherits="Cpanel_ERP_CRM_PageKind_Support_PageKind_SupportAdd" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/ERP/WUCFooterBottomERP.ascx" TagPrefix="uc1" TagName="WUCFooterBottomERP" %>

<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<%@ Import Namespace="Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters" %>
<%@ Import Namespace="Library_CLS_Arn.OM.Repostry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btn_Add_To_.ClientID %>").disabled = true;
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
            var gv = document.getElementById("<%=GVBillAll.ClientID%>");
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

    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>

    <script type="text/javascript">
        function ConfirmDelete() {
            if (confirm("هل تريد إلغاء الامر ستفقد البيانات التي قمت بكتابتها حالياً ؟") == true)
                return true;
            else
                return false;
        }
    </script>

    <script type="text/javascript">
        function showModal() {
            $("#myModal").modal('show');
        }

        $(function () {
            $("#btnShow").click(function () {
                showModal();
            });
        });
    </script>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip"
                        title="طباعة" OnClick="btnPrint_Click" Visible="false">
                    <i class="fa fa-print"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnDelete" runat="server" Text="إلغاء المحددة" title="إلغاء المحددة" Visible="false"
                        data-toggle="tooltip" CssClass="btn btn-danger" OnClientClick="return ConfirmDelete();" OnClick="btnDelete_Click" />
                    <asp:LinkButton ID="LBRefrsh" runat="server" data-toggle="tooltip" title="تحديث" OnClick="LBRefrsh_Click"
                        class="btn btn-info"> <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" OnClick="LBExit_Click"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageKind_Support.aspx">فواتير الدعم العيني</a></li>
                    <li><a href="PageKind_SupportAdd.aspx">إضافة فاتورة</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        إضافة رقم فاتورة دعم عيني
                    </h3>
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
                            <div class="container-fluid">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>حدد الداعم :
                                        </h5>
                                        <asp:DropDownList ID="DLCompany" runat="server" ValidationGroup="VGAddBill" AutoPostBack="true" OnSelectedIndexChanged="DLCompany_SelectedIndexChanged"
                                            Height="25px" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem></asp:ListItem>

                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="DLCompany" ErrorMessage="* حدد الداعم" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="VGAddBill" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>الارشيف :
                                        </h5>
                                        <asp:DropDownList ID="ddlYears" runat="server" ValidationGroup="GBill"
                                            Height="25px" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                            ControlToValidate="ddlYears" ErrorMessage="* الارشيف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>رقم الفاتورة : <span class="required">*</span></h5>
                                        <asp:TextBox ID="txtNo_Bill" runat="server" class="form-control" ValidationGroup="VGAddBill" TextMode="Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtNo_Bill" ErrorMessage="* رقم الفاتورة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAddBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNo_Bill"
                                            ErrorMessage="* أرقام فقط ..." ValidationExpression="^[0-9]+$" ValidationGroup="VGAddBill" Font-Size="10px"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btn_Add_To_" runat="server" Text="التحقق من الفاتورة" CssClass="btn btn-info"
                                            ValidationGroup="VGAddBill" OnClick="btn_Add_To__Click" />
                                        <div class="modal fade" id="myModal" role="dialog">
                                            <div class="modal-dialog modal-sm">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                        <h4 class="modal-title">رسالة تأكيد</h4>
                                                    </div>
                                                    <div class="modal-body">
                                                        <span id="IDMessage" runat="server"></span>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <asp:Button ID="btn_Add" runat="server" OnClick="btn_Add_Click" 
                                                             CssClass="btn btn-success" Text="هل تريد الإستمرار ؟" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlData" runat="server" Visible="false" Direction="RightToLeft">
                            <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                                <div class="table table-responsive" id="pnlPrint" runat="server" dir="rtl">
                                <div class="HideNow">
                                    <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                </div>
                                <div align="center" class="w">
                                    <div>
                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث"
                                            Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                    </div>
                                </div>
                                <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                    <thead>
                                        <tr class="th">
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVBillAll" runat="server" AutoGenerateColumns="False" DataKeyNames="_ID_Item_"
                                                    Width="100%" CssClass="footable1" OnRowDataBound="GVBillAll_RowDataBound"
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
                                                         <asp:TemplateField HeaderText="الإرشيف" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <small><%# Repostry_FinancialYear_.FErp_FinancialYear_ByID(new Guid(Eval("_ID_FinancialYear_").ToString())) %></small>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="_Registration_No_" HeaderText="رقم تسجيل الداعم" SortExpression="_Registration_No_" HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:BoundField DataField="_Company_Name_" HeaderText="إسم الداعم" SortExpression="_Company_Name_" HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:BoundField DataField="_ID_Bill_" HeaderText="رقم الفاتورة" SortExpression="_ID_Bill_" HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:TemplateField HeaderText="مبلغ الفاتورة" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" 
                                                                    Text='<%# Repostry_In_Kind_Donation_Details_.FOM_In_Kind_Donation_Details_Manage(new Guid (Eval("_ID_Item_").ToString()))%>'></asp:Label>
                                                                <small><%# ClassSaddam.FGetMonySa() %></small>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CreatedDate" HeaderText="تاريخ الإضافة" SortExpression="CreatedDate" HeaderStyle-ForeColor="#CCCCCC" />
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderText="أُضيف من قبل">
                                                            <ItemTemplate>
                                                                <%# ClassQuaem.FAlBaheth((Int32) (Eval("CreatedBy")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16px">
                                                            <ItemTemplate>
                                                                <a href='<%=ResolveUrl("~/Cpanel/ERP/OM/In_Kind_Donation/PageView.aspx?ID=")%><%# Eval("_ID_Bill_")%>&IDUniq=<%# Eval("_ID_FinancialYear_")%>&Type=Moder' title="عرض الفاتورة" data-toggle="tooltip"
                                                                    class="btn btn-info"><span class="fa fa-eye"></span></a>
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
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th colspan="9">
                                                <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                                <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                <span class="fa fa-table"></span>
                                                <br />
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <div class="HideNow">
                                                    <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                                </div>
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 15%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">المجموع : 
                                            </td>
                                            <td style="width: 65%; border: thin double #808080; border-width: 1px;" align="center">
                                                <asp:TextBox ID="lblSumWord" runat="server" class="form-control" placeholder="المبلغ" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                            </td>
                                            <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                                <asp:Label ID="lblTotalPrice" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                <asp:Label ID="Label5" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                            </div>
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
                        </asp:Panel>
                            <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                                    <hr />
                                    <br />
                                    <br />
                                    <div align="center">
                                        <h3 style="font-size: 18px">يرجى تحديد الداعم المراد عمل فاتورة له ...
                                        </h3>
                                    </div>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </asp:Panel>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <br />
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

