using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using vikaro_angular.Models;

namespace vikaro_angular.Controllers
{
    public class vmController : Controller
    {
       
        //
        // GET: /vm/

        //public ActionResult Index()
        //{
        //    //return View();
        //}
        [HttpPost]
        public ActionResult SignIn(UserDataModel user)
        {
            using (VikaroContext dc = new VikaroContext())
            {
                var checkdb = dc.Users.Where(a => a.UserName.Equals(user.UserName) && a.Password.Equals(user.Password)).FirstOrDefault();
                try { 
                    if (checkdb.UserName != null) { 
                        HttpResponseMessage msg = new HttpResponseMessage();
                        if (this.ModelState.IsValid)
                        {
                            var authenticated = true;
                            // the user authenticated in the above method
                            if (authenticated)
                            {
                                var response = new HttpStatusCodeResult(HttpStatusCode.Created);
                                FormsAuthentication.SetAuthCookie(user.UserName, true);
                                return response;
                            }
                            else
                            {
                                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                            }
                        }
                    }
                }
                catch { return new HttpStatusCodeResult(HttpStatusCode.Forbidden); }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [HttpPost]
        public ActionResult SignOut(UserDataModel user)
        {
            if (this.ModelState.IsValid)
            {
                var response = new HttpStatusCodeResult(HttpStatusCode.Created);
                FormsAuthentication.SignOut();
                return response;
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }


        [HttpPost]
        [Authorize]
        public ActionResult PerformAction(UserDataModel user)
        {
            var response = "action performed for " + user.UserName;
            /// or no need to pass the user
            var username = Thread.CurrentPrincipal.Identity.Name;
            /// perfrom acction with Identity of the user from Thread..

            return Json(response, JsonRequestBehavior.AllowGet); ;
            
        }
    }
}
