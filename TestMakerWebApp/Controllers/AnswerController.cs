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
        private ITestDataProcessor dataProcessor;

        public AnswerController(ITestDataProcessor testDataProcessor)
        {
            dataProcessor = testDataProcessor;
        }

        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId) => new JsonResult(dataProcessor.GetAnswerViewModelsList(questionId), dataProcessor.JsonSettings);          
    }
}
