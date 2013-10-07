var Application;
(function (Application) {
    var Service = (function () {
        function Service($path, $resource, $rootScope) {
            this.$path = $path;
            this.$resource = $resource;
            this.$provider = this.$resource(Service.$url + 'Api/' + this.$path + '/:id', {
                id: '@Id'
            }, {
                update: {
                    method: 'PUT',
                    headers: { Authorization: 'Basic ' + $rootScope.username + ':' + $rootScope.password }
                },
                get: {
                    method: 'GET',
                    headers: { Authorization: 'Basic ' + $rootScope.username + ':' + $rootScope.password }
                },
                save: {
                    method: 'POST',
                    headers: { Authorization: 'Basic ' + $rootScope.username + ':' + $rootScope.password }
                }
            });
        }
        Service.$url = 'http://192.168.56.1:80/';
        Service.prototype.Get = function (id, success, error) {
            this.$provider.get({
                id: id
            }, success, error);
        };
        Service.prototype.GetAll = function (filter, success, error) {
            this.$provider.query(filter, success, error);
        };
        Service.prototype.Create = function (model, success, error) {
            this.$provider.save(model, success, error);
        };
        Service.prototype.Update = function (model, success, error) {
            var _this = this;
            if (model.PhotoURI) {
                this.Upload(model.Id, model.PhotoURI, function () {
                    delete model.PhotoURI;
                    _this.$provider.update(model, success, error);
                }, error)
            } else {
                this.$provider.update(model, success, error);
            }
        };
        Service.prototype.Delete = function (model, success, error) {
            this.$provider.delete(model, success, error);
        };
        Service.prototype.Upload = function (id, uri, success, error) {
            var options = new FileUploadOptions();
            options.fileKey = "file";
            options.fileName = uri.substr(uri.lastIndexOf('/') + 1);
            options.mimeType = "image/jpeg";

            var fileTransfer = new FileTransfer();
            fileTransfer.upload(uri, Service.$url + 'Api/Content/' + id, success, error, options);
        };
        return Service;
    })();
    Application.Service = Service;
})(Application || (Application = {}));
