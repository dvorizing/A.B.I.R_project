using AbirCarsProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace AbirCarsProject.Controllers
{
    public class CustomerController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
