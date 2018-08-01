using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMaker.Data.Context;
using TestMaker.Data.Proccesor;
using TestMaker.Data.Processor.Providers;
using TestMaker.Models.ViewModels;

namespace TestMakerWebApp.Controllers
{
    [Route("api/[controller]")]
    public class ResultController : Controller
    {
        IResultProvider dataProcessor;
        private ApplicationDbContext context;
        private JsonSerializerSettings JsonSettings { get; set; } = new JsonSerializerSettings { Formatting = Formatting.Indented };

        public ResultController(ApplicationDbContext dbContext, IDataProcessor testDataProcessor)
        {
            dataProcessor = testDataProcessor;
            context = dbContext;
        }

        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId) => new JsonResult(dataProcessor.GetResultViewModelsList(context, quizId), JsonSettings);

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
