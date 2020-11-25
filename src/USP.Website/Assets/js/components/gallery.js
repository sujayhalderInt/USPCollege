window.usp = window.usp || {};
window.usp.gallery = window.usp.gallery || {};

window.usp.gallery = (function ($) {
    var model,
        initGallery = function () {
            $(document).ready( function (){
                $('.popup-gallery').magnificPopup({
                delegate: 'a',
                type: 'image',
                tLoading: 'Loading image #%curr%...',
                mainClass: 'mfp-img-mobile',
                gallery: {
                    enabled: true,
                    navigateByImgClick: true,
                    preload: [0,1] // Will preload 0 - before current, and 1 after the current image
                },
                image: {
                    tError: '<a href="%url%">The image #%curr%</a> could not be loaded.',
                    titleSrc: function(item) {
                        return item.el.attr('title');
                    }
                }
                });
            });
            /************Jira issue 485 ************/
            $(document).ready( function (){
                $("body").swipe({
                    swipeLeft: function(event, direction, distance, duration, fingerCount) {
                        $(".mfp-arrow-left").magnificPopup("prev");
                    },
                    swipeRight: function() {
                        $(".mfp-arrow-right").magnificPopup("next");
                    }
                });
                console.log('anmkan');
            });
            
            /************Jira issue 485 ************/
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
                    this.gallery = args.gallery || '.gallery';
                }
            };
            model.init();
            /************Jira issue 485 ************/
            if ($(window).width() < 991.97) {
                $.getScript(baseUrl + "/assets/js/vendors/jquery.magnific-popup.min.js"),
                $.getScript(baseUrl + "/assets/js/vendors/jquery.touchSwipe.min.js")
                .done(function( script, textStatus ) {
                    initGallery();
                });
            }
            else {
                $.getScript(baseUrl + "/assets/js/vendors/jquery.magnific-popup.min.js")
                .done(function( script, textStatus ) {
                    initGallery();
                });
            }
            /*$.getScript(baseUrl + "/assets/js/vendors/jquery.magnific-popup.min.js");
            $.getScript(baseUrl + "/assets/js/vendors/jquery.touchSwipe.min.js")
            .done(function( script, textStatus ) {
                initGallery();
            });*/
            /************Jira issue 485 ************/

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