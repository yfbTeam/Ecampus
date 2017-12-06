<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartRegistManage.aspx.cs" Inherits="SCWeb.StuActivity.DepartRegistManage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>报名管理</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/laypage/laypage.js"></script>
    <%--活动成员--%>
    <script id="tr_MemberSub" type="text/x-jquery-tmpl">
        <tr>
            <td style="width: 40px;">
                <input type="checkbox" name="ck_member" value="${Id},${JoinMember}"/>
            </td>
            <td>
                <div class="name clearfix">
                    <img src="${AbsHeadPic}" onerror="javascript:this.src='/images/member_img.png'" class="fl" />
                    <span class="fl ml10">${CreateName}</span>
                     {{if Sex==0}}<i class="iconfont icon-man fl ml10 colorblue"></i>{{else}}<i class="iconfont icon-woman fl ml10 colorred"></i>{{/if}}
                </div>
            </td>
            <td>${Name}</td>
            <td>${GradeName}</td>
            <td>${OrgName}</td>
        </tr>
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="header"></div>
            <div id="main">
                <div class="center clearfix  pt10">
                    <aside class="aside fl" style="position: fixed;">
                        <div class="aside_top">
                            <select id="sel_StudySection" class="select" style="width: 100%;"></select>
                            <h1 class="allasso mt10">
                                <i class="iconfont icon-crowd"></i>
                                全部活动（<span id="span_TotalCount">0</span>人）
                            </h1>
                        </div>
                        <ul id="ul_list" class="asso_lists"></ul>
                    </aside>
                    <article class="article fr">
                        <div class="pl20 pr20">
                            <div class="clearfix tools">
                                <div class="fl" style="line-height: 40px; color: #333333; font-size: 18px;">
                                    报名成员列表
                                </div>
                                <div class="fr tools_right">
                                    <a href="javascript:;" class="bggreen" onclick="PagePopWindow(0);">添加成员</a>
                                    <a href="javascript:;" id="a_ExportMember" class="bgblue">全部导出</a>
                                    <div class="search_wrap pr ml20 fl" style="width: 170px;">
                                        <input type="text" id="txtUserName" class="search" placeholder="搜索报名成员" />
                                        <i class="iconfont icon-search" style="top: 0px;" onclick="SearchCondition();"></i>
                                    </div>
                                </div>
                            </div>
                            <div class="table_wraps">
                                <div class="clearfix table_tool">
                                    <div class="fl clearfix">
                                        <select id="sel_Project" class="select fl" style="min-width:120px;">
                                            <option value="">全部项目</option>
                                        </select>                                        
                                    </div>
                                    <div class="fr tools_right">
                                        <a href="javascript:;" class="bgred" onclick="DelProjectMember();">删除成员</a>
                                    </div>
                                </div>
                                <table class="table">
                                    <thead>
                                        <tr>
                                            <th style="width: 40px;">
                                                <input type="checkbox" name="name" value=""/>
                                            </th>
                                            <th>姓名</th>
                                            <th>所报项目</th>                                          
                                            <th>年级</th>
                                            <th>班级</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tb_Member"></tbody>
                                </table>
                            </div>
                            <!--分页-->
                            <div class="page" id="pageBar_Member"></div>
                        </div>
                    </article>
                </div>
            </div>
            <div id="footer"></div>
        </div>
    </form>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        var selactivityid = 0;
        $(function () {
            $('#header').load('/CommonPage/Header.aspx?t=' + new Date().getTime(), function (response, status, xhr) {
                if (status == "success" || status == "notmodified") {
                    SetNavSelected(4);
                }
            });
            $('#footer').load('/CommonPage/footer.html');
            GetStudySectionData();
            getData(1, 10);
        });
        function GetStudySectionData() {
            $sel_StudySection = $("#sel_StudySection");
            $.ajax({
                url: "/CommonHandler/UnifiedHelpHandler.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { "Func": "GetStudySectionData"},
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function (i, n) {
                            $sel_StudySection.append('<option value="' + n.Id + '">' + n.Academic + '</option>');
                        });
                    }
                    else {}
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        //获取活动
        function getData(startIndex, pageSize) {
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { //不分页
                    PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                    Func: "GetActiActivityDataPage",
                    ispage: false,
                    LoginUID: "<%=UserInfo.UniqueNo%>",
			        },
			        success: function (json) {
			            if (json.result.errNum.toString() == "0") {
			                $("#ul_list").html('');
			                if (selactivityid == 0) { selactivityid = json.result.retData[0].Id; }
			                var totalCount = 0;
			                $(json.result.retData).each(function (i, n) {
			                    totalCount +=1;			                    
			                    $("#ul_list").append('<li id="li_acti_' + n.Id + '" ' + (n.Id == selactivityid ? 'class="selected"' : '') + '>' + n.Name + '</li>');
			                });
			                $("#span_TotalCount").html(totalCount);
			                BindProjectSelect();
			                GetActiProjectData(1,10);
			                //添加选中事件
			                $("#ul_list li").each(function (i, n) {			                    
			                    $(this).click(function () {
			                        $(this).addClass('selected').siblings().removeClass('selected');
			                        selactivityid = this.id.replace("li_acti_", "");
			                        BindProjectSelect();
			                        GetActiProjectData(1, 10);
			                    });
			                });
			                $("#a_ExportMember").click(function () {
			                    Export_ActiProMember();
			                });
			            }
			            else {
			                $("#ul_list").html('<li>暂无活动！</li>');
			                $("#tb_Member").html('<tr><td colspan="6">暂无成员！</td></tr>');
			            }
			        },
			        error: function (errMsg) {
			            $("#ul_list").html('<li>暂无活动！</li>');
			        }
			    });
        }
        //加载项目下拉框
        function BindProjectSelect() {
            $("#sel_Project").html('<option value="">全部项目</option>');
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { PageName: "/StuActivity/Acti_ActivityHandler.ashx", Func: "GetActiProjectDataPage", ActivityId: selactivityid, ispage:false },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function (i, n) {
                            $("#sel_Project").append('<option value="' + n.Id + '">' + n.Name + '</option>');
                        });                        
                    }                   
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        var txtUserName = $("#txtUserName").val().trim();
        function SearchCondition() {
            txtUserName = $("#txtUserName").val().trim();
            GetActiProjectData(1, 10);
        }
        //加载活动项目报名成员
        function GetActiProjectData(startIndex, pageSize) {
            var $tbPar = $("#tb_Member");
            var $trSub = $("#tr_MemberSub");
            var $pageBar = $("#pageBar_Member");
            $('.table input[type=checkbox]')[0].checked = false;
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                    Func: "GetActiProjectDataPage",
                    ActivityId: selactivityid,
                    Id: $("#sel_Project").val(),
                    SerUserName: txtUserName,
                    IsMember:1,
                    PageIndex: startIndex,
                    PageSize: pageSize,
                    IsOnlyBase: "1",
                    IsUnifiedInfo: "1"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $tbPar.html('');
                        var rtnObj = json.result.retData;
                        $trSub.tmpl(rtnObj.PagedData).appendTo("#tb_Member");
                        $pageBar.show();
                        laypage({
                            cont: $pageBar, //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                            pages: json.result.retData.PageCount, //通过后台拿到的总页数
                            curr: json.result.retData.PageIndex || 1, //当前页
                            skip: false, //是否开启跳页
                            skin: '#74c3f4',
                            jump: function (obj, first) { //触发分页后的回调
                                if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                    GetActiProjectData(obj.curr, pageSize)
                                }
                            }
                        });
                        NewCheckAll($('.table input[type=checkbox]'));
                    }
                    else {
                        $tbPar.html('<tr><td colspan="6">暂无成员！</td></tr>');
                        $pageBar.hide();
                    }                    
                },
                error: function (errMsg) {
                    $tbPar.html('<tr><td colspan="6">暂无成员！</td></tr>');
                }
            });
        }
        //删除活动项目报名成员
        function DelProjectMember() {
            var checkedtr = $("input[type='checkbox'][name='ck_member']:checked");
            if (checkedtr.length == 0) { layer.msg('请选择要删除的成员！'); return; }
            var idArray = [];
            $(checkedtr).each(function (i, n) {
                idArray.push(n.value);
            });
            layer.confirm('确定要删除勾选的报名成员吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                $.ajax({
                    url: "/Common.ashx",
                    type: "post",
                    dataType: "json",
                    data: {
                        PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                        Func: "DelProjectMember",
                        ActivityId: selactivityid,
                        MemberNos: idArray.join(","),
                        LoginUID: "<%=UserInfo.UniqueNo%>"
                    },
                    success: function (json) {
                        if (json.result.errNum == 0) {
                            layer.msg('删除成员成功！');
                            getData(1, 10);
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
        function Export_ActiProMember() {
            window.open('/CommonHandler/ImportOrExport.ashx?Func=Export_ActiProMember', "myIframe");
        }
        function PagePopWindow(type) {
            if (type == 0) { //添加成员
                if (selactivityid != 0) {
                    if ($("#sel_Project").val() != "") {
                        OpenIFrameWindow('添加成员', '/StuActivity/AddAssoMember.aspx?relid=' + selactivityid + '&tab=0', '900px', '660px');
                    } else {
                        layer.msg('请选择要添加成员的项目！');
                    }
                }
            }
        }
    </script>
</body>
</html>
