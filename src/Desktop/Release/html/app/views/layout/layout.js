'use strict';

app.controller('LayoutCtrl', function ($rootScope, $scope, $modal, $http, $state, $filter, $stateParams, AppSettings, AccountsService)
               {
    $scope.selectedAccount= '';
    $scope.Accounts = {};
    
    $scope.selectAccount= selectAccount;

    $scope.newAccount = newAccount;
    $scope.editAccount = editAccount;


    AppInfo()
    loadAccountSummary();        
    
    $scope.$on('refreshAccounts', function(event, args) {
        loadAccountSummary();
    });

    function AppInfo()
    {
        $rootScope.AppName = AppSettings.AppName;

        $http.get(AppSettings.ApiUrl).success(function (response) {
            $scope.ApiVersion = response.Version;
            $scope.ApiUrl = response.GitHub;
        });
    }


    function loadAccountSummary() {
        AccountsService.getSummary()
            .success(function (response) {
            $scope.Accounts = response;
            
            setSelectedAccount($stateParams.accountID);
        })
            .error(function (error) {
            $scope.errorMsg = error.Message;
        })
            .finally(function () {

        });
    }

    function selectAccount(accountID) {

        setSelectedAccount(accountID);
        
        $state.go($state.current, {accountID: accountID});
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
    
    function setSelectedAccount(accountID){
        var found = $filter('filter')($scope.Accounts, {AccountID: accountID}, true);
        if (found.length)
            $scope.selectedAccount= found[0].Description;
        
        $scope.selectedAccountID = accountID;
    }


})
