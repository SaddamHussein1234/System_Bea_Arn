<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="PageEmployeeSalarySave.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeSalarySave" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSave.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

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

    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" onload="checkCookies()">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../../Default.aspx">الرئيسية</a></li>
                    <li><a href="PageEmployeeSalaryList.aspx">قائمة تسليم الرواتب</a></li>
                    <li><a href="">إضافة الراتب للموظف </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة الراتب للموظف"></asp:Label>
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
                                <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2 chzn-select dropdown" Visible="false"
                                    Width="100%" ValidationGroup="g2">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-12">
                                    <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
                                    <asp:HiddenField ID="hfId" runat="server" />
                                    <asp:HiddenField ID="hfMonth" runat="server" />
                                    <asp:HiddenField ID="hfEmployeeId" runat="server" />

                                    <div class="form-group efirst">
                                        <label class="col-md-1 control-label">
                                            الإدارة:
                                        </label>
                                        <div class="col-md-3 control-label textLeft">
                                            <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 control-label">
                                            الإسم :
                                        </label>
                                        <div class="col-md-6 control-label textLeft">
                                            <asp:Label ID="lblEmployeeName" runat="server"></asp:Label> <span id="lblPhone" runat="server"></span>
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">
                                            رقم الموظف :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:Label ID="lblEmployeeNo" runat="server"></asp:Label>
                                        </div>

                                        <label class="col-md-2 control-label">
                                            الشهر :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:HiddenField ID="HMonth" runat="server" Value="0"></asp:HiddenField>
                                            <asp:Label ID="lblMonth" runat="server"></asp:Label>
                                        </div>
                                        <label class="col-md-2 control-label">
                                            السنة :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:Label ID="lblYear" runat="server"></asp:Label>
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">
                                            أيام الشهر :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:Label ID="lblTotalDays" runat="server" CssClass="lblTotalDays" Text="0"></asp:Label> / يوم
                                        </div>
                                        <label class="col-md-2 conftrol-label">
                                            أيام الحضور :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:Label ID="lblTotalPresentDays" runat="server" CssClass="lblTotalPresentDays" Text="0"></asp:Label> / يوم
                                        </div>                                        
                                    </div>

                                    <div class="form-group" runat="server" visible="true">
                                        <label class="col-md-2 control-label">
                                            نهاية الأسبوع :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:Label ID="lblWeeklyOff" runat="server" Text="0"></asp:Label> / يوم
                                        </div>
                                        <label class="col-md-2 control-label">
                                            الغياب :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:HiddenField ID="hfAllowLeave" runat="server" Value="0"></asp:HiddenField>
                                            <asp:HiddenField ID="hfTotalUsedLeave" runat="server" Value="0"></asp:HiddenField>
                                            <asp:HiddenField ID="hfCalculateLeave" runat="server" Value="0"></asp:HiddenField>
                                            <asp:Label ID="lblLeave" runat="server" Text="0" /> / يوم
                                        </div>
                                        <div id="divPaidLeaveAmount" runat="server">
                                            <label class="col-md-2 control-label">
                                                خصم غياب :
                                            </label>
                                            <div class="col-md-2 control-label textLeft">
                                                <asp:Label ID="lblTotalPaidLeaveSalary" runat="server" Text="0"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label ">
                                            ساعات الدوام :
                                        </label>
                                        <div class="col-md-6 control-label textLeft">
                                            <asp:Label ID="lblWorkingHours" runat="server" Text="0"></asp:Label>
                                            / ساعة من إجمالي 
                                            <asp:Label ID="lblOrgWorkingHours" runat="server" Text="0"></asp:Label>
                                            / ساعة
                                            / تأخير وإنصراف مبكر
                                            <asp:Label ID="lblGo" runat="server" Text="0"></asp:Label>
                                            / ساعة
                                        </div>
                                        <label class="col-md-2 control-label ">
                                            خصم تأخير/إنصراف
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:HiddenField ID="HFTotalGo" runat="server" />
                                            <asp:Label ID="lblTotalGo" runat="server" Text="0"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <label class="col-md-2 control-label">
                                            الإجازات الرسمية :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:HiddenField ID="HFTotalHolidays" runat="server" />
                                            <asp:Label ID="lblTotalHolidays" runat="server" CssClass="lblTotalHolidays" Text="0"></asp:Label> / يوم
                                        </div>
                                        <label class="col-md-2 control-label ">
                                            إجازات الموظف :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:HiddenField ID="HFLeave2" runat="server" />
                                            <asp:Label ID="lblLeave2" runat="server" Text="0"></asp:Label>
                                            / يوم
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label ">
                                            الإنتدابات :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:HiddenField ID="HFMandate" runat="server" />
                                            <asp:Label ID="lblMandate" runat="server" Text="0"></asp:Label>
                                            / يوم
                                        </div>
                                        <label class="col-md-2 control-label ">
                                            مبلغ الإنتدابات :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:HiddenField ID="HFTotlMandate" runat="server" />
                                            <asp:Label ID="lblTotlMandate" runat="server" Text="0"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label ">
                                            الحسومات :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:HiddenField ID="HFResolved" runat="server" />
                                            <asp:Label ID="lblResolved" runat="server" Text="0"></asp:Label>
                                            / يوم
                                        </div>
                                        <label class="col-md-2 control-label ">
                                            مبلغ الحسومات :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:HiddenField ID="HFTotalResolved" runat="server" />
                                            <asp:Label ID="lblTotalResolved" runat="server" Text="0"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label ">
                                            ساعات الإضافي :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:Label ID="lblTotalOverTimeDays" runat="server" Text="0"></asp:Label>
                                            / ساعة
                                        </div>
                                        <label class="col-md-2 control-label ">
                                            مبلغ الإضافي :
                                        </label>
                                        <div class="col-md-2 control-label textLeft">
                                            <asp:Label ID="lblTotalOverTimeSalary" runat="server" Text="0"></asp:Label>
                                        </div>
                                        <label class="col-md-2 control-label">
                                            الأساسي :
                                        </label>
                                        <div class="col-md-2  control-label textLeft">
                                            <asp:Label ID="lblBasic" Text="0" CssClass="lblBasic" runat="server"></asp:Label>
                                            /
                                            <asp:Label ID="lblPaidBasic" Text="0" CssClass="lblPaidBasic" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label1" runat="server" Text="قائمة البدلات والعلاوات + المستقطع من الراتب"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="col-md-6">
                                <div class="widget box">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">
                                            <i class="fa fa-list"></i>
                                            البدلات(العلاوات) 
                                        </h3>
                                    </div>
                                    <div class="widget-content">
                                        <asp:Repeater ID="rptAllowance" runat="server">
                                            <ItemTemplate>
                                                <%--<div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                        <%#Eval("AllowanceName") %>
                                                        <br />
                                                        <span class="redFont"><%#Convert.ToBoolean(Eval("IsConsider"))?"Consider":"" %></span>
                                                    </label>
                                                    <div class="col-md-8 control-label textLeft">
                                                        <asp:HiddenField ID="hfAllowanceId" runat="server" Value='<%#Eval("AllowanceID") %>' />
                                                        <asp:Label ID="lblAllowance" CssClass="lblAllowance" runat="server" Text='<%#String.Format("{0:0.00}",Eval("Amount")) %>' />
                                                        /
                                                        <asp:Label ID="lblPaidAllowance" runat="server" Text='<%#String.Format("{0:0.00}",Eval("PaidAmount")) %>'></asp:Label>
                                                    </div>
                                                </div>--%>

                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                        <%#Eval("AllowanceName") %>
                                                        <br />
                                                        <span class="redFont"><%#Convert.ToBoolean(Eval("IsConsider"))?"يحتسب":"" %></span>
                                                    </label>
                                                    <div class="col-md-8 control-label textLeft">
                                                        <asp:HiddenField ID="hfAllowanceId" runat="server" Value='<%#Eval("AllowanceID") %>' />
                                                        <asp:Label ID="lblAllowance" CssClass="lblAllowance" runat="server" Text='<%#String.Format("{0:0.00}",Eval("Amount")) %>' />
                                                        /
                                                        <asp:Label ID="lblPaidAllowance" runat="server" Text='<%#String.Format("{0:0.00}",Eval("PaidAmount")) %>'></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="clearfix"></div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                الإجمالي :
                                            </label>
                                            <div class="col-md-2 control-label textLeft">
                                                <asp:HiddenField ID="hfTotalAllowance" runat="server" Value="0" ClientIDMode="Static" />
                                                <asp:Label ID="lblTotalAllowance" runat="server" CssClass="lblTotalAllowance" Text="0"></asp:Label>
                                            </div>
                                            <label class="col-md-4 control-label">
                                                المستحق :
                                            </label>
                                            <div class="col-md-2 control-label textLeft">
                                                <asp:Label ID="lblPaidTotalAllowance" runat="server" CssClass="lblPaidTotalAllowance" Text="0"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="widget box">
                                    <div class="panel-heading">
                                        <h3 class="panel-title">
                                            <i class="fa fa-list"></i>
                                            المستقطع من الراتب
                                        </h3>
                                    </div>
                                    <div class="widget-content">
                                        <asp:Repeater ID="rptDeduction" runat="server">
                                            <ItemTemplate>
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                        <%#Eval("DeductionName") %>
                                                        <br />
                                                        <span class="redFont"><%#Convert.ToBoolean( Eval("IsConsider"))?"يحتسب":"" %></span>
                                                    </label>
                                                    <div class="col-md-8 control-label textLeft">
                                                        <asp:HiddenField ID="hfDeductionId" runat="server" Value='<%#Eval("DeductionID") %>' />
                                                        <asp:Label ID="lblDeduction" CssClass="lblDeduction" runat="server" Text='<%#String.Format("{0:0.00}",Eval("Amount")) %>' />
                                                        /
                                                                    <asp:Label ID="lblPaidDeduction" runat="server" Text='<%#String.Format("{0:0.00}",Eval("PaidAmount")) %>' CssClass="lblPaidDeduction"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="clearfix"></div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <div class="clearfix"></div>
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                الإجمالي  :
                                            </label>
                                            <div class="col-md-2 control-label textLeft">
                                                <asp:HiddenField ID="hfTotalDeduction" runat="server" Value="0" ClientIDMode="Static" />
                                                <asp:Label ID="lblTotalDeduction" runat="server" CssClass="lblTotalDeduction" Text="0"></asp:Label>
                                            </div>
                                            <label class="col-md-4 control-label">
                                                المستحق :
                                            </label>
                                            <div class="col-md-2 control-label textLeft">
                                                <asp:Label ID="lblPaidTotalDeduction" runat="server" CssClass="lblPaidTotalDeduction" Text="0"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label2" runat="server" Text="تفاصيل القروض"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <asp:Repeater ID="rptLoan" runat="server">
                                <ItemTemplate>
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">
                                            <%#Eval("LoanTitle") %>
                                        </label>
                                        <div class="col-md-1">
                                            <asp:HiddenField ID="hfLoanId" runat="server" Value='<%# Eval("EmployeeLoanMapID") %>' />
                                            <asp:Label ID="lblPendingLoan" CssClass="lblPendingLoan" runat="server" 
                                                Text='<%# Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions.Repostry_EmployeePaidLoan_.FErp_EmployeePaidLoan_Manage(new Guid(Eval("EmployeeLoanMapID").ToString()), Convert.ToDecimal(Eval("Amount")))%>' />
                                        </div>
                                        <label class="col-md-1 control-label">
                                            القسط : 
                                        </label>
                                        <div class="col-md-4">
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
                            <div class="clearfix"></div>
                            <div class="form-group">
                                <label class="col-md-2 control-label">
                                    صافي الراتب :
                                </label>
                                <div class="col-md-1 control-label textLeft">
                                    <asp:Label ID="lblNetSalary" runat="server" CssClass="lblNetSalary" Text="0"></asp:Label>
                                </div>
                                <label class="col-md-2 control-label">
                                    الضريبة المهنية :
                                </label>
                                <div class="col-md-1 control-label textLeft">
                                    <asp:Label ID="lblProfessionalTax" runat="server" CssClass="lblProfessionalTax" Text="0"></asp:Label>
                                </div>
                                <label class="col-md-3 control-label">
                                    الراتب في متناول اليد :
                                </label>
                                <div class="col-md-1 control-label textLeft">
                                    <asp:HiddenField ID="hfCalculateSalary" Value="0" runat="server" ClientIDMode="Static" />
                                    <asp:Label ID="lblOnHandSalary" runat="server" CssClass="lblOnHandSalary" Text="0"></asp:Label>
                                    <asp:TextBox ID="txtOnHandSalary" runat="server" CssClass="displayNone txtOnHandSalary" Width="100" style="display:none;"></asp:TextBox>
                                    <asp:CompareValidator ID="cvSalary" runat="server" ValueToCompare="0" ControlToValidate="txtOnHandSalary" Display="Dynamic"
                                        ErrorMessage="Must set On Hand Salary grater than 0." CssClass="required" Operator="GreaterThanEqual" Type="Double"></asp:CompareValidator>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <hr />
                            <div class="form-group">
                                <label class="col-md-2 control-label">
                                    تاريخ الدفع <span class="required">*</span>
                                </label>
                                <div class="col-md-2 date-select">
                                    <div class="input-group date " style="margin-right: -10px;">
                                        <asp:TextBox ID="txtPaidDate" runat="server" placeholder="تاريخ الدفع ... " class="form-control"
                                            data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align: center;"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                    </div>
                                    <asp:RequiredFieldValidator ID="rfvPaidDate" SetFocusOnError="true" ControlToValidate="txtPaidDate" CssClass="required" Display="Dynamic" ErrorMessage="Please enter Paid Date." runat="server"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-2">
                                    <div class="keepmeLogged">
                                        <label class="switch">
                                            <asp:CheckBox ID="chkbxIsPaid" runat="server" Checked="true" />
                                            <%--<input name="RememberMe" type="checkbox" id="chkbxIsPaid" runat="server" checked="checked" />--%>
                                            <span class="slider round"></span>
                                            <span class="keepme">تم الدفع </span>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        <div class="clearfix"></div>
        <div class="container-fluid">
            <div align="left">
                <asp:Button ID="btnSave" runat="server" Text="تسليم الراتب" Style="font-size: medium" OnClick="btnSave_Click"
                    class="btn btn-info" ValidationGroup="g2" OnClientClick="return insertConfirmation();" />
                <a href="PageEmployeeSalaryList.aspx" style="font-size: medium" class="btn btn-danger">رجوع</a>
            </div>
            <br />
            <br />
        </div>

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

        <script src="../../PageEmployeeSalarySave.js"></script>
        <asp:HiddenField ID="HFPhone" runat="server" />
        <asp:HiddenField ID="HFEmail" runat="server" />
</asp:Content>

