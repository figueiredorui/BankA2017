'use strict';

app.controller('AccountsListCtrl', function ($scope, $state, $modal, BankAccountService) {

    $scope.accountList = [];

    $scope.editAccount = editAccount;
    $scope.newAccount = newAccount;

    init();

    function init() {
        loadAccounts();
    };

    function loadAccounts() {

        BankAccountService.getAll()
            .success(function (response) {
                $scope.accountList = response;
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });

    };

    function updateAccount(account) {

        BankAccountsService.update(account)
            .success(function (response) {

            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });

    };

    function newAccount() {

        var modalInstance = $modal.open({
            templateUrl: 'EditAccountModal.html',
            controller: 'ModalInstanceCtrl',
            backdrop: false,
            resolve: {
                account: function () {
                    return { AccountID: 0, Description: 'a', BankName: 'b' };
                }
            }
        })

        modalInstance.result.then(function () {
            loadAccounts();
        }, function () {
            // $log.info('Modal dismissed at: ' + new Date());
        });
    }

    function editAccount(account) {

        var modalInstance = $modal.open({
            templateUrl: 'EditAccountModal.html',
            controller: 'ModalInstanceCtrl',
            backdrop: false,
            resolve: {
                account: function () {
                    return account;
                }
            }
        })

        modalInstance.result.then(function () {
            loadAccounts();
        }, function () {
            // $log.info('Modal dismissed at: ' + new Date());
        });
    }


})

// Please note that $modalInstance represents a modal window (instance) dependency.
// It is not the same as the $modal service used above.

app.controller('ModalInstanceCtrl', function ($scope, $modalInstance, BankAccountService, account) {

    $scope.account = account;

    $scope.saveAccount = saveAccount;
    $scope.deleteAccount = deleteAccount;
    $scope.cancel = cancel;

    function saveAccount() {
        if ($scope.account.AccountID == 0)
            BankAccountService.add($scope.account)
                .success(function (response) {
                    $modalInstance.close();
                })
                .error(function (error) {
                    $scope.errorMsg = error.Message;
                })
                .finally(function () {

                });
        else
            BankAccountService.update($scope.account)
                .success(function (response) {
                    $modalInstance.close();
                })
                .error(function (error) {
                    $scope.errorMsg = error.Message;
                })
                .finally(function () {

                });


        
    };

    function deleteAccount() {
        BankAccountService.delete($scope.account.AccountID)
            .success(function (response) {
                $modalInstance.close();
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });

    };

    function cancel() {
        $modalInstance.dismiss('cancel');
    };
});



