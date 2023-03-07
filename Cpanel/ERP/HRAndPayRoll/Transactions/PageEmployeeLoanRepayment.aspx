<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="PageEmployeeLoanRepayment.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeLoanRepayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
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
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click"><i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../../Default.aspx">الرئيسية</a></li>
                    <li><a href="PageEmployeeLoans.aspx">قائمة قروض الموظفين</a></li>
                    <li><a href="PageEmployeeLoanRepayment.aspx">تسديد/تعديل قروض الموظفين </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="تسديد/تعديل قروض الموظفين"></asp:Label>
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
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label">
                                              حدد الإدارة : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" Width="100%" ValidationGroup="g2"
                                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control2 chzn-select dropdown">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDepartment" SetFocusOnError="true" ControlToValidate="ddlDepartment" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic"  Font-Size="10px" ErrorMessage="* حدد الإدارة" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label">
                                             حدد الموظف :  <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control2 chzn-select dropdown"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged"
                                                Width="100%" ValidationGroup="g2">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEmployee" SetFocusOnError="true" ControlToValidate="ddlEmployee" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الموظف" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <br />
                                <div class="col-md-12" style="border: 2px solid #c0c0c0; border-radius: 7px; margin-bottom: 5px;">
                                    <asp:Repeater ID="rptLoan" runat="server">
                                        <ItemTemplate>
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">
                                                    <%#Eval("LoanTitle") %>
                                                </label>
                                                <label class="col-md-2 control-label">
                                                    المبلغ المتبقي : 
                                                </label>
                                                <div class="col-md-2">
                                                    <asp:HiddenField ID="hfLoanId" runat="server" Value='<%# Eval("EmployeeLoanMapID") %>' />
                                                    <asp:Label ID="lblPendingLoan" CssClass="lblPendingLoan" runat="server" 
                                                        Text='<%# Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions.Repostry_EmployeePaidLoan_.FErp_EmployeePaidLoan_Manage(new Guid(Eval("EmployeeLoanMapID").ToString()), Convert.ToDecimal(Eval("Amount")))%>' />
                                                </div>
                                                <label class="col-md-2 control-label">
                                                    القسط الشهري : 
                                                </label>
                                                <div class="col-md-3">
                                                    <asp:TextBox ID="txtPaidLoanAmount" style="margin-top:-10px" MaxLength="12" pendingLoan='<%#Convert.ToInt32(Eval("Amount")) %>'
                                                        runat="server" Text='<%# String.Format("{0:0.00}",Eval("PaidLoan")) %>'
                                                        CssClass="form-control input-width-xlarge txtPaidLoanAmount"
                                                        onkeypress="return Common.isNumericKey(event,this)"
                                                        onkeyup="EmployeeSalaryProcessSave.checkPaidLoanAmount(this)" Width="100"></asp:TextBox>
                                                </div>
                                                <div class="clearfix"></div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Panel ID="pnlNull" runat="server" Visible="False">
                                        <br />
                                        <br />
                                        <div align="center">
                                            <h3 style="font-size: 20px">لا توجد قروض لهذا الموظف ... 
                                            </h3>
                                        </div>
                                        <br />
                                        <br />
                                    </asp:Panel>
                                </div>
                            <div class="form-group">
                                <label class="col-md-4 control-label">
                                    مجموع القروض التي سيتم تسديدها :
                                </label>
                                <div class="col-md-8 control-label textLeft">
                                    <asp:Label ID="lblTotalPaidLoanAmount" runat="server" CssClass="lblTotalPaidLoanAmount" Text="0"></asp:Label>
                                    <asp:TextBox ID="txtTotalPaidLoanAmount" runat="server" CssClass="txtTotalPaidLoanAmount" Text="0" Width="100" style="display:none;"></asp:TextBox>
                                </div>
                                <br /><br />
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div align="left">
                <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="font-size: medium"
                    class="btn btn-info" OnClick="btnAdd_Click" ValidationGroup="g2" />
                <asp:LinkButton ID="LBBack" runat="server" Style="font-size: medium" OnClick="LBBack_Click"
                    class="btn btn-danger">خروج</asp:LinkButton>
            </div>
            <br />
            <br />
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
        <script src="/PageEmployeeSalarySave.js"></script>
</asp:Content>

