using Microsoft.AspNetCore.Mvc;
using ShareReview.Contracts.Users;
using ShareReview.Services.Interfaces;
using ShareReview.Web.ViewModels;

namespace ShareReview.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register() {
            return View();
        }

        [HttpPost]
        public  async Task<IActionResult> Register(UserRegisterViewModel userVeiwModel)
        {
            if (ModelState.IsValid)
            {
                var result= await userService.RegisterAsync(userVeiwModel.GetUserDTO());
                TempData["message"] = result.Message;
                return RedirectToAction(nameof(Register));
            }

            return View();  
        }
    }
}
