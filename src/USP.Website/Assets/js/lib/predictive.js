/*globals Cookies, starterui, baseUrl, LazyLoad*/
window.usp = window.usp || {};
window.usp.predictive = window.usp.predictive || {};

window.usp.predictive = (function ($) {
    var model,
    typingTimer,
        initPredictive = function () {
            $(model.searchBox).on('keyup', function() {
               var $this = $(this),
                characters = $this.val().length,
                inputSearchBox = $this.closest(model.search),
                predictiveBox = inputSearchBox.siblings(model.predictive);

                clearTimeout(typingTimer);
                typingTimer = setTimeout(function() {
                    if (characters >= 1 ) {
                        populatePredictive($this);
                    }
                    else {
                        $(predictiveBox).addClass(model.hidden);
                    }
                    
                }, 350 );
            });
            $(window).on('click', function (e){
                if ( $(e.target).closest(model.searchBox).length != 1) {
                    $(model.predictive).addClass(model.hidden);
               }
             });
        },
        createLines = function(data) {
            var template = '';

            $.each(data, function(i, content) {
                template += '<a href="' + content.ItemUrl + '">' + content.Heading + '<span>' + content.ResultType + '</span></a>';
            });

            return template;
        },

        populatePredictive = function(input) {

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
                success: function(data){

                    var template = createLines(data);
                   $(predictiveBox).html(template);
                    if ( template != '') {
                        $(predictiveBox).removeClass(model.hidden);
                    }
                   
                   console.log('success');
                },
                error: function (jqxhr,textStatus,errorThrown) {
                    console.log('error');
                }
            });

        },
         /**
         * Init class to initiate script
         * @public
         * @param {object} args is from doc-ready file
         */
        init = function (args) {
            args = args || {};
            model = {
                init: function () {
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

}(jQuery));