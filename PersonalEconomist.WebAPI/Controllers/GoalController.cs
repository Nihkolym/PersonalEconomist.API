using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalEconomist.Entities.Models.Goal;
using PersonalEconomist.Services.Services.FileService;
using PersonalEconomist.Services.Services.GoalService;
using PersonalEconomist.Services.Stores.GoalStore;

namespace PersonalEconomist.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class GoalController : Controller
    {
        private readonly IGoalStore _goalStore;
        private readonly IFileService _fileService;
        private readonly IGoalService _goalService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GoalController(IGoalStore goalStore, IFileService fileService, IGoalService goalService, IHttpContextAccessor httpContextAccessor)
        {
            _goalStore = goalStore;
            _fileService = fileService;
            _goalService = goalService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("Id").Value;

            return Ok(await _goalStore.GetGoals(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _goalStore.GetGoal(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]GoalDTO value, [FromForm] IFormFile image)
        {
            value.UserId = _httpContextAccessor.HttpContext.User.FindFirst("Id").Value;

            if (image != null)
            {
                value.Image = _fileService.GetUniqueFileName(image.FileName);
            }

            var goal = await _goalStore.AddGoal(value);

            if (value.Image != null)
            {
                _fileService.SaveFile(image, value.Image);
            }

            return Ok(goal);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var goal = await _goalStore.DeleteGoal(id);

            _fileService.DeleteFile(goal.Image);

            return Ok(goal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromForm]GoalDTO value, [FromForm]IFormFile image)
        {
            var oldImage = (await _goalStore.GetGoal(value.Id)).Image;

            if (image != null)
            {
                value.Image = _fileService.GetUniqueFileName(image.FileName);
            }
            else if (value.Image == null && !string.IsNullOrEmpty(oldImage))
            {
                value.Image = null;
            }

            var goal = await _goalStore.UpdateGoal(id, value);

            if (value.Image != null && oldImage != value.Image)
            {
                _fileService.UpdateFile(oldImage, image, value.Image);
            }
            else if (value.Image == null && !string.IsNullOrEmpty(oldImage))
            {
                _fileService.DeleteFile(value.Image);
            }

            return Ok(goal);
        }

        [HttpPost("reach")]
        public async Task<IActionResult> Post([FromBody]ReachGoal reachGoal)
        {
            return Ok(await _goalService.ReachGoal(reachGoal.GoalId, reachGoal.CreditCardId));
        }
    }
}