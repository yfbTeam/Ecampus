<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssoIndex.aspx.cs" Inherits="SCWeb.StuAssociate.AssoIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
    <title>社团首页</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css"/>
    <link rel="stylesheet" href="/css/style.css"/>
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/laypage/laypage.js"></script>
    <script src="/CommonPage/NewInfoCommon.js"></script>
    <%--社团分类下的社团--%>
    <script id="li_sub" type="text/x-jquery-tmpl">
        <li>
            <a href="/StuAssociate/AssociateDetail.aspx?itemid=${Id}&nav=1">
                <div>
                    <img src="${PicURL}" onerror="javascript:this.src='/images/assodefault.jpg'" alt="Alternate Text" /></div>
                <h1>${Name}</h1>
                <p>成员：${MemCount}人</p>
            </a>
        </li>
    </script>
     <%--我的社团--%>
    <script id="a_MyAssoSub" type="text/x-jquery-tmpl">
        <a href="/StuAssociate/AssociateDetail.aspx?itemid=${Id}&nav=1">
            <div class="member_img">
                <img src="${PicURL}" onerror="javascript:this.src='/images/assodefault.jpg'"/>
            </div>
            <p class="member_name">${Name}</p>
        </a>
    </script>
    <%--社团排行榜--%>
    <script id="dl_RankSub" type="text/x-jquery-tmpl">
        <dl class="fl" style="cursor:pointer;" onclick="javascript:window.location.href='/StuAssociate/AssociateDetail.aspx?itemid=${Id}&nav=1'">
            {{if rowNum<=3}}<div class="top">${rowNum}</div>{{/if}}
            <dt>
                <img src="${PicURL}" onerror="javascript:this.src='/images/assodefault.jpg'" alt="Alternate Text" /></dt>
            <dd>
                <h1>${Name}</h1>
                <p>成员：${MemCount}人</p>
            </dd>
        </dl>
    </script>
    <%--社团公告--%>
    <script id="li_Newest" type="text/x-jquery-tmpl">       
        <li>
            <span>new</span>
            <a href="/CommonPage/NewNoticeDetail.aspx?type=0&newtype=1&itemid=${Id}&nav=1">${Name}</a>
        </li>
    </script>
    <%--热门帖子--%>
    <script id="div_HotNewSub" type="text/x-jquery-tmpl">
        <div class="mnsc">
            <div class="posts_img">
                <i class="img">
                    <img src="${AbsHeadPic}" onerror="javascript:this.src='/images/member_img.png'" alt="" />
                </i>
                <p class="name">${CreateName}</p>
                <%--<div class="posts_imgnone">
                    <div class="imgnone_top">
                        <i class="img">
                            <img src="/images/member_img.png" alt="" />
                        </i>
                        <p class="name">李淼淼 <i class="iconfont icon-woman"></i></p>
                    </div>
                    <div class="community pr clearfix" style="padding: 20px 0px 10px 0px;">
                        <label for="">社团</label>
                        <div class="community_con">
                            <a href="javascript:;" class="img">
                                <img src="/images/soeicty_img.png" alt="" /></a>
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
            <div class="post_titledate clearfix" onclick="javascript:window.location.href='/CommonPage/NewInfoDetail.aspx?type=0&newtype=${NewType}&itemid=${Id}&nav=1'">
                <div class="post_title fl">
                    <a href="javascript:;">${Name}</a>
                </div>
                <div class="post_date fr">${getDateDiff(CreateTime_Stamp*1000)}</div>
            </div>
            <div class="post_content">{{html Content}}</div>            
            <div class="answer_tools clearfix">
                <div class="last_time fl">
                    <span class="mr10">${RelationName}</span>|{{if LastComUID=='暂无回复'}}<span class="ml10">最后回复：暂无回复</span>{{else}} <span class="ml10">最后回复：${TwoUserName}</span><span class="ml10">${getDateDiff(LastComTime*1000)}</span>{{/if}}
                </div>
                <div class="answer_tool fr clearfix">
                    <a href="javascript:;">
                        <i class="iconfont icon-see"></i>
                        <span>${BrowsingTimes}</span>
                    </a>
                    <a href="javascript:;">
                        <i class="iconfont icon-commit"></i>
                        <span>${CommontCount}</span>
                    </a>
                    <a href="javascript:;" onclick="GoodClick(${Id},'span_goodcount${Id}');">
                        <i class="iconfont icon-praise"></i>
                        <span id="span_goodcount${Id}">${ClickCount}</span>
                    </a>
                </div>
            </div>
        </div>
    </script>
</head>
<body>
   <input type="hidden" id="HLoginUID" value="<%=UserInfo.UniqueNo%>"/>
   <div id="header"></div>
   <div id="main" style="background:#c5eafb url(/images/bg_01.jpg) no-repeat center top;">
       <div class="center">
           <div class="banner pt10">
               <img src="/images/banner.jpg" alt="Alternate Text" />
           </div>
           <div class="clearfix mt10">
               <div class="left fl">
                   <div class="wrap_left">
                       <nav class="nav_index">
                            <ul id="ul_TypeAsso" class="clearfix"></ul>
                        </nav>
                        <div class="tab_wraps">
                            <div class="tab_wrap">
                                <ul id="ul_list" class="asso_type clearfix"></ul>
                            </div>                         
                        </div>
                   </div>
                   <div class="wrap_left mt10">
                       <div class="pl20 pr20">
                           <div class="clearfix reply_tools mt10 pr">
                                <div class="reply_left fl clearfix">
                                    <ul class="clearfix">
                                        <li class="selected"><a href="javascript:;">热门帖子</a></li>
                                        <li><a href="javascript:;">招新通知</a></li>
                                    </ul>
                                </div>
                            </div>
                           <div class="posts_lists">
                                <div id="div_HotNew" class="posts_item clearfix"></div>
                            </div>
                       </div>
                   </div>
                   <%--<div class="wrap_left mt10">
                       <div class="loadding">
                           加载中 <i class="iconfont icon-loadding"></i>
                       </div>
                   </div>--%>
               </div>
               <div class="wrap_right fr">
                   <div class="modult">
                       <div class="clearfix pt10 pb10" style="border-bottom:1px solid #dcdcdc;">
                            <div class="community_img fl" style="width:61px;height:61px;margin-left:30px;">
                                <img src="<%=UserInfo.AbsHeadPic%>" onerror="javascript:this.src='/images/member_img.png'" alt=""/>
                            </div>
                            <div class="fl ml10">
                                <h1 class="community_name" style="font-size:16px;color:#333;line-height:38px;"><%=UserInfo.Name%></h1>
                                <a href="javascript:;" style="font-size:12px;color:#999999;">欢迎回来</a>
                            </div>
                           <a href="/StuAssociate/AssociateDetail.aspx?nav=2" class="fl" style="line-height:61px;text-align:center;font-size:14px;color:#333;display:block;width:114px;margin-left:30px;border-left:1px solid #dcdcdc;">
                               我的社团
                           </a>
                       </div>
                       <div id="div_MyAsso" class="add_members clearfix pt10"></div>
                        <h1 style="padding:5px 0px;">公告</h1>
                        <ul id="ul_Newest"  class="Newposts_lists newpost"></ul>
                   </div>
                   <div class="modult mt10">
                        <h1 class="title">社团排行榜 <a href="/StuAssociate/AllAssociateManage.aspx" class="fr more">更多</a></h1>
                       <div id="div_Rank" class="asso_top clearfix"></div>
                    </div>
               </div>
           </div>
       </div>
   </div>
   <div id="footer"></div>
    <script>
        var typeidArray = [];
        $(function () {
            SetPageButton('<%=UserInfo.UniqueNo%>');

            $('#header').load('/CommonPage/Header.aspx?t='+new Date().getTime(), function (response, status, xhr) {
                if (status == "success" || status == "notmodified") {
                    SetNavSelected(1);
                }
            });
            $('#footer').load('/CommonPage/footer.html');
            MoveFly('.posts_lists .posts_img', '.posts_imgnone');
            navWidth('.nav_index ul');
            tab('.nav_index ul', '.tab_wraps', '.btn_wraps');
            GetAssoType(1, 4);//获取社团分类
            GetMyAsso();//获取我的社团
            GetAssoRank(1, 5); //社团排行榜
            GetHotComNew(1, 5); //默认热门帖子
            //帖子和通知切换
            $('.reply_left li').click(function () {
                $(this).addClass('selected').siblings().removeClass('selected');
                if ($(this).index() == 0) { GetHotComNew(1, 5); } else { GetRecruitNotice(1, 5); }
            });
            GetNewestNew(0, '', 'ul_Newest', 'li_Newest', 1, 3, 1);//加载最新公告            
        });
        //获取社团分类
         function GetAssoType(startIndex, pageSize) {
            $("#ul_TypeAsso").html('<li class="selected"><a href="javascript:;"><div><span>全部社团</span></div></a></li>');
            $.ajax({
                url: "/Common.ashx",
                type: "post",         
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_TypeHandler.ashx",
                    Func: "GetAssoTypeDataPage",
                    PageIndex: startIndex,
                    PageSize: pageSize
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        typeidArray = [];
                        $(json.result.retData.PagedData).each(function (i, n) {
                            typeidArray.push(n.Id);
                            $("#ul_TypeAsso").append('<li id="li_assotype'+n.Id+'"><a href="javascript:;"><div><span>' + n.Name + '</span></div></a></li>');
                        });
                        $("#ul_TypeAsso").append('<li id="li_assotype-1"><a href="javascript:;"><div><span>其他</span></div></a></li>');
                        $('#ul_TypeAsso li').click(function () {
                            $(this).addClass('selected').siblings().removeClass('selected');
                            getData(1, 10);
                        });
                        getData(1, 10);
                    } else {
                        $("#ul_list").html('<li>暂无社团！</li>');
                    }
                }
            });
        }
        //获取分类下的社团
        function getData(startIndex, pageSize) {
            var assotype = $('#ul_TypeAsso').find("li.selected")[0].id.replace("li_assotype", "");
            var params ={ //暂不分页
                PageName: "/StuAssociate/Asso_InfoHandler.ashx",
                Func: "GetAssoInfoDataPage",
                ispage: false,
                AssoType: assotype
            };
            if (assotype.toString() == "-1") {
                params["UnAssoTypes"] = typeidArray.join(',');
            }
            $.ajax({
                url: "/Common.ashx",
                type: "post",            
                dataType: "json",
                data: params,
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#ul_list").html('');
                        var rtnObj = json.result.retData;
                        $("#li_sub").tmpl(rtnObj).appendTo("#ul_list");
                    }
                    else {
                        $("#ul_list").html('<li style="width:100%;text-align:center;color:#333;">暂无社团！</li>');
                    }
                },
                error: function (errMsg) {
                    $("#ul_list").html('<li style="width:100%;text-align:center;color:#333;">暂无社团！</li>');
                }
            });
        }
        //获取我的社团
        function GetMyAsso() {                      
            $.ajax({
                url: "/Common.ashx",
                type: "post",         
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_InfoHandler.ashx",
                    Func: "GetAssoInfoDataPage",
                    LoginUID: "<%=UserInfo.UniqueNo%>",
                    MyUserNo:"<%=UserInfo.UniqueNo%>",
                    ispage: false                    
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#div_MyAsso").html('');
                        var rtnObj = json.result.retData;
                        $("#a_MyAssoSub").tmpl(rtnObj).appendTo("#div_MyAsso");
                    }
                    else {
                        $("#div_MyAsso").html('<div>暂无社团！</div>');
                    }
                },
                error: function (errMsg) {
                    $("#div_MyAsso").html('<div>暂无社团！</div>');
                }
            });
        }
        //社团排行榜
          function GetAssoRank(startIndex, pageSize) {
            $.ajax({
                url: "/Common.ashx",
                type: "post",             
                dataType: "json",
                data: {
                    PageName: "/StuAssociate/Asso_InfoHandler.ashx",
                    Func: "GetAssoInfoDataPage",
                    OrderBy: "MemCount desc",
                    PageIndex: startIndex,
                    PageSize: pageSize
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#div_Rank").html('');
                        var rtnObj = json.result.retData;
                        $("#dl_RankSub").tmpl(rtnObj.PagedData).appendTo("#div_Rank");
                    }
                    else {
                        $("#div_Rank").html('<div>暂无社团！</div>');
                    }
                },
                error: function (errMsg) {
                    $("#div_Rank").html('<div>暂无社团！</div>');
                }
            });
        }
    </script>   
</body>
</html>
