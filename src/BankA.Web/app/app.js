'use strict';

var app = angular.module('BankA', ['ui.router', 'ngCookies']);



app.run(function ($rootScope, $state) {
        $rootScope.$state = $state;
    });