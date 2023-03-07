<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageEmployeeSalaryByView.ascx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeSalary_PageEmployeeSalaryByView" %>
<style type="text/css">
    .StyleTD {
        text-align: center;
        padding: 5px;
        border: double;
        border-width: 2px;
        border-color: #a1a0a0;
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

        .WidthText30 {
            float: right;
            Width: 16%;
            padding-right: 5px;
        }

        .WidthText2 {
            float: right;
            Width: 32%;
            padding-left: 5px;
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

        .Width10Percint {
            float: right;
            Width: 10%;
            padding-right: 5px;
        }

        .WidthText5 {
            float: right;
            Width: 100%;
        }


        .WidthText20 {
            Width: 150px;
            height: 36px;
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

        .WidthText2 {
            Width: 95%;
        }

        .WidthText3 {
            Width: 95%;
        }

        .Width10Percint {
            Width: 95%;
        }

        .WidthText30 {
            Width: 95%;
        }

        .WidthText4 {
            Width: 95%;
        }

        .WidthText5 {
            Width: 95%;
        }

        .WidthText20 {
            Width: 100px;
            height: 36px;
        }
    }

    .MarginBottom {
        margin-top: 15px;
    }
</style>

<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click" title="طباعة">
                <i class="fa fa-print"></i></asp:LinkButton>
            <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" OnClick="LBExit_Click"
                class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
            <div style="float: left" runat="server" visible="false">
                <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;" data-toggle="tooltip" title="بحث"
                    class="btn btn-info btn-fill pull-right" OnClick="btnSearch_Click" />
                &nbsp;
                <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthText20" placeholder=" رقم القرار ... "></asp:TextBox>
                <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2 chzn-select dropdown" Visible="false"
                    Width="100%" ValidationGroup="g2">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <h1>لوحة التحكم</h1>
        <ul class="breadcrumb">
            <li><a href="Default.aspx">الرئيسية</a></li>
            <li><a href="PageEmployeeSalaryList.aspx">رواتب الموظفين</a></li>
            <li><a href="#">تفاصيل راتب الموظف</a></li>
        </ul>
    </div>
</div>
<asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
    <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
        <table style="width: 100%;">
            <tr>
                <td align="center">
                    <asp:TextBox ID="txtTitle" runat="server" Font-Size="14px" class="form-control" placeholder="عنوان البحث" Text=" تفاصيل راتب الموظف" Style="text-align: center; width: 95%; background-color: #19d404; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                </td>
                <td align="center">
                    <a href='javaScript:void(0)' data-toggle='modal' data-target='#IDQRCode' title='تكبير'>
                        <asp:Image ID="ImgQRCode" runat="server" alt='QR Code' />
                    </a>
                    <div id="IDQRCode" class="modal fade in modal_New_Style HideThis">
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
                                                    <i class="fa fa-star"></i> QR Code : 
                                                </label><br />
                                                <div align="center">
                                                    <asp:Image ID="ImgQRCode2" runat="server" alt='صورة QRCode' style="width:300px; height:300px;" />
                                                </div>
                                                <div class='clearfix'></div>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-default" data-dismiss="modal">اغلاق</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:ScriptManager ID="smMain" runat="server"></asp:ScriptManager>
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfMonth" runat="server" />
                    <asp:HiddenField ID="hfEmployeeId" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <table style="width: 95%">
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الإدارة:
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الإسم
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblEmployeeName" runat="server"></asp:Label>
                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم الموظف:
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            <asp:Label ID="lblEmployeeNo" runat="server"></asp:Label>
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الشهر:
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:HiddenField ID="HMonth" runat="server" Value="0"></asp:HiddenField>
                                            <asp:Label ID="lblMonth" runat="server"></asp:Label>
                                            / 
                                            <asp:Label ID="lblYear" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">أيام الشهر:
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            <asp:Label ID="lblTotalDays" runat="server" CssClass="lblTotalDays" Text="0"></asp:Label>
                                            / يوم
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">أيام الحضور:
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblTotalPresentDays" runat="server" CssClass="lblTotalPresentDays" Text="0"></asp:Label>
                                            / يوم
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">نهاية الأسبوع:
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            <asp:Label ID="lblWeeklyOff" runat="server" Text="0"></asp:Label>
                                            / يوم
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الغياب:
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:HiddenField ID="hfAllowLeave" runat="server" Value="0"></asp:HiddenField>
                                            <asp:HiddenField ID="hfTotalUsedLeave" runat="server" Value="0"></asp:HiddenField>
                                            <asp:HiddenField ID="hfCalculateLeave" runat="server" Value="0"></asp:HiddenField>
                                            <asp:Label ID="lblLeave" runat="server" Text="0" />
                                            / يوم
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">خصم غياب:
                                            <asp:Label ID="lblTotalPaidLeaveSalary" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td style="width: 75%; border-width: 2px; border-color: #a1a0a0;">ساعات الدوام:
                                            <asp:Label ID="lblWorkingHours" runat="server" Text="0"></asp:Label>
                                            / ساعة من إجمالي 
                                    <asp:Label ID="lblOrgWorkingHours" runat="server" Text="0"></asp:Label>
                                            / ساعة
                                    / تأخير وإنصراف مبكر
                                    <asp:Label ID="lblGo" runat="server" Text="0"></asp:Label>
                                            / ساعة
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">خصم تأخير/إنصراف:
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            <asp:HiddenField ID="HFTotalGo" runat="server" />
                                            <asp:Label ID="lblTotalGo" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الإجازات الرسمية:
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:HiddenField ID="HFTotalHolidays" runat="server" />
                                            <asp:Label ID="lblTotalHolidays" runat="server" CssClass="lblTotalHolidays" Text="0"></asp:Label>
                                                / يوم
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الإنتدابات:
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            <asp:HiddenField ID="HFMandate" runat="server" />
                                                            <asp:Label ID="lblMandate" runat="server" Text="0"></asp:Label>
                                                            / يوم
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">مبلغ الإنتدابات:
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:HiddenField ID="HFTotlMandate" runat="server" />
                                                            <asp:Label ID="lblTotlMandate" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الحسومات:
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            <asp:HiddenField ID="HFResolved" runat="server" />
                                                            <asp:Label ID="lblResolved" runat="server" Text="0"></asp:Label>
                                                            / يوم
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">مبلغ الحسومات:
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:HiddenField ID="HFTotalResolved" runat="server" />
                                            <asp:Label ID="lblTotalResolved" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">ساعات الإضافي:
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            <asp:Label ID="lblTotalOverTimeDays" runat="server" Text="0"></asp:Label>
                                            / ساعة
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">مبلغ الإضافي:
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblTotalOverTimeSalary" runat="server" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">إجازات الموظف:
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            <asp:HiddenField ID="HFLeave2" runat="server" />
                                                            <asp:Label ID="lblLeave2" runat="server" Text="0"></asp:Label>
                                                            / يوم
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الراتب الأساسي:
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblBasic" Text="0" CssClass="lblBasic" runat="server"></asp:Label>
                                                            /
                                            <asp:Label ID="lblPaidBasic" Text="0" CssClass="lblPaidBasic" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <table style="width: 95%">
                        <tr>
                            <td class="StyleTD">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align:right; border-width: 2px; border-color: #a1a0a0;">
                                                <h3 class="panel-title">
                                                <i class="fa fa-list"></i>
                                                <asp:Label ID="Label3" runat="server" Text="البدلات(العلاوات) "></asp:Label>
                                            </h3>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="StyleTD">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align:right; border-width: 2px; border-color: #a1a0a0;">
                                                <h3 class="panel-title">
                                                <i class="fa fa-list"></i>
                                                <asp:Label ID="Label4" runat="server" Text="المستقطع من الراتب "></asp:Label>
                                            </h3>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD">
                                <table style="width: 100%">
                                    <asp:Repeater ID="rptAllowance" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    <%#Eval("AllowanceName") %>
                                                        / 
                                                    <span class="redFont"><%#Convert.ToBoolean(Eval("IsConsider"))?"يحتسب":"" %></span>
                                                </td>
                                                <td style="width: 50%; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:HiddenField ID="hfAllowanceId" runat="server" Value='<%#Eval("AllowanceID") %>' />
                                                    <asp:Label ID="lblAllowance" CssClass="lblAllowance" runat="server" Text='<%#String.Format("{0:0.00}",Eval("Amount")) %>' />
                                                    /
                                                <asp:Label ID="lblPaidAllowance" runat="server" Text='<%#String.Format("{0:0.00}",Eval("PaidAmount")) %>'></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                            <td class="StyleTD">
                                <table style="width: 100%">
                                    <asp:Repeater ID="rptDeduction" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    <%#Eval("DeductionName") %> /
                                                                    <span class="redFont"><%#Convert.ToBoolean( Eval("IsConsider"))?"يحتسب":"" %></span>
                                                </td>
                                                <td style="width: 50%; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:HiddenField ID="hfDeductionId" runat="server" Value='<%#Eval("DeductionID") %>' />
                                                    <asp:Label ID="lblDeduction" CssClass="lblDeduction" runat="server" Text='<%#String.Format("{0:0.00}",Eval("Amount")) %>' />
                                                                    /
                                                    <asp:Label ID="lblPaidDeduction" runat="server" Text='<%#String.Format("{0:0.00}",Eval("PaidAmount")) %>' CssClass="lblPaidDeduction"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD">
                                    <table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            الإجمالي:
                                            <asp:HiddenField ID="hfTotalAllowance" runat="server" Value="0" ClientIDMode="Static" />
                                                            <asp:Label ID="lblTotalAllowance" runat="server" CssClass="lblTotalAllowance" Text="0"></asp:Label>
                                        </td>
                                        <td style="width: 50%; border-width: 2px; border-color: #a1a0a0;">
                                            المستحق:
                                            <asp:Label ID="lblPaidTotalAllowance" runat="server" CssClass="lblPaidTotalAllowance" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="StyleTD">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            الإجمالي:
                                            <asp:HiddenField ID="hfTotalDeduction" runat="server" Value="0" ClientIDMode="Static" />
                                                            <asp:Label ID="lblTotalDeduction" runat="server" CssClass="lblTotalDeduction" Text="0"></asp:Label>
                                        </td>
                                        <td style="width: 50%; border-width: 2px; border-color: #a1a0a0;">
                                            المستحق:
                                            <asp:Label ID="lblPaidTotalDeduction" runat="server" CssClass="lblPaidTotalDeduction" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="text-align:right; border-width: 2px; border-color: #a1a0a0;">
                                                <h3 class="panel-title">
                                                <i class="fa fa-list"></i>
                                                <asp:Label ID="Label2" runat="server" Text="تفاصيل القروض"></asp:Label>
                                            </h3>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <asp:Repeater ID="rptLoan" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    <%#Eval("LoanTitle") %>
                                                </td>
                                                <td style="width: 50%; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:HiddenField ID="hfLoanId" runat="server" Value='<%# Eval("EmployeeLoanMapID") %>' />
                                                        <asp:Label ID="lblPendingLoan" CssClass="lblPendingLoan" runat="server"
                                                            Text='<%# Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Transactions.Repostry_EmployeePaidLoan_.FErp_EmployeePaidLoan_Manage(new Guid(Eval("EmployeeLoanMapID").ToString()), Convert.ToDecimal(Eval("Amount")))%>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">صافي الراتب:
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            <asp:Label ID="lblNetSalary" runat="server" CssClass="lblNetSalary" Text="0"></asp:Label>
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الضريبة المهنية:
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblProfessionalTax" runat="server" CssClass="lblProfessionalTax" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="StyleTD" colspan="2">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الراتب في متناول اليد:
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                            <asp:HiddenField ID="hfCalculateSalary" Value="0" runat="server" ClientIDMode="Static" />
                                                <asp:Label ID="lblOnHandSalary" runat="server" CssClass="lblOnHandSalary" Text="0"></asp:Label>
                                                        
                                                <asp:TextBox ID="txtOnHandSalary" runat="server" CssClass="displayNone txtOnHandSalary" Width="100" Style="display: none;"></asp:TextBox>
                                                <asp:CompareValidator ID="cvSalary" runat="server" ValueToCompare="0" ControlToValidate="txtOnHandSalary" Display="Dynamic"
                                                    ErrorMessage="Must set On Hand Salary grater than 0." CssClass="required" Operator="GreaterThanEqual" Type="Double"></asp:CompareValidator>
                                        </td>
                                        <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">تاريخ الدفع:
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="txtPaidDate" runat="server" CssClass="lblOnHandSalary" Text="0"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <%--<div class="keepmeLogged">
                                    <label class="switch">
                                        <asp:CheckBox ID="chkbxIsPaid" runat="server" Checked="true" />
                                        <span class="slider round"></span>
                                        <span class="keepme">تم الدفع </span>
                                    </label>
                                </div>--%>
                            </td>
                        </tr>
                    </table>
                    <div runat="server" id="IDKhatm" align="left" style="margin-top: -80px" visible="true">
                        <img src="<%=ResolveUrl("~/ImgSystem/ImgSignature/الختم.png")%>" />
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Panel>
<asp:Panel ID="pnlSelect" runat="server" Direction="RightToLeft" Visible="false">
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i class="fa fa-pencil"></i>
                    <asp:Label ID="Label6" runat="server" Text="يرجى إدخال رقم سجل صحيح"></asp:Label>
                </h3>
            </div>
            <div class="panel-body">
                <div class="content-box-large">
                    <div class="widget-box">
                        <div class="container-fluid" dir="rtl">
                            <asp:Panel ID="Panel1" runat="server">
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
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>