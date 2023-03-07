<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/CRS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAll.aspx.cs" Inherits="Cpanel_ERP_CRS_Committee_Report_PageAll" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/ERP/WUCFooterBottomERP.ascx" TagPrefix="uc1" TagName="WUCFooterBottomERP" %>
<%@ Register Src="~/Cpanel/CAttach/WUCFooterSSM.ascx" TagPrefix="uc1" TagName="WUCFooterSSM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script type="text/javascript">
        function insertConfirmation() {
            var answer = confirm("هل تريد الإستمرار ؟")
            if (answer) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <script type="text/javascript">
<!--
    function Check_Click(objRef) {
        var row = objRef.parentNode.parentNode;
        var GridView = row.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var headerCheckBox = inputList[0];
            var checked = true;
            if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                if (!inputList[i].checked) {
                    checked = false;
                    break;
                }
            }
        }
        headerCheckBox.checked = checked;
    }
    function checkAll(objRef) {
        var GridView = objRef.parentNode.parentNode.parentNode;
        var inputList = GridView.getElementsByTagName("input");
        for (var i = 0; i < inputList.length; i++) {
            var row = inputList[i].parentNode.parentNode;
            if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                if (objRef.checked) {
                    inputList[i].checked = true;
                }
                else {
                    inputList[i].checked = false;
                }
            }
        }
    }
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCount.ClientID %>").value;
            var gv = document.getElementById("<%=GVAll.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].checked && chk[i].id.indexOf("chkAll") == -1) {
                    count++;
                }
            }
            if (count == 0) {
                alert("لم تقم بالتحديد على أي سجل");
                return false;
            }
            else {
                return confirm(" هل أنت متأكد من الإستمرار ؟");
            }
        }
    </script>

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a href="PageAdd.aspx" data-toggle="tooltip" title="إضافة جديد" class="btn btn-primary"><i class="fa fa-plus"></i></a>
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click" title="طباعة">
                    <i class="fa fa-print"></i></asp:LinkButton>
                    <asp:LinkButton ID="btnDelete1" runat="server" class="btn btn-danger"
                        OnClientClick="return ConfirmDelete();" title="حذف" data-toggle="tooltip"
                        OnClick="btnDelete1_Click"><span class="tip-bottom">
                            <i class="fa fa-trash-o"></i></span></asp:LinkButton>
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <div class="container-fluid">
                    <h1>لوحة التحكم</h1>
                    <ul class="breadcrumb">
                        <li><a href="../">الرئيسية</a></li>
                        <li><a href="PageAll.aspx">قائمة تقارير اللجان </a></li>
                    </ul>
                </div>
            </div>
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-list"></i>قائمة تقارير اللجان
                        </h3>
                    </div>
                    <div class="panel-body">
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
                        <asp:Panel ID="IDFilter" runat="server" ScrollBars="Auto" Height="250">
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
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <h5><i class="fa fa-star"></i> من تاريخ : </h5>
                                        <div class="input-group date ">
                                            <asp:TextBox ID="txtDateFrom" runat="server" placeholder="من تاريخ" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="VGDetails" Style="direction: ltr;"></asp:TextBox>
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
                                        <div class="form-group">
                                        <h5><i class="fa fa-star"></i> جملة البحث : </h5>
                                        <asp:TextBox ID="txtSearch" runat="server" placeholder="إبحث هنا ... " class="form-control" ValidationGroup="g2" Style="width: 100%"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnGet" runat="server" Text="بحث حسب الفلترة" Style="margin-right: 4px;"
                                            class="btn btn-info btn-fill " ValidationGroup="VGDetails" OnClick="btnGet_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="clearfix"></div>
                        <asp:Panel ID="pnlData" runat="server" Direction="RightToLeft" Visible="False">
                            <div class="table table-responsive" id="pnlData2" runat="server" dir="rtl">
                                <div class="HideNow">
                                    <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                </div>
                                <table class='table' style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>
                                                <div align="center" class="w">
                                                    <div align="center" class="w">
                                                        <div class="col-lg-11">
                                                            <asp:TextBox ID="txtTitle" runat="server" class="form-control" Text="قائمة فرز أوامر الصرف" placeholder="عنوان البحث" Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                        </div>
                                                        <div class="col-lg-1 HideThis">
                                                            <asp:LinkButton ID="LBGetFilter" runat="server" OnClick="LBGetFilter_Click" data-toggle="tooltip" title="جلب قائمة الفلترة"> <i class="fa fa-refresh"></i> </asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="GVAll" runat="server" AutoGenerateColumns="False" DataKeyNames="_ID_Item_"
                                                    Width="100%" CssClass="footable1" EnableTheming="True" GridLines="Horizontal"
                                                    UseAccessibleHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Width="10px">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAll" runat="server" onclick='checkAll(this);' />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="_ID_Item_" HeaderText="_ID_Item_" InsertVisible="False" ReadOnly="True"
                                                            SortExpression="_ID_Item_" Visible="false" />
                                                        <asp:TemplateField HeaderText="م" HeaderStyle-Width="16" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Font-Size="12px" Text='<%# Eval("_ID_Item_") %>' Visible="false"></asp:Label>
                                                                <span style="margin-right: 5px; font-size: 11px"><%# Container.DataItemIndex + 1 %></span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="الإرشيف" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <i class="fa fa-calendar"></i> 
                                                                <%# Library_CLS_Arn.ERP.Repostry.HRAndPayRoll.Masters.Repostry_FinancialYear_.FErp_FinancialYear_ByID(new Guid(Eval("_ID_FinancialYear_").ToString()))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="نوع التقرير" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="font-size: 12px">
                                                                    <%# Eval("_Name_Ar_")%>
                                                                </span>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="رقم التقرير" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="font-size: 11px">رقم تقرير اللجنة <%# Eval("_Nmber_")%>
                                                                </span>
                                                                <br />
                                                                <div style="font-size: 11px" class="HideThis">
                                                                   , <%# ClassSaddam.FRaeesMaglis4((bool) (Eval("_IsRaees_")))%>
                                                                </div>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="عنوان التقرير" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <span style="font-size: 12px">
                                                                    <%# Eval("_Title_")%>
                                                                </span>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderText="أُضيف من قبل">
                                                            <ItemTemplate>
                                                                <%# ClassAdmin_Arn.FGetNameByID((Int32) (Eval("_CreatedBy_")))%>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="بتاريخ" HeaderStyle-ForeColor="#CCCCCC">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDate_Add" runat="server"
                                                                    Text='<%# Eval("_CreatedDate_", "{0:dd/MM/yyyy}") + " " + Eval("_CreatedDate_", "{0:HH:mm tt}")  %>' Font-Size="11px"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-ForeColor="#CCCCCC" HeaderStyle-Width="16">
                                                            <ItemTemplate>
                                                                <a href="PageAdd.aspx?ID=<%# Eval("_ID_Item_") %>" data-toggle="tooltip" title="تعديل البيانات">
                                                                    <i class="fa fa-edit"></i>
                                                                </a>
                                                                <a href='PageView.aspx?ID=<%# Eval("_Nmber_")%>&IDUniq=<%# Eval("_ID_FinancialYear_")%>' title="عرض التقرير" data-toggle="tooltip"><span class="fa fa-eye"></span></a>
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
                                                <asp:HiddenField ID="hfCount" runat="server" Value="0" />
                                            </td>
                                        </tr>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th>
                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                <div>
                                                    <span style="font-size: 12px; padding-right: 5px">
                                                        إرشيف 
                                                        <asp:Label ID="lbl_Years" runat="server"></asp:Label>
                                                    </span> - 
                                                    <span>العدد : </span>
                                                    <asp:Label ID="lblCount" runat="server" Text="0"></asp:Label>
                                                </div>
                                                <div align="Left" class="HideThis">
                                                    <img src='/Img/IconTrue.png' style='width: 20px' />
                                                    <span style="font-size: 11px">إطلع</span>
                                                    <img src='/Img/IconFalse.png' style='width: 20px' />
                                                    <span style="font-size: 11px">لم يطلع</span>
                                                </div>
                                                <div class="hide">
                                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                                    <uc1:WUCFooterSSM runat="server" ID="WUCFooterSSM" />
                                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                                    <div class="HideNow">
                                                        <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom1" />
                                                    </div>
                                                </div>
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>
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
                        <asp:Panel ID="pnlSelect" runat="server" Visible="true">
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <div align="center">
                                <h3 style="font-size: 20px">إدخل جملة البحث ... 
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

