angular.module('myUserIndex', [])
.controller('UserIndexController', function ($scope) {
    $scope.userList = window.jj;
})