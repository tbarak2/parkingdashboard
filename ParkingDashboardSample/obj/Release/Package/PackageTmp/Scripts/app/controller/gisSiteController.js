GisSites.controller('GitSitesController', function ($scope, GisSiteService, uiGmapGoogleMapApi, uiGmapIsReady) {
    $scope.map = { center: { latitude: 17.406278, longitude: 78.469060 }, zoom: 16 }
    $scope.markers = [];
    $scope.locations = [];
    $scope.message = "Infrgistics";
    $scope.control = {};
    $scope.hide = true;
    getSites();

    function getSites() {
        GisSiteService.GisSites()
        .success(function (site) {
            $scope.siteName = site;
            console.log($scope);
        })
         .error(function (error) {
             $scope.status = 'Unable to load customer data: ' + error.message;
             console.log($scope.status);
         });
    }

    var areaLat = 44.2126995,
      areaLng = -100.2471641,
      areaZoom = 3;

    uiGmapGoogleMapApi.then(function (maps) {
        $scope.map = { center: { latitude: areaLat, longitude: areaLng }, zoom: areaZoom };
        $scope.options = { scrollwheel: false };
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
