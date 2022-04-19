var apiDomain = 'https://localhost:7149';
var events = {
    Display: 0,
    Click: 1
};

var actions = {
    AddBanner: "AddBanner",
    DeleteBanner: "DeleteBanner"
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

$(function () {
    var protocol = location.protocol === "https:" ? "wss:" : "ws:";
    var wsUri = protocol + "//" + window.location.host;
    var socket = new WebSocket(wsUri);
    socket.onopen = e => {
        console.log("socket opened", e);
    };

    socket.onclose = function (e) {
        console.log("socket closed", e);
    };

    socket.onmessage = function (e) {
        data = JSON.parse(e.data);
        switch (data.Action) {
            case actions.AddBanner:
                AddBanner(data.Model);
                break;
            case actions.DeleteBanner:
                DeleteBanner(data.Model);
                break;

        }
    };

    socket.onerror = function (e) {
        console.error(e.data);
    };

    function AddBanner(banner) {
        $('.splide__slide[slideid=' + banner.Id + ']').remove();

        const slideElemen = $('#slide-element').clone(true);
        slideElemen.attr('slideid', banner.Id);
        slideElemen.attr('slidehref', banner.LinkURL);
        slideElemen.removeClass('d-none');
        slideElemen.removeAttr('id');

        const img = slideElemen.find('img');
        img.attr('src', img.attr('src') + banner.ImageURL);

        //this should add by order
        $('.splide__track .splide__list')/*.find('li:last-child')*/.append(slideElemen);

        ReloadSplide();
    }

    function DeleteBanner(banner) {
        $('.splide__slide[slideid=' + banner.Id + ']').remove();
        ReloadSplide();
    }

    function ReloadSplide() {
        splide.destroy(true);
        splide.mount();
    }
});