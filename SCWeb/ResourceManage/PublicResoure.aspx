<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublicResoure.aspx.cs" Inherits="SCWeb.ResourceManage.PublicResoure" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>资源库</title>
    <!--图标样式-->
    <link rel="stylesheet" type="text/css" href="/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/reset.css" />
    <link rel="stylesheet" type="text/css" href="/css/common.css" />
    <link rel="stylesheet" type="text/css" href="/css/repository.css" />
    <link rel="stylesheet" href="/css/iconfont.css"/>
    <link rel="stylesheet" href="/css/style.css"/>
    <link href="/css/sprite.css" rel="stylesheet" />
    <script type="text/javascript" src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/laypage/laypage.js"></script>
    <!--[if IE]>
			<script src="/js/html5.js"></script>
		<![endif]-->
    <script id="tr_User" type="text/x-jquery-tmpl">
        <li class="clearfix">
            <div class="checkbox fl">
                <input type='checkbox' value='${ID}' class='Check_box' name='Check_box' id='subcheck' />
            </div>
            <div class="docu_messages fl">
                <p class="docu_title" onclick="FileClick(${ID},'${FileUrl}')">
                    <span class="ico-${postfix1}-min h-ico">
                        <img style="width: 100%; height: auto;" /></span><a class="docu_name" href="#" title="${Name}">${cutstr(Name,40)}</a>
                </p>
                <div class="down_mes">
                    <%--<span class="down_mes">${FileGroup}</span>--%>
                    <span class="down_mes_dates">
                        <i class="test_type">{{if FileGroup==""}} 
                            其他 {{else}}
                            ${FileGroup}
                             {{/if}}
                        </i>
                        <em class="download_mes_date">${DateTimeConvert(CreateTime,'yyyy-MM-dd')}</em>
                    </span>
                </div>
            </div>
            <div class="unload_none" style="display: none">
                <%--<span class="upload">
                    <i class="icon icon-download-alt"></i>
                </span>--%>
                <span class="arrow-down pr">
                    <i class="icon" title="更多">
                        <img src="/images/xai.png" /></i>
                    <div class="arrow_downwrap">
                        <span class="rename" onclick="Down(${ID})">下载</span>
                        <span class="delete" onclick="Del(${ID},'${Name}')">删除</span>
                    </div>
                </span>
            </div>
            <div class="assess" id="${ClickNum}">

                <span id="1" onclick="Evalue(1,${ID},this)"></span>
                <span id="2" onclick="Evalue(2,${ID},this)"></span>
                <span id="3" onclick="Evalue(3,${ID},this)"></span>
                <span id="4" onclick="Evalue(4,${ID},this)"></span>
                <span id="5" onclick="Evalue(5,${ID},this)"></span>
            </div>
        </li>
    </script>
    <script id="tr_User1" type="text/x-jquery-tmpl">
        <li>
            <div class="checkbox">
                <input type="checkbox" name="Check_box" id="" value="" />
            </div>
            <div class="grid_view">
                <a href="#" onclick="FileClick(${ID},'${FileUrl}')">
                    <em class="ico-${postfix1}-max grid_view_ico">
                        <img style="width: 100%; height: auto;" /></em>
                    <p class="grid_view_name" title="${Name}">${cutstr(Name,12)}</p>
                </a>
                <%--<span class="down_mes">${FileGroup}</span>--%>
                <div class="grid_view_dates">
                    <span class="grid_view_date fl">${DateTimeConvert(CreateTime,'yyyy-MM-dd')}</span>

                </div>
            </div>
        </li>
    </script>
    <script id="tr_DowDetail" type="text/x-jquery-tmpl">
        <li>
            <div class="download_messages clearfix">
                <span class="date fl">${getDateDiff(ClickTime*1000)}
                </span>
                <span class="download_message fl">${CreateName}下载了
                </span>
            </div>
            <p class="download_title">
                <span class="ico-${postfix1}-min g-ico">
                    <img style="width: 100%; height: auto;" /></span><span class="download_name" title="${Name}">${Name}</span>
            </p>
        </li>
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="hidden" id="HUserIdCard" value="<%=UserInfo.IDCard %>" />
            <%--<input type="hidden" id="HUserName" runat="server" />
            <input type="hidden" id="HClassID" runat="server" />

            <asp:HiddenField ID="HSchoolID" runat="server" />--%>
            <input id="HFoldUrl" type="hidden" value="/PubFolder" />
            <input id="code" type="hidden" value="0" />
            <input id="ShowType" type="hidden" value="1" />
            <input id="GroupName" type="hidden" value="" />
            <input id="Postfixs" type="hidden" value="" />
            <input id="ID" type="hidden" value="0" />
            <input id="SubjectID" type="hidden" />
            <%-- 科目--%>
            <input id="SubjectName" type="hidden" />
            <%-- 版本--%>
            <input id="VersionID" type="hidden" />
            <%-- 年级--%>
            <input id="GradeID" type="hidden" />
            <input id="HChapterID" type="hidden" />
           
            <div id="header"></div>
            <!--内容-->
            <div id="main">
                <div class="center clearfix  pt10">
                    <div class="choiceversion">
                        <div class="selected clearfix">
                            <strong>选择教材</strong>
                            <em class="trigger"><i class="icon-angle-down icon"></i></em>
                        </div>
                        <div class="contentbox">
                            <h2 class="subtitle">选择教材</h2>
                            <div class="item">
                                <select name="" id="Subject" onchange="SubjectChange()">
                                </select>
                            </div>
                            <div class="item">
                                <select id="Version" onchange="VersionChange()">
                                </select>
                            </div>
                            <div class="item">
                                <select id="Grade" onchange="GradeChange()">
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="main fl clearfix">
                        <section class="menu fl">
                            <div class="grade pr">
                                <div class="item">
                                    <span class="icon-th-list icon icon_list"></span>
                                    <span class="title" style="cursor:pointer;" id="CatagoryManage">目录管理</span>
                                    <span class="icon icon-angle-right icon_right"></span>
                                </div>

                            </div>
                            <div class="items" id="menuChapater"></div>
                        </section>
                        <section class="article_content fr bordshadrad">
                            <div class="modult_toolsbar clearfix">
                                <!--列表、视图-->
                                <div class="list_grid_switch fr">
                                    <a href="javascript:;" class="list_switch on">
                                        <i class="icon icon-th-list" onclick="ShowType(1)"></i>
                                    </a>
                                    <a href="javascript:;" class="grid_switch">
                                        <i class="icon  icon-th-large" onclick="ShowType(2)"></i>
                                    </a>
                                </div>
                                <!--工具条-->
                                <div class="toolsbar fl">
                                    <div class="bars">
                                        <%--<a href="javascript:;" class="upload pr" id="CourceResource">
                                            <i class="icon  icon-upload-alt"></i>
                                            <span class="txt" onclick="CourceResource()">确定</span>
                                        </a>--%>
                                        <div id="other">
                                            <a href="javascript:;" class="upload pr">
                                                <i class="icon  icon-upload-alt"></i>
                                                <span class="txt" onclick="upload()">上传文件</span>
                                            </a>
                                            <a href="javascript:;" class="operate1 pr">
                                                <i class="icon  icon-wrench"></i>
                                                <span class="txt">批量操作</span>
                                                <div class="operate_wrap">
                                                    <span onclick="Del('','')">删除</span>
                                                    <span onclick="Down('')">下载</span>
                                                </div>
                                            </a>
                                            <a href="javascript:;" class="selection">
                                                <i class="icon icon-filter"></i>
                                                <span class="txt">筛选</span>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="selectionwrap none">
                                <div class="select_nav clearfix pr">
                                    <div class="select_nav_left fl">
                                        分类:
                                    </div>
                                    <ul class="select_nav_right fl" id="ResourceType">
                                        <li class="on">
                                            <a href="#" onclick="serchType('',this)">全部</a>
                                        </li>
                                    </ul>
                                </div>

                            </div>
                            <!--docu-->
                            <div class="docu_wrap">
                                <div class="docu_item">
                                    <a href="javascript:;" class="on" onclick="FileGroup('',this)">全部</a>
                                    <a href="javascript:;" onclick="FileGroup('教案',this)">教案</a>
                                    <a href="javascript:;" onclick="FileGroup('课件',this)">课件</a>
                                    <a href="javascript:;" onclick="FileGroup('习题',this)">习题</a>
                                    <a href="javascript:;" onclick="FileGroup('微课',this)">微课</a>

                                </div>

                                <!--内容-->
                                <div class="docu_content">

                                    <!--docu_list-->
                                    <ul class="document_list" style="display: block;" id="tb_MyResource">
                                    </ul>
                                    <ul class="docu_grid clearfix" id="tb_MyResource1">
                                    </ul>
                                </div>
                            </div>
                            <!--分页-->
                            <div class="page" id="pageBar">
                            </div>
                        </section>
                    </div>
                    <div class="aside fr bordshadrad">
                        <div class="teacher_downloading">
                            <header class="title">
                                <h2>老师们正在下载</h2>
                            </header>
                            <ul class="downloading_xiang" id="tb_DowDetail">
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div id="footer"></div>
            <script type="text/javascript" src="/Scripts/repository.js"></script>
        </div>
    </form>
</body>
<script type="text/javascript">

    $(function () {
        SetPageButton('<%=UserInfo.UniqueNo%>');
        $('#header').load('/CommonPage/Header.aspx');
        $('#footer').load('/CommonPage/footer.html');
        //鼠标划过批量操作
        $('.operate1').hover(function () {
            $(this).find('.operate_wrap').show();
        }, function () {
            $(this).find('.operate_wrap').hide();
        })
    })
    function changeMenu(id) {
        $("#HChapterID").val(id);
        getData(1, 10);
       
    }
    function menuSel()//menu折叠展开 选中切换
    {
        $('.items').find('.units').each(function () {
            var oLi = $('.items').find('li')
            oLi.click(function () {
                oLi.removeClass('active');
                $(this).addClass('active');
            });
            $(this).find('.item_title').click(function () {
                var $next = $(this).next();
                var $icon = $(this).find('.icon');
                $icon.toggleClass('active');
                $next.stop().slideToggle();
                $('.items').find('.contentbox').not($next).slideUp();
                $('.items').find('.icon').not($icon).removeClass('active');
            })
        })
    }
    function BindSubject() {
        $("#Subject").children().remove();
        option = "<option value='0'>选择科目</option>";
        $("#Subject").append(option);

        $.ajax({
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            type: "post",
            async: false,
            dataType: "json",
            data: { "PageName": "/BookSubject/SubJect.ashx", "Func": "SubJect", IsPage: "false", "Distinct": "1" },
            success: function (json) {
                var num = 0;
                var NameArry = "";
                $(json.result.retData).each(function () {
                    var option = "";
                    if (num == 0) {
                        option = "<option value='" + this.ID + "'  selected='selected'>" + this.Name + "</option>";
                        $("#SubjectName").val(this.Name);
                        $("#Subject").append(option);
                        NameArry += this.Name;
                        BindVersion()
                    }
                    else {
                        if (NameArry.indexOf(this.Name) < 0) {
                            option = "<option value='" + this.ID + "'>" + this.Name + "</option>";
                            $("#Subject").append(option);
                            NameArry += this.Name;
                        }
                    }
                    num++;
                })
            },
            error: function (errMsg) {
                layer.msg(errMsg);
            }
        });
    }
    function SubjectChange() {
        var SubjectName = $("#Subject").find("option:selected").text();//.children('span').html();

        $("#SubjectName").val(SubjectName);
        BindVersion();
    }
    function BindVersion() {
        $("#Version").children().remove();
        option = "<option value='0'>选择版本</option>";
        $("#Version").append(option);

        $.ajax({
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            type: "post",
            async: false,
            dataType: "json",
            data: { "PageName": "/BookSubject/SubJect.ashx", "Func": "Version", "SubjectName": $("#SubjectName").val(), IsPage: "false" },
            success: function (json) {
                var num = 0;
                $(json.result.retData).each(function () {
                    var option = "";
                    if (num == 0) {
                        option = "<option value='" + this.ID + "'  selected='selected'>" + this.Name + "</option>";
                        $("#VersionID").val(this.ID);
                        $("#Version").append(option);
                        BindGrade()
                    }
                    else {
                        option = "<option value='" + this.ID + "'>" + this.Name + "</option>";
                        $("#Version").append(option);
                    }
                    num++;
                })
            },
            error: function (errMsg) {
                layer.msg(errMsg);
            }
        });
    }
    function VersionChange() {
        $("#VersionID").val($("#Version").val());
        BindGrade();
    }
    function BindGrade() {
        $("#Grade").children().remove();
        option = "<option value='0'>选择版本</option>";
        $("#Version").append(option);

        $.ajax({
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            type: "post",
            async: false,
            dataType: "json",
            data: { "PageName": "/BookSubject/SubJect.ashx", "Func": "Grade", "SubjectName": $("#SubjectName").val(), IsPage: "false", "VersionID": $("#VersionID").val() },
            success: function (json) {
                var num = 0;
                $(json.result.retData).each(function () {
                    var option = "";
                    if (num == 0) {
                        option = "<option value='" + this.ID + "'  selected='selected'>" + this.Name + "</option>";
                        $("#GradeID").val(this.ID);
                        $("#Grade").append(option);
                    }
                    else {
                        option = "<option value='" + this.ID + "'>" + this.Name + "</option>";
                        $("#Grade").append(option);
                    }
                    num++;
                })
            },
            error: function (errMsg) {
                layer.msg(errMsg);
            }
        });
        Chapator();
    }
    function GradeChange() {
        $("#GradeID").val($("#Grade").val());
        Chapator();
    }
    var div = "";
    var TopMenuNum = 0;

    function Chapator() {
        $("#HChapterID").val("");

        $.ajax({
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            type: "post",
            async: false,
            dataType: "json",
            data: { "PageName": "/BookSubject/SubJect.ashx", "Func": "Chapator", "IsPage": "false", "SubName": $("#SubjectName").val(), VersionID: $("#VersionID").val(), GradeID: $("#GradeID").val() },
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    $("#SubjectID").val(this.SubjectID);
                    div = "";
                    BindleftMenu(json.result.retData, 0);
                    $("#menuChapater").html("");
                    $("#menuChapater").append(div);
                    menuSel();
                }
                else {
                    $("#menuChapater").html("");
                }

            },
            error: function (errMsg) {
                layer.msg(errMsg);
            }
        });
    }
    function BindleftMenu(data, id) {
        var i = 0;
        $(data).each(function () {

            if (this.Pid == 0 && this.Pid == id) {
                div += "<div class=\"units\">";
                div += " <div class=\"item_title\"><span class=\"text\" onclick=\"changeMenu(" + this.ID + ")\">" + this.Name + "</span><span class=\"icon icon-angle-down\"></span></div>";
                BindleftMenu(data, this.ID);
                if (i > 0) {
                    div += "</ul>";
                }
                i = 0;
                div += "</div>";
                TopMenuNum++;
            }
            if (this.Pid != 0 && this.Pid == id) {
                if (TopMenuNum == 0 && i == 0) {
                    div += "<ul class=\"contentbox\" style=\"display: block;\"><li class=\"active\" onclick=\"changeMenu(" + this.ID + ")\">\<span class=\"text\">" + this.Name + "</span> </li>";
                    $("#HChapterID").val(this.ID);
                    getData(1, 10);
                }
                if (TopMenuNum > 0 && i == 0) {
                    div += "<ul class=\"contentbox\">";
                }
                if (i > 0) {
                    div += "<li>\<span class=\"text\" onclick=\"changeMenu(" + this.ID + ")\">" + this.Name + "</span> </li>";
                }
                i++;
            }

        })
    }
    $(function () {
        //列表页与图标页切换
        $('.list_grid_switch').find('a').click(function () {
            $(this).addClass('on').siblings().removeClass('on');
            var n = $(this).index();
            $('.docu_content>ul').eq(n).show().siblings().hide();
        })
        BindSubject();
        GetFileType();
        DowDetail();
        $("#CatagoryManage").click(function () {
            window.location.href = "/ResourceManage/CatagoryManage.aspx";
        })
    });
    function DowDetail() {

        $.ajax({
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            type: "post",
            dataType: "json",
            data: { "PageName": "ResourceManage/PublicResoure.ashx", "PageIndex": 1, "PageSize": 10, "Func": "GetDowDetail" },
            success: OnDetailSuccess,
            error: OnDetailError
        });
    }
    function OnDetailSuccess(json) {
        if (json.result.errNum.toString() == "0") {
            $("#tb_DowDetail").html('');
            $("#tr_DowDetail").tmpl(json.result.retData.PagedData).appendTo("#tb_DowDetail");
        }
        else {
            var html = '<div style="background: url(/images/error.png) no-repeat center center; height: 500px;"></div>';
            $("#tb_DowDetail").html(html);
        }
    }
    function OnDetailError(XMLHttpRequest, textStatus, errorThrown) {
        $("#tb_DowDetail").html('无内容');
    }


    function FileGroup(GroupName, em) {
        $(em).addClass("on").siblings().removeClass("on");

        $("#HFoldUrl").val("/PubFolder/" + GroupName);
        $("#GroupName").val(GroupName);
        if ($("#ShowType").val() == "1") {
            getData(1, 10);
        }
        else {
            getData(1, 12);
        }
    }
    //绑定文件类型
    function GetFileType() {
        $.ajax({
            type: "Post",
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            data: { "PageName": "ResourceManage/PublicResoure.ashx", Func: "ResourceType" },
            dataType: "json",
            success: function (returnVal) {

                $(returnVal.result.retData).each(function () {
                    var result = "<li><a href='#' onclick=\"serchType('" + this.ID + "',this)\">" + this.Name + "</a>";
                    $("#ResourceType").append(result);
                });
            },
            error: function (errMsg) {
                layer.msg('数据加载失败！');
            }
        });
    }
    function serchType(Postfixs, em) {
        $(em).parent().addClass("on").siblings().removeClass("on");

        $("#Postfixs").val(Postfixs);
        if ($("#ShowType").val() == "1") {
            getData(1, 10);
        }
        else {
            getData(1, 12);
        }
    }
    //获取数据
    function ShowType(Type) {
        if (Type == "1") {
            $("#ShowType").val("1");
            getData(1, 10);
        }
        if (Type == "2") {
            $("#ShowType").val("2");
            getData(1, 12);
        }
    }
    function getData(startIndex, pageSize) {
        var DocName = $("#search_w").val();
        var ChapterID = $("#HChapterID").val();
        //初始化序号 
        pageNum = (startIndex - 1) * pageSize + 1;
        //name = name || '';
        $.ajax({
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            type: "post",
            dataType: "json",
            data: {
                "PageName": "ResourceManage/PublicResoure.ashx", "Func": "GetPageList", PageIndex: startIndex, pageSize: pageSize, "DocName": DocName,
                "GroupName": $("#GroupName").val(), "Postfixs": $("#Postfixs").val(), "IDCard": $("#HUserIdCard").val(), "ChapterID": ChapterID
            },
            success: function OnSuccess(json) {
                var ShowType = $("#ShowType").val();

                if (json.result.errNum.toString() == "0") {
                    if (ShowType == "1") {
                        $("#tb_MyResource").html('');
                        $("#tr_User").tmpl(json.result.retData.PagedData).appendTo("#tb_MyResource");
                    }
                    else {
                        $("#tb_MyResource1").html('');
                        $("#tr_User1").tmpl(json.result.retData.PagedData).appendTo("#tb_MyResource1");
                    }
                    $(".page").show();
                    laypage({
                        cont: 'pageBar', //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                        pages: json.result.retData.PageCount, //通过后台拿到的总页数
                        curr: json.result.retData.PageIndex || 1, //当前页
                        skip: false, //是否开启跳页
                        skin: '#1472b9',
                        jump: function (obj, first) { //触发分页后的回调
                            if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                getData(obj.curr, pageSize)
                            }
                        }
                    });
                    //makePageBar(getData, document.getElementById("pageBar"), json.result.retData.PageIndex, json.result.retData.PageCount, pageSize, json.result.retData.RowCount);
                    //列表页与图标页每项划过显示
                    hoverShow($('.document_list li'), $('.unload_none'));
                    hoverShow($('.docu_grid li'), $('.checkbox'));
                    Star();
                }
                else {
                    $(".page").hide();
                    var html = '<div style="background: url(/images/error.png) no-repeat center center; height: 500px;"></div>';
                    if (ShowType == "1") {
                        $("#tb_MyResource").html(html);
                    }
                    else { $("#tb_MyResource1").html(html); }
                }
            }
,
            error: OnError
        });
    }

    function OnError(XMLHttpRequest, textStatus, errorThrown) {
        var htmlBq = '<li style="text-align:center;">无内容</li>';
        if (ShowType == "1") {
            $("#tb_UserList").html(htmlBq);
        }
        else { $("#tb_UserList1").html(htmlBq); }
    }
    //评价
    function Star() {
        //stars评价
        $('.document_list').find(".assess").each(function () {
            var num = $(this).attr("id");
            if (num > 0) {
                $(this).find("span").eq(num - 1).siblings().removeClass('on');
                $(this).find("span").eq(num - 1).prevAll().andSelf().addClass('on');
            }
        })
    }
    function hoverShow(hoverObj, showObj) {
        showObj.find('.arrow-down').click(function () {
            $(this).find('.arrow_downwrap').show();
        });
        hoverObj.find('input[type=checkbox]').click(function () {
            if ($(this).is(':checked')) {
                $(this).parents('li').addClass('active');
            } else {
                $(this).parents('li').removeClass('active');
            }
        });
        hoverObj.hover(function () {
            $(this).find(showObj).show();
        }, function () {
            $(this).find(showObj).hide();
            showObj.find('.arrow_downwrap').hide();
            if ($(this).find('input[type=checkbox]').is(':checked')) {
                $(this).find(showObj).show();
            }
        })
    }
    //资源绑定课程
    function CourceResource() {
        var ids = "";
        $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
            if (this.checked) {
                ids += this.value + ",";
            }
        });
        var weikePic = "";
        var ResourcesID = ids;
        var CourceID = GetUrlDate.CourceID;
        var ChapterID = GetUrlDate.ChapterID;
        var IsVideo = GetUrlDate.IsVideo;

        $.ajax({
            url: "/CourseManage/Uploade.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: {
                func: "AddWeike", VidoeImag: weikePic, ResourcesID: ResourcesID, CourceID: CourceID, ChapterID: ChapterID, IsVideo: IsVideo
            },
            success: function (json) {
                var result = json.result;
                if (result.errNum == 0) {
                    parent.layer.msg('操作成功!');
                    if (IsVideo == "1") {
                        parent.BindWeikeResource();
                    }
                    else {
                        parent.BindPutongResource();
                    } parent.CloseIFrameWindow();
                } else {
                    layer.msg(result.errMsg);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.msg("操作失败！");
            }
        });
    }


    //文件下载
    function Down(id) {
        var ids = "";
        if (id != "") {
            ids = id;
        }
        else {
            $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
                if (this.checked) {
                    ids += this.value + ",";
                }
            });
        }
        if (ids != "") {

            $.ajax({
                url: "/ResourceManage/PublicResoure.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                //async: false,
                dataType: "json",
                data: { "PageName": "ResourceManage/PublicResoure.ashx", "Func": "Down", DownID: ids, "IDCard": $("#HUserIdCard").val() },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        window.open(json.result.retData);
                        DowDetail();
                    }
                    else { layer.msg('下载失败！'); }
                },
                error: function (errMsg) {
                    layer.msg('下载失败！');
                }
            });
        }
        else {
            layer.msg('请选择要下载的文件！');
        }
    }
    //文件删除
    function Del(id, name) {
        var len = 0;
        var ids = "";
        if (id != "") {
            ids = id;
        }
        else {
            $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
                if (this.checked) {
                    ids += this.value + ",";
                    len++;

                }
            });
        }
        if (ids != "") {

            if (name == "") {
                name = "这" + len + "个文件/文件夹"
            }
            if (confirm("确定要删除'" + name + "'吗？")) {
                $.ajax({
                    url: "/ResourceManage/PublicResoure.ashx",//random" + Math.random(),//方法所在页面和方法名
                    type: "post",
                    //async: false,
                    dataType: "json",
                    data: { "PageName": "ResourceManage/PublicResoure.ashx", "Func": "Del", DelID: ids },
                    success: function (json) {
                        if (json.result.errNum.toString() == "0") {
                            layer.msg("删除成功");
                            getData(1, 10)
                        }
                        else { layer.msg('删除失败！'); }
                    },
                    error: function (errMsg) {
                        layer.msg('删除失败！');
                    }
                });
            }
        }
        else {
            layer.msg('请选择要删除的文件！');
        }
    }

    //文件上传
    function upload() {
        var Pcode = $("#code").val();
        var FoldUrl = $("#HFoldUrl").val();
        var GroupName = $("#GroupName").val();
        var ChapterID = $("#HChapterID").val();
        //var CatagoryID = $("#HPeriod").val() + "|" + $("#HSubject").val() + "|" + $("#bookVersion").val() + "|" + $("#HTextboox").val();
        OpenIFrameWindow('文件上传', '/ResourceManage/PublicResouceUpload.aspx?FoldUrl=' + encodeURI(FoldUrl) +
            "&GroupName=" + encodeURI(GroupName)  + "&ChapterID=" + ChapterID + "&CreateUID=" + $("#HUserIdCard").val(), '400px', '300px');
    }
    //文件点击
    function FileClick(id, FoldUrl, code) {
        var FileExt = getFileName(FoldUrl);
        $.ajax({
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            type: "post",
            //async: false,
            dataType: "text",

            data: { "PageName": "ResourceManage/PublicResoure.ashx", "Func": "ResourceClick", "ID": id, "ClickType": "1", "IDCard": $("#HUserIdCard").val() },
            success: function (json) {
                if (FileExt == "ppt" || FileExt == "pptx" || FileExt == "doc" || FileExt == "docx" || FileExt == "xls" || FileExt == "xlsx" || FileExt == "pdf") {
                    $.ajax({
                        url: "/ResourceManage/MyResourceHander.ashx",
                        type: "post",
                        async: false,
                        dataType: "text",
                        data: {
                            "Func": "Wopi_Proxy", filepath: FoldUrl
                        },
                        success: function (result) {
                            window.open(result);
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            layer.msg("资源不存在！");
                        }
                    });
                }
                else {
                    DownLoad(FoldUrl);
                }
            },
            error: function (errMsg) {
                layer.msg(errMsg)
            }
        });
    }
    function getFileName(o) {
        //通过第一种方式获取文件名
        var pos = o.lastIndexOf(".");
        //查找最后一个\的位置
        return o.substring(pos + 1);
    }
    function DownLoad(FoldUrl) {
        $.ajax({
            url: "/ResourceManage/DownLoadHandler.ashx",
            type: "post",
            async: false,
            dataType: "text",
            data: {
                filepath: FoldUrl
            },
            success: function (result) {

                if (result == "-1") {
                    layer.msg('文件不存在!');
                    return;
                }
                else {
                    window.open("/ResourceManage/DownLoadHandler.ashx?filepath=" + FoldUrl + "&UserIdCard=" + $("#HUserIdCard").val() + "&time=" + new Date());
                    // location.href = "/OnlineLearning/DownLoadHandler.ashx?filepath=" + FoldUrl + "&UserIdCard=" + $("#HUserIdCard").val() + "&time=" + new Date();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.msg("资源不存在！");
            }
        });
    }
    //评价
    function Evalue(star, ID, em) { //stars评价
        if ($(em).parent().find(".on").length > 0) {
            layer.msg("不允许重复评论");
        }
        else {
            $(em).siblings().removeClass('on');
            $(em).prevAll().andSelf().addClass('on');

            $.ajax({
                url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                // async: false,
                dataType: "json",

                data: { "PageName": "ResourceManage/PublicResoure.ashx", "Func": "Evalue", "ID": ID, "ClickType": "2", "IDCard": $("#HUserIdCard").val(), "Evalue": star },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        layer.msg("评价成功");
                        getData(1, 10);
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
</html>

