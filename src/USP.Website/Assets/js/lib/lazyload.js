/*globals Cookies, starterui, baseUrl, LazyLoad*/
window.starterui = window.starterui || {};
window.starterui.lazy = window.starterui.lazy || {};

window.starterui.lazy = (function ($) {
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
                   this.lazy = args.lazy || '.lazy';
                }
            };

            model.init();
            
            
            if ($(model.lazy).length > 0 ){
                $.getScript(baseUrl + '/assets/js/vendors/lazyload.min.js')
                .done(function( script, textStatus ) {
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

}(jQuery));