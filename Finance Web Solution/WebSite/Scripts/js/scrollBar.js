
$(function(){
	//滚动功能
	var Scro=$("#scroll");
	var Scrp=$("#p");
	var Bool=false;
	var Scro=$("#scroll");
	var Scrp=$("#p");
	var Scrobd=$("#bd");
	var Scroul=$("#nav-list");
	function moScroll(height1,height2){

	var Scrp_Height =height2/2;
	var Num2=height1-height2;
	var offsetX=0;
	var offsetY=0;
	Scrp.mousedown(function(e){  
		Bool=true;
	});
	$(document).mouseup(function(){
		Bool=false;
	});
	$(document).mousemove(function(e){
		if(Bool){
			var Num1= e.clientY - Scro.position().top;
			var y=Num1 - Scrp_Height;
			if(y<=1){
				Scrll(0);
				Scrp.css("top",1);
			}else if(y>=Num2){
				Scrp.css("top",Num2);
				Scrll(Num2);
			}else{
				Scrll(y);
			}
		}
	});
	function Scrll(y){
		Scrp.css("top",y);
		Scroul.css("margin-top",-(y/(height1-height2))*(Scroul.outerHeight()-Scrobd.height()));
	}
	if(document.getElementById("scroll_bd").addEventListener) 
	document.getElementById("scroll_bd").addEventListener('DOMMouseScroll',wheel,true);
	document.getElementById("scroll_bd").onmousewheel=wheel;
	var Distance=Num2*0.1;
	function wheel(e){
		var evt = e || window.event;
		var wheelDelta = evt.wheelDelta || evt.detail;
		if(wheelDelta == -120 || wheelDelta == 3){
			var Distances=Scrp.position().top+Distance;
			if(Distances>=Num2){
				Scrp.css("top",Num2);
				Scrll(Num2);
			}else{
				Scrll(Distances);
			}
			return false;
		}else if (wheelDelta == 120 || wheelDelta == -3){
			var Distances=Scrp.position().top-Distance;
			if(Distances<=1){
				Scrll(0);
				Scrp.css("top",1);
			}else{
				Scrll(Distances);
			}
			return false;
		}   
	} 
	}
	
	 var minheight;
	 minheight = $("body").eq(0).height()-$("#navbar").height()-$("#sidebar-shortcuts").height()-$("#sidebar-collapse").height()  - $("#note1").height() -15;
	 $("#scroll_bd,#bd").height(minheight);
	 $("#scroll").height(minheight-2);
	
	 if($("#nav-list").height()>minheight){
		$("#scroll").show();
		moScroll(Scro.outerHeight(),Scrp.outerHeight());
	}
	  else{
		$("#scroll").hide();	 
	}
	
	 
	
	$(window).resize(function(){
		minheight = $("body").eq(0).height()-$("#navbar").height()-$("#sidebar-shortcuts").height()-$("#sidebar-collapse").height() - $("#note1").height() -20;
		Scrll(0);
		Scrp.css("top",1);
		$("#scroll_bd,#bd").height(minheight);
		$("#scroll").height(minheight-2);
		if($("#nav-list").height()>minheight){
	 		$("#scroll").show();
			moScroll(Scro.outerHeight(),Scrp.outerHeight());
		}
		  else{
			$("#scroll").hide();
		}
		
		 function Scrll(y){
			 Scrp.css("top",y);
			 Scroul.css("margin-top",-(y/(Scro.outerHeight()-Scrp.outerHeight()))*(Scroul.outerHeight()-Scrobd.height()));
		}
	})
	
	
	
	//计算高度
	var ominHeight=$("body").eq(0).height()-$("#navbar").height();
	
	$("#main-container").css("height",ominHeight+'px');
	$(window).resize(function(){
		var ominHeight=$("body").eq(0).height()-$("#navbar").height();
		$("#main-container").css("height",ominHeight+'px');	
	})	
	
	
	//点击之后，子菜单展开
	$(".nav-list li.parent_li").click(function(){
		$(this).siblings().find(".line").hide();
		if($(this).has($(".submenu"))){
		    $(this).find(".submenu li").eq(0).addClass("active2").find(".icon-left img").attr("src", "/Content/Images/icon0.jpg");
			$(this).find(".submenu li").eq(0).siblings().removeClass("active2").find(".icon-left img").attr("src", "/Content/Images/icon1.jpg");
			$(this).find(".line").show().parent().siblings().find(".line").hide();	
			$(".note_content").html($(this).find(".submenu li:eq(0) a").attr('title')).parent().show();
		}
		$(this).addClass("active").siblings().removeClass("active");
		$(this).find(".submenu").show().animate({"opacity":"1"},100,function(){
			//回调函数 ---判断是否左侧导航滚动条出现
			if($("#nav-list").height()>minheight){
				$("#scroll").show();
				 //滚动条滚动功能
				 minheight = $("body").eq(0).height()-$("#navbar").height()-$("#sidebar-shortcuts").height()-$("#sidebar-collapse").height() - 15;
				 $("#scroll_bd,#bd").height(minheight);
				 $("#scroll").height(minheight-2);
				 moScroll(Scro.outerHeight(),Scrp.outerHeight()); 
			 }
			 else{
				$("#scroll").hide();
			}	
		});
		$(this).siblings().find(".submenu").hide();

		$(this).siblings().find(".submenu li").removeClass("active2").find(".icon-left").hide();
		
		
		 
	});

	
	$(".nav-list .submenu li").click(function(e){
		e.stopPropagation();
		$(this).addClass("active2").siblings().removeClass("active2");
		$(this).parent().find(".active2 .icon-left img").attr("src", "/Content/Images/icon0.jpg");
		$(this).parent().parent().find(".line").show();
		$(this).parent().parent().siblings().find(".line").hide();
		$(this).siblings().find(".icon-left img").attr("src", "/Content/Images/icon1.jpg");
		$(this).siblings().find(".icon-left").hide();
		$(".note_content").html($(this).find("a").attr('title')).parent().show();
	});
	
	$(".submenu li a").hover(function(){
		$(this).find(".icon-left").show();
	},function(){
		$(this).find(".icon-left").not(".active2 a .icon-left").hide();
	});

})