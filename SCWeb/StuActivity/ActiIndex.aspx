<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActiIndex.aspx.cs" Inherits="SCWeb.StuActivity.ActiIndex" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <title>活动首页</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css"/>
    <link rel="stylesheet" href="/css/style.css"/>
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/jquery.SuperSlide.2.1.1.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/CommonPage/NewInfoCommon.js"></script>
    <%--头部活动--%>
    <script id="div_ActiSub_H" type="text/x-jquery-tmpl">
        <li>
            <a href="javascript:;" target="_blank">
                <img src="/images/activity_01.jpg" />
            </a>
        </li>
    </script>
     <%--头部活动Num--%>
    <script id="div_ActiSub_H_Num" type="text/x-jquery-tmpl">
        <li {{if rowNum==1}}class="on"{{/if}}>${rowNum}</li>
    </script>
    <%--热门活动--%>
    <script id="div_ActiSub_L" type="text/x-jquery-tmpl">
        <div class="activity_item clearfix">
            <div class="newreleate">最新发布</div>
            <div class="activity_img fl">
                <img src="/images/activity_img_01.jpg" />
            </div>
            <div class="activity_detail fr">
                <h1>${Name}</h1>
                <div class="activity_zuzhi">
                    <img src="#" alt="Alternate Text" />
                    <span>学生会部长</span>
                </div>
                <p>时间：2017-07-05 00:00</p>
                <p>地点：${Address}</p>
                <div class="fr tools_right">
                    <a href="javascript:;" class="bggreen">立即报名</a>
                </div>
            </div>
        </div>
    </script>
    <%--部门公告--%>
    <script id="li_Newest" type="text/x-jquery-tmpl">  
        <li>
            {{if rowNum<=3}}<span>new</span>{{/if}}
            <a href="/CommonPage/NewNoticeDetail.aspx?type=1&newtype=1&itemid=${Id}&nav=1">${Name}</a>
        </li>        
    </script>
    <%--我参与的活动--%>
    <script id="div_ActiSub_R" type="text/x-jquery-tmpl">
        <li>
            <a href="javascript:;" target="_blank">
                <div class="mytake">我参与</div>
                <img src="/images/activity_01.jpg" />
                <div>${Name}</div>
                <p><i class="iconfont icon-date"></i>时间：2016-12-24 </p>
                <p><i class="iconfont icon-place"></i>地点：${Address}</p>
                <p><i class="iconfont icon-crowd"></i>人数：100人</p>
            </a>
        </li>
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="header"></div>
        <div id="main" style="background:#c5eafb url(/images/bg_01.jpg) no-repeat center top;">
            <div class="center">
                <div class="banner pt10">
                   <div class="slider">
                        <ul class="51buypic" id="div_Acti_H"></ul>
                        <div class="num">
				    	    <ul id="div_Acti_H_Num"><li class="on">1</li></ul>
				        </div>
                   </div>
                </div>
                 <div class="clearfix mt10">
                    <div class="left fl">
                        <div class="hot_activity">
                            <a href="javascript:;" class="active">
                                热门活动
                            </a>
                        </div>
                        <div id="div_Acti_L" class="activity_lists"></div>
                    </div>
                    <div class="wrap_right fr">
                        <div class="modult" style="height:255px;">
                            <h1 class="title">公告 <a href="javascript:;" class="more fr">更多</a></h1>
                            <ul id="ul_Newest"  class="Newposts_lists newpost mt5"></ul>                                                     
                        </div>
                        <div class="modult">
                            <h1 class="title">我参与的活动 <a href="javascript:;" class="more fr">更多</a></h1>
                            <div class="add_activity">
                                <ul id="div_Acti_R" class="51buypic"></ul>
                                <div class="prev iconfont icon-left"></div>
                                <div class="next iconfont icon-right"></div>
                            </div>
                        </div>
                    </div>
                 </div>
            </div>
        </div>
        <div id="footer"></div>
    </div>
    </form>
    <script>
        $(function () {
            $('#header').load('/CommonPage/Header.aspx?t=' + new Date().getTime(), function (response, status, xhr) {
                if (status == "success" || status == "notmodified") {
                    SetNavSelected(1);
                }
            });
            $('#footer').load('/CommonPage/footer.html');                      
            GetActiActivityData(1, 3, 'H'); //加载头部活动
            GetActiActivityData(1, 5, 'L'); //加载左侧热门活动
            GetActiActivityData(1, 5, 'R'); //加载右侧我参与的活动
            GetNewestNew(1, '', 'ul_Newest', 'li_Newest', 1, 3, 1);//加载最新公告         
        });
         //加载部门活动
        function GetActiActivityData(startIndex, pageSize, loadtype) {  
            var $tbPar = $("#div_Acti_" + loadtype);
            var $trSub = $("#div_ActiSub_" + loadtype);
            //var $pageBar = $("#pageBar_" + loadtype);
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            var parmaArray = {
                PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                Func: "GetActiActivityDataPage",                
                LoginUID: "<%=UserInfo.UniqueNo%>",
                PageIndex: startIndex,
                PageSize: pageSize
            };
            var actiTabindex = $('#div_ActivityTab').find("a.selected").index();
            if (loadtype == 'H') {  }
            else if (loadtype == 'L') { parmaArray["IsOnlyBase"] = "1"; parmaArray["IsUnifiedInfo"] = "1"; }
            else if (loadtype == 'R') { parmaArray["MyUserNo"] = "<%=UserInfo.UniqueNo%>"; }
            var noneCon = '<div>' + (loadtype == 'H' ? '<li><a href="javascript:void(0);"><img src="/images/activity_01.jpg" /></a></li>' : '暂无活动！') + '</div>';
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                dataType: "json",
                data: parmaArray,
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $tbPar.html('');
                        var rtnObj = json.result.retData;
                        $trSub.tmpl(rtnObj.PagedData).appendTo("#div_Acti_" + loadtype);
                        if (loadtype == 'H') {
                            $("#div_ActiSub_" + loadtype + "_Num").tmpl(rtnObj.PagedData).appendTo("#div_Acti_" + loadtype + "_Num");
                            $(".slider").slide({
                                titCell: ".num ul", mainCell: ".51buypic", effect: "leftLoop", autoPlay: true, delayTime: 1200, autoPage: true
                            });
                        } else if (loadtype == 'R') {
                            $(".add_activity").slide({
                                titCell: ".num ul", mainCell: ".51buypic", effect: "leftLoop", autoPlay: true, delayTime: 1200, autoPage: true
                            });
                        }                        
                        //$pageBar.show();
                        //laypage({
                        //    cont: $pageBar, //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                        //    pages: json.result.retData.PageCount, //通过后台拿到的总页数
                        //    curr: json.result.retData.PageIndex || 1, //当前页
                        //    skip: false, //是否开启跳页
                        //    skin: '#74c3f4',
                        //    jump: function (obj, first) { //触发分页后的回调
                        //        if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                        //            GetAssoActivityData(obj.curr, pageSize)
                        //        }
                        //    }
                        //});
                    }
                    else {
                        $tbPar.html(noneCon);
                        //$pageBar.hide();
                    }                    
                },
                error: function (errMsg) {
                    $tbPar.html(noneCon);
                }
            });
        }
    </script>
</body>
</html>
