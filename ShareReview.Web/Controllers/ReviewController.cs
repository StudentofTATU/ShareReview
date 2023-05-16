using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareReview.Contracts.Users;
using ShareReview.Data.Interfaces;
using ShareReview.Services;
using ShareReview.Services.Interfaces;
using ShareReview.Web.ViewModels.Reviews;


namespace ShareReview.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly IUserService userService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ReviewController(IReviewService reviewService, IWebHostEnvironment webHostEnvironment, IUserService userService)
        {
            this.reviewService = reviewService;
            this.webHostEnvironment = webHostEnvironment;
            this.userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            UserDTO userDTO = await userService.GetCurrentUserAsync(HttpContext);
           
            ViewData["id"] = userDTO.Id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReviewViewModel reviewVM)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";
                if (reviewVM.Image != null)
                {
                    fileName= Guid.NewGuid().ToString()+reviewVM.Image.FileName;
                    string path = "images/" + fileName;
                    string file = Path.Combine(webHostEnvironment.WebRootPath,path);

                    await reviewVM.Image.CopyToAsync(new FileStream(file,FileMode.Create));
                }

                reviewService.CreateReview(reviewVM.GetReviewDTO(fileName));
                return RedirectToAction("Index", "Home");
            }
            return View(reviewVM);
        }
    }
}
