'use strict';

var app = angular.module('app', ['ui.router',
                                 'ngCookies',
                                 'ui.bootstrap',
                                 'angular-loading-bar',
                                 'daterangepicker',
                                 'angularFileUpload',
                                 'chart.js',
                                 'toastr']);



app.run(function ($rootScope, $state, AppSettings) {
    $rootScope.$state = $state;
    $rootScope.AppName = AppSettings.AppName;
    $rootScope.AppVersion = AppSettings.AppVersion;
    
});
