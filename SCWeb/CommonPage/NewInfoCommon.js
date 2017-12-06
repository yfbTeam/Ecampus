//发布话题
function AddComNewInfo(relid, isdetail, navid) {
    isdetail = arguments[1] || 0;
    navid = arguments[2] || 0;
    OpenIFrameWindow('发布话题', '/CommonPage/EditComNewInfo.aspx?itemid=0&type=' + UrlDate.type + '&newtype=' + UrlDate.newtype + '&relid=' + relid + '&isdetail=' + isdetail + '&nav=' + navid, '730px', '600px');
}
//编辑话题
function EditComNewInfo(itemid) {
    OpenIFrameWindow('编辑话题', '/CommonPage/EditComNewInfo.aspx?itemid=' + itemid, '730px', '600px');
}
//删除话题
function DelComNewInfo(itemid) {
    layer.confirm('确定要删除该话题吗？', {
        btn: ['确定', '取消'] //按钮
    }, function () {
        $.ajax({
            url: "/Common.ashx",
            type: "post",
            dataType: "json",
            data: {
                PageName: "/CommonHandler/Com_NewHandler.ashx",
                func: "DelComNewInfo",
                ItemId: itemid
            },
            success: function (json) {
                if (json.result.errNum == 0) {                    
                    layer.msg('删除成功！');
                    GetComNewInfoData(1, 10);
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
//点赞
function GoodClick(relationid, spanid, type,pwordid) {
    type = arguments[2] || 0;
    pwordid = arguments[3] || '';
    $.ajax({
        url: "/Common.ashx",
        type: "post",
        async: false,
        dataType: "json",
        data: {
            PageName: "/CommonHandler/Com_NewHandler.ashx",
            Func: "GoodClick",
            Type: type,
            RelationId: relationid,
            LoginUID: $("#HLoginUID").val()
        },
        success: function (json) {
            var result = json.result;
            if (result.errNum == 0) {
                var $sobj = $("#" + spanid);
                var oldcount = parseInt($sobj.html());
                var newcount = result.errMsg.indexOf('取消') > -1 ? (oldcount - 1) : (oldcount + 1);
                $sobj.html(newcount);
                layer.msg(result.errMsg);
                if (pwordid != '') { $('#' + pwordid).html(result.errMsg.indexOf('取消') > -1 ? "点赞" : "取消点赞"); }
            }
        },
        error: function (errMsg) { }
    });
}
//置顶
function NewTopSet(itemid, istop) {
    $.ajax({
        url: "/Common.ashx",
        type: "post",
        async: false,
        dataType: "json",
        data: {
            PageName: "/CommonHandler/Com_NewHandler.ashx",
            Func: "NewTopSet",
            ItemId: itemid,
            IsTop: istop
        },
        success: function (json) {
            var result = json.result;
            if (result.errNum == 0) {
                layer.msg(istop == 1 ? "置顶成功!" : "取消置顶成功!");
                GetComNewInfoData(1, 10);
            }
        },
        error: function (errMsg) { }
    });
}
//精华
function NewEliteSet(itemid, iselite) {
    $.ajax({
        url: "/Common.ashx",
        type: "post",
        async: false,
        dataType: "json",
        data: {
            PageName: "/CommonHandler/Com_NewHandler.ashx",
            Func: "NewEliteSet",
            ItemId: itemid,
            IsElite: iselite
        },
        success: function (json) {
            var result = json.result;
            if (result.errNum == 0) {
                layer.msg(iselite == 1 ? "加精成功!" : "取消加精成功!");
                var $em_NewElite_id = $("#em_NewElite_" + itemid), $spanid = $("#span_NewElite_" + itemid);
                $em_NewElite_id.html(iselite == 1 ? "取消加精" : "加精");
                if (iselite == 1) { $spanid.show() } else { $spanid.hide() }
                $em_NewElite_id.removeAttr("onclick");
                $em_NewElite_id.click(function () {
                    NewEliteSet(itemid, iselite == 1 ? 0 : 1);
                });
            }
        },
        error: function (errMsg) { }
    });
}
//加载最新贴/最新公告
function GetNewestNew(type,relationid,parid, childid,startIndex, pageSize,newtype) {    
    type = arguments[0] || 0; //0社团；1部门；2宿舍
    relationid = arguments[1] || '';
    parid = arguments[2] || 'ul_Newest';
    childid = arguments[3] || 'li_Newest';
    startIndex = arguments[4] || 1;
    pageSize = arguments[5] || 5;
    newtype = arguments[6] ||0;  //0帖子；1通知
    var parmaArray = {
        PageName: "/CommonHandler/Com_NewHandler.ashx",
        Func: "GetComNewInfoDataPage",
        Type: type,
        NewType: newtype,
        PageIndex: startIndex,
        PageSize: pageSize
    };
    if (relationid != '') {
        parmaArray["RelationId"] = relationid;
    }
    var $parobj = $("#" + parid), $childobj = $("#" + childid);
    $.ajax({
        url: "/Common.ashx",
        type: "post",
        dataType: "json",
        data: parmaArray,
        success: function (json) {            
            if (json.result.errNum.toString() == "0") {                
                $parobj.html('');
                var rtnObj = json.result.retData;
                $childobj.tmpl(rtnObj.PagedData).appendTo("#" + parid);
            }
            else {
                $parobj.html('<li>' + (newtype == 0 ? "暂无最新贴！" : "暂无公告！") + '</li>');
            }
        },
        error: function (errMsg) {
            $parobj.html('<li>' + (newtype == 0 ? "暂无最新贴！" : "暂无公告！") + '</li>');
        }
    });
}
//社团热门帖子
function GetHotComNew(startIndex, pageSize) {
    //初始化序号 
    pageNum = (startIndex - 1) * pageSize + 1;
    var parmaArray = {
        PageName: "/CommonHandler/Com_NewHandler.ashx",
        Func: "GetComNewInfoDataPage",
        Type: 0,
        OrderBy: "IsTop desc,T.BrowsingTimes desc",
        PageIndex: startIndex,
        PageSize: pageSize,
        IsOnlyBase: "1",
        IsUnifiedInfo: "1"
    };
    $.ajax({
        url: "/Common.ashx",
        type: "post",
        dataType: "json",
        data: parmaArray,
        success: function (json) {
            if (json.result.errNum.toString() == "0") {
                $("#div_HotNew").html('');
                var rtnObj = json.result.retData;
                $("#div_HotNewSub").tmpl(rtnObj.PagedData).appendTo("#div_HotNew");
            }
            else {
                $("#div_HotNew").html('<div>暂无帖子！</div>');
            }
        },
        error: function (errMsg) {
            $("#div_HotNew").html('<div>暂无帖子！</div>');
        }
    });
}
//社团招新通知
function GetRecruitNotice(startIndex, pageSize) {
    //初始化序号 
    pageNum = (startIndex - 1) * pageSize + 1;
    var parmaArray = {
        PageName: "/CommonHandler/Com_NewHandler.ashx",
        Func: "GetComNewInfoDataPage",
        Type: 0,
        NewType: 1,
        IsRecruit:1,
        PageIndex: startIndex,
        PageSize: pageSize,
        IsOnlyBase: "1",
        IsUnifiedInfo: "1"
    };
    $.ajax({
        url: "/Common.ashx",
        type: "post",
        dataType: "json",
        data: parmaArray,
        success: function (json) {
            if (json.result.errNum.toString() == "0") {
                $("#div_HotNew").html('');
                var rtnObj = json.result.retData;
                $("#div_HotNewSub").tmpl(rtnObj.PagedData).appendTo("#div_HotNew");
            }
            else {
                $("#div_HotNew").html('<div>暂无招新通知！</div>');
            }
        },
        error: function (errMsg) {
            $("#div_HotNew").html('<div>暂无招新通知！</div>');
        }
    });
}

/**********************************************************************************************************************************/
var curalbumid = 0;
//相册点击事件
function ComAlbumClick(id,name) {
    curalbumid = id;
    $aAlbum_TwoCrumbs = $("#aAlbum_TwoCrumbs");
    $aAlbum_TwoCrumbs.html('&gt;'+name);
    $aAlbum_TwoCrumbs.show();
    GetComAlbumPicData(1, 10);
}
function Album_OneCrumbsClick() {
    if (picviewer) { picviewer.destroy(); }
    curalbumid = 0;
    $aAlbum_TwoCrumbs = $("#aAlbum_TwoCrumbs");
    $aAlbum_TwoCrumbs.hide();
    GetComAlbumData(1, 10);
}
//加载相册照片
var picviewer = "";
function GetComAlbumPicData(startIndex, pageSize) {
    if (picviewer) { picviewer.destroy(); }
    $("#div_Album").html('');
    $("#pageBar_Album").hide();
    $.ajax({
        url: "/Common.ashx",
        type: "post",
        dataType: "json",
        data: {
            PageName: "/CommonHandler/Com_AlbumHandler.ashx",
            Func: "GetComAlbumPicDataPage",
            AlbumId: curalbumid,
            ispage: false
        },
        success: function (json) {
            if (json.result.errNum.toString() == "0") {                
                var rtnObj = json.result.retData;
                $("#div_PicSub").tmpl(rtnObj).appendTo("#div_Album");
                picviewer = new Viewer(document.getElementById('div_Album'), {
                    url: 'data-original'
                });
                //$('#div_Album').viewer({
                //    url: 'data-original',
                //});
            }
            else { }
        },
        error: function (errMsg) {
            layer.msg(errMsg);
        }
    });
}
