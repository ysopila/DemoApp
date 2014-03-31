/// <reference path="../_references.ts"/>
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Application;
(function (Application) {
    var PersonService = (function (_super) {
        __extends(PersonService, _super);
        function PersonService($resource) {
            _super.call(this, 'Person', $resource);
        }
        return PersonService;
    })(Application.Service);
    Application.PersonService = PersonService;
})(Application || (Application = {}));
