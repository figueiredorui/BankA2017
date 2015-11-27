'use strict';

var app = angular.module('app', ['ui.router',
                                 'ngCookies',
                                 'ui.bootstrap',
                                 'angular-loading-bar',
                                 'angularFileUpload',
                                 'chart.js',
                                 'nvd3',
                                 'toastr']);



app.run(function ($rootScope, $state) {
    $rootScope.$state = $state;
    
});

