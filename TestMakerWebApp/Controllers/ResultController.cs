using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TestMaker.Data;
using TestMaker.Data.Context;
using TestMaker.Data.Processor.Providers;
using TestMaker.Models.Data;
using TestMaker.Models.ViewModels;

namespace TestMakerWebApp.Controllers
{
    public class ResultController : BaseApiController
    {
        IResultProvider dataProcessor;       

        public ResultController(
            ApplicationDbContext dbContext, 
            IResultProvider DataProcessor,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
            :base(dbContext, roleManager, userManager, configuration)
        {
            dataProcessor = DataProcessor;
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
        [Authorize]
        public IActionResult Put([FromBody] ResultViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            var answer = dataProcessor.PutResult(DbContext, model);

            if (answer == null) return NotFound(new { Error = $"Result ID = {model.Id} has not been found" });

            return new JsonResult(answer, JsonSettings);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] ResultViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            return new JsonResult(dataProcessor.PostResult(DbContext, model), JsonSettings);
        }

        [HttpDelete("{id}")]
        [Authorize]
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
