<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="PageEmployeeAttendanceListAll.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeAttendanceListAll" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/Cpanel/ERP/WUCFooterBottomERP.ascx" TagPrefix="uc1" TagName="WUCFooterBottomERP" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>

    <style type="text/css">

    #main-report{
        width: 100%;
    }
    table { 
        color: #333;width: 640px; border-collapse: collapse; border-spacing: 0; 
    }
    td, th { 
        border: 1px solid transparent; /* No more visible border */
    }
    th {
        background: #DFDFDF;  /* Darken header a bit */
    }
    td {
        background: #FAFAFA;
        text-align: center;
    }
    /* Cells in even rows (2,4,6...) are one color */ 
    tr:nth-child(even) td { background: #F1F1F1; }
    /* Cells in odd rows (1,3,5...) are another (excludes header cells)  */ 
    tr:nth-child(odd) td { background: #FEFEFE; }
    table, tr, td, th, tbody, thead, tfoot {
        page-break-inside: avoid !important;
    }
    </style>

    <style type="text/css">  
        @media print  {th{font-family:Arial; font-size:16px; color:black; background-color:lightgrey;}thead {display:table-header-group;}tbody {display:table-row-group;}}  
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
                        title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة">
                    <i class="fa fa-print"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../../Default.aspx">الرئيسية</a></li>
                    <li><a href="PageEmployeeAttendanceListAll.aspx">كشف الحضور والغياب </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="كشف الحضور والغياب"></asp:Label>
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
                                <div class="col-md-3">
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
                            <asp:HiddenField ID="HFCountDay" runat="server" />
                            <div class="container-fluid table-responsive" dir="rtl">
                                <asp:Panel ID="pnlData" runat="server" Visible="False" Direction="RightToLeft">
                                    <div class="table table-responsive" id="pnlPrint" runat="server" dir="rtl">
                                        <div class="HideNow">
                                            <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                        </div>
                                        <div align="center" class="w">
                                            <div>
                                                <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث"
                                                    Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"
                                                    Text="0"></asp:TextBox>
                                            </div>
                                        </div>
                                        <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                            <thead>
                                                <tr class="th">
                                                    <th class="StyleTD">م
                                                    </th>
                                                    <th class="StyleTD">الإسم
                                                    </th>
                                                    <th class="StyleTD" runat="server" id="IDTowPart1">
                                                        عدد الأيام
                                                    </th>
                                                    <th class="StyleTD" runat="server" id="IDTowPart2">
                                                        أيام الحضور
                                                    </th>
                                                    <th class="StyleTD">نهاية الاسبوع
                                                    </th>
                                                    <th class="StyleTD">اجازات الموظف
                                                    </th>
                                                    <th class="StyleTD">الإجازات الرسمية
                                                    </th>
                                                    <th class="StyleTD">الإنتدابات
                                                    </th>
                                                    <th class="StyleTD">
                                                        الحسومات
                                                    </th>
                                                    <th class="StyleTD">
                                                        ساعات العمل
                                                    </th>
                                                    <th class="StyleTD">
                                                        ساعات الإضافي
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="gvEmployeeAttendance" runat="server" OnPreRender="gvEmployeeAttendance_PreRender">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td style="width: 10px;" class="StyleTD">
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.ItemIndex + 1 %></span>
                                                            </td>
                                                            
                                                            <td class="StyleTD">
                                                                <span style="font-size: 12px"><%# Eval("EmpName")%></span>
                                                            </td>
                                                            <td class="StyleTD">
                                                                <span style="font-size: 12px"><%# HFCountDay.Value %> / يوم</span>
                                                            </td>
                                                            <td class="StyleTD">
                                                                <span style="font-size: 12px"><%# Eval("CountPresent") %> / يوم</span>
                                                            </td>
                                                            <td class="StyleTD">
                                                                <span style="font-size: 12px"><%# Eval("CountWeeklyOff") %> / يوم</span>
                                                            </td>
                                                            <td class="StyleTD">
                                                                <span style="font-size: 12px">
                                                                    <asp:Label ID="lblLeaveCategory" runat="server" Text='<%# Eval("CountLeaveCategory") %>'></asp:Label>
                                                                     / يوم</span>
                                                            </td>
                                                            <td class="StyleTD">
                                                                <span style="font-size: 12px"><%# Eval("CountHoliday") %> / يوم</span>
                                                            </td>
                                                            <td class="StyleTD">
                                                                <span style="font-size: 12px">
                                                                    <asp:Label ID="lblMandate" runat="server" Text='<%# Eval("CountMandate") %>'></asp:Label>
                                                                     / يوم</span>
                                                            </td>
                                                            <td class="StyleTD">
                                                                <span style="font-size: 12px">
                                                                    <asp:Label ID="lblResolved" runat="server" Text='<%# Eval("CountResolved") %>'></asp:Label>
                                                                     / يوم</span>
                                                            </td>
                                                            <td class="StyleTD">
                                                                <span style="font-size: 12px"><%# Eval("WorkingHours") %> / ساعة</span>
                                                            </td>
                                                            <td class="StyleTD">
                                                                <span style="font-size: 12px">
                                                                    <asp:Label ID="lblOverTime" runat="server" Text='<%# Eval("OverTimeHours") %>'></asp:Label>
                                                                     / ساعة</span>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <tr>
                                                    <th class="StyleTD" colspan="2">
                                                        أيام الشهر
                                                    </th>
                                                    <th class="StyleTD" colspan="3">
                                                        إجمالي إجازات الموظفين
                                                    </th>
                                                    <th class="StyleTD" colspan="2">
                                                        إجمالي الإنتدابات
                                                    </th>
                                                    <th class="StyleTD" colspan="2">
                                                        إجمالي الحسومات
                                                    </th>
                                                    <th class="StyleTD" colspan="2">
                                                        ساعات الإضافي
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <th class="StyleTD" colspan="2">
                                                        <asp:Label ID="lblCountDayInMonth" runat="server" Text="0"></asp:Label> / يوم
                                                    </th>
                                                    <th class="StyleTD" colspan="3">
                                                        <asp:Label ID="lblCountLeaveCategory" runat="server" Text="0"></asp:Label> / يوم
                                                    </th>
                                                    <th class="StyleTD" colspan="2">
                                                        <asp:Label ID="lblCountMandate" runat="server" Text="0"></asp:Label> / يوم
                                                    </th>
                                                    <th class="StyleTD" colspan="2">
                                                        <asp:Label ID="lblCountResolved" runat="server" Text="0"></asp:Label> / يوم
                                                    </th>
                                                    <th class="StyleTD" colspan="2">
                                                        <asp:Label ID="lblCountOverTime" runat="server" Text="0"></asp:Label> / ساعة
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td colspan="11">
                                                        <div class="container-fluid " dir="rtl" runat="server">
                                                            <hr style='border: solid; border-width: 1px; width: 100%' />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                            <tfoot>
                                                <tr>
                                                    <th colspan="11">
                                                        <span style="font-size: 12px; padding-right: 5px">عدد الموظفين : </span>
                                                        <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                        <br />
                                                        <hr style='border: solid; border-width: 1px; width: 100%' />
                                                        <div class="HideNow">
                                                            <uc1:WUCFooterBottomERP runat="server" ID="WUCFooterBottomERP" />
                                                        </div>
                                                    </th>
                                                </tr>
                                            </tfoot>
                                        </table>
                                    </div>
                                    <asp:HiddenField ID="hfCount" runat="server" Value="0" />
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
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </asp:Panel>
                                <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <div align="center">
                                        <h3 style="font-size: 20px">يُرجى تحديد البيانات ... 
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

