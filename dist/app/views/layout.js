'use strict';

app.controller('LayoutCtrl', function ($scope, $cookieStore, $http, AppSettings)
{
    ApiVersion()

    function ApiVersion()
    {
        $http.get(AppSettings.ApiUrl).success(function (response) {
            $scope.ApiVersion = response.Version;
            $scope.ApiUrl = response.GitHub;
        });
    }


    /************ Sidebar Toggle & Cookie Control *****************/
    var mobileView = 1400;

    $scope.getWidth = function () {
        return window.innerWidth;
    };

    $scope.$watch($scope.getWidth, function (newValue, oldValue) {
        if (newValue >= mobileView) {
            if (angular.isDefined($cookieStore.get('toggle'))) {
                $scope.toggle = !$cookieStore.get('toggle') ? false : true;
            } else {
                $scope.toggle = true;
            }
        } else {
            $scope.toggle = false;
        }

    });

    $scope.toggleSidebar = function () {
        $scope.toggle = !$scope.toggle;
        $cookieStore.put('toggle', $scope.toggle);
    };

    window.onresize = function () {
        $scope.$apply();
    };
})
