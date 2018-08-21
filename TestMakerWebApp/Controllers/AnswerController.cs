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
    public class AnswerController : BaseApiController
    {
        private IAnswerProvider dataProcessor;

        public AnswerController(
            ApplicationDbContext dbContext,
            IAnswerProvider DataProcessor,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)            
            :base(dbContext, roleManager,userManager,configuration )
        {
            dataProcessor = DataProcessor;
        }

        [HttpGet("All/{questionId}")]
        public IActionResult All(int questionId) => new JsonResult(dataProcessor.GetAnswerViewModelsList(DbContext, questionId), JsonSettings);

        #region RESTfull convention methods

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var answer = dataProcessor.GetAnswer(DbContext, id);

            if (answer == null) return NotFound(new { Error = $"Answer ID = {id} has not been found" });

            return new JsonResult(answer, JsonSettings);            
        }

        [HttpPut]
        [Authorize]
        public IActionResult Put([FromBody] AnswerViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            var answer = dataProcessor.PutAnswer(DbContext, model);

            if (answer == null) return NotFound(new { Error = $"Answer ID = {model.Id} has not been found" });

            return new JsonResult(answer, JsonSettings);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] AnswerViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            return new JsonResult(dataProcessor.PostAnswer(DbContext, model), JsonSettings);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var result = dataProcessor.DeleteAnswer(DbContext, id);

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
