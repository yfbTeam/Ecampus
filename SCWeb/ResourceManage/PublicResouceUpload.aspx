<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublicResouceUpload.aspx.cs" Inherits="SCWeb.ResourceManage.PublicResouceUpload" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="/Scripts/Uploadyfy/uploadify/uploadify.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/Uploadyfy/uploadify/jquery.uploadify-3.1.min.js"></script>
    <title>文件上传</title>
    <script type="text/javascript">
        var GetUrlDate = new GetUrlDate();
        $(document).ready(function () {
            var formData = {
                'FoldUrl': decodeURI(GetUrlDate.FoldUrl), 'GroupName': decodeURI(GetUrlDate.GroupName), 
                'ChapterID': GetUrlDate.ChapterID, "CreateUID": GetUrlDate.CreateUID
            };
            $("#uploadify").uploadify({
                'swf': '/Scripts/Uploadyfy/uploadify/uploadify.swf',
                'uploader': '/Synergy/FileUpload.ashx?Func=UploadPubResource',
                //'buttonCursor': 'hand',
                'buttonText': '选择文件',
                //'cancelImg': 'upload/uploadify-cancel.png',
                //'buttonImage': FirstUrl + '/_layouts/15/SVDigitalCampus/Image/bg.png',
                // 'folder': '/jxdBlog/photos',
                'auto': false,
                'multi': true,
                //'wmode': 'transparent',
                'queueID': 'fileQueue',
                'width': '100%',
                'height': 70,
                'fileSizeLimit': '100MB',
                'fileTypeDesc': '文件',       //图片选择描述  
                'queueSizeLimit': 10,                   //一个队列上传文件数限制  
                'removeTimeout': 1,                  //完成时清除队列显示秒数,默认3秒  
                'successTimeout': 20,                  //上传超时  
                //'fileTypeExts': '*.docx;*.doc;*.ppt;*.pptx;*.pdf;*.caj;*.txt;*.rar;*.zip;*.jpg;*.gif;',
                //'fileTypeDesc:': '请选择docx doc ppt pptx pdf caj txt rar zip jpg gif文件',
                'progressData': 'all',
                'formData': formData, //参数
                'removeCompleted': true,
                'overrideEvents': ['onUploadProgress'],
                onUploadSuccess: function (file, data, response) {
                    parent.getData(1, 10);
                },
                //上传汇总  
                'onUploadProgress': function (file, bytesUploaded, bytesTotal, totalBytesUploaded, totalBytesTotal) {
                    var persent = parseInt(100 * bytesUploaded / bytesTotal);

                    $("#" + file.id).find(".data").html(persent + "%");
                    $("#" + file.id).find(".uploadify-progress-bar").css("width", persent + "%");

                },
                //返回一个错误，选择文件的时候触发             
                'onUploadError': function (file, errorCode, errorMsg, errorString) {
                    alert('文件 ' + file.name + '上传失败: ' + errorString);
                },
                //检测FLASH失败调用              
                'onFallback': function () {
                    alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
                },
                'onAllComplete': function (event, data) {
                    alert(data.filesUploaded + '个图片上传成功');
                }

            });
        });

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
        <input type="hidden" id="HUserIdCard" value="<%=UserInfo.IDCard %>" />

        <input type="file" name="uploadify" id="uploadify" />
        <div class="fileup_w">
            <a href="javascript:$('#uploadify').uploadify('upload', '*')" class="fileup">开始上传</a>
            <a href="javascript:jQuery('#uploadify').uploadify('cancel')" class="fileup">取消上传</a>
        </div>
        <div class="T_list">
            <div id="fileQueue" style="width: 100%; height: auto; border: 2px solid #5493d7;"></div>
        </div>

    </form>
</body>
</html>

