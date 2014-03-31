/// <reference path="../_references.ts"/>
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Application;
(function (Application) {
    var Book = (function (_super) {
        __extends(Book, _super);
        function Book(model) {
            _super.call(this, model);
            this.Published = model.Published;
            this.Copyright = model.Copyright;
            this.Author = model.Author;
        }
        return Book;
    })(Application.Content);
    Application.Book = Book;
})(Application || (Application = {}));
