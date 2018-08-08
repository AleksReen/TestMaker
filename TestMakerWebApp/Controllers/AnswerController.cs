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
        public IActionResult Put (AnswerViewModel m) => throw new NotImplementedException();

        [HttpPost]
        public IActionResult Post(AnswerViewModel m) => throw new NotImplementedException();

        [HttpDelete]
        public IActionResult Delete(int id) => throw new NotImplementedException();

        #endregion
    }
}
