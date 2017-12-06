<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubJectCatogory.aspx.cs" Inherits="SCWeb.ResourceManage.SubJectCatogory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>管理教材目录</title>
    <link href="/css/style.css" rel="stylesheet" />
    <link href="/css/common.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script id="tb_Unit" type="text/x-jquery-tmpl">
        <tr id="un_${ID}">
            <td>${pageIndex()}</td>
            <td><a href="javascript:getSonData('${ID}','${Name}');">${Name}</a></td>
            <td>
                <input type="button" name="delete" class="Topic_btn" value="删除" onclick="javascript: Delete('un_${ID}');" />
            </td>
        </tr>
    </script>
    <script id="tb_Section" type="text/x-jquery-tmpl">
        <tr id="se_${ID}">
            <td>${pageIndex()}</td>
            <td>${Name}</td>
            <td>
                <input type="button" name="delete" class="Topic_btn" value="删除" onclick="javascript: Delete('se_${ID}');" />
            </td>
        </tr>
    </script>
</head>
<body>
    <input type="hidden" id="hid_Id" runat="server" />
    <input type="hidden" id="hid_UserIDCard" runat="server" />
    <input type="hidden" id="hid_LoginName" runat="server" />
    <div class="yy_tab">
        <div class="content">
            <div class="tc">
                <div class="bar" id="tab"></div>
                <div class="t_message">
                    <table class="W_form" id="tb_CatalogList">
                        <thead>
                            <tr class="trth">
                                <th class="number">序号</th>
                                <th class="Project_name" style="width:60%;">目录</th>
                                <th>操作</th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
                <div class="submit_btn">
                    <span class="Save_and_submit">
                        <input type="submit" value="新建" class="Save_and_submit" onclick="Add();" /></span>
                    <span class="cancel">
                        <input type="submit" value="取消" class="cancel" onclick="javascript: parent.CloseIFrameWindow();" /></span>
                </div>
            </div>
        </div>
    </div>
</body>    
<script type="text/javascript">
    var UrlDate= new GetUrlDate();
    $(document).ready(function () {
        //获取数据
        getData();
    });
    var Pid = "0";//目录父ID，Pid=0
    //获取数据
    function getData() {
        $("#tab").html("<a href='javascript:getData();'>一级目录</a> > ");
        $.ajax({
            url: "/Common.ashx?Trandom=" + Math.random(),
            type: "post",
            dataType: "json",
            data: {
                "PageName": "/BookSubject/SubJect.ashx",
                "Func": "Chapator",
                SubID: UrlDate.SubID,
                PID: "0"
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
    function getSonData(id, name) {
        $.ajax({
            url: "/Common.ashx?Trandom=" + Math.random(),
            type: "post",
            dataType: "json",
            data: {
                "PageName": "/BookSubject/SubJect.ashx",
                "Func": "Chapator",
                SubID: UrlDate.SubID,
                PID: id
            },
            success: function (json) {
                Pid = id;
                if (json.result.errNum.toString() != "0") {
                    $("#tb_CatalogList tbody").html("<tr><td class='NoContent' colspan='100'>无内容</td></tr>");
                } else {
                    $("#tb_CatalogList tbody").html('');
                    pageNum = 1;
                    $("#tb_Section").tmpl(json.result.retData).appendTo("#tb_CatalogList");
                    $("#tab").append(name);
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
        if (Pid == "0") {
            randomId = "un_" + randomId;
        }
        else {
            randomId = "se_" + randomId;
        }
        var tbody = $("#tb_CatalogList tbody");
        if (tbody.text() == "无内容") {
            $(tbody).html(''); trCount = 0;
        }
        $(tbody).append("<tr id='" + randomId + "'><td>" + (trCount + 1) + "</td><td><input type='text' value='' /></td><td><input type='button' name='save' class='Topic_btn' value='保存' onclick=\"javascript: Save('" + randomId + "','');\" /></td></tr>");
    }
    function Save(id, name) {
        var tds = $("#" + id).find("td");
        var btnsave = $(tds).eq(2).find("input[name='save']");
        if (btnsave.val() == "保存") {
            var newname = $(tds).eq(1).find("input").val();
            if (newname != "") {
                id = id.substr(3, id.length);
                var fun = "UpdateCatalog";
                if (name == "") {
                    fun = "AddCatalog";
                }
                if (name != newname) { //更改或添加
                    $.ajax({
                        url: "/Common.ashx?Trandom=" + Math.random(),
                        type: "post",
                        dataType: "json",
                        data: {
                            "PageName": "/BookSubject/SubJect.ashx",
                            func: fun,
                            Id: id,
                            Name: newname,
                            SubID: UrlDate.SubID,
                            PID: Pid,
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
                $(tds).eq(1).html(newname);
                btnsave.val("编辑");
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
                func: "DelCatalog",
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

