<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleSettings.aspx.cs" Inherits="SCWeb.SystemSettings.RoleSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>权限设置</title>
    <link href="/css/stylebase.css" rel="stylesheet" />
    <link href="/css/commonbase.css" rel="stylesheet" />
    <link href="/css/iconfont.css" rel="stylesheet" />
    <link href="/css/animate.css" rel="stylesheet" />



    <link rel="stylesheet" type="text/css" href="/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/reset.css" />
    <link rel="stylesheet" type="text/css" href="/css/common.css" />
    <link rel="stylesheet" type="text/css" href="/css/repository.css" />
    <link rel="stylesheet" href="/css/iconfont.css" />
    <link rel="stylesheet" href="/css/style.css" />

    <link href="/Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/jquery.cookie.js"></script>
    <script src="/Scripts/Common.js?parm=1.01"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/tz_slider.js"></script>
    <script src="/Scripts/PageBar.js"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.core-3.5.min.js"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.exedit-3.5.min.js"></script>
    <script id="tr_User" type="text/x-jquery-tmpl">
        <tr>
            <td>${pageIndex()}</td>
            <td>${Name}</td>
            <td>${LoginName}</td>
            <td>${Sex==1?'男':'女'}</td>
            <td>${UserType}</td>
            <td>
                <input type="button" class="Topic_btn" value="删除" onclick="javascript: DeleteUserRelation('${IDCard}')" />
            </td>
        </tr>

    </script>
    <style type="text/css">
        .ztree li a.curSelectedNode {
            height: 30px;
            line-height: 30px;
            background: #83bcd8;
            border: none;
        }

        .ztree li span.button.add { /*增加节点 按钮的样式*/
            margin-left: 2px;
            margin-right: -1px;
            background-position: -144px 0;
            vertical-align: middle;
            *vertical-align: middle;
        }

        .ztree li span.edit.button, .ztree li span.remove.button {
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <input type="hidden" id="hid_UserIDCard" runat="server" />
    <input type="hidden" id="hid_LoginName" runat="server" />
    <div id="header"></div>

    <div id="main">
        <div class="center clearfix pt10">
            <div class="Teaching_plan_management">
                <h1 class="Page_name none">管理员设置</h1>
                <div class="left_choice fl none">
                    <ul>
                        <li class="Sear">学校：<select id="sel_school" class="option" onchange="BindSystemBySchoolId(this.value);"></select>
                            系统：<select id="sel_system" class="option" onchange="BindRoleTree(this.value);"></select>
                        </li>
                    </ul>
                </div>
                <div style="clear: both;"></div>
                <div class="menubox1 left_navcon fl">
                    <!--头部-->
                    <h1><span class="tit_name">角色管理</span><span class="fr btn22"><a href="#" onclick="PermissionManage();">权限设置</a><a href="#" onclick="addHoverDom();">添加角色</a></span></h1>
                    <!--菜单区域-->
                    <div class="menu">
                        <ul>
                            <li>
                                <a class="menuclick1" href="#"><i>-</i>全部</a>
                                <div class="zTreeDemoBackground">
                                    <ul id="tree_Role" class="ztree submenu1" style="padding: 0px;"></ul>
                                </div>
                            </li>

                        </ul>
                    </div>
                    <!--end 菜单区域-->
                </div>
                <div class="right_dcon fr" style="background: #ffffff;">
                    <div class="Operation_area">
                        <div class="left_choice fl">
                            <ul>
                                <li class="Sear">
                                    <input type="text" id="search_w" name="search_w" class="search_w" placeholder="请输入用户名" value="" /><a class="sea" href="#" onclick="SearchUser();">搜索</a>
                                </li>
                            </ul>
                        </div>
                        <div class="right_add fr">
                            <a class="add" href="#" onclick="AddMemberByRoleid()"><i class="iconfont">&#xe726;</i>添加成员</a>
                            <a class="add" href="#" onclick="UserImport()"><i class="iconfont">&#xe726;</i>用户同步</a>

                        </div>
                    </div>
                    <div class="Administrator_settings">
                        <table class="W_form" id="tb_UserList">
                            <thead>
                                <tr class="trth">
                                    <th class="number">序号</th>
                                    <th class="Project_name">用户名</th>
                                    <th class="">登录名</th>
                                    <th class="">性别</th>
                                    <th class="">用户类型</th>
                                    <th>操作</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </div>
                    <!--分页页码开始-->
                    <div class="paging">
                        <span id="pageBar"></span>
                    </div>
                    <!--分页页码结束-->
                </div>
            </div>
        </div>
    </div>
    <div id="footer"></div>

</body>
<script type="text/javascript">
    $(function () {
        SetPageButton('<%=UserInfo.UniqueNo%>');

        $('#header').load('/CommonPage/Header.aspx?t=' + new Date().getTime());
        $('#footer').load('/CommonPage/footer.html');
    })
    function UserImport() {
        $.ajax({
            url: "/SystemSettings/UserImport.ashx?Func=UserAsync",
            async: false,
            dataType: "text",
            success: function (json) {
                if (json == "") {
                    layer.msg("数据更新同步成功");
                }
                else {
                    layer.msg(json);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.msg("操作失败！");
            }
        });
    }
    //zTree配置，添加/编辑/删除按钮需要引用jquery.ztree.exedit-3.5.min.js 或 jquery.ztree.all-3.5.min.js
    var setting = {
        view: {
            showLine: false,
            showIcon: false,
            selectedMulti: false//,
            //addHoverDom: addHoverDom,  //添加增加按钮，必须与 removeHoverDom 同时使用。注意：需要手动添加按钮样式 .ztree li span.button.add
            //removeHoverDom: removeHoverDom //当鼠标移出节点时，隐藏按钮
        },
        edit: {
            enable: true,
            editNameSelectAll: true,
            removeTitle: "删除", //删除按钮提示文字，showRemoveBtn（默认值：true）删除按钮，true/false分别表示 显示/隐藏 删除按钮
            renameTitle: "编辑",  //编辑按钮提示文字，showRenameBtn（默认值：true）编辑按钮，true/false分别表示 显示/隐藏 编辑按钮
            showRemoveBtn: function (treeId, treeNode) {
                var showdel = true;
                if (treeNode.name == '超级管理员' || treeNode.name == '校级管理员') { //什么时候可以不显示删除按钮
                    showdel = false;
                }
                return showdel;
            },
            showRenameBtn: function (treeId, treeNode) {
                var showdel = true;
                if (treeNode.name == '超级管理员' || treeNode.name == '校级管理员') { //什么时候可以不显示编辑按钮
                    showdel = false;
                }
                return showdel;
            }
        },
        data: {
            simpleData: {
                enable: true,
                idKey: "id",
                pIdKey: "pId"
            }
        },
        callback: {
            onClick: zTreeOnClick,  //节点单击方法
            beforeRemove: beforeRemove, //节点被删除之前的方法
            beforeEditName: zTreeBeforeEditName,//编辑按钮点击之前的方法
            beforeRename: zTreeBeforeRename,//节点被确定编辑之前的方法      
            onRemove: zTreeOnRemove, //删除节点
            onRename: zTreeOnRename  //编辑节点
        }
    };
    $(document).ready(function () {
        //BindSchoolData();//绑定学校
        BindRoleTree();
    });
    //绑定角色树
    function BindRoleTree(syskey) {
        $("#tb_UserList tbody").html("");
        $("#pageBar").hide();
        treeBind("tree_Role", "/Common.ashx", {
            "PageName": "SystemSettings/RoleHandler.ashx",
            func: "GetRoleTreeData",
            loginname: $("#<%=hid_LoginName.ClientID%>").val(),
            useridcard: $("#<%=hid_UserIDCard.ClientID%>").val()
        }, setting);
    }
    //角色节点单击方法
    function zTreeOnClick(event, treeId, treeNode) {
        $("#search_w").val("");
        $("#" + treeNode.tId).addClass("selected").siblings().removeClass("selected");
        selroleid = treeNode.id;
        getDataByRoleId(1);
    };

    //添加成员
    function AddMemberByRoleid() {
        var treeRole = $.fn.zTree.getZTreeObj("tree_Role");
        var selnodes = treeRole.getSelectedNodes();
        if (!selnodes.length) {
            layer.msg("请在左侧选中要添加成员的角色!");
            return;
        }
        OpenIFrameWindow('成员设置', 'MemberSettings.aspx?itemid=' + selnodes[0].id + "&SystemKey=" + $("#sel_system").val(), '620px', '610px');
    }

    //删除成员
    function DeleteUserRelation(idcard) {
        var treeRole = $.fn.zTree.getZTreeObj("tree_Role");
        var selnodes = treeRole.getSelectedNodes();
        if (!selnodes.length) {
            layer.msg("请在左侧选中要删除成员的角色!");
            return;
        }
        layer.msg("确定要删除角色下的该用户？", {
            time: 0 //不自动关闭
            , btn: ['确定', '取消']
            , yes: function (index) {
                layer.close(index);
                $.ajax({
                    url: "/Common.ashx",

                    async: false,
                    dataType: "json",
                    data: {
                        "PageName": "SystemSettings/RoleHandler.ashx",
                        func: "DeleteUserRelation",
                        roleid: selnodes[0].id,
                        deluseridcard: idcard,
                        loginname: $("#<%=hid_LoginName.ClientID%>").val(),
                        useridcard: $("#<%=hid_UserIDCard.ClientID%>").val()
                    },
                    success: function (json) {
                        var result = json.result;
                        if (result.errNum == 0) {
                            layer.msg("删除成功！");
                            selroleid = selnodes[0].id;
                            getDataByRoleId(1); //刷新列表
                        } else {
                            layer.msg("删除失败！");
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        layer.msg("操作失败！");
                    }
                });
            }
        });
    }

    //节点被删除之前的方法
    function beforeRemove(treeId, treeNode) {
        var zTree = $.fn.zTree.getZTreeObj("tree_Role");
        zTree.selectNode(treeNode);
        $("#" + treeNode.tId).addClass("selected").siblings().removeClass("selected");
        layer.msg("确认删除角色 " + treeNode.name + " 吗？", {
            time: 0 //不自动关闭
            , btn: ['确定', '取消']
            , yes: function (index) {
                layer.close(index);
                zTreeOnRemove(null, treeId, treeNode);
            }
        });
        return false;
    }
    //删除节点
    function zTreeOnRemove(event, treeId, treeNode) {
        $.ajax({
            url: "/Common.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: {
                "PageName": "SystemSettings/RoleHandler.ashx",
                func: "DeleteRole",
                roleid: treeNode.id,
                loginname: $("#<%=hid_LoginName.ClientID%>").val(),
                useridcard: $("#<%=hid_UserIDCard.ClientID%>").val()
            },
            success: OnSuccessDelete,
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.msg("操作失败！");
            }
        });

        function OnSuccessDelete(json) {
            var result = json.result;
            if (result.errNum == 0) {
                layer.msg("删除成功！");
                BindRoleTree($("#sel_system").val());//绑定角色树数据
            } else {
                layer.msg("删除失败！");
            }
        }
    }

    //编辑按钮点击之前的方法
    function zTreeBeforeEditName(treeId, treeNode) {
        var zTree = $.fn.zTree.getZTreeObj("tree_Role");
        zTree.selectNode(treeNode);
        $("#" + treeNode.tId).addClass("selected").siblings().removeClass("selected");
        return true;
    }
    //节点被确定编辑之前的方法
    function zTreeBeforeRename(treeId, treeNode, newName, isCancel) {
        var newname = newName.trim();
        if (treeNode.id == "" && !newname.length) {
            var zTree = $.fn.zTree.getZTreeObj("tree_Role");
            zTree.removeNode(treeNode);
        }
        return newname.length > 0;
    }
    //编辑节点
    function zTreeOnRename(event, treeId, treeNode, isCancel) {
        SaveRole(treeNode.id.toString(), treeNode.name);
    }

    //增加节点   
    function addHoverDom(e) {
        var zTree = $.fn.zTree.getZTreeObj("tree_Role");
        var nodes = zTree.getNodes();
        var data = eval(nodes);
        var isEdit = false;
        $.each(data, function (n, value) {
            if (value.editNameFlag)  //节点处于编辑状态
                isEdit = true;
        });
        if (!isEdit) { //如果没有节点处于编辑状态，则添加新节点
            var newNodes = zTree.addNodes(null, { id: "", pId: 0, name: "" });
            zTree.editName(newNodes[0]);
        }
        return false;
    };


    //保存角色
    function SaveRole(nodeid, nodename) {
        var func = nodeid.length ? "EditRole" : "AddRole";
        $.ajax({
            url: "/Common.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: {
                "PageName": "SystemSettings/RoleHandler.ashx",
                func: func,
                roleid: nodeid,
                name: nodename.trim(),
                loginname: $("#<%=hid_LoginName.ClientID%>").val(),
                useridcard: $("#<%=hid_UserIDCard.ClientID%>").val()
            },
            success: function (json) {
                var result = json.result;
                if (result.errNum == -1) {
                    layer.msg("该角色已存在！");
                    var zTree = $.fn.zTree.getZTreeObj("tree_Role");
                    var node = zTree.getNodeByParam("id", nodeid, null);
                    zTree.editName(node);
                }
                else if (result.errNum == 0) {
                    layer.msg("操作成功！");
                    BindRoleTree($("#sel_system").val());//绑定角色树数据
                } else {
                    layer.msg("操作失败！");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.msg("操作失败！");
            }
        });
    }

    //角色管理方法
    function PermissionManage() {
        var itemid = "";
        var treeRole = $.fn.zTree.getZTreeObj("tree_Role");
        var selnodes = treeRole.getSelectedNodes();
        if (selnodes.length) {
            itemid = selnodes[0].id;
        }
        OpenIFrameWindow('权限设置', 'PermissionSettings.aspx?itemid=' + itemid + "&SystemKey=" + $("#sel_system").val(), '590px', '510px');
    }
    var sername = $("#search_w").val().trim();
    //查询用户方法
    function SearchUser() {
        sername = $("#search_w").val().trim();
        var treeRole = $.fn.zTree.getZTreeObj("tree_Role");
        var selnodes = treeRole.getSelectedNodes();
        if (selnodes.length) {
            selroleid = selnodes[0].id;
            getDataByRoleId(1);
        }
    }

    var selroleid = "";
    //根据角色id获取用户数据
    function getDataByRoleId(startIndex) {
        //初始化序号 
        pageNum = (startIndex - 1) * 10 + 1;
        $.ajax({
            url: "/Common.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: {
                "PageName": "SystemSettings/RoleHandler.ashx",
                func: "GetUserDataByRoleId",
                roleid: selroleid,
                name: sername,
                PageIndex: startIndex,
                PageSize: 10,
                loginname: $("#<%=hid_LoginName.ClientID%>").val(),
                useridcard: $("#<%=hid_UserIDCard.ClientID%>").val()
            },
            success: OnSuccess,
            error:  //根据角色id获取用户数据-失败
                function (XMLHttpRequest, textStatus, errorThrown) {
                    $("#tb_UserList tbody").html("<tr><td class='NoContent' colspan='100'>无内容</td></tr>");
                }
        });
    }
    //根据角色id获取用户数据-成
    function OnSuccess(json) {
        if (json.result.errNum.toString() == "999") {
            $("#tb_UserList tbody").html("<tr><td class='NoContent' colspan='100'>无内容</td></tr>");
            $("#pageBar").hide();
        } else {
            $("#tb_UserList tbody").html("");
            $("#tr_User").tmpl(json.result.retData.PagedData).appendTo("#tb_UserList");
            //隔行变色以及鼠标移动高亮
            $(".main-bd table tbody tr").mouseover(function () {
                $(this).addClass("over");
            }).mouseout(function () {
                $(this).removeClass("over");
            })
            $(".main-bd table tbody tr:odd").addClass("alt");
            $("#pageBar").show();
            //生成页码条方法（方法对象,页码条容器，当前页码，总页数，页码组容量，总行数）
            makePageBar(getDataByRoleId, document.getElementById("pageBar"), json.result.retData.PageIndex, json.result.retData.PageCount, 8, json.result.retData.RowCount);
        }
    }


    <%--function BindSchoolData() {
        $.ajax({
            type: "post",
            url: "/Common.ashx?",
            async: false,
            dataType: "json",
            data: {
                "PageName": "SchoolHandler.ashx",
                func: "GetSchoolAll",
            },
            success: function (json) {
                $("#sel_school").empty();
                if (json.result.errNum.toString() != "0" && json.result.errNum.toString() != "999") {
                    layer.msg(json.result.errMsg);
                    return;
                }
                if (json.result.retData) {
                    $.each(json.result.retData, function (i, item) {
                        var option = "<option value='" + item.Id + "'>" + item.Name + "</option>"
                        $("#sel_school").append(option);
                    });
                    BindSystemBySchoolId($("#sel_school").val());
                }
            }
        });
    }
    //根据学校id绑定学校下的系统
    function BindSystemBySchoolId(schoolid) {
        $.ajax({
            url: "../Common.ashx?",
            type: "post",
            async: false,
            dataType: "json",
            data: {
                "PageName": "InterfaceConfig/SystemHandler.ashx",
                func: "GetSystemDataPage",
                ispage: false,
                schoolid: schoolid,
                PageIndex: 1,
                PageSize: 10,
                loginname: $("#<%=hid_LoginName.ClientID%>").val(),
                useridcard: $("#<%=hid_UserIDCard.ClientID%>").val()
            },
            success: function (json) {
                $("#sel_system").empty();
                if (json.result.errNum.toString() != "0" && json.result.errNum.toString() != "100") {
                    layer.msg(json.result.errMsg);
                    return;
                }
                if (json.result.retData.PagedData) {
                    $.each(json.result.retData.PagedData, function (i, item) {
                        var option = "<option value='" + item.SystemKey + "'>" + item.SystemName + "</option>";
                        $("#sel_system").append(option);
                    });
                    BindRoleTree($("#sel_system").val());//绑定角色树数据
                }
            }
        });
    }--%>
</script>
</html>

