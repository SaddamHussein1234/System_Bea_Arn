<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageEmployeeOvertimeList.ascx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeOvertimes_PageEmployeeOvertimeList" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/ERP/WUCFooterBottomERP.ascx" TagPrefix="uc1" TagName="WUCFooterBottomERP" %>

<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<style type="text/css">
    #main-report {
        width: 100%;
    }

    table {
        color: #333;
        width: 640px;
        border-collapse: collapse;
        border-spacing: 0;
    }

    td, th {
        border: 1px solid transparent; /* No more visible border */
    }

    th {
        background: #DFDFDF; /* Darken header a bit */
    }

    td {
        background: #FAFAFA;
        text-align: center;
    }
    /* Cells in even rows (2,4,6...) are one color */
    tr:nth-child(even) td {
        background: #F1F1F1;
    }
    /* Cells in odd rows (1,3,5...) are another (excludes header cells)  */
    tr:nth-child(odd) td {
        background: #FEFEFE;
    }

    table, tr, td, th, tbody, thead, tfoot {
        page-break-inside: avoid !important;
    }
</style>

<style type="text/css">
    @media print {
        th {
            font-family: Arial;
            font-size: 16px;
            color: black;
            background-color: lightgrey;
        }

        thead {
            display: table-header-group;
        }

        tbody {
            display: table-row-group;
        }
    }
</style>

<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                title="طباعة">
            <i class="fa fa-print"></i></asp:LinkButton>
            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
                title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
        </div>
        <h1>لوحة التحكم</h1>
        <ul class="breadcrumb">
            <li><a href="../../Default.aspx">الرئيسية</a></li>
            <li><a href="PageEmployeeOvertimeList.aspx">مسير العمل الإضافي </a></li>
        </ul>
    </div>
</div>
<div class="container-fluid">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="lbmsg" runat="server" Text="مسير العمل الإضافي"></asp:Label>
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
                                            <th class="StyleTD">الإدارة
                                            </th>
                                            <th class="StyleTD">إسم الموظف
                                            </th>

                                            <th class="StyleTD">ر/الموظف
                                            </th>
                                            <th class="StyleTD">الوظيفة
                                            </th>
                                            <th class="StyleTD">التاريخ
                                            </th>
                                            <th class="StyleTD">الوقت
                                            </th>
                                            <th class="StyleTD">ساعات في اليوم
                                            </th>
                                            <th class="StyleTD">عدد الأيام
                                            </th>
                                            <th class="StyleTD">ر/القرار
                                            </th>
                                            <th class="StyleTD">المبلغ
                                            </th>

                                            <th class="StyleTD">التوقيع
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="GVEmployeeOvertime" runat="server" OnPreRender="GVEmployeeOvertime_PreRender">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width: 10px;" class="StyleTD">
                                                        <span style="margin-right: 5px; font-size: 11px"><%# Container.ItemIndex + 1 %></span>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <span style="font-size: 11px"><%# Eval("Department")%></span>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <span style="font-size: 11px"><%# Eval("_Name")%></span>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <span style="font-size: 12px"><%# Eval("EmployeeNo")%></span>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <span style="font-size: 11px"><%# Eval("Designation")%></span>
                                                    </td>
                                                    <th class="StyleTD th">
                                                        <span style="font-size: 12px"><%# Eval("Start_Date_", "{0:MM/dd/yyyy}") %></span><br />
                                                        <span style="font-size: 12px"><%# Eval("End_Date_", "{0:MM/dd/yyyy}") %></span>
                                                    </th>
                                                    <th class="StyleTD th">
                                                        <span style="font-size: 12px"><%# Eval("Start_Time_") %></span><br />
                                                        <span style="font-size: 12px"><%# Eval("End_Time_") %></span>
                                                    </th>
                                                    <td class="StyleTD">
                                                        <%# Eval("Hours_In_Day_") %>
                                                        /<span style="font-size: 11px">ساعات</span>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <%# ClassSaddam.FFilterNumber(Convert.ToString(Eval("TotalDays"))) %>
                                                        /<span style="font-size: 11px">يوم</span>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <span style="font-size: 12px"><%# Eval("Number_OverTime_")%></span>
                                                    </td>
                                                    <th class="StyleTD th">
                                                        <asp:Label ID="lblTotal_Amount" runat="server" 
                                                            Text='<%# Eval("Total_Amount")%>'></asp:Label> <%# ClassSaddam.FGetMonySa() %>
                                                    </th>
                                                    <td class="StyleTD">
                                                        <img src='/<%# Eval("Img_Signature_")%>' width='100px' height='30' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <tr>
                                            <th class="StyleTD" colspan="2">
                                                المجموع
                                            </th>
                                            <th class="StyleTD" colspan="8">
                                                <asp:Label ID="lblSumWord" runat="server" Text="0"></asp:Label>
                                            </th>
                                            <th class="StyleTD" colspan="2">
                                                <asp:Label ID="lbl_Sum" runat="server" Text="0"></asp:Label>
                                            </th>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th colspan="12">
                                                <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                                                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>                                                      
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>
                                <div class="hide">
                                    <div class="container-fluid " dir="rtl" runat="server">
                                        <hr style='border: solid; border-width: 1px; width: 100%' />
                                        <uc1:WUCFooterBottomERP runat="server" ID="WUCFooterBottomERP" />
                                    </div>
                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                    <div class="HideNow">
                                        <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                    </div>
                                </div>
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
                                <h3 style="font-size: 20px">إدخل جملة البحث ... 
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