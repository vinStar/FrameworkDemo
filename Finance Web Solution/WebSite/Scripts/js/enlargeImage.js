
	(function () {

        var container = $$("targetPic"), src = "",
            options = {

            },
            it = new ImageTrans(container, options);
            it.load(src);

        //右旋转
        $$("idRight").onclick = function () { it.right(); }

        //var marginL = $(".index_pic img").css('width');

        $(".index_pic img").css({ 'max-width': $(window).width() - 40 });

        $(window).resize(function () {
            $(".index_pic img").css({ 'max-width': $(window).width() - 40 });
        })



    })()
    
	$(function(){
		
//		$(document).keyup(function(e){
//	        var key =  e.keyCode;
//	        if(key == 27){
//	        	$("#mask0,#targetPic").hide();
//	        }
//	    });
//	
//		
//		$(".index_pic img").css({'max-width':$(window).width()-40});
//		
//		$(window).resize(function(){
//			$(".index_pic img").css({'max-width':$(window).width()-40});
//		})
		
		
	$("#targetPic img").attr('data-index', '');
        $(document).keyup(function (e) {
            var key = e.keyCode;
            if (key == 27) {
                $("#mask0,#targetPic").hide();
            }
        });
       
		
		
		$("#cp_right").click(function(){
			 var i = $(".index_pic img").attr('data-index');
			var length = $("#targetPic").attr('data-count');
			if(i==length-1) i=0;
			else i++;
			var urls = $("#targetPic").attr('data-msg');
			var aUrl = urls.split('|');
			$("#targetPic img").attr({'src':aUrl[i],'data-index':i});
		});

		
		
		$("#cp_left").click(function(){
			 var i = $(".index_pic img").attr('data-index');
			var length = $("#targetPic").attr('data-count');
			if(i==0) i=length-1;
			else i--;
			var urls = $("#targetPic").attr('data-msg');
			var aUrl = urls.split('|');
			$("#targetPic img").attr({'src':aUrl[i],'data-index':i});
		});
		
		$("#idDel").click(function () {
            $("#targetPic").hide();
            $("#j_alertbox_db").css('z-index',1000);
        })

	});
