<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageStatisticsGeneralMony.ascx.cs" Inherits="Shaerd_ERP_FMS_Statistics_PageStatisticsGeneralMony" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterWSM.ascx" TagPrefix="uc1" TagName="WUCFooterWSM" %>
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
        }

        @font-face {
            font-family: 'Alwatan';
            font-size: 18px;
            src: url(/fonts/AlWatanHeadlines-Bold.ttf);
        }

        @media screen and (min-width: 768px) {
            .WidthMaglis24 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }

            .Width10Percint {
                float: right;
                Width: 15%;
                padding-right: 5px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthMaglis {
                Width: 95%;
            }

            .Width10Percint {
                Width: 95%;
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
                <li><a href="PageStatisticsGeneralMony.aspx">قائمة الإحصاء المالي للأموال</a></li>
            </ul>
        </div>
    </div>
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-heading">
                <h3 class="panel-title">
                    <i class="fa fa-list"></i>قائمة الإحصاء المالي للأموال
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
                            <script type="text/javascript">
                                function jsYears(ch) {
                                    var allcheckboxes = document.getElementById('<%=CBYears.ClientID %>').getElementsByTagName("input");
                                    for (i = 0; i < allcheckboxes.length; i++)
                                        allcheckboxes[i].checked = ch.checked;
                                }
                            </script>
                            <div class="checkbox checkbox-primary" align="right">
                                <asp:CheckBox ID="CBCheckAllYears" onclick="jsYears(this)" runat="server"
                                    Text="حدد الكل" CssClass="styled" Checked="true" />
                            </div>
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
                            <h5><i class="fa fa-star"></i> حدد الحساب : </h5>
                            <script type="text/javascript">
                                function jsCategory(ch) {
                                    var allcheckboxes = document.getElementById('<%=CBAccount.ClientID %>').getElementsByTagName("input");
                                    for (i = 0; i < allcheckboxes.length; i++)
                                        allcheckboxes[i].checked = ch.checked;
                                }
                            </script>
                            <div class="checkbox checkbox-primary" align="right">
                                <asp:CheckBox ID="CBCheckAllCategory" onclick="jsCategory(this)" runat="server"
                                    Text="حدد الكل" CssClass="styled" Checked="true" />
                            </div>
                            <div class="checkbox checkbox-primary">
                                <asp:CheckBoxList ID="CBAccount" runat="server"
                                    RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                    <asp:ListItem Value="الصندوق">الصندوق</asp:ListItem>
                                    <asp:ListItem Value="البنك">البنك</asp:ListItem>
                                    <asp:ListItem Value="تبرع_عام">تبرع_عام</asp:ListItem>
                                    <asp:ListItem Value="مصاريف_تشغيلية">مصاريف_تشغيلية</asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="col-sm-12">
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
                        </div>
                        <div class="col-sm-12">
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
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="col-sm-12">
                            <br />
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
                    <div align="center" class="w">
                        <div align="center" class="w">
                            <div class="col-lg-11">
                                <asp:TextBox ID="txtTitleReceipt" runat="server" class="form-control" Text="قائمة الحساب" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                            </div>
                            <div class="col-lg-1 HideThis">
                                <asp:LinkButton ID="LBGetFilter" runat="server" OnClick="LBGetFilter_Click" data-toggle="tooltip" title="جلب قائمة الفلترة"> <i class="fa fa-refresh"></i> </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                        <thead>
                            <tr class="th">
                                <th class="StyleTD" style="width:10%;">م
                                </th>
                                <th class="StyleTD" style="width:70%;">الحساب
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
                                        <asp:Label ID="lblboxReceipt" runat="server"></asp:Label>
                                    </span>
                                </td>
                                <th class="StyleTD th">
                                    <asp:Label ID="lblSumboxReceipt" runat="server" 
                                        Text=''></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td style="width: 10px;" class="StyleTD">
                                    <span style="margin-right: 5px; font-size: 11px">2</span>
                                </td>
                                <td class="StyleTD">
                                    <span style="font-size: 11px">
                                        <asp:Label ID="lblbankReceipt" runat="server"></asp:Label>
                                    </span>
                                </td>
                                <th class="StyleTD th">
                                    <asp:Label ID="lblSumbankReceipt" runat="server" 
                                        Text=''></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td style="width: 10px;" class="StyleTD">
                                    <span style="margin-right: 5px; font-size: 11px">3</span>
                                </td>
                                <td class="StyleTD">
                                    <span style="font-size: 11px"><asp:Label ID="lblDonate_PublicReceipt" runat="server"></asp:Label></span>
                                </td>
                                <th class="StyleTD th">
                                    <asp:Label ID="lblSumDonate_PublicReceipt" runat="server" 
                                        Text=''></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <th colspan="3">
                                    <table class=' table-bordered table-condensed' style="width: 100%">
                                        <thead>
                                            <tr class="th">
                                                <th class="StyleTD" style="width:25%;">
                                                    سندات القبض
                                                </th>
                                                <th class="StyleTD" style="width:25%;">
                                                    <asp:Label ID="lblSumReceiptAll" runat="server"></asp:Label>
                                                </th>
                                                <th class="StyleTD" style="width:25%;">
                                                    التبرع النقدي
                                                </th>
                                                <th class="StyleTD" style="width:25%;">
                                                    <asp:Label ID="lblSumCash_DonationAll" runat="server"></asp:Label>
                                                </th>
                                            </tr>
                                        </thead>
                                    </table>
                                </th>
                            </tr>
                            <tr>
                                <th class="StyleTD">
                                    المجموع
                                </th>
                                <th class="StyleTD">
                                    <asp:Label ID="lblSumWordReceipt" runat="server" Text="0" Font-Size="12px"></asp:Label>
                                </th>
                                <th class="StyleTD">
                                    <asp:Label ID="lbl_SumReceipt" runat="server" Text="0"></asp:Label>
                                </th>
                            </tr>
                        </tbody>
                    </table>
                    <hr />
                    <div align="center" class="w">
                        <div align="center" class="w">
                            <asp:TextBox ID="txtTitleCashing" runat="server" class="form-control" Text="قائمة الحساب" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                        </div>
                    </div>
                    <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                        <thead>
                            <tr class="th">
                                <th class="StyleTD" style="width:10%;">م
                                </th>
                                <th class="StyleTD" style="width:70%;">الحساب
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
                                    <span style="font-size: 11px"><asp:Label ID="lblboxCashing" runat="server"></asp:Label></span>
                                </td>
                                <th class="StyleTD th">
                                    <asp:Label ID="lblSumboxCashing" runat="server" 
                                        Text=''></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td style="width: 10px;" class="StyleTD">
                                    <span style="margin-right: 5px; font-size: 11px">2</span>
                                </td>
                                <td class="StyleTD">
                                    <span style="font-size: 11px"><asp:Label ID="lblbankCashing" runat="server"></asp:Label></span>
                                </td>
                                <th class="StyleTD th">
                                    <asp:Label ID="lblSumbankCashing" runat="server" 
                                        Text=''></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <td style="width: 10px;" class="StyleTD">
                                    <span style="margin-right: 5px; font-size: 11px">3</span>
                                </td>
                                <td class="StyleTD">
                                    <span style="font-size: 11px"><asp:Label ID="lblDonate_PublicCashing" runat="server"></asp:Label></span>
                                </td>
                                <th class="StyleTD th">
                                    <asp:Label ID="lblSumDonate_PublicCashing" runat="server" 
                                        Text=''></asp:Label>
                                </th>
                            </tr>
                            <tr>
                                <th colspan="3">
                                    <table class=' table-bordered table-condensed' style="width: 100%">
                                        <thead>
                                            <tr class="th">
                                                <th class="StyleTD" style="width:12.5%;">
                                                    سندات الصرف
                                                </th>
                                                <th class="StyleTD" style="width:12.5%;">
                                                    <asp:Label ID="lblSumCashingAll" runat="server"></asp:Label>
                                                </th>
                                                <th class="StyleTD" style="width:12.5%;">
                                                    البناء والترميم
                                                </th>
                                                <th class="StyleTD" style="width:12.5%;">
                                                    <asp:Label ID="lblSumRestorationAndConstructionAll" runat="server"></asp:Label>
                                                </th>
                                                <th class="StyleTD" style="width:12.5%;">
                                                    الدعم النقدي
                                                </th>
                                                <th class="StyleTD" style="width:12.5%;">
                                                    <asp:Label ID="lblSumSupportForPrismAll" runat="server"></asp:Label>
                                                </th>
                                                <th class="StyleTD" style="width:12.5%;">
                                                    المصاريف التشغيلية
                                                </th>
                                                <th class="StyleTD" style="width:12.5%;">
                                                    <asp:Label ID="lblSumOperating_ExpensesAll" runat="server"></asp:Label>
                                                </th>
                                            </tr>
                                        </thead>
                                    </table>
                                </th>
                            </tr>
                            <tr>
                                <th class="StyleTD">
                                    المجموع
                                </th>
                                <th class="StyleTD">
                                    <asp:Label ID="lblSumWordCashing" runat="server" Text="0" Font-Size="12px"></asp:Label>
                                </th>
                                <th class="StyleTD">
                                    <asp:Label ID="lbl_SumCashing" runat="server" Text="0"></asp:Label>
                                </th>
                            </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <th colspan="3">
                                    <span style="font-size: 12px; padding-right: 5px">إرشيف 
                                    <asp:Label ID="lbl_Years" runat="server"></asp:Label>
                                    </span>                                                      
                                </th>
                            </tr>
                        </tfoot>
                    </table>
                    <div class="hide">
                        <hr style='border: solid; border-width: 1px; width: 100%' />
                        <div class="container-fluid" dir="rtl" runat="server">
                            <uc1:WUCFooterWSM runat="server" ID="WUCFooterWSM" />
                        </div>
                        <hr style='border: solid; border-width: 1px; width: 100%' />
                        <div class="HideNow">
                            <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
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