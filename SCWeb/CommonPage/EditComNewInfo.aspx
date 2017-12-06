<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditComNewInfo.aspx.cs" Inherits="SCWeb.CommonPage.EditComNewInfo" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>发布通知</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css"/>
    <link rel="stylesheet" href="/css/style.css"/>
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/Uploadyfy/uploadify/jquery.uploadify-3.1.min.js"></script>
    <link href="/Scripts/Uploadyfy/uploadify/uploadify.css" rel="stylesheet" />
    <script charset="utf-8" src="/Scripts/KindUeditor/kindeditor-min.js"></script>
    <script charset="utf-8" src="/Scripts/KindUeditor/plugins/code/prettify.js"></script>
    <script charset="utf-8" src="/Scripts/KindUeditor/lang/zh_CN.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
</head>
<body style="background: #fff;">
    <div style="padding:20px 20px 50px 20px;">
        <div class="row clearfix">
            <label for="" class="row_label fl">名称：</label>
            <div class="row_content">
                <input id="txt_Name" type="text" class="text">
            </div>
        </div>
        <div class="row clearfix">
            <label for="" class="row_label fl">内容：</label>
            <div class="row_content">
                <textarea id="area_Content" name="area_Content" placeholder="请填写内容..." style="width: 100%; height: 300px;"></textarea>
            </div>
        </div>
        <%--<div class="row clearfix">
            <label for="" class="row_label fl">附件：</label>
            <div class="row_content">
                <input type="file" name="" id="uploadify" multiple="multiple" />
            </div>
        </div>--%>
    </div>
    <div class="tools_bottom">
        <input type="button" class="keep" value="保存" onclick="SaveItem();">
        <input type="button" class="cancel" value="取消" onclick="javascript: parent.CloseIFrameWindow();">
    </div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        var curEditor;
        $(function () {
            var itemid = UrlDate.itemid;
            if (itemid != 0) {
                GetItemById(itemid);
            }            
            KindEditor.ready(function (K) {
                curEditor = K.create('#area_Content', {
                    uploadJson: '/CommonHandler/Upload.ashx?Func=Upload_NewInfoPic',
                    allowFileManager: false,//true时显示浏览服务器图片功能。
                    imageSizeLimit: '2MB', //批量上传图片单张最大容量
                    imageUploadLimit: 20, //批量上传图片同时上传最多个数
                    filterMode: true,
                    allowImageRemote: false,//网络图片
                    items: [
	                    'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', "strikethrough",
	                    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
	                    'insertunorderedlist', '|', 'undo', 'redo', '|', 'emoticons', 'image','multiimage', 'link'],
                    resizeType: 0,
                    //得到焦点事件
                    afterFocus: function () {
                        self.edit = edit = this; var strIndex = self.edit.html().indexOf("请填写内容..."); if (strIndex != -1) { self.edit.html(self.edit.html().replace("请填写内容...", "")); }
                    },
                    afterBlur: function () { this.sync(); self.edit = edit = this; if (self.edit.isEmpty()) { self.edit.html("请填写内容..."); } },   //关键  同步KindEditor的值到textarea文本框 
                    afterCreate: function () {
                        var self = this;
                        if (UrlDate.itemid == 0) { self.html("请填写内容..."); }
                        K.ctrl(document, 13, function () {
                            K('form[name=example]')[0].submit();
                        });
                        K.ctrl(self.edit.doc, 13, function () {
                            self.sync();
                            K('form[name=example]')[0].submit();

                        });
                    }
                });
            });
            //$("#uploadify").uploadify({
            //    'auto': true,                      //是否自动上传
            //    'swf': '../Scripts/Uploadyfy/uploadify/uploadify.swf',
            //    'uploader': 'Uploade.ashx',
            //    'formData': { Func: "UplodExcel" }, //参数
            //    'fileTypeExts': '*.xls;*.xlsx',
            //    'buttonText': '选择Excel',//按钮文字
            //    // 'cancelimg': 'uploadify/uploadify-cancel.png',
            //    'width': 130,
            //    'height': 32,
            //    //最大文件数量'uploadLimit':
            //    'multi': false,//单选
            //    'fileSizeLimit': '10MB',//最大文档限制
            //    'queueSizeLimit': 1,  //队列限制
            //    'removeCompleted': true, //上传完成自动清空
            //    'removeTimeout': 0, //清空时间间隔
            //    //'overrideEvents': ['onDialogClose', 'onUploadSuccess', 'onUploadError', 'onSelectError'],
            //    'onUploadSuccess': function (file, data, response) {
            //        var json = $.parseJSON(data);
            //        $("#weike").attr("src", json.url);
            //    },

            //});
        });
        //保存信息
        function SaveItem() {
            var name = $("#txt_Name").val().trim();
            var content = $("#area_Content").val().trim();            
            if (!name.length) {layer.msg("请填写名称！");return;}
            if (!content.length || content == "请填写内容...") { layer.msg("请填写内容！"); return; }
            content = encodeURIComponent(content);
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/CommonHandler/Com_NewHandler.ashx",
                    Func: UrlDate.itemid == 0 ? "AddComNewInfo" : "EditComNewInfo",
                    Type: UrlDate.type,
                    RelationId: UrlDate.relid,
                    ItemId: UrlDate.itemid,
                    Name: name,
                    NewType:UrlDate.newtype,
                    Content:content,                        
                    LoginUID:"<%=UserInfo.UniqueNo%>"	               
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == -1) {
                        layer.msg("该名称已存在!");
                    }
                    else if (result.errNum == 0) {
                        parent.layer.msg(UrlDate.itemid == 0 ? '发布成功!' : '修改成功!');
                        if (UrlDate.itemid == 0) {
                            if (UrlDate.relid != 0) {
                                if (UrlDate.isdetail == 1) {
                                    parent.window.location.href = '/CommonPage/NewInfoDetail.aspx?type=' + UrlDate.type + '&newtype=' + UrlDate.newtype + '&itemid=' + result.retData + '&nav=' + UrlDate.nav;
                                } else {
                                    parent.GetComNewInfoData(1, 10);
                                }                                
                            }
                        } else {
                            parent.GetComNewInfoData(1, 10);
                        }                                                                     
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
        //绑定数据
        function GetItemById(itemid) {
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/CommonHandler/Com_NewHandler.ashx",
                    Func: "GetComNewInfoById",
                    ItemId: itemid
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var model = json.result.retData;
                        $("#txt_Name").val(model.Name);
                        $("#area_Content").val(model.Content);
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
    </script>    
</body>
</html>
