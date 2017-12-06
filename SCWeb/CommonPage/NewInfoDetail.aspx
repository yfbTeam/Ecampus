<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewInfoDetail.aspx.cs" Inherits="SCWeb.CommonPage.NewInfoDetail" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>帖子详情</title>       
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css"/>
    <link rel="stylesheet" href="/css/style.css"/>
    <script src="/Scripts/jquery-1.8.3.min.js"></script>    
    <script charset="utf-8" src="/Scripts/KindUeditor/kindeditor-min.js"></script>
    <script charset="utf-8" src="/Scripts/KindUeditor/plugins/code/prettify.js"></script>
    <script charset="utf-8" src="/Scripts/KindUeditor/lang/zh_CN.js"></script>
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
                    <span id="span_NewTop_${Id}" class="shi bgblue" {{if IsTop==0}}style="display:none;"{{/if}}>置顶</span>
                    <span id="span_NewElite_${Id}" class="shi bgorange mr10" {{if IsElite==0}}style="display:none;"{{/if}}>精华</span>
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
                        <a href="javascript:;"><i class="iconfont icon-commit"></i><span id="span_CommCount${Id}">${CommontCount}</span></a>
                        <a href="javascript:;" onclick="GoodClick(${Id},'span_goodcount${Id}',0,'p_GoodWord');"><i class="iconfont icon-praise"></i><span id="span_goodcount${Id}">${ClickCount}</span></a>
                        <div class="setting fl pr ml10" {{if IsLeader==0}}style="display:none;"{{/if}}>
                            <span>
                                <i class="iconfont icon-settings fl ml10"></i>
                                <i class="iconfont icon-xia fr mr10"></i>
                            </span>
                            <div class="settings_none">
                                <em onclick="EditComNewInfo(${Id});">修改</em>
                                <em onclick="DelComNewInfo(${Id});" style="display:none;">删除</em> 
                                {{if NewType==0}}  
                                <em id="em_NewTop_${Id}" onclick="NewTopSet(${Id},${IsTop==0?1:0});">${IsTop==0?'置顶':'取消置顶'}</em>
                                <em id="em_NewElite_${Id}" onclick="NewEliteSet(${Id},${IsElite==0?1:0});">${IsElite==0?'加精':'取消加精'}</em>                                   
                                {{/if}}
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
    <%--话题回复--%>
    <script id="div_CommentSub" type="text/x-jquery-tmpl">
        <div class="posts_item clearfix">
            <div class="mnsc">
                <div class="posts_img">
                    <i class="img">
                        <img src="${AbsHeadPic}" onerror="javascript:this.src='/images/member_img.png'" alt="">
                    </i>
                   <%-- <p class="name">李淼淼</p>--%>
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
                                    <img src="/images/soeicty_img.png" alt=""></a>
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
                    <div class="post_title fl">
                        <a href="javascript:;">${CreateName}</a>
                    </div>
                    <div class="post_date fl" style="margin-left: 20px">${DateTimeConvert(CreateTime,'yyyy-MM-dd HH:mm:ss')}</div>
                    <div class="post_date fr">${rowNum}#</div>
                </div>
                <div class="post_content">{{html Content}}</div>
                <div class="answer_tools clearfix">
                    <div class="answer_tool fr clearfix">
                        <a href="javascript:;"  onclick="GoodClick(${Id},'sCom_goodcount${Id}',2);">
                            <i class="iconfont icon-praise"></i>
                            <span id="sCom_goodcount${Id}">${GoodCount}</span>
                        </a>
                    </div>
                </div>
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
                    <a href="javascript:;" id="a_TwoCrumbs"></a>
                    <span>&gt;</span>
                    <a href="javascript:;" id="a_ThreeCrumbs"></a>
                </div>
            </div>
            <div class="wrap_left fl" style="margin-top: 60px; ">
                <div id="div_New" class="pl20 pr20"></div>
            </div>
            <div class="wrap_right fr" style="position:fixed;margin-left: 10px;margin-top: 60px;">
                <div class="topic_tools clearfix">
                    <a href="#edit_wrap" class="colorgreen"><i class="iconfont icon-commit"></i><p>回复</p></a>
                    <a href="javascript:;" id="a_AddComNew" class="colorblue"><i class="iconfont icon-edit"></i><p id="p_AddComNew"></p></a>
                    <a href="javascript:;" id="a_NewClick" class="colorred"><i class="iconfont icon-praise"></i><p id="p_GoodWord">点赞</p></a>
                </div>
                <div class="modult mt10 clearfix">
                    <div class="community_img fl">
                        <img id="img_Pic" src="images/soeicty_img.png" alt="">
                    </div>
                    <div class="fl">
                        <h1 class="community_name" id="h1_RelName"></h1>
                        <a href="javascript:;" id="a_IntoWord"></a>
                    </div>
                </div>
                <div class="modult mt10" style="min-height: 290px;">
                    <h1 class="title" id="h1_RelTypeName"></h1>
                    <ul id="ul_Newest" class="Newposts_lists"></ul>
                </div>
            </div>
            <div class="clear"></div>
            <div class="wrap_left fl mt10">
                <div class="pl20 pr20">
                    <div class="clearfix reply_tools mt10">
                        <div class="reply_left fl clearfix">
                            <a href="javascript:;" class="selected">
                                全部回复（ <span id="span_Count">0</span>）
                            </a>
                        </div>
                        <a href="#edit_wrap" class="btn fr bgblue mb10">回复</a>
                    </div>
                    <div id="div_Comment" class="posts_lists"></div>
                    <div class="page" id="pageBar_Comment"></div>
                    <div id="edit_wrap" class="mt10">
                        <textarea id="area_Content" name="area_Content" placeholder="请填写内容..." style="width: 100%; height: 300px;"></textarea>
                        <input type="button" value="发送回复" onclick="AddComNewComment();" class="btn bgblue mt10 mb10 fr">
                    </div>
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
                    SetNavSelected(navid, 'a_OneCrumbs');
                    var itemid = UrlDate.itemid;
                    if (itemid != 0) {
                        BrowsingTimesSet(itemid);
                        GetComNewInfoData(1, 10);
                    }
                }
            });
            $('#footer').load('/CommonPage/footer.html');
            KindEditor.ready(function (K) {
                curEditor = K.create('#area_Content', {
                    uploadJson: '/CommonHandler/Upload.ashx?Func=Upload_NewInfoPic',
                    allowFileManager: false,//true时显示浏览服务器图片功能。
                    imageSizeLimit: '2MB', //批量上传图片单张最大容量
                    imageUploadLimit: 20, //批量上传图片同时上传最多个数
                    filterMode: true,
                    allowImageRemote: false,//网络图片
                    items: [
	                    'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline', "strikethrough",
	                    'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'insertorderedlist',
	                    'insertunorderedlist', '|', 'undo', 'redo', '|', 'emoticons', 'image', 'multiimage', 'link'],
                    resizeType: 0,
                    //得到焦点事件
                    afterFocus: function () {
                        self.edit = edit = this; var strIndex = self.edit.html().indexOf("请填写内容..."); if (strIndex != -1) { self.edit.html(self.edit.html().replace("请填写内容...", "")); }
                    },
                    afterBlur: function () { this.sync(); self.edit = edit = this; if (self.edit.isEmpty()) { self.edit.html("请填写内容..."); } },   //关键  同步KindEditor的值到textarea文本框 
                    afterCreate: function () {
                        var self = this;
                        self.html("请填写内容...");
                        K.ctrl(document, 13, function () {
                            K('form[name=example]')[0].submit();
                        });
                        K.ctrl(self.edit.doc, 13, function () {
                            self.sync();
                            K('form[name=example]')[0].submit();

                        });
                    }
                });
            });
        });
        //加载帖子
        function GetComNewInfoData(startIndex, pageSize) {
            var warning = UrlDate.newtype == 1 ? "暂无招新通知！" : "暂无话题！";
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
                    LoginUID: "<%=UserInfo.UniqueNo%>",
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
                        GetComNewCommentDataPage(1, 10);
                    }
                    else {
                        $("#div_New").html('<div>'+warning+'</div>');
                    }
                },
                error: function (errMsg) {
                    $("#div_New").html('<div>' + warning + '</div>');
                }
            });
        }
        function ControlInitConfig(model) { //控件初始化设置           
            if (model != '') {
                InitByType(model);
                var $a_AddComNew = $('#a_AddComNew'); //发帖按钮
                $('#p_AddComNew').html(UrlDate.newtype == 0 ? '发帖' : '发布招新');
                var $a_NewClick = $('#a_NewClick'); //右侧帖子点赞按钮
                var $p_GoodWord = $('#p_GoodWord'); //点赞按钮文字               
                $p_GoodWord.html(model.IsGoodClick == 0 ? "点赞" : "取消点赞");
                $a_NewClick.click(function () {
                    GoodClick(model.Id, 'span_goodcount' + model.Id, 0, 'p_GoodWord');
                });
                if (model.IsLeader == 1) {
                    $a_AddComNew.show();
                    $a_AddComNew.click(function () {
                        AddComNewInfo(model.RelationId, 1, navid);
                    });
                } else {
                    if (model.IsMember == 0) { }
                    $a_AddComNew.hide();
                }
                GetNewestNew(0, model.RelationId, 'ul_Newest', 'li_Newest', 1, 5, UrlDate.newtype);
            }
        }
        function InitByType(model) {
            var $img_Pic = $('#img_Pic');
            var $h1_RelName = $('#h1_RelName');
            var $a_IntoWord = $('#a_IntoWord');
            var $h1_RelTypeName = $('#h1_RelTypeName');
            var $a_TwoCrumbs = $('#a_TwoCrumbs'); //二级面包屑
            var $a_ThreeCrumbs = $('#a_ThreeCrumbs'); //三级面包屑
            $a_ThreeCrumbs.html(UrlDate.newtype == 0 ? "帖子" : "招新通知");
            $a_TwoCrumbs.html(model.RelationName);
            if (UrlDate.type == 0) {
                $img_Pic.attr("src", model.PicURL);
                var assoUrl = '/StuAssociate/AssociateDetail.aspx?itemid=' + model.RelationId + '&nav=' + navid;
                $a_TwoCrumbs.attr('href', assoUrl);
                $h1_RelName.html(model.RelationName);
                $a_IntoWord.html("进入社团>");
                $a_IntoWord.click(function () {
                    location.href = assoUrl;
                });
                $h1_RelTypeName.html(UrlDate.newtype==0?"社团最新贴":"社团招新通知");
            } else if (UrlDate.type == 1) {
                $img_Pic.attr("src", model.PicURL);
                var departUrl = '/StuActivity/DepartDetail.aspx?itemid=' + model.RelationId + '&nav=' + navid;
                $a_TwoCrumbs.attr('href', departUrl);
                $h1_RelName.html(model.RelationName);
                $a_IntoWord.html("进入部门>");
                $a_IntoWord.click(function () {
                    location.href = departUrl;
                });
                $h1_RelTypeName.html(UrlDate.newtype == 0 ? "部门最新贴" : "部门招新通知");
            } else if (UrlDate.type == 2) {
                var roomUrl = '/StuDormitory/DormRoomDetail.aspx?itemid=' + model.RelationId + '&nav=' + navid;
                $a_TwoCrumbs.attr('href', roomUrl);
                $h1_RelName.html(model.BuildName + "-" + model.RelationName);
                $a_IntoWord.html("进入宿舍>");
                $a_IntoWord.click(function () {
                    location.href = roomUrl;
                });
                $h1_RelTypeName.html(UrlDate.newtype == 0 ? "宿舍最新贴" : "宿舍招新通知");
            }
        }
        //加载回复
        function GetComNewCommentDataPage(startIndex, pageSize) {
            var $tbPar = $("#div_Comment");
            var $trSub = $("#div_CommentSub");
            var $pageBar = $("#pageBar_Comment");
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/CommonHandler/Com_NewHandler.ashx",
                    Func: "GetComNewCommentDataPage",
                    Type: UrlDate.type,
                    LoginUID: "<%=UserInfo.UniqueNo%>",
                    NewId: UrlDate.itemid,
                    PageIndex: startIndex,
                    PageSize: pageSize,
                    IsOnlyBase: "1",
                    IsUnifiedInfo: "1"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $tbPar.html('');
                        var rtnObj = json.result.retData;
                        $trSub.tmpl(rtnObj.PagedData).appendTo("#div_Comment");
                        $("#span_CommCount" + UrlDate.itemid).html(rtnObj.RowCount);
                        $("#span_Count").html(rtnObj.RowCount);
                        $pageBar.show();
                        laypage({
                            cont: $pageBar, //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                            pages: json.result.retData.PageCount, //通过后台拿到的总页数
                            curr: json.result.retData.PageIndex || 1, //当前页
                            skip: false, //是否开启跳页
                            skin: '#74c3f4',
                            jump: function (obj, first) { //触发分页后的回调
                                if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                    GetComNewCommentDataPage(obj.curr, pageSize)
                                }
                            }
                        });
                    }
                    else {
                        $tbPar.html('<div>暂无回复！</div>');
                        $pageBar.hide();
                    }
                },
                error: function (errMsg) {
                    $tbPar.html('<div>暂无回复！</div>');
                }
            });
        }
        //保存回复信息
        function AddComNewComment() {
            var content = $("#area_Content").val().trim();
            if (!content.length || content == "请填写内容...") { layer.msg("请填写内容！"); return; }
            content = encodeURIComponent(content);
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/CommonHandler/Com_NewHandler.ashx",
                    Func: "AddComNewComment",
                    NewId: UrlDate.itemid,
                    Content: content,
                    LoginUID: "<%=UserInfo.UniqueNo%>"
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == 0) {
                        layer.msg("回复成功!");
                        GetComNewCommentDataPage(1, 10);
                        $("#area_Content").val('');
                        curEditor.edit.html("请填写内容...");
                    } else {
                        layer.msg(result.errMsg);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.msg("操作失败！");
                }
            });
        }
        function JumpToPage(type, itemid) {
            if (type == 0) {  //跳转帖子详情页面
                window.location.href = '/CommonPage/NewInfoDetail.aspx?type=' + UrlDate.type + '&newtype=' + UrlDate.newtype + '&itemid=' + itemid + '&nav=' + navid;
            }
        }
    </script>     
</body>
</html>