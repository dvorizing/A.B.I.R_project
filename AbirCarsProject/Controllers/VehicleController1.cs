using Microsoft.AspNetCore.Mvc;

namespace AbirCarsProject.Controllers
{
    public class VehicleController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
