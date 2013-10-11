/// <reference path="_references.ts"/>
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Application;
(function (Application) {
    (function (Gender) {
        Gender[Gender["Male"] = 0] = "Male";
        Gender[Gender["Female"] = 1] = "Female";
    })(Application.Gender || (Application.Gender = {}));
    var Gender = Application.Gender;

    var Person = (function (_super) {
        __extends(Person, _super);
        function Person(model) {
            _super.call(this, model);
            this.FirstName = model.FirstName;
            this.LastName = model.LastName;
            this.BirthDate = model.BirthDate;
            this.Gender = model.Gender;
        }
        return Person;
    })(Application.Content);
    Application.Person = Person;

    var PersonService = (function (_super) {
        __extends(PersonService, _super);
        function PersonService($resource) {
            _super.call(this, 'Person', $resource);
        }
        return PersonService;
    })(Application.Service);
    Application.PersonService = PersonService;

    var PersonController = (function () {
        function PersonController($scope, $rootScope, $resource, $routeParams, $fileUploader) {
            if (this.$service == null)
                this.$service = new PersonService($resource);

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
            $scope.Gender = Gender;
            $scope.MapGender = function () {
                return { 0: Gender[0], 1: Gender[1] };
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
