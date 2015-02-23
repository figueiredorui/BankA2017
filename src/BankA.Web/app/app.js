'use strict';

var app = angular.module('BankA', ['ui.router', 'ngCookies', 'ui.bootstrap', 'angular-loading-bar', 'daterangepicker', 'angularFileUpload']);



app.run(function ($rootScope, $state) {
        $rootScope.$state = $state;
    });