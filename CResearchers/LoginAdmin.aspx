<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginAdmin.aspx.cs" Inherits="CResearchers_LoginAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>تسجيل دخول موظف</title>
    <link href="GridView.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="test/Login.css" rel="stylesheet" />
    <script src="test/jquery-1.10.2.js"></script>
    <link href="../fonts/font-awesome.css" rel="stylesheet" />
    <style type="text/css">
        body, h1, h2, h3, h4, h5, h6, p, ul, a, input, span {
            font-family: Alwatan;
        }

        body, h1, h2, h3, h4, h5, h6, p, ul, a, input, span {
            font-family: 'Droid Arabic Kufi', serif;
        }
    </style>
    <link type="text/css" href="../view/stylesheet/stylesheet-a.css" rel="stylesheet" media="screen" />
    <script src="../view/javascript/jquery.min.js"></script>
    <script src="../view/javascript/ShowProgressOnLoad.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="continer-Login">
            <header>
                <a href="../" class="w3-bar-item w3-button tablink">شاشة للموقع</a>
            </header>
            <div style=" border-radius: 6px; margin-top:60px">
                <h2 class="Title-Login">
                    <span>الدخول الى</span>  لوحة تحكم <span>موظف</span>
                </h2>
                <div id="StudentLogin" class="w3-container city w3-animate-right">
                    <div class="continerGate">
                        <div id="uxGetLoginName" align="center">
                            <i class="fa fa-sign-in" style="font-size:70px; color:#cccccc"></i>
                        </div>
                        <div class="form-group">
                            <div id="uxdivErrorMessage" class="" style="background-color: #a58c03; padding: 5px; border-radius: 6px" runat="server" visible="false">
                                <asp:Label ID="lbmsg" runat="server" Font-Size="Medium" ForeColor="#F0F0F0"></asp:Label>
                            </div>
                            <div id="uxSystemLoginForm" class="w3-animate-right" style="display: inline-block">
                                <asp:TextBox ID="txtUserAdmin" runat="server" class="boxUserName" placeholder=" إسم المستخدم أو السجل المدني ..." ValidationGroup="G3"></asp:TextBox>
                                <asp:TextBox ID="txtPassAdmin" runat="server" class="boxUserName" placeholder=" كلمة المرور ... " type="password" ValidationGroup="G3"></asp:TextBox>
                                <div style="margin-right: 70px">
                                    <img src="../captcha.aspx" height="35" width="100" />
                                </div>
                                <asp:TextBox ID="txtCapatshaAdmin" runat="server" class="boxPassword" placeholder="رمز التحقق ..." autocomplete="off"></asp:TextBox>
                                <asp:Button ID="btnLoginAdmin" runat="server" class="LoginBotton" ValidationGroup="G3" OnClick="btnLoginAdmin_Click" style="cursor: pointer;" />
                                <div class="keepmeLogged">
                                    <label class="switch">
                                        <input name="RememberMe" type="checkbox" id="CBRememberMe" runat="server" checked="checked" />
                                        <span class="slider round"></span>
                                        <span class="keepme">تذكرني </span>
                                    </label>
                                </div>
                                <a class="ForGotPassword" href="#">نسيت كلمة المرور؟</a>
                            </div>
                            <div id="uxForgetPassword" class="w3-animate-left" style="display: none;">
                                <asp:TextBox ID="txtRest" runat="server" class="boxPassword" placeholder="إسم المستخدم أو البريد ..."></asp:TextBox>
                                <asp:Button ID="btnRest" runat="server" class="LoginBotton" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="loading2" align="center" id="lodi">
            <div>
                <img src="../Img/Logo.png" width="200" style="background-color: #017893; padding: 5px; border-radius: 4px" />
                <br />
                <span style="background-color: #017893; padding: 5px; border-radius: 4px">يرجى الإنتظار , جاري تنفيذ المهام ...</span><br />
                <br />
                <img src="loader.gif" alt="" />
            </div>
        </div>

        <script type="text/javascript">
            //<![CDATA[
            WebForm_AutoFocus('LinkButtonLogin');//]]>
        </script>
    </form>
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
