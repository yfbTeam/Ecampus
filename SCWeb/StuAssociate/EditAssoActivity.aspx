<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditAssoActivity.aspx.cs" Inherits="SCWeb.StuAssociate.EditAssoActivity" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>发布活动</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css"/>
    <link rel="stylesheet" href="/css/style.css"/>
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/My97DatePicker/WdatePicker.js"></script>
<%--    <script src="/Scripts/Uploadyfy/uploadify/jquery.uploadify-3.1.min.js"></script>
    <link href="/Scripts/Uploadyfy/uploadify/uploadify.css" rel="stylesheet" />--%>
    <link href="/Scripts/Stu_upload/webuploader.css" rel="stylesheet" />
    <script src="/Scripts/Stu_upload/webuploader.min.js"></script>
    <script charset="utf-8" src="/Scripts/KindUeditor/kindeditor-min.js"></script>
    <script charset="utf-8" src="/Scripts/KindUeditor/plugins/code/prettify.js"></script>
    <script charset="utf-8" src="/Scripts/KindUeditor/lang/zh_CN.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
</head>
<body style="background: #fff;">
    <div style="padding:20px 20px 50px 20px;">
        <div class="fl" style="width:70%;">
            <div class="row clearfix">
                <label for="" class="row_label fl">活动名称：</label>
                <div class="row_content">
                    <input type="text" id="txt_Name" class="text" placeholder="请输入活动名称">
                </div>
            </div>
            <div class="row clearfix">
                <label for="" class="row_label fl">活动地址：</label>
                <div class="row_content">
                    <input type="text" id="txt_Address" class="text" placeholder="请输入活动地址">
                </div>
            </div>
            <div class="row clearfix">
                <label for="" class="row_label fl">活动时间：</label>
                <div class="row_content">
                    <input type="text" id="txt_StartTime" class="Wdate text" readonly="readonly" onclick="javascript: WdatePicker({ maxDate: '#F{$dp.$D(\'txt_EndTime\')}' });"/>
                    <span>~</span>
                    <input type="text" id="txt_EndTime" class="Wdate text" readonly="readonly" onclick="    javascript: WdatePicker({ minDate: '#F{$dp.$D(\'txt_StartTime\')||\'%y-%M-%d\'}' });"/>
                </div>
            </div>
        </div>
        <div class="fl" style="width:30%;">
            <div class="see_img" style="height:140px;width:99%;">
                <img id="img_ActivityImg" src="/images/defaultimg.jpg" onerror="javascript:this.src='/images/defaultimg.jpg'"  alt="">
                <input type="hidden" id="hid_ActivityImg" value="" />
                <div class="upload">
                    <div id="filePicker">活动封面</div>
                    <%--<input type="file" name="" id="uploadify" multiple="multiple" />--%>
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <div class="row clearfix">
            <label for="" class="row_label fl">描述：</label>
            <div class="row_content">
                <textarea id="area_Content" name="area_Content" placeholder="请输入活动描述..." style="width: 100%; height: 200px;"></textarea>
            </div>
        </div>
    </div>
    <div class="tools_bottom">
        <input type="button" class="keep" value="保存" onclick="SaveItem();">
        <input type="button" class="cancel" value="取消" onclick="javascript: parent.CloseIFrameWindow();">
    </div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        var curEditor;
        $(function () {
            KindEditor.ready(function (K) {
                curEditor = K.create('#area_Content', {
                    uploadJson: '/CommonHandler/Upload.ashx?Func=Upload_AssoActivityImg',
                    allowFileManager: false,//true时显示浏览服务器图片功能。
                    imageSizeLimit: '2MB', //批量上传图片单张最大容量
                    imageUploadLimit: 20, //批量上传图片同时上传最多个数
                    filterMode: true,
                    allowImageRemote: false,//网络图片
                    items: [
	                    'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', "strikethrough",
	                    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
	                    'insertunorderedlist', '|', 'undo', 'redo', '|', 'emoticons', 'image', 'multiimage', 'link'],
                    resizeType: 0,
                    //得到焦点事件
                    afterFocus: function () {
                        self.edit = edit = this; var strIndex = self.edit.html().indexOf("请输入活动描述..."); if (strIndex != -1) { self.edit.html(self.edit.html().replace("请输入活动描述...", "")); }
                    },
                    afterBlur: function () { this.sync(); self.edit = edit = this; if (self.edit.isEmpty()) { self.edit.html("请输入活动描述..."); } },   //关键  同步KindEditor的值到textarea文本框 
                    afterCreate: function () {
                        var self = this;
                        if (UrlDate.itemid == 0) { self.html("请输入活动描述..."); }
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
            //    'auto': true, //是否自动上传
            //    'queueID': 'some_file_queue',
            //    'swf': '/Scripts/Uploadyfy/uploadify/uploadify.swf',
            //    'uploader': '/CommonHandler/Upload.ashx',
            //    'formData': { Func: "Upload_AssoActivityImg" }, //参数
            //    'fileTypeExts': '*.jpg;*.jpeg;*.png;*.gif;*.bmp',   //文件类型限制,默认不受限制
            //    'buttonText': '活动封面',//按钮文字
            //    'width': 130,
            //    'height': 32,
            //    //最大文件数量'uploadLimit':
            //    'multi': false,//单选
            //    'fileSizeLimit': '20MB',//最大文档限制
            //    'queueSizeLimit': 1,  //队列限制
            //    'removeCompleted': true, //上传完成自动清空
            //    'removeTimeout': 0, //清空时间间隔
            //    //'overrideEvents': ['onDialogClose', 'onUploadSuccess', 'onUploadError', 'onSelectError'],
            //    'onUploadSuccess': function (file, data, response) {
            //        var json = $.parseJSON(data);
            //        $("#img_ActivityImg").attr("src", json.url);
            //        $("#hid_ActivityImg").val(json.url);
            //    }
            //});
            WebUploader.create({
                pick: '#filePicker',
                formData: { Func: "Upload_AssoActivityImg" },
                accept: {
                    title: 'Images',
                    extensions: 'gif,jpg,jpeg,bmp,png',
                    mimeTypes: 'image/*'
                },
                auto: true,
                chunked: false,
                chunkSize: 512 * 1024,
                server: '/CommonHandler/Upload.ashx',
                // 禁掉全局的拖拽功能。这样不会出现图片拖进页面的时候，把图片打开。
                disableGlobalDnd: true,
                fileNumLimit: 1,
                fileSizeLimit: 200 * 1024 * 1024,    // 200 M
                fileSingleSizeLimit: 50 * 1024 * 1024    // 50 M
            })
           .on('uploadSuccess', function (file, response) {
               var json = $.parseJSON(response._raw);
               $("#img_ActivityImg").attr("src", json.result.retData);
               $("#hid_ActivityImg").val(json.result.retData);
           }).onError = function (code) {
               switch (code) {
                   case 'exceed_size':
                       layer.msg('文件大小超出');
                       break;
                   case 'interrupt':
                       layer.msg('上传暂停');
                       break;
                   default:
                       layer.msg('错误: ' + code);
                       break;
               }
           };
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
                    PageName: "/StuAssociate/Asso_ActivityHandler.ashx",
                    Func: "GetAssoActivityById",
                    ItemId: itemid
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var model = json.result.retData;
                        $("#txt_Name").val(model.Name);
                        $("#txt_Address").val(model.Address);
                        $("#txt_StartTime").val(DateTimeConvert(model.StartTime, 'yyyy-MM-dd'));
                        $("#txt_EndTime").val(DateTimeConvert(model.EndTime, 'yyyy-MM-dd'));
                        $("#area_Content").val(model.Content);
                        $("#img_ActivityImg").attr("src", model.ActivityImg);
                        $("#hid_ActivityImg").val(model.ActivityImg);
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
            var address = $("#txt_Address").val().trim();
            var startTime = $("#txt_StartTime").val();
            var endTime = $("#txt_EndTime").val();
            var content = $("#area_Content").val().trim();
            if (!name.length) { layer.msg("请填写活动名称！"); return; }
            if (!address.length) { layer.msg("请填写活动地址！"); return; }
            if (!startTime) { layer.msg("请选择活动开始时间！"); return; }
            if (!endTime) { layer.msg("请选择活动结束时间！"); return; }
            if (!content.length || content == "请输入活动描述...") { layer.msg("请填写活动描述！"); return; }
            content = encodeURIComponent(content);
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_ActivityHandler.ashx",
                    Func: UrlDate.itemid == 0 ? "AddAssoActivity" : "EditAssoActivity",
                    AssoId: UrlDate.relid,
                    ItemId: UrlDate.itemid,
                    Name: name,
                    StartTime: startTime,
                    EndTime: endTime,
                    Address: address,
                    Content: content,
                    ActivityImg: $("#hid_ActivityImg").val(),
                    LoginUID: "<%=UserInfo.UniqueNo%>"
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == -1) {
                        layer.msg("该活动名称已存在!");
                    }
                    else if (result.errNum == 0) {
                        parent.layer.msg(UrlDate.itemid == 0 ? '创建活动成功!' : '修改活动成功!');
                        parent.GetAssoActivityData(1, 10);
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