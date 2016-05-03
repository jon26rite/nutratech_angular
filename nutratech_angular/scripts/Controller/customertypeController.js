//angularjs controller method
var customertypeController = function ($scope, $http) {

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
    //get all customer information
    $http.get('/api/CustomerTypes/').success(function (data) {
        $scope.customertypes = data;
        $scope.loading = false;
    })
    .error(function () {
        $scope.error = "An Error has occured while loading posts!";
        $scope.loading = false;
    });

   
    
    
    //by pressing toggleEdit button ng-click in html, this method will be hit
    $scope.toggleEdit = function () {
        this.customertype.editMode = !this.customertype.editMode;
    };

    //by pressing toggleAdd button ng-click in html, this method will be hit
    $scope.toggleAdd = function () {
        $scope.addMode = !$scope.addMode;
    };

    //Insert Customer Types
    $scope.add = function () {
        $scope.loading = true;
        $http.post('/api/CustomerTypes/', this.newcustomertype).success(function (data) {
            alert("Added Successfully!!");
            $scope.addMode = false;
            $scope.customertypes.push(data);
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Adding Customer Type! " + data;
            $scope.loading = false;
        });
    };

    //Edit Customer Types
    $scope.save = function () {
        //alert("Edit");
        $scope.loading = true;
        var frien = this.customertype;
        //alert(frien);
        $http.put('/api/CustomerTypes/' + frien.Id, frien).success(function (data) {
            alert("Saved Successfully!!");
            frien.editMode = false;
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving customer type! " + data;
            $scope.loading = false;
        });
    };

    //Delete Customer
    $scope.deletecustomertype = function () {
        $scope.loading = true;
        var Id = this.customertype.Id;
        $http.delete('/api/CustomerTypes/' + Id).success(function (data) {
            alert("Deleted Successfully!!");
            $.each($scope.customertypes, function (i) {
                if ($scope.customertypes[i].Id === Id) {
                    $scope.customertypes.splice(i, 1);
                    return false;
                }
            });
            $scope.loading = false;
        }).error(function (data) {
            $scope.error = "An Error has occured while Saving Customer Type! " + data;
            $scope.loading = false;
        });
    };

}

customertypeController.$inject = ['$scope', '$http'];