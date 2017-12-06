<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlbumPicList.aspx.cs" Inherits="SCWeb.CommonPage.AlbumPicList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>相册照片</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css"/>
    <link rel="stylesheet" href="/css/style.css"/>
    <script src="/Scripts/jquery-1.8.3.min.js"></script>    
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <%--相册照片--%>
    <script id="div_PicSub" type="text/x-jquery-tmpl">
        <a href="javascript:;">
            <i>
                <img src="${PicUrl}" alt="" /></i>
            <div class="photos_tool">
                <div style="text-align: center">${SetPicName(PicUrl)}</div>
            </div>
        </a>
    </script>
</head>
<body style="background:#fff;">
    <div style="padding:0px 20px 50px 20px;">
        <div id="div_Pic" class="photo_lists"></div>
    </div> 
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            GetComAlbumPicData(1, 10);
        });
        //加载相册照片
        function GetComAlbumPicData(startIndex, pageSize) {
            $.ajax({
                url: "/Common.ashx",
                type: "post",                
                dataType: "json",
                data: {
                    PageName: "/CommonHandler/Com_AlbumHandler.ashx",
                    Func: "GetComAlbumPicDataPage",
                    AlbumId: UrlDate.itemid,
                    ispage: false
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#div_Pic").html('');
                        var rtnObj = json.result.retData;
                        $("#div_PicSub").tmpl(rtnObj).appendTo("#div_Pic");
                    }
                    else { }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }        
        function SetPicName(picurl) {
            if (picurl) {
                var nameindexS = picurl.lastIndexOf('/');
                var nameindexE = picurl.lastIndexOf('_');
                var extindex = picurl.lastIndexOf('.');
                var picExt = picurl.substring(extindex);
                var name = picurl.substring(nameindexS + 1, nameindexE);
                return cutstr(name + picExt,26);
            }            
        }
    </script>   
</body>
</html>
