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
    public class QuizController : Controller
    {
        private ITestDataProcessor dataProcessor;

        public QuizController(ITestDataProcessor testDataProcessor)
        {
            dataProcessor = testDataProcessor;
        }

        #region Restful convention methods
        //Retrives the Quiz with the given {id}
        [HttpGet("{id}")]
        public IActionResult Get(int id) => new JsonResult(dataProcessor.GetQuizById(id), dataProcessor.JsonSettings);

        [HttpPut]
        public IActionResult Put(QuestionViewModel m) => throw new NotImplementedException();

        [HttpPost]
        public IActionResult Post(QuestionViewModel m) => throw new NotImplementedException();

        [HttpDelete]
        public IActionResult Delete(int id) => throw new NotImplementedException();
        #endregion

        [HttpGet("Latest/{num:int?}")]
        public IActionResult Latest(int num = 10) => new JsonResult(dataProcessor.GetQuizViewModelsList(num), dataProcessor.JsonSettings);

        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10) => new JsonResult(dataProcessor.GetQuizViewModelsList(num).OrderBy(x => x.Title), dataProcessor.JsonSettings);

        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10) => new JsonResult(dataProcessor.GetQuizViewModelsList(num).OrderBy(x => Guid.NewGuid()), dataProcessor.JsonSettings);
    }
}
