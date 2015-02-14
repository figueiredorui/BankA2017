'use strict';

//-------------------------------------------------------------
//     CONSTANTS
//-------------------------------------------------------------
app.constant('AppSettings', {
    UrlBase: 'http://localhost/banka.api/',
    //UrlBase: 'http://apibanka.apphb.com/',
});

//-------------------------------------------------------------
//     CONFIG
//-------------------------------------------------------------
app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptor');
    $httpProvider.defaults.cache = false;
});

app.config(function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeBar = false;
});

//-------------------------------------------------------------
//     RUN
//-------------------------------------------------------------
app.run(function ($rootScope, $state, authService) {

    //authService.LoadAuthData();

    //$rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
    //    if (toState.name != 'app.login' && authService.AuthData != null && authService.AuthData.isAuth == false) {
    //        event.preventDefault();
    //        $state.go('app.login');
    //    }

    //    if (toState.name == 'app.login' && authService.AuthData != null && authService.AuthData.isAuth == true) {
    //        event.preventDefault();
    //        $state.go('app.home');
    //    }
    //});

});


//-------------------------------------------------------------
//     DIRECTIVES
//-------------------------------------------------------------
app.directive('onlyDecimal', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }

            ngModelCtrl.$parsers.push(function (val) {
                var clean = val.replace(/[^0-9]+/g, '');
                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });

            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});