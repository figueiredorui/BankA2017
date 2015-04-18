'use strict';

var app = angular.module('app', ['ui.router',
                                 'ngCookies',
                                 'ui.bootstrap',
                                 'ui.select',
                                 'angular-loading-bar',
                                 'daterangepicker',
                                 'angularFileUpload',
                                 'chart.js',
                                 'toastr']);



app.run(function ($rootScope, $state) {
    $rootScope.$state = $state;
    
});

