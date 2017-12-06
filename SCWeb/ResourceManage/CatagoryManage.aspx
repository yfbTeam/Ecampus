<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatagoryManage.aspx.cs" Inherits="SCWeb.ResourceManage.CatagoryManage" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>模块管理</title>
    <!--图标样式-->
    <link href="/css/reset.css" rel="stylesheet" />
    <link href="/css/style.css" rel="stylesheet" />
    <link rel="stylesheet" href="/css/iconfont.css"/>
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/laypage/laypage.js"></script>
    <style type="text/css">
        .h-ico {
            display: inline-block;
        }
    </style>
    <!--[if IE]>
			<script src="js/html5.js"></script>
		<![endif]-->
    <script id="tr_User" type="text/x-jquery-tmpl">
        <tr>
            <td>${SubJectName}</td>
            <td>${VersionName}</td>
            <td>${GradeName}</td>
            <td>${DateTimeConvert(CreateTime,"yyyy-MM-dd")}</td>
            <td>
                <a href="javascript:;" onclick="DelCatagory(${ID})">删除</a>
                <a href="javascript::" onclick="OpenIFrameWindow('管理目录', '/ResourceManage/SubJectCatogory.aspx?SubID=${ID}', '420px', '320px');">管理目录</a>
        </tr>
    </script>
</head>
<body>

    <form id="form1" runat="server">
        <!--header-->
        <div id="header"></div>
        <input type="hidden" id="HUserIdCard" value="<%=UserInfo.UniqueNo %>"
        <div id="main">
             <div class="center clearfix  pt10">
                <div class="table_wraps">
                    <div class="clearfix table_tool">
                        <div class="fl clearfix">
                            <select name="" class="select fl" id="Version" onchange="getData(1, 10)">
                            </select>
                            <select name="" class="select fl" id="Grade" onchange="getData(1, 10)">
                            </select>
                        </div>
                        <div class="fr tools_right clearfix">

                            <a href="javascript:;" class="bgblue" onclick="AddCatagory()">
                                <i class="icon icon-plus"></i>新建科目
                            </a>
                            <a href="javascript::" class="bgblue" onclick="OpenIFrameWindow('年级管理', '/ResourceManage/GradeManage.aspx', '500px', '450px');">
                                <i class="icon icon-plus"></i>年级管理
                            </a>
                            <a href="javascript::" class="bgblue" onclick="OpenIFrameWindow('教材版本管理', '/ResourceManage/VersionManage.aspx', '500px', '450px');">
                                <i class="icon icon-plus"></i>教材版本管理
                            </a>
                        </div>
                    </div>
                    <div class="wrap" style="padding:0;">
                        <table id="Course" class="table">
                            <thead>
                                <th class="pr checkall">科目名称</th>
                                <th>教材版本</th>
                                <th>年级名次</th>
                                <th>创建时间</th>
                                <th>操作</th>
                            </thead>
                            <tbody id="tb_Manage">
                            </tbody>
                        </table>
                    </div>
                </div>
                <!--分页-->
                <div class="page">
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
        BindVersion();
        BindGrade();
        getData(1, 15);
    });
    function BindVersion() {
        $("#Version").html("");
        $.ajax({
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            type: "post",
            async: false,
            dataType: "json",
            data: {

                PageName: "/BookSubject/SubJect.ashx",
                "Func": "Version", IsPage: "false"
            },
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    $(json.result.retData).each(function () {
                        $("#Version").append("<option value='" + this.ID + "'>" + this.Name + "</option>");
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
    function BindGrade() {
        $("#Grade").html("");
        $.ajax({
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            type: "post",
            async: false,
            dataType: "json",
            data: {
                PageName: "/BookSubject/SubJect.ashx",
                "Func": "Grade", IsPage: "false"
            },
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    $(json.result.retData).each(function () {
                        $("#Grade").append("<option value='" + this.ID + "'>" + this.Name + "</option>");
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
    function getData(startIndex, pageSize) {
        //初始化序号 
        pageNum = (startIndex - 1) * pageSize + 1;

        $.ajax({
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            type: "post",
            async: false,
            dataType: "json",
            data: { PageName: "/BookSubject/SubJect.ashx", "Func": "GetSubJectData", PageIndex: startIndex, pageSize: pageSize, "VersionID": $("#Version").val(), "GradeID": $("#Grade").val() },
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    $("#tb_Manage").html('');
                    $("#tr_User").tmpl(json.result.retData.PagedData).appendTo("#tb_Manage");
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
                else {
                    $("#tb_Manage").html('');
                    $(".page").hide();
                }
            },
            error: function (errMsg) {
                layer.msg(errMsg);
            }
        });
    }

    //添加教材
    function AddCatagory() {
        OpenIFrameWindow('添加分类', '/ResourceManage/SubJectAdd.aspx', '450px', '280px');
    }

    function DelCatagory(ID) {
        if (confirm("确定要删除数据吗？")) {
            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/BookSubject/SubJect.ashx", "Func": "DelSubJect", ID: ID
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        layer.msg("删除成功！");
                        getData(1, 15);
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
