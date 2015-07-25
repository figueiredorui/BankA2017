'use strict';

app.controller('SettingsCtrl', function ($scope, $state, $modal, RuleService, StatementsService) {

    $scope.pageTitle = 'Rules';
    $scope.RuleList = [];
    $scope.FileList = [];

    $scope.editRule = editRule;
    $scope.newRule = newRule;
    
    $scope.deleteStatementFile = deleteStatementFile;

    init();

    function init() {
        loadRules();
        getStatementFiles();
    };

    function loadRules() {

        RuleService.getAll()
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

        var modalInstance = $modal.open({
            templateUrl: 'EditRuleModal.html',
            controller: 'RulesModalCtrl',
            backdrop: false,
            resolve: {
                RuleID: function () {
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

        var modalInstance = $modal.open({
            templateUrl: 'EditRuleModal.html',
            controller: 'RulesModalCtrl',
            backdrop: false,
            resolve: {
                RuleID: function () {
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
    
    
    //------------------------
    
   

    function getStatementFiles() {
       
        StatementsService.getAll()
            .success(function (response) {
                $scope.FileList = response;
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });
        
    }
    
    function deleteStatementFile(fileID) {
       
        StatementsService.delete(fileID)
            .success(function (response) {
                getStatementFiles();
            })
            .error(function (error) {
                $scope.errorMsg = error.Message;
            })
            .finally(function () {

            });
        
    }
    
    
})

// Please note that $modalInstance represents a modal window (instance) dependency.
// It is not the same as the $modal service used above.
app.controller('RulesModalCtrl', function ($scope, $modalInstance, RuleService, RuleID, toastr) {

    $scope.saveRule = saveRule;
    $scope.deleteRule = deleteRule;
    $scope.cancel = cancel;

    getBanksLookUp();
    getRule();

    function getRule() {

        if (RuleID == 0) {
            $scope.Rule = { RuleID: 0, Description: '', BankName: '' };
        }
        else {
            RuleService.get(RuleID)
                    .success(function (response) {
                        $scope.Rule = response;
                    })
                    .error(function (error) {
                        $scope.errorMsg = error.Message;
                    })
                    .finally(function () {

                    });
        }
    }

    function getBanksLookUp() {
        RuleService.getBanks()
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
        if ($scope.Rule.RuleID == 0)
            RuleService.add($scope.Rule)
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
            RuleService.update($scope.Rule)
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

    function deleteRule() {
        RuleService.delete($scope.Rule.RuleID)
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



