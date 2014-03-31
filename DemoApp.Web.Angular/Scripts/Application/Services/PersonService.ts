/// <reference path="../_references.ts"/>

module Application {
    export class PersonService extends Service<Person> {
        constructor($resource: IResourceService) {
            super('Person', $resource);
        }
    }
}