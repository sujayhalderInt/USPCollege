window.starterui = window.starterui || {};
window.starterui.viewport = window.starterui.viewport || {};

/* VIEWPORT */
// Classes and viewport values based on Bootstrap
window.starterui.viewport = function() {
    var width = $(window).width(),
    viewport = 'default';
    if (width >= 1200) { viewport = { vpClass: 'desktop-extra-large', vpSize: 1200 }; }
    if (width <= 1199) { viewport = { vpClass: 'desktop-large', vpSize: 1199 }; }
    if (width <= 991)  { viewport = { vpClass: 'tablet-medium', vpSize: 991 }; }
    if (width <= 767)  { viewport = { vpClass: 'mobile-small', vpSize: 767 }; }
    if (width <= 575)  { viewport = { vpClass: 'mobile-extra-small', vpSize: 575 }; }

    $('html').removeClass('default desktop-extra-large desktop-large tablet-medium mobile-small mobile-extra-small').addClass(viewport.vpClass);

    return viewport;
};