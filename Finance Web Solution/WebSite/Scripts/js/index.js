// JavaScript Document

	$(function(){
	
			//导航收缩功能
			/*begin*/
			$("#sidebar-collapse a").click(function(){
				
				if($(this).hasClass("icon_left")){
					$(this).removeClass("icon_left").addClass("icon_right");
					$("#sidebar").addClass("menu-min");
					$("#sidebar .parent_li").find(".menu-text").hide();
					$("#sidebar .parent_li").find(".submenu").css("position","relative").hide();
					$("#sidebar-shortcuts").hide();	
					$(".summary-div").hide();	
					$("#main-content").css("marginLeft",$("#sidebar").width());
					//$("#sidebar").find(".line").hide();
					$("#scroll").hide();
					$(".note").hide();
					
				}	
				else{
					$(this).removeClass("icon_right").addClass("icon_left");
					$("#sidebar").removeClass("menu-min");
					$("#sidebar .parent_li").find(".menu-text").show();
					$("#sidebar .parent_li.active").find(".submenu").css("position","relative").show();
					$("#sidebar-shortcuts").show();	
					$(".summary-div").show();	
					$("#main-content").css("marginLeft",$("#sidebar").width());
					
					//location.reload();
				}
			});
			
			$("#sidebar .parent_li").each(function(i) {
				$(this).hover(function(){
					if($("#sidebar").hasClass("menu-min")){
						$("#sidebar .parent_li").find(".menu-text").hide();
						$("#sidebar .parent_li").find(".submenu").css("position","relative").hide();
						$(this).find(".menu-text").show();
						$(this).find(".submenu").css("position","absolute").show();
					}
				},function(){
					if($("#sidebar").hasClass("menu-min")){
						$("#sidebar .parent_li").find(".menu-text").hide();
						$("#sidebar .parent_li").find(".submenu").css("position","relative").hide();
						
					}
				});
			
		})
		
		//含有三级菜单的
		$("#sidebar .submenu li").click(function(){
			$(".child_layer").hide();
			if($(this).find(".child_layer").length){
				$(this).find(".child_layer").show();
				//三级菜单点击事件
				$(".child_layer a").each(function(){
					$(this).click(function(){
						$(this).addClass("active3").siblings().removeClass('active3');
					})
				})
			}	
		})
		
		
		
		$("#sidebar-shortcuts").hover(function(){
			if($(this).parent().hasClass("menu-min")){
				$("#sidebar-shortcuts-large").show();	
			}
		},function(){
			 if($(this).parent().hasClass("menu-min")){
				$("#sidebar-shortcuts-large").hide();	
			}
		})
		

		
		
		
	})