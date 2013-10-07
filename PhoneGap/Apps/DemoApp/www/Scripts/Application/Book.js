var __extends = this.__extends || function (d, b) {
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
        function BookController($scope, $rootScope, $resource) {
            if (this.$service == null) {
                this.$service = new BookService($resource);
            }
            if (this.$personService == null) {
                this.$personService = new Application.PersonService($resource);
            }
            this.$personService.GetAll({}, function (data) {
                $scope.Authors = data;
            }, function (error) {
                console.log(error);
            });
            
            this.SetupScope($scope, $rootScope);
        }
        BookController.prototype.SetupScope = function ($scope, $rootScope) {
            var _this = this;
            $scope.$watch('AuthorId', function (id) {
                angular.forEach($scope.Authors, function (value) {
                    if (value.Id == id) {
                        $scope.Book.Author = value;
                    }
                });
            });

            $scope.$on('loadBook', function (event, id) {
                _this.$service.Get(id, function (data) {
                    data.Photo = Application.Service.$url + data.Photo;
                    $scope.Book = data;
                    $scope.AuthorId = $scope.Book.Author.Id;
                }, function (error) {
                    console.log(error);
                });
            });

            $scope.Save = function () {
                _this.$service.Update($scope.Book, function (data) {
                    $rootScope.$broadcast('loadBook', data.Id);
                    $rootScope.$broadcast('updateCollection');
                }, function (error) {
                    console.log(error);
                });
            };
            $scope.Choose = function (sourceType) {
                navigator.camera.getPicture(function (uri) {
                    $scope.Book.PhotoURI = uri;
                }, function () {
                }, {
                    quality: 95,
                    destinationType: Camera.DestinationType.FILE_URI,
                    encodingType: Camera.EncodingType.JPEG,
                    sourceType: sourceType
                });
            };
        };
        return BookController;
    })();
    Application.BookController = BookController;    
})(Application || (Application = {}));
