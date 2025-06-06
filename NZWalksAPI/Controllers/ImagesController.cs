using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;

namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRespository imgrepo;

        public ImagesController(IImageRespository imgrepo)
        {
            this.imgrepo = imgrepo;
        }
        //POST:api/Images/Upload
        [HttpPost]
        [Route("Upload")]

        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto imgup)
        {
            ValidateFileUpload(imgup);
            if(ModelState.IsValid)
            {
                //convert dto to domain model

                var imageDM = new Image
                {
                    File = imgup.File,
                    FileExtension = Path.GetExtension(imgup.File.FileName),
                    FileSizeInBytes=imgup.File.Length,
                    FileName=imgup.FileName,
                    FileDescription=imgup.FileDescription
                };


                //user repsitry to upload image
                await imgrepo.Upload(imageDM);

                return Ok(imageDM);
            }
            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageUploadRequestDto imgup)
        {
            var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };

            if (!allowedExtensions.Contains(Path.GetExtension(imgup.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if(imgup.File.Length>1048570)
            {
                ModelState.AddModelError("file", "File size more than 10MB,please upload a smaller file.");

            }
        }

    }
}
