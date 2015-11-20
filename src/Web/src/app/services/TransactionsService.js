'use strict';

app.service("TransactionsService", function ($http, AppSettings) {

    var urlBase = AppSettings.ApiUrl + 'Transactions';

    this.uploadUrl = function () {
        return urlBase + '/upload';
    }

     this.getAll = function (search) {
        return $http.post(urlBase + '/Search', search);
    }

    this.get = function (id) {
        return $http.get(urlBase + '/' + id);
    }

    this.add = function (transaction) {
        return $http.post(urlBase, transaction);
    }

    this.update = function (transaction) {
        return $http.put(urlBase + '/' + transaction.ID, transaction);
    }

    this.delete = function (id) {
        return $http.delete(urlBase + '/' + id);
    }

    

    this.getChart = function () {
        return $http.get(urlBase + '/Chart');
    }
    
    this.scrapper = function () {
        //return $http.get('https://www.saas.hsbc.co.uk/1/3/personal/online-banking/recent-transaction')
        //return $http.post('', '');
        
        var request = {
                    method: 'POST',
                    url: 'https://www.saas.hsbc.co.uk/1/3/personal/online-banking/recent-transaction',
                    headers: {
                        'Content-Type': 'application/x-www-form-urlencoded' 
                    },
                    data: 'ActiveAccountKey=40253371696599&BlitzToken=blitz'
                };
        
        
        
         return $http(request);
        
    }

});
