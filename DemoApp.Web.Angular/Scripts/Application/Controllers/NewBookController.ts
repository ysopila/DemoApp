/// <reference path="../_references.ts"/>

module Application {
    export interface IBookScope extends ng.IScope {
        CurrentBook: Book;
        AuthorList: Array<Person>;
        AuthorAutoCompliteOption: Object;
        Add: Function;
        ViewDate: Function;

    }

    export class NewBookController {
        static $viewTemplateUrl = '/Content/Views/NewBook.html';
        private $service: BookService;
        private $personService: PersonService;
        constructor($scope: IBookScope, $rootScope: ng.IScope, $resource: IResourceService) {
            if (this.$service == null)
                this.$service = new BookService($resource);
            if (this.$personService == null)
                this.$personService = new PersonService($resource);

            this.SetupScope($scope, $rootScope);
        }
        SetupScope($scope: IBookScope, $rootScope: ng.IScope) {
            $scope.Add = () => {
                console.log($scope.CurrentBook);
            }
            $scope.ViewDate = () => {
                if ($scope.CurrentBook && $scope.CurrentBook.Published)
                    return moment($scope.CurrentBook.Published).format("MMM Do YYYY");
                return "";
            }

            $scope.AuthorAutoCompliteOption = {
                options: {
                    source: (request, response) => {
                        this.$personService.GetAll({ authorName: request.term },
                            (data: Array<Person>) => {
                                data.forEach((person: any) => { person.label = person.Name });
                                response(data);
                            },
                            (error) => {
                                console.log(error);
                            });
                    },
                    minLength: 2,
                    change: (event, ui) => {
                        $scope.CurrentBook.Author = ui.item;
                        $("#auto-message").text("");
                    },
                    appendTo: $('#test-auto'),
                    messages: {
                        noResults: () => { },
                        results: (amount) => { }
                        //noResults: () => { $("#auto-message").text("No search results. but you can added a new author."); },
                        //results: (amount) => {
                        //    $("#auto-message").text(
                        //        amount + (amount > 1 ? " results are" : " result is") +
                        //        " available, use up and down arrow keys to navigate.");
                        //}
                    }
                }
            }

        }
    }
}