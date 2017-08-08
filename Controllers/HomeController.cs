using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;


namespace QuotingDojo.Controllers
{
    public class HomeController : Controller
    {
        private DbConnector cnx;

        public HomeController(){
            cnx = new DbConnector();
        }
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            string query = "SELECT * FROM quotes";
            var allQuotes = cnx.Query(query);
            ViewBag.allQuotes = allQuotes;
            return View();
        }
        [HttpPost]
        [Route("/addQuote")]
        public IActionResult addQuote(string name, string quote){
            System.Console.WriteLine(name);
            System.Console.WriteLine(quote);
            string query =  $"INSERT INTO quotes (name, quote, CreatedAt, UpdatedAt) VALUES ('{name}','{quote}', NOW(), NOW())";
            DbConnector.Execute(query);
            return RedirectToAction("addQuote");
        }
        [HttpGet]
        [Route("/addQuote")]
        public IActionResult addQuote(){
            string query = "SELECT * FROM quotes ORDER BY CreatedAt DESC";
            var allQuotes = cnx.Query(query);
            ViewBag.allQuotes= allQuotes;
            return View("addQuote");
        }
    }
}
