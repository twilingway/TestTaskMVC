using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using TestTaskMVC.Models;

namespace TestTaskMVC.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Admin admin)
        {   
            if (admin.Email == "test@test.com" && admin.Password == "test")
            {
                return View("Index", User);
            }
            else
            {
                return View("Index");
            }
        }
    }
}