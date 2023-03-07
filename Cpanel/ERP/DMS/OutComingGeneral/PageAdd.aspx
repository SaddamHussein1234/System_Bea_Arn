﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/DMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAdd.aspx.cs" Inherits="Cpanel_ERP_DMS_OutComingGeneral_PageAdd" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
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
    <script src="/SADDAMEditor4.19.1/ckeditor.js"></script>
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
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" class="btn btn-default" OnClick="LB_Back_Click">
                     <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="/Cpanel/ERP/DMS/">الرئيسية</a></li>
                    <li><a href="PageAdd.aspx">إضافة ملف جديد</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة ملف صادر عام"></asp:Label>
                    </h3>
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
                            <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                    </div>
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>الارشيف : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="ddlYears" runat="server" ValidationGroup="GBill" AutoPostBack="true" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged"
                                            Height="25px" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                            ControlToValidate="ddlYears" ErrorMessage="* الارشيف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>رقم الصادر : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtNumber" runat="server" class="form-control" ValidationGroup="GBill"
                                            Style="direction: ltr"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="txtNumber" ErrorMessage="* رقم الفاتورة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNumber"
                                            Font-Size="11px" ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="GBill"
                                            Display="Dynamic">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5><a href="../Setting/PageCategory.aspx?ID=Out_General" data-toggle="tooltip" title="عرض القوائم">حدد الفئة</a> : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLCategory" runat="server" ValidationGroup="GBill" 
                                            Height="25px" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="DLCategory" ErrorMessage="* حدد الفئة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <br />
                                        <a  href='javaScript:void(0)' data-toggle="modal" data-target="#IDAddCategory" title="إضافة جديد" class="btn btn-info" runat="server" id="AddCategory"><i class="fa fa-plus"></i></a>
                                        <div id="IDAddCategory" class="modal fade in modal_New_Style">
                                            <div class="modal-dialog " style="max-width: 450px">
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
                                                                        <asp:Label ID="Label2" runat="server" Text="إضافة فئة : "></asp:Label> 
                                                                    </label>
                                                                    <div align="">
                                                                        <div class="" dir="rtl">
                                                                            <div class="col-lg-12">
                                                                                <div class="form-group">
                                                                                    <h5 style="text-align:right"> عنوان الفئة عربي : <i style="color: red">*</i></h5>
                                                                                    <asp:TextBox ID="txt_Category_Ar" runat="server" class="form-control" ValidationGroup="VGCategory"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator14" runat="server"
                                                                                        ControlToValidate="txt_Category_Ar" ErrorMessage="* العنوان" ForeColor="#FF0066"
                                                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGCategory" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-12">
                                                                                <div class="form-group">
                                                                                    <h5 style="text-align:right"> عنوان الفئة إنجليزي : <i style="color: red">*</i></h5>
                                                                                    <asp:TextBox ID="txt_Category_En" runat="server" class="form-control" ValidationGroup="VGCategory" 
                                                                                        Style="text-align: left; direction: ltr;" Text="-"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator16" runat="server"
                                                                                        ControlToValidate="txt_Category_En" ErrorMessage="* العنوان" ForeColor="#FF0066"
                                                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGCategory" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <asp:LinkButton ID="LBVGCategory" runat="server" Style="margin-right: 4px;" OnClick="LBVGCategory_Click" Visible="true"
                                                                        class="btn btn-success btn-fill pull-left" ValidationGroup="VGCategory">تحديث البيانات</asp:LinkButton>
                                                                    <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>تاريخ الصادر : <span style="color: red">*</span>
                                        </h5>
                                        <div class="input-group date " >
                                            <asp:TextBox ID="txtDateAdd" runat="server" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="GBill" Style="direction: ltr"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                                ControlToValidate="txtDateAdd" ErrorMessage="* تاريخ الصادر" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>حدد الجهة الصادر منها : <span runat="server" id="lblPhoneSend"></span> <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLParty_Send" runat="server" ValidationGroup="GBill" 
                                            Height="25px" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator21" runat="server"
                                            ControlToValidate="DLParty_Send" ErrorMessage="* حدد الجهة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <br />
                                        <a  href='javaScript:void(0)' data-toggle="modal" data-target="#IDAddCompanySend" title="إضافة جديد" class="btn btn-info" runat="server" id="IDAddSend"><i class="fa fa-plus"></i></a>
                                        <div id="IDAddCompanySend" class="modal fade in modal_New_Style">
                                            <div class="modal-dialog " style="max-width: 450px">
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
                                                                        <asp:Label ID="lblTitleSend" runat="server" Text="إضافة جهة مصدرة : "></asp:Label> 
                                                                    </label>
                                                                    <div align="">
                                                                        <div class="" dir="rtl">
                                                                            <div class="col-md-12">
                                                                                <div class="form-group">
                                                                                    <h5>الفئة : <i style="color: red">*</i></h5>
                                                                                    <asp:DropDownList ID="DLType_Customer_Send" runat="server" Width="250" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="VGSend">
                                                                                        <asp:ListItem Value=""></asp:ListItem>
                                                                                        <asp:ListItem Value="شركات">شركات</asp:ListItem>
                                                                                        <asp:ListItem Value="أفراد">أفراد</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator25" runat="server"
                                                                                        ControlToValidate="DLType_Customer_Send" ErrorMessage="*  حدد الفئة" ForeColor="#FF0066"
                                                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGSend" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-12">
                                                                                <div class="form-group">
                                                                                    <h5 style="text-align:right"> الإسم : <i style="color: red">*</i></h5>
                                                                                    <asp:HiddenField ID="HFPhoneSend" runat="server" />
                                                                                    <asp:TextBox ID="txtCompanyNameSend" runat="server" class="form-control" ValidationGroup="VGSend"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator26" runat="server"
                                                                                        ControlToValidate="txtCompanyNameSend" ErrorMessage="* الإسم" ForeColor="#FF0066"
                                                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGSend" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-12">
                                                                                <div class="form-group">
                                                                                    <h5>رقم الجوال (رسائل SMS) : <i style="color: red">*</i></h5>
                                                                                    <asp:TextBox ID="txtPhone_Number1_Send" runat="server" class="form-control" ValidationGroup="VGSend" TextMode="Number"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator27" runat="server"
                                                                                        ControlToValidate="txtPhone_Number1_Send" ErrorMessage="* رقم الجوال" ForeColor="#FF0066"
                                                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGSend" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator36" runat="server" ControlToValidate="txtPhone_Number1_Send"
                                                                                        ErrorMessage="* أرقام فقط ..." ValidationExpression="^[0-9]+$" ValidationGroup="VGSend" Font-Size="10px"
                                                                                        Display="Dynamic">
                                                                                    </asp:RegularExpressionValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-12 hide">
                                                                                <div class="form-group">
                                                                                    <h5>حدد البلد : <i style="color: red">*</i></h5>
                                                                                    <asp:DropDownList ID="ddlCountrySend" runat="server" Width="250" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="VGSend">
                                                                                        <asp:ListItem Value=""></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator28" runat="server"
                                                                                        ControlToValidate="ddlCountrySend" ErrorMessage="* حدد البلد" ForeColor="#FF0066"
                                                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGSend" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <asp:LinkButton ID="LBSend" runat="server" Style="margin-right: 4px;" OnClick="LBSend_Click" Visible="true"
                                                                        class="btn btn-success btn-fill pull-left" ValidationGroup="VGSend">تحديث البيانات</asp:LinkButton>
                                                                    <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5><a href="../Setting/PageParty.aspx" data-toggle="tooltip" title="عرض الجهات">حدد الجهة المصدر لها</a> : <span runat="server" id="lblPhone"></span><span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLParty" runat="server" ValidationGroup="GBill" 
                                            Height="25px" Width="100%" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator18" runat="server"
                                            ControlToValidate="DLParty" ErrorMessage="* حدد الجهة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        <br />
                                        <a  href='javaScript:void(0)' data-toggle="modal" data-target="#IDAddCompany" title="إضافة جديد" class="btn btn-info" runat="server" id="IDAdd"><i class="fa fa-plus"></i></a>
                                        <div id="IDAddCompany" class="modal fade in modal_New_Style">
                                            <div class="modal-dialog " style="max-width: 450px">
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
                                                                        <asp:Label ID="lblTitle" runat="server" Text="إضافة جهة : "></asp:Label> 
                                                                    </label>
                                                                    <div align="">
                                                                        <div class="" dir="rtl">
                                                                            <div class="col-md-12">
                                                                                <div class="form-group">
                                                                                    <h5>الفئة : <i style="color: red">*</i></h5>
                                                                                    <asp:DropDownList ID="DLType_Customer" runat="server" Width="250" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="VGAdd2">
                                                                                        <asp:ListItem Value=""></asp:ListItem>
                                                                                        <asp:ListItem Value="شركات">شركات</asp:ListItem>
                                                                                        <asp:ListItem Value="أفراد">أفراد</asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator20" runat="server"
                                                                                        ControlToValidate="DLType_Customer" ErrorMessage="*  حدد الفئة" ForeColor="#FF0066"
                                                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-12">
                                                                                <div class="form-group">
                                                                                    <h5 style="text-align:right"> الإسم : <i style="color: red">*</i></h5>
                                                                                    <asp:HiddenField ID="HFPhone" runat="server" />
                                                                                    <asp:TextBox ID="txtCompanyName" runat="server" class="form-control" ValidationGroup="VGAdd2"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator19" runat="server"
                                                                                        ControlToValidate="txtCompanyName" ErrorMessage="* الإسم" ForeColor="#FF0066"
                                                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-lg-12">
                                                                                <div class="form-group">
                                                                                    <h5>رقم الجوال (رسائل SMS) : <i style="color: red">*</i></h5>
                                                                                    <asp:TextBox ID="txtPhone_Number1" runat="server" class="form-control" ValidationGroup="VGAdd2" TextMode="Number"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                                                                        ControlToValidate="txtPhone_Number1" ErrorMessage="* رقم الجوال" ForeColor="#FF0066"
                                                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPhone_Number1"
                                                                                        ErrorMessage="* أرقام فقط ..." ValidationExpression="^[0-9]+$" ValidationGroup="VGAdd2" Font-Size="10px"
                                                                                        Display="Dynamic">
                                                                                    </asp:RegularExpressionValidator>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-12 hide">
                                                                                <div class="form-group">
                                                                                    <h5>حدد البلد : <i style="color: red">*</i></h5>
                                                                                    <asp:DropDownList ID="ddlCountry" runat="server" Width="250" CssClass="form-control2 chzn-select dropdown" Style="font-size: 12px;" ValidationGroup="VGAdd2">
                                                                                        <asp:ListItem Value=""></asp:ListItem>
                                                                                    </asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator22" runat="server"
                                                                                        ControlToValidate="ddlCountry" ErrorMessage="* حدد البلد" ForeColor="#FF0066"
                                                                                        meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="VGAdd2" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="modal-footer">
                                                                    <asp:LinkButton ID="LBSave2" runat="server" Style="margin-right: 4px;" OnClick="LBSave2_Click" Visible="true"
                                                                        class="btn btn-success btn-fill pull-left" ValidationGroup="VGAdd2">تحديث البيانات</asp:LinkButton>
                                                                    <button type="button" class="btn btn-default -mb-3" data-dismiss="modal">اغلاق</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>تاريخ إرسال المعاملة : <span style="color: red">*</span>
                                        </h5>
                                        <div class="input-group date " >
                                            <asp:TextBox ID="txtDateAddSend" runat="server" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="GBill" Style="direction: ltr"></asp:TextBox>
                                            <span class="input-group-btn">
                                                <button class="btn btn-default" type="button">
                                                    <i class="fa fa-calendar"></i>
                                                </button>
                                            </span>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="txtDateAddSend" ErrorMessage="* تاريخ المعاملة" ForeColor="#FF0066"
                                                meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5><a href="../Setting/PageNature.aspx" data-toggle="tooltip" title="عرض القوائم">طبيعة المعاملة</a> : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DL_Nature" runat="server" ValidationGroup="GBill"
                                            CssClass="form-control2 chzn-select dropdown" Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                            ControlToValidate="DL_Nature" ErrorMessage="* حدد طبيعة المعاملة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <br />
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5><a href="../Setting/PageImportance.aspx" data-toggle="tooltip" title="عرض القوائم">أهمية المعاملة </a>: <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DL_Importance" runat="server" ValidationGroup="GBill"
                                            CssClass="form-control2 chzn-select dropdown" Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                            ControlToValidate="DL_Importance" ErrorMessage="* حدد أهمية المعاملة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <br />
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>الموضوع : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txt_Title" runat="server" class="form-control" ValidationGroup="GBill"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                            ControlToValidate="txt_Title" ErrorMessage="* الموضوع" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>عنوان المرفقات : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txt_Title_Attachments" runat="server" class="form-control" ValidationGroup="GBill"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator15" runat="server"
                                            ControlToValidate="txt_Title_Attachments" ErrorMessage="* عنوان المرفقات" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
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
                        <asp:Label ID="lblTitleView" runat="server" Text="حدد من سيظهر في الخطاب"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-4" runat="server">
                                    <div class="form-group">
                                        <script type="text/javascript">
                                            $(function () {
                                                $(document.getElementById("<%=CBGeneral_Director.ClientID %>")).click(function () {
                                                    if ($(this).is(":checked")) {
                                                        $("#pnlGeneral_Director").show();
                                                    } else {
                                                        $("#pnlGeneral_Director").hide();
                                                    }
                                                });
                                            });
                                        </script>
                                        <h5>مدير الجمعية :
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBGeneral_Director" runat="server" />
                                            <span class="slider round"></span>
                                        </label></h5>
                                        <div id="pnlGeneral_Director" style="margin-top:20px; display: none; <%= FCheck("General_Director") %>">
                                            <asp:DropDownList ID="DLGeneral_Director" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown" Width="250" >
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator13" runat="server"
                                                ControlToValidate="DLGeneral_Director" ErrorMessage="* مدير الجمعية" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                        
                                    </div>
                                </div>
                                <div class="col-lg-4" runat="server">
                                    <div class="form-group">
                                        <script type="text/javascript">
                                            $(function () {
                                                $(document.getElementById("<%=CBDirector_Of_Personnel.ClientID %>")).click(function () {
                                                    if ($(this).is(":checked")) {
                                                        $("#pnlDirector_Of_Personnel").show();
                                                    } else {
                                                        $("#pnlDirector_Of_Personnel").hide();
                                                    }
                                                });
                                            });
                                        </script>
                                        <h5>مدير شؤون الموظفين :
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBDirector_Of_Personnel" runat="server" />
                                            <span class="slider round"></span>
                                        </label></h5>
                                        <div id="pnlDirector_Of_Personnel" style="margin-top:20px; display: none; <%= FCheck("Director_Of_Personnel") %>">
                                            <asp:DropDownList ID="DLDirector_Of_Personnel" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown" Width="250" >
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator23" runat="server"
                                                ControlToValidate="DLDirector_Of_Personnel" ErrorMessage="* مدير شؤون الموظفين" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                        
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <script type="text/javascript">
                                            $(function () {
                                                $(document.getElementById("<%=CBCashier.ClientID %>")).click(function () {
                                                    if ($(this).is(":checked")) {
                                                        $("#pnlCashier").show();
                                                    } else {
                                                        $("#pnlCashier").hide();
                                                    }
                                                });
                                            });
                                        </script>
                                        <h5>المشرف المالي : 
                                            <label class="switch">
                                                <input name="RememberMe" type="checkbox" id="CBCashier" runat="server" />
                                                <span class="slider round"></span>
                                            </label>
                                        </h5>
                                        <div id="pnlCashier" style="margin-top:20px; display: none; <%= FCheck("Cashier") %>">
                                            <asp:DropDownList ID="DLCashier" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown" Width="250">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server"
                                                ControlToValidate="DLCashier" ErrorMessage="* المشرف المالي" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <script type="text/javascript">
                                            $(function () {
                                                $(document.getElementById("<%=CBSecretary_General.ClientID %>")).click(function () {
                                                    if ($(this).is(":checked")) {
                                                        $("#pnlSecretary_General").show();
                                                    } else {
                                                        $("#pnlSecretary_General").hide();
                                                    }
                                                });
                                            });
                                        </script>
                                        <h5>الأمين العام : 
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBSecretary_General" runat="server" />
                                            <span class="slider round"></span>
                                        </label></h5>
                                        <div id="pnlSecretary_General" style="margin-top:20px; display: none; <%= FCheck("Secretary_General") %>">
                                            <asp:DropDownList ID="DLSecretary_General" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown" Width="250">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="DLSecretary_General" ErrorMessage="* الأمين العام" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <script type="text/javascript">
                                            $(function () {
                                                $(document.getElementById("<%=CBDeputy_Chairman_Of_The_Board.ClientID %>")).click(function () {
                                                    if ($(this).is(":checked")) {
                                                        $("#pnlDeputy_Chairman_Of_The_Board").show();
                                                    } else {
                                                        $("#pnlDeputy_Chairman_Of_The_Board").hide();
                                                    }
                                                });
                                            });
                                        </script>
                                        <h5>نائب رئيس مجلس الإدارة : 
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBDeputy_Chairman_Of_The_Board" runat="server" />
                                            <span class="slider round"></span>
                                        </label></h5>
                                        <div id="pnlDeputy_Chairman_Of_The_Board" style="margin-top:20px; display: none; <%= FCheck("Deputy_Chairman_Of_The_Board") %>">
                                            <asp:DropDownList ID="DLDeputy_Chairman_Of_The_Board" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown" Width="250">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator24" runat="server"
                                                ControlToValidate="DLDeputy_Chairman_Of_The_Board" ErrorMessage="* نائب رئيس مجلس الإدارة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <script type="text/javascript">
                                            $(function () {
                                                $(document.getElementById("<%=CBChairman_Of_Board_Of_Directors.ClientID %>")).click(function () {
                                                    if ($(this).is(":checked")) {
                                                        $("#pnlChairman_Of_Board_Of_Directors").show();
                                                    } else {
                                                        $("#pnlChairman_Of_Board_Of_Directors").hide();
                                                    }
                                                });
                                            });
                                        </script>
                                        <h5>رئيس مجلس الإدارة : 
                                        <label class="switch">
                                            <input name="RememberMe" type="checkbox" id="CBChairman_Of_Board_Of_Directors" runat="server" />
                                            <span class="slider round"></span>
                                        </label></h5>
                                        <div id="pnlChairman_Of_Board_Of_Directors" style="margin-top:20px; display: none; <%= FCheck("Chairman_Of_Board_Of_Directors") %>">
                                            <asp:DropDownList ID="DLChairman_Of_Board_Of_Directors" runat="server" ValidationGroup="GBill" CssClass="form-control2 chzn-select dropdown" Width="250">
                                                <asp:ListItem></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                                ControlToValidate="DLChairman_Of_Board_Of_Directors" ErrorMessage="* رئيس مجلس الإدارة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
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
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lblTitleDetails" runat="server" Text="البيان"></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:TextBox ID="txt_Note" runat="server" class="form-control" ValidationGroup="GBill" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                        <script type="text/javascript" lang="javascript">CKEDITOR.replace('<%=txt_Note.ClientID%>');</script> 
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
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <h5>
                                            <asp:Label ID="lblSend" runat="server" Text="إرسال إشعار SMS"></asp:Label> <span style="color: red">تحت الصيانة</span>
                                            <i class="fa fa-envelope"></i>: <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLSend" runat="server" ValidationGroup="GBill" Enabled="false"
                                            CssClass="form-control" Width="100%">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="Yes">نعم أرسل</asp:ListItem>
                                            <asp:ListItem Value="No">لا تقم بالإرسل</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator50" runat="server" Visible="false"
                                            ControlToValidate="DLSend" ErrorMessage="* حدد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-9">
                                    <div class="form-group" align="left">
                                        <br />
                                        <asp:LinkButton ID="LBNew" runat="server"  ValidationGroup="GBill"  OnClick="LBNew_Click"
                                            class="btn btn-info">حفظ الخطاب والذهاب إلى المرفقات</asp:LinkButton>
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
        <asp:HiddenField ID="HFID" runat="server" />
        <asp:HiddenField ID="HFIDStore" runat="server" />
</asp:Content>

