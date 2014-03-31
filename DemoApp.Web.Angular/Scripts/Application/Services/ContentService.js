/// <reference path="../_references.ts"/>
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Application;
(function (Application) {
    var ContentService = (function (_super) {
        __extends(ContentService, _super);
        function ContentService($resource) {
            _super.call(this, 'Content', $resource);
        }
        return ContentService;
    })(Application.Service);
    Application.ContentService = ContentService;
})(Application || (Application = {}));
