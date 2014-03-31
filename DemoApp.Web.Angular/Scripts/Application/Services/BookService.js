/// <reference path="../_references.ts"/>
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Application;
(function (Application) {
    var BookService = (function (_super) {
        __extends(BookService, _super);
        function BookService($resource) {
            _super.call(this, 'Book', $resource);
        }
        return BookService;
    })(Application.Service);
    Application.BookService = BookService;
})(Application || (Application = {}));
