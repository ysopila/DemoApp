/// <reference path="../_references.ts"/>
var Application;
(function (Application) {
    var AccountController = (function () {
        function AccountController($scope, $rootScope, $http, $location) {
            this.auth = new Application.Authentication($http, $location);
            this.SetupScope($scope, $rootScope);
        }
        AccountController.prototype.SetupScope = function ($scope, $rootScope) {
            var _this = this;
            $scope.IsAuthenticated = false;
            $scope.SignIn = function () {
                _this.auth.SignIn($scope.Account, function () {
                    $scope.IsAuthenticated = true;
                    $scope.ShowSignInForm = false;
                    $rootScope.$broadcast("updateCollection");
                }, function (error) {
                    console.log(error);
                });
            };

            $scope.SignOut = function () {
                _this.auth.SignOut($scope.Account, function () {
                    $scope.IsAuthenticated = false;
                }, function (error) {
                    console.log(error);
                });
            };

            $scope.Register = function () {
                _this.auth.Register($scope.Account, function () {
                    $scope.IsAuthenticated = true;
                    $scope.ShowRegistrationForm = false;
                    $rootScope.$broadcast("updateCollection");
                }, function (error) {
                    console.log(error);
                });
            };

            this.auth.IsAuthenticated(function (isAuthenticated, username) {
                $scope.IsAuthenticated = isAuthenticated;
                $scope.Account = new Application.Account();
                $scope.Account.Username = username;
                $scope.IsLoaded = true;
                if (isAuthenticated)
                    $rootScope.$broadcast("updateCollection");
            });
        };
        return AccountController;
    })();
    Application.AccountController = AccountController;
})(Application || (Application = {}));
