<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssoEnteredSet.aspx.cs" Inherits="SCWeb.StuAssociate.AssoEnteredSet" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>报名时间设置</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css">
    <link rel="stylesheet" href="/css/style.css">
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/My97DatePicker/WdatePicker.js"></script>
    <style>
        .row .row_content{padding-left:115px;}
    </style>
</head>
<body style="background: #fff;">
    <div style="padding:20px 20px 50px 20px;">
        <div class="row clearfix" style="display:none;">
            <label for="" class="row_label fl">社团报名多选：</label>
            <div class="row_content" style="display:table-cell;vertical-align:middle;height:30px;">
               <input type="checkbox" id="ck_IsOnly" checked="checked" />
            </div>
        </div>
        <div class="row clearfix">
            <label for="" class="row_label fl">报名起止时间：</label>
            <div class="row_content">
                <input type="text" class="text Wdate" id="dt_StartTime" placeholder="请选择开始时间" onclick="javascript: WdatePicker({ maxDate: '#F{$dp.$D(\'dt_EndTime\')}' });" style="min-width: 140px;" />
                <span>~</span>
                <input type="text" class="text Wdate" id="dt_EndTime" placeholder="请选择结束时间" onclick="    javascript: WdatePicker({ minDate: '#F{$dp.$D(\'dt_StartTime\')||\'%y-%M-%d\'}' });" style="min-width: 140px;" />
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
            GetAssoEnteredData();
        });
        //绑定数据
        function GetAssoEnteredData() {
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_InfoHandler.ashx",
                    Func: "GetAssoEnteredData"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var model = json.result.retData[0];
                        $("#ck_IsOnly").attr("checked", model.IsOnly.toString()=="0"?false:true)
                        $("#dt_StartTime").val(DateTimeConvert(model.StartTime, 'yyyy-MM-dd'));
                        $("#dt_EndTime").val(DateTimeConvert(model.EndTime, 'yyyy-MM-dd'));
                    }                    
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        //保存信息
        function SaveItem() {
            var isonly = $("#ck_IsOnly").attr("checked") ? 1 : 0;;
            var startTime = $("#dt_StartTime").val();
            var endTime = $("#dt_EndTime").val();
            if (!startTime) { layer.msg("请选择开始时间！"); return; }
            if (!endTime) { layer.msg("请选择结束时间！"); return; }
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_InfoHandler.ashx",
                    Func:"AssoEnteredSet",
                    IsOnly: isonly,
                    StartTime: startTime,
                    EndTime: endTime,
                    LoginUID: "<%=UserInfo.UniqueNo%>"        
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == 0) {
                        parent.layer.msg('报名设置成功!');
                        parent.GetAssoType(1, 4);
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