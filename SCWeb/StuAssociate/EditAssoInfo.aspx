﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditAssoInfo.aspx.cs" Inherits="SCWeb.StuAssociate.EditAssoInfo" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>创建社团</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <link rel="stylesheet" href="/Scripts/choosen/prism.css" />
    <link rel="stylesheet" href="/Scripts/choosen/chosen.css" />
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <%--<script src="/Scripts/Uploadyfy/uploadify/jquery.uploadify-3.1.min.js"></script>
    <link href="/Scripts/Uploadyfy/uploadify/uploadify.css" rel="stylesheet" />--%>
    <link href="/Scripts/Stu_upload/webuploader.css" rel="stylesheet" />
    <script src="/Scripts/Stu_upload/webuploader.min.js"></script>
    <script src="/Scripts/choosen/chosen.jquery.js"></script>
    <script src="/Scripts/choosen/prism.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
</head>
<body style="background: #fff;">
    <div id="some_file_queue" style="display: none;"></div>
    <div style="padding: 20px 20px 50px 20px;">
        <div class="fl" style="width: 50%;">
            <div class="row clearfix">
                <label for="" class="row_label fl">社团名称：</label>
                <div class="row_content">
                    <input type="text" id="txt_Name" class="text">
                </div>
            </div>
            <div class="row clearfix">
                <label for="" class="row_label fl">指定团长：</label>
                <div class="row_content">
                    <select id="sel_LeaderNo" class="select chosen-select" data-placeholder="请选择社团长" style="width: 210px;"></select>
                </div>
            </div>
            <div class="row clearfix">
                <label for="" class="row_label fl">社团分类：</label>
                <div class="row_content">
                    <select class="select" id="sel_AssoType" style="width: 210px;"></select>
                </div>
            </div>
            <%--<div class="row clearfix">
                <label for="" class="row_label fl">社团类型：</label>
                <div class="row_content">
                    <select class="select" id="sel_ApplyType" style="width:210px;">
                        <option value="0">申请加入</option>
                        <option value="1">公开</option>
                    </select>
                </div>
            </div>--%>
        </div>
        <div class="fl" style="width: 50%;">
            <div class="see_img">
                <img id="img_PicURL" src="" onerror="javascript:this.src='/images/assodefault.jpg'" alt="" />
                <div class="upload" style="width:auto;">
                    <div id="filePicker">社团图片</div>
                    <%--<input type="file" name="" id="uploadify" multiple="multiple" />--%>
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <div class="row clearfix">
            <label for="" class="row_label fl">描述：</label>
            <div class="row_content">
                <textarea id="area_Introduce" name="area_Introduce" placeholder="请输入社团描述......" style="width: 100%; height: 200px;"></textarea>
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
            //$("#uploadify").uploadify({
            //    'auto': true, //是否自动上传
            //    'queueID': 'some_file_queue',
            //    'swf': '/Scripts/Uploadyfy/uploadify/uploadify.swf',
            //    'uploader': '/CommonHandler/Upload.ashx',
            //    'formData': { Func: "Upload_AssoImg" }, //参数
            //    'fileTypeExts': '*.jpg;*.jpeg;*.png;*.gif;*.bmp',   //文件类型限制,默认不受限制
            //    'buttonText': '社团图片',//按钮文字
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
            //        $("#img_PicURL").attr("src", json.url);
            //    },
            //});
            WebUploader.create({
                pick: '#filePicker',
                formData: { Func: "Upload_AssoImg" },
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
               $("#img_PicURL").attr("src", json.result.retData);
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
            } else {
                GetUserInfoData(0);
                GetAssoType(0);
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
                    PageName: "/StuAssociate/Asso_InfoHandler.ashx",
                    Func: "GetAssoInfoById",
                    ItemId: itemid
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var model = json.result.retData;
                        $("#txt_Name").val(model.Name);
                        //$("#sel_ApplyType").val(model.ApplyType);
                        $("#area_Introduce").val(model.Introduce);
                        $("#img_PicURL").attr("src", model.PicURL);
                        GetUserInfoData(model);
                        GetAssoType(model);
                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        //获取用户
        function GetUserInfoData(model) {
            var $sel_LeaderNo = $("#sel_LeaderNo");
            $sel_LeaderNo.html('');
            $.ajax({
                url: "/CommonHandler/UnifiedHelpHandler.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { "Func": "GetUserInfoData" },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function (i, n) {
                            $sel_LeaderNo.append('<option value="' + n.UniqueNo + '">' + n.Name + '</option>');
                        });
                        if (model != 0) {
                            $sel_LeaderNo.val(model.LeaderNo);
                        }
                        $sel_LeaderNo.chosen({
                            allow_single_deselect: true,
                            disable_search_threshold: 6,
                            no_results_text: '未找到',
                            search_contains: true,
                            width: '95%'
                        });
                        //$sel_LeaderNo.trigger("chosen:updated");//--新版本
                        //$sel_LeaderNo.trigger("liszt:updated");//--旧版本 动态更新select下的选择项时，只要在更新选择项后触发Chosen中的liszt:updated事件就可以了
                        //$sel_LeaderNo.chosen();
                    } else { }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        //获取社团分类
        function GetAssoType(model) {
            var $sel_AssoType = $("#sel_AssoType");
            $sel_AssoType.html('');
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_TypeHandler.ashx",
                    Func: "GetAssoTypeDataPage",
                    ispage: false
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function (i, n) {
                            $sel_AssoType.append('<option value="' + n.Id + '">' + n.Name + '</option>');
                        });
                        if (model != 0) {
                            $sel_AssoType.val(model.AssoType);
                        }
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
            var introduce = $("#area_Introduce").val().trim();
            var leaderNo = $("#sel_LeaderNo").val();
            var assoType = $("#sel_AssoType").val();
            if (!name.length) { layer.msg("请填写社团名称！"); return; }
            if (!introduce.length) { layer.msg("请填写社团描述！"); return; }
            if (!leaderNo) { layer.msg("请选择社团长！"); return; }
            if (!assoType) { layer.msg("请选择社团分类！"); return; }
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_InfoHandler.ashx",
                    Func: UrlDate.itemid == 0 ? "AddAssoInfo" : "EditAssoInfo",
                    ItemId: UrlDate.itemid,
                    Name: name,
                    Introduce: introduce,
                    LeaderNo: leaderNo,
                    AssoType: assoType,
                    //ApplyType: $("#sel_ApplyType").val(),
                    PicURL: $("#img_PicURL").attr("src"),
                    LoginUID: "<%=UserInfo.UniqueNo%>"
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == -1) {
                        layer.msg("该社团名称已存在!");
                    }
                    else if (result.errNum == 0) {
                        parent.layer.msg(UrlDate.itemid == 0 ? '创建社团成功!' : '修改社团成功!');
                        parent.getData(1, 10);
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
