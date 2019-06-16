using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalEconomist.Entities.Models.Activity;
using PersonalEconomist.Services.Services.FileService;
using PersonalEconomist.Services.Stores.ActivityStore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalEconomist.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ActivityController : Controller
    {
        private readonly IActivityStore _activityStore;
        private readonly IFileService _fileService;

        public ActivityController(IActivityStore activityStore, IFileService fileService)
        {
            _activityStore = activityStore;
            _fileService = fileService;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _activityStore.GetActivities());
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _activityStore.GetActivity(id));
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> Post([FromForm]ActivityDTO value, [FromForm] IFormFile image)
        {
            if (image != null)
            {
                value.Image = _fileService.GetUniqueFileName(image.FileName);
            }

            var activity = await _activityStore.AddActivity(value);

            if (value.Image != null)
            {
                _fileService.SaveFile(image, value.Image);
            }

            return Ok(activity);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> Put(Guid id, [FromBody]ActivityDTO value, [FromForm] IFormFile image)
        {
            var oldImage = (await _activityStore.GetActivity(value.Id)).Image;

            if (image != null)
            {
                value.Image = _fileService.GetUniqueFileName(image.FileName);
            }
            else if (value.Image == null && !string.IsNullOrEmpty(oldImage))
            {
                value.Image = null;
            }

            var activity = await _activityStore.UpdateActivity(id, value);

            if (value.Image != null && oldImage != value.Image)
            {
                _fileService.UpdateFile(oldImage, image, value.Image);
            }
            else if (value.Image == null && !string.IsNullOrEmpty(oldImage))
            {
                _fileService.DeleteFile(value.Image);
            }

            return Ok(activity);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var activity = await _activityStore.DeleteActivity(id);

            _fileService.DeleteFile(activity.Image);

            return Ok(activity);
        }
    }
}
