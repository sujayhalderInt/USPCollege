window.starterui = window.starterui || {};
window.starterui.share = window.starterui.share || {};

window.starterui.share = (function ($) {
    var model,
        /**
         * Init class to initiate script
         * @public
         * @param {object} args is from doc-ready init file
         */
        init = function (args) {
            args = args || {};
            model = {
                init: function () {
                    this.share = args.share || '.social-share';
                }
            };
            model.init();

            var key = $(model.share).data('key');
            $.getScript('//s7.addthis.com/js/300/addthis_widget.js#pubid=' + key);
            
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

$(document).ready(function(){
	$(".social-share").on('click',function (e){
		if (window.outerWidth < 1024) {
			
			if ($(this).find('> a > .icon-share').hasClass('icon-close')){
				$(this).find('> a > .icon-share').removeClass('icon-close');
				$(this).find('.share-wrapper').css('display','none');
			}
			else {
				$(this).find('> a > .icon-share').addClass('icon-close');
				$(this).find('.share-wrapper').css('display','block');
			}
			/*if (!container.is(e.target) && container.has(e.target).length !== 0)  {
				$('.hero-banner .social-share .share-wrapper').css('display','block');
				alert('vitore');
			}				
			else {
				$('.hero-banner .social-share .share-wrapper').css('display','none');
				alert('baire');
				
			}
			//alert()*/
		}
	});
	
});