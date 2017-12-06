<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAssoApply.aspx.cs" Inherits="SCWeb.StuAssociate.AddAssoApply" %>

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
    <%--社团--%>
    <script id="div_AssoDetail" type="text/x-jquery-tmpl">
        <div class="clearfix" style="width: 360px; margin: 0 auto;">
            <div class="img fl">
                <img src="${PicURL}" onerror="javascript:this.src='/images/assodefault.jpg'" />
            </div>
            <div class="society_wenzi ml20 fl">
                <h1 class="clearfix soeicty_1">
                    <div class="soeicty_name fl">${Name}</div>
                    <div class="soeicty_wrap ml10 fl">(<span>成员${MemCount}</span>|<span>总贴量${NewCount}</span>)</div>
                </h1>
                <div class="sort_type clearfix">
                    <span class="soeicty_sort mr10">分类：${AssoTypeName}</span>| <span class="soeicty_type ml10">类型：<span>${ApplyType==0?'申请加入':'公开'}</span></span>
                </div>
                <h2 class="soeicty_head fl">社团长：${CreateName}</h2>
            </div>
        </div>
    </script>
    <style>
        .img{width:100px;height:100px;}
        .society_wenzi .soeicty_1{line-height:35px;}
        .society_wenzi .soeicty_1 .soeicty_name{font-size:20px;color:#333333;}
        .society_wenzi .soeicty_1  .soeicty_wrap{font-size:12px;color:#666666;}
        .society_wenzi .sort_type{font-size:14px;color:#666666;line-height:24px;}
        .society_wenzi .soeicty_head{font-size:14px;color:#666666;line-height:24px;}
    </style>
</head>
<body style="background:#fff;">
    <div style="padding:20px 20px 50px 20px;">
        <div id="div_Asso" class="row"></div>
        <div id="div_ApplyIntroduce" class="row" style="display:none;">
            <label for="" class="row_label fl">申请原因：</label>
            <div class="row_content">
               <textarea id="area_ApplyIntroduce" name="area_ApplyIntroduce" placeholder="申请原因" style="width: 100%; height: 140px;"></textarea>
            </div>
        </div>
    </div>
    <div class="tools_bottom">
        <input type="button" id="btn_Sure" class="keep" value="确定"/>
        <input type="button" class="cancel" value="取消" onclick="javascript: parent.CloseIFrameWindow();" />
    </div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {              
            if (UrlDate.relid != undefined) {
                GetAssoInfo();
                if (UrlDate.astype == 0) { //社团申请类型
                    $("#div_ApplyIntroduce").show();
                } else {
                    $("#div_ApplyIntroduce").hide();
                }
            }           
        });
        //获取社团信息
        function GetAssoInfo() {          
            var params = {
                PageName: "/StuAssociate/Asso_InfoHandler.ashx",
                Func: "GetAssoInfoDataPage",
                LoginUID: "<%=UserInfo.UniqueNo%>",
                Id:UrlDate.relid,
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
                        $("#div_Asso").html('');
                        var rtnObj = json.result.retData;
                        $("#div_AssoDetail").tmpl(rtnObj).appendTo("#div_Asso");
                        $("#btn_Sure").click(function () {
                            SaveItem(rtnObj[0]);                  
                        });
                    }
                    else {
                        $("#div_Asso").html('<div>暂无社团！</div>');
                    }
                },
                error: function (errMsg) {
                    $("#div_Asso").html('<div>暂无社团！</div>');
                }
            });
        }
        //保存信息
        function SaveItem(assoModel) {
            var astype = UrlDate.astype;
            var params;
            if (astype == 0) {
                var introduce = $("#area_ApplyIntroduce").val().trim();
                if (!introduce.length) { layer.msg("请填写申请原因！"); return; }
                params={
                    PageName: "/StuAssociate/Asso_ApplyHandler.ashx",
                    Func: "AddAssoApply",
                    AssoId: UrlDate.relid,
                    ApplyUserNo: "<%=UserInfo.UniqueNo%>",
                    ApplyType: UrlDate.type,  //1入团申请，2退团申请
                    ApplyIntroduce: introduce
                }
            } else {
                params={
                    PageName: "/StuAssociate/Asso_InfoHandler.ashx",
                    Func: UrlDate.type == 1 ? "AddAssoMember" : "DelAssoMember",
                    AssoId: UrlDate.relid,                    
                    MemberNos: "<%=UserInfo.UniqueNo%>",
                    LoginUID: "<%=UserInfo.UniqueNo%>"
                }
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
                        if (UrlDate.astype == 0) { layer.msg("已申请,请等待审批..."); return; }
                    }
                    else if (result.errNum == 0) {
                        if (astype == 0) {
                            parent.layer.msg("申请已发出,请等待审批...");
                            AddComMessage("社团申请", "<%=UserInfo.Name%>" + "申请" + (UrlDate.type == 1 ? "加入" : "退出") + assoModel.Name + "!", "0", "<%=UserInfo.UniqueNo%>", assoModel.CreateUID, "", "/StuAssociate/SingleAssoManage.aspx?itemid=" + UrlDate.relid + "&nav=2&tab=3", "<%=UserInfo.Name%>", assoModel.CreateName, 0);
                            parent.CloseIFrameWindow();                            
                        } else {
                            layer.msg(UrlDate.type == 1 ? "入团成功!" : "退团成功!");
                            AddComMessage("社团申请", "<%=UserInfo.Name%>" + "申请" + (UrlDate.type == 1 ? "加入" : "退出") + assoModel.Name + "!", "0", "<%=UserInfo.UniqueNo%>", assoModel.CreateUID, "", "/StuAssociate/SingleAssoManage.aspx?itemid=" + UrlDate.relid + "&nav=2&tab=3", "<%=UserInfo.Name%>", assoModel.CreateName, 0);
                            parent.window.location.reload();
                        }                        
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
