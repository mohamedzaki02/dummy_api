using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Dtos.Photo
{
    public class PhotoForCreationDto
    {
        public PhotoForCreationDto()
        {
            CreatedAt = DateTime.Now;
        }
        public string Url { get; set; }
        public string Description { get; set; }
        public IFormFile File { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PublicId { get; set; }
        public bool IsMain { get; set; }
    }
}