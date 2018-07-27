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
    public class ResultController : Controller
    {
        ITestDataProcessor dataProcessor;

        public ResultController(ITestDataProcessor testDataProcessor)
        {
            dataProcessor = testDataProcessor;
        }

        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId) => new JsonResult(dataProcessor.GetResultViewModelsList(quizId), dataProcessor.JsonSettings);

        #region RESTfull convention methods

        [HttpGet("{id}")]
        public IActionResult Get(int id) => Content("Not implemented (yet)!");

        [HttpPut]
        public IActionResult Put(ResultViewModel m) => throw new NotImplementedException();

        [HttpPost]
        public IActionResult Post(ResultViewModel m) => throw new NotImplementedException();

        [HttpDelete]
        public IActionResult Delete(int id) => throw new NotImplementedException();

        #endregion
    }
}
