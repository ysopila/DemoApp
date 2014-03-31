/// <reference path="../_references.ts"/>

module Application {

    export class Book extends Content {
        Published: string;
        Copyright: string;
        Author: Person;
        constructor(model: Book) {
            super(model);
            this.Published = model.Published;
            this.Copyright = model.Copyright;
            this.Author = model.Author;
        }
    }
}