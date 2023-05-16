using System.Collections;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ShareReview.Contracts.Reviews;
using ShareReview.Services.Interfaces;
using ShareReview.Web.Models;

namespace ShareReview.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IReviewService reviewService;

        public HomeController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<ReviewDTO> reviews=await reviewService.GetAllReviewsAsync();
            return View(reviews);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}