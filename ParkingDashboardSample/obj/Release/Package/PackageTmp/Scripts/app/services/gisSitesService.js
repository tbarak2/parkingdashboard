DashBoard.factory('GisSiteService', function ($http) {
    var GisSiteService = {};
    GisSiteService.GisSites = function () {
        return $http.get('/GisSitesData/GetSites');
    }

    GisSiteService.GetSitesDetails = function (id){
        return $http.get('/SiteData/GetSitesDetails/'+id);
    }

    GisSiteService.GetSitesEvents=function(id) {
        return $http.get('/SiteData/GetSitesEvents/' + id);
    }
    return GisSiteService;

});
