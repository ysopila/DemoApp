/// <reference path="../_references.ts"/>
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Application;
(function (Application) {
    (function (Gender) {
        Gender[Gender["Male"] = 0] = "Male";
        Gender[Gender["Female"] = 1] = "Female";
    })(Application.Gender || (Application.Gender = {}));
    var Gender = Application.Gender;

    var Person = (function (_super) {
        __extends(Person, _super);
        function Person(model) {
            _super.call(this, model);
            this.FirstName = model.FirstName;
            this.LastName = model.LastName;
            this.BirthDate = model.BirthDate;
            this.Gender = model.Gender;
        }
        return Person;
    })(Application.Content);
    Application.Person = Person;
})(Application || (Application = {}));
