/*globals Cookies, starterui, baseUrl, LazyLoad*/
window.starterui = window.starterui || {};
window.starterui.table = window.starterui.table || {};

window.starterui.table = (function ($) {
    var model,

       
        /**
         * Init class to initiate script
         * @public
         * @param {object} args is from doc-ready file
         */
        init = function (args) {
            args = args || {};
            model = {
                init: function () {
                   this.table = args.table || 'table';
                }
            };

            model.init();
            
            // $.getScript(baseUrl + "/assets/js/vendors/scrollbar.js")
            // .done(function( script, textStatus ) {
            //     $('.table_scroller').overlayScrollbars();
            // });

            if ($(model.table).length > 0 ) {
                // $(model.table).removeAttr('style').removeAttr('border');
                // $(model.table).find('tr').removeAttr('style');
                // $(model.table).find('td').removeAttr('style');
                $('html, html[class^="mobile"] > body').addClass('ancasci');
            }
            $(model.table).each(function(){
                if ($(this).find('thead').length > 0 ) {
                    //alert('ache');
                    $(this).find('thead tr td:first-child').css({
                        'border-top-left-radius': '0'
                    });
                    $(this).find('thead tr td:last-child').css({
                        'border-top-right-radius': '0'
                    });
                }
                else {
                    //alert('nahi hai');
                    $(this).find('> tbody > tr:first-child td:first-child').css({
                        'border-top-left-radius': '0.9375rem'
                    });
                    $(this).find('> tbody > tr:first-child td:last-child').css({
                        'border-top-right-radius': '0.9375rem'
                    });
                    $(this).find('> tbody > tr:first-child td').css('border-top', '0');
                }
                $(this).wrap('<div class="table_scroller" style="overflow:auto;padding: 0 2px;"></div>');
            });
            
        };

    /**
     * Returns init
     * @public
     */
    return {
        init: init

    };
}(jQuery));

$(window).on('load resize', function(){
    //alert();
	//console.log($('.table_scroller').prop('scrollWidth'));
	//console.log($('.table_scroller').width());
	$('.table_scroller').each(function(){
		if (($(this).prop('scrollWidth') - $(this).width()) > 5 ) {
			$(this).overlayScrollbars({
				className       : "os-theme-round-dark",
				sizeAutoCapable : true,
				paddingAbsolute : true,
				scrollbars : {
					clickScrolling : true
				}
			});
		}
	});
});
