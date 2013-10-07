var __extends = this.__extends || function (d, b) {
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var Application;
(function (Application) {
    var Content = (function () {
        function Content(model) {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Description = model.Description;
            this.Type = model.Type;
        }
        return Content;
    })();
    Application.Content = Content;
    var ContentService = (function (_super) {
        __extends(ContentService, _super);
        function ContentService($resource) {
            _super.call(this, 'Content', $resource);
        }
        return ContentService;
    })(Application.Service);
    Application.ContentService = ContentService;
    var ContentController = (function () {
        function ContentController($scope, $rootScope, $resource) {
            if (this.$service == null) {
                this.$service = new ContentService($resource);
            }
            this.SetupScope($scope, $rootScope);
            $scope.$broadcast('updateCollection');
        }
        ContentController.prototype.SetupScope = function ($scope, $rootScope) {
            var _this = this;
            $scope.$on('updateCollection', function () {
                _this.$service.GetAll({}, function (data) {
                    $.each(data, function (index, value) {
                        value.Photo = Application.Service.$url + value.Photo;
                    });
                    $scope.Collection = data;
                }, function (error) {
                    console.log(error);
                });
            });

            $scope.Load = function (item) {
                switch (item.Type) {
                    case 'book': $rootScope.$broadcast('loadBook', item.Id); break;
                    case 'person': $rootScope.$broadcast('loadPerson', item.Id); break;
                }
            };
        };
        return ContentController;
    })();
    Application.ContentController = ContentController;
})(Application || (Application = {}));
