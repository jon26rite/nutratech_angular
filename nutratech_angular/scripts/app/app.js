(function () {

    var app = angular.module("myapp", ["ngAnimate", "ngRoute", "ngResource"]);
    //service
    app.factory('DataPost', function ($http, $q) {
        return {
            get: function (url, json) {
                var mydeffered = $q.defer();
                $http.post(url, json)
                .success(mydeffered.resolve).error(mydeffered.reject);
                return mydeffered.promise;
            }
        }
    });

    app.directive('datepicker', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function (scope, element, attrs, ngModelCtrl) {
                $(function () {
                    element.datepicker({
                        dateFormat: 'mm/dd/yy',
                        onSelect: function (date) {
                            ngModelCtrl.$setViewValue(date);
                            scope.$apply();
                        }
                    });
                });
            }
        }
    });

    // controller
    app.controller("SignInCtrl", function ($scope, DataPost, $rootScope) {


        /// Privates
        var _signIn = function (user) {
            DataPost.get("/vm/SignIn", user).then(function (data) {
                console.log(JSON.stringify(data));
                user.authenticated = true;
                $rootScope.user = user;
                $scope.CurrentUser = user.UserName;

            }, function (e) {
                console.log("error " + e.error + "");
                alert('Invalid Credentials!');
                user.authenticated = false;
                $rootScope.user = {};
            });
        }
        var _signOut = function (user) {
            DataPost.get("/vm/SignOut", user).then(function (data) {
                $rootScope.user = {};
                user.authenticated = false;

            });
        }
        var _doSomething = function (user) {
            DataPost.get("/vm/PerformAction", user).then(function (data) {
                console.log(data);
                $scope.responseError = false;
                $scope.response = data;
            }, function (e) {
                console.log("Unauthorized access");
                $scope.responseError = true;
            });
 
        }

        // $scope
        $scope.signIn = function () {
            console.log($scope.user);
            _signIn($scope.user);
        }
        $scope.signOut = function () {
            _signOut($scope.user);
            $scope.performclicked = false;
        }
        $scope.Perform = function () {
            _doSomething($scope.user);
            $scope.performclicked = true;
        }
        $scope.init = function () {
            // for display only
            $scope.performclicked = false;
            $scope.response = "";
            $scope.responseError = false;
        }
        $scope.init();
    });
    app.controller('LandingPageController', LandingPageController);
    //Customer Type Settings
    app.controller('customertypeController', customertypeController);
    app.controller('SupplierController', SupplierController);
    app.controller('JournalController', JournalController);
    app.controller('AccountsController', AccountController);

    app.config(['$routeProvider',
     function ($routeProvider) {
         $routeProvider
             .when('/CustomerType', {
                 templateUrl: 'Home/CustomerType',
                 controller: 'customertypeController'
             })
             .when('/Suppliers', {
                 templateUrl: 'Home/Suppliers',
                 controller: 'supplierController'
             }).
             when('/Customers', {
                 templateUrl: 'Home/Customers',
                 controller: 'customerController'
             }).
             when('/Journals', {
                 templateUrl: 'Home/Journals',
                 controller: 'JournalController'
             }).
             when('/Accounts', {
                 templateUrl: 'Home/Accounts',
                 controller: 'AccountsController'
             })
            .otherwise({ redirectTo: '/' });
     }
    ]);

   // app.config(configFunction);

}).call(this);