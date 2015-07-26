'use strict';

app.controller('LayoutCtrl', function ($rootScope, $scope, $modal, $http, $state, AppSettings, AccountService)
               {
    $scope.showTransactions= showTransactions;
    $scope.selectAccount= selectAccount;

    $scope.newAccount = newAccount;
    $scope.editAccount = editAccount;


    AppInfo()
    loadAccountSummary();

    function AppInfo()
    {
        $rootScope.AppName = AppSettings.AppName;

        $http.get(AppSettings.ApiUrl).success(function (response) {
            $scope.ApiVersion = response.Version;
            $scope.ApiUrl = response.GitHub;
        });
    }


    function loadAccountSummary() {
        AccountService.getSummary()
            .success(function (response) {
            $scope.Accounts = response;
        })
            .error(function (error) {
            $scope.errorMsg = error.Message;
        })
            .finally(function () {

        });
    }

    function selectAccount(accountID) {

        $rootScope.accountID = accountID;
        $scope.$broadcast('selectedAccountChanged', null);

        /* var state = $state.current.name
        if (state == 'app.dashboard')
            $scope.$broadcast('refreshDashboard', null);
        else
            $state.go('app.dashboard');*/

    }

    function showTransactions(accountID) {
        $rootScope.accountID = accountID;

        var state = $state.current.name
        if (state == 'app.transactions')
            $scope.$broadcast('refreshTransactions', null);
        else
            $state.go('app.transactions');

    }

    function newAccount() {

        var modalInstance = $modal.open({
            templateUrl: 'EditAccountModal.html',
            controller: 'AccountsModalCtrl',
            backdrop: false,
            resolve: {
                accountID: function () {
                    return 0;
                }
            }
        })

        modalInstance.result.then(function () {
            loadAccountSummary();
        }, function () {
            // $log.info('Modal dismissed at: ' + new Date());
        });
    }

    function editAccount(id) {

        var modalInstance = $modal.open({
            templateUrl: 'EditAccountModal.html',
            controller: 'AccountsModalCtrl',
            backdrop: false,
            resolve: {
                accountID: function () {
                    return id;
                }
            }
        })

        modalInstance.result.then(function () {
            loadAccountSummary();
        }, function () {
            // $log.info('Modal dismissed at: ' + new Date());
        });
    }


})
