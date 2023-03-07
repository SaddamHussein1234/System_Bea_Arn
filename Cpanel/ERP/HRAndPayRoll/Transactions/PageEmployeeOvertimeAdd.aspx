<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="PageEmployeeOvertimeAdd.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Transactions_PageEmployeeOvertimeAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnAdd.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>

    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث" OnClick="btnRefrish_Click"><i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../../Default.aspx">الرئيسية</a></li>
                    <li><a href="PageEmployeeOvertimes.aspx">قائمة قرارات العمل الإضافي</a></li>
                    <li><a href="#">إضافة/تعديل قرار عمل إضافي </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title" style="float:right">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة/تعديل قرار عمل إضافي"></asp:Label>
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
                    <div class=" ">
                        <div class=" ">
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
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label class="control-label">
                                            رقم القرار : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                            <asp:TextBox ID="txtNumberOverTime" runat="server" CssClass="form-control" ValidationGroup="g2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true" ControlToValidate="txtNumberOverTime" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* رقم القرض" runat="server"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumberOverTime"
                                                ErrorMessage="* أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                                Display="Dynamic" Font-Size="10px">
                                            </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label">
                                              حدد الإدارة : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" ValidationGroup="g2"
                                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control chzn-select dropdown">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDepartment" SetFocusOnError="true" ControlToValidate="ddlDepartment" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic"  Font-Size="10px" ErrorMessage="* حدد الإدارة" runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="control-label">
                                             حدد الموظف :  <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label> <span id="lblPhone" runat="server"></span>
                                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control chzn-select dropdown" AutoPostBack="true"
                                                 ValidationGroup="g2" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
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
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <label class="control-label" id="Label8" runat="server">
                                            تاريخ الإضافة : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                            <div class="input-group date ">
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
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label class="control-label" id="lblBasic" runat="server">
                                             الراتب بالساعة :
                                        </label>
                                        <div class="col-md-12">
                                            <asp:HiddenField ID="HFBaiscHours" runat="server" />
                                            <asp:Label ID="lblBaiscHours" runat="server" Text="0"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label class="control-label" id="Label7" runat="server">
                                             الإجمالي باليوم :
                                        </label>
                                        <div class="col-md-12">
                                            <asp:HiddenField ID="HFAmount" runat="server" />
                                            <asp:Label ID="lblAmount" runat="server" Text="0"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label" id="Label3" runat="server">
                                           عدد أيام العمل <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="ddlTotalDays" runat="server" CssClass="form-control2 chzn-select dropdown"
                                                Width="100%" ValidationGroup="g2" onkeyup="EmployeeOverTimeSave.CalculateInstallment()" onkeypress="return Common.isNumberKey(event)">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="1.00">01</asp:ListItem>
                                                <asp:ListItem Value="1.50">01.50</asp:ListItem>
                                                <asp:ListItem Value="2.00">02</asp:ListItem>
                                                <asp:ListItem Value="2.50">02.50</asp:ListItem>
                                                <asp:ListItem Value="3.00">03</asp:ListItem>
                                                <asp:ListItem Value="3.50">03.50</asp:ListItem>
                                                <asp:ListItem Value="4.00">04</asp:ListItem>
                                                <asp:ListItem Value="4.50">04.50</asp:ListItem>
                                                <asp:ListItem Value="5.00">05</asp:ListItem>
                                                <asp:ListItem Value="5.50">05.50</asp:ListItem>
                                                <asp:ListItem Value="6.00">06</asp:ListItem>
                                                <asp:ListItem Value="6.50">06.50</asp:ListItem>
                                                <asp:ListItem Value="7.00">07</asp:ListItem>
                                                <asp:ListItem Value="7.50">07.50</asp:ListItem>
                                                <asp:ListItem Value="8.00">08</asp:ListItem>
                                                <asp:ListItem Value="8.50">08.50</asp:ListItem>
                                                <asp:ListItem Value="9.00">09</asp:ListItem>
                                                <asp:ListItem Value="9.50">09.50</asp:ListItem>
                                                <asp:ListItem Value="10.00">10</asp:ListItem>
                                                <asp:ListItem Value="10.50">10.50</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" ControlToValidate="ddlTotalDays" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الأيام" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label" id="Label4" runat="server">
                                           عدد ساعات العمل لكل يوم <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:DropDownList ID="ddlTotalHours" runat="server" CssClass="form-control2 chzn-select dropdown" AutoPostBack="true"
                                                Width="100%" ValidationGroup="g2" OnSelectedIndexChanged="ddlTotalHours_SelectedIndexChanged">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="1.00">01</asp:ListItem>
                                                <asp:ListItem Value="1.50">01.50</asp:ListItem>
                                                <asp:ListItem Value="2.00">02</asp:ListItem>
                                                <asp:ListItem Value="2.50">02.50</asp:ListItem>
                                                <asp:ListItem Value="3.00">03</asp:ListItem>
                                                <asp:ListItem Value="3.50">03.50</asp:ListItem>
                                                <asp:ListItem Value="4.00">04</asp:ListItem>
                                                <asp:ListItem Value="4.50">04.50</asp:ListItem>
                                                <asp:ListItem Value="5.00">05</asp:ListItem>
                                                <asp:ListItem Value="5.50">05.50</asp:ListItem>
                                                <asp:ListItem Value="6.00">06</asp:ListItem>
                                                <asp:ListItem Value="6.50">06.50</asp:ListItem>
                                                <asp:ListItem Value="7.00">07</asp:ListItem>
                                                <asp:ListItem Value="7.50">07.50</asp:ListItem>
                                                <asp:ListItem Value="8.00">08</asp:ListItem>
                                                <asp:ListItem Value="8.50">08.50</asp:ListItem>
                                                <asp:ListItem Value="9.00">09</asp:ListItem>
                                                <asp:ListItem Value="9.50">09.50</asp:ListItem>
                                                <asp:ListItem Value="10.00">10</asp:ListItem>
                                                <asp:ListItem Value="10.50">10.50</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true" ControlToValidate="ddlTotalHours" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الساعات" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label" id="Label5" runat="server">
                                             من الساعة  <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label><br />
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlFromHour" runat="server" CssClass="form-control2">
                                                <asp:ListItem Value="01">01</asp:ListItem>
                                                <asp:ListItem Value="02">02</asp:ListItem>
                                                <asp:ListItem Value="03">03</asp:ListItem>
                                                <asp:ListItem Value="04">04</asp:ListItem>
                                                <asp:ListItem Value="05">05</asp:ListItem>
                                                <asp:ListItem Value="06">06</asp:ListItem>
                                                <asp:ListItem Value="07">07</asp:ListItem>
                                                <asp:ListItem Value="08">08</asp:ListItem>
                                                <asp:ListItem Value="09">09</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                <asp:ListItem Value="12">12</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlFromMinute" runat="server" CssClass="form-control2">
                                                <asp:ListItem Value="00">00</asp:ListItem>
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="45">45</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlFromMeridiem" runat="server" CssClass="form-control2">
                                                <asp:ListItem Value="AM">ص</asp:ListItem>
                                                <asp:ListItem Value="PM">م</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="control-label" id="Label6" runat="server">
                                             إلى الساعة  <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label><br />
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlToHour" runat="server" CssClass="form-control2">
                                                <asp:ListItem Value="01">01</asp:ListItem>
                                                <asp:ListItem Value="02">02</asp:ListItem>
                                                <asp:ListItem Value="03">03</asp:ListItem>
                                                <asp:ListItem Value="04">04</asp:ListItem>
                                                <asp:ListItem Value="05">05</asp:ListItem>
                                                <asp:ListItem Value="06">06</asp:ListItem>
                                                <asp:ListItem Value="07">07</asp:ListItem>
                                                <asp:ListItem Value="08">08</asp:ListItem>
                                                <asp:ListItem Value="09">09</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                <asp:ListItem Value="12">12</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlToMinute" runat="server" CssClass="form-control2">
                                                <asp:ListItem Value="00">00</asp:ListItem>
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="45">45</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlToMeridiem" runat="server" CssClass="form-control2">
                                                <asp:ListItem Value="AM">ص</asp:ListItem>
                                                <asp:ListItem Value="PM">م</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label class="control-label" id="Label2" runat="server">
                                            من تاريخ : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="input-group date ">
                                            <asp:TextBox ID="txtStartDate" runat="server" placeholder="من تاريخ ... " class="form-control"
                                                data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align:center;"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                        </div>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtStartDate" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label class="control-label" id="Label1" runat="server">
                                            إلى تاريخ : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="input-group date ">
                                            <asp:TextBox ID="txtEndDate" runat="server" placeholder="إلى تاريخ ... " class="form-control"
                                                data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align:center;"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                        </div>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                                ControlToValidate="txtEndDate" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                
                                <%--<div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label">
                                             رئيس الشؤون المالية والادارية :  <span title="إجباري" data-toggle="tooltip">*</span>
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
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <%--<label class="control-label" id="Label8" runat="server">
                                            تاريخ الإضافة : <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <div class="input-group date " style="margin-right: -10px;">
                                                <asp:TextBox ID="txtDateAdd" runat="server" placeholder="تاريخ الإضافة ... " class="form-control"
                                                    data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="text-align:center;"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                            </div>
                                        </div>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                                ControlToValidate="txtDateAdd" ErrorMessage="* حدد التأريخ" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-9">
                                    <div class="form-group">
                                        <label class="control-label">
                                            مزيد من التفاصيل :
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtDescrption" runat="server" TextMode="MultiLine" Rows="6"
                                                CssClass="form-control" ValidationGroup="g2" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" ControlToValidate="txtDescrption" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* تفاصيل العملية" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
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
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label9" runat="server" Text="المشرف المختص"></asp:Label>
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
</asp:Content>

