/// <reference path="../_references.ts"/>
var Application;
(function (Application) {
    var ContentController = (function () {
        function ContentController($scope, $resource) {
            if (this.$service == null)
                this.$service = new Application.ContentService($resource);

            this.SetupScope($scope);

            $scope.$broadcast('updateCollection');
        }
        ContentController.prototype.SetupScope = function ($scope) {
            var _this = this;
            $scope.$on('updateCollection', function () {
                _this.$service.GetAll({}, function (data) {
                    $scope.Collection = data;
                }, function (error) {
                    console.log(error);
                });
            });
        };
        return ContentController;
    })();
    Application.ContentController = ContentController;
})(Application || (Application = {}));
