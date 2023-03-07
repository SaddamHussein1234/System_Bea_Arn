<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="PageEmployeeSalaryList.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeSalaryList" %>

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
            var gv = document.getElementById("<%=gvEmployeeCompletedSalaryProcess.ClientID%>");
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
    <script src="/EmployeeSalaryProcessSave.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click"><i class="fa fa-refresh"></i></asp:LinkButton>

                    <asp:LinkButton ID="btnDelete" runat="server" class="btn btn-danger" Visible="false" OnClick="btnDelete_Click"
                        OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"><span class="tip-bottom">
                    <i class="fa fa-trash-o"></i></span></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../../Default.aspx">الرئيسية</a></li>
                    <li><a href="PageEmployeeSalaryList.aspx">رواتب الموظفين </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="رواتب الموظفين"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
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
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label" id="Label1" runat="server">
                                            الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2 chzn-select dropdown" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlYears_SelectedIndexChanged" Width="100%" ValidationGroup="g2">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="ddlYears" ValidationGroup="g2"
                                            CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد السنة" runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label" id="Label3" runat="server">
                                            حدد الشهر <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control2 chzn-select dropdown" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" Width="100%" ValidationGroup="g2">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" ControlToValidate="ddlMonth" ValidationGroup="g2"
                                            CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الشهر" runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>                               
                            </div>
                            <div class="container-fluid table-responsive" dir="rtl">
                                <div id="divSalaryProcess" visible="false" class="form-group" runat="server">
                                    <div class="col-md-12">
                                        <div class="tabbable tabbable-custom">
                                            <ul class="nav nav-tabs">
                                                <li class="active"><a href="#tabPending" data-toggle="tab"><i class="fa fa-star"></i> لم يتم تسليم الراتب</a></li>
                                                <li><a href="#tabCompleted" data-toggle="tab"><i class="fa fa-star"></i> تم تسليم الراتب</a></li>
                                            </ul>
                                            <div class="tab-content">
                                                <div class="tab-pane active" id="tabPending">
                                                    <asp:GridView ID="gvEmployeePendingSalaryProcess" runat="server" AutoGenerateColumns="False"
                                                        Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
                                                        UseAccessibleHeader="False">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="الإدارة" DataField="Department" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField HeaderText="إسم الموظف" DataField="FullName" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField HeaderText="رقم الموظف" DataField="EmployeeNo" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField HeaderText="الراتب الأساسي" DataField="PaidBasic" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField HeaderText="البدلات" DataField="PaidTotalEarning" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField HeaderText="المستقطع" DataField="PaidTotalDeduction" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField HeaderText="إجمالي الراتب" DataField="PaidTotalSalary" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <a href='PageEmployeeSalarySave.aspx?Monthyear=<%# FGetIDMounth() %>&Employeeid=<%#Eval("EmployeeId")%>&IDYear=<%# ddlYears.SelectedValue %>' 
                                                                        id="btnEdit" class="btnEdit"> <i class="fa fa-edit" title="edit sign"></i> </a>
                                                                </ItemTemplate>
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
                                                <div class="tab-pane" id="tabCompleted">
                                                    <asp:GridView ID="gvEmployeeCompletedSalaryProcess" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeId"
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
                                                            <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="الإدارة" DataField="Department" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField HeaderText="إسم الموظف" DataField="FullName" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField HeaderText="رقم الموظف" DataField="EmployeeNo" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField HeaderText="الراتب الأساسي" DataField="PaidBasic" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField HeaderText="البدلات" DataField="PaidTotalEarning" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField HeaderText="المستقطع" DataField="PaidTotalDeduction" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField HeaderText="إجمالي الراتب" DataField="PaidTotalSalary" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:TemplateField HeaderText="تاريخ التسليم" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPaidDate" runat="server" Text='<%# Eval("PaidDate")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="20px" HeaderStyle-ForeColor="#CCCCCC">
                                                                <ItemTemplate>            
                                                                    <a href='PageEmployeeSalaryByView.aspx?Monthyear=<%# FGetIDMounth() %>&Employeeid=<%#Eval("EmployeeId")%>&IDYear=<%# ddlYears.SelectedValue %>'
                                                                        class="btnView"><i class="fa fa-eye" title="Click to View Employee Salary Process"></i></a>
                                                                </ItemTemplate>
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
                                                    <asp:HiddenField ID="hfCount" runat="server" />
                                                </div>                                              
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div align="center">
                                        <h3 style="font-size: 20px">يرجى تحديد البيانات ... 
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
            </div>
        </div>
        <br />
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
        <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

