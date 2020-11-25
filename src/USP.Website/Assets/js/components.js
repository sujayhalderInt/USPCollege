/**
 * Check to see if component exists
 */

loadScripts = function (component) {

    if (component.css !== null) {
        // console.log('load ' + component.css);
        loadCSS( baseUrl + component.css );
    }

    if (component.js !== null) {
        // console.log('load js : ' + component.js);
        $.getScript(baseUrl + component.js)
        .done(function( script, textStatus ) {
            if(component.init !== null) {
                // console.log('load js init : ' + component.init);
                component.init();
            }
        });
    } 
}

$(document).ready(function () {
    
    var version = $('body').data('versioning');
    var componentsPath = {
        accordion : {
            css: '/assets/css/components/accordion.css?cdv=' + version,
            js: '/assets/js/lib/accordion.js?cdv=' + version,
            init: function () {
                return starterui.accordion.init();
            }
        },
        video: {
            css: '/assets/css/components/video.css?cdv=' + version,
            js: '/assets/js/components/video.js?cdv=' + version,
            init: function () {
                return starterui.video.init();
            }
        },
        carousel: {
            css: '/assets/css/components/carousel.css?cdv=' + version,
            js: '/assets/js/components/carousel.js?cdv=' + version,
            init: function () {
                return starterui.carousel.init();
            }
        },
        map: {
            css: '/assets/css/components/map.css?cdv=' + version,
            js: '/assets/js/components/maps.js?cdv=' + version,
            init: function () {
                return starterui.maps.init();
            }
        },
        quote: {
            css: '/assets/css/components/blockquote.css?cdv=' + version,
            js: null,
            init: null
        },
        form: {
            css: '/assets/css/components/form.css?cdv=' + version,
            js: '/assets/js/components/form.js?cdv=' + version,
            init: function () {
                return usp.form.init();
            }
        },
        spotlight: {
            css: '/assets/css/components/spotlight.css?cdv=' + version,
            js: null,
            init: null
        },
        event: {
            css: '/assets/css/components/event.css?cdv=' + version,
            js: null,
            init: null
        },
        contextNav: {
            css: '/assets/css/components/context-nav.css?cdv=' + version,
            js: '/assets/js/components/context-nav.js?cdv=' + version,
            init: function () {
                return usp.contextNav.init();
            }
        },
        listingComponents: {
            css: '/assets/css/components/listing.css?cdv=' + version,
            js: '/assets/js/components/listing.js?cdv=' + version,
            init: function () {
                return usp.listing.init();
            }
        },
        cookie: {
            css: '/assets/css/components/cookie.css?cdv=' + version,
            js: '/assets/js/lib/cookie.js?cdv=' + version,
            init: function () {
                return starterui.cookie.init();
            }
        },
        timeline: {
            css: '/assets/css/components/timeline.css?cdv=' + version,
            js: null,
            init: null
        },
        picture: {
            css: null,
            js: '/assets/js/vendors/picturefill.min.js?cdv=' + version,
            init: null
        },
        share: {
            css: null,
            js: '/assets/js/components/share.js?cdv=' + version,
            init: function () {
                return starterui.share.init();
            }
        },
        people: {
            css: '/assets/css/components/people.css?cdv=' + version,
            js: null,
            init: null
        },
        statistics: {
            css: '/assets/css/components/statistics.css?cdv=' + version,
            js: null,
            init: null
        },
        gallery: {
            css: '/assets/css/components/gallery.css?cdv=' + version,
            js: '/assets/js/components/gallery.js?cdv=' + version,
            init: function () {
                return usp.gallery.init();
            }
        },
        calculator: {
            css: '/assets/css/components/calculator.css?cdv=' + version,
            js: '/assets/js/components/calculator.js?cdv=' + version,
            init: function () {
                return usp.calculator.init();
            }
        }
    }

    if ($('.accordion').length > 0 ) loadScripts(componentsPath.accordion); 
    if ($('.video-wrapper').length > 0 ) loadScripts(componentsPath.video);
    if ($('.carousel-wrapper, .mobile-carousel, .desktop-carousel, .mobile-centered-carousel').length > 0 ) loadScripts(componentsPath.carousel);
    if ($('.locations-map').length > 0 ) loadScripts(componentsPath.map);
    if ($('blockquote').length > 0 ) loadScripts(componentsPath.quote);
    if ($('form:not(.exclude-form), .review-headline, .form-description, .points').length > 0)  loadScripts(componentsPath.form);

    if ($('.spotlight, .spotlight__fullwidth, .spotlight-tertiary').length > 0 ) loadScripts(componentsPath.spotlight);
    if ($('.upcoming-events, .event, .event-listing').length > 0 ) loadScripts(componentsPath.event);
    if ($('[data-anchor], [data-doctype="pageCourseDetail"], [data-cta-link]').length > 0 ) loadScripts(componentsPath.contextNav);
    if ($('.listing-page, .related-item').length > 0 ) loadScripts(componentsPath.listingComponents);
    if ($('.cookie-banner').length > 0 ) loadScripts(componentsPath.cookie);
    if ($('.timeline').length > 0 ) loadScripts(componentsPath.timeline);
    if ($('picture').length > 0 ) loadScripts(componentsPath.picture);
    if ($('.social-share').length > 0 ) loadScripts(componentsPath.share);
    if ($('.people').length > 0 ) loadScripts(componentsPath.people);
    if ($('.statistic, .features').length > 0 ) loadScripts(componentsPath.statistics);
    if ($('.gallery').length > 0 ) loadScripts(componentsPath.gallery);
    if ($('.points').length > 0 ) loadScripts(componentsPath.calculator);
    if ($('.desktop-only').css('display') == 'none') {
        $(document).find(".is-hidden").addClass("is--hidden");
    }
	
	var isMobile = false; //initiate as false
	// device detection
	if(/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent) 
		|| /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0,4))) { 
		isMobile = true;
	}
	var wdth = $(window).outerWidth();
	var hi8 = $(window).outerHeight();
	$(window).resize(function() {
		if (isMobile == false) {
			
			//console.log('outw '+window.outerWidth);
			//console.log('w '+$(window).width());
			//console.log('inner '+window.innerWidth);
			if (window.innerWidth > 991.97) {
				//$('ul.menu__primary').css('display', 'flex');
                //$('ul.menu__secondary').css('display', 'flex');
                //alert();
                $('.sidebar-filter__section__head').each(function(){
					$(this).next('.sidebar-filter__section__body.is-hidden').show();
				});
			}
			
			if (window.innerWidth < 991.98) {
				//$('ul.menu__primary').css('display', 'none');
				//$('ul.menu__secondary').css('display', 'none');
				$('.nav-toggle, .menu__primary').removeClass('active');
				$('.sidebar-filter__section__head').each(function(){
					if ($(window).width() != wdth) {
						$(this).next('.sidebar-filter__section__body.is-hidden').hasClass('clicked') ? $(this).next('.sidebar-filter__section__body.is-hidden').show() : $(this).next('.sidebar-filter__section__body.is-hidden').hide();/*($(this).next('.sidebar-filter__section__body.is-hidden').addClass('is--hidden').hide(), $(this).removeClass('oppened'))*/;
					}
					else {
						//alert('sdss');
					}
				});
			}
			if (window.innerWidth > 975 && window.innerWidth < 991.98 ) {
				//console.log('asnais');
				$('.menu__primary').hide();
			}
		
		}
		else {
            //alert();
			//alert('mobile');
			//console.log($(this).width());
			//console.log($(this).height());
			console.log(wdth);
			console.log(hi8);
			if ($(window).width() < 991){
				//alert('mob');
				$('.sidebar-filter__section__head').each(function(){
					
					if ($(window).width() != wdth) {
						$(this).next('.sidebar-filter__section__body.is-hidden').hasClass('clicked') ? $(this).next('.sidebar-filter__section__body.is-hidden').show() : $(this).next('.sidebar-filter__section__body.is-hidden').hide();/*($(this).next('.sidebar-filter__section__body.is-hidden').addClass('is--hidden').hide(), $(this).removeClass('oppened'))*/;
					}
					else {
						//alert('sdss');
						
					}
					
				});
				hi8 = $(window).height();
				wdth = $(window).width();
			}
			else {
				//alert('desk');
				$('.sidebar-filter__section__head').each(function(){
					$(this).next('.sidebar-filter__section__body.is-hidden').show();
				});
				wdth = $(window).width();
				hi8 = $(window).height();
			}
		}		
    });
});


/*var width = $(window).innerWidth();
var width = $(window).innerHeight();
$(window).on('resize', function() {
  if ($(this).width() != width) {
    width = $(this).width();
    console.log(width);
  }
});*/
/******************* Jira issue 478****************/
function gotothat(e, stckh8){
	var hrehf = $(e).attr('lnksh'), mqn = 0;
	hrehf = hrehf.replace('#', '');
	event.preventDefault();
	var stckh8 = $('div#sticky-wrapper').height()+62;
	console.log(stckh8);
	$('html, body').css({'scroll-behavior': 'auto'});
	
	console.log("href "+hrehf);
	console.log("mqn '"+mqn);
	$('body, html').animate({
		scrollTop: $("a[name='"+hrehf+"']").offset().top-stckh8
	}, 1000, function(){
		$('html, body').css('scroll-behavior', 'smooth');
	});
}

/******************* Jira issue 478****************/

/******************* Jira issue 555****************/
$(window).on('load', function(){
    setTimeout(function(){
        $('.highlight.success.msg, .highlight.error').slideUp('slow');
    }, 5000);
});