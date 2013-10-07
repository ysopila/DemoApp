/// <reference path="_references.ts"/>

module Application {
    export class Account {
        Username: string;
        Password: string;
    }

    export interface IAccountScope extends IScope<Account> {
        SignIn: Function;
        SignOut: Function;
        Account: Account;
        IsAuthenticated: boolean;
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
                    $rootScope.$broadcast("updateCollection");
                }, (error) => { console.log(error); });
            };

            $scope.SignOut = () => {
                this.auth.SignOut($scope.Account, () => { $scope.IsAuthenticated = false; }, (error) => { console.log(error); });
            };
        }
    }
}