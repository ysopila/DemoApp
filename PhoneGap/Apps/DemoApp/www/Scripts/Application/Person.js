var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Application;
(function (Application) {
    (function (Gender) {
        Gender._map = [];
        Gender._map[0] = "Male";
        Gender.Male = 0;
        Gender._map[1] = "Female";
        Gender.Female = 1;
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
        function PersonController($scope, $rootScope, $resource, $routeParams) {
            if (this.$service == null) {
                this.$service = new PersonService($resource);
            }
            this.SetupScope($scope, $rootScope);
        }
        PersonController.prototype.SetupScope = function ($scope, $rootScope) {
            var _this = this;

            $scope.$on('loadPerson', function (event, id) {
                _this.$service.Get(id, function (data) {
                    data.Photo = Application.Service.$url + data.Photo;
                    $scope.Person = data;
                }, function (error) {
                    console.log(error);
                });
            });

            $scope.Gender = Gender;
            $scope.MapGender = function (value) {
                return Gender._map[value];
            };
            $scope.Save = function () {
                _this.$service.Update($scope.Person, function (data) {
                    $rootScope.$broadcast('loadPerson', data.Id);
                    $rootScope.$broadcast('updateCollection');
                }, function (error) {
                    console.log(error);
                });
            };
            $scope.Choose = function (sourceType) {
                navigator.camera.getPicture(function (uri) {
                    $scope.Person.PhotoURI = uri;
                }, function (error) {
                    console.log(error);
                }, {
                    quality: 45,
                    destinationType: Camera.DestinationType.FILE_URI,
                    encodingType: Camera.EncodingType.JPEG,
                    sourceType: sourceType
                });
            };
        };
        return PersonController;
    })();
    Application.PersonController = PersonController;
})(Application || (Application = {}));
