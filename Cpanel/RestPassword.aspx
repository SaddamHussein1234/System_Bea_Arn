<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RestPassword.aspx.cs" Inherits="Cpanel_RestPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="test/Login.css" rel="stylesheet" />
    <script src="test/jquery-1.10.2.js"></script>
    <link href="../fonts/font-awesome.css" rel="stylesheet" />
    <style type="text/css">
        body , h1 , h2 , h3 ,h4 ,h5 ,h6 ,p  ,ul ,a , input{font-family: 'Droid Arabic Kufi', serif;}
    </style>
    <link type="text/css" href="../view/stylesheet/stylesheet-a.css?v=2.2" rel="stylesheet" media="screen" />
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="continer-Login">
            <header>
                <a href="../" class="w3-bar-item w3-button tablink">شاشة للموقع</a>
            </header>
            <div style="background-color: rgba(216, 217, 217, 0.50); border-radius:6px">
                <h2 class="Title-Login">
                    <span>الدخول الى</span>  لوحة التحكم
                </h2>
                <div id="StudentLogin" class="w3-container city w3-animate-right">
                    <div class="continerGate">
                        <div id="uxGetLoginName" class="BoxLogin"></div>
                        <div class="form-group">
                            <div id="uxdivErrorMessage" class="">
                                <asp:Label ID="lbmsg" runat="server"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage=" إسم المستخدم" ControlToValidate="txtRest" ValidationGroup="G3" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div id="uxForgetPassword" class="w3-animate-left">
                                <asp:TextBox ID="txtRest" runat="server" class="boxUserName" placeholder="إسم المستخدم أو البريد ..."></asp:TextBox>
                                <div style="margin-right:70px">
                                    <img src="../captcha.aspx" height="35px" width="100px" />
                                </div>
                                <asp:TextBox ID="txtCapatsha" runat="server" class="boxPassword" placeholder="رمز التحقق ..."></asp:TextBox>
                                <asp:Button ID="btnRest" runat="server" class="LoginBotton" OnClick="btnRest_Click" />
                                <a class="ForGotPassword" href="Log.aspx">رجوع لشاشة الدخول</a>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
            </div>
        </div>
        <div class="loading2" align="center" id="lodi">
            <div>
                <img src="../Img/Logo.png" width="200" style="background-color: #349301; padding: 5px; border-radius: 4px" />
                <br />
                <span style="background-color: #349301; padding: 5px; border-radius: 4px">يرجى الإنتظار , جاري تنفيذ المهام</span><br />
                <br />
                <img src="loader.gif" alt="" />
            </div>
        </div>

    <script type="text/javascript">
            //<![CDATA[
            WebForm_AutoFocus('LinkButtonLogin');//]]>
        </script>
    </form>
    <!-- Js -->
    <script>
        $(document).ready(function () {
            $("#forGotPassword").click(function () {
                if ($("#hdnUserType").val().toLocaleLowerCase().toString() == "student") {
                    $("#txtStudentNumber").removeAttr("style");
                } else {
                    $("#txtStudentNumber").attr("style", "display:none");
                }
                $("#uxSystemLoginForm").attr("style", "display:none");
                $("#uxForgetPassword").attr("style", "display:block");
            });
        });

        document.getElementById("uxLoginBox").click();

        if (getParameterByName("ReturnUrl") != "") {
            // $("#uxdivErrorMessage,#uxdivinfoMessage").removeAttr("style");
            var loginParam1 = getParameterByName("ReturnUrl").toString().toLocaleLowerCase();

            var loginName = "";
            if (loginParam1.includes("student"))
                document.getElementById("loginID_2").click();
            else if (loginParam1.includes("teacher"))
                document.getElementById("loginID_3").click();
            else if (loginParam1.includes("parent"))
                document.getElementById("loginID_1").click();
            else if (loginParam1.includes("supervisor"))
                document.getElementById("loginID_4").click();
            else if (loginParam1.includes("employee"))
                document.getElementById("loginID_6").click();
            else if (loginParam1.includes("manager"))
                document.getElementById("loginID_7").click();
        }
        function openLink(evt, animName, loginName, resetPasswordType) {

            if ($("#uxForgetPassword").css("display") == "block") {
                $("#uxSystemLoginForm").removeAttr("style");
                $("#uxForgetPassword").attr("style", "display:none");
            } else {
                if (animName == "StudentLogin") {
                    $('#hdnSystemName').val("/" + resetPasswordType + "/");
                    $("#uxGetLoginName").html("<p><strong>" + loginName + "</strong></p>");

                    $("#hdnUserType").val(resetPasswordType);
                    $('#uxLoginBox').css('display', 'block');
                    $('#uxHomeLink').css('display', 'none');
                    if (resetPasswordType == "parent" && $('#hdnBKParent').val() != "")
                        $('#loginBackground').css('background-image', '' + $('#hdnBKParent').val() + '');
                    else if (resetPasswordType == "employee" && $('#hdnBKEmployee').val() != "")
                        $('#loginBackground').css('background-image', '' + $('#hdnBKEmployee').val() + '');

                    var _h = document.documentElement.clientHeight;
                    $('#loginBackground').css('height', '' + _h + 'px');
                }
                else {
                    $('#uxHomeLink').css('display', 'block');
                    $('#uxLoginBox').css('display', 'none');
                    $('#loginBackground').removeAttr("style");
                }
                var i, x, tablinks;
                x = document.getElementsByClassName("city");
                for (i = 0; i < x.length; i++) {
                    x[i].style.display = "none";
                }
                tablinks = document.getElementsByClassName("tablink");
                for (i = 0; i < x.length; i++) {
                    tablinks[i].className = tablinks[i].className.replace(" w3-red", "");
                }
                document.getElementById(animName).style.display = "block";
                if (evt != null)
                    evt.currentTarget.className += " w3-red";
            }
        }
        var baseUrl = "/";
        function ResolveUrl(url) {
            if (url.indexOf("~/") == 0) {
                url = baseUrl + url.substring(2);
            }
            return url;
        }
    </script>
</body>
</html>
