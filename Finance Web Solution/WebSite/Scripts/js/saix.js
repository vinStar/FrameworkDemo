// JavaScript Document
$("#selectRegion .regionTxt").click(function(){
            var blockType = $("#regionlist").css('display');
            if(blockType!='block'){
                $("#regionlist").css('display','block').attr('data-block','block');
            }else{
                $("#regionlist").css('display','none').attr('data-block','none');
            }
        })
        //$(document).click(function(e){
        //    e.stopPropagation();
        //    $("#regionlist").css('display','none').attr('data-block','none');
        //})

        $(document).bind("click",function(e){
            var target = $(e.target);
            if(target.closest("#regionlist,#selectRegion").length == 0){
                $("#regionlist").hide();
            }
        })



        $(".btn_country").click(function(){
            if($(this).hasClass('active')){
                $(this).removeClass('active');
                $(":checkbox").removeAttr("checked");
            }else{
                $(this).addClass('active');
                $(".btn_region").removeClass('active');
                $(":checkbox").attr("checked","checked");
            }
        })
