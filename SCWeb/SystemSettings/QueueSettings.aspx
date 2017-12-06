<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueueSettings.aspx.cs" Inherits="SCWeb.SystemSettings.QueueSettings" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>队列维护</title>
    <link href="/css/style.css" rel="stylesheet" />
    <link href="/css/common.css" rel="stylesheet" />
    <link href="/css/iconfont.css" rel="stylesheet" />
    <link href="/css/animate.css" rel="stylesheet" />
    <link href="/Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.8.2.min.js"></script>
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
            <td><input type="checkbox" name="user_list" id="${IDCard}" value="${Id}" onclick="chkSingle(this);" /></td>
            <td>${pageIndex()}</td>
            <td>${Name}</td>
            <td>${LoginName}</td>
            <td>${Sex==0?'男':'女'}</td>
            <td>${UserState(State)}</td>
            <td>
                <input type="button" class="Topic_btn" value="删除" onclick="javascript:DeleteUserRelation('${Id}');" />
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
    <div class="Teaching_plan_management">
        <h1 class="Page_name">队列维护</h1>
        <div class="left_choice fl">
            <ul>
                <li class="Sear">
                    学校：<select id="sel_school" class="option" onchange="BindQueueTree(this.value);"></select>    
                    <select id="sel_role" class="option">
                        <option value='0'>教师</option>
                        <option value='1'>学生</option>
                    </select>
                    <input type="text" id="search_w" name="search_w" class="search_w" placeholder="请输入用户名" value="" /><a class="sea" href="#" onclick="SearchUser();">搜索</a>               
                </li>
            </ul>
        </div>
        <div class="right_add fr">
            <select id="sel_GroupList" class="option" style="width:110px;" onchange="AddUserGroup(this.value);"></select>
            <a class="add" href="#" onclick="BatchDelete();"><i class="iconfont">&#xe726;</i>批量删除</a>
        </div>
        <div style="clear:both;"></div>
        <div class="menubox1 left_navcon fl">
            <!--头部-->
            <h1><span class="tit_name">队列管理</span><span class="fr btn"><a href="#" onclick="addHoverDom();">添加队列</a></span></h1>
            <!--菜单区域-->
            <div class="menu">
                <ul>
                    <li>
                        <a class="menuclick1" href="#"><i>-</i>全部</a>
                        <div class="zTreeDemoBackground left">
                            <ul id="tree_Role" class="ztree submenu1" style="padding: 0px;"></ul>
                        </div>
                    </li>
                </ul>
            </div>
            <!--end 菜单区域-->
        </div>
        <div class="right_dcon fr">
            <div class="Administrator_settings" style="margin-top:10px;">
                <table class="W_form" id="tb_UserList">
                    <thead>
                        <tr class="trth">
                            <th class="number"><input type="checkbox" id="chkall" name="chkall" onclick="chkAll();" /></th>
                            <th class="number">序号</th>
                            <th class="Project_name">用户名</th>
                            <th class="">登录名</th>
                            <th class="">性别</th>
                            <th class="">用户状态</th>
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
</body>
<script type="text/javascript">
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
        BindSchoolData();//绑定学校
    });
    //绑定学校
    function BindSchoolData() {
        $.ajax({
            type: "post",
            url: "../Common.ashx?",
            async: false,
            dataType: "json",
            data: {
                "PageName": "SchoolHandler.ashx",
                func: "GetSchoolAll",
            },
            success: function (json) {
                $("#sel_school").empty();
                if (json.result.errNum.toString() != "0" && json.result.errNum.toString() != "100") {
                    layer.msg(json.result.errMsg);
                    return;
                }
                if (json.result.retData) {
                    $.each(json.result.retData, function (i, item) {
                        var option = "<option value='" + item.Id + "'>" + item.Name + "</option>"
                        $("#sel_school").append(option);
                    });
                    BindQueueTree($("#sel_school").val());//绑定队列树数据
                }
            }
        });
    }
    //绑定组
    function BindQueueTree(schoolId) {
        $.ajax({
            type: "get",
            url: "../Common.ashx",
            dataType: "JSON",
            data: {
                "PageName": "UserGroupHandler.ashx",
                func: "GetGroupTreeData",
                SystemKey: SystemKey,
                InfKey: InfKey,
                SchoolID: schoolId
            },
            async: false,
            cache: false,
            success: function (json) {
                var result = json.result;
                if (result.errNum == 0 && result.retData.length > 0) {
                    var zTreeNode = $.parseJSON(result.retData);
                    //绑定选项
                    $("#sel_GroupList").html("<option value=''>+加入队列</option>");
                    for(var i=0;i<zTreeNode.length;i++) {
                        var node=$(zTreeNode[i]);
                        $("#sel_GroupList").append("<option value='" + node[0].id + "'>" + node[0].name + "</option>");
                    }
                    //绑定组
                    $("#tree_Role").html("");
                    var zTreeObj = $.fn.zTree.init($("#tree_Role"), setting, zTreeNode); //返回树对象
                    zTreeObj.expandNode(zTreeObj.getNodeByParam("id", 0, null), true, false, false); //展开第一个顶级节点
                }
            }
        });
    }
    var selroleid = "";
    var sername = $("#search_w").val().trim();
    //查询用户方法
    function SearchUser() {
        selroleid = "";
        sername = $("#search_w").val().trim();
        var treeRole = $.fn.zTree.getZTreeObj("tree_Role");
        var selnodes = treeRole.getSelectedNodes();
        if (selnodes.length) {
            var li = $("#" + selnodes[0].tId).removeClass("selected");
            li.find("a").removeClass("curSelectedNode");
        }
        getDataByGroupId(1);
    }
    //组节点单击方法
    function zTreeOnClick(event, treeId, treeNode) {
        $("#" + treeNode.tId).addClass("selected").siblings().removeClass("selected");
        selroleid = treeNode.id;
        getDataByGroupId(1);
    };
    //根据队列id获取用户数据
    function getDataByGroupId(startIndex) {
        //初始化序号 
        pageNum = (startIndex - 1) * 10 + 1;
        var PageName = "TeacherHandler.ashx"; //教师
        var Function = "GetTeacherPageData";
        var role = $("#sel_role").val();
        if (role == "1") { //学生
            PageName = "StudentHandler.ashx";
            Function = "GetStudentPageData";
        }
        $.ajax({
            url: "../Common.ashx?Trandom=" + Math.random(),
            type: "get",
            async: false,
            dataType: "json",
            data: {
                "PageName": PageName,
                func: Function,
                SystemKey: SystemKey,
                InfKey: InfKey,
                Name: sername,
                PageIndex: startIndex,
                PageSize: 10,
                SchoolID: $("#sel_school").val(),
                GroupID: selroleid
            },
            success: OnSuccess,
            error: OnError
        });
    }
    //根据角色id获取用户数据-成
    function OnSuccess(json) {
        if (json.result.errNum.toString() != "0") {
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
            makePageBar(getDataByGroupId, document.getElementById("pageBar"), json.result.retData.PageIndex, json.result.retData.PageCount, 8, json.result.retData.RowCount);
        }
    }
    //根据角色id获取用户数据-失败
    function OnError(XMLHttpRequest, textStatus, errorThrown) {
        $("#tb_UserList tbody").html("<tr><td class='NoContent' colspan='100'>无内容</td></tr>");
    }
    //加入队列
    function AddUserGroup() {
        var groupId=$("#sel_GroupList").val();
        if (groupId != "") {//组不为空
            var userlist = $("#tb_UserList tbody").find("input:checked");
            if (userlist.length) {
                var idCard = "";
                for (var i = 0; i < userlist.length; i++) {
                    idCard = $(userlist[i]).attr("id");
                    if (idCard != "") {
                        $.ajax({ //加入人与队列的关系
                            url: "../Common.ashx?Trandom=" + Math.random(),
                            type: "post",
                            async: false,
                            dataType: "json",
                            data: {
                                "PageName": "UserGroupHandler.ashx",
                                func: "JoinGroup",
                                GroupId: groupId,
                                IDCard: idCard
                            },
                            success: function (json) {}
                        });
                    }
                }
                layer.msg("加入队列成功！");
            }
            else {
                layer.msg("请选择要加入此队列的成员！"); 
            }
        }
    }
    //全选/全不选
    function chkAll() {
        var chked = document.getElementById("chkall").checked;
        var singleChk = $("#tb_UserList").find("input[name='user_list']");
        for (var i = 0; i < singleChk.length; i++) {
            if (chked == undefined) {
                $(singleChk[i]).removeAttr("checked");
            } else {
                $(singleChk[i]).attr('checked', chked);
            }
        }
    }
    function chkSingle(chk) {
        var allchk = true;
        var tb = $(chk).parents("table");
        var singleChk = $(tb).find("input[name='user_list']");
        for (var i = 0; i < singleChk.length; i++) {
            var chked = singleChk.eq(i).attr("checked");
            if (chked == undefined) {
                allchk = false;
                break;
            }
        }
        var chkAll = $("#chkall");
        if (allchk == true) {
            $(chkAll).attr('checked', allchk);
        } else {
            $(chkAll).removeAttr("checked");
        }
    }
    //批量删除
    function BatchDelete() {
        var userlist = $("#tb_UserList tbody").find("input:checked");
        if (userlist.length) {
            layer.msg("确定要删除这些用户？", {
                time: 0 //不自动关闭
            , btn: ['确定', '取消']
            , yes: function (index) {
                layer.close(index);
                var value = "";
                for (var i = 0; i < userlist.length; i++) {
                    value = $(userlist[i]).val();
                    if (value != "") {
                        deleteUser(value, function (json) { });
                    }
                }
                getDataByGroupId(1); //刷新列表
            }
            });
        }
    }
    //删除成员
    function DeleteUserRelation(id) {
        layer.msg("确定要删除该用户？", {
            time: 0 //不自动关闭
            , btn: ['确定', '取消']
            , yes: function (index) {
                layer.close(index);
                if (id != "") {
                    deleteUser(id, function (json) {
                        var result = json.result;
                        if (result.errNum == 0) {
                            layer.msg("删除成功！");
                            getDataByGroupId(1); //刷新列表
                        } else {
                            layer.msg("删除失败！");
                        }
                    });
                }
            }
        });
    }
    function deleteUser(id,callback) {
        var PageName = "TeacherHandler.ashx"; //教师
        var Function = "DeleteTeacher";
        var role = $("#sel_role").val();
        if (role == "1") { //学生
            PageName = "StudentHandler.ashx";
            Function = "DeleteStudent";
        }
        $.ajax({
            url: "../Common.ashx?Trandom=" + Math.random(),
            async: false,
            dataType: "json",
            data: {
                "PageName": PageName,
                func: Function,
                SystemKey: SystemKey,
                InfKey: InfKey,
                Id: id
            },
            success: callback,
        });
    }
    //节点被删除之前的方法
    function beforeRemove(treeId, treeNode) {
        var zTree = $.fn.zTree.getZTreeObj("tree_Role");
        zTree.selectNode(treeNode);
        $("#" + treeNode.tId).addClass("selected").siblings().removeClass("selected");
        layer.msg("确认删除队列 " + treeNode.name + " 吗？", {
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
            type: "post",
            async: false,
            dataType: "json",
            data: {
                "PageName": "UserGroupHandler.ashx",
                func: "DeleteGroup",
                SystemKey: SystemKey,
                InfKey: InfKey,
                GroupId: treeNode.id
            },
            success: OnSuccessDelete,
            //error: OnErrorDelete
        });

        function OnSuccessDelete(json) {
            var result = json.result;
            if (result.errNum == 0) {
                layer.msg("删除成功！");
                BindQueueTree($("#sel_school").val());//绑定角色树数据
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
        SaveGroup(treeNode.id.toString(), treeNode.name);
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
    }
    //保存角色
    function SaveGroup(nodeid, nodename) {
        var func = nodeid.length ? "EditGroup" : "AddGroup";
        $.ajax({
            url: "../Common.ashx?Trandom=" + Math.random(),
            type: "post",
            async: false,
            dataType: "json",
            data: {
                "PageName": "UserGroupHandler.ashx",
                func: func,
                GroupId: nodeid,
                Name: nodename.trim(),
                SchoolID: $("#sel_school").val()
            },
            success: function (json) {
                var result = json.result;
                if (result.errNum == -1) {
                    layer.msg("该队列已存在！");
                    var zTree = $.fn.zTree.getZTreeObj("tree_Role");
                    var node = zTree.getNodeByParam("id", nodeid, null);
                    zTree.editName(node);
                }
                else if (result.errNum == 0) {
                    layer.msg("操作成功！");
                    BindQueueTree($("#sel_school").val());//绑定角色树数据
                } else {
                    layer.msg("操作失败！");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.msg("操作失败！");
            }
        });
    }
</script>
</html>
