<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditDormRoom.aspx.cs" Inherits="SCWeb.StuDormitory.EditDormRoom" %>
<!DOCTYPE html>
<html>
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>创建宿舍</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css"/>
    <link rel="stylesheet" href="/css/style.css"/>
    <link rel="stylesheet" href="/Scripts/choosen/prism.css" />
    <link rel="stylesheet" href="/Scripts/choosen/chosen.css" />
    <script src="/Scripts/jquery-1.8.3.min.js"></script>    
    <link href="/Scripts/Stu_upload/webuploader.css" rel="stylesheet" />
    <script src="/Scripts/Stu_upload/webuploader.min.js"></script>
    <script src="/Scripts/choosen/chosen.jquery.js"></script>
    <script src="/Scripts/choosen/prism.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
</head>
<body style="background: #fff;">
    <div id="some_file_queue" style="display: none;"></div>
    <div style="padding:20px 20px 50px 20px;">
        <div class="fl" style="width:50%;">
            <div class="row clearfix">
                <label for="" class="row_label fl">宿舍名称：</label>
                <div class="row_content">
                    <input type="text" id="txt_Name" class="text">
                </div>
            </div>
            <div class="row clearfix">
                <label for="" class="row_label fl">宿舍楼：</label>
                <div class="row_content">
                    <select class="select" id="sel_Build" style="width:210px;"></select>
                </div>
            </div>
            <div class="row clearfix">
                <label for="" class="row_label fl">楼层：</label>
                <div class="row_content">
                    <input type="number" id="txt_FloorNo" class="text">
                </div>
            </div>
            <div class="row clearfix">
                <label for="" class="row_label fl">床位数：</label>
                <div class="row_content">
                    <input type="radio" name="Beds" value="4"/>4
                    <input type="radio" name="Beds" value="6" checked="checked"/>6
                    <input type="radio" name="Beds" value="8"/>8
                 </div>
            </div>
        </div>
        <div class="fl" style="width:50%;">
            <div class="see_img">
                <img id="img_PicURL" src="" onerror="javascript:this.src='/images/assodefault.jpg'" alt=""/>
                <div class="upload" style="width:auto;">
                    <div id="filePicker">宿舍图片</div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <div class="row clearfix">
            <label for="" class="row_label fl">描述：</label>
            <div class="row_content">
                <textarea id="area_Introduce" name="area_Introduce" placeholder="请输入宿舍描述......" style="width: 100%; height: 200px;"></textarea>
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
            WebUploader.create({
                pick: '#filePicker',
                formData: { Func: "Upload_DormImg" },
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
                GetDormBuildingData(0);
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
                    PageName: "/StuDormitory/Dorm_RoomHandler.ashx",
                    Func: "GetDormRoomById",
                    ItemId: itemid
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var model = json.result.retData;
                        $("#txt_Name").val(model.Name);
                        $("#txt_FloorNo").val(model.FloorNo);
                        $("input[name='Beds'][value=" + model.Beds + "]").attr("checked", true);
                        $("#area_Introduce").val(model.Introduce);
                        $("#img_PicURL").attr("src", model.PicURL);
                        GetDormBuildingData(model);
                    }                    
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        //获取宿舍楼
        function GetDormBuildingData(model) {
            var $sel_Build = $("#sel_Build");
            $sel_Build.html('');
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuDormitory/Dorm_BuildingHandler.ashx",
                    Func: "GetDormBuildingDataPage",
                    ispage: false
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function (i, n) {
                            $sel_Build.append('<option value="' + n.Id + '">' + n.Name + '</option>');
                        });
                        if (model != 0) {
                            $sel_Build.val(model.BuildId);
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
            var buildId = $("#sel_Build").val();
            var floorNo = $("#txt_FloorNo").val().trim();
            var introduce = $("#area_Introduce").val().trim();            
            if (!name.length) { layer.msg("请填写宿舍名称！"); return; }
            if (!buildId) { layer.msg("请选择宿舍楼！"); return; }
            if (!floorNo.length) { layer.msg("请填写楼层！"); return; }
            if (!introduce.length) { layer.msg("请填写宿舍描述！"); return; }            
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuDormitory/Dorm_RoomHandler.ashx",
                    Func: UrlDate.itemid == 0 ? "AddDormRoom" : "EditDormRoom",
                    ItemId: UrlDate.itemid,
                    Name: name,                    
                    BuildId: buildId,
                    FloorNo:floorNo,
                    Beds: $("input[name='Beds']:checked").val(),
                    Introduce: introduce,
                    PicURL:$("#img_PicURL").attr("src"),
                    LoginUID: "<%=UserInfo.UniqueNo%>"
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == -1) {
                        layer.msg("该宿舍名称已存在!");
                    }
                    else if (result.errNum == 0) {
                        parent.layer.msg(UrlDate.itemid == 0 ? '创建宿舍成功!' : '修改宿舍成功!');
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
