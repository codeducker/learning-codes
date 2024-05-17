using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcWebApplication.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // GET: /HelloWorld/Welcome/ 
        // Requires using System.Text.Encodings.Web;
        public string Welcome(string name, int numTimes = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }

        public IActionResult Say(string name, int numTimes = 1)
        {
            ViewData["name"]=name;
            ViewData["numTimes"] = numTimes;
            return View();
        }
    }
}
