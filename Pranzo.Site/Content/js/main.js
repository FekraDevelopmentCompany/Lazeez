$(document).ready(function(){

    $(".filter-button").click(function(){
        var value = $(this).attr('data-filter');
        
        if(value == "all")
        {
            //$('.filter').removeClass('hidden');
            $('.filter').show('1000');
        }
        else
        {
//            $('.filter[filter-item="'+value+'"]').removeClass('hidden');
//            $(".filter").not('.filter[filter-item="'+value+'"]').addClass('hidden');
            $(".filter").not('.'+value).hide('3000');
            $('.filter').filter('.'+value).show('3000');
            
        }
    });
    
    if ($(".filter-button").removeClass("active")) {
$(this).removeClass("active");
}
$(this).addClass("active");




     $(".btn1").click(function(){
         $("#tab1").removeClass("grey-scale");
        $("#tab2, #tab3, #tab4").addClass("grey-scale");
    });
    
     $(".btn2").click(function(){
        $("#tab2").removeClass("grey-scale");
        $("#tab1, #tab3, #tab4").addClass("grey-scale");
    });
     $(".btn3").click(function(){
        $("#tab3").removeClass("grey-scale");
        $("#tab1, #tab2, #tab4").addClass("grey-scale");
    });
     $(".btn4").click(function(){
                 $("#tab4").removeClass("grey-scale");
        $("#tab1, #tab2, #tab3").addClass("grey-scale");
    });
  


});




