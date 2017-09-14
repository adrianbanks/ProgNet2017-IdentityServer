using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> CallApi()
        {
            var client = new HttpClient();

            var token = await HttpContext.Authentication.GetTokenAsync("access_token");
            client.SetBearerToken(token);

            var response = client.GetStringAsync("http://localhost:5002/api/values");
            var model = JArray.Parse(response.Result).ToString();

            return View("Contact", model);
        }
    }
}
