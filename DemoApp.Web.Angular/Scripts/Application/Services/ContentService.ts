/// <reference path="../_references.ts"/>

module Application {
   
    export class ContentService extends Service<Content> {
        constructor($resource: IResourceService) {
            super('Content', $resource);
        }
    }
}