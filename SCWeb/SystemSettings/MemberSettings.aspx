<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberSettings.aspx.cs" Inherits="SCWeb.SystemSettings.MemberSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>成员设置</title>
    <link href="/css/stylebase.css" rel="stylesheet" />
    <link href="/css/commonbase.css" rel="stylesheet" />
    <link href="/css/iconfont.css" rel="stylesheet" />
    <link href="/css/animate.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/jquery.cookie.js"></script>
    <script src="/Scripts/Common.js?parm=1.01"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/PageBar.js"></script>
    <script id="tr_User" type="text/x-jquery-tmpl">
        <tr>
            <td>
                <input type="checkbox" name="cb_sub" /></td>
            <td>${pageIndex()}</td>
            <td>${Name}</td>
            <td>${LoginName}</td>
            <td>${Sex==1?'男':'女'}</td>
            <td>${UserType}</td>
            <td style="display: none;">${IDCard}</td>
        </tr>
    </script>
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
                        <div class="Operation_area">
                            <div class="left_choice fl">
                                <ul>
                                    <li class="Sear">
                                        <select id="sel_Type" class="option" onchange="GetNotDataByRoleId(1)">
                                            <option id="0">学生</option>
                                            <option id="1" selected="selected">老师</option>\
                                            <%--<option id="3" selected="selected">普通用户</option>--%>
                                        </select>
                                        <input type="text" id="search_w" name="search_w" class="search_w" placeholder="请输入用户名" value="" /><a class="sea" href="#" onclick="SearchUser();">搜索</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="Honor_management">
                            <table class="W_form" id="tb_UserList" style="background: white;">
                                <thead>
                                    <tr class="trth">
                                        <th class="cbox">
                                            <input type="checkbox" id="cb_all" name="cb_all" onclick="CheckAll(this);" /></th>
                                        <th class="number">序号</th>
                                        <th class="Project_name">用户名</th>
                                        <th class="">登录名</th>
                                        <th class="">性别</th>
                                        <th class="">用户类型</th>
                                        <th style="display: none;"></th>
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
                        <div class="submit_btn">
                            <span class="Save_and_submit">
                                <input type="submit" value="确定" class="Save_and_submit" onclick="SaveMemberSetting();" /></span>
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
    var sername = $("#search_w").val().trim();
    $(document).ready(function () {
        //获取数据
        GetNotDataByRoleId(1);
    });
    //全选或全不选
    function CheckAll(obj) {
        var flag = obj.checked;//判断全选按钮的状态 
        $("input[type=checkbox][name=cb_sub]").each(function () {//查找每一个name为cb_sub的checkbox 
            this.checked = flag;//选中或者取消选中 
        });
    }
    function SearchUser() {
        sername = $("#search_w").val().trim();
        GetNotDataByRoleId(1);
    }
    //获取数据
    function GetNotDataByRoleId(startIndex) {
        var roleid = $("#<%=hid_Id.ClientID%>").val();
        //初始化序号 
        pageNum = (startIndex - 1) * 10 + 1;
        $.ajax({
            url: "/Common.ashx?Trandom=" + Math.random(),
            type: "get",
            async: false,
            dataType: "json",
            data: {
                "PageName": "SystemSettings/RoleHandler.ashx",
                func: "GetNotDataByRoleId",
                roleid: roleid,
                name: sername,
                PageIndex: startIndex,
                PageSize: 10,
                loginname: $("#<%=hid_LoginName.ClientID%>").val(),
                useridcard: $("#<%=hid_UserIDCard.ClientID%>").val(),
                sel_Type: $("#sel_Type").val()
            },
            success: OnSuccess,
            error: OnError
        });

    }
    function OnSuccess(json) {
        if (json.result.errNum.toString() == "999") {
            $(".paging").hide();
            $("#tb_UserList tbody").html("<tr><td class='NoContent' colspan='100'>无内容</td></tr>");
        } else {
            $("#tb_UserList tbody").html('');
            $("#tr_User").tmpl(json.result.retData.PagedData).appendTo("#tb_UserList");
            //隔行变色以及鼠标移动高亮
            $(".main-bd table tbody tr").mouseover(function () {
                $(this).addClass("over");
            }).mouseout(function () {
                $(this).removeClass("over");
            })
            $(".main-bd table tbody tr:odd").addClass("alt");
            //生成页码条方法（方法对象,页码条容器，当前页码，总页数，页码组容量，总行数）
            makePageBar(GetNotDataByRoleId, document.getElementById("pageBar"), json.result.retData.PageIndex, json.result.retData.PageCount, 8, json.result.retData.RowCount);
        }
    }
    function OnError(XMLHttpRequest, textStatus, errorThrown) {
        $("#tb_UserList tbody").html("<tr><td class='NoContent' colspan='100'>无内容</td></tr>");
    }

    //保存成员设置
    function SaveMemberSetting() {
        var idcardArray = [];
        var leg = $("#tb_UserList tr").length - 1  //$("#thistab tr").length是获取table的行数
        $("#tb_UserList tr:gt(0):lt(" + leg + ")").each(function () {  //gt（0）代表是大于第一行，从第二行起;  lt（10）代表小于；
            if ($(this).children("td").eq(0).children("input")[0].checked) {
                temp = $(this).children("td").eq(6)[0].innerHTML; //获取IDCard
                if (temp.length > 0) {
                    idcardArray.push(temp);
                }
            }
        });
        if (!idcardArray.length) {
            layer.msg("请选择要添加的成员!");
            return false;
        }
        var itemid = $("#<%=hid_Id.ClientID%>").val();
        $.ajax({
            url: "/Common.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: {
                "PageName": "SystemSettings/RoleHandler.ashx",
                func: "SetRoleMember",
                roleid: itemid,
                idcardStr: idcardArray.join(","),
                loginname: $("#<%=hid_LoginName.ClientID%>").val(),
                useridcard: $("#hid_UserIDCard").val()
            },
            success: function (json) {
                if (json.result.errNum == 0) {
                    parent.layer.msg("操作成功！");
                    parent.getDataByRoleId(1);
                    parent.CloseIFrameWindow();
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

