// JavaScript Document
$(function(){
	//表格隔行变色
	rxued.table.LChangeapart($("#minshowtable .target_tr"),'#fff','#F8EFED');
	
	//鼠标滑过变色
	rxued.table.hoverChage($("#minshowtable .target_tr"),"#A8D7CA")		
	
	function init(){
		//计算高度，显示滚动条
		rxued.table.countTB($("#headtable"),$("#minshowtable"),$("#j_scrollbox"));
		//tb_3根据上面表头变化而宽度变化
		$("#tb_3").width($("#headtable").width());
		}  
    		
		//窗口变化时			
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
})