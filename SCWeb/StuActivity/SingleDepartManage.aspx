<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SingleDepartManage.aspx.cs" Inherits="SCWeb.StuActivity.SingleDepartManage" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>管理部门</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css" />
    <link rel="stylesheet" href="/css/style.css" />
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <script src="/Scripts/laypage/laypage.js"></script>   
    <link href="/Scripts/Stu_upload/webuploader.css" rel="stylesheet" />
    <script src="/Scripts/Stu_upload/webuploader.min.js"></script>
    <%--部门--%>
    <script id="div_DepartDetail" type="text/x-jquery-tmpl">
        <div class="society clearfix fl">
            <div class="society_img fl">
                <img src="${PicURL}" onerror="javascript:this.src='/images/assodefault.jpg'" alt="">
            </div>
            <div class="society_right fl">
                <h1 class="clearfix soeicty_1">
                    <div class="soeicty_name">${Name}</div>
                    <div class="soeicty_wrap ml10">(<span>成员${MemCount}</span>|<span>总贴量${NewCount}</span>)<i class="iconfont icon-heart heart"></i></div>
                </h1>               
                <h2 class="soeicty_head">部长：${CreateName}</h2>
            </div>
        </div>
        <div id="div_btnReturn" class="society_manage fr"><i class="iconfont icon-settings"></i>返回部门</div>
    </script>
    <%--所有部门--%>
    <script id="tr_DepartSub" type="text/x-jquery-tmpl">
        <tr>
            <td style="width: 40px;">
                <input type="checkbox" name="ck_depart_sub" value="${Id}" />
            </td>
            <td>
                <div class="name clearfix">
                    <img src="${PicURL}" onerror="javascript:this.src='/images/member_img.png'" class="fl" />
                    <span class="fl ml10">${Name}</span>
                </div>
            </td>
            <td>
                <div class="name clearfix">
                    <img src="${AbsHeadPic}" onerror="javascript:this.src='/images/member_img.png'" class="fl" />
                    <span class="fl ml10">${CreateName}</span>
                    {{if Sex==0}}<i class="iconfont icon-man fl ml10 colorblue"></i>{{else}}<i class="iconfont icon-woman fl ml10 colorred"></i>{{/if}}
                </div>
            </td>
            <td>${DateTimeConvert(CreateTime)}</td>
            <td>${Introduce}</td>            
        </tr>
    </script>
    <%--部门成员--%>
    <script id="tr_MemberSub" type="text/x-jquery-tmpl">
        <tr>
            <td style="width: 40px;">
                <input type="checkbox" name="ck_member_sub" value="${MemberNo}" />
            </td>
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
            <td>${DateTimeConvert(CreateTime,'yyyy-MM-dd HH:mm:ss')}</td>
            <%--<td>2016-11-5</td>--%>
        </tr>
    </script>
    <%--部门相册--%>
    <script id="tr_AlbumSub" type="text/x-jquery-tmpl">
        <tr>
            <td style="width: 40px;">
                <input type="checkbox" name="ck_album_sub" value="${Id}" />
            </td>
            <td>${Name}</td>
            <td>${CreateName}</td>
            <td>${DateTimeConvert(CreateTime,'yyyy-MM-dd HH:mm:ss')}</td>
        </tr>
    </script>
    <%--部门申请--%>
    <script id="tr_ApplySub" type="text/x-jquery-tmpl">
        <tr>
            <td style="width: 40px;">
                <input type="checkbox" name="name" value=" " />
            </td>
            <td>
                <div class="name clearfix">
                    <img src="${AbsHeadPic}" onerror="javascript:this.src='/images/member_img.png'" class="fl" />
                    <span class="fl ml10">${CreateName}</span>
                     {{if Sex==0}}<i class="iconfont icon-man fl ml10 colorblue"></i>{{else}}<i class="iconfont icon-woman fl ml10 colorred"></i>{{/if}}
                </div>
            </td>
            <td>${DepartName}</td>
            <td>${Type==1?'入部申请':'退部申请'}</td>
            <td>${GradeName}</td>
            <td>${OrgName}</td>
            <td>${DateTimeConvert(CreateTime,'yyyy-MM-dd HH:mm:ss')}</td>
            <td>
                <div class="operate" onclick="OpenIFrameWindow('审批成员','/StuActivity/RecruitApply_Audit.aspx?itemid=${Id}','480px','280px')">
                    <i class="iconfont icon-check"></i>
                    <span class="operate_none">审核
                    </span>
                </div>
            </td>
        </tr>
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="header"></div>
            <div id="main">
                <div id="div_BackPicURL" class="mycorporation pr">
                    <div class="center clearfix" id="div_Depart"></div>
                </div>
                <div class="center pt10 clearfix">
                    <div class="pl20 pr20" style="background: #fff;">
                        <div class="clearfix reply_tools pt10">
                            <div class="reply_left clearfix">
                                <ul id="ul_DepartTab" class="clearfix">
                                    <li class="selected">
                                        <a href="javascript:;">部门信息</a>
                                    </li>
                                    <li>
                                        <a id="li_DepartManage" href="javascript:;" style="display:none;">部门管理</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">成员管理</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">相册管理</a>
                                    </li>
                                    <li>
                                        <a href="javascript:;">批准成员</a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="tab_wraps">
                            <div class="tab_wrap">
                                <div class="row">
                                    <label for="" class="row_label fl">名称：</label>
                                    <div class="row_content">
                                        <input type="text" class="text" id="txt_Name" />
                                    </div>
                                </div>
                                <div class="row">
                                    <label for="" class="row_label fl">描述：</label>
                                    <div class="row_content">
                                        <textarea id="area_Introduce" name="area_Introduce" placeholder="描述" style="width: 70%; height: 80px;"></textarea>
                                    </div>
                                </div>                                                               
                                <div class="row">
                                    <label for="" class="row_label fl">图标：</label>
                                    <div class="row_content clearfix">
                                        <div class="upload fl">
                                            <div id="filePicker">部门图片</div>                                            
                                        </div>

                                        <span class="fl input_tips">你可以上传一个 160*160 大小的图片作为部门图标。
                                        </span>
                                        <div class="clear"></div>
                                        <img id="img_PicURL" onerror="javascript:this.src='/images/assodefault.jpg'" alt="" style="margin-top: 10px;" />
                                    </div>
                                </div>
                                <div class="row">
                                    <label for="" class="row_label fl"></label>
                                    <div class="row_content">
                                        <input type="button" name="name" onclick="EditDepartInfo();" value="修改部门" class="btn bgblue" />
                                    </div>
                                </div>
                            </div>
                            <div class="tab_wrap none">
                                <div class="clearfix tools">
                                    <div class="fl" style="line-height: 40px; color: #333333; font-size: 18px;">
                                        所有部门列表
                                    </div>
                                    <div class="fr tools_right">
                                        <a href="javascript:;" class="bggreen" onclick="PagePopWindow(2);">添加部门</a>
                                        <a href="javascript:;" class="bgred" onclick="PagePopWindow(3);">编辑部门</a>                                       
                                        <div class="search_wrap pr ml20 fl" style="width: 170px;">
                                            <input type="text" id="txtDepartName" class="search" placeholder="搜本部门名称" />
                                            <i class="iconfont icon-search" style="top: 0px;" onclick="SearchDepart();"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="table_wraps">
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th style="width: 40px;">
                                                    <input type="checkbox" id="ck_depart_all" name="ck_depart_all" onclick="CheckAll(this, 'ck_depart_sub');" value=" " />
                                                </th>
                                                <th>名称</th>
                                                <th>部长</th>
                                                <th>创建时间</th>
                                                <th>描述</th>                                                
                                            </tr>
                                        </thead>
                                        <tbody id="tb_Depart"></tbody>
                                    </table>
                                </div>
                                <!--分页-->
                                <div class="page" id="pageBar_Depart"></div>
                            </div>
                            <div class="tab_wrap none">
                                <div class="clearfix tools">
                                    <div class="fl" style="line-height: 40px; color: #333333; font-size: 18px;">
                                        所有成员列表
                                    </div>
                                    <div class="fr tools_right">
                                        <a href="javascript:;" class="bggreen" onclick="PagePopWindow(1);">添加成员</a>
                                        <a href="javascript:;" class="bgred" onclick="DelDepartMember();">删除成员</a>
                                        <a href="javascript:;" class="bgblue" onclick="Export_DepartMember();">全部导出</a>
                                        <%-- <select class="fl ml10 select" style="height:40px;min-width:100px;">
                                        <option value="value">分配职位</option>
                                    </select>--%>
                                        <div class="search_wrap pr ml20 fl" style="width: 170px;">
                                            <input type="text" id="txtUserName" class="search" placeholder="搜本部门成员" />
                                            <i class="iconfont icon-search" style="top: 0px;" onclick="SearchCondition();"></i>
                                        </div>
                                    </div>
                                </div>
                                <div class="table_wraps">
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th style="width: 40px;">
                                                    <input type="checkbox" id="ck_member_all" name="ck_member_all" onclick="CheckAll(this, 'ck_member_sub');" value=" " />
                                                </th>
                                                <th>姓名</th>
                                                <th>部门</th>
                                                <th>职位</th>
                                                <th>年级</th>
                                                <th>班级</th>
                                                <th>入社时间</th>
                                                <%--<th>最后发言时间</th>--%>
                                            </tr>
                                        </thead>
                                        <tbody id="tb_Member"></tbody>
                                    </table>
                                </div>
                                <!--分页-->
                                <div class="page" id="pageBar_Member"></div>
                            </div>
                            <div class="tab_wrap none">
                                <div class="clearfix tools">
                                    <div class="fl" style="line-height: 40px; color: #333333; font-size: 18px;">
                                        所有相册列表
                                    </div>
                                    <div class="fr tools_right">
                                        <a href="javascript:;" class="bggreen" onclick="PagePopWindow(0);">创建相册</a>
                                        <a href="javascript:;" class="bgred" onclick="DelComAlbum();">删除相册</a>
                                    </div>
                                </div>
                                <div class="table_wraps">
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th style="width: 40px;">
                                                    <input type="checkbox" id="ck_album_all" name="ck_album_all" onclick="CheckAll(this, 'ck_album_sub');" value=" " />
                                                </th>
                                                <th>相册名称</th>
                                                <th>创建者</th>
                                                <th>创建时间</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tb_Album"></tbody>
                                    </table>
                                </div>
                                <!--分页-->
                                <div class="page" id="pageBar_Album"></div>
                            </div>
                            <div class="tab_wrap none">
                                <table class="table mt10">
                                    <thead>
                                        <tr>
                                            <th style="width: 40px;">
                                                <input type="checkbox" name="name" value=" " />
                                            </th>
                                            <th>姓名</th>
                                            <th>申请部门</th>
                                            <th>申请类型</th>
                                            <th>年级</th>
                                            <th>班级</th>
                                            <th>申请时间</th>
                                            <th>操作</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tb_Apply"></tbody>
                                </table>
                                <!--分页-->
                                <div class="page" id="pageBar_Apply"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="footer"></div>
        </div>
    </form>
    <script>
        var UrlDate = new GetUrlDate();
        var navid = UrlDate.nav;
        $(function () {
            $('#header').load('/CommonPage/Header.aspx?t=' + new Date().getTime(), function (response, status, xhr) {
                if (status == "success" || status == "notmodified") {
                    SetNavSelected(navid);
                }
            });
            $('#footer').load('/CommonPage/footer.html');            
            WebUploader.create({
                pick: '#filePicker',
                formData: { Func: "Upload_DepartImg" },
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
               $("#img_PicURL").attr("src", json.result.retData);
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
            tableSlide();
            tab('.reply_left ul', '.tab_wraps');
            GetMyDepart(UrlDate.tab || 0);
        });
        //获取我的部门
        function GetMyDepart(tabindex) {
            tabindex = arguments[0] || 0;
            $('#ul_DepartTab li:eq(' + tabindex + ')').addClass('selected').siblings().removeClass('selected');
            $('.tab_wraps').children().eq(tabindex).show().siblings().hide();
            var params = {
                PageName: "/StuActivity/Acti_DepartHandler.ashx",
                Func: "GetDepartInfoDataPage",
                Id: UrlDate.itemid,
                LoginUID: "<%=UserInfo.UniqueNo%>",
                ispage: false,
                IsOnlyBase: "1",
                IsUnifiedInfo: "1"
            }
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: params,
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#div_Depart").html('');
                        var rtnObj = json.result.retData;
                        $("#div_DepartDetail").tmpl(rtnObj).appendTo("#div_Depart");
                        SetDisplayOrHide(rtnObj[0]);
                        LoadFunc(tabindex);
                        //部门信息切换
                        $('#ul_DepartTab li').click(function () {
                            $(this).addClass('selected').siblings().removeClass('selected');
                            LoadFunc($(this).index());
                        });
                    }
                    else {
                        $("#div_Depart").html('<div>暂无部门！</div>');
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
                $("#div_btnReturn").click(function () {
                    location.href = '/StuActivity/DepartDetail.aspx?itemid=' + UrlDate.itemid + '&nav=' + navid;//跳转部门详情页面                        
                });                
                $("#txt_Name").val(model.Name);
                $("#area_Introduce").val(model.Introduce);
                $("#img_PicURL").attr("src", model.PicURL);
            }
        }
        function PagePopWindow(type) {
            if (type == 0) { //创建相册
                OpenIFrameWindow('创建相册', '/CommonPage/EditComAlbum.aspx?type=1&itemid=0&relid=' + UrlDate.itemid, '480px', '400px');
            } else if (type == 1) { //添加成员
                OpenIFrameWindow('添加成员', '/StuActivity/AddDepartMember.aspx?relid=' + UrlDate.itemid + '&tab=1', '900px', '660px');
            }
        }
        function Export_DepartMember() {
            window.open('/CommonHandler/ImportOrExport.ashx?Func=Export_DepartMember&DepartId=' + UrlDate.itemid, "myIframe");
        }
        function LoadFunc(tabindex) {
            if (tabindex == 1) {
                GetAllDepartInfo(1,10);
            } else if (tabindex == 2) {
                GetDepartMemberData(1, 10);
            } else if (tabindex == 3) {
                GetComAlbumData(1, 10);
            } else if (tabindex == 4) {
                GetActiRecruitApplyData(1, 10);
            }
        }
        //编辑部门信息
        function EditDepartInfo() {
            var name = $("#txt_Name").val().trim();
            var introduce = $("#area_Introduce").val().trim();
            if (!name.length) { layer.msg("请填写部门名称！"); return; }
            if (!introduce.length) { layer.msg("请填写部门描述！"); return; }
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuActivity/Acti_DepartHandler.ashx",
                    Func: "EditDepartInfo",
                    ItemId: UrlDate.itemid,
                    Name: name,
                    Introduce: introduce,
                    PicURL: $("#img_PicURL").attr("src"),
                    LoginUID: "<%=UserInfo.UniqueNo%>"
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == -1) {
                        layer.msg("该部门名称已存在!");
                    }
                    else if (result.errNum == 0) {
                        layer.msg('修改部门成功!');
                        GetMyDepart(0);
                    } else {
                        layer.msg(result.errMsg);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.msg("操作失败！");
                }
            });
        }
        var txtDepartName = $("#txtDepartName").val().trim();
        function SearchDepart() {
            txtDepartName = $("#txtDepartName").val().trim();
            GetAllDepartInfo(1, 10);
        }
        //加载所有部门
        function GetAllDepartInfo(startIndex, pageSize) {
            var $tbPar = $("#tb_Depart");
            var $trSub = $("#tr_DepartSub");
            var $pageBar = $("#pageBar_Depart");
            $("input[type=checkbox][name='ck_depart_all']")[0].checked = false;
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/StuActivity/Acti_DepartHandler.ashx",
                    Func: "GetDepartInfoDataPage",
                    DepartId: UrlDate.itemid,
                    Name: txtDepartName,
                    PageIndex: startIndex,
                    PageSize: pageSize,
                    IsUnifiedInfo: "1"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $tbPar.html('');
                        var rtnObj = json.result.retData;
                        $trSub.tmpl(rtnObj.PagedData).appendTo("#tb_Depart");
                        $pageBar.show();
                        laypage({
                            cont: $pageBar, //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                            pages: json.result.retData.PageCount, //通过后台拿到的总页数
                            curr: json.result.retData.PageIndex || 1, //当前页
                            skip: false, //是否开启跳页
                            skin: '#74c3f4',
                            jump: function (obj, first) { //触发分页后的回调
                                if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                    GetAllDepartInfo(obj.curr, pageSize)
                                }
                            }
                        });
                        $("input[type=checkbox][name=ck_depart_sub]").click(function () {
                            CheckSub(this, 'ck_depart_all');
                        });
                    }
                    else {
                        $tbPar.html('<tr><td colspan="5">暂无部门！</td></tr>');
                        $pageBar.hide();
                    }
                },
                error: function (errMsg) {
                    $tbPar.html('<tr><td colspan="5">暂无部门！</td></tr>');
                }
            });
        }
        var txtUserName = $("#txtUserName").val().trim();
        function SearchCondition() {
            txtUserName = $("#txtUserName").val().trim();
            GetDepartMemberData(1, 10);
        }
        //加载部门成员
        function GetDepartMemberData(startIndex, pageSize) {
            var $tbPar = $("#tb_Member");
            var $trSub = $("#tr_MemberSub");
            var $pageBar = $("#pageBar_Member");
            $("input[type=checkbox][name='ck_member_all']")[0].checked = false;
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/StuActivity/Acti_DepartHandler.ashx",
                    Func: "GetDepartMemberDataPage",
                    DepartId: UrlDate.itemid,
                    Name: txtUserName,
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
                        $("input[type=checkbox][name=ck_member_sub]").click(function () {
                            CheckSub(this, 'ck_member_all');
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
        //删除部门成员
        function DelDepartMember() {
            var checkedtr = $("input[type='checkbox'][name='ck_member_sub']:checked");
            if (checkedtr.length == 0) { layer.msg('请选择要删除的行！'); return; }
            var idArray = [];
            $(checkedtr).each(function (i, n) {
                idArray.push(n.value);
            });
            layer.confirm('确定要删除勾选的部门成员吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                $.ajax({
                    url: "/Common.ashx",
                    type: "post",
                    dataType: "json",
                    data: {
                        PageName: "/StuActivity/Acti_DepartHandler.ashx",
                        Func: "DelDepartMember",
                        DepartId: UrlDate.itemid,
                        MemberNos: idArray.join(","),
                        LoginUID: "<%=UserInfo.UniqueNo%>"
                    },
                    success: function (json) {
                        if (json.result.errNum == 0) {
                            layer.msg('删除成员成功！');
                            GetMyDepart(1);
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
        //加载部门相册
        function GetComAlbumData(startIndex, pageSize) {
            var $tbPar = $("#tb_Album");
            var $trSub = $("#tr_AlbumSub");
            var $pageBar = $("#pageBar_Album");
            $("input[type=checkbox][name='ck_album_all']")[0].checked = false;
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
                    RelationId: UrlDate.itemid,
                    PageIndex: startIndex,
                    PageSize: pageSize,
                    IsUnifiedInfo: "1"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $tbPar.html('');
                        var rtnObj = json.result.retData;
                        $trSub.tmpl(rtnObj.PagedData).appendTo("#tb_Album");
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
                        $("input[type=checkbox][name=ck_album_sub]").click(function () {
                            CheckSub(this, 'ck_album_all');
                        });
                    }
                    else {
                        $tbPar.html('<tr><td colspan="4">暂无相册！</td></tr>');
                        $pageBar.hide();
                    }
                },
                error: function (errMsg) {
                    $tbPar.html('<tr><td colspan="4">暂无相册！</td></tr>');
                }
            });
        }
        //删除部门相册
        function DelComAlbum() {
            var checkedtr = $("input[type='checkbox'][name='ck_album_sub']:checked");
            if (checkedtr.length == 0) { layer.msg('请选择要删除的行！'); return; }
            var idArray = [];
            $(checkedtr).each(function (i, n) {
                idArray.push(n.value);
            });
            layer.confirm('确定要删除勾选的相册吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                $.ajax({
                    url: "/Common.ashx",
                    type: "post",
                    dataType: "json",
                    data: {
                        PageName: "/CommonHandler/Com_AlbumHandler.ashx",
                        Func: "DelComAlbum",
                        ItemIdStr: idArray.join(","),
                    },
                    success: function (json) {
                        if (json.result.errNum == 0) {
                            layer.msg('删除相册成功！');
                            GetComAlbumData(1, 10);
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
        //加载部门申请
        function GetActiRecruitApplyData(startIndex, pageSize) {
            var $tbPar = $("#tb_Apply");
            var $trSub = $("#tr_ApplySub");
            var $pageBar = $("#pageBar_Apply");
            //初始化序号 
            pageNum = (startIndex - 1) * pageSize + 1;
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                dataType: "json",
                data: {
                    PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                    Func: "GetActiRecruitApplyDataPage",
                    DepartId: UrlDate.itemid,
                    ExamStatus: 1,
                    PageIndex: startIndex,
                    PageSize: pageSize,
                    IsOnlyBase: "1",
                    IsUnifiedInfo: "1"
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $tbPar.html('');
                        var rtnObj = json.result.retData;
                        $trSub.tmpl(rtnObj.PagedData).appendTo("#tb_Apply");
                        $pageBar.show();
                        laypage({
                            cont: $pageBar, //容器。值支持id名、原生dom对象，jquery对象。【如该容器为】：<div id="page1"></div>
                            pages: json.result.retData.PageCount, //通过后台拿到的总页数
                            curr: json.result.retData.PageIndex || 1, //当前页
                            skip: false, //是否开启跳页
                            skin: '#74c3f4',
                            jump: function (obj, first) { //触发分页后的回调
                                if (!first) { //点击跳页触发函数自身，并传递当前页：obj.curr
                                    GetActiRecruitApplyData(obj.curr, pageSize)
                                }
                            }
                        });
                    }
                    else {
                        $tbPar.html('<tr><td colspan="7">暂无申请！</td></tr>');
                        $pageBar.hide();
                    }
                },
                error: function (errMsg) {
                    $tbPar.html('<tr><td colspan="7">暂无申请！</td></tr>');
                }
            });
        }
    </script>
</body>
</html>
