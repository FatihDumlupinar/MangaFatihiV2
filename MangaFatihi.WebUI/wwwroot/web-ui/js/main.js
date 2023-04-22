
var owl = $('.owl-carousel');
owl.owlCarousel({
    items:4,
    loop:true,
    margin:10,
    autoplay:true,
    autoplayTimeout:2000,
    autoplayHoverPause:true,
    responsiveClass:true,
    responsive:{
        0:{
            items:2,
            nav:true
        },
        600:{
            items:3,
            nav:false
        },
        1000:{
            items:5,
            nav:true,
            
        },
        2000:{
            items:7,
            nav:true,
            
        }
    }
});

$('i.rate-popover').click(function(){
    alert('this.data-index')
});