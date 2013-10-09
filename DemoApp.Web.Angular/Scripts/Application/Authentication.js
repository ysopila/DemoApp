/// <reference path="_references.ts"/>
var Application;
(function (Application) {
    var AuthResponse = (function () {
        function AuthResponse() {
        }
        return AuthResponse;
    })();
    Application.AuthResponse = AuthResponse;

    var Authentication = (function () {
        function Authentication($http, $location) {
            this.$http = $http;
            this.$location = $location;
        }
        Authentication.prototype.IsAuthenticated = function (success) {
            var _this = this;
            this.$http.get('/Account/IsAuthenticated').success(function (response) {
                if (response.Success) {
                    _this.$http.defaults.headers.common.Authorization = response.AuthHeader;
                    _this.$location.path('/');
                    success();
                }
                _this.$location.path('/');
            });
        };

        Authentication.prototype.SignIn = function (model, success, error) {
            var _this = this;
            this.$http.post('/Account/SignIn', model).success(function (response) {
                if (response.Success) {
                    _this.$http.defaults.headers.common.Authorization = response.AuthHeader;
                    _this.$location.path('/');
                    success();
                } else
                    alert(response.ErrorMessage);
            }).error(error);
        };

        Authentication.prototype.SignOut = function (model, success, error) {
            var _this = this;
            this.$http.post('/Account/SignOut', model).success(function () {
                delete _this.$http.defaults.headers.common.Authorization;
                success();
            }).error(error);
        };

        Authentication.prototype.Register = function (model, success, error) {
            var _this = this;
            this.$http.post('/Account/Register', model).success(function (response) {
                if (response.Success) {
                    _this.$http.defaults.headers.common.Authorization = response.AuthHeader;
                    _this.$location.path('/');
                    success();
                } else
                    alert(response.ErrorMessage);
            }).error(error);
        };
        return Authentication;
    })();
    Application.Authentication = Authentication;
})(Application || (Application = {}));
