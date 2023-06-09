﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShareReview.Contracts.Users;
using ShareReview.Models.Users;
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

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Users()
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

        [HttpGet]
        public IActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(UserRegisterViewModel userVeiwModel)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.RegisterAdminAsync(userVeiwModel.GetUserDTO());
                TempData["message"] = result.Message;
                return RedirectToAction(nameof(Index));
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


        public IActionResult LogInGoogle()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("SignGoogle") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("~/User/signin-google")]
        public  async Task<IActionResult> SignGoogle()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            return Json(claims);        
        }

        [Authorize]
        public async Task<IActionResult> Logout() 
        { 
            await userService.LogoutAsync();
            return RedirectToAction(nameof(Login)); 
        }

        [Route("~/Account/AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
