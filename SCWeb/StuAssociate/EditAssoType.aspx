<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditAssoType.aspx.cs" Inherits="SCWeb.StuAssociate.EditAssoType" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>社团分类</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css">
    <link rel="stylesheet" href="/css/style.css">
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <%--社团分类--%>
    <script id="div_sub" type="text/x-jquery-tmpl">
        <div class="fl edit_close">
            <input id="txt_Name_${Id}" type="text" readonly="readonly" class="text" value="${Name}" onblur="SaveItem(${Id});">
            <i class="iconfont icon-bianji edit" onclick="EditItem(${Id});"></i>
            <i class="iconfont icon-close close1" onclick="DelItem(${Id},this);"></i>
        </div>
    </script>
</head>
<body style="background: #fff;">
    <div style="padding:20px 20px 50px 20px;">
        <DIV class="row clearfix" style="padding-bottom:20px;border-bottom:1px dashed #E5EBEC;">
            <label for="" class="row_label fl">创建社团分类：</label>
            <input id="txt_Name" type="text" class="text fl" style="margin-left: 120px;">
            <input type="button" value="创建" class="btn fl ml10 bgblue" onclick="SaveItem(0);">
        </DIV>
        <div class="row clearfix">
            <label for="" class="row_label fl">已有社团分类：</label>
        </div>
        <div id="div_TypeAsso" class="row clearfix"></div>
    </div>
    <script type="text/javascript">
        var editid = 0;
        $(function () {
            GetAssoType();
        });
        //获取社团分类
        function GetAssoType() {
            $("#div_TypeAsso").html('');
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_TypeHandler.ashx",
                    Func: "GetAssoTypeDataPage",
                    ispage: false
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#div_sub").tmpl(json.result.retData).appendTo("#div_TypeAsso");
                    } else {
                        $("#div_TypeAsso").html('<div>暂无社团分类！</div>');
                    }
                },
                error: function (errMsg) {
                    $("#div_TypeAsso").html('<div>暂无社团分类！</div>');
                }
            });
        }
        function EditItem(itemid) {
            editid = itemid;
            var $txtObj = $("#txt_Name_" + itemid);
            $txtObj.removeAttr("readonly");
            var t = $txtObj.val();
            $txtObj.val("").focus().val(t); //jquery中focus()函数实现当对象获得焦点后自动把光标移到内容最后
        }
        //保存信息
        function SaveItem(itemid) {
            var name = "";
            if (itemid == 0) {
                name = $("#txt_Name").val().trim();     
            } else {
                if (editid == 0) {
                    return;
                }
                name = $("#txt_Name_" + itemid).val().trim();
            }      
            if (!name.length) {layer.msg("请填写分类名称！");return;}
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_TypeHandler.ashx",
                    Func: itemid == 0 ? "AddAssoType" : "EditAssoType",
                    ItemId: itemid,
                    Name: name,
                    LoginUID:"<%=uniqueNo%>"
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == -1) {
                        layer.msg("该分类名称已存在!");
                    }
                    else if (result.errNum == 0) {                        
                        if (itemid == 0) {
                            layer.msg('创建分类成功!');
                            GetAssoType();
                            $("#txt_Name").val('');                            
                        } else {
                            layer.msg('修改分类成功!');
                            editid = 0;
                            $("#txt_Name_" + itemid).attr("readonly", "readonly");
                        }
                        parent.GetAssoType(1, 4);
                    } else {
                        layer.msg(result.errMsg);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.msg("操作失败！");
                }
            });
        }
        function DelItem(itemid,obj) {
            layer.confirm('确定要删除该分类吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                $.ajax({
                    url: "/Common.ashx",
                    type: "post",
                    dataType: "json",
                    data: {
                        PageName: "/StuAssociate/Asso_TypeHandler.ashx",
                        func: "DelAssoType",
                        ItemId: itemid
                    },
                    success: function (json) {
                        if (json.result.errNum == 0) {
                            $(obj).parent().remove();                   
                            layer.msg('删除成功！')
                            parent.GetAssoType(1, 4);
                        }
                        else {
                            layer.msg(json.result.errMsg);
                        }
                    },
                    error: function (ErroMsg) {
                        layer.msg(ErroMsg);
                    }
                });
            }, function () { });
        }
    </script>
</body>
</html>
