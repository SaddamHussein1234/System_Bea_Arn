<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUCSortingOutFinancialSupport.ascx.cs" Inherits="Cpanel_ERP_EOS_PageSheard_WUCSortingOutFinancialSupport" %>
<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterWSM.ascx" TagPrefix="uc1" TagName="WUCFooterWSM" %>

<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<div class="container-fluid">
    <div style="float:right;">
        <h5><i class="fa fa-star"></i> فلترة العرض : </h5>
        <div class="checkbox checkbox-primary">
            <asp:CheckBox ID="CB2" runat="server" Text="الإرشيف" Font-Size="12px" Checked="true" CssClass="styled" Width="120" />
            <asp:CheckBox ID="CB3" runat="server" Text="رقم الملف" Font-Size="12px" Checked="false" CssClass="styled" Width="120" />
            <asp:CheckBox ID="CB4" runat="server" Text="الإسم" Font-Size="12px" Checked="true" CssClass="styled" Width="120" />
            <asp:CheckBox ID="CB5" runat="server" Text="القرية" Font-Size="12px" Checked="true" CssClass="styled" Width="120" />
            <asp:CheckBox ID="CB6" runat="server" Text="حالات الاسر" Font-Size="12px" Checked="true" CssClass="styled" Width="120" />
            <asp:CheckBox ID="CB7" runat="server" Text="رقم السجل المدني" Font-Size="12px" Checked="true" CssClass="styled" Width="150" /><br />
            <asp:CheckBox ID="CB8" runat="server" Text="الجوال" Font-Size="12px" Checked="true" CssClass="styled" Width="120" />
            <asp:CheckBox ID="CB9" runat="server" Text="أفراد الاسرة" Font-Size="12px" Checked="true" CssClass="styled" Width="120" />
            <asp:CheckBox ID="CB10" runat="server" Text="الدخل" Font-Size="12px" Checked="true" CssClass="styled" Width="120" />
            <asp:CheckBox ID="CB11" runat="server" Text="المشروع" Font-Size="12px" Checked="true" CssClass="styled" Width="120" />
            <asp:CheckBox ID="CB12" runat="server" Text="العدد الموزع" Font-Size="12px" Checked="true" CssClass="styled" Width="120" />
            <asp:CheckBox ID="CB13" runat="server" Text="الإجمالي" Font-Size="12px" Checked="true" CssClass="styled" Width="120" />
        </div>
    </div>
    <div class="pull-right">
        <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
            title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
        <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
            title="طباعة"><i class="fa fa-print"></i></asp:LinkButton>
    </div>
</div>
<div class="panel-body">
    <asp:Label ID="lbmsgPrisms" runat="server"></asp:Label>
    <div class="panel-body">
        <asp:Panel ID="IDFilterPrisms" runat="server" ScrollBars="Auto" Height="250">
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
                <div class="col-sm-12">
                    <div class="form-group">
                        <h5><i class="fa fa-star"></i> حدد القُرى : </h5>
                        <div class="checkbox checkbox-primary">
                            <asp:CheckBoxList ID="CBAlQariahPrisms" runat="server"
                                RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                <asp:ListItem></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-group">
                    <h5><i class="fa fa-star"></i>حدد المشروع : </h5>
                    <div class="checkbox checkbox-primary">
                        <asp:CheckBoxList ID="CBCategoryPrisms" runat="server"
                            RepeatDirection="Vertical" CssClass="styled" Width="100%">
                            <asp:ListItem></asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
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
                    <h5><i class="fa fa-star"></i>حدد المستفيد : </h5>
                    <asp:DropDownList ID="DLName" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown"
                            Style="font-size: 12px;">
                        <asp:ListItem Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <div class="input-group date ">
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
                <div class="form-group">
                    <div class="input-group date ">
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
        </asp:Panel>
    </div>
    <hr />
    <div class="clearfix visible-sm-block"></div>
    <asp:Panel ID="pnlDataPrisms" runat="server" Direction="RightToLeft" Visible="False">
        <div class="table table-responsive">
            <table class='table' style="width: 100%">
                <thead>
                    <tr>
                        <th>
                            <div class="HideNow">
                                <uc1:WUCHeader runat="server" ID="WUCHeader" />
                            </div>
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
                                    <asp:TemplateField HeaderText="الإرشيف" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters.Repostry_FinancialYear_.FErp_FinancialYear_ByID(new Guid(Eval("_ID_FinancialYear_").ToString()))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="رقم الملف" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("NumberMostafeed")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="إسم المستفيد" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("NameMostafeed") %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="القرية" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("AlQriah") %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="الحالة" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("HalatMostafeed") %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="رقم السجل المدني" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("NumberAlSegelAlMadany") %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="الجوال" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            0<%# Eval("PhoneNumber") %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="أفراد الأسرة" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("AfradAlOsrah") %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="الدخل" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("AlDakhlAlShahryllMostafeed") %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="المشروع" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <%# Eval("TypeAlDam") %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="العدد الموزع" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <i class="fa fa-cart-plus"></i> 
                                            <asp:Label ID="lblCountGet" runat="server" Font-Size="12px" 
                                            Text='<%# FGetCountCard(new Guid(Eval("_ID_FinancialYear_").ToString()),Convert.ToInt32(Eval("NumberMostafeed")),Convert.ToInt32(Eval("ID_Project_"))) %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="الإجمالي" HeaderStyle-ForeColor="#CCCCCC">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCountTotalPrice" runat="server" Font-Size="12px" 
                                                Text='<%# FGetSumCard(new Guid(Eval("_ID_FinancialYear_").ToString()),Convert.ToInt32(Eval("NumberMostafeed")),Convert.ToInt32(Eval("ID_Project_"))) %>'></asp:Label>
                                            <small>
                                                <%# XMony %>
                                            </small>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                        <ItemTemplate>
                                            <a href='PageSupportByBeneficiary.aspx?SIDX=<%# Eval("NumberMostafeed")%>&XIDCate=<%# Eval("ID_Project_")%>&XIDFrom=<%# txtDateFromPrisms.Text.Trim()%>&XIDTo=<%# txtDateToPrisms.Text.Trim()%>&IDYear=<%# Eval("_ID_FinancialYear_")%>' title="عرض التفاصيل" data-toggle="tooltip"
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
                        </th>
                    </tr>
                </tfoot>
            </table>
            <div class="hide">
                <hr style='border: solid; border-width: 1px; width: 100%' />
                <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                / <span style="font-size: 12px; padding-right: 5px">العدد الموزع : </span>
                <asp:Label ID="lbl_CountGet" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
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