'use strict';

var app = angular.module('BankA', ['ui.router', 'ngCookies', 'ui.bootstrap', 'angular-loading-bar']);



app.run(function ($rootScope, $state) {
        $rootScope.$state = $state;
    });