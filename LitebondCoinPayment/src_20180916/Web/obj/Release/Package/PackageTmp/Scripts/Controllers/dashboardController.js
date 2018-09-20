'use strict';
myApp.controller('One', ['$scope', '$rootScope'],
function($scope) {
    $rootScope.$on("CallParentMethod", function(){
        $scope.parentmethod();
    });

    $scope.parentmethod = function() {
        // task
    }
});
myApp.controller('two', ['$scope', '$rootScope'],
function($scope) {
    $scope.childmethod = function() {
        $rootScope.$emit("CallParentMethod", {});
    }
});
myApp.controller("DashboardController", function ($scope, $rootScope, DashboardServices) {

    var userId = window._userId;

    $rootScope.$on("AddNewProjectSuccsess", function () {
        $scope.load();
        $scope.loadT();
    });


    $scope.load = function () {
       
        
        DashboardServices.GetAllMyProject(userId).then(function (d) {
            $scope.ListMyProject = d.data; //Success callback
        }, function (error) {
            alert('Error!'); // Failed Callback
        });
    }
    $scope.loadT = function () {
        DashboardServices.GetAllMyTeam(userId).then(function (d) {
            $scope.ListMyTeam2 = d.data; //Success callback
        }, function (error) {
            alert('Error!'); // Failed Callback
        });
    }


 

    $scope.submitText = "Save";
    $scope.submitted = false;
    $scope.isEdit = false;
    $scope.message = '';
    $scope.isFormValid = false;
    $scope.Department = {
        Id: 1,
        Name: '',
        Description: '',

    };
    //Check form Validation // here f1 is our form name
    $scope.$watch('f1.$valid', function (newValue) {
        $scope.isFormValid = newValue;
    });
    $scope.edit = function (item) {
        $scope.Department.Id = item.Id;
        $scope.Department.Name = item.Name;
        $scope.Department.Description = item.Description;
        $scope.submitText = "Update";
        $scope.isEdit = true;
    }
   
    $scope.SaveData = function (data) {
        if ($scope.submitText == 'Save') {
            $scope.submitted = true;
            $scope.message = '';


            if ($scope.isFormValid) {
                $scope.submitText = 'Đang xử lý...';
                $scope.Department = data;
                Services.Create($scope.Department).then(function (d) {

                    if (d > 1) {

                        $scope.ListDepartment.push(data);
                        notifySuccess('Thêm phòng ban thành công!');
                        ClearForm();
                        $("#myFormDepartmentModal").modal('hide');
                    }
                });
            }
            else {
                $scope.message = 'Vui lòng nhập đủ thông tin';
            }
        }
        else {
            $scope.submitted = true;
            $scope.message = '';

            if ($scope.isFormValid) {
                $scope.submitText = 'Đang xử lý...';
                $scope.Department = data;
                Services.Edit($scope.Department).then(function (d) {


                    if (d === 1) {
                        notifySuccess('Cập nhật phòng ban thành công!');
                        //have to clear form here
                        ClearForm();
                    }
                    $scope.submitText = "Save";
                });
            }
            else {
                $scope.message = 'Vui lòng nhập đủ thông tin';
            }
        }
    }

    function ClearForm() {
        $scope.Department = {};
        $scope.Department.Id = 1;
        $scope.f1.$setPristine(); //here f1 our form name
        $scope.submitted = false;
        $scope.loadingText = '';
        $scope.message = '';
        $scope.submitText = "Save";
        $scope.isEdit = false;
        $scope.load();
    };

}).factory('DashboardServices', function ($http, $q) {
    var fac = {};
    fac.GetAllMyProject = function (id) {
        return $http.get('/api/project/GetAllMyProject/'+id);
    }
    fac.GetAllMyTeam = function (id) {
        return $http.get('/api/team/GetAllMyTeam2/' + id);
    }
    fac.Create = function (data) {
        var defer = $q.defer();
        $http({
            url: '/api/Department',
            method: 'POST',
            data: JSON.stringify(data),
            headers: { 'content-type': 'application/json' }
        }).success(function (d) {
            // Success callback
            defer.resolve(d);
        }).error(function (e) {
            //Failed Callback
            alert('Error!');
            defer.reject(e);
        });
        return defer.promise;
    }
    fac.Edit = function (data) {
        var defer = $q.defer();
        $http({
            url: '/api/department',
            method: 'PUT',
            data: JSON.stringify(data),
            headers: { 'content-type': 'application/json' }
        }).success(function (d) {
            // Success callback
            defer.resolve(d);
        }).error(function (e) {
            console.log(e);
            //Failed Callback
            alert('Error!');
            defer.reject(e);
        });
        return defer.promise;
    }
    
    return fac;
});