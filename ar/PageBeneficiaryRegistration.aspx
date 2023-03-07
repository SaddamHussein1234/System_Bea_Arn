<%@ Page Title="" Language="C#" MasterPageFile="~/ar/MPAr.master" AutoEventWireup="true" CodeFile="PageBeneficiaryRegistration.aspx.cs" Inherits="ar_PageBeneficiaryRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta name="theme-color" content="#373533" />
    <meta name="msapplication-navbutton-color" content="#373533" />
    <meta name="apple-mobile-web-app-status-bar-style" content="#373533" />

    <link rel="icon" type="image/png" href="<%= "https://" + HFLink.Value + HFImage.Value %>" />
    <link rel="apple-touch-icon-precomposed" href="<%= "https://" + HFLink.Value + HFImage.Value %>" />
    <meta name="msapplication-TileColor" content="#ffffff" />
    <meta name="msapplication-TileImage" content="<%= "https://" + HFLink.Value + HFImage.Value %>" />

    <meta name="title" content="<%= HFTitle.Value %>" />
    <meta name="description" content="<%= HFDescrption.Value %>" />
    <meta name="keywords" content="<%= HFKeyWord.Value %>" />
    <meta name="url" content="<%= "https://" + HFLink.Value + "/ar/PageBeneficiaryRegistration.aspx" %>" />

    <meta property="og:description" content="<%= HFDescrption.Value %>" />
    <meta property="og:title" content="<%= HFTitle.Value %>" />
    <meta property="og:type" content="site" />
    <meta property="og:url" content="<%= "https://" + HFLink.Value + "/ar/PageBeneficiaryRegistration.aspx" %>" />
    <meta property="og:locale" content="ar_AR" />
    <meta property="og:locale:alternate" content="ar_AR" />
    <meta property="og:locale:alternate" content="en_US" />
    <meta property="og:image" content="<%= "https://" + HFLink.Value + HFImage.Value %>" />
    <meta property="og:image:width" content="600" />
    <meta property="og:image:height" content="300" />

    <meta name="twitter:description" content="<%= HFDescrption.Value %>" />
    <meta name="twitter:image" content="<%= "https://" + HFLink.Value + HFImage.Value %>" />
    <meta name="twitter:card" content="summary_large_image" />
    <meta name="twitter:title" content="<%= HFTitle.Value %>" />
    <meta name="twitter:url" content="<%= "https://" + HFLink.Value + "/ar/PageBeneficiaryRegistration.aspx" %>" />

    <link rel="stylesheet" href="/MultiForm3/assets/css/style.css" />
    <script>
        window.scroll({
            top: 100,
            left: 100,
            behavior: 'smooth'
        });
    </script>
    <script type="text/javascript">
        function insertConfirmation() {
            var answer = confirm("هل تريد الإستمرار ؟")
            if (answer) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <body onload="window.scroll(0, 370 )">
        <div class="section_header original" style="background: url('/Themes/Ar_Qader/images/section_header_background.jpg')">
            <div class="container">
                <!-- Description -->
                <div class="section_description">
                    <h1>تسجيل مستفيد جديد</h1>
                </div>
                <!-- Breadcrumbs -->
                <div class="breadcrumb_container">
                    <ul class="breadcrumb">
                        <i class="fas fa-bookmark"></i>
                        <li><a href='/ar/'>الرئيسية</a></li>
                        <li>تسجيل مستفيد جديد</li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="body_container">
            <div class="container inner">
                <asp:Panel ID="pnlMessage" runat="server">
                    <div role="form" method="post" class="registration-form">
                        <div runat="server" id="IDInfo">
                            <fieldset>
                                <div class="form-top">
                                    <div class="form-top-right">
                                        <i class="fa fa-user"></i>
                                    </div>
                                    <div class="form-top-left">
                                        <h3>الخطوة 1 / 6</h3>
                                        <p>
                                            <asp:Label ID="lbmsg" runat="server" Text="البيانات الشخصية للمستفيد : "></asp:Label>
                                        </p>
                                    </div>
                                </div>
                                <div class="form-bottom">
                                    <div class="container-fluid">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="">رقم الملف : </label>
                                                <asp:Label ID="txtNumberMostafeed" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>تاريخ التسجيل : </label>
                                                <asp:Label ID="txtDateRegistry" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>اسمك الكريم :  <span style="color: red">*</span> </label>
                                                <asp:TextBox ID="txtNameMostafeed" runat="server" class="form-control" ValidationGroup="g2" placeholder="الإسم كامل ..."></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                                    ControlToValidate="txtNameMostafeed" ErrorMessage="* إسم المستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    تاريخ الميلاد (هجري) : <span style="color: red">*</span>
                                                </label>
                                                <table>
                                                    <tr>
                                                        <td style="padding-left: 3px; width: 50%">
                                                            <asp:DropDownList ID="ddlYearsH" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding-left: 3px; width: 25%">
                                                            <asp:DropDownList ID="ddlMonthsH" runat="server" CssClass="form-control">
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem Text="01" Value="01" />
                                                                <asp:ListItem Text="02" Value="02" />
                                                                <asp:ListItem Text="03" Value="03" />
                                                                <asp:ListItem Text="04" Value="04" />
                                                                <asp:ListItem Text="05" Value="05" />
                                                                <asp:ListItem Text="06" Value="06" />
                                                                <asp:ListItem Text="07" Value="07" />
                                                                <asp:ListItem Text="08" Value="08" />
                                                                <asp:ListItem Text="09" Value="09" />
                                                                <asp:ListItem Text="10" Value="10" />
                                                                <asp:ListItem Text="11" Value="11" />
                                                                <asp:ListItem Text="12" Value="12" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding-left: 3px; width: 25%">
                                                            <asp:DropDownList ID="ddlDatesH" runat="server" CssClass="form-control">
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem Text="01" Value="01" />
                                                                <asp:ListItem Text="02" Value="02" />
                                                                <asp:ListItem Text="03" Value="03" />
                                                                <asp:ListItem Text="04" Value="04" />
                                                                <asp:ListItem Text="05" Value="05" />
                                                                <asp:ListItem Text="06" Value="06" />
                                                                <asp:ListItem Text="07" Value="07" />
                                                                <asp:ListItem Text="08" Value="08" />
                                                                <asp:ListItem Text="09" Value="09" />
                                                                <asp:ListItem Text="10" Value="10" />
                                                                <asp:ListItem Text="11" Value="11" />
                                                                <asp:ListItem Text="12" Value="12" />
                                                                <asp:ListItem Text="13" Value="13" />
                                                                <asp:ListItem Text="14" Value="14" />
                                                                <asp:ListItem Text="15" Value="15" />
                                                                <asp:ListItem Text="16" Value="16" />
                                                                <asp:ListItem Text="17" Value="17" />
                                                                <asp:ListItem Text="18" Value="18" />
                                                                <asp:ListItem Text="19" Value="19" />
                                                                <asp:ListItem Text="20" Value="20" />
                                                                <asp:ListItem Text="21" Value="21" />
                                                                <asp:ListItem Text="22" Value="22" />
                                                                <asp:ListItem Text="23" Value="23" />
                                                                <asp:ListItem Text="24" Value="24" />
                                                                <asp:ListItem Text="25" Value="25" />
                                                                <asp:ListItem Text="26" Value="26" />
                                                                <asp:ListItem Text="27" Value="27" />
                                                                <asp:ListItem Text="28" Value="28" />
                                                                <asp:ListItem Text="29" Value="29" />
                                                                <asp:ListItem Text="30" Value="30" />
                                                                <asp:ListItem Text="31" Value="31" />
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container-fluid">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    حدد قريتك : <span style="color: red">*</span>
                                                </label>
                                                <asp:DropDownList ID="DLAlQriah" runat="server" ValidationGroup="g2" CssClass="form-control" Style="font-size: 13px;">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                                    ControlToValidate="DLAlQriah" ErrorMessage="* القرية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    حدد الجنس : <span style="color: red">*</span>
                                                </label>
                                                <asp:DropDownList ID="DLGender" runat="server" ValidationGroup="g2" CssClass="form-control" Style="font-size: 13px;">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                                    ControlToValidate="DLGender" ErrorMessage="* الجنس" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    رقم السجل المدني : <span style="color: red">*</span>
                                                </label>
                                                <asp:TextBox ID="txtNumberAlSegelAlMadany" runat="server" class="form-control" ValidationGroup="g2"
                                                    Style="direction: ltr"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                                    ControlToValidate="txtNumberAlSegelAlMadany" ErrorMessage="* رقم السجل المدني" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtNumberAlSegelAlMadany"
                                                    ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                                    Display="Dynamic">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container-fluid">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    رقم الجوال 1 : <span style="color: red">*</span>
                                                </label>
                                                <asp:TextBox ID="txtCellPhoneOne" runat="server" class="form-control" ValidationGroup="g2"
                                                    Style="direction: ltr"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="txtCellPhoneOne" ErrorMessage="* رقم الجوال 1" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCellPhoneOne"
                                                    ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                                    Display="Dynamic">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    رقم الجوال 2 : 
                                                </label>
                                                <asp:TextBox ID="txtCellPhoneTow" runat="server" class="form-control" ValidationGroup="g2" Text="0"
                                                    Style="direction: ltr"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtCellPhoneTow"
                                                    ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                                    Display="Dynamic">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    هاتف ثابت 1 : 
                                                </label>
                                                <asp:TextBox ID="txtPhoneOne" runat="server" class="form-control" ValidationGroup="g2" Text="0"
                                                    Style="direction: ltr"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtPhoneOne"
                                                    ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                                    Display="Dynamic">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    هاتف ثابت 2 : 
                                                </label>
                                                <asp:TextBox ID="txtPhoneTow" runat="server" class="form-control" ValidationGroup="g2" Text="0"
                                                    Style="direction: ltr"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtPhoneTow"
                                                    ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                                    Display="Dynamic">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container-fluid">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    الحالة الإجتماعية : <span style="color: red">*</span>
                                                </label>
                                                <asp:DropDownList ID="DLHalafAlMosTafeed" runat="server" ValidationGroup="g2" CssClass="form-control" Style="font-size: 13px;" AutoPostBack="true" OnSelectedIndexChanged="DLHalafAlMosTafeed_SelectedIndexChanged">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator15" runat="server"
                                                    ControlToValidate="DLHalafAlMosTafeed" ErrorMessage="* حالة المستفيد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    رقم القريب : <span style="color: red">*</span>
                                                </label>
                                                <asp:TextBox ID="txtNumberQareb" runat="server" class="form-control" ValidationGroup="g2"
                                                    Style="direction: ltr"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtNumberQareb" ErrorMessage="* رقم القريب" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtNumberQareb"
                                                    ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                                    Display="Dynamic">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    إسم القريب : <span style="color: red">*</span>
                                                </label>
                                                <asp:TextBox ID="txtNameQareb" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                                    ControlToValidate="txtNameQareb" ErrorMessage="* إسم القريب" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    صلة القرابة : <span style="color: red">*</span>
                                                </label>
                                                <asp:DropDownList ID="DLSelatAlQarabah" runat="server" ValidationGroup="g2" CssClass="form-control" Style="font-size: 13px;">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator18" runat="server"
                                                    ControlToValidate="DLSelatAlQarabah" ErrorMessage="* صلة القرابة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix visible-sm-block"></div>
                                    <div align="left">
                                        <button type="button" class="btn btn-next btn-info">الخطوة التالية</button>
                                    </div>
                                </div>
                            </fieldset>

                            <fieldset style="display: none">
                                <div class="form-top">
                                    <div class="form-top-left">
                                        <h3>الخطوة 2 / 6</h3>
                                        <p>الحالة العلمية والتعليمية والصحية للمستفيد :</p>
                                    </div>
                                    <div class="form-top-right">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                </div>
                                <div class="form-bottom">
                                    <div class="container-fluid" dir="rtl" runat="server" visible="false">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    تاريخ الميلاد (ميلادي) : <span style="color: red">*</span>
                                                </label>
                                                <table>
                                                    <tr>
                                                        <td style="padding-left: 3px; width: 50%">
                                                            <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding-left: 3px; width: 25%">
                                                            <asp:DropDownList ID="ddlMonths" runat="server" CssClass="form-control">
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem Text="01" Value="01" />
                                                                <asp:ListItem Text="02" Value="02" />
                                                                <asp:ListItem Text="03" Value="03" />
                                                                <asp:ListItem Text="04" Value="04" />
                                                                <asp:ListItem Text="05" Value="05" />
                                                                <asp:ListItem Text="06" Value="06" />
                                                                <asp:ListItem Text="07" Value="07" />
                                                                <asp:ListItem Text="08" Value="08" />
                                                                <asp:ListItem Text="09" Value="09" />
                                                                <asp:ListItem Text="10" Value="10" />
                                                                <asp:ListItem Text="11" Value="11" />
                                                                <asp:ListItem Text="12" Value="12" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding-left: 3px; width: 25%">
                                                            <asp:DropDownList ID="ddlDates" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlDates_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem></asp:ListItem>
                                                                <asp:ListItem Text="01" Value="01" />
                                                                <asp:ListItem Text="02" Value="02" />
                                                                <asp:ListItem Text="03" Value="03" />
                                                                <asp:ListItem Text="04" Value="04" />
                                                                <asp:ListItem Text="05" Value="05" />
                                                                <asp:ListItem Text="06" Value="06" />
                                                                <asp:ListItem Text="07" Value="07" />
                                                                <asp:ListItem Text="08" Value="08" />
                                                                <asp:ListItem Text="09" Value="09" />
                                                                <asp:ListItem Text="10" Value="10" />
                                                                <asp:ListItem Text="11" Value="11" />
                                                                <asp:ListItem Text="12" Value="12" />
                                                                <asp:ListItem Text="13" Value="13" />
                                                                <asp:ListItem Text="14" Value="14" />
                                                                <asp:ListItem Text="15" Value="15" />
                                                                <asp:ListItem Text="16" Value="16" />
                                                                <asp:ListItem Text="17" Value="17" />
                                                                <asp:ListItem Text="18" Value="18" />
                                                                <asp:ListItem Text="19" Value="19" />
                                                                <asp:ListItem Text="20" Value="20" />
                                                                <asp:ListItem Text="21" Value="21" />
                                                                <asp:ListItem Text="22" Value="22" />
                                                                <asp:ListItem Text="23" Value="23" />
                                                                <asp:ListItem Text="24" Value="24" />
                                                                <asp:ListItem Text="25" Value="25" />
                                                                <asp:ListItem Text="26" Value="26" />
                                                                <asp:ListItem Text="27" Value="27" />
                                                                <asp:ListItem Text="28" Value="28" />
                                                                <asp:ListItem Text="29" Value="29" />
                                                                <asp:ListItem Text="30" Value="30" />
                                                                <asp:ListItem Text="31" Value="31" />
                                                            </asp:DropDownList>

                                                        </td>
                                                    </tr>
                                                </table>
                                                <div class="col-sm-3" runat="server" visible="false">
                                                    <div class="input-group date " style="margin-right: -10px">
                                                        <asp:TextBox ID="txtdateBrith" runat="server" class="form-control" Width="150" data-date-format="DD-MM-YYYY" ValidationGroup="g2" Style="direction: ltr" AutoPostBack="True" OnTextChanged="txtdateBrith_TextChanged"></asp:TextBox>
                                                        <span class="input-group-btn">
                                                            <button class="btn btn-default" type="button">
                                                                <i class="fa fa-calendar"></i>
                                                            </button>
                                                        </span>
                                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator33" runat="server"
                                                            ControlToValidate="txtdateBrith" ErrorMessage="* تاريخ الميلاد" ForeColor="#FF0066"
                                                            meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-8">
                                            <div class="form-group">
                                                <label>
                                                    العمر : <span style="color: red">*</span> <a href="http://dirarab.net/dateconversion" target="_blank">الذهاب لموقع التحويل</a>
                                                </label>
                                                <asp:TextBox ID="txtAge" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator17" runat="server"
                                                    ControlToValidate="txtAge" ErrorMessage="* العمر" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAge"
                                        ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                        Display="Dynamic">
                                    </asp:RegularExpressionValidator>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container-fluid">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    المهنة الحالية : <span style="color: red">*</span>
                                                </label>
                                                <asp:TextBox ID="txtAlMehnahAlHaliyahllmostafeed" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtAlMehnahAlHaliyahllmostafeed" ErrorMessage="* المهنة الحالية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    الحالة التعليمية : <span style="color: red">*</span>
                                                </label>
                                                <asp:DropDownList ID="DLAlHalahAlTaelimiahllmostafeed" runat="server" ValidationGroup="g2" CssClass="form-control"
                                                    Height="34" Width="95%">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="دكتوراة">دكتوراة</asp:ListItem>
                                                    <asp:ListItem Value="ماجستير">ماجستير</asp:ListItem>
                                                    <asp:ListItem Value="بكالوريوس">بكالوريوس</asp:ListItem>
                                                    <asp:ListItem Value="جامعي">جامعي</asp:ListItem>
                                                    <asp:ListItem Value="دبلوم">دبلوم</asp:ListItem>
                                                    <asp:ListItem Value="ثانوي">ثانوي</asp:ListItem>
                                                    <asp:ListItem Value="ابتدائي">ابتدائي</asp:ListItem>
                                                    <asp:ListItem Value="متوسط">متوسط</asp:ListItem>
                                                    <asp:ListItem Value="امي">امي</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                                    ControlToValidate="DLAlHalahAlTaelimiahllmostafeed" ErrorMessage="* الحالة التعليمية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4" runat="server" visible="false" id="PnlMehnah">
                                            <div class="form-group">
                                                <label>
                                                    مهنة الاب قبل الوفاة : <span style="color: red">*</span>
                                                </label>
                                                <asp:TextBox ID="txtMehnahAlAAbKablAlWafah" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                                    ControlToValidate="DLAlHalahAlTaelimiahllmostafeed" ErrorMessage="* مهنة الاب قبل الوفاة" ForeColor="White" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container-fluid">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>
                                                    سليم :
                                                </label>
                                                <asp:CheckBox ID="CBSaleem" runat="server" Font-Size="14px" CssClass="checkbox-inline" ValidationGroup="g2" />
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>
                                                    معاق :
                                                </label>
                                                <asp:CheckBox ID="CBMoalek" runat="server" Font-Size="14px" CssClass="checkbox-inline" ValidationGroup="g2" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    نوع الإعاقة :
                                                </label>
                                                <asp:TextBox ID="txtTypeAleakah" runat="server" class="form-control" ValidationGroup="g2" Text="-"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container-fluid">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label>
                                                    مريض :
                                                </label>
                                                <asp:CheckBox ID="CBMareedh" runat="server" Font-Size="14px" CssClass="checkbox-inline" ValidationGroup="g2" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    نوع المرض :
                                                </label>
                                                <asp:TextBox ID="txtTypeAlmaradh" runat="server" class="form-control" ValidationGroup="g2" Text="-"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="clearfix visible-sm-block"></div>
                                    <div align="left">
                                        <button type="button" class="btn btn-previous btn-warning">الخطوة السابقة</button>
                                        <button type="button" class="btn btn-next btn-info">الخطوة التالية</button>
                                    </div>
                                </div>
                            </fieldset>

                            <fieldset style="display: none">
                                <div class="form-top">
                                    <div class="form-top-left">
                                        <h3>خطوة 3 / 6</h3>
                                        <p>الحالة المادية والسكنية للمستفيد :</p>
                                    </div>
                                    <div class="form-top-right">
                                        <i class="fa fa-money"></i>
                                    </div>
                                </div>
                                <div class="form-bottom">
                                    <div class="container-fluid">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    الدخل الشهري : <span style="color: red">*</span>
                                                </label>
                                                <asp:TextBox ID="txtAlDakhlAlShahryllMostafeed" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator20" runat="server"
                                                    ControlToValidate="txtAlDakhlAlShahryllMostafeed" ErrorMessage="* الدخل الشهري" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtAlDakhlAlShahryllMostafeed"
                                                    ErrorMessage="أرقام فقط" Font-Size="10px" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                                    Display="Dynamic">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    مصدر الدخل : <span style="color: red">*</span>
                                                </label>
                                                <asp:DropDownList ID="DLMasderAlDakhl" runat="server" ValidationGroup="g2" CssClass="form-control" Style="font-size: 13px;">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator21" runat="server"
                                                    ControlToValidate="DLMasderAlDakhl" ErrorMessage="* مصدر الدخل" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container-fluid">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    نوع السكن : <span style="color: red">*</span>
                                                </label>
                                                <asp:DropDownList ID="DLTypeAlMasken" runat="server" ValidationGroup="g2" CssClass="form-control" Style="font-size: 13px;">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator22" runat="server"
                                                    ControlToValidate="DLTypeAlMasken" ErrorMessage="* نوع السكن" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    حالة المسكن : <span style="color: red">*</span>
                                                </label>
                                                <asp:DropDownList ID="DLHaletAlMasken" runat="server" ValidationGroup="g2" CssClass="form-control" Style="font-size: 13px;">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator23" runat="server"
                                                    ControlToValidate="DLHaletAlMasken" ErrorMessage="* حالة المسكن" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div align="left">
                                        <button type="button" class="btn btn-previous btn-warning">الخطوة السابقة</button>
                                        <button type="button" class="btn btn-next btn-info">الخطوة التالية</button>
                                    </div>
                                </div>
                            </fieldset>
                            
                            <fieldset style="display: none">
                                <div class="form-top">
                                    <div class="form-top-left">
                                        <h3>خطوة 4 / 6</h3>
                                        <p>الحساب البنكي :</p>
                                    </div>
                                    <div class="form-top-right">
                                        <i class="fa fa-money"></i>
                                    </div>
                                </div>
                                <div class="form-bottom">
                                    <div class="container-fluid">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    حدد البنك : <span style="color: red">*</span>
                                                </label>
                                                <asp:DropDownList ID="DLBank" runat="server" ValidationGroup="g2" CssClass="form-control" Style="font-size: 13px;">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                    <asp:ListItem Value="البنك الأهلي">البنك الأهلي</asp:ListItem>
                                                    <asp:ListItem Value="بنك ساب">بنك ساب</asp:ListItem>
                                                    <asp:ListItem Value="البنك السعودي للاستثمار">البنك السعودي للاستثمار</asp:ListItem>
                                                    <asp:ListItem Value="البنك السعودي للاستثمار">البنك السعودي للاستثمار</asp:ListItem>
                                                    <asp:ListItem Value="مصرف الإنماء">مصرف الإنماء</asp:ListItem>
                                                    <asp:ListItem Value="البنك السعودي الفرنسي">البنك السعودي الفرنسي</asp:ListItem>
                                                    <asp:ListItem Value="بنك الرياض">بنك الرياض</asp:ListItem>
                                                    <asp:ListItem Value="مصرف الراجحي">مصرف الراجحي</asp:ListItem>
                                                    <asp:ListItem Value="البنك العربي الوطني">البنك العربي الوطني</asp:ListItem>
                                                    <asp:ListItem Value="بنك البلاد">بنك البلاد</asp:ListItem>
                                                    <asp:ListItem Value="بنك الجزيرة">بنك الجزيرة</asp:ListItem>
                                                    <asp:ListItem Value="بنك الخليج الدولي">بنك الخليج الدولي</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator212" runat="server"
                                                    ControlToValidate="DLBank" ErrorMessage="* حدد البنك" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    رقم الحساب : <span style="color: red">*</span>
                                                </label>
                                                <asp:TextBox ID="txtBank_Account" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                                    ControlToValidate="txtBank_Account" ErrorMessage="* رقم الحساب" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtBank_Account"
                                                    ErrorMessage="أرقام فقط" Font-Size="10px" ValidationExpression="^[0-9]+$" ValidationGroup="g2"
                                                    Display="Dynamic">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container-fluid">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>
                                                    رقم IBAN : <span style="color: red">*</span>
                                                </label>
                                                <asp:TextBox ID="txtIBAN_Account" runat="server" class="form-control" ValidationGroup="g2"></asp:TextBox>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                                    ControlToValidate="txtIBAN_Account" ErrorMessage="* رقم IBAN" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="container-fluid" dir="rtl" runat="server" visible="false">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    الباحث : 
                                                </label>
                                                <asp:DropDownList ID="DLAlBaheth" runat="server" ValidationGroup="g2" CssClass="form-control" Style="font-size: 13px;">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator19" runat="server"
                                                    ControlToValidate="DLHaletAlMasken" ErrorMessage="* حدد الباحث" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                    ValidationGroup="g2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    رئيس لجنة البحث : 
                                                </label>
                                                <asp:DropDownList ID="DLRaeesLagnatAlBahath" runat="server" ValidationGroup="g2" CssClass="form-control" Style="font-size: 13px;">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    مدير الجمعية :
                                                </label>
                                                <asp:DropDownList ID="DLModerAlGmeiah" runat="server" ValidationGroup="g2" CssClass="form-control" Style="font-size: 13px;">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>
                                                    رئيس مجلس الإدارة : 
                                                </label>
                                                <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="g2" CssClass="form-control" Style="font-size: 13px;">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div align="left">
                                        <button type="button" class="btn btn-previous btn-warning">الخطوة السابقة</button>
                                        <asp:Button ID="btnAdd" runat="server" Text="الخطوة التالية" CssClass="btn btn-info" OnClick="btnAdd_Click" />
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div runat="server" id="IDFile" visible="false">
                            <div class="form-top">
                                <div class="form-top-left">
                                    <h3>خطوة 5 / 6</h3>
                                    <p>الوثائق والمرفقات :</p>
                                </div>
                                <div class="form-top-right">
                                    <i class="fa fa-file"></i>
                                </div>
                            </div>
                            <div class="form-bottom">
                                <div class="container-fluid">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                عنوان الصورة : 
                                            </label>
                                            <asp:TextBox ID="txtTitleImg" runat="server" class="form-control" ValidationGroup="gImg"></asp:TextBox>
                                            <asp:Label ID="lblTitleImg" runat="server" Text="* عنوان الصورة" Font-Size="11px" ForeColor="Red" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>
                                                المسموح :(bmp , gif , png , jpg , jpeg)
                                            </label>
                                            <asp:FileUpload ID="FBenaaHome" runat="server" ValidationGroup="gImg" />
                                            <asp:Label ID="lblBenaaHome" runat="server" Text="* حدد الصور" Font-Size="11px" ForeColor="Red" Visible="false"></asp:Label>
                                            <br />
                                            <asp:Button ID="LBBenaaHome" runat="server" Text="رفع الصور" data-toggle="tooltip" title="رفع الصور" CssClass="btn btn-info" ValidationGroup="gImg" OnClick="LBBenaaHome_Click" />
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <asp:Repeater ID="RPTImgMosTafeed" runat="server">
                                            <ItemTemplate>
                                                <div class="col-md-2">
                                                    <span><%# Eval("TitleImg") %></span>
                                                    <a href='<%# "../" + Eval("ImgMosTafeed") %>' target="_blank" title="تكبير الصورة" data-toggle="tooltip">
                                                        <img src='<%# "../" + Eval("ImgMosTafeed") %>' style="margin: 4px; border-radius: 5px" width="90%" height="92" />
                                                    </a>
                                                    <div align="center">
                                                        <%--<asp:Button ID="LBDeleteBenaaHome" OnClientClick="return insertConfirmation();" title="حذف" runat="server" CssClass="btn btn-danger" Text="حذف الصورة" OnClick="LBDeleteBenaaHome_Click" CommandArgument='<%# Eval("_IDItam") %>' />--%>
                                                        <%--<asp:LinkButton ID="LBDeleteBenaaHome" runat="server" OnClientClick="return insertConfirmation();" title="حذف" data-toggle="tooltip" OnClick="LBDeleteBenaaHome_Click" CommandArgument='<%# Eval("_IDItam") %>'>
                                                            <i class="fa fa-trash-o"></i>
                                                        </asp:LinkButton>--%>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <div align="left">
                                    <asp:Button ID="btnBuy" runat="server" class="btn btn-info" Text="الخطوة التالية" OnClick="btnBuy_Click" />
                                </div>

                            </div>
                        </div>
                </asp:Panel>
            </div>
        </div>
    </body>
    <!-- Javascript -->
    <script src="/MultiForm3/assets/js/jquery.backstretch.min.js"></script>
    <script src="/MultiForm3/assets/js/scripts.js"></script>

    <asp:HiddenField ID="HFNameSite" runat="server" />
    <asp:HiddenField ID="HFTitle" runat="server" />
    <asp:HiddenField ID="HFDescrption" runat="server" />
    <asp:HiddenField ID="HFKeyWord" runat="server" />
    <asp:HiddenField ID="HFImage" runat="server" />
    <asp:HiddenField ID="HFLink" runat="server" />
</asp:Content>

