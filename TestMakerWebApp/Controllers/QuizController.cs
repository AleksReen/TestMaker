﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMaker.Data;
using TestMaker.Data.Context;
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

        public QuizController(ApplicationDbContext dbContext, IQuizProvider DataProcessor)
        {
            dataProcessor = DataProcessor;
            context = dbContext;
        }

        #region Restful convention methods
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var quiz = dataProcessor.GetQuiz(context, id);

            if (quiz == null) return NotFound (new { Error = $"Quiz ID = {id} has not been found" });

            return new JsonResult(quiz, JsonSettings);
        }

        [HttpPut]
        public IActionResult Put([FromBody] QuizViewModel model) {

            if (model == null) return new StatusCodeResult(500);

            var quiz = dataProcessor.PutQuiz(context, model);

            if (quiz == null) return NotFound(new { Error = $"Quiz ID = {model.Id} has not been found" });

            return new JsonResult(quiz, JsonSettings);
           
        }

        [HttpPost]
        public IActionResult Post([FromBody] QuizViewModel model) {

            if (model == null) return new StatusCodeResult(500);

            return new JsonResult(dataProcessor.PostQuiz(context, model), JsonSettings);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {

            var result = dataProcessor.DeleteQuiz(context, id);

            if (result == ResultOperation.NotFound)
            {
                return NotFound(new
                {
                    Error = $"Quiz ID = {id} has not been found"
                });
            }
      
            return new OkResult();
        }
        #endregion

        [HttpGet("Latest/{num:int?}")]
        public IActionResult Latest(int num = 10) => new JsonResult(dataProcessor.GetQuizViewModelsList(context, num), JsonSettings);

        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10) => new JsonResult(dataProcessor.GetQuizByTitle(context, num), JsonSettings);

        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10) => new JsonResult(dataProcessor.GetQuizRandom(context, num), JsonSettings);
    }
}
