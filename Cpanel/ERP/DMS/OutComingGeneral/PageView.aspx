<%@ Page Title="" Language="C#" MasterPageFile="~/Cpanel/ERP/DMS/MPCPanel.master" AutoEventWireup="true" CodeFile="PageView.aspx.cs" Inherits="Cpanel_ERP_DMS_OutComingGeneral_PageView" %>
<%@ Register Src="~/WUCHeader.ascx" TagPrefix="uc1" TagName="WUCHeader" %>
<%@ Register Src="~/WUCFooterNew.ascx" TagPrefix="uc1" TagName="WUCFooterNew" %>
<%@ Register Src="~/WUCHeaderInstitute.ascx" TagPrefix="uc1" TagName="WUCHeaderInstitute" %>
<%@ Register Src="~/WUCFooterInstitute.ascx" TagPrefix="uc1" TagName="WUCFooterInstitute" %>

<%@ Import Namespace="Library_CLS_Arn.Saddam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .StyleTD {
            text-align: center;
            padding: 5px;
            border: double;
            border-width: 2px;
            border-color: #a1a0a0;
        }

        @media screen and (min-width: 768px) {
            .WidthTex {
                float: right;
                Width: 13%;
                padding-right: 5px;
            }

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

            .WidthText30 {
                float: right;
                Width: 16%;
                padding-right: 5px;
            }

            .WidthText2 {
                float: right;
                Width: 32%;
                padding-left: 5px;
            }

            .WidthText1 {
                float: right;
                Width: 24%;
                padding-right: 5px;
            }

            .WidthText4 {
                float: right;
                Width: 50%;
            }

            .Width10Percint {
                float: right;
                Width: 10%;
                padding-right: 5px;
            }

            .WidthText5 {
                float: right;
                Width: 100%;
            }


            .WidthText20 {
                Width: 150px;
                height: 36px;
            }
        }

        @media screen and (max-width: 767px) {
            .WidthTex {
                Width: 95%;
            }

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

            .Width10Percint {
                Width: 95%;
            }

            .WidthText30 {
                Width: 95%;
            }

            .WidthText4 {
                Width: 95%;
            }

            .WidthText5 {
                Width: 95%;
            }

            .WidthText20 {
                Width: 100px;
                height: 36px;
            }
        }

        .MarginBottom {
            margin-top: 15px;
        }
    </style>

    <link href="<%=ResolveUrl("~/Cpanel/css/chosen.css")%>" rel="stylesheet" />
    <script src="<%=ResolveUrl("~/view/javascript/jquery.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/view/javascript/ShowProgressOnLoad.js")%>"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">
        <div class="page-header">
            <div class="container-fluid">
                <div class="pull-right">
                    <a runat="server" id="ID_Edit_" class="btn btn-info" visible="false" data-toggle="tooltip" title="تعديل الخطاب">الذهاب إلى وضع التعديل <span class="fa fa-edit"></span></a>
                        &nbsp;
                    السنة : 
                    <asp:DropDownList ID="ddlYears" runat="server" CssClass="form-control2"
                        Width="100" ValidationGroup="GDetails">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtSearch" runat="server" Width="100" Height="30" placeholder=" رقم الخطاب ... "></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="بحث" Style="margin-right: 4px;" data-toggle="tooltip" title="بحث"
                        class="btn btn-info" OnClick="btnSearch_Click" />
                    <asp:DropDownList ID="DLPrint" runat="server" CssClass="form-control2"
                        Width="100" ValidationGroup="VGPrint">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem Value="OutCome">الخطاب فقط</asp:ListItem>
                        <asp:ListItem Value="OutCome_Khatm">الخطاب+الختم والتوقيع</asp:ListItem>
                        <asp:ListItem Value="OutCome_Kalesh">الخطاب+الترويسة</asp:ListItem>
                        <asp:ListItem Value="OutCome_Khatm_Kalesh" Selected="True">الخطاب+الختم والتوقيع+الترويسة</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator10" runat="server" Display="Dynamic"
                        ControlToValidate="DLPrint" ErrorMessage="*" ForeColor="#FF0066" meta:resourcekey="RequiredFieldValidator1Resource1"
                        ValidationGroup="VGPrint" Font-Size="10px"></asp:RequiredFieldValidator>
                    <asp:LinkButton ID="btnPrint" runat="server" class="btn btn-success" data-toggle="tooltip" OnClick="btnPrint_Click"
                        title="طباعة" ValidationGroup="VGPrint">
                        <i class="fa fa-print"></i></asp:LinkButton>
                    <asp:LinkButton ID="LBExit" runat="server" data-toggle="tooltip" title="رجوع" OnClick="LBExit_Click"
                        class="btn btn-default"> <i class="fa fa-reply"></i></asp:LinkButton>
                </div>
                <h1>لوحة التحكم</h1>
                <ul class="breadcrumb">
                    <li><a href="/Cpanel/ERP/DMS/">الرئيسية</a></li>
                    <li><a href="PageAll.aspx">ملفات الصادر</a></li>
                    <li><a href="">تفاصيل الخطاب</a></li>
                </ul>
            </div>
        </div>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <i class="fa fa-list"></i>
                        تفاصيل الخطاب
                    </h3>
                </div>
                <div class="panel-body">
                    <div class="content-box-large">
                        <div class="widget-box">
                            <style>
                                .mydiv {
                                    z-index: 9;
                                          left:30px;
                                          top:-130px;
                                    }
                            </style>
                            <asp:Panel ID="pnlPrint" runat="server" Direction="RightToLeft" Visible="false">
                                <asp:Panel ID="pnl2" runat="server" Direction="RightToLeft">  
                                    <style>
                                        #mydiv {
                                          position: absolute; width:200px; z-index: 9; left:30px; top:130px; background-color: #027a1e; text-align: center; border: 1px solid #d3d3d3; font-size:8px;
                                        }

                                        #mydivheader {
                                          padding: 3px; cursor: move; z-index: 10; color: #fff;
                                        }
                                        .XBody
                                        {
                                            background:#fff; margin:5px; padding:5px; border-radius:7px;
                                        }

                                    </style>

                                    <div id="IDBody" runat="server">
                                        <table style="width: 95%; background-color: #ffffff; color: #393939" align="center">
                                        <thead>
                                            <tr>
                                                <th colspan="2">
                                                    <div class="hide" id="headerDMS">
                                                        <uc1:WUCHeader runat="server" ID="wucHeaderAssociation" Visible="false" />
                                                        <uc1:WUCHeaderInstitute runat="server" ID="wucHeaderInstitute" Visible="false" />
                                                    </div>
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <th colspan="2">
                                                    <div id="Kelesha" runat="server" visible="true" align="left">
                                                        <div id="mydiv" class="mydiv">
                                                            <div id="mydivheader"><asp:Label ID="lblSender" runat="server" Font-Size="8px"></asp:Label></div>
                                                            <div class="XBody" align="right">
                                                                <b>
                                                                    <span>رقم الصادر : <asp:Label ID="lblNumber_File" runat="server" Font-Size="8px"></asp:Label></span><br />
                                                                    <span>التاريخ : <asp:Label ID="lblDate_Transaction" runat="server" Font-Size="8px"></asp:Label></span><br />
                                                                    <span>المرفقات : <asp:Label ID="lblTitle_Attachments" runat="server" Font-Size="8px"></asp:Label></span><br />
                                                                    <span>الموضوع : <asp:Label ID="lblTitle" runat="server" Font-Size="8px"></asp:Label></span>
                                                                </b>
                                                            </div>
                                                            <script>
                                                                //Make the DIV element draggagle:
                                                                dragElement(document.getElementById("mydiv"));

                                                                function dragElement(elmnt) {
                                                                    var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;
                                                                    if (document.getElementById(elmnt.id + "header")) {
                                                                        /* if present, the header is where you move the DIV from:*/
                                                                        document.getElementById(elmnt.id + "header").onmousedown = dragMouseDown;
                                                                    } else {
                                                                        /* otherwise, move the DIV from anywhere inside the DIV:*/
                                                                        elmnt.onmousedown = dragMouseDown;
                                                                    }

                                                                    function dragMouseDown(e) {
                                                                        e = e || window.event;
                                                                        e.preventDefault();
                                                                        // get the mouse cursor position at startup:
                                                                        pos3 = e.clientX;
                                                                        pos4 = e.clientY;
                                                                        document.onmouseup = closeDragElement;
                                                                        // call a function whenever the cursor moves:
                                                                        document.onmousemove = elementDrag;
                                                                    }

                                                                    function elementDrag(e) {
                                                                        e = e || window.event;
                                                                        e.preventDefault();
                                                                        // calculate the new cursor position:
                                                                        pos1 = pos3 - e.clientX;
                                                                        pos2 = pos4 - e.clientY;
                                                                        pos3 = e.clientX;
                                                                        pos4 = e.clientY;
                                                                        // set the element's new position:
                                                                        elmnt.style.top = (elmnt.offsetTop - pos2) + "px";
                                                                        elmnt.style.left = (elmnt.offsetLeft - pos1) + "px";
                                                                    }

                                                                    function closeDragElement() {
                                                                        /* stop moving when mouse button is released:*/
                                                                        document.onmouseup = null;
                                                                        document.onmousemove = null;
                                                                    }
                                                                }
                                                            </script>
                                                        </div>
                                                    </div>
                                                </th>
                                            </tr>
                                            <tr>
                                                <th colspan="2">
                                                    <div align="center" style="margin-top:20px;">
                                                        <a href='javaScript:void(0)' data-toggle='modal' data-target='#IDQRCode' title='تكبير'>
                                                            <asp:Image ID="ImgQRCode" runat="server" alt='QR Code' />
                                                        </a>
                                                        <div id="IDQRCode" class="modal fade in modal_New_Style HideThis">
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
                                                                                        <i class="fa fa-star"></i> QR Code : 
                                                                                    </label><br />
                                                                                    <div align="center">
                                                                                        <asp:Image ID="ImgQRCode2" runat="server" alt='صورة QRCode' style="width:300px; height:300px;" />
                                                                                    </div>
                                                                                    <div class='clearfix'></div>
                                                                                </div>
                                                                                <div class="modal-footer">
                                                                                    <button type="button" class="btn btn-default" data-dismiss="modal">اغلاق</button>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td colspan='2'>
                                                    <br />
                                                </td>
                                            </tr>
                                            <span id="IDDetails" runat="server"></span>
                                            <tr runat="server" id="IDTable" visible="false" class="HideThis">
                                                <td colspan="2">
                                                    <hr style='border: solid; border-width: 1px; width: 100%' />
                                                    <b>المرفقات : </b><br />
                                                    <table class='table table-bordered table-condensed' style="width: 100%" aria-multiline="true">
                                                        <thead>
                                                            <tr class="th">
                                                                <th class="StyleTD">م</th>
                                                                <th class="StyleTD">عنوان الملف</th>
                                                                <th class="StyleTD">نوع الملف</th>
                                                                <th class="StyleTD">حجم الملف</th>
                                                                <th class="StyleTD">بتاريخ</th>
                                                                <th class="StyleTD">العرض</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="RPTFiles" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td style="width: 10px;" class="StyleTD">
                                                                            <span style="margin-right: 5px; font-size: 11px"><%# Container.ItemIndex + 1 %></span>
                                                                        </td>
                                                                        <td class="StyleTD">
                                                                            <span style="font-size: 12px"><%# Eval("_The_Title_")%></span>
                                                                        </td>
                                                                        <td class="StyleTD">
                                                                            <span style="font-size: 12px"><%# Eval("_Type_File_").ToString().Replace(".", "") %></span>
                                                                        </td>
                                                                        <td class="StyleTD">
                                                                            <span style="font-size: 12px"><%# ClassSaddam.FormatSize(Convert.ToInt32(Eval("_Size_File_"))) %></span>
                                                                        </td>
                                                                        <th class="StyleTD th">
                                                                            <span style="font-size: 12px"><%# Eval("_CreatedDate_", "{0:MM/dd/yyyy}") %></span>
                                                                        </th>
                                                                        <td class="StyleTD">
                                                                            <a class="btn btn-default btn-sm download_button" style="<%# ClassSaddam.FCheckNullFile(Eval("_Src_").ToString()) %>"
                                                                                href="/<%# Eval("_Src_") %>" data-file="pdf" data-fancybox data-type="iframe" title="عرض الملف" data-toggle="tooltip">
                                                                                <i class="fas fa-file-pdf"></i>
                                                                                <div>
                                                                                    <span>عرض الملف </span><small><%# ClassSaddam.FGetTypeFileOutTitle((string)Eval("_Type_File_")) %> </small>
                                                                                </div>
                                                                            </a>
                                                                            <a class="btn btn-default btn-sm download_button" style="<%# ClassSaddam.FCheckNotNullFile(Eval("_Src_").ToString()) %>" data-file="pdf">
                                                                                <i class="fas fa-file-pdf"></i>
                                                                                <div><span>بدون ملف مرفق</span></div>
                                                                            </a>
                                                                        </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th colspan="2">
                                                    <br />
                                                    <div style="margin-bottom:20px;">
                                                        <div runat="server" id="ID_General_Director" visible="false" class="WidthText1" align="center">
                                                            <span style="font-family: 'Alwatan'; font-size: 18px;">مدير الجمعية</span>
                                                            <br />
                                                            <asp:Image ID="Img_General_Director" runat="server" Width='100px' Height='25' />
                                                            <br />
                                                            <asp:Label ID="lbl_General_Director" runat="server"></asp:Label>
                                                        </div>
                                                        <div runat="server" id="ID_Director_Of_Personnel" visible="false" class="WidthText1" align="center">
                                                            <span style="font-family: 'Alwatan'; font-size: 18px;">مدير شؤون الموظفين</span>
                                                            <br />
                                                            <asp:Image ID="Img_Director_Of_Personnel" runat="server" Width='100px' Height='25' />
                                                            <br />
                                                            <asp:Label ID="lbl_Director_Of_Personnel" runat="server"></asp:Label>
                                                        </div>
                                                        <div runat="server" id="ID_Cashier" visible="false" class="WidthText1" align="center">
                                                            <span style="font-family: 'Alwatan'; font-size: 18px;">المشرف المالي</span>
                                                            <br />
                                                            <asp:Image ID="Img_Cashier" runat="server" Width='100px' Height='25' />
                                                            <br />
                                                            <asp:Label ID="lbl_Cashier" runat="server"></asp:Label>
                                                        </div>
                                                        <div runat="server" id="ID_Secretary_General" visible="false" class="WidthText1" align="center">
                                                            <span style="font-family: 'Alwatan'; font-size: 18px;">الأمين العام</span>
                                                            <br />
                                                            <asp:Image ID="Img_Secretary_General" runat="server" Width='100px' Height='25' />
                                                            <br />
                                                            <asp:Label ID="lbl_Secretary_General" runat="server"></asp:Label>
                                                        </div>
                                                        <div runat="server" id="ID_Deputy_Chairman_Of_The_Board" visible="false" class="WidthText1" align="center">
                                                            <span style="font-family: 'Alwatan'; font-size: 18px;">نائب رئيس مجلس الإدارة</span>
                                                            <br />
                                                            <asp:Image ID="Img_Deputy_Chairman_Of_The_Board" runat="server" Width='100px' Height='25' />
                                                            <br />
                                                            <asp:Label ID="lbl_Deputy_Chairman_Of_The_Board" runat="server"></asp:Label>
                                                        </div>
                                                        <div runat="server" id="ID_Chairman_Of_Board_Of_Directors" visible="false" class="WidthText1" align="center" style="position:absolute; left:10px;">
                                                            <span style="font-family: 'Alwatan'; font-size: 18px;">رئيس مجلس الإدارة</span>
                                                            <br />
                                                            <asp:Image ID="Img_Chairman_Of_Board_Of_Directors" runat="server" Width='100px' Height='25' />
                                                            <br />
                                                            <asp:Label ID="lbl_Chairman_Of_Board_Of_Directors" runat="server"></asp:Label>
                                                        </div>
                                                        <div runat="server" id="IDKhatm" align="right" style="margin-top: -20px" visible="true">
                                                            <img src="<%=ResolveUrl("~/ImgSystem/ImgSignature/الختم.png")%>" />
                                                        </div>
                                                    </div>
                                                    <%--<hr style='border: solid; border-width: 1px; width: 100%' />--%>
                                                </th>
                                            </tr>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <th colspan="2">
                                                    <div class="hide" id="footerDMS">
                                                        <footer>
                                                            <uc1:WUCFooterNew runat="server" ID="wucFooterNewAssociation" Visible="false" />
                                                            <uc1:WUCFooterInstitute runat="server" ID="wucFooterInstitute" Visible="false" />
                                                        </footer>
                                                    </div>
                                                </th>
                                            </tr>
                                        </tfoot>
                                    </table>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel ID="pnlSelect" runat="server" Direction="RightToLeft" Visible="false">
            <div class="container-fluid">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            <i class="fa fa-pencil"></i>
                            <asp:Label ID="Label6" runat="server" Text="يرجى إدخال رقم سجل صحيح"></asp:Label>
                        </h3>
                    </div>
                    <div class="panel-body">
                        <div class="content-box-large">
                            <div class="widget-box">
                                <div class="container-fluid" dir="rtl">
                                    <asp:Panel ID="Panel1" runat="server">
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
            </div>
        </asp:Panel>
        <script src="<%=ResolveUrl("~/Cpanel/css/chosen.jquery.js")%>" type="text/javascript"></script>
        <script type="text/javascript"> $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true }); </script>
        <link href="../jquery.fancybox.min.css" rel="stylesheet" />
        <script src="../jquery.fancybox.min.js"></script>
        <asp:HiddenField ID="HFIDStore" runat="server" />
</asp:Content>

