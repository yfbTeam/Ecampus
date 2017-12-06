<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModelAdd.aspx.cs" Inherits="SCWeb.DeskManage.ModelAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <link href="css/colorpicker.css" rel="stylesheet" />
    <script type="text/javascript" src="/js/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="Scripts/jquery.js"></script>
    <script src="Scripts/colorpicker.js"></script>


</head>
<body style="background: #fff">
    <form id="form2" enctype="multipart/form-data" method="post" runat="server">
        <input type="hidden" id="UniqueNo" value="<%=UserInfo.UniqueNo %>" />
        <!--创建课程dialog-->
        <div style="background: #fff">
            <div style="padding: 20px 20px 50px 20px;">
                <div class="fl" style="width: 100%;">
                    <div class="clearfix">
                        <div class="fl" style="width: 50%;">
                            <div class="row clearfix">
                                <div class="row_content">
                                    <label for="" class="row_label fl">模块名称：</label>
                                    <input type="text" placeholder="模块名称" class="text" id="ModelName" value="" />
                                    <i class="stars"></i>
                                </div>
                            </div>
                            <div class="course_form_div clearfix">
                                <label for="">模块排序：</label>
                                <input type="text" placeholder="模块排序" class="text" id="OrderNum" value="0" />
                                <i class="stars"></i>
                            </div>
                            <div class="row clearfix">
                                <div class="row_content">
                                    <label for="" class="row_label fl">模块分类：</label>
                                    <select id="ModelType" class="select" style="width: 210px;">
                                    </select>
                                </div>
                            </div>
                            <div class="row clearfix">
                                <div class="row_content">
                                    <label for="" class="row_label fl">打开方式：</label>
                                    <select id="OpenType" class="select" style="width: 210px;">
                                        <option value="_self">当前页</option>
                                        <option value="_blank" selected="selected">新页面</option>

                                    </select>
                                </div>
                            </div>
                            <div class="row clearfix">
                                <div class="row_content">
                                    <label for="" class="row_label fl">类型：</label>
                                    <select id="MenuType" class="select" style="width: 210px;">
                                        <option value="1" selected="selected">菜单</option>
                                        <option value="2">按钮</option>

                                    </select>
                                </div>
                            </div>
                            <div class="row clearfix">
                                <div class="row_content">
                                    <label for="" class="row_label fl">图标名称：</label>
                                    <input type="text" placeholder="图标名称" class="text" id="iconCss" />
                                    <i class="stars"></i>
                                </div>
                            </div>
                        </div>
                        <div class="fr" style="width: 50%;">
                            <div class="row clearfix">
                                <div class="course_form_div clearfix">
                                    <label for="">背景颜色：</label>

                                    <input type="text" id="color" name="color" value="#123456" style="background-color: rgb(72, 86, 18); color: rgb(255, 255, 255);" class="text" />
                                    <div id="picker" style="width: 195px; height: 195px; padding-left: 40px;"></div>

                                </div>

                            </div>

                            <div class="row clearfix">
                                <div class="row_content">
                                    <label for="" class="row_label fl">链接地址：</label>
                                    <input type="text" placeholder="链接地址" class="text" id="LinkUrl" value="" />
                                    <i class="stars"></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tools_bottom">
                <input type="button" class="keep" value="确定" onclick="AddModol();" />
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        $(function () {
            $('#picker').farbtastic('#color');

            CatoryModel();
            if (UrlDate.ID != undefined) {
                getData();
            }
        });
        function CatoryModel() {
            $("#ModelType").html("<option value='0'>==请选择==</option>");
            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: { PageName: "/DeskManage/AppHandler.ashx", "Func": "ModelCatogory", "Status": "1" },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            $("#ModelType").append("<option value='" + this.ID + "'>" + this.Name + "</option>");
                        })
                    }
                    else {
                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        function AddModol() {
            var ModelName = $("#ModelName").val();
            var ModelType = $("#ModelType").val();
            var OpenType = $("#OpenType").val();
            var ModelCss = $("#color").val();
            var iconCss = $("#iconCss").val();
            var LinkUrl = $("#LinkUrl").val();
            var OrderNum = $("#OrderNum").val();
            var Pid = UrlDate.Pid;
            if (!ModelName.length || ModelType=="0") {
                layer.msg("模块名称、模块分类不能为空！");
            }
            else {
                $.ajax({
                    url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: {
                        PageName: "/DeskManage/AppHandler.ashx",
                        "Func": "AddModol",
                        ID: UrlDate.ID,
                        ModelName: ModelName,
                        ModelType: ModelType,
                        OpenType: OpenType,
                        ModelCss: ModelCss,
                        iconCss: iconCss,
                        LinkUrl: LinkUrl,
                        OrderNum: OrderNum,
                        Pid: Pid,
                        MenuType:$("#MenuType").val(),
                        UniqueNo: $("#UniqueNo").val()
                    },
                    success: function (json) {
                        if (json.result.errNum.toString() == "0") {
                            parent.layer.msg('操作成功!');
                            parent.GetMenuInfo();
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
        function getData() {

            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: { PageName: "/DeskManage/AppHandler.ashx", "Func": "GetModelList", ID: UrlDate.ID, Ispage: 'false' },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            $("#ModelName").val(this.ModelName);
                            $("#ModelType").val(this.ModelType);
                            $("#OpenType").val(this.OpenType);
                            $("#ModelCss").val(this.ModelCss);
                            $("#LinkUrl").val(this.LinkUrl);
                            $("#color").val(this.ModelCss);
                            $("#OrderNum").val(this.OrderNum);
                            $("#iconCss").val(this.iconCss);

                        })
                    }
                    else {
                    }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
    </script>


</body>
</html>
