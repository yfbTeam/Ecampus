<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeskModel.aspx.cs" Inherits="SCWeb.DeskManage.DeskModel" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>我的桌面</title>
    <link rel="stylesheet" type="text/css" href="css/reset.css" />
    <link rel="stylesheet" type="text/css" href="/css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/layer/layer.js"></script>
    <script src="Scripts/common.js"></script>
</head>
<body style="background: #fff;">
    <input type="hidden" id="IDCard" value="<%=UserInfo.UniqueNo %>" />
    <div class="pr addSystemAppa">
        <div class="addSystemApp">
            <h1>可添加模块</h1>
            <ul class="clearfix showApp">
                <%--<li>
                    <a class="sushe">
                        <i class="iconfont icon-sushe"></i>
                        <p>学生宿舍</p>
                    </a>
                    <div class="addremove">
                        <img src="images/add.jpg" />
                    </div>
                </li>
                <li>
                    <a class="xiaoben">
                        <i class="iconfont icon-xiaoben"></i>
                        <p>校本资源库</p>
                    </a>
                    <div class="addremove">
                        <img src="images/add.jpg" />
                    </div>
                </li>
                <li>
                    <a class="wangpan">
                        <i class="iconfont icon-wangpan"></i>
                        <p>个人网盘</p>
                    </a>
                    <div class="addremove">
                        <img src="images/add.jpg" />
                    </div>
                </li>
                <li>
                    <a class="shetuan">
                        <i class="iconfont icon-shetuan"></i>
                        <p>学生社团</p>
                    </a>
                    <div class="addremove">
                        <img src="images/add.jpg" />
                    </div>
                </li>
                <li>
                    <a class="huodong">
                        <i class="iconfont icon-huodong"></i>
                        <p>学生活动</p>
                    </a>
                    <div class="addremove">
                        <img src="images/add.jpg" />
                    </div>
                </li>
                <li>
                    <a class="chengzhang">
                        <i class="iconfont icon-chengzhang"></i>
                        <p>学生成长</p>
                    </a>
                    <div class="addremove">
                        <img src="images/add.jpg" />
                    </div>
                </li>--%>
            </ul>
            <h1>已有模块</h1>
            <ul class="clearfix hideApp">
                <%--<li>
                    <a class="sushe">
                        <i class="iconfont icon-sushe"></i>
                        <p>学生宿舍</p>
                    </a>
                    <div class="addremove">
                        <img src="images/remove.jpg" />
                    </div>
                </li>
                <li>
                    <a class="xiaoben">
                        <i class="iconfont icon-xiaoben"></i>
                        <p>校本资源库</p>
                    </a>
                    <div class="addremove">
                        <img src="images/remove.jpg" />
                    </div>
                </li>
                <li>
                    <a class="wangpan">
                        <i class="iconfont icon-wangpan"></i>
                        <p>个人网盘</p>
                    </a>
                    <div class="addremove">
                        <img src="images/remove.jpg" />
                    </div>
                </li>
                <li>
                    <a class="shetuan">
                        <i class="iconfont icon-shetuan"></i>
                        <p>学生社团</p>
                    </a>
                    <div class="addremove">
                        <img src="images/remove.jpg" />
                    </div>
                </li>
                <li>
                    <a class="huodong">
                        <i class="iconfont icon-huodong"></i>
                        <p>学生活动</p>
                    </a>
                    <div class="addremove">
                        <img src="images/remove.jpg" />
                    </div>
                </li>
                <li>
                    <a class="chengzhang">
                        <i class="iconfont icon-chengzhang"></i>
                        <p>学生成长</p>
                    </a>
                    <div class="addremove">
                        <img src="images/remove.jpg" />
                    </div>
                </li>
                <li>
                    <a class="xuanke">
                        <i class="iconfont icon-xuanke"></i>
                        <p>选课系统</p>
                    </a>
                    <div class="addremove">
                        <img src="images/remove.jpg" />
                    </div>
                </li>--%>
            </ul>
        </div>
    </div>
    <div class="preserved">
        <input type="button" class="cancel fr" value="取消" onclick="cancelIframe()" />
        <input type="button" class="keep fr" value="保存" onclick="keepIframe()" />
    </div>
    <script>
        $(function () {
            NewModel();
            HasMode();
        })
        function NewModel() {
            $("#main").html("");
            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/DeskManage/AppHandler.ashx", "IDCard": $("#IDCard").val(), "Func": "GetModelList", Ispage: "false", IsShow: "not in","Pid":"0"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            var html = '<li id=' + this.ID + '><a style="background:' + this.ModelCss + '"><i class="iconfont icon-' + this.iconCss + '"></i><p>' + this.ModelName + '</p></a><div class="addremove"><img src="images/add.jpg" /></div></li>';
                            $(".showApp").append(html);
                        })
                    }
                    else {

                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        //获取数据
        function HasMode() {
            $("#main").html("");
            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/DeskManage/AppHandler.ashx", "IDCard": $("#IDCard").val(), "Func": "GetModelList", Ispage: "false", IsShow: "in"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            var html = '<li id=' + this.ID + '><a style="background:' + this.ModelCss + '"><i class="iconfont icon-' + this.iconCss + '"></i><p>' + this.ModelName + '</p></a><div class="addremove"><img src="images/remove.jpg" /></div></li>';
                            $(".hideApp").append(html);
                        })
                    }
                    else {

                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
    </script>
    <script type="text/javascript">
        $(function () {
            check('.hideApp');
            check('.showApp');
        })
        function check(obj) {
            $(obj).children().hover(function () {
                $(this).find('.addremove').show();
            }, function () {
                if ($(this).hasClass('selected')) {
                    $(this).find('.addremove').show();
                    return;
                }
                $(this).find('.addremove').hide();
            }).click(function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected')
                } else {
                    $(this).addClass('selected')
                }
            })
        }
        function cancelIframe() {
            var index = parent.layer.getFrameIndex(window.name);
            parent.layer.close(index); //执行关闭
        }
        function keepIframe() {
            var AddIDS = "";
            var DelIDS = "";
            $($(".showApp").find("li.selected")).each(function () {
                AddIDS += this.id + ",";
            })
            $($(".hideApp").find("li.selected")).each(function () {
                DelIDS += this.id + ",";
            })
            if (AddIDS == "" && DelIDS == "") {
                parent.CloseIFrameWindow();
            }
            else {
                $.ajax({
                    url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: {
                        PageName: "/DeskManage/AppHandler.ashx",
                        "Func": "AddDeskModel",
                        AddIDS: AddIDS,
                        DelIDS: DelIDS,
                        IDCard: $("#IDCard").val()
                    },
                    success: function (json) {
                        if (json.result.errNum.toString() == "0") {
                            parent.layer.msg('设置成功!');
                            parent.getData();
                            parent.CloseIFrameWindow();
                        }
                        else {
                            layer.msg(json.result.errMsg);
                        }
                    },
                    error: function (errMsg) {
                        layer.msg(errMsg);
                    }
                });
            }
        }
    </script>
</body>
</html>
