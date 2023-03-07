<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageManageProductRestorationAndConstructionEdit.ascx.cs" Inherits="Shaerd_CPanelManageExchangeOrders_PageManageProductRestorationAndConstructionEdit" %>
<style type="text/css">
    @media screen and (min-width: 768px) {
        .WidthText {
            float: right;
            Width: 13%;
            padding-right: 5px;
        }

        .WidthText3 {
            float: right;
            Width: 19%;
            padding-right: 5px;
        }

        .WidthText2 {
            float: right;
            Width: 32%;
            padding-right: 5px;
        }

        .WidthText1 {
            float: right;
            Width: 24%;
            padding-right: 5px;
        }

        .WidthText4 {
            float: right;
            Width: 50%;
            padding-right: 5px;
        }
    }

    @media screen and (max-width: 767px) {
        .WidthText {
            Width: 95%;
        }

        .WidthText1 {
            Width: 95%;
        }

        .WidthText2 {
            Width: 95%;
        }

        .WidthText3 {
            Width: 95%;
        }

        .WidthText4 {
            Width: 95%;
        }
    }

    @media screen and (min-width: 768px) {
        .WidthText20 {
            Width: 250px;
            height: 36px;
        }
    }

    @media screen and (max-width: 767px) {
        .WidthText20 {
            Width: 150px;
            height: 36px;
        }
    }
</style>
<link href="../css/chosen.css" rel="stylesheet" />
<link href="../GridView.css" rel="stylesheet" type="text/css" />

<div class="page-header">
    <div class="container-fluid">
        <div class="pull-right">
            <a href="PageManageProductRestorationAndConstruction.aspx" class="btn btn-primary">
                <i class="fa fa-plus"></i>
            </a>
            <asp:LinkButton ID="btnRefrish" runat="server" class="btn btn-default" data-toggle="tooltip"
                title="تحديث"><i class="fa fa-refresh"></i></asp:LinkButton>
            <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" class="btn btn-default">
                     <i class="fa fa-reply"></i></asp:LinkButton>
        </div>
        <h1>لوحة التحكم</h1>
        <ul class="breadcrumb">
            <li><a href="Default.aspx">الرئيسية</a></li>
            <li><a href="">أوامر الصرف</a></li>
            <li><a href="">تعديل أمر صرف بناء وترميم</a></li>
        </ul>
    </div>
</div>
<div class="container-fluid">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="lbmsg" runat="server" Text="تعديل أمر صرف بناء وترميم"></asp:Label>
                <asp:Label ID="lblCountProduct" runat="server"></asp:Label>
                بناء منزل
                        <asp:RadioButton ID="RBBenaCheck" runat="server" GroupName="GCheck" AutoPostBack="true" OnCheckedChanged="RBBenaCheck_CheckedChanged" />
                - ترميم منزل
                        <asp:RadioButton ID="RBTarmimCheck" runat="server" GroupName="GCheck" AutoPostBack="true" OnCheckedChanged="RBTarmimCheck_CheckedChanged" />
            </h3>
        </div>
        <div class="panel-body">
            <div class="content-box-large">
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
            </div>
        </div>
    </div>
</div>
<div class="container-fluid" runat="server" id="pnlMostafeed">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="Label1" runat="server" Text="بيانات المستفيد"></asp:Label>
            </h3>
        </div>
        <div class="panel-body">
            <div class="content-box-large">
                <div class="container-fluid" dir="rtl">
                    <div class="col-lg-2">
                        <div class="form-group">
                            <h5>رقم المستفيد : <span style="color: red">*</span>
                            </h5>
                            <asp:TextBox ID="txtNumberMostafeed" runat="server" class="form-control" ValidationGroup="g2" AutoPostBack="true" OnTextChanged="txtNumberMostafeed_TextChanged" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                ControlToValidate="txtNumberMostafeed" ErrorMessage="* رقم المستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNumberMostafeed"
                                ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                Display="Dynamic">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class="form-group">
                            <h5>أو إسم المستفيد : <span style="color: red">*</span>
                            </h5>
                            <asp:DropDownList ID="DLName" runat="server" ValidationGroup="g2" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" AutoPostBack="true" OnSelectedIndexChanged="DLName_SelectedIndexChanged" Enabled="false">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-2">
                        <div class="form-group">
                            <h5>رقم الفاتورة : <span style="color: red">*</span>
                            </h5>
                            <asp:TextBox ID="txtNumberOrder" runat="server" class="form-control" ValidationGroup="g2" Enabled="false"></asp:TextBox>
                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                ControlToValidate="txtNumberOrder" ErrorMessage="* رقم القرار" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtNumberOrder"
                                ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                Display="Dynamic">
                            </asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class="form-group">
                            <h5>المبادرة أو الداعم : <span style="color: red">*</span>
                            </h5>
                            <asp:DropDownList ID="DLInitiatives" runat="server" ValidationGroup="g2" Enabled="false" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                <asp:ListItem Value=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="DLInitiatives" ErrorMessage="* المبادرة أو الداعم" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="form-group">
                            <h5>تاريخ الإضافة : <span style="color: red">*</span>
                            </h5>
                            <div class="col-sm-3">
                                <div class="input-group date " style="margin-right: -10px">
                                    <asp:TextBox ID="txt_Add" runat="server" class="form-control" Width="100" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <button class="btn btn-default" type="button">
                                            <i class="fa fa-calendar"></i>
                                        </button>
                                    </span>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txt_Add" ErrorMessage="* تاريخ الإضافة" ForeColor="#FF0066"
                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Panel ID="pnlDataMosTafeed" runat="server" Visible="false">
                    <div class="container-fluid" dir="rtl">
                        <div class="WidthText4">
                            <div class="form-group">
                                الاسم :
                                    <asp:Label ID="lblName" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="WidthText1">
                            <div class="form-group">
                                القرية :
                                    <asp:Label ID="lblAlQariah" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="WidthText1">
                            <div class="form-group">
                                الجنس :
                                    <asp:Label ID="lblGender" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="WidthText3">
                            <div class="form-group">
                                <h5>رقم الهاتف :
                                </h5>
                                0<asp:Label ID="lblPhone" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="WidthText3">
                            <div class="form-group">
                                <h5>حالة المستفيد :
                                </h5>
                                <asp:Label ID="lblHalatAlmostafeed" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="WidthText3">
                            <div class="form-group">
                                <h5>السجل المدني :
                                </h5>
                                <asp:Label ID="lblNumberSigal" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="WidthText3">
                            <div class="form-group">
                                <h5>تاريخ الميلاد :
                                </h5>
                                <asp:Label ID="lblDateBrithDay" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="WidthText3">
                            <div class="form-group">
                                <h5>العمر :
                                </h5>
                                <asp:Label ID="lblAge" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</div>

<div class="container-fluid" runat="server" visible="false" id="pnlAlDaam">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="Label2" runat="server" Text="يرجى تحدد المبلغ"></asp:Label>
            </h3>
        </div>
        <div class="panel-body">
            <div class="content-box-large">
                <div class="widget-box">
                    <div class="container-fluid" dir="rtl">
                        <div class="col-md-3">
                            <div class="form-group">
                                <h5>المبلغ : <span style="color: red">*</span>
                                </h5>
                                <asp:TextBox ID="txtThe_Mony" runat="server" class="form-control" ValidationGroup="g2" AutoPostBack="true" OnTextChanged="txtThe_Mony_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                    ControlToValidate="txtThe_Mony" ErrorMessage="* المبلغ" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtThe_Mony"
                                    ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                    Display="Dynamic">
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <h5>المبلغ كتابتاً : <span style="color: red">*</span>
                                </h5>
                                <asp:TextBox ID="txtThe_Mony_Word" runat="server" class="form-control" ValidationGroup="g2" Style="font-family: 'Alwatan'; font-size: 18px; text-align: center"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator17" runat="server"
                                    ControlToValidate="txtThe_Mony_Word" ErrorMessage="* المبلغ كتابتاً" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <h5>نوع الدفع : <span style="color: red">*</span>
                                </h5>
                                <asp:RadioButton ID="RBIsCash_Money" runat="server" GroupName="Type" AutoPostBack="true" OnCheckedChanged="RBIsCash_Money_CheckedChanged" />
                                <span>كاش </span>
                                <asp:RadioButton ID="RBIsShayk_Bank" runat="server" GroupName="Type" AutoPostBack="true" OnCheckedChanged="RBIsShayk_Bank_CheckedChanged" />
                                <span>شيك </span>
                                <span runat="server" id="NumberShayk" visible="false">-  
                                         <span>رقم الشيك </span>
                                    <asp:TextBox ID="txtNumber_Shayk_Bank" runat="server" class="form-control2" ValidationGroup="g2" Width="80" Height="32"
                                        Style="border: double; border-color: #aeadad; border-width: 1px; border-radius: 2px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNumber_Shayk_Bank"
                                        ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                        Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-md-3" style="display:none;">
                            <div class="form-group">
                                <h5>تاريخ الطلب : <span style="color: red">*</span>
                                </h5>
                                <div class="col-sm-3">
                                    <div class="input-group date " style="margin-right: -10px">
                                        <asp:TextBox ID="txtProductionDate" runat="server" class="form-control" Width="150" data-date-format="YYYY-MM-DD" ValidationGroup="g2" Style="direction: ltr"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <button class="btn btn-default" type="button">
                                                <i class="fa fa-calendar"></i>
                                            </button>
                                        </span>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator32" runat="server"
                                            ControlToValidate="txtProductionDate" ErrorMessage="* تاريخ الطلب" ForeColor="#FF0066"
                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <h5>لمشروع : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLSupportType" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown" Enabled="false"
                                    Width="100%">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator13" runat="server"
                                    ControlToValidate="DLSupportType" ErrorMessage="* حدد المشروع" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group">
                                <h5>وذلك مقابل :
                                </h5>
                                <asp:TextBox ID="txtMoreDetails" runat="server" class="form-control" ValidationGroup="g2"
                                        TextMode="MultiLine"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtMoreDetails" ErrorMessage="* مقابل" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="container-fluid" dir="rtl">
                        <div class="col-md-12">
                            <div class="form-group">
                                <h5>ملاحظة :
                                </h5>
                                <asp:TextBox ID="txt_Note" runat="server" class="form-control" ValidationGroup="g2" MaxLength="512"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <h5>مدير الجمعية : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                    Width="100%">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                    ControlToValidate="DLModerAlGmeiah" ErrorMessage="* مدير الجمعية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <h5>المشرف المالي : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLAmeenAlSondoq" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                    Width="100%">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                    ControlToValidate="DLAmeenAlSondoq" ErrorMessage="* المشرف المالي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <h5>رئيس مجلس الإدارة : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                    Width="100%">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                    ControlToValidate="DLRaeesMaglesAlEdarah" ErrorMessage="* رئيس مجلس الإدارة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-5" runat="server" visible="false">
                            <div class="form-group">
                                <h5>نائب رئيس المجلس : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLNaeebRaeesMagles" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                    Width="100%">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                    ControlToValidate="DLNaeebRaeesMagles" ErrorMessage="* نائب رئيس مجلس" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-5" runat="server" visible="false">
                            <div class="form-group">
                                <h5>أمين المستودع : <span style="color: red">*</span>
                                </h5>
                                <asp:DropDownList ID="DLIDStorekeeper" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                    Width="100%">
                                    <asp:ListItem></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server"
                                    ControlToValidate="DLIDStorekeeper" ErrorMessage="* أمين المخزن" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-5" runat="server" visible="false">
                            <div class="form-group">
                                <div>
                                    <h5>الباحث الذي سيقوم بالتسليم : <span style="color: red">*</span>
                                    </h5>
                                    <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="g2" CssClass="form-control2 chzn-select dropdown"
                                        Width="100%">
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator15" runat="server"
                                        ControlToValidate="DLAlBaheth" ErrorMessage="* حدد الباحث" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:CheckBox ID="CBFinish" runat="server" Text=" تحديد على أنه سيتم الصرف " Font-Size="12px" />
                                <br />
                                <asp:Button ID="btnEdit" runat="server" Text="تعديل البانات" Style="margin-right: 4px; font-size: medium"
                                    class="btn btn-info btn-fill pull-left" ValidationGroup="g2" OnClick="btnEdit_Click" />
                                <asp:LinkButton ID="LinkButton1" runat="server" Style="margin-right: 4px; font-size: medium" Visible="false"
                                    class="btn btn-danger btn-fill pull-left">خروج بدون حفظ</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<div class="container-fluid" id="pnlStar" runat="server" visible="false">
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">
                <i class="fa fa-pencil"></i>
                <asp:Label ID="Label3" runat="server" Text="يرجى تحديد نوع الصرف"></asp:Label>
            </h3>
        </div>
        <div class="panel-body">
            <div class="content-box-large">
                <div class="container-fluid" dir="rtl">
                    <asp:Panel ID="pnlSelect" runat="server">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <div align="center">
                            <h3 style="font-size: 20px">حدد نوع الصرف
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
