<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewNoticeDetail.aspx.cs" Inherits="SCWeb.CommonPage.NewNoticeDetail" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>公告详情</title>       
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css"/>
    <link rel="stylesheet" href="/css/style.css"/>
    <script src="/Scripts/jquery-1.8.3.min.js"></script>    
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/laypage/laypage.js"></script>
    <script src="/CommonPage/NewInfoCommon.js"></script>
    <%--话题详情--%>
    <script id="div_NewSub" type="text/x-jquery-tmpl">
        <div id="div_NewSub_${Id}" class="posts_lists">
            <div class="posts_item clearfix post_title">
                <h1 class="title mt10 mb10" style="color:#333333;font-size:18px;">                    
                    ${Name}
                </h1>
                <div class="mnsc">
                <div class="posts_img">
                    <i class="img"><img src="${AbsHeadPic}" onerror="javascript:this.src='/images/member_img.png'" alt=""></i>
                    <%--<p class="name">${CreateName}</p>--%>
                    <%--<div class="posts_imgnone">
                        <div class="imgnone_top">
                            <i class="img">
                                <img src="images/member_img.png" alt="">
                            </i>
                            <p class="name">李淼淼 <i class="iconfont icon-woman"></i></p>
                        </div>
                        <div class="community pr clearfix" style="padding:20px 0px 10px 0px;">
                            <label for="">社团</label>
                            <div class="community_con">
                                <a href="javascript:;" class="img"><img src="/images/soeicty_img.png" alt=""></a>
                            </div>
                        </div>
                        <div class="community pr clearfix" style="border-top:1px solid #F2F5F5;">
                            <label for="">社团</label>
                            <div class="community_con">
                                <p class="sign">没有什么过不去，只是再也回不去</p>
                            </div>
                        </div>
                    </div>--%>
                </div>
                <div class="post_titledate clearfix" style="padding-top:0;">
                    <div class="post_title fl">                              
                        <a href="javascript:;" style="font-size:14px;color:#333;">${CreateName}</a>
                    </div>
                    <div class="answer_tool fr clearfix">
                        <a href="javascript:;"><i class="iconfont icon-see"></i><span id="span_NewBrowTimes${Id}">${BrowsingTimes}</span></a>
                        <div class="setting fl pr ml10" {{if CreateUID!='<%=UserInfo.UniqueNo%>'}}style="display:none;"{{/if}}>
                            <span>
                                <i class="iconfont icon-settings fl ml10"></i>
                                <i class="iconfont icon-xia fr mr10"></i>
                            </span>
                            <div class="settings_none">
                                <em onclick="EditComNewInfo(${Id});">修改</em>  
                            </div>
                        </div>
                    </div>
                </div>
                <div class="post_date mb10" style="font-size:14px;color:#666;">发表于：${DateTimeConvert(CreateTime,'yyyy-MM-dd HH:mm:ss')}</div>
            </div>
            <div class="topic_content">{{html Content}}</div>
        </div>
        </div>
    </script>
    <%--社团/部门/宿舍最新贴--%>
    <script id="li_Newest" type="text/x-jquery-tmpl">
        <li onclick="JumpToPage(0,${Id});">
            <span>${rowNum}</span>
            <a href="javascript:;">${Name}</a>
        </li>
    </script>    
</head>
<body>
    <input type="hidden" id="HLoginUID" value="<%=UserInfo.UniqueNo%>"/>
    <div id="header"></div>
    <div id="main">
        <div class="center clearfix">
            <div style="height:60px;position:fixed;width:100%;background: #C5EAFB;z-index: 99;">
                <div class="crumbs" style="margin-top: 10px;">
                    <a href="javascript:;" id="a_OneCrumbs"></a>
                    <span>&gt;</span>
                    <a href="javascript:;" id="a_TwoCrumbs">公告</a>
                </div>
            </div>
            <div class="wrap_left fl" style="margin-top: 60px;">
                <div id="div_New" class="pl20 pr20"></div>
            </div>
            <div class="wrap_right fr" style="position:fixed;margin-left: 10px;margin-top: 50px;">                            
                <div class="modult mt10">
                    <h1 class="title" id="h1_RelTypeName"></h1>
                    <ul id="ul_Newest" class="Newposts_lists"></ul>
                </div>
            </div>
            <div class="clear"></div>
        </div>
    </div>
    <div id="footer"></div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        var navid = UrlDate.nav;
        $(function () {
            $('#header').load('/CommonPage/Header.aspx?t=' + new Date().getTime(), function (response, status, xhr) {
                if (status == "success" || status == "notmodified") {
                    SetNavSelected(navid, 'a_OneCrumbs');
                    var itemid = UrlDate.itemid;
                    if (itemid != 0) {
                        BrowsingTimesSet(itemid);
                        GetComNewInfoData(1, 10);
                    }
                }
            });
            $('#footer').load('/CommonPage/footer.html');
        });
        //加载帖子
        function GetComNewInfoData(startIndex, pageSize) {
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/CommonHandler/Com_NewHandler.ashx",
                    Func: "GetComNewInfoDataPage",
                    Type: UrlDate.type,
                    NewType: UrlDate.newtype,
                    LoginUID:$("#HLoginUID").val(),
                    Id: UrlDate.itemid,
                    ispage: false,
                    IsOnlyBase: "1",
                    IsUnifiedInfo: "1"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#div_New").html('');
                        var rtndata = json.result.retData;
                        ControlInitConfig(rtndata[0]);
                        $("#div_NewSub").tmpl(rtndata).appendTo("#div_New");
                    }
                    else {
                        $("#div_New").html('<div>暂无话题！</div>');
                    }
                },
                error: function (errMsg) {
                    $("#div_New").html('<div>暂无话题！</div>');
                }
            });
        }
        function ControlInitConfig(model) { //控件初始化设置           
            if (model != '') {
                InitByType(model);
                GetNewestNew(0, "");
            }            
        }
        function InitByType(model) {
            var $h1_RelTypeName = $('#h1_RelTypeName');
            if (UrlDate.type == 0) {
                var assoUrl = '/StuAssociate/AssociateDetail.aspx?itemid=' + model.RelationId + '&nav=' + navid;
                $h1_RelTypeName.html("社团最新贴");
            } else if (UrlDate.type == 1) {
                var departUrl = '/StuActivity/DepartDetail.aspx?itemid=' + model.RelationId + '&nav=' + navid;
                $h1_RelTypeName.html("部门最新贴");
            } else if (UrlDate.type == 2) {
                var roomUrl = '/StuDormitory/DormRoomDetail.aspx?itemid=' + model.RelationId + '&nav=' + navid;
                $h1_RelTypeName.html("宿舍最新贴");
            }
        }
        function JumpToPage(type, itemid) {
            if (type == 0) {  //跳转帖子详情页面
                window.location.href = '/CommonPage/NewInfoDetail.aspx?type=' + UrlDate.type + '&newtype=0&itemid=' + itemid + '&nav=' + navid;
            } 
        }
    </script>     
</body>
</html>