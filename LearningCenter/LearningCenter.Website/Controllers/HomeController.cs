﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearningCenter.Website.Business;
using LearningCenter.Website.Models;

// Student: Brian Rickard

namespace LearningCenter.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClassManager classManager;
        private readonly IUserManager  userManager;

        public HomeController(IClassManager classManager, IUserManager userManager)
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
            var classes = classManager.GetClasses
                .Select(t => new Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                .ToArray();
            var model = new ClassListModel { Classes = classes };
            return View(model);
        }

        public ActionResult EnrollinClass()
        {
            var user = (Models.UserModel)Session["User"];
            if (null == user)
            {
                return View("LogIn");
            }

            var classes = classManager.GetClasses
                .Select(t => new LearningCenter.Website.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                .ToArray();
            var model = new ClassListModel { Classes = classes };
            return View(model);
        }

        [HttpPost]
        // TODO: Need to work out how to get the selection made on Enroll.cshtml
        // Tried various types of parameters (ClassListModel, string, int) - always null (so far)
        public ActionResult EnrollinClass(FormCollection fc, string returnUrl)
        {
            var user = (Models.UserModel)Session["User"];
            if (null == user)
            {
                return View("LogIn");
            }
            //ClassModel classToAdd = ;
            int userId = user.Id;
            ViewBag.Id = fc["Id"];
            return View("Index");
        }

        public ActionResult StudentClasses()
        {
            var classes = classManager.GetClasses
                .Select(t => new LearningCenter.Website.Models.ClassModel(t.Id, t.Name, t.Description, t.Price))
                .ToArray();
            var model = new ClassListModel { Classes = classes };
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Register(registerModel.Email, registerModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name already exists.");
                }
                else
                {
                    Session["User"] = new LearningCenter.Website.Models.UserModel { Id = user.Id, Name = user.Name };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(registerModel.Email, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(registerModel);
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    Session["User"] = new LearningCenter.Website.Models.UserModel { Id = user.Id, Name = user.Name };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(loginModel.UserName, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }
    }
}
