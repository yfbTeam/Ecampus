$(function () {
    //点击切换显示
    function clickTab(tabObj, transformObj, tarObj) {
        var $contentbox = tabObj.find('.contentbox');
        if ($contentbox.is(':hidden')) {
            $contentbox.show();
            tabObj.find(transformObj).css('transform', 'rotate(180deg)');
        } else {
            $contentbox.hide();
            tabObj.find(transformObj).css('transform', 'rotate(0deg)');
        }
    }
	//教育学部显示隐藏
    $('.choiceversion').find('.selected').click(function () {
        clickTab($('.choiceversion'), '.icon');
        $('.grade').find('.contentbox').hide();
        $('.grade').find('.icon_right').css('transform', 'rotate(0deg)');
    });
	//menu显示隐藏
	$('.grade').find('.item').click(function(){		
	    clickTab($('.grade'), '.icon_right');
	    $('.choiceversion').find('.contentbox').hide();
	    $('.choiceversion').find('.icon').css('transform', 'rotate(0deg)');
	});
	//鼠标划过批量操作
	$('.operate').hover(function(){
		$(this).find('.operate_wrap').show();
	},function(){
		$(this).find('.operate_wrap').hide();
	})
	//筛选
	$('.selection').click(function(){
		$('.selectionwrap').toggle();
	})
	//列表页与图标页切换
	$('.list_grid_switch').find('a').click(function(){
		$(this).addClass('on').siblings().removeClass('on');
		var n = $(this).index();
		$('.docu_content>ul').eq(n).show().siblings().hide();
	})
	//列表页与图标页每项划过显示
	function hoverShow(hoverObj,showObj){
		showObj.find('.arrow-down').click(function(){
			$(this).find('.arrow_downwrap').show();
		});
		hoverObj.find('input[type=checkbox]').click(function(){
			if($(this).is(':checked')){
				$(this).parents('li').addClass('active');
			}else{
				$(this).parents('li').removeClass('active');	
			}
		});
		hoverObj.hover(function(){
			$(this).find(showObj).show();
		},function(){
			$(this).find(showObj).hide();
			showObj.find('.arrow_downwrap').hide();
			if($(this).find('input[type=checkbox]').is(':checked')){
				$(this).find(showObj).show();
			}
		})
	}
	hoverShow($('.document_list li'),$('.unload_none'));
	hoverShow($('.docu_grid li'),$('.checkbox'));
	hoverShow($('.skydrive_grid_list li'),$('.checkbox'));
	//删除
	function deleted(deleteObj){
		deleteObj.click(function(){
			$(this).parents('li').remove();
		});
	}
	deleted($('.arrow_downwrap .delete'));
	//重命名
	$('.arrow_downwrap .rename').click(function(){
		var renameObj = $(this).parents('li').find('.docu_title');
		var className = renameObj.children('span')[0].className;
		var text = renameObj.clone().text();
		renameObj.html('<span class="'+className+'"></span><input type="text" name="" id="rename" value="'+text+'"/><span class="sure"></span><span class="cancal"></span>');
		//确定
		$('.sure').click(function(){
			var name = $('#rename').val();
			renameObj.html('<span class="'+className+'"></span><a class="docu_name" href="">'+name+'</a>');
		});
		//取消
		$('.cancal').click(function(){
			renameObj.html('<span class="'+className+'"></span><a class="docu_name" href="">'+text+'</a>');
		});
	});	
	//教案切换、
	$('.docu_item a').click(function(){
		$(this).addClass('on').siblings().removeClass('on');
	})
	//stars评价
	$('.assess').each(function(){
		$(this).find('span').click(function(){
			$(this).siblings().removeClass('on');
			$(this).prevAll().andSelf().addClass('on');
		});
	})
})
	
