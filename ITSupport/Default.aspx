<%@ Page Title="" Language="C#" MasterPageFile="~/ITSupport/MPITSupport.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ITSupport_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        $(function () {
            $("#prod_nav ul").tabs("#panes > div", {
                effect: 'fade',
                fadeOutSpeed: 400
            });
        });
    </script>
    <script>
        $(document).ready(function () {
            $(".pane-list li").click(function () {
                window.location = $(this).find("a").attr("href");
                return false;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="container">
        <!-- tab panes -->
        <div id="prod_wrapper">
            
            <div align="right">
                <h4> : قائمة الدعم الفني * </h4>
                <br />
            </div>
            <!-- navigator -->
            <div id="prod_nav">
                <ul>
                    <li style="float:right"><a href="HRM/" class="AClick">
                            <img src="img/demo/1.jpg" class="WithImg" alt=""><span style="font-size:16px;">نظام الموارد البشرية</span> </a></li>

                    <li style="float:right; margin-bottom:50px;"><a href='CRM/' class="AClick">
                            <img src="img/demo/1.jpg" class="WithImg" alt=""><span style="font-size:16px;">نظام متابعة الداعمين</span> </a></li>
                </ul>
            </div>
            <!-- END navigator -->
        </div>
        <!-- END prod wrapper -->
    </div>
    <!-- END container -->
</asp:Content>

