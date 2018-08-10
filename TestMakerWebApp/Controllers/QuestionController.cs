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
    public class QuestionController : BaseApiController
    {
        private IQuestionProvider dataProcessor;

        public QuestionController(ApplicationDbContext dbContext, IQuestionProvider DataProcessor)
            :base(dbContext)
        {
            dataProcessor = DataProcessor;
        }

        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId) => new JsonResult(dataProcessor.GetQuestionViewModelsList(DbContext, quizId), JsonSettings);

        #region RESTfull convention methods

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var question = dataProcessor.GetQuestion(DbContext, id);
            if (question == null) return NotFound(new { Error = $"Question ID = {id} has not been found" });           
            return new JsonResult(question, JsonSettings);
        }

        [HttpPut]
        public IActionResult Put([FromBody] QuestionViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            var question = dataProcessor.PutQuestion(DbContext, model);

            if (question == null) return NotFound(new { Error = $"Question ID = {model.Id} has not been found" });

            return new JsonResult(question, JsonSettings);

            
        }

        [HttpPost]
        public IActionResult Post([FromBody] QuestionViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            return new JsonResult(dataProcessor.PostQuestion(DbContext, model), JsonSettings);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = dataProcessor.DeleteQuestion(DbContext, id);

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
