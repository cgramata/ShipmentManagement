angular.module('myUserIndex', [])
    .controller('UserIndexController', function ($scope) {
        $scope.userList = null;
        userIndexService.GetListOfUsers().then(function (d) {
            $scope.userList = d.data;
        }, function (error) {
            alert('Failed');
        });
    });