var myApp = angular.module("MyManageApp", []);

//myApp.config(function ($routeProvider, $locationProvider) {
// $routeProvider.html5Mode(true);
//$routeProvider.when('/', {
//    controller: 'homeController',
//    templateUrl: 'Index'
//}).when('Home/About',{
//    controller: 'homeController',
//    templateUrl: 'About'
//}).when('/lien-he',{

//    templateUrl: 'Home/Contact'
//}).otherwise({
//    redirectTo: '/'
//});

//$locationProvider.html5Mode({
//    enabled: true,
//    requireBase: false
//});
//})
myApp.run(function ($http) {
    // $http.get('/api/Template').success(function (data, $scope) {
    // $scope.universityName = data.Name;
    // });
});
myApp.controller("myAccountController", function ($scope, $rootScope, MyManageServices) {

    var userId = window._userId;
    MyManageServices.GetCurrentUser().then(function (d) {
        var data = d.data;
        
        $scope.CurrentUserViewModel = data;
        $('#data_user').val(data.UserId);
        $('#data_user').attr('data-email', data.Email);
        $('#data_user').attr('data-fullname', data.FullName);
        $('#data_user').attr('data-avatar', data.Avatar);
    });

    $rootScope.$on("AddNewProjectSuccsess", function () {
        $scope.load();
        $scope.loadT();
    });


    $scope.load = function () {

 
    }
    $scope.loadT = function () {
        
    }


    $scope.load();
    $scope.loadT();

     
}).factory('MyManageServices', function ($http, $q) {
    var fac = {};
    fac.GetCurrentUser = function () {
        return $http.get('/account/tgvuzgfuafr1yw5bqtxdenns0yqy02mw01qs1fms0ync05rs03qz0ymc01mcr1d');
    }
     
    return fac;
});

myApp.filter('unsafe', function ($sce) {
    return function (val) {
        return $sce.trustAsHtml(val);
    };
});
