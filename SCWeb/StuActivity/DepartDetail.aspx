﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DepartDetail.aspx.cs" Inherits="SCWeb.StuActivity.DepartDetail" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>部门详情</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css">
    <link rel="stylesheet" href="/css/style.css">
    <link rel="stylesheet" href="/Scripts/Viewer/css/viewer.min.css"/>
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <link href="/Scripts/Stu_upload/webuploader.css" rel="stylesheet" />
    <script src="/Scripts/Stu_upload/webuploader.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/Viewer/js/viewer.min.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/laypage/laypage.js"></script>
    <script src="/CommonPage/NewInfoCommon.js"></script>
    <%--部门--%>
    <script id="div_DepartDetail" type="text/x-jquery-tmpl">
        <div class="society clearfix fl">
            <div class="society_img fl">
                <img src="${PicURL}" onerror="javascript:this.src='/images/assodefault.jpg'" alt="">
            </div>
            <div class="society_right fl">
                <h1 class="clearfix soeicty_1">
                    <div class="soeicty_name pr">${Name}</div>
                    <div class="soeicty_wrap ml10">(<span>成员${MemCount}</span>|<span>总贴量${NewCount}</span>)<i class="iconfont icon-heart heart"></i></div>
                </h1>                
                <h2 class="soeicty_head">部长：${CreateName}</h2>
            </div>
        </div>
        <div id="div_btnManage" class="society_manage fr" style="display:none;"></div>
    </script>
    <%--全部下的帖子--%>
    <script id="div_NewSub" type="text/x-jquery-tmpl">
        <div id="div_NewSub_${Id}" class="posts_item clearfix">
            <div class="mnsc">
                <div class="posts_img">
                    <i class="img">
                        <img src="${AbsHeadPic}" onerror="javascript:this.src='/images/member_img.png'" alt="">
                    </i>
                    <p class="name">${CreateName}</p>
                    <%--<div class="posts_imgnone">
                        <div class="imgnone_top">
                            <i class="img">
                                <img src="/images/member_img.png" alt="">
                            </i>
                            <p class="name">李淼淼 <i class="iconfont icon-woman"></i></p>
                        </div>
                        <div class="community pr clearfix" style="padding: 20px 0px 10px 0px;">
                            <label for="">部门</label>
                            <div class="community_con">
                                <a href="javascript:;" class="img">
                                    <img src="/images/soeicty_img.png" alt=""></a>
                            </div>
                        </div>
                        <div class="community pr clearfix" style="border-top: 1px solid #F2F5F5;">
                            <label for="">签名</label>
                            <div class="community_con">
                                <p class="sign">没有什么过不去，只是再也回不去</p>
                            </div>
                        </div>
                    </div>--%>
                </div>
                <div class="post_titledate clearfix">
                    <div class="post_title fl" onclick="JumpToPage(0,${Id},${NewType});">
                        <span id="span_NewTop_${Id}" class="shi bgblue" {{if IsTop==0}}style="display:none;"{{/if}}>置顶</span>
                        <span id="span_NewElite_${Id}" class="shi bgorange" {{if IsElite==0}}style="display:none;"{{/if}}>精华</span>                        
                        <a href="javascript:;">${Name}</a>
                        <span class="kong">new</span>
                    </div>
                    <div class="post_date fr">${getDateDiff(CreateTime_Stamp*1000)}</div>
                </div>
                <div class="post_content">{{html Content}}</div>
                <div class="post_imgs clearfix"></div>
                <div class="answer_tools clearfix">
                    <div class="last_time fl">
                        {{if LastComUID=='暂无回复'}}<span>最后回复：暂无回复</span>{{else}}<span>最后回复：${TwoUserName}</span><span class="ml10">${getDateDiff(LastComTime*1000)}</span>{{/if}}
                        
                    </div>
                    <div class="answer_tool fr clearfix">
                        <a href="javascript:;"><i class="iconfont icon-see"></i><span>${BrowsingTimes}</span></a>
                        <a href="javascript:;"><i class="iconfont icon-commit"></i><span>${CommontCount}</span></a>
                        <a href="javascript:;" onclick="GoodClick(${Id},'span_goodcount${Id}');"><i class="iconfont icon-praise"></i><span id="span_goodcount${Id}">${ClickCount}</span></a>
                        <div class="setting fl pr ml10" {{if IsLeader==0}}style="display:none;"{{/if}}>
                            <span>
                                <i class="iconfont icon-settings fl ml10"></i>
                                <i class="iconfont icon-xia fr mr10"></i>
                            </span>
                            <div class="settings_none">
                                <em onclick="EditComNewInfo(${Id});">修改</em>
                                <em onclick="DelComNewInfo(${Id});">删除</em>  
                                {{if NewType==0}} 
                                <em id="em_NewTop_${Id}" onclick="NewTopSet(${Id},${IsTop==0?1:0});">${IsTop==0?'置顶':'取消置顶'}</em>
                                <em id="em_NewElite_${Id}" onclick="NewEliteSet(${Id},${IsElite==0?1:0});">${IsElite==0?'加精':'取消加精'}</em>
                                {{/if}}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </script>
    <%--部门活动--%>
    <script id="div_ActivitySub" type="text/x-jquery-tmpl">
        <div id="div_ActivitySub_${Id}" class="posts_item clearfix">
            <div class="mnsc">
                <div class="posts_img">
                    <i class="img">
                        <img src="${AbsHeadPic}" onerror="javascript:this.src='/images/member_img.png'" alt="">
                    </i>
                    <p class="name">${CreateName}</p>
                    <%--<div class="posts_imgnone">
                        <div class="imgnone_top">
                            <i class="img">
                                <img src="images/member_img.png" alt="">
                            </i>
                            <p class="name">李淼淼 <i class="iconfont icon-woman"></i></p>
                        </div>
                        <div class="community pr clearfix" style="padding: 20px 0px 10px 0px;">
                            <label for="">部门</label>
                            <div class="community_con">
                                <a href="javascript:;" class="img">
                                    <img src="images/soeicty_img.png" alt=""></a>
                            </div>
                        </div>
                        <div class="community pr clearfix" style="border-top: 1px solid #F2F5F5;">
                            <label for="">部门</label>
                            <div class="community_con">
                                <p class="sign">没有什么过不去，只是再也回不去</p>
                            </div>
                        </div>
                    </div>--%>
                </div>
                <div class="post_titledate clearfix">
                    <div class="post_title fl" onclick="JumpToPage(1,${Id});">
                        <%--<span class="shi bgblue">置顶</span>--%>
                        <a href="javascript:;">${Name}</a>
                        <span class="kong">new</span>
                    </div>
                    <div class="post_date fr">${getDateDiff(CreateTime_Stamp*1000)}</div>
                </div>
                <div class="post_content">{{html Content}}</div>
                <div class="date_place" style="margin: 5px 0px;">
                    <div class="fl date_place1 clearfix">
                        <div class="fl">
                            <i class="iconfont icon-date"></i>
                            <span>${DateTimeConvert(ActStartTime,'yyyy-MM-dd')}~${DateTimeConvert(ActEndTime,'yyyy-MM-dd')} </span>
                        </div>
                        <div class="fl ml10">
                            <i class="iconfont icon-people"></i>
                            <span>${MemCount}</span>
                        </div>
                        <div class="fl ml10">
                            <i class="iconfont icon-place"></i>
                            <span>${Address}</span>
                        </div>
                    </div>
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
                                <em onclick="EditActiActivity(${Id});">修改</em>
                                <em onclick="DelActiActivity(${Id});">删除</em>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </script>
    <%--部门相册--%>
    <script id="div_AlbumSub" type="text/x-jquery-tmpl">
        <a href="javascript:;" {{if PicCount>0}}onclick="ComAlbumClick(${Id},'${Name}');{{/if}}">
            <i><img src="${FirstPicUrl}" onerror="javascript:this.src='/images/albumdefault.jpg'" alt=""></i>
            <div class="photos_tool">
                <span class="photo_name">${Name}</span>
                <span class="fr photo_number">${PicCount}张</span>
            </div>
        </a>
    </script>
    <%--相册照片--%>
    <script id="div_PicSub" type="text/x-jquery-tmpl">
        <a href="javascript:;">
            <i>
                <img data-original="${PicUrl}" src="${PicUrl}" alt="${SetPicName(PicUrl)}" /></i>
            <div class="photos_tool">
                <div style="text-align: center">${SetPicName(PicUrl)}</div>
            </div>
        </a>
    </script>
    <%--部门成员--%>
    <script id="tr_MemberSub" type="text/x-jquery-tmpl">
        <tr>
            <td>
                <div class="name clearfix">
                    <img src="${AbsHeadPic}" onerror="javascript:this.src='/images/member_img.png'" class="fl" />
                    <span class="fl ml10">${CreateName}</span>
                     {{if Sex==0}}<i class="iconfont icon-man fl ml10 colorblue"></i>{{else}}<i class="iconfont icon-woman fl ml10 colorred"></i>{{/if}}
                </div>
            </td>
            <td>${DepartName}</td>
            <td>成员</td>
            <td>${GradeName}</td>
            <td>${OrgName}</td>
        </tr>
    </script>
    <%--学生会部门--%>
    <script id="div_AllDepartSub" type="text/x-jquery-tmpl">
        <a href="javascript:;" style="width:80px;">
            <div class="member_img" style="width:80px;height:80px;">
                <img src="${PicURL}" onerror="javascript:this.src='/images/member_img.png'" class="fl" alt="">
            </div>
            <p class="member_name">${Name}</p>
        </a>
    </script>
    <%--最新加入成员--%>
    <script id="div_MemberNewSub" type="text/x-jquery-tmpl">
        <a href="javascript:;">
            <div class="member_img">
                <img src="${AbsHeadPic}" onerror="javascript:this.src='/images/member_img.png'" class="fl" alt="">
            </div>
            <p class="member_name">${CreateName}</p>
        </a>
    </script>
    <%--部门最新贴--%>
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
        <div id="div_BackPicURL" class="mycorporation pr">
            <div class="center clearfix" id="div_Depart"></div>
            <div id="div_btnPicURL" class="replace_bg" style="display:none;">
                <div class="upload">
                    <div id="filePicker">更换背景</div>
                </div>
            </div>
        </div>
        <div class="center pt10 clearfix" id="divacco_right">
            <div class="wrap_left fl">
                <div class="pl20 pr20">
                    <nav class="mycorporation_nav">
                        <ul id="ul_DepartInfo" class="clearfix">
                            <li class="selected">
                                <a href="javascript:;">
                                    <i class="iconfont icon-all"></i>
                                    <span>全部</span>
                                </a>
                            </li>
                            <li>
                                <a href="javascript:;">
                                    <i class="iconfont icon-activity"></i>
                                    <span>部门活动</span>
                                </a>
                            </li>
                            <li>
                                <a href="javascript:;">
                                    <i class="iconfont icon-album"></i>
                                    <span>部门相册</span>
                                </a>
                            </li>
                            <li>
                                <a href="javascript:;">
                                    <i class="iconfont icon-member"></i>
                                    <span>部门成员</span>
                                </a>
                            </li>
                        </ul>
                    </nav>
                    <div class="tab_wraps">
                        <div class="tab_wrap">
                            <div class="clearfix tools">
                                <div id="div_NewTab" class="select_tools fl clearfix">
                                    <a href="javascript:;" class="selected">话题</a>                               
                                    <a href="javascript:;">精华</a>
                                    <a href="javascript:;">我的帖子</a>
                                    <a href="javascript:;">零回复</a>
                                </div>
                                <%--<div class="tools_option fr clearfix">
                                    <select class="select fl">
                                        <option value="">NEW</option>
                                    </select>
                                    <select class="select fl ml10">
                                        <option value=""></option>
                                    </select>
                                </div>--%>
                            </div>
                            <div id="div_New" class="posts_lists"></div>
                            <div class="page" id="pageBar_New"></div>
                        </div>
                        <div class="tab_wrap none">
                            <div class="clearfix tools">
                                <div id="div_ActivityTab" class="select_tools fl clearfix">
                                    <a href="javascript:;" class="selected">全部活动</a>                                  
                                    <a href="javascript:;">正在进行的</a>
                                    <a href="javascript:;">已经结束的</a>                                  
                                </div>
                                <%--<div class="tools_option fr clearfix">
                                    <select class="select fl">
                                        <option value="">NEW</option>
                                    </select>
                                </div>--%>
                            </div>
                            <div id="div_Activity" class="posts_lists"></div>
                            <div class="page" id="pageBar_Activity"></div>
                        </div>
                        <div class="tab_wrap none">
                            <div class="crumbs" style="padding-left:0px;">
                                <a href="javascript:;" id="aAlbum_OneCrumbs" onclick="Album_OneCrumbsClick();">全部相册</a>
                                <a href="javascript:;" id="aAlbum_TwoCrumbs" style="display:none;"></a>
                            </div>
                            <div id="div_Album" class="photo_lists" style="margin-top:0px;"></div>
                        </div>
                        <div class="tab_wrap none">
                            <div class="clearfix tools">
                               <div class="fl" style="line-height:40px;color:#333333;font-size:18px;">
                                   所有成员列表
                               </div>
                               <div class="fr tools_right">
                                    <a id="a_ExitBtn" href="javascript:;" class="tui" style="display:none;">我要退部</a>                                    
                               </div>
                            </div>
                            <div class="table_wraps">
                                <table class="table">
                                       <thead>
                                           <tr>
                                               <th>姓名</th>
                                               <th>部门</th>
                                               <th>职位</th>
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
                    </div>
                </div>
            </div>
            <div class="wrap_right fr">
                <div id="div_wrapsbtn" class="btn_wraps" style="display: none;">
                    <div class="btn_wrap">
                        <a href="javascript:;" class="MySpeak bggreen" onclick="PagePopWindow(0);">
                            <i class="iconfont icon-edit"></i>
                            我要发言
                        </a>                        
                    </div>
                    <div class="btn_wrap none">
                        <a href="javascript:;" class="MySpeak bggreen" onclick="PagePopWindow(1);">
                            <i class="iconfont icon-edit"></i>
                            发起活动
                        </a>
                    </div>
                    <div class="btn_wrap none clearfix">
                        <a id="abtn_AddAlbum" href="javascript:;" class="MySpeak bggreen fl" onclick="PagePopWindow(2);">
                            <i class="iconfont icon-edit"></i>
                            创建相册
                        </a>
                        <a id="abtn_UploadPic" href="javascript:;" class="MySpeak bgblue fr" onclick="PagePopWindow(3);">
                            <i class="iconfont icon-xiangce"></i>
                            上传照片
                        </a>
                    </div>
                    <div class="btn_wrap none">
                        <a href="javascript:;" class="MySpeak bggreen" onclick="PagePopWindow(4);">
                            <i class="iconfont icon-edit"></i>
                            添加成员
                        </a>
                    </div>
                </div>
                <div class="search_wrap pr">
                    <input id="txt_CurName" type="text" class="search mt10" placeholder="搜本部门话题">
                    <i class="iconfont icon-search" onclick="SearchCondition();"></i>
                </div>
                <div id="div_TeachPlay" class="teach_bg mt20 pr" style="display:none;">
                    <a href="javascript:;"></a>
                    <span class="close bgorange"><i class="iconfont icon-close"></i></span>
                </div>
                <div class="modult mt20">
                    <h1 class="title">学生会部门</h1>
                    <div id="div_AllDepart" class="add_members clearfix mt10"></div>
                </div>
                <div class="modult mt20">
                    <h1 class="title">最新加入成员</h1>
                    <a id="a_Apply" href="javascript:;" class="tips bgblue" style="display:none;"></a>
                    <div id="div_MemberNew" class="add_members clearfix mt10"></div>
                </div>
                <div class="modult mt20">
                    <h1 class="title">部门最新贴</h1>
                    <ul id="ul_Newest" class="Newposts_lists"></ul>
                </div>
            </div>
        </div>
        <div class="center pt10" id="noacco" style="display:none;">
            <div class="wrap">
                <div class="noacco clearfix">
                    <div class="noacco_img fl">
                        <img src="../images/noacco.png" />
                    </div>
                    <div class="fl" style="margin-left:30px;">                        
                        <p id="p_nullWord"></p>
                        <a id="a_nullWord" href="javascript:;" style="display:none;" onclick="OpenIFrameWindow('创建学生会','/StuActivity/EditDepartInfo.aspx?itemid=0&pid=0','730px','600px')" class="MySpeak bggreen">创建学生会</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="footer"></div>
    <script>
        var UrlDate = new GetUrlDate();
        var navid = UrlDate.nav||3;
        var CurDepartID = 0;
        var departModel = '';
        var $txt_CurName = $("#txt_CurName"); //搜索框        
        $(function () {
            $('#header').load('/CommonPage/Header.aspx?t=' + new Date().getTime(), function (response, status, xhr) {
                if (status == "success" || status == "notmodified") {
                    SetNavSelected(navid);
                }
            });
            $('#footer').load('/CommonPage/footer.html');
            if("<%=UserInfo.UserType%>"=="2"){
                $("#p_nullWord").html('耐心等待管理员创建学生会哦！');
            } else {
                $("#p_nullWord").html('请尽快创建学生会吧！');
            }
            GetMyDepart(UrlDate.tab || 0);
            MoveFly('.posts_lists .posts_img', '.posts_imgnone');
            tab('.mycorporation_nav ul', '.tab_wraps', '.btn_wraps');            
        });
        function MoveFly(obj, hideobj) {
            $(obj).hover(function (e) {
                //获取当前块距离屏幕的top，left值；
                var e = e || window.event;
                var offsetTop = $(this).offset().top; offsetLeft = $(this).offset().left;
                var clientX = e.pageX, clientY = e.pageY;
                var x = clientX - offsetLeft + 'px', y = clientY - offsetTop + 'px';
                $(this).find(hideobj).css({ 'left': x, 'top': y }).fadeIn();
            }, function () {
                $(this).find(hideobj).hide();
            })
        }
        //获取我的部门
        function GetMyDepart(tabindex) {
            tabindex = arguments[0] || 0;
            $('#ul_DepartInfo li:eq(' + tabindex + ')').addClass('selected').siblings().removeClass('selected');
            $('.tab_wraps').children().eq(tabindex).show().siblings().hide();
            $('.btn_wraps').children().eq(tabindex).show().siblings().hide();
            var params = {
                PageName: "/StuActivity/Acti_DepartHandler.ashx", Func: "GetDepartInfoDataPage",
                LoginUID: "<%=UserInfo.UniqueNo%>", ispage: false, IsOnlyBase: "1",IsUnifiedInfo: "1"};
            if (UrlDate.itemid != undefined) {
                CurDepartID = UrlDate.itemid;
                params["Id"] = CurDepartID;
            } else {
                params["ParentId"] = 0;
            }            
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: params,
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $('#div_BackPicURL').show();
                        $('#divacco_right').show();
                        $('#noacco').hide();
                        $("#div_Depart").html('');                        
                        var rtnObj = json.result.retData;
                        $("#div_DepartDetail").tmpl(rtnObj[0]).appendTo("#div_Depart");
                        departModel = rtnObj[0];
                        CurDepartID = rtnObj[0].Id;
                        //if (navid !=2) {
                        //    $("#div_DepartDetail").tmpl(rtnObj).appendTo("#div_Depart");
                        //    departModel = rtnObj[0];
                        //} else {
                        //    if (UrlDate.itemid == undefined) {
                        //        CurDepartID = rtnObj[0].Id;
                        //        departModel = rtnObj[0];
                        //        $("#div_DepartDetail").tmpl(rtnObj[0]).appendTo("#div_Depart");
                        //    } else {
                        //        $(json.result.retData).each(function (i, n) {
                        //            if (n.Id == CurDepartID) {
                        //                departModel = rtnObj[i];
                        //                $("#div_DepartDetail").tmpl(rtnObj[i]).appendTo("#div_Depart");                                      
                        //            }
                        //        });
                        //    }                            
                        //}
                        SetDisplayOrHide(departModel);
                        ChangeBackPic();                        
                        LoadFunc(tabindex);
                        GetAllDepartInfo();
                        GetNewDepartMemberData();
                        GetNewestNew(1, CurDepartID);
                        TabChange();
                    }
                    else {
                        $("#div_Depart").html('<div>暂无部门！</div>');
                        $('#div_BackPicURL').hide();
                        $('#divacco_right').hide();
                        $('#noacco').show();
                        if ("<%=UserInfo.UserType%>" != "2") {
                            $("#a_nullWord").show();
                        }                        
                    }
                },
                error: function (errMsg) {
                    $("#div_Depart").html('<div>暂无部门！</div>');
                }
            });
        }
        function SetDisplayOrHide(model) { //设置显示隐藏
            if (model != '') {
                if (model.BackPicUrl != "") {
                    $("#div_BackPicURL").css("background-image", "url(" + model.BackPicUrl + ")");
                }                
                var $div_btnManage = $("#div_btnManage"); //管理部门按钮
                var $div_btnPicURL = $("#div_btnPicURL"); //更换背景按钮
                var $div_wrapsbtn = $("#div_wrapsbtn"); //右侧操作按钮
                var $div_TeachPlay = $("#div_TeachPlay"); //指导操作部门图片
                var $a_ExitBtn = $("#a_ExitBtn"); //退部按钮
                var $a_Apply = $("#a_Apply");  //申请提示
                if (model.IsLeader == 1) {
                    $div_btnManage.show();
                    $div_btnManage.append('<i class="iconfont icon-settings"></i>管理部门');
                    $div_btnManage.click(function () {
                        JumpToPage(2, model.Id);//跳转部门管理页面                        
                    });
                    $div_btnPicURL.show();
                    $div_wrapsbtn.show();
                    //$div_TeachPlay.show();  //暂时不展示
                    if (model.ExamApplyCount > 0) {
                        $a_Apply.show();
                        $a_Apply.html('提醒：' + model.ExamApplyCount + '个人员申请，去处理 >');
                        $a_Apply.click(function () {
                            JumpToPage(3, model.Id);//跳转部门管理页面并且默认选中成员审批选项卡 
                        });
                    } else {
                        $a_Apply.hide();
                    }
                    $a_ExitBtn.hide();
                } else {
                    if (model.IsMember == 1) {
                        $a_ExitBtn.show();
                        $a_ExitBtn.click(function () {
                            OpenIFrameWindow('申请退出', '/StuActivity/AddRecruitApply.aspx?itemid=0&relid=' + CurDepartID + '&type=2', '480px', '400px');
                            //////OpenIFrameWindow('申请加入', '/StuActivity/AddRecruitApply.aspx?itemid=0&relid=' + CurDepartID + '&type=1', '480px', '400px');
                        });
                    } else {}
                    $div_btnPicURL.hide();
                    $div_wrapsbtn.hide();
                    $div_TeachPlay.hide();
                    $a_Apply.hide();
                }
            }
        }
        function TabChange() {
            //部门信息切换
            $('#ul_DepartInfo li').click(function () {
                $(this).addClass('selected').siblings().removeClass('selected');
                LoadFunc($(this).index());
            });
            //话题切换
            $('#div_NewTab a').click(function () {
                $(this).addClass('selected').siblings().removeClass('selected');
                GetComNewInfoData(1, 10);
            });
            //活动切换
            $('#div_ActivityTab a').click(function () {
                $(this).addClass('selected').siblings().removeClass('selected');
                $txt_CurName.val('');
                GetActiActivityData(1, 10);
            });
        }
        function SetUploadPicBtn(tabindex) {
            var $div_wrapsbtn = $("#div_wrapsbtn"); //左侧按钮
            var $abtn_UploadPic = $("#abtn_UploadPic");//上传照片按钮
            if (departModel.IsLeader != 1) {
                if (departModel.IsMember == 1 && tabindex == 2) {
                    $("#abtn_AddAlbum").hide();
                    $abtn_UploadPic.parent().show();
                    $div_wrapsbtn.show();
                } else {
                    $div_wrapsbtn.hide();
                }
            } else {
                $div_wrapsbtn.show();
            }
        }
        var CurNameVal = $txt_CurName.val().trim();
        function SearchCondition() {
            CurNameVal = $txt_CurName.val().trim();
            LoadFunc($('#ul_DepartInfo').find("li.selected").index());
        }
        function LoadFunc(tabindex) {
            SetUploadPicBtn(tabindex);
            if (tabindex == 0) {
                $txt_CurName.attr('placeholder', '搜本部门话题');
                GetComNewInfoData(1, 10);
            } else if (tabindex == 1) {
                $txt_CurName.attr('placeholder', '搜本部门活动');
                GetActiActivityData(1, 10);
            } else if (tabindex == 2) {
                $txt_CurName.attr('placeholder', '搜本部门相册');
                $aAlbum_TwoCrumbs = $("#aAlbum_TwoCrumbs");
                $aAlbum_TwoCrumbs.hide();
                GetComAlbumData(1, 10);
            } else if (tabindex == 3) {
                $txt_CurName.attr('placeholder', '搜本部门成员');
                GetDepartMemberData(1, 10);
            }
        }
        //加载帖子
        function GetComNewInfoData(startIndex, pageSize) {
            var $tbPar = $("#div_New");
            var $trSub = $("#div_NewSub");
            var $pageBar = $("#pageBar_New");
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            var parmaArray = {
                PageName: "/CommonHandler/Com_NewHandler.ashx",
                Func: "GetComNewInfoDataPage",
                Type: 1,
                RelationId: CurDepartID,
                Name: CurNameVal,
                LoginUID: "<%=UserInfo.UniqueNo%>",                
                PageIndex: startIndex,
                PageSize: pageSize,
                IsOnlyBase: "1",
                IsUnifiedInfo: "1"
            };
            var newTabindex = $('#div_NewTab').find("a.selected").index();
            if (newTabindex == 1) { parmaArray["IsElite"] = 1; }
            else if (newTabindex == 2) { parmaArray["CreateUID"] = "<%=UserInfo.UniqueNo%>"; }
            else if (newTabindex == 3) { parmaArray["CommontCount"] = 0; }
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                dataType: "json",
                data: parmaArray,
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $tbPar.html('');
                        var rtnObj = json.result.retData;
                        $trSub.tmpl(rtnObj.PagedData).appendTo("#div_New");
                        $pageBar.show();
                        laypage({
                            cont: $pageBar, //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                            pages: json.result.retData.PageCount, //通过后台拿到的总页数
                            curr: json.result.retData.PageIndex || 1, //当前页
                            skip: false, //是否开启跳页
                            skin: '#74c3f4',
                            jump: function (obj, first) { //触发分页后的回调
                                if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                    GetComNewInfoData(obj.curr, pageSize)
                                }
                            }
                        });
                    }
                    else {
                        $tbPar.html('<div>暂无话题！</div>');
                        $("#pageBar_New").hide();
                    }                    
                },
                error: function (errMsg) {
                    $tbPar.html('<div>暂无话题！</div>');
                }
            });
        }
        //加载部门活动
        function GetActiActivityData(startIndex, pageSize) {
            var $tbPar = $("#div_Activity");
            var $trSub = $("#div_ActivitySub");
            var $pageBar = $("#pageBar_Activity");
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            var parmaArray = {
                PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                Func: "GetActiActivityDataPage",
                DepartId: CurDepartID,
                Name: CurNameVal,
                LoginUID: "<%=UserInfo.UniqueNo%>",
                PageIndex: startIndex,
                PageSize: pageSize
            };
            var actiTabindex = $('#div_ActivityTab').find("a.selected").index();
            if (actiTabindex == 1) {
                parmaArray["ActStatus"] ="ing";
            } else if (actiTabindex == 2) { parmaArray["ActStatus"] = "end" }
            $.ajax({
                url: "/Common.ashx",
                type: "post",            
                dataType: "json",
                data: parmaArray,
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $tbPar.html('');
                        var rtnObj = json.result.retData;
                        $trSub.tmpl(rtnObj.PagedData).appendTo("#div_Activity");
                        $pageBar.show();
                        laypage({
                            cont: $pageBar, //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                            pages: json.result.retData.PageCount, //通过后台拿到的总页数
                            curr: json.result.retData.PageIndex || 1, //当前页
                            skip: false, //是否开启跳页
                            skin: '#74c3f4',
                            jump: function (obj, first) { //触发分页后的回调
                                if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                    GetActiActivityData(obj.curr, pageSize)
                                }
                            }
                        });
                    }
                    else {
                        $tbPar.html('<div>暂无活动！</div>');
                        $pageBar.hide();
                    }                    
                },
                error: function (errMsg) {
                    $tbPar.html('<div>暂无活动！</div>');
                }
            });
        }
        //加载部门相册
        function GetComAlbumData(startIndex, pageSize) {
            var $tbPar = $("#div_Album");
            var $trSub = $("#div_AlbumSub");
            var $pageBar = $("#pageBar_Album");
            $tbPar.html('');
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;            
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/CommonHandler/Com_AlbumHandler.ashx",
                    Func: "GetComAlbumDataPage",
                    Type: 1,
                    RelationId: CurDepartID,
                    Name: CurNameVal,
                    PageIndex: startIndex,
                    PageSize: pageSize
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        if (departModel.IsLeader == 1) {
                            $("#abtn_UploadPic").show();
                        }
                        var rtnObj = json.result.retData;
                        $trSub.tmpl(rtnObj.PagedData).appendTo("#div_Album");
                        $pageBar.show();
                        laypage({
                            cont: $pageBar, //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                            pages: json.result.retData.PageCount, //通过后台拿到的总页数
                            curr: json.result.retData.PageIndex || 1, //当前页
                            skip: false, //是否开启跳页
                            skin: '#74c3f4',
                            jump: function (obj, first) { //触发分页后的回调
                                if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                    GetComAlbumData(obj.curr, pageSize)
                                }
                            }
                        });
                    }
                    else {
                        $("#abtn_UploadPic").hide();
                        $tbPar.html('<div>暂无相册！</div>');
                        $pageBar.hide();
                    }                   
                },
                error: function (errMsg) {
                    $tbPar.html('<div>暂无相册！</div>');
                }
            });
        }
        //加载部门成员
        function GetDepartMemberData(startIndex, pageSize) {
            var $tbPar = $("#tb_Member");
            var $trSub = $("#tr_MemberSub");
            var $pageBar = $("#pageBar_Member");
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/StuActivity/Acti_DepartHandler.ashx",
                    Func: "GetDepartMemberDataPage",
                    DepartId: CurDepartID,
                    Name: CurNameVal,
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
                                    GetDepartMemberData(obj.curr, pageSize)
                                }
                            }
                        });
                    }
                    else {
                        $tbPar.html('<tr><td colspan="8">暂无成员！</td></tr>');
                        $pageBar.hide();
                    }
                },
                error: function (errMsg) {
                    $tbPar.html('<tr><td colspan="8">暂无成员！</td></tr>');
                }
            });
        }
        //获取学生会部门
        function GetAllDepartInfo() {
            var $tbPar = $("#div_AllDepart");
            var $trSub = $("#div_AllDepartSub");
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                dataType: "json",
                data: {PageName: "/StuActivity/Acti_DepartHandler.ashx", Func: "GetDepartInfoDataPage",LoginUID: "<%=UserInfo.UniqueNo%>", ispage: false},
                success: function (json) {
                     if (json.result.errNum.toString() == "0") {
                        $tbPar.html('');
                        var rtnObj = json.result.retData;
                        $trSub.tmpl(rtnObj).appendTo("#div_AllDepart");
                    }
                    else {
                        $tbPar.html('<a href="javascript:;">暂无！</a>');
                    }
                },
                error: function (errMsg) {
                    $tbPar.html('<a href="javascript:;">暂无！</a>');
                }
            });
        }
        //加载最新加入成员
        function GetNewDepartMemberData() {
            var $tbPar = $("#div_MemberNew");
            var $trSub = $("#div_MemberNewSub");
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/StuActivity/Acti_DepartHandler.ashx",
                    Func: "GetDepartMemberDataPage",
                    DepartId: CurDepartID,
                    NewMemerDay: 3,
                    ispage: false,
                    IsOnlyBase: "1",
                    IsUnifiedInfo: "1"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $tbPar.html('');
                        var rtnObj = json.result.retData;
                        $trSub.tmpl(rtnObj).appendTo("#div_MemberNew");                     
                    }
                    else {
                        $tbPar.html('<a href="javascript:;">暂无！</a>');
                    }
                },
                error: function (errMsg) {
                    $tbPar.html('<a href="javascript:;">暂无！</a>');
                }
            });
        }
        function JumpToPage(type, itemid, newtype) {
            newtype = arguments[2] || 0;  //0帖子；1通知
            if (type == 0) {  //跳转帖子详情页面
                window.location.href = '/CommonPage/NewInfoDetail.aspx?type=1&newtype=' + newtype + '&itemid=' + itemid + '&nav=' + navid;
            } else if (type == 1) { //跳转活动详情页面
                window.location.href = '/StuActivity/DepartActivityDetail.aspx?itemid=' + itemid + '&nav=' + navid;
            } else if (type == 2) { //跳转部门管理页面
                window.location.href = '/StuActivity/SingleDepartManage.aspx?itemid=' + itemid + '&nav=' + navid;
            } else if (type == 3) { //跳转部门管理页面并且默认选中成员审批选项卡 
                window.location.href = '/StuActivity/SingleDepartManage.aspx?itemid=' + itemid + '&nav=' + navid + "&tab=3";
            }
        }
        function PagePopWindow(type) {
            if (type == 0) { //发布话题
                OpenIFrameWindow('发布话题', '/CommonPage/EditComNewInfo.aspx?type=1&newtype=0&itemid=0&relid=' + CurDepartID, '730px', '600px');
            } else if (type == 1) { //发起活动
                OpenIFrameWindow('发起活动', '/StuActivity/EditActiActivity.aspx?itemid=0&relid=' + CurDepartID, '900px', '540px');
            } else if (type == 2) { //创建相册
                OpenIFrameWindow('创建相册', '/CommonPage/EditComAlbum.aspx?type=1&itemid=0&relid=' + CurDepartID, '480px', '400px');
            } else if (type == 3) { //上传照片
                OpenIFrameWindow('上传照片', '/CommonPage/UploadAlbumPic.aspx?type=1&relid=' + CurDepartID, '900px', '660px');
            } else if (type == 4) { //添加成员
                OpenIFrameWindow('添加成员', '/StuActivity/AddDepartMember.aspx?relid=' + CurDepartID + '&tab=3', '900px', '660px');
            }
        }
        function EditActiActivity(itemid) {
            OpenIFrameWindow('编辑活动', '/StuActivity/EditActiActivity.aspx?itemid=' + itemid, '900px', '540px');
        }
        function DelActiActivity(itemid) {
            layer.confirm('确定要删除该活动吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                $.ajax({
                    url: "/Common.ashx",
                    type: "post",
                    dataType: "json",
                    data: {
                        PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                        func: "DelActiActivity",
                        ItemId: itemid
                    },
                    success: function (json) {
                        if (json.result.errNum == 0) {                            
                            layer.msg('删除成功！');
                            GetActiActivityData(1, 10);
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
        function ChangeBackPic() {
            WebUploader.create({
                pick: '#filePicker',
                formData: { Func: "Upload_DeparHomePic" },
                accept: {
                    title: 'Images',
                    extensions: 'gif,jpg,jpeg,bmp,png',
                    mimeTypes: 'image/*'
                },
                auto: true,
                chunked: false,
                chunkSize: 512 * 1024,
                server: '/CommonHandler/Upload.ashx',
                // 禁掉全局的拖拽功能。这样不会出现图片拖进页面的时候，把图片打开。
                disableGlobalDnd: true,
                fileNumLimit: 1,
                fileSizeLimit: 200 * 1024 * 1024,    // 200 M
                fileSingleSizeLimit: 50 * 1024 * 1024    // 50 M
            })
           .on('uploadSuccess', function (file, response) {
               var json = $.parseJSON(response._raw);
               $("#div_BackPicURL").css("background-image", "url(" + json.result.retData + ")");                            
               $.ajax({
                   url: "/Common.ashx",
                   type: "post",
                   async: false,
                   dataType: "json",
                   data: {
                       PageName: "/StuActivity/Acti_DepartHandler.ashx",
                       Func: "ChangeBackPic",
                       ItemId: CurDepartID,
                       BackPicUrl: json.result.retData
                   },
                   success: function (json) {
                       var result = json.result;
                       if (result.errNum == 0) {
                           layer.msg('更换背景成功!');
                       } else {
                           layer.msg(result.errMsg);
                       }
                   },
                   error: function (XMLHttpRequest, textStatus, errorThrown) {
                       layer.msg("操作失败！");
                   }
               });
           }).onError = function (code) {
               switch (code) {
                   case 'exceed_size':
                       layer.msg('文件大小超出');
                       break;
                   case 'interrupt':
                       layer.msg('上传暂停');
                       break;
                   default:
                       layer.msg('错误: ' + code);
                       break;
               }
           };
        }       
    </script>
</body>
</html>
