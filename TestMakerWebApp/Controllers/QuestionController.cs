using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMaker.Data;
using TestMaker.Data.Context;
using TestMaker.Data.Proccesor;
using TestMaker.Data.Processor.Providers;
using TestMaker.Models.ViewModels;

namespace TestMakerWebApp.Controllers
{
    [Route("api/[controller]")]
    public class QuestionController : Controller
    {
        private IQuestionProvider dataProcessor;
        private ApplicationDbContext context;
        private JsonSerializerSettings JsonSettings { get; set; } = new JsonSerializerSettings { Formatting = Formatting.Indented };

        public QuestionController(ApplicationDbContext dbContext, IQuestionProvider DataProcessor)
        {
            dataProcessor = DataProcessor;
            context = dbContext;
        }

        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId) => new JsonResult(dataProcessor.GetQuestionViewModelsList(context, quizId), JsonSettings);

        #region RESTfull convention methods

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var question = dataProcessor.GetQuestion(context, id);
            if (question == null) return NotFound(new { Error = $"Question ID = {id} has not been found" });           
            return new JsonResult(question, JsonSettings);
        }

        [HttpPut]
        public IActionResult Put([FromBody] QuestionViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            return new JsonResult(dataProcessor.PutQuestion(context, model), JsonSettings);
        }

        [HttpPost]
        public IActionResult Post(QuestionViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            var question = dataProcessor.PostQuestion(context, model);

            if (question == null) return NotFound(new { Error = $"Question ID = {model.Id} has not been found" });

            return new JsonResult(question, JsonSettings);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = dataProcessor.DeleteQuestion(context, id);

            if (result == ResultOperation.NotFound)
            {
                return NotFound(new
                {
                    Error = $"Question ID = {id} has not been found"
                });
            }

            return new OkResult();
        }

        #endregion
    }
}
