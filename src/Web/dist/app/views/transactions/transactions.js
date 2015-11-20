'use strict';

app.controller('TransactionsListCtrl', function ($scope, $stateParams, $location, $modal, TransactionsService, AccountsService, RulesService) {

    $scope.transactionList = [];
    $scope.transaction = null;
    $scope.pagination = { Page: 1, ItemsPerPage: 50, TotalItems: 0 };

    $scope.refresh = loadTransactions;
    $scope.uploadFile = uploadFile;
    $scope.addTransaction = addTransaction;
    $scope.editTransaction = editTransaction;
    $scope.createRule = createRule;

    $scope.updateTransaction = updateTransaction;
    $scope.updateTag = updateTag;
    $scope.originalTag = originalTag;
    $scope.originalTagGroup = originalTagGroup;

    var accountID = $stateParams.accountID

    init();

    function init() {
        loadTransactions();
        loadLookups();
    };

    function loadTransactions() {

        var search = {
            Filter: {
                AccountID: accountID,
                Description: $scope.Description,
                ShowAll: $scope.ShowAll
            },
            Pagination: $scope.pagination
        }

        TransactionsService.getAll(search)
            .success(function (response) {
            $scope.transactionList = response.Transactions;
            $scope.pagination = response.Pagination;
        })
            .error(function (error) {
            $scope.errorMsg = error.Message;
        })
            .finally(function () {

        });

    };

    function loadLookups() {
        RulesService.getTags()
            .success(function (response) {
            $scope.tags = response;
        })
            .error(function (error) {
            $scope.errorMsg = error.Message;
        })
            .finally(function () {

        });
        RulesService.getGroups()
            .success(function (response) {
            $scope.groups = response;
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

    function uploadFile() {
        var modalInstance = $modal.open({
            templateUrl: 'UploadFileModal.html',
            controller: 'UploadFileModalCtrl',
            backdrop: false,
            resolve: {
                accountID: function () { return accountID }
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
                accountID: function () { return accountID }
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

            loadTransactions();
        }, function () {
            // $log.info('Modal dismissed at: ' + new Date());
        });
    }

    function loadAccounts(){
        $scope.$emit('refreshAccounts', null);
    }
})

// Please note that $modalInstance represents a modal window (instance) dependency.
// It is not the same as the $modal service used above.
app.controller('UploadFileModalCtrl', function ($scope, $modalInstance, $timeout, $upload, toastr, accountID, AccountsService, TransactionsService) {

    $scope.submitFile = submitFile;
    $scope.cancel = cancel;

    getAccountsLookUp();

    function getAccountsLookUp() {
        AccountsService.getAll()
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

app.controller('TransactionsModalCtrl', function ($scope, $modalInstance, TransactionsService, AccountsService, accountID, transactionID) {

    $scope.saveTransaction = saveTransaction;
    $scope.deleteTransaction = deleteTransaction;
    $scope.cancel = cancel;

    getAccountsLookUp();
    getLookUps();
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

    function getLookUps() {
        AccountsService.getAll()
            .success(function (response) {
            $scope.Accounts = response;
        })
            .error(function (error) {
            $scope.errorMsg = error.Message;
        })
            .finally(function () {

        });


        RulesService.getTags()
            .success(function (response) {
            $scope.Tags = response;
        })
            .error(function (error) {
            $scope.errorMsg = error.Message;
        })
            .finally(function () {

        });

        RulesService.getGroups()
            .success(function (response) {
            $scope.Groups = response;
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

app.controller('CreateRuleModalCtrl', function ($scope, $modalInstance, RulesService, transaction) {

    $scope.saveRule = saveRule;
    $scope.cancel = cancel;

    getLookUps();
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


    function getLookUps() {
        RulesService.getTags()
            .success(function (response) {
            $scope.Tags = response;
        })
            .error(function (error) {
            $scope.errorMsg = error.Message;
        })
            .finally(function () {

        });

        RulesService.getGroups()
            .success(function (response) {
            $scope.Groups = response;
        })
            .error(function (error) {
            $scope.errorMsg = error.Message;
        })
            .finally(function () {

        });
    }

    function saveRule() {
        RulesService.add($scope.rule)
            .success(function (response) {

            transaction.Tag= $scope.rule.Tag;
            transaction.TagGroup= $scope.rule.TagGroup; 
            transaction.IsTransfer= $scope.rule.IsTransfer; 

            $modalInstance.close();
        })
            .error(function (error) {
            $scope.errorMsg = error.ExceptionMessage;
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
