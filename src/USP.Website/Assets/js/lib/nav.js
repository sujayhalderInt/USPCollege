/*globals Cookies, starterui*/
window.starterui = window.starterui || {};
window.starterui.nav = window.starterui.nav || {};

window.starterui.nav = (function ($) {
  var model,janala,
  initGlobalSearch = function () {
   
    $(model.globalSearchClose).on('click', function (e) {
        e.preventDefault();

        $(model.globalSearch).removeClass(model.active);
        $(model.globalSearch).slideUp(function () {
            setTimeout(function () {
                $(model.globalSearch).removeClass(model.active);
            }, 500);
        });
    });
        
    $(model.globalSearchBtn).on('click', function (e)  {
        e.preventDefault();
        $(model.globalSearch).slideDown(function() {
            setTimeout(function () {
                $(model.globalSearch).addClass(model.active);
            }, 500);
        });
    });

    $(model.globalSearch).on('change', 'select', function (e) {
        e.preventDefault();
        $(model.globalSearchForm).attr('action', $(this).val());
    })

  },
  openMobileNav = function () {
    $(model.navPrimaryBtn).on('click', function(e) {
        e.preventDefault();

        if ($(model.primaryLevelNav).hasClass(model.active) ) {

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

            setTimeout(function(){ 
                $(model.primaryLevelNav).removeClass('minify');
                $(model.secondaryLevelNav).removeClass('minify');
                $(model.dropdownMenu).removeAttr('style');
                $('.has-dropdown').removeClass(model.oppened);
                $(model.tertiaryLevelNavItem).removeClass(model.active);
               
                $(model.tertiaryLevelNavItem).find('ul').slideUp();

             }, 300);

            
        
        } else {
            $(model.navPrimaryBtn).addClass(model.active);

            $(model.primaryLevelNav).slideDown(function() {
                setTimeout(function () {
                    $(model.primaryLevelNav).addClass(model.active);
                }, 500);
            });

            $(model.secondaryLevelNav).slideDown(function() {
                setTimeout(function () {
                    $(model.secondaryLevelNav).addClass(model.active);
                }, 500);
            });
        }
    });
  },
  openSecondaryNav = function () {
    $(model.primaryLevelNavItem).on('click', function(e) {
        e.preventDefault();

        $(model.primaryLevelNav).addClass('minify');
        $(model.secondaryLevelNav).addClass('minify');

        var elem = $(this).parent();
        $('.has-dropdown').removeClass(model.oppened);
        $(model.dropdownMenu).removeAttr('style');

        setTimeout(function(){ 
            $(elem).addClass(model.oppened);
            elem.find(model.dropdownMenu).css('height', getHeight());
         }, 300);
    });
  },
  openTertiaryNav = function () {
    $(model.tertiaryLevelNavItem).on('click', function(e) {
        // e.preventDefault();

        var submenu = $(this);
        // $(model.tertiaryLevelNavItem).removeClass(model.active);
        // $(model.tertiaryLevelNavItem).find('ul').slideUp(function () {
        //     setTimeout(function () {
        //         submenu.removeClass(model.active);
        //     }, 500);
        // });

        if (submenu.hasClass(model.active) ) {
            submenu.removeClass('is--active');
            submenu.find('ul').slideUp(function () {
                setTimeout(function () {
                    submenu.removeClass(model.active);
                }, 500);
            });
        
        } else {
            submenu.find('ul').slideDown(function() {
                submenu.addClass('is--active');
                setTimeout(function () {
                    submenu.addClass(model.active);
                }, 500);
            });
        }
      });
  },
  setActiveItem = function () {
    var path = $(model.pathToId).data('nav-path');
    $('a[data-nav-id]').each(function () {
        var $this = $(this),
        navId = $this.data('nav-id');
        if (path.lastIndexOf(navId) > -1) {
            $this.addClass('active');
        }
    });
  },
  getHeight = function () {
      return $(model.primaryLevelNav).outerHeight() + $(model.secondaryLevelNav).outerHeight();
  },
  socialShare = function () {
      $(model.socialShare).on('click','a', function (e) {
        e.preventDefault();
      });
  },
  /***************jira issue 538fix start**************/
  initNav = function () {
    if ($(window).outerWidth() < 991.98) {
        openMobileNav();
        openSecondaryNav();
        openTertiaryNav();
        janala = 'mob';
      } else {
          janala = 'desk';
          $(model.primaryLevelNavItem).unbind('click');
          $(model.dropdownMenu).removeAttr('style');
          $(model.desktopDropdown).on('click','>a', function (e) {
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

          $(window).on('click', function (e){
             if ( $(e.target).closest(model.desktopDropdown).length != 1) {
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
    init = function (args) {
      args = args || {};
      model = {
        init: function () {
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
	  
	  
	  var acbt = true, par, isMobile = false;
	  // device detection
		if(/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent) 
			|| /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0,4))) { 
			isMobile = true;
		}
		if ( /^((?!chrome|android).)*safari/i.test(navigator.userAgent) && isMobile == false) {
			par = 1006;
		}
		else {
			par = 991.97;
		}
		
	$(document).ready(function(){
		if ( /^((?!chrome|android).)*safari/i.test(navigator.userAgent) && $(window).outerWidth() == par) {
			$('header.header nav.menu').addClass('maxl');
		}
	})
      /***************jira issue 538fix start**************/
      $(window).on('resize', function () {
		  acbt = true;
		 //alert();
        //console.log('doc '+ $(window).outerWidth() );
		//console.log('normal '+ $(window).width() );
		//console.log('outer '+ window.outerWidth );
		var widnbo = $(window).outerWidth();
		
		if(widnbo >= par ) {
            $(model.primaryLevelNavItem).unbind('click');
            $(model.dropdownMenu).removeAttr('style');
			$('.menu__primary').css('display', 'flex');
			$('.menu__secondary').css('display', 'flex');
			if ( /^((?!chrome|android).)*safari/i.test(navigator.userAgent) && widnbo == par) {
				$('header.header nav.menu').addClass('maxl');
			}
			else {
				$('header.header nav.menu').removeClass('maxl');
			}
        }
		
        else {
			$('header.header nav.menu').removeClass('maxl');
            openMobileNav();
            openSecondaryNav();
            openTertiaryNav();
			if (isMobile == false) {
				$('.menu__primary').css('display', 'none');
				$('.menu__secondary').css('display', 'none');
			}
			else {
				if ($('a.nav-toggle').hasClass('clicked')){
					$('.menu__primary').css('display', 'block');
					$('.menu__secondary').css('display', 'block');
				}
				else {
					$('.menu__primary').css('display', 'none');
					$('.menu__secondary').css('display', 'none');
				}
				
			}
			
        }
        
        $('ul.menu__primary > li[role="menuitem"] > a').click(function(w){
            if(widnbo > par ) {
                w.preventDefault();
                var dropdown = $(this).closest(model.desktopDropdown);
                
                if (dropdown.hasClass(model.isVisible)) {
                    janala == 'mob' ?  dropdown.removeClass(model.isVisible) : '';
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
	  $('a.nav-toggle').click(function(p){
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

}(jQuery));


/*$(window).scroll(function(){
	if ($('.menu__primary').hasClass('active') ) {
		$('.menu__primary').show();
		$('.menu__secondary').show();
	}
});*/
