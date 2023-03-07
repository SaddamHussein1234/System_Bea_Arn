<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/CRM/CRM_Main.master" AutoEventWireup="true" CodeFile="PageCash_Support.aspx.cs" Inherits="Cpanel_ERP_CRM_PageCash_Support_PageCash_Support" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/ERP/WUCFooterBottomERP.ascx" TagPrefix="uc1" TagName="WUCFooterBottomERP" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterSSM.ascx" TagPrefix="uc1" TagName="WUCFooterSSM" %>


<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    </style>
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip"
                        title="طباعة" OnClick="btnPrint_Click">
                    <i class="fa fa-print"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="Default.aspx">الرئيسية</a></li>
                        <li><a href="PageCash_Support.aspx">قائمة الدعم النقدي</a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title" style="float: right">
                            <i class="fa fa-list"></i>قائمة الدعم العيني 
                            <asp:RadioButton ID="RBIsGet" runat="server" Text="الشركات التي دعمت" Checked="true" GroupName="GCheck" Style="font-size: 13px;" />
                            <asp:RadioButton ID="RBIsSet" runat="server" Text="الشركات التي لم تدعم" GroupName="GCheck" Style="font-size: 13px;" />
                        </h3>
                        <div class="panel-title" align="left" style="margin: -5px 0 -5px 0" dir="rtl">
                            <label class="control-label">
                                الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                            </label>
                            <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2"
                                Width="100" ValidationGroup="g2">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>

                            <div runat="server" visible="false">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="WidthTextSearch2" placeholder=" إبحث هنا ... " Height="31px"></asp:TextBox>
                                &nbsp;
                             <asp:LinkButton ID="btnSearch" runat="server" class="btn btn-info" data-toggle="tooltip" ValidationGroup="g2"
                                 title="بحث">
                            <i class="fa fa-search"></i></asp:LinkButton>
                            </div>
                            <div class="clearfix"></div>
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

                        <div class="col-md-2">
                            <div class="form-group">
                                <div class="input-group date ">
                                    <asp:TextBox ID="txtStartDate" runat="server" class="form-control" ValidationGroup="g2" data-date-format="YYYY-MM-DD" placeholder=" من تاريخ ... "
                                        Style="text-align: center"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator20" runat="server"
                                        ControlToValidate="txtStartDate" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="g2" Font-Size="10px"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <div class="input-group date ">
                                    <asp:TextBox ID="txtEndDate" runat="server" class="form-control" ValidationGroup="g2" placeholder=" إلى تاريخ ... " data-date-format="YYYY-MM-DD"
                                        Style="text-align: center"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtEndDate" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="g2" Font-Size="10px"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Button ID="btnGet" runat="server" Text="جلب البيانات" class="btn btn-info pull-right" ValidationGroup="g2" OnClick="btnGet_Click" />
                            </div>
                        </div>
                        <asp:Panel ID="pnlData" runat="server" Visible="false" Direction="RightToLeft">
                            <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                                <div class="table table-responsive" id="pnlPrint" runat="server" dir="rtl">
                                    <div class="HideNow">
                                        <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                    </div>
                                    <div align="center" class="w">
                                        <div>
                                            <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث"
                                                Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"
                                                Text="قائمة أنواع شركات الدعم"></asp:TextBox>
                                        </div>
                                    </div>
                                    <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                        <thead>
                                            <tr class="th">
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="GVBillAll" runat="server" AutoGenerateColumns="False"
                                                        Width="100%" CssClass="footable1"
                                                        EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False">
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
                                                                    <small><%# ddlYears.SelectedItem.ToString() %></small>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="_Registration_No_" HeaderText="رقم تسجيل الداعم" SortExpression="_Registration_No_" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField DataField="_Company_Name_" HeaderText="إسم الداعم" SortExpression="_Company_Name_" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField DataField="_Email_Address_" HeaderText="البريد الإلكتروني" SortExpression="_Email_Address_" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:BoundField DataField="_Phone_Number1_" HeaderText="رقم الهاتف" SortExpression="_Phone_Number1_" HeaderStyle-ForeColor="#CCCCCC" />
                                                            <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderText="عدد مرات الدعم">
                                                                <ItemTemplate>
                                                                    <%# Library_CLS_Arn.CRM.Repostry.Repostry_In_Cash_Support_.FCRM_In_Cash_Support_Manage(new Guid(ddlYears.SelectedValue), new Guid(Eval("_ID_Item_").ToString()), txtStartDate.Text.Trim(),txtEndDate.Text.Trim()) %>
                                                                    <i class="fa fa-cart-plus"></i>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16px">
                                                                <ItemTemplate>
                                                                    <a href='PageCash_SupportByDate.aspx?IDYear=<%# ddlYears.SelectedValue %>&ID=<%# Eval("_ID_Item_")%>&From_Date=<%# txtStartDate.Text.Trim() %>&To_Date=<%# txtEndDate.Text.Trim() %>' title="عرض التفاصيل" data-toggle="tooltip"
                                                                        class="btn btn-info"><span class="fa fa-eye"></span></a>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                        <HeaderStyle CssClass="Colorloading" Font-Bold="True" ForeColor="White" />
                                                        <PagerSettings Mode="NextPrevious" Position="TopAndBottom" NextPageText=" -- التالي " PreviousPageText=" السابق - " />
                                                        <PagerStyle CssClass="pagination-ys" BackColor="White" ForeColor="Red" HorizontalAlign="Right" />
                                                        <RowStyle CssClass="rows"></RowStyle>
                                                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                        <SortedDescendingHeaderStyle BackColor="#242121" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <th colspan="9">
                                                    <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                                    <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                                                    <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                    <span class="fa fa-table"></span>
                                                </th>
                                            </tr>
                                        </tfoot>
                                    </table>
                                    <div class="hide">
                                        <hr style='border: solid; border-width: 1px; width: 100%' />
                                        <uc1:WUCFooterSSM runat="server" ID="WUCFooterSSM" />
                                        <hr style='border: solid; border-width: 1px; width: 100%' />
                                        <div class="HideNow">
                                            <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </asp:Panel>
                        <asp:Panel ID="pnlNull" runat="server" Visible="False">
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
                        </asp:Panel>
                        <asp:Panel ID="pnlSelect" runat="server" Visible="False">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <div align="center">
                                <h3 style="font-size: 20px">حدد التاريخ
                                </h3>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
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
        <script type="text/javascript"><!--
    $('#language a:first').tab('show');
    $('#option a:first').tab('show');
    //--></script>
</asp:Content>

