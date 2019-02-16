




function initMap() {

    //$.ajax({
    //    url: '~/Map/Test',
    //    type: 'GET',
    //    success: (data) => {
    //        let obj = data
    //        var map = new google.maps.Map(document.getElementById('map'),
    //            {
    //                center: {
    //                    lat: obj[0].lat,
    //                    lng: obj[0].lng
    //                },
    //                zoom: 10,
    //                mapTypeId: google.maps.MapTypeId.ROADMAP
    //            });

    //        var infoWindow = new google.maps.InfoWindow({});

    //        var marker, i;

    //        for (i = 0; i < obj.length; i++) {
    //            marker = new google.maps.Marker({
    //                position: new google.maps.LatLng(obj[i].lat, obj[i].lng),
    //                map: map
    //            });

    //            google.maps.event.addListener(marker, 'click', (function (marker, i) {
    //                return function () {
    //                    infoWindow.setContent(obj[i].deviceID.toString());
    //                    infoWindow.open(map, marker);
    //                }
    //            })(marker, i));
    //        }
    //    }
    //})
    //var map = new google.maps.Map(document.getElementById('map'),
    //    {
    //        center: {
    //            lat: obj[0].lat,
    //            lng: obj[0].lng
    //        },
    //        zoom: 10,
    //        mapTypeId: google.maps.MapTypeId.ROADMAP
    //    });

    //var infoWindow = new google.maps.InfoWindow({});

    //var marker, i;

    //for (i = 0; i < obj.length; i++) {
    //    marker = new google.maps.Marker({
    //        position: new google.maps.LatLng(obj[i].lat, obj[i].lng),
    //        map: map
    //    });

    //    google.maps.event.addListener(marker, 'click', (function (marker, i) {
    //        return function () {
    //            infoWindow.setContent(obj[i].deviceID.toString());
    //            infoWindow.open(map, marker);
    //        }
    //    })(marker, i));
    //}

}