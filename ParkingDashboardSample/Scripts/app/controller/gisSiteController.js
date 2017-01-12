GisSites.controller('GitSitesController',
    function ($scope, GisSiteService, uiGmapGoogleMapApi, uiGmapIsReady) {
        $scope.map = { control: {} };// { center: { latitude: 17.406278, longitude: 78.469060 }, zoom: 16 }}
        $scope.markers = [];
        $scope.locations = [];
        $scope.message = "Infrgistics";
        $scope.control = {};
        $scope.hide = true;
        var sitesNamesAdresses = [];

        getSites();

        function getSites() {
            GisSiteService.GisSites()
                .then(function successCallback(response) {
                    $scope.siteName = response.data;
                        var i = 0;
                        $scope.siteName.forEach(function(s) {
                            console.log(s.SiteName);
                            var icon;
                            if (s.Status === "Warning") {
                                icon = "http://maps.google.com/mapfiles/ms/icons/yellow-dot.png";
                            } else if (s.Status === "Faults") {
                                icon = 'http://maps.google.com/mapfiles/ms/icons/red-dot.png';
                            } else {
                                icon = 'http://maps.google.com/mapfiles/ms/icons/green-dot.png';
                            }
                            sitesNamesAdresses[i] = { 'address': s.SiteName, 'icon': icon }
                            i++;
                        });
                        console.log($scope);
                    },
                    function errorCallback(response) {
                        $scope.status = 'Unable to load customer data: ' + response.message;
                        console.log($scope.status);
                    });
            /*.success(function (site) {
                $scope.siteName = site;
                var i = 0;
                $scope.siteName.forEach(function (s) {
                    console.log(s.SiteName);
                    var icon;
                    if (s.Status === "Warning") {
                        icon = "http://maps.google.com/mapfiles/ms/icons/yellow-dot.png";
                    }
                    else if (s.Status === "Faults") {
                        icon = 'http://maps.google.com/mapfiles/ms/icons/red-dot.png';
                    } else {
                        icon = 'http://maps.google.com/mapfiles/ms/icons/green-dot.png';
                    }
                    sitesNamesAdresses[i] = { 'address': s.SiteName, 'icon': icon }
                    i++;
                });
                console.log($scope);
            })*/
            /*.error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });*/
        }

        /* var areaLat = 44.2126995,
           areaLng = -100.2471641,
           areaZoom = 3;*/
        $scope.areaLat;
        $scope.areaLng;
        $scope.areaZoom = 3;

        uiGmapGoogleMapApi.then(function (maps) {
            var geocoder = new maps.Geocoder();
            geocoder.geocode({ "address": "Tel Aviv Israel" },
                function (results, status) {
                    if (status === maps.GeocoderStatus.OK && results.length > 0) {
                        var location = results[0].geometry.location;
                        $scope.areaLat = location.lat();
                        $scope.areaLng = location.lng();

                        $scope.map = { control: {}, center: { latitude: $scope.areaLat, longitude: $scope.areaLng }, zoom: $scope.areaZoom };
                    }
                });
            $scope.markers = [];
            sitesNamesAdresses.forEach(function (elemnt) {
                geocoder.geocode({ "address": elemnt.address },
                    function (results, status) {
                        if (status === maps.GeocoderStatus.OK && results.length > 0) {
                            var lt = results[0].geometry.location.lat();
                            var lg = results[0].geometry.location.lng();
                            $scope.markers.push({
                                id: results[0].place_id,
                                coords: { latitude: lt, longitude: lg },
                                options: { icon: elemnt.icon, label: "A" },

                                //title: data.data.title, //title of the makers
                                address: results[0].formatted_address
                                //image: data.data.ImagePath //image --optional
                            });
                        }

                    });


            });


            $scope.options = { scrollwheel: false };


        });

        uiGmapIsReady.promise()
            .then(function (map_instances) {
                var map1 = $scope.map.control.getGMap(); // get map object through $scope.map.control getGMap() function
                var map2 = map_instances[0].map; // get map object through array object returned by uiGmapIsReady promise
            });
        //$scope.ShowLocation = function (locationID) {
        //    $http.get('/home/GetMarkerData', {
        //        params: {
        //            locationID: locationID
        //        }
        //    }).then(function (data) {
        //        $scope.markers = [];
        //        $scope.markers.push({
        //            id: data.data.LocationID,
        //            coords: { latitude: data.data.Lat, longitude: data.data.Long },
        //            title: data.data.title,     //title of the makers
        //            address: data.data.Address,     //Address of the makers
        //            image : data.data.ImagePath     //image --optional
        //        });
        //        //set map focus to center
        //        $scope.map.center.latitude = data.data.Lat;
        //        $scope.map.center.longitude = data.data.Long;
        //    }, function () {
        //        alert('Error'); //shows error if connection lost or error occurs
        //    });
        //}


    });
