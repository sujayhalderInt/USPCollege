window.starterui = window.starterui || {};
window.starterui.maps = window.starterui.maps || {};

window.starterui.maps = (function ($) {
    var model,

        /** 
         * Generate Map
         */
        generateMap = function (mapCanvas) {
            var marker, i, 
            lat = $(mapCanvas).closest(model.container).attr('data-locations-lat'),
            long = $(mapCanvas).closest(model.container).attr('data-locations-long');

            var position = new google.maps.LatLng(lat, long);
            var map = new google.maps.Map( mapCanvas, {zoom: 11, center: position});
            var marker = new google.maps.Marker({position: position, map: map});

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
                    this.container = args.container || '.locations-map';
                    this.mapDiv = args.mapDiv || '.map';
                    this.mapAPIKey = args.mapAPIKey || $(model.container).attr('data-map-apiKey');
                    // this.centerLatLong = args.centerLatLong || {lat: 40.813731, lng: -102.134880};
                }
            };
            model.init();
            
                
                $.getScript('https://maps.googleapis.com/maps/api/js?key=' + model.mapAPIKey + '').done(function(script, textStatus ) {
                    
                    $(model.mapDiv).each( function (index, content){
                        generateMap(content);
                    });
                });

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