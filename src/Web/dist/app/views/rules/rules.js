'use strict';

app.controller('RulesListCtrl', function ($scope, $state, $uibModal , RulesService) {

    $scope.pageTitle = 'Rules';
    $scope.ruleList = [];

    $scope.editRule = editRule;
    $scope.newRule = newRule;

    init();

    function init() {
        loadRules();
    };

    function loadRules() {

        RulesService.getAll()
            .success(function (response) {
                $scope.ruleList = response;
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });

    };

    function newRule() {

        var modalInstance = $uibModal .open({
            templateUrl: 'EditRuleModal.html',
            controller: 'RulesModalCtrl',
            backdrop: false,
            resolve: {
                accountID: function () {
                    return 0;
                }
            }
        })

        modalInstance.result.then(function () {
            loadRules();
        }, function () {
            // $log.info('Modal dismissed at: ' + new Date());
        });
    }

    function editRule(id) {

        var modalInstance = $uibModal .open({
            templateUrl: 'EditRuleModal.html',
            controller: 'RulesModalCtrl',
            backdrop: false,
            resolve: {
                accountID: function () {
                    return id;
                }
            }
        })

        modalInstance.result.then(function () {
            loadRules();
        }, function () {
            // $log.info('Modal dismissed at: ' + new Date());
        });
    }
})

// Please note that $uibModal Instance represents a modal window (instance) dependency.
// It is not the same as the $uibModal  service used above.
app.controller('RulesModalCtrl', function ($scope, $uibModalInstance, RulesService, accountID, toastr) {

    $scope.saveRule = saveRule;
    $scope.deleteRule = deleteRule;
    $scope.cancel = cancel;

    getBanksLookUp();
    getRule();

    function getRule() {

        if (accountID == 0) {
            $scope.account = { RuleID: 0, Description: '', BankName: '' };
        }
        else {
            RulesService.get(accountID)
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
        RulesService.getBanks()
                .success(function (response) {
                    $scope.Banks = response;
                })
                .error(function (error) {
                    $scope.errorMsg = error.Message;
                })
                .finally(function () {

                });
    }

    function saveRule() {
        if ($scope.account.RuleID == 0)
            RulesService.add($scope.account)
                .success(function (response) {
                    $uibModalInstance.close();
                    toastr.success('success!');
                })
                .error(function (error) {
                    $scope.errorMsg = error.Message;
                })
                .finally(function () {

                });
        else
            RulesService.update($scope.account)
                .success(function (response) {
                    $uibModalInstance.close();
                    toastr.success('success!');
                })
                .error(function (error) {
                    $scope.errorMsg = error.Message;
                })
                .finally(function () {

                });



    };

    function deleteRule() {
        RulesService.delete($scope.account.RuleID)
            .success(function (response) {
                $uibModalInstance.close();
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });

    };

    function cancel() {
        $uibModalInstance.dismiss('cancel');
    };
});



