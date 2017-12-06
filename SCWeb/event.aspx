<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="event.aspx.cs" Inherits="SCWeb._event" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery.form.min.js"></script>
    <script type="text/javascript">
        $(function () {

            if ($("#EventID").val() != undefined && $("#EventID").val() != "") {
                $(".del").show();
            }
            else {
                $(".del").hide();
            }
            $(".datepicker").datepicker({ dateFormat: 'yy-mm-dd' });

            $("#isallday").click(function () {
                if ($("#sel_start").css("display") == "none") {
                    $("#sel_start,#sel_end").show();
                } else {
                    $("#sel_start,#sel_end").hide();
                }
            });

            $("#isend").click(function () {
                if ($("#p_endtime").css("display") == "none") {
                    $("#p_endtime").show();
                } else {
                    $("#p_endtime").hide();
                }
                $.fancybox.resize();//调整高度自适应
            });
            if ($("#EventID").val() != undefined && $("#EventID").val() != "") {
                $("#Title").html("编辑事件");
                GetDateByID();

            }
            else { $("#Title").html("新建事件"); }
        });
        //添加日程数据
        function Add() {
            var events = $("#event").val();
            if (events == '') {
                alert("请输入日程内容！");
                $("#event").focus();
                return false;
            }
            else {
                var StartDate = $("#startdate").val();
                var enddate = $("#enddate").val();
                var AllDay = 0;
                var endTime = 1;
                if ($("#sel_start").css("display") == "none") {
                    AllDay = 1;
                }
                else {
                    AllDay = 0;
                }
                if ($("#p_endtime").css("display") == "none") {
                    endTime = 0;
                    enddate = StartDate;
                }
                else {
                    endTime = 1;
                }
                if ($("#sel_start").css("display") != "none") {
                    StartDate = StartDate + " " + $("#s_hour").val() + ":" + $("#s_minute").val();
                    enddate = enddate + " " + $("#e_hour").val() + ":" + $("#e_minute").val()
                }
                $.ajax({
                    type: "post",
                    url: "json.ashx",
                    data: { "Func": "AddSchedule", Content: events, StartDate: StartDate, EndDate: enddate, IdCard: $("#HUserIdCard").val(), AllDay: AllDay, EndTime: endTime, id: $("#EventID").val() },
                    dataType: "json",
                    success: function (json) {
                        if (json.result.errNum.toString() == "0") {
                            $.fancybox.close();
                            $('#calendar').fullCalendar('refetchEvents'); //重新获取所有事件数据
                        }
                        else {
                            alert(json.result.errMsg)
                        }
                    },
                    error: function (errMsg) {
                        alert('操作失败！');
                    }
                })
            }
        }

        //根据ID获取日程信息
        function GetDateByID() {
            $.ajax({
                type: "post",
                url: "json.ashx",
                data: {
                    "Func": "GetSchedule", EditID: $("#EventID").val(), id: $("#EventID").val()
                },
                dataType: "json",
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $(json.result.retData).each(function () {
                            $("#event").val(this.Name);
                            var startTime = new Date(this.StartDate);
                            var isEndTime = this.isEndTime;
                            var endtime = new Date(this.EndDate);
                            var start_d = startTime.getFullYear() + "-" + (parseInt(startTime.getMonth()) + 1) + "-" + startTime.getDate();
                            var start_h = startTime.getHours();

                            if (start_h.toString().length == 1) {
                                start_h = "0" + start_h;
                            }
                            var start_m = startTime.getMinutes();
                            if (start_m.toString().length == 1) {
                                start_m = "0" + start_m;
                            }
                            $("#startdate").val(start_d);
                            var allday = this.AllDay;
                            if (allday == 1) {
                                $("#sel_start,#sel_end").hide();
                                $("#isallday").attr("checked", true);
                            } else {
                                $("#sel_start,#sel_end").show();
                                $("#s_hour").val(start_h);
                                $("#s_minute").val(start_m);
                                $("#isallday").attr("checked", false);
                            }
                            if (isEndTime == 0) {
                                endtime = startTime;
                                $("#isend").attr("checked", false);
                                $("#p_endtime").hide();
                            }
                            else {
                                //$.fancybox.resize();//调整高度自适应

                                $("#isend").attr("checked", true);
                                $("#p_endtime").show();

                            }
                            var end_d = endtime.getFullYear() + "-" + (parseInt(endtime.getMonth()) + 1) + "-" + endtime.getDate();
                            var end_h = endtime.getHours();
                            var end_m = endtime.getMinutes();
                            if (end_h.toString().length == 1) {
                                end_h = "0" + end_h;
                            }
                            if (end_m.toString().length == 1) {
                                end_m = "0" + end_m;
                            }
                            $("#e_hour").val(end_h);
                            $("#e_minute").val(end_m);

                            $("#enddate").val(end_d);

                        })
                    }
                    else {
                        alert(json.result.errMsg);
                    }
                },
                error: function (errMsg) {
                    alert('操作失败！');
                }
            })
            $.fancybox.resize();//调整高度自适应

        }
        function Del() {
            if (confirm("您确定要删除吗？")) {
                var eventid = $("#EventID").val();
                $.ajax({
                    type: "post",
                    url: "json.ashx",
                    data: {
                        "PageName": "ResourceManage/MyResourceHander.ashx", "Func": "DelSchedule", id: $("#EventID").val()
                    },
                    dataType: "json",
                    success: function (json) {
                        if (json.result.errNum.toString() == "0") {
                            $.fancybox.close();
                            $('#calendar').fullCalendar('refetchEvents'); //重新获取所有事件数据 
                        }
                        else {
                            alert(json.result.errMsg);
                        }
                    },
                    error: function (errMsg) {
                        alert('操作失败！');
                    }
                })
            }
        }
    </script>
</head>
<body>
    <div class="fancy">
        <h3 id="Title">新建事件</h3>
        <input type="hidden" id="HUserIdCard"  value="<%=UserInfo.IDCard %>"/>
        <input type="hidden" id="EventID" runat="server"/>
        <input type="hidden" name="action" value="add" />
        <p>日程内容：<input type="text" class="input" name="event" id="event" style="width: 320px" placeholder="记录你将要做的一件事..." /></p>
        <p>
            开始时间：<input type="text" class="input datepicker" name="startdate" id="startdate" value="<%=Date %>" />
            <span id="sel_start" style="display: none">
                <select name="s_hour" id="s_hour">
                    <option value="00">00</option>
                    <option value="01">01</option>
                    <option value="02">02</option>
                    <option value="03">03</option>
                    <option value="04">04</option>
                    <option value="05">05</option>
                    <option value="06">06</option>
                    <option value="07">07</option>
                    <option value="08" selected="selected">08</option>
                    <option value="09">09</option>
                    <option value="10">10</option>
                    <option value="11">11</option>
                    <option value="12">12</option>
                    <option value="13">13</option>
                    <option value="14">14</option>
                    <option value="15">15</option>
                    <option value="16">16</option>
                    <option value="17">17</option>
                    <option value="18">18</option>
                    <option value="19">19</option>
                    <option value="20">20</option>
                    <option value="21">21</option>
                    <option value="22">22</option>
                    <option value="23">23</option>
                </select>:
                    <select name="s_minute" id="s_minute">
                        <option value="00">00</option>
                        <option value="10">10</option>
                        <option value="20">20</option>
                        <option value="30">30</option>
                        <option value="40">40</option>
                        <option value="50">50</option>
                    </select>
            </span>
        </p>
        <p id="p_endtime" style="display: none">
            结束时间：<input type="text" class="input datepicker" name="enddate" id="enddate" value="<%=Date %>">
            <span id="sel_end" style="display: none">
                <select name="e_hour" id="e_hour">
                    <option value="00">00</option>
                    <option value="01">01</option>
                    <option value="02">02</option>
                    <option value="03">03</option>
                    <option value="04">04</option>
                    <option value="05">05</option>
                    <option value="06">06</option>
                    <option value="07">07</option>
                    <option value="08">08</option>
                    <option value="09">09</option>
                    <option value="10">10</option>
                    <option value="11">11</option>
                    <option value="12" selected="selected">12</option>
                    <option value="13">13</option>
                    <option value="14">14</option>
                    <option value="15">15</option>
                    <option value="16">16</option>
                    <option value="17">17</option>
                    <option value="18">18</option>
                    <option value="19">19</option>
                    <option value="20">20</option>
                    <option value="21">21</option>
                    <option value="22">22</option>
                    <option value="23">23</option>
                </select>:
                    <select name="e_minute" id="e_minute">
                        <option value="00">00</option>
                        <option value="10">10</option>
                        <option value="20">20</option>
                        <option value="30">30</option>
                        <option value="40">40</option>
                        <option value="50">50</option>
                    </select>
            </span>
        </p>
        <p>
            <label>
                <input type="checkbox" value="1" id="isallday" name="isallday" checked="checked" />
                全天</label>
            <label>
                <input type="checkbox" value="1" id="isend" name="isend" />
                结束时间</label>
        </p>
        <div class="sub_btn">
            <span class="del" style="display: none">
                <input type="button" class="btn1 btn_del" id="del_event" value="删除" onclick="Del()" />
            </span>
            <input type="submit" class="btn1 btn_ok" value="确定" onclick="Add()" />
            <input type="button" class="btn1 btn_cancel" value="取消" onclick="$.fancybox.close()" />
        </div>
    </div>

</body>
</html>
