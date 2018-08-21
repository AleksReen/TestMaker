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
    public class QuestionController : BaseApiController
    {
        private IQuestionProvider dataProcessor;

        public QuestionController(
            ApplicationDbContext dbContext, 
            IQuestionProvider DataProcessor,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
            :base(dbContext, roleManager, userManager, configuration)
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
        [Authorize]
        public IActionResult Put([FromBody] QuestionViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            var question = dataProcessor.PutQuestion(DbContext, model);

            if (question == null) return NotFound(new { Error = $"Question ID = {model.Id} has not been found" });

            return new JsonResult(question, JsonSettings);

            
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] QuestionViewModel model)
        {
            if (model == null) return new StatusCodeResult(500);

            return new JsonResult(dataProcessor.PostQuestion(DbContext, model), JsonSettings);
        }

        [HttpDelete("{id}")]
        [Authorize]
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
