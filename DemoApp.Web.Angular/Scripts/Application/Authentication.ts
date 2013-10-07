/// <reference path="_references.ts"/>

module Application {

    export class AuthResponse {
        Success: boolean;
        AuthHeader: string;
    }

    export class Authentication {
        private $http: ng.IHttpService
        private $location: ng.ILocationService
        constructor($http: ng.IHttpService, $location: ng.ILocationService) {
            this.$http = $http;
            this.$location = $location;
        }

        SignIn(model: Account, success: () => void, error: (reason: any) => void) {
            this.$http.post('/Account/SignIn', model)
                .success((response: AuthResponse) => {
                    if (response.Success) {
                        this.$http.defaults.headers.common.Authorization = response.AuthHeader;
                        this.$location.path('/');
                        success();
                    }
                })
                .error(error);
        }

        SignOut(model: Account, success: () => void, error: (reason: any) => void) {
            this.$http.post('/Account/SignOut', model)
                .success(() => {
                    delete this.$http.defaults.headers.common.Authorization;
                    success();
                })
                .error(error);
        }
    }
}