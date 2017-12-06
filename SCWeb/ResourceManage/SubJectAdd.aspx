<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubJectAdd.aspx.cs" Inherits="SCWeb.ResourceManage.SubJectAdd" %>

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
            BindGrade();
            BindVersion();
            //var GetName = UrlDate.Name;
            //if (GetName != undefined) {
            //    CatoryModelByID();
            //}
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
        function BindGrade() {
            $("#Grade").html("");
            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/BookSubject/SubJect.ashx",
                    "Func": "Grade", IsPage: "false"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            $("#Grade").append("<option value='" + this.ID + "'>" + this.Name + "</option>");
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
        function BindVersion() {
            $("#Version").html("");
            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: {

                    PageName: "/BookSubject/SubJect.ashx",
                    "Func": "Version", IsPage: "false"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            $("#Version").append("<option value='" + this.ID + "'>" + this.Name + "</option>");
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
        function AddSubJect() {
            var Name = $("#Name").val();

            if (!Name.length) {
                layer.msg("请填写科目名称！");
            }
            else {
                $.ajax({
                    url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: {
                        PageName: "/BookSubject/SubJect.ashx",
                        "Func": "AddSubJect",
                        ID: UrlDate.ID,
                        Name: $("#Name").val(),
                        VersionID: $("#Version").val(),
                        GradeID: $("#Grade").val()
                    },
                    success: function (json) {
                        if (json.result.errNum.toString() == "0") {
                            parent.layer.msg('操作成功!');
                            parent.getData(1,15);
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
                                <label for="" class="row_label fl">科目名称：</label>
                                <input type="text" placeholder="分类名称" class="text" id="Name" />
                            </div>
                        </div>
                        <div class="row clearfix">
                            <div class="row_content">
                                <label for="" class="row_label fl">年级：</label>
                                <select id="Grade" class="select" style="width: 210px;">
                                    
                                </select>
                            </div>
                        </div>
                        <div class="row clearfix">
                            <div class="row_content">
                                <label for="" class="row_label fl">版本号：</label>
                                <select id="Version" class="select" style="width: 210px;">
                                   
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tools_bottom">
                <input type="button" class="keep" value="确定" onclick="AddSubJect();" />
                <input type="button" class="cancel" value="取消" onclick="javascript: parent.CloseIFrameWindow();" />
            </div>

        </div>
    </form>


</body>
</html>

