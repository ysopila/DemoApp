/// <reference path="_references.ts"/>
var Application;
(function (Application) {
    var Account = (function () {
        function Account() {
        }
        return Account;
    })();
    Application.Account = Account;

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
        };
        return AccountController;
    })();
    Application.AccountController = AccountController;
})(Application || (Application = {}));
