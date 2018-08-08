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
    public class ResultController : BaseApiController
    {
        IResultProvider dataProcessor;       

        public ResultController(ApplicationDbContext dbContext, IResultProvider testDataProcessor)
            :base(dbContext)
        {
            dataProcessor = testDataProcessor;
        }

        [HttpGet("All/{quizId}")]
        public IActionResult All(int quizId) => new JsonResult(dataProcessor.GetResultViewModelsList(DbContext, quizId), JsonSettings);

        #region RESTfull convention methods

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = dataProcessor.GetResult(DbContext, id);

            if (result == null) return NotFound(new { Error = $"Result ID = {id} has not been found" });

            return new JsonResult(result, JsonSettings);
        }

        [HttpPut]
        public IActionResult Put([FromBody] ResultViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            var answer = dataProcessor.PutResult(DbContext, model);

            if (answer == null) return NotFound(new { Error = $"Result ID = {model.Id} has not been found" });

            return new JsonResult(answer, JsonSettings);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ResultViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            return new JsonResult(dataProcessor.PostResult(DbContext, model), JsonSettings);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = dataProcessor.DeleteResult(DbContext, id);

            if (result == ResultOperation.NotFound)
            {
                return NotFound(new
                {
                    Error = $"Result ID = {id} has not been found"
                });
            }

            return new OkResult();
        }

        #endregion
    }
}
