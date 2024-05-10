using Microsoft.AspNetCore.Mvc;

namespace ApiApp.Controllers
{
    [Route("Mvc")]
    public class MVCController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            Task.Delay(2500).Wait();
            return View();
        }
    }
}
