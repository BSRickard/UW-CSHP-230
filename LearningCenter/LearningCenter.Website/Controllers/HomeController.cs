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

        public HomeController()
        {
            //this.classManager = new ClassManager();
        }

        public HomeController(IClassManager classManager)
        {
            this.classManager = classManager;
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

        [Authorize]
        public ActionResult ClassList()
        {
            var classes = classManager.Classes
                .Select(t => new LearningCenter.Website.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                .ToArray();
            var model = new ClassListModel { Classes = classes };
            return View(model);

            //var school = new Entities();

            //foreach (var aClass in school.Classes)
            //{
            //    Debug.WriteLine(string.Format("ClassID: {0}\nClassName: {1}\nClassDescription: {2}\nClassPrice: {3}\n",
            //        aClass.ClassId, aClass.ClassName, aClass.ClassDescription, aClass.ClassPrice));
            //}
            //Debug.WriteLine("End.");
            //return View();
        }

    }
}
