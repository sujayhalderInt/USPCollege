/*globals Cookies, starterui, baseUrl*/
window.starterui = window.starterui || {};
window.starterui.cookie = window.starterui.cookie || {};

window.starterui.cookie = (function ($) {
    var model,
        addAnimation = function (item) {
            var itemParent = $(item).parent();
            itemParent.addClass('animate');
            setTimeout(function () { itemParent.removeClass('animate'); }, 1000);
        },
        dropGDPR = function () {
            var cookieStatus = Cookies.get('CookieAgreementUSP');
            if (!cookieStatus) {
                setTimeout(function () {
                    $(model.cookieBanner).removeClass(model.hidden);
                }, 2000);
            }

            $(model.cookiePolicyGDPRdeny).on('click', function (e) {
                e.preventDefault();
                closeCookieAgreement();
                deleteSessionCookies();
            });

            $(model.cookiePolicyGDPRAccept).click(function (e) {
                e.preventDefault();
                addAnimation(this);                
                checkCookieAgreement();
                //USP-2_13/11/2020 Start
                setTimeout(function () {
                    closeCookieAgreement();
                }, 200);
                //USP-2_13/11/2020 End
            });

        },
        checkCookieAgreement = function () {

            var cookieStatus = Cookies.get('CookieAgreementUSP');
            var other = $('.other-cookie').prop('checked') == true;

            if (!cookieStatus) {
                Cookies.set('CookieAgreementUSP', 'other=' + other, {
                    expires: 365
                });
            } else {
                Cookies.remove('CookieAgreementUSP');
                Cookies.set('CookieAgreementUSP', 'other=' + other, {
                    expires: 365
                });
            }
            //USP-2_13/11/2020 Start
            $.ajax({
                url: '/umbraco/surface/Script/RenderAnalyticsScript',
                type: "GET",
                async: true,
                dataType: "text",
                success: function (data) {
                    $('head').append(data);
                },
                error: function () {
                    console.log("Something wrong happen");
                }
            });
            //USP-2_13/11/2020 End
        },
        deleteSessionCookies = function () {
            var array = Cookies.get();
            $.each(array, function (index, cookie) {
                Cookies.remove(index);
            });
        },
        closeCookieAgreement = function () {
            $(model.cookieBanner).slideDown(function () {
                setTimeout(function () {
                    $(model.cookieBanner).addClass(model.hidden);
                }, 500);
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
                    this.cookieBanner = args.cookieBanner || '.cookie-banner';
                    this.cookiePolicyGDPRAccept = args.cookiePolicyGDPRAccept || '.cookie-agree';
                    this.cookiePolicyGDPRdeny = args.cookiePolicyGDPRdeny || '.cookie-refuse';
                    this.hidden = args.hidden || 'hidden';
                }
            };
            model.init();

            if ($(model.cookieBanner).length > 0) {
                $.getScript(baseUrl + '/assets/js/vendors/js.cookie.js')
                    .done(function (script, textStatus) {
                        dropGDPR();
                    });
            }

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

