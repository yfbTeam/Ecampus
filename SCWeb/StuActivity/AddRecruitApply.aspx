<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRecruitApply.aspx.cs" Inherits="SCWeb.StuActivity.AddRecruitApply" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>申请加入</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <%--部门--%>
    <script id="div_DepartDetail" type="text/x-jquery-tmpl">
        <div class="clearfix" style="width: 360px; margin: 0 auto;">
            <div class="img fl">
                <img src="${PicURL}" onerror="javascript:this.src='/images/assodefault.jpg'" />
            </div>
            <div class="society_wenzi ml20 fl">
                <h1 class="clearfix soeicty_1">
                    <div class="soeicty_name fl">${Name}</div>
                    <div class="soeicty_wrap ml10 fl">(<span>成员${MemCount}</span>|<span>总贴量${NewCount}</span>)</div>
                </h1>
                <h2 class="soeicty_head fl">部长：${CreateName}</h2>
            </div>
        </div>
    </script>
    <style>
        .img {
            width: 100px;
            height: 100px;
        }

        .society_wenzi .soeicty_1 {
            line-height: 35px;
        }

            .society_wenzi .soeicty_1 .soeicty_name {
                font-size: 20px;
                color: #333333;
            }

            .society_wenzi .soeicty_1 .soeicty_wrap {
                font-size: 12px;
                color: #666666;
            }

        .society_wenzi .sort_type {
            font-size: 14px;
            color: #666666;
            line-height: 24px;
        }

        .society_wenzi .soeicty_head {
            font-size: 14px;
            color: #666666;
            line-height: 24px;
        }
    </style>
</head>
<body style="background: #fff;">
    <div style="padding: 20px 20px 50px 20px;">
        <div id="div_Depart" class="row"></div>
        <div class="row" style="display: none;">
            <label for="" class="row_label fl">申请原因：</label>
            <div class="row_content">
                <textarea id="area_Content" name="area_Content" placeholder="申请原因" style="width: 100%; height: 140px;"></textarea>
            </div>
        </div>
    </div>
    <div class="tools_bottom">
        <input type="button" class="keep" value="确定" onclick="SaveItem();" />
        <input type="button" class="cancel" value="取消" onclick="javascript: parent.CloseIFrameWindow();" />
    </div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            if (UrlDate.relid != undefined) {
                GetDepartInfo();
            }
        });
        //获取部门信息
        function GetDepartInfo() {
            var params = {
                PageName: "/StuActivity/Acti_DepartHandler.ashx",
                Func: "GetDepartInfoDataPage",
                LoginUID: "<%=UserInfo.UniqueNo%>",
                Id: UrlDate.relid,
                ispage: false,
                IsOnlyBase: "1",
                IsUnifiedInfo: "1"
            };
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: params,
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#div_Depart").html('');
                        var rtnObj = json.result.retData;
                        $("#div_DepartDetail").tmpl(rtnObj).appendTo("#div_Depart");
                    }
                    else {
                        $("#div_Depart").html('<div>暂无部门！</div>');
                    }
                },
                error: function (errMsg) {
                    $("#div_Depart").html('<div>暂无部门！</div>');
                }
            });
        }
        //保存信息
        function SaveItem() {
            var content = $("#area_Content").val().trim();
            if (!content.length) { layer.msg("请填写申请原因！"); return; }
            var params = {
                PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                Func: "AddActiRecruitApply",
                ActivityId: UrlDate.relid,
                CreateUID: "<%=UserInfo.UniqueNo%>",
                Type: UrlDate.type,  //1入部申请，2退部申请
                Content: content
            }
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: params,
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == -1) {
                        layer.msg("已申请,请等待审批...");
                    }
                    else if (result.errNum == 0) {
                        parent.layer.msg("申请已发出,请等待审批...");
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

