angular.module('myUserIndex', [])
    .controller('UserIndexController', function ($scope, userIndexService) {
        $scope.userList = null;
        userIndexService.GetListOfUsers().then(function (d) {
            $scope.userList = d.data;
        }, function (error) {
            alert('Failed');
        });
    })
    .factory('userIndexService', function ($http) {
        var fac = {};
        fac.GetListOfUsers = function () {
            return $http.get('/User/GetUserList')
        }
        return fac;
    });