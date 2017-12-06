<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PermissionSettings.aspx.cs" Inherits="SCWeb.SystemSettings.PermissionSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>权限设置</title>
    <link href="/css/stylebase.css" rel="stylesheet" />
    <link href="/css/commonbase.css" rel="stylesheet" />
    <link href="/css/iconfont.css" rel="stylesheet" />
    <link href="/css/animate.css" rel="stylesheet" />
    <link href="/Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/jquery.cookie.js"></script>
    <script src="/Scripts/Common.js?parm=1.01"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/tz_slider.js"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.all-3.5.min.js"></script>
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
    <input type="hidden" id="hid_Id" runat="server" />
    <input type="hidden" id="hid_UserIDCard" runat="server" />
    <input type="hidden" id="hid_LoginName" runat="server" />
    <!--tz_dialog start-->
    <div class="yy_dialog">
        <div class="t_content">
            <div class="yy_tab">
                <div class="content">
                    <div class="tc">
                        <div class="t_message">
                            <div class="message_con">
                                <form>
                                    <div class="menubox1 left_navcon fl" style="min-height:390px;width:36%;">
                                        <!--头部-->
                                        <h1><span class="tit_name">角色管理</span><%--<span class="fr btn22"><a href="javascript:;" onclick="addHoverDom();">添加角色</a></span>--%></h1>
                                        <!--菜单区域-->
                                        <div class="menu">
                                            <ul>
                                                <li>
                                                    <a class="menuclick1" href="#" style="text-align:left;"><i>-</i>全部</a>
                                                    <div class="zTreeDemoBackground left">
                                                        <ul id="tree_Role" class="ztree submenu1" style="padding: 0px;"></ul>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>
                                        <!--end 菜单区域-->
                                    </div>
                                    <div class="right_dcon fr" style="width:56%;min-height:390px;">
                                        <div class="zTreeDemoBackground left">
                                            <ul id="tree_Permission" class="ztree"></ul>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>
                        <div class="submit_btn" style="clear:both;">
                            <span class="Save_and_submit">
                                <input type="submit" value="确定" class="Save_and_submit" onclick="SavePermission();" /></span>
                            <span class="cancel">
                                <input type="submit" value="取消" class="cancel" onclick="javascript: parent.CloseIFrameWindow();" /></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--end tz_dialog-->

    <!--tz_yy start-->
    <div class="tz_yy"></div>
    <!--end tz_yy-->
</body>
<script type="text/javascript">
    var UrlDate = new GetUrlDate(); //实例化
    //zTree配置，添加/编辑/删除按钮需要引用jquery.ztree.exedit-3.5.min.js 或 jquery.ztree.all-3.5.min.js
    var rolesetting = {
        view: {
            showLine: false,
            showIcon: false,
            selectedMulti: false//,
            //addHoverDom: addHoverDom,  //添加增加按钮，必须与 removeHoverDom 同时使用。注意：需要手动添加按钮样式 .ztree li span.button.add
            //removeHoverDom: removeHoverDom //当鼠标移出节点时，隐藏按钮
        },
        edit: {
            enable: false,
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
    var persetting = {
        view: {
            dblClickExpand: false,
            showLine: false,
            selectedMulti: false
        },
        data: {
            simpleData: {
                enable: true,
                idKey: "id",
                pIdKey: "pId"
            }
        },
        check: {
            autoCheckTrigger: true,
            enable: true,
            chkStyle: "checkbox",
            chkboxType: { "Y": "ps", "N": "ps" }

        },
    };

    $(function () {
        BindRoleTreeAndInit();
    });
    //绑定角色树并且根据roleid判断是否加载权限
    function BindRoleTreeAndInit() {
        $.ajax({

            url: "/Common.ashx?Trandom=" + Math.random(),
            type: "get",
            data: {
                "PageName": "SystemSettings/RoleHandler.ashx",
                func: "GetRoleTreeData",
                loginname: $("#<%=hid_LoginName.ClientID%>").val(),
                useridcard: $("#<%=hid_UserIDCard.ClientID%>").val()
            },
            dataType: "JSON",
            async: false,
            cache: false,
            success: function (json) {
                var result = json.result;
                if (result.errNum == 0 && result.retData.length > 0) {
                    var zTreeNode = $.parseJSON(result.retData);
                    var zTreeObj = $.fn.zTree.init($("#tree_Role"), rolesetting, zTreeNode); //返回树对象
                    zTreeObj.expandNode(zTreeObj.getNodeByParam("id", 0, null), true, false, false); //展开第一个顶级节点
                    var roleid = $("#<%=hid_Id.ClientID%>").val();
                    if (roleid.length) {
                        var node = zTreeObj.getNodeByParam("id", roleid, null);
                        zTreeObj.selectNode(node);
                        $("#" + node.tId).addClass("selected").siblings().removeClass("selected");
                        BindPermTree(roleid);//绑定权限树
                    }
                } else {
                    layer.msg("您没有此权限!");
                }
            },
            error: function () {
                layer.msg("Ajax请求数据失败!");
            }
        });
    }
    //绑定角色树
    function BindRoleTree() {
        treeBind("tree_Role", "/Common.ashx", {
            "PageName": "SystemSettings/RoleHandler.ashx",
            func: "GetRoleTreeData",
            loginname: $("#<%=hid_LoginName.ClientID%>").val(),
            useridcard: $("#<%=hid_UserIDCard.ClientID%>").val()
        }, rolesetting);
    }
    function BindPermTree(roleid) {
        treeBind("tree_Permission", "/Common.ashx", {
            "PageName": "SystemSettings/MenuHandler.ashx",
            func: "GetMenuByRoleId",
            roleid: roleid,
            loginname: $("#<%=hid_LoginName.ClientID%>").val(),
            useridcard: $("#<%=hid_UserIDCard.ClientID%>").val()
        }, persetting);
    }
    //角色节点单击方法
    function zTreeOnClick(event, treeId, treeNode) {
        $("#" + treeNode.tId).addClass("selected").siblings().removeClass("selected");
        BindPermTree(treeNode.id);
    };

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
            url: "../Common.ashx",

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
            //error: OnErrorDelete
        });

        function OnSuccessDelete(json) {
            var result = json.result;
            if (result.errNum == 0) {
                layer.msg("删除成功！");
                BindRoleTree();//绑定角色树数据
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
        return newName.length > 0;
    }
    //编辑节点
    function zTreeOnRename(event, treeId, treeNode, isCancel) {
        SaveRole(treeNode.id.toString(), treeNode.name);
    }

    //增加节点   
    function addHoverDom(treeId, treeNode) {
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
    //当鼠标移出节点时，隐藏按钮
    function removeHoverDom(treeId, treeNode) {
        $("#addBtn_" + treeNode.tId).unbind().remove();
    };

    //保存角色
    function SaveRole(nodeid, nodename) {
        var func = nodeid.length ? "EditRole" : "AddRole";
        $.ajax({
            url:"/Common.ashx",

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
                    BindRoleTree();//绑定角色树数据
                } else {
                    layer.msg("操作失败！");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.msg("操作失败！");
            }
        });
    }

    //保存权限设置
    function SavePermission() {
        var nodeids = getChildNodes("tree_Permission");
        if (!nodeids.length) {
            layer.msg("请选择权限!");
        }
        var treeRole = $.fn.zTree.getZTreeObj("tree_Role");
        var selnodes = treeRole.getSelectedNodes();
        if (!selnodes.length) {
            layer.msg("请在左侧选中要设置权限的角色!");
            return;
        }
        $.ajax({
            url: "/Common.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: {
                "PageName": "SystemSettings/MenuHandler.ashx",

                func: "SetRoleMenu",
                roleid: selnodes[0].id,
                nodeids: nodeids,
                loginname: $("#<%=hid_LoginName.ClientID%>").val(),
                useridcard: $("#<%=hid_UserIDCard.ClientID%>").val()
            },
            success: function (json) {
                var result = json.result;
                if (result.errNum == 0) {
                    layer.msg("权限设置成功");
                    BindPermTree(selnodes[0].id);
                } else {
                    layer.msg(json.result.errMsg);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.msg("操作失败！");
            }
        });
    }
</script>
</html>
