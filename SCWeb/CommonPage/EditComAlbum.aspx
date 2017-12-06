<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditComAlbum.aspx.cs" Inherits="SCWeb.CommonPage.EditComAlbum" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>创建相册</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css"/>
    <link rel="stylesheet" href="/css/style.css"/>
    <script src="/Scripts/jquery-1.8.3.min.js"></script>    
    <script charset="utf-8" src="/Scripts/KindUeditor/kindeditor-min.js"></script>
    <script charset="utf-8" src="/Scripts/KindUeditor/plugins/code/prettify.js"></script>
    <script charset="utf-8" src="/Scripts/KindUeditor/lang/zh_CN.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
</head>
<body style="background: #fff;">
    <div style="padding:20px 20px 50px 20px;">
        <div class="row">
            <label for="" class="row_label fl">相册名称：</label>
            <div class="row_content">
                <input type="text" id="txt_Name" class="text" placeholder="请输入相册名称" />
            </div>
        </div>
        <div class="row">
            <label for="" class="row_label fl">相册描述：</label>
            <div class="row_content">
                <textarea id="area_Description" name="area_Description" placeholder="描述" style="width: 100%; height: 200px;"></textarea>
            </div>
        </div>
    </div>
    <div class="tools_bottom">
        <input type="button" class="keep" value="保存" onclick="SaveItem();">
        <input type="button" class="cancel" value="取消" onclick="javascript: parent.CloseIFrameWindow();">
    </div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {            
            var itemid = UrlDate.itemid;
            if (itemid != 0) {
                GetItemById(itemid);
            }           
        });
        //绑定数据
        function GetItemById(itemid) {
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/CommonHandler/Com_AlbumHandler.ashx",
                    Func: "GetComAlbumById",
                    ItemId: itemid
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var model = json.result.retData;
                        $("#txt_Name").val(model.Name);
                        $("#area_Description").val(model.Description);
                    }                    
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        //保存信息
        function SaveItem() {
            var name = $("#txt_Name").val().trim();
            var description = $("#area_Description").val().trim();           
            if (!name.length) { layer.msg("请填写相册名称！"); return; }
            if (!description.length) { layer.msg("请填写相册描述！"); return; }            
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/CommonHandler/Com_AlbumHandler.ashx",
                    Func: UrlDate.itemid == 0 ? "AddComAlbum" : "EditComAlbum",
                    ItemId: UrlDate.itemid,
                    Type: UrlDate.type,
                    RelationId: UrlDate.relid,
                    ItemId: UrlDate.itemid,
                    Name: name,
                    Description: description,
                    LoginUID:"<%=UserInfo.UniqueNo%>"	
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == -1) {
                        layer.msg("该相册名称已存在!");
                    }
                    else if (result.errNum == 0) {
                        parent.layer.msg(UrlDate.itemid == 0 ? '创建相册成功!' : '修改相册成功!');
                        parent.GetComAlbumData(1, 10);
                        parent.CloseIFrameWindow();
                    } else {
                        layer.msg(result.errMsg);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.msg("操作失败！");
                }
            });
        }             
    </script>
</body>
</html>