/// <reference path="../_references.ts"/>
var Application;
(function (Application) {
    var PersonController = (function () {
        function PersonController($scope, $rootScope, $resource, $routeParams, $fileUploader) {
            if (this.$service == null)
                this.$service = new Application.PersonService($resource);

            this.$service.Get($routeParams.id, function (data) {
                $scope.Person = data;
                $scope.Uploader = $fileUploader.create({
                    scope: $scope,
                    autoUpload: true,
                    url: '/Api/Content/' + $scope.Person.Id
                });
            }, function (error) {
                console.log(error);
            });

            this.SetupScope($scope, $rootScope, $fileUploader);
        }
        PersonController.prototype.SetupScope = function ($scope, $rootScope, $fileUploader) {
            var _this = this;
            $scope.Gender = Application.Gender;
            $scope.MapGender = function () {
                return { 0: Application.Gender[0], 1: Application.Gender[1] };
            };
            $scope.Save = function () {
                _this.$service.Update($scope.Person, function (data) {
                    $scope.Person = data;
                    $rootScope.$broadcast('updateCollection');
                }, function (error) {
                    console.log(error);
                });
            };
        };
        PersonController.$viewTemplateUrl = '/Content/Views/Person.html';
        PersonController.$editTemplateUrl = '/Content/Views/PersonEdit.html';
        return PersonController;
    })();
    Application.PersonController = PersonController;
})(Application || (Application = {}));
