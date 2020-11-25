window.starterui = window.starterui || {};
window.starterui.carousel = window.starterui.carousel || {};

window.starterui.carousel = (function ($) {
    var model,
        initMobileCenteredCarousel = function () {
            $(model.mobileCenteredCarousel).slick({
                mobileFirst: true,
                centerMode: true,
                centerPadding: '70px 0 0',
                dots: true,
                arrows: false,
                slidesToShow: 1.2,
                infinite: false,
                //variableWidth: true,
                responsive: [{
                    breakpoint: 769,
                    settings: 'unslick'
                }]
            }).on('setPosition', function (event, slick) {
                var viewportSize = $(window).width();
                if ($(slick.$slider).find('.related-item').length != 0 && viewportSize < 770) {
                    var relatedSlides = $(slick.$slider).find('.related-item__details');
                    setHeight(relatedSlides);
                } else {
                    //$('.related-item__details').removeAttr('style');
                }
                
            });

            // console.log($(model.mobileCenteredCarousel).find('.listing-item'));
            // if ($(model.mobileCenteredCarousel).find('.listing-item').length != 0) {
            //     $(model.mobileCenteredCarousel).each(function (content, index) {
            //         console.log(content, index);
            //     });
            // }
        },  
        initMobileCarousel = function () {
            $(model.mobileCarousel).slick({
                mobileFirst: true,
                dots: true,
                autoplay: false,
                fade: true,
                cssEase: 'linear',
                arrows: true,
                accessibility: true,
                responsive: [
                    {
                    breakpoint: 769,
                    settings: 'unslick'
                    }
                ]
            });
        },
        initDesktopCarousel = function () {
            $(model.desktopCarousel).slick({
                mobileFirst: true,
                dots: true,
                autoplay: false,
                fade: true,
                cssEase: 'linear',
                arrows: true,
                accessibility: true
            }).on('setPosition', function (event, slick) {
                if ($(slick.$slider).find('.spotlight__description').length != 0) {
                    var spotlights = $(model.desktopCarousel).find('.spotlight__description');
                    setHeight(spotlights);
                }
            });
            setHeight();
        },
        getHeight = function (item) {
            var max = 0;
            $(item).each( function (){
                var itemHeight = $(this).height();
                if (max < itemHeight) {
                    max = itemHeight;
                }
            });
            return max;
        },
        setHeight = function (item) {
            var max = getHeight(item);
            $(item).each( function (){
                $(this).height(max);
            });
        },

        /**
         * Init class to initiate script
         * @public
         * @param {object} args is from doc-ready init file
         */
        init = function (args) {
            args = args || {};
            model = {
                init: function () {
                    this.container = args.container || '.carousel-wrapper';
                    this.carouselItem = args.carouselItem || '.carousel-container';
                    this.textContainer = args.textContainer || model.container + ' .slide-text-container .text-content';
                    this.mobileCarousel = args.mobileCarousel || '.mobile-carousel';
                    this.desktopCarousel = args.desktopCarousel || '.desktop-carousel';
                    this.mobileCenteredCarousel = args.mobileCenteredCarousel || '.mobile-centered-carousel';
                }
            };
            model.init();
            
            $.getScript(baseUrl + "/assets/js/vendors/slick.js")
            .done(function( script, textStatus ) {

                initMobileCarousel();
                initMobileCenteredCarousel();
				
                window.addEventListener('resize', function () {
                    if (!$(model.mobileCarousel).hasClass('slick-initialized')) {
                        return initMobileCarousel();
                    }
                });

                window.addEventListener('resize', function () {
					 if ($(window).width() < 770) {
                        $(model.mobileCenteredCarousel).slick({
                            mobileFirst: true,
                            centerMode: false,
                            centerPadding: "25px",
                            dots: true,
                            arrows: false,
                            slidesToShow: 1.2,
                            infinite: false,
                            responsive: [{
                                breakpoint: 769,
                                settings: "unslick"
                            }]
                        })
                    }
                    if (!$(model.mobileCenteredCarousel).hasClass('slick-initialized')) {
                        return initMobileCenteredCarousel();
                    }
                });


                initDesktopCarousel();

                $(model.carouselItem).slick({
                    dots: true,
                    autoplay: true,
                    autoplaySpeed: 5000,
                    fade: true,
                    cssEase: 'linear',
                    arrows: false
                });
				
				$(model.mobileCenteredCarousel).slick({
                        mobileFirst: true,
                        centerMode: false,
                        centerPadding: "25px",
                        dots: true,
                        arrows: false,
                        slidesToShow: 1.2,
                        infinite: false,
                        responsive: [{
                            breakpoint: 769,
                            settings: "unslick"
                        }]
                    });
                

            });
        }

    /**
     * Returns init
     * @return public methods/functions
     * @public
     */
    return {
        init: init
    };
    

}(jQuery));

$(window).on('load', function(){
	var i;
    $('.spotlight__wrapper .spotlight__component').each(function(){
        var lilngth = $(this).find('ul.slick-dots > li').length;
        if (lilngth == 6) {i=16;}
        if (lilngth == 5) {i=20;}
        if (lilngth == 4) {i=24;}
        if (lilngth == 3) {i=28;}
        if (lilngth == 2) {i=32;}
        console.log('ankan'+ lilngth);
        $(this).find('.slick-prev').css('left',i+'%');
        $(this).find('.slick-next').css('right',i+'%');
    });
        
});