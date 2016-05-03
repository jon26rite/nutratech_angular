//angularjs controller method
var JournalController = function ($scope, $http) {

    //declare variable for mainain ajax load and entry or edit mode
    $scope.loading = true;
    $scope.addMode = false;

    $scope.TextSearch;
    $scope.SelectedDebitCredit;
    $scope.SelectedAccount;

    $scope.DebitCreditMode = [
        { id: 'Credit', name: 'Credit' },
        { id: 'Debit', name: 'Debit' }
    ]
    $scope.myFunct = function (keyEvent, TextSearch) {
        var text = " ";
        if (TextSearch) {

            console.log("the value of search bar is : " + TextSearch);
            $http.get('/api/Journals/SearchKey/' + TextSearch).success(function (data) {
                console.log(" the data is : " + JSON.stringify(data));
                $scope.journals = data;
                $scope.loading = false;
            }).error(function (error, status) {
                console.log("ang error : " + JSON.stringify(error) + " : " + status);
                $scope.error = "An Error has occured while loading posts!";
                $scope.loading = false;
            });
        }
        else {
            console.log("walang laman ang SearchField.");
            $http.get('/api/Journals').success(function (data) {
                $scope.journals = data;
                $scope.loading = false;
            })
   .error(function () {
       $scope.error = "An Error has occured while loading posts!";
       $scope.loading = false;
   });
        }
    }
    //get all Journals
    $http.get('/api/Journals/').success(function (data) {
        console.log("the data is : " + JSON.stringify(data));
        $scope.journals = data;
        $scope.loading = false;
    })
    .error(function (data, status) {
        console.log("data : " + JSON.stringify(data) + " status : " + JSON.stringify(status));
        $scope.error = "An Error has occured while loading posts!";
        $scope.loading = false;
    });

    //get all Accounts
    $http.get('/api/Accounts/').success(function (data) {
        //console.log("the data is : " + JSON.stringify(data));
        console.log(" from rootscope : " + JSON.stringify($scope.user.UserName));
        $scope.accounts = data;
        $scope.loading = false;
    })
    .error(function (data, status) {
        console.log("data : " + JSON.stringify(data) + " status : " + JSON.stringify(status));
        $scope.error = "An Error has occured while loading posts!";
        $scope.loading = false;
    });



    //by pressing toggleEdit button ng-click in html, this method will be hit
    $scope.toggleEdit = function () {
        this.journal.editMode = !this.journal.editMode;
    };

    //by pressing toggleAdd button ng-click in html, this method will be hit
    $scope.toggleAdd = function () {
        $scope.addMode = !$scope.addMode;
    };

    //Insert Journals
    $scope.add = function () {
        $scope.loading = true;

        this.newjournal.AuditUser = $scope.user.UserName;
        this.newjournal.AuditDate = new Date();
        console.log('the newjournal before http.post is : ' + JSON.stringify(this.newjournal));
        $http.post('/api/Journals/', this.newjournal).success(function (data) {
            console.log('the newjournal is : ' + JSON.stringify(data));
            alert("Added Successfully!!");
            $scope.addMode = false;
            $scope.journals.push(data);
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Adding Journal! " + data;
            $scope.loading = false;
        });
    };

    //Edit Customer Types
    $scope.save = function () {
        //alert("Edit");
        $scope.loading = true;
        var frien = this.journal;
        //alert(frien);
        $http.put('/api/Journals/' + frien.Id, frien).success(function (data) {
            alert("Saved Successfully!!");
            frien.editMode = false;
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving journal! " + data;
            $scope.loading = false;
        });
    };

    //Delete Journal
    $scope.deletejournal = function () {
        $scope.loading = true;
        var Id = this.journal.Id;
        $http.delete('/api/Journals/' + Id).success(function (data) {
            alert("Deleted Successfully!!");
            $.each($scope.journals, function (i) {
                if ($scope.journals[i].Id === Id) {
                    $scope.journals.splice(i, 1);
                    return false;
                }
            });
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving Journal! " + data;
            $scope.loading = false;
        });
    };

}

JournalController.$inject = ['$scope', '$http'];