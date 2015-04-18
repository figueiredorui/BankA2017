'use strict';

//-------------------------------------------------------------
//     CONSTANTS
//-------------------------------------------------------------
app.constant('AppSettings', {
    AppName: 'BankA',
    AppVersion: '1.0.1',
    ApiUrl: 'https://apibanka.apphb.com/api/',
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

app.config(function (toastrConfig) {
    angular.extend(toastrConfig, {
        allowHtml: false,
        closeButton: true,
        closeHtml: '<button>&times;</button>',
        containerId: 'toast-container',
        extendedTimeOut: 1000,
        iconClasses: {
            error: 'toast-error',
            info: 'toast-info',
            success: 'toast-success',
            warning: 'toast-warning'
        },
        maxOpened: 0,
        messageClass: 'toast-message',
        newestOnTop: true,
        onHidden: null,
        onShown: null,
        positionClass: 'toast-top-right',
        tapToDismiss: true,
        target: 'body',
        timeOut: 3000,
        titleClass: 'toast-title',
        toastClass: 'toast'
    });
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

//-------------------------------------------------------------
//     FILTERS
//-------------------------------------------------------------
