<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/OM/MPCPanel.master" AutoEventWireup="true" CodeFile="PageTricker.aspx.cs" Inherits="Cpanel_ERP_OM_PageTricker" %>
<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/ERP/WUCFooterBottomERP.ascx" TagPrefix="uc1" TagName="WUCFooterBottomERP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <div>
                        <asp:LinkButton ID="LB_Print" runat="server" class="btn btn-success" data-toggle="tooltip" ValidationGroup="DLType"
                            title="طباعة" OnClick="LB_Print_Click">
                        <span class="fa fa-print"></span></asp:LinkButton>
                        <asp:LinkButton ID="Lb_Refresh" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="Lb_Refresh_Click"
                            title="تحديث"><span class="fa fa-refresh"></span></asp:LinkButton>
                    </div>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageTricker.aspx">مركز العمليات</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid" runat="server" id="ProductByUser">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-list"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="مركز العمليات"></asp:Label>
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
                    <div class="col-lg-2">
                        <div class="form-group">
                            <h5>حدد المستخدمين : <i style="color: red">*</i></h5>
                            <a href='javaScript:void(0)' data-toggle="modal" data-target="#IDUsers">
                                <i class="fa fa-eye"></i>عرض المستخدمين
                            </a>
                        </div>
                    </div>
                    <div id="IDUsers" class="modal fade in modal_New_Style">
                        <div class="modal-dialog " style="max-width: 650px">
                            <div class="modal-content">
                                <div class="modal-header no-border">
                                    <button type="button" class="close" data-dismiss="modal">×</button>
                                </div>
                                <div class="modal-body" id="modal_ajax_content">
                                    <div class="page-container">
                                        <div class="page-content">
                                            <div class=" panel-body">
                                                <label>
                                                    <i class="fa fa-star"></i>حدد المستخدمين <span class="text-danger">*</span>
                                                </label>
                                                <script type="text/javascript">
                                                    function jsUsers(ch) {
                                                        var allcheckboxes = document.getElementById('<%=CBUsers.ClientID %>').getElementsByTagName("input");
                                                        for (i = 0; i < allcheckboxes.length; i++)
                                                            allcheckboxes[i].checked = ch.checked;
                                                    }
                                                </script>
                                                <div class="checkbox checkbox-primary" align="right">
                                                    <asp:CheckBox ID="CBCheckAllUsers" onclick="jsUsers(this)" runat="server"
                                                        Text="تحديد الكل" CssClass="styled" Checked="true" />
                                                </div>
                                                <div class="checkbox checkbox-primary">
                                                    <asp:CheckBoxList ID="CBUsers" runat="server"
                                                        RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="form-group">
                            <h5><i class="fa fa-star"></i> الأنظمة المرتبطة : <i style="color: red">*</i></h5>
                            <a href='javaScript:void(0)' data-toggle="modal" data-target="#IDSystems">
                                <i class="fa fa-eye"></i>عرض الأنظمة
                            </a>
                        </div>
                    </div>
                    <div id="IDSystems" class="modal fade in modal_New_Style">
                        <div class="modal-dialog " style="max-width: 650px">
                            <div class="modal-content">
                                <div class="modal-header no-border">
                                    <button type="button" class="close" data-dismiss="modal">×</button>
                                </div>
                                <div class="modal-body" id="modal_ajax_content">
                                    <div class="page-container">
                                        <div class="page-content">
                                            <div class=" panel-body">
                                                <label>
                                                    <i class="fa fa-star"></i>حدد الأنظمة المرتبطة <span class="text-danger">*</span>
                                                </label>
                                                <script type="text/javascript">
                                                    function jsSystems(ch) {
                                                        var allcheckboxes = document.getElementById('<%=CBSystems.ClientID %>').getElementsByTagName("input");
                                                        for (i = 0; i < allcheckboxes.length; i++)
                                                            allcheckboxes[i].checked = ch.checked;
                                                    }
                                                </script>
                                                <div class="checkbox checkbox-primary" align="right">
                                                    <asp:CheckBox ID="CBCheckAllSystems" onclick="jsSystems(this)" runat="server"
                                                        Text="تحديد الكل" CssClass="styled" Checked="true" />
                                                </div>
                                                <div class="checkbox checkbox-primary">
                                                    <asp:CheckBoxList ID="CBSystems" runat="server"
                                                        RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                        <asp:ListItem Value="SSP">الإعدادات والصلاحيات</asp:ListItem>
                                                        <asp:ListItem Value="HRM">الموارد البشرية</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <h5><i class="fa fa-star"></i> العملية : <i style="color: red">*</i></h5>
                            <a href='javaScript:void(0)' data-toggle="modal" data-target="#IDProcess">
                                <i class="fa fa-eye"></i>عرض العمليات
                            </a>
                        </div>
                    </div>
                    <div id="IDProcess" class="modal fade in modal_New_Style">
                        <div class="modal-dialog " style="max-width: 650px">
                            <div class="modal-content">
                                <div class="modal-header no-border">
                                    <button type="button" class="close" data-dismiss="modal">×</button>
                                </div>
                                <div class="modal-body" id="modal_ajax_content">
                                    <div class="page-container">
                                        <div class="page-content">
                                            <div class=" panel-body">
                                                <label>
                                                    <i class="fa fa-star"></i>حدد العمليات <span class="text-danger">*</span>
                                                </label>
                                                <script type="text/javascript">
                                                    function jsSystems(ch) {
                                                        var allcheckboxes = document.getElementById('<%=CBProcess.ClientID %>').getElementsByTagName("input");
                                                        for (i = 0; i < allcheckboxes.length; i++)
                                                            allcheckboxes[i].checked = ch.checked;
                                                    }
                                                </script>
                                                <div class="checkbox checkbox-primary" align="right">
                                                    <asp:CheckBox ID="CBCheckAllProcess" onclick="jsSystems(this)" runat="server"
                                                        Text="تحديد الكل" CssClass="styled" Checked="true" />
                                                </div>
                                                <div class="checkbox checkbox-primary">
                                                    <asp:CheckBoxList ID="CBProcess" runat="server"
                                                        RepeatDirection="Vertical" CssClass="styled" Width="100%">

                                                        <asp:ListItem Value="عرض">عرض</asp:ListItem>
                                                        <asp:ListItem Value="بحث">بحث</asp:ListItem>
                                                        <asp:ListItem Value="إضافة">إضافة</asp:ListItem>
                                                        <asp:ListItem Value="تعديل">تعديل</asp:ListItem>
                                                        <asp:ListItem Value="تحديث">تحديث</asp:ListItem>
                                                        <asp:ListItem Value="وافق">وافق</asp:ListItem>
                                                        <%--<asp:ListItem Value="3">طباعة</asp:ListItem>--%>
                                                        <%--<asp:ListItem Value="6">حذف</asp:ListItem>--%>
                                                        <asp:ListItem Value="تسجيل دخول">دخول آمن</asp:ListItem>
                                                        <asp:ListItem Value="تسجيل خروج">خروج آمن</asp:ListItem>
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="form-group">
                            <h5><i class="fa fa-star"></i> من تاريخ : <i style="color: red">*</i></h5>
                            <div class="input-group date">
                                <asp:TextBox ID="txtDateFrom" runat="server" placeholder="من تاريخ" class="form-control" 
                                    data-date-format="YYYY-MM-DD" ValidationGroup="VGfilter" Style="direction: ltr;"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </span>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="txtDateFrom" ValidationGroup="VGfilter"
                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد التاريخ" runat="server"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="form-group">
                            <h5><i class="fa fa-star"></i> إلى تاريخ : <i style="color: red">*</i></h5>
                            <div class="input-group date">
                                <asp:TextBox ID="txtDateTo" runat="server" placeholder="إلى تاريخ" class="form-control" 
                                    data-date-format="YYYY-MM-DD" ValidationGroup="VGfilter" Style="direction: ltr;"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button">
                                        <i class="fa fa-calendar"></i>
                                    </button>
                                </span>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" ControlToValidate="txtDateTo" ValidationGroup="VGfilter"
                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد التاريخ" runat="server"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-sm-1">
                        <br />
                        <div class="form-group">
                            <asp:Button ID="btnSearch" runat="server" Text="بحث" class="btn btn-info" OnClick="btnSearch_Click" ValidationGroup="VGfilter" />
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <asp:Panel ID="pnlData" runat="server" Visible="false" Direction="RightToLeft">
                        <div class="table table-responsive" runat="server" dir="rtl" id="pnlprint">
                            <table class='table table-bordered table-condensed' style="font-size: 12px; width: 100%" aria-multiline="true">
                                <thead>
                                    <tr>
                                        <th colspan="6">
                                            <div class="HideNow">
                                                <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                            </div>
                                            <div align="center" class="w">
                                                <div>
                                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث" 
                                                        Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                </div>
                                            </div>
                                        </th>
                                    </tr>
                                    <tr>
                                        <th class="StyleTD"><strong>م</strong></th>
                                        <th class="StyleTD"><strong>النظام</strong></th>
                                        <th class="StyleTD"><strong>نوع العملية</strong></th>
                                        <th class="StyleTD"><strong>التفاصيل</strong></th>
                                        <th class="StyleTD"><strong>من قام بذلك</strong></th>
                                        <th class="StyleTD"><strong>تاريخ العملية</strong></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="RPTProccess" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="StyleTD"><%# Container.ItemIndex + 1 %></td>
                                                <td class="StyleTD"><%# Eval("_Type_System_") %></td>
                                                <td class="StyleTD"><%# Eval("_Type_Process_") %></td>
                                                <td class="StyleTD"><%# Eval("_Details_") %></td>
                                                <td class="StyleTD"><%# Eval("_Name") %></td>
                                                <td class="StyleTD"><%# Eval("_Date_Process_") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th colspan="6" class="StyleTD">
                                            <span style="font-size: 12px; padding-right: 5px">العدد : </span>
                                            <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>   
                                        </th>
                                    </tr>
                                </tfoot>
                            </table>
                            <div class="hide">
                                <div class="container-fluid " dir="rtl" runat="server">
                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                    <uc1:WUCFooterBottomERP runat="server" ID="WUCFooterBottomERP" />
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

