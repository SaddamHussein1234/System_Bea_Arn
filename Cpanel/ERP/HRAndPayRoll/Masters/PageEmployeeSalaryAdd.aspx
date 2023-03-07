<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/Main.master" AutoEventWireup="true" CodeFile="PageEmployeeSalaryAdd.aspx.cs" Inherits="Cpanel_ERP_HRAndPayRoll_Masters_PageEmployeeSalaryAdd" %>

<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

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
    <script src="/EmployeeSalarySave.js"></script>

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
    //-->
    </script>

    <script type="text/javascript">
        function ConfirmDelete() {
            var count = document.getElementById("<%=hfCountAllowance.ClientID %>").value;
            var gv = document.getElementById("<%=GVAllowance.ClientID%>");
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

        function ConfirmDeleteDeduction() {
            var count = document.getElementById("<%=hfCountDeduction.ClientID %>").value;
            var gv = document.getElementById("<%=GVDeduction.ClientID%>");
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                        title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../../Default.aspx">الرئيسية</a></li>
                    <li><a href="PageEmployeeSalary.aspx">قائمة رواتب الموظفين</a></li>
                    <li><a href="PageEmployeeSalaryAdd.aspx">إضافة/تعديل رواتب الموظفين </a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة/تعديل رواتب الموظفين"></asp:Label>
                    </h3>
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
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="col-md-16">
                                            نوع الراتب 
                                        </label><br />
                                        <div class="col-md-6">
                                            <asp:RadioButton ID="rbtnMonthSalary" runat="server" Text="الراتب الشهري" GroupName="Salary" Checked="true" AutoPostBack="true" OnCheckedChanged="rbtnMonthSalary_CheckedChanged" />
                                        </div>
                                        <div class="col-md-6" runat="server" visible="false">
                                            <asp:RadioButton ID="rbtnHourSalary" runat="server" Text="الراتب بالساعة" GroupName="Salary" AutoPostBack="true" OnCheckedChanged="rbtnMonthSalary_CheckedChanged" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            <asp:Label ID="lblTitleDepartment" runat="server" Text="حدد الإدارة : " CssClass="control-label"></asp:Label> <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:Label ID="lblDepartment" Visible="false" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" Width="100%" ValidationGroup="g2"
                                                OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" CssClass="form-control2 chzn-select dropdown">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDepartment" SetFocusOnError="true" ControlToValidate="ddlDepartment" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic"  Font-Size="10px" ErrorMessage="* حدد الإدارة" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>
                                            <asp:Label ID="lblTitleEmployee" runat="server" Text="حدد الموظف : " CssClass="control-label"></asp:Label> <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:HiddenField ID="HFIDEmployee" runat="server" Value="0" ClientIDMode="Static" />
                                            <asp:Label ID="lblEmployee" Visible="false" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control2 chzn-select dropdown" onkeyup="EmployeeSalarySave.CalculateTotal()"
                                                Width="100%" ValidationGroup="g2">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvEmployee" SetFocusOnError="true" ControlToValidate="ddlEmployee" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الموظف" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="control-label" id="lblBasic" runat="server">
                                            الأساسي (36%) <span title="إجباري" data-toggle="tooltip">*</span>
                                        </label>
                                        <div class="col-md-12">
                                            <asp:TextBox ID="txtBasic" MaxLength="10" runat="server" CssClass="form-control" ValidationGroup="g2"
                                                onkeypress="return Common.isNumericKey(event,this)"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvBasic" SetFocusOnError="true" ControlToValidate="txtBasic" ValidationGroup="g2"
                                                CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد الراتب الأساسي" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <br />
                                        <label class="col-md-4 control-label">
                                            إجمالي الراتب :
                                        </label>
                                        <div class="col-md-8 control-label textLeft">
                                            <asp:HiddenField ID="hfTotalSalary" runat="server" Value="0" ClientIDMode="Static" />
                                            <asp:Label ID="lblTotalSalary" runat="server" CssClass="lblTotalSalary" Text="0"></asp:Label>
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
            <div class="panel panel-default"  runat="server" id="divAllowance">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label1" runat="server" Text="قائمة البدلات ( العلاوات)"></asp:Label>
                    </h3>
                    <div style="float:left">
                        <label class="col-md-12">
                            إجمالي البدلات ( العلاوات) :
                                <asp:HiddenField ID="hfTotalAllowance" runat="server" Value="0" ClientIDMode="Static" />
                                <asp:Label ID="lblTotalAllowance" runat="server" CssClass="lblTotalAllowance" Text="0"></asp:Label>
                            <asp:Button ID="btnAllowanceDelete" runat="server" Text="حذف الملفات المحددة" title="حذف المحدد" OnClick="btnAllowanceDelete_Click"
                                data-toggle="tooltip" CssClass="btn btn-danger" OnClientClick="return ConfirmDelete();" Visible="false" />
                        </label>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <asp:Panel ID="pnlAllowanceAdd" runat="server" Visible="False">
                                <asp:Repeater ID="rptAllowance" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-12 control-label">
                                                    <%#Eval("Allowance") %> (<%#Eval("Percentage") %>%)
																
													<span class="redFont"><%#Convert.ToBoolean(Eval("IsConsider"))?"/يعتبر":"" %></span>
                                                </label>
                                                <div class="col-md-12">
                                                    <asp:HiddenField ID="hfAllowanceId" runat="server" Value='<%#Eval("AllowanceID") %>' />
                                                    <asp:TextBox ID="txtAllowance" MaxLength="10" runat="server"
                                                        CssClass="form-control input-width-xlarge txtAllowance" onkeypress="return Common.isNumericKey(event,this)"
                                                        onkeyup="EmployeeSalarySave.CalculateTotal()"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                            <asp:Panel ID="pnlAllowanceEdit" runat="server" Visible="False">
                                <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <h5>حدد نوع البدلات : 
                                            </h5>
                                            <asp:DropDownList ID="ddlAllowance" runat="server" Width="100%" ValidationGroup="g2Allowance"
                                                  CssClass="form-control2 chzn-select dropdown">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="ddlAllowance" ValidationGroup="g2Allowance"
                                                CssClass="required" Display="Dynamic"  Font-Size="10px" ErrorMessage="* حدد البيانات" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <h5>المبلغ : <span class="required">*</span>
                                            </h5>
                                            <div class="col-md-12">
                                                <div>
                                                    <asp:TextBox ID="txtAllowance" MaxLength="10" runat="server" CssClass="form-control" ValidationGroup="g2Allowance"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" ControlToValidate="txtAllowance" ValidationGroup="g2Allowance"
                                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد المبلغ" runat="server"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnAllowanceEdit" runat="server" Text="حفظ" Style="font-size: medium"
                                                CssClass="btn btn-info" OnClick="btnAllowanceEdit_Click" ValidationGroup="g2Allowance" />
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="table table-responsive">
                                        <asp:GridView ID="GVAllowance" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeAllowanceMapID"
                                            Width="100%" CssClass="footable1"
                                            EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="10px">
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
                                                <asp:BoundField DataField="Allowance" HeaderText="البدلات ( العلاوات)" SortExpression="Allowance" HeaderStyle-ForeColor="#CCCCCC" />
                                                <asp:TemplateField HeaderText="المبلغ" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                            <%# Eval("Amount") %> <%# ClassSaddam.FGetMonySa() %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CreatedDate" HeaderText="تاريخ الإضافة" SortExpression="CreatedDate" HeaderStyle-ForeColor="#CCCCCC" />
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
                                    </div>
                                    <asp:HiddenField ID="hfCountAllowance" runat="server" Value="0" />
                                    <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                    <asp:Label ID="lblCountAllowance" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                    <span class="fa fa-table"></span>

                                </asp:Panel>
                            </asp:Panel>
                            <asp:Panel ID="pnlAllowanceNull" runat="server" Visible="False">
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
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label2" runat="server" Text="المستقطع من الراتب"></asp:Label>
                    </h3>
                    <div style="float:left">
                        <label class="col-md-12">
                            إجمالي المستقطع :
                                <asp:HiddenField ID="hfTotalDeduction" runat="server" Value="0" ClientIDMode="Static" />
                                <asp:Label ID="lblTotalDeduction" runat="server" CssClass="lblTotalDeduction" Text="0"></asp:Label>
                            <asp:Button ID="btnDeductionDelete" runat="server" Text="حذف الملفات المحددة" title="حذف المحدد" OnClick="btnDeductionDelete_Click"
                                data-toggle="tooltip" CssClass="btn btn-danger" OnClientClick="return ConfirmDeleteDeduction();" Visible="false" />
                        </label>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <asp:Panel ID="pnlDeductionAdd" runat="server" Visible="False">
                                <asp:Repeater ID="rptDeduction" runat="server">
                                    <ItemTemplate>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-12 control-label">
                                                    <%#Eval("Deduction") %>
                                                    <span class="redFont"><%#Convert.ToBoolean( Eval("IsConsider"))?"/يعتبر":"" %></span>
                                                </label>
                                                <div class="col-md-12">
                                                    <asp:HiddenField ID="hfDeductionId" runat="server" Value='<%#Eval("DeductionID") %>' />
                                                    <%--<asp:TextBox ID="txtDeduction" MaxLength="10" runat="server" Text='<%#Eval("Amount") %>' 
                                                    CssClass="form-control input-width-xlarge txtDeduction" onkeypress="return Common.isNumericKey(event,this)" 
                                                    onkeyup="EmployeeSalarySave.CalculateTotal()"></asp:TextBox>--%>
                                                    <asp:TextBox ID="txtDeduction" MaxLength="10" runat="server"
                                                        CssClass="form-control input-width-xlarge txtDeduction" onkeypress="return Common.isNumericKey(event,this)"
                                                        onkeyup="EmployeeSalarySave.CalculateTotal()"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </asp:Panel>
                            <asp:Panel ID="pnlDeductionEdit" runat="server" Visible="False">
                                <asp:Panel ID="Panel2" runat="server" Direction="RightToLeft">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <h5>حدد نوع المستقطع : 
                                            </h5>
                                            <asp:DropDownList ID="ddlDeduction" runat="server" Width="100%" ValidationGroup="g2Deduction"
                                                  CssClass="form-control2 chzn-select dropdown">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" ControlToValidate="ddlDeduction" ValidationGroup="g2Deduction"
                                                CssClass="required" Display="Dynamic"  Font-Size="10px" ErrorMessage="* حدد البيانات" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <h5>المبلغ : <span class="required">*</span>
                                            </h5>
                                            <div class="col-md-12">
                                                <div>
                                                    <asp:TextBox ID="txtDeduction" MaxLength="10" runat="server" CssClass="form-control" ValidationGroup="g2Deduction"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" ControlToValidate="txtDeduction" ValidationGroup="g2Deduction"
                                                        CssClass="required" Display="Dynamic" Font-Size="10px" ErrorMessage="* حدد المبلغ" runat="server"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnDeductionEdit" runat="server" Text="حفظ" Style="font-size: medium"
                                                CssClass="btn btn-info" OnClick="btnDeductionEdit_Click" ValidationGroup="g2Deduction" />
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="table table-responsive">
                                        <asp:GridView ID="GVDeduction" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeDeductionMapID"
                                            Width="100%" CssClass="footable1"
                                            EnableTheming="True" GridLines="Horizontal" UseAccessibleHeader="False">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Width="10px">
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
                                                <asp:BoundField DataField="Deduction" HeaderText="المستقطع من الراتب" SortExpression="Deduction" HeaderStyle-ForeColor="#CCCCCC" />
                                                <asp:TemplateField HeaderText="المبلغ" HeaderStyle-ForeColor="#CCCCCC">
                                                    <ItemTemplate>
                                                         <%# Eval("Amount") %> <%# ClassSaddam.FGetMonySa() %>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CreatedDate" HeaderText="تاريخ الإضافة" SortExpression="CreatedDate" HeaderStyle-ForeColor="#CCCCCC" />
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
                                    </div>
                                    <asp:HiddenField ID="hfCountDeduction" runat="server" Value="0" />
                                    <span style="font-size: 12px; padding-right: 5px">عدد الملفات : </span>
                                    <asp:Label ID="lblCountDeduction" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                    <span class="fa fa-table"></span>
                                </asp:Panel>
                            </asp:Panel>
                            <asp:Panel ID="pnlDeductionNull" runat="server" Visible="False">
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
                            </asp:Panel>
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
        <br />
        <br />
        <br />

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
</asp:Content>

