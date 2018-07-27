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

        #region RESTfull convention methods

        [HttpGet("{id}")]
        public IActionResult Get(int id) => Content("Not implemented (yet)!");

        [HttpPut]
        public IActionResult Put (AnswerViewModel m) => throw new NotImplementedException();

        [HttpPost]
        public IActionResult Post(AnswerViewModel m) => throw new NotImplementedException();

        [HttpDelete]
        public IActionResult Delete(int id) => throw new NotImplementedException();

        #endregion
    }
}
