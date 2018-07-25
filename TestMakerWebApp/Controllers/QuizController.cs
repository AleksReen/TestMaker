using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMaker.Models.ViewModels;
using TestMakerWebApp.Helpers.DataHelper;

namespace TestMakerWebApp.Controllers
{
    [Route("api/[controller]")]
    public class QuizController : Controller
    {
        [HttpGet("Latest/{num}")]
        public IActionResult Latest(int num = 10) => new JsonResult(QuizDataHelper.GetQuizViewModelsList(num), QuizDataHelper.JsonSettings);

        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10) => new JsonResult(QuizDataHelper.GetQuizViewModelsList(num).OrderBy(x => x.Title), QuizDataHelper.JsonSettings);

        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10) => new JsonResult(QuizDataHelper.GetQuizViewModelsList(num).OrderBy(x => Guid.NewGuid()), QuizDataHelper.JsonSettings);
    }
}
