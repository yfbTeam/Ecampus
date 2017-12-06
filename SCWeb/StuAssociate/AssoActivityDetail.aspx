<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssoActivityDetail.aspx.cs" Inherits="SCWeb.StuAssociate.AssoActivityDetail" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>活动详情</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/CommonPage/NewInfoCommon.js"></script>
    <%--社团活动--%>
    <script id="div_ActivitySub" type="text/x-jquery-tmpl">
        <div class="posts_lists">
            <div class="posts_item clearfix">
                <div class="mnsc">
                    <div class="posts_img">
                        <i class="img"><img src="${AbsHeadPic}" onerror="javascript:this.src='/images/member_img.png'" alt=""></i>
                        <p class="name">${CreateName}</p>
                        <%--<div class="posts_imgnone">
                            <div class="imgnone_top">
                                <i class="img">
                                    <img src="images/member_img.png" alt="">
                                </i>
                                <p class="name">李淼淼 <i class="iconfont icon-woman"></i></p>
                            </div>
                            <div class="community pr clearfix" style="padding: 20px 0px 10px 0px;">
                                <label for="">社团</label>
                                <div class="community_con">
                                    <a href="javascript:;" class="img">
                                        <img src="images/soeicty_img.png" alt=""></a>
                                </div>
                            </div>
                            <div class="community pr clearfix" style="border-top: 1px solid #F2F5F5;">
                                <label for="">社团</label>
                                <div class="community_con">
                                    <p class="sign">没有什么过不去，只是再也回不去</p>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                    <div class="post_titledate clearfix">
                        <div class="post_title fl"><a href="javascript:;">${Name}</a></div>
                        <div class="answer_tool fr clearfix">
                            <a href="javascript:;">
                                <i class="iconfont icon-see"></i>
                                <span>${BrowsingTimes}</span>
                            </a>                            
                           
                            <div class="setting fl pr ml10" {{if IsLeader==0}}style="display:none;"{{/if}}>
                                <span>
                                    <i class="iconfont icon-settings fl ml10"></i>
                                    <i class="iconfont icon-xia fr mr10"></i>
                                </span>
                                <div class="settings_none">
                                    <em onclick="EditAssoActivity(${Id});">修改</em>
                                    <em style="display:none;">删除</em>                                    
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="post_date mb10">发表于：${DateTimeConvert(CreateTime,'yyyy-MM-dd HH:mm:ss')}</div>
                </div>
                <div class="post_imgs clearfix tc">
                    <img src="${ActivityImg}" onerror="javascript:{{if ActivityImg==''}}$(this).hide();{{else}}this.src='/images/defaultimg.jpg';{{/if}}" alt="">
                </div>
                <div class="answer_tools clearfix">
                    <div class="last_time fl mt10">
                        <span style="display: block">活动时间：${DateTimeConvert(StartTime,'yyyy-MM-dd')}~${DateTimeConvert(EndTime,'yyyy-MM-dd')} </span>
                        <span style="display: block">地点： ${Address}</span>
                    </div>
                    <div class="fr">
                        <input id="btn_Join" type="button" class="btn bggreen fr" {{if IsJoin<=0 && ActStatus==0}}onclick="JoinAssoActivity(${Id});"{{/if}} value="${IsJoin<=0?(ActStatus==0?'我要参加':(ActStatus==1?'正在进行中':'已结束')):'已报名'}" />
                        <div class="clear"></div>
                        <div class="mt10">
                            <span>${MemCount}人参加</span>
                            <%--<a href="javascript:;" class="ml10" style="color: #74c3f4">查看详情</a>--%>  <%--以后完善--%>
                        </div>
                    </div>
                </div>
            </div>

            <div class="activity_detail ">
                <h1 class="title mt10 mb10">活动详情</h1>
                <div class="activity_content ">{{html Content}}</div>
            </div>
        </div>
    </script>
    <%--社团最新贴--%>
    <script id="li_Newest" type="text/x-jquery-tmpl">
        <li>
            <span>${rowNum}</span>
            <a href="javascript:;">${Name}</a>
        </li>
    </script>
    <%--已报名成员--%>
    <script id="div_JoinMemSub" type="text/x-jquery-tmpl">
        <a href="javascript:;">
            <div class="member_img">
                <img src="${AbsHeadPic}" onerror="javascript:this.src='/images/member_img.png'" alt="">
            </div>
            <p class="member_name">${Name}</p>
        </a>
    </script>
</head>
<body>
    <div id="header"></div>
    <div id="main">
        <div class="center clearfix" style="min-height:655px;">
            <div style="height: 60px; position: fixed; width: 1200px; background: #C5EAFB; z-index: 99;">
                <div class="crumbs" style="margin-top: 10px;">
                    <a href="javascript:;" id="a_OneCrumbs"></a>
                    <span>&gt;</span>
                    <a href="javascript:;" id="a_TwoCrumbs"></a>
                    <span>&gt;</span>
                    <a href="javascript:;" id="a_ThreeCrumbs">活动</a>
                </div>
            </div>
            <div class="wrap_left fl" style="margin-top:60px;">
                <div id="div_Activity" class="pl20 pr20"></div>
            </div>
            <div class="wrap_right fr" style="margin-top: 60px;">
                <div class="btn_wraps">
                    <div class="btn_wrap">
                        <a href="javascript:;" id="a_AddActivity" class="MySpeak bggreen"><i class="iconfont icon-edit"></i>发起活动</a>
                    </div>
                </div>
                <div class="modult mt10 clearfix">
                    <div class="community_img fl">
                        <img id="img_Pic" src="images/soeicty_img.png" alt="">
                    </div>
                    <div class="fl">
                        <h1 id="h1_RelName" class="community_name"></h1>
                        <a id="a_IntoWord" href="javascript:;">进入社团></a>
                    </div>
                </div>
                <div class="modult mt10" style="min-height: 290px;">
                    <h1 class="title">社团最新贴</h1>
                    <ul id="ul_Newest" class="Newposts_lists"></ul>
                </div>
                <div class="modult mt10">
                    <h1 class="title">已报名成员</h1>
                    <div id="div_JoinMem" class="add_members clearfix pt10"></div>
                </div>
            </div>
        </div>
    </div>
    <div id="footer"></div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        var navid = UrlDate.nav;
        var curEditor;
        $(function () {
            $('#header').load('/CommonPage/Header.aspx?t='+new Date().getTime(), function (response, status, xhr) {
                if (status == "success" || status == "notmodified") {
                    SetNavSelected(navid,'a_OneCrumbs');
                    var itemid = UrlDate.itemid;
                    if (itemid != 0) {
                        BrowsingTimesSet(itemid, "/StuAssociate/Asso_ActivityHandler.ashx");
                        GetAssoActivityData(1, 1);
                    }                    
                }
            });
            $('#footer').load('/CommonPage/footer.html');
        });
        //加载社团活动
        function GetAssoActivityData(startIndex, pageSize) {                               
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { 
                    PageName: "/StuAssociate/Asso_ActivityHandler.ashx", 
                    Func: "GetAssoActivityDataPage", 
                    Id: UrlDate.itemid,
                    LoginUID: "<%=UserInfo.UniqueNo%>",
                    ispage: false,
                    IsOnlyBase: "1",
                    IsUnifiedInfo: "1"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#div_Activity").html('');
                        var rtnObj = json.result.retData;                        
                        $("#div_ActivitySub").tmpl(rtnObj).appendTo("#div_Activity");
                        ControlInitConfig(rtnObj[0]);                     
                        GetJoinMember(rtnObj[0].JoinMembers);                      
                    }
                    else {
                        $("#div_Activity").html('<div>暂无活动！</div>');
                    }                    
                },
                error: function (errMsg) {
                    $("#div_Activity").html('<div>暂无活动！</div>');
                }
            });
        }
        function GetJoinMember(memberStr) {
            if (memberStr) {
                $.ajax({
                    url: "/CommonHandler/UnifiedHelpHandler.ashx",
                    type: "post",               
                    dataType: "json",
                    data: { "Func": "GetUserInfoData", UniqueNos: memberStr },
                    success: function (json) {
                        if (json.result.errNum.toString() == "0") {
                            $("#div_JoinMem").html('');
                            var rtnObj = json.result.retData;
                            $("#div_JoinMemSub").tmpl(rtnObj).appendTo("#div_JoinMem");
                        }
                        else {
                            $("#div_JoinMem").html('<a style="width:80px;">暂无成员！</a>');
                        }
                    },
                    error: function (errMsg) {
                        $("#div_JoinMem").html('<a style="width:80px;">暂无成员！</a>');
                    }
                });
            } else {
                $("#div_JoinMem").html('<a style="width:80px;">暂无成员！</a>');
            }            
        }
        function ControlInitConfig(model) { //控件初始化设置
            if (model != '') {
                var $img_Pic = $('#img_Pic');
                var $h1_RelName = $('#h1_RelName');
                var $a_IntoWord = $('#a_IntoWord');
                $img_Pic.attr("src", model.PicURL); //设置社团图片
                $h1_RelName.html(model.AssoName);//设置社团名称
                var $a_TwoCrumbs = $('#a_TwoCrumbs'); //二级面包屑
                var $a_ThreeCrumbs = $('#a_ThreeCrumbs'); //三级面包屑
                $a_TwoCrumbs.html(model.AssoName);
                $a_TwoCrumbs.attr('href', '/StuAssociate/AssociateDetail.aspx?itemid=' + model.AssoId + '&nav=' + navid+ "&tab=1");
                $a_IntoWord.click(function () { //设置社团路径
                    location.href = '/StuAssociate/AssociateDetail.aspx?itemid=' + model.AssoId + '&nav=' + navid;
                });
                var $a_AddActivity = $('#a_AddActivity'); //发起活动按钮                
                if (model.IsLeader == 1) {
                    $a_AddActivity.show();
                    $a_AddActivity.click(function () {
                        OpenIFrameWindow('发起活动', '/StuAssociate/EditAssoActivity.aspx?itemid=0&relid=' + model.AssoId, '900px', '540px');
                    });
                } else {
                    if (model.IsMember== 0) { }
                    $a_AddActivity.hide();
                }                
                GetNewestNew(0, model.AssoId);
            }
        }
        function EditAssoActivity(itemid) {
            OpenIFrameWindow('编辑活动', '/StuAssociate/EditAssoActivity.aspx?itemid=' + itemid, '900px', '540px');
        }
        function JoinAssoActivity(itemid) {
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_ActivityHandler.ashx",
                    Func: "JoinAssoActivity",
                    ItemId: itemid,
                    LoginUID: "<%=UserInfo.UniqueNo%>"
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == 0) {                        
                        layer.msg('报名成功!');
                        GetAssoActivityData(1,1);
                    }else if(result.errNum == -1) {
                        layer.msg('亲，来晚了，活动已在进行中!');
                    }
                    else if(result.errNum == -2) { 
                        layer.msg('亲，来晚了，活动已结束!');
                    }
                },
                error: function (errMsg) { }
            });            
        }
    </script>
</body>
</html>
