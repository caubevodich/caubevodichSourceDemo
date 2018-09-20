$( document ).ready(function() {
    
    $(".basic-multisig #coins").css("display", "none");
    
    $(window).on("scroll", function() {
	
	var basicTop = $(".basic-multisig").offset().top;
	var basicBottom = $(".basic-multisig").offset().top + $(".basic-multisig").height();
	
	if ($(window).scrollTop() > (basicTop - 200) && $(window).scrollTop() < basicBottom) {
	    console.log("basic")
	    $(".basic-multisig #two-keys").animate({
		top: "245px"
	    }, 500, function() {
		$(".basic-multisig #coins").fadeIn("fast");
	    });
	}
	
    });
    
    $('.fadein img:gt(0)').hide();
    setInterval(function(){
	$('.fadein :first-child').fadeOut("slow")
	    .next('img').fadeIn("slow")
	    .end().appendTo('.fadein');}, 
		15000);
    
});
