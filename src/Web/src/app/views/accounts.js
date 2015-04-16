'use strict';

app.controller('AccountsListCtrl', function ($scope, $state, $modal, AccountService) {

    $scope.pageTitle = 'Accounts';
    $scope.accountList = [];

    $scope.editAccount = editAccount;
    $scope.newAccount = newAccount;

    init();

    function init() {
        loadAccounts();
    };

    function loadAccounts() {

        AccountService.getAll()
            .success(function (response) {
                $scope.accountList = response;
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
            controller: 'AccountsModalCtrl',
            backdrop: false,
            resolve: {
                accountID: function () {
                    return 0;
                }
            }
        })

        modalInstance.result.then(function () {
            loadAccounts();
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
            loadAccounts();
        }, function () {
            // $log.info('Modal dismissed at: ' + new Date());
        });
    }
})

// Please note that $modalInstance represents a modal window (instance) dependency.
// It is not the same as the $modal service used above.
app.controller('AccountsModalCtrl', function ($scope, $modalInstance, AccountService, accountID, toastr) {

    $scope.saveAccount = saveAccount;
    $scope.deleteAccount = deleteAccount;
    $scope.cancel = cancel;

    getBanksLookUp();
    getAccount();

    function getAccount() {

        if (accountID == 0) {
            $scope.account = { AccountID: 0, Description: '', BankName: '' };
        }
        else {
            AccountService.get(accountID)
                    .success(function (response) {
                        $scope.account = response;
                    })
                    .error(function (error) {
                        $scope.errorMsg = error.Message;
                    })
                    .finally(function () {

                    });
        }
    }

    function getBanksLookUp() {
        AccountService.getBanks()
                .success(function (response) {
                    $scope.Banks = response;
                })
                .error(function (error) {
                    $scope.errorMsg = error.Message;
                })
                .finally(function () {

                });
    }

    function saveAccount() {
        if ($scope.account.AccountID == 0)
            AccountService.add($scope.account)
                .success(function (response) {
                    $modalInstance.close();
                    toastr.success('success!');
                })
                .error(function (error) {
                    $scope.errorMsg = error.Message;
                })
                .finally(function () {

                });
        else
            AccountService.update($scope.account)
                .success(function (response) {
                    $modalInstance.close();
                    toastr.success('success!');
                })
                .error(function (error) {
                    $scope.errorMsg = error.Message;
                })
                .finally(function () {

                });



    };

    function deleteAccount() {
        AccountService.delete($scope.account.AccountID)
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



