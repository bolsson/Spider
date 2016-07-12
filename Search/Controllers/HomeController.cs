using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spider;

namespace Search.Controllers
{
    public class HomeController : Controller
    {
        Tokens tokens = new Tokens();
        InvertedIndex index;

        public HomeController()
        {
            Parser parse = new Parser(tokens);
            Node root = parse.BinaryTree.root;

            Evaluater eval = new Evaluater(root, index);
            List<string> res = eval.result.ToList<string>();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}