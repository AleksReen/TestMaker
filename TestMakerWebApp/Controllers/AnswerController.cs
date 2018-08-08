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
    public class AnswerController : Controller
    {
        private IAnswerProvider dataProcessor;
        private ApplicationDbContext context;
        private JsonSerializerSettings JsonSettings { get; set; } = new JsonSerializerSettings { Formatting = Formatting.Indented };

        public AnswerController(ApplicationDbContext dbContext, IDataProcessor DataProcessor)
        {
            dataProcessor = DataProcessor;
            context = dbContext;
        }

        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId) => new JsonResult(dataProcessor.GetAnswerViewModelsList(context, questionId), JsonSettings);

        #region RESTfull convention methods

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var answer = dataProcessor.GetAnswer(context, id);

            if (answer == null) return NotFound(new { Error = $"Answer ID = {id} has not been found" });

            return new JsonResult(answer, JsonSettings);            
        }

        [HttpPut]
        public IActionResult Put([FromBody] AnswerViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            var answer = dataProcessor.PutAnswer(context, model);

            if (answer == null) return NotFound(new { Error = $"Answer ID = {model.Id} has not been found" });

            return new JsonResult(answer, JsonSettings);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AnswerViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            return new JsonResult(dataProcessor.PostAnswer(context, model), JsonSettings);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = dataProcessor.DeleteAnswer(context, id);

            if (result == ResultOperation.NotFound)
            {
                return NotFound(new
                {
                    Error = $"Answer ID = {id} has not been found"
                });
            }

            return new OkResult();
        }

        #endregion
    }
}
