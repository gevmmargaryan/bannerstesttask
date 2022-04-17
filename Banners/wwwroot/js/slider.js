var apiDomain = 'https://localhost:7149';
var events = {
    Display: 0,
    Click: 1
};
var interval = 100000;

var splide = new Splide('.splide',
    {
        autoplay: true,
        type: 'loop',
        interval: interval
    }
);

splide.on('visible', function (activeSlide) {
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

    const url = activeSlide.slide.getAttribute("slidehref");
    window.open(url, '_blank').focus();

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
        }
    });
});

splide.mount();