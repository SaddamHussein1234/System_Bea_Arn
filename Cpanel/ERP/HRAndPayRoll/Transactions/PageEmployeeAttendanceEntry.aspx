<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="PageEmployeeAttendanceEntry.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeAttendanceEntry" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>

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
    <script src="/EmployeeAttendanceEntry.js"></script>

    <script type="text/javascript">
        function ConfirmDelete() {
            if (confirm("هل تريد الإستمرار ؟") == true)
                return true;
            else
                return false;
        }
    </script>

    <style type="text/css">
        
        .Timer table td {
            padding: 0px !important;
            background-color: transparent !important;
            border: medium none !important;
            vertical-align: top !important;
            line-height: 8px !important;
        }

    .Timer table td input {
        height: 20px !important;
        background-color: transparent !important;
        color: #000000 !important;
        cursor: default !important;
    }

    .Timer table td img {
        vertical-align: top !important;
        height: 10px;
        width: 16px;
    }

        .attendaceTable table.dataTable .Timer table td input{
	        width:35px !important;
	        border:none !important;
        }
        .attendaceTable table.dataTable .Timer table td input:nth-child(2){
	        width:8px !important;
        }
    </style>
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
                    <li><a href="PageEmployeeAttendance.aspx">قائمة الحضور</a></li>
                    <li><a href="PageEmployeeAttendanceEntry.aspx">إضافة/تعديل حضور الموظفين </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة/تعديل حضور الموظفين"></asp:Label>
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
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label class="control-label" id="Label1" runat="server">
                                            الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2 chzn-select dropdown" AutoPostBack="true"
                                            Width="100%" ValidationGroup="g2" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="ddlYears" ValidationGroup="g2"
                                            CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد السنة" runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label">
                                            حدد الإدارة : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" Width="100%" ValidationGroup="g2"
                                            OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control2 chzn-select dropdown">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDepartment" SetFocusOnError="true" ControlToValidate="ddlDepartment" ValidationGroup="g2"
                                            CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الإدارة" runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">
                                            حدد الموظف :  <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label> <span id="lblPhone" runat="server"></span>
                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control2 chzn-select dropdown"
                                            OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="true" Width="100%" ValidationGroup="g2">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvEmployee" SetFocusOnError="true" ControlToValidate="ddlEmployee" ValidationGroup="g2"
                                            CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الموظف" runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
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
                                <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
                                <asp:GridView ID="gvEmployeeAttendance" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeAttendanceID"
                                        Width="100%" CssClass="footable"
                                        EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="اليوم" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <asp:Label ID="lblToDate" runat="server" Text='<%# Eval("AttendanceDate", "{0:ddd}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="التاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <asp:HiddenField runat="server" ID="hfEmployeeAttendanceId" Value='<%# Eval("EmployeeAttendanceID") %>' />
                                                <asp:Label ID="lblAttendanceDate" runat="server" Text='<%# Eval("AttendanceDate", "{0:dd/MM/yyyy}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="الأولى(حضور)" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <cc1:TimeSelector ID="tsTimeIn" Hour='<%# Eval("TimeInHours") %>' Minute='<%# Eval("TimeInMinutes") %>'
                                                    SelectedTimeFormat="Twelve" AmPm='<%# Eval("TimeInAMPM") == "AM" ? MKB.TimePicker.TimeSelector.AmPmSpec.AM : MKB.TimePicker.TimeSelector.AmPmSpec.AM %>' 
                                                    runat="server" DisplaySeconds="false" CssClass="Timer">
                                                </cc1:TimeSelector>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="الأولى(إنصراف)" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <cc1:TimeSelector ID="tsTimeOut" Hour='<%# Eval("TimeOutHours") %>' Minute='<%# Eval("TimeOutMinutes") %>' 
                                                    SelectedTimeFormat="Twelve" AmPm='<%# Eval("TimeOutAMPM") == "PM" ? MKB.TimePicker.TimeSelector.AmPmSpec.PM : MKB.TimePicker.TimeSelector.AmPmSpec.PM %>' 
                                                    runat="server" DisplaySeconds="false" CssClass="Timer">
                                                </cc1:TimeSelector>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="الثانية(حضور)" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <cc1:TimeSelector ID="tsTimeIn_Tow" Hour='<%# Eval("TimeIn_Tow_Hours") %>' Minute='<%# Eval("TimeIn_Tow_Minutes") %>'
                                                    SelectedTimeFormat="Twelve" AmPm='<%# Eval("TimeIn_Tow_AMPM") == "AM" ? MKB.TimePicker.TimeSelector.AmPmSpec.PM : MKB.TimePicker.TimeSelector.AmPmSpec.PM %>' 
                                                    runat="server" DisplaySeconds="false" CssClass="Timer">
                                                </cc1:TimeSelector>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="الثانية(إنصراف)" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <cc1:TimeSelector ID="tsTimeOut_Tow" Hour='<%# Eval("TimeOut_Tow_Hours") %>' Minute='<%# Eval("TimeOut_Tow_Minutes") %>' 
                                                    SelectedTimeFormat="Twelve" AmPm='<%# Eval("TimeOut_Tow_AMPM") == "PM" ? MKB.TimePicker.TimeSelector.AmPmSpec.PM : MKB.TimePicker.TimeSelector.AmPmSpec.PM %>' 
                                                    runat="server" DisplaySeconds="false" CssClass="Timer">
                                                </cc1:TimeSelector>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ساعات العمل" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtWorkingHours" runat="server" onKeyUp="EmployeeAttendanceEntry.checkDecimal(this);" Width="60"
                                                    Text='<%# Eval("WorkingHours") %>' Enabled='<%# ((Convert.ToInt32(Eval("AttendanceType")) == 2 || (Convert.ToInt32(Eval("AttendanceType")) == 3)) ? false : true) %>' CssClass="form-control" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ساعات الإضافي" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOvertimeHours" runat="server" onKeyUp="EmployeeAttendanceEntry.checkDecimal(this);" Width="60"
                                                    Text='<%# Eval("OvertimeHours") %>' CssClass="form-control" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="نوع العملية" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlAttendanceType" CssClass="form-control" AutoPostBack="true" Width="100"
                                                    OnSelectedIndexChanged="ddlAttendanceType_SelectedIndexChanged"  Font-Size="11px"
                                                    Enabled='<%# ((Convert.ToInt32(Eval("AttendanceType")) == 2 || (Convert.ToInt32(Eval("AttendanceType")) == 3)) ? false : true) %>' 
                                                    SelectedValue='<%# Eval("AttendanceType") %>' runat="server">
                                                    <asp:ListItem Value="4">حاضر</asp:ListItem>
                                                    <asp:ListItem Value="3">يوم الاجازة</asp:ListItem>
                                                    <asp:ListItem Value="1">غادر</asp:ListItem>
                                                    <asp:ListItem Value="2">إجازة الاسبوع</asp:ListItem>
                                                    <asp:ListItem Value="5">انتداب</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="الحضور" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAttendance"  Width="50" Text='<%# (((Convert.ToInt32(Eval("AttendanceType")) == 4) || (Convert.ToInt32(Eval("AttendanceType")) == 5)) ? "حاضر":"") %>' Visible='<%# (((Convert.ToInt32(Eval("AttendanceType")) == 4) || (Convert.ToInt32(Eval("AttendanceType")) == 5))? true:false) %>' runat="server" />
                                                <asp:DropDownList ID="ddlAttendance" CssClass="form-control" Visible='<%# ((Convert.ToInt32(Eval("AttendanceType"))== 1)? true:false) %>' SelectedValue='<%# Eval("Attendance") %>' runat="server">
                                                    <asp:ListItem Value="1.00">1</asp:ListItem>
                                                    <asp:ListItem Value="0.50">0.5</asp:ListItem>
                                                    <asp:ListItem Value="0.00">0</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="الوصف" HeaderStyle-ForeColor="#CCCCCC">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDescription" MaxLength="30" runat="server"  Width="110"
                                                    Text='<%# Eval("Description") %>' CssClass="form-control" />
                                            </ItemTemplate>
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div align="left" runat="server" visible="false" id="IDAdd">
                <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="font-size: medium" 
                    OnClientClick="return ConfirmDelete();"
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
        <asp:HiddenField ID="HFPhone" runat="server" />
        <asp:HiddenField ID="HFEmail" runat="server" />
</asp:Content>

