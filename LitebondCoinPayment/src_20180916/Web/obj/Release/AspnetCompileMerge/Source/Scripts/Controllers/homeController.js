var myApp = angular.module("MyApp", []);

myApp.controller('homeController', function ($scope, HomeCommonService) { //inject ContactService
    $scope.ListLibrary = null;
    HomeCommonService.GetLibrary().then(function (d) {
        $scope.ListLibrary = d.data; // Success
    }, function () {
        alert('Failed'); // Failed
    });
})
.factory('HomeCommonService', function ($http) { // here I have created a factory which is a populer way to create and configure services
    var fac = {};
    fac.GetLibrary = function () {
        return $http.get('/api/library');
    }
    return fac;
});
