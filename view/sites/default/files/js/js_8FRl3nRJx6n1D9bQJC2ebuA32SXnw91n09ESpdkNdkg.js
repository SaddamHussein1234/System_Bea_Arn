
//contact map
if ($('#map').length) {
    function initMap() {

        if (window.innerWidth < 768) {
            var center = new google.maps.LatLng(22.875122, 40.492588);
        }
        else {
            var center = new google.maps.LatLng(22.875122, 40.492588);
        }

        //22.875122, 40.492588

        setTimeout(function () {

            var icon = {
                url: "../../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png",
                anchor: new google.maps.Point(150, 50),
                scaledSize: new google.maps.Size(100, 100)
            };


            infowindow = new google.maps.InfoWindow();

            var center = new google.maps.LatLng(22.875122, 40.492588);
            var center_en = new google.maps.LatLng(22.875122, 40.492588, 16);
            if ($('html').attr('lang') == 'en') {
                center = center_en;
            }

            var mapOptions = {
                zoom: 14,
                //                 maxZoom:14,
                minZoom: 10,
                center: center,
                //                 disableDefaultUI: true,
                styles:
                [
  {
      "elementType": "geometry",
      "stylers": [
        {
            "color": "#d6dce4"
        }
      ]
  },
  {
      "elementType": "labels",
      "stylers": [
        {
            "saturation": "-100"
        },
        {
            "visibility": "on"
        }
      ]
  },
  {
      "elementType": "labels.icon",
      "stylers": [
        {
            "visibility": "off"
        }
      ]
  },
  {
      "elementType": "labels.text.fill",
      "stylers": [
        {
            "color": "#616161"
        }
      ]
  },
  {
      "elementType": "labels.text.stroke",
      "stylers": [
        {
            "color": "#f5f5f5"
        }
      ]
  },
  {
      "featureType": "administrative",
      "elementType": "geometry",
      "stylers": [
        {
            "visibility": "off"
        }
      ]
  },
  {
      "featureType": "administrative.land_parcel",
      "stylers": [
        {
            "visibility": "off"
        }
      ]
  },
  {
      "featureType": "administrative.land_parcel",
      "elementType": "labels.text.fill",
      "stylers": [
        {
            "color": "#bdbdbd"
        }
      ]
  },
  {
      "featureType": "administrative.neighborhood",
      "stylers": [
        {
            "visibility": "off"
        }
      ]
  },
  {
      "featureType": "landscape",
      "stylers": [
        {
            "visibility": "on"
        }
      ]
  },
  {
      "featureType": "poi",
      "stylers": [
        {
            "visibility": "off"
        }
      ]
  },
  {
      "featureType": "poi",
      "elementType": "geometry",
      "stylers": [
        {
            "color": "#eeeeee"
        }
      ]
  },
  {
      "featureType": "poi",
      "elementType": "geometry.stroke",
      "stylers": [
        {
            "color": "#b0c2d7"
        }
      ]
  },
  {
      "featureType": "poi",
      "elementType": "labels.text",
      "stylers": [
        {
            "visibility": "off"
        }
      ]
  },
  {
      "featureType": "poi",
      "elementType": "labels.text.fill",
      "stylers": [
        {
            "color": "#757575"
        }
      ]
  },
  {
      "featureType": "poi.park",
      "elementType": "geometry",
      "stylers": [
        {
            "color": "#e5e5e5"
        }
      ]
  },
  {
      "featureType": "poi.park",
      "elementType": "labels.text.fill",
      "stylers": [
        {
            "color": "#9e9e9e"
        }
      ]
  },
  {
      "featureType": "road",
      "stylers": [
        {
            "color": "#c4dce4"
        },
        {
            "visibility": "simplified"
        }
      ]
  },
  {
      "featureType": "road",
      "elementType": "geometry",
      "stylers": [
        {
            "color": "#d0d7e0"
        }
      ]
  },
  {
      "featureType": "road",
      "elementType": "labels",
      "stylers": [
        {
            "visibility": "off"
        }
      ]
  },
  {
      "featureType": "road",
      "elementType": "labels.icon",
      "stylers": [
        {
            "visibility": "off"
        }
      ]
  },
  {
      "featureType": "road.arterial",
      "elementType": "labels.text.fill",
      "stylers": [
        {
            "color": "#757575"
        }
      ]
  },
  {
      "featureType": "road.highway",
      "elementType": "geometry",
      "stylers": [
        {
            "color": "#dadada"
        }
      ]
  },
  {
      "featureType": "road.highway",
      "elementType": "labels.text.fill",
      "stylers": [
        {
            "color": "#616161"
        }
      ]
  },
  {
      "featureType": "road.local",
      "elementType": "labels.text.fill",
      "stylers": [
        {
            "color": "#9e9e9e"
        }
      ]
  },
  {
      "featureType": "transit",
      "stylers": [
        {
            "visibility": "off"
        }
      ]
  },
  {
      "featureType": "transit.line",
      "stylers": [
        {
            "color": "#e5e5e5"
        }
      ]
  },
  {
      "featureType": "transit.line",
      "elementType": "geometry",
      "stylers": [
        {
            "color": "#e5e5e5"
        }
      ]
  },
  {
      "featureType": "transit.station",
      "stylers": [
        {
            "color": "#eeeeee"
        }
      ]
  },
  {
      "featureType": "transit.station",
      "elementType": "geometry",
      "stylers": [
        {
            "color": "#eeeeee"
        }
      ]
  },
  {
      "featureType": "water",
      "stylers": [
        {
            "color": "#c9c9c9"
        }
      ]
  },
  {
      "featureType": "water",
      "elementType": "geometry",
      "stylers": [
        {
            "color": "#c9c9c9"
        }
      ]
  },
  {
      "featureType": "water",
      "elementType": "labels.text",
      "stylers": [
        {
            "visibility": "off"
        }
      ]
  },
  {
      "featureType": "water",
      "elementType": "labels.text.fill",
      "stylers": [
        {
            "color": "#9e9e9e"
        }
      ]
  }
                ],
                scrollwheel: false,
                navigationControl: true,
                scaleControl: true,
                //                 draggable: false
            };



            map = new google.maps.Map(document.getElementById('map'), mapOptions);
            var marker = new google.maps.Marker({
                position: new google.maps.LatLng(22.875122, 40.492588),
                map: map,
                icon: icon
            });


            var marker_url = "https://www.google.com/maps/place/%D8%A7%D9%84%D8%B5%D9%84%D8%AD%D8%A7%D9%86%D9%8A%D8%A9%E2%80%AD/@22.8773997,40.4968286,15z/data=!4m5!3m4!1s0x159522bf174997cd:0xdd32bd6645ea4d43!8m2!3d22.8768698!4d40.4885438";
            marker.addListener('click', function () {
                window.open(marker_url);
            });


        }, 100);




    }


}





//contact map
if ($('#office-map').length) {

    // ###innosoft.sa @2018 ###
    function chkOrgin() {
        if (!window.location.origin) {
            var port = window.location.port ? ':' + window.location.port : '';
            return window.location.protocol + "//" + window.location + port;
        } else {
            return window.location;
        }
    }

    var lat = parseFloat($('#latitude').text());
    var long = parseFloat($('#longitude').text());


    if (window.location.href.indexOf('?lat=') > 0) {

        var main_title = $('.main-welcome h1').text();
        $('body').addClass('office-map-param');
        $('.preloader').remove();
        $('.site-header').remove();
        $('#footer').remove();
        $('.content-section.speak-section').remove();
        $('.main-welcome').remove();
        $('.modal').remove();



        function getURLParameters(url) {

            var result = {};
            var searchIndex = url.indexOf("?");
            if (searchIndex == -1) return result;
            var sPageURL = url.substring(searchIndex + 1);
            var sURLVariables = sPageURL.split('&');
            for (var i = 0; i < sURLVariables.length; i++) {
                var sParameterName = sURLVariables[i].split('=');
                result[sParameterName[0]] = sParameterName[1];
            }
            return result;
        }

        var url = chkOrgin().toString();
        console.log(url);
        var params = getURLParameters(url);

        for (var paramName in params) {
            //           console.log(paramName +" is " + params[paramName]);

            if (paramName == 'lat') { lat = params[paramName]; }
            if (paramName == 'lng') { long = params[paramName]; }
        }



        lat = parseFloat(lat);
        long = parseFloat(long);

        //            var lat = parseFloat($('#latitude').text()) ;
        //        var long = parseFloat($('#longitude').text()) ;



        function initMap() {

            //               var center = new google.maps.LatLng(  longitude , latitude);

            setTimeout(function () {

                var icon = {
                    url: "../../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png",
                    anchor: new google.maps.Point(25, 50),
                    scaledSize: new google.maps.Size(100, 100)
                };



                var center = new google.maps.LatLng(latitude, longitude);

                var mapOptions = {
                    zoom: 14,
                    center: { lat: lat, lng: long },
                    scrollwheel: true,
                    navigationControl: true,
                    scaleControl: true,
                    draggable: true
                };


                map = new google.maps.Map(document.getElementById('office-map-in'), mapOptions);

                var marker = new google.maps.Marker({
                    position: new google.maps.LatLng(lat, long),
                    map: map,
                    icon: icon,
                    title: $('.main-welcome h1').text()
                });

                var contentString = '<div class="contact-box branch-item-cont p-0" ><h3>' + main_title + '</h3><p class="box-contact"> ' + $('.office-latitude-cont').html() + '</p> <p class="box-contact">' + $('.office-longitude-cont').html() + '</p></div>';




                var infowindow = new google.maps.InfoWindow({
                    content: contentString
                });


                marker.addListener('click', function () {
                    infowindow.open(map, marker);
                    map.setZoom(map.getZoom() + 1);


                });


            }, 100);




        }




    }


    else {
        function initMap() {

            //               var center = new google.maps.LatLng(  longitude , latitude);

            setTimeout(function () {

                var icon = {
                    url: "../../view/sites/all/themes/mlsd2019/assets/img/StartSoon.png",
                    anchor: new google.maps.Point(25, 50),
                    scaledSize: new google.maps.Size(100, 100)
                };



                var center = new google.maps.LatLng(latitude, longitude);

                var mapOptions = {
                    zoom: 14,
                    //                 maxZoom:14,
                    minZoom: 10,
                    center: { lat: lat, lng: long },
                    //                 disableDefaultUI: true,
                    styles:
                    [
      {
          "elementType": "geometry",
          "stylers": [
            {
                "color": "#d6dce4"
            }
          ]
      },
      {
          "elementType": "labels",
          "stylers": [
            {
                "saturation": "-100"
            },
            {
                "visibility": "on"
            }
          ]
      },
      {
          "elementType": "labels.icon",
          "stylers": [
            {
                "visibility": "off"
            }
          ]
      },
      {
          "elementType": "labels.text.fill",
          "stylers": [
            {
                "color": "#616161"
            }
          ]
      },
      {
          "elementType": "labels.text.stroke",
          "stylers": [
            {
                "color": "#f5f5f5"
            }
          ]
      },
      {
          "featureType": "administrative",
          "elementType": "geometry",
          "stylers": [
            {
                "visibility": "off"
            }
          ]
      },
      {
          "featureType": "administrative.land_parcel",
          "stylers": [
            {
                "visibility": "off"
            }
          ]
      },
      {
          "featureType": "administrative.land_parcel",
          "elementType": "labels.text.fill",
          "stylers": [
            {
                "color": "#bdbdbd"
            }
          ]
      },
      {
          "featureType": "administrative.neighborhood",
          "stylers": [
            {
                "visibility": "off"
            }
          ]
      },
      {
          "featureType": "landscape",
          "stylers": [
            {
                "visibility": "on"
            }
          ]
      },
      {
          "featureType": "poi",
          "stylers": [
            {
                "visibility": "off"
            }
          ]
      },
      {
          "featureType": "poi",
          "elementType": "geometry",
          "stylers": [
            {
                "color": "#eeeeee"
            }
          ]
      },
      {
          "featureType": "poi",
          "elementType": "geometry.stroke",
          "stylers": [
            {
                "color": "#b0c2d7"
            }
          ]
      },
      {
          "featureType": "poi",
          "elementType": "labels.text",
          "stylers": [
            {
                "visibility": "off"
            }
          ]
      },
      {
          "featureType": "poi",
          "elementType": "labels.text.fill",
          "stylers": [
            {
                "color": "#757575"
            }
          ]
      },
      {
          "featureType": "poi.park",
          "elementType": "geometry",
          "stylers": [
            {
                "color": "#e5e5e5"
            }
          ]
      },
      {
          "featureType": "poi.park",
          "elementType": "labels.text.fill",
          "stylers": [
            {
                "color": "#9e9e9e"
            }
          ]
      },
      {
          "featureType": "road",
          "stylers": [
            {
                "color": "#c4dce4"
            },
            {
                "visibility": "simplified"
            }
          ]
      },
      {
          "featureType": "road",
          "elementType": "geometry",
          "stylers": [
            {
                "color": "#d0d7e0"
            }
          ]
      },
      {
          "featureType": "road",
          "elementType": "labels",
          "stylers": [
            {
                "visibility": "off"
            }
          ]
      },
      {
          "featureType": "road",
          "elementType": "labels.icon",
          "stylers": [
            {
                "visibility": "off"
            }
          ]
      },
      {
          "featureType": "road.arterial",
          "elementType": "labels.text.fill",
          "stylers": [
            {
                "color": "#757575"
            }
          ]
      },
      {
          "featureType": "road.highway",
          "elementType": "geometry",
          "stylers": [
            {
                "color": "#dadada"
            }
          ]
      },
      {
          "featureType": "road.highway",
          "elementType": "labels.text.fill",
          "stylers": [
            {
                "color": "#616161"
            }
          ]
      },
      {
          "featureType": "road.local",
          "elementType": "labels.text.fill",
          "stylers": [
            {
                "color": "#9e9e9e"
            }
          ]
      },
      {
          "featureType": "transit",
          "stylers": [
            {
                "visibility": "off"
            }
          ]
      },
      {
          "featureType": "transit.line",
          "stylers": [
            {
                "color": "#e5e5e5"
            }
          ]
      },
      {
          "featureType": "transit.line",
          "elementType": "geometry",
          "stylers": [
            {
                "color": "#e5e5e5"
            }
          ]
      },
      {
          "featureType": "transit.station",
          "stylers": [
            {
                "color": "#eeeeee"
            }
          ]
      },
      {
          "featureType": "transit.station",
          "elementType": "geometry",
          "stylers": [
            {
                "color": "#eeeeee"
            }
          ]
      },
      {
          "featureType": "water",
          "stylers": [
            {
                "color": "#c9c9c9"
            }
          ]
      },
      {
          "featureType": "water",
          "elementType": "geometry",
          "stylers": [
            {
                "color": "#c9c9c9"
            }
          ]
      },
      {
          "featureType": "water",
          "elementType": "labels.text",
          "stylers": [
            {
                "visibility": "off"
            }
          ]
      },
      {
          "featureType": "water",
          "elementType": "labels.text.fill",
          "stylers": [
            {
                "color": "#9e9e9e"
            }
          ]
      }
                    ],
                    scrollwheel: false,
                    navigationControl: true,
                    scaleControl: true,
                    //                 draggable: false
                };



                map = new google.maps.Map(document.getElementById('office-map'), mapOptions);
                var marker = new google.maps.Marker({
                    position: new google.maps.LatLng(lat, long),
                    map: map,
                    icon: icon,
                    title: $('.main-welcome h1').text()
                });

                var contentString = '<div class="contact-box branch-item-cont p-0" > <p class="box-contact"> ' + $('.office-latitude-cont').html() + '</p> <p class="box-contact">' + $('.office-longitude-cont').html() + '</p></div>';




                var infowindow = new google.maps.InfoWindow({
                    content: contentString
                });


                marker.addListener('click', function () {
                    //           infowindow.open(map, marker);

                    window.open(chkOrgin() + '?lat=' + lat + "&lng=" + long, "MyTargetWindowName");

                });


            }, 100);




        }

    }



}




;