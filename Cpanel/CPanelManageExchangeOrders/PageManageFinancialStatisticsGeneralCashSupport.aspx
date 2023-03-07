<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/CPanelManageExchangeOrders/MPCPanel.master" AutoEventWireup="true" CodeFile="PageManageFinancialStatisticsGeneralCashSupport.aspx.cs" Inherits="Cpanel_CPanelManageExchangeOrders_PageManageFinancialStatisticsGeneralCashSupport" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterWSM.ascx" TagPrefix="uc1" TagName="WUCFooterWSM" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../GridView.css?v=2.2" rel="stylesheet" type="text/css" />
    <link href="../css/chosen.css" rel="stylesheet" />
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
    </style>
    <script src="/view/javascript/jquery.min.js"></script>
    <script src="/view/javascript/ShowProgressOnLoad.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="" data-toggle="tooltip" title="إضافة مستفيد" class="btn btn-primary" style="display:none"><i class="fa fa-plus"></i></a>
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
                        <li><a href="PageManageFinancialStatisticsGeneralCashSupport.aspx">قائمة الإحصاء المالي العام حسب الدعم النقدي</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة الإحصاء المالي العام حسب الدعم النقدي
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <div style="float: right; width: 230px">
                                <asp:Label ID="lbmsg" runat="server" Text="فرز أوامر"></asp:Label>
                                لــ :
                                <asp:DropDownList ID="DLType" runat="server" ValidationGroup="g2" CssClass="form-control2"
                                    Width="150px" Height="30px" Enabled="false">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">أمر صرف لمستفيد</asp:ListItem>
                                    <asp:ListItem Value="2">أمر صرف لموظف</asp:ListItem>
                                    <asp:ListItem Value="3">تالف</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblType" runat="server" Text="حدد الامر * " ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                            <div style="float: right; width: 150px">
                                <div class="col-sm-3">
                                    <div class="input-group date " style="margin-right: -10px;">
                                        <asp:TextBox ID="txtDateFrom" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; width: 100px"></asp:TextBox>
                                        <asp:Label ID="lblDateFrom" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <div style="float: right; width: 150px">
                                <div class="col-sm-3">
                                    <div class="input-group date " style="margin-right: -10px;">
                                        <asp:TextBox ID="txtDateTo" runat="server" placeholder="إلى تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; width: 100px"></asp:TextBox>
                                        <asp:Label ID="lblDateTo" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                            </div>
                            <asp:Button ID="btnGet" runat="server" Text="بحث" Style="margin-right: 4px;"
                                class="btn btn-info btn-fill " ValidationGroup="g2" OnClick="btnGet_Click" />
                        </div>
                        <asp:Panel ID="pnlData" runat="server" Visible="False" Direction="RightToLeft">
                            <div class="table table-responsive">
                                <div class="HideNow">
                                    <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                </div>
                                <table class='table' style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <div align="center" class="w">
                                                    <div>
                                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVFinancialStatistics" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVFinancialStatistics_RowDataBound"
                                                    UseAccessibleHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="رقم المشروع" HeaderStyle-ForeColor="#CCCCCC" Visible="true">
                                                            <ItemTemplate>
                                                                <%# Eval("IDItem")%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="المشروع" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# Eval("TypeAlDam")%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="القُرى المستفيدة" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <%# FGetCountAlQariah(Convert.ToInt32(Eval("IDItem")))%> / قرية
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="عدد الأسر المستفيدة" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                (
                                                                <asp:Label ID="lblCountAsrah" runat="server" Font-Size="12px" 
                                                                    Text='<%# FGetCountFamily(Convert.ToInt32(Eval("IDItem")))%>'></asp:Label>
                                                                ) أسره           
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>   
                                                        <asp:TemplateField HeaderText="عدد مرات الدعم" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                ( <%# FGetCard((Int32) (Eval("IDItem")))%> ) / مرة
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="المبلغ الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" 
                                                                    Text='<%# FPrice(Convert.ToInt32(Eval("IDItem")))%>'></asp:Label>
                                                                ريال
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                    <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" NextPageText=" التالي  "
                                                        PreviousPageText=" السابق - " PageButtonCount="30" />
                                                    <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" Font-Size="Large" />
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <RowStyle CssClass="rows"></RowStyle>
                                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                </asp:GridView>
                                                <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td style="width: 15%; border: thin double #808080; border-width: 1px; padding: 10px" align="center">المجموع : 
                                                        </td>
                                                        <td style="width: 65%; border: thin double #808080; border-width: 1px;" align="center">
                                                            <asp:TextBox ID="lblSumWord" runat="server" Text="0" class="form-control" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                        </td>
                                                        <td style="width: 20%; border: thin double #808080; border-width: 1px;" align="center">
                                                            <asp:Label ID="lblSum" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                            <asp:Label ID="Label5" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div>
                                                    <span style="font-size: 12px; padding-right: 5px">عدد الأسر المستفيدة : </span>
                                                    <asp:Label ID="lblCountAosar" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                    <span style="font-size: 12px; padding-right: 2px">أسره</span>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th>
                                                
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
                            </div>
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
        <script src="../css/chosen.jquery.js" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
</asp:Content>

