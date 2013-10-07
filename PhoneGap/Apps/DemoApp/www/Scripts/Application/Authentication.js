var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Application;
(function (Application) {
    var User = (function () {
        function User(model) {
            this.Id = model.Id;
            this.Username = model.Username;
            this.Password = model.Password;
            this.AccessToken = model.AccessToken;
        }
        return User;
    })();
    Application.User = User;

    var UserService = (function (_super) {
        __extends(UserService, _super);
        function UserService($resource) {
            _super.call(this, 'User', $resource);
        }
        return UserService;
    })(Application.Service);
    Application.UserService = UserService;

    var AuthenticationController = (function () {
        function AuthenticationController($scope, $rootScope, $resource, $http, Base64) {
            if (this.$service == null) {
                this.$service = new UserService($resource);
            }
            this.SetupScope($scope, $rootScope, $http, Base64);
        }
        AuthenticationController.prototype.SetupScope = function ($scope, $rootScope, $http, Base64) {
            var _this = this;

            $scope.Login = function () {
                $http.defaults.headers.common.Authorization = 'Basic ' + Base64.encode($scope.User.Username + ':' + $scope.User.Password);
            };
        };
        return AuthenticationController;
    })();
    Application.AuthenticationController = AuthenticationController;
})(Application || (Application = {}));