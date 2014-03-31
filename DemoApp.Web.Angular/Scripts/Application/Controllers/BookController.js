/// <reference path="../_references.ts"/>
var Application;
(function (Application) {
    var BookController = (function () {
        function BookController($scope, $rootScope, $resource, $routeParams, $fileUploader) {
            if (this.$service == null)
                this.$service = new Application.BookService($resource);
            if (this.$personService == null)
                this.$personService = new Application.PersonService($resource);

            this.$personService.GetAll({}, function (data) {
                $scope.Authors = data;
            }, function (error) {
                console.log(error);
            });

            this.$service.Get($routeParams.id, function (data) {
                $scope.Book = data;
                $scope.AuthorId = $scope.Book.Author.Id;
                $scope.Uploader = $fileUploader.create({
                    scope: $scope,
                    autoUpload: true,
                    url: '/Api/Content/' + $scope.Book.Id
                });
            }, function (error) {
                console.log(error);
            });

            this.SetupScope($scope, $rootScope, $fileUploader);
        }
        BookController.prototype.SetupScope = function ($scope, $rootScope, $fileUploader) {
            var _this = this;
            $scope.$watch('AuthorId', function (id) {
                angular.forEach($scope.Authors, function (value) {
                    if (value.Id == id)
                        $scope.Book.Author = value;
                });
            });
            $scope.Save = function () {
                _this.$service.Update($scope.Book, function (data) {
                    $scope.Book = data;
                    $scope.AuthorId = $scope.Book.Author.Id;
                    $rootScope.$broadcast('updateCollection');
                }, function (error) {
                    console.log(error);
                });
            };
        };
        BookController.$viewTemplateUrl = '/Content/Views/Book.html';
        BookController.$editTemplateUrl = '/Content/Views/BookEdit.html';
        return BookController;
    })();
    Application.BookController = BookController;
})(Application || (Application = {}));
