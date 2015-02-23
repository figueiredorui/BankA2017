﻿'use strict';

app.controller('TransactionsListCtrl', function ($scope, $location, $stateParams, $modal, TransactionsService, BankAccountService) {

    $scope.tFilter = {};

    $scope.transactionList = [];
    $scope.transaction = null;
    $scope.selectedAccount = null;


    $scope.Search = Search;
    $scope.uploadFile = uploadFile;
    $scope.addTransaction = addTransaction;

    $scope.selectAccount = selectAccount;
    $scope.updateTransaction = updateTransaction;
    

    init();

    function init() {
        loadAccounts();
        loadFilters();
        loadTransactions();
    };

    function Search() {
        loadTransactions();
    };

    function loadFilters() {
        $scope.tFilter.DateRange = {
            startDate: moment().subtract(3, "months"),
            endDate: moment(),
        };
        $scope.tFilter.dateRangeOpts = {
            ranges: {
                'Last Month': [moment().subtract(30, 'days'), moment()],
                'Current Year': [moment().subtract(moment().dayOfYear() - 1, 'days'), moment()],
                'Last Year': [moment().subtract(1, 'year'), moment()],
                'Last two Years': [moment().subtract(2, 'year'), moment()]
            }
        };

        TransactionsService.getTags()
            .success(function (response) {
                $scope.tFilter.Tags = response;
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });

    }

    function loadAccounts() {
        BankAccountService.getSummary()
            .success(function (response) {
                $scope.tFilter.Accounts = response;
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });
    }

    function loadTransactions(accountID) {

        TransactionsService.getAll(accountID, $scope.tFilter.DateRange.startDate, $scope.tFilter.DateRange.endDate, $scope.tFilter.Tag)
            .success(function (response) {
                $scope.transactionList = response;
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });

    };

    function updateTransaction(transaction) {

        TransactionsService.update(transaction)
            .success(function (response) {

            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });

    };

    function selectAccount(accountID) {

        $scope.selectedAccount = accountID;

        loadTransactions(accountID);

    };

    function uploadFile() {
        var modalInstance = $modal.open({
            templateUrl: 'UploadFileModal.html',
            controller: 'UploadFileModalCtrl',
            backdrop: false,
            resolve: {

            }
        })

        modalInstance.result.then(function () {
            loadTransactions();
        }, function () {
            // $log.info('Modal dismissed at: ' + new Date());
        });
    };

    function addTransaction() {
        var modalInstance = $modal.open({
            templateUrl: 'EditTransactionModal.html',
            controller: 'TransactionsModalCtrl',
            backdrop: false,
            resolve: {
                transactionID: function () { return 0; }
            }
        })

        modalInstance.result.then(function () {
            loadTransactions();
        }, function () {
            // $log.info('Modal dismissed at: ' + new Date());
        });
    };

    $scope.getClass = function (path) {
        if ($location.path().substr($location.path().length - path.length, $location.path().length) == path) {
            return "active"
        } else {
            return ""
        }
    }

})

// Please note that $modalInstance represents a modal window (instance) dependency.
// It is not the same as the $modal service used above.
app.controller('UploadFileModalCtrl', function ($scope, $modalInstance, $timeout, $upload, BankAccountService, TransactionsService) {

    $scope.submitFile = submitFile;
    $scope.cancel = cancel;

    getAccountsLookUp();

    function getAccountsLookUp() {
        BankAccountService.getAll()
                .success(function (response) {
                    $scope.Accounts = response;
                })
                .error(function (error) {
                    $scope.errorMsg = error.Message;
                })
                .finally(function () {

                });
    }

    function submitFile(files) {

        $scope.formUpload = true;
        if (files != null) {

            var file = files[0];

            file.upload = $upload.upload({
                url: TransactionsService.uploadUrl(),
                method: 'POST',
                headers: {
                    'my-header': 'my-header-value'
                },
                fields: { AccountID: $scope.AccountID },
                file: file,
                fileFormDataName: 'myFile',
            });

            file.upload.then(function (response) {
                $timeout(function () {
                    file.result = response.data;
                    $modalInstance.close();
                });
            }, function (response) {
                if (response.status > 0)
                    $scope.errorMsg = response.status + ': ' + response.data;
            });

            file.upload.progress(function (evt) {
                // Math.min is to fix IE which reports 200% sometimes
                file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
            });

            file.upload.xhr(function (xhr) {
                // xhr.upload.addEventListener('abort', function(){console.log('abort complete')}, false);
            });


        }

    };



    function cancel() {
        $modalInstance.dismiss('cancel');
    };
});


app.controller('TransactionsModalCtrl', function ($scope, $modalInstance, TransactionsService, BankAccountService, transactionID) {

    $scope.saveTransaction = saveTransaction;
    $scope.deleteTransaction = deleteTransaction;
    $scope.cancel = cancel;

    getAccountsLookUp();
    getTagsLookUp();
    getTransaction();

    function getTransaction() {

        if (transactionID == 0) {
            $scope.transaction = { ID: 0, AccountID: null, TransactionDate: null, Description: '', Amount: '' };
        }
        else {
            TransactionsService.get(transactionID)
                    .success(function (response) {
                        $scope.transaction = response;
                    })
                    .error(function (error) {
                        $scope.errorMsg = error.Message;
                    })
                    .finally(function () {

                    });
        }
    }

    function getAccountsLookUp() {
        BankAccountService.getAll()
                .success(function (response) {
                    $scope.Accounts = response;
                })
                .error(function (error) {
                    $scope.errorMsg = error.Message;
                })
                .finally(function () {

                });
    }

    function getTagsLookUp() {
        TransactionsService.getTags()
                .success(function (response) {
                    $scope.Tags = response;
                })
                .error(function (error) {
                    $scope.errorMsg = error.Message;
                })
                .finally(function () {

                });
    }

    function saveTransaction() {
        if ($scope.transaction.ID == 0)
            TransactionsService.add($scope.transaction)
                .success(function (response) {
                    $modalInstance.close();
                })
                .error(function (error) {
                    $scope.errorMsg = error.Message;
                })
                .finally(function () {

                });
        else
            TransactionsService.update($scope.transaction)
                .success(function (response) {
                    $modalInstance.close();
                })
                .error(function (error) {
                    $scope.errorMsg = error.Message;
                })
                .finally(function () {

                });



    };

    function deleteTransaction() {
        TransactionsService.delete($scope.transaction.ID)
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

    $scope.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();

        $scope.opened = true;
    };



});
