/// <reference path="_references.ts"/>

module Application {
    angular.module('Application', ['ngResource'])
        .config(['$routeProvider', '$locationProvider', '$httpProvider',
            ($routeProvider: ng.IRouteProvider, $locationProvider: ng.ILocationProvider, $httpProvider: ng.IHttpProvider) => {
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

                $routeProvider.otherwise({ redirectTo: '/' });
                $httpProvider.defaults.withCredentials = true;
                $httpProvider.responseInterceptors.push(['$location', '$q', ($location: ng.ILocationService, $q: ng.IQService) => {
                    return (promise: ng.IPromise) => {
                        return promise.then((response: any) => {
                            return response;
                        }, (response: ng.IHttpPromiseCallbackArg) => {
                                if (response.status === 401) {
                                    $location.path('/');
                                    return $q.reject(response);
                                }
                                return $q.reject(response);
                            });
                    };
                }]);
            }])
        .controller('AccountController', Application.AccountController)
        .controller('ContentController', Application.ContentController)
        .controller('PersonController', Application.PersonController)
        .controller('BookController', Application.BookController);
}