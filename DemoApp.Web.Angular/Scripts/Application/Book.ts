/// <reference path="_references.ts"/>

module Application {
    export class Book extends Content {
        Published: string;
        Copyright: string;
        Author: Person;
        constructor(model: Book) {
            super(model);
            this.Published = model.Published;
            this.Copyright = model.Copyright;
            this.Author = model.Author;
        }
    }

    export interface IBookScope extends IScope<Book> {
        Book: Book;
        Uploader: any;
        Authors: Person[];
        AuthorId: number;
        Save: Function;
    }

    export class BookService extends Service<Book> {
        constructor($resource: IResourceService) {
            super('Book', $resource);
        }
    }

    export class BookController {
        static $viewTemplateUrl = '/Content/Views/Book.html';
        static $editTemplateUrl = '/Content/Views/BookEdit.html';
        private $service: BookService;
        private $personService: PersonService;
        constructor($scope: IBookScope, $rootScope: ng.IScope, $resource: IResourceService, $routeParams: IRouteParams, $fileUploader) {
            if (this.$service == null)
                this.$service = new BookService($resource);
            if (this.$personService == null)
                this.$personService = new PersonService($resource);

            this.$personService.GetAll({}, (data) => {
                $scope.Authors = data;
            }, (error) => { console.log(error); });

            this.$service.Get($routeParams.id, (data) => {
                $scope.Book = data;
                $scope.AuthorId = $scope.Book.Author.Id;
                $scope.Uploader = $fileUploader.create({
                    scope: $scope,
                    autoUpload: true,
                    url: '/Api/Content/' + $scope.Book.Id
                });
            }, (error) => { console.log(error); });

            this.SetupScope($scope, $rootScope, $fileUploader);
        }

        SetupScope($scope: IBookScope, $rootScope: ng.IScope, $fileUploader) {
            $scope.$watch('AuthorId', (id) => {
                angular.forEach($scope.Authors, (value: Person) => {
                    if (value.Id == id)
                        $scope.Book.Author = value;
                });
            });
            $scope.Save = () => {
                this.$service.Update($scope.Book,
                    (data) => {
                        $scope.Book = data;
                        $scope.AuthorId = $scope.Book.Author.Id;
                        $rootScope.$broadcast('updateCollection');
                    },
                    (error) => { console.log(error); });
            }
        }
    }
}