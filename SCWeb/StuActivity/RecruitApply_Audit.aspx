<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecruitApply_Audit.aspx.cs" Inherits="SCWeb.StuActivity.RecruitApply_Audit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>部门审核</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
</head>
<body style="background: #fff;">
    <div style="padding: 20px 20px 50px 20px;">
        <div class="row">
            <label for="" class="row_label fl">申请原因：</label>
            <div class="row_content">
                <textarea id="area_Content" name="area_Content" readonly="readonly" style="width: 100%; height: 120px;"></textarea>
            </div>
        </div>
    </div>
    <div class="tools_bottom">
        <input type="button" class="keep" value="通过" onclick="SaveItem(2);" />
        <input type="button" class="cancel" value="拒绝" onclick="SaveItem(3);" />
    </div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            if (UrlDate.itemid != undefined) {
                GetActiRecruitApplyById();
            }
        });
        //获取部门申请详情
        function GetActiRecruitApplyById() {
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                    Func: "GetActiRecruitApplyById",
                    ItemId: UrlDate.itemid
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var model = json.result.retData;
                        $("#area_Content").val(model.Content);
                    }
                },
                error: function (errMsg) { }
            });
        }
        //保存审核信息
        function SaveItem(status) {
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                    Func: "ActiRecruitApply_Audit",
                    ItemId: UrlDate.itemid,
                    ExamUserNo: "<%=UserInfo.UniqueNo%>",
                    ExamStatus: status,
                    ExamSuggest: ""
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == 0) {
                        parent.layer.msg("审核成功!");
                        parent.GetMyDepart(3);
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

