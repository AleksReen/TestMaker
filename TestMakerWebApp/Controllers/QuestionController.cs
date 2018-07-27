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
        private ITestDataProcessor dataProcessor;

        public QuestionController(ITestDataProcessor testDataProcessor)
        {
            dataProcessor = testDataProcessor;
        }
        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId) => new JsonResult(dataProcessor.GetQuestionViewModelsList(quizId), dataProcessor.JsonSettings);

        #region RESTfull convention methods

        [HttpGet("{id}")]
        public IActionResult Get(int id) => Content("Not implemented (yet)!");

        [HttpPut]
        public IActionResult Put(QuestionViewModel m) => throw new NotImplementedException();

        [HttpPost]
        public IActionResult Post(QuestionViewModel m) => throw new NotImplementedException();

        [HttpDelete]
        public IActionResult Delete(int id) => throw new NotImplementedException();

        #endregion
    }
}
