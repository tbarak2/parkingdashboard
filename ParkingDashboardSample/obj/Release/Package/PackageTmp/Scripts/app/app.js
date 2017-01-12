var DashBoard = angular.module('DashBoard', ['uiGmapgoogle-maps'])
    .config(['uiGmapGoogleMapApiProvider', function (GoogleMapApiProviders) {
    GoogleMapApiProviders.configure({
        key: 'AIzaSyCDnVQMhcjxcPtuWg23KgElL2UAhFsPZwE',
        v: '3.25', //defaults to latest 3.X anyhow
        libraries: 'weather,geometry,visualization'
    });
}]
);