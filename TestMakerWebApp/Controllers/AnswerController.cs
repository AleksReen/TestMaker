using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestMaker.Helpers.Helpers.DataHelper;
using TestMaker.Models.ViewModels;

namespace TestMakerWebApp.Controllers
{
    [Route("api/[controller]")]
    public class AnswerController : Controller
    {
        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId) => new JsonResult(AnswerDataHelper.GetAnswerViewModelsList(questionId), AnswerDataHelper.JsonSettings);          
    }
}
