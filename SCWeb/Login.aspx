<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SCWeb.Login" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <title>登录</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/Validform_v5.3.1.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/md5.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.cookie.js"></script>
    <style type="text/css">
        .Validform_checktip {
            display: block;
            line-height: 25px;
            font-size: 15px;
            color: #fff;
            text-indent: 45px;
        }

        .Validform_wrong {
            color: red;
        }

        .Validform_right {
            color: #91c954;
        }
    </style>
</head>
<body>
    <input type="hidden" id="hidPreUrl" runat="server" />
    <header id="login_header">
        <div class="center clearfix">
            <a class="logo fl">
                <img src="/images/logo.png" alt="">
            </a>
        </div>
    </header>
    <div id="content">
        <div class="login_img">
            <img src="/images/login_img.png" alt="">
        </div>
        <div class="center pr login">
            <div class="login_con pr">
                <img src="/images/border.png" alt="" style="position: absolute; left: 0; top: 0;">
                <h1>登录</h1>
                <div class="form">
                    <form id="loginform" name="loginform" runat="server">
                        <ul id="sortable1" class=" con">
                            <li class=" xian">
                                <span class="icon">
                                    <img src="/images/people.png" />
                                </span>
                                <input id="txt_loginName" type="text" class="kuang" placeholder="请输入用户名" datatype="*" nullmsg="请输入登录名！" />
                            </li>
                            <li class=" xian">
                                <span class="icon">
                                    <img src="/images/password.png" />
                                </span>
                                <input id="txt_passWord" type="password" class="kuang" placeholder="请输入密码" datatype="*" nullmsg="请输入密码" />
                            </li>
                            <li class="yzm xian">
                                <span class="icon">
                                    <img src="/images/yzm.png" />
                                </span>
                                <input type="hidden" id="hidCode" name="hidCode" />
                                <input id="inpCode" name="inpCode" type="text" class="kuang1" style="ime-mode: disabled" placeholder="请输入验证码" datatype="iCode" nullmsg="请输入验证码！" errormsg="验证码输入错误！" />
                                <span class="yzmtu" id="checkCode" onclick="createCode() "></span>
                            </li>
                            <li class="clearfix">
                                <div class="fl">
                                    <input type="checkbox" id="rem_paddword" />
                                    <label for="rem_paddword">记住密码</label>
                                </div>
                                <a href="javascript:void(0);" class="fr" id="forgetPwd">忘记密码？</a>
                            </li>
                            <li>
                                <input type="button" id="BtnLogin" name="BtnLogin" class="btn_btn" value="登录" />
                            </li>
                        </ul>
                        <div class="clear"></div>
                    </form>
                </div>
            </div>
        </div>
    </div>
    <div class="bottom">
        <div class="center clearfix">
            <span class="clearfix">
                <img src="/images/zixun.png" alt="" class="fl">
                <div class="description fl">
                    <h1>动态资讯</h1>
                    <div>
                        资讯一键同步
                        <br />
                        活动动态随时掌握
                    </div>
                </div>
            </span>
            <span class="clearfix">
                <img src="/images/gexing.png" alt="" class="fl">
                <div class="description fl">
                    <h1>社团个性化</h1>
                    <div>
                        社团秀秀秀<br />
                        展现不一样的你
                    </div>
                </div>
            </span>
            <span class="clearfix">
                <img src="/images/xiangce.png" alt="" class="fl">
                <div class="description fl">
                    <h1>活动相册</h1>
                    <div>
                        活动靓照封存<br />
                        留住你感动的瞬间
                    </div>
                </div>
            </span>
            <span class="clearfix">
                <img src="/images/renyuan.png" alt="" class="fl">
                <div class="description fl">
                    <h1>人员管理</h1>
                    <div>
                        支持批量管理学生<br />
                        一键导入，导出
                    </div>
                </div>
            </span>
        </div>
    </div>
    <div id="footer"></div>
    <script type="text/javascript">
        $(function () {
            $('#footer').load('/CommonPage/footer.html');
            var loadIndex = layer.load(1, {
                shade: [0.8, '#393D49'], //0.1透明度的白色背景
            });
            createCode();
            GetSysToken();
            //加载验证码

            //回车提交事件
            $("body").keydown(function () {
                if (event.keyCode == "13") {//keyCode=13是回车键
                    $("#BtnLogin").click();
                }
            });

            var valiNewForm = $("#loginform").Validform({
                datatype: {
                    "iCode": function (gets, obj, curform, regxp) {
                        /*参数gets是获取到的表单元素值，
                          obj为当前表单元素，
                          curform为当前验证的表单，
                          regxp为内置的一些正则表达式的引用。*/
                        var reg1 = regxp["*"];

                        var hidcode = curform.find("#hidCode");
                        if (reg1.test(gets)) { if (hidcode.val().toUpperCase() == gets.toUpperCase()) { return true; } }
                        return false;
                    }
                },
                ajaxPost: true,
                btnSubmit: "#BtnLogin",
                tiptype: 3,
                showAllError: false,
                beforeSubmit: function (curform) {
                    //在验证成功后，表单提交前执行的函数，curform参数是当前表单对象。
                    //这里明确return false的话表单将不会提交;	
                    Login();
                }
            })
        });
        function GetSysToken() {
            //var userInfo = "{\"Id\":7,\"UniqueNo\":\"啊发生\",\"UserType\":3,\"Name\":\"唐\",\"Nickname\":\"唐\",\"Sex\":1,\"Phone\":\"\",\"Birthday\":\"2016-09-29\",\"LoginName\":\"tang\",\"IDCard\":\"140481199805263255\",\"HeadPic\":\"\",\"RegisterOrg\":\"1001\",\"AuthenType\":0,\"Address\":\"\",\"Remarks\":\"\",\"CreateUID\":\"\",\"CreateTime\":\"2016-09-29 11:12:47\",\"EditUID\":null,\"EditTime\":null,\"IsEnable\":1,\"IsDelete\":0}";
            //$.cookie('TokenID', "e90bd89c594744c0b15d916a22b8ae92", { path: '/', secure: false });
            //$.cookie('LoginCookie_Author', userInfo, { path: '/', secure: false });
            if ($.cookie('TokenID') != null && $.cookie('TokenID') != "null" && $.cookie('TokenID') != "")
                GetUserInfoByToken($.cookie('TokenID'));
            else if ($.cookie('LoginCookie_Author') != null && $.cookie('LoginCookie_Author') != "null" && $.cookie('LoginCookie_Author') != "") {
                layer.closeAll('loading');
                var item = JSON.parse($.cookie('LoginCookie_Author'));
                if (item.LoginName != "") $("#txt_loginName").val(item.LoginName);
                if ($.cookie('RememberCookie_Cube') != null && $.cookie('RememberCookie_Cube') != "null" && $.cookie('RememberCookie_Cube') != "") {
                    if (item.Password != "") $("#txt_passWord").val($.cookie('RememberCookie_Cube'));
                    $("#rem_paddword").prop("checked", true);
                }
            }
            else
                layer.closeAll('loading');
        }


        var code; //在全局 定义验证码
        function createCode() {
            code = "";
            var codeLength = 4;//验证码的长度
            var checkCode = document.getElementById("checkCode");
            checkCode.innerHTML = "";
            var selectChar = new Array(1, 2, 3, 4, 5, 6, 7, 8, 9, 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z');

            for (var i = 0; i < codeLength; i++) {
                var charIndex = Math.floor(Math.random() * 60);
                code += selectChar[charIndex];
            }
            if (code.length != codeLength) {
                createCode();
            }
            checkCode.innerHTML = code;
            $("#hidCode").val(code);
            //$("#inpCode").val(code);
        }

        function Login() {
            var loginName = $("#txt_loginName").val();
            var passWord = $("#txt_passWord").val();
            layer.load(1, {
                shade: [0.8, '#393D49'], //0.1透明度的白色背景
            });

            /******************************统一认证登录*************************************/

            var postData = { Func: "Login", userName: loginName, password: hex_md5(passWord), returnUrl: window.location.href };
            $.ajax({
                type: "Post",
                url: '<%=TokenPath%>',
                data: postData,
                dataType: "jsonp",
                jsonp: "jsoncallback",
                success: function (returnVal) {
                    var result = returnVal.result;
                    GetUserInfoByToken(result);

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.closeAll('loading');
                    //console.log(errorThrown);
                }
            });

            /*
            $.ajax({
                url: "/CommonHandler/UnifiedHelpHandler.ashx",
                async: false,
                type: "Post",
                dataType: "json",
                data: { "Func": "Login", "loginName": loginName, "passWord": passWord },
                success: OnSuccessLogin,
                error: OnErrorLogin

            });
            return false;*/
        }

        function GetUserInfoByToken(tokenID) {
            if (tokenID != "") {
                var postData = { Func: "GetUserInfoByToken", tokenID: tokenID, returnUrl: window.location.href };
                $.ajax({
                    type: "Post",
                    url: '<%=TokenPath%>',
                    data: postData,
                    dataType: "jsonp",
                    jsonp: "jsoncallback",
                    success: function (returnVal) {
                        var flg = returnVal.result;
                        if (flg != null) {
                            if (flg.errNum == 0) {
                                var item = flg.retData;
                                $.cookie('TokenID', tokenID, { path: '/', secure: false });
                                if (item.CreateTime != null) item.CreateTime = DateTimeConvert(item.CreateTime);
                                if (item.EditTime != null) item.EditTime = DateTimeConvert(item.EditTime);
                                if (item.Birthday != null) item.Birthday = DateTimeConvert(item.Birthday, true);
                                $.cookie('LoginCookie_Author', encodeURIComponent(JSON.stringify(item)), { path: '/', secure: false });
                                if ($("#rem_paddword").is(":checked")) $.cookie('RememberCookie_Cube', $("#txt_passWord").val(), { path: '/', secure: false });
                                if ($("#hidPreUrl").val() != "" && ($("#hidPreUrl").val().toLocaleLowerCase().indexOf("login.aspx") < -1 || $("#hidPreUrl").val().toLocaleLowerCase().indexOf("register.aspx") < -1)) window.location = $("#hidPreUrl").val();
                                else window.location.href = "/DeskManage/Index.aspx";
                            } else {
                                layer.msg(flg.errMsg);
                            }
                        }
                        layer.closeAll('loading');
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        layer.closeAll('loading');
                        //console.log(errorThrown);
                    }
                });
            }
        }
        function OnSuccessLogin(json) {
            layer.closeAll('loading');
            var cookie = json.result;
            if (cookie.errNum == "0") {
                var str = cookie.retData[0];
                if (str != "" && str.length > 0) {
                    var items = JSON.parse(cookie.retData[0]);
                    if (items != null && items.data.length > 0) {
                        var item = items.data;
                        $.cookie('LoginCookie_Author', encodeURIComponent(JSON.stringify(item[0])), { path: '/', secure: false });
                        if ($("#rem_paddword").is(":checked")) $.cookie('RememberCookie_Cube', $("#txt_passWord").val(), { path: '/', secure: false });
                        if ($("#hidPreUrl").val() != "" && ($("#hidPreUrl").val().toLocaleLowerCase().indexOf("login.aspx") < -1 || $("#hidPreUrl").val().toLocaleLowerCase().indexOf("register.aspx") < -1)) window.location = $("#hidPreUrl").val();
                        else window.location.href = "/StuAssociate/AssoIndex.aspx";
                        return;
                    }
                }
                layer.msg("用户名或密码错误！");

            } else if (cookie.errNum == "333") {
                layer.msg("帐号已被禁用请联系管理员！");
            } else if (cookie.errNum == "444") {
                layer.msg("帐号已被删除请重新注册！");
            } else if (cookie.errNum == "999") {
                layer.msg("用户名或密码错误！");
            }
            else {
                layer.msg(json.result.errMsg + "！");
            }
        }
        function OnErrorLogin(XMLHttpRequest, textStatus, errorThrown) {
            layer.msg("登录名或密码错误！" + errorThrown);
        }
    </script>
</body>
</html>
