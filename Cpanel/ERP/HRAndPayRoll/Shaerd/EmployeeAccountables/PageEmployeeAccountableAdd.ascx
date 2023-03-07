<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageEmployeeAccountableAdd.ascx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Shaerd_EmployeeAccountables_PageEmployeeAccountableAdd" %>
<div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click"><i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../../Default.aspx">الرئيسية</a></li>
                    <li><a href="PageEmployeeAccountables.aspx">قائمة المساءلات</a></li>
                    <li><a href="PageEmployeeAccountableAdd.aspx">إضافة/تعديل إجراء مساءلة </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title" style="float:right">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة/تعديل إجراء مساءلة"></asp:Label>
                    </h3>
                    <div align="left">
                        <label class="control-label">
                            الارشيف <span title="إجباري" data-toggle="tooltip">*</span>
                        </label>
                        <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlYears_SelectedIndexChanged" Width="100" ValidationGroup="g2">
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
                                <div class="col-md-12" runat="server" id="ID_Raees" visible="false">
                                    <div class="form-group">
                                        <label class="control-label">
                                            قرار رئيس الشؤون المالية والادارية : 
                                        </label>
                                        <br /><br />
                                        <div class="keepmeLogged">
                                            <label class="switch" id="IDAllow_Accountable" runat="server">
                                                <input name="RememberMe" type="radio" id="CB_Allow_Accountable" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme" style="font-size:14px;">
                                                    تحتسب له إجازة مرضية بعد التأكد من نظامية التقرير
                                                </span>
                                            </label>
                                            <br /><br />
                                            <label class="switch">
                                                <input name="RememberMe" type="radio" id="CB_Allow_Resolved" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme" style="font-size:14px;">يعتمد الحسم لعدم قبول العذر </span>
                                            </label>
                                            <br /><br />
                                            <label class="switch">
                                                <input name="RememberMe" type="radio" id="CB_Warning_Oral" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme" style="font-size:14px;">إنذار شفهي </span>
                                            </label>
                                            <br /><br />
                                            <label class="switch">
                                                <input name="RememberMe" type="radio" id="CB_Warning_Written" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme" style="font-size:14px;">إنذار كتابي </span>
                                            </label>
                                            <br /><br />
                                            <label class="switch">
                                                <input name="RememberMe" type="radio" id="CB_Warning_Final" runat="server" />
                                                <span class="slider round"></span>
                                                <span class="keepme" style="font-size:14px;">إنذار نهائي </span>
                                            </label>
                                        </div>
                                    </div>
                                </div> 
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label class="control-label">
                                            رقم الإجراء : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtNumberAccountable" runat="server" CssClass="form-control" ValidationGroup="g2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" ControlToValidate="txtNumberAccountable" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* رقم الإجراء" runat="server"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumberAccountable"
                                                ErrorMessage="* أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                                Display="Dynamic" Font-Size="10px">
                                            </asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="pnlDepartment" class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label">
                                              حدد الإدارة : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
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
                                <div runat="server" id="pnlEmployee" class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">
                                             حدد الموظف :  <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label> <span id="lblPhone" runat="server"></span>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control2 chzn-select dropdown"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" Width="100%" ValidationGroup="g2">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEmployee" SetFocusOnError="true" ControlToValidate="ddlEmployee" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الموظف" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div runat="server" id="pnlAccountableType" class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label">
                                             نوع المساءلة :  <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="ddlAccountableType" runat="server" CssClass="form-control2 chzn-select dropdown"
                                                Width="100%" ValidationGroup="g2" AutoPostBack="true" OnSelectedIndexChanged="ddlAccountableType_SelectedIndexChanged">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" ControlToValidate="ddlAccountableType" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد نوع المساءلة" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">
                                            سبب المساءلة :
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtDescrption" runat="server" TextMode="MultiLine" Rows="4"
                                                CssClass="form-control" ValidationGroup="g2" Font-Size="14px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="txtDescrption" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* سبب المساءلة" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="control-label">
                                            طلب الإفادة :
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtStatement_Request" runat="server" TextMode="MultiLine" Rows="4"
                                                CssClass="form-control" ValidationGroup="g2" Font-Size="14px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" ControlToValidate="txtStatement_Request" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* طلب الإفادة" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="pnlThe_Statement" class="container-fluid" dir="rtl">
                                <div class="col-md-9">
                                    <div class="form-group">
                                        <label class="control-label">
                                            الإفادة :
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtThe_Statement" runat="server" TextMode="MultiLine" Rows="10"
                                                CssClass="form-control" ValidationGroup="g2" Font-Size="14px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" ControlToValidate="txtThe_Statement" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* الإفادة" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label">
                                            المرفقات :
                                        </label>
                                        <asp:FileUpload ID="FUFiles" runat="server" AllowMultiple="false" ValidationGroup="GVImg" />
                                    </div>
                                </div>
                            </div>
                            <div runat="server" id="pnlOther" class="container-fluid" dir="rtl">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label class="control-label" id="Label2" runat="server">
                                            تاريخ المساءلة : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <div class="input-group date " style="margin-right: -10px;">
                                                <asp:TextBox ID="txtDate_Accountable" runat="server" placeholder="تاريخ المساءلة ... " class="form-control"
                                                    data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align:center;"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                            </div>
                                        </div>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtDate_Accountable" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <%--<div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">
                                             رئيس الشؤون المالية :  <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="DLIDRaees" runat="server" CssClass="form-control2 chzn-select dropdown"
                                                Width="100%" ValidationGroup="g2">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true" ControlToValidate="DLIDRaees" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد رئيس الشؤون" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>--%>
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
                        </div>
                    </div>
                </div>
            </div>
            <div runat="server" id="IDAccess" class="panel panel-default">
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
                                            <asp:DropDownList ID="DLRaees" runat="server" CssClass="form-control2 chzn-select dropdown"
                                                Width="100%" ValidationGroup="g2">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" SetFocusOnError="true" ControlToValidate="DLRaees" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد مجلس الإدارة" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
            <br />

        </div>
        <div class="container-fluid">
            <div align="left">
                <asp:Button ID="btnAdd" runat="server" Text="حفظ البيانات" Style="font-size: medium"
                    class="btn btn-info" OnClick="btnAdd_Click" ValidationGroup="g2" />
                <asp:LinkButton ID="LBBack" runat="server" Style="font-size: medium" OnClick="LBBack_Click"
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