<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/WSM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageStatisticsMony.aspx.cs" Inherits="Cpanel_ERP_WSM_Statistics_PageStatisticsMony" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterWSM.ascx" TagPrefix="uc1" TagName="WUCFooterWSM" %>

<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<%@ Import Namespace="Library_CLS_Arn.WSM" %>
<%@ Import Namespace="Library_CLS_Arn.WSM.Repostry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnGet.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
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
    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="">قائمة الإحصاء المالي للتبرع العيني</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة الإحصاء المالي للتبرع العيني
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
                                    <h5><i class="fa fa-star"></i> فرز أوامر لــ : </h5>
                                    <asp:DropDownList ID="DLType" runat="server" ValidationGroup="g2" Width="100%"
                                        CssClass="form-control2 chzn-select dropdown" Enabled="false"
                                            Style="font-size: 12px;">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">أمر صرف لمستفيد</asp:ListItem>
                                            <asp:ListItem Value="2">أمر صرف لموظف</asp:ListItem>
                                            <asp:ListItem Value="3">تالف</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblType" runat="server" Text="حدد الامر * " ForeColor="Red" Visible="false"></asp:Label>
                                </div>

                                <div class="form-group">
                                    <h5><i class="fa fa-star"></i>سنوات الإرشيف : </h5>
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
                                    <h5><i class="fa fa-star"></i>حدد المشروع : </h5>
                                    <div class="checkbox checkbox-primary">
                                        <asp:CheckBoxList ID="CBCategory" runat="server"
                                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i>من تاريخ : </h5>
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
                                        <h5><i class="fa fa-star"></i>إلى تاريخ : </h5>
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
                        <asp:Panel ID="pnlData" runat="server" Visible="False" Direction="RightToLeft">
                            <div class="table table-responsive" runat="server" id="pnlDataPrint" dir="rtl">
                                <div class="HideNow">
                                    <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                </div>
                                <table class='table' style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <div align="center" class="w">
                                                    <div class="col-lg-11">
                                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="قائمة فرز أوامر الصرف" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-1 HideThis">
                                                        <asp:LinkButton ID="LBGetFilter" runat="server" OnClick="LBGetFilter_Click" data-toggle="tooltip" title="جلب قائمة الفلترة"> <i class="fa fa-refresh"></i> </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <div class="col-lg-6 col-md-6 col-sm-6">
                                                    <a data-toggle="tooltip" title="إجمالي شحنات المستودع">
                                                        <div class="tile tile-heading" style="border-radius:8px">
                                                            <div class="tile-body">
                                                                <h4>إجمالي شحنات المستودع : 
                                                                    <asp:Label ID="lblSumIn_Kind_Donation" runat="server"></asp:Label>
                                                                </h4>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 15%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">المجموع
                                                                        </td>
                                                                        <td style="width: 65%; border: thin double #808080; border-width: 1px;" align="center">
                                                                            <asp:TextBox ID="lblSumWordIn_Kind_Donation" runat="server" Text="0" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                                                            <asp:Label ID="lblSumIn_Kind_Donation2" runat="server" Text="0" Style=' font-size: 12px'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                
                                                            </div>
                                                        </div>
                                                    </a>
                                                </div>
                                                <div class="col-lg-6 col-md-6 col-sm-6">
                                                    <a data-toggle="tooltip" title="إجمالي ماتم صرفه">
                                                        <div class="tile tile-heading" style="border-radius:8px">
                                                            <div class="tile-body">
                                                                <h4>إجمالي ماتم صرفه : 
                                                                    <asp:Label ID="lblSumExchange_Order" runat="server"></asp:Label>
                                                                </h4>
                                                                <table style="width: 100%;">
                                                                    <tr>
                                                                        <td style="width: 15%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">المجموع
                                                                        </td>
                                                                        <td style="width: 65%; border: thin double #808080; border-width: 1px;" align="center">
                                                                            <asp:TextBox ID="lblSumWordExchange_Order" runat="server" Text="0" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                                                            <asp:Label ID="lblSumExchange_Order2" runat="server" Text="0" Style=' font-size: 12px'></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </a>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <span style="font-size: 12px; padding-right: 5px">إرشيف 
                                                <asp:Label ID="lbl_Years" runat="server"></asp:Label>
                                                </span>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th></th>
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
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnlNull" runat="server" Visible="False">
                            <hr />
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

