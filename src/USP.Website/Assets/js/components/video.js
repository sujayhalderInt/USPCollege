window.starterui = window.starterui || {};
window.starterui.video = window.starterui.video || {};

window.starterui.video = (function ($) {

    var model, player, videoArray = [],

        /**
         * Create a player for each video
         * @public
         * @param {object} opts is an obejct containing data to build video
         */
        createPlayer = function (opts) {
            player = new YT.Player(opts.name, {
                videoId: opts.id,
                playerVars: {
                    controls: 1,
                    iv_load_policy: 3, // Disables annotations.
                    showinfo: 0,
                    rel: 0,
                    cc_load_policy: 1,
                    modestbranding: 1,
                    //playsinline: 1, USP-4_30/11/2020
                    fs: 1,
                    enablejsapi: 1
                },
                events: {
                    "onReady": onReady,
                    "onStateChange": onPlayerStateChange,
                    "onError": onError
                }
            });

            videoArray.push(player);
        },

        /**
         * play the targeted video (and hide the poster frame)
         * @param {object} e is event
         */
        videoPlay = function (e) {
            e.preventDefault();
            var $el = $(e.currentTarget);
            var $wrapper = $el.closest(model.container);
            // hide poster
            $wrapper.addClass(model.videoWrapperActive);
            e.data.param.target.playVideo();
        },
        getTitle = function (id, key) {
            $.getJSON('https://www.googleapis.com/youtube/v3/videos?id=' + id + '&key=' + key + '&part=snippet&callback=?', function (data) {
                // set title
                //console.log(data);
                //USP-4_30/11/2020 Begin
                //$(model.outerContainer).find(model.videoTitle).html(data.items[0].snippet.title);
                $("#vt-" + id).html(data.items[0].snippet.title);
                //USP-4_30/11/2020 End
            });
        },

        /**
         * OnReady Event from youTube API. key: onReady
         * @param {object} e is event
         */
        onReady = function (e) {
            var targetPlayer = e.target.f;  //USP-4_30/11/2020 
            var total;
            var vidDuration = 0;
            var videoPoster = $(targetPlayer).next();
            var videoId = $(targetPlayer).parent().attr('data-youTubeId');
            var videoKey = $(targetPlayer).parent().attr('data-ytkey');            
            
            if (e === null) {
                return;
            } else {
                total = e.target.getDuration();
                getTitle(videoId, videoKey);
                //USP-4_30/11/2020 Begin
                //vidDuration = Math.floor(total / 60) + ":" + (total % 60 ? total % 60 : '00');
                vidDuration = Math.floor(total / 60) + ":" + (total % 60 ? total % 60 <= 9 ? '0' + total % 60 : total % 60 : '00');
                //USP-4_30/11/2020 End
            }

            // set time
            var durationid = '#v-' + videoId;
            $(model.outerContainer).find(durationid).html(vidDuration);
            //$(model.outerContainer).find(model.videoDuration).html(vidDuration);



            setTimeout(function () {
                $(targetPlayer).parent(model.container).removeClass('is-loading');
            }, 300);
            //console.log(videoPoster);

            //$(document).off(videoPoster).on("click", videoPoster, {
            //    param: e
            //}, videoPlay);

            $(videoPoster).click({
                param: e
            }, videoPlay);

        },

        /**
         * onPlayerStateChange Event from youTube API. key: onStateChange
         * Manages player state
         * @param {object} e is event
         */
        onPlayerStateChange = function (e) {
            var vid = e.target.f;   //USP-4_30/11/2020 
            /** Pauses current video playing if a new vide is seleted to play */
            if (e.data == YT.PlayerState.PLAYING) {
                //BH
                //var temp = e.target.f.src;     //USP-4_30/11/2020 
                //for (var i = 0; i < videoArray.length; i++) {
                //    if (videoArray[i].a.src !== temp) {
                //        videoArray[i].pauseVideo();
                //    }
                //}
            }

        },

        /**
         * onError Event from youTube API key: onError
         * @param {object} e is event
         */
        onError = function (e) {
            console.error('playback error');
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
                    this.outerContainer = args.outerContainer || '.video-wrapper';
                    this.container = args.container || '.video-wrapper-single';
                    this.videoWrapperActive = args.videoWrapperActive || 'video-wrapper-active';
                    this.videoDuration = args.videoDuration || '.video-duration';
                    this.videoTitle = args.videoTitle || '.video-title';
                    this.videoPoster = args.videoPoster || '.video-poster';
                    this.containerSingle = args.containerSingle || '.video-wrapper-single';
                    this.carousel = args.carousel || '.carousel-inner';

                }
            };

            model.init();

            /** Check if video single or playlist is on the page */
            if ($(model.containerSingle).length > 0) {

                /** get youtube API script */
                $.getScript('//www.youtube.com/iframe_api');

                /** If single player is present add id to each */
                if ($(model.containerSingle).length > 0) {
                    $.each($(model.containerSingle), function (i, container) {
                        $(container).find('.video-player').attr('id', 'ytPlayer-s' + i);
                    });
                }

                /** on youTube API ready */
                window.onYouTubeIframeAPIReady = function onYouTubeIframeAPIReady() {
                    /** If single player is present */
                    if ($(model.containerSingle).length > 0) {
                        /** Initiate single player js */
                        starterui.video.init();
                        /** Create player for each - function in components/video-single.js  */
                        $.each($(model.containerSingle), function (i, video) {
                            starterui.video.createPlayer({
                                name: $(video).find('.video-player').attr('id'),
                                id: $(video).attr("data-youTubeId"),
                                duration: ''
                            });
                        });
                    }

                    /** If playlist player is present */
                    if ($(model.containerPlaylist).length > 0) {
                        /** Initiate playlst player js */
                        starterui.videoPlayerPlaylist.init();
                    }
                };
            }

        };

    /**
     * Returns init
     * @return public methods/functions
     * @public
     */
    return {
        init: init,
        createPlayer: createPlayer
    };

}(jQuery));