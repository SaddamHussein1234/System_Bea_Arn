<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageAdd.ascx.cs" Inherits="Shaerd_ERP_FMS_GeneralAssembly_PageAdd" %>
<div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="LB_Add" runat="server" data-toggle="tooltip" title="إضافة سند جديد" OnClick="LB_Add_Click"
                        class="btn btn-info"> <i class="fa fa-plus"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" OnClick="LB_Back_Click"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../">الرئيسية</a></li>
                    <li><a href="PageAll.aspx">ايصالات الإشتراكات</a></li>
                    <li><a href="PageAdd.aspx">إضافة ايصال إشتراك</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label1" runat="server" Text="المالية"></asp:Label>
                    </h3>
                </div>
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
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>تغذية حساب : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLAccount" runat="server" ValidationGroup="g2" Width="100%" 
                                            CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" OnLoad="DLAccount_Load">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="الصندوق">الصندوق</asp:ListItem>
                                            <asp:ListItem Value="البنك">البنك</asp:ListItem>
                                            <asp:ListItem Value="تبرع_عام">تبرع_عام</asp:ListItem>
                                            <asp:ListItem Value="مصاريف_تشغيلية">مصاريف_تشغيلية</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator13" runat="server"
                                            ControlToValidate="DLAccount" ErrorMessage="* حدد التغذية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>  
                                <script type="text/javascript">
                                    function Validate() {
                                        var ddlFruits = document.getElementById("<%=DLAccount.ClientID %>");
                                        var PanelID = document.getElementById("pnlBank");

                                        if (ddlFruits.value == "البنك") {
                                            if (PanelID.style.display == "none") {
                                                PanelID.style.display = "block";
                                            }
                                            else {
                                                PanelID.style.display = "none";
                                            }
                                        }
                                        else {
                                            PanelID.style.display = "none";
                                        }
                                        return true;
                                    }
                                </script>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>لمشروع : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLProject" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" 
                                            Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                            ControlToValidate="DLProject" ErrorMessage="* حدد المشروع" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>  
                            </div>
                            <div class="container-fluid" id="pnlBank" style="<%= FCheck("Bank") %>">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <h5>حدد البنك : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DL_Bank" runat="server" ValidationGroup="GBill" Width="300" 
                                            AutoPostBack="true" OnSelectedIndexChanged="DL_Bank_SelectedIndexChanged"
                                            CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <h5>حدد الحساب : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DL_Account" runat="server" ValidationGroup="GBill" Width="350" 
                                            AutoPostBack="true" OnSelectedIndexChanged="DL_Account_SelectedIndexChanged"
                                            CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
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
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة ايصال إشتراك"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>رقم الايصال : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtbill_Number" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="txtbill_Number" ErrorMessage="* رقم الايصال" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtbill_Number"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2" Font-Size="10px"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>حدد العضو : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLNumber_Admin" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="DLNumber_Admin" ErrorMessage="* حدد العضو" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>      
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>وذلك يوم : <span style="color: red">*</span>
                                        </h5>
                                        <div class="col-sm-3">
                                            <div class="input-group date " style="margin-right: -10px">
                                                <asp:TextBox ID="txtDate_Get" runat="server" class="form-control" Width="170" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; text-align:left;"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator32" runat="server"
                                                    ControlToValidate="txtDate_Get" ErrorMessage="*" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>                           
                            <div class="container-fluid" dir="rtl">   
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>المبلغ : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtThe_Mony" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtThe_Mony" ErrorMessage="* المهنة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtThe_Mony"
                                            ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2" Font-Size="10px"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-3" align="center">
                                    <div class="form-group" style="background-color:#f1eded; padding-right:5px; border-radius:7px">
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                نقداً : 
                                                <br />
                                                <asp:RadioButton ID="RBCash_Money" runat="server" GroupName="GAdow" AutoPostBack="true" OnCheckedChanged="RBCash_Money_CheckedChanged" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                                <div class="col-md-3" align="center">
                                    <div class="form-group" style="background-color:#f1eded; padding-right:5px; border-radius:7px">
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                شيك : 
                                                <br />
                                                <asp:RadioButton ID="RBShayk_Bank" runat="server" GroupName="GAdow" AutoPostBack="true" OnCheckedChanged="RBShayk_Bank_CheckedChanged" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                                <div class="col-md-3" align="center">
                                    <div class="form-group" style="background-color:#f1eded; padding-right:5px; border-radius:7px">
                                        <div class="keepmeLogged">
                                            <label class="switch">
                                                إيداع بنكي : 
                                                <br />
                                                <asp:RadioButton ID="RBEdaa_Bank" runat="server" GroupName="GAdow" AutoPostBack="true" OnCheckedChanged="RBEdaa_Bank_CheckedChanged" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                                <div class="col-md-3" runat="server" id="PnlAllow" visible="false">
                                    <div class="form-group">
                                        <h5>توقيع بدل كلاً من :
                                        </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch" runat="server" id="IDRaeesAlmaglis" visible="false">
                                                رئيس المجلس
                                                <br />
                                                <input name="RememberMe" type="checkbox" id="CBRaeesAlmaglis" runat="server" checked="checked" />
                                                <span class="slider round"></span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch" runat="server" id="IDAmeenAlsondoq" visible="false">
                                                المشرف المالي
                                                <br />
                                                <input name="RememberMe" type="checkbox" id="CBAmeenAlsondoq" runat="server" checked="checked" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" id="IDSheyk" runat="server" visible="false" dir="rtl" 
                                style="background-color:#f1eded; padding-right:5px; border-radius:7px; margin-top:10px;">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>رقم الشيك : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtNumber_Shayk" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr; text-align:left;"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txtNumber_Shayk" ErrorMessage="* رقم الشيك" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>تاريخ الشيك : <span style="color: red">*</span>
                                        </h5>
                                        <div class="col-sm-3">
                                            <div class="input-group date " style="margin-right: -10px">
                                                <asp:TextBox ID="txtDate_Shayk_Bank" runat="server" class="form-control" Width="220" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; text-align:left;"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="txtDate_Shayk_Bank" ErrorMessage="*" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>على بنك : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtFor_Bank" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="txtFor_Bank" ErrorMessage="* على بنك" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" id="IDEdaa" runat="server" visible="false" dir="rtl" 
                                style="background-color:#f1eded; padding-right:5px; border-radius:7px; margin-top:10px;">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>على الحساب رقم : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtNumber_Edaa" runat="server" class="form-control" ValidationGroup="g2" Style="direction: ltr; text-align:left;"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="txtNumber_Edaa" ErrorMessage="* رقم الإيداع" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>تاريخ الإيداع : <span style="color: red">*</span>
                                        </h5>
                                        <div class="col-sm-3">
                                            <div class="input-group date ">
                                                <asp:TextBox ID="txtDate_Edaa_Bank" runat="server" class="form-control" Width="220" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr; text-align:left;"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                                    ControlToValidate="txtDate_Edaa_Bank" ErrorMessage="*" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>على بنك : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtFor_Edaa_Bank" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                            ControlToValidate="txtFor_Edaa_Bank" ErrorMessage="* على بنك" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                
                            </div>
                            <div class="container-fluid">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <h5>وذلك رسوم إشتراك عضوية : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtMore_Details" runat="server" class="form-control" ValidationGroup="g2" MaxLength="100"
                                            Text="تجديد إشتراك العضوية لمدة عام من تاريخه"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                            ControlToValidate="txtMore_Details" ErrorMessage="* التفاصيل" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>لسنة : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLYears" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                            ControlToValidate="DLYears" ErrorMessage="* حدد السنة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>المشرف المالي : 
                                        </h5>
                                        <asp:DropDownList ID="DLAmeenAlsondoq" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server"
                                            ControlToValidate="DLAmeenAlsondoq" ErrorMessage="* المشرف المالي" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>رئيس مجلس الإدارة : 
                                        </h5>
                                        <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                            ControlToValidate="DLRaeesMaglesAlEdarah" ErrorMessage="* رئيس مجلس الإدارة" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group" align="left">
                                        <asp:LinkButton ID="btnAdd" runat="server"  ValidationGroup="g2"  OnClick="btnAdd_Click"
                                            class="btn btn-info">حفظ البيانات</asp:LinkButton>
                                        <asp:LinkButton ID="LB_Back" runat="server"  OnClick="LB_Back_Click"
                                            class="btn btn-danger">خروج</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
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