using HTF2018.Backend.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HTF2018.Backend.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageLogic _imageLogic;

        public ImagesController(IImageLogic imageLogic)
        {
            _imageLogic = imageLogic;
        }

        [HttpGet, Route("{imageId}")]
        public async Task<IActionResult> GetImage(Guid imageId)
        {
            var image = await _imageLogic.LoadImage(imageId);
            return image != null ? (IActionResult)File(image.Data, "image/png") : NotFound();
        }
    }
}