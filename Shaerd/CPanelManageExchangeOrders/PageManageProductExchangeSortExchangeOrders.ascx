<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageManageProductExchangeSortExchangeOrders.ascx.cs" Inherits="Shaerd_CPanelManageExchangeOrders_PageManageProductExchangeSortExchangeOrders" %>
<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterWSM.ascx" TagPrefix="uc1" TagName="WUCFooterWSM" %>

<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>

<div class="container-fluid">
    <div class="pull-right">
        <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
            title="تحديث">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
        <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
            title="طباعة" OnClientClick="return insertConfirmation();">
                    <i class="fa fa-print"></i></asp:LinkButton>
    </div>
</div>
<div class="panel-body">
    <asp:Label ID="lbmsgPrisms" runat="server"></asp:Label>
    <div class="panel-body" id="IDFilterPrisms" runat="server">
        <div class="col-sm-3">
            <div class="form-group">
                <h5><i class="fa fa-star"></i>حدد القُرى : </h5>
                <asp:CheckBoxList ID="CBAlQariahPrisms" runat="server" Font-Size="12px" RepeatDirection="Vertical" CssClass="checkbox" Width="160">
                </asp:CheckBoxList>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-group">
                <h5><i class="fa fa-star"></i>حدد المشروع : </h5>
                <asp:CheckBoxList ID="CBCategoryPrisms" runat="server" Font-Size="12px" RepeatDirection="Vertical" CssClass="checkbox" Width="180">
                </asp:CheckBoxList>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-group">
                <h5><i class="fa fa-star"></i>حدد المستفيد : </h5>
                <asp:DropDownList ID="DLName" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown"
                        Style="font-size: 12px;">
                    <asp:ListItem Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-3">
            <div class="col-sm-12">
                <div class="input-group date " style="margin-right: -10px;">
                    <asp:TextBox ID="txtDateFromPrisms" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr;"></asp:TextBox>
                    <asp:Label ID="lblDateFromPrisms" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button">
                            <i class="fa fa-calendar"></i>
                        </button>
                    </span>
                </div>
                <br />
            </div>
            <div class="col-sm-12">
                <div class="input-group date " style="margin-right: -10px;">
                    <asp:TextBox ID="txtDateToPrisms" runat="server" placeholder="إلى تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr;"></asp:TextBox>
                    <asp:Label ID="lblDateToPrisms" runat="server" Text="حدد التاريخ * " ForeColor="Red" Visible="false"></asp:Label>
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button">
                            <i class="fa fa-calendar"></i>
                        </button>
                    </span>
                </div>
                <br />
            </div>
            <div class="col-sm-12">
                <asp:Button ID="btnSearchPrisms" runat="server" Text="بحث" Style="margin-right: 4px; width:80%" ValidationGroup="g2"
                    class="btn btn-info btn-fill" OnClick="btnSearchPrisms_Click" />
            </div>
        </div>
    </div>
    <hr />
    <div class="clearfix visible-sm-block"></div>
    <asp:Panel ID="pnlDataPrisms" runat="server" Direction="RightToLeft" Visible="False">
        <div class="table table-responsive">
            <div class="HideNow">
                <uc1:WUCHeader runat="server" ID="WUCHeader" />
            </div>
            <table class='table' style="width: 100%">
                <thead>
                    <tr>
                        <th>
                            <div align="center" class="w">
                                <div class="col-lg-11">
                                    <asp:TextBox ID="txtTitlePrisms" runat="server" class="form-control" Text="قائمة فرز أوامر الصرف" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                </div>
                                <div class="col-lg-1 HideThis">
                                    <asp:LinkButton ID="LBGetFilterPrisms" runat="server" OnClick="LBGetFilterPrisms_Click" data-toggle="tooltip" title="جلب قائمة الفلترة"> <i class="fa fa-refresh"></i> </asp:LinkButton>
                                </div>
                            </div>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:GridView ID="GVExchangeOrdersPrisms" runat="server" AutoGenerateColumns="False"
                                Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal" OnRowDataBound="GVExchangeOrdersPrisms_RowDataBound"
                                UseAccessibleHeader="False">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="10px" Visible="false">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="رقم الملف" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("ID")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="رقم الأمر" HeaderStyle-ForeColor="#CCCCCC" Visible="false">
                                        <ItemTemplate>
                                            <%--<%# Eval("billNumber_")%>--%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("NameMostafeed")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="القرية" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# ClassQuaem.FAlQarabah((Int32) Eval("AlQaryah"))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="لمشروع" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# ClassQuaem.FSupportType((Int64) (Eval("ID_Type")))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="عدد مرات الدعم" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# FGetCountCardPrisms(Convert.ToInt32(Eval("ID")) , Convert.ToInt64(Eval("ID_Type"))) %> مرات
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" Text='<%# FPricePrisms(Convert.ToInt32(Eval("ID")) , Convert.ToInt64(Eval("ID_Type"))) %>'></asp:Label>
                                            ريال
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                        <ItemTemplate>
                                            <a href='PageManageProductSupportByBeneficiary.aspx?SIDX=<%# Eval("ID")%>&XIDCate=<%# Eval("ID_Type")%>&XIDFrom=<%# txtDateFromPrisms.Text.Trim()%>&XIDTo=<%# txtDateToPrisms.Text.Trim()%>' title="عرض التفاصيل" data-toggle="tooltip"
                                                class="btn btn-info"><span class="fa fa-eye"></span></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
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
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <th>
                            <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                            <hr style='border: solid; border-width: 1px; width: 100%' />
                            <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                            <asp:Label ID="lblCountPrisms" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                            - <span style="font-size: 12px; padding-right: 5px">المجموع : </span>
                            <asp:Label ID="lblTotalPricePrisms" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text="ريال" Style='color: Red; font-size: 12px'></asp:Label>
                            <div align="Left" class="HideThis">
                                <img src='/Img/IconTrue.png' style='width: 20px' alt="" />
                                <span style="font-size: 11px">موافق</span>
                                <img src='/Img/IconFalse.png' style='width: 20px' alt="" />
                                <span style="font-size: 11px">غير موافق</span>
                            </div>
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
                <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlNullPrisms" runat="server" Visible="False">
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
    </asp:Panel>
    <asp:Panel ID="pnlSelectPrisms" runat="server" Visible="False">
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
            <h3 style="font-size: 20px">يرجى تحديد البيانات
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
    </asp:Panel>
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
