using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareReview.Contracts.Users;
using ShareReview.Services.Interfaces;
using ShareReview.Web.ViewModels.Users;

namespace ShareReview.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            UserDTO userDTO =await userService.GetCurrentUserAsync(HttpContext);
            UserViewModel userViewModel=new UserViewModel(userDTO);
            TempData["userName"] = userDTO.Name;
            return View(userViewModel);
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

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userViewModel) 
        {
            if(!ModelState.IsValid) { return View(userViewModel); }

            var result = await userService.LoginAsync(userViewModel.GetUserDTO());

            if (result.StatusCode == 1)
            {
                return RedirectToAction("Index","User");
            }
            else
            {
                TempData["message"] = result.Message;
                return RedirectToAction(nameof(Login));
            }
        }

        public IActionResult Login() { return View(); }

        [Authorize]
        public async Task<IActionResult> Logout() 
        { 
            await userService.LogoutAsync();
            return RedirectToAction(nameof(Login)); 
        }
    }
}
