using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using vikaro_angular.Models;

namespace vikaro_angular.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var datamodel = new BootStrapperDataModel
            {
                Authenticated = Request.IsAuthenticated ? "true" : "false"
            };
            if (Request.IsAuthenticated)
            {
                datamodel.UserName = Thread.CurrentPrincipal.Identity.Name;
            }

            return View(datamodel);
        }

        public ActionResult CustomerType()
        {
            return View();
        }

        public ActionResult Suppliers()
        {
            return View();
        }

        public ActionResult Customers()
        {
            return View();
        }

    }
}
