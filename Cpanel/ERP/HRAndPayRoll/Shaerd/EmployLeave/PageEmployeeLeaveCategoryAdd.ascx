<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageEmployeeLeaveCategoryAdd.ascx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Shaerd_EmployLeave_PageEmployeeLeaveCategoryAdd" %>

<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
        </div>
        <h1>لوحة التحكم</h1>
        <ul class="breadcrumb">
            <li><a href="../../Default.aspx">الرئيسية</a></li>
            <li><a href="PageEmployeeLeaveCategory.aspx">قائمة إجازات الموظفين</a></li>
            <li><a href="#">إضافة/تعديل إجازات الموظفين </a></li>
        </ul>
    </div>
</div>
<div class="container-fluid">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title" style="float:right">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="lbmsg" runat="server" Text="إضافة/تعديل إجازات الموظفين"></asp:Label>
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
                    <div class="col-lg-12" style="border: 2px solid #c0c0c0; border-radius: 7px; margin-bottom: 5px;" 
                        runat="server" id="IDLeaveDetails" visible="false">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="control-label">
                                    رصيد الإجازات :
                                </label>
                                <br />
                                <div class="col-md-12">
                                    <div class="row">
                                        <asp:HiddenField ID="HFCountDay" runat="server" />
                                        الاعتيادية :  <asp:Label ID="lblCountDay" runat="server"></asp:Label> / يوم 
                                        <hr style='border: solid; border-width: 1px; width: 100%; margin:10px 0 10px 0;' />
                                        <asp:HiddenField ID="HFCountEmergency" runat="server" />
                                        الاضطرارية :  <asp:Label ID="lblCountEmergency" runat="server"></asp:Label> / يوم 
                                        <hr style='border: solid; border-width: 1px; width: 100%; margin:10px 0 10px 0;' />
                                        التعويضية :  <asp:HiddenField ID="HFCountCompensatory" runat="server" />
                                        <asp:Label ID="lblCountCompensatory" runat="server"></asp:Label> / يوم
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="control-label">
                                    رصيد الإجازات المستخدمة :
                                </label>
                                <br />
                                <div class="col-md-12">
                                    <div class="row">
                                        الاعتيادية :  <asp:HiddenField ID="HFCountDayUse" runat="server" />
                                        <asp:Label ID="lblCountDayUse" runat="server"></asp:Label> / يوم 
                                        <hr style='border: solid; border-width: 1px; width: 100%; margin:10px 0 10px 0;' />
                                        <asp:HiddenField ID="HFCountEmergencyUse" runat="server" />
                                        الاضطرارية :  <asp:Label ID="lblCountEmergencyUse" runat="server"></asp:Label> / يوم 
                                        <hr style='border: solid; border-width: 1px; width: 100%; margin:10px 0 10px 0;' />
                                        التعويضية :  <asp:HiddenField ID="HFCountCompensatoryUse" runat="server" />
                                        <asp:Label ID="lblCountCompensatoryUse" runat="server"></asp:Label> / يوم 
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                <label class="control-label">
                                    رصيد الإجازات المتبقي :
                                </label>
                                <br />
                                <div class="col-md-12">
                                    <div class="row">
                                        الاعتيادية :  <asp:HiddenField ID="HFCountDayAllow" runat="server" />
                                        <asp:Label ID="lblCountDayAllow" runat="server"></asp:Label> / يوم 
                                        <hr style='border: solid; border-width: 1px; width: 100%; margin:10px 0 10px 0;' />
                                        <asp:HiddenField ID="HFCountEmergencyAllow" runat="server" />
                                        الاضطرارية :  <asp:Label ID="lblCountEmergencyAllow" runat="server"></asp:Label> / يوم 
                                        <hr style='border: solid; border-width: 1px; width: 100%; margin:10px 0 10px 0;' />
                                        التعويضية :  <asp:HiddenField ID="HFCountCompensatoryAllow" runat="server" />
                                        <asp:Label ID="lblCountCompensatoryAllow" runat="server"></asp:Label> / يوم 
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label">
                                    رقم الإجازة : <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                    <asp:TextBox ID="txtNumberLeave" runat="server" CssClass="form-control" ValidationGroup="g2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" ControlToValidate="txtNumberLeave" ValidationGroup="g2"
                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* رقم الإجازة" runat="server"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumberLeave"
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
                                        OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control2 chzn-select dropdown">
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
                                <label class="control-label" id="Label3" runat="server">
                                    حدد نوع الإجازة <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                    <asp:DropDownList ID="ddlLeaveCategory" runat="server" CssClass="form-control2 chzn-select dropdown"
                                        Width="100%" ValidationGroup="g2" onkeyup="EmployeeLoanSave.CalculateInstallment()" onkeypress="return Common.isNumberKey(event)">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" ControlToValidate="ddlLeaveCategory" ValidationGroup="g2"
                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد نوع الإجازة" runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="control-label" id="Label2" runat="server">
                                    من تاريخ : <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                <div class="input-group date " style="margin-right: -10px;">
                                    <asp:TextBox ID="txtStartDate" runat="server" placeholder="من تاريخ ... " class="form-control"
                                        data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align:center;"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtStartDate" ErrorMessage="* من تاريخ" ForeColor="#FF0066"
                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="control-label" id="Label1" runat="server">
                                    إلى تاريخ : <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                <div class="input-group date " style="margin-right: -10px;">
                                    <asp:TextBox ID="txtEndDate" runat="server" placeholder="إلى تاريخ ... " class="form-control"
                                        data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align:center;"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                </div>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="txtEndDate" ErrorMessage="* إلى تاريخ" ForeColor="#FF0066"
                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">
                                    إجمالي أيام الإجازة : <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                    <asp:TextBox ID="txtTotalLeave" MaxLength="2" runat="server" CssClass="form-control" ValidationGroup="g2"
                                        onkeypress="return Common.isNumericKey(event,this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="txtTotalLeave" ValidationGroup="g2"
                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* إجمالي الأيام" runat="server"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTotalLeave"
                                        ErrorMessage="* أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                        Display="Dynamic" Font-Size="10px">
                                    </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div runat="server" id="IDView" class="col-md-3">
                            <div class="form-group">
                                <label class="control-label">
                                    النظر في نصف إجازة : 
                                </label>
                                <br />
                                <div class="keepmeLogged">
                                    <label class="switch">
                                        <input name="RememberMe" type="checkbox" id="chkStartDateHalfLeave" runat="server" />
                                        <span class="slider round"></span>
                                        <span class="keepme">تاريخ البدء نصف إجازة </span>
                                    </label>
                                    <br />
                                    <label class="switch">
                                        <input name="RememberMe" type="checkbox" id="chkEndDateHalfLeave" runat="server" />
                                        <span class="slider round"></span>
                                        <span class="keepme">تاريخ الانتهاء نصف إجازة </span>
                                    </label>
                                </div>
                            </div>
                        </div>  
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-md-8">
                            <div class="form-group">
                                <label class="control-label">
                                    إستخدام الرصيد : 
                                </label>
                                <br />
                                <div class="keepmeLogged">
                                    <label class="switch">
                                        <input name="RememberMe" type="radio" id="CB_Basic" runat="server" />
                                        <span class="slider round"></span>
                                        <span class="keepme">الاعتيادية </span>
                                    </label>
                                    <label class="switch">
                                        <input name="RememberMe" type="radio" id="CB_Emergency" runat="server" />
                                        <span class="slider round"></span>
                                        <span class="keepme">الاضطرارية </span>
                                    </label>
                                            
                                    <label class="switch">
                                        <input name="RememberMe" type="radio" id="CB_Compensatory" runat="server" />
                                        <span class="slider round"></span>
                                        <span class="keepme">التعويضية </span>
                                    </label>
                                    <label class="switch">
                                        <input name="RememberMe" type="radio" id="CB_Sick" runat="server" />
                                        <span class="slider round"></span>
                                        <span class="keepme">المرضية </span>
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label class="control-label">
                                        حدد الموظف البديل :  <span title="إجباري" data-toggle="tooltip">*</span>
                                </label> <span id="lblPhone2" runat="server"></span>
                                    <asp:DropDownList ID="DLIDEmp" runat="server" CssClass="form-control2 chzn-select dropdown"
                                        AutoPostBack="true" OnSelectedIndexChanged="DLIDEmp_SelectedIndexChanged" Width="100%" ValidationGroup="g2">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" ControlToValidate="DLIDEmp" ValidationGroup="g2"
                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الموظف البديل" runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-md-4" style="display:none;">
                            <div class="form-group">
                                <label class="control-label">
                                        رئيس الشؤون المالية :  <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                    <asp:DropDownList ID="DLIDRaeesShoon" runat="server" CssClass="form-control2 chzn-select dropdown"
                                        Width="100%" ValidationGroup="g2">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" ControlToValidate="DLIDRaeesShoon" ValidationGroup="g2"
                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد رئيس الشؤون" runat="server"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-md-9">
                            <div class="form-group">
                                <label class="control-label">
                                    سبب الإجازة : <span title="إجباري" data-toggle="tooltip">*</span>
                                </label>
                                <div class="col-md-12">
                                    <asp:TextBox ID="txtDescrption" runat="server" TextMode="MultiLine" Rows="6"
                                        CssClass="form-control" ValidationGroup="g2" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" ControlToValidate="txtDescrption" ValidationGroup="g2"
                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* سبب الإجازة" runat="server"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label class="control-label">
                                         إشعار نصي <i class="fa fa-envelope"></i> : <span title="إجباري" data-toggle="tooltip">*</span>
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
                </div>
            </div>
        </div>
    </div>
    <div id="IDAccess" runat="server" visible="false" class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="Label4" runat="server" Text="المشرف المختص"></asp:Label>
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
<asp:HiddenField ID="HFPhone2" runat="server" />