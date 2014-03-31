/// <reference path="../_references.ts"/>

module Application {
    export class BookService extends Service<Book> {
        constructor($resource: IResourceService) {
            super('Book', $resource);
        }
    }
}