<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyDrive.aspx.cs" Inherits="SCWeb.Synergy.MyDrive" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="pragma" content="no-cache" />
    <title></title>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    <link href="/Scripts/zTree/css/zTreeStyle/zTreeStyle.css" rel="stylesheet" />

    <script src="/Scripts/PageBar.js"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.core-3.5.js"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.excheck-3.5.js"></script>
    <script src="/Scripts/zTree/js/jquery.ztree.exedit-3.5.js"></script>

    <script type="text/javascript">
        var setting = {
            view: {
                selectedMulti: false
            },
            data: {
                keep: {
                    parent: true,
                    leaf: true
                },
                simpleData: {
                    enable: true
                }
            },
            callback: {
                onClick: onClick
            }
        };

        var log, className = "dark";
        function beforeClick(treeId, treeNode, clickFlag) {
            className = (className === "dark" ? "" : "dark");
            showLog("[ " + getTime() + " beforeClick ]&nbsp;&nbsp;" + treeNode.name);
            return (treeNode.click != false);
        }
        function onClick(event, treeId, treeNode, clickFlag) {
            $("#Url").val(treeNode.root);
            $("#Pid").val(treeNode.id);

            //showLog("[ " + getTime() + " onClick ]&nbsp;&nbsp;clickFlag = " + clickFlag + " (" + (clickFlag === 1 ? "普通选中" : (clickFlag === 0 ? "<b>取消选中</b>" : "<b>追加选中</b>")) + ")");
        }
        function showLog(str) {
            if (!log) log = $("#log");
            log.append("<li class='" + className + "'>" + str + "</li>");
            if (log.children("li").length > 8) {
                log.get(0).removeChild(log.children("li")[0]);
            }
        }
        function getTime() {
            var now = new Date(),
            h = now.getHours(),
            m = now.getMinutes(),
            s = now.getSeconds();
            return (h + ":" + m + ":" + s);
        }
        function treeNode() {

        }
        $(document).ready(function () {

            //var json="{{ "id":0,"root":"#", "pId": 0, "name":"根目录", "open":"true"},            {"id": 24,"root":"/DriveFolder/图片635968556777630935", "pId": 0, "name":"图片"},            {"id": 23,"root":"/DriveFolder/文件夹二635968556693004749", "pId": 0, "name":"文件夹二"},            {"id": 22,"root":"/DriveFolder/文件夹一635968556578548806", "pId": 0, "name":"文件夹一"},            {"id": 21,"root":"/DriveFolder/文件夹一635968556532238039", "pId": 0, "name":"文件夹一"}        }"
            //$.fn.zTree.init($("#treeDemo"), setting, $.parseJSON(returnVal));
            $.ajax({
                type: "get",
                url: "/Common.ashx",
                dataType: "text",
                data: { "PageName": "Synergy/SkyDrivePersonal.ashx", "Func": "TreeNodes", CreateUID: $("#HUserIdCard").val() },
                success: function (returnVal) {
                    $.fn.zTree.init($("#treeDemo"), setting, $.parseJSON(returnVal));
                },
                error: function (errMsg) {
                    alert('数据加载失败！');
                }
            });
        });
        var GetUrlDate = new GetUrlDate();
        function treeClick() {
            this.close();
            var Url = $("#Url").val();
            var Pid = $("#Pid").val();
            if (Url == undefined || Pid == undefined || Url == "" || Pid == "") {
                layerMsg("请选择要移动的目录");
            }
            else {
                parent.MoveMore($("#Url").val(), $("#Pid").val(), GetUrlDate.code, GetUrlDate.id);
            }
        }
        function GetRequest() {
            var url = location.search; //获取url中"?"符后的字串

            var theRequest = new Object();

            if (url.indexOf("?") != -1) {

                var str = url.substr(1);

                strs = str.split("&");

                for (var i = 0; i < strs.length; i++) {

                    theRequest[strs[i].split("=")[0]] = (strs[i].split("=")[1]);

                }

            }

            return theRequest;

        }
    </script>


</head>
<body>

    <form id="form1" runat="server">
        <div class="content_wrap">
            <input type="hidden" id="HUserIdCard" value="<%=UserInfo.IDCard %>"/>
            <input id="Url" type="hidden" />
            <input id="Pid" type="hidden" />

            <div class="zTreeDemoBackground left">
                <ul id="treeDemo" class="ztree"></ul>
                <input type="button" value="确定" onclick='treeClick()' style="background: rgb(84, 147, 215); margin: 10px; border-radius: 3px; width: 100px; height: 34px; text-align: center; color: white;" />
            </div>

        </div>

    </form>
</body>
</html>