// JavaScript Document
// JavaScript Document
//定义命名空间
var rxued;
if (!rxued) rxued = {};


//页面布局
rxued.mainlayout = {
    uLower: function (a, b) {
        var allheight = $("body").eq(0).height();
        var ominHeight = allheight - a.height();
        b.css("height", ominHeight);
    }
};

$(function(){
    
	 
	 //右边表格的效果
	 //$(".tb_sub_container").animate({ top: 0}, 1000, function() {
     //           doProgress();
     //});
     //$(".tb_sub_container").animate({top:0},1000);
	 


     
     //搜索框长度变换
    /*$("#serTxt").focus(function() {
        $(this).animate({ width: 120 }, 500);
        $(this).val("");
    }).blur(function() {
        $(this).animate({ width: 50 }, 500);
        $(this).val("查询");;
    });*/
    
	//搜索框长度变换
    $(".inp_search").focus(function(){
		$(this).animate({width:120},500);
		//$(this).val("");
	}).blur(function(){
		$(this).animate({width:65},500);
		//$(this).val("查询");;
	});
	

    //小table隔行换色
    var allTr=$(".table_3 tr");
	var oColor="";
	var aTr=$(".table_3 tr:even");
	aTr.css("backgroundColor","#f0f9ff");
	allTr.hover(function(){
		oColor=$(this).css("backgroundColor");
		$(this).css("backgroundColor","#a8d7ca");
	},function(){
		$(this).css("backgroundColor",oColor);
	});	

	
	
	
	//右边大table隔行变色
	var allTr=$(".ui-jqgrid-bdiv table tr");
	var oColor="";
	var aTr=$(".ui-jqgrid-bdiv tr:odd");
	aTr.css("backgroundColor","#f0f9ff");
	allTr.hover(function(){
		oColor=$(this).css("backgroundColor");
		$(this).css("backgroundColor","#a8d7ca");
	},function(){
		$(this).css("backgroundColor",oColor);
	});	
	
	$(".ui-jqgrid-bdiv input[type='checkbox']").click(function(e){
		 e.stopPropagation();
	});
	
	$(".btnpx").css("marginLeft",-$(".btnpx").width()/2);


})


function judgeBodyH() {
    var bodyH = $('body')[0].offsetHeight - $(".alert_box_head").height() - $(".alert_box_btns").height() - 30;
    if ($("#breadcrumbs").length > 0) {
        bodyH = bodyH - $("#breadcrumbs").height();
    }
    var maxH = 500;
    bodyH = (bodyH > maxH) ? maxH : bodyH;
    $('.alert_box_iframe').css('max-height', bodyH);
	
	//var alertC = $("#ifm2").contents().find("#alertContent");
	//$("#ifm2").contents().find("#alertContent").css('max-height',(bodyH-30)+"px");

}

function judgeBtnH(){
	if($("#alert_box_iframe2",parent.document) || $("#alertContent")){
		var minh =parseInt( $("#alert_box_iframe2",parent.document).css("max-height"));
		$("#alertContent").css('max-height',(minh-30)+"px");	
	}
}


//阻止事件冒泡
function stopBubble(e) {
    //如果提供了事件对象，则这是一个非IE浏览器
    if(e && e.stopPropagation) {
  　　//因此它支持W3C的stopPropagation()方法
  　　e.stopPropagation(); 
    } else {
  　　//否则，我们需要使用IE的方式来取消事件冒泡 
  　　window.event.cancelBubble = true;
    }
    return false; 
}

//点击放大图片
function imgShow(outerdiv, innerdiv, bigimg, _this){
    var src = _this.attr("src");
    bigimg.attr("src", src);
 
    $("<img/>").attr("src", src).load(function(){
        var windowW = $(window).width();
        var windowH = $(window).height();
        var realWidth = this.width;
        var realHeight = this.height;
        var imgWidth, imgHeight;
        var scale = 0.8;
         
        if(realHeight>windowH*scale) {
            imgHeight = windowH*scale;
            imgWidth = imgHeight/realHeight*realWidth;
            if(imgWidth>windowW*scale) {
                imgWidth = windowW*scale;
            }
        } else if(realWidth>windowW*scale) {
            imgWidth = windowW*scale;
            imgHeight = imgWidth/realWidth*realHeight;
        } else {
            imgWidth = realWidth;
            imgHeight = realHeight;
        }
        bigimg.css("width",imgWidth);
         
        //var w = (windowW-imgWidth)/2;
        var h = (windowH-imgHeight)/2;
        $("#mask2",parent.document).fadeOut("fast");
        innerdiv.css({"top":h});
        outerdiv.fadeIn("fast");
        innerdiv.fadeIn("fast");
    });
     
    innerdiv.click(function(){//再次点击淡出消失弹出层
        $(this).fadeOut("fast");
        outerdiv.fadeOut("fast");
        $("#mask2",parent.document).fadeIn();
    });
}

//弹出弹出框
function alertShow(obj){
    $("#mask2").css("width",$("#tb_main").width());
    $("#mask",parent.document).show();
    $("#mask2").show().css("background","#000");
    obj.fadeIn();
}

//显示三层弹出框
function alertThreeShow(obj) {
    $("#mask2",parent.document).css("width", $("#tb_main").width());
    $("#mask", parent.parent.document).show();
	if($("#mask3")!=null){
  	  $("#mask3").show();
	}
    $("#mask2", parent.document).show().css("backgroundImage", "none");;
    obj.fadeIn();
}
//隐去弹出框
function alertThreeHide(obj) {
    $("#mask2",parent.document).hide();
    $("#mask", parent.parent.document).hide().css("backgroundImage", "none");
	if($("#mask3")!=null){
    	$("#mask3").hide();
	}
    obj.fadeOut();
}

//隐去弹出框
function alertHide(obj){
    $("#mask",parent.document).hide();
    $("#mask2").hide().css("background","#000");
	obj.fadeOut();
}

//销毁弹出框
function removeAlert(obj){
    $("#mask",parent.document).hide();
    $("#mask2").hide().css("background","#000");
	obj.remove();
}


//type：alert_success,alert_error,alert_doubt   existButton:false,true,text：内容
function createAllAlert(type,text,existButton) {
    var parentdiv = $("<div class='alertInfor'> <div class='btns'></div><p>" + text + "</p></div>");
    $(document.body).append(parentdiv);
    $('.alertInfor').addClass(type);
    //alert(text);
    var buttonEle = $("<input type='button' value='提交' class='btnSuccess fl'> <input type='button' value='取消' class='btnCancel fl'>");
    existButton && $('.btns').append(buttonEle);
    alert(existButton && $('.btns').append(buttonEle));

    alertShow($('.'+ type));

    $(".btnCancel").click(function () {
        removeAlert($('.'+type));
    });

}

//创建弹出信息框
function createAlert(type){
	var parentdiv=$("<div class='alertInfor'> <div class='btns'><input type='button' value='提交' class='btnSuccess fl'> <input type='button' value='取消' class='btnCancel fl'></div><p></p></div>");
    $(document.body).append(parentdiv);  
	$('.alertInfor').addClass(type);
}

//全选功能
function checkall(obj){
	var chk = document.getElementsByTagName("input");
	for(var i=0;i<chk.length;i++){
		if(chk[i].type =="checkbox"){
			chk[i].checked = obj.checked;	
		}	
	}	
}

function checkall2(){
	$("#tb_2 input").each(function () {
         {
			$(this).click(function () {
				if ($(this).prop("checked") == false) {
					$("#allCheck").prop("checked", false);
				}
				else if ($("#tb_2 input:checked").length == $("#tb_2 input").length) {
					$("#allCheck").prop("checked", true);
				}
			})
                
           }
     })	
}

//计算右边table的宽度，自适应出现滚动条
function countTb_height(para){
	$("#mask2").css({"width":$("#tb_main").width(),"height":$(document).height()});
	if($("#tb_2").width()>0){
	    var ominWidth=$("#tb_2").outerWidth();
	    $("#tb_1").width(ominWidth);
	    $("#tb_3").width(ominWidth);
	}
	var ominHeight=$("body").eq(0).height()-$("#navbar").height();
	$(".ui-jqgrid-bdiv").css("maxHeight",ominHeight-para+'px');
}

function countTb_height2(para){
	$("#mask2").css({"width":$("#tb_main").width(),"height":$(document).height()});
	if($("#tb_2").width()>0){
	    var ominWidth=$("#tb_2").outerWidth();
	    $("#tb_1").width(ominWidth);
	    $("#tb_3").width(ominWidth);
	}
	var ominHeight=$("body").eq(0).height();
	$(".ui-jqgrid-bdiv").css("maxHeight",ominHeight-para+'px');
}
//表格的样式
rxued.table = {
    LChangeapart: function (a, c1, c2) {
        a.each(function (i) {
            i % 2 == 0 ? a.eq(i).css('backgroundColor', c1) : a.eq(i).css('backgroundColor', c2);
        });
    },
    hoverChage: function (obj, c1) {
        var sColor = "";
        obj.hover(function () {
            sColor = $(this).css("backgroundColor");
            $(this).css("backgroundColor", c1)
        }, function () {
            $(this).css("backgroundColor", sColor)
        });
    },
    towidth: function (tab1, tab2) {
        tab1.width(tab2.width());
    },

    //tb1：表头表格，tb2：数据表格，resultObj：数据表格外面的div
    countTB: function (tb1, tb2, resultObj, fn) {
        if (typeof (fn) == "function") {
            fn();
        }
        var aHeight = 0;
        var ReductionBox = $(".j_outerheight"); //所有需要减去高度的元素加上CLASS："j_outerheight"

        for (var i = 0; i < ReductionBox.length; i++) {
            aHeight += ReductionBox.eq(i).outerHeight();

        }
        if (tb2.width() > 0) {
            var ominWidth = tb2.outerWidth();
            tb1.width(ominWidth);
        }
        //alert(ominWidth);
        var ResultHeight = $("body").eq(0).height() - parseInt(aHeight);
        resultObj.css("max-height", ResultHeight + 'px');
    }

};



//选项卡的功能
function Tab(option){
            this.opts = $(option.opts);
            this.tabTag = this.opts.find('#tab_a li');
            this.hidden = this.opts.find('.hidden');
            this.init();
}
Tab.prototype = {
	init:function(){
		var that = this;
		this.tabTag.each(function(i){
			$(this).hover(function(){
				that.tabTag.removeClass('cur');
				$(this).addClass('cur');
				that.hidden.eq(i).show().siblings().hide();
			})
		})
	}
}




function countGJ(){
	if($("#tb_2").width()>0){
	    var ominWidth=$("#tb_2").outerWidth();
	    $("#tb_1").width(ominWidth);
	    $("#tb_3").width(ominWidth);
	}
	var ominHeight=$("body").eq(0).height()-$(".btnPanel").height()-$(".big_paging").height()-70;
	$(".ui-jqgrid-bdiv").css("maxHeight",ominHeight+'px');
}



//弹出框中iframe内容自适应高度
function auto_boxHeight(objname,h){
    var oIframe = window.parent.document.getElementById(objname);
    var oBody = document.getElementsByTagName("body")[0];
    //alert(oBody.offsetHeight);
    oIframe.style.height = oBody.offsetHeight + h + 'px';
}

//滚动条的效果(信息员中)
function doProgress() {
	$(".scroll_bar").each(function(i) {
		var oAllWidth = $(this).width();
		var text =$(this).siblings(".scroll_num").text().replace(/%/g, "");
		var oWidth = ((text)/150)*oAllWidth;
	   $(this).find(".scroll_bar_content").animate({"width":oWidth},800);
	});
}



//右边table隔行变色
function change_tdColor(){
	var allTr=$(".ui-jqgrid-bdiv table tr");
	var oColor="";
	var aTr=$(".ui-jqgrid-bdiv tr:odd");
	aTr.css("backgroundColor","#f0f9ff");
	allTr.hover(function(){
		oColor=$(this).css("backgroundColor");
		$(this).css("backgroundColor","#a2d9f8");
	},function(){
		$(this).css("backgroundColor",oColor);
	});	
}

//分页控件鼠标滑过的效果
function paging_hover(){
	$(".big_page_mid .page_num").hover(function(){
			$(this).addClass("active");
	},function(){
			$(this).removeClass("active");
		}
	);
	$(".big_paging_btn").hover(function(){
		$(this).addClass("btn_hover");
	},function(){
		$(this).removeClass("btn_hover");
	})
}

function small_paging_hover(){
	$(".paging_btn").hover(function(){
		$(this).addClass("btn_hover");
	},function(){
		$(this).removeClass("btn_hover");
	})	
}

//弹出框中选项卡按钮样式的切换
function change_tab_bg(obj){
	obj.siblings().removeClass("cur").parent().find(".spn_1").removeClass("alert_cur_left").parent().find(".spn_2").removeClass("alert_cur_mid").parent().find(".spn_3").removeClass("alert_cur_right");
	obj.addClass("cur").find(".spn_1").addClass("alert_cur_left").parent().find(".spn_2").addClass("alert_cur_mid").parent().find(".spn_3").addClass("alert_cur_right");
}

//iframe - 自动计算高度
rxued.iframe = {
    autoHeight: function (pIframe) {
        var oIframe = $('#' + pIframe, parent.document);
        var oBodyHeight = $("body").eq(0).height();
        //alert(oBodyHeight);
        oIframe.height(oBodyHeight);
    },
    //带高度参数的
    autoHeight2: function (pIframe, h) {
        var oIframe = $('#' + pIframe, parent.document);
        var oBodyHeight = $("body").eq(0).height() + h;
        //alert(oBodyHeight);
        oIframe.height(oBodyHeight);
    }
};