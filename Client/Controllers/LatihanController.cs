using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    public class LatihanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LatihanArray()
        {
            return View();
        }

        public IActionResult LatihanAjax()
        {
            return View();
        }
    }
}