<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserMessage.aspx.cs" Inherits="SCWeb.CommonPage.UserMessage" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>消息</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css">
    <link rel="stylesheet" href="/css/style.css">
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/laypage/laypage.js"></script>
    <script type="text/javascript">
        $(function () {
            $('.stytem_select_left a').on('click', function () {
                $(this).addClass('on').siblings().removeClass('on');
                var n = $(this).index();
                $('.messages_wrap>.messages_state').eq(n).show().siblings().hide();
            })
        })
    </script>
    <script id="tr_message" type="text/x-jquery-tmpl">
        {{if Status==0}}
            <li id="li_email${Id}">
        {{else}}<li class="readed" id="li_email${Id}">{{/if}}
                        <input type="checkbox" name="cbkmessage" value="${Id}"/>
            <i class="icon icon-envelope"></i>
            <a href="javascript:" msgid="${Id}" status="${Status}">
                <span class="catagory">{{if Type==0}}[社团]
                    {{else Type==1}}[活动]
                    {{else Type==2}}[宿舍]                    
                    {{else}}[暂无]
                    {{/if}}
                </span>
                ${NameLengthUpdate(Title,30)}
            </a>
            <span class="fr date">${DateTimeConvert(CreateTime)}</span>
            <span class="fr" style="float: right; margin-right: 10px">发件人：${CreateName}</span>
            <div class="messages_detail clearfix">
                {{html Contents}}
            </div>
        </li>
    </script>
    <style type="text/css">
        .messages_state ul li {
            border-bottom: 1px dotted #ccc;
            padding: 5px 0px;
            line-height: 20px;
            font-size: 15px;
            overflow: hidden;
        }

            .messages_state ul li input {
                float: left;
                margin-right: 10px;
            }

            .messages_state ul li .date {
                float: right;
                line-height: 20px;
            }

            .messages_state ul li span {
                display: inline-block;
                float: left;
            }

            .messages_state ul li a {
                font-size: 15px;
                display: inline-block;
                float: left;
                width: 70%;
                text-overflow: ellipsis;
                overflow: hidden;
                white-space: nowrap;
            }

            .messages_state ul li .icon {
                width: 20px;
                height: 20px;
                float: left;
                margin-right: 10px;
                color: #ea5666;
            }

            .messages_state ul li.readed .icon {
                color: #999;
            }

            .messages_state ul li.readed a {
                color: #999;
            }

            .messages_state ul li.readed .messages_detail, .messages_state ul li.readed .catagory {
                color: #999;
            }

        .tips_bar {
            border-top: 1px solid #ccc;
            border-bottom: 1px solid #ccc;
            line-height: 48px;
            background: #FBFBFB;
        }

            .tips_bar input[type=button] {
                height: 30px;
                color: #fff;
                font-size: 14px;
                border-radius: 2px;
                border: none;
                cursor: pointer;
            }

            .tips_bar .delete {
                background: #1472b9;
            }

        .messages_state ul li .messages_detail {
            padding-left: 58px;
            padding-right: 195px;
            margin-top: 10px;
            font-size: 14px;
            display: none;
            text-indent: 2em;
        }
    </style>
</head>
<body>
    <input type="hidden" id="HLoginUID" value="<%=UserInfo.UniqueNo%>"/>
    <input type="hidden" id="HLoginName" value="<%=UserInfo.Name%>"/>
    <input type="hidden" id="HEmailID" runat="server"/>
    <div id="header"></div>
    <div id="main">
        <div class="center pt10">
            <div class="wrap" style="padding-bottom: 20px;">
                <div class="bordshadrad width" style="background: #fff; margin-top: 20px;">
                    <div class="personal_spacea  clearfix " style="padding: 20px;">
                        <div class="messages_wrap">
                            <div class="messages_state">
                                <div class="tips_bar">
                                    <input type="checkbox" name="cbkAllmessage" class="Check_box"/>
                                    <input type="button" value="标记所选为已读" style="background:darkgrey;" class="mark_readed" onclick="javascript: ReaderMessage('red');" />
                                    <input type="button" value="删除所选" class="delete" onclick="javascript: ReaderMessage('del');" />
                                </div>
                                <ul id="tb_message"></ul>
                                <div class="page" id="pageBar"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="footer"></div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        var isload = false;
        $(function () {            
            $('#header').load('/CommonPage/Header.aspx?t=' + new Date().getTime(), function (response, status, xhr) {
                if (status == "success" || status == "notmodified") {
                   
                }
            });
            $('#footer').load('/CommonPage/footer.html');
            if (UrlDate.Type == "0") {
                $(".repository_header_wrap").hide();
            }
            else {
                $(".repository_header_wrap").show();
            }
            if ($("#HEmailID").val() != "" && $("#HEmailID").val().length>0) {
                readMsg($("#HEmailID").val());
            } else {
                getData(1, 10);
            }          
            $('.messages_state ul li').children('a').on('click', function () {
                $(this).siblings('.messages_detail').slideToggle();
                $('.messages_state ul li').find('.messages_detail').not($(this).siblings('.messages_detail')).slideUp();
                $(this).parent('li').addClass('readed');
                var msgid = $(this).attr("msgid");
                var status = $(this).attr("status");
                if (msgid != "" && status == 0) {
                    UpdateMessage(msgid);
                }
            })
        })

        function getData(startIndex, pageSize) {
            var $tbPar = $("#tb_message");
            var $trSub = $("#tr_message");
            var $pageBar = $("#pageBar");
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/CommonHandler/Com_MessageHandler.ashx",
                    Func: "GetComMessageDataPage",
                    Receiver: $("#HLoginUID").val(),
                    PageIndex: startIndex,
                    pageSize: pageSize
                },
                success: function (json) {
                    if (json.result.errMsg == "success") {
                        $tbPar.html('');
                        var items = json.result.retData.PagedData;
                        $trSub.tmpl(items).appendTo("#tb_message");
                        $pageBar.show();
                        laypage({
                            cont: $pageBar, //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                            pages: json.result.retData.PageCount, //通过后台拿到的总页数
                            curr: json.result.retData.PageIndex || 1, //当前页
                            skip: false, //是否开启跳页
                            skin: '#74c3f4',
                            jump: function (obj, first) { //触发分页后的回调
                                if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                    getData(obj.curr, pageSize)
                                }
                            }
                        });
                        if (items != null && items.length > 0 && $("#HEmailID").val().length>0) {
                            $.each(items, function (index, evet) {
                                if (evet.Id == $("#HEmailID").val() && !isload) {
                                    $("#li_email" + evet.Id).find(".messages_detail").slideDown();
                                    isload = true;
                                    return;
                                }
                            })
                        }
                        NewCheckAll($('.messages_state input[type=checkbox]'));
                    }
                    else {
                        $tbPar.html("<li>暂无消息！</li>");
                        $pageBar.hide();
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {

                }
            });
        }

        function ReaderMessage(param) {
            var messages = "";
            var optValDel = "";
            var optValStus = "";
            $("input[name='cbkmessage']").each(function () {
                if ($(this).is(':checked')) {
                    var vals = $(this).val();
                    messages += vals + ",";
                }
            });
            if (param == "del") {
                optValDel = 1;
            } else {
                optValStus = 1;
            }
            if (messages != "") {
                messages = messages.substring(0, messages.length - 1);
                $.ajax({
                    url: "/Common.ashx",
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: { PageName: "/CommonHandler/Com_MessageHandler.ashx", Func: "ReaderMessage", Ids: messages, IsDelete: optValDel, Status: optValStus, Receiver: $("#HLoginUID").val() },
                    success: function (json) {
                        if (json.result.errMsg.toString() == "success") {
                            layer.msg('更新成功！');
                        }
                        getData(1, 10);
                    },
                    error: function (errMsg) {
                        layer.msg('更新失败！');
                    }
                });
            }
        }

        function readMsg(id)
        {
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/CommonHandler/Com_MessageHandler.ashx",
                    Func: "ReaderMessage",
                    Ids: id,
                    Status: 1,
                    Receiver: $("#HLoginUID").val()
                },
                success: function (json) {
                    getData(1, 10);
                },
                error: function (errMsg) {
                }
            });
        }

        function UpdateMessage(id) {
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { PageName: "/CommonHandler/Com_MessageHandler.ashx", Func: "UpdateMessage", Id: id, Status: 1 },
                success: function (json) {

                },
                error: function (errMsg) {

                }
            });
        }
    </script>
</body>
</html>

