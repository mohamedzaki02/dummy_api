using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingApp.Dtos.Photo;
using DatingApp.Helpers;
using DatingApp.Models;
using DatingApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DatingApp.Controllers
{

    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IDatingRepository<User> _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryOptions;

        private Cloudinary _Cloudinary;
        public PhotoController(IDatingRepository<User> repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryOptions)
        {
            _cloudinaryOptions = cloudinaryOptions;
            _mapper = mapper;
            _repo = repo;
            _Cloudinary = new Cloudinary(new Account(
                _cloudinaryOptions.Value.CloudName,
                _cloudinaryOptions.Value.ApiKey,
                _cloudinaryOptions.Value.ApiSecret
            ));

        }


        [HttpGet("{photoId}", Name = "GetUserPhoto")]
        public async Task<IActionResult> GetUserPhoto(int photoId)
        {
            var userPhoto = await _repo.GetUserPhoto(photoId);
            if (userPhoto == null) return NotFound();
            return Ok(_mapper.Map<PhotoForDetailsDto>(userPhoto));
        }


        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();

            var user = await _repo.GetById(userId);
            var file = photoForCreationDto.File;
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    uploadResult = _Cloudinary.Upload(new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    });
                }
            }

            photoForCreationDto.Url = uploadResult.Url.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            if (!user.Photos.Any()) photoForCreationDto.IsMain = true;

            var addedPhoto = _mapper.Map<Photo>(photoForCreationDto);

            user.Photos.Add(addedPhoto);

            if (await _repo.SaveAll())
            {
                var returnedPhoto = _mapper.Map<PhotoForDetailsDto>(addedPhoto);
                return CreatedAtRoute("GetUserPhoto", new { userId = userId, photoId = returnedPhoto.Id }, returnedPhoto);
            }
            return BadRequest("Could Not Save Photo!");
        }



        [HttpPost("{id}/main")]
        public async Task<IActionResult> UpdateMainPhoto(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();
            var user = await _repo.GetById(userId);
            if (user == null) return NotFound();
            var usrPhotos = user.Photos;
            var oldMainPhoto = usrPhotos.SingleOrDefault(p => p.IsMain);
            if (oldMainPhoto != null) oldMainPhoto.IsMain = false;
            var photoToUpdate = usrPhotos.SingleOrDefault(p => p.Id == id);
            if (photoToUpdate == null) return BadRequest("could not update user photo");
            photoToUpdate.IsMain = true;
            if (await _repo.SaveAll()) return NoContent();
            return BadRequest("update failure");
        }



    }
}