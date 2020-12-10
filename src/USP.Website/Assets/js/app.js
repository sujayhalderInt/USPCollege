/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, {
/******/ 				configurable: false,
/******/ 				enumerable: true,
/******/ 				get: getter
/******/ 			});
/******/ 		}
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(1);


/***/ }),
/* 1 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


__webpack_require__(2);

__webpack_require__(3);

__webpack_require__(4);

__webpack_require__(5);

__webpack_require__(6);

__webpack_require__(7);

window.baseUrl = '';
if (window.location.hostname == 'templates.precedenthost.co.uk') {
    window.baseUrl = '/USP';
}

//import './components/maps';


starterui.viewport();
starterui.lazy.init();
starterui.nav.init();
starterui.table.init();
starterui.flyingFocus.init();
usp.predictive.init();
// starterui.maps.init();

/***/ }),
/* 2 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


window.starterui = window.starterui || {};
window.starterui.viewport = window.starterui.viewport || {};

/* VIEWPORT */
// Classes and viewport values based on Bootstrap
window.starterui.viewport = function () {
    var width = $(window).width(),
        viewport = 'default';
    if (width >= 1200) {
        viewport = { vpClass: 'desktop-extra-large', vpSize: 1200 };
    }
    if (width <= 1199) {
        viewport = { vpClass: 'desktop-large', vpSize: 1199 };
    }
    if (width <= 991) {
        viewport = { vpClass: 'tablet-medium', vpSize: 991 };
    }
    if (width <= 767) {
        viewport = { vpClass: 'mobile-small', vpSize: 767 };
    }
    if (width <= 575) {
        viewport = { vpClass: 'mobile-extra-small', vpSize: 575 };
    }

    $('html').removeClass('default desktop-extra-large desktop-large tablet-medium mobile-small mobile-extra-small').addClass(viewport.vpClass);

    return viewport;
};

/***/ }),
/* 3 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/*globals Cookies, starterui, baseUrl, LazyLoad*/
window.starterui = window.starterui || {};
window.starterui.lazy = window.starterui.lazy || {};

window.starterui.lazy = function ($) {
    var model,


    /**
     * Init class to initiate script
     * @public
     * @param {object} args is from doc-ready file
     */
    init = function init(args) {
        args = args || {};
        model = {
            init: function init() {
                this.lazy = args.lazy || '.lazy';
            }
        };

        model.init();

        if ($(model.lazy).length > 0) {
            $.getScript(baseUrl + '/assets/js/vendors/lazyload.min.js').done(function (script, textStatus) {
                var myLazyLoad = new LazyLoad({
                    elements_selector: model.lazy
                });
            });
        }
    };

    /**
     * Returns init
     * @public
     */
    return {
        init: init

    };
}(jQuery);

/***/ }),
/* 4 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/*globals Cookies, starterui*/
window.starterui = window.starterui || {};
window.starterui.nav = window.starterui.nav || {};

window.starterui.nav = function ($) {
    var model,
        janala,
        initGlobalSearch = function initGlobalSearch() {

        $(model.globalSearchClose).on('click', function (e) {
            e.preventDefault();

            $(model.globalSearch).removeClass(model.active);
            $(model.globalSearch).slideUp(function () {
                setTimeout(function () {
                    $(model.globalSearch).removeClass(model.active);
                }, 500);
            });
        });

        $(model.globalSearchBtn).on('click', function (e) {
            e.preventDefault();
            $(model.globalSearch).slideDown(function () {
                setTimeout(function () {
                    $(model.globalSearch).addClass(model.active);
                }, 500);
            });
        });

        $(model.globalSearch).on('change', 'select', function (e) {
            e.preventDefault();
            $(model.globalSearchForm).attr('action', $(this).val());
        });
    },
        openMobileNav = function openMobileNav() {
        $(model.navPrimaryBtn).on('click', function (e) {
            e.preventDefault();

            if ($(model.primaryLevelNav).hasClass(model.active)) {

                $(model.navPrimaryBtn).removeClass(model.active);

                $(model.primaryLevelNav).slideUp(function () {
                    setTimeout(function () {
                        $(model.primaryLevelNav).removeClass(model.active);
                    }, 500);
                });

                $(model.secondaryLevelNav).slideUp(function () {
                    setTimeout(function () {
                        $(model.secondaryLevelNav).removeClass(model.active);
                    }, 500);
                });

                setTimeout(function () {
                    $(model.primaryLevelNav).removeClass('minify');
                    $(model.secondaryLevelNav).removeClass('minify');
                    $(model.dropdownMenu).removeAttr('style');
                    $('.has-dropdown').removeClass(model.oppened);
                    $(model.tertiaryLevelNavItem).removeClass(model.active);

                    $(model.tertiaryLevelNavItem).find('ul').slideUp();
                }, 300);
            } else {
                $(model.navPrimaryBtn).addClass(model.active);

                $(model.primaryLevelNav).slideDown(function () {
                    setTimeout(function () {
                        $(model.primaryLevelNav).addClass(model.active);
                    }, 500);
                });

                $(model.secondaryLevelNav).slideDown(function () {
                    setTimeout(function () {
                        $(model.secondaryLevelNav).addClass(model.active);
                    }, 500);
                });
            }
        });
    },
        openSecondaryNav = function openSecondaryNav() {
        $(model.primaryLevelNavItem).on('click', function (e) {
            e.preventDefault();

            $(model.primaryLevelNav).addClass('minify');
            $(model.secondaryLevelNav).addClass('minify');

            var elem = $(this).parent();
            $('.has-dropdown').removeClass(model.oppened);
            $(model.dropdownMenu).removeAttr('style');

            setTimeout(function () {
                $(elem).addClass(model.oppened);
                elem.find(model.dropdownMenu).css('height', getHeight());
            }, 300);
        });
    },
        openTertiaryNav = function openTertiaryNav() {
        $(model.tertiaryLevelNavItem).on('click', function (e) {
            // e.preventDefault();

            var submenu = $(this);
            // $(model.tertiaryLevelNavItem).removeClass(model.active);
            // $(model.tertiaryLevelNavItem).find('ul').slideUp(function () {
            //     setTimeout(function () {
            //         submenu.removeClass(model.active);
            //     }, 500);
            // });

            if (submenu.hasClass(model.active)) {
                submenu.removeClass('is--active');
                submenu.find('ul').slideUp(function () {
                    setTimeout(function () {
                        submenu.removeClass(model.active);
                    }, 500);
                });
            } else {
                submenu.find('ul').slideDown(function () {
                    submenu.addClass('is--active');
                    setTimeout(function () {
                        submenu.addClass(model.active);
                    }, 500);
                });
            }
        });
    },
        setActiveItem = function setActiveItem() {
        var path = $(model.pathToId).data('nav-path');
        $('a[data-nav-id]').each(function () {
            var $this = $(this),
                navId = $this.data('nav-id');
            if (path.lastIndexOf(navId) > -1) {
                $this.addClass('active');
            }
        });
    },
        getHeight = function getHeight() {
        return $(model.primaryLevelNav).outerHeight() + $(model.secondaryLevelNav).outerHeight();
    },
        socialShare = function socialShare() {
        $(model.socialShare).on('click', 'a', function (e) {
            e.preventDefault();
        });
    },

    /***************jira issue 538fix start**************/
    initNav = function initNav() {
        if ($(window).outerWidth() < 991.98) {
            openMobileNav();
            openSecondaryNav();
            openTertiaryNav();
            janala = 'mob';
        } else {
            janala = 'desk';
            $(model.primaryLevelNavItem).unbind('click');
            $(model.dropdownMenu).removeAttr('style');
            $(model.desktopDropdown).on('click', '>a', function (e) {
                if ($(window).outerWidth() > 991.97) {
                    e.preventDefault();
                    var dropdown = $(this).closest(model.desktopDropdown);
                    if (dropdown.hasClass(model.isVisible)) {
                        dropdown.removeClass(model.isVisible);
                        //console.log('1');
                    } else {
                        $(model.desktopDropdown).removeClass(model.isVisible);
                        dropdown.addClass(model.isVisible);
                        //console.log('1 else');
                    }
                }
            });

            $(window).on('click', function (e) {
                if ($(e.target).closest(model.desktopDropdown).length != 1) {
                    $(model.desktopDropdown).removeClass(model.isVisible);
                    //alert(e.target.html());
                }
            });
        }
    },

    /***************jira issue 538fix end**************/
    /**
     * Init class to initiate script
     * @public
     * @param {object} args is from doc-ready init file
     */
    init = function init(args) {
        args = args || {};
        model = {
            init: function init() {
                this.navPrimaryBtn = args.navPrimaryBtn || '.nav-toggle';
                this.primaryLevelNav = model.primaryLevelNav || '.menu__primary';
                this.secondaryLevelNav = model.secondaryLevelNav || '.menu__secondary';
                this.dropdownMenu = args.dropdownMenu || '.menu__dropdown';
                this.active = args.active || 'active';
                this.oppened = args.oppened || 'oppened';
                this.desktopDropdown = args.desktopDropdown || '.has-dropdown';
                this.isVisible = args.isVisible || 'is--visible';

                this.primaryLevelNavItem = model.primaryLevelNavItem || '.menu__primary > li.has-dropdown > a';
                this.tertiaryLevelNavItem = model.tertiaryLevelNavItem || '.menu__primary > li.has-dropdown .menu__dropdown__part > ul > li.dropdown';
                this.pathToId = args.pathToId || '[data-nav-path]';
                this.socialShare = args.socialShare || '.social-share';

                this.globalSearch = args.globalSearch || '.header-search';
                this.globalSearchBtn = args.globalSearchBtn || '.header-search-btn';
                this.globalSearchClose = args.globalSearchClose || '.header-search-close';

                //   this.glabalSearch = args.globalSearch || '.global-search';
                this.globalSearchResultPage = args.globalSearchResultPage || model.globalSearch + ' select';
                this.globalSearchForm = args.globalSearchFrom || model.globalSearch + ' form';
            }
        };
        model.init();
        initNav();

        var acbt = true,
            par,
            isMobile = false;
        // device detection
        if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent) || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) {
            isMobile = true;
        }
        if (/^((?!chrome|android).)*safari/i.test(navigator.userAgent) && isMobile == false) {
            par = 1006;
        } else {
            par = 991.97;
        }

        $(document).ready(function () {
            if (/^((?!chrome|android).)*safari/i.test(navigator.userAgent) && $(window).outerWidth() == par) {
                $('header.header nav.menu').addClass('maxl');
            }
        });
        /***************jira issue 538fix start**************/
        $(window).on('resize', function () {
            acbt = true;
            //alert();
            //console.log('doc '+ $(window).outerWidth() );
            //console.log('normal '+ $(window).width() );
            //console.log('outer '+ window.outerWidth );
            var widnbo = $(window).outerWidth();

            if (widnbo >= par) {
                $(model.primaryLevelNavItem).unbind('click');
                $(model.dropdownMenu).removeAttr('style');
                $('.menu__primary').css('display', 'flex');
                $('.menu__secondary').css('display', 'flex');
                if (/^((?!chrome|android).)*safari/i.test(navigator.userAgent) && widnbo == par) {
                    $('header.header nav.menu').addClass('maxl');
                } else {
                    $('header.header nav.menu').removeClass('maxl');
                }
            } else {
                $('header.header nav.menu').removeClass('maxl');
                openMobileNav();
                openSecondaryNav();
                openTertiaryNav();
                if (isMobile == false) {
                    $('.menu__primary').css('display', 'none');
                    $('.menu__secondary').css('display', 'none');
                } else {
                    if ($('a.nav-toggle').hasClass('clicked')) {
                        $('.menu__primary').css('display', 'block');
                        $('.menu__secondary').css('display', 'block');
                    } else {
                        $('.menu__primary').css('display', 'none');
                        $('.menu__secondary').css('display', 'none');
                    }
                }
            }

            $('ul.menu__primary > li[role="menuitem"] > a').click(function (w) {
                if (widnbo > par) {
                    w.preventDefault();
                    var dropdown = $(this).closest(model.desktopDropdown);

                    if (dropdown.hasClass(model.isVisible)) {
                        janala == 'mob' ? dropdown.removeClass(model.isVisible) : '';
                        //console.log('2');
                    } else {
                        //$(model.desktopDropdown).removeClass(model.isVisible);
                        //dropdown.addClass(model.isVisible);
                        //dropdown.addClass('ankan');
                        janala == 'mob' ? ($(model.desktopDropdown).removeClass(model.isVisible), dropdown.addClass(model.isVisible)) : '';
                        //console.log('2 else');
                    }
                    w.preventDefault();
                }
            });
        });
        $('a.nav-toggle').click(function (p) {
            $(this).toggleClass('clicked');
            p.preventDefault();
        });
        /***************jira issue 538fix end*************/
        setActiveItem();
        socialShare();

        initGlobalSearch();
    };

    /**
     * Returns init
     * @return public methods/functions
     * @public
     */
    return {
        init: init
    };
}(jQuery);

/*$(window).scroll(function(){
	if ($('.menu__primary').hasClass('active') ) {
		$('.menu__primary').show();
		$('.menu__secondary').show();
	}
});*/

/***/ }),
/* 5 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/*globals Cookies, starterui, baseUrl, LazyLoad*/
window.starterui = window.starterui || {};
window.starterui.table = window.starterui.table || {};

window.starterui.table = function ($) {
    var model,


    /**
     * Init class to initiate script
     * @public
     * @param {object} args is from doc-ready file
     */
    init = function init(args) {
        args = args || {};
        model = {
            init: function init() {
                this.table = args.table || 'table';
            }
        };

        model.init();

        // $.getScript(baseUrl + "/assets/js/vendors/scrollbar.js")
        // .done(function( script, textStatus ) {
        //     $('.table_scroller').overlayScrollbars();
        // });

        if ($(model.table).length > 0) {
            // $(model.table).removeAttr('style').removeAttr('border');
            // $(model.table).find('tr').removeAttr('style');
            // $(model.table).find('td').removeAttr('style');
            $('html, html[class^="mobile"] > body').addClass('ancasci');
        }
        $(model.table).each(function () {
            if ($(this).find('thead').length > 0) {
                //alert('ache');
                $(this).find('thead tr td:first-child').css({
                    'border-top-left-radius': '0'
                });
                $(this).find('thead tr td:last-child').css({
                    'border-top-right-radius': '0'
                });
            } else {
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
}(jQuery);

$(window).on('load resize', function () {
    //alert();
    //console.log($('.table_scroller').prop('scrollWidth'));
    //console.log($('.table_scroller').width());
    $('.table_scroller').each(function () {
        if ($(this).prop('scrollWidth') - $(this).width() > 5) {
            $(this).overlayScrollbars({
                className: 'os-theme-round-dark',
                sizeAutoCapable: true,
                paddingAbsolute: true,
                scrollbars: {
                    clickScrolling: true
                }
            });
        }
    });
});

/***/ }),
/* 6 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/*globals Cookies, starterui, baseUrl, LazyLoad*/
window.starterui = window.starterui || {};
window.starterui.flyingFocus = window.starterui.flyingFocus || {};

window.starterui.flyingFocus = function ($) {
    var model,
        initFlyingFocus = function initFlyingFocus() {

        var DURATION = 150;

        var ringElem = null;
        var movingId = 0;
        var prevFocused = null;
        var keyDownTime = 0;

        var win = window;
        var doc = document;
        var docElem = doc.documentElement;
        var body = doc.body;

        docElem.addEventListener('keydown', function (event) {
            var code = event.which;
            // Show animation only upon Tab or Arrow keys press.
            if (code === 9 || code > 36 && code < 41) {
                keyDownTime = Date.now();
            }
        }, false);

        docElem.addEventListener('focus', function (event) {
            var target = event.target;
            if (target.id === 'flying-focus') {
                return;
            }

            var isFirstFocus = false;
            if (!ringElem) {
                isFirstFocus = true;
                initialize();
            }

            var offset = offsetOf(target);
            ringElem.style.left = offset.left + 'px';
            ringElem.style.top = offset.top + 'px';
            ringElem.style.width = target.offsetWidth + 'px';
            ringElem.style.height = target.offsetHeight + 'px';

            if (isFirstFocus || !isJustPressed()) {
                return;
            }

            onEnd();
            target.classList.add('flying-focus_target');
            ringElem.classList.add('flying-focus_visible');
            prevFocused = target;
            movingId = setTimeout(onEnd, DURATION);
        }, true);

        docElem.addEventListener('blur', function () {
            onEnd();
        }, true);

        function initialize() {
            ringElem = doc.createElement('flying-focus'); // use uniq element name to decrease the chances of a conflict with website styles
            ringElem.id = 'flying-focus';
            ringElem.style.transitionDuration = ringElem.style.WebkitTransitionDuration = DURATION / 1000 + 's';
            body.appendChild(ringElem);
        }

        function onEnd() {
            if (!movingId) {
                return;
            }
            clearTimeout(movingId);
            movingId = 0;
            //USP-1_08/12/2020 Begin
            //ringElem.classList.remove('flying-focus_visible');
            //USP-1_08/12/2020 End
            prevFocused.classList.remove('flying-focus_target');           
            prevFocused = null;
        }

        function isJustPressed() {
            return Date.now() - keyDownTime < 42;
        }

        function offsetOf(elem) {
            var rect = elem.getBoundingClientRect();
            var clientLeft = docElem.clientLeft || body.clientLeft;
            var clientTop = docElem.clientTop || body.clientTop;
            var scrollLeft = win.pageXOffset || docElem.scrollLeft || body.scrollLeft;
            var scrollTop = win.pageYOffset || docElem.scrollTop || body.scrollTop;
            var left = rect.left + scrollLeft - clientLeft;
            var top = rect.top + scrollTop - clientTop;
            return {
                top: top || 0,
                left: left || 0
            };
        }
    },

    /**
     * Init class to initiate script
     * @public
     * @param {object} args is from doc-ready file
     */
    init = function init(args) {
        args = args || {};
        model = {
            init: function init() {}
        };

        model.init();
        initFlyingFocus();
    };

    /**
     * Returns init
     * @public
     */
    return {
        init: init

    };
}(jQuery);

/***/ }),
/* 7 */
/***/ (function(module, exports, __webpack_require__) {

"use strict";


/*globals Cookies, starterui, baseUrl, LazyLoad*/
window.usp = window.usp || {};
window.usp.predictive = window.usp.predictive || {};

window.usp.predictive = function ($) {
    var model,
        typingTimer,
        initPredictive = function initPredictive() {
        $(model.searchBox).on('keyup', function () {
            var $this = $(this),
                characters = $this.val().length,
                inputSearchBox = $this.closest(model.search),
                predictiveBox = inputSearchBox.siblings(model.predictive);

            clearTimeout(typingTimer);
            typingTimer = setTimeout(function () {
                if (characters >= 1) {
                    populatePredictive($this);
                } else {
                    $(predictiveBox).addClass(model.hidden);
                }
            }, 350);
        });
        $(window).on('click', function (e) {
            if ($(e.target).closest(model.searchBox).length != 1) {
                $(model.predictive).addClass(model.hidden);
            }
        });
    },
        createLines = function createLines(data) {
        var template = '';

        $.each(data, function (i, content) {
            template += '<a href="' + content.ItemUrl + '">' + content.Heading + '<span>' + content.ResultType + '</span></a>';
        });

        return template;
    },
        populatePredictive = function populatePredictive(input) {

        var chars = input.val(),
            inputSearchBox = input.closest(model.search),
            predictiveBox = inputSearchBox.siblings(model.predictive),
            url = input.data('predictive') + '?term=' + chars;

        // test porposes
        // url = input.data('predictive');

        $.ajax({
            type: "GET",
            url: url,
            dataType: 'json',
            success: function success(data) {

                var template = createLines(data);
                $(predictiveBox).html(template);
                if (template != '') {
                    $(predictiveBox).removeClass(model.hidden);
                }

                console.log('success');
            },
            error: function error(jqxhr, textStatus, errorThrown) {
                console.log('error');
            }
        });
    },

    /**
    * Init class to initiate script
    * @public
    * @param {object} args is from doc-ready file
    */
    init = function init(args) {
        args = args || {};
        model = {
            init: function init() {
                this.search = args.search || '.search';
                this.searchBox = args.searchBox || '.search input[data-predictive]';
                this.predictive = args.predictive || '.hero-search__results';
                this.hidden = args.hidden || 'hidden';
            }
        };

        model.init();

        console.log('Predictive Initiaized');
        initPredictive();
    };

    /**
     * Returns init
     * @public
     */
    return {
        init: init

    };
}(jQuery);

/***/ })
/******/ ]);