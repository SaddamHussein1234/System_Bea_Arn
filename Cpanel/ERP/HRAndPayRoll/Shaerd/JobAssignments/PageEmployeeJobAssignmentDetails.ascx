<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageEmployeeJobAssignmentDetails.ascx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Shaerd_JobAssignments_PageEmployeeJobAssignmentDetails" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>
<script type="text/javascript">
    function Confirmation() {
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
    function ShowModelConvert() {
        $("#IDModelConvert").modal('show');
    }

    $(function () {
        $("#btnShow").click(function () {
            ShowModelConvert();
        });
    });
</script>
<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <a runat="server" id="ID_Edit_" class="btn btn-info" visible="false" data-toggle="tooltip" title="تعديل الخطاب">الذهاب إلى وضع التعديل <span class="fa fa-edit"></span></a>
            &nbsp;
            <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                    title="طباعة"><i class="fa fa-print"></i></asp:LinkButton>
            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                title="تحديث" OnClick="btnRefrish_Click">
            <i class="fa fa-refresh"></i></asp:LinkButton>
        </div>
        <h1>لوحة التحكم</h1>
        <ul class="breadcrumb">
            <li><a href="../">الرئيسية</a></li>
            <li><a href="#">إضافة المرفقات</a></li>
        </ul>
    </div>
</div>
<div class="container-fluid">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="lbmsg" runat="server" Text="سجل المهام"></asp:Label>
            </h3>
            <div style="float: left; margin-right:15px;">
                <label class="control-label">
                    الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                </label>
                <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2" AutoPostBack="true" Enabled="false"
                    Width="100" ValidationGroup="g2" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div style="float: left;">
                <div class="clearfix pull-left pdn--tt" style="margin-left:20px;">
                    <div runat="server" id="IDOption" visible="false" class="dropdown pdn--an-imp">
                        <button class="dropdown-toggle btn btn-info " data-toggle="dropdown">
                            <i class="fa fa-gear"></i>
                            <span class="hidden-xs"><b>خيارات المهمة</b></span>
                            <i class="fa fa-caret-down"></i>
                        </button>
                        <ul class="dropdown-menu dropdown-menu-left" role="menu" aria-labelledby="خيارات" style="width:300px;">
                            <li id="IDSuccess" runat="server">
                                <asp:LinkButton ID="LB_Success" runat="server" data-toggle="tooltip" title="تحديد أنها أُنجزت"
                                    OnClientClick="return Confirmation();" style='background: #58c9b9; margin-bottom:10px; margin:5px;'
                                    OnClick="LB_Success_Click" CssClass="badge order-status-badge"><b><i class="fa fa-check-square-o"></i> تحديد أنها أُنجزت</b> </asp:LinkButton>
                            </li>
                            <li id="IDSuccessWithComment" runat="server">
                                <asp:LinkButton ID="LB_SuccessWithComment" runat="server" data-toggle="tooltip" title="أُنجزت مع ملاحظة" ValidationGroup="VGComment"
                                    style='background: #c6b607; margin-bottom:10px; margin:5px;'
                                    OnClick="LB_SuccessWithComment_Click" CssClass="badge order-status-badge"><b><i class="fa fa-check-square-o"></i> أُنجزت مع ملاحظة</b> </asp:LinkButton>
                                <asp:TextBox ID="txtComment" runat="server" placeholder="الملاحظة ,,, " TextMode="MultiLine" Rows="3"
                                    CssClass="form-control" ValidationGroup="VGComment" Font-Size="14px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" ControlToValidate="txtComment" ValidationGroup="VGComment"
                                    CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* الملاحظات" runat="server"></asp:RequiredFieldValidator>
                            </li>
                            <li id="IDNotSuccess" runat="server">
                                <asp:LinkButton ID="LB_NotSuccess" runat="server" data-toggle="tooltip" title="لم تُنجز المهمة"
                                    OnClientClick="return Confirmation();" style='background: #8d0303; margin-bottom:10px; margin:5px;'
                                    OnClick="LB_NotSuccess_Click" CssClass="badge order-status-badge"><b><i class="fa fa-check-square-o"></i> لم تُنجز المهمة</b> </asp:LinkButton>
                                <hr />
                            </li>
                            <li id="IDConvert" runat="server">
                                <asp:LinkButton ID="LB_Convert" runat="server" data-toggle="tooltip" title="تحويل المهمة" style='background: #5c9b02; margin:5px;'
                                    OnClick="LB_Convert_Click" CssClass="badge order-status-badge"><b><i class='fa fa-share'></i> تحويل المهمة</b> </asp:LinkButton>
                            </li>
                            <li id="IDExtension" runat="server">
                                <asp:LinkButton ID="LB_Extension" runat="server" data-toggle="tooltip" title="تمديد وقت المهمة" style='background: #0e2e97; margin:5px;'
                                    OnClick="LB_Extension_Click" CssClass="badge order-status-badge"><b><i class='fa fa-calendar'></i> تمديد وقت المهمة</b> </asp:LinkButton>
                            </li>
                            <li id="IDActive" runat="server">
                                <asp:LinkButton ID="LB_Active" runat="server" data-toggle="tooltip" OnClick="LB_Active_Click"
                                    OnClientClick="return Confirmation();" style='background: #c3bf04; margin:5px;'
                                    title="إلغاء إيقاف المهمة" CssClass="badge order-status-badge"><b><i class="fa fa-asterisk"></i> إلغاء إيقاف المهمة</b></asp:LinkButton>
                            </li>
                            <li id="IDStop" runat="server">
                                <asp:LinkButton ID="LB_Stop" runat="server" data-toggle="tooltip" OnClick="LB_Stop_Click"
                                    OnClientClick="return Confirmation();" style='background: #a90202; margin:5px;'
                                    title="إيقاف المهمة" CssClass="badge order-status-badge"><b><i class="fa fa-asterisk"></i> إيقاف المهمة</b></asp:LinkButton>
                                <br />
                            </li>
                        </ul>
                    </div>
                </div>
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
                    <span id="lblSuccess" runat="server"></span>
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
            </div>
            <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft">
                <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>المهام : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLJobAssignment" runat="server" ValidationGroup="GVImg"
                                            AutoPostBack="true" OnSelectedIndexChanged="DLJobAssignment_SelectedIndexChanged"
                                            CssClass="form-control2 chzn-select dropdown" Width="300">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="DLJobAssignment" ErrorMessage="* حدد المهام" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GVImg" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </div>
                                    <div runat="server" id="pnlEmp" visible="false" class="form-group">
                                        <h5>المشتركين بالمهمة :
                                        </h5>
                                        <asp:Repeater ID="RPTJobAssignment_Map" runat="server">
                                            <ItemTemplate>
                                                <span style="font-size: 12px;"><%# Container.ItemIndex + 1 %> - <%# Eval("_Name") %>
                                                    <%# Convert.ToBoolean(Eval("Is_View_"))
                                                            ? 
                                                            " <span style='font-size:10px; background-color:#5c9b02; color:#FFF;' class='badge order-status-badge'><i class='fa fa-eye'></i>  فُتحت بتأريخ " + Eval("Date_View_", "{0:dd/MM/yyyy hh:mm:tt}") + "</span> " 
                                                            :
                                                            " <span style='font-size:10px; color:#FFF;' class='BackgroundAll badge order-status-badge'><i class='fa fa-envelope'></i>  لم تُفتح </span>"
                                                    %>
                                                    <br />
                                                </span>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlDetails" runat="server" Visible="false">
                                <div class="col-md-7">
                                    <a href="javaScript:void(0)" type="button" data-toggle="collapse" data-target="#navbarToggleExternalContent" 
                                        aria-controls="navbarToggleExternalContent" aria-expanded="false" aria-label="Toggle navigation">
                                    <i class="fa fa-plus"></i>
                                        تفاصيل المهمة : 
                                    </a>
                                    <div class="collapse2" id="navbarToggleExternalContent" style="background-color: #f6f7f6; border-radius:8px;">
                                        <div class="container-fluid" dir="rtl">
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    نوع المهمة : <br />
                                                    <asp:Label ID="lblAssignment_Title" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    رقم المهمة : <br />
                                                    <asp:Label ID="lblNumberJob" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    المهمة : <br />
                                                    <asp:Label ID="lblThe_Assignment" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    ساعات العمل في اليوم : <br />
                                                    <asp:Label ID="lblHours_In_Day" runat="server"></asp:Label> / ساعات
                                                </div>
                                            </div>
                                            <div class="col-md-8">
                                                <div class="form-group">
                                                    تاريخ بدأ المهمة : <br />
                                                    <asp:Label ID="lblDate_Job" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-4">
                                                <div class="form-group">
                                                    تاريخ إنتهاء المهمة : <br />
                                                    <asp:Label ID="lblDateEnd_Job" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    شرح المهمة : <br />
                                                    <asp:Label ID="lblThe_Qriah" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>الحالة : 
                                        </h5>
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>

                                        <asp:Label ID="lblExtension" runat="server"></asp:Label>
                                    </div>
                                </div>
                                </asp:Panel>
                            </div>
                            <div class="clearfix"></div><br />
                            <div class="container-fluid" dir="rtl">
                                <div class="control-group">
                                    <div class="controls">
                                        <asp:Panel ID="pnlData" runat="server">
                                            <div class="col-md-12" dir="rtl">
                                                <asp:Repeater ID="RPTMessage" runat="server">
                                                    <ItemTemplate>
                                                        <div <%# FStyle(Eval("_Type_Send_").ToString()) %> dir="rtl">
                                                            <div style="margin:5px 0 -5px 10px; <%# FHideDelete((Int32) (Eval("_CreatedBy_"))) %>">
                                                                <asp:LinkButton ID="LB_Delete" runat="server" title="حذف" data-toggle="tooltip"
                                                                     CommandArgument='<%# Eval("_ID_Item_") %>'
                                                                    class="delete-button" OnClientClick="return Confirmation();" OnClick="LB_Delete_Click">
                                                                    <button type="button" class="close fa-2x" data-dismiss="modal">×</button>
                                                                </asp:LinkButton>
                                                            </div>
                                                            <div class='panel-body'>
                                                                <h5 >
                                                                    <%# ClassQuaem.FAlBaheth((Int32) (Eval("_CreatedBy_")))%>
                                                                </h5>
                                                                <p style='padding: 10px 10px 0 10px; font-size: 13px'>
                                                                    <%# Eval("_The_Title_") %>
                                                                </p>
                                                                <div class="col-md-8">
                                                                    <div align='center'>
                                                                        <span style='font-size: 12px'>
                                                                            <%# Eval("_CreatedDate_", "{0:dd/MM/yyyy HH:mm tt}") %>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <a class="btn btn-default btn-sm download_button" style="<%# ClassSaddam.FCheckNullFile(Eval("_Src_").ToString()) %>"
                                                                        href="/<%# Eval("_Src_") %>" data-file="pdf" data-fancybox data-type="iframe" title="عرض الملف" data-toggle="tooltip">
                                                                        <i class="fas fa-file-pdf"></i>
                                                                        <div>
                                                                            <span>عرض الملف </span><small><%# ClassSaddam.FGetTypeFileOutTitle((string)Eval("_Type_File_")) %> </small>
                                                                        </div>
                                                                    </a>
                                                                    <a class="btn btn-default btn-sm download_button" style="<%# ClassSaddam.FCheckNotNullFile(Eval("_Src_").ToString()) %>" data-file="pdf">
                                                                        <i class="fas fa-file-pdf"></i>
                                                                        <div><span>بدون ملف مرفق</span></div>
                                                                    </a>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <hr />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlNull" runat="server" Visible="False">
                                            <br />
                                            <br />
                                            <br />
                                            <div align="center">
                                                <h3 style="font-size: 18px">لا يوجد سجل للمهام بعد !!!
                                                </h3>
                                            </div>
                                            <br />
                                            <br />
                                        </asp:Panel>
                                
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </asp:Panel>
            <div class="content-box-large">
                <div class="widget-box">
                    <div class="container-fluid" dir="rtl">
                        <div class="control-group">
                            <div class="controls">
                                <asp:Panel ID="pnlConvert" runat="server" Visible="true">
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <h5>إضافة تعليق : <span style="color: red">*</span>
                                            </h5>
                                            <asp:TextBox ID="txt_Message" Width="100%" runat="server" Style="direction: rtl; text-align: right;" class="form-control"
                                                ValidationGroup="GVImg" placeholder="نص التعليق ... " TextMode="MultiLine" Rows="6"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="إدخل نص الرسالة" CssClass="font"
                                                ControlToValidate="txt_Message" ValidationGroup="GVImg" Font-Size="10px" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <h5>حدد الملف إن وجد : 
                                            </h5>
                                            <asp:FileUpload ID="FUFiles" runat="server" AllowMultiple="false" ValidationGroup="GVImg" />
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <asp:LinkButton ID="LBImage" runat="server" CssClass="btn btn-info" data-toggle="tooltip" title="حفظ البيانات"
                                                    ValidationGroup="GVImg" OnClick="LBImage_Click">
                                                حفظ البيانات </asp:LinkButton>

                                                <asp:LinkButton ID="LB_Back" runat="server" CssClass="btn btn-danger">خروج</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlConverted" runat="server" Visible="False">
                                    <br />
                                    <hr />
                                    <br />
                                    <div align="center">
                                        <h3 style="font-size: 18px">تم تحويل المهمة ,,, 
                                        </h3>
                                    </div>
                                    <br />
                                    <br />
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </div>
</div>
<div id="IDModelConvert" class="modal fade in modal_New_Style">
    <div class="modal-dialog modal-lg">
        <div class="modal-content">
            <div class="modal-header no-border">
                <button type="button" class="close" data-dismiss="modal">×</button>
            </div>
            <div class="modal-body" id="modal_ajax_content">
                <div class="page-container">
                    <div class="page-content">
                        <div class=" panel-body">
                            <label>
                                <i class="fa fa-star"></i> 
                                واجهة تحويل المهمة <asp:Label ID="lblTitle" runat="server"></asp:Label> : 
                            </label>
                            <div>
                                <div class="" dir="rtl">
                                    <div class="collapse2" id="navbarToggleExternalContent" style="background-color: #f6f7f6; border-radius:8px;">
                                        <div class="container-fluid" dir="rtl">
                                            <div class="container-fluid">
                                                <div runat="server" id="IDEmp" class="col-lg-4" visible="false">
                                                    <div class="form-group">
                                                        <h5>حدد الموظفين : <i style="color: red">*</i></h5>
                                                        <a href='javaScript:void(0)' data-toggle="modal" data-target="#IDModel">
                                                            <i class="fa fa-eye"></i>عرض الموظفين
                                                        </a>
                                                    </div>
                                                </div>
                                                <div id="IDModel" class="modal fade in modal_New_Style">
                                                    <div class="modal-dialog " style="max-width: 650px">
                                                        <div class="modal-content">
                                                            <div class="modal-header no-border">

                                                            </div>
                                                            <div class="modal-body" id="modal_ajax_content">
                                                                <div class="page-container">
                                                                    <div class="page-content">
                                                                        <div class=" panel-body">
                                                                            <label>
                                                                                <i class="fa fa-star"></i>حدد الموظفين المناسبين لهذه المهمة <span class="text-danger">*</span>
                                                                            </label>
                                                                            <div class="checkbox checkbox-primary">
                                                                                <asp:CheckBoxList ID="CBEmployee" runat="server"
                                                                                    RepeatDirection="Vertical" CssClass="styled" Width="100%">
                                                                                    <asp:ListItem></asp:ListItem>
                                                                                </asp:CheckBoxList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        نوع المهمة : <br />
                                                        <asp:Label ID="lblAssignment_Title2" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        رقم المهمة : <br />
                                                        <asp:Label ID="lblNumberJob2" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="container-fluid">
                                                <div class="col-md-8">
                                                    <div class="form-group">
                                                        المهمة : <br />
                                                        <asp:Label ID="lblThe_Assignment2" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        ساعات العمل في اليوم : <br />
                                                        <asp:Label ID="lblHours_In_Day2" runat="server"></asp:Label> / ساعات
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="container-fluid">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        تاريخ بدأ المهمة : <br />
                                                        <asp:Label ID="lblDate_Job2" runat="server"></asp:Label>
                                                        <asp:Panel ID="pnl_Start" runat="server" Visible="false">
                                                            <div class="input-group date " >
                                                                    <asp:TextBox ID="txtDate_Job" runat="server" placeholder="تاريخ المهمة ... " class="form-control"
                                                                        data-date-format="YYYY-MM-DD" ValidationGroup="VGExtension" Style="text-align: center;"></asp:TextBox>
                                                                    <span class="input-group-btn">
                                                                        <button class="btn btn-default" type="button">
                                                                            <i class="fa fa-calendar"></i>
                                                                        </button>
                                                                    </span>
                                                                </div>
                                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                                                ControlToValidate="txtDate_Job" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGExtension" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        تاريخ إنتهاء المهمة : <br />
                                                        <asp:Label ID="lblDateEnd_Job2" runat="server"></asp:Label>
                                                        <asp:Panel ID="pnl_End" runat="server" Visible="false">
                                                            <div class="input-group date " >
                                                                <asp:TextBox ID="txtDateEnd_Job" runat="server" placeholder="تاريخ المهمة ... " class="form-control"
                                                                    data-date-format="YYYY-MM-DD" ValidationGroup="VGExtension" Style="text-align: center;"></asp:TextBox>
                                                                <span class="input-group-btn">
                                                                    <button class="btn btn-default" type="button">
                                                                        <i class="fa fa-calendar"></i>
                                                                    </button>
                                                                </span>
                                                            </div>
                                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                                            ControlToValidate="txtDateEnd_Job" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGExtension" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <h5 class="control-label">
                                                                تحويل ملف المتابعة : 
                                                        </h5> 
                                                        <div class="col-md-12">
                                                            <div class="checkbox checkbox-primary" align="right">
                                                                <asp:CheckBox ID="CBCheckRicord" runat="server" 
                                                                    Text="نعم / لا" CssClass="styled" Checked="true" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="container-fluid">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        شرح المهمة : <br />
                                                        <asp:Label ID="lblThe_Qriah2" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:Panel ID="pnl_Convert" runat="server" Visible="false">
                                                <div class="col-md-4">
                                                    <div class="form-group">
                                                        <h5 class="control-label">
                                                                حدد الإدارة : <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                                        </h5>
                                                        <div class="col-md-12">
                                                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" Width="300px" ValidationGroup="VGConvert"
                                                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control2 chzn-select dropdown">
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDepartment" SetFocusOnError="true" ControlToValidate="ddlDepartment" ValidationGroup="VGConvert"
                                                                CssClass="required" Display="Dynamic"  Font-Size="10px" ErrorMessage="* حدد الإدارة" runat="server"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-5">
                                                    <div class="form-group">
                                                        <h5 class="control-label">
                                                                حدد الموظف : <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span> <span id="lblPhone" runat="server"></span>
                                                        </h5> 
                                                        <div class="col-md-12">
                                                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control2 chzn-select dropdown" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" Width="300px" ValidationGroup="VGConvert">
                                                                <asp:ListItem></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvEmployee" SetFocusOnError="true" ControlToValidate="ddlEmployee" ValidationGroup="VGConvert"
                                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الموظف" runat="server"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:LinkButton ID="LB_Convert_New" runat="server" CssClass="btn btn-info" data-toggle="tooltip" title="حفظ البيانات"
                                ValidationGroup="VGConvert" OnClick="LB_Convert_New_Click" OnClientClick="return Confirmation();">
                            تحويل المهمة </asp:LinkButton>
                            <asp:LinkButton ID="LB_Extension_New" runat="server" CssClass="btn btn-info" data-toggle="tooltip" title="حفظ البيانات"
                                ValidationGroup="VGExtension" OnClick="LB_Extension_New_Click" OnClientClick="return Confirmation();">
                            تمديد المهمة </asp:LinkButton>
                            <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
<hr />
<asp:HiddenField ID="HFIDStore" runat="server" />
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
<link href="../../DMS/jquery.fancybox.min.css" rel="stylesheet" />
<script src="../../DMS/jquery.fancybox.min.js"></script>
<asp:HiddenField ID="HFName" runat="server" />
<asp:HiddenField ID="HFIDNumber" runat="server" />
<asp:HiddenField ID="HFPhoneManager" runat="server" />
<asp:HiddenField ID="HFPhoneAdmin" runat="server" />
<asp:HiddenField ID="HFEmp_Allow" runat="server" />
<asp:HiddenField ID="HFEnd" runat="server" />
<asp:HiddenField ID="HFStope" runat="server" />
<asp:HiddenField ID="HFConvert" runat="server" />

<asp:HiddenField ID="HFPhone" runat="server" />
<asp:HiddenField ID="HFEmail" runat="server" />

<asp:HiddenField ID="HFStartDate" runat="server" />
<asp:HiddenField ID="HFEndDate" runat="server" />
