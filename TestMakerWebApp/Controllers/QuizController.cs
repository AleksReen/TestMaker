using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestMaker.Data;
using TestMaker.Data.Context;
using TestMaker.Data.Processor.Providers;
using TestMaker.Models.ViewModels;

namespace TestMakerWebApp.Controllers
{
    public class QuizController : BaseApiController
    {
        private IQuizProvider dataProcessor;

        public QuizController(ApplicationDbContext dbContext, IQuizProvider DataProcessor)
            :base(dbContext)
        {
            dataProcessor = DataProcessor;
        }

        #region Restful convention methods
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var quiz = dataProcessor.GetQuiz(DbContext, id);

            if (quiz == null) return NotFound (new { Error = $"Quiz ID = {id} has not been found" });

            return new JsonResult(quiz, JsonSettings);
        }

        [HttpPut]
        public IActionResult Put([FromBody] QuizViewModel model) {

            if (model == null) return new StatusCodeResult(500);

            var quiz = dataProcessor.PutQuiz(DbContext, model);

            if (quiz == null) return NotFound(new { Error = $"Quiz ID = {model.Id} has not been found" });

            return new JsonResult(quiz, JsonSettings);
           
        }

        [HttpPost]
        public IActionResult Post([FromBody] QuizViewModel model) {

            if (model == null) return new StatusCodeResult(500);

            return new JsonResult(dataProcessor.PostQuiz(DbContext, model), JsonSettings);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {

            var result = dataProcessor.DeleteQuiz(DbContext, id);

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
        public IActionResult Latest(int num = 10) => new JsonResult(dataProcessor.GetQuizViewModelsList(DbContext, num), JsonSettings);

        [HttpGet("ByTitle/{num:int?}")]
        public IActionResult ByTitle(int num = 10) => new JsonResult(dataProcessor.GetQuizByTitle(DbContext, num), JsonSettings);

        [HttpGet("Random/{num:int?}")]
        public IActionResult Random(int num = 10) => new JsonResult(dataProcessor.GetQuizRandom(DbContext, num), JsonSettings);
    }
}
