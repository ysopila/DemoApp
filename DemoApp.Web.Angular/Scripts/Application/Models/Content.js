/// <reference path="../_references.ts"/>
var Application;
(function (Application) {
    var Content = (function () {
        function Content(model) {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Description = model.Description;
            this.Photo = model.Photo;
            this.Type = model.Type;
        }
        return Content;
    })();
    Application.Content = Content;
})(Application || (Application = {}));
