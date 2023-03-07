<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/EOS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageStatisticsByCustomers.aspx.cs" Inherits="Cpanel_ERP_EOS_PageStatistics_PageStatisticsByCustomers" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterWSM.ascx" TagPrefix="uc1" TagName="WUCFooterWSM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnGet.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
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
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="../">الرئيسية</a></li>
                        <li><a href="">قائمة الإحصاء المالي حسب المستفيد</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة الإحصاء المالي حسب المستفيد
                        </h3>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="IDFilter" runat="server" ScrollBars="Auto" Height="250">
                            <div class="col-sm-12">
                                <div id="IDMessageWarning" runat="server" visible="false" class="alert  alert-warning alert-dismissible" role="alert">
                                    <span class="badge badge-pill badge-warning">تحذير</span>
                                    <asp:Label ID="lblMessageWarning" runat="server"></asp:Label>
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div id="IDMessageSuccess" runat="server" visible="false" class="alert  alert-success alert-dismissible" role="alert">
                                    <span class="badge badge-pill badge-success">عملية ناجحة</span>
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> سنوات الإرشيف : </h5>
                                    <div class="checkbox checkbox-primary">
                                        <asp:CheckBoxList ID="CBYears" runat="server"
                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> حدد المشروع : </h5>
                                    <div class="checkbox checkbox-primary">
                                        <asp:CheckBoxList ID="CBCategory" runat="server"
                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> حدد المستفيد : </h5>
                                    <asp:DropDownList ID="DLName" runat="server" ValidationGroup="VGDetails" Width="100%" CssClass="form-control chzn-select dropdown"
                                            Style="font-size: 12px;">
                                        <asp:ListItem Value=""></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="DLName" ErrorMessage="* حدد المستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> من تاريخ : </h5>
                                    <div class="input-group date ">
                                        <asp:TextBox ID="txtDateFrom" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="VGDetails" Style="direction: ltr;"></asp:TextBox>
                                        <asp:Label ID="lblDateFrom" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                    </div>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtDateFrom" ErrorMessage="* حدد التاريخ" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> إلى تاريخ : </h5>
                                    <div class="input-group date ">
                                        <asp:TextBox ID="txtDateTo" runat="server" placeholder="إلى تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="VGDetails" Style="direction: ltr;"></asp:TextBox>
                                        <asp:Label ID="lblDateTo" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                    </div>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtDateTo" ErrorMessage="* حدد التاريخ" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="VGDetails" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnGet" runat="server" Text="بحث حسب الفلترة" Style="margin-right: 4px;"
                                        class="btn btn-info btn-fill " ValidationGroup="VGDetails" OnClick="btnGet_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="clearfix"></div>
                        <div class="table table-responsive" runat="server" id="pnlDataPrint" dir="rtl" visible="false">
                            <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                            <div class="HideNow">
                                <uc1:WUCHeader runat="server" ID="WUCHeader" />
                            </div>
                            <hr style='border: solid; border-width: 1px; width: 100%' />
                            <div class="w">
                                <div class="w" align="center">
                                    <div class="col-lg-11">
                                        <asp:Label ID="lblTitle" runat="server" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                                    </div>
                                    <div class="col-lg-1 HideThis">
                                        <asp:LinkButton ID="LBGetFilter" runat="server" OnClick="LBGetFilter_Click" data-toggle="tooltip" title="جلب قائمة الفلترة"> <i class="fa fa-refresh"></i> </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <hr style='border: solid; border-width: 1px; width: 100%' />
                            <br />
                            <div class="w">
                                <asp:Label ID="txtTitleReceipt" runat="server" Text="بيانات المستفيد : " placeholder="عنوان البحث" Style="text-align: right; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                            </div>
                            <table style="width: 100%">
                                <tr>
                                    <td class="StyleTD" colspan="2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">إسم المستفيد
                                                </td>
                                                <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:Label ID="lblNameMosTafeed" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <table style="width: 100%">
                                                        <tr>
                                                            <td style="width: 50%; border-left: double; border-width: 2px; border-color: #a1a0a0;">القرية
                                                            </td>
                                                            <td style="width: 50%">
                                                                <asp:Label ID="lblAlqariah" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="StyleTD" colspan="2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم السجل المدني
                                                </td>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:Label ID="lblNumberAlSegelAlMadany" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم المستفيد
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblNumberMostafeed" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="StyleTD" colspan="2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">الحالة
                                                </td>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">
                                                    <asp:Label ID="lblHalafAlMosTafeed" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%; border-left: double; border-width: 2px; border-color: #a1a0a0;">رقم الجوال
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <hr style='border: solid; border-width: 1px; width: 100%' />
                            <br /><br />
                            <asp:Label ID="Label2" runat="server" Text="كشف الحساب : " placeholder="عنوان البحث" Style="text-align: right; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:Label>
                            <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                <thead>
                                    <tr class="th">
                                        <th class="StyleTD" style="width:10%;">م
                                        </th>
                                        <th class="StyleTD" style="width:70%;">البيان
                                        </th>
                                        <th class="StyleTD" style="width:20%;">المبلغ
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td style="width: 10px;" class="StyleTD">
                                            <span style="margin-right: 5px; font-size: 11px">1</span>
                                        </td>
                                        <td class="StyleTD">
                                            <span style="font-size: 11px">
                                                إجمالي ما تم صرفة : (الدعم العيني) 
                                            </span>
                                        </td>
                                        <th class="StyleTD th">
                                            <asp:Label ID="lblSumExchange_Order" runat="server" 
                                                Text='0'></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px;" class="StyleTD">
                                            <span style="margin-right: 5px; font-size: 11px">2</span>
                                        </td>
                                        <td class="StyleTD">
                                            <span style="font-size: 11px">
                                                إجمالي ما تم صرفة : (البناء والترميم)
                                            </span>
                                        </td>
                                        <th class="StyleTD th">
                                            <asp:Label ID="lblSumBenaaAndTarmim" runat="server" 
                                                Text='0'></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td style="width: 10px;" class="StyleTD">
                                            <span style="margin-right: 5px; font-size: 11px">3</span>
                                        </td>
                                        <td class="StyleTD">
                                            <span style="font-size: 11px">
                                                إجمالي ما تم صرفة : (الدعم النقدي) 
                                            </span>
                                        </td>
                                        <th class="StyleTD th">
                                            <asp:Label ID="lblSumSupportForPrisms" runat="server" 
                                                Text='0'></asp:Label>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th class="StyleTD">
                                            المجموع
                                        </th>
                                        <th class="StyleTD">
                                            <asp:Label ID="lblSumWord" runat="server" Text="0"></asp:Label>
                                        </th>
                                        <th class="StyleTD">
                                            <asp:Label ID="lbl_Sum" runat="server" Text="0"></asp:Label>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="hide">
                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                <div class="container-fluid" dir="rtl" runat="server">
                                    <uc1:WUCFooterWSM runat="server" ID="WUCFooterWSM" />
                                </div>
                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                <div class="HideNow">
                                    <footer>
                                        <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                    </footer>
                                </div>
                            </div>
                            </asp:Panel>
                        </div>
                        <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                            <hr />
                            <div align="center">
                                <h3 style="font-size: 20px">حدد البيانات
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
        <script type="text/javascript">
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
        </script>
        <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

