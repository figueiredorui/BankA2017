'use strict';

app.controller('TransactionsListCtrl', function ($scope, $rootScope, $location, $stateParams, $modal, TransactionsService, AccountService) {

    $scope.tFilter = {};

    $scope.transactionList = [];
    $scope.transaction = null;

    $scope.selectedAccountID = 0;
    if ($rootScope.accountID != "")
        $scope.selectedAccountID = $rootScope.accountID;


    $scope.Search = Search;
    $scope.refresh = refresh;
    $scope.uploadFile = uploadFile;
    $scope.addTransaction = addTransaction;
    $scope.editTransaction = editTransaction;
    $scope.createRule = createRule;

    $scope.newAccount = newAccount;
    $scope.editAccount = editAccount;
    $scope.selectAccount = selectAccount;

    $scope.updateTag = updateTag;
    $scope.originalTag = originalTag;
    $scope.originalTagGroup = originalTagGroup;


    init();

    function init() {
        loadAccounts();
        loadFilters();
        loadTransactions();
    };

    function refresh() {
        loadAccounts();
        loadTransactions();
    };

    function Search() {
        loadTransactions();
    };

    function loadFilters() {
        $scope.tFilter.DateRange = {};
        $scope.tFilter.DateRange.startDate = moment().subtract(moment().dayOfYear() - 1, 'days');
        $scope.tFilter.DateRange.endDate = moment().toDate();

        $scope.tFilter.dateRangeOpts = {
            ranges: {
                'Last Month': [moment().subtract(1, 'months'), moment()],
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
        AccountService.getSummary()
            .success(function (response) {
            $scope.tFilter.Accounts = response;
        })
            .error(function (error) {
            $scope.errorMsg = error.Message;
        })
            .finally(function () {

        });
    }

    function loadTransactions() {

        var accountID = $scope.selectedAccountID

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

    var oldTag= '';
    var oldTagGroup= '';
    function originalTag(tag) {
        oldTag = tag;
    }

    function originalTagGroup(tagGroup) {
        oldTagGroup = tagGroup;
    }

    function updateTag(transaction) {

        if (oldTag != transaction.Tag || oldTagGroup != transaction.TagGroup)
        {
            updateTransaction(transaction)
        }

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

        $scope.selectedAccountID = accountID;

        loadTransactions();

    };

    function uploadFile() {
        var modalInstance = $modal.open({
            templateUrl: 'UploadFileModal.html',
            controller: 'UploadFileModalCtrl',
            backdrop: false,
            resolve: {
                accountID: function () { return $scope.selectedAccountID }
            }
        })

        modalInstance.result.then(function () {
            loadAccounts();
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
                transactionID: function () { return 0; },
                accountID: function () { return $scope.selectedAccountID }
            }
        })

        modalInstance.result.then(function () {
            loadTransactions();
        }, function () {
            // $log.info('Modal dismissed at: ' + new Date());
        });
    };

    function editTransaction(id) {

        var modalInstance = $modal.open({
            templateUrl: 'EditTransactionModal.html',
            controller: 'TransactionsModalCtrl',
            backdrop: false,
            resolve: {
                transactionID: function () { return id; },
                accountID: function () { return 0; }
            }
        })

        modalInstance.result.then(function () {
            loadAccounts();
        }, function () {
            // $log.info('Modal dismissed at: ' + new Date());
        });
    }

    function createRule(transaction) {

        var modalInstance = $modal.open({
            templateUrl: 'CreateRuleModal.html',
            controller: 'CreateRuleModalCtrl',
            backdrop: false,
            resolve: {
                transaction: function () { return transaction; },
            }
        })

        modalInstance.result.then(function () {

            updateTag(transaction);


        }, function () {
            // $log.info('Modal dismissed at: ' + new Date());
        });
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
app.controller('UploadFileModalCtrl', function ($scope, $modalInstance, $timeout, $upload, toastr, accountID, AccountService, TransactionsService) {

    $scope.submitFile = submitFile;
    $scope.cancel = cancel;

    getAccountsLookUp();

    function getAccountsLookUp() {
        AccountService.getAll()
            .success(function (response) {
            $scope.Accounts = response;
            $scope.AccountID = accountID;
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
                    toastr.success('success!');
                });
            }, function (response) {
                if (response.status > 0)
                    $scope.errorMsg = response.status + ': ' + response.data.ExceptionMessage;
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

app.controller('TransactionsModalCtrl', function ($scope, $modalInstance, TransactionsService, AccountService, accountID, transactionID) {

    $scope.saveTransaction = saveTransaction;
    $scope.deleteTransaction = deleteTransaction;
    $scope.cancel = cancel;

    getAccountsLookUp();
    getTagsLookUp();
    getTransaction();

    function getTransaction() {

        if (transactionID == 0) {
            $scope.transaction = { ID: 0, AccountID: accountID, TransactionDate: null, Description: '', Amount: '' };
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
        AccountService.getAll()
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

app.controller('CreateRuleModalCtrl', function ($scope, $modalInstance, TransactionsService, RuleService, transaction) {

    $scope.saveRule = saveRule;
    $scope.cancel = cancel;

    getTagsLookUp();
    createRule();

    function createRule() {

        $scope.rule = { 
            RuleID: 0, 
            Description: transaction.Description, 
            Tag: transaction.Tag, 
            TagGroup: transaction.TagGroup, 
            IsTransfer: transaction.IsTransfer 
        };
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

    function saveRule() {
        RuleService.add($scope.rule)
            .success(function (response) {

            transaction.Tag= $scope.rule.Tag;
            transaction.TagGroup= $scope.rule.TagGroup; 
            transaction.IsTransfer= $scope.rule.IsTransfer; 

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
