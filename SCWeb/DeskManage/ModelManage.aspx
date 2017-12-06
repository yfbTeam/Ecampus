<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelManage.aspx.cs" Inherits="SCWeb.DeskManage.ModelManage" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>模块管理</title>
    <!--图标样式-->
    <link href="/css/reset.css" rel="stylesheet" />
    <link href="/css/style.css" rel="stylesheet" />
    <link href="/css/iconfont.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/laypage/laypage.js"></script>
    <link href="/Scripts/jquery-treetable-3.1.0-0/stylesheets/jquery.treetable.css" rel="stylesheet" />
    <link href="/Scripts/jquery-treetable-3.1.0-0/stylesheets/jquery.treetable.theme.default.css" rel="stylesheet" />
    <script src="/Scripts/jquery-treetable-3.1.0-0/vendor/jquery-ui.js"></script>
    <script src="/Scripts/jquery-treetable-3.1.0-0/javascripts/src/jquery.treetable.js"></script>
    <!--[if IE]>
		<script src="/Scripts/html5.js"></script>
	<![endif]-->
    <style>
        #table_tree thead tr th {
            height: 40px;
            background: #eef5fd;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #d0ebff;
            font-size: 14px;
            color: #777;
        }

        table.treetable tbody tr td {
            height: 32px;
            text-align: center;
            vertical-align: middle;
            border: 1px solid #d0ebff;
            font-size: 14px;
            color: #888;
            position: relative;
        }

            table.treetable tbody tr td:first-child {
                text-align: left;
                padding-left: 70px;
            }

            table.treetable tbody tr td .setings {
                position: absolute;
                top: -18px;
                right: -18px;
                transform: rotate(225deg);
                border: 18px solid #91c954;
                width: 0px;
                height: 0px;
                border-color: #91c954 transparent transparent transparent;
                z-index: 9999;
                cursor: pointer;
            }

                table.treetable tbody tr td .setings .iconfont {
                    color: #fff;
                    font-size: 13px;
                    position: absolute;
                    bottom: 5px;
                    left: -7px;
                }

            table.treetable tbody tr td .setting_none {
                display: none;
                z-index: 999;
                position: absolute;
                right: -80px;
                top: 0;
                width: 78px;
                border: 1px solid #DEEFCB;
            }

                table.treetable tbody tr td .setting_none a {
                    display: block;
                    text-align: center;
                    width: 100%;
                    height: 29px;
                    border-bottom: 1px solid #DEEFCB;
                    background: #fff;
                    line-height: 29px;
                    color: #777777;
                    font-size: 14px;
                }

                    table.treetable tbody tr td .setting_none a:hover {
                        background: #91c954;
                        color: #fff;
                    }
    </style>
    <script id="tr_User" type="text/x-jquery-tmpl">
        <tr>
            <td>${ModelName}</td>
            <td>${CatogryName}</td>
            <td>${LinkUrl}</td>
            <td>${DateTimeConvert(CreateTime,"yyyy-MM-dd")}</td>
            <td>
                <a href="javascript:;" onclick="EditModol(${ID})"><i class="icon icon-edit"></i>修改</a>
            </td>
        </tr>
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="UniqueNo" value="<%=UserInfo.UniqueNo %>" />
        <div id="header"></div>
        <div id="main">
            <div class="center clearfix  pt10">
                <div class="table_wraps">
                    <div class="clearfix table_tool" id="CourseSel">
                        <div class="fl clearfix">
                            <select name="" class="select fl" id="ModelType" onchange="GetMenuInfo()">
                            </select>
                        </div>

                        <div class="fr tools_right clearfix">
                            <a href="/DeskManage/ModelCatogry.aspx" class="bgblue" target="_blank">分类管理
                            </a>
                            <%--<a href="javascript:;" class="bgblue" onclick="AddModol(0,0)" id="icon-plus">
                                <i class="icon icon-plus"></i>新建模块
                            </a>--%>
                        </div>
                    </div>
                    <%--<div class="wrap" style="padding:0px;">
                        <table class="table">
                            <thead>
                                <th class="pr checkall">模块名称</th>
                                <th>所属分类</th>
                                <th>链接地址</th>
                                <th>创建时间</th>
                                <th>操作</th>
                            </thead>
                            <tbody id="tb_Manage">
                            </tbody>
                        </table>
                    </div>--%>
                    <div id="div_table"></div>

                </div>
                <!--分页-->
                <div class="page none">
                    <span id="pageBar"></span>
                </div>
            </div>
        </div>
        <div id="footer"></div>
    </form>
</body>

<script type="text/javascript">
    $(function () {
        SetPageButton('<%=UserInfo.UniqueNo%>');
        $('#header').load('/CommonPage/Header.aspx');
        $('#footer').load('/CommonPage/footer.html');
        CatoryModel();
        GetMenuInfo();
    });

    function CatoryModel() {
        $("#ModelType").html("<option value=''>==请选择==</option>");
        $.ajax({
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            type: "post",
            async: false,
            dataType: "json",
            data: {
                PageName: "/DeskManage/AppHandler.ashx",
                "Func": "ModelCatogory", Status: "1"
            },
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    $(json.result.retData).each(function () {
                        $("#ModelType").append("<option value='" + this.ID + "'>" + this.Name + "</option>");
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
    /*
    //获取数据
    function getData(startIndex, pageSize) {
        //初始化序号 
        pageNum = (startIndex - 1) * pageSize + 1;
        var ModelType = $("#ModelType").val();

        $.ajax({
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            type: "post",
            async: false,
            dataType: "json",
            data: {
                PageName: "/DeskManage/AppHandler.ashx", "Func": "GetModelList", PageIndex: startIndex, pageSize: pageSize, Ispage: "true", "ModelType": ModelType
            },
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    $("#tb_Manage").html('');
                    $("#tr_User").tmpl(json.result.retData.PagedData).appendTo("#tb_Manage");

                    if (json.result.retData.RowCount > pageSize) {
                        $(".page").show();
                        laypage({
                            cont: 'pageBar', //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                            pages: json.result.retData.PageCount, //通过后台拿到的总页数
                            curr: json.result.retData.PageIndex || 1, //当前页
                            skip: false, //是否开启跳页
                            skin: '#1472b9',
                            jump: function (obj, first) { //触发分页后的回调
                                if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                    getData(obj.curr, pageSize)
                                }
                            }
                        });
                    }
                }
                else {
                    var html = '<tr><td colspan="1000"><div style="background:#fff url(/images/error.png) no-repeat center center; height: 500px;"></div></td></tr>';
                    $("#tb_Manage").html(html);

                    layer.msg(json.result.errMsg);
                }
            },
            error: function (errMsg) {
                layer.msg(errMsg);
            }
        });
    }*/
    var menu_list = [];
    function GetMenuInfo() {
        var ModelType = $("#ModelType").val();
        $("#div_table").html('<table id="table_tree"><thead><tr><th style="width: 45%">导航名称</th><th style="width: 55%;">URL</th></tr></thead><tbody id="tb_list" style="background:#ffffff;"><tr data-tt-id="0" data-tt-parent-id="null"><td>全部<div btncls="icon-setting"  class="setings" onclick="AddModol(0,0);"><i class="iconfont" style="transform:rotate(-45deg)">&#xe605;</i></div></td><td></td></tr></tbody></table>');
        $.ajax({
            url: "/Common.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: {
                PageName: "/DeskManage/AppHandler.ashx", "Func": "GetModelList", Ispage: "false", "ModelType": ModelType
            },
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    var html = '';
                    menu_list = json.result.retData;
                    BindMenu(0);
                    $("#table_tree").treetable({ expandable: true });
                    $('table.treetable tbody tr').find('.setings').click(function () {
                        if ($(this).next().is(':hidden')) {
                            $('table.treetable tbody tr').find('.setting_none').hide();
                            $(this).next().show();
                            $(this).next().mouseleave(function () {
                                $(this).hide();
                            });
                        } else {
                            $(this).next().hide();
                        }
                    })
                }
                else {
                    $("#tb_list").html("<tr><td colspan='2' style='text-align:center;'>暂无模块！</td></tr>");
                    $("#table_tree").treetable({ expandable: true });
                }
            },
            error: function (errMsg) {
                $("#tb_list").html("<tr><td colspan='2' style='text-align:center;'>暂无模块！</td></tr>");
                $("#table_tree").treetable({ expandable: true });
            }
        });
    }
    function BindMenu(parentid) {
        for (var menu in menu_list) {
            var curmenu = menu_list[menu];
            if (curmenu.Pid == parentid) {
                var mtr = '<tr data-tt-id=' + curmenu.ID + ' data-tt-parent-id=' + (curmenu.Pid == 0 ? null : curmenu.Pid) + '><td>' + curmenu.ModelName
                    + '<div btncls="icon-setting" class="setings"><i class="iconfont">&#xe63b;</i></div><div class="setting_none">' +
                    (curmenu.IsMenu.toString().toUpperCase() == "TRUE" ? '' : '<a href="javascript:;" onclick="AddModol(0,' + curmenu.ID + ');">添加</a>') + '<a href="javascript:;" onclick="EditModol(' + curmenu.ID +
                    ');">编辑</a><a href="javascript:;" onclick="DelModol(' + curmenu.ID + ');">删除</a></div></td><td>' + curmenu.LinkUrl + '</td></tr>';
                $('#tb_list').append(mtr);
                if (curmenu.ChildCount > 0) {
                    BindMenu(curmenu.ID);
                }
            }
        }
    }
    //添加模块
    function AddModol(itemid, pid) {
        OpenIFrameWindow('添加模块', '/DeskManage/ModelAdd.aspx?Pid=' + pid, '720px', '470px');
    }
    //修改模块
    function EditModol(id) {
        OpenIFrameWindow('修改模块', '/DeskManage/ModelAdd.aspx?ID=' + id, '720px', '470px');
    }
    function DelModol(ID) {
        if (confirm("确定要删除模块吗？")) {
            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/DeskManage/AppHandler.ashx", "Func": "DelModel", ID: ID
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        layer.msg("删除成功！");
                        GetMenuInfo();
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
</html>
