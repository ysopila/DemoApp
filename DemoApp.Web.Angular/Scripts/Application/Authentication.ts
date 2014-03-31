/// <reference path="_references.ts"/>

module Application {

    export class AuthResponse {
        Success: boolean;
        AuthHeader: string;
        ErrorMessage: string;
        Username: string;
    }

    export class Authentication {
        private $http: ng.IHttpService
        private $location: ng.ILocationService
        constructor($http: ng.IHttpService, $location: ng.ILocationService) {
            this.$http = $http;
            this.$location = $location;
        }

        IsAuthenticated(success: (isAuthenticated: boolean, username: string) => void) {
            this.$http.get('/Account/IsAuthenticated').success((response: AuthResponse) => {
                success(response.Success, response.Username);
                if (response.Success) {
                    this.$http.defaults.headers.common.Authorization = response.AuthHeader;
                }
                else
                    this.$location.path('/');
            });
        }

        SignIn(model: Account, success: () => void, error: (reason: any) => void) {
            this.$http.post('/Account/SignIn', model)
                .success((response: AuthResponse) => {
                    if (response.Success) {
                        success();
                        this.$http.defaults.headers.common.Authorization = response.AuthHeader;
                    }
                    else
                        alert(response.ErrorMessage);
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

        Register(model: Account, success: () => void, error: (reason: any) => void) {
            this.$http.post('/Account/Register', model)
                .success((response: AuthResponse) => {
                    if (response.Success) {
                        success();
                        this.$http.defaults.headers.common.Authorization = response.AuthHeader;
                    }
                    else
                        alert(response.ErrorMessage);
                })
                .error(error);
        }
    }
}