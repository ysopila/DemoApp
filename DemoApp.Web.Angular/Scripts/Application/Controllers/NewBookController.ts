/// <reference path="../_references.ts"/>

module Application {
    export interface IBookScope extends IScope<Book> {
    }

    export class NewBookController {
        static $viewTemplateUrl = '/Content/Views/NewBook.html';
    }
}