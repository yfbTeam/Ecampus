<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Skin.aspx.cs" Inherits="SMSWeb.AppManage.Skin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>我的桌面</title>
    <link rel="stylesheet" type="text/css" href="css/reset.css" />
    <link rel="stylesheet" type="text/css" href="css/iconfont.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <script src="Scripts/jquery-1.8.3.min.js"></script>
    <script src="Scripts/layer/layer.js"></script>
    <script src="Scripts/common.js"></script>
    <script type="text/javascript">
        $(function () {
            //退出
            $('.setting').hover(function () {
                $(this).find('.setting_none').show();
            }, function () {
                $(this).find('.setting_none').hide();
            })
            $(".skin_ul li").click(function () {
                $(this).addClass('selected').siblings().removeClass("selected");
                var ImageUrl = $(this).find("img").attr("src");
                $("#ImageUrl").val(ImageUrl);
                parent.ViewSkin(ImageUrl);
            });
        })
        function cancelIframe() {
            parent.CancelSkin();
        }
        function keepIframe() {
            parent.UpdateSkin($("#ImageUrl").val());
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="ImageUrl" />
        <div>
            <div style="padding: 20px 20px 54px 20px;">
                <ul class="skin_ul clearfix">
                    <li>
                        <img src="/DeskManage/images/6.jpg" alt="Alternate Text" />
                    </li>
                    <li>
                        <img src="/DeskManage/images/2.jpg" alt="Alternate Text" />
                    </li>
                    <li>
                        <img src="/DeskManage/images/3.jpg" alt="Alternate Text" />
                    </li>
                    <li>
                        <img src="/DeskManage/images/4.jpg" alt="Alternate Text" />
                    </li>
                    <li>
                        <img src="/DeskManage/images/5.jpg" alt="Alternate Text" />
                    </li>
                    <li>
                        <img src="/DeskManage/images/1.jpg" alt="Alternate Text" />
                    </li>
                    <li>
                        <img src="/DeskManage/images/7.jpg" alt="Alternate Text" />
                    </li>
                    <li>
                        <img src="/DeskManage/images/8.jpg" alt="Alternate Text" />
                    </li>
                    <li>
                        <img src="/DeskManage/images/bg01.jpg" alt="Alternate Text" />
                    </li>
                </ul>
            </div>
            <div class="preserved" style="position: fixed;">
                <input type="button" class="cancel fr" value="取消" onclick="cancelIframe()" />
                <input type="button" class="keep fr" value="保存" onclick="keepIframe(this)" />
            </div>
        </div>
    </form>
</body>
</html>
