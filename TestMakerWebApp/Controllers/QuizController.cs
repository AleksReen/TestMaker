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
    public class QuizController : Controller
    {
        private IQuizProvider dataProcessor;
        private ApplicationDbContext context;
        private JsonSerializerSettings JsonSettings { get; set; } = new JsonSerializerSettings { Formatting = Formatting.Indented };

        public QuizController(ApplicationDbContext dbContext, IDataProcessor DataProcessor)
        {
            dataProcessor = DataProcessor;
            context = dbContext;
        }

        #region Restful convention methods
        [HttpGet("{id}")]
        public IActionResult Get(int id) => new JsonResult(dataProcessor.GetQuiz(context, id), JsonSettings);

        [HttpPut]
        public IActionResult Put(QuizViewModel m) => throw new NotImplementedException();

        [HttpPost]
        public IActionResult Post(QuizViewModel m) => throw new NotImplementedException();

        [HttpDelete]
        public IActionResult Delete(int id) => throw new NotImplementedException();
        #endregion

        [HttpGet("Latest/{num:int?}")]
        public IActionResult Latest(int num = 10) => new JsonResult(dataProcessor.GetQuizViewModelsList(context, num), JsonSettings);

        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10) => new JsonResult(dataProcessor.GetQuizByTitle(context, num), JsonSettings);

        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10) => new JsonResult(dataProcessor.GetQuizRandom(context, num), JsonSettings);
    }
}
