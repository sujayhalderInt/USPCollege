window.starterui = window.starterui || {};
window.starterui.videoPlayer = window.starterui.videoPlayer || {};

window.starterui.videoPlayer = (function ($) {
  var model,
    // https://codepen.io/pixelthing/pen/zGZKaQ
    /** 
     * play the targeted video (and hide the poster frame)
     * @param {object} e is event
     */
    videoPlay = function (e) {
      e.preventDefault();
      var $el = $(e.currentTarget);
      var $wrapper = $el.closest(model.videoWrapper);
      var $iframe = $wrapper.find(model.videoIFrame);
      var src = $iframe.data('src');
      // hide poster
      $wrapper.addClass(model.videoWrapperActive);
      // add iframe src in, starting the video
      $iframe.attr('src', src);
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
          this.container = args.container || '.video-wrapper';
          this.videoWrapper = args.videoWrapper || '.js-video-wrapper';
          this.videoIFrame = args.videoIFrame || '.js-video-iframe';
          this.videoWrapperActive = args.videoWrapperActive || 'video-wrapper-active';
        }
      };
      model.init();

      /**
       * Check to see if component exists
       */
      if ($(model.container).length > 0) {

        /** 
         * poster frame click listener
         */

        $.getScript(baseUrl + "/components/video-initialiser")
        .done(function( script, textStatus ) {
            $(document).on('click', '.js-video-poster', videoPlay);
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
