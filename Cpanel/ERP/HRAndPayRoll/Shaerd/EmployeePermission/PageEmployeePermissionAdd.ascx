<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageEmployeePermissionAdd.ascx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeePermission_PageEmployeePermissionAdd" %>
<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                title="تحديث" OnClick="btnRefrish_Click"><i class="fa fa-refresh"></i></asp:LinkButton>
        </div>
        <h1>لوحة التحكم</h1>
        <ul class="breadcrumb">
            <li><a href="../../Default.aspx">الرئيسية</a></li>
            <li><a href="PageEmployeePermission.aspx">قائمة قرارات المكافآت</a></li>
            <li><a href="PageEmployeePermissionAdd.aspx">إضافة/تعديل إستئذان موظف </a></li>
        </ul>
    </div>
</div>
<div class="container-fluid">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title" style="float:right">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="lbmsg" runat="server" Text="إضافة/تعديل إستئذان موظف"></asp:Label>
            </h3>
            <div align="left">
                <label class="control-label">
                    الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                </label>
                <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2"
                    Width="100" ValidationGroup="g2" AutoPostBack="true" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged">
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
                        <div class="col-lg-12" style="border: 2px solid #c0c0c0; border-radius: 7px; margin-bottom: 5px;" 
                        runat="server" id="IDDetails" visible="false">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label class="control-label">
                                    رصيد الإستئذانات خلال الشهر : 
                                    <asp:LinkButton ID="LBRefresh" runat="server" data-toggle="tooltip" OnClick="LBRefresh_Click"
                                        title="تحديث حسب التاريخ"><i class="fa fa-refresh"></i></asp:LinkButton>
                                </label>
                                <br />
                                <div class="col-md-4">
                                    <div class="row">
                                        الإجمالي :  
                                        <asp:HiddenField ID="HFCountAll" runat="server" />
                                        <asp:Label ID="lblCountAll" runat="server" Text="3"></asp:Label> / <span style="font-size:11px;">إستئذانات</span>  
                                        <hr style='border: solid; border-width: 1px; width: 100%; margin:10px 0 10px 0;' />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        <asp:HiddenField ID="HFCountUse" runat="server" />
                                        المستخدم :  <asp:Label ID="lblCountUse" runat="server"></asp:Label> / <span style="font-size:11px;">إستئذانات</span> 
                                        <hr style='border: solid; border-width: 1px; width: 100%; margin:10px 0 10px 0;' />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="row">
                                        المتبقي :  <asp:HiddenField ID="HFCountAllow" runat="server" />
                                        <asp:Label ID="lblCountAllow" runat="server"></asp:Label> / <span style="font-size:11px;">إستئذانات</span>  
                                        <hr style='border: solid; border-width: 1px; width: 100%; margin:10px 0 10px 0;' />
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label">
                                    رقم الإستئذان : <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                    <asp:TextBox ID="txtNumberPermission" runat="server" CssClass="form-control" ValidationGroup="g2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" ControlToValidate="txtNumberPermission" ValidationGroup="g2"
                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* رقم الإستئذان" runat="server"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumberPermission"
                                        ErrorMessage="* أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                        Display="Dynamic" Font-Size="10px">
                                    </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div runat="server" id="IDDepartment" class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">
                                        حدد الإدارة : <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" Width="100%" ValidationGroup="g2"
                                            CssClass="form-control2 chzn-select dropdown" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvDepartment" SetFocusOnError="true" ControlToValidate="ddlDepartment" ValidationGroup="g2"
                                        CssClass="required" Display="Dynamic"  Font-Size="10px" ErrorMessage="* حدد الإدارة" runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div runat="server" id="IDEmployee" class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">
                                        حدد الموظف :  <span title="إجباري" data-toggle="tooltip">*</span>
                                </label> <span id="lblPhone" runat="server"></span>
                                    <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control2 chzn-select dropdown" AutoPostBack="true"
                                        Width="100%" ValidationGroup="g2" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvEmployee" SetFocusOnError="true" ControlToValidate="ddlEmployee" ValidationGroup="g2"
                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الموظف" runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">
                                    عنوان العملية : <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                    <asp:TextBox ID="txtTitle" MaxLength="20" runat="server" CssClass="form-control" ValidationGroup="g2"
                                        onkeypress="return Common.isNumericKey(event,this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="txtTitle" ValidationGroup="g2"
                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* عنوان العملية" runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="control-label" id="Label8" runat="server">
                                    تاريخ الإضافة : <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                    <div class="input-group date " style="margin-right: -10px;">
                                        <asp:TextBox ID="txtDateAdd" runat="server" placeholder="تاريخ الإضافة ... " class="form-control"
                                            data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align:center;"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                    </div>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                        ControlToValidate="txtDateAdd" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="control-label" id="Label2" runat="server">
                                    تاريخ الإستئذان : <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                <div class="col-md-12">
                                    <div class="input-group date " style="margin-right: -10px;">
                                        <asp:TextBox ID="txtDate_Permission" runat="server" placeholder="حدد تاريخ ... " class="form-control"
                                            data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align:center;"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                    </div>
                                </div>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtDate_Permission" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label class="control-label" id="Label7" runat="server">
                                        طبيعة الإستئذان :
                                </label>
                                <div class="col-md-12">
                                    <label class="switch">
                                        <input name="RememberMe" type="radio" id="CB_Early_Dismissal_" runat="server" />
                                        <span class="slider round"></span>
                                        <span runat="server" id="IDEarly_Dismissal" class="keepme" style="font-size:14px;">الإنصراف المبكر </span>
                                    </label>
                                    <label class="switch">
                                        <input name="RememberMe" type="radio" id="CB_Late_In_Attendance_" runat="server" />
                                        <span class="slider round"></span>
                                        <span runat="server" id="IDLate_In_Attendance" class="keepme" style="font-size:14px;">تأخر في الحضور </span>
                                    </label>
                                    <label class="switch">
                                        <input name="RememberMe" type="radio" id="CB_Exit_And_Return_" runat="server" />
                                        <span class="slider round"></span>
                                        <span runat="server" id="IDExit_And_Return" class="keepme" style="font-size:14px;">الخروج والعودة أثناءالدوام </span>
                                    </label>
                                </div>
                            </div>
                        </div>                                
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-md-12">
                            <hr />
                            <label class="control-label" id="Label1" runat="server">
                                    وقت الإستئذان :
                            </label>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="control-label" id="Label3" runat="server">
                                    من الساعة : <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                <asp:TextBox ID="txtFrom_The_Hour_" runat="server" CssClass="form-control" ValidationGroup="g2" TextMode="Time"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtFrom_The_Hour_" ErrorMessage="* من الساعة" ForeColor="#FF0066"
                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="control-label" id="Label4" runat="server">
                                    إلى الساعة : <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                <asp:TextBox ID="txtTo_The_Hour_" runat="server" CssClass="form-control" ValidationGroup="g2" TextMode="Time"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="txtTo_The_Hour_" ErrorMessage="* إلى الساعة" ForeColor="#FF0066"
                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="control-label">
                                        إرسال إشعار نصي <i class="fa fa-envelope"></i> : <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
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
                    <div class="container-fluid" dir="rtl">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="control-label">
                                    سبب الإستئذان :
                                </label>
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtDescrption" runat="server" TextMode="MultiLine" Rows="6"
                                        CssClass="form-control" ValidationGroup="g2" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" ControlToValidate="txtDescrption" ValidationGroup="g2"
                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* تفاصيل العملية" runat="server"></asp:RequiredFieldValidator>
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
                <asp:Label ID="Label5" runat="server" Text="المشرف المختص"></asp:Label>
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" SetFocusOnError="true" ControlToValidate="DLModer" ValidationGroup="g2"
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" SetFocusOnError="true" ControlToValidate="DLIDRaees" ValidationGroup="g2"
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
        <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="font-size: medium"
            class="btn btn-info" OnClick="btnAdd_Click" ValidationGroup="g2" />
        <asp:LinkButton ID="LBBack" runat="server" Style="font-size: medium" OnClick="LBBack_Click"
            class="btn btn-danger">خروج</asp:LinkButton>
    </div>
    <br />
    <br />
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