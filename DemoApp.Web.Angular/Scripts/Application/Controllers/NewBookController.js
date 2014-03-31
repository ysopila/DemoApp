/// <reference path="../_references.ts"/>
var Application;
(function (Application) {
    var NewBookController = (function () {
        function NewBookController($scope, $rootScope, $resource) {
            if (this.$service == null)
                this.$service = new Application.BookService($resource);
            if (this.$personService == null)
                this.$personService = new Application.PersonService($resource);

            this.SetupScope($scope, $rootScope);
        }
        NewBookController.prototype.SetupScope = function ($scope, $rootScope) {
            var _this = this;
            $scope.Add = function () {
                console.log($scope.CurrentBook);
            };
            $scope.ViewDate = function () {
                if ($scope.CurrentBook && $scope.CurrentBook.Published)
                    return moment($scope.CurrentBook.Published).format("MMM Do YYYY");
                return "";
            };

            $scope.AuthorAutoCompliteOption = {
                options: {
                    source: function (request, response) {
                        _this.$personService.GetAll({ authorName: request.term }, function (data) {
                            data.forEach(function (person) {
                                person.label = person.Name;
                            });
                            response(data);
                        }, function (error) {
                            console.log(error);
                        });
                    },
                    minLength: 2,
                    change: function (event, ui) {
                        $scope.CurrentBook.Author = ui.item;
                        $("#auto-message").text("");
                    },
                    appendTo: $('#test-auto'),
                    messages: {
                        noResults: function () {
                        },
                        results: function (amount) {
                        }
                    }
                }
            };
        };
        NewBookController.$viewTemplateUrl = '/Content/Views/NewBook.html';
        return NewBookController;
    })();
    Application.NewBookController = NewBookController;
})(Application || (Application = {}));
