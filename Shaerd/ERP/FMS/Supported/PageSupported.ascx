<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageSupported.ascx.cs" Inherits="Shaerd_ERP_FMS_Supported_PageSupported" %>
<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterMony.ascx" TagPrefix="uc1" TagName="WUCFooterMony" %>

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
                        <li><a href="../">الرئيسية</a></li>
                        <li><a href="PageSupported.aspx">قائمة المشاريع المدعومة</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>
                            <asp:Label ID="lbmsg" runat="server" Text="قائمة المشاريع المدعومة"></asp:Label>
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
                            <div runat="server" visible="false" class="col-sm-3">
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
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i> حدد المشاريع : </h5>
                                    <script type="text/javascript">
                                        function jsCategory(ch) {
                                            var allcheckboxes = document.getElementById('<%=CBSupport.ClientID %>').getElementsByTagName("input");
                                            for (i = 0; i < allcheckboxes.length; i++)
                                                allcheckboxes[i].checked = ch.checked;
                                        }
                                    </script>
                                    <div class="checkbox checkbox-primary" align="right">
                                        <asp:CheckBox ID="CBCheckAllCategory" onclick="jsCategory(this)" runat="server"
                                            Text="حدد الكل" CssClass="styled" Checked="true" />
                                    </div>
                                    <div class="checkbox checkbox-primary">
                                        <asp:CheckBoxList ID="CBSupport" runat="server"
                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                            <asp:ListItem></asp:ListItem>
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
                            <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                <thead>
                                    <tr>
                                        <th colspan="5">
                                            <div class="HideNow">
                                                <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                            </div>
                                            <div align="center" class="w">
                                                <div align="center" class="w">
                                                    <div class="col-lg-11">
                                                        <asp:TextBox ID="txtTitleReceipt" runat="server" class="form-control" Text="قائمة المشاريع" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-1 HideThis">
                                                        <asp:LinkButton ID="LBGetFilter" runat="server" OnClick="LBGetFilter_Click" data-toggle="tooltip" title="جلب قائمة الفلترة"> <i class="fa fa-refresh"></i> </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </th>
                                    </tr>
                                    <tr class="th">
                                        <th class="StyleTD">م
                                        </th>
                                        <th class="StyleTD">المشروع
                                        </th>
                                        <th class="StyleTD">المبالغ_الواردة
                                        </th>
                                        <th class="StyleTD">المبالغ_المصروفة
                                        </th>
                                        <th class="StyleTD">المبالغ_المتبقية
                                        </th>
                                    </tr>
                                    <tr class="th">
                                        <th class="StyleTD">
                                        </th>
                                        <th class="StyleTD">
                                        </th>
                                        <th class="StyleTD">
                                            <table class='table-bordered table-condensed' style="width: 100%">
                                                <tr class="th">
                                                    <th class="StyleTD" style="width: 20%">
                                                        الصندوق
                                                    </th>
                                                    <th class="StyleTD" style="width: 20%">
                                                        البنك
                                                    </th>
                                                    <th class="StyleTD" style="width: 20%">
                                                        تبرع_عام
                                                    </th>
                                                    <th class="StyleTD" style="width: 20%">
                                                        مصاريف تشغيلية
                                                    </th>
                                                    <th class="StyleTD" style="width: 20%">
                                                        الإجمالي
                                                    </th>
                                                </tr>
                                            </table>
                                        </th>
                                        <th class="StyleTD">
                                            <table class='table-bordered table-condensed' style="width: 100%">
                                                <tr class="th">
                                                    <th class="StyleTD" style="width: 20%">
                                                        الصندوق
                                                    </th>
                                                    <th class="StyleTD" style="width: 20%">
                                                        البنك
                                                    </th>
                                                    <th class="StyleTD" style="width: 20%">
                                                        تبرع_عام
                                                    </th>
                                                    <th class="StyleTD" style="width: 20%">
                                                        مصاريف تشغيلية
                                                    </th>
                                                    <th class="StyleTD" style="width: 20%">
                                                        الإجمالي
                                                    </th>
                                                </tr>
                                            </table>
                                        </th>
                                        <th class="StyleTD">
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RPTSupported" runat="server" OnPreRender="RPTSupported_PreRender">
                                        <ItemTemplate>
                                            <tr>
                                                <td style="width: 10px;" class="StyleTD">
                                                    <span style="margin-right: 5px; font-size: 11px"><%# Container.ItemIndex + 1 %></span>
                                                </td>
                                                <td class="StyleTD">
                                                    <span style="font-size: 11px">
                                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("TypeAlDam") %>'></asp:Label>
                                                    </span>
                                                </td>
                                                <th class="StyleTD th">
                                                    <table class='' style="width: 100%">
                                                        <tr class="th">
                                                            <th class="StyleTD" style="width: 20%">
                                                                <asp:Label ID="lblGetSumBox" runat="server" Font-Size="12px"
                                                                    Text='<%# FGetSum((int) Eval("IDItem"), "الصندوق") %>'></asp:Label> <span style="font-size:8px;"><%# XMony %></span>
                                                            </th>
                                                            <th class="StyleTD" style="width: 20%">
                                                                <asp:Label ID="lblGetSumBank" runat="server" Font-Size="12px"
                                                                    Text='<%# FGetSum((int) Eval("IDItem"), "البنك") %>'></asp:Label> <span style="font-size:8px;"><%# XMony %></span>
                                                            </th>
                                                            <th class="StyleTD" style="width: 20%">
                                                                <asp:Label ID="lblGetSumGeneral" runat="server" Font-Size="12px"
                                                                    Text='<%# FGetSum((int) Eval("IDItem"), "تبرع_عام") %>'></asp:Label> <span style="font-size:8px;"><%# XMony %></span>
                                                            </th>
                                                            <th class="StyleTD" style="width: 20%">
                                                                <asp:Label ID="lblGetSumOperating_Expenses" runat="server" Font-Size="12px"
                                                                    Text='<%# FGetSum((int) Eval("IDItem"), "مصاريف_تشغيلية") %>'></asp:Label> <span style="font-size:8px;"><%# XMony %></span>
                                                            </th>
                                                            <th class="StyleTD" style="width: 20%">
                                                                <asp:Label ID="lblGetSum" runat="server" Font-Size="12px"
                                                                    Text='<%# FGetSum((int) Eval("IDItem")) %>'></asp:Label> <span style="font-size:8px;"><%# XMony %></span>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </th>
                                                <th class="StyleTD th">
                                                    <table class='' style="width: 100%">
                                                        <tr class="th">
                                                            <th class="StyleTD" style="width: 20%">
                                                                <asp:Label ID="lblSetSumBox" runat="server" Font-Size="12px"
                                                                    Text='<%# FSetSum((int) Eval("IDItem"), "الصندوق") %>'></asp:Label> <span style="font-size:8px;"><%# XMony %></span>
                                                            </th>
                                                            <th class="StyleTD" style="width: 20%">
                                                                <asp:Label ID="lblSetSumBank" runat="server" Font-Size="12px"
                                                                    Text='<%# FSetSum((int) Eval("IDItem"), "البنك") %>'></asp:Label> <span style="font-size:8px;"><%# XMony %></span>
                                                            </th>
                                                            <th class="StyleTD" style="width: 20%">
                                                                <asp:Label ID="lblSetSumGeneral" runat="server" Font-Size="12px"
                                                                    Text='<%# FSetSum((int) Eval("IDItem"), "تبرع_عام") %>'></asp:Label> <span style="font-size:8px;"><%# XMony %></span>
                                                            </th>
                                                            <th class="StyleTD" style="width: 20%">
                                                                <asp:Label ID="lblSetSumOperating_Expenses" runat="server" Font-Size="12px"
                                                                    Text='<%# FSetSum((int) Eval("IDItem"), "مصاريف_تشغيلية") %>'></asp:Label> <span style="font-size:8px;"><%# XMony %></span>
                                                            </th>
                                                            <th class="StyleTD">
                                                                <asp:Label ID="lblSetSum" runat="server" Font-Size="12px" 
                                                                    Text='<%# FSetSum((int) Eval("IDItem")) %>'></asp:Label> <span style="font-size:8px;"><%# XMony %></span>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </th>
                                                <th class="StyleTD th">
                                                    <asp:Label ID="lblAllSum" runat="server" Text='0' Font-Size="12px"></asp:Label> <span style="font-size:8px;"><%# XMony %></span>
                                                </th>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <tr>
                                        <th class="StyleTD">
                                            المتبقي
                                        </th>
                                        <th colspan="3" class="StyleTD">
                                            <asp:Label ID="lbl_SumWord" runat="server" Text="0" Font-Size="12px"></asp:Label>
                                        </th>
                                        <th class="StyleTD">
                                            <asp:Label ID="lbl_Sum_All" runat="server" Text="0"></asp:Label> 
                                            <asp:Label ID="lbl_Sum_Mony" runat="server" Font-Size="9px"></asp:Label>
                                        </th>
                                    </tr>
                                </tbody>
                            </table>
                            <hr />
                            <div class="hide">
                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                <div class="container-fluid" dir="rtl" runat="server">
                                    <uc1:WUCFooterMony runat="server" ID="WUCFooterMony" />
                                </div>
                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                <div class="HideNow">
                                    <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                </div>
                            </div>
                            </asp:Panel>
                        </div>
                        <asp:Panel ID="PnlNull" runat="server" Visible="False">
                            <hr />
                            <div align="center">
                                <h3 style="font-size: 20px">
                                    <i class="fa-3x fa-empire"></i> لا توجد نتائج ,,,
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