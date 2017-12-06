<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SkyDrivePersonal.aspx.cs" Inherits="SCWeb.Synergy.SkyDrivePersonal" %>

<!DOCTYPE html>

<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <title>个人网盘</title>
    <!--图标样式-->
    <link rel="stylesheet" type="text/css" href="/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="/css/reset.css" />
    <link rel="stylesheet" type="text/css" href="/css/common.css" />
    <link rel="stylesheet" type="text/css" href="/css/repository.css" />
    <link rel="stylesheet" href="/css/iconfont.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <link href="/css/sprite.css" rel="stylesheet" />
    <script src="/Scripts/jquery-1.11.2.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/laypage/laypage.js"></script>
    <style type="text/css">
        .h-ico {
            display: inline-block;
        }
    </style>
    <script id="tr_User" type="text/x-jquery-tmpl">
        <li class="clearfix">
            <div class="checkbox fl">
                <input type='checkbox' value='${ID}' class='Check_box' name='Check_box' id='subcheck' onclick='setSelectAll()' />
            </div>
            <div class="docu_messages fl">
                <p class="docu_title">
                    <span class="ico-${postfix1}-min h-ico" onclick="FolderClick(${ID},'${FileUrl}','${code}',${IsFolder})"></span>
                    <span class="docu_name" onclick="FolderClick(${ID},'${FileUrl}','${code}',${IsFolder})" title="${Name}">${cutstr(Name,30)}</span>
                </p>
            </div>
            <div class="unload_none fl">
                <span class="upload">
                    <i class="icon icon-download-alt" onclick="Down(${ID})" title="下载"></i>
                </span>
                <span class="arrow-down pr">
                    <i class="icon" title="更多">
                        <img src="/images/xai.png" /></i>
                    <div class="arrow_downwrap">
                        <span onclick="Move(${ID})">移动到</span>
                        <%-- <span onclick="Del(${ID})">复制到</span>--%>
                        <span class="rename" onclick="cl(this,${ID},'${Name}','${FileUrl}')">重命名</span>
                        <span class="delete" onclick="Del(${ID},'${Name}')">删除</span>
                    </div>
                </span>
            </div>
            <div class="skydrive_size fl">
                <span>{{if FileSize==0}}--
                        {{else FileSize>1024*1024}}
                            ${(FileSize/1024/1024).toFixed(2)}MB
                        {{else}}
                            ${(FileSize/1024).toFixed(2)}KB
                        {{/if}}
                   
                </span>
            </div>
            <div class="date fr">
                <span>${DateTimeConvert(CreateTime,'yyyy-MM-dd')}</span>
            </div>
        </li>


    </script>
    <script id="tr_User1" type="text/x-jquery-tmpl">
        <li>
            <div class="checkbox">
                <input type='checkbox' value='${ID}' class='Check_box' name='Check_box' onclick='setSelectAll()' />
            </div>
            <div class="skydrive_grid_view">
                <a href="#" onclick="FolderClick(${ID},'${FileUrl}','${code}',${IsFolder})">
                    <em class="ico-${postfix1}-max a-ico-file">
                        <img style="width: 100%; height: auto;" /></em>
                    <p class="grid_view_name" title="${Name}">${cutstr(Name,12)}</p>
                </a>
            </div>
        </li>
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="hidden" id="HUserIdCard" value="<%=UserInfo.UniqueNo %>" />
            <input id="Pid" type="hidden" value="0" />
            <input id="code" type="hidden" value="0" />
            <input id="HFoldUrl" type="hidden" value="/DriveFolder" />
            <input id="ShowType" type="hidden" value="1" />
            <input id="GroupName" type="hidden" value="" />
            <input id="Postfixs" type="hidden" value="" />
            <input id="TimeQuery" type="hidden" value="" />
            <div id="header"></div>
            <!--网盘-->
            <div id="main">
                <div class="center clearfix  pt10">
                    <div style="background: #fff;">
                        <div class="modult_toolsbar clearfix">
                            <div class="list_grid_switch fr">
                                <a href="javascript:;" class="list_switch on">
                                    <i class="icon icon-th-list" onclick="ShowType(1)"></i>
                                </a>
                                <a href="javascript:;" class="grid_switch">
                                    <i class="icon  icon-th-large" onclick="ShowType(2)"></i>
                                </a>
                            </div>
                            <div class="toolsbar fl">
                                <div class="bars">
                                    <a href="#" class="upload pr">
                                        <i class="icon  icon-upload-alt"></i>
                                        <span class="txt" onclick="upload()">上传文件</span>
                                    </a>
                                    <a href="#" class="newfile">
                                        <i class="icon icon-folder-close-alt"></i>
                                        <span class="txt" onclick="javascript: appendAfterRow('tb_MyResource', '1')">新建文件夹</span>
                                    </a>
                                    <a href="javascript:;" class="operate1 pr">
                                        <i class="icon  icon-wrench"></i>
                                        <span class="txt">批量操作</span>
                                        <div class="operate_wrap">
                                            <span onclick="Del('','')">删除</span>
                                            <span onclick="Move('','')">移动</span>
                                            <span onclick="Down('','')">下载</span>
                                        </div>
                                    </a>
                                </div>
                            </div>
                        </div>
                        <div class="selectionwrap">
                            <div class="select_nav clearfix pr">
                                <div class="select_nav_left fl">
                                    文件类型:
                                </div>
                                <ul class="select_nav_right clearfix" id="ResourceType">
                                    <li class="on">
                                        <a href="#" onclick="serchType('',this)">不限</a>
                                    </li>

                                </ul>
                            </div>
                            <div class="select_nav clearfix pr">
                                <div class="select_nav_left fl">
                                    上传时间
                                </div>
                                <ul class="select_nav_right">
                                    <li class="on">
                                        <a href="#" onclick="SerchTime('',this)">不限</a>
                                    </li>
                                    <li>
                                        <a href="#" onclick="SerchTime('Week',this)">一周内</a>
                                    </li>
                                    <li>
                                        <a href="#" onclick="SerchTime('Month',this)">一月内</a>
                                    </li>
                                    <li>
                                        <a href="#" onclick="SerchTime('Year',this)">半年内</a>
                                    </li>
                                </ul>
                            </div>
                        </div>

                        <!--docu-->
                        <div class="docu_wrap">
                            <!--面包屑-->
                            <div class="crumbs" id="Nav">
                                <a href="#" onclick="FolderClick(0, '\DriveFolder', 0,1)">全部</a>
                            </div>
                            <div class="docu_content">
                                <!--docu_list-->
                                <div class="IsCheckAll">
                                    <div class="clearfix" style="padding: 10px; border-top: 1px dotted #CFCFCF;">
                                        <div class="checkbox fl" style="margin-left: 1px;">
                                            <input type="checkbox" name="" id="checkAll" value="" style="width: 18px; height: 18px; border: 1px solid rgba(0,0,0,.2); border-radius: 2px; background: #fff;" />
                                        </div>
                                        <div class="docu_messages fl">
                                            <p class="docu_title" style="height: 24px; font-size: 15px; color: #555555; line-height: 24px; padding-left: 12px;">
                                                文件名
                                            </p>
                                        </div>
                                        <div class="bars none" style="margin: 5px 0px 0px 100px;">
                                            <a href="#" class="newfile">
                                                <i class="icon icon-trash"></i>
                                                <span class="txt" onclick="Del('','')">删除</span>
                                            </a>
                                            <a href="#" class="upload pr">
                                                <i class="icon  icon-download-alt"></i>
                                                <span class="txt" onclick="Down('','')">下载</span>
                                            </a>

                                            <a href="#" class="newfile">
                                                <span class="txt" onclick="Move('','')">移动</span>
                                            </a>
                                        </div>
                                        <div class="skydrive_size fl" style="left: 61%">
                                            <span style="font-size: 15px;">大小</span>
                                        </div>
                                        <div class="datea fr" style="line-height: 23px; margin-right: 15px;">
                                            <span style="font-size: 15px; color: #999999">修改时间</span>
                                        </div>
                                    </div>
                                    <ul class="document_list skydrive_docu_list" style="display: block;" id="tb_MyResource">
                                    </ul>
                                </div>
                                <div class="none IsCheckAlla">
                                    <div class="clearfix" style="padding: 10px; border-top: 1px dotted #CFCFCF;">
                                        <div class="checkbox fl" style="margin-left: 1px;">
                                            <input type="checkbox" name="" id="checkAll" value="" style="width: 18px; height: 18px; border: 1px solid rgba(0,0,0,.2); border-radius: 2px; background: #fff;" />
                                        </div>
                                        <div class="docu_messages fl">
                                            <p class="docu_title" style="height: 24px; font-size: 15px; color: #555555; line-height: 24px; padding-left: 12px;">
                                                文件名
                                            </p>
                                        </div>
                                        <div class="bars none" style="margin: 5px 0px 0px 100px;">
                                            <a href="#" class="newfile">
                                                <i class="icon icon-trash"></i>
                                                <span class="txt" onclick="Del('','')">删除</span>
                                            </a>
                                            <a href="#" class="upload pr">
                                                <i class="icon  icon-download-alt"></i>
                                                <span class="txt" onclick="Down('','')">下载</span>
                                            </a>

                                            <a href="#" class="newfile">
                                                <span class="txt" onclick="Move('','')">移动</span>
                                            </a>

                                        </div>
                                    </div>
                                    <ul class="skydrive_grid_list clearfix" id="tb_MyResource1">
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <!--分页-->
                        <div class="page none">
                            <span id="pageBar"></span>
                        </div>
                    </div>
                </div>
            </div>
            <div id="footer"></div>
        </div>
    </form>
</body>


<script type="text/javascript">
    $(document).ready(function () {
        $('#header').load('/CommonPage/Header.aspx');
        SetPageButton($("#HUserIdCard").val());
        $('#footer').load('/CommonPage/footer.html');
        //列表页与图标页切换
        $('.list_grid_switch').find('a').click(function () {
            $(this).addClass('on').siblings().removeClass('on');
            var n = $(this).index();
            $('.docu_content>div').eq(n).show().siblings().hide();
        })
        //鼠标划过批量操作
        $('.operate1').hover(function () {
            $(this).find('.operate_wrap').show();
        }, function () {
            $(this).find('.operate_wrap').hide();
        })
        GetFileType()
        //获取数据
        getData(1, 10);
    });

    //获取数据
    function ShowType(Type) {
        if (Type == "1") {
            $('.IsCheckAll .bars').hide();
            $('.IsCheckAlla .bars').hide();
            $("#ShowType").val("1");
            getData(1, 10);
        }
        if (Type == "2") {
            $('.IsCheckAll .bars').hide();
            $('.IsCheckAlla .bars').hide();
            $("#ShowType").val("2");
            getData(1, 16);
        }
    }

    //获取数据
    function getData(startIndex, pageSize) {
        $('.IsCheckAll .bars').hide();
        $('.IsCheckAlla .bars').hide();
        $("#checkAll").attr("checked", false);
        var Pid = $("#Pid").val();
        var DocName = $("#search_w").val();
        //初始化序号 
        pageNum = (startIndex - 1) * pageSize + 1;
        var CreateUID = $("#HUserIdCard").val();
        //name = name || '';
        $.ajax({
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            type: "post",
            async: false,
            dataType: "json",
            data: { "PageName": "Synergy/SkyDrivePersonal.ashx", "Func": "GetPageList", PageIndex: startIndex, pageSize: pageSize, CreateUID: CreateUID, "Pid": Pid, "DocName": DocName, "Postfixs": $("#Postfixs").val(), "CreateTime": $("#TimeQuery").val() },
            success: function OnSuccess(json) {
                var ShowType = $("#ShowType").val();

                if (json.result.errNum.toString() == "0") {
                    if (ShowType == "1") {
                        $("#tb_MyResource").html('');
                        $("#tr_User").tmpl(json.result.retData.PagedData).appendTo("#tb_MyResource");
                    }
                    else {
                        $("#tb_MyResource1").html('');
                        $("#tr_User1").tmpl(json.result.retData.PagedData).appendTo("#tb_MyResource1");
                    }
                    if (json.result.retData.RowCount > pageSize) {
                        $(".page").show();
                    }
                    laypage({
                        cont: 'pageBar', //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                        pages: json.result.retData.PageCount, //通过后台拿到的总页数
                        curr: json.result.retData.PageIndex || 1, //当前页
                        skip: false, //是否开启跳页
                        skin: '#1472b9',
                        jump: function (obj, first) { //触发分页后的回调
                            if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                getData(obj.curr, pageSize)
                            }
                        }
                    });
                    hoverShow($('.document_list li'), $('.unload_none'));
                    hoverShow($('.skydrive_grid_list li'), $('.checkbox'));
                    //全选
                    CheckAlla($('.IsCheckAll input[type=checkbox]'));
                    CheckAllb($('.IsCheckAlla input[type=checkbox]'));
                }
                else {
                    var html = '<div style="background: url(/images/error.png) no-repeat center center; height: 500px;"></div>';
                    if (ShowType == "1") {
                        $("#tb_MyResource").html(html);
                    }
                    else {
                        $("#tb_MyResource1").html(html);
                    }
                    $(".page").hide();
                }
            },
            error: OnError
        });
    }
    function CheckAlla(oInput) {
        var isCheckAll = function () {
            for (var i = 1, n = 0; i < oInput.length; i++) {
                oInput[i].checked && n++
            }
            oInput[0].checked = n == oInput.length - 1;
        };
        //全选
        oInput[0].onchange = function () {
            for (var i = 1; i < oInput.length; i++) {
                oInput[i].checked = this.checked;
                tabshow();
                listselect();
            }
            isCheckAll()
        };
        //根据复选个数更新全选框状态
        for (var i = 1; i < oInput.length; i++) {
            oInput[i].onchange = function () {
                isCheckAll()
                tabshow();
            }
        }
        var listselect = function () {
            if (oInput.eq(0).is(':checked')) {
                $('.document_list li').addClass('active');
                $('.document_list li .unload_none').show();
            } else {
                $('.document_list li').removeClass('active');
                $('.document_list li .unload_none').hide();
            }

        }
        var tabshow = function () {
            if (oInput.is(':checked')) {
                $('.IsCheckAll .skydrive_size,.IsCheckAll .datea').hide();
                $('.IsCheckAll .bars').show();
            } else {
                $('.IsCheckAll .skydrive_size,.IsCheckAll .datea').show();
                $('.IsCheckAll .bars').hide();
            }
        }
    }
    function CheckAllb(oInput) {
        var isCheckAll = function () {
            for (var i = 1, n = 0; i < oInput.length; i++) {
                oInput[i].checked && n++
            }
            oInput[0].checked = n == oInput.length - 1;
        };
        //全选
        oInput[0].onchange = function () {
            for (var i = 1; i < oInput.length; i++) {
                oInput[i].checked = this.checked;
                tabshow();
                listselect();
            }
            isCheckAll()
        };

        //根据复选个数更新全选框状态
        for (var i = 1; i < oInput.length; i++) {
            oInput[i].onchange = function () {
                isCheckAll();
                tabshow();
            }
        }
        var listselect = function () {
            if (oInput.eq(0).is(':checked')) {
                $('.skydrive_grid_list li').addClass('active');
                $('.skydrive_grid_list li .unload_none,.skydrive_grid_list li .checkbox').show();
            } else {
                $('.skydrive_grid_list li').removeClass('active');
                $('.skydrive_grid_list li .unload_none,.skydrive_grid_list li .checkbox').hide();
            }

        }
        var tabshow = function () {
            if (oInput.is(':checked')) {
                $('.IsCheckAlla .bars').show();
            } else {
                $('.IsCheckAlla .bars').hide();
            }
        }
    }
    function hoverShow(hoverObj, showObj) {
        showObj.find('.arrow-down').click(function () {
            $(this).find('.arrow_downwrap').show();
        });
        hoverObj.find('input[type=checkbox]').click(function () {
            if ($(this).is(':checked')) {
                $(this).parents('li').addClass('active');
            } else {
                $(this).parents('li').removeClass('active');
            }
        });
        hoverObj.hover(function () {
            $(this).find(showObj).show();
        }, function () {
            $(this).find(showObj).hide();
            showObj.find('.arrow_downwrap').hide();
            if ($(this).find('input[type=checkbox]').is(':checked')) {
                $(this).find(showObj).show();
            }
        })
    }

    function OnError(XMLHttpRequest, textStatus, errorThrown) {
        var htmlBq = '<li style="text-align:center">' + json.result.errMsg.toString() + '</li>';
        if (ShowType == "1") {
            $("#tb_MyResource").html(htmlBq);
        }
        else { $("#tb_MyResource1").html(htmlBq); }
        //$("#tb_MyResource").html('无内容');
    }
    //绑定文件类型
    function GetFileType() {
        $.ajax({
            type: "Post",
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            data: { "PageName": "ResourceManage/PublicResoure.ashx", Func: "ResourceType" },
            dataType: "json",
            success: function (returnVal) {

                //var PagedData = $.parseJSON(returnVal.result.retData);

                $(returnVal.result.retData).each(function () {
                    var result = "<li><a href='#' onclick=\"serchType('" + this.ID + "',this)\">" + this.Name + "</a>";
                    $("#ResourceType").append(result);
                });
            },
            error: function (errMsg) {
                layer.msg('数据加载失败！');
            }
        });
    }
    function CheckAll(flag) {
        ////var flag = obj.checked;//判断全选按钮的状态 
        //$("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
        //    this.checked = flag;//选中或者取消选中 
        //});
        //var checked = flag.checked;
        //if(checked){
        //    $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
        //           this.checked = true;//选中或者取消选中 
        //    });
        //} else {
        //    $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
        //        this.checked = false;//选中或者取消选中 
        //    });
        //}
    }
    //面包屑
    function BindNav() {
        var Pid = $("#Pid").val();
        var CreateUID = $("#HUserIdCard").val();
        $("#Nav").html("<a href=\"#\" onclick=\"FolderClick(0, '\DriveFolder', 0,1)\">全部</a>");
        $.ajax({
            type: "Post",
            url: "/Common.ashx",//random" + Math.random(),//方法所在页面和方法名
            data: { "PageName": "Synergy/SkyDrivePersonal.ashx", Func: "BindNav", Pid: Pid, CreateUID: CreateUID },
            dataType: "json",
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    $(json.result.retData).each(function () {
                        var result = "<span>></span><a href=\"#\" onclick=\"FolderClick(" + this.ID + ",'" + this.FileUrl + "', " + this.code + "," + this.IsFolder + ")\">" + this.Name + "</a>";
                        $("#Nav").append(result);
                    });
                }
            },
            error: function (errMsg) {
                layer.msg('数据加载失败！');
            }
        });
    }
    //子复选框的事件  
    function setSelectAll() {
        //当没有选中某个子复选框时，SelectAll取消选中 
        //var checkAll = $("#checkAll");
        //if (!$("input[type=checkbox][name=Check_box]").is(":checked"))
        //{
        //    checkAll.checked = false;
        //}
        //if (!$("#subcheck").is(":checked")) {
        //    checkAll.checked = false;//.attr("checked", false);
        //}
        //var chsub = $("input[type='checkbox'][id='subcheck']").length; //获取subcheck的个数  
        //var checkedsub = $("input[type='checkbox'][id='subcheck']:checked").length; //获取选中的subcheck的个数  
        //if (checkedsub >0) {
        //    $("#checkAll").attr("checked", true);
        //}
    }
    function Down(id) {
        var ids = "";
        if (id != "") {
            ids = id;
        }
        else {
            $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
                if (this.checked) {
                    ids += this.value + ",";
                }
            });
        }
        if (ids != "") {

            $.ajax({
                url: "/Synergy/SkyDrivePersonal.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: { "PageName": "Synergy/SkyDrivePersonal.ashx", "Func": "Down", DownID: ids },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        window.open(json.result.retData);
                    }
                    else { layer.msg('下载失败！'); }
                },
                error: function (errMsg) {
                    layer.msg('下载失败！');
                }
            });
        }
        else {
            layer.msg("请选择要下载的文件");
        }
    }


    function Move(id) {
        var ids = "";
        var pid = $("#Pid").val();
        var Pcode = $("#code").val();
        if (id != "") {
            ids = id;
        }
        else {
            var ids = "";
            $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
                if (this.checked) {
                    ids += this.value + ",";
                }
            });
        }
        if (ids == "") {
            layer.msg("请选择要移动的文件");
        }
        else {
            OpenIFrameWindow('文件移动', '/Synergy/MyDrive.aspx?Pid=' + pid + "&code=" + Pcode + "&id=" + id + "&random=" + Math.random(), '450px', '300px;');
        }
    }
    function MoveMore(url, pid, code, id) {
        var ids = "";

        if (id != "") {
            ids = id;
        }
        else {
            var ids = "";
            $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
                if (this.checked) {
                    ids += this.value + ",";
                }
            });
        }
        $.ajax({
            type: "Post",
            url: "/Synergy/SkyDrivePersonal.ashx",
            data: {
                "PageName": "Synergy/SkyDrivePersonal.ashx",
                Func: "MoveTo", "MoveIDs": ids, "Url": url,
                "pid": pid, "code": code, CreateUID: $("#HUserIdCard").val()
            },
            dataType: "json",
            success: function (json) {
                if (json.result.errNum.toString() == "0") {
                    layer.msg("文件移动成功");
                    getData(1, 10);
                    CloseIFrameWindow();
                }
                else {
                    layer.msg(json.result.errMsg);
                }
            },
            error: function (errMsg) {
                layer.msg('操作失败！');
            }
        });

    }
    function Del(id, name) {
        var len = 0;
        var ids = "";
        if (id != "") {
            ids = id;
        }
        else {
            $("input[type=checkbox][name=Check_box]").each(function () {//查找每一个name为cb_sub的checkbox 
                if (this.checked) {
                    ids += this.value + ",";// $(this).attr('value') + ",";
                    len++;
                }
            });
        }
        if (ids != "") {
            if (name == "") {
                name = "这" + len + "个文件/文件夹"
            }
            if (confirm("确定要删除'" + name + "'吗？")) {
                $.ajax({
                    url: "/Synergy/SkyDrivePersonal.ashx",//random" + Math.random(),//方法所在页面和方法名
                    type: "post",
                    async: false,
                    dataType: "json",
                    data: { "PageName": "Synergy/SkyDrivePersonal.ashx", "Func": "Del", DelID: ids },
                    success: function (json) {
                        if (json.result.errNum.toString() == "0") {
                            layer.msg("删除成功");
                            getData(1, 10);
                        }
                        else { layer.msg('删除失败！'); }
                    },
                    error: function (errMsg) {
                        layer.msg(errMsg);
                    }
                });
                $('.IsCheckAll .bars').hide();
                $('.IsCheckAlla .bars').hide();

            }
        }
        else { layer.msg("请选择要删除的文件"); }
    }


    function getO(id) {
        if (typeof (id) == "string")
            return document.getElementById(id);
    }
    //删除一行（table）
    function DelRow() {
        $('ul#tb_MyResource li:eq(0)').remove();
    }
    //文件夹点击
    function FolderClick(id, FoldUrl, code, IsFolder) {
        if (IsFolder == 1) {

            $("#Pid").val(id);
            $("#HFoldUrl").val(FoldUrl);
            $("#code").val(code)
            var ShowType = $("#ShowType").val();
            if (ShowType == "1") {
                getData(1, 10);
            }
            else {
                getData(1, 16);
            }
            if (id != 0) {
                BindNav();
            }
            else {
                $("#Nav").html("<a href=\"#\" onclick=\"FolderClick(0, '\DriveFolder', 0,1)\">全部</a>");
            }
        }
        else {
            var FileExt = getFileName(FoldUrl);
            if (FileExt == "ppt" || FileExt == "pptx" || FileExt == "doc" || FileExt == "docx" || FileExt == "xls" || FileExt == "xlsx" || FileExt == "pdf") {
                $.ajax({
                    url: "/Synergy/SkyDrivePersonal.ashx",
                    type: "post",
                    async: false,
                    dataType: "text",
                    data: {
                        "Func": "Wopi_Proxy", filepath: FoldUrl
                    },
                    success: function (result) {
                        window.open(result);
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        layer.msg("资源不存在！");
                    }
                });
            }
            else {
                DownLoad(FoldUrl);
            }
        }
    }

    function getFileName(o) {
        //通过第一种方式获取文件名
        var pos = o.lastIndexOf(".");
        //查找最后一个\的位置
        return o.substring(pos + 1);
    }
    function DownLoad(FoldUrl) {
        $.ajax({
            url: "/OnlineLearning/DownLoadHandler.ashx",
            type: "post",
            async: false,
            dataType: "text",
            data: {
                filepath: FoldUrl
            },
            success: function (result) {

                if (result == "-1") {
                    layer.msg('文件不存在!');
                    return;
                }
                else {
                    window.open("/OnlineLearning/DownLoadHandler.ashx?filepath=" + FoldUrl + "&UserIdCard=" + $("#HUserIdCard").val() + "&time=" + new Date());
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                layer.msg("资源不存在！");
            }
        });
        $('.IsCheckAll .bars').hide();
        $('.IsCheckAlla .bars').hide();

    }
    //按类型搜索
    function serchType(Postfixs, em) {
        $(em).parent().addClass("on").siblings().removeClass("on");

        $("#Postfixs").val(Postfixs);
        if ($("#ShowType").val() == "1") {
            getData(1, 10);
        }
        else {
            getData(1, 16);
        }
    }
    //按时间搜索
    function SerchTime(STime, em) {
        $(em).parent().addClass("on").siblings().removeClass("on");

        $("#TimeQuery").val(STime);
        if ($("#ShowType").val() == "1") {
            getData(1, 10);
        }
        else {
            getData(1, 16);
        }
    }
    //新增文件夹
    function AddFold(em) {
        var FileName = $("#txt" + em + "").val();
        var pid = $("#Pid").val();
        var Pcode = $("#code").val();
        var CreateUID = $("#HUserIdCard").val();
        if (FileName.length > 0) {
            $.ajax({
                type: "Post",
                url: "/Synergy/SkyDrivePersonal.ashx",
                data: { Func: "AddFolder", "FileName": FileName, FoldUrl: $("#HFoldUrl").val(), Pid: pid, "code": Pcode, "CreateUID": CreateUID },
                dataType: "json",
                success: function (json) {

                    if (json.result.errNum.toString() == "0") {
                        layer.msg("文件夹添加成功");
                        getData(1, 10);
                    }
                    else { layer.msg('文件夹添加失败！'); }
                },
                error: function (errMsg) {
                    layer.msg('文件夹添加失败！');
                }
            });
        }
        else { layer.msg("请输入文件夹名称"); }
    }
    function upload() {
        var pid = $("#Pid").val();
        var Pcode = $("#code").val();
        var FoldUrl = $("#HFoldUrl").val();
        var CreateUID = $("#HUserIdCard").val();

        OpenIFrameWindow('文件上传', '/Synergy/DriveUpload.aspx?FoldUrl=' + FoldUrl + "&Pid=" + pid + "&code=" + Pcode + "&CreateUID=" + CreateUID, '400px', '300px');
    }
    //格式化时间
    Date.prototype.Format = function (fmt) { //author: meizz 
        var o = {
            "M+": this.getMonth() + 1, //月份 
            "d+": this.getDate(), //日 
            "h+": this.getHours(), //小时 
            "m+": this.getMinutes(), //分 
            "s+": this.getSeconds(), //秒 
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
            "S": this.getMilliseconds() //毫秒 
        };
        if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        return fmt;
    }

    //修改文件名称
    function EditName(em, id, oldname, FileUrl) {
        var name = $("#txt" + id).val();
        if (name == oldname) {
            layer.msg("请输入新的文件名");
        }
        else {
            $.ajax({
                url: "/Synergy/SkyDrivePersonal.ashx",//random" + Math.random(),//方法所在页面和方法名
                type: "post",
                async: false,
                dataType: "json",
                data: { "Func": "reName", ID: id, "NewName": name, "FileUrl": FileUrl, oldname: oldname },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        layer.msg("重命名成功");
                        getData(1, 10);
                    }
                    else { layer.msg('重命名失败！'); }
                },
                error: function (errMsg) {
                    layer.msg('重命名失败！');
                    $(em).parents("li").find(".docu_name").html(oldname);
                }
            });
        }
    }
    //取消修改文件名称
    function EditNameQ(em, name) {
        $(em).parents("li").find(".docu_name").html(name);
    }
    function cl(em, id, title, FileUrl) {
        var CurrentHtml = $(em).parents("li").find(".docu_name").html();
        var len = $(em).parents("ul").find('input:text').length;
        if (CurrentHtml.indexOf("text") > 0) {
            layer.msg("当前文件已处于编辑状态");
        }
        else {
            if (len > 0) {
                layer.msg("当前只能同时编辑一个文件");
            }
            else {
                var now = new Date();
                var nowStr = now.Format("yyyy-MM-dd");

                var name = $(em).parents("li").find(".docu_name").html();
                if (name = title) {
                    var v = "<input type='text' value='" + title + "' style='float:left;line-height:10px;margin-top:5px;width:100px;' id=\"txt" + id +
            "\"/> <i class=\"icon tishi true_t icon-ok\" style=\"margin-left: 6px;margin-top:6px; color: #87c352; float: left;cursor:pointer;\" onclick=\"EditName(this,'" + id + "','" + name + "','" + FileUrl + "')\"></i> <i class=\"icon icon-remove tishi fault_t\" style=\"margin-left: 6px;margin-top:6px;color: #ff6d72; float: left;cursor:pointer;\" onclick=\"EditNameQ(this,'" + name +
            "')\"></i>";
                    $(em).parents("li").find(".docu_name").html(v);
                    $(em).parents("li").find(".docu_name").removeAttr("onclick");
                }
            }
        }
    }

    //追加一行（新建文件夹）

    function appendAfterRow(tableID, RowIndex) {
        var txt1 = document.getElementById("txt1");
        if (txt1 != undefined) {
            txt1.onfocus();
        }//<img src='" + $("#HIcons").val() + "/ico-file.png' style='float:left;'>
        else {
            var li = document.createElement("li");
            li.className = "clearfix";
            var ul = document.getElementById("tb_MyResource");
            var now = new Date();
            var nowStr = now.Format("yyyy-MM-dd");
            //newRefRow.insertCell(3).innerHTML = nowStr;
            li.innerHTML = "<div class='checkbox fl'><input type='checkbox'/></div><div class='docu_messages fl'>" +
                "<p class=\"docu_title\"><span class=\"h-ico ico-file-min\"></span><a class=\"docu_name\"><input type='text' value='新建文件夹' style='float:left;line-height:10px;margin-top:5px;width:100px;' id=\"txt" + RowIndex +
                "\"/> <i class=\"icon tishi true_t icon-ok\" style=\"margin-top:6px ;margin-left:6px; color: #87c352; float: left;cursor:pointer;\" onclick=\"AddFold('" + RowIndex
                + "')\"></i> <i class=\"icon tishi fault_t icon-remove\" style=\"margin-top:6px ;margin-left:6px; color: #ff6d72; float: left;cursor:pointer;\" onclick=\"DelRow()\"></i></a></p></div><div class=\"skydrive_size fl\"><span>--</span></div><div class=\"date fr\"><span>" + nowStr + "</span></div>";
            var nodeli1 = ul.getElementsByTagName('li')[0];//获取ul下第3个节点——秋天
            ul.insertBefore(li, nodeli1);

        }
    }
</script>
</html>
