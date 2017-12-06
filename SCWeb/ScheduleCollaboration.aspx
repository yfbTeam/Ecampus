<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduleCollaboration.aspx.cs" Inherits="SCWeb.ScheduleCollaboration" %>

<!DOCTYPE html>

<html>
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <meta charset="utf-8" />
    <title>日程协同</title>
    <!--图标样式-->
    <link rel="stylesheet" type="text/css" href="/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/reset.css" />
    <link rel="stylesheet" type="text/css" href="/css/common.css" />
    <link rel="stylesheet" type="text/css" href="/css/index.css" />
    <link rel="stylesheet" type="text/css" href="/css/fullcalendar.css" />
    <link rel="stylesheet" href="/css/iconfont.css">
    <link rel="stylesheet" href="/css/style.css">
    <link href="/css/fancybox.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src='http://code.jquery.com/ui/1.10.3/jquery-ui.js'></script>
    <script src="/Scripts/jquery.fancybox-1.3.1.pack.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <style type="text/css">
        .fancy {
            width: 450px;
            height: auto;
        }

            .fancy h3 {
                height: 30px;
                line-height: 30px;
                border-bottom: 1px solid #d3d3d3;
                font-size: 14px;
            }

            .fancy form {
                padding: 10px;
            }

            .fancy p {
                height: 28px;
                line-height: 28px;
                padding: 4px;
                color: #999;
            }

        .input {
            height: 20px;
            line-height: 20px;
            padding: 2px;
            border: 1px solid #d3d3d3;
            width: 100px;
        }

        .btn1 {
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            padding: 5px 12px;
            cursor: pointer;
        }

        .btn_ok {
            background: #360;
            border: 1px solid #390;
            color: #fff;
        }

        .btn_cancel {
            background: #f0f0f0;
            border: 1px solid #d3d3d3;
            color: #666;
        }

        .btn_del {
            background: #f90;
            border: 1px solid #f80;
            color: #fff;
        }

        .sub_btn {
            height: 32px;
            line-height: 32px;
            padding-top: 6px;
            border-top: 1px solid #f0f0f0;
            text-align: right;
            position: relative;
        }

            .sub_btn .del {
                position: absolute;
                left: 2px;
            }
    </style>
    <!--[if IE]>
			<script src="js/html5.js"></script>
		<![endif]-->
    <script type="text/javascript">
        //日程协同
        $(function () {
            SetPageButton('<%=UserInfo.UniqueNo%>');

            $('#calendar').fullCalendar({
                header: {
                    left: 'prev,next today',
                    center: 'title',
                    right: 'month,agendaWeek,agendaDay'
                },
                height: '700',
                firstDay: 0,
                weekMode: 'liquid',
                events: 'json.ashx?Func=GetDate&CreateUID=' + $("#HUserIdCard").val(),
                dayClick: function (date, allDay, jsEvent, view) {
                    var selDate = $.fullCalendar.formatDate(date, 'yyyy-MM-dd');
                    $.fancybox({
                        'type': 'ajax',
                        'href': 'event.aspx?action=add&date=' + selDate,
                    });
                },
                //单击事件项时触发
                eventClick: function (calEvent, jsEvent, view) {
                    if (!calEvent.isEndTime) {
                        $.fancybox({
                            'type': 'ajax',
                            'href': 'event.aspx?action=edit&id=' + calEvent.id,
                        });
                    }
                    else {
                        $.fancybox({
                            width: 450,
                            height: 220,
                            maxheight: 220,
                            autoScale: true,
                            autoDimensions: true,
                            'autoDimensions': false,
                            'type': 'ajax',
                            'href': 'event.aspx?action=edit&id=' + calEvent.id,
                        });
                    }
                }

            });

        });
    </script>
</head>
<body>
    <input type="hidden" id="HUserIdCard" value="<%=UserInfo.UniqueNo %>" />

    <!--header-->
    <div id="header"></div>
    <!--内容-->
    <div id="main">
        <div class="center clearfix  pt10">
            <div class="scheduprog">
                <div class="calendar_titie">
                    日程安排
                </div>
                <div class="calendar_wrap bordshadrad">
                    <div class="wrap" style="padding: 15px 20px 0px 20px">
                        <div id='calendar'></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="footer"></div>
    <script type="text/javascript" src="/Scripts/fullcalendar.min.js"></script>
    <script>
        $(function () {
            $('#header').load('/CommonPage/Header.aspx?t=' + new Date().getTime(), function (response, status, xhr) {
                if (status == "success" || status == "notmodified") {
                    SetNavSelected(1);
                }
            });
            $('#footer').load('/CommonPage/footer.html');
        })
    </script>
</body>
</html>
