<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/CRS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageAdd.aspx.cs" Inherits="Cpanel_ERP_CRS_Committee_Report_PageAdd" %>
<%@ Import Namespace="Library_CLS_Arn.ClassOutEntity" %>
<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script type="text/javascript">
        function Confirmation() {
            var answer = confirm("هل تريد الإستمرار ؟")
            if (answer) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>

    <script type="text/javascript">
        function ShowIDModelEdit() {
            $("#IDEditCompany").modal('show');
        }

        $(function () {
            $("#btnShow").click(function () {
                showModal();
            });
        });
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
                        title="تحديث" OnClick="btnRefrish_Click">
                    <i class="fa fa-refresh"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" class="btn btn-default" OnClick="LB_Back_Click">
                     <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="../">الرئيسية</a></li>
                    <li><a href="PageAdd.aspx">تقرير اللجنة</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-pencil"></i>
                        <asp:Label ID="Label1" runat="server" Text="تقرير اللجنة"></asp:Label>
                    </h3>
                    <div style="float: left">
                        رقم التقرير :
                            <asp:TextBox ID="txtNumberBill" runat="server" ValidationGroup="GBill" Width="100" Style="padding-right: 5px"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator11" runat="server"
                            ControlToValidate="txtNumberBill" ErrorMessage="* رقم التقرير" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtNumberBill"
                            Font-Size="11px" ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="GBill"
                            Display="Dynamic">
                        </asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="panel-body">
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
                            <asp:Label ID="lblSuccess" runat="server"></asp:Label>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                    </div>
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" >
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>الارشيف : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="ddlYears" runat="server" ValidationGroup="GBill" AutoPostBack="true" OnSelectedIndexChanged="ddlYears_SelectedIndexChanged"
                                            Height="25px" Width="100%" CssClass="form-control chzn-select dropdown" Style="font-size: 12px;">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                            ControlToValidate="ddlYears" ErrorMessage="* الارشيف" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <h5>نوع اللجنة : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLType" runat="server" ValidationGroup="GBill" CssClass="form-control chzn-select dropdown">
                                            <asp:ListItem Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator23" runat="server"
                                            ControlToValidate="DLType" ErrorMessage="* حدد نوع التقرير" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>  
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>موضوع التقرير : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" ValidationGroup="GBill"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtTitle" ErrorMessage="* عنوان التقرير" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <h5>مقر الاجتماع : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtMeeting_Venue" runat="server" CssClass="form-control" ValidationGroup="GBill"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator6" runat="server"
                                            ControlToValidate="txtMeeting_Venue" ErrorMessage="* مقر الاجتماع" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <h5>تاريخ التقرير : <span style="color: red">*</span>
                                        </h5>
                                        <div>
                                            <div class="input-group date " >
                                                <asp:TextBox ID="txtDateAdd" runat="server" class="form-control" data-date-format="YYYY-MM-DD" ValidationGroup="GBill" Style="direction: ltr"></asp:TextBox>
                                                <span class="input-group-btn">
                                                    <button class="btn btn-default" type="button">
                                                        <i class="fa fa-calendar"></i>
                                                    </button>
                                                </span>
                                                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator8" runat="server"
                                                    ControlToValidate="txtDateAdd" ErrorMessage="* تاريخ الفاتورة" ForeColor="#FF0066"
                                                    meta:resourcekey="RequiredFieldValidator1Resource1" ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid">
                                <div class="col-lg-4" style="background-color:#f1eded; border-radius:7px">
                                    <div class="form-group">
                                        <h5>الهدف من التقرير : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtObjective_Of_the_Report" runat="server" CssClass="form-control" ValidationGroup="GBill" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server"
                                            ControlToValidate="txtObjective_Of_the_Report" ErrorMessage="* الهدف من التقرير" ForeColor="#FF0066" 
                                            meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <h5>توصيات التقرير : <span style="color: red">*</span>
                                        </h5>
                                        <asp:TextBox ID="txtReport_Recommendations" runat="server" CssClass="form-control" ValidationGroup="GBill" TextMode="MultiLine" Rows="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator5" runat="server"
                                            ControlToValidate="txtReport_Recommendations" ErrorMessage="* توصيات التقرير" ForeColor="#FF0066" 
                                            meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-8">
                                    <div class="col-lg-4" style="background-color:#f1eded; border-radius:7px">
                                        <div class="form-group">
                                            <h5>صور الإجتماع : 
                                            </h5>
                                            <asp:FileUpload ID="FUImages" runat="server" AllowMultiple="true" ValidationGroup="VGImages"  data-toggle="tooltip" ToolTip="يمكنك رفع أكثر من صورة في نفس الوقت" />
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server"
                                                ControlToValidate="FUImages" ErrorMessage="* حدد الصور" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="VGImages" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <br />
                                            <asp:LinkButton ID="LBUplode" runat="server" CssClass="btn btn-info" data-toggle="tooltip"
                                                title="رفع الصور" ValidationGroup="VGImages" OnClick="LBUplode_Click">
                                            رفع الصور <span class="tip-bottom"><i class="fa fa-upload" style="font-size:16px"></i></span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-lg-8" style="background-color:#f1eded; border-radius:7px">
                                        <asp:Repeater ID="RPTImages" runat="server" Visible="false">
                                            <ItemTemplate>
                                                <div class="col-lg-2">
                                                    <a href='<%# "/" + Eval("_Src_") %>' target="_blank" title="تكبير الصورة" data-toggle="tooltip">
                                                        <img src='<%# "/" + Eval("_Src_") %>' style="margin: 4px; border-radius: 5px" width="100%" height="52" />
                                                    </a>
                                                    <div align="center">
                                                        <asp:LinkButton ID="LBDeleteImage" runat="server" OnClientClick="return Confirmation();"
                                                            title="حذف" data-toggle="tooltip" OnClick="LBDeleteImage_Click" CommandArgument='<%# Eval("_ID_Item_") %>'
                                                            CommandName='<%# Eval("_Src_") %>'>
                                                         <i class="fa fa-trash"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        <asp:Panel ID="pnlNullImages" runat="server" Visible="true">
                                            <div align="center">
                                                <br /><br />
                                                <h4>لم يتم رفع صور بعد
                                                </h4>
                                                <br /><br /><br />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-lg-4"  style="background-color:#f1eded; border-radius:7px; margin-top:5px;">
                                        <div class="form-group">
                                            <h5>أعضاء اللجنة : <span style="color: red">*</span>
                                            </h5>
                                            <asp:DropDownList ID="DLCommittee_Members" runat="server" ValidationGroup="VGCommittee_Members" Width="100%" CssClass="form-control chzn-select dropdown">
                                                <asp:ListItem Value=""></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" runat="server"
                                                ControlToValidate="DLCommittee_Members" ErrorMessage="* حدد العضو" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="VGCommittee_Members" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <h5>الصفة : <span style="color: red">*</span>
                                            </h5>
                                            <asp:TextBox ID="txtAdjective" runat="server" CssClass="form-control" ValidationGroup="VGCommittee_Members"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator9" runat="server"
                                                ControlToValidate="txtAdjective" ErrorMessage="* الصفة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="VGCommittee_Members" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <h5>ترتيب رقم : <span style="color: red">*</span>
                                            </h5>
                                            <asp:TextBox ID="txtOrder" runat="server" CssClass="form-control" ValidationGroup="VGCommittee_Members"></asp:TextBox>
                                            <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator7" runat="server"
                                                ControlToValidate="txtOrder" ErrorMessage="* ترتيب رقم" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                                ValidationGroup="VGCommittee_Members" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtOrder"
                                                Font-Size="11px" ErrorMessage="أرقام فقط" ValidationExpression="^[0-9]+$" ValidationGroup="VGCommittee_Members"
                                                Display="Dynamic">
                                            </asp:RegularExpressionValidator>
                                            
                                        </div>
                                    </div>
                                    <div class="col-lg-8" style="background-color:#f1eded; border-radius:7px; margin-top:5px;">
                                        <asp:Panel ID="pnlDataCommittee_Members" runat="server" Visible="false">
                                            <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                                <thead>
                                                    <tr class="th">
                                                        <th class="StyleTD">م</th>
                                                        <th class="StyleTD">الإسم</th>
                                                        <th class="StyleTD">الصفة</th>
                                                        <th class="StyleTD">التوقيع</th>
                                                        <th class="StyleTD"></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="RPTCommittee_Members" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td style="width: 10px;" class="StyleTD">
                                                                    <span style="margin-right: 5px; font-size: 11px"><%# Container.ItemIndex + 1 %></span>
                                                                </td>
                                                                <td class="StyleTD">
                                                                    <span style="font-size: 12px"><%# ClassQuaem.FAlBaheth(Convert.ToInt32(Eval("_ID_Admin_")))%></span> <%# Eval("_ID_Admin_") %>
                                                                </td>
                                                                <td class="StyleTD">
                                                                    <span style="font-size: 12px"><%# Eval("_Adjective_")%></span>
                                                                </td>
                                                                <td class="StyleTD">
                                                                    <img src='<%# ClassSaddam.FGetSignature(Convert.ToInt32(Eval("_ID_Admin_")), Convert.ToBoolean(Eval("_Is_Admin_"))) %>' alt="Img" width="50" height="25" />
                                                                </td>
                                                                <td class="StyleTD">
                                                                    <asp:LinkButton ID="LBDeleteCommittee_Members" runat="server" OnClientClick="return Confirmation();"
                                                                        title="حذف" data-toggle="tooltip" OnClick="LBDeleteCommittee_Members_Click" CommandArgument='<%# Eval("_ID_Item_") %>'>
                                                                     <i class="fa fa-trash"></i>
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <th colspan="9">
                                                            <span style="font-size: 12px; padding-right: 5px">عدد الأعضاء : </span>
                                                            <asp:Label ID="lblCount" runat="server" Text="0" Style='color: Red; font-size: 12px'></asp:Label>
                                                        </th>
                                                    </tr>
                                                </tfoot>
                                            </table>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlNullCommittee_Members" runat="server" Visible="true">
                                            <div align="center">
                                                <br /><br />
                                                <h4>لم يتم إضافة الأعضاء بعد
                                                </h4>
                                                <br /><br /><br />
                                            </div>
                                        </asp:Panel>
                                            <div class="keepmeLogged">
                                                <label class="switch">
                                                    <input name="RememberMe" type="checkbox" id="CBViewAdmin_Allow" runat="server" checked="checked" />
                                                    <span class="slider round"></span><span class="keepme">توقيع نيابة عنة </span>
                                                </label>
                                            </div>
                                            <asp:LinkButton ID="LBCommittee_Members" runat="server" CssClass="btn btn-info" data-toggle="tooltip"
                                                title="إضافة العضو" ValidationGroup="VGCommittee_Members" OnClick="LBCommittee_Members_Click">
                                            إضافة العضو <span class="tip-bottom"><i class="fa fa-user" style="font-size:16px"></i></span></asp:LinkButton>
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
                        
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <div class="container-fluid" dir="rtl">
                                <%--<div class="form-group">
                                    <h5>إعتماد رئيس مجلس الإدارة : <span style="color: red">*</span>
                                    </h5>
                                    <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" ValidationGroup="GBill"></asp:TextBox>
                                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server"
                                        ControlToValidate="txtNote" ErrorMessage="* الإعتماد" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                        ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                </div>--%>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <h5>رئيس مجلس الإدارة : <span style="color: red">*</span>
                                        </h5>
                                        <asp:DropDownList ID="DLRaeesMaglesAlEdarah" runat="server" ValidationGroup="GBill" CssClass="form-control chzn-select dropdown">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator12" runat="server"
                                            ControlToValidate="DLRaeesMaglesAlEdarah" ErrorMessage="* رئيس مجلس الإدارة" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                                            ValidationGroup="GBill" Font-Size="10px" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group" align="left">
                                        <br />
                                        <asp:LinkButton ID="LBNew" runat="server"  ValidationGroup="GBill"  OnClick="LBNew_Click"
                                            class="btn btn-info">حفظ البيانات</asp:LinkButton>
                                        <asp:LinkButton ID="LB_Back" runat="server"  OnClick="LB_Back_Click"
                                            class="btn btn-danger">خروج</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="container-fluid" dir="rtl">
                                <div class="col-lg-5" runat="server" id="PnlAllow" visible="false">
                                    <div class="form-group">
                                        <h5>توقيع بدل كلاً من :
                                        </h5>
                                        <div class="keepmeLogged">
                                            <label class="switch" runat="server" id="IDRaeesAlmaglis" visible="false">
                                                رئيس المجلس
                                                <br />
                                                <input name="RememberMe" type="checkbox" id="CBRaeesAlmaglis" runat="server" />
                                                <span class="slider round"></span>
                                            </label>
                                            <i class="fa fa-minus"></i>
                                            <label class="switch" runat="server" id="IDAmeenAlsondoq" visible="false">
                                                المشرف المالي
                                                <br />
                                                <input name="RememberMe" type="checkbox" id="CBAmeenAlsondoq" runat="server" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <style type="text/css">
            .modal-open {
                overflow: hidden
            }

            .modal {
                position: fixed;
                top: 0;
                right: 0;
                bottom: 0;
                left: 0;
                z-index: 1050;
                display: none;
                overflow: hidden;
                -webkit-overflow-scrolling: touch;
                outline: 0;
                background-color: hsla(120, 3%, 82%, 0.30);
            }
        </style>
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
</asp:Content>

