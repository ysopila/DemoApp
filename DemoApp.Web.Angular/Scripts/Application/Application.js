/// <reference path="_references.ts"/>
var Application;
(function (Application) {
    angular.module('Application', ['ngResource', 'angularFileUpload', 'ngRoute', 'ui.autocomplete', 'ui.bootstrap.datetimepicker']).config([
        '$routeProvider', '$locationProvider', '$httpProvider',
        function ($routeProvider, $locationProvider, $httpProvider) {
            $locationProvider.hashPrefix('!');
            $routeProvider.when('/', {
                controller: Application.ContentController
            });
            $routeProvider.when('/person/:id', {
                templateUrl: Application.PersonController.$viewTemplateUrl,
                controller: Application.PersonController
            });
            $routeProvider.when('/person/edit/:id', {
                templateUrl: Application.PersonController.$editTemplateUrl,
                controller: Application.PersonController
            });
            $routeProvider.when('/book/:id', {
                templateUrl: Application.BookController.$viewTemplateUrl,
                controller: Application.BookController
            });
            $routeProvider.when('/book/edit/:id', {
                templateUrl: Application.BookController.$editTemplateUrl,
                controller: Application.BookController
            });
            $routeProvider.when('/book/add/new', {
                templateUrl: Application.NewBookController.$viewTemplateUrl,
                controller: Application.NewBookController
            });

            $routeProvider.otherwise({ redirectTo: '/' });
            $httpProvider.defaults.withCredentials = true;
            $httpProvider.responseInterceptors.push([
                '$location', '$q', function ($location, $q) {
                    return function (promise) {
                        return promise.then(function (response) {
                            return response;
                        }, function (response) {
                            if (response.status === 401) {
                                $location.path('/');
                                return $q.reject(response);
                            }
                            return $q.reject(response);
                        });
                    };
                }]);
        }]).controller('AccountController', Application.AccountController).controller('ContentController', Application.ContentController).controller('PersonController', Application.PersonController).controller('NewBookController', Application.NewBookController).controller('BookController', Application.BookController);
})(Application || (Application = {}));
