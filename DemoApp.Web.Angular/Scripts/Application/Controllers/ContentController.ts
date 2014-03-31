/// <reference path="../_references.ts"/>

module Application {
    export interface IContentScope extends IScope<Content> {
    }

    export class ContentController {
        private $service: ContentService;
        constructor($scope: IContentScope, $resource: IResourceService) {
            if (this.$service == null)
                this.$service = new ContentService($resource);

            this.SetupScope($scope);

            $scope.$broadcast('updateCollection');
        }

        SetupScope($scope: IContentScope) {
            $scope.$on('updateCollection', () => {
                this.$service.GetAll({}, (data) => {
                    $scope.Collection = data;
                }, (error) => { console.log(error); });
            });
        }
    }
}
