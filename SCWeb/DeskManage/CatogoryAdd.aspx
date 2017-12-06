<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatogoryAdd.aspx.cs" Inherits="SCWeb.DeskManage.CatogoryAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css" />
    <link rel="stylesheet" href="/css/style.css" />

    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>

    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            var GetName = UrlDate.Name;
            if (GetName != undefined) {
                CatoryModelByID();
                //$("#Name").val(UrlDate.Name);
                //$("#Status").val(UrlDate.Status);
            }
        });
        function CatoryModelByID() {
            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: { PageName: "/DeskManage/AppHandler.ashx", "Func": "ModelCatogory", ID: UrlDate.ID },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            $("#Name").val(this.Name);
                            $("#SortNum").val(this.SortNum);
                            $("#Status").val(this.Status);
                        })
                    }

                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        function AddCatogory() {
            var Name = $("#Name").val();

            if (!Name.length) {
                layer.msg("请填写分类名称！");
            }
            else {
                $.ajax({
                    url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: {
                        PageName: "/DeskManage/AppHandler.ashx",
                        "Func": "AddCatory",
                        ID: UrlDate.ID,
                        Name: $("#Name").val(),
                        Status: $("#Status").val(),
                        SortNum: $("#SortNum").val()
                    },
                    success: function (json) {
                        if (json.result.errNum.toString() == "0") {
                            parent.layer.msg('操作成功!');
                            parent.CatoryModel();
                            parent.CloseIFrameWindow();
                        }
                        else {
                            layer.msg(json.result.errMsg);
                        }
                    },
                    error: function (errMsg) {
                        layer.msg(errMsg);
                    }
                });
            }
        }

    </script>

</head>
<body style="background: #fff">
    <form id="form2" enctype="multipart/form-data" method="post" runat="server">
        <!--创建课程dialog-->
        <div style="background: #fff">
            <div style="padding: 20px 20px 50px 20px;">
                <div class="fl">
                    <div class="clearfix">

                        <div class="row clearfix">
                            <div class="row_content">
                                <label for="" class="row_label fl">分类名称：</label>
                                <input type="text" placeholder="分类名称" class="text" id="Name" />
                            </div>
                        </div>
                        <div class="row clearfix">
                            <div class="row_content">
                                <label for="" class="row_label fl">分类排序：</label>
                                <input type="text" placeholder="分类排序" class="text" id="SortNum" value="0" />
                            </div>
                        </div>
                        <div class="row clearfix">
                            <div class="row_content">
                                <label for="" class="row_label fl">分类状态：</label>
                                <select id="Status" class="select" style="width: 210px;">
                                    <option value="0">禁用</option>
                                    <option value="1" selected="selected">启用</option>
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tools_bottom">
                <input type="button" class="keep" value="确定" onclick="AddCatogory();" />
            </div>

        </div>
    </form>


</body>
</html>
