<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QueryConfigure.aspx.cs" Inherits="SCWeb.SystemSettings.QueryConfigure" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>配置查询条件</title>
    <link href="/css/style.css" rel="stylesheet" />
    <link href="/css/common.css" rel="stylesheet" />
    <link href="/css/iconfont.css" rel="stylesheet" />
    <link href="/css/animate.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/jquery.cookie.js"></script>
    <script src="/Scripts/Common.js?parm=1.01"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="../Scripts/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <input type="hidden" id="hid_Id" runat="server" />
    <input type="hidden" id="hid_UserIDCard" runat="server" />
    <input type="hidden" id="hid_LoginName" runat="server" />
    <!--tz_dialog start-->
    <div class="yy_dialog">
        <div class="t_content">
            <div class="yy_tab">
                <div class="content">
                    <div class="tc">
                        <div class="t_message">
                            <div class="message_con">
                                <form>
                                    <table class="m_top">
                                        <tr>
                                            <td class="mi"><span class="m"></span></td>
                                            <td >
                                                启用/禁用：<input type="checkbox" id="State" />
                                            </td>
                                        </tr>  
                                        <tr>
                                            <td class="mi"><span class="m"></span></td>
                                            <td >
                                                登录名：<input type="checkbox" id="LoginName" /></td>
                                        </tr> 
                                        <tr>
                                            <td class="mi"><span class="m"></span></td>
                                            <td >
                                                身份证号：<input type="checkbox" id="IDCard" />

                                            </td>
                                        </tr>
                                        <tr style="display:none;">
                                            <td class="mi"><span class="m"></span></td>
                                            <td >
                                                分组：<input type="checkbox" id="Group" />
                                            </td>
                                        </tr>
                                    </table>
                                </form>
                            </div>
                        </div>
                        <div class="submit_btn">
                            <span class="Save_and_submit">
                                <input type="submit" value="保存" class="Save_and_submit" onclick="Save();" /></span>
                            <span class="cancel">
                                <input type="submit" value="取消" class="cancel" onclick="javascript: parent.CloseIFrameWindow();" /></span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--end tz_dialog-->

    <!--tz_yy start-->
    <div class="tz_yy"></div>
    <!--end tz_yy-->
</body>
<script type="text/javascript">
    $(document).ready(function () {
        QueryCondition();
    });
    //绑定自定义条件
    function QueryCondition() {
        $.ajax({
            url: "../Common.ashx?Trandom=" + Math.random(),
            type: "get",
            async: false,
            dataType: "json",
            data: {
                "PageName": "ConfigHandler.ashx",
                func: "QueryCondition"
            },
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    var item = json.result.retData[0];
                    if (item.State == "显示") {
                        $("#State").attr("checked", true);
                    }
                    if (item.LoginName == "显示") {
                        $("#LoginName").attr("checked", true);
                    }
                    if (item.IDCard == "显示") {
                        $("#IDCard").attr("checked", true);
                    }
                    if (item.Group == "显示") {
                        $("#Group").attr("checked", true);
                    }
                }
            }
        });

    }
    //保存接口
    function Save() {
        var State = $("#State").is(':checked') ? "显示" : "隐藏";
        var IDCard = $("#IDCard").is(':checked') ? "显示" : "隐藏";
        var LoginName = $("#LoginName").is(':checked') ? "显示" : "隐藏";
        var Group = $("#Group").is(':checked') ? "显示" : "隐藏";
        $.ajax({
            url: "/Common.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: {
                "PageName": "ConfigHandler.ashx",
                func: "UpdateQueryCondition",
                State: State,
                IDCard: IDCard,
                LoginName: LoginName,
                Group: Group
            },
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    parent.layer.msg('保存成功!');
                    parent.QueryCondition();
                    parent.CloseIFrameWindow();
                } else {
                    layer.msg("保存失败！");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.msg("操作失败！");
            }
        });
    }
</script>
</html>

