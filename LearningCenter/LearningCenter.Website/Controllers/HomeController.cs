using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearningCenter.Website.Models;

namespace LearningCenter.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClassManager classManager;
        private readonly IUserManager  userManager;

        public HomeController(IClassManager classManager)
        {
            this.classManager = classManager;
            this.userManager  = userManager;
        }

       public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            // Throw an intentional exception to demonstrate generic exception handling
            int x = 1;
            x = x / (x - 1);
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ClassList()
        {
            var classes = classManager.Classes
                .Select(t => new LearningCenter.Website.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                .ToArray();
            var model = new ClassListModel { Classes = classes };
            return View(model);
        }

        public ActionResult LogIn()
        {
            return View();
        }
    }
}
