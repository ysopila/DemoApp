/// <reference path="../_references.ts"/>

module Application {
    export class Content {
        Id: number;
        Name: string;
        Description: string;
        Photo: string; 
        Type: string; 
        constructor(model: Content) {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Description = model.Description;
            this.Photo = model.Photo;
            this.Type = model.Type;
        }
    }
}
