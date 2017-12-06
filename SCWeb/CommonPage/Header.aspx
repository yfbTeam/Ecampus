<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Header.aspx.cs" Inherits="SCWeb.CommonPage.Header" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>头部</title>
    <%-- <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css">
    <link rel="stylesheet" href="/css/style.css">
    <script src="../Scripts/jquery-1.8.3.min.js"></script>
    <script src="../Scripts/jquery.tmpl.js"></script>
    <script src="../Scripts/Common.js"></script>--%>
    <script src="/Scripts/jquery.cookie.js"></script>
    <script src="/Scripts/zepto.min.js"></script>
    <script id="module_list" type="text/x-jquery-tmpl">
        <a href="${Href}" class="clearfix">
            <div class="corporation_icon fl" style="background: ${IconBg}">
                <i class="iconfont icon-${Icon}"></i>
            </div>
            <p class="fl">${Name}</p>
        </a>
    </script>
    <script id="nav_item" type="text/x-jquery-tmpl">
        <li nav="${Index}">
            <a href="${Href}">
                <i class="iconfont icon-${Icon}"></i>
                <span>${Name}</span>
            </a>
        </li>
    </script>
</head>
<body>
    <div class="top">
        <div class="center clearfix">
            <div class="corporation_type fl clearfix curpoiner pr">
                <div id="setItem">
                    <div class="corporation_icon fl">
                        <i class="iconfont icon-tuan"></i>
                    </div>
                    <span class="fl wenzi">学生社团
                            <i class="iconfont icon-down"></i>
                    </span>
                </div>
                <div class="wenzi_none tab_modult" style="width: 350px; min-height: 100px;" id="module_wrap">
                    <%-- <a href="javascript:;" class="clearfix">
                             <div class="corporation_icon fl bgqiangreen">
                                <i class="iconfont icon-tuan"></i>
                            </div>
                            <p class="fl">学生社团</p>
                        </a>
                       <a href="javascript:;" class="clearfix">
                             <div class="corporation_icon fl bghuang">
                                <i class="iconfont icon-huodong"></i>
                            </div>
                            <p class="fl">学生活动</p>
                        </a>
                         <a href="javascript:;" class="clearfix">
                             <div class="corporation_icon fl bgblue">
                                <i class="iconfont icon-msnui-disk-cloud"></i>
                            </div>
                            <p class="fl">个人网盘</p>
                        </a>
                        <a href="javascript:;" class="clearfix">
                             <div class="corporation_icon fl bgblue">
                                <i class="iconfont icon-ziyuan"></i>
                            </div>
                            <p class="fl">校本资源库</p>
                        </a>--%>
                </div>
            </div>

            <div class="top_right fr clearfix">
                <div class="messages fl clearfix curpoiner pr" onclick="javascript:window.location.href='/CommonPage/UserMessage.aspx'">
                    <div class="icon fl">
                        <i class="iconfont icon-message"></i>
                        <span id="span_MesCount">0</span>
                    </div>
                    <span class="fl wenzi">消息
                                <i class="iconfont icon-down"></i>
                    </span>
                    <div class="wenzi_none">
                    </div>
                </div>
                <div class="settings fl ml20 curpoiner pr">
                    <span class=" wenzi"><%=this.userName %>
                        <i class="iconfont icon-down"></i>
                    </span>
                    <div class="wenzi_none">
                        <a href="javascript:;" class="loginout" id="loginOut">退出</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="logonav">
        <div class="center clearfix">
            <a class="logo fl">
                <img src="/images/logo.png" alt="">
            </a>
            <nav class="fr nav" id="nav">
                <ul id="ul_HeadNav" class="clearfix"></ul>
            </nav>
        </div>
    </div>
    <script>

        var navAry = [];// [{ "IconBg": "#1483f3", "Icon": "shetuan", "Name": "学生社团", "Href": "/StuAssociate/AssoIndex.aspx", "NavLists": [{ "Index": "01", "Href": "", "Icon": "", "Name": "我的社团" }, { "Index": "11", "Href": "", "Icon": "", "Name": "首页" }] }, { "IconBg": "#949615", "Icon": "", "Name": "模块管理", "Href": "/DeskManage/ModelManage.aspx", "NavLists": [] }, { "IconBg": "OrangeRed", "Icon": "", "Name": "角色管理", "Href": "/SystemSettings/RoleSettings.aspx", "NavLists": [] }, { "IconBg": "#81af80", "Icon": "", "Name": "分类管理", "Href": "/DeskManage/ModelCatogry.aspx", "NavLists": [] }, { "IconBg": "#c856ee", "Icon": "wangpan", "Name": "个人网盘", "Href": "/Synergy/SkyDrivePersonal.aspx", "NavLists": [] }, { "IconBg": "#a9b925", "Icon": "", "Name": "日程协同", "Href": "/ScheduleCollaboration.aspx", "NavLists": [] }, { "IconBg": "blue", "Icon": "ziyuan", "Name": "校本资源库", "Href": "/ResourceManage/PublicResoure.aspx", "NavLists": [] }];/* [
        /*{
            IconBg: 'bgqiangreen',
            Icon: 'icon-tuan',
            Name: '学生社团',
            Href:'/StuAssociate/AssoIndex.aspx',
            NavLists: [
                {
                    Index:'1',
                    Href: '/StuAssociate/AssoIndex.aspx',
                    Icon: 'icon-home',
                    Name: '首页'
                },
                {
                    Index: '2',
                    Href: '/StuAssociate/AssociateDetail.aspx',
                    Icon: 'icon-orangize',
                    Name: '我的社团'
                },
                {
                    Index: '3',
                    Href: '/StuAssociate/AllAssociateManage.aspx',
                    Icon: 'icon-all',
                    Name: '全部社团'
                },
                {
                    Index: '4',
                    Href: '/StuAssociate/AssoMemberManage.aspx',
                    Icon: 'icon-manage',
                    Name: '社团成员管理'
                }
            ]
        },
        {
            IconBg: 'bghuang',
            Icon: 'icon-huodong',
            Name: '学生活动',
            Href: '/StuActivity/ActiIndex.aspx',
            NavLists: [
                {
                    Index:'1',
                    Href: '/StuActivity/ActiIndex.aspx',
                    Icon: 'icon-home',
                    Name: '活动首页'
                },
                {
                    Index: '2',
                    Href: '/StuActivity/AllActivity.aspx',
                    Icon: 'icon-huodong',
                    Name: '全部活动'
                },
                {
                    Index: '3',
                    Href: '/StuActivity/DepartDetail.aspx',
                    Icon: 'icon-studenthui',
                    Name: '学生会'
                },
                {
                    Index: '4',
                    Href: '/StuActivity/DepartRegistManage.aspx',
                    Icon: 'icon-manage',
                    Name: '报名管理'
                }
            ]
        },
        {
            IconBg: 'bgblue',
            Icon: 'icon-msnui-disk-cloud',
            Name: '个人网盘',
            Href: '/Synergy/SkyDrivePersonal.aspx',
            NavLists: [
                {
                    Index: '1',
                    Href: '/Synergy/SkyDrivePersonal.aspx',
                    Icon: 'icon-msnui-disk-cloud',
                    Name: '个人网盘'
                }
            ]
        },
        {
            IconBg: 'bgblue',
            Icon: 'icon-ziyuan',
            Name: '校本资源库',
            Href: '/ResourceManage/PublicResoure.aspx',
            NavLists: [
                {
                    Index: '1',
                    Href: '/ResourceManage/PublicResoure.aspx',
                    Icon: 'icon-ziyuan',
                    Name: '校本资源库'
                },
                {
                    Index: '2',
                    Href: '/ResourceManage/CatagoryManage.aspx',
                    Icon: 'icon-manage',
                    Name: '目录管理'
                }
            ]
        },
        {
            IconBg: 'bgorange',
            Icon: 'icon-xietong',
            Name: '日程协同',
            Href: '/ScheduleCollaboration.aspx',
            NavLists: [
                {
                    Index: '1',
                    Href: '/ScheduleCollaboration.aspx',
                    Icon: 'icon-xietong',
                    Name: '日程协同'
                }
            ]
        },
        {
            IconBg: 'bgblue',
            Icon: 'icon-ziyuan',
            Name: '分类管理',
            Href: '/DeskManage/ModelCatogry.aspx',
            NavLists: [
                {
                    Index: '1',
                    Href: '/DeskManage/ModelCatogry.aspx',
                    Icon: 'icon-ziyuan',
                    Name: '分类管理'
                }
            ]
        },
        {
            IconBg: 'bgorange',
            Icon: 'icon-xietong',
            Name: '模块管理',
            Href: '/DeskManage/ModelManage.aspx',
            NavLists: [
                {
                    Index: '1',
                    Href: '/DeskManage/ModelManage.aspx',
                    Icon: 'icon-xietong',
                    Name: '模块管理'
                }
            ]
        }
    ];
    function GetNavData() {
        $.ajax({
            url: "/Common.ashx",
            type: "post",
            async: false,
            dataType: "json",
            data: {
                PageName: "/DeskManage/AppHandler.ashx", "Func": "GetMenu", Ispage: "false", "ModelType": ModelType
            },
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    navAry = json.result.retData;                       
                }
                else {
                    $("#tb_list").html("<tr><td colspan='2' style='text-align:center;'>暂无模块！</td></tr>");
                    $("#table_tree").treetable({ expandable: true });
                }
            },
            error: function (errMsg) {
                $("#tb_list").html("<tr><td colspan='2' style='text-align:center;'>暂无模块！</td></tr>");
                $("#table_tree").treetable({ expandable: true });
            }
        });
    }*/
        //设置当前模块的localStorage
        function setlocalStorage() {
            var LocalUrl = window.location;
            $('#module_wrap a').each(function (index, item) {
                var itemHref = $(item).attr("href");
                var localHref = window.location;
                if (localHref.toString().indexOf(itemHref) > 0) {
                    localStorage.removeItem('current');
                    var iconbg = $(item).find('div').attr('style');
                    //iconbg = iconbg[iconbg.length - 1];
                    var icon = $(item).find('div').children('i').attr('class').split(' ');
                    icon = icon[icon.length - 1]
                    var obj = {
                        name: $(item).find('p').html(),
                        iconbg: iconbg,
                        icon: icon,
                        index: index
                    }
                    localStorage.setItem('current', JSON.stringify(obj));
                }
            })
        }
        var initTopNav = (function () {
            return {
                initTop: (function () {
                    $("#module_wrap,#ul_HeadNav").html('');
                    $.ajax({
                        url: "/Common.ashx",
                        type: "post",
                        async: false,
                        dataType: "json",
                        data: {
                            PageName: "/DeskManage/AppHandler.ashx", "Func": "GetMenu", Ispage: "false"
                        },
                        success: function (json) {
                            if (json.result.errNum.toString() == "0") {
                                navAry = eval(json.result.retData);

                                $("#module_list").tmpl(navAry).appendTo("#module_wrap");
                                $('#nav_item').tmpl(navAry[0].NavLists).appendTo('#ul_HeadNav');

                            }
                            else {

                            }
                        },
                        error: function (errMsg) {
                        }
                    });
                    setlocalStorage();
                    $('#module_wrap a').each(function (index, item) {
                          $(item).click(function (e) {
                            localStorage.removeItem('current');
                            var iconbg = $(this).find('div').attr('style');
                            //iconbg = iconbg[iconbg.length - 1];
                            var icon = $(this).find('div').children('i').attr('class').split(' ');
                            icon = icon[icon.length - 1]
                            var obj = {
                                name: $(this).find('p').html(),
                                iconbg: iconbg,
                                icon: icon,
                                index: index
                            }
                            localStorage.setItem('current', JSON.stringify(obj));
                        });
                    })
                })(),
                setItem: (function () {
                    var current = localStorage.getItem('current');
                    if (current) {
                        current = JSON.parse(current);
                        $('#setItem').find('div').attr('style',current.iconbg);//('class', 'corporation_icon fl ' + current.iconbg);
                        $('#setItem').find('div').children('i').attr('class', 'iconfont ' + current.icon);
                        $('#setItem').find('span').html(current.name + '<i class="iconfont icon-down"></i>');
                        $('#ul_HeadNav').html('');
                        $('#nav_item').tmpl(navAry[current.index].NavLists).appendTo('#ul_HeadNav');                        
                    }
                })()
            }
        })();
        $('.corporation_type').hover(function () {
            $(this).find('.tab_modult').slideDown();
        }, function () {
            $(this).find('.tab_modult').stop().slideUp();
        })
        $.ajaxSetup({
            cache: false //关闭AJAX相应的缓存 
        });
        var reg1 = /AppleWebKit.*Mobile/i, reg2 = /MIDP|SymbianOS|NOKIA|SAMSUNG|LG|NEC|TCL|Alcatel|BIRD|DBTEL|Dopod|PHILIPS|HAIER|LENOVO|MOT-|Nokia|SonyEricsson|SIE-|Amoi|ZTE/;
        $(function () {
            LoginOut('#loginOut');
            //检测手机端执行方法
            if (reg1.test(navigator.userAgent) || reg2.test(navigator.userAgent)) {

                $('#nav').layerFloor({ event: 'tap', child: '#nav ul' })
            }
            InitMsgCount();
        });
        function SetNavSelected(navVal, a_OneCrumbs) {
            a_OneCrumbs = arguments[1] || "";
            var navObj = $('#ul_HeadNav').find('li[nav="' + navVal + '"]');
            if (navObj) {
                navObj.addClass('selected').siblings().removeClass('selected');
                if (a_OneCrumbs != "") {
                    $a_OneCrumbs = $("#" + a_OneCrumbs);
                    $a_OneCrumbs.attr('href', navObj.find('a').attr('href'));
                    $a_OneCrumbs.html(navObj.find('span').html());
                }
            }
        }
        function InitMsgCount() {
            $span_MesCount = $('#span_MesCount');
            $.ajax({
                url: "/Common.ashx",
                type: "post",           
                dataType: "json",
                data: {
                    PageName: "/CommonHandler/Com_MessageHandler.ashx",
                    Func: "GetComMessageDataPage",
                    Receiver:'<%=UserInfo.UniqueNo%>',
                    Status: 0,
                    Ispage: false
                },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        var items = json.result.retData;
                        if (items != null && items.length > 0) {
                            $span_MesCount.html(items.length);

                        } else {
                            $span_MesCount.html("0");
                        }
                    }
                    else {
                        $span_MesCount.html("0");
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {}
            });
        }
    </script>
</body>
</html>
