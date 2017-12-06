<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddDepartMember.aspx.cs" Inherits="SCWeb.StuActivity.AddDepartMember" %>
<!DOCTYPE html>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>添加成员</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/laypage/laypage.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <%--学生信息--%>
    <script id="tr_StuSub" type="text/x-jquery-tmpl">
         <tr>
            <td style="width: 40px;">
                <input type="checkbox" name="ck_stu" value="${UniqueNo}" />
            </td>
            <td>
                <div class="name clearfix">
                    <img src="${AbsHeadPic}" onerror="javascript:this.src='/images/member_img.png'" class="fl" />
                    <span class="fl ml10">${Name}</span>
                     {{if Sex==0}}<i class="iconfont icon-man fl ml10 colorblue"></i>{{else}}<i class="iconfont icon-woman fl ml10 colorred"></i>{{/if}}
                </div>
            </td>
            <td>${GradeName}</td>
            <td>${OrgName}</td>
        </tr>
    </script>   
</head>
<body style="background:#fff;">
    <div style="padding:20px 20px 50px 20px;">
        <div class="table_wraps">
            <table class="table">
                <thead>
                    <tr>
                        <th style="width: 40px;">
                            <input type="checkbox" name="name" value=" " />
                        </th>
                        <th>姓名</th>
                        <th>年级</th>
                        <th>班级</th>
                    </tr>
                </thead>
                <tbody id="tb_Stu"></tbody>
            </table>
        </div>
        <!--分页-->
        <div class="page" id="pageBar"></div>
    </div>
    <div class="tools_bottom">
        <input type="button" class="keep" value="确定" onclick="SaveItem();"/>
        <input type="button" class="cancel" value="取消" onclick="javascript: parent.CloseIFrameWindow();" />
    </div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        var memNoArray = [];
        $(function () {
            if (UrlDate.relid != undefined) {
                GetDepartMember();                
            }           
        });
        //获取部门成员
        function GetDepartMember() {
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { PageName: "/StuActivity/Acti_DepartHandler.ashx", Func: "GetDepartInfoById", ItemId: UrlDate.relid, },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var model = json.result.retData;
                        if (model.LeaderNo.length) { memNoArray.push(model.LeaderNo); }
                        if (model.SecondLeaderNo.length) { memNoArray.push(model.SecondLeaderNo); }
                        $.ajax({
                            url: "/Common.ashx",
                            type: "post",
                            async: false,
                            dataType: "json",
                            data: { PageName: "/StuActivity/Acti_DepartHandler.ashx", Func: "GetDepartMemberDataPage", LoginUID: "<%=UserInfo.UniqueNo%>", DepartId: UrlDate.relid, ispage: false },
                            success: function (json) {
                                if (json.result.errNum.toString() == "0") {
                                    $(json.result.retData).each(function (i, n) {
                                        memNoArray.push(n.MemberNo);
                                    });
                                }
                                GetUserInfoData(1, 10);
                            }
                        });
                    }
                }
            });
        }
        //获取学生信息
        function GetUserInfoData(startIndex, pageSize) {
            var $tbPar = $("#tb_Stu");
            var $trSub = $("#tr_StuSub");
            var $pageBar = $("#pageBar");
            $('.table_wraps input[type=checkbox]')[0].checked = false;
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "/CommonHandler/UnifiedHelpHandler.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {                   
                    Func: "GetUserInfoData",
                    IsStu:true,
                    Ispage: true,
                    JoinNoConn:"not",
                    UniqueNos:memNoArray.join(','),
                    PageIndex: startIndex,
                    PageSize: pageSize
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $tbPar.html('');
                        var rtnObj = json.result.retData;
                        $trSub.tmpl(rtnObj.PagedData).appendTo("#tb_Stu");
                        $pageBar.show();
                        laypage({
                            cont: $pageBar, //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                            pages: json.result.retData.PageCount, //通过后台拿到的总页数
                            curr: json.result.retData.PageIndex || 1, //当前页
                            skip: false, //是否开启跳页
                            skin: '#74c3f4',
                            jump: function (obj, first) { //触发分页后的回调
                                if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                    GetUserInfoData(obj.curr, pageSize)
                                }
                            }
                        });
                        NewCheckAll($('.table_wraps input[type=checkbox]'));
                    }
                    else {
                        $tbPar.html('<tr><td colspan="4">暂无！</td></tr>');
                        $pageBar.hide();
                    }
                },
                error: function (errMsg) {
                    $tbPar.html('<tr><td colspan="4">暂无！</td></tr>');
                }
            });
        }
        //保存信息
        function SaveItem() {
            var checkedtr = $("input[type='checkbox'][name='ck_stu']:checked");
            if (checkedtr.length == 0) { layer.msg('请选择要添加的行！'); return; }
            var idArray = [];
            $(checkedtr).each(function (i, n) {
                idArray.push(n.value);
            });
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuActivity/Acti_DepartHandler.ashx",
                    Func: "AddDepartMember",
                    DepartId: UrlDate.relid,
                    MemberNos: idArray.join(","),
                    LoginUID: "<%=UserInfo.UniqueNo%>"
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == 0) {
                        parent.layer.msg("添加成员成功!");
                        if (UrlDate.tab == 0) {
                            parent.getData(1, 10);
                        } else {
                            parent.GetMyDepart(UrlDate.tab);
                        }                        
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


