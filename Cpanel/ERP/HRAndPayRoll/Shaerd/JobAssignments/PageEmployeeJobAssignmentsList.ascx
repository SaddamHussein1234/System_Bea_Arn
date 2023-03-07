<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageEmployeeJobAssignmentsList.ascx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Shaerd_JobAssignments_PageEmployeeJobAssignmentsList" %>
<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/ERP/WUCFooterBottomERP.ascx" TagPrefix="uc1" TagName="WUCFooterBottomERP" %>

<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                title="طباعة">
            <i class="fa fa-print"></i></asp:LinkButton>
            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip" OnClick="btnRefrish_Click"
                title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
        </div>
        <h1>لوحة التحكم</h1>
        <ul class="breadcrumb">
            <li><a href="../../Default.aspx">الرئيسية</a></li>
            <li><a href="PageEmployeeJobAssignmentsList.aspx"> ملخص مهام الموظفين </a></li>
        </ul>
    </div>
</div>
<div class="container-fluid">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="lbmsg" runat="server" Text="ملخص مهام الموظفين"></asp:Label>
            </h3>
            <div style="float:left; width:200px; margin-top:-10px;">
                <label>حسب الشهر :
                    <label class="switch">
                        <asp:RadioButton ID="RBMonth" runat="server" GroupName="XDate" Checked="true" />
                        <span class="slider round"></span>
                    </label>
                </label>
                <label>حسب التاريخ :
                    <label class="switch">
                        <asp:RadioButton ID="RBDate" runat="server" GroupName="XDate" />
                        <span class="slider round"></span>
                    </label>
                </label>

                <script type="text/javascript">
                    $(function () {
                        $(document.getElementById("<%=RBMonth.ClientID %>")).click(function () {
                            if ($(this).is(":checked")) {
                                $("#pnlMonth").show();
                                $("#pnlDate").hide();
                            } else {
                                $("#pnlMonth").hide();
                                $("#pnlDate").show();
                            }
                        });
                    });
                </script>
                <script type="text/javascript">
                    $(function () {
                        $(document.getElementById("<%=RBDate.ClientID %>")).click(function () {
                            if ($(this).is(":checked")) {
                                $("#pnlDate").show();
                                $("#pnlMonth").hide();
                            } else {
                                $("#pnlDate").hide();
                                $("#pnlMonth").show();
                            }
                        });
                    });
                </script>
            </div>
        </div>

        <div class="panel-body">
            <div class="content-box-large">
                <div class="widget-box">
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
                    <div class="container-fluid" dir="rtl">
                        <div runat="server" id="IDDepartment" class="col-md-3">
                            <div class="form-group" style="margin-top:-20px;">
                                <script type="text/javascript">
                                    $(function () {
                                        $(document.getElementById("<%=CBDepartment.ClientID %>")).click(function () {
                                            if ($(this).is(":checked")) {
                                                $("#pnlDepartment").show();
                                            } else {
                                                $("#pnlDepartment").hide();
                                            }
                                        });
                                    });
                                </script>
                                <label>حسب الإدارة :
                                    <label class="switch">
                                        <input name="RememberMe" type="checkbox" id="CBDepartment" runat="server" />
                                        <span class="slider round"></span>
                                    </label>
                                </label>
                                <div id="pnlDepartment" style="margin-top:20px; display: none; <%= FCheck("Department") %>">
                                    <%--<label class="control-label">
                                        حدد الإدارة : <span title="إجباري" data-toggle="tooltip">*</span>
                                    </label>--%>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" Width="100%" ValidationGroup="g2"
                                        OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control2 chzn-select dropdown">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDepartment" SetFocusOnError="true" ControlToValidate="ddlDepartment" ValidationGroup="g2"
                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الإدارة" runat="server"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3">
                                <label class="control-label" id="Label1" runat="server">
                                    الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2 chzn-select dropdown" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlYears_SelectedIndexChanged" Width="100%" ValidationGroup="g2">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="ddlYears" ValidationGroup="g2"
                                    CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد السنة" runat="server"></asp:RequiredFieldValidator>
                        </div>
                        <div id="pnlMonth" class="col-md-3" style="display: none; <%= FCheck("_Month") %>">
                                <label class="control-label" id="Label3" runat="server">
                                    حدد الشهر <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control2 chzn-select dropdown" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" Width="100%" ValidationGroup="g2">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" ControlToValidate="ddlMonth" ValidationGroup="g2"
                                    CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الشهر" runat="server"></asp:RequiredFieldValidator>
                        </div>
                        <div id="pnlDate" class="col-md-6" style="display: none; <%= FCheck("_Date") %>">
                            <div class="col-sm-5">
                                <h5><i class="fa fa-star"></i> من تاريخ : </h5>
                                <div class="input-group date ">
                                    <asp:TextBox ID="txtDateFrom" runat="server" placeholder="من تاريخ" class="form-control" 
                                        data-date-format="YYYY-MM-DD" ValidationGroup="VGDate" Style="direction: ltr;"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" ControlToValidate="txtDateFrom" ValidationGroup="VGDate"
                                    CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-5">
                                <h5><i class="fa fa-star"></i> إلى تاريخ : </h5>
                                <div class="input-group date ">
                                    <asp:TextBox ID="txtDateTo" runat="server" placeholder="إلى تاريخ" class="form-control" 
                                        data-date-format="YYYY-MM-DD" ValidationGroup="VGDate" Style="direction: ltr;"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" ControlToValidate="txtDateTo" ValidationGroup="VGDate"
                                    CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="*" runat="server"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-sm-2">
                                <br />
                                    <asp:Button ID="btnGet" runat="server" Text="بحث" OnClick="btnGet_Click"
                                        class="btn btn-info btn-fill " ValidationGroup="VGDate" />
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="container-fluid table-responsive" dir="rtl">
                        <asp:Panel ID="pnlData" runat="server" Visible="False" Direction="RightToLeft">
                            <div class="table table-responsive" id="pnlPrint" runat="server" dir="rtl">
                                <div class="HideNow">
                                    <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                </div>
                                <div align="center" class="w">
                                    <div>
                                        <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث"
                                            Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"
                                            Text="قائمة ملخص مهام الموظفين"></asp:TextBox>
                                    </div>
                                </div>
                                <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                    <thead>
                                        <tr class="th">
                                            <th class="StyleTD">م
                                            </th>
                                            <th class="StyleTD">الإدارة
                                            </th>
                                            <th class="StyleTD">إسم الموظف
                                            </th>
                                            <th class="StyleTD">الوظيفة
                                            </th>
                                            <th class="StyleTD">سارية
                                            </th>
                                            <%--<th class="StyleTD">جديدة
                                            </th>--%>
                                            <th class="StyleTD">أُنجزت
                                            </th>
                                            <th class="StyleTD">أُنجزت مع ملاحظات
                                            </th>
                                            <th class="StyleTD">لم_تُنجز
                                            </th>
                                            <th class="StyleTD">متأخرة
                                            </th>
                                            <th class="StyleTD">مُدد التاريخ
                                            </th>
                                            <th class="StyleTD">محولة
                                            </th>
                                            <th class="StyleTD">متوقفة
                                            </th>
                                            <th class="StyleTD">مرفوضة
                                            </th>
                                            <th class="StyleTD">الإجمالي
                                            </th>
                                            <th class="StyleTD">التوقيع
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="RPTJobAssignments" runat="server" OnPreRender="RPTJobAssignments_PreRender">
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width: 10px;" class="StyleTD">
                                                        <span style="margin-right: 5px; font-size: 11px"><%# Container.ItemIndex + 1 %></span>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <span style="font-size: 11px"><%# Eval("Department")%></span>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <span style="font-size: 11px"><%# Eval("_Name")%></span>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <span style="font-size: 11px"><%# Eval("Designation")%></span>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <asp:Label ID="lblCountActive" runat="server" 
                                                            Text='<%# FGetCount("GetByAdminActiveCountM", Eval("EmployeeId").ToString()) %>'></asp:Label>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <asp:Label ID="lblCountFinsh" runat="server" 
                                                            Text='<%# FGetCount("GetByAdminFinshCountM", Eval("EmployeeId").ToString()) %>'></asp:Label>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <asp:Label ID="lblCountFinshWithComment" runat="server" 
                                                            Text='<%# FGetCount("GetByAdminFinshWithCommentCountM", Eval("EmployeeId").ToString()) %>'></asp:Label>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <asp:Label ID="lblCountFinshNot" runat="server" 
                                                            Text='<%# FGetCount("GetByAdminFinshNotCountM", Eval("EmployeeId").ToString()) %>'></asp:Label>
                                                    </td>
                                                    <%--<td class="StyleTD">
                                                        <%# FGetCount("GetByAdminNewCountM", Eval("EmployeeId").ToString()) %>
                                                    </td>--%>
                                                    <td class="StyleTD">
                                                        <asp:Label ID="lblCountLate" runat="server" 
                                                            Text='<%# FGetCount("GetByAdminLateCountM", Eval("EmployeeId").ToString()) %>'></asp:Label>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <asp:Label ID="lblCountExtension" runat="server" 
                                                            Text='<%# FGetCount("GetByAdminExtensionCountM", Eval("EmployeeId").ToString()) %>'></asp:Label>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <asp:Label ID="lblCountConvert" runat="server" 
                                                            Text='<%# FGetCount("GetByAdminConvertCountM", Eval("EmployeeId").ToString()) %>'></asp:Label>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <asp:Label ID="lblCountStoped" runat="server" 
                                                            Text='<%# FGetCount("GetByAdminStopedCountM", Eval("EmployeeId").ToString()) %>'></asp:Label>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <asp:Label ID="lblCountDeny" runat="server" 
                                                            Text='<%# FGetCount("GetByAdminDenyCountM", Eval("EmployeeId").ToString()) %>'></asp:Label>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <asp:Label ID="lblCount" runat="server" Text='<%# Eval("_Count")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblCountSum" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="StyleTD">
                                                        <img src='/<%# Eval("Img_Signature_")%>' width='100' height='30' />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th colspan="10" align="right">
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
                            <asp:HiddenField ID="hfCount" runat="server" Value="0" />
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
<script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
<script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>