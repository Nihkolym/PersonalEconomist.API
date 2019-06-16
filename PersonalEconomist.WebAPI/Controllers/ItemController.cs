using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalEconomist.Entities.Models.Item;
using PersonalEconomist.Services.Services.FileService;
using PersonalEconomist.Services.Stores.ItemStore;

namespace PersonalEconomist.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ItemController : Controller
    {
        private readonly IItemStore _itemStore;
        private readonly IFileService _fileService;

        public ItemController(IItemStore itemStore, IFileService fileService)
        {
            _itemStore = itemStore;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _itemStore.AllItems());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _itemStore.GetItem(id));
        }

        [HttpPost]
        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> Post([FromForm] ItemDTO value, [FromForm] IFormFile image)
        {
            if (image != null)
            {
                value.Image = _fileService.GetUniqueFileName(image.FileName);
            }

            var item = await _itemStore.AddItem(value);

            if (value.Image != null)
            {
                _fileService.SaveFile(image, value.Image);
            }

            return Ok(item);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _itemStore.DeleteItem(id);

            _fileService.DeleteFile(item.Image);

            return Ok(item);
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "ApiAdmin")]
        public async Task<IActionResult> Put(Guid id, [FromBody]ItemDTO value, [FromForm] IFormFile image)
        {
            var oldImage = (await _itemStore.GetItem(value.Id)).Image;

            if (image != null)
            {
                value.Image = _fileService.GetUniqueFileName(image.FileName);
            }
            else if (value.Image == null && !string.IsNullOrEmpty(oldImage))
            {
                value.Image = null;
            }

            var item = await _itemStore.UpdateItem(id, value);
          
            if (value.Image != null && oldImage != value.Image)
            {
                _fileService.UpdateFile(oldImage, image, value.Image);
            }
            else if (value.Image == null && !string.IsNullOrEmpty(oldImage))
            {
                _fileService.DeleteFile(value.Image);
            }

            return Ok(item);
        }
    }
}
