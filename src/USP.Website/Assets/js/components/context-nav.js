window.usp = window.usp || {};
window.usp.contextNav = window.usp.contextNav || {};

window.usp.contextNav = (function ($) {
    var model,
        closeSticky = function () {
            $(model.tagWrapper).on('click', 'a', function (e) {
                $('#sticky-wrapper').removeClass('open-context');
            });
        },
        linkGoToBtn = function () {
            $(document).on('click', model.goTo, function (e) {
                e.preventDefault();
                $('#sticky-wrapper').toggleClass('open-context');
                closeSticky();
            });
        },
        addScrollSpy = function () {
			// Cache selectors
			var lastId,
				topMenu = $(model.tagWrapper),
				topMenuHeight = topMenu.outerHeight() + 15,
				// All list items
				menuItems = topMenu.find('a[href*="#"]').not('[href="#"]'),
				// Anchors corresponding to menu items
				scrollItems = menuItems.map(function () {
					var item = $(this).attr('href');
					if (item.length) {
						return item;
					}
                });
                
                // console.log(menuItems.length, scrollItems);

			// Bind to scroll
			$(window).scroll(function () {
				// Get container scroll position
				var fromTop = $(this).scrollTop() + topMenuHeight + 5;

				// Get id of current scroll item
				var cur = scrollItems.map(function (item, content) {
                    // console.log(item, content);
					if ($('[name="' + content.replace(/#/g, '') + '"]').offset().top < fromTop) {
						return this;
					}
                });
                // Get the id of the current element
                cur = cur[cur.length - 1];
                if (cur != undefined) {
                    var data = $('[name="' + cur.replace(/#/g, '') + '"]').data('anchor');
                    if (  $(model.stickyText).html != data ){
                        $(model.stickyText).html(data);
                    }
                }
                

			});
		},
        stickyContentNav = function () {
            $(model.contextNavSection).sticky({topSpacing:0});
            linkGoToBtn();
        },
        makeContextNav = function () {
            $(model.contextNavItems).each( function(item, content) {
                var template = '',
                id = 'anchor' + $(content).data('anchor').toString().replace(/\s/g, '');

                $(content).attr('name', id);
                template = '<p><a onclick="gotothat(this)" href="javascript:void(0)" lnksh="#' + id + '">' +
                            $(content).data('anchor') + '</a></p>';
                $(template).appendTo(model.tagWrapper);
                
            });

            addScrollSpy();

            $.getScript(baseUrl + "/assets/js/vendors/jquery.sticky.js")
            .done(function( script, textStatus ) {
                stickyContentNav();
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
                    this.contextNavSection = args.contextNavSection || '.context-nav-wrapper';
                    this.contextNavWrapper = args.contextNavWrapper || '.context-nav';
                    this.tagWrapper = args.tagWrapper || '.tag-wrapper';
                    this.contextNavItems = args.contextNavItems || '[data-anchor]';
                    this.stickyText = args.stickyText || '.sticky-context-nav__item';
                    this.goTo = args.goTo || '.sticky-context-nav__goto';
                }
            };
            model.init();

            var template = '<section class="bg-usp-navi-light context-nav-wrapper">' +
            '<div class="container"><div class="row"><div class="col-12">' +
            '<div class="sticky-context-nav"><p class="sticky-context-nav__item mb-0"> </p>' +
            '<p class="mb-0"><a href="#" class="sticky-context-nav__goto">Go to <i class="icon-arrow" aria-hidden="true"></i></a></p>' +
            '</div><div class="context-nav"><h3 class="mb-20"></h3>' +
            '<div class="tag-wrapper" id="nasjkn"> </div>';

            if ($('[data-cta-link]').length != 0) {
                template += '<p><a href="' + $('[data-cta-link]').data('cta-link') + '" class="btn btn--animated btn--white"' + $('[data-cta-newwindow]').data('cta-newwindow') + '>' +
                '<span class="wrapper"> '+ $('[data-cta-text]').data('cta-text') +'</span></a></p>';
            } else if (($('[data-doctype="pageCourseDetail"] .btn--apply').length != 0) || ($('[data-doctype="pageCareerDetail"] .btn--apply').length != 0)) {
                template += '<p><a href="' + $('.btn--apply').attr('href') + '" class="btn btn--animated btn--white"' + $('[data-cta-newwindow]').data('cta-newwindow') + '>' +
                    '<span class="wrapper"> ' + $('.btn--apply').parent().text() + '</span></a></p>';
            }
            
            template += '</div> </div></div> </div></section>';

            if ($('.course-banner').length > 0) {
                $(template).insertAfter( '.course-banner' );
            } else {
                $(template).insertAfter( 'main > header' );
            }
            makeContextNav();
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

