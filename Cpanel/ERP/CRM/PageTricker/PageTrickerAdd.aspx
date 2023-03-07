<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/CRM/CRM_Main.master" AutoEventWireup="true" CodeFile="PageTrickerAdd.aspx.cs" Inherits="Cpanel_ERP_CRM_PageTricker_PageTrickerAdd" %>

<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterBottom.ascx" TagPrefix="uc1" TagName="WUCFooterBottom" %>
<%@ Register Src="~/Cpanel/ERP/WUCFooterBottomERP.ascx" TagPrefix="uc1" TagName="WUCFooterBottomERP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function DisableButton() {
            document.getElementById("<%=btnSend.ClientID %>").disabled = true;
        }
        window.onbeforeunload = DisableButton;
    </script>
    <script type="text/javascript">
        function ConfirmRgistry() {
            if (confirm("رسالة تأكيد , هل تريد الإستمرار ؟") == true)
                return true;
            else
                return false;
        }
    </script>

    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <link href="<%=ResolveUrl("~/Cpanel/test/LoginAr.css")%>" rel="stylesheet" />

    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content" dir="rtl">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة">
                    <i class="fa fa-print"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="Default.aspx">الرئيسية</a></li>
                    <li><a href="PageTricker.aspx">متابعة الداعمين</a></li>
                    <li><a href="PageTrickerAdd.aspx">إضافة متابعة</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="lbmsg" runat="server" Text="إضافة عملة متابعة"></asp:Label>
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
                                <div class="control-group">
                                    <div class="controls">
                                        <asp:Panel ID="pnlData" runat="server">
                                            <div class="table table-responsive" id="pnlPrint" runat="server" dir="rtl">
                                                <div class="HideNow">
                                                    <uc1:WUCHeader runat="server" ID="WUCHeader" />
                                                </div>
                                                <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                                    <thead>
                                                        <tr class="th">
                                                            <th colspan="2">
                                                                <div align="center" class="w">
                                                                    <asp:TextBox ID="txtTitle" runat="server" class="form-control" placeholder="عنوان البحث"
                                                                        Style="text-align: center; width: 100%; font-family: 'Alwatan'; font-size: 18px;"></asp:TextBox>
                                                                </div>
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:Repeater ID="RPTMessage" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="font-size:18px; font-weight:bold; width:20px;">
                                                                        <%# Container.ItemIndex + 1 %>
                                                                    </td>
                                                                    <td>
                                                                        <div style='background-color: #bcff9d; margin-bottom: 10px; padding: 2px 7px 2px 2px; border-top-left-radius: 20px; border-bottom-left-radius: 20px; border-bottom-right-radius: 20px; margin-left: 10%'>
                                                                            <div class='panel-body'>
                                                                                <asp:LinkButton ID="LBDelete" runat="server" class="close" 
                                                                                    style="float:left; margin-left:10px;" OnClientClick="return ConfirmRgistry();"
                                                                                    OnClick="LBDelete_Click" CommandArgument='<%# Eval("_ID_Item_") %>'>
                                                                                        <span aria-hidden="true">&times;</span>
                                                                                </asp:LinkButton>
                                                                                <h4 style='color: Red'>من قبل : <%# Library_CLS_Arn.ClassOutEntity.ClassQuaem.FAlBaheth((Int32) (Eval("CreatedBy")))%>
                                                                                </h4>
                                                                                <h5 class="MarginTop_" style='padding-right: 10px;'><%# Eval("_Descryption_") %></h5>
                                                                                <div align='center'><span class="MarginTop_" style='font-size: 12px'><%# Eval("CreatedDate") %></span></div>
                                                                                <div style="float: left; margin: -35px 0 0 10px;">
                                                                                    <%# FGetAttach((string) Eval("_File_Attach_")) %>
                                                                                    <asp:LinkButton ID="LinkTitle" runat="server" OnClick="LinkTitle_Click" CommandArgument='<%# Eval("_File_Attach_") %>'>
                                                                                                <%# FGetPath((string) Eval("_File_Attach_")) %>
                                                                                    </asp:LinkButton>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                    <tfoot>
                                                        <tr>
                                                            <th colspan="2">
                                                                <hr style='border: solid; border-width: 1px; width: 100%' />
                                                                <div class="HideNow">
                                                                    <uc1:WUCFooterBottom runat="server" ID="WUCFooterBottom" />
                                                                </div>
                                                            </th>
                                                        </tr>
                                                    </tfoot>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlNull" runat="server" Visible="False">
                                            <br />
                                            <br />
                                            <br />
                                            <div align="center" style="">
                                                <h3>لا توجد نتائج </h3>
                                            </div>
                                            <br />
                                            <br />
                                        </asp:Panel>
                                        <br />
                                    </div>
                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <asp:TextBox ID="txt_Message" runat="server" Style="direction: rtl; text-align: right;" class="form-control" ValidationGroup="g2" placeholder="إدخل النص ... " TextMode="MultiLine" Rows="6"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="إدخل النص" CssClass="font"
                                                ControlToValidate="txt_Message" ValidationGroup="g2" Font-Size="10px" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <h5>إرفاق ملف PDF</h5>
                                            <div id="divUploadPhoto" runat="server" class="divUploadPhoto" style="display: block;">
                                                <asp:FileUpload ID="fuPhoto" runat="server" data-toggle="tooltip" ToolTip="تحديد شعار الشركة" ValidationGroup="g2" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:LinkButton ID="LBBack" runat="server" class="btn btn-danger btn-fill pull-right"
                                                Style="margin-right: 5px" OnClick="LBBack_Click">إنهاء</asp:LinkButton>
                                            <asp:Button ID="btnSend" runat="server" Text="إرسال" CssClass="btn btn-info btn-fill pull-right"
                                                ValidationGroup="g2" OnClick="btnSend_Click" />
                                        </div>
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>

