window.usp = window.usp || {};
window.usp.listing = window.usp.listing || {};
var  anh8;
window.usp.listing = (function ($) {
    var max_,
        model,
        pageNo = 1,
        type = "",
        filters = "",
        sort = "",
        other = "",
        term = "",
        posturl,
        form,
        frmdata,
        title = $(document).find("title").text(),
        arr = [],
        postForListing = function (posturl, data) {
            $(model.resultContainerLoader).addClass('active');
            
            $.ajax({
                url: posturl,
                type: 'POST',
                data: data,
                contentType: "application/x-www-form-urlencoded",
                dataType: "html",
                success: function(result) {
                    $(model.resultContainer).html(result);
                    $(model.resultContainerLoader).removeClass('active');
                     var myLazyLoad = new LazyLoad({
                        callback_set: function () {
                            picturefill({ reevaluate: true });
                        }
                    });
                    myLazyLoad.update();
					$('article.listing-item').each(function () {
						var h8 = $(this).find('> .listing-item__details h3').height();
						arr.push(parseInt(h8));
                    }),
                    
                    max_ = Math.max.apply(Math, arr);
                    //$('.results__body').css('min-height', anh8);
                    //$('.listing-item__img').css('height',anh8);
                    $('.results__head').unwrap();
                },
                complete: function () {
                    inittrimtext();
                },
                error: function(result) {
                    
                }
            });
        },
        initClearBtn = function () {
            $(model.clearBtn).on('click', function(e) {
                e.preventDefault();
				$('.date').val("");
                $('.sidebar-filter input[type="checkbox"]:checked').prop('checked', false);                            
                filters = "";
                other = "";                
                DoPushState(title);
            });
        },

        initTriggerChange = function () {
            $(document).on('change', '.sidebar-filter input[type="checkbox"], .sort-by, .date', function () {
                var btn = $(this).closest('form').find(model.btnFind);

                form = $(this).parents('form');

                var filterText = [];
                var docTypeArr = [];
                var otherFilter = [];
                var seralizedForm = form.serializeArray();
                seralizedForm.map(function (item, index) {
                    if (typeof (item.value) !== "undefined" && item.value !== "" && item.name != "ufprt" && item.name != "ListingPostback.Page") {
                        if (item.name == "ListingPostback.SelectedTaxonomy") {
                            filterText.push({ name: "T", value: item.value });
                        }
                        else if (item.name == "ListingPostback.SelectedDocTypes") {
                            docTypeArr.push({ name: "D", value: item.value });
                        }
                        else if (item.name == "ListingPostback.SortBy")
                        {
                            sort = item.value;
                        }
                        else if (item.name == "ListingPostback.SearchText")
                        {
                            term = item.value;
                        }
                        else {
                            otherFilter.push({ name: item.name.split(".").pop(), value: item.value });
                        }
                    }
                });
                
                filters = filterText.length > 0 ? filterText.map(function (item, index) { return item.value; }).join(",") : "";
                type = docTypeArr.length > 0 ? docTypeArr.map(function (item, index) { return item.value; }).join(",") : "";
                other = otherFilter.length > 0 ? otherFilter.map(function (item, index) { return item.name + "-" + item.value; }).join(",") : "";
                
                $(".hdnPageNo").val("1");
                
                DoPushState(title);
               
            });

            $(document).on('click', '.pagination__item input', function (e) {
                e.preventDefault();
                $('html, body').animate({
                    scrollTop: parseInt($("#mainContent").offset().top)
                }, 100);
                var form = $(this).parents('form');
                var posturl = form.attr('action');
                var data = form.serialize() + "&" + $(this).attr("name") + "=" + $(this).val();
                
                var page = $(".hdnPageNo").val();
                if (this.id == "btnnext") {
                    
                    pageNo = parseInt(page) + 1;
                    pageNo = pageNo;
                }
                else if (this.id == "btnprev") {
                    
                    pageNo = parseInt(page) - 1;
                    pageNo = pageNo;
                }
                else {
                    var p = $(this).val();
                    pageNo = p;
                }      
                          
                DoPushState(title);
            });
        },
        inittrimtext = function () {          
            var $list = $('.related-course-list');
            var $items = $list.find('article > div');

            $list.each(function () {
                var $items = $(this).find('article > div');
                var perRow = Math.floor($(this).width() / $items.width());
                
                if (perRow == null || perRow < 3) return true;
                for (var i = 0, j = $items.length; i < j; i += perRow) {
                    var maxHeight = 0,
                        $row = $items.slice(i, i + perRow);
                    $row.each(function () {                        
                        var itemHeight = parseInt($(this).outerHeight());
                        if (itemHeight > maxHeight) {
                            maxHeight = itemHeight;
                        }
                    });
                    $row.css('height', maxHeight);
                }
            });
        },
        getversion = function GetIEVersion() {
            var sAgent = window.navigator.userAgent;
            var Idx = sAgent.indexOf("MSIE");
          
            // If IE, return version number.
            if (Idx > 0) 
              return parseInt(sAgent.substring(Idx+ 5, sAgent.indexOf(".", Idx)));
          
            // If IE 11 then look for Updated user agent string.
            else if (!!navigator.userAgent.match(/Trident\/7\./)) 
              return 11;
          
            else
              return 0; //It is not IE
        },
        initListing = function () {

            $(document).on('click', model.btnSubmitForm, function (e) {                
                e.preventDefault();
                //var form = $(this).parents('form');
                //var posturl = form.attr('action');
                //var data = form.serialize() + "&" + $(this).attr("name") + "=pressed";
                //search term
                term = $("#page-search").val();
                
                var page = $(".hdnPageNo").val();
                if (this.id == "btnpageing") {  
                    //clicked on load more
                    pageNo = parseInt(page) + 1;
                    pageNo = pageNo;                    
                }
                else {                 
                    pageNo = 1;                   
                }               
                DoPushState(title);
                
            });

        },
        DoHistory = function () {
            form = $(model.btnSubmitForm).parents('form');
            posturl = form.attr('action');

            var url = window.location.href;
            if (url.indexOf("?") != -1) {

                term = GetParameterByName("term", url);

                var pno = GetParameterByName("page", url);

                var f = GetParameterByName("filters", url);
                
                var dType = GetParameterByName("types", url);

                var otherDate = GetParameterByName("other", url);

                sort = GetParameterByName("sortby", url);

                pageNo = pno != null && pno != "" ? pno : 1;

                if (typeof (f) != "undefined" && f != "") {
                    var fArr = f.split(",");
                    fArr.map(function (i, j) {
                        $('.sidebar-filter__body').find('input[value="' + i + '"]').attr('checked', true);
                        $('.sidebar-filter__body').find('input[value="' + i + '"]').prop('checked', true);
                    });
                }
                else {
                    $('.sidebar-filter__body').find('input[type="checkbox"]').prop('checked', false);
                }

                if (typeof (dType) != "undefined" && dType != "") {
                    var dArr = dType.split(",");
                    dArr.map(function (i, j) {
                        $('.sidebar-filter__body').find('input[value="' + i + '"]').attr('checked', true);
                        $('.sidebar-filter__body').find('input[value="' + i + '"]').prop('checked', true);
                    });
                }

                if (typeof (sort) != "undefined" && sort != "") {
                    $('#single-select-sort option').filter(function () {
                        return ($(this).val() == sort);
                    }).prop('selected', true);
                }
                else {
                    var defaultval = $('#single-select-sort option:first').val();                    
                    $('#single-select-sort option').filter(function () {
                        return ($(this).val() == defaultval);
                    }).prop('selected', true);

                }
               
                if (typeof (otherDate) != "undefined" && otherDate != "") {
                    var otherArr = otherDate.split(",");
                    $('input[type="text"].date').val('');
                    otherArr.map(function (i, j) {
                        var otherItem = i.split("-");
                        $('input[name="ListingPostback.' + otherItem[0] + '"]').val(otherItem[1]);
                    });                   
                }
                else {
                    $('input[type="text"].date').val('');
                }

                if (typeof (term) == "undefined" || term == "") {
                    $("#page-search").val("");
                }

                filters = typeof (f) != "undefined" && f != "" ? f : "";

                $(".hdnPageNo").val(pageNo);

            }
            
            frmdata = GetFormValue(form, pageNo, filters) + "&" + $(this).attr("name") + "=pressed";
            
            History.Adapter.bind(window, 'statechange', function () {

                var State = History.getState();

                var data = State.data;
                if (data.PageNo) {
                    pageNo = data.PageNo;
                }
                else {
                    pageNo = 1;
                }
               
                filters = data.Filters;
                other = data.Other;
                sort = data.Sortby;
                term = data.Term;
                type = data.Types;
                console.log(sort);
                if (typeof (filters) != "undefined" && filters != "") {
                    var fhArr = filters.split(",");
                    $('.sidebar-filter__body').find('input[type="checkbox"]').prop('checked', false);
                    fhArr.map(function (i, j) {
                        $('.sidebar-filter__body').find('input[value="' + i + '"]').prop('checked', true);
                    });
                }
                else {
                    $('.sidebar-filter__body').find('input[type="checkbox"]').prop('checked', false);
                }
               
                if (typeof (type) != "undefined" && type != "") {
                    var typeArr = type.split(",");
                    $('.sidebar-filter__body').find('input[type="checkbox"].doc-type-filter').prop('checked', false);
                    typeArr.map(function (i, j) {
                        $('.sidebar-filter__body').find('input[value="' + i + '"]').prop('checked', true);
                    });
                }
                
                if (typeof (other) != "undefined" && other != "") {
                    var otherArr = other.split(",");
                    $('input[type="text"].date').val('');
                    otherArr.map(function (i, j) {
                        var otherItem = i.split("-");
                        $('input[name="ListingPostback.' + otherItem[0] + '"]').val(otherItem[1]);
                    });
                    console.log(otherArr);
                }
                else {
                    $('input[type="text"].date').val('');
                }
                 
                if (typeof (sort) != "undefined" && sort != "") {
                    $('#single-select-sort option').filter(function () {
                        return ($(this).val() == sort);
                    }).prop('selected', true);
                }
                else {
                    var defaultval = $('#single-select-sort option:first').val();
                    console.log(defaultval);
                    $('#single-select-sort option').filter(function () {
                        return ($(this).val() == defaultval);
                    }).prop('selected', true);
                    
                }

                if (typeof (term) == "undefined" || term == "") {
                    $("#page-search").val("");
                }
                
                frmdata = GetFormValue(form, pageNo, filters) + "&" + $(this).attr("name") + "=pressed";
                
                postForListing(posturl, frmdata);
            });
        },
        DoPushState = function (title) {
            var query = GetPushStateURL();
            console.log(query + "------" + filters);
            History.pushState({ Term: term, PageNo: pageNo, Filters: filters,Types: type, Sortby: sort,  Other: other }, title, query);
        },
        GetPushStateURL = function () {
            var random = Math.random(); 
            
            var url = "";

            if (typeof (term) != "undefined" && term != "") {
                url += "?term=" + term;
            }

            if (typeof (filters) != "undefined" && filters != "") {
                url += url != "" ? "&filters=" + filters : "?filters=" + filters;
            }

            if (typeof (type) != "undefined" && type != "") {
                url += url != "" ? "&types=" + type : "?types=" + type;
            }

            if (typeof (sort) != "undefined" && sort != "") {
                url += url != "" ? "&sortby=" + sort : "?sortby=" + sort;
            }

            if (typeof (other) != "undefined" && other != "") {
                url += url != "" ? "&other=" + other : "?other=" + other;
            }

            if (typeof (pageNo) != "undefined" && pageNo != "") {
                url += url != "" ? "&page=" + pageNo : "?page=" + pageNo;
            }
            if (typeof (random) != "undefined" && random != "") {
                url += url != "" ? "&random=" + random : "?random=" + random;
            }

            return url;
        },
        GetFormValue = function (form, page, filters) {
            var values, index;

            values = form.serializeArray();
            
            for (index = 0; index < values.length; ++index) {
                if (values[index].name == "ListingPostback.Page") {
                    values[index].value = page;
                }
                else if (typeof (filters) != "undefined" && filters != "") {
                    //var filterItem = filters.split(",");
                    //filterItem.map(function (item, index) {
                    //    var fArray = item.split("-");
                    //});
                }
            }
            values = jQuery.param(values);
            
            return values;
        },
        GetParameterByName = function (name, url) {

            if (!url) url = window.location.href;
            name = name.replace(/[\[\]]/g, "\\$&");
            var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
                results = regex.exec(url);
            if (!results) return "";
            if (!results[2]) return '';

            return decodeURIComponent(results[2].replace(/\+/g, " "));
        },
        initDropdownMobile = function () {

            // $(document).on('click', model.filterWrapperHead, function () {
            //     var item = $(model.filterWrapperBody);
               
            //     if ($(item).hasClass(model.hidden)) {
            //         $(this).addClass(model.oppened);
            //         item.slideDown(function() {
            //             setTimeout(function () {
            //                 item.removeClass(model.hidden);
            //             }, 500);
            //         });
            //     } else {
            //         $(this).removeClass(model.oppened);
            //         item.slideUp(function () {
            //             setTimeout(function () {
            //                 item.addClass(model.hidden);
            //             }, 500);
            //         });
            //     }


            // });


            $(document).on('click', model.filterSubHeader, function () {

                var item = $(this).closest(model.filterHeader).find(model.filterBody);
				$(item).toggleClass('clicked');
                if ($(item).hasClass(model.hidden)) {
                    $(this).addClass(model.oppened);
                    item.slideDown(function() {
                        setTimeout(function () {
                            item.removeClass(model.hidden);
                        }, 500);
                    });
                } else {
                    $(this).removeClass(model.oppened);
                    item.slideUp(function () {
                        setTimeout(function () {
                            item.addClass(model.hidden);
                        }, 500);
                    });
                }

            });

            /**************Jira issue 476*******/
            $(window).on('load', function(){
                $('.sidebar-filter__section__body').addClass(model.hidden);
            });
            /*$(document).ready(function(){
                if (getversion() > 0) {
                    var divs = $('.results__body.related-course-list  > article');
                    for (var i = 0; i < divs.length; i+=3){
                        divs.slice(i, i+3).wrapAll('<div class="kah9834"></div>');
                    }
                }
                else {
                    
                }
            });*/
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
                    this.clearBtn = args.clearBtn || '#btn-clear-all';
                    this.resultContainer = args.resultContainer || '.results__wrapper';
                    this.resultContainerLoader = args.resultContainerLoader || '.loader';

                    this.btnSubmitForm = args.btnSubmitForm || 'input.btn-listing-submit, input.btn-listing-submit1';
                    this.btnFind = args.btnFind || '.search input[type="submit"]';

                    this.filterWrapperHead = args.filterWrapperHead || '.sidebar-filter__head'
                    this.filterWrapperBody = args.filterWrapperBody || '.sidebar-filter__body';
                    this.filterHeader = args.filterHeader || '.sidebar-filter__section';
                    this.filterSubHeader = args.filterSubHeader || '.sidebar-filter__section__head';
                    this.filterBody = args.filterBody || '.sidebar-filter__section__body';
                    this.hidden = args.hidden || 'is--hidden';
                    this.oppened = args.oppened || 'oppened';
                }
            };
            model.init();
            DoHistory();
            //DoPushState("test");
            initClearBtn();
            initTriggerChange();
            initListing();

            initDropdownMobile();
            getversion();
            inittrimtext();

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

(function ($, window, document, undefined) {
    'use strict';

    var $list = $('.related-course-list'),
        arr = [];

    function isIE() {
        var ua = navigator.userAgent;
        /* MSIE used to detect old browsers and Trident used to newer ones*/
        var is_ie = ua.indexOf("MSIE ") > -1 || ua.indexOf("Trident/") > -1;
        return is_ie;
    }

    $(document).ready(function(){
        $(window).scroll(function (event) {
            var scroll = $(window).scrollTop();
            if ($('input[value="Load More"]').length > 1) {
                var stop = $('input[value="Load More"]').offset().top;
            }
            if (scroll >= stop - ($(window).height()/2)) {
                anh8 = $('.listing-item__img').height();
            }
        });
    });
   
    $(window).on('resize', function () {
        arr = [];
        $list.each(function () {
            var $items = $(this).find('article > div');
            var perRow = Math.floor($(this).width() / $items.width());
            //alert(perRow);
            $items.css('height', 'auto');
            if (perRow == null || perRow < 3) return true;
            for (var i = 0, j = $items.length; i < j; i += perRow) {
                var maxHeight = 0,
                $row = $items.slice(i, i + perRow);
                $row.each(function () {
                    
                    var itemHeight = parseInt($(this).outerHeight());
                    if (itemHeight > maxHeight) {
                        maxHeight = itemHeight;
                    }
                    arr.push(maxHeight);

                });
                if (isIE()) {
                    $row.css('height', Math.max.apply(Math, arr));
                }
                else {
                    $row.css('height', maxHeight);
                }
                
                //console.log(Math.max.apply(Math, arr));
            }
        });
    });

})(jQuery, window, document);