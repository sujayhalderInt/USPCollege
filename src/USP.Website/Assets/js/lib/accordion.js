window.starterui = window.starterui || {};
window.starterui.accordion = window.starterui.accordion || {};

window.starterui.accordion = (function ($) {
	var model,
	/** 
	 * open and scroll to accordion item
	 * @param {object} e is event
	 */
	
    accordionToggle = function (e) {
		e.preventDefault();
		var accParent = $(this).parent();
		if ($(this).hasClass('active')) {
			$(this).next().slideUp();
			$(this).removeClass('active');
		} else {
			accParent.find('.accordion__title').removeClass('active');
			//accTitle.removeClass('active');
			$(this).addClass('active');
			accParent.find('.accordion__content').slideUp();
			$(this).next().slideDown();

			setTimeout(function(){
				var containerPos = accParent.find('.accordion__title.active').offset().top - 150;
				$('html, body').animate({ scrollTop: containerPos }, 600);
			},400);
		}
		return false;
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
					this.container = args.container || '.accordion';
					this.accordionTitle = args.accordionTitle || '.accordion__title';
        }
      };
      model.init();
			// console.log('init accordion');
			
			$(document).on('click', model.accordionTitle, accordionToggle);

    };

  /**
   * Returns init
   * @return public methods/functions
   * @public
   */
  return {
    init: init
  };

}(jQuery));