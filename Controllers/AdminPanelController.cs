using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace TestTaskMVC.Controllers
{
    public class AdminPanelController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}