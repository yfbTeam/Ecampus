<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VersionManage.aspx.cs" Inherits="SCWeb.ResourceManage.VersionManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>教材版本管理</title>
     <link rel="stylesheet" href="/css/reset.css" />
    <link href="/css/style.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script id="tb_Unit" type="text/x-jquery-tmpl">
        <tr id="un_${ID}">
            <td>${pageIndex()}</td>
            <td>${Name}</td>
            <td>
                 <a href="javascript:;" onclick="Delete('un_${ID}')">删除</a>
            </td>
        </tr>
    </script>

</head>
<body style="background:#fff;">
    <input type="hidden" id="hid_Id" runat="server" />
    <input type="hidden" id="hid_UserIDCard" runat="server" />
    <input type="hidden" id="hid_LoginName" runat="server" />
    <div style="padding:20px 0px 50px 0px;">
        <table class="table" id="tb_CatalogList">
            <thead>
                <tr class="trth">
                    <th class="number">序号</th>
                    <th class="Project_name" style="width: 60%;">版本名称</th>
                    <th>操作</th>
                </tr>
            </thead>
            <tbody></tbody>
        </table>
    </div>
    <div class="tools_bottom">
        <input type="button" class="keep" value="新建" onclick="Add();"/>
        <input type="button" class="cancel" value="取消" onclick="javascript: parent.CloseIFrameWindow();" />
    </div>
</body>
<script type="text/javascript">
    var UrlDate = new GetUrlDate();
    $(document).ready(function () {
        //获取数据
        getData();
    });
    //获取数据
    function getData() {
        $.ajax({
            url: "/Common.ashx?Trandom=" + Math.random(),
            type: "post",
            dataType: "json",
            data: {
                PageName: "/BookSubject/SubJect.ashx",
                "Func": "Version", IsPage: "false"
            },
            success: function (json) {
                if (json.result.errNum.toString() != "0") {
                    $("#tb_CatalogList tbody").html("<tr><td class='NoContent' colspan='100'>无内容</td></tr>");
                } else {
                    $("#tb_CatalogList tbody").html('');
                    pageNum = 1;
                    $("#tb_Unit").tmpl(json.result.retData).appendTo("#tb_CatalogList");
                    //隔行变色以及鼠标移动高亮
                    $(".main-bd table tbody tr").mouseover(function () {
                        $(this).addClass("over");
                    }).mouseout(function () {
                        $(this).removeClass("over");
                    })
                    $(".main-bd table tbody tr:odd").addClass("alt");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $("#tb_CatalogList tbody").html("<tr><td class='NoContent' colspan='100'>无内容</td></tr>");
            }
        });
    }

    function Add() {
        var trCount = $("#tb_CatalogList tbody").find("tr").length;
        var random = Math.random().toString();
        var randomId = random.substr(2, random.length);
        randomId = "un_" + randomId;
        var tbody = $("#tb_CatalogList tbody");
        if (tbody.text() == "无内容") {
            $(tbody).html(''); trCount = 0;
        }
        $(tbody).append("<tr id='" + randomId + "'><td>" + (trCount + 1) + "</td><td><input type='text' value='' class='text' style='width:100%;' autofocus/></td><td><input type=\"button\" value=\"保存\" class=\"btn bgblue\" onclick=\"javascript: Save('" + randomId + "','');\"></td></tr>");
    }
    function Save(id, name) {
        var tds = $("#" + id).find("td");
        var btnsave = $(tds).eq(2).find("input[name='save']");
        if (btnsave.val() == "保存") {
            var newname = $(tds).eq(1).find("input").val();
            if (newname != "") {
                id = id.substr(3, id.length);

                if (name != newname) { //更改或添加
                    $.ajax({
                        url: "/Common.ashx?Trandom=" + Math.random(),
                        type: "post",
                        dataType: "json",
                        data: {
                            "PageName": "/BookSubject/SubJect.ashx",
                            func: "AddVersion",
                            Id: id,
                            Name: newname,
                        },
                        success: function (json) {
                            if (json.result.errNum.toString() == "0") {
                                if (Pid == "0") {
                                    if (name == "") { //如果新添加则应改id为新加项的id
                                        id = json.result.retData;
                                    }
                                    newname = "<a href=\"javascript:getSonData('" + id + "');\">" + newname + "</a>";
                                }
                            }
                        }
                    });
                }
                else { //还原
                    if (Pid == "0") {
                        newname = "<a href=\"javascript:getSonData('" + id + "');\">" + name + "</a>";
                    }
                }
            }
        }
        else {
            var oldname = $(tds).eq(1).text();
            $(tds).eq(1).html("<input type='text' value='" + oldname + "' />");
            btnsave.val("保存");
        }
    }
    function Delete(id) {
        var caid = id.substr(3, id.length);
        $.ajax({
            url: "/Common.ashx?Trandom=" + Math.random(),
            type: "get",
            async: false,
            dataType: "json",
            data: {
                "PageName": "/BookSubject/SubJect.ashx",
                func: "DelVersion",
                Id: caid
            },
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    $("#" + id).remove();
                }
            }
        });
    }
</script>
</html>


