GisSites.factory('GisSiteService',  function ($http) {
    var GisSiteService = {};
    GisSiteService.GisSites = function () {
        return $http.get('/GisSitesData/GetSites')
    }
    return GisSiteService;

});
