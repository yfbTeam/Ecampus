<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SCWeb.DeskManage.Index" %>

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
    <script type="text/javascript">
        $(function () {
            //退出
            $('.setting').hover(function () {
                $(this).find('.setting_none').show();
            }, function () {
                $(this).find('.setting_none').hide();
            })
        })
    </script>
</head>
<body>
    <input type="hidden" id="IDCard" value="<%=UserInfo.UniqueNo%>" />
    <input type="hidden" id="CatagoryID" value="" />
    <div id="container">
        <div id="header">
            <div class="logo fl">
                <img src="/DeskManage/images/logo.png" style="height: 32px;" />
            </div>
            <div class="mydesktop fl">
                <a href="javascript::">
                    <i class="iconfont icon-home"></i>
                    <span>我的桌面</span>
                </a>
            </div>
            <div class="user_setting fr clearfix mr20">
                <div class="replace_bg fl" onclick="openSkin()">
                    <img src="/DeskManage/images/huanfu.png" alt="Alternate Text" />
                </div>
                <div class="fl user_img">
                    <%if (!string.IsNullOrWhiteSpace(UserInfo.AbsHeadPic))
                      { %>
                    <img src="<%=UserInfo.AbsHeadPic %>" />
                    <%}
                      else
                      { %>
                    <img src="/images/teacher_img.png" />
                    <%} %>
                </div>
                <div class="user_name fl">
                    <%=UserInfo.Name %>
                </div>
                <div class="setting fl pr">

                    <a href="javascript:;">
                        <i class="iconfont icon-settings"></i>
                    </a>
                    <div class="setting_none">
                        <span onclick="logOut()">退出</span>
                    </div>
                </div>
            </div>
        </div>
        <div id="main">
            <div class="modult">
                <h1 class="title" id="title">
                    <%--<span class="active">
                        基础平台
                    </span>
                    <span>
                        基础平台
                    </span>--%>
                </h1>
            </div>
            <div class="app_lists clearfix" id="module">
                <%--<a href="" class="clearfix">
                    <div class="icon bgred">
                        <i class="iconfont icon-xuexi"></i>
                    </div>
                    <p class="fl">课程管理</p>
                </a>--%>
            </div>
        </div>
        <div id="footer"></div>
    </div>
    <script>
        $(function () {
            $('#footer').load('/CommonPage/footer.html');
            //CatoryModel();
            getData();
            InitSkin();
        })
        //function initData() {

        //}

        function CatoryModel() {
            $("#title").html("");
            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: { PageName: "/DeskManage/AppHandler.ashx", "Func": "ModelCatogory" },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var i = 0;
                        $(json.result.retData).each(function () {
                            var html = "";
                            if (i == "0") {
                                html = "<span class=\"active\" id=\"modult\"" + this.ID + ">" + this.Name + "</span>";
                                $("#title").append(html);
                                $("#CatagoryID").val(this.ID);
                                i++;
                            }
                            else {
                                html = "<span>基础平台</span>";
                                $("#title").append(html);
                                i++;
                            }
                            //var span = '<span>' + item.CatogryName + '</span>';
                            //$('#title').append(span);
                            //$('#title span:eq(0)').addClass('active');

                            //var modultID = "modult" + this.CatogryID;
                            //var len = $("#main").find("#" + modultID).length;
                            //var html = '<div class="modult" id="' + modultID + '"><h1 class="title"><span>' + this.CatogryName + '</span></h1><div class="apps_wrap clearfix"><div class="apps clearfix"></div></div></div>';
                            //if (len == 0) {
                            //    $("#main").append(html);
                            //}
                            //$("#" + modultID).find(".apps").append('<a href="' + this.LinkUrl + '" class="' + this.ModelCss + '" target="' + this.OpenType + '"><i class="iconfont icon-' + this.iconCss + '"></i><p>' + this.ModelName + '</p></a>');
                            //i++;
                            //if (i == RowCount) {
                            //    $("#" + modultID).find(".apps").append('<div class="add fl" onclick="AddApp()"><i class="iconfont icon-add"></i></div>');
                            //}
                        })
                        //$("#tb_Manage").html('');
                        //$("#tr_User").tmpl(json.result.retData).appendTo("#tb_Manage");
                    }
                    else {
                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        function InitSkin() {
            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/DeskManage/AppHandler.ashx", "UserID": $("#IDCard").val(), "Func": "Skin"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        //$('#bgcolor').attr('src', json.result.retData)

                        document.getElementById("container").style.background = "url(" + json.result.retData + ") no-repeat center top fixed";
                        //CloseIFrameWindow();
                    }
                    else {
                        layer.msg("皮肤更新失败");
                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        function UpdateSkin(ImageUrl) {

            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/DeskManage/AppHandler.ashx", "UserID": $("#IDCard").val(), "Func": "AddSkin", "SkinImage": ImageUrl
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        //$('#bgcolor').attr('src', json.result.retData)
                        document.getElementById("container").style.background = "url(" + ImageUrl + ") no-repeat center top fixed;background-size:100%;";
                        CloseIFrameWindow();
                    }
                    else {
                        layer.msg("皮肤更新失败");
                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });

        }
        function ViewSkin(ImageUrl) {
            //$('#bgcolor').attr('src', ImageUrl)

            document.getElementById("container").style.background = "url(" + ImageUrl + ") no-repeat center top fixed";
        }
        function CancelSkin() {
            $("#layui-layer-shade1").hide();
            $("#layui-layer1").hide();
            //CloseIFrameWindow();
            InitSkin();
        }
        function AddApp() {
            OpenIFrameWindow('模块添加', 'DeskModel.aspx', '800px', '650px');
        }
        closeBtn: 0
        function openSkin() {
            layer.open({
                type: 2,
                title: "选择皮肤",
                shadeClose: false,
                shade: 0.2,
                closeBtn: 0,
                area: ['700px', '680px'],
                content: "Skin.aspx" //iframe的url
            });
            //OpenIFrameWindow('选择皮肤', 'Skin.aspx', '700px', '680px');
        }
        function getData() {
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
                        var RowCount = json.result.retData.length;
                        var i = 0;
                        $(json.result.retData).each(function () {
                            var li = '<a href="' + this.LinkUrl + '" class="clearfix" target="' + this.OpenType +
                                '"><div class="icon"	style="background: ' + this.ModelCss + '"><i class="iconfont icon-' + this.iconCss + '"></i></div><p class="fl">' + this.ModelName + '</p></a>';
                            var modultID = "modult" + this.CatogryID;
                            var len = $("#main").find("#" + modultID).length;
                            var html = '<div class="modult" id="' + modultID + '"><h1 class="title"><span>' + this.CatogryName + '</span></h1><div class="apps_wrap clearfix"><div class="apps clearfix"></div></div></div>';
                            if (len == 0) {
                                $("#main").append(html);
                            }
                            $("#" + modultID).find(".apps").append('<a href="' + this.LinkUrl + '" style="background:' + this.ModelCss + '" target="' + this.OpenType + '"><i class="iconfont icon-' + this.iconCss + '"></i><p>' + this.ModelName + '</p></a>');
                            i++;
                            if (i == RowCount) {
                                $("#" + modultID).find(".apps").append('<div class="add fl" onclick="AddApp()"><i class="iconfont icon-add"></i></div>');
                            }
                        })
                    }
                    else {
                        $("#main").html('<div class="modult" id=""><h1 class="title"><span>无分类</span></h1><div class="apps_wrap clearfix"><div class="apps clearfix"><div class="add fl" onclick="AddApp()"><i class="iconfont icon-add"></i></div></div></div></div>');

                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        //获取数据
        /*function getData() {
            $("#main").html("");
            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/DeskManage/AppHandler.ashx", "IDCard": $("#IDCard").val(), "Func": "GetModelList", Ispage: "false"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var RowCount = json.result.retData.length;
                        var i = 0;

                        $(json.result.retData).each(function () {
                            var modultID = "modult" + this.CatogryID;
                            var len = $("#main").find("#" + modultID).length;
                            var html = '<div class="modult" id="' + modultID + '"><h1 class="title"><span>' + this.CatogryName + '</span></h1><div class="apps_wrap clearfix"><div class="apps clearfix"></div></div></div>';
                            if (len == 0) {
                                $("#main").append(html);
                            }
                            $("#" + modultID).find(".apps").append('<a href="' + this.LinkUrl + '" class="' + this.ModelCss + '" target="' + this.OpenType + '"><i class="iconfont icon-' + this.iconCss + '"></i><p>' + this.ModelName + '</p></a>');
                            i++;
                            if (i == RowCount) {
                                $("#" + modultID).find(".apps").append('<div class="add fl" onclick="AddApp()"><i class="iconfont icon-add"></i></div>');
                            }
                        })
                    }
                    else {
                        $("#main").html('<div class="modult" id=""><h1 class="title"><span>无分类</span></h1><div class="apps_wrap clearfix"><div class="apps clearfix"><div class="add fl" onclick="AddApp()"><i class="iconfont icon-add"></i></div></div></div></div>');

                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }*/
    </script>
</body>
</html>
