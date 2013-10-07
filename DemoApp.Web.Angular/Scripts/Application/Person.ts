/// <reference path="_references.ts"/>

module Application {

    export enum Gender {
        Male,
        Female
    }

    export class Person extends Content {
        FirstName: string;
        LastName: string;
        BirthDate: string;
        Gender: Gender;
        constructor(model: Person) {
            super(model);
            this.FirstName = model.FirstName;
            this.LastName = model.LastName;
            this.BirthDate = model.BirthDate;
            this.Gender = model.Gender;
        }
    }

    export interface IPersonScope extends IScope<Person> {
        Person: Person;
        MapGender(): Object;
        Save: Function;
        Gender: any;
    }

    export class PersonService extends Service<Person> {
        constructor($resource: IResourceService) {
            super('Person', $resource);
        }
    }

    export class PersonController {
        static $viewTemplateUrl = '/Content/Views/Person.html';
        static $editTemplateUrl = '/Content/Views/PersonEdit.html';
        private $service: PersonService;
        constructor($scope: IPersonScope, $rootScope: ng.IScope, $resource: IResourceService, $routeParams: IRouteParams) {
            if (this.$service == null)
                this.$service = new PersonService($resource);

            this.$service.Get($routeParams.id, (data) => {
                $scope.Person = data;
            }, (error) => { console.log(error); });

            this.SetupScope($scope, $rootScope);
        }

        SetupScope($scope: IPersonScope, $rootScope: ng.IScope) {
            $scope.Gender = Gender;
            $scope.MapGender = () => {
                return { 0: Gender[0], 1: Gender[1] };
            }
            $scope.Save = () => {
                this.$service.Update($scope.Person,
                    (data) => { $rootScope.$broadcast('updateCollection'); },
                    (error) => { console.log(error); });
            }
        }
    }
}
