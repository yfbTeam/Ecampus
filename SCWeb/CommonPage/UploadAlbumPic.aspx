<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadAlbumPic.aspx.cs" Inherits="SCWeb.CommonPage.UploadAlbumPic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>上传照片</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <link rel="stylesheet" href="/Scripts/choosen/prism.css" />
    <link rel="stylesheet" href="/Scripts/choosen/chosen.css" />
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/choosen/chosen.jquery.js"></script>
    <script src="/Scripts/choosen/prism.js"></script>
    <link href="/Scripts/Stu_upload/upstyle.css" rel="stylesheet" />
    <link href="/Scripts/Stu_upload/webuploader.css" rel="stylesheet" />
    <script src="/Scripts/Stu_upload/webuploader.min.js"></script>
    <script src="/Scripts/Stu_upload/upload.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
</head>
<body style="background: #fff;">
    <form id="form1" runat="server">
        <input type="hidden" id="hid_Album" />
        <input type="hidden" id="HLoginUID" value="<%=UserInfo.UniqueNo%>" />
        <div style="padding: 20px; border-bottom: 1px solid #E5EBEC">
            <div class="row">
                <label for="" class="row_label fl">上传到：</label>
                <div class="row_content">
                    <select id="sel_Album" class="text chosen-select" onchange="SetAlbumHid();"></select>
                </div>
            </div>
        </div>
        <div id="uploader" style="padding: 0px 20px 50px 20px;">
            <div class="queueList">
                <div id="dndArea" class="placeholder photo_lists">
                    <div id="filePicker"></div>
                </div>
            </div>
            <div class="statusBar" style="display: none;">
                <div class="progress">
                    <span class="text">0%</span>
                    <span class="percentage"></span>
                </div>
                <div class="info"></div>
                <div class="btns">
                    <div id="filePicker2"></div>
                    <div class="uploadBtn">开始上传</div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            GetComAlbumData(1, 10);
        });
        //加载社团相册
        function GetComAlbumData(startIndex, pageSize) {
            $sel_Album = $("#sel_Album");
            $sel_Album.html('');
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/CommonHandler/Com_AlbumHandler.ashx",
                    Func: "GetComAlbumDataPage",
                    Type: UrlDate.type,
                    RelationId: UrlDate.relid,
                    ispage: false
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function (i, n) {
                            $sel_Album.append('<option value="' + n.Id + '">' + n.Name + '</option>');
                        });
                        $("#hid_Album").val($sel_Album.val());                     
                        $sel_Album.chosen({
                            allow_single_deselect: true,
                            disable_search_threshold: 6,
                            no_results_text: '未找到',
                            search_contains: true,
                            width: '95%'
                        });
                    }
                    else { }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        function SetAlbumHid() {
            $("#hid_Album").val($sel_Album.val());
        }
    </script>
</body>
</html>
