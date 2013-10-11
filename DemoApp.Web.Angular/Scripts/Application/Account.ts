/// <reference path="_references.ts"/>

module Application {
    export class Account {
        Username: string;
        Password: string;
    }

    export interface IAccountScope extends IScope<Account> {
        SignIn: Function;
        SignOut: Function;
        Register: Function;
        Account: Account;
        IsAuthenticated: boolean;
        IsLoaded: boolean;
        ShowRegistrationForm: boolean;
        ShowSignInForm: boolean;
    }

    export class AccountController {
        private auth: Authentication
        constructor($scope: IAccountScope, $rootScope: ng.IScope, $http: ng.IHttpService, $location: ng.ILocationService) {
            this.auth = new Authentication($http, $location);
            this.SetupScope($scope, $rootScope);
        }

        SetupScope($scope: IAccountScope, $rootScope: ng.IScope) {
            $scope.IsAuthenticated = false;
            $scope.SignIn = () => {
                this.auth.SignIn($scope.Account, () => {
                    $scope.IsAuthenticated = true;
                    $scope.ShowSignInForm = false;
                    $rootScope.$broadcast("updateCollection");
                }, (error) => { console.log(error); });
            };

            $scope.SignOut = () => {
                this.auth.SignOut($scope.Account, () => { $scope.IsAuthenticated = false; }, (error) => { console.log(error); });
            };

            $scope.Register = () => {
                this.auth.Register($scope.Account, () => {
                    $scope.IsAuthenticated = true;
                    $scope.ShowRegistrationForm = false;
                    $rootScope.$broadcast("updateCollection");
                }, (error) => { console.log(error); });
            }

            this.auth.IsAuthenticated((isAuthenticated: boolean, username: string) => {
                $scope.IsAuthenticated = isAuthenticated;
                $scope.Account = new Account();
                $scope.Account.Username = username;
                $scope.IsLoaded = true;
                if (isAuthenticated)
                    $rootScope.$broadcast("updateCollection");
            });

        }
    }
}