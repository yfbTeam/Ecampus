<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllAssociateManage.aspx.cs" Inherits="SCWeb.StuAssociate.AllAssociateManage" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>全部社团</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css">
    <link rel="stylesheet" href="/css/style.css">
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/laypage/laypage.js"></script>
    <%--社团分类下的社团--%>
    <script id="div_sub" type="text/x-jquery-tmpl">
        <div class="corporation_item">
            <a href="javascript:;" onclick="JumpToAssociateDetail(${Id});">
                <div class="corporation_img fl">
                    <img src="${PicURL}" onerror="javascript:this.src='/images/assodefault.jpg'" alt="">
                </div>
                <div class="corporation_detail fl">
                    <h1 class="corporation_name">${Name}</h1>
                    <h2 class="corporation_head">社团长：${CreateName}</h2>
                    <div class="corporation_description">
                        ${cutstr(Introduce,64)}
                    </div>
                </div>
            </a>
            <div class="corporation_bottom">
                <span class="fl">成员：${MemCount}人</span>
                {{if <%=UserInfo.UserType%>=="2"}}
                    {{if IsLeader==0 && IsMember==0}}
                   <a href="javascript:;" class="fr" onclick="OpenAssoApply(${Id},${ApplyType});">加入社团</a>
                {{else}}
                   <a href="javascript:;" class="fr">已加入社团</a>
                {{/if}}
                {{else}}
                   <a href="javascript:;" class="fr" onclick="DelAssoInfo(${Id});">删除</a>
                <a href="javascript:;" class="fr" onclick="OpenIFrameWindow('编辑社团','/StuAssociate/EditAssoInfo.aspx?itemid=${Id}','730px','600px');">编辑</a>
                {{/if}}
            </div>
        </div>
    </script>
</head>
<body>
    <div id="header"></div>
    <div id="main">
        <div class="center pt10">
            <div class="wrap" style="padding-bottom: 20px;">
                <div class="clearfix tools">
                    <div id="div_TypeAsso" class="select_tools fl clearfix">
                        <a href="javascript:;" class="selected">全部</a>
                        <a href="javascript:;">语言</a>
                        <a href="javascript:;">体育</a>
                        <a href="javascript:;">学习</a>
                        <a href="javascript:;">娱乐</a>
                        <a href="javascript:;">其他</a>
                    </div>
                    <div class="fr tools_right" id="div_tools" style="display: none;">
                        <a href="javascript:;" class="bgblue" onclick="OpenIFrameWindow('报名时间设置','/StuAssociate/AssoEnteredSet.aspx','520px','250px')">报名时间设置</a>
                        <a href="javascript:;" class="bgblue" onclick="OpenIFrameWindow('社团分类管理','/StuAssociate/EditAssoType.aspx','680px','485px')">社团分类管理</a>
                        <a href="javascript:;" class="bggreen" onclick="OpenIFrameWindow('创建社团','/StuAssociate/EditAssoInfo.aspx?itemid=0','730px','600px')">创建社团</a>
                        <a href="javascript:;" class="bgorange" onclick="OpenIFrameWindow('发布通知','/CommonPage/EditComNewInfo.aspx?type=0&relid=0&itemid=0&newtype=1','730px','600px')">发布通知</a>
                    </div>
                </div>
                <div id="div_list" class="corporation_lists clearfix"></div>
            </div>
        </div>
    </div>
    <div id="footer"></div>
    <script>
        var typeidArray = [];
        $(function () {
            SetPageButton('<%=UserInfo.UniqueNo%>');

            $('#header').load('/CommonPage/Header.aspx?t=' + new Date().getTime(), function (response, status, xhr) {
                if (status == "success" || status == "notmodified") {
                    SetNavSelected(3);
                }
            });
            $('#footer').load('/CommonPage/footer.html');
            SetDisplayOrHide();
            GetAssoType(1, 4);//获取社团分类
        });
        function SetDisplayOrHide() { //设置显示隐藏
            if ("<%=UserInfo.UserType%>" == "2") {
                $("#div_tools").hide();
            } else {
                $("#div_tools").show();
            }
        }
        //获取社团分类
        function GetAssoType(startIndex, pageSize) {
            $("#div_TypeAsso").html('<a href="javascript:;" class="selected">全部</a>');
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_TypeHandler.ashx",
                    Func: "GetAssoTypeDataPage",
                    PageIndex: startIndex,
                    PageSize: pageSize
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        typeidArray = [];
                        $(json.result.retData.PagedData).each(function (i, n) {
                            typeidArray.push(n.Id);
                            $("#div_TypeAsso").append('<a id="a_assotype' + n.Id + '" href="javascript:;">' + n.Name + '</a>');
                        });
                        $("#div_TypeAsso").append('<a id="a_assotype-1" href="javascript:;">其他</a>');
                        $('#div_TypeAsso a').click(function () {
                            $(this).addClass('selected').siblings().removeClass('selected');
                            getData(1, 10);
                        });
                        getData(1, 10);
                    } else {
                        $("#div_list").html('<div>暂无社团！</div>');
                    }
                }
            });
        }
        //获取分类下的社团
        function getData(startIndex, pageSize) {
            var assotype = $('#div_TypeAsso').find("a.selected")[0].id.replace("a_assotype", "");
            var params = { //暂不分页
                PageName: "/StuAssociate/Asso_InfoHandler.ashx",
                Func: "GetAssoInfoDataPage",
                ispage: false,
                AssoType: assotype,
                LoginUID: "<%=UserInfo.UniqueNo%>",
                IsOnlyBase: "1",
                IsUnifiedInfo: "1"
            };
            if (assotype.toString() == "-1") {
                params["UnAssoTypes"] = typeidArray.join(',');
            }
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: params,
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#div_list").html('');
                        var rtnObj = json.result.retData;
                        $("#div_sub").tmpl(rtnObj).appendTo("#div_list");
                    }
                    else {
                        $("#div_list").html('<div>暂无社团！</div>');
                    }
                },
                error: function (errMsg) {
                    $("#div_list").html('<div>暂无社团！</div>');
                }
            });
        }
        function JumpToAssociateDetail(assoid) {
            window.open('/StuAssociate/AssociateDetail.aspx?itemid=' + assoid + '&nav=3');
        }
        function OpenAssoApply(relid, astype) {
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_InfoHandler.ashx",
                    Func: "GetAssoEnteredData"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var model = json.result.retData[0];
                        var isonly = model.IsOnly.toString();
                        if (model.EnteredStats.toString() == "-1") { //-1 报名未开始；1报名中；-2 报名已结束
                            layer.msg("报名还未开始（" + DateTimeConvert(model.StartTime, 'yyyy-MM-dd') + "~" + DateTimeConvert(model.EndTime, 'yyyy-MM-dd') + "）");
                        } else if (model.EnteredStats.toString() == "-2") {
                            layer.msg("报名已结束");
                        } else {
                            if (isonly == "0") {

                            }
                            OpenIFrameWindow('申请加入', '/StuAssociate/AddAssoApply.aspx?itemid=0&relid=' + relid + '&astype=' + astype + '&type=1', '480px', '400px');
                        }

                    } else {
                        OpenIFrameWindow('申请加入', '/StuAssociate/AddAssoApply.aspx?itemid=0&relid=' + relid + '&astype=' + astype + '&type=1', '480px', '400px');
                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        function DelAssoInfo(itemid) {
            layer.confirm('确定要删除该社团吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                $.ajax({
                    url: "/Common.ashx",
                    type: "post",
                    dataType: "json",
                    data: {
                        PageName: "/StuAssociate/Asso_InfoHandler.ashx",
                        func: "DelAssoInfo",
                        ItemId: itemid
                    },
                    success: function (json) {
                        if (json.result.errNum == 0) {
                            layer.msg('删除成功！');
                            getData(1, 10);
                        }
                        else {
                            layer.msg(json.result.errMsg);
                        }
                    },
                    error: function (ErroMsg) {
                        layer.msg(ErroMsg);
                    }
                });
            }, function () { });
        }
    </script>
</body>
</html>
