/// <reference path="_references.ts"/>
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Application;
(function (Application) {
    var Book = (function (_super) {
        __extends(Book, _super);
        function Book(model) {
            _super.call(this, model);
            this.Published = model.Published;
            this.Copyright = model.Copyright;
            this.Author = model.Author;
        }
        return Book;
    })(Application.Content);
    Application.Book = Book;

    var BookService = (function (_super) {
        __extends(BookService, _super);
        function BookService($resource) {
            _super.call(this, 'Book', $resource);
        }
        return BookService;
    })(Application.Service);
    Application.BookService = BookService;

    var BookController = (function () {
        function BookController($scope, $rootScope, $resource, $routeParams, $fileUploader) {
            if (this.$service == null)
                this.$service = new BookService($resource);
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
