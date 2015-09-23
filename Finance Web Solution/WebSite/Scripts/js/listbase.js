// JavaScript Document


$(function(){
	$('#mask3').hide();
	
	rxued.table.LChangeapart($("#minshowtable tr"),"#fff","#F0F9FF");
	rxued.table.hoverChage($("#minshowtable tr"),"#A8D7CA");
	rxued.table.LChangeapart($("#minshowtable2 tr"),"#fff","#F0F9FF");
	rxued.table.hoverChage($("#minshowtable2 tr"),"#A8D7CA");
	
	//含多个iframe的页面的计算高度
	function init(){
		rxued.table.countTB($('#headtable'),$('#minshowtable'),$(".tableshow"),function(){
			rxued.layer.tolayerWH($("#mask2"),$("#tb_main"),$(document));
		}); 
		rxued.alertbox.judgeH($('.alertminbox'), 500);
		rxued.table.towidth($("#tb_3"), $("#minshowtable"));
	}
	
	

	//进度条
	rxued.progress.doProgress($(".scroll_bar"),".scroll_bar_content",".scroll_num");
	
	//计算高度和宽度
	parent.$(window).load(function(){ 
		init();
	});
    parent.parent.$(window).load(function(){  
		init();
    });
    parent.parent.$(window).resize(function(){  
		init();
    });
    parent.$(window).resize(function(){ 
		init();
    })
	
	
});