<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssoApply_Audit.aspx.cs" Inherits="SCWeb.StuAssociate.AssoApply_Audit" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>社团审核</title>
<link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css"/>
    <link rel="stylesheet" href="/css/style.css"/>
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
</head>
<body style="background:#fff;">
     <div style="padding:20px 20px 50px 20px;">
        <div class="row">
            <label for="" class="row_label fl">申请原因：</label>
            <div class="row_content">
                <textarea id="area_ApplyIntroduce" name="area_ApplyIntroduce" readonly="readonly" style="width: 100%; height: 120px;"></textarea>
            </div>
        </div>
    </div>
    <div class="tools_bottom">
        <input id="btn_Sure" type="button" class="keep" value="通过"/>
        <input id="btn_Refuse" type="button" class="cancel" value="拒绝"/>
    </div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        var assoname = decodeURIComponent(UrlDate.assoname);
        $(function () {              
            if (UrlDate.itemid != undefined) {
                GetAssoApplyById();                
            }           
        });
        //获取社团申请详情
        function GetAssoApplyById() {          
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_ApplyHandler.ashx",
                    Func: "GetAssoApplyById",
                    ItemId: UrlDate.itemid
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var model = json.result.retData;
                        $("#area_ApplyIntroduce").val(model.ApplyIntroduce);
                        $("#btn_Sure").click(function () {
                            SaveItem(2, model);
                        });
                        $("#btn_Refuse").click(function () {
                            SaveItem(3, model);
                        });
                    }
                },
                error: function (errMsg) {}
            });
        }
        //保存审核信息
        function SaveItem(status,applyModel) {
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_ApplyHandler.ashx",
                    Func: "AssoApply_Audit",
                    ItemId: UrlDate.itemid,
                    ExamUserNo: "<%=UserInfo.UniqueNo%>",
                    ExamStatus: status,
                    ExamSuggest: ""
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == 0) {
                        parent.layer.msg("审核成功!");
                        parent.GetMyAsso(3);               
                        AddComMessage("社团审核", assoname + (status == 2 ? "通过了您的" : "拒绝了您的") + (applyModel.ApplyType == 1 ? "入团申请" : "退团申请") + applyModel.Name + "!", "0", "<%=UserInfo.UniqueNo%>", applyModel.ApplyUserNo, "", "/StuAssociate/AssociateDetail.aspx?itemid=" + applyModel.AssoId + "&nav=1&tab=3", "<%=UserInfo.Name%>", "", 0);
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
