using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMaker.Helpers.Helpers.DataHelper;
using TestMaker.Models.ViewModels;

namespace TestMakerWebApp.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId) => new JsonResult( QuestionDataHelper.GetQuestionViewModelsList(quizId), QuestionDataHelper.JsonSettings);
    }
}
