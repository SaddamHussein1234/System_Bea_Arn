<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageEmployeeJobAssignmentAdd.ascx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Shaerd_JobAssignments_PageEmployeeJobAssignmentAdd" %>
<div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click"><i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../../Default.aspx">الرئيسية</a></li>
                    <li><a href="PageEmployeeJobAssignmentAdd.aspx">قائمة مهام العمل</a></li>
                    <li><a href="PageEmployeeJobAssignments.aspx">إضافة/تعديل مهام عمل </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title" style="float:right">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة/تعديل مهام عمل"></asp:Label>
                    </h3>
                    <div align="left">
                        <label class="control-label">
                            الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                        </label>
                        <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2" AutoPostBack="true"
                            Width="100" ValidationGroup="g2" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged">
                            <asp:ListItem></asp:ListItem>
                        </asp:DropDownList>
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
                                <div class="col-lg-3">
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
                                                <button type="button" class="close" data-dismiss="modal">×</button>
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
                                                        <div class="modal-footer">
                                                            <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5 class="control-label">
                                            رقم المهام : <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                        </h5>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtNumberJob" runat="server" CssClass="form-control" ValidationGroup="g2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" ControlToValidate="txtNumberJob" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* رقم المهام" runat="server"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumberJob"
                                                ErrorMessage="* أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                                Display="Dynamic" Font-Size="10px">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" visible="false" class="col-md-3">
                                    <div class="form-group">
                                        <h5 class="control-label">
                                              حدد الإدارة : <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                        </h5>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" Width="100%" ValidationGroup="g2"
                                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control2 chzn-select dropdown">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDepartment" SetFocusOnError="true" ControlToValidate="ddlDepartment" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic"  Font-Size="10px" ErrorMessage="* حدد الإدارة" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" visible="false" class="col-md-4">
                                    <div class="form-group">
                                        <h5 class="control-label">
                                             حدد الموظف : <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span> <span id="lblPhone" runat="server"></span>
                                        </h5> 
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control2 chzn-select dropdown" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" Width="100%" ValidationGroup="g2">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEmployee" SetFocusOnError="true" ControlToValidate="ddlEmployee" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الموظف" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <h5 class="control-label">
                                            نوع المهمة : <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                        </h5>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="DLAssignment_Title" runat="server" Width="100%" ValidationGroup="g2"
                                                CssClass="form-control2 chzn-select dropdown">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="مهمة عمل داخلية">مهمة عمل داخلية</asp:ListItem>
                                                <asp:ListItem Value="مهمة عمل خارجية">مهمة عمل خارجية</asp:ListItem>
                                                <asp:ListItem Value="مهمة عمل داخلية وخارجية">مهمة عمل داخلية وخارجية</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" ControlToValidate="DLAssignment_Title" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* نوع المهمة" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <h5 class="control-label">
                                            المهمة : <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                        </h5>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtThe_Assignment" runat="server"
                                                CssClass="form-control" ValidationGroup="g2" Font-Size="14px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="txtThe_Assignment" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* المهمة" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5 class="control-label" id="Label1" runat="server">
                                            تاريخ بداية المهمة : <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                        </h5>
                                            <div class="input-group date " >
                                                <asp:TextBox ID="txtDate_Job" runat="server" placeholder="تاريخ المهمة ... " class="form-control"
                                                    data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align: center;"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                            </div>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="txtDate_Job" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5 class="control-label" id="Label2" runat="server">
                                            تاريخ نهاية المهمة : <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                        </h5>
                                            <div class="input-group date " >
                                                <asp:TextBox ID="txtDateEnd_Job" runat="server" placeholder="تاريخ المهمة ... " class="form-control"
                                                    data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align: center;"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                            </div>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                            ControlToValidate="txtDateEnd_Job" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5 class="control-label">
                                            ساعات العمل في اليوم : <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                        </h5>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtHours_In_Day" runat="server"
                                                CssClass="form-control" ValidationGroup="g2" Font-Size="14px" TextMode="Number"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" ControlToValidate="txtHours_In_Day" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الساعات" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5 class="control-label">
                                             فترة التكليف : <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                        </h5>
                                            <asp:DropDownList ID="DLTime_Assignment" runat="server" CssClass="form-control chzn-select dropdown"
                                                Width="100%" ValidationGroup="g2">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="الفترة الصباحية">الفترة الصباحية</asp:ListItem>
                                                <asp:ListItem Value="الفترة المسائية">الفترة المسائية</asp:ListItem>
                                                <asp:ListItem Value="غير محدد">غير محدد</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" SetFocusOnError="true" ControlToValidate="DLTime_Assignment" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* فترة التكليف" runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div style="display:none;" class="col-md-2">
                                    <div class="form-group">
                                        <h5 class="control-label">
                                             هل يُحتسب إنتداب ؟ : 
                                        </h5>
                                        <script type="text/javascript">
                                            $(function () {
                                                $(document.getElementById("<%=CBIs_Mandate.ClientID %>")).click(function () {
                                                    if ($(this).is(":checked")) {
                                                        $("#pnlMandate").show();
                                                    } else {
                                                        $("#pnlMandate").hide();
                                                    }
                                                });
                                            });
                                        </script>
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBIs_Mandate" runat="server" />
                                            <span class="slider round"></span>
                                        </label>   
                                    </div>
                                </div>
                                <div id="pnlMandate" style="display: none; <%= FCheck("_Mandate") %>" class="col-md-5">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <h5 class="control-label">
                                                 مبلغ الإنتداب لكل يوم  <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                            </h5>
                                                <asp:TextBox ID="txtAmount" MaxLength="10" runat="server" CssClass="form-control" ValidationGroup="g2"
                                                    onkeyup="EmployeeMandateSave.CalculateInstallment()" onkeypress="return Common.isNumericKey(event,this);" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <h5 class="control-label">
                                               عدد أيام الإنتداب <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                            </h5>
                                                <asp:DropDownList ID="ddlTotalDays" runat="server" CssClass="form-control2 chzn-select dropdown"
                                                    Width="100%" ValidationGroup="g2" onkeyup="EmployeeMandateSave.CalculateInstallment()" onkeypress="return Common.isNumberKey(event)">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="1">01</asp:ListItem>
                                                    <asp:ListItem Value="2">02</asp:ListItem>
                                                    <asp:ListItem Value="3">03</asp:ListItem>
                                                    <asp:ListItem Value="4">04</asp:ListItem>
                                                    <asp:ListItem Value="5">05</asp:ListItem>
                                                    <asp:ListItem Value="6">06</asp:ListItem>
                                                    <asp:ListItem Value="7">07</asp:ListItem>
                                                    <asp:ListItem Value="8">08</asp:ListItem>
                                                    <asp:ListItem Value="9">09</asp:ListItem>
                                                </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-9">
                                    <div class="form-group">
                                        <h5 class="control-label">
                                            شرح المهمة :
                                        </h5>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtThe_Qriah" runat="server" TextMode="MultiLine" Rows="3"
                                                CssClass="form-control" ValidationGroup="g2" Font-Size="14px"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5 class="control-label">
                                                إرسال إشعار نصي <i class="fa fa-envelope"></i> : <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                        </h5>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="DLSend" runat="server" ValidationGroup="g2"
                                                CssClass="form-control2 chzn-select dropdown" Width="100%" >
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="Yes">نعم أرسل</asp:ListItem>
                                                <asp:ListItem Value="No">لا تقم بالإرسل</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                                ControlToValidate="DLSend" ErrorMessage="* حدد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="IDAccess" runat="server" visible="false" class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label3" runat="server" Text="المشرف المختص"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5 class="control-label">
                                             مدير الجمعية :  <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                            <label class="switch">
                                                <input runat="server" id="RB_Moder" type="radio" name="cars" onchange="show2()" checked />
                                                <span class="slider round"></span>
                                            </label> 
                                        </h5>
                                        <div id="pnlModer" style="display: none; <%= FCheck("_Moder") %>" class="col-md-12"><br />
                                            <asp:DropDownList ID="DLModer" runat="server" CssClass="form-control2 chzn-select dropdown"
                                                Width="100%" ValidationGroup="g2">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" ControlToValidate="DLModer" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* مدير الجمعية" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5 class="control-label">
                                             رئيس مجلس الإدارة :  <span style="color:#e80505" title="إجباري" data-toggle="tooltip">*</span>
                                            <label class="switch">
                                                <input runat="server" type="radio" id="RB_Raees" name="cars" onchange="show(this.value)" />
                                                <span class="slider round"></span>
                                            </label> 
                                        </h5>
                                        <script type="text/javascript">
                                            function show(str) {
                                                document.getElementById('pnlModer').style.display = 'none';
                                                document.getElementById('pnlRaees').style.display = 'block';
                                            }
                                            function show2(sign) {
                                                document.getElementById('pnlModer').style.display = 'block';
                                                document.getElementById('pnlRaees').style.display = 'none';
                                            }
                                        </script>
                                        <div id="pnlRaees" style="display: none; <%= FCheck("_Raees") %>" class="col-md-12"><br />
                                            <asp:DropDownList ID="DLIDRaees" runat="server" CssClass="form-control2 chzn-select dropdown"
                                                Width="100%" ValidationGroup="g2">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" ControlToValidate="DLIDRaees" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد مجلس الإدارة" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div align="left">
                <asp:Button ID="btnAdd" runat="server" Text="حفظ المهام والذهاب إلى المرفقات"
                    class="btn btn-info" OnClick="btnAdd_Click" ValidationGroup="g2" />
                <asp:LinkButton ID="LBBack" runat="server" OnClick="LBBack_Click"
                    class="btn btn-danger">خروج</asp:LinkButton>
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
        <asp:HiddenField ID="HFPhone" runat="server" />
        <asp:HiddenField ID="HFEmail" runat="server" />