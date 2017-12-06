<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditActiActivity.aspx.cs" Inherits="SCWeb.StuActivity.EditActiActivity" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta charset="UTF-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta name="format-detection" content="telephone=no" />
    <title>发布活动</title>
    <link rel="stylesheet" href="/css/reset.css" />
    <link rel="stylesheet" href="/css/iconfont.css"/>
    <link rel="stylesheet" href="/css/style.css"/>
    <script src="/Scripts/jquery-1.8.3.min.js"></script>
    <script src="/Scripts/My97DatePicker/WdatePicker.js"></script>
    <link href="/Scripts/Stu_upload/webuploader.css" rel="stylesheet" />
    <script src="/Scripts/Stu_upload/webuploader.min.js"></script>
    <script charset="utf-8" src="/Scripts/KindUeditor/kindeditor-min.js"></script>
    <script charset="utf-8" src="/Scripts/KindUeditor/plugins/code/prettify.js"></script>
    <script charset="utf-8" src="/Scripts/KindUeditor/lang/zh_CN.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <script src="/Scripts/Common.js"></script>
    <script src="/Scripts/jquery.tmpl.js"></script>
    <%--活动项目--%>
    <script id="div_ProjectSub" type="text/x-jquery-tmpl">
        <div class="fl edit_close">
            <input id="txt_Project_${Id}" type="text" readonly="readonly" class="text" value="${Name}" onblur="SaveActiProject(${Id});"/>
            <i class="iconfont icon-bianji edit" onclick="EditActiProject(${Id});"></i>
            <i class="iconfont icon-close close1" onclick="DelActiProject(${Id},this);"></i>
        </div>
    </script>
</head>
<body style="background:#fff;">
    <div style="padding:20px 20px 50px 20px;">
        <div class="fl" style="width:70%;">
            <div class="row clearfix">
                <label for="" class="row_label fl">活动名称：</label>
                <div class="row_content">
                    <input type="text" id="txt_Name" class="text" placeholder="请输入活动名称"/>
                </div>
            </div>
            <div class="row clearfix">
                <label for="" class="row_label fl">活动类型：</label>
                <div class="row_content">
                    <select class="select" id="sel_Type" style="width:210px;" onchange="ActTypeChange(this);">
                        <option value="0">招新</option>
                        <option value="1">其他</option>
                    </select>
                </div>
            </div>
            <div class="row clearfix">
                <label for="" class="row_label fl">活动范围：</label>
                <div id="div_Grades" class="row_content"></div>
            </div>            
            <div class="row clearfix">
                <label for="" class="row_label fl">活动地址：</label>
                <div class="row_content">
                    <input type="text" id="txt_Address" class="text" placeholder="请输入活动地址"/>
                </div>
            </div>
            <div class="row clearfix">
                <label for="" class="row_label fl">报名时间：</label>
                <div class="row_content">
                    <input type="text" id="txt_EntStartTime" class="Wdate text" readonly="readonly" onclick="javascript: WdatePicker({ maxDate: '#F{$dp.$D(\'txt_EntEndTime\')}' });"/>
                    <span>~</span>
                    <input type="text" id="txt_EntEndTime" class="Wdate text" readonly="readonly" onclick="    javascript: WdatePicker({ minDate: '#F{$dp.$D(\'txt_EntStartTime\')||\'%y-%M-%d\'}', maxDate: '#F{$dp.$D(\'txt_ActStartTime\')}' });"/>
                </div>
            </div>
            <div class="row clearfix">
                <label for="" class="row_label fl">活动时间：</label>
                <div class="row_content">
                    <input type="text" id="txt_ActStartTime" class="Wdate text" readonly="readonly" onclick="javascript: WdatePicker({minDate: '#F{$dp.$D(\'txt_EntStartTime\')||\'%y-%M-%d\'}', maxDate: '#F{$dp.$D(\'txt_ActEndTime\')}' });"/>
                    <span>~</span>
                    <input type="text" id="txt_ActEndTime" class="Wdate text" readonly="readonly" onclick="    javascript: WdatePicker({ minDate: '#F{$dp.$D(\'txt_ActStartTime\')||\'%y-%M-%d\'}' });"/>
                </div>
            </div>
        </div>
        <div class="fl" style="width:30%;">
            <div class="see_img" style="height:170px;width:99%;">
                <img id="img_ActivityImg" src="/images/defaultimg.jpg" onerror="javascript:this.src='/images/defaultimg.jpg'"  alt=""/>
                <div class="upload" style="width:auto;">
                    <div id="filePicker">活动封面</div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
        <div class="row clearfix">
            <label for="" class="row_label fl">活动简介：</label>
            <div class="row_content">
                <textarea id="area_Introduction" name="area_Introduction" placeholder="请输入活动简介......" style="width: 100%; height: 200px;" class="editor_id"></textarea>
            </div>
        </div>
        <div id="div_RowProject" class="row clearfix" style="display:none;">
            <label for="" class="row_label fl">项目名称：</label>
            <div class="row_content">
                <div class="editor_id" style="padding: 10px; min-height: 200px;overflow:hidden;">
                    <input type="text" id="txt_Project" class="text fl" placeholder="" />
                    <a href="javascript:;" class="btn bgblue fl" onclick="AddActiProject();" style="padding: 9px 16px; margin-left: 10px;">确定</a>
                    <div style="clear:both;"></div>
                    <div id="div_Project" style="padding:10px 0px;"></div>
                </div>
            </div>
        </div>
         <div id="div_RowAwards" class="row clearfix" style="display:none;">
            <label for="" class="row_label fl">奖项说明：</label>
            <div class="row_content">
                <textarea id="area_Awards" name="area_Awards" placeholder="请输入奖项说明......" style="width: 100%; height: 200px;" class="editor_id"></textarea>
            </div>
        </div>
    </div>
    <div class="tools_bottom">
        <input type="button" class="keep" value="保存" onclick="SaveItem();"/>
        <input type="button" class="cancel" value="取消" onclick="javascript: parent.CloseIFrameWindow();" />
    </div>
    <script type="text/javascript">
        var UrlDate = new GetUrlDate();
        var curEditor;
        $(function () {
            KindEditor.ready(function (K) {
                curEditor = K.create('#area_Content', {
                    uploadJson: '/CommonHandler/Upload.ashx?Func=Upload_DeparActivityImg',
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
                        self.edit = edit = this; var strIndex = self.edit.html().indexOf("请输入活动描述..."); if (strIndex != -1) { self.edit.html(self.edit.html().replace("请输入活动描述...", "")); }
                    },
                    afterBlur: function () { this.sync(); self.edit = edit = this; if (self.edit.isEmpty()) { self.edit.html("请输入活动描述..."); } },   //关键  同步KindEditor的值到textarea文本框 
                    afterCreate: function () {
                        var self = this;
                        if (UrlDate.itemid == 0) { self.html("请输入活动描述..."); }
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
            WebUploader.create({
                pick: '#filePicker',
                formData: { Func: "Upload_DeparActivityImg" },
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
               $("#img_ActivityImg").attr("src", json.result.retData);
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
            var itemid = UrlDate.itemid;
            if (itemid != 0) {
                GetActiProjectData();
                GetItemById(itemid);
            } else {
                GetGradeData('');
            }
        });
        //绑定数据
        function GetItemById(itemid) {
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                    Func: "GetActiActivityById",
                    ItemId: itemid
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        var model = json.result.retData;
                        $("#txt_Name").val(model.Name);
                        $("#sel_Type").val(model.Type);
                        $("#txt_Address").val(model.Address);
                        $("#txt_EntStartTime").val(DateTimeConvert(model.EntStartTime, 'yyyy-MM-dd'));
                        $("#txt_EntEndTime").val(DateTimeConvert(model.EntEndTime, 'yyyy-MM-dd'));
                        $("#txt_ActStartTime").val(DateTimeConvert(model.ActStartTime, 'yyyy-MM-dd'));
                        $("#txt_ActEndTime").val(DateTimeConvert(model.ActEndTime, 'yyyy-MM-dd'));
                        $("#area_Introduction").val(model.Introduction);
                        $("#area_Awards").val(model.Awards);
                        $("#img_ActivityImg").attr("src", model.ActivityImg);
                        GetGradeData(model.Range);
                    }                    
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        //获取活动项目
        function GetActiProjectData() {
            $("#div_Project").html('');
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                    Func: "GetActiProjectDataPage",
                    ActivityId:UrlDate.itemid,
                    ispage: false
                },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        $("#div_ProjectSub").tmpl(json.result.retData).appendTo("#div_Project");
                    } else {
                        $("#div_Project").html('<div>暂无项目！</div>');
                    }
                },
                error: function (errMsg) {
                    $("#div_Project").html('<div>暂无项目！</div>');
                }
            });
        }
        //保存信息
        function SaveItem() {
            var name = $("#txt_Name").val().trim();
            var type = $("#sel_Type").val();
            var gsel_ck = $("input[type='checkbox'][name='ck_grade']:checked");
            if (gsel_ck.length == 0) { layer.msg('请勾选活动范围！'); return; }
            var graderray = [];
            $(gsel_ck).each(function (i, n) {
                graderray.push(n.value);
            });
            var sendExamStatus = graderray.length > 1 ? 2 :0;
            var address = $("#txt_Address").val().trim();
            var entStartTime = $("#txt_EntStartTime").val();
            var entEndTime = $("#txt_EntEndTime").val();
            var actStartTime = $("#txt_ActStartTime").val();
            var actEndTime = $("#txt_ActEndTime").val();
            var introduction = $("#area_Introduction").val().trim();
            var awards = $("#area_Awards").val().trim();
            if (!name.length) { layer.msg("请填写活动名称！"); return; }
            if (!address.length) { layer.msg("请填写活动地址！"); return; }
            if (!entStartTime) { layer.msg("请选择报名开始时间！"); return; }
            if (!entEndTime) { layer.msg("请选择报名结束时间！"); return; }
            if (!actStartTime) { layer.msg("请选择活动开始时间！"); return; }
            if (!actEndTime) { layer.msg("请选择活动结束时间！"); return; }
            if (!introduction.length || introduction == "请输入活动描述...") { layer.msg("请填写活动描述！"); return; }                   
            var projectStr = "";
            if (UrlDate.itemid == 0 && type != "0") {
                var proArray = [];
                $.each($("#div_Project").find("div").find("input"), function () {
                    proArray.push($(this).val());
                });
                projectStr=proArray.join(",");
            }
            if (type != "0"&&!awards.length) {
               layer.msg("请输入奖项说明！"); return; 
            }
            introduction = encodeURIComponent(introduction);
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                    Func: UrlDate.itemid == 0 ? "AddActiActivity" : "EditActiActivity",
                    DepartId: UrlDate.relid,
                    ItemId: UrlDate.itemid,
                    Name: name,
                    Range: graderray.join(','),
                    Type: type,
                    EntStartTime:entStartTime,
                    EntEndTime: entEndTime,
                    ActStartTime: actStartTime,
                    ActEndTime: actEndTime,
                    Address: address,
                    Introduction: introduction,
                    Awards:awards,
                    ActivityImg: $("#img_ActivityImg").attr("src"),
                    SendExamStatus:sendExamStatus,
                    LoginUID: "<%=UserInfo.UniqueNo%>",
                    ProjectStr: projectStr
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == -1) {
                        layer.msg("该活动名称已存在!");
                    }
                    else if (result.errNum == 0) {
                        parent.layer.msg(UrlDate.itemid == 0 ? '创建活动成功!' : '修改活动成功!');
                        parent.GetActiActivityData(1, 10);
                        parent.CloseIFrameWindow();
                    } else {
                        layer.msg(result.errMsg);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.msg("操作失败！");
                }
            });
        }
        function GetGradeData(grange) {
            var $div_Grades = $("#div_Grades");
            $div_Grades.html('');
            var gselArray = [];
            if (grange.length) {
                gselArray = grange.split(",");
            }
            $.ajax({
                url: "/CommonHandler/UnifiedHelpHandler.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: { "Func": "GetGradeData" },
                success: function (json) {
                    if (json.result.errNum.toString() == "0") {
                        if (gselArray.length) {
                            $(json.result.retData).each(function (i, n) {
                                $div_Grades.append('<input type="checkbox" name="ck_grade" value="' + n.Id + '" ' + ($.inArray(n.Id, gselArray) == -1 ? '' : 'checked="checked"') + '/>' + n.GradeName);
                            });
                        } else {
                            $(json.result.retData).each(function (i, n) {
                                $div_Grades.append('<input type="checkbox" name="ck_grade" value="' + n.Id + '"/>' + n.GradeName);
                            });
                        }                        
                    }
                    else { }
                },
                error: function (errMsg) {
                    layer.msg(errMsg);
                }
            });
        }
        var editproid = 0;
        function AddActiProject() {
            if (UrlDate.itemid != 0) {
                SaveActiProject(0);
            } else {
                var $proObj = $("#txt_Project");
                var proName = $proObj.val().trim();
                var isexist = false;
                if (proName.length > 0) {
                    $.each($("#div_Project").find("div").find("input"), function () {
                        if ($(this).val() == proName) { isexist = true; }
                    });
                    if (!isexist) {
                        if (proName.indexOf(",") == -1) {
                            var proLi = '<div class="fl edit_close"><input type="text" readonly="readonly" class="text" value="' + proName + '"/><i class="iconfont icon-close close1" onclick="$(this).parent().remove();"></i></div>';
                            $("#div_Project").prepend(proLi);
                            $proObj.val('');
                        }
                        else { layer.msg("名称中不能包含<q>,</q>字符!");}
                    } else {
                        layer.msg("该项目已存在!");
                    }
                }
            }            
        }
        function EditActiProject(itemid) {
            editproid = itemid;
            var $txtObj = $("#txt_Project_" + itemid);
            $txtObj.removeAttr("readonly");
            var t = $txtObj.val();
            $txtObj.val("").focus().val(t); //jquery中focus()函数实现当对象获得焦点后自动把光标移到内容最后
        }        
        function SaveActiProject(itemid) {
            var name = "";
            if (itemid == 0) {
                name = $("#txt_Project").val().trim();
            } else {
                if (editproid == 0) {
                    return;
                }
                name = $("#txt_Project_" + itemid).val().trim();
            }      
            if (!name.length) {layer.msg("请填写项目名称！");return;}
            $.ajax({
                url: "/Common.ashx",
                type: "post",
                async: false,
                dataType: "json",
                data: {
                    PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                    Func: itemid == 0 ? "AddActiProject" : "EditActiProject",
                    ItemId: itemid,
                    ActivityId: UrlDate.itemid,
                    Name: name,
                    LoginUID:"<%=uniqueNo%>"
                },
                success: function (json) {
                    var result = json.result;
                    if (result.errNum == -1) {
                        layer.msg("该项目名称已存在!");
                    }
                    else if (result.errNum == 0) {                        
                        if (itemid == 0) {
                            layer.msg('创建项目成功!');
                            GetActiProjectData();
                            $("#txt_Project").val('');
                        } else {
                            layer.msg('修改项目成功!');
                            editproid = 0;
                            $("#txt_Project_" + itemid).attr("readonly", "readonly");
                        }
                    } else {
                        layer.msg(result.errMsg);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    layer.msg("操作失败！");
                }
            });
        }
        function DelActiProject(itemid, obj) {
            layer.confirm('确定要删除该项目吗？', {
                btn: ['确定', '取消'] //按钮
            }, function () {
                $.ajax({
                    url: "/Common.ashx",
                    type: "post",
                    dataType: "json",
                    data: {
                        PageName: "/StuActivity/Acti_ActivityHandler.ashx",
                        func: "DelActiProject",
                        ItemId: itemid
                    },
                    success: function (json) {
                        if (json.result.errNum == 0) {
                            $(obj).parent().remove();
                            layer.msg('删除成功！');                           
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
        function ActTypeChange(obj) {
            var $div_RowProject = $("#div_RowProject"), $div_RowAwards = $("#div_RowAwards");
            if (obj.value == "0") {
                $div_RowProject.hide();
                $div_RowAwards.hide();
            } else {
                $div_RowProject.show();
                $div_RowAwards.show();
            }
        }
    </script>
</body>
</html>
