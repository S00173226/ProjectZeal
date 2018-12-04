
//var locations;
function getLocations() {
    for (i = 0; i < arguments.length; i++)
    {
        locations[i] = [arguments.info, arguments.lat, arguments.lng]
    }
}

function initMap() {


    var stop1 = {
        info: '<strong>Device1</strong><br>\
        Time: 12:00pm <br>',
        lat: 53.730266,
        lng: -7.802022

    };
    var stop2 = {
        info: '<strong>Device1</strong><br>\
        Time: 12:00pm <br>',
        lat: 54.730266,
        lng: -7.802022

    };
    var stop3 = {
        info: '<strong>Device1</strong><br>\
        Time: 12:00pm <br>',
        lat: 52.730266,
        lng: -7.802022

    };

    var locations = [
        [stop1.info, stop1.lat, stop1.lng],
        [stop2.info, stop2.lat, stop2.lng],
        [stop3.info, stop3.lat, stop3.lng],
    ];
    var map = new google.maps.Map(document.getElementById('map'),
        {
            center: {
                lat: stop1.lat,
                lng: stop1.lng
            },
            zoom: 10,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });

    var infoWindow = new google.maps.InfoWindow({});

    var marker, i;

    for (i = 0; i < locations.length; i++) {
        marker = new google.maps.Marker({
            position: new google.maps.LatLng(locations[i][1], locations[i][2]),
            map: map
        });

        google.maps.event.addListener(marker, 'click', (function (marker, i) {
            return function () {
                infoWindow.setContent(locations[i][0]);
                infoWindow.open(map, marker);
            }
        })(marker, i));
    }

}