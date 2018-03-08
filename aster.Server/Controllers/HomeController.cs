using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aster.Server.Controllers
{
    public class HomeController : Controller
    {
        private static List<int> _scores = new List<int>();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.Scores = _scores;
            return View();
        }

        public ActionResult AddScore(int score)
        {
            _scores.Add(score);
            ViewBag.Title = "Home Page";
            ViewBag.Scores = _scores;
            return View("Index");
        }
    }
}
