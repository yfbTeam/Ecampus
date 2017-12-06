/*************************************IFrame弹框方法***********************************************************/
var curWinindex;
function OpenIFrameWindow(title,url, width, height) {
    //iframe层
    var index = layer.open({
        type: 2,
        title: title,
        shadeClose: false,
        shade: 0.2,
        area: [width, height],
        content: url //iframe的url
    });
    curWinindex = index;
}
function CloseIFrameWindow() {
    layer.close(curWinindex);
}
function layerMsg(title) { //msg信息框
    layer.msg(title, {
        time: 0 //不自动关闭
        , btn: ['确定']
        , yes: function (index) {
            layer.close(index);
        }
    });
}
//退出
$('.setting').hover(function () {
    $(this).find('.setting_none').show();
}, function () {
    $(this).find('.setting_none').hide();
})
function logOut() {
    window.location.href = "/DeskManage/Index.aspx?action=loginOut";
}