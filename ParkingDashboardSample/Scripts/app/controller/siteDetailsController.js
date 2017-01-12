DashBoard.controller('SiteDetailsController',
    function($scope,$window, GisSiteService) {
        console.log(window.siteId);
        var successCallback = function(response) {
            $scope.siteDetails = response.data;
            $scope.GetSitesEvents(response.data.SiteName);
        };

        var failCallback = function(response) {
            console.log(response.message);
        };


       
        GisSiteService.GetSitesDetails(window.siteId).then(successCallback,failCallback);

         $scope.GetSitesEvents = function(siteName) {
             GisSiteService.GetSitesEvents(siteName).then(function(response) {
                 console.log(response.data);
                 $scope.events = response.data;
                 
             }, failCallback);
         }

         $scope.setClass = function(event) {
             if (event.Severity === 'Fault')
                 return 'danger';
             else if (event.Severity === 'Warning')
                 return 'warning';
             return 'success';
         }

         $scope.OpenCam = function(camId) {
             console.log(camId);
             $window.open('http://support:Hbk2405@108.35.104.190:10080', '_blank');
         }
        

    });