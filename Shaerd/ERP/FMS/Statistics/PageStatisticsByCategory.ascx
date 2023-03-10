<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageStatisticsByCategory.ascx.cs" Inherits="Shaerd_ERP_FMS_Statistics_PageStatisticsByCategory" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterWSM.ascx" TagPrefix="uc1" TagName="WUCFooterWSM" %>

<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<%@ Import Namespace="Library_CLS_Arn.WSM" %>
<%@ Import Namespace="Library_CLS_Arn.WSM.Repostry" %>
<%@ Import Namespace="Library_CLS_Arn.OM.Repostry" %>
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
                            <i class="fa fa-list"></i>قائمة الإحصاء المالي للتبرع للتبرع العيني
                        </h3>
                    </div>
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
                    <div class="clearfix"></div>
                    <div class="panel-body">
                        <asp:Panel ID="IDFilter" runat="server" ScrollBars="Auto" Height="250">
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
                                    <h5><i class="fa fa-star"></i> حدد المشروع : </h5>
                                    <script type="text/javascript">
                                        function jsCategory(ch) {
                                            var allcheckboxes = document.getElementById('<%=CBCategory.ClientID %>').getElementsByTagName("input");
                                            for (i = 0; i < allcheckboxes.length; i++)
                                                allcheckboxes[i].checked = ch.checked;
                                        }
                                    </script>
                                    <div class="checkbox checkbox-primary" align="right">
                                        <asp:CheckBox ID="CBCheckAllCategory" onclick="jsCategory(this)" runat="server"
                                            Text="حدد الكل" CssClass="styled" Checked="true" />
                                    </div>
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
                                                <asp:GridView ID="GVFinancialStatistics" runat="server" AutoGenerateColumns="False"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
                                                    OnRowDataBound="GVFinancialStatistics_RowDataBound"
                                                    UseAccessibleHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="10px" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الإرشيف" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-calendar"></i> 
                                                                <%# Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters.Repostry_FinancialYear_.FErp_FinancialYear_ByID(new Guid(Eval("IDYear").ToString()))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="المشروع" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-magic"></i> 
                                                                <%# ClassQuaem.FSupportType((Int32) (Eval("ID_Project")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الصنف" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-magic"></i> 
                                                                <%# WSM_ClassProduct.FProductName((Int32) (Eval("ID_Product")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الكمية" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-area-chart"></i>  
                                                                <%# Repostry_In_Kind_Donation_Details_.FGetCountProduct("GetStaticCountByProject", new Guid(Eval("IDYear").ToString()), Convert.ToInt64(Eval("ID_Product")),
                                                                    txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), string.Empty, Eval("ID_Project").ToString() , string.Empty) %> 
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="التجزئة" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-area-chart"></i>
                                                                <%# Repostry_In_Kind_Donation_Details_.FGetCountProduct("GetStaticCountPartitionByProject", new Guid(Eval("IDYear").ToString()), Convert.ToInt64(Eval("ID_Product")),
                                                                    txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), string.Empty, Eval("ID_Project").ToString() , string.Empty) %> / 
                                                                <small> <%# WSM_Repostry_Units_.FGetByIDProduct(Convert.ToInt64(Eval("ID_Product"))) %> </small>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="المبلغ الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-money"></i>
                                                                    <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" Text='<%# Repostry_In_Kind_Donation_Details_.FGetCountProduct("GetStaticSumByProject", new Guid(Eval("IDYear").ToString()), Convert.ToInt64(Eval("ID_Product")),
                                                                    txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), string.Empty, Eval("ID_Project").ToString() , string.Empty) %>'></asp:Label>
                                                                    <small>
                                                                        <%# ClassSaddam.FGetMonySa() %>
                                                                    </small>
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
                                                            <asp:Label ID="lblMony" runat="server" Style='color: Red; font-size: 12px'></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <div>
                                                    <span style="font-size: 12px; padding-right: 5px">
                                                إرشيف 
                                                <asp:Label ID="lbl_Years" runat="server"></asp:Label>
                                                        </span>
                                                </div>
                                                <div runat="server" id="pnlProduct" visible="true">
                                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                                    <div class="container-fluid" dir="rtl" runat="server">
                                                        <div class="WidthText3" style="text-align: right">
                                                            <h5>إجمالي التجزئة 
                                                            </h5>
                                                        </div>
                                                    </div>
                                                    <div class="container-fluid" dir="rtl">
                                                        <asp:Repeater ID="RPTProductAll" runat="server">
                                                            <ItemTemplate>
                                                                <div class='Width10Percint StyleTD' style="height: 50px;">
                                                                    <span style="font-size: 11px;"><%# WSM_ClassProduct.FProductName((Int32) (Eval("ID_Product")))%></span> : 
                                                                    <%# Repostry_In_Kind_Donation_Details_.FGetCountProduct("GetStaticCountPartitionByProjectAll", Guid.Empty, Convert.ToInt64(Eval("ID_Product")),
                                                                        txtDateFrom.Text.Trim(), txtDateTo.Text.Trim(), XYears.Substring(0, XYears.Length - 1), XCategory.Substring(0, XCategory.Length - 1) , string.Empty) %>
                                                                    <span style="font-size: 10px;"><%# WSM_Repostry_Units_.FGetByIDProduct(Convert.ToInt64(Eval("ID_Product"))) %></span>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
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