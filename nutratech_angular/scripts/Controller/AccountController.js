//angularjs controller method
var AccountController = function ($scope, $http) {

    //declare variable for mainain ajax load and entry or edit mode
    $scope.loading = true;
    $scope.addMode = false;

    $scope.TextSearch;
     
    $scope.myFunct = function (keyEvent, TextSearch) {
        var text = " ";
        if (TextSearch) {

            console.log("the value of search bar is : " + TextSearch);
            $http.get('/api/CustomerTypes/SearchKey/' + TextSearch).success(function (data) {
                console.log(" the data is : " + JSON.stringify(data));
                $scope.customertypes = data;
                $scope.loading = false;
            }).error(function (error, status) {
                console.log("ang error : " + JSON.stringify(error) + " : " + status);
                $scope.error = "An Error has occured while loading posts!";
                $scope.loading = false;
            });
        }
        else {
            console.log("walang laman ang SearchField.");
            $http.get('/api/CustomerTypes/').success(function (data) {
                $scope.customertypes = data;
                $scope.loading = false;
            })
   .error(function () {
       $scope.error = "An Error has occured while loading posts!";
       $scope.loading = false;
   });
        }
    }
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
        this.account.editMode = !this.account.editMode;
    };

    //by pressing toggleAdd button ng-click in html, this method will be hit
    $scope.toggleAdd = function () {
        $scope.addMode = !$scope.addMode;
    };

    //Insert Accounts
    $scope.add = function () {
        $scope.loading = true;
        
        this.newaccount.AuditUser = $scope.user.UserName;
        this.newaccount.AuditDate = new Date();
        $http.post('/api/Accounts/', this.newaccount).success(function (data) {
            alert("Added Successfully!!");
            $scope.addMode = false;
            $scope.accounts.push(data);
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
        var frien = this.account;
        //alert(frien);
        $http.put('/api/Accounts/' + frien.Id, frien).success(function (data) {
            alert("Saved Successfully!!");
            frien.editMode = false;
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving journal! " + data;
            $scope.loading = false;
        });
    };

    //Delete Journal
    $scope.deleteaccount = function () {
        $scope.loading = true;
        var Id = this.account.Id;
        $http.delete('/api/Accounts/' + Id).success(function (data) {
            alert("Deleted Successfully!!");
            $.each($scope.accounts, function (i) {
                if ($scope.accounts[i].Id === Id) {
                    $scope.accounts.splice(i, 1);
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

AccountController.$inject = ['$scope', '$http'];