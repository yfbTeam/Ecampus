<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelCatogry.aspx.cs" Inherits="SCWeb.DeskManage.ModelCatogry" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <title>分类管理</title>
    <!--图标样式-->
    <link href="/css/reset.css" rel="stylesheet" />
    <link href="/css/style.css" rel="stylesheet" />
    <link href="/css/iconfont.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/PageBar.js"></script>
    <style type="text/css">
        .h-ico {
            display: inline-block;
        }
    </style>
    <!--[if IE]>
			<script src="/Script/html5.js"></script>
		<![endif]-->
    <script id="tr_User" type="text/x-jquery-tmpl">
        <tr>
            <td>${Name}</td>
            <td>{{if Status==0}}禁用
                {{else}}启用
                {{/if}}</td>
            <td>${SortNum}</td>
            <td>${DateTimeConvert(CreateTime,"yyyy-MM-dd")}</td>
            <td>
                <a href="javascript:;" onclick="EditCatagory(${ID},'${Name}',${Status})"><i class="icon icon-edit"></i>修改</a>
        </tr>
    </script>

</head>
<body>

    <form id="form1" runat="server">
        <!--header-->
        <div id="header"></div>
        <input type="hidden" id="UniqueNo" value="<%=UserInfo.UniqueNo %>" />
        <div id="main">
            <div class="center clearfix  pt10">
                <div class="table_wraps">
                    <div class="clearfix table_tool">
                        <%--<div class="fl clearfix">
                            <select name="" class="select fl" id="ModelType" onchange="getData(1, 10)">
                            </select>
                        </div>--%>

                        <div class="fr tools_right clearfix">

                            <a href="javascript:;" class="bgblue" onclick="AddCatagory()">
                                <i class="icon icon-plus"></i>新建分类
                            </a>
                        </div>
                    </div>

                    <div class="wrap">
                        <table id="Course" class="table">
                            <thead>
                                <th class="pr checkall">类别名称</th>
                                <th>状态</th>
                                <th>排序号</th>
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
<script src="/Scripts/Common.js"></script>

<script type="text/javascript">
    $(function () {
        SetPageButton('<%=UserInfo.UniqueNo%>');

        $('#header').load('/CommonPage/Header.aspx');
        $('#footer').load('/CommonPage/footer.html');

        CatoryModel();
    });
    function CatoryModel() {
        $.ajax({
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            type: "post",
            async: false,
            dataType: "json",
            data: { PageName: "/DeskManage/AppHandler.ashx", "Func": "ModelCatogory" },
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    $("#tb_Manage").html('');
                    $("#tr_User").tmpl(json.result.retData).appendTo("#tb_Manage");
                }
                else {
                }
            },
            error: function (errMsg) {
                layer.msg(errMsg);
            }
        });
    }

    //添加课程
    function AddCatagory() {
        OpenIFrameWindow('添加分类', '/DeskManage/CatogoryAdd.aspx', '320px', '280px');
    }
    //修改课程
    function EditCatagory(id, name, Status) {
        OpenIFrameWindow('修改分类', '/DeskManage/CatogoryAdd.aspx?ID=' + id + '&Name=' + name + '&Status=' + Status, '320px', '280px');
    }
    function DelCatagory(ID) {
        if (confirm("确定要删除模块分类吗？")) {
            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/DeskManage/AppHandler.ashx", "Func": "DelCatagory", ID: ID
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        layer.msg("删除成功！");
                        getData(1, 10);
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
