// JavaScript Document

function countIframe(){
    var  minheight = $('body')[0].offsetHeight - $('#breadcrumbs').height() - $("#tabnavbox").height()-25;
    $('#ifrma1').css('height',minheight+'px');
}

//弹出框中iframe内容自适应高度
function auto_boxHeight(objname,h){
    var oIframe = window.parent.document.getElementById(objname);
    var oBody = document.getElementsByTagName("body")[0];
    //alert(oBody.offsetHeight);
    $(oIframe).css("height",oBody.offsetHeight + h + 'px')
    //oIframe.style.height = oBody.offsetHeight + h + 'px';
}
	
$(function(){
    $('#mask2').hide();
    tabchange("#navBox a","tabCur");
    tabchange("#navBox2 a","btn_blueCur");
	function tabchange(tabli,tabcur){
		//弹出框中，点击弹出框上的按钮，切换样式
	    $(tabli).each(function(index, element) {
	        $(this).click(function(){
	           $(this).addClass(tabcur).siblings().removeClass(tabcur);
	            countIframe();
	        });
	    });
	}
    




    countIframe();

	//计算高度和宽度
	$(window).load(function(){  countIframe(); 	 });
	parent.$(window).load(function(){  countIframe(); });
	parent.$(window).resize(function(){ countIframe();  });
	$(window).resize(function(){ countIframe(); })
});