/// <reference path="../_references.ts"/>

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
}