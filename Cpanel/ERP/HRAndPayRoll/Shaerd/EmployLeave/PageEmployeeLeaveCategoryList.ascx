<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageEmployeeLeaveCategoryList.ascx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Shaerd_EmployLeave_PageEmployeeLeaveCategoryList" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/ERP/WUCFooterBottomERP.ascx" TagPrefix="uc1" TagName="WUCFooterBottomERP" %>

<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<style type="text/css">
        .bl {
            color: #fff;
        }

        .fo {
            font-size: 12px;
        }

        @media screen and (min-width: 768px) {
            .WidthText2 {
                Width: 250px;
                height: 36px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthText2 {
                Width: 150px;
                height: 36px;
            }
        }

        @media screen and (min-width: 768px) {
            .WidthTex {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }

            .WidthText {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }

            .WidthText3 {
                float: right;
                Width: 19%;
                padding-right: 5px;
            }

            .WidthText1 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }

            .WidthText4 {
                float: right;
                Width: 50%;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthTex {
                Width: 95%;
            }

            .WidthText {
                Width: 95%;
            }

            .WidthText1 {
                Width: 95%;
            }

            .WidthText3 {
                Width: 95%;
            }

            .WidthText4 {
                Width: 95%;
            }
        }

        @media screen and (min-width: 768px) {
            .WidthMaglis {
                float: right;
                Width: 19%;
                padding-right: 5px;
            }

            .WidthMaglis24 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthMaglis {
                Width: 95%;
            }

            .WidthMaglis24 {
                Width: 95%;
            }
        }

        .HideNow {
            display: none;
        }
    </style>
<style>
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
        <div class="container-fluid">
            <h1>لوحة التحكم</h1>
            <ul class="breadcrumb">
                <li><a href="Default.aspx">الرئيسية</a></li>
                <li><a href="PageEmployeeLeaveCategoryList.aspx">رصيد الاجازات</a></li>
            </ul>
        </div>
    </div>
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title" style="float: right">
                    <i class="fa fa-list"></i>رصيد الاجازات
                </h3>
                <div align="left">
                    <label class="control-label">
                        الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                    </label>
                    <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2" AutoPostBack="true"
                        Width="100" ValidationGroup="g2" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
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
                <asp:Panel ID="pnlData" runat="server" Visible="False" Direction="RightToLeft">
                    <div class="table table-responsive" id="pnlPrint" runat="server" dir="rtl">
                        <div class="HideNow">
                            <uc1:WUCHeader runat="server" ID="WUCHeader" />
                        </div>
                        <div align="center" class="w">
                            <div>
                                <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث"
                                    Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"
                                    Text="قائمة رصيد الاجازات"></asp:TextBox>
                            </div>
                        </div>
                        <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                            <thead>
                                <tr class="th">
                                    <th>
                                                
                                    </th>
                                    <th>
                                                
                                    </th>
                                    <th>
                                                
                                    </th>

                                    <th colspan="3" style="text-align:center;" class="StyleTD">
                                        الإجازات الاعتيادية
                                    </th>

                                    <th colspan="3" style="text-align:center;" class="StyleTD">
                                        الإجازات الاضطرارية
                                    </th>

                                    <th colspan="3" style="text-align:center;" class="StyleTD">
                                        الإجازات التعويضية
                                    </th>

                                    <th>
                                                
                                    </th>
                                </tr>
                                <tr class="th">
                                    <th class="StyleTD">
                                        م
                                    </th>
                                    <th class="StyleTD">
                                        الإدارة
                                    </th>
                                    <th class="StyleTD">
                                        إسم الموظف
                                    </th>

                                    <th class="StyleTD">
                                        رصيد
                                    </th>
                                    <th class="StyleTD">
                                        سحب
                                    </th>
                                    <th class="StyleTD">
                                        متبقي
                                    </th>

                                    <th class="StyleTD">
                                        رصيد
                                    </th>
                                    <th class="StyleTD">
                                        سحب
                                    </th>
                                    <th class="StyleTD">
                                        متبقي
                                    </th>

                                    <th class="StyleTD">
                                        رصيد
                                    </th>
                                    <th class="StyleTD">
                                        سحب
                                    </th>
                                    <th class="StyleTD">
                                        متبقي
                                    </th>

                                    <th class="StyleTD">
                                        الرصيد المتبقي
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="RPEmpLeave" runat="server" OnPreRender="RPEmpLeave_PreRender">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="width:10px;" class="StyleTD">
                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.ItemIndex + 1 %></span>
                                            </td>
                                            <td class="StyleTD">
                                                <span style="font-size: 11px"><%# Eval("Department")%></span>
                                            </td>
                                            <td class="StyleTD">
                                                <span style="font-size: 11px"><%# Eval("_Name")%></span>
                                            </td>
                                            <td class="StyleTD">
                                                <asp:Label ID="lblCountLeave" runat="server" 
                                                            Text='<%# ClassSaddam.FFilterNumber(Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters.Repostry_FinancialYear_.FErp_FinancialYear_ByID(new Guid(ddlYears.SelectedValue), "Org")) %>'></asp:Label>/<span style="font-size: 11px">يوم</span>
                                            </td>
                                            <td class="StyleTD">
                                                <asp:Label ID="lblCountLeaveUse" runat="server" 
                                                            Text='<%# ClassSaddam.FFilterNumber(Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions.Repostry_EmployeeLeaveCategory_.BErp_EmployeeLeaveCategory_SumByEmp(new Guid(Eval("EmployeeID").ToString()), new Guid(ddlYears.SelectedValue), 1)) %>'></asp:Label>/<span style="font-size: 11px">يوم</span>
                                            </td>
                                            <th class="StyleTD th">
                                                <asp:Label ID="lblCountLeaveUseAllow" runat="server"></asp:Label>/<span style="font-size: 11px">يوم</span>
                                            </th>

                                            <td class="StyleTD">
                                                <asp:Label ID="lblCountEmergency" runat="server" 
                                                            Text='<%# ClassSaddam.FFilterNumber(Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters.Repostry_FinancialYear_.FErp_FinancialYear_ByID(new Guid(ddlYears.SelectedValue), "Emg")) %>'></asp:Label>/<span style="font-size: 11px">يوم</span>
                                            </td>
                                            <td class="StyleTD">
                                                <asp:Label ID="lblCountEmergencyUse" runat="server" 
                                                            Text='<%# ClassSaddam.FFilterNumber(Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions.Repostry_EmployeeLeaveCategory_.BErp_EmployeeLeaveCategory_SumByEmp(new Guid(Eval("EmployeeID").ToString()), new Guid(ddlYears.SelectedValue), 4)) %>'></asp:Label>/<span style="font-size: 11px">يوم</span>
                                            </td>
                                            <th class="StyleTD th">
                                                <asp:Label ID="lblCountEmergencyAllow" runat="server"></asp:Label>/<span style="font-size: 11px">يوم</span>
                                            </th>

                                            <td class="StyleTD">
                                                <asp:Label ID="lblCountCompensatory" runat="server" 
                                                            Text='<%# Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions.Repostry_EmployeeCompensatory_.BErp_EmployeeLeaveCompensatory_SumByEmp(new Guid(Eval("EmployeeID").ToString()), new Guid(ddlYears.SelectedValue)) %>'>

                                                        </asp:Label>/<span style="font-size: 11px">يوم</span>
                                            </td>
                                            <td class="StyleTD">
                                                <asp:Label ID="lblCountCompensatoryUse" runat="server" 
                                                            Text='<%# Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions.Repostry_EmployeeLeaveCategory_.BErp_EmployeeLeaveCategory_SumByEmp(new Guid(Eval("EmployeeID").ToString()), new Guid(ddlYears.SelectedValue), 2) %>'></asp:Label>/<span style="font-size: 11px">يوم</span>
                                            </td>
                                            <th class="StyleTD th">
                                                <asp:Label ID="lblCountCompensatoryUseAllow" runat="server"></asp:Label>/<span style="font-size: 11px">يوم</span>
                                            </th>
                                            <td class="StyleTD">
                                                <asp:Label ID="lblCountTotalAllow" runat="server"></asp:Label>
                                                        / <span style="font-size: 11px">يوم</span>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <th colspan="13">
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
<script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>