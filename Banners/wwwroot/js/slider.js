var apiDomain = 'https://localhost:7149';
var events = {
    Display: 0,
    Click: 1
};

var splide = new Splide('.splide', { autoplay: true }).mount();
var Autoplay = splide.Components.Autoplay;
var play = splide.root.querySelector('.splide__play');
var pause = splide.root.querySelector('.splide__pause');

Autoplay.play();

splide.on('active', function (activeSlide) {
    let slideid = activeSlide.slide.getAttribute("slideid");

    var data = {
        Event: events.Display,
        BannerId: slideid
    }

    $.ajax({
        url: apiDomain + '/api/Banners',
        type: 'post',
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (result) { 
        }

    });
});

splide.on('click', function (activeSlide) {
    let slideid = activeSlide.slide.getAttribute("slideid");

    var data = {
        Event: events.Click,
        BannerId: slideid
    }

    $.ajax({
        url: apiDomain + '/api/Banners',
        type: 'post',
        contentType: "application/json",
        data: JSON.stringify(data),
        success: function (result) {
            var slidehref = activeSlide.slide.getAttribute("slidehref")
            window.location.href = slidehref;
        }
    });
});
