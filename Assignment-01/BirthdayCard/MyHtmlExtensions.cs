using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BirthdayCard
{
    public static class MyHtmlExtensions
    {
        public static MvcHtmlString Include(this HtmlHelper helper, string srcPath)
        {
            string path = HttpContext.Current.Server.MapPath(srcPath);
            return MvcHtmlString.Create(File.ReadAllText(path));
        }
    }
}